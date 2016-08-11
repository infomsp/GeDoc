/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 11/01/2016			Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Microsoft.Web.Mvc;
    [KnownType(typeof(catPersonas))]
    [Serializable()]
    public class catHCAduHistoriaFamiliarTabWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public catHCAduAnteFamiliarWS antecedentesFamiliares
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCAduAnteMedicoWS antecedentesMedicos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCAduAnteGinecoWS antecedentesGinecologicos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCAduHabitosWS habitos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCAduSituacionPsicoWS psicosocial
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCAduMedioAmbienteWS medioAmbiente
        {
            get;
            set;
        }

    }
}