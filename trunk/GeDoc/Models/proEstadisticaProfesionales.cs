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

    [KnownType(typeof(proEstadisticaProfesionales))]
    [Serializable()]
    public class proEstadisticaProfesionales : EntityObject
    {
        [ScaffoldColumn(false)]
        public int perId { get; set; }


         [ScaffoldColumn(false)]
        public string perApellidoyNombre { get; set; }

          [ScaffoldColumn(false)]

        public string ControlEmbarazo { get; set; }

          [ScaffoldColumn(false)]
          public string espNombre { get; set; }


    }
}