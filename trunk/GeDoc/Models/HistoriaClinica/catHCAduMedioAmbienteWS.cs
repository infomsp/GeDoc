/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 11/01/2016			Implementación inicial
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
    public class catHCAduMedioAmbienteWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 hcaduid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 aduMAHabitaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 aduMAConvivientes
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMAViviendaSi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMAViviendaNo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMAAguaSi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMAAguaNo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMABanoSi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMABanoNo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMAResiduoSi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMAResiduoNo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string aduMAResiduoFrecu
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMACloacasSi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool aduMACloacasNo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool vivienda
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool agua
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool residuos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool banio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool cloacas
        {
            get;
            set;
        }
    }
}