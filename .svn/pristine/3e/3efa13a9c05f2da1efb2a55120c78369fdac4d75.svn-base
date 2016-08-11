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
    [KnownType(typeof(proCompraOfertaWS))]
    [Serializable()]
    public class proCompraOfertaWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 comoId
        {
            get;
            set;
        }

        [DisplayName("Nº Presupuesto:")]
        public Int32 comoNumeroPresupuesto
        {
            get;
            set;
        }

        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        public DateTime comoFechaPresupuesto
        {
            get;
            set;
        }

        [DisplayName("Validez Oferta (Días):")]
        [UIHint("Integer")]
        public int comoDiasValidezOferta
        {
            get;
            set;
        }

        [DisplayName("Asunto:")]
        [UIHint("GridForeignKeyAutoComplete")]
        public string comoProveedorNombre
        {
            get;
            set;
        }

        [DisplayName("CUIT:")]
        public string comoProveedorCUIT
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int comId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public List<proCompraOfertaDetalleWS> DetalleOferta
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool comoEsCompraSanJuan
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public decimal comoEsCompraSanJuanPorcentaje
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool comoEsAlternativa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Accion
        {
            get;
            set;
        }

    }
}