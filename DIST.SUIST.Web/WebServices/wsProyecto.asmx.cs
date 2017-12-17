using Newtonsoft.Json;
using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DIST.SUIST.Web
{
    /// <summary>
    /// Descripción breve de wsProyecto
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsProyecto : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ListarProyecto()
        {
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ProyectoBE> lstProyecto = new List<ProyectoBE>();
            List<ListProyectosBE> lstListProyectosBE = new List<ListProyectosBE>();

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    lstProyecto = objProyectoBL.ListarProyectos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                objMwResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstProyecto.Count > 0)
            {
                foreach (ProyectoBE objProyectoBE in lstProyecto)
                {
                    ListProyectosBE oListProyectosBE = new ListProyectosBE();

                    oListProyectosBE.col_IdProyecto = objProyectoBE.IdProyecto != 0 ? objProyectoBE.IdProyecto : 0;
                    oListProyectosBE.col_Cliente = !string.IsNullOrEmpty(objProyectoBE.Cliente.NombreCompleto) ? objProyectoBE.Cliente.NombreCompleto : "";
                    oListProyectosBE.col_NombreProyecto = !string.IsNullOrEmpty(objProyectoBE.NombreProyecto) ? objProyectoBE.NombreProyecto : "";
                    oListProyectosBE.col_Precio = objProyectoBE.Precio != 0 ? objProyectoBE.Precio.ToString() : "";

                    lstListProyectosBE.Add(oListProyectosBE);
                }

                objMwResultado.Resultado = "OK";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListProyectosBE, Formatting.Indented);
            }
            else
            {
                objMwResultado.Mensaje = "No se encontraron registros solicitados";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListProyectosBE, Formatting.Indented);
            }

            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public List<ProyectoBE> ListarProyectoCliente(int idCliente)
        {
            List<ProyectoBE> lstProyecto = new List<ProyectoBE>();
            
            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    return objProyectoBL.ListarProyectosCliente(idCliente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<ProyectoBE>();
            }
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ExportarProyecto()
        {
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<ProyectoBE> lstProyecto = new List<ProyectoBE>();
            List<ListProyectosBE> lstListProyectosBE = new List<ListProyectosBE>();

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    lstProyecto = objProyectoBL.ListarProyectos();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                objMwResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstProyecto.Count > 0)
            {
                int cont = 1;

                foreach (ProyectoBE objProyectoBE in lstProyecto)
                {
                    ListProyectosBE oListProyectosBE = new ListProyectosBE();

                    oListProyectosBE.Nro = cont;
                    oListProyectosBE.col_IdProyecto = objProyectoBE.IdProyecto != 0 ? objProyectoBE.IdProyecto : 0;
                    oListProyectosBE.col_Cliente = !string.IsNullOrEmpty(objProyectoBE.Cliente.NombreCompleto) ? objProyectoBE.Cliente.NombreCompleto : "";
                    oListProyectosBE.col_NombreProyecto = !string.IsNullOrEmpty(objProyectoBE.NombreProyecto) ? objProyectoBE.NombreProyecto : "";
                    oListProyectosBE.col_Precio = objProyectoBE.Precio != 0 ? objProyectoBE.Precio.ToString() : "";

                    lstListProyectosBE.Add(oListProyectosBE);
                    cont++;
                }

                objMwResultado.Resultado = "OK";

                DataTable dtProyectos = Globales.ToDataTable(lstListProyectosBE);

                //Crear cabecera
                dtProyectos.DefaultView.Sort = "Nro ASC";
                dtProyectos.Columns["Nro"].ColumnName = "Nº";
                dtProyectos.Columns.Remove("col_IdProyecto");
                dtProyectos.Columns["col_Cliente"].ColumnName = "Cliente";
                dtProyectos.Columns["col_NombreProyecto"].ColumnName = "Nombre";
                dtProyectos.Columns["col_Precio"].ColumnName = "Precio";

                Session[Constantes.Sesion_DtExcel] = dtProyectos;
            }
            else
            {
                objMwResultado.Mensaje = "No se encontraron registros solicitados";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListProyectosBE, Formatting.Indented);
            }

            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarProyecto(ProyectoBE oProyecto)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    string mensajeout;

                    oProyecto.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objProyectoBL.GuardarProyecto(oProyecto, out mensajeout))
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
        public MensajeWrapperBE EliminarProyecto(int IdProyecto)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ProyectoBL objProyectoBL = new ProyectoBL())
                {
                    string mensajeout;

                    if (objProyectoBL.EliminarProyecto(IdProyecto, out mensajeout))
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
