/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Rolando M. Delgado
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 16/04/2014   RMD      Implementación inicial
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


     [KnownType(typeof(catDiagnosticos))]
    [Serializable()]
    public class catDiagnosticos : EntityObject
    {
         [ScaffoldColumn(false)]
         public Int64 diagId
         {
             get;
             set;
         }
          public string DiagnosticoID
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Descripcion
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string td1Descripcion
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string td2Descripcion
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string TipoDiagnosticoID
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]

        public string td1TipoDiagnosticoID
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]

        public string Aud_FechaAlta
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Aud_UsuarioModi
        {
            get;
            set;
        }

       

    }
}
