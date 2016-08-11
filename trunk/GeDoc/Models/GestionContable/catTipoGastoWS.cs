namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catTipoGastoWS))]
    [Serializable()]
    public class catTipoGastoWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int tgId
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage="Ingrese la descripción")]
        [StringLength(150, MinimumLength=5, ErrorMessage= "Debe escribir entre 5 y 150 caracteres")]
        public string tgDescripcion
        {
            get;
            set;
        }
    }
}