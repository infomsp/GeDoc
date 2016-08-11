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

    [KnownType(typeof(catCargosCategoriasPresupuestados))]
    [Serializable()]
    public class catCargosCategoriasPresupuestados : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 presId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Zona sanitaria:")]
        public int repId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string repNombre
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la fecha")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime presFecha
        {
            get;
            set;
        }

        [DisplayName("Cantidad:")]
        [Required(ErrorMessage = "Ingrese la cantidad")]
        [Range(1, 1000, ErrorMessage = "Cantidad incorrecta (Rango entre 1-1000)")]
        [UIHint("Integer")]
        public int presCantidad
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [Required(ErrorMessage = "Ingrese las observaciones")]
        [DataType(DataType.MultilineText)]
        public string presObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int categId
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (presFecha.Date > DateTime.Now.Date)
            {
                yield return new ValidationResult("Fecha incorrecta.", new[] { "presFecha" });
            }
        }
    }
}