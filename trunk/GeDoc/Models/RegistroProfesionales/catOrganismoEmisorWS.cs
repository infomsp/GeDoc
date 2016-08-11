using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeDoc.Models.RegistroProfesionales
{
    public class catOrganismoEmisorWS
    {
        public int oeId { get; set; }
        public string oEmisorNombre { get; set; }
        public int oeSISA { get; set; }
        public Int16 paisOrgEmisorId { get; set; }
        public Int16 provOrgEmisorId { get; set; }
        public Int16 depOrgEmisorId { get; set; }
    }
}
