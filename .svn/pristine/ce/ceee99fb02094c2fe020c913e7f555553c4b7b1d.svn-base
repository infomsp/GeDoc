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

    [KnownType(typeof(sisUsuariosCentroDeSalud))]
    [Serializable()]
    public class sisUsuariosCentroDeSalud : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 ucsId
        {
            get;
            set;
        }

        [DisplayName("Centro de Salud:")]
        [UIHint("GridForeignKeyComboBox")]
        public int csId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ucsCentroDeSalud
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