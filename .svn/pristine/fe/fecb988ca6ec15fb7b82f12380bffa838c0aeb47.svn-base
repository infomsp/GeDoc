/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 11/11/2015	GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using Models;
    using System.Collections.Generic;

    [KnownType(typeof(catHCPerCalendarioWS))]
    [Serializable()]
    public class catHCPerCalendarioWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 perCAid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 hcperid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime perCAFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCATipo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perCACumplida
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public List<getCalendarioDeProgramacion_Result> gridListaDeDatos
        {
            get;
            set;
        }

    }
}