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

    [KnownType(typeof(catPersonasContratadosHorarios))]
    [Serializable()]
    public class catPersonasContratadosHorarios : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 conhId
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
        public string conhDiaSemana
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la hora")]
        [DisplayName("Hora inicial:")]
        [DataType(DataType.Time)]
        public string conhHoraInicio
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la hora")]
        [DisplayName("Hora Final:")]
        [DataType(DataType.Time)]
        public string conhHoraFin
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int conId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool conhActivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string conhHoras
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
            var _HoraInicio = DateTime.Parse(conhHoraInicio).ToString("HH:mm").Replace(":", "");
            var _HoraFin = DateTime.Parse(conhHoraFin).ToString("HH:mm").Replace(":", "");

            if (int.Parse(_HoraInicio) > int.Parse(_HoraFin))
            {
                yield return new ValidationResult("Horario superpuesto.", new[] { "conhHoraFin" });
            }
        }
    }
}