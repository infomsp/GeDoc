/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Rolando M. Delgado
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 16/07/2013   RMD      Implementación inicial
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
    [KnownType(typeof(catPacientes))]
    [Serializable()]
    public class enlPacientesAntecedenteMedico : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 paamId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdAntecedenteMedico
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string paamDetalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 paamAnio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 pacId
        {
            get;
            set;
        }

    }
}