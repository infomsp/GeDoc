using System.ComponentModel;

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;

    [KnownType(typeof(proEstadisticaPlanillas))]
    [Serializable()]
    public class proEstadisticaPlanillas : EntityObject
    {
        [ScaffoldColumn(false)]
        public int plaId
        {
            get;
            set;
        }

         [ScaffoldColumn(false)]
        public string centroSaludTexto
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Centro de Salud:")]
        public int? csId
        {
            get;
            set;
        }

         [ScaffoldColumn(false)]
        public string departamentoTexto
        {
            get;
            set;
        }


        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Departamento:")]
        public Int16? depId
        {
            get;
            set;
        }

       [ScaffoldColumn(false)]
        public DateTime? plaFecha 
        {
            get;
            set;
        }

       
        
        [ScaffoldColumn(false)]
        public int? usrId
        {
            get;
            set;
        }

        [DisplayName("Mes:")]
        [UIHint("GridForeignKeyComboBox")]
        public int? plaMes
        {
            get;
            set;
        }
        
        [ScaffoldColumn(false)]
        public string plaMesTexto
        {
            get;
            set;
        }

        [DisplayName("Año:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el Año de la planilla a generar")]
        [Range(2015, 2200, ErrorMessage = "Número incorrecto (Rango entre 2015-2200)")]
        [UIHint("Integer")]
        public int? plaAnio
        {
            get;
            set;
        }


        [DisplayName("Observaciones:")]
        public string plaObservaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public bool? plaValidado
        {
            get;
            set;
        }


    }
}