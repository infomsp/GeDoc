/************************************************************************************************************
 * Descripción: Clase referente a la estructura de archivos adjuntos a un requerimiento.
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 22/05/2014   GNT     Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
// ReSharper disable CheckNamespace
namespace GeDoc
// ReSharper restore CheckNamespace
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(proCompraDocumentacionWS))]
    [Serializable]
    public class proCompraDocumentacionWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int comaId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime comaFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisUsuarios Usuario
        {
            get;
            set;
        }

        [DisplayName("Adjunto:")]
        [UIHint("SubirArchivo")]
        public string comaArchivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int comacomId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string comaArchivoNombre
        {
            get;
            set;
        }

        [DisplayName("Proveedor:")]
        [UIHint("GridForeignKeyComboBoxNullLoad")]
        public int provId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Proveedor
        {
            get;
            set;
        }

        [DisplayName("Fecha de Retiro:")]
        [DataType(DataType.Date)]
        public DateTime comaFechaDeRetiro
        {
            get;
            set;
        }

        [DisplayName("Plazo de Entrega:")]
        [UIHint("Integer")]
        public int comaPlazoDeEntrega
        {
            get;
            set;
        }

        [DisplayName("Renglones:")]
        public string comaRenglones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? comaFechaDeVencimiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? comaFechaDeEntrega
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Vencido
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string comaAccion
        {
            get;
            set;
        }

    }
}