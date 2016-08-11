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
    [KnownType(typeof(catPersonas))]
    [Serializable()]
    public class catAgenteWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int agtId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int agtDNI
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtCUIL
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtApellidoyNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ospeId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime agtFechaNacimiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short agtEdad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int tarId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtLugarTrabajo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime agtFechaIngreso
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyAutoComplete")]
        public string agtCalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? agtCalleNumero
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtBlock
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtBlockPiso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtBlockDepto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtBarrio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtManzana
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtSector
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? agtCodigoPostal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? locId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool agtOrigenInfOSP
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtFoto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtSexo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? agtFuma
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? agtHTA
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? agtColesterol
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? agtDiabetes
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? agtActividadFisica
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? agtAlergias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public decimal? agtPeso
        {
            get;
            set;
        }

        [DataType(DataType.Currency, ErrorMessage = "*")]
        public decimal? agtTalla
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtTelefonoFijo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtTelefonoMovil
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtCorreoElectronico
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtAccion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtApellido
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int depId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string agtDomicilioReferencia
        {
            get;
            set;
        }

        public List<catAgenteGrupoFamiliarWS> ListaGrupoFamiliar { get; set; }
        public List<catAgenteReparticionWS> ListaReparticiones { get; set; }
    }
}