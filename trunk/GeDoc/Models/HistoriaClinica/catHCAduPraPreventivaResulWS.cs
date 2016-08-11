/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 17/02/2016			Implementación inicial
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
    public class catHCAduPraPreventivaResulWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64? aduPraPrevResId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 hcaduid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64? aduPraPrevId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? usrId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? aduPraPrevFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPrevResultado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduPraPrevDescri
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Usuario
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? Cantidad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ColorRiesgo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string RiesgoCardioActual
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool MostrarImagen
        {
            get;
            set;
        }

    }
}