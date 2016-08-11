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

    [KnownType(typeof(catPersonasContratados))]
    [Serializable()]
    public class catPersonasContratados : EntityObject
    {
        [ScaffoldColumn(false)]
        public int conId
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese una fecha")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime conFecha
        {
            get;
            set;
        }

        [DisplayName("Apellido y Nombre:")]
        [Required(ErrorMessage = "Ingrese el Apellido y Nombre")]
        public string conApellidoyNombre
        {
            get;
            set;
        }

        [DisplayName("Sexo:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string conSexo
        {
            get;
            set;
        }

        [DisplayName("DNI:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el DNI")]
        [Range(99999, 99999999, ErrorMessage = "Número incorrecto (Rango entre 99999-99999999)")]
        [UIHint("Integer")]
        public int conDNI
        {
            get;
            set;
        }

        [DisplayName("CUIL:")]
        [Required(ErrorMessage = "Ingrese el número de CUIL")]
        [StringLength(13, ErrorMessage = "CUIL Incorrecto (Ej: 20-28321920-5)", MinimumLength = 13)]
        [RegularExpression(@"[0-9]{2}-[0-9]{8}-[0-9]", ErrorMessage = "CUIL inválido (Ej: 20-28321920-5)")]
        public string conCUIL
        {
            get;
            set;
        }

        [DisplayName("Domicilio:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string conDomicilio
        {
            get;
            set;
        }

        [DisplayName("Teléfono:")]
        [DataType(DataType.PhoneNumber)]
        public string conTelefono
        {
            get;
            set;
        }

        [DisplayName("Celular:")]
        [DataType(DataType.PhoneNumber)]
        public string conCelular
        {
            get;
            set;
        }

        [DisplayName("Correo Electrónico:")]
        [EmailAddress(ErrorMessage = "Debe ingresar un e-mail válido")]
        public string conEmail
        {
            get;
            set;
        }

        [DisplayName("Profesión:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 profId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string profNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string espNombre
        {
            get;
            set;
        }

        [DisplayName("Carga Horaria:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la Carga Horaria")]
        [Range(10, 40, ErrorMessage = "Número incorrecto (Rango entre 10-40)")]
        public short conCargaHorariaSemanal
        {
            get;
            set;
        }

        [DisplayName("Cuotas:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese las Cuotas")]
        [Range(1, 12, ErrorMessage = "Número incorrecto (Rango entre 1-12)")]
        public short conCuotas
        {
            get;
            set;
        }

        [DisplayName("Monto Mensual:")]
        [DataType(DataType.Currency, ErrorMessage = "Ingrese un monto.")]
        [Range(100.0, 99999.99, ErrorMessage = "Monto incorrecto, debe ser mayor a 0")]
        public decimal conMontoMensual
        {
            get;
            set;
        }

        [DisplayName("Zona Sanitaria:")]
        [UIHint("GridForeignKeyComboBox")]
        public int repId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string repNombre
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        [DataType(DataType.MultilineText)]
        public string conObservaciones
        {
            get;
            set;
        }

        [DisplayName("Fecha Baja:")]
        [DataType(DataType.Date)]
        public DateTime? conFechaBaja
        {
            get;
            set;
        }

        [DisplayName("Motivo Baja:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        [DataType(DataType.MultilineText)]
        public string conMotivoBaja
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public decimal conMontoAnual
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool conDeBaja
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string TextoMontoMensual
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string TextoMontoAnual
        {
            get;
            set;
        }
    }
}