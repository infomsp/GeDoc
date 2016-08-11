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
    [KnownType(typeof(catProveedorContactoWS))]
    [Serializable()]
    public class catProveedorContactoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int pcId
        {
            get;
            set;
        }

        [DisplayName("Nombre y Apellido:")]
        [Required(ErrorMessage = "Ingrese el Nombre y Apellido")]
        public string pcApellidoyNombre
        {
            get;
            set;
        }

        [DisplayName("Teléfono:")]
        [DataType(DataType.PhoneNumber)]
        public string pcTelefono
        {
            get;
            set;
        }

        [DisplayName("Correo Electrónico:")]
        [EmailAddress(ErrorMessage = "Debe ingresar un e-mail válido")]
        public string pcCorreoElectronico
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pcAccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int pcprovId
        {
            get;
            set;
        }

    }
}