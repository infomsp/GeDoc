namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;
    using System.Web.Security;

    public class Accion
    {
        public string MenuController { get; set; }
        public string _Accion { get; set; }
        public string Visibilidad { get; set; }
    }
}
