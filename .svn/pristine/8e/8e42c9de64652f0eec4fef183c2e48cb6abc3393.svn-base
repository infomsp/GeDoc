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
    public class catHCAduProCronicosWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 aduCRId
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
        [DataType(DataType.Date)]
        public DateTime? aduCRFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [UIHint("GridForeignKeyAutoComplete")]
        public string aduCRProblema
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduCRCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [UIHint("GridForeignKeyAutoComplete")]
        public string aduCRMedi
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

    }
}