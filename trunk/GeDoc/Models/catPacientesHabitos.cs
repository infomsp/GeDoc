/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Rolando M. Delgado
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 12/07/2013   RMD  Implementación inicial
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
    [KnownType(typeof(catPacientesHabitos))]
    [Serializable()]
    public class catPacientesHabitos : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pachId
        {
            get;
            set;
        }

        [DisplayName("Tipo Tabaco: ")]
        [UIHint("GridForeignKeyComboBox")]
        public Int32 tipoIdTabaco
        {
            get;
            set;
        }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar fecha desde que comenzo a fumar: ")]
        [DisplayName("Desde cuando comenzó a fumar: ")]
        [DataType(DataType.Date)]
        public DateTime pachFechaTabaco
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pachEsFumadorPasivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdAlcohol
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pachAlcoholVasos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pachSedentarismo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdActividadFisica
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdActividadFisicaFrecuencia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pathLlevaSaleroAMesa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pathDesayunaFrutasLacteos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pathFrutasEnsaladasConLaComida
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdCinturonSeguridadOCasco
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 pacId
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar fecha desde que dejo de fumar: ")]
        [DisplayName("Desde cuando dejo de fumar: ")]
        [DataType(DataType.Date)]
        public DateTime pachFechadejoFumar
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pachInyectadoDrogas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pachInyectadoDrogasCuales
        {
            get;
            set;
        }

    }
}