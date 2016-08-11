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

    [KnownType(typeof(catPaises))]
    [Serializable()]
    public class catPaises : EntityObject
    {
        [ScaffoldColumn(false)]
        public int paisId
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage="Debe especificar el Nombre")]
        [StringLength(50, ErrorMessage = "Texto muy largo, máximo 50 carácteres")]
        public string paisNombre
        {
            get;
            set;
        }
    }
}