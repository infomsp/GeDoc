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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Microsoft.Web.Mvc;
    [KnownType(typeof(catHCPerRNWS))]
    [Serializable()]
    public class catHCPerRNWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 perRNid
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
        public Int64? pacidRN
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNNacimiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? perRNFechaNac
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perRNMultiple
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNOrden
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNTermi
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perRNMotivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perRNCodINDUC
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perRNCodOPER
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNSexo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNPesoAlNacer
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perRNPeriCefa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perRNLong
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNEGSem
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNEGDias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perRNEGFUM
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perRNEGECO
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perRNEGEsti
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNEGPeso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perRNApgar1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perRNApgar5
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perRNEPISIO
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perRNDesgarro
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perRNDesgarro14
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perRNLigadura
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? perEGRNFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perEGRNEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? perEGRNTrasLugar
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEGRNTrasFallece
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perEGRNTrasFalleceDias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perEGRNAlimento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEGRNBocaUP
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? perEGRNBCG
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perEGRNPeso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perEGRNAntirubeola
        {
            get;
            set;
        }

    }
}