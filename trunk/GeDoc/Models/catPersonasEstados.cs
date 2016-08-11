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

    [KnownType(typeof(catPersonasEstados))]
    [Serializable()]
    public class catPersonasEstados : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int32 pereId
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime? pereFecha
        {
            get;
            set;
        }

        [DisplayName("Estado:")]
        [UIHint("GridForeignKeyComboBox")]
        public int tipoIdEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perEstado
        {
            get;
            set;
        }

        [DisplayName("Motivo:")]
        [UIHint("GridForeignKeyComboBox")]
        public short? motId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perMotivo
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string pereObservaciones
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
            if (tipoIdEstado == 24)
            {
                if (pereFecha.Value.Date > (new DateTime(1998, 1, 1)).Date)
                {
                    yield return new ValidationResult("Debe ser menor al 01/01/1998.", new[] { "pereFecha" });
                }
            }
            else
            {
                if (tipoIdEstado != 7)
                {
                    if (pereFecha.Value.Date > DateTime.Now.Date)
                    {
                        yield return new ValidationResult("Debe ser <= a la fecha actual.", new[] { "pereFecha" });
                    }
                }
            }
        }

    }
}