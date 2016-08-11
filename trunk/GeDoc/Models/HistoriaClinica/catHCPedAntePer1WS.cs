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
    public class catHCPedAntePer1WS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 hcpedid
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMControles
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? pedAPEMFechaC
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? pedAPEMCantidadC
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMAntitetanica
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMHepatitisB
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMStrep
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMVIH
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMToxo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMHVB
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMChagas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPEMVDRL
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPEMPatologias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPEMComplicaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPAIns
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPADom
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPAEU
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPAForceps
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPPACesarea
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? pedAPPARuptura
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPPAPresentacion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPPAPatologias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPPAComplicaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEHipo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEHipoRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEFeni
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEFeniRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEGala
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEGalaRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEHipe
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEHipeRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEDefi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEDefiRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEFibr
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEFibRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEAudi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEAudiRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEVisi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEVisiRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEOtros
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPPEOtrosRES
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPRNEG
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? pedAPRNPeso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? pedAPRNTalla
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? pedAPRNPerCef
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPRNApgar1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPRNApgar5
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPRNReanima
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? pedAPRNInterna
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPRNCausa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPALPecho
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPALLeche
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pedAPALPapilla
        {
            get;
            set;
        }

    }
}