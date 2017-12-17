using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DIST.SUIST.BE
{
    [DataContract]
    public class ClienteBE
    {
        #region Variables

        [DataMember]
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
        public TipoClienteBE TipoCliente { get; set; }
        [DataMember]
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
