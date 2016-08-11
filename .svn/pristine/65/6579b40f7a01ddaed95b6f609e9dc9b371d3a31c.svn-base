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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Microsoft.Web.Mvc;
    [KnownType(typeof(catHCPerinatalWS))]
    [Serializable()]
    public class catHCPerinatalWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 hcperid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 pacid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool activa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? Fecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string hcperAccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCPerPacienteWS InformacionPaciente
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCPerAntesWS InformacionAntecedentes
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCPerGesActualWS InformacionGestacionActual
        {
            get;
            set;
        }

    }
}