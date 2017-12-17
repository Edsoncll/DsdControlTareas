using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIST.SUIST.BE
{
    [DataContract]
    public class ActividadBE
    {
        #region Variables

        [DataMember]
        public int IdActividad { get; set; }
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public int IdCliente { get; set; }
        [DataMember]
        public int IdProyecto { get; set; }
        [DataMember]
        public int IdTipoActividad { get; set; }
        [DataMember]
        public int IdContacto { get; set; }
        [DataMember]
        public string strUsuario { get; set; }
        [DataMember]
        public string strNombreCompleto { get; set; }
        [DataMember]
        public string Glosa { get; set; }
        [DataMember]
        public DateTime? FechaInicio { get; set; }
        [DataMember]
        public DateTime? FechaFin { get; set; }
        [DataMember]
        public string StrFechaInicio { get; set; }
        [DataMember]
        public string StrFechaFin { get; set; }
        [DataMember]
        public int TotalHoras { get; set; }
        [DataMember]
        public int TotalMinutos { get; set; }
        [DataMember]
        public bool Facturable { get; set; }
        
        [DataMember]
        public string strFecha { get; set; }
        [DataMember]
        public string strFechaTitulo { get; set; }
        [DataMember]
        public string strFechaInicio { get; set; }
        [DataMember]
        public string strFechaFin { get; set; }
        [DataMember]
        public string strFechaAcumulada { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public UsuarioBE Usuario { get; set; }
        [DataMember]
        public ClienteBE Cliente { get; set; }
        [DataMember]
        public ProyectoBE Proyecto { get; set; }
        [DataMember]
        public TipoActividadBE TipoActividad { get; set; }
        [DataMember]
        public ContactoBE Contacto { get; set; }
        [DataMember]
        public AuditoriaBE Auditoria { get; set; }

        #endregion
    }

    [DataContract]
    public class ActividadExportarBE
    {
        #region Variables

        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Cliente { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public string totalHorasMes { get; set; }
        [DataMember]
        public string totalMontoContrato { get; set; }
        [DataMember]
        public string totalMontoProyecto { get; set; }
        [DataMember]
        public string totalHorasRetainer { get; set; }
        [DataMember]
        public string totalHorasExceso { get; set; }
        [DataMember]
        public string totalMontoRetainer { get; set; }
        [DataMember]
        public string totalMontoHoraExceso { get; set; }
        [DataMember]
        public string totalMontoFlat { get; set; }
        [DataMember]
        public string totalPagar { get; set; }

        #endregion

        #region Objetos

        [DataMember]
        public List<ListaActividadesBE> ListaActividadesBE { get; set; }

        #endregion     
    }

    [DataContract]
    public class ListaActividadesBE
    {
        #region Variables

        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public string Cliente { get; set; }
        [DataMember]
        public string Tema { get; set; }
        [DataMember]
        public string Tiempo { get; set; }
        [DataMember]
        public string TiempoAcumulado { get; set; }

        #endregion
    }
}
