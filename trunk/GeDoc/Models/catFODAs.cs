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

    public class catFODAs : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 fodaId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 usrId
        {
            get;
            set;
        }

        [DisplayName("Fortalezas:")]
        [DataType(DataType.MultilineText)]
        public string fodaFortaleza
        {
            get;
            set;
        }

        [DisplayName("Oportunidades:")]
        [DataType(DataType.MultilineText)]
        public string fodaOportunidad
        {
            get;
            set;
        }

        [DisplayName("Debilidades:")]
        [DataType(DataType.MultilineText)]
        public string fodaDebilidad
        {
            get;
            set;
        }

        [DisplayName("Amenazas:")]
        [DataType(DataType.MultilineText)]
        public string fodaAmenaza
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public sisUsuarios Usuario
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? fodaFechaUltimoCambio
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool PermiteEditar
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 ofiId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catOficinas Oficina
        {
            get;
            set;
        }

    }
}