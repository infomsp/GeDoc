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
    [KnownType(typeof(catPacientes))]
    [Serializable()]
    public class catPacientesMedioAmbiente : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pvivId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdPropiedad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdConstruccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdTipoBanio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdAgua
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdIluminacion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdPared
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoIdPiso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pvivHabitaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short pvivConvivientes
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pvivRecoleccionResiduos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool pvivCloacas
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

    }
}