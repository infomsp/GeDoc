using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeDoc.Models.RegistroProfesionales
{
    public class rpProfesionalConsulta
    {
        public catProfesionalWS profesional { get; set; }
        public List<regProDetalleTramite> tramites { get; set; }
        public catProfesionalTitulosWS titulo { get; set; }
    }
}