/************************************************************************************************************
 * Descripción: Clase referente a la estructura de detalle de compra.
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 27/03/2014   GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System.Data.Objects.DataClasses;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class proCompraDetalleWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int comdId
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage = "Especifique la Descripción")]
        public string comdDetalle
        {
            get;
            set;
        }

        [DisplayName("Cantidad:")]
        [UIHint("NumberDecimal")]
        public decimal comdCantidad
        {
            get;
            set;
        }

        [DisplayName("Estimado Unitario:")]
        [DataType(DataType.Currency)]
        public decimal comdPrecioEstimado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string comdDetallePresupuestado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public decimal comdPrecioPresupuestado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool comdActivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string comdObservaciones
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
        public decimal comdSubTotal
        {
            get;
            set;
        }

    }
}