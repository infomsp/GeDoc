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

    [KnownType(typeof(proResolucionesNotificacionEmpleado))]
    [Serializable()]
    public class proResolucionesNotificacionEmpleado : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 resneId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int64 resId
        {
            get;
            set;
        }

        [DisplayName("Empleado:")]
        [UIHint("GridForeignKeyComboBox")]
        public int perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perNombre
        {
            get;
            set;
        }
    }
}