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
    public class catHCPedConsultasWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pedCOid
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
        public Int64? turId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? pedCOFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedCOEdad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedCODX
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedCOExCom
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedCOTratamiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedCOMedico
        {
            get;
            set;
        }

    }
}