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

    [KnownType(typeof(catProfesiones))]
    [Serializable()]
    public class catProfesiones : EntityObject
    {
        [ScaffoldColumn(false)]
        public int profId
        {
            get;
            set;
        }

        [DisplayName("Profesión:")]
        [Required(ErrorMessage="Debe especificar la Profesión")]
        [StringLength(150, ErrorMessage = "Texto muy largo, máximo 150 carácteres")]
        public string profNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int profEmpleados
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int profContratados
        {
            get;
            set;
        }
    }
}