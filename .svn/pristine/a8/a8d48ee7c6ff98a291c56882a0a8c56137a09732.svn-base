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

    public class catFacturas:IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int32 facId
        {
            get;
            set;
        }

        [DisplayName("Obra Social:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 osId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string osNombre
        {
            get;
            set;
        }

        [DisplayName("Centro de Salud:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 sucId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string sucNombre
        {
            get;
            set;
        }

        [DisplayName("Periodo Mes:")]
        [UIHint("GridForeignKeyComboBox")]
        public Byte facPeriodoMes
        {
            get;
            set;
        }

        [DisplayName("Periodo Año:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el Año del periodo facturado")]
        [Range(2010, 2200, ErrorMessage = "Número incorrecto (Rango entre 2010-2200)")]
        [UIHint("Integer")]
        public Int32 facPeriodoAnio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Periodo
        {
            get;
            set;
        }

        [DisplayName("Número:")]
        [Required(ErrorMessage="Debe ingresar el número de factura")]
        [StringLength(13, ErrorMessage = "Número de factura Incorrecto", MinimumLength = 13)]
        [RegularExpression(@"[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessage = "Número de factura inválido.")]
        public string facNumero
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings=false, ErrorMessage="Debe ingresar la fecha de la Factura")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        //[DateValidation(ValidationType.Compare, "La Fecha de Factura debe ser menor a la de recepción.", compareWith: "facFecha")]
        public DateTime facFecha
        {
            get;
            set;
        }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de Recepción")]
        [DisplayName("Fecha de Recepción:")]
        [DataType(DataType.Date)]
        public DateTime? facFechaRecepcion
        {
            get;
            set;
        }

        [DisplayName("Importe:")]
        //[Range(1.0, 9999999.99, ErrorMessage = "Importe incorrecto, debe ser mayor a 0")]
        [DataType(DataType.Currency,ErrorMessage="Debe ingresar un importe.")]
        public decimal facImporte
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public decimal facHaber
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public decimal facSaldo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string facImporteTexto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string facEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string facEstadoImagen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int facDiasVtoSSS //Días de vencimiento para reclamar a la SSS (Superintendencia de seguro de salud)
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string VisibilidadImagen
        {
            get;
            set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (facFecha.Date > DateTime.Now.Date)
            {
                yield return new ValidationResult("Debe ser menor o igual a la Fecha del día.", new[] { "facFecha" });
            }

            if (facFechaRecepcion != null)
            {
                if (facFecha.Date > facFechaRecepcion.Value.Date)
                {
                    yield return new ValidationResult("Debe ser menor a la Fecha de recepción.", new[] { "facFecha" });
                }

                if (facFechaRecepcion.Value.Date > DateTime.Now.Date)
                {
                    yield return new ValidationResult("Debe ser menor o igual a la Fecha del día.", new[] { "facFechaRecepcion" });
                }
            }
        }
    }
}