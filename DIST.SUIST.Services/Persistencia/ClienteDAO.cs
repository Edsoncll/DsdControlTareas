using DIST.SUIST.BE;
using DIST.SUIST.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIST.SUIST.Services
{
    public class ClienteDAO
    {
        public List<ClienteDC> ListarClientes()
        {
            ClienteBL oClienteBL = new ClienteBL();

            try
            {
                return (from c
                        in oClienteBL.ListarClientes()
                        select new ClienteDC()
                        {
                            IdCliente = c.IdCliente,
                            Prefijo = c.Prefijo,
                            DocumentoIdentidad = c.DocumentoIdentidad,
                            NombreCompleto = c.NombreCompleto,
                            Email = c.Email,
                            Telefono = c.Telefono,
                            SitioWeb = c.SitioWeb,
                            Direccion = c.Direccion,
                            FechaInicioContrato = c.FechaInicioContrato,
                            FechaFinContrato = c.FechaFinContrato,
                            Color = c.Color
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oClienteBL = null;
            }
        }

        public List<TipoClienteDC> ListarTipoClientes()
        {
            ClienteBL oClienteBL = new ClienteBL();

            try
            {
                return (from c
                        in oClienteBL.ListarTipoClientes()
                        select new TipoClienteDC()
                        {
                            IdTipoCliente = c.IdTipoCliente,
                            Descripcion = c.Descripcion
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oClienteBL = null;
            }
        }

        public ClienteDC ObtenerCliente(int IdCliente, string DocumentoIdentidad)
        {
            ClienteBL oClienteBL = new ClienteBL();

            try
            {
                ClienteBE objCliente = oClienteBL.ObtenerCliente(IdCliente, DocumentoIdentidad);

                if (string.IsNullOrEmpty(objCliente.DocumentoIdentidad))
                {
                    return new ClienteDC();
                }

                return new ClienteDC
                {
                    IdCliente = objCliente.IdCliente,
                    Prefijo = objCliente.Prefijo,
                    DocumentoIdentidad = objCliente.DocumentoIdentidad,
                    NombreCompleto = objCliente.NombreCompleto,
                    Email = objCliente.Email,
                    Telefono = objCliente.Telefono,
                    SitioWeb = objCliente.SitioWeb,
                    Direccion = objCliente.Direccion,
                    FechaInicioContrato = objCliente.FechaInicioContrato,
                    FechaFinContrato = objCliente.FechaFinContrato,
                    Color = objCliente.Color,
                    //TipoCliente = objCliente.TipoCliente
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oClienteBL = null;
            }
        }

        public bool GuardarCliente(ClienteDC objCliente)
        {
            ClienteBL objClienteBL = new ClienteBL();

            try
            {
                return objClienteBL.GuardarCliente(new ClienteBE
                {
                    IdCliente = objCliente.IdCliente,
                    Prefijo = objCliente.Prefijo,
                    DocumentoIdentidad = objCliente.DocumentoIdentidad,
                    NombreCompleto = objCliente.NombreCompleto,
                    Email = objCliente.Email,
                    Telefono = objCliente.Telefono,
                    SitioWeb = objCliente.SitioWeb,
                    Direccion = objCliente.Direccion,
                    FechaInicioContrato = objCliente.FechaInicioContrato,
                    FechaFinContrato = objCliente.FechaFinContrato,
                    Color = objCliente.Color
                }, out string mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarCliente(int IdCliente)
        {
            ClienteBL objClienteBL = new ClienteBL();

            try
            {
                return objClienteBL.EliminarCliente(IdCliente, out string mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}