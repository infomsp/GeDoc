namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catGradosDesignacionWS))]
    [Serializable()]
    public class catGradosDesignacionWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int gdId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime gdFecha
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Cargo:")]
        public short gracId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Tipo de Cargo:")]
        public int tipoIdTipo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string TipoCargo
        {
            get;
            set;
        }

        [DisplayName("Horas:")]
        [Required(ErrorMessage = "Ingrese las horas")]
        [Range(0, 50, ErrorMessage = "Horas incorrectas (Rango entre 0-50)")]
        [UIHint("Integer")]
        public int gdHoras
        {
            get;
            set;
        }

        [DisplayName("Con Adicional:")]
        public bool gdConAdicional
        {
            get;
            set;
        }

        [DisplayName("Horas adicionales:")]
        [Required(ErrorMessage = "Ingrese las horas")]
        [Range(0, 50, ErrorMessage = "Horas incorrectas (Rango entre 0-50)")]
        [UIHint("Integer")]
        public int gdHorasAdicional
        {
            get;
            set;
        }

        [DisplayName("Vigencia Desde:")]
        [Required(ErrorMessage = "Ingrese la Vigencia")]
        [DataType(DataType.Date)]
        public DateTime? gdFechaDesde
        {
            get;
            set;
        }

        [DisplayName("Vigencia Hasta:")]
        [DataType(DataType.Date)]
        public DateTime? gdFechaHasta
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Categoria
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catPersonas InfoPersona
        {
            get;
            set;
        }

        [DisplayName("Servicio:")]
        [UIHint("GridForeignKeyAutoComplete")]
        [StringLength(50, ErrorMessage = "Máximo 50 carácteres")]
        public string gdServicio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public long? resId
        {
            get;
            set;
        }

        [DisplayName("N° Resolución:")]
        public string Resolucion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public proResoluciones InfoResolucion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public long? decId
        {
            get;
            set;
        }

        [DisplayName("N° Decreto:")]
        public string Decreto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public proDecretos InfoDecreto
        {
            get;
            set;
        }

        [DisplayName("N° Registro:")]
        [Required(ErrorMessage = "Ingrese el N° de Registro")]
        [UIHint("Integer")]
        public int? gdNumeroRegistro
        {
            get;
            set;
        }

        [DisplayName("Cumple Función en:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string gdFuncion
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string gdObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Grado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Agrupamiento
        {
            get;
            set;
        }

    }
}