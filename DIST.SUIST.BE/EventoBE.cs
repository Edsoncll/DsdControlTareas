using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    [DataContract]
    public class EventoBE
    {
        #region Variables

        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public DateTime start { get; set; }
        [DataMember]
        public DateTime end { get; set; }
        [DataMember]
        public string description { get; set; }

        #endregion
        
    }
}
