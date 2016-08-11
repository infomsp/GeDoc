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
    public class catHCAduEXAFISICOWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 hcaduid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFITA
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIFC
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIPeso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFITalla
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIIMC
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFICC
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIPiel
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIPielAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFILentesNo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFILentesSi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFILentes
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIvisualOD
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIvisualOI
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIOjosAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIOidos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIOidosAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIDentadura
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIDentaduraAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIPulmones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIPulmonesAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFICorazon
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFICorazonAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIAbdomen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIAbdomenAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIGenitales
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIGenitalesAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIMamas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIMamasAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduFIOsteo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduFIOsteoAnormal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catHCAduPraPatologicasResulWS[] patologicas
        {
            get;
            set;
        }
    }
}