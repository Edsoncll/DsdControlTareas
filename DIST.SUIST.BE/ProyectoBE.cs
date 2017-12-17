using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    [DataContract]
    public class ProyectoBE
    {
        #region Variables

        [DataMember]
        public int IdProyecto { get; set; }
        [DataMember]
        public string NombreProyecto { get; set; }
        [DataMember]
        public double Precio { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public ClienteBE Cliente { get; set; }
        [DataMember]
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
