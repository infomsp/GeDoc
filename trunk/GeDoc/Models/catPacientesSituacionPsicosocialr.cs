/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Rolando M. Delgado
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 12/07/2013   RMD  Implementación inicial
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
    [KnownType(typeof(catPacientes))]
    [Serializable()]
    public class catPacientesSituacionPsicosocial : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pspId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pspViolenciaFamiliar
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdSucesosAfectan
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdOcupacion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdAQuienRecurre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pspAQuienRecurreOtros
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 pacId
        {
            get;
            set;
        }

    }
}