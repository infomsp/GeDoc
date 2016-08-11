/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Delgado Rolando
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 29/05/2014   Implementación inicial
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
    [KnownType(typeof(enlTurnosPracticas))]
    [Serializable()]
    public class enlTurnosPracticas : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 turpracId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public Int64? turId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64? pracId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string PracticaDescripcion
        {
            get;
            set;
        }
        
        public string NomencladorDescripcion
        {
            get;
            set;
        }
        
       [ScaffoldColumn(false)]
        public Int64? NomencladorId
        {
            get;
            set;
        }

       [ScaffoldColumn(false)]
       public decimal? pracCosto
       {
           get;
           set;
       }

       [ScaffoldColumn(false)]
       public decimal? pracUop
       {
           get;
           set;
       }

       [ScaffoldColumn(false)]
       public Int32? turpracCantidad
       {
           get;
           set;
       }
       public string Medico
       {
           get;
           set;
       }
       public DateTime Fecha
       {
           get;
           set;
       }
       public string Especialidad
       {
           get;
           set;
       }
       public string CentroDeSalud
       {
           get;
           set;
       }
    }
}
