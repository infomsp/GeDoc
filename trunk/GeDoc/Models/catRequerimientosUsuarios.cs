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

    [KnownType(typeof(catRequerimientoNotificaWS))]
    [Serializable]
    public class catRequerimientoNotificaWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int requId
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

        [ScaffoldColumn(false)]
        public bool requNotificar
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