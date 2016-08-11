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

    [KnownType(typeof(catEncuestaSinadmisionPersonasEspecialidades))]
    [Serializable()]
    public class catEncuestaSinadmisionPersonasEspecialidades : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 peId
        {
            get;
            set;
        }

        [DisplayName("Especialidad:")]
        [UIHint("GridForeignKeyComboBox")]
        public int? espId
        {
            get;
            set;
        }

        [DisplayName("Interconsulta:")]
        [UIHint("GridForeignKeyComboBox")]
        public int? interconsulta
        {
            get;
            set;
        }

        [DisplayName("Derivado:")]
        [UIHint("GridForeignKeyComboBox")]
        public int? derivado
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
        public int? encId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string programadoEnNombre
        {
            get;
            set;
        }
      

          [ScaffoldColumn(false)]
        public string derDescripcion
        {
            get;
            set;
        }

          [DisplayName("Atendido Local:")]
          [UIHint("GridForeignKeyComboBox")]
          public int? atendidoLocal
          {
              get;
              set;
          }

        [ScaffoldColumn(false)]
          public string atendidoLocalDescripcion
          {
              get;
              set;
          }
          [DisplayName("Programó Turno:")]
          [UIHint("GridForeignKeyComboBox")]
          public int? programado
          {
              get;
              set;
          }
         
          [DisplayName("Programado En:")]
          [UIHint("GridForeignKeyComboBox")]
          public int? programadoEn
          {
              get;
              set;
          }

        [DisplayName("Programado Cuando:")]
        [DataType(DataType.Date)]
         public DateTime? programadoCuando
         {
             get;
             set;
         }


        [ScaffoldColumn(false)]
        //[UIHint("LimitedTextArea")]   
        [DisplayName("Comentario")]
        public string comentario
        {
            get;
            set;
        }


        [ScaffoldColumn(false)]
        [DisplayName("InterconsultaDesc:")]
        public string interconsulta_desc
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("DerivadoDesc:")]
        public string derivado_desc
        {
            get;
            set;
        }


    }
}