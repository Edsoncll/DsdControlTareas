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
    /// Descripción breve de wsGastos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsGasto : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ListarGastos()
        {
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<GastoBE> lstGasto = new List<GastoBE>();
            List<ListGastosBE> lstListGastosBE = new List<ListGastosBE>();

            try
            {
                using (GastoBL objGastoBL = new GastoBL())
                {
                    lstGasto = objGastoBL.ListarGastos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                objMwResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstGasto.Count > 0)
            {
                foreach (GastoBE objGastoBE in lstGasto)
                {
                    ListGastosBE oListGastosBE = new ListGastosBE()
                    {
                        col_IdGasto = objGastoBE.IdGasto != 0 ? objGastoBE.IdGasto : 0,
                        col_NombreCliente = objGastoBE.Cliente.NombreCompleto ?? "",
                        col_NombreProyecto = objGastoBE.Proyecto.NombreProyecto ?? "",
                        col_NombreAbogado = objGastoBE.Usuario.NombreCompleto ?? "",
                        col_Fecha = objGastoBE.Fecha != null ? objGastoBE.Fecha.Value.ToString("dd/MM/yyyy") : "",
                        col_Monto = objGastoBE.Monto != 0 ? objGastoBE.Monto.ToString() : ""
                    };
                    lstListGastosBE.Add(oListGastosBE);
                }

                objMwResultado.Resultado = "OK";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListGastosBE, Formatting.Indented);
            }
            else
            {
                objMwResultado.Mensaje = "No se encontraron registros solicitados";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListGastosBE, Formatting.Indented);
            }

            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarGasto(GastoBE oGasto)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (GastoBL objGastoBL = new GastoBL())
                {
                    string mensajeout;

                    oGasto.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objGastoBL.GuardarGasto(oGasto, out mensajeout))
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
        public MensajeWrapperBE EliminarGasto(int IdGasto)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (GastoBL objGastoBL = new GastoBL())
                {
                    string mensajeout;

                    if (objGastoBL.EliminarGasto(IdGasto, out mensajeout))
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
