/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Oficinas.
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 26/04/2013   GNT     Implementación inicial
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
    [KnownType(typeof(catOficinas))]
    [Serializable()]
    public class catOficinas : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 ofiId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 ofiCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ofiNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? repId
        {
            get;
            set;
        }

    }
}