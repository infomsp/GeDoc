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
    [KnownType(typeof(proCompraSeguimientoWS))]
    [Serializable()]
    public class proCompraSeguimientoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 segId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime segFecha
        {
            get;
            set;
        }

        [DisplayName("Tipo:")]
        [UIHint("GFKComboBoxOnChangeCustom")]
        public Int32 tipoIdTipoNotificacion
        {
            get;
            set;
        }

        [DisplayName("Proveedor:")]
        [UIHint("GFKComboBoxOnChangeCustom")]
        public int provId
        {
            get;
            set;
        }

        [DisplayName("Contacto:")]
        [UIHint("GridForeignKeyComboBoxNullLoad")]
        public Int32? pcId
        {
            get;
            set;
        }

        [DisplayName("Respuesta:")]
        [UIHint("MultilineText")]
        public string segRespuesta
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [UIHint("MultilineText")]
        public string segObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string segAccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int segcomId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos TipoNotificacion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Proveedor
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Contacto
        {
            get;
            set;
        }
    }
}