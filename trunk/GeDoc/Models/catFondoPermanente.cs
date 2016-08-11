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

    [KnownType(typeof(catFondosPermanentes))]
    [Serializable()]
    public class catFondosPermanentes : EntityObject
    {
        [ScaffoldColumn(false)]
        public int fpId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Fuente:")]
        public short fteId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Banco:")]
        public short bcoId
        {
            get;
            set;
        }

        [DisplayName("Decripción:")]
        [Required(ErrorMessage = "Debe especificar la descripción")]
        [StringLength(80, ErrorMessage = "Texto muy largo, máximo 80 carácteres")]
        public string fpDescripcion
        {
            get;
            set;
        }

        [DisplayName("Número de Cuenta:")]
        [Required(ErrorMessage="Debe especificar el Número de Cuenta")]
        [StringLength(12, ErrorMessage = "Número incorrecto (Ej: 001-123456-9)", MinimumLength = 12)]
        public string fpNumeroCuenta
        {
            get;
            set;
        }

        [DisplayName("CBU:")]
        [Required(ErrorMessage = "Debe especificar el CBU")]
        [StringLength(22, ErrorMessage = "CBU incorrecto (Ej: 1234567890123456789012)", MinimumLength = 22)]
        public string fpCBU
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string bcoRazonSocial
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
    }
}