using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DIST.SUIST.Services
{
    [DataContract]
    public class AuditoriaDC
    {
        #region Variables

        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public DateTime? Fecha { get; set; }

        #endregion
    }
}