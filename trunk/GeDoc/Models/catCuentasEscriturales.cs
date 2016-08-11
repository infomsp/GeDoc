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

    [KnownType(typeof(catCuentasEscriturales))]
    [Serializable()]
    public class catCuentasEscriturales : EntityObject
    {
        [ScaffoldColumn(false)]
        public int ceId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Required(ErrorMessage = "Debe especificar el código")]
        [StringLength(7, ErrorMessage = "Código muy largo, máximo 7 carácteres")]
        public string ceCodigo
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage="Debe especificar la descripción")]
        [StringLength(80, ErrorMessage = "Texto muy largo, máximo 80 carácteres")]
        public string ceDescripcion
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Fuente:")]
        public short fteId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string fteDescripcion
        {
            get;
            set;
        }
    }
}