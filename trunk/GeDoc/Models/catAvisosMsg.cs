namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;

    public class catAvisoMsg : EntityObject
    {
        [ScaffoldColumn(false)]
        public int aviId { get; set; }

        [ScaffoldColumn(false)]
        public int usrId { get; set; }

        [ScaffoldColumn(false)]
        public string aviTitulo { get; set; }

        [ScaffoldColumn(false)]
        public string aviContenido { get; set; }

        [ScaffoldColumn(false)]
        public int aviRestantes { get; set; }

        [ScaffoldColumn(false)]
        public bool aviActivo { get; set; }

    }

}