using System;
using System.Linq;

namespace GeDoc.Models
{
    public class catCartillaBasicas
    {
        public int? cartId { get; set; }
        public DateTime? cartFecha { get; set; }
        public int? turId { get; set; }
        public short? espId { get; set; }
        public int? perId { get; set; }
        public int? conId { get; set; }
        public int? csId { get; set; }
        public short? usrId { get; set; }
        public long? pacId { get; set; }

        public string proApellidoYNombre { get; set; }
        public string csNombre { get; set; }
        public string espNombre { get; set; }
        public string usrApellidoyNombre { get; set; }

        public int? resp1 { get; set; }
        public int? resp2 { get; set; }
        public int? resp3 { get; set; }
        public int? resp4 { get; set; }
        public int? resp5 { get; set; }
        public int? resp6 { get; set; }
        public int? resp7 { get; set; }
        public int? resp8 { get; set; }
        public decimal? resp9 { get; set; }
        public int? resp10 { get; set; }
        public int? resp11 { get; set; }
        public int? resp12 { get; set; }
        public int? resp13 { get; set; }
        public decimal? resp14 { get; set; }
        public decimal? resp15 { get; set; }
        public int? minId { get; set; }
        public string minDescripcion { get; set; }
        public string LugarTrabajo { get; set; }

        //public IQueryable<string> Diagnosticos { get; set; }
        //public IQueryable<string> Practicas { get; set; }
    }
}