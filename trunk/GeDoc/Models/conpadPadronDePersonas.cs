/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Padrón de personas.
 * Observaciones: 
 * Creado por: Gustavo Nicolas Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 21/08/2013   GNT     Implementación inicial
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
    [KnownType(typeof(conpadPadronDePersonas))]
    [Serializable()]
    public class conpadPadronDePersonas : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 ppId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppApellido
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? tdocId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tipoDocumento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int ppNroDoc
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppCUIL
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppSexoId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppSexo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? ppFechaNacimiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppDomCalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppDomNroCalle
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppDomPiso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppDomDepto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppDomBarrio_Loc
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppDepartamento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppCP
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string locId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Localidad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string locDepartamento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? ttelId1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppTelefono1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? ttelId2
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppTelefono2
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppNivelValidacion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppObraSocial
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? ppEdad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ppTipoEdad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? csId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string CentroDeSalud
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool Clasificado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Documento
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public Int16 depId
        {
            get;
            set;
        }
    }
}