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

    [KnownType(typeof(catGradosWS))]
    [Serializable()]
    public class catGradosWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public short graId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Agrupamiento:")]
        public short ageId
        {
            get;
            set;
        }

        [DisplayName("Denominación:")]
        [Required(ErrorMessage = "Debe especificar la denominación")]
        [StringLength(150, ErrorMessage = "Texto muy largo, máximo 150 carácteres")]
        public string graDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ageDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int graEmpleados
        {
            get;
            set;
        }
    }
}