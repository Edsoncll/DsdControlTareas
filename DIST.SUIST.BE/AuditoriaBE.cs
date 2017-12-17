using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DIST.SUIST.BE
{
    [DataContract]
    public class AuditoriaBE
    {
        #region Variables

        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public DateTime? Fecha { get; set; }
        
        #endregion
    }
}
