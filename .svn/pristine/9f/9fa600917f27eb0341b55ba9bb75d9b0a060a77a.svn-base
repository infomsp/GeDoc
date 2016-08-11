/************************************************************************************************************
 * Descripción: Clase referente a la estructura de archivos adjuntos a un HC.
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

    [KnownType(typeof(catHCAdjuntoWS))]
    [Serializable]
    public class catHCAdjuntoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int hcadjId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime hcadjFecha
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
        public string hcadjArchivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int hcadjpacId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string hcadjArchivoNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string hcadjObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string hcadjAccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string hcadjVisualizar
        {
            get;
            set;
        }
        
    }
}