using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeDoc.Models.RegistroProfesionales
{
    public class catProfesionalTitulosWS
    {
        public int pId { get; set; }
        public int ptitId { get; set; }
        [UIHint("GridForeignKeyComboBox")]
        public Int16 profId { get; set; }
        //[UIHint("GridForeignKeyComboBox")]
        //public Int32 titId { get; set; }
        [UIHint("GFKAutoCompleteOnChange")]
        public string oeNombre { get; set; }

        [UIHint("GFKAutoCompleteOnChange")]
        public string profNombre
        {
            get;
            set;
        }  
        //[UIHint("GridForeignKeyComboBox")]
        public Int16 paisTituloId { get; set; }
        //[UIHint("GridForeignKeyComboBox")]
        public Int16 provTituloId { get; set; }
        //[UIHint("GridForeignKeyComboBox")]
        public Int16 depTituloId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha del diploma")]
        [DataType(DataType.Date)]
        public DateTime ptitFechaDiploma { get; set; }
        public string ptitObservacionDiploma { get; set; }
        public int ptitMarticulaNro { get; set; }
        public int ptitMatriculaFolio { get; set; }
        public int ptitMatriculaLibro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha de inscripción de la matricula")]        
        [DataType(DataType.Date)]
        public DateTime ptitMatriculaFechaInscr { get; set; }

        public string ptitMatriculaObservacion { get; set; }

        [UIHint("GFKAutoCompleteOnChange")]
        public string ptitTitulo { get; set; }
        public string ptitProfesion { get; set; }        
        public string ptitOrganismoEmisor { get; set; }
        public string pAccion { get; set; }
        
    }
}
