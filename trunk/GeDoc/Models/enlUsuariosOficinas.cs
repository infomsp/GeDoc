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

    [KnownType(typeof(enlUsuariosOficinas))]
    [Serializable()]
    public class enlUsuariosOficinas : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 uoId
        {
            get;
            set;
        }

        [DisplayName("Oficina:")]
        [UIHint("GridForeignKeyComboBox")]
        public int ofiId
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

        [ScaffoldColumn(false)]
        public int usrId
        {
            get;
            set;
        }
    }
}