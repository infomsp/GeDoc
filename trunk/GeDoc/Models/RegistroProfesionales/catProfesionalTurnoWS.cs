/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Gustavo Nicolas Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 27/03/2016	GNT		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System.ComponentModel;

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catProfesionalTurnoWS))]
    [Serializable()]
    public class catProfesionalTurnoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pturId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime pturFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pturEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int csId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? pturFechaAdmisionado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? pturTiempoDeEspera
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? pturFechaInicioAtencion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public DateTime? pturFechaFinAtencion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? pturTiempoAtencion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pturPrioridad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("DNI:")]
        [Required(ErrorMessage = "Ingrese el DNI")]
        public string pturDNI
        {
            get;
            set;
        }

        [DisplayName("Apellido y Nombre:")]
        [Required(ErrorMessage = "Ingrese el Apellido y Nombre")]
        public string pturApellidoyNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ImagenPrioridad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string EstadoImagen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Box
        {
            get;
            set;
        }

    }
}