/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 28/08/2013   Implementación inicial
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
    [KnownType(typeof(catLocalidades))]
    [Serializable()]
    public class catLocalidades : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 locId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string locNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 depId
        {
            get;
            set;
        }

    }
}