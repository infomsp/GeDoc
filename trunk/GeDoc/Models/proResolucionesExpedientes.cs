﻿namespace GeDoc
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

    [KnownType(typeof(proResolucionesExpedientes))]
    [Serializable()]
    public class proResolucionesExpedientes : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 reseId
        {
            get;
            set;
        }

        [DisplayName("Zona:")]
        [UIHint("GridForeignKey")]
        public int zonId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string zonNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int zonCodigo
        {
            get;
            set;
        }

        [DisplayName("Número:")]
        [Range(1, 10000, ErrorMessage = "Número incorrecto (Rango entre 1-10000)")]
        [Required(ErrorMessage = "Debe especificar un número válido")]
        public int reseExpedienteNumero
        {
            get;
            set;
        }

        [DisplayName("Año:")]
        [Range(2000, 2300, ErrorMessage = "Año incorrecto (Rango entre 2000-2300)")]
        [Required(ErrorMessage = "Debe especificar un año válido")]
        public int reseExpedienteAnio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 resId
        {
            get;
            set;
        }
    }
}