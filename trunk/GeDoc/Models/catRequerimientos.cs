/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Requerimientos de Sistema
 * Observaciones: 
 * Creado por: Gustavo Nicolas Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 22/05/2014   GNT     Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
// ReSharper disable CheckNamespace
namespace GeDoc
// ReSharper restore CheckNamespace
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catRequerimientoWS))]
    [Serializable]
    public class catRequerimientoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int reqId
        {
            get;
            set;
        }

        [DisplayName("Tipo:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 tipoIdTipo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos Tipo
        {
            get;
            set;
        }

        [DisplayName("Asunto:")]
        [UIHint("GridForeignKeyAutoComplete")]
        [StringLength(80, ErrorMessage = "Máximo 80 carácteres")]
        public string reqAsunto
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage = "Ingrese la descripción")]
        [StringLength(255, ErrorMessage = "Texto muy largo, máximo 255 carácteres")]
        [UIHint("MultilineText")]
        public string reqDescripcion
        {
            get;
            set;
        }

        [DisplayName("Solicitante:")]
        [UIHint("GridForeignKeyComboBox")]
        public int perIdSolicitante
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catPersonas Solicitante
        {
            get;
            set;
        }

        [DisplayName("Estimado (Horas):")]
        [UIHint("Integer")]
        public int? reqTiempoEstimado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string TiempoEstimado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? reqFechaEstimado
        {
            get;
            set;
        }

        [DisplayName("Asignado a:")]
        [UIHint("GridForeignKeyComboBox")]
        public int usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisUsuarios Asignado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catRequerimientoEstadoWS Estado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catRequerimientoEstadoWS Creado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool NoEditar
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool NoCambiarEstado
        {
            get;
            set;
        }

        [DisplayName("Depende de:")]
        [UIHint("GridForeignKeyCBNullServer")]
        public int? reqLinkId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Dependencia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int CantidadComentarios
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int CantidadAdjuntos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int CantidadLogWork
        {
            get;
            set;
        }

    }
}