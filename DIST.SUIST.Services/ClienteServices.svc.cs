using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
    
namespace DIST.SUIST.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ClienteServices" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ClienteServices.svc o ClienteServices.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ClienteServices : IClienteServices
    {
        public bool EliminarCliente(int IdCliente)
        {
            return new ClienteDAO().EliminarCliente(IdCliente);
        }

        public bool GuardarCliente(ClienteDC objCliente)
        {
            bool result = false;

            if (objCliente.IdCliente == 0 && !string.IsNullOrEmpty(new ClienteDAO().ObtenerCliente(0, objCliente.DocumentoIdentidad).DocumentoIdentidad))
                throw new FaultException<ClienteExcption>(new ClienteExcption()
                {
                    CodError = 2,
                    DescError = "Cliente Duplicado"
                }, new FaultReason("Ya existe un cliente con num. documento = " + objCliente.DocumentoIdentidad));
            else
                result = new ClienteDAO().GuardarCliente(objCliente);

            if (!result)
                throw new FaultException<ClienteExcption>(new ClienteExcption()
                {
                    CodError = 3,
                    DescError = "Error guardado"
                }, new FaultReason("Ocurrio un error inesperado al intentar guardar al Cliente."));

            return result;
        }

        public List<ClienteDC> ListarClientes()
        {
            List<ClienteDC> lstCliente = new ClienteDAO().ListarClientes();

            if (lstCliente.Count.Equals(0))
            {
                throw new FaultException<ClienteExcption>(new ClienteExcption()
                {
                    CodError = 1,
                    DescError = "No found error"
                }, new FaultReason("No se encontraron registros"));
            }

            return lstCliente;
        }

        public List<TipoClienteDC> ListarTipoClientes()
        {
            List<TipoClienteDC> lstTipoCliente = new ClienteDAO().ListarTipoClientes();

            if (lstTipoCliente.Count.Equals(0))
            {
                throw new FaultException<ClienteExcption>(new ClienteExcption()
                {
                    CodError = 1,
                    DescError = "No found error"
                }, new FaultReason("No se encontraron registros"));
            }

            return lstTipoCliente;
        }

        public ClienteDC ObtenerCliente(int IdCliente, string DocumentoIdentidad)
        {
            ClienteDC objCliente = new ClienteDAO().ObtenerCliente(IdCliente, DocumentoIdentidad);

            if (string.IsNullOrEmpty(objCliente.DocumentoIdentidad))
            {
                throw new FaultException<ClienteExcption>(new ClienteExcption()
                {
                    CodError = 1,
                    DescError = "No found error"
                }, new FaultReason("No se encontro cliente"));
            }

            return objCliente;
        }
    }
}
