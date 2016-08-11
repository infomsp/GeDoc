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

    [KnownType(typeof(catFacturasPagos))]
    [Serializable()]
    public class catFacturasPagos : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 pagId
        {
            get;
            set;
        }

        [DisplayName("Tipo de Cobro:")]
        [UIHint("GridForeignKeyComboBoxOnChange")]
        public int tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pagTipo
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de pago")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime pagFecha
        {
            get;
            set;
        }

        [DisplayName("Número de Recibo:")]
        [Range(1, 99999999, ErrorMessage = "Número incorrecto (rango de 1 a 99999999)")]
        //[Required(ErrorMessage = "Debe ingresar el número de recibo")]
        public int? pagNumeroRecibo
        {
            get;
            set;
        }

        [DisplayName("Forma de Pago:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public int? tipoIdForma
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pagForma
        {
            get;
            set;
        }

        [DisplayName("Detalle:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string pagDetalle
        {
            get;
            set;
        }

        [DisplayName("Motivo del Débito:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string pagMotivo
        {
            get;
            set;
        }

        [DisplayName("Importe:")]
        [DataType(DataType.Currency, ErrorMessage = "Debe ingresar un importe.")]
        public decimal pagImporte
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
            if (pagFecha.Date > DateTime.Now.Date)
            {
                yield return new ValidationResult("Debe ser menor o igual a la Fecha del día.", new[] { "pagFecha" });
            }

            if (pagImporte <= 0)
            {
                yield return new ValidationResult("Debe ingresar un importe mayor a Cero.", new[] { "pagImporte" });
            }

            if (tipoId == 11)//Débito
            {
                if (pagMotivo == null || pagMotivo.Trim().Length == 0)
                {
                    yield return new ValidationResult("Ingrese el motivo del Débito.", new[] { "pagMotivo" });
                }
            }
            else//Crédito
            {
                if (pagNumeroRecibo <= 0 || pagNumeroRecibo == null)
                {
                    yield return new ValidationResult("Ingrese un Número de recibo válido.", new[] { "pagNumeroRecibo" });
                }
                if (tipoIdForma <= 0 || tipoIdForma == null)
                {
                    yield return new ValidationResult("Ingrese una Forma de Pago.", new[] { "tipoIdForma" });
                }
            }
        }
    }
}