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

    [KnownType(typeof(catProductosImprenta))]
    [Serializable()]
    public class catProductosImprenta : EntityObject
    {
        [ScaffoldColumn(false)]
        public int piId
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage="Debe especificar la Descripción")]
        public string piDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool piActivo
        {
            get;
            set;
        }
    }
}