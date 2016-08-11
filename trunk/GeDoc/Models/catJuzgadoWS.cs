namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catJuzgadoWS))]
    [Serializable()]
    public class catJuzgadoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int juzId
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage="Ingrese el Nombre del Juzgado")]
        [StringLength(80, ErrorMessage = "Texto muy largo, máximo 80 carácteres")]
        public string juzDenominacion
        {
            get;
            set;
        }
    }
}