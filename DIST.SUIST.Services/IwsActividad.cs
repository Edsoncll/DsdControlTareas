using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DIST.SUIST.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IwsActividad" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IwsActividad
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Evento", RequestFormat = WebMessageFormat.Json)]
        List<EventoBE> ListarEventos();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Recurso", RequestFormat = WebMessageFormat.Json)]
        List<RecursosEventoBE> ListarRecursos();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Evento/{idCliente}", RequestFormat = WebMessageFormat.Json)]
        double ObtenerMontoContrato(int idCliente);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Evento", RequestFormat = WebMessageFormat.Json)]
        MensajeWrapperBE GuardarActividad(ActividadBE oActividad);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Evento", RequestFormat = WebMessageFormat.Json)]
        MensajeWrapperBE EliminarActividad(int IdActividad);
    }
}
