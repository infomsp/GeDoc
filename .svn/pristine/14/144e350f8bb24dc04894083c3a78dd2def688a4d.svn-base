/************************************************************************************************************
 * Descripción: Clase referente a las de los centros de Salud.
 * Observaciones: 
 * Creado por: Gustavo Nicolas Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 25/01/2016			Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System.ComponentModel;

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catCentroDeSaludSalaWS))]
    [Serializable()]
    public class catCentroDeSaludSalaWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int cssId
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Ingresar el nombre de Sala.")]
        [StringLength(80, ErrorMessage = "No más de 80 Caracteres")]
        [DisplayName("Nombre de la Sala:")]
        public string cssNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int csId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int Consultorios
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int Televisores
        {
            get;
            set;
        }

    }
}