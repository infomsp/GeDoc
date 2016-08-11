/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Proveedores.
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 17/11/2014   Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
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
    [KnownType(typeof(catProveedorWS))]
    [Serializable()]
    public class catProveedorWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int provId
        {
            get;
            set;
        }

        [DisplayName("Razón Social:")]
        [Required(ErrorMessage = "Debe especificar el Apellido")]
        public string provRazonSocial
        {
            get;
            set;
        }

        [DisplayName("Nombre de Fantasía:")]
        public string provNombreDeFantasia
        {
            get;
            set;
        }

        [DisplayName("CUIT:")]
        public string provCUIT
        {
            get;
            set;
        }

        [DisplayName("Domicilio:")]
        [StringLength(250, ErrorMessage = "Máximo 250 carácteres")]
        public string provDomicilio
        {
            get;
            set;
        }

        [DisplayName("Teléfono:")]
        [DataType(DataType.PhoneNumber)]
        public string provTelefono
        {
            get;
            set;
        }

        [DisplayName("Correo Electrónico:")]
        [EmailAddress(ErrorMessage = "Debe ingresar un e-mail válido")]
        public string provCorreoElectronico
        {
            get;
            set;
        }

        [DisplayName("Rubro(s):")]
        [ScaffoldColumn(false)]
        public string provRubro
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string provAccion
        {
            get;
            set;
        }

    }
}