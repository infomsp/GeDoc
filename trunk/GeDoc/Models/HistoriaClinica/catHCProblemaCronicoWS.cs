/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Consultorios o Centros de Atención que estan dentro de una sala.
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
    [KnownType(typeof(catHCProblemaCronicoWS))]
    [Serializable()]
    public class catHCProblemaCronicoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int pcroId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int pcroCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pcroDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pcroAccion
        {
            get;
            set;
        }
    }
}