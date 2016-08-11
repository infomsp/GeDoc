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

    [KnownType(typeof(sisUsuarios))]
    [Serializable()]
    public class sisUsuarios : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int16 usrId
        {
            get;
            set;
        }

        [DisplayName("Nombre de Usuario:")]
        [Required(ErrorMessage = "Debe especificar el Nombre de Usuario")]
        public string usrNombreDeUsuario
        {
            get;
            set;
        }

        [DisplayName("Apellido y Nombre:")]
        [Required(ErrorMessage = "Debe especificar el Apellido y Nombre")]
        public string usrApellidoyNombre
        {
            get;
            set;
        }

        [DisplayName("Contraseña:")]
        [DataType(DataType.Password)]
        public string usrPassword
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perNombre
        {
            get;
            set;
        }

        [DisplayName("Estilo:")]
        [UIHint("GridForeignKey")]
        public Int16? estId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string estDescripcion
        {
            get;
            set;
        }

        [DisplayName("Correo Electrónico:")]
        [EmailAddress(ErrorMessage = "Debe ingresar un e-mail válido")]
        public string usrEmail
        {
            get;
            set;
        }

        [DisplayName("Zona Sanitaria:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public Int32? repId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catReparticiones ZonaSanitaria
        {
            get;
            set;
        }

        [DisplayName("Es Médico:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public int? perId
        {
            get;
            set;
        }

        [DisplayName("Institución Contable:")]
        [UIHint("GridForeignKeyComboBoxNull")]
        public Int32? icId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string InstitucionContable
        {
            get;
            set;
        }

    }
}