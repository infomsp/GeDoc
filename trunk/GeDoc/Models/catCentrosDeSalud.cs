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

    [KnownType(typeof(catCentrosDeSalud))]
    [Serializable()]
    public class catCentrosDeSalud : EntityObject
    {
        [ScaffoldColumn(false)]
        public int sucId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        public decimal sucCodigo
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        public string sucNombre
        {
            get;
            set;
        }
    }
}