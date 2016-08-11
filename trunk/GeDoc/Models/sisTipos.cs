/************************************************************************************************************
 * Descripción: Clase referente a la estructura de tipos generales.
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
    [KnownType(typeof(sisTipos))]
    [Serializable()]
    public class sisTipos : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tipoCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoeId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tipoDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tipoImagen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tipoColor
        {
            get;
            set;
        }

    }
}