/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 02/03/2015   Implementación inicial
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
    [KnownType(typeof(catPagoWS))]
    [Serializable()]
    public class catPagoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int pagId
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "       *")]
        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime pagFecha
        {
            get;
            set;
        }

        [DisplayName("N° Expediente:")]
        [Required(ErrorMessage = "Ingrese el número de expediente")]
        public string pagExpediente
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? pagIdExpediente
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        //[DisplayName("Proveedor:")]
        //[UIHint("GridForeignKeyComboBoxNull")]
        public Int32? provId
        {
            get;
            set;
        }

        [DisplayName("Proveedor:")]
        [UIHint("GridForeignKeyAutoComplete")]
        public string Proveedor
        {
            get;
            set;
        }

        [DisplayName("Detalle:")]
        [Required(ErrorMessage = "Ingrese el detalle")]
        public string pagDetalle
        {
            get;
            set;
        }

        [DisplayName("Cuenta Escritural:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 ceId
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

        [DisplayName("Monto:")]
        [DataType(DataType.Currency, ErrorMessage = "       *")]
        public decimal pagMonto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ProveedorNombreDeFantasia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pagExpedienteCaratula
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ProveedorCUIT
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Fuente
        {
            get;
            set;
        }

    }
}