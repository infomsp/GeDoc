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
    public class catHCPedFactoresWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 hcpedid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRFumaCasa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRInterna
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRHnoFalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRMadPri
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRTraInf
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRPrematuro
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRBajoPeso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRFaltaLac
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRMadreAdo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFRHacinamiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFPMaltrato
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFPAbuso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFPAlcoholismo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedFPAdicciones
        {
            get;
            set;
        }

    }
}