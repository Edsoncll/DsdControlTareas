using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DIST.SUIST.BE;
using System.Web;
using DIST.SUIST.BL;
using DIST.SUIST.Web;

namespace DIST.SUIST.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "wsActividad" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione wsActividad.svc o wsActividad.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class wsActividad : IwsActividad
    {
        public MensajeWrapperBE EliminarActividad(int IdActividad)
        {
            throw new NotImplementedException();
        }

        public MensajeWrapperBE GuardarActividad(ActividadBE oActividad)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un error inesperado");

            try
            {
                using (ActividadBL objActividadBL = new ActividadBL())
                {
                    oActividad.Auditoria = HttpContext.Current.Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objActividadBL.GuardarActividad(oActividad, out string mensajeout))
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

        public List<EventoBE> ListarEventos()
        {
            List<ActividadBE> lstActividad = new List<ActividadBE>();
            List<EventoBE> lstEventos = new List<EventoBE>();

            try
            {
                using (ActividadBL objActividadBL = new ActividadBL())
                {
                    PerfilBE perfil = HttpContext.Current.Session[Constantes.Sesion_Perfil] as PerfilBE;
                    int IdUsuario = (int)HttpContext.Current.Session[Constantes.Sesion_IdUsuario];

                    switch (perfil.IdPerfil)
                    {
                        case (int)EnumeradoresBE.enumPerfiles.Administrador:
                        case (int)EnumeradoresBE.enumPerfiles.Jefe:
                        case (int)EnumeradoresBE.enumPerfiles.Secretaria:
                            IdUsuario = 0;
                            break;
                    }

                    lstActividad = objActividadBL.ListarActividades(IdUsuario);
                    HttpContext.Current.Session[Constantes.Session_ListaActividades] = lstActividad;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (lstActividad.Count > 0)
            {
                foreach (ActividadBE objActividadBE in lstActividad)
                {
                    EventoBE objEventoBE = new EventoBE
                    {
                        id = objActividadBE.IdActividad,
                        start = objActividadBE.FechaInicio.Value,
                        end = objActividadBE.FechaFin.Value,
                        title = objActividadBE.Cliente.NombreCompleto,
                        description = objActividadBE.TipoActividad.Nombre
                    };

                    lstEventos.Add(objEventoBE);
                }
            }

            return lstEventos;
        }

        public List<RecursosEventoBE> ListarRecursos()
        {
            throw new NotImplementedException();
        }

        public double ObtenerMontoContrato(int idCliente)
        {
            throw new NotImplementedException();
        }
    }
}
