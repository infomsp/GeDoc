namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Microsoft.Web.Mvc;

    [KnownType(typeof(catZonas))]
    [Serializable()]
    public class catZonas : EntityObject
    {
        [ScaffoldColumn(false)]
        public int zonId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Range(1, 1000, ErrorMessage="Código incorrecto (ingrese uno entre 1 y 1000)")]
        [Required(ErrorMessage = "Debe especificar un código de zona válido")]
        public int zonCodigo
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage="Debe especificar el Nombre de la zona")]
        [StringLength(80, MinimumLength=10, ErrorMessage= "Debe escribir entre 10 y 80 caracteres")]
        public string zonNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKey")]
        [DisplayName("Depende de la Zona:")]
        public int? zonIdDependencia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string zonNombreDependencia
        {
            get;
            set;
        }
    }
}