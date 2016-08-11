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
    [KnownType(typeof(catCentroDeSaludSalaConsultorioWS))]
    [Serializable()]
    public class catCentroDeSaludSalaConsultorioWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int csscId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string csscNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int cssId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int csscIdSala
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string csscAccion
        {
            get;
            set;
        }
    }
}