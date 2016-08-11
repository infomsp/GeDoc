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
    public class catPacientesGineco : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pacgId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgMenarca
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgIRS
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgGestas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgPartos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgCesareas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgAbortos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgCiclos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pacgMenopausia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pacgAnticoncepcionQuirurgica
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