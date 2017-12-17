using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DIST.SUIST.Services
{
    [DataContract]
    public class TipoClienteDC
    {
        #region Variables

        [DataMember]
        public int IdTipoCliente { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public AuditoriaDC Auditoria { get; set; }

        #endregion
    }
}