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
    using GeDoc.Models;

    public class catCentrosDeSaludConsultorios : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 cscId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 csId
        {
            get;
            set;
        }

        [DisplayName("Denominación:")]
        [Required(ErrorMessage = "Ingrese Denominación")]
        [StringLength(80, ErrorMessage = "Texto muy largo, máximo 80 carácteres")]
        public string cscNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool cscActivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catCentrosDeSalud CentroDeSalud
        {
            get;
            set;
        }
    }
}