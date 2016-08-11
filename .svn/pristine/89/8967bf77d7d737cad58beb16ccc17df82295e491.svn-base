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

    [KnownType(typeof(catReparticiones))]
    [Serializable()]
    public class catReparticiones : EntityObject
    {
        [ScaffoldColumn(false)]
        public int repId
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage = "Debe especificar el Nombre")]
        public string repNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int Sectores
        {
            set;
            get;
        }

        [ScaffoldColumn(false)]
        public int Empleados
        {
            set;
            get;
        }

        [ScaffoldColumn(false)]
        public int Contratados
        {
            set;
            get;
        }
    }
}