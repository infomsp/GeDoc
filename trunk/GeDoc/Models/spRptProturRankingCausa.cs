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

    [KnownType(typeof(spRptProturRankingCausa))]
    [Serializable()]
    public class spRptProturRankingCausa : EntityObject
    {
        [ScaffoldColumn(false)]
        public int? csId
        {
            get;
            set;
        }

        [DisplayName("Centro de Salud:")]
        [ScaffoldColumn(false)]
        public string centro_de_salud
        {
            get;
            set;
        }

        [DisplayName("Causa:")]
        [ScaffoldColumn(false)]
        public string causa
        {
            get;
            set;
        }

        [DisplayName("Cantidad:")]
        [ScaffoldColumn(false)]
        public int? cantidad
        {
            get;
            set;
        }
    }
}