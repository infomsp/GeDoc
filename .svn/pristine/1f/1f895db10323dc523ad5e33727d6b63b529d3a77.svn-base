/************************************************************************************************************
 * Descripción: Clase referente a la estructura de compras...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 24/03/2014   Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    //[KnownType(typeof(proCompraWS))]
    //[Serializable()]
    public class proCompraWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int comId
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage = "Ingrese la descripción")]
        public string comDescripcion
        {
            get;
            set;
        }

        [DisplayName("Tipo Comprobante:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos TipoComprobante
        {
            get;
            set;
        }

        [DisplayName("N° Comprobante:")]
        [Required(ErrorMessage = "Ingrese el número de comprobante")]
        public string comComprobante
        {
            get;
            set;
        }

        [DisplayName("Encuadre Legal:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public Int32? tipoIdEncuadreLegal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos EncuadreLegal
        {
            get;
            set;
        }

        [DisplayName("Fecha:")]
        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime comFecha
        {
            get;
            set;
        }

        [DisplayName("Fecha de Ingreso:")]
        [DataType(DataType.Date)]
        public DateTime comFechaIngreso
        {
            get;
            set;
        }

        [DisplayName("Fecha de Apertura:")]
        [DataType(DataType.Date)]
        public DateTime? comFechaApertura
        {
            get;
            set;
        }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la hora")]
        [DisplayName("Hora de Apertura:")]
        [DataType(DataType.Time)]
        public string comHoraApertura
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public long? resId
        {
            get;
            set;
        }

        [DisplayName("N° Resolución:")]
        public string comResolucion
        {
            get;
            set;
        }

        [DisplayName("Solicitante:")]
        [UIHint("GridForeignKeyComboBox")]
        public int perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catPersonas Solicitante
        {
            get;
            set;
        }

        [DisplayName("Destino:")]
        [Required(ErrorMessage = "Ingrese la descripción")]
        public string comDestino
        {
            get;
            set;
        }

        [DisplayName("Lugar de Entrega:")]
        //[Required(ErrorMessage = "Ingrese el Lugar de entrega")]
        public string comLugarDeEntrega
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos Estado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool TieneDetalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string EstadoImagen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool CompraAnulada
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool AutorizadoEncuadreLegal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Currency)]
        public decimal ImporteEstimado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? IdExpediente
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string expUbicacion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? expDiasHabiles
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? expDiasCorridos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public proResoluciones InfoResolucion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? pagFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool Pagado
        {
            get;
            set;
        }

    }
}