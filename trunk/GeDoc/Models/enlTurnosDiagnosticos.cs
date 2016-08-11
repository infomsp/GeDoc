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

    [KnownType(typeof(enlTurnosDiagnosticos))]
    [Serializable()]
    public class enlTurnosDiagnosticos : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 tdId
        {
            get;
            set;
        }

        [DisplayName("Diagnostico:")]
        //[UIHint("GridForeignKeyComboBox")]
        public Int64 diagId
        {
            get;
            set;
        }

      
        [ScaffoldColumn(false)]
        public Int64 turId
        {
            get;
            set;
        }
        public string diagnosticoDescripcion
        {
            get;
            set;
        }
        public string diagnosticoTipo1_Nombre
        {
            get;
            set;
        }
        public string diagnosticoTipo2_Nombre
        {
            get;
            set;
        }
        public string Medico
        {
            get;
            set;
        }
        public DateTime Fecha
        {
            get;
            set;
        }
        public string Especialidad
        {
            get;
            set;
        }
        public string CentroDeSalud
        {
            get;
            set;
        }
    }
}