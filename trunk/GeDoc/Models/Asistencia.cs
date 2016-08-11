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
    using GeDoc.Models;

    [KnownType(typeof(Asistencia))]
    [Serializable()]
    public class Asistencia : EntityObject
    {
        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? Fecha
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short? Codigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Estado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? Entrada
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? Salida
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string AsistenciaImagen
        {
            get;
            set;
        }

    }
}