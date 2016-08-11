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

    public class catAgendas : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int32 agId
        {
            get;
            set;
        }

        [DisplayName("Profesional:")]
        [UIHint("GridForeignKeyComboBoxOnChange")]
        public Int32? perId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32? conId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public Int32 csId
        {
            get;
            set;
        }

        [DisplayName("Especialidad:")]
        [UIHint("GridForeignKeyComboBox")]
        public short espId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catEspecialidades Especialidad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Profesional
        {
            get;
            set;
        }

      //  [DisplayName("Duración:")]
        //[Range(5, 120, ErrorMessage = "Duración incorrecta (Rango entre 5-120)")]
          [ScaffoldColumn(false)]
        [UIHint("Integer")]
        public int agDuracion
        {
            get;
            set;
        }

      //  [DisplayName("Sobreturnos:")]
       // [Range(1, 5, ErrorMessage = "Valor incorrecto (Rango entre 1-5)")]
          [ScaffoldColumn(false)]
        [UIHint("Integer")]
        public int agSobreturnos
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool agActivo
        {
            get;
            set;
        }
    }
}