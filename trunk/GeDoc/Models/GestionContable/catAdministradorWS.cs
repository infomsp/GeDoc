namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catAdministradorWS))]
    [Serializable()]
    public class catAdministradorWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int admId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Required(ErrorMessage = "Debe especificar el código")]
        [StringLength(7, ErrorMessage = "Código muy largo, máximo 7 carácteres")]
        public string admCodigo
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage="Debe especificar la descripción")]
        [StringLength(80, ErrorMessage = "Texto muy largo, máximo 80 carácteres")]
        public string admDescripcion
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Cuenta Escritural:")]
        public short ceId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ceDescripcion
        {
            get;
            set;
        }
    }
}