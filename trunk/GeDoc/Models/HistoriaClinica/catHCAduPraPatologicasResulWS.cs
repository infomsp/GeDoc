/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 15/02/2016			Implementación inicial
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
    public class catHCAduPraPatologicasResulWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 aduPraPatResId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 hcaduid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 aduPraPatId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduPraPatFecha1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPatResul1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduPraPatFecha2
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPatResul2
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduPraPatFecha3
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPatResul3
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduPraPatFecha4
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPatResul4
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduPraPatFecha5
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPatResul5
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduPraPatFecha6
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPatResul6
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPatDescri
        {
            get;
            set;
        }

    }
}