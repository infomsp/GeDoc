namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catGradosCategoriaWS))]
    [Serializable()]
    public class catGradosCategoriaWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public short gracId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Grado:")]
        public short graId
        {
            get;
            set;
        }

        [DisplayName("Categoría:")]
        [Required(ErrorMessage = "Debe especificar la categoría")]
        [StringLength(150, ErrorMessage = "Texto muy largo, máximo 150 carácteres")]
        public string gracDescripcion
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Grado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string Agrupamiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int gracEmpleados
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int gracPresupuestado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int gracLibres
        {
            get;
            set;
        }

    }
}