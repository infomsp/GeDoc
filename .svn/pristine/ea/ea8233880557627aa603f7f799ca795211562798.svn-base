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

    [KnownType(typeof(catEntidadesBancarias))]
    [Serializable()]
    public class catEntidadesBancarias : EntityObject
    {
        [ScaffoldColumn(false)]
        public int bcoId
        {
            get;
            set;
        }

        [DisplayName("Razón Social:")]
        [Required(ErrorMessage="Debe especificar la Razón Social")]
        [StringLength(80, ErrorMessage = "Texto muy largo, máximo 80 carácteres")]
        public string bcoRazonSocial
        {
            get;
            set;
        }

        [DisplayName("CUIT:")]
        [Required(ErrorMessage = "Debe especificar el CUIT")]
        [StringLength(13, ErrorMessage = "CUIT incorrecto", MinimumLength = 13)]
        [RegularExpression("^(20|23|27|30|33)-[0-9]{8}-[0-9]$", ErrorMessage = "CUIT incorrecto (Ej: 20-24785748-7)")]
        public string bcoCUIT
        {
            get;
            set;
        }

        [DisplayName("Número de Sucursal:")]
        [Required(ErrorMessage = "Debe especificar el número de sucursal")]
        [StringLength(4, ErrorMessage = "Número de sucursal incorrecto", MinimumLength = 4)]
        [RegularExpression("[0-9]{4}$", ErrorMessage = "Número de sucursal incorrecto (Ej: 0002)")]
        public string bcoNumeroSucursal
        {
            get;
            set;
        }
    }
}