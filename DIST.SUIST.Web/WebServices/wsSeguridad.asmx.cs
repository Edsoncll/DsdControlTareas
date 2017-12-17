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
    /// Descripción breve de wsSeguridad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsSeguridad : WebService
    {
        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE LogearUsuario(UsuarioBE objUsuario)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };

            using (SeguridadBL objSeguridadBL = new SeguridadBL())
            {
                UsuarioBE oUsuario = new UsuarioBE();

                if (ManejoColas.ObtenerCola("Logueo") == 3)
                {
                    objMwResultado.Resultado = "ERROR";
                    objMwResultado.Mensaje = "Usted ah superado la cantidad de intentos";

                    goto Termino;
                }

                oUsuario = objSeguridadBL.ValidarUsuario(objUsuario);

                if (oUsuario.IdUsuario != 0)
                    goto GetSesion;

                ManejoColas.AgregarCola("Logueo", objUsuario.Usuario);
                
                objMwResultado.Resultado = "ERROR";
                objMwResultado.Mensaje = "Credenciales ingresadas no son correctas o se encuentran inhabilitadas";

                goto Termino;

                GetSesion:
                using (SeguridadBL oSeguridadBL = new SeguridadBL())
                {
                    Session[Constantes.USER_SESSION] = oUsuario;
                    Session[Constantes.Sesion_IdUsuario] = oUsuario.IdUsuario;
                    Session[Constantes.Sesion_Usuario] = oUsuario.Usuario;
                    Session[Constantes.Sesion_NombreUsuario] = oUsuario.NombreCompleto;
                    Session[Constantes.Sesion_Perfil] = oUsuario.Perfil;
                    Session[Constantes.Sesion_Empresa] = oUsuario.Empresa;
                    Session[Constantes.Sesion_Auditoria] = new AuditoriaBE { Usuario = oUsuario.Usuario };

                    objMwResultado.Resultado = "OK";
                    objMwResultado.Mensaje = "Credenciales correctas";

                    goto Termino;
                }
            }
            Termino:
            return objMwResultado;
        }

        [WebMethod(EnableSession = true)]
        public MensajeWrapperBE ActualizarContraseniaUsuario(UsuarioBE oUsuario)
        {
            string strError = string.Empty;
            MensajeWrapperBE objMwResultado = new MensajeWrapperBE { Resultado = "ER", Mensaje = "" };
            objMwResultado.Mensaje = HttpUtility.HtmlEncode("Ocurrio un problema inesperado");

            try
            {
                using (SeguridadBL objSeguridadBL = new SeguridadBL())
                {
                    string mensajeout;

                    oUsuario.Auditoria = Session[Constantes.Sesion_Auditoria] as AuditoriaBE;

                    if (objSeguridadBL.ActualizarContraseniaUsuario(oUsuario, out mensajeout))
                    {
                        UsuarioBE objUsuarioBE = Session[Constantes.USER_SESSION] as UsuarioBE;
                        objUsuarioBE.Contrasenia = oUsuario.Contrasenia;
                        Session[Constantes.USER_SESSION] = objUsuarioBE;

                        objMwResultado.Resultado = "OK";
                        objMwResultado.Mensaje = mensajeout;
                        goto Termino;
                    }
                    else
                    {
                        objMwResultado.Mensaje = mensajeout;
                        goto Termino;
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
