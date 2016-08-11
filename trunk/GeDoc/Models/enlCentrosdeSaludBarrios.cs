/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido Rolando Delgado
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 15/09/2014   Implementación inicial
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
    [KnownType(typeof(enlCentrosdeSaludBarrios))]
    [Serializable()]
    public class enlCentrosdeSaludBarrios : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 cbId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int csId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 barId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public String barrioNombre
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public String centroSaludNombre
        {
            get;
            set;
        }
       
    }
}
