/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 10/10/2014   GNT     Implementación inicial.
 * -----------------------------------------------------------------------------------------------
*/

using GeDoc.Models;

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
    [KnownType(typeof(proCompraProveedorWS))]
    [Serializable()]
    public class proCompraProveedorWS : getProveedoresWS
    {
        [ScaffoldColumn(false)]
        public string SeleccionProveedor
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string SeleccionContacto
        {
            get;
            set;
        }

    }
}