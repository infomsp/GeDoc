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

    [KnownType(typeof(catPersonasIdiomas))]
    [Serializable()]
    public class catPersonasIdiomas : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 pidiomaId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short idiomaId
        {
            get;
            set;
        }

        [DisplayName("Idioma:")]
        [StringLength(50, ErrorMessage = "Texto muy largo, máximo 50 carácteres")]
        public string idiomaDescripcion
        {
            get;
            set;
        }
    }
}