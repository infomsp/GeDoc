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

    [KnownType(typeof(enlEstadisticaPlanillaEspecialidades))]
    [Serializable()]
    public class enlEstadisticaPlanillaEspecialidades : EntityObject
    {
        [ScaffoldColumn(false)]
        public int peId
        { get; set; }

         [ScaffoldColumn(false)]
        public int plaId { get; set; }

        [DisplayName("Especialidad:")]
        [UIHint("GridForeignKeyComboBox")]
        public int espId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public int? resId
        { get; set; }
       

        [ScaffoldColumn(false)]
        public catEspecialidades Especialidades
        { get; set; }

        [ScaffoldColumn(false)]
        public int? planillaValidada
        { get; set; }

        [ScaffoldColumn(false)]
        public string planillaValidadaImagen
        { get; set; }

        [ScaffoldColumn(false)]
        public int? planillaGenerada
        { get; set; }
    }
}
