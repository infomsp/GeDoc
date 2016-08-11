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
    [KnownType(typeof(catHCPerPuerperioWS))]
    [Serializable()]
    public class catHCPerPuerperioWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 perPUid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 hcperid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 pacidRN
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? perPUFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perPUTemp
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perPUPA
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perPUPulso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perPUUter
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perPULoquios
        {
            get;
            set;
        }

    }
}