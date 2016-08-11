/************************************************************************************************************
 * Descripción: Clase referente a la estructura de estados de Ticket de soporte
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
    [KnownType(typeof(catTicketSoportesEstados))]
    [Serializable()]
    public class catTicketSoportesEstados : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 tiqeId
        {
            get;
            set;
        }

        [DataType(DataType.DateTime)]
        public DateTime tiqeFecha
        {
            get;
            set;
        }

        [DataType(DataType.DateTime)]
        public string tiqeHora
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdEstado
        {
            get;
            set;
        }

        [DisplayName("Motivo Soporte:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32? motsId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos Estado
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
        public sisUsuarios Usuario
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tiqeDetalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 tiqId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catMotivosSoporte MotivoSoporte
        {
            get;
            set;
        }
    }
}