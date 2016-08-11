/************************************************************************************************************
 * Descripción: Clase referente a la estructura de certificados emitidos
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 31/01/2014   GNT     Implementación inicial
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
    [KnownType(typeof(catPersonaCertificados))]
    [Serializable()]
    public class catPersonaCertificados : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 percId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime percFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 tipoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public decimal percSueldoBruto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public decimal percSueldoNeto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool percPoseeEmbargos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string percLugarDePresentacion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 percCopias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool percImprimeSueldos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool percImprimeEmbargos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool percImprimeGuardias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool percPoseeGuardias
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string percAntiguedadVacaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisTipos Tipo
        {
            get;
            set;
        }

    }
}