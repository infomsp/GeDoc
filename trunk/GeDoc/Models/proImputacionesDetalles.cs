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
    using System.Text.RegularExpressions;

    public class proImputacionesDetalles : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 impdId
        {
            get;
            set;
        }

        [DisplayName("Sub-Partida:")]
        //[UIHint("GridForeignKeyComboBox")]
        public Int16 subpId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string subpNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 impId
        {
            get;
            set;
        }

        [DisplayName("Importe:")]
        [DataType(DataType.Currency, ErrorMessage = "Debe ingresar un importe.")]
        public decimal impdImporte
        {
            get;
            set;
        }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (impdImporte == 0)
        //    {
        //        yield return new ValidationResult("Debe ser distinto de Cero.", new[] { "impImporte" });
        //    }
        //}
    }
}