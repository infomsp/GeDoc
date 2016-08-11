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

    [KnownType(typeof(proDecretos))]
    [Serializable()]
    public class proDecretos : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 decId
        {
            get;
            set;
        }

        [DisplayName("Es decreto acuerdo:")]
        public bool decEsAcuerdo
        {
            get;
            set;
        }

        [DisplayName("Número:")]
        [Range(1, 10000, ErrorMessage = "Número incorrecto (Rango entre 1-10000)")]
        [UIHint("Integer")]
        public short? decNumero
        {
            get;
            set;
        }

        [DisplayName("Fecha:")]
        //[UIHint("Date")]
        [DataType(DataType.Date, ErrorMessage = "Debe ingresar la fecha del decreto")]
        public DateTime decFecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        //[UIHint("Editor")]
        //[DisplayName("Detalle:")]
        public string decConsiderando
        {
            get;
            set;
        }

        //[ScaffoldColumn(false)]
        //[UIHint("Editor"), Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Detalle:")]
        public string decResuelve
        {
            get;
            set;
        }

        [UIHint("SubirArchivo")]
        [DisplayName("Archivo:")]
        public string decLinkArchivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Resoluciones
        {
            get;
            set;
        }
    }
}