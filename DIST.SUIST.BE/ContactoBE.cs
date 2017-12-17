using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    [DataContract]
    public class ContactoBE
    {
        #region Variables

        [DataMember]
        public int IdContacto { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string TelefonoFijo { get; set; }
        [DataMember]
        public string TelefonoCelular { get; set; }
        [DataMember]
        public string Correo { get; set; }
        [DataMember]
        public string Cargo { get; set; }
        [DataMember]
        public bool Principal { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public ClienteBE Cliente { get; set; }
        [DataMember]
        public AuditoriaBE Auditoria { get; set; }

        #endregion

    }
}
