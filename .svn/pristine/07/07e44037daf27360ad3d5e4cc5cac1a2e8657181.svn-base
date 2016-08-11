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

    [KnownType(typeof(catCuentasContables))]
    [Serializable()]
    public class catCuentasContables : EntityObject
    {
        [ScaffoldColumn(false)]
        public int ccId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Required(ErrorMessage = "Debe especificar el código")]
        [StringLength(13, ErrorMessage = "Código muy largo, máximo 13 carácteres")]
        public string ccCodigo
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage="Debe especificar la descripción")]
        [StringLength(80, ErrorMessage = "Texto muy largo, máximo 80 carácteres")]
        public string ccDescripcion
        {
            get;
            set;
        }
    }
}