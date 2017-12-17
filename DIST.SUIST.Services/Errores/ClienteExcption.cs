using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DIST.SUIST.Services
{
    [DataContract]
    public class ClienteExcption
    {
        [DataMember]
        public int CodError { get; set; }
        [DataMember]
        public string DescError { get; set; }
    }
}