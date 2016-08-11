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

    [KnownType(typeof(catRequerimientoAdjuntoWS))]
    [Serializable]
    public class catRequerimientoAdjuntoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int reqaId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime reqaFecha
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
        public string reqaArchivo
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [Required(ErrorMessage = "Ingrese observaciones")]
        [StringLength(255, ErrorMessage = "Máximo 255 carácteres")]
        [UIHint("MultilineText")]
        public string reqaObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int reqId
        {
            get;
            set;
        }
    }
}