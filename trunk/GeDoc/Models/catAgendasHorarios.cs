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

    [KnownType(typeof(catAgendasHorarios))]
    [Serializable()]
    public class catAgendasHorarios : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 aghId
        {
            get;
            set;
        }

        [DisplayName("Lunes:")]
        public bool Lunes
        {
            get;
            set;
        }

        [DisplayName("Martes:")]
        public bool Martes
        {
            get;
            set;
        }

        [DisplayName("Miércoles:")]
        public bool Miercoles
        {
            get;
            set;
        }

        [DisplayName("Jueves:")]
        public bool Jueves
        {
            get;
            set;
        }

        [DisplayName("Viernes:")]
        public bool Viernes
        {
            get;
            set;
        }

        [DisplayName("Sábado:")]
        public bool Sabado
        {
            get;
            set;
        }

        [DisplayName("Domingo:")]
        public bool Domingo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aghDiaSemana
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese una fecha")]
        [DisplayName("Vigencia desde:")]
        [DataType(DataType.Date)]
        public DateTime aghVigenciaDesde
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese una fecha")]
        [DisplayName("Vigencia hasta:")]
        [DataType(DataType.Date)]
        public DateTime aghVigenciaHasta
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la hora")]
        [DisplayName("Hora inicial:")]
        [DataType(DataType.Time)]
        public string aghHoraInicio
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la hora")]
        [DisplayName("Hora Final:")]
        [DataType(DataType.Time)]
        public string aghHoraFin
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int agId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aghActivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aghFechas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aghHoras
        {
            get;
            set;
        }
      
        [DisplayName("Cant Turnos:")]
        public int? aghCantTurnos
        {
            get;
            set;
        }
        [DisplayName("Sobreturnos:")]
        public int? aghSobreturnos
        {
            get;
            set;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!(Lunes || Martes || Miercoles || Jueves || Viernes || Sabado || Domingo))
            {
                yield return new ValidationResult("Seleccione un día", new[] { "Lunes" });
            }
            if (aghVigenciaDesde.Date.Year == 1)
            {
                yield return new ValidationResult("Ingrese una fecha.", new[] { "aghVigenciaDesde" });
            }
            if (aghVigenciaHasta.Date.Year == 1)
            {
                yield return new ValidationResult("Ingrese una fecha.", new[] { "aghVigenciaHasta" });
            }
            if (aghVigenciaDesde.Date.Year < 2013)
            {
                yield return new ValidationResult("Fecha muy antigua.", new[] { "aghVigenciaDesde" });
            }
            if (aghVigenciaHasta.Date.Year < 2013)
            {
                yield return new ValidationResult("Fecha muy antigua.", new[] { "aghVigenciaHasta" });
            }
            if (aghVigenciaDesde.Date > aghVigenciaHasta.Date)
            {
                yield return new ValidationResult("Fechas superpuestas.", new[] { "aghVigenciaHasta" });
            }
            var _HoraInicio = DateTime.Parse(aghHoraInicio).ToString("HH:mm").Replace(":", "");
            var _HoraFin = DateTime.Parse(aghHoraFin).ToString("HH:mm").Replace(":", "");

            if (int.Parse(_HoraInicio) > int.Parse(_HoraFin))
            {
                yield return new ValidationResult("Horario superpuesto.", new[] { "aghHoraFin" });
            }
        }
    }
}