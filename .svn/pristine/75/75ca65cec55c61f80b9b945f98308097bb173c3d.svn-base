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
    [KnownType(typeof(enlEncuestaAPSPersonasEnfermedades))]
    [Serializable()]
    public class enlEncuestaAPSPersonasEnfermedades : EntityObject
    {

         [ScaffoldColumn(false)]
        public int encEnfId
        {
            get;
            set;
        }

         [DisplayName("")]
        [UIHint("HiddenInput")]      
        public int? encPerId
        {
            get;
            set;
        }

        [UIHint("GridForeignKey")]
        [DisplayName("Enfermedad:")]
        public int? enfId
        {
            get;
            set;
        }
          [UIHint("LimitedTextArea")]        
         [DisplayName("Descripcion:")]       
        public string encEnfDescripcionComentario
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string enfDescripcion
        {
            get;
            set;
        }
    }
}