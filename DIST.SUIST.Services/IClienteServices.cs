using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DIST.SUIST.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IClienteServices" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IClienteServices
    {
        [OperationContract]
        List<ClienteDC> ListarClientes();

        [OperationContract]
        List<TipoClienteDC> ListarTipoClientes();

        [FaultContract(typeof(ClienteExcption))]
        [OperationContract]
        ClienteDC ObtenerCliente(int IdCliente, string DocumentoIdentidad);

        [OperationContract]
        bool GuardarCliente(ClienteDC objCliente);

        [OperationContract]
        bool EliminarCliente(int IdCliente);
    }
}
