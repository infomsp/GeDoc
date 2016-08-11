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

    [KnownType(typeof(GraficoLogUsuario))]
    [Serializable()]
    public class GraficoLogUsuario : EntityObject
    {
        public DateTime Fecha
        {
            get;
            set;
        }

        public int Cantidad
        {
            get;
            set;
        }

        public string Usuario
        {
            get;
            set;
        }

        public List<GraficoLogUsuario> Datos
        {
            get;
            set;
        }
    }
}