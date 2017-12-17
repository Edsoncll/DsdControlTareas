using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{

    [DataContract]
    public class TipoActividadBE
    {
        #region Variables

        [DataMember]
        public int IdTipoActividad { get; set; }
        [DataMember]
        public string Nombre { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public PrecioBE Precio { get; set; }
        [DataMember]
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
