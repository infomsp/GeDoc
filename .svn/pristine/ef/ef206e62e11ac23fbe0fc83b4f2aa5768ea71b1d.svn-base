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

    [KnownType(typeof(catFacturasNotificaciones))]
    [Serializable()]
    public class catFacturasNotificaciones : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 notId
        {
            get;
            set;
        }

        [DisplayName("Tipo de Notificacion:")]
        [UIHint("GridForeignKeyComboBox")]
        public short tipoIdNota
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string notTipo
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de notificación")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime notFecha
        {
            get;
            set;
        }

        [DisplayName("Detalle:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string notDetalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int facId
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (notFecha.Date > DateTime.Now.Date)
            {
                yield return new ValidationResult("Debe ser menor o igual a la Fecha del día.", new[] { "notFecha" });
            }
        }
    }
}