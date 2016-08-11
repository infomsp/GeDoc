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

    [KnownType(typeof(proDecretosResoluciones))]
    [Serializable()]
    public class proDecretosResoluciones : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 drId
        {
            get;
            set;
        }

        [DisplayName("Resolución:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int64 resId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 decId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string resLinkArchivo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string resNumero
        {
            get;
            set;
        }
    }
}