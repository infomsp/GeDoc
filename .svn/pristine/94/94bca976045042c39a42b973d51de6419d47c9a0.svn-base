namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catPersonaNovedadWS))]
    [Serializable()]
    public class catPersonaNovedadWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 novId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime novFecha
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

        [DisplayName("Detalle:")]
        [DataType(DataType.MultilineText)]
        public string novDescripcion
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

        [ScaffoldColumn(false)]
        public int? idExpediente
        {
            get;
            set;
        }

        [DisplayName("Expediente:")]
        public string Expediente
        {
            get;
            set;
        }

    }
}