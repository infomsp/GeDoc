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

    [KnownType(typeof(catCargosCategoriasDesignados))]
    [Serializable()]
    public class catCargosCategoriasDesignados : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 desigId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Tipo:")]
        public int tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string desigTipo
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxNull")]
        [DisplayName("Empleado:")]
        public int? perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catSectores Sector
        {
            get;
            set;
        }

        //[ScaffoldColumn(false)]
        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Cargo:")]
        [ReadOnly(true)]
        public int categId
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

        [DisplayName("Vigencia Desde:")]
        [DataType(DataType.Date)]
        public DateTime? desigVigenciaDesde
        {
            get;
            set;
        }

        [DisplayName("Vigencia Hasta:")]
        [DataType(DataType.Date)]
        public DateTime? desigVigenciaHasta
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxNull")]
        [DisplayName("Subrogado por:")]
        public int? perIdSubRogancia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perSubRoganciaNombre
        {
            get;
            set;
        }

        [DisplayName("SubRogancia Desde:")]
        [DataType(DataType.Date)]
        public DateTime? desigSubRoganciaDesde
        {
            get;
            set;
        }

        [DisplayName("SubRogancia Hasta:")]
        [DataType(DataType.Date)]
        public DateTime? desigSubRoganciaHasta
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        [DataType(DataType.MultilineText)]
        public string desigObservaciones
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (desigVigenciaDesde != null)
            {
                if (desigVigenciaDesde.Value.Date > DateTime.Now.Date)
                {
                    yield return new ValidationResult("Fecha incorrecta.", new[] { "desigVigenciaDesde" });
                }
            }

            if (perIdSubRogancia != null)
            {
                if (desigSubRoganciaDesde == null)
                {
                    yield return new ValidationResult("Ingrese una fecha.", new[] { "desigSubRoganciaDesde" });
                }
            }
            else
            {
                desigSubRoganciaDesde = null;
                desigSubRoganciaHasta = null;
            }

            if (desigVigenciaHasta != null)
            {
                if (desigVigenciaHasta.Value.Date > DateTime.Now.Date)
                {
                    yield return new ValidationResult("Fecha incorrecta.", new[] { "desigVigenciaHasta" });
                }
                if (desigVigenciaDesde.Value.Date >= desigVigenciaHasta.Value.Date)
                {
                    yield return new ValidationResult("Fecha incorrecta.", new[] { "desigVigenciaHasta" });
                }
            }

            if (desigSubRoganciaDesde != null)
            {
                if (desigSubRoganciaDesde.Value.Date > DateTime.Now.Date)
                {
                    yield return new ValidationResult("Fecha incorrecta.", new[] { "desigSubRoganciaDesde" });
                }

                if (desigSubRoganciaHasta != null)
                {
                    if (desigSubRoganciaHasta.Value.Date > DateTime.Now.Date)
                    {
                        yield return new ValidationResult("Fecha incorrecta.", new[] { "desigSubRoganciaHasta" });
                    }
                    if (desigSubRoganciaDesde.Value.Date >= desigSubRoganciaHasta.Value.Date)
                    {
                        yield return new ValidationResult("Fecha incorrecta.", new[] { "desigSubRoganciaHasta" });
                    }
                }
            }

            if (tipoId == 42)
            {
                if (desigObservaciones == null || desigObservaciones.Length <= 10)
                {
                    yield return new ValidationResult("Escriba las observaciones (N° Expediente, persona).", new[] { "desigObservaciones" });
                }
            }
            else
            {
                if (desigVigenciaDesde == null)
                {
                    yield return new ValidationResult("Fecha incorrecta.", new[] { "desigVigenciaDesde" });
                }
            }
        }
    }
}