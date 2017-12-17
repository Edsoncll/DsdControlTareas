using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DIST.SUIST.Services
{
    [DataContract]
    public class ClienteDC
    {
        #region Variables

        [DataMember(IsRequired = false)]
        public int IdCliente { get; set; }
        [DataMember]
        public string Prefijo { get; set; }
        [DataMember]
        public string DocumentoIdentidad { get; set; }
        [DataMember]
        public string NombreCompleto { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string SitioWeb { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public DateTime? FechaInicioContrato { get; set; }
        [DataMember]
        public DateTime? FechaFinContrato { get; set; }
        [DataMember]
        public string Color { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public TipoClienteDC TipoCliente { get; set; }

        [DataMember]
        public AuditoriaDC Auditoria { get; set; }

        #endregion
    }
}