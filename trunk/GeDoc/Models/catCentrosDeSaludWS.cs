/************************************************************************************************************
 * Descripción: Clase referente a la estructura de...
 * Observaciones: 
 * Creado por: Nombre y Apellido
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 25/02/2016			Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/

using System.ComponentModel;

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    [KnownType(typeof(catPersonas))]
    [Serializable()]
    public class catCentroDeSaludWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int csId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Required(ErrorMessage = "Ingrese el código")]
        [StringLength(15, ErrorMessage = "Número muy largo, máximo 15 carácteres")]
        public string csCodigo
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage = "Debe especificar el Nombre")]
        public string csNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Deptartamento:")]
        public Int32? depId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string depNombre
        {
            get;
            set;
        }

        [DisplayName("Latitud:")]
        public string csLatitud
        {
            get;
            set;
        }

        [DisplayName("Longitud:")]
        public string csLongitud
        {
            get;
            set;
        }

        [DisplayName("Director:")]
        public string csDirector
        {
            get;
            set;
        }

        [DisplayName("Ubicación:")]
        [UIHint("Ubicacion")]
        public string csUbicacionGeografica
        {
            get;
            set;
        }

        [DisplayName("Topología:")]
        public string csTipologia
        {
            get;
            set;
        }

        [DisplayName("Teléfono:")]
        [DataType(DataType.PhoneNumber)]
        public string csTelefono
        {
            get;
            set;
        }

        [DisplayName("Domicilio:")]
        [DataType(DataType.MultilineText)]
        public string csDomicilio
        {
            get;
            set;
        }

        [DisplayName("Repartición Pública:")]
        public bool csPublicoAuxiliar
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Zona Sanitaria:")]
        public Int32? cszId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ZonaSanitaria
        {
            get;
            set;
        }

        [DisplayName("Código Bioest.:")]
        public string CodBioestadistica
        {
            get;
            set;
        }

        [DisplayName("Código Remediar:")]
        public string CodRemediar
        {
            get;
            set;
        }

        [DisplayName("Cantidad Viviendas:")]
        public int? CantVivienda
        {
            get;
            set;
        }

        [DisplayName("Cantidad Varones:")]
        public int? CantVaron
        {
            get;
            set;
        }

        [DisplayName("Cantidad Mujeres:")]
        public int? CantMujeres
        {
            get;
            set;
        }

        [DisplayName("Cantidad Total:")]
        public int? Total
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Administrador:")]
        public Int16? admId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Administrador
        {
            get;
            set;
        }

    }
}