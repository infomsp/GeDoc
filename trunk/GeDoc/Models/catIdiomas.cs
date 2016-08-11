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

    [KnownType(typeof(catIdiomas))]
    [Serializable()]
    public class catIdiomas : EntityObject
    {
        [ScaffoldColumn(false)]
        public int idiomaId
        {
            get;
            set;
        }

        [DisplayName("Idioma:")]
        [Required(ErrorMessage="Debe especificar la Idioma")]
        [StringLength(50, ErrorMessage = "Texto muy largo, máximo 50 carácteres")]
        public string idiomaDescripcion
        {
            get;
            set;
        }
    }
}