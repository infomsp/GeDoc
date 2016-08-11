/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 10/10/2014   GNT     Implementación inicial.
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
    [KnownType(typeof(proCompraOfertaDetalleWS))]
    [Serializable()]
    public class proCompraOfertaDetalleWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 codId
        {
            get;
            set;
        }

        //[DisplayName("Cantidad:")]
        //[UIHint("NumberDecimal")]
        [ScaffoldColumn(false)]
        public decimal codCantidad
        {
            get;
            set;
        }

        [DisplayName("Precio Unitario:")]
        [DataType(DataType.Currency)]
        public decimal codPrecioUnitario
        {
            get;
            set;
        }

        [DisplayName("Marca / Tipo:")]
        [Required(ErrorMessage = "Debe especificar la Marca/Tipo")]
        [StringLength(250, ErrorMessage = "Máximo 250 carácteres")]
        [DataType(DataType.MultilineText)]
        public string codDetalle
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [DataType(DataType.MultilineText)]
        [StringLength(250, ErrorMessage = "Máximo 250 carácteres")]
        public string codObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 comoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int comdId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public proCompraDetalleWS DetalleOriginal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public decimal comoSubTotal
        {
            get;
            set;
        }

    }
}