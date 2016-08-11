/************************************************************************************************************
 * Descripción: Clase referente a la estructura de Encuesta planilla sin Admisionar
 * Observaciones: 
 * Creado por: Rolando Delgado
 * Historial de Revisiones:
 * -----------------------------------------------------------------------------------------------
 * Fecha        Autor                   Descripción
 * -----------------------------------------------------------------------------------------------
 * 13/01/2016	Rolando Delgado 		Implementación inicial
 * -----------------------------------------------------------------------------------------------
*/
using System.ComponentModel;

namespace GeDoc
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;

    [KnownType(typeof(catEncuestasSinAdmisionPlanilla))]
    [Serializable()]
    public class catEncuestasSinAdmisionPlanilla : EntityObject
    {
        [ScaffoldColumn(false)]
        public int plaId
        {
            get;
            set;
        }

        [ScaffoldColumn(false)]
        public string centroSaludTexto
        {
            get;
            set;
        }

        //[UIHint("GridForeignKeyComboBox")]
        //[DisplayName("Centro de Salud:")]
        [ScaffoldColumn(false)]
        public int? csId
        {
            get;
            set;
        }

     
        [DisplayName("Fecha de Encuesta:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe Ingresar Fecha.")]
        [DataType(DataType.Date)]
        public DateTime? plaFechaPlanilla
        {
            get;
            set;
        }



        [ScaffoldColumn(false)]
        public int? usrId
        {
            get;
            set;
        }
        [ScaffoldColumn(false)]
        public string usrNombre
        {
            get;
            set;
        }

        [UIHint("GridForeignKeyComboBox")]
        [DisplayName("Encuestador:")]
        public int? encuestadorId
        {
            get;
            set;
        }

     
         [ScaffoldColumn(false)]
        public string encuestadorApyNom
        {
            get;
            set;
        }

         [ScaffoldColumn(false)]
         public int? cantPacientes
         {
             get;
             set;
         }
        //semáforo
         [ScaffoldColumn(false)]
         public int? resuelto
         {
             get;
             set;
         }
    }
}