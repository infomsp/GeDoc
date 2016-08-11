namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catGradosCategoriasPresupuestadosWS))]
    [Serializable()]
    public class catGradosCategoriasPresupuestadosWS : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 psId
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la fecha")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime psFecha
        {
            get;
            set;
        }

        [DisplayName("Cantidad:")]
        [Required(ErrorMessage = "Ingrese la cantidad")]
        [Range(1, 1000, ErrorMessage = "Cantidad incorrecta (Rango entre 1-1000)")]
        [UIHint("Integer")]
        public int psCantidad
        {
            get;
            set;
        }

        [DisplayName("Institución:")]
        [UIHint("GridForeignKeyComboBox")]
        public int? icId
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [Required(ErrorMessage = "Ingrese las observaciones")]
        [DataType(DataType.MultilineText)]
        public string psObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int gracId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Institucion
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (psFecha.Date > DateTime.Now.Date)
            {
                yield return new ValidationResult("Fecha incorrecta.", new[] { "psFecha" });
            }
        }
    }
}