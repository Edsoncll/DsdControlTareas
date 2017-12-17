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
    /// Descripción breve de wsTipoActividad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsTipoActividad : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ListarTipoActividad()
        {
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<TipoActividadBE> lstTipoActividad = new List<TipoActividadBE>();
            List<ListTipoActividadesBE> lstListTipoActividadsBE = new List<ListTipoActividadesBE>();

            try
            {
                using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
                {
                    lstTipoActividad = objTipoActividadBL.ListarTipoActividades();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                objMwResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstTipoActividad.Count > 0)
            {
                foreach (TipoActividadBE objTipoActividadBE in lstTipoActividad)
                {
                    ListTipoActividadesBE oListTipoActividadsBE = new ListTipoActividadesBE();

                    oListTipoActividadsBE.col_IdTipoActividad = objTipoActividadBE.IdTipoActividad != 0 ? objTipoActividadBE.IdTipoActividad : 0;
                    oListTipoActividadsBE.col_Nombre = !string.IsNullOrEmpty(objTipoActividadBE.Nombre) ? objTipoActividadBE.Nombre : "";

                    lstListTipoActividadsBE.Add(oListTipoActividadsBE);
                }

                objMwResultado.Resultado = "OK";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListTipoActividadsBE, Formatting.Indented);
            }
            else
            {
                objMwResultado.Mensaje = "No se encontraron registros solicitados";
                objMwResultado.Listado = JsonConvert.SerializeObject(lstListTipoActividadsBE, Formatting.Indented);
            }

            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public List<TipoActividadBE> ListarComboTipoActividad()
        {
            try
            {
                using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
                {
                    return objTipoActividadBL.ListarTipoActividades();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<TipoActividadBE>();
            }
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ExportarTipoActividad()
        {
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            List<TipoActividadBE> lstTipoActividad = new List<TipoActividadBE>();
            List<ListTipoActividadesBE> lstListTipoActividadsBE = new List<ListTipoActividadesBE>();

            try
            {
                using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
                {
                    lstTipoActividad = objTipoActividadBL.ListarTipoActividades();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                objMwResultado.Mensaje = "Ocurrio un error inesperado";
                goto Termino;
            }

            if (lstTipoActividad.Count > 0)
            {
                ListTipoActividadesBE oListTipoActividadBE;
                int cont = 1;

                foreach (TipoActividadBE objTipoActividadBE in lstTipoActividad)
                {
                    oListTipoActividadBE = new ListTipoActividadesBE();

                    oListTipoActividadBE.Nro = cont;
                    oListTipoActividadBE.col_Nombre = !string.IsNullOrEmpty(objTipoActividadBE.Nombre) ? objTipoActividadBE.Nombre : "";
                    oListTipoActividadBE.col_Precio = (objTipoActividadBE.Precio.Monto > 0) ? objTipoActividadBE.Precio.Monto.ToString().Trim() : "";

                    lstListTipoActividadsBE.Add(oListTipoActividadBE);
                    cont++;
                }

                objMwResultado.Resultado = "OK";

                DataTable dtTipoActividades = Globales.ToDataTable(lstListTipoActividadsBE);

                //Crear cabecera
                dtTipoActividades.DefaultView.Sort = "Nro ASC";
                dtTipoActividades.Columns["Nro"].ColumnName = "Nº";
                dtTipoActividades.Columns.Remove("col_IdTipoActividad");
                dtTipoActividades.Columns["col_Nombre"].ColumnName = "Nombre";

                Session[Constantes.Sesion_DtExcel] = dtTipoActividades;
            }
            else
            {
                objMwResultado.Mensaje = "No se puede realizar la exportación";
            }

            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE GuardarTipoActividad(TipoActividadBE oTipoActividad)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
                {
                    string mensajeout;

                    oTipoActividad.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objTipoActividadBL.GuardarTipoActividad(oTipoActividad, out mensajeout))
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
        public MensajeWrapperBE EliminarTipoActividad(int IdTipoActividad)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (TipoActividadBL objTipoActividadBL = new TipoActividadBL())
                {
                    string mensajeout;

                    if (objTipoActividadBL.EliminarTipoActividad(IdTipoActividad, out mensajeout))
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
