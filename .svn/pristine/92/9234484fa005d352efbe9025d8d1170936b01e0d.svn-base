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

    [KnownType(typeof(catObrasSociales))]
    [Serializable()]
    public class catObrasSociales : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 osId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Range(1, 9999999, ErrorMessage = "Número incorrecto (Rango entre 1-9999999)")]
        [UIHint("Integer")]
        public Int32 osCodigo
        {
            get;
            set;
        }


        [DisplayName("Razón Social:")]
        [Required(ErrorMessage = "Debe especificar la Razón Social")]
        public string osDenominacion
        {
            get;
            set;
        }

        [DisplayName("Sigla:")]
        public string osSigla
        {
            get;
            set;
        }

        [DisplayName("Es Prepaga:")]
        public bool osEsPrepaga
        {
            get;
            set;
        }

        [DisplayName("Domicilio:")]
        public string osDomicilio
        {
            get;
            set;
        }

        [DisplayName("Código Postal:")]
        [Range(1000, 9999, ErrorMessage = "Número incorrecto (Rango entre 1000-9999)")]
        [UIHint("Integer")]
        public Int32 osCodigoPostal
        {
            get;
            set;
        }

        [DisplayName("Provincia:")]
        [UIHint("GridForeignKey")]
        public Int16? provId
        {
            get;
            set;
        }

        [DisplayName("Teléfono:")]
        [DataType(DataType.PhoneNumber)]
        public string osTelefono
        {
            get;
            set;
        }

        [DisplayName("Correo Electrónico:")]
        [DataType(DataType.EmailAddress)]
        public string osMail
        {
            get;
            set;
        }

        [DisplayName("Página Web:")]
        [DataType(DataType.Url)]
        public string osWeb
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string provNombre
        {
            get;
            set;
        }
    }
}