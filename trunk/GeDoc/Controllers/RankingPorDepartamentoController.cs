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
        public ActionResult _SelectEditingRankingDerivacionDpto(string FechaDesde, string FechaHasta, int Departamento)
        {
            return View(new GridModel(AllRankingDerivacionDpto(FechaDesde, FechaHasta, Departamento)));
        }

        private IList<spRptProturRankingDerivacionDpto> AllRankingDerivacionDpto(string FechaDesde, string FechaHasta, int Departamento)
        {
            return getDatosRankingDerivacionDpto(FechaDesde, FechaHasta, Departamento).ToList();
        }

        private IEnumerable<spRptProturRankingDerivacionDpto> getDatosRankingDerivacionDpto(string _FechaDesde, string _FechaHasta, int _Departamento)
        {
            var _Datos = (from d in context.spRptProturRankingDerivacionDpto(_FechaDesde, _FechaHasta, _Departamento).ToList()
                          select new spRptProturRankingDerivacionDpto()
                          {
                              csId = d.csId.Value,
                              dpto = d.dpto,
                              csNombre = d.csNombre,
                              Derivado = d.Derivado.Value
                          }).ToList();


            return _Datos.AsEnumerable();
        }
    }
}
