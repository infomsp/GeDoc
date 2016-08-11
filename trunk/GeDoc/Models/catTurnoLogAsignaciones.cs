/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por:MC
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 11/08/2016   MC      Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GeDoc
{
    [KnownType(typeof(catTurnoLogAsignaciones))]
    [Serializable()]
    public class catTurnoLogAsignaciones : EntityObject
    {
        [ScaffoldColumn(false)]
        public int tlaId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 turId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime fechaCarga
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int16 usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short logTipo
        {
            get;
            set;
        }
    }
}