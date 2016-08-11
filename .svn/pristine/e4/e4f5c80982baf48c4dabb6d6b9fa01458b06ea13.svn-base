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
    [KnownType(typeof(catPracticas))]
    [Serializable()]
    public class catPracticas : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pracId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pracCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pracDescripcion
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
        public Int64? nomId
        {
            get;
            set;
        }
       

        public string NomencladorDescripcion
        {
            get;
            set;
        }
    }
}