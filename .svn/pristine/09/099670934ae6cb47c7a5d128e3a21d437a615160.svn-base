/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 11/01/2016			Implementación inicial
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
    [KnownType(typeof(catPersonas))]
    [Serializable()]
    public class catHCPediatricaWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 hcpedid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64? hcperid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 pacid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64? pacidMadre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? Fecha
        {
            get;
            set;
        }

    }
}