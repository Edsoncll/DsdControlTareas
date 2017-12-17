using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DIST.SUIST.Web
{
    [DataContract]
    public class ErroresExcption
    {
        [DataMember]
        public int CodError { get; set; }
        [DataMember]
        public string DescError { get; set; }
    }
}