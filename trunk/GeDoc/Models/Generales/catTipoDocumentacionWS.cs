namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    [KnownType(typeof(catTipoDocumentacionWS))]
    [Serializable()]
    public class catTipoDocumentacionWS : EntityObject
    {
        [ScaffoldColumn(false)]
        public int tdId
        {
            get;
            set;
        }

        [DisplayName("Descripción:")]
        [Required(ErrorMessage = "Ingrese la Descripción")]
        public string tdDescripcion
        {
            get;
            set;
        }
    }
}