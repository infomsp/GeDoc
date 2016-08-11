/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 04/04/2016			Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System.Collections.Generic;
using GeDoc.Models.JuntaMedica.Modelo;

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catAgenteGrupoFamiliarWS))]
    [Serializable()]
    public class catAgenteGrupoFamiliarWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int agtfId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtfApellidoyNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int agtfDNI
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ospvId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int agtIdGF
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtfAccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool agtfActivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Vinculo
        {
            get;
            set;
        }

    }
}