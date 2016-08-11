/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Vademecum de medicamentos.
 * Observaciones: 
 * Creado por: Gutavo Nicolas Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 25/01/2016	GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catHCVademecumWS))]
    [Serializable()]
    public class catHCVademecumWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int vadId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string vadCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string vadDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string vadAccion
        {
            get;
            set;
        }
    }
}