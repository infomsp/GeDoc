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

    [KnownType(typeof(GraficoPoblacionCargo))]
    [Serializable()]
    public class GraficoPoblacionCargo : EntityObject
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

        public string CentroSalud
        {
            get;
            set;
        }
        public int? Edades
        {
            get;
            set;
        }
        public string Sexos
        {
            get;
            set;
        }
        public List<GraficoPoblacionCargo> Datos
        {
            get;
            set;
        }
        public int menosde1
        {
            get;
            set;
        }
        public int de1a4
        {
            get;
            set;
        }
        public int de5a14
        {
            get;
            set;
        }
        public int de15a19
        {
            get;
            set;
        }
        public int de20a39
        {
            get;
            set;
        }
        public int de40a69
        {
            get;
            set;
        }
        public int de70yMas
        {
            get;
            set;
        }
       

    }
}
