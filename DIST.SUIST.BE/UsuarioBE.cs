using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    [DataContract]
    public class UsuarioBE
    {
        #region Variables

        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public int IdEmpresa { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Contrasenia { get; set; }
        [DataMember]
        public string NombreCompleto { get; set; }
        [DataMember]
        public bool MasterAdmin { get; set; }
        [DataMember]
        public bool Estado { get; set; }
        [DataMember]
        public string strEstado { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public PerfilBE Perfil { get; set; }
        [DataMember]
        public EmpresaBE Empresa { get; set; }
        [DataMember]
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }
}
