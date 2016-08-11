/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Pacientes.
 * Observaciones: 
 * Creado por: Delgado Rolando
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 08/12/2013   RMD     Implementación inicial.
 * -----------------------------------------------------------------------------------------------
*/
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
    using GeDoc.Models;
    using System.Diagnostics;

    [KnownType(typeof(catTurnosPacientes))]
    [Serializable()]
    public class catTurnosPacientes : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pacId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxOnChange")]
        [DisplayName("Tipo de Documento:")]
        public Int16 tipoIdTipoDocumento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string tipoDescDocumento
        {
            get;
            set;
        }

        [UIHint("Integer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el Nro. de Doc.")]
        [Range(11111, 99999999, ErrorMessage = "Número incorrecto (Rango entre 99999-99999999)")]
        [DisplayName("Nro. de Doc.:")]
        public int? pacNumeroDocumento
        {
            get;
            set;
        }

        [DisplayName("Nro H.C.:")]
        public string nroHC
        {
            get;
            set;
        }

        [DisplayName("Apellido:")]
        [Required(ErrorMessage = "Debe especificar el Apellido")]
        public string pacApellido
        {
            get;
            set;
        }

        //   [ScaffoldColumn(false)]
        [DisplayName("Nombre:")]
        [Required(ErrorMessage = "Debe especificar el Nombre")]
        public string pacNombre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Apellido y Nombre:")]
        public string pacApellidoyNombre
        {
            get;
            set;
        }



        // [ScaffoldColumn(false)]
        // [Required(ErrorMessage = "Debe ingresar el número de CUIL")]
        [DisplayName("CUIL:")]
        [StringLength(13, ErrorMessage = "CUIL Incorrecto (Ej: 20-28321920-5)", MinimumLength = 13)]
        [RegularExpression(@"[0-9]{2}-[0-9]{8}-[0-9]", ErrorMessage = "CUIL inválido (Ej: 20-28321920-5)")]
        public string pacCUIL
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Sexo:")]
        public Int16 tipoIdSexo
        {
            get;
            set;
        }


        [ScaffoldColumn(false)]
        public string tipoSexoNombre
        {
            get;
            set;
        }


        // [ScaffoldColumn(false)]
        [DisplayName("Ocupación:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? tipoIdOcupacion
        {
            get;
            set;
        }


        [ScaffoldColumn(false)]
        public string OcupacionDescripcion
        {
            get;
            set;
        }

        //  [ScaffoldColumn(false)]    
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de nacimiento")]
        [DisplayName("Fecha de Nacimiento:")]
        [DataType(DataType.Date)]
        public DateTime? pacFechaNacimiento
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pacFechaNacimientoTexto
        {
            get;
            set;
        }



        [ScaffoldColumn(false)]
        public Int16? pacEdad
        {
            get;
            set;
        }

        //  [ScaffoldColumn(false)]
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


        [ScaffoldColumn(false)]
        public string provNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Obra Social:")]
        public int? osId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public String osSigla
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public String osNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Estado Civil:")]
        public Int16? tipoIdEstadoCivil
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string DescEstadoCivil
        {
            get;
            set;
        }

        //  [ScaffoldColumn(false)]
        [DisplayName("Calle:")]
        [StringLength(150)]
        public string pacCalle
        {
            get;
            set;
        }


        [UIHint("Integer")]
        [DisplayName("Calle Numero:")]
        public Int16? pacCalleNumero
        {
            get;
            set;
        }

        //  [ScaffoldColumn(false)]
        [DisplayName("Referencia del Domicilio:")]
        [StringLength(250)]
        [UIHint("LimitedTextBox")]
        public string pacDomicilioReferencias
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Provincia:")]
        public Int16 provId
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxOnChange")]
        [DisplayName("Departamento:")]
        public Int16? depId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string depNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBoxOnChange")]
        [DisplayName("Localidad:")]
        public Int16? locId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string locNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Barrio:")]
        public Int16? barId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string barNombre
        {
            get;
            set;
        }


        [DisplayName("Dona Organos:")]
        public Boolean pacDonaOrganos
        {
            get;
            set;
        }

        [DisplayName("Grupo Sanguineo:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16? tipoIdGrupoSanguineo
        {
            get;
            set;
        }

        [DisplayName("Telefono de Casa:")]
        [StringLength(20)]
        [UIHint("LimitedTextBox")]
        public string pacTelefonoCasa
        {
            get;
            set;
        }

        [DisplayName("Telefono de Trabajo:")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string pacTelefonoTrabajo
        {
            get;
            set;
        }
        //  [ScaffoldColumn(false)]
        [DisplayName("Telefono Celular:")]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string pacTelefonoCelular
        {
            get;
            set;
        }

        //  [ScaffoldColumn(false)]
        [DisplayName("Notificar por SMS:")]
        public bool pacNotificarXSMS
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        [DisplayName("Notificar por SMS:")]
        public string pacNotificarXSMS_Descripcion
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        [DisplayName("Hospitalizado:")]
        public bool pacHospitalizado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pacImagenHospitalizado
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Tranfusion de Sangre:")]
        public Int16? pacTransfusionesDeSangre
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Fecha de Ultima Tranfusion:")]
        [DataType(DataType.Date)]
        public DateTime? pacTransfusionesDeSangreUltima
        {
            get;
            set;
        }

        // [ScaffoldColumn(false)]
        [DisplayName("Correo Electronico:")]
        public String pacMail
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Centro de Salud Cercano:")]
        public Int16 csId
        {
            get;
            set;
        }


        [ScaffoldColumn(false)]
        public Int16 csaId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        [DisplayName("Centro de Salud Cercano:")]
        public string csNombre
        {
            get;
            set;
        }

        [DisplayName("Etnia:")]
        [UIHint("GridForeignKeyComboBox")]
        public Int16 etnId
        {
            get;
            set;
        }

        /*
        [ScaffoldColumn(false)]   
        [DisplayName("Datos Ginecologicos:")]
        public catPacientesGineco DatosGinecologicos
        {
            get;
            set;
        }

        [DisplayName("Datos Hábitos:")]
        public catPacientesHabitos Habitos
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]   
        [DisplayName("Datos Medio Ambiente:")]
        public catPacientesMedioAmbiente MedioHambiente
        {
            get;
            set;
        }
         [ScaffoldColumn(false)]   
        [DisplayName("Situación Psicosocial:")]
        public catPacientesSituacionPsicosocial SituacionPsicosocial
        {
            get;
            set;
        }
         [ScaffoldColumn(false)]   
         [DisplayName("Alergias:")]
        public List<enlPacientesAlergia> Alergias
        {
            get;
            set;
        }
         [ScaffoldColumn(false)]   
        [DisplayName("Antecedentes Médicos:")]
        public List<enlPacienteAntecedenteMedico> AntecedentesMedicos
        {
            get;
            set;
        }
         [ScaffoldColumn(false)]   
        [DisplayName("Emergencias:")]
        public List<enlPacientesEmergencia> Emergencias
        {
            get;
            set;
        }
         [ScaffoldColumn(false)]   
        [DisplayName("Drogas:")]
        public List<enlPacientesHabitosDrogas> Drogas
        {
            get;
            set;
        }
         [ScaffoldColumn(false)]   
       [DisplayName("Patologías:")]
        public List<enlPacientesPatologia> Patologias
        {
            get;
            set;
        }*/

        [ScaffoldColumn(false)]
        public string ErrorMessage { get; set; }


    }
}