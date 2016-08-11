/************************************************************************************************************
 * Descripción: Clase referente a la estructura de bienes inventariados...
 * Observaciones: 
 * Creado por: Gustavo N. Tripolone
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 17/09/2013   GNT     Implementación inicial.
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
    [KnownType(typeof(catBien))]
    [Serializable()]
    public class catBien : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 biId
        {
            get;
            set;
        }

        [DisplayName("Sector:")]
        [UIHint("GridForeignKeyComboBox")]
        public int cscId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Sector
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Required(ErrorMessage = "Ingrese el código")]
        [StringLength(15, ErrorMessage = "Número muy largo, máximo 15 carácteres")]
        public string biCodigo
        {
            get;
            set;
        }

        [DisplayName("Detalle 1/Equipo:")]
        [StringLength(50, ErrorMessage = "Texto muy largo, máximo 50 carácteres")]
        [Required(ErrorMessage = "Ingrese el Detalle 1")]
        [DataType(DataType.MultilineText)]
        public string biDetalle1
        {
            get;
            set;
        }

        [DisplayName("Detalle 2:")]
        [StringLength(50, ErrorMessage = "Texto muy largo, máximo 50 carácteres")]
        [Required(ErrorMessage = "Ingrese el Detalle 2")]
        [DataType(DataType.MultilineText)]
        public string biDetalle2
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        [Required(ErrorMessage = "Ingrese la Descripción")]
        [DataType(DataType.MultilineText)]
        public string biDescripcion
        {
            get;
            set;
        }

        [DisplayName("Es Biomédico:")]
        public bool biEsBiometrico
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int Movimientos
        {
            get;
            set;
        }

        [DisplayName("Código Biomédico:")]
        [StringLength(20, ErrorMessage = "Número muy largo, máximo 20 carácteres")]
        public string biCodigoBiomedico
        {
            get;
            set;
        }

        [DisplayName("Marca:")]
        [StringLength(150, ErrorMessage = "Número muy largo, máximo 150 carácteres")]
        public string biMarca
        {
            get;
            set;
        }

        [DisplayName("Modelo:")]
        [StringLength(80, ErrorMessage = "Número muy largo, máximo 80 carácteres")]
        public string biModelo
        {
            get;
            set;
        }

        [DisplayName("Nº de Serie:")]
        [StringLength(50, ErrorMessage = "Número muy largo, máximo 50 carácteres")]
        public string biNroSerie
        {
            get;
            set;
        }

        [DisplayName("Fabricación:")]
        [DataType(DataType.Date)]
        public DateTime? biFabricacion
        {
            get;
            set;
        }

        [DisplayName("Manuales:")]
        [StringLength(150, ErrorMessage = "Número muy largo, máximo 50 carácteres")]
        public string biManuales
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        [DataType(DataType.MultilineText)]
        public string biObservaciones
        {
            get;
            set;
        }

    }
}