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
        public ActionResult _SelectEditingRankingDerivacion(string FechaDesde, string FechaHasta, string CentroDeSalud)
        {
            return View(new GridModel(AllRankingDerivacion(FechaDesde, FechaHasta, CentroDeSalud)));
        }



        private IList<spRptProturRankingDerivacion> AllRankingDerivacion(string FechaDesde, string FechaHasta, string CentroDeSalud)
        {
            return getDatosRankingDerivacion(FechaDesde, FechaHasta, CentroDeSalud).ToList();
        }

        private IEnumerable<spRptProturRankingDerivacion> getDatosRankingDerivacion(string _FechaDesde, string _FechaHasta, string _CentroDeSalud)
        {
            var _Datos = (from d in context.spRptProturRankingDerivacion(_FechaDesde, _FechaHasta, _CentroDeSalud).ToList()
                          select new spRptProturRankingDerivacion()
                          {
                              csId = d.csId.Value,
                              centro_de_salud = d.centro_de_salud,
                              Derivado = d.derivado.Value
                          }).ToList();


            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GraficoDerivados(string fechadesde, string fechahasta, string centro)
        {
            var Json = "[";
            int i = 0;

            foreach (var item in context.spRptProturRankingDerivacion(fechadesde, fechahasta, centro))
            {
                i++;
                Json += "{";
                Json += "\"label\"" + ":" + "\"" + item.centro_de_salud + "\",";
                Json += "\"value\"" + ":" + "\"" + item.derivado + "\"";
                Json += "},";
            }

            Json = Json.Trim();
            Json = Json.Remove(Json.Length - 1);
            if (i > 0)
            {
                Json += "]";
            }
            return new JsonResult { Data = Json };
        }
    }
}
