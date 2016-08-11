/************************************************************************************************************
 * Descripción: Clase referente a la estructura de la tabla <catTurnoMotivoBloqueo>
 * Observaciones: 
 * Creado por: Federico Rojas
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 27/05/2015   FAR     Implementación inicial
 * ---
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GeDoc
{
    [KnownType(typeof(catTurnoMotivoBloqueos))]
    [Serializable()]
    public class catTurnoMotivoBloqueos : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 tmbId
        {
            get;
            set;
        }

        [DisplayName("Motivo:")]
        [Required(ErrorMessage = "Debe escribir el motivo.")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Debe escribir entre 10 y 250 caracteres")]
        public string tmbDescripcion
        {
            get;
            set;
        }
    }
}