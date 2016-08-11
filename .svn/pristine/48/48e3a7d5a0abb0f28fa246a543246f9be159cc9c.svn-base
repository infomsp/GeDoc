/************************************************************************************************************
 * Descripción: Clase referente a la estructura de movimientos de bienes.
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 17/09/2013   GNT     Implementación inicial
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
    [KnownType(typeof(catBienMovimientos))]
    [Serializable()]
    public class catBienMovimientos : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 bimId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime bimfecha
        {
            get;
            set;
        }

        [DisplayName("Estado:")]
        [UIHint("GridForeignKeyComboBoxOnChange")]
        public Int32 tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos Tipo
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        [DataType(DataType.MultilineText)]
        public string bimObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 biId
        {
            get;
            set;
        }

        [DisplayName("Origen:")]
        [UIHint("GridForeignKeyComboBox")]
        [ScaffoldColumn(false)]
        public Int32? cscIdOrigen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catCentrosDeSaludConsultorios CSOrigen
        {
            get;
            set;
        }

        [DisplayName("Destino:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32? cscIdDestino
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catCentrosDeSaludConsultorios CSDestino
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [StringLength(15, ErrorMessage = "Número muy largo, máximo 15 carácteres")]
        public string bimCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string bimCodigoAnterior
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisUsuarios Usuario
        {
            get;
            set;
        }

    }
}