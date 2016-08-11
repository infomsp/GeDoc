/************************************************************************************************************
 * Descripción: Clase referente a la estructura de llamado de pacientes...
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 08/02/2016	GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catCentroDeSaludLLamadorWS))]
    [Serializable()]
    public class catCentroDeSaludLLamadorWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 llamId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string llamTexto1
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string llamTexto2
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime llamFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool llamLlamar
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string llamIdentificador
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string llamOficina
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? csstId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string llamOrigen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime llamFechaDeLlamado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string llamHoraDeLlamado
        {
            get;
            set;
        }
    }
}