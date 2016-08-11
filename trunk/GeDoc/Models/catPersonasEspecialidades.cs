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

    [KnownType(typeof(catPersonasEspecialidades))]
    [Serializable()]
    public class catPersonasEspecialidades : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 peId
        {
            get;
            set;
        }

        [DisplayName("Especialidad:")]
        [UIHint("GridForeignKeyComboBox")]
        public int espId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catEspecialidades peEspecialidades
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int perId
        {
            get;
            set;
        }
    }
}