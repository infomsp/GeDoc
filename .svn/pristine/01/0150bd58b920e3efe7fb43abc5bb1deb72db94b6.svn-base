using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeDoc.Models.RegistroProfesionales
{
    public class regProDetalleTramite
    {
        public proRegistroProfesionalTramite tramite { get; set; }
        public string profesionalNombre { get; set; }        
        public List<rpItemTramite> requerimientosDeTramite { get; set; }
    }

    public class rpItemTramite
    {
        public int gtId { get; set; }
        public int traId { get; set; }
        public string gtDescripcion { get; set; }
        public List<rpItemTramiteDetalle> detalle { get; set; }
    }

    public class rpItemTramiteDetalle
    {
        public int gtdId { get; set; }        
        public string gtdDescripcion { get; set; }
        public bool gtdValor { get; set; }
        public DateTime gtdFecha { get; set; }
    }



}