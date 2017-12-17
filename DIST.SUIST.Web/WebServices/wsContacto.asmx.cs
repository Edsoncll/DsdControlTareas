﻿using Newtonsoft.Json;
using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DIST.SUIST.Web
{
    /// <summary>
    /// Descripción breve de wsContacto
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsContacto : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ListarContacto(int IdCliente)
        {
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ContactoBE> lstContacto = new List<ContactoBE>();
            List<ListContactosBE> lstListContactosBE = new List<ListContactosBE>();

            try
            {
                using (ContactoBL objContactoBL = new ContactoBL())
                {
                    lstContacto = objContactoBL.ListarContactos(IdCliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                objMwResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstContacto.Count > 0)
            {
                foreach (ContactoBE objContactoBE in lstContacto)
                {
                    ListContactosBE oListContactosBE = new ListContactosBE();

                    oListContactosBE.col_IdContacto = objContactoBE.IdContacto != 0 ? objContactoBE.IdContacto : 0;
                    oListContactosBE.col_Principal = objContactoBE.Principal == true ? 1 : 0;
                    oListContactosBE.col_NombreCompleto = !string.IsNullOrEmpty(objContactoBE.Nombre) ? objContactoBE.Nombre : "";

                    lstListContactosBE.Add(oListContactosBE);
                }

                objMwResultado.Resultado = "OK";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListContactosBE, Formatting.Indented);
            }
            else
            {
                objMwResultado.Mensaje = "No se encontraron registros solicitados";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListContactosBE, Formatting.Indented);
            }

            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public List<ContactoBE> ListarContactoCliente(int idCliente)
        {
            List<ContactoBE> lstContacto = new List<ContactoBE>();

            try
            {
                using (ContactoBL objContactoBL = new ContactoBL())
                {
                    return objContactoBL.ListarContactos(idCliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<ContactoBE>();
            }
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarContacto(ContactoBE oContacto)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ContactoBL objContactoBL = new ContactoBL())
                {
                    string mensajeout;

                    oContacto.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objContactoBL.GuardarContacto(oContacto, out mensajeout))
                    {
                        objMwResultado.Resultado = "OK";
                        objMwResultado.Mensaje = HttpUtility.HtmlEncode(mensajeout);
                        goto Termino;
                    }
                    else
                    {
                        objMwResultado.Mensaje = mensajeout;
                    }
                }
            }
            catch (Exception ex)
            {
                objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un problema guardando la información.");
                throw ex;
            }

            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE EliminarContacto(int IdContacto)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ContactoBL objContactoBL = new ContactoBL())
                {
                    string mensajeout;

                    if (objContactoBL.EliminarContacto(IdContacto, out mensajeout))
                    {
                        objMwResultado.Resultado = "OK";
                        objMwResultado.Mensaje = HttpUtility.HtmlEncode(mensajeout);
                        goto Termino;
                    }
                    else
                    {
                        objMwResultado.Mensaje = mensajeout;
                    }
                }
            }
            catch (Exception ex)
            {
                objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un problema guardando la información.");
                throw ex;
            }

            Termino:
            return objMwResultado;
        }
    }
}
