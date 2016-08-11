using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeDoc.Models;

namespace GeDoc.Controllers
{ 
    public partial class catUmbralTiemposTurnoEspecialidadController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        public ActionResult Index()
        {
            cargaCentroDeSaludPorUsuario();
            CargaEspecialidadesPorCS(0);
            return PartialView();
        }

        #region Centros de Salud por Usuarios
        private void cargaCentroDeSaludPorUsuario()
        {
            var list = new List<SelectListItem>();
            var _usrId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).usrId;

            var tblUsuarioCentroSalud = context.sisUsuarioCentroDeSalud;
            var tblCentroSalud = context.catCentroDeSalud;

            var lista = (from a in tblUsuarioCentroSalud
                         join b in tblCentroSalud on a.csId equals b.csId into c
                         from d in c
                         where a.usrId == _usrId
                         select new { csId = a.csId, csNombre = d.csNombre });

            list.AddRange(new SelectList(lista, "csId", "csNombre"));

            ViewData["CentroDeSalud"] = list;
        }
        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CargaEspecialidadesPorCS(int csId)
        {
            var _Especialidades = new List<catEspecialidades>();

            var especialidades = ((from s in context.catEspecialidad //.ToList()
                                   join t in context.catAgenda on s.espId equals t.espId
                                   where t.csId == csId && t.agActivo
                                   select new catEspecialidades
                                   {
                                       espId = s.espId,
                                       espNombre = s.espNombre + " (" + (s.espCodigo ?? "000") + ")"
                                   }).ToList().Distinct().OrderBy(o => o.espNombre));

            ViewData["Especialidad"] = new SelectList(especialidades, "espId", "espNombre");
            return Json((SelectList)ViewData["Especialidad"]);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getDatosUmbralesPorCSyESP(int _csId, int _espId)
        {
            List<catUmbralTiemposTurnoEspecialidades> _Datos = new List<catUmbralTiemposTurnoEspecialidades>();
            if (_espId == 0) //Todas las especialidades, traigo los tiempos de del Centro de Salud
            {
                _Datos = (from x in context.catUmbralTiemposTurno
                              where x.csId == _csId
                              select new catUmbralTiemposTurnoEspecialidades()
                              {
                                  TEsperaMin = x.TEsperaMin,
                                  TEsperaMax = x.TEsperaMax,
                                  TAtencionMin = x.TAtencionMin,
                                  TAtencionMax = x.TAtencionMax
                              }
                          ).ToList();
            }
            else
            {
                _Datos = (from x in context.catUmbralTiemposTurnoEspecialidad
                              where x.csId == _csId && x.espId == _espId
                              select new catUmbralTiemposTurnoEspecialidades()
                              {
                                  TEsperaMin = x.TEsperaMin,
                                  TEsperaMax = x.TEsperaMax,
                                  TAtencionMin = x.TAtencionMin,
                                  TAtencionMax = x.TAtencionMax
                              }
                          ).ToList();
            }

            return Json(new { Data = _Datos });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setDatosUmbralesPorCSyESP(int _csId, int _espId, int _teMin, int _teMax, int _taMin, int _taMax)
        {
            var res = context.spActualizaDatosUmbralesPorCSyESP(_csId,_espId,_teMin,_teMax,_taMin,_taMax).FirstOrDefault();

            return Json(new { msg = res.Mensaje });
        }
    }
}