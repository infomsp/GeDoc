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

    [KnownType(typeof(catTipoNormasLegales))]
    [Serializable()]
    public class catTipoNormasLegales : EntityObject
    {
        [ScaffoldColumn(false)]
        public int tnlId
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage="Debe especificar el Nombre")]
        [StringLength(50, MinimumLength=5, ErrorMessage= "Debe escribir entre 5 y 50 caracteres")]
        public string tnlNombre
        {
            get;
            set;
        }
    }
}