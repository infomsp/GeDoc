/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Paciente Perinatal.
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 14/11/2015	GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catHCPerPacienteWS))]
    [Serializable()]
    public class catHCPerPacienteWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 perhcId
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
        public string perDomicilio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perLocalidad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perTelefono
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 perEdad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEtniaBlanca
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEtniaIndigena
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEtniaMestiza
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perEtnia
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEtniaNegra
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEtniaOtra
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perEstudio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstudioNinguno
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstudioPrimaria
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstudioSecundaria
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstudioUniversitaria
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short? perEstudioAnios
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perEstadoCivil
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstadoCivilCasada
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstadoCivilUnionEstable
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstadoCivilSoltera
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEstadoCivilOtro
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool perAlfabeta
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perViveSola
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? csIdPrenatal
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? csIdParto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catPacientes perInfoPaciente
        {
            get;
            set;
        }

    }
}