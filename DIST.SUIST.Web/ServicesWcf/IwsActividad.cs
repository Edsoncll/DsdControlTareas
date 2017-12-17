using DIST.SUIST.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DIST.SUIST.Web.ServicesWcf
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IwsActividad" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IwsActividad
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Evento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<EventoBE> ListarEventos();
                
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Evento", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        MensajeWrapperBE GuardarActividad(ActividadBE oActividad);
    }
}
