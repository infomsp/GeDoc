/************************************************************************************************************
 * Descripción: Clase referente a la estructura de ticket de soporte técnico
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
    [KnownType(typeof(catTicketSoportes))]
    [Serializable()]
    public class catTicketSoportes : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 tiqId
        {
            get;
            set;
        }

        [DisplayName("Oficina:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 ofiId
        {
            get;
            set;
        }

        [DisplayName("Motivo:")]
        [UIHint("GridForeignKeyComboBox")]
        public int tipoIdLlamada
        {
            get;
            set;
        }

        [DisplayName("Detalle:")]
        [Required(ErrorMessage = "Debe especificar el detalle")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string tiqDetalle
        {
            get;
            set;
        }

        [DisplayName("Prioridad:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 tipoIdPrioridad
        {
            get;
            set;
        }

        [DisplayName("Solicitante:")]
        [UIHint("GridForeignKeyComboBox")]
        public int? perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? conId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catTicketSoportesEstados Estado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos Prioridad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catPersonas Empleado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos MotivoLlamada
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catOficinas Oficina
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTime tiqFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tiqHora
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public List<catTicketSoportesEstados> Historial
        {
            get;
            set;
        }

    }
}