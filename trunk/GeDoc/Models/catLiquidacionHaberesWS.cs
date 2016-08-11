/************************************************************************************************************
 * Descripción: Clase referente a la estructura Embargos y cuotas alimentarias
 * Observaciones: 
 * Creado por: Gustavo Nicolas Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 12/08/2015	GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catPersonas))]
    [Serializable()]
    public class catLiquidacionHaberesWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 liqId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxLoad")]
        public Int32 tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Tipo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [UIHint("GridForeignKeyComboBoxNullLoadOC")]
        public int perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Empleado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? EmpleadoDNI
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        public DateTime liqFechaEntrada
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string liqNumeroExpediente
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string liqAutos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? liqIdExpediente
        {
            get;
            set;
        }

        [DataType(DataType.MultilineText)]
        public string liqCaratulados
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxNullLoadOC")]
        public int juzId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Juzgado
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxNullLoadOC")]
        public Int32 bcoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Banco
        {
            get;
            set;
        }

        [DataType(DataType.Currency)]
        public decimal liqImporte
        {
            get;
            set;
        }

        [DataType(DataType.Currency)]
        public decimal liqImporteSF
        {
            get;
            set;
        }

        [UIHint("Integer")]
        public Int32 liqCuotas
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

        [ScaffoldColumn(false)]
        public string Estado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string liqAccion
        {
            get;
            set;
        }

    }
}