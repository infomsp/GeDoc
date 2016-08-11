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
    [KnownType(typeof(catSectores))]
    [Serializable()]
    public class catSectores : EntityObject
    {
        [ScaffoldColumn(false)]
        public short secId
        {
            get;
            set;
        }

        [DisplayName("Código:")]
        [Required(ErrorMessage = "Debe ingresar el Código")]
        [UIHint("Integer")]
        public Int32 secCodigo
        {
            get;
            set;
        }

        [DisplayName("Nombre:")]
        [Required(ErrorMessage="Debe especificar el Nombre")]
        public string secNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Ubicación:")]
        [Required(ErrorMessage = "Debe especificar la Ubicación")]
        public string secUbicacionGeografica
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string secLinkMapa
        {
            get;
            set;
        }

        [UIHint("GridForeignKey")]
        [DisplayName("Zona:")]
        public int repId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string repNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int Empleados
        {
            set;
            get;
        }

        [UIHint("GridForeignKey")]
        [DisplayName("Cuenta Contable:")]
        public int? ccId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catCuentasContables ccCuentaContable
        {
            get;
            set;
        }

    }
}