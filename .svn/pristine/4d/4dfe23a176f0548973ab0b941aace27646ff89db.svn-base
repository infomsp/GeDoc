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
    using System.Text.RegularExpressions;

    public class proImputaciones : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public Int64 impId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Fuente:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 fteId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string fteDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Cuenta:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 ccId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ccDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Partida:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? partId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string partNombre
        {
            get;
            set;
        }

        [DisplayName("Tipo Comprobante:")]
        [UIHint("GridForeignKeyComboBoxOnChange")]
        public Int16 tcoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tcoDescripcion
        {
            get;
            set;
        }

        [DisplayName("N° Comprobante:")]
        [Required(ErrorMessage = "       *")]
        public string impComprobante
        {
            get;
            set;
        }

        [DisplayName("Fondo Permanente:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public Int16? fpId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string fpDescripcion
        {
            get;
            set;
        }

        [DisplayName("Cuenta Escritural:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public Int16? ceId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ceDescripcion
        {
            get;
            set;
        }

        [DisplayName("Tipo de Gasto:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32? tgId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tgDescripcion
        {
            get;
            set;
        }

        [DisplayName("Detalle:")]
        [Required(ErrorMessage = "       *")]
        [StringLength(250, ErrorMessage = "       *")]
        public string impDetalle
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "       *")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime impFecha
        {
            get;
            set;
        }

        [DisplayName("Fecha de Impresión:")]
        [DataType(DataType.Date)]
        public DateTime? impFechaImpresion
        {
            get;
            set;
        }

        [DisplayName("Importe:")]
        [DataType(DataType.Currency, ErrorMessage = "       *")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C0}")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un importe.")]
        public decimal impImporte
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int16 usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime impFechaUltimoCambio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double Acumulado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double Saldo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double Credito
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string CreditoTexto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string AcumuladoTexto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string CEFP
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string SaldoTexto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public long UltimoId
        {
            get;
            set;
        }

        [DisplayName("Sub-Partidas:")]
        [UIHint("SubPartidas")]
        public int SubPartidas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool impEsCompromiso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double BienesDeConsumo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double ServiciosNoPersonales
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double BienesDeUso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double Transferencias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public double GastosEnPersonal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64? impAnuladaPor
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public Int64? impuAnuladaParcialPor
        {
            get;
            set;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (impFecha.Date > DateTime.Now.Date)
            //{
            //    yield return new ValidationResult("Debe ser menor o igual a la Fecha del día.", new[] { "impFecha" });
            //}
            //
            //if (impFecha.Date > DateTime.Now.Date)
            //{
            //    yield return new ValidationResult("Debe ser menor o igual a la Fecha del día.", new[] { "impFecha" });
            //}
            //if (impImporte < 0)
            //{
            //    //yield return new ValidationResult("Debe ser Mayor a Cero.", new[] { "impImporte" });
            //}
            if (!ValidarComprobante())
            {
                yield return new ValidationResult("       *", new[] { "impComprobante" });
            }
            if (tcoId == 1)
            {
                if (fpId == null)
                {
                    yield return new ValidationResult("       *", new[] { "fpId" });
                }
            }
            else
            {
                if (ceId == null)
                {
                    yield return new ValidationResult("       *", new[] { "ceId" });
                }
            }
        }

        private bool ValidarComprobante()
        {
            string pattern = @"[0-9]{3}-[0-9]{4}-[0-9]{4}";
            int _nro = 0;
            if (tcoId == 1)
            {
                if (impComprobante.Length > 5 || !int.TryParse(impComprobante, out _nro))
                {
                    return false;
                }
                return true;
            }
            else if (impComprobante.Length != 13)
            {
                return false;
            }

            Regex regex = new Regex(pattern);

            return regex.IsMatch(impComprobante);
        }
    }
}