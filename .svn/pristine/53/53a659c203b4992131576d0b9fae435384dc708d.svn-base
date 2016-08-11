/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 11/11/2015	GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catHistoriaClinicaWS))]
    [Serializable()]
    public class catHistoriaClinicaWS
    {
        [ScaffoldColumn(false)]
        public Int64? pacidRN
        {
            get;
            set;
        }

        //[ScaffoldColumn(false)]
        public catPacientes Paciente
        {
            get;
            set;
        }
    }
}