/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Rolando M. Delgado
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 06/04/2015   RMD      Implementación inicial
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
    [KnownType(typeof(catEncuestaAPSEnfermedades))]
    [Serializable()]
    public class catEncuestaAPSEnfermedades : EntityObject
    {
        [ScaffoldColumn(false)]
        public int enfId
        {
            get;
            set;
        }
        public int? enfCodigo
        {
            get;
            set;
        }
       
        public string enfDescripcion
        {
            get;
            set;
        }
        //public string encEnfDescripcionComentario
        //{
        //    get;
        //    set;
        //}


    }
}
