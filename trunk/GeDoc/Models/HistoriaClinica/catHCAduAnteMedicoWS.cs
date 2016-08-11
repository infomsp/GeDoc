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
    public class catHCAduAnteMedicoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 hcaduid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduAMAlergia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduAMAlergiaCual
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduAMInterna
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduAMMotivo1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 aduAMAño1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduAMMotivo2
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 aduAMAño2
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduAMMotivo3
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 aduAMAño3
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduAMTranfusiones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduAMTranfusionesCuando
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduAMInfecciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduAMInfeccionesCual
        {
            get;
            set;
        }

    }
}