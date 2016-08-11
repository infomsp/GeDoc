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

    public class catCreditosAnuales : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int32 creId
        {
            get;
            set;
        }

        [DisplayName("Fuente:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 fteId
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

        [DisplayName("Cuenta:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 ccId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ccDescripcion
        {
            get;
            set;
        }

        [DisplayName("Partida:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 partId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string partNombre
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime creFecha
        {
            get;
            set;
        }

        [DisplayName("Resolución:")]
        [Range(1, 99999, ErrorMessage = "Número incorrecto (Rango entre 1-99999)")]
        [UIHint("Integer")]
        public Int16? creResolucion
        {
            get;
            set;
        }

        [DisplayName("Importe:")]
        [DataType(DataType.Currency,ErrorMessage="Debe ingresar un importe.")]
        public decimal creImporte
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [DataType(DataType.MultilineText)]
        public string creObservaciones
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (creFecha.Date > DateTime.Now.Date)
            //{
            //    yield return new ValidationResult("Debe ser menor o igual a la Fecha del día.", new[] { "creFecha" });
            //}
            if (creImporte == 0)
            {
                yield return new ValidationResult("Debe ser distinto a Cero.", new[] { "creImporte" });
            }
        }
    }
}