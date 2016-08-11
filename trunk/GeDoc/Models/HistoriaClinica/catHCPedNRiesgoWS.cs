﻿/************************************************************************************************************
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
    public class catHCPedNRiesgoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pedNRid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 hcpedid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime pedNRFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedNRNivel
        {
            get;
            set;
        }

    }
}