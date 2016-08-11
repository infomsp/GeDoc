/************************************************************************************************************
 * Descripción: Clase referente a la estructura de estado de los requerimientos.
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

    [KnownType(typeof(catRequerimientoEstadoWS))]
    [Serializable]
    public class catRequerimientoEstadoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int reqeId
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
        public DateTime reqeFecha
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
        public string reqeObservaciones
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

        [ScaffoldColumn(false)]
        public sisTipos Tipo
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

    }
}