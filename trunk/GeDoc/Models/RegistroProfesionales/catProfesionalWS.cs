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

    [KnownType(typeof(catProfesionalWS))]
    [Serializable()]
    public class catProfesionalWS : EntityObject
    {
        public catProfesionalWS()
        {
            this.pFotoPerfil = "~/Content/Fotos/Profesionales/default.png";
        }

        public bool modoLectura { get; set; }

        [ScaffoldColumn(false)]
        public int pId { get; set; }
        [Required(ErrorMessage = "Debe especificar el Nombre")]
        public string pNombre { get; set; }
        [Required(ErrorMessage = "Debe especificar el Apellido")]
        public string pApellido { get; set; }
        [DisplayName("Tipo Documento:")]
        [UIHint("GridForeignKeyComboBoxLoad")]
        public Int16 tipoIdDocumento { get; set; }
        [DisplayName("Sexo:")]
        [UIHint("GridForeignKeyComboBoxLoad")]
        public Int16 tipoIdSexo { get; set; }


        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> paisId { get; set; }
        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> provId { get; set; }
        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> depId { get; set; }
        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> locId { get; set; }

        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> paisIdNacimiento { get; set; }
        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> provIdNacimiento { get; set; }
        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> depIdNacimiento { get; set; }
        [UIHint("GridForeignKeyComboBox")]
        public Nullable<Int16> locIdNacimiento { get; set; }

        
        [DisplayName("Lugar de Nacimiento:")]
        [UIHint("GridForeignKeyComboBoxLoad")]
        public Nullable<Int16> paisIdNacionalidad { get; set; }
        public string paisNacionalidad { get; set; }
        public bool pTieneFirmaDigital { get; set; }
        public bool pFallecido { get; set; }
        
        
        public string pFotoPerfil { get; set; }
        public string pFotoPerfilCanvas { get; set; }
  

        public string pCalle { get; set; }
        public Nullable<int> pNroCalle { get; set; }
        public Nullable<int> pPiso { get; set; }
        public string pBarrio { get; set; }
        public Nullable<int> pNroDpto { get; set; }
        public Nullable<int> pCP { get; set; }
        public Nullable<bool> pTieneTelefono { get; set; }
        [EmailAddress(ErrorMessage = "Debe ingresar un e-mail válido")]
        public string pEmail { get; set; }
        [EmailAddress(ErrorMessage = "Debe ingresar un e-mail válido")]
        public string pEmail2 { get; set; }
        public string pObservaciones { get; set; }
        public DateTime pFechaAlta { get; set; }
        public DateTime pFechaActualizacion { get; set; }
        
        [UIHint("GridForeignKeyComboBoxLoad")]
        public Nullable<short> tipoIdTelefono1 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public Nullable<int> pTelefono1 { get; set; }
                
        [UIHint("GridForeignKeyComboBoxLoad")]
        public Nullable<short> tipoIdTelefono2 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public Nullable<int> pTelefono2 { get; set; }

        [UIHint("GridForeignKeyComboBoxLoad")]
        public Nullable<short> tipoIdTelefono3 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public Nullable<int> pTelefono3 { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        public Nullable<int> pTelefono4 { get; set; }
        [UIHint("GridForeignKeyComboBoxLoad")]
        public Nullable<short> tipoIdTelefono4 { get; set; }
        
        
        [DisplayName("DNI:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el DNI")]
        [Range(99999, 99999999, ErrorMessage = "Número incorrecto (Rango entre 99999-99999999)")]
        [UIHint("Integer")]
        public int pNumDocumento
        {
            get;
            set;
        }

        [DisplayName("CUIL:")]
        [Required(ErrorMessage = "Debe ingresar el número de CUIL")]
        [StringLength(13, ErrorMessage = "CUIL Incorrecto (Ej: 20-28321920-5)", MinimumLength = 13)]
        [RegularExpression(@"[0-9]{2}-[0-9]{8}-[0-9]", ErrorMessage = "CUIL inválido (Ej: 20-28321920-5)")]
        public string pCuil
        {
            get;
            set;
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        [DisplayName("Fecha de Nacimiento:")]
        [DataType(DataType.Date)]
        public DateTime pFechaNacimiento
        {
            get;
            set;
        }

        [DisplayName("Fecha de Fallecimiento:")]
        [DataType(DataType.Date)]
        public DateTime? pFechaFallecimiento
        {
            get;
            set;
        }

    }
}
