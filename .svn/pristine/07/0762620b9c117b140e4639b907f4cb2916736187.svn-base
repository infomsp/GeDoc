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
    using System.Web.Mvc;

    [KnownType(typeof(catCargosCategorias))]
    [Serializable()]
    public class catCargosCategorias : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public short categId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Cargo:")]
        public short carId
        {
            get;
            set;
        }

        [DisplayName("Categoría:")]
        [Required(ErrorMessage = "Debe especificar la categoría")]
        public string categNumero
        {
            get;
            set;
        }

        [DisplayName("Antigüedad Desde:")]
        [Required(ErrorMessage = "Ingrese la antigüedad inicial")]
        [Range(0, 50, ErrorMessage = "Antigüedad incorrecta (Rango entre 0-50)")]
        [UIHint("Integer")]
        public int categAntiguedadDesde
        {
            get;
            set;
        }

        [DisplayName("Antigüedad Hasta:")]
        [Required(ErrorMessage = "Ingrese la antigüedad final")]
        [Range(0, 50, ErrorMessage = "Antigüedad incorrecta (Rango entre 0-50)")]
        [UIHint("Integer")]
        public int categAntiguedadHasta
        {
            get;
            set;
        }

        [DisplayName("Horas:")]
        [Required(ErrorMessage = "Ingrese las horas")]
        [Range(0, 50, ErrorMessage = "Horas incorrectas (Rango entre 0-50)")]
        [UIHint("Integer")]
        public int categHoras
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string carDescripcion
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

        [ScaffoldColumn(false)]
        public int carPresupuestado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int carLibres
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (categAntiguedadDesde > categAntiguedadHasta)
            {
                yield return new ValidationResult("Antigüedad incorrecta.", new[] { "categAntiguedadHasta" });
            }
        }
    }
}