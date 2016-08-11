/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Pacientes.
 * Observaciones: 
 * Creado por: Delgado Rolando
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor   Descripción
 * -----------------------------------------------------------------------------------------------
 * 07/05/2015   RMD     Implementación inicial.
 * -----------------------------------------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace GeDoc
{
    [KnownType(typeof(catEncuestasPacientes))]
    [Serializable()]
    public class catEncuestasPacientes : EntityObject
    {
        [ScaffoldColumn(false)]
        public Int64 pacId
        {
            get;
            set;
        }


        [DisplayName("Tipo Documento:")]
        [UIHint("GFKComboBoxOnChangeCustom")]     
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
        [Range(0, 99999999, ErrorMessage = "Número incorrecto (Rango entre 99999-99999999)")]
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
        [DisplayName("Padron:")]
        public long? pacPadron
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

        [DisplayName("Sexo:")]
        [UIHint("GFKComboBoxOnChangeCustom")]        
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

        [DisplayName("Ocupacion:")]
        [UIHint("GFKComboBoxOnChangeCustom")]          
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

        [DisplayName("Fecha Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

         [DisplayName("Nacionalidad:")]
        [UIHint("GFKComboBoxOnChangeCustom")]        
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

         [DisplayName("Obra Social:")]
        [UIHint("GFKComboBoxOnChangeCustom")]      
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

        [UIHint("GFKComboBoxOnChangeCustom")]       
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


      
        [DisplayName("Calle Numero:")]
        public Int16? pacCalleNumero
        {
            get;
            set;
        }



        [ScaffoldColumn(false)]
        public string pacManzana
        {
            get;
            set;
        }


        [ScaffoldColumn(false)]
        public string pacPiso
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string pacDepto
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

        [UIHint("GFKComboBoxOnChangeCustom")]        
        public Int16 provId
        {
            get;
            set;
        }

        [UIHint("GFKComboBoxOnChangeCustom")]      
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

        [UIHint("GFKComboBoxOnChangeCustom")]       
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

        [UIHint("GFKComboBoxOnChangeCustom")]       
        public int? barId
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
        
        [UIHint("GFKComboBoxOnChangeCustom")]           
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

        
        [DisplayName("Centro de Salud Cercano:")]
        public Int16 csId
        {
            get;
            set;
        }


       [UIHint("GFKComboBoxOnChangeCustom")]
        public Int16 csaId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string pacAccion
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

        [UIHint("GFKComboBoxOnChangeCustom")]
        [DisplayName("Etnia:")]     
        public Int16 etnId
        {
            get;
            set;
        }

        
        [ScaffoldColumn(false)]
        public List<enlEncuestaAPSPersonasEnfermedades> PacienteEnfermedad
        {
            get;
            set;
        }


        public Int32 encId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string ErrorMessage { get; set; }
        
    }
}