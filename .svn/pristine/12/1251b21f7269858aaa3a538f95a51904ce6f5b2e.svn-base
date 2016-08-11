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
    [KnownType(typeof(catHCPerConsultasWS))]
    [Serializable()]
    public class catHCPerConsultasWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 perConsId
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
        public Int64? turId
        {
            get;
            set;
        }

         [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime? perCFecha
        {
            get;
            set;
        }
        [RegularExpression("([1-9][0-9]*)")] 
        [ScaffoldColumn(false)]
        public Int32? perCEdadGes
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? perCpeso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCPA
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCAlturaUt
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCPresenta
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCFCF
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCMovF
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCSignos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perCTecnico
        {
            get;
            set;
        }
         [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime? perCFechaProx
        {
            get;
            set;
        }

        public string pAccion { get; set; }
    }
}