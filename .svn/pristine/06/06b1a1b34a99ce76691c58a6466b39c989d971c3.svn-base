
using System.ComponentModel;

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catCentroDeSaludPublicidades))]
    [Serializable()]
    public class catCentroDeSaludPublicidades : EntityObject
    {
        [ScaffoldColumn(false)]
        public int cspId
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Ingresar el mensaje.")]
        [StringLength(250, ErrorMessage = "No más de 250 Caracteres")]
        [DisplayName("Mensaje:")]
        [DataType(DataType.MultilineText)]
        public string cspDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int csId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string csNombre
        {
            get;
            set;
        }

       [Required(ErrorMessage = "Ingresar fecha.")]
       [DisplayName("Desde:")]
       [DataType(DataType.Date)]
        public DateTime? cspDesde
        {
            get;
            set;
        }

       [Required(AllowEmptyStrings = false, ErrorMessage = "Ingresar la hora")]
       [DisplayName("Hora Inicial:")]
       [DataType(DataType.Time)]
       public string horaDesde
       {
           get;
           set;
       }


        [Required(ErrorMessage = "Ingresar fecha.")]
        [DisplayName("Hasta:")]
        [DataType(DataType.Date)]
         public DateTime? cspHasta
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingresar la hora")]
        [DisplayName("Hora Final:")]
        [DataType(DataType.Time)]
        public string horaHasta
        {
            get;
            set;
        }


       
    }
}