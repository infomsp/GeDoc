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

    [KnownType(typeof(catCargos))]
    [Serializable()]
    public class catCargos : EntityObject
    {
        [ScaffoldColumn(false)]
        public short carId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Agrupamiento:")]
        public short agrId
        {
            get;
            set;
        }

        [DisplayName("Denominación:")]
        [Required(ErrorMessage = "Debe especificar la denominación")]
        [StringLength(150, ErrorMessage = "Texto muy largo, máximo 150 carácteres")]
        public string carDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agrDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int carEmpleados
        {
            get;
            set;
        }
    }
}