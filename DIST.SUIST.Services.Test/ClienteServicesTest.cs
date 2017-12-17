using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DIST.SUIST.Services.Test
{
    [TestClass]
    public class ClienteServicesTest
    {
        [TestMethod]
        public void EliminarClienteTest()
        {
            ClienteServices.ClienteServicesClient clienteServices = new ClienteServices.ClienteServicesClient();
            bool result = clienteServices.EliminarCliente(1);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void GuardarClienteTest()
        {
            ClienteServices.ClienteServicesClient clienteServices = new ClienteServices.ClienteServicesClient();
            bool result = clienteServices.GuardarCliente(new ClienteServices.ClienteDC()
            {
                // TipoCliente = new ClienteServices.TipoClienteDC { IdTipoCliente = 4 },
                //Auditoria = new ClienteServices.AuditoriaDC { Usuario = "admin" },
                Prefijo = "Sr.",
                DocumentoIdentidad = "41926563",
                NombreCompleto = "NEPTUNO S.A.",
                Telefono = "666666",

            });
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void ObtenerCliente()
        {
            ClienteServices.ClienteServicesClient clienteServices = new ClienteServices.ClienteServicesClient();
            ClienteServices.ClienteDC clienteDC = clienteServices.ObtenerCliente(1, string.Empty);
            Assert.IsNotNull(clienteDC);
            Assert.AreEqual(clienteDC.IdCliente, 1);
        }
    }
}
