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

    [KnownType(typeof(catProvincias))]
    [Serializable()]
    public class catProvincias : EntityObject
    {
        [ScaffoldColumn(false)]
        public int provId
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage="Debe especificar el nombre")]
        [StringLength(50, ErrorMessage = "Texto muy largo, máximo 50 carácteres")]
        public string provNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Pais:")]
        public short paisId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string paisNombre
        {
            get;
            set;
        }
    }
}