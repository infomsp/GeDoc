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

    [KnownType(typeof(catPersonasGrupoFamiliar))]
    [Serializable()]
    public class catPersonasGrupoFamiliar : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 gfId
        {
            get;
            set;
        }

        [DisplayName("Parentesco:")]
        [UIHint("GridForeignKeyComboBox")]
        public int tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string gfParentesco
        {
            get;
            set;
        }

        [DisplayName("Apellido y Nombre:")]
        [Required(ErrorMessage = "Ingrese el Apellido y Nombre")]
        public string gfApellidoyNombre
        {
            get;
            set;
        }

        [DisplayName("Sexo:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? tipoIdSexo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string gfSexo
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese una fecha")]
        [DisplayName("Fecha Nacimiento:")]
        [DataType(DataType.Date)]
        public DateTime gfFechaNacimiento
        {
            get;
            set;
        }

        //[DisplayName("Fecha Fallecimiento:")]
        //[DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime? gfFechaFallecimiento
        {
            get;
            set;
        }

        [DisplayName("Tipo Documento:")]
        [UIHint("GridForeignKeyComboBox")]
        public int tipoIdTipoDocumento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string gfTipoDocumento
        {
            get;
            set;
        }

        [DisplayName("Número Documento:")]
        [UIHint("Integer")]
        public int gfNumeroDocumento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int perId
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (gfFechaNacimiento.Date > DateTime.Now.Date)
            {
                yield return new ValidationResult("Fecha Incorrecta.", new[] { "gfFechaNacimiento" });
            }
            if (gfFechaFallecimiento != null)
            {
                if (gfFechaFallecimiento.Value.Date > DateTime.Now.Date)
                {
                    yield return new ValidationResult("Fecha Incorrecta.", new[] { "gfFechaFallecimiento" });
                }
                if (gfFechaNacimiento.Date > gfFechaFallecimiento.Value.Date)
                {
                    yield return new ValidationResult("Fecha Incorrecta.", new[] { "gfFechaFallecimiento" });
                }
            }
        }
    }
}