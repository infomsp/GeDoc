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

    [KnownType(typeof(catPersonas))]
    [Serializable()]
    public class catPersonas : EntityObject
    {
        [ScaffoldColumn(false)]
        public int perId
        {
            get;
            set;
        }

        [DisplayName("Padrón:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el padrón")]
        [Range(1, 99999999, ErrorMessage = "Número incorrecto (Rango entre 1-99999999)")]
        [UIHint("Integer")]
        public long? perPadron
        {
            get;
            set;
        }

        [DisplayName("Apellido y Nombre:")]
        [Required(ErrorMessage = "Debe especificar el Apellido")]
        public string perApellidoyNombre
        {
            get;
            set;
        }

        [DisplayName("Sexo:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? tipoIdSexo
        {
            get;
            set;
        }

        [DisplayName("Lugar de Nacimiento:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? provId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string provNombre
        {
            get;
            set;
        }

        [DisplayName("DNI:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el DNI")]
        [Range(99999, 99999999, ErrorMessage = "Número incorrecto (Rango entre 99999-99999999)")]
        [UIHint("Integer")]
        public int? perDNI
        {
            get;
            set;
        }

        [DisplayName("CUIL:")]
        [Required(ErrorMessage = "Debe ingresar el número de CUIL")]
        [StringLength(13, ErrorMessage = "CUIL Incorrecto (Ej: 20-28321920-5)", MinimumLength = 13)]
        [RegularExpression(@"[0-9]{2}-[0-9]{8}-[0-9]", ErrorMessage = "CUIL inválido (Ej: 20-28321920-5)")]
        public string perCUIL
        {
            get;
            set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        [DisplayName("Fecha de Nacimiento:")]
        [DataType(DataType.Date)]
        public DateTime? perFechaNacimiento
        {
            get;
            set;
        }

        [DisplayName("Profesión:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? profId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string profNombre
        {
            get;
            set;
        }

        [DisplayName("Lee o Escribe:")]
        public bool perLeeoEscribe
        {
            get;
            set;
        }

        [DisplayName("Estado Civil:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? tipoIdEstadoCivil
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perEstadoCivil
        {
            get;
            set;
        }

        [DisplayName("Fecha de Casamiento:")]
        [DataType(DataType.Date)]
        public DateTime? perFechaCasamiento
        {
            get;
            set;
        }

        [DisplayName("Nacionalidad:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? paisId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string paisNombre
        {
            get;
            set;
        }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el teléfono")]
        [DisplayName("Teléfono:")]
        [DataType(DataType.PhoneNumber)]
        public string perTelefono
        {
            get;
            set;
        }

        [DisplayName("Celular:")]
        [DataType(DataType.PhoneNumber)]
        public string perCelular
        {
            get;
            set;
        }

        [DisplayName("Correo Electrónico:")]
        [EmailAddress(ErrorMessage="Debe ingresar un e-mail válido")]
        public string perEmail
        {
            get;
            set;
        }

        [DisplayName("Es contratado:")]
        public bool perEsContratado
        {
            get;
            set;
        }

        [DisplayName("Domicilio:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string perDomicilio
        {
            get;
            set;
        }

        [DisplayName("Notifica por SMS:")]
        public bool perAutorizaNotificarSMS
        {
            get;
            set;
        }

        [DisplayName("Observaciones:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string perObservaciones
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Sector:")]
        public int secId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short secCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string secNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Lugar de Trabajo:")]
        public int? ofiId
        {
            get;
            set;
        }

        [DisplayName("Función:")]
        [StringLength(250, ErrorMessage = "Texto muy largo, máximo 250 carácteres")]
        public string perFuncion
        {
            get;
            set;
        }

        [DisplayName("Planta Administrativa:")]
        public bool perPertenecePlantaAdministrativa
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public catOficinas Oficina
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int perEdad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perAntiguedad
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perAntiguedadPago
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perAntiguedadVacaciones
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perFechaNacimientoTexto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perFechaCasamientoTexto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tipoIdSexoTexto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short? perAntiguedadAnios
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perEstadoImagen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string VisibilidadImagen
        {
            get;
            set;
        }

        [UIHint("SubirArchivo")]
        [DisplayName("Foto:")]
        public string perFoto
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string perIdiomas
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime? perFechaEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string carDescripcion //Cargo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public short? AsistenciaCodigo
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string AsistenciaEstado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string AsistenciaImagen
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? gdId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public int? repId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ZonaSanitaria
        {
            get;
            set;
        }

    }
}