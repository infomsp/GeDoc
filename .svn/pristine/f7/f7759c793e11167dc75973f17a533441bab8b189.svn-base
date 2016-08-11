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

    // Los Sectores son tomados de la tabal expOficina del sistema de expedientes
    [KnownType(typeof(catSubPartidas))]
    [Serializable()]
    public class catSubPartidas : EntityObject
    {
        [ScaffoldColumn(false)]
        public short subpId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Partida:")]
        public int partId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string partNombre
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Required(ErrorMessage = "Debe especificar el código")]
        [StringLength(13, ErrorMessage = "Código muy largo, máximo 13 carácteres")]
        public string subpCodigo
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage="Debe especificar el Nombre")]
        [StringLength(80, ErrorMessage = "Nombre muy largo, máximo 80 carácteres")]
        public string subpNombre
        {
            get;
            set;
        }
    }
}