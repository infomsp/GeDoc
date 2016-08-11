using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    //controlador de Reporte de ranking de causas comparte con los demás controladores----------------------------------------------------------------------------
    public partial class ReporteProturAvanceController : Controller
    {

        [GridAction]
        public ActionResult _SelectEditingRankingCausas(string FechaDesde, string FechaHasta, string CentroDeSalud)
        {
            return View(new GridModel(AllRankingCausas(FechaDesde, FechaHasta, CentroDeSalud)));
        }



        private IList<spRptProturRankingCausa> AllRankingCausas(string FechaDesde, string FechaHasta, string CentroDeSalud)
        {
            return getDatosRankingCausas(FechaDesde, FechaHasta, CentroDeSalud).ToList();
        }

        private IEnumerable<spRptProturRankingCausa> getDatosRankingCausas(string _FechaDesde, string _FechaHasta, string _CentroDeSalud)
        {
            var _Datos = (from d in context.spRptProturRankingCausa(_FechaDesde, _FechaHasta, _CentroDeSalud).ToList()
                          select new spRptProturRankingCausa()
                          {
                              csId = d.csId.Value,
                              centro_de_salud = d.centro_de_salud,
                              causa = d.causa,
                              cantidad = d.cantidad.Value
                          }).ToList();

            
            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Grafico(string fechadesde, string fechahasta, string centro)
        {
            var Json = "[";
            int i=0;

            foreach (var item in context.spRptProturRankingCausa(fechadesde,fechahasta,centro))
            {
                i++;
                Json += "{";
                Json += "\"label\""+":"+"\""+item.centro_de_salud+"\",";
                Json += "\"cant\"" + ":" + "\"" + item.cantidad + "\",";
                Json += "\"causa\"" + ":" + "\"" + item.causa + "\"";
                Json += "},";
            }

            Json = Json.Trim();
            Json = Json.Remove(Json.Length - 1);
            if (i > 0)
            {
                Json += "]";
            }
            return new JsonResult { Data = Json};
        }

        //private JsonResult Grafico(string FechaDesde, string FechaHasta, string CentroDeSalud)
        //{

        //    var nombre = "";
        //    var loc = "{";
        //    foreach (var item in context.catLocalidad)
        //    {
        //        if (item.depId == id)
        //        {
        //            nombre = item.locNombre.Length < 40 ? item.locNombre.ToString() : item.locNombre.Substring(0, 40) + "...";
        //            loc += "\"" + item.locId.ToString() + '"' + ":" + '"' + nombre.ToString() + "\",";
        //        }
        //    }

        //    loc = loc.Trim();
        //    loc = loc.Remove(loc.Length - 1);

        //    loc += "}";


        //    return new JsonResult() { Data = loc };
        //}
    }
}
