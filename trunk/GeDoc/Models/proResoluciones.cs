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

    [KnownType(typeof(proResoluciones))]
    [Serializable()]
    public class proResoluciones : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 resId
        {
            get;
            set;
        }

        [DisplayName("Número:")]
        [Range(1, 10000, ErrorMessage = "Número incorrecto (Rango entre 1-10000)")]
        [UIHint("Integer")]
        public short? resNumero
        {
            get;
            set;
        }

        [DisplayName("Fecha:")]
        //[UIHint("Date")]
        [DataType(DataType.Date, ErrorMessage="Debe ingresar la fecha de resolución")]
        public DateTime resFecha
        {
            get;
            set;
        }

        [DisplayName("Tipo:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32? tnlId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tnlNombre
        {
            get;
            set;
        }

        //[ScaffoldColumn(false)]
        [UIHint("Editor")]
        [DisplayName("Detalle:")]
        //[Required(ErrorMessage="Debe ingresar el texto de 'Considerando'")]
        public string resConsiderando
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        //[UIHint("Editor"), Required]
        //[DisplayName("Resuelve:")]
        //[Required(ErrorMessage = "Debe ingresar el texto de la Resolución")]
        public string resResuelve
        {
            get;
            set;
        }

        [UIHint("SubirArchivo")]
        [DisplayName("Archivo:")]
        public string resLinkArchivo
        {
            get;
            set;
        }

        [DisplayName("Notificar:")]
        public bool resEsImportante
        {
            get;
            set;
        }

        [DisplayName("Vencimiento:")]
        [DataType(DataType.Date)]
        public DateTime? resNotificacionVencimiento
        {
            get;
            set;
        }

        [DisplayName("Días de Inicio de la Alerta:")]
        [UIHint("Integer")]
        [Range(5, 120, ErrorMessage = "Número incorrecto (5-120)")]
        public short? resNotificacionDiaInicio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short? decNumero
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string decLinkArchivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Expedientes
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (resEsImportante)
            {
                if (resNotificacionVencimiento == null)
                {
                    yield return new ValidationResult("Debe ingresar una fecha.", new[] { "resNotificacionVencimiento" });
                }
                else
                {
                    if (resNotificacionVencimiento.Value.Date < DateTime.Now.Date)
                    {
                        yield return new ValidationResult("Debe ser mayor o igual a la Fecha del día.", new[] { "resNotificacionVencimiento" });
                    }
                }

                if (resNotificacionDiaInicio == null)
                {
                    yield return new ValidationResult("Debe ingresar un valor.", new[] { "resNotificacionDiaInicio" });
                }
                else
                {
                    if (resNotificacionDiaInicio.Value <= 4)
                    {
                        yield return new ValidationResult("Debe ingresar un valor mayor o igual a 5.", new[] { "resNotificacionDiaInicio" });
                    }
                }
            }
        }
    }
}