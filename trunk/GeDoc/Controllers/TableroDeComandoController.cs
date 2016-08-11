using GeDoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public class TableroDeComandoController : Controller
    {
        private readonly efAccesoADatosEntities context = new efAccesoADatosEntities();
        //
        // GET: /TableroDeComando/

        public ActionResult Index()
        {
            cargaCentroDeSaludPorUsuario();
            cargaComboEspecialidad(); 
            cargaComboEspecialistas();
            cargaUmbrales();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getListadoDeTrunosJM(DateTime _fDesde, DateTime _fHasta, string _especialidades, string _medicos, int _csId)
        {
            //var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _Datos = (from x in context.getDatosTableroTurnos(_csId, _fDesde,_fHasta,_especialidades, _medicos)
                          select new getDatosTableroTurnos_Result()
                          {
                              TiempoDeAtencion=x.TiempoDeAtencion,
                              TiempoDeEspera=x.TiempoDeEspera,
                              Atendidos=x.Atendidos,
                              NoAtendidos=x.NoAtendidos,
                              Otorgados=x.Otorgados,
                              Disponibles=x.Disponibles,
                              Total=x.Total,
                              AtendidosPorc=x.AtendidosPorc,
                              NoAtendidosPorc=x.NoAtendidosPorc,
                              OtorgadosPorc=x.OtorgadosPorc,
                              DisponiblesPorc=x.DisponiblesPorc
                          }
                          ).ToList();

            return Json(new { Data = _Datos });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getDatosTableroTurnosDetalle(DateTime _fDesde, DateTime _fHasta, string _especialidades, string _medicos, int _csId)
        {
            //var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _Datos = (from x in context.getDatosTableroTurnosDetalle(_csId, _fDesde, _fHasta, _especialidades, _medicos)
                          select new getDatosTableroTurnosDetalle_Result()
                          {
                              turFecha = x.turFecha,
                              TiempoDeAtencion = x.TiempoDeAtencion,
                              TiempoDeEspera = x.TiempoDeEspera
                          }
                          ).ToList();

            return Json(new { Data = _Datos });
        }

        private void cargaUmbrales()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            ViewData["Umbrales"] = context.catUmbralTiemposTurno.First(w => w.csId == _csId);
        }

        #region Centros de Salud por Usuarios
        private void cargaCentroDeSaludPorUsuario()
        {
            var list = new List<SelectListItem>();
            var _usrId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).usrId;

            var tblUsuarioCentroSalud = context.sisUsuarioCentroDeSalud;
            var tblCentroSalud=context.catCentroDeSalud;

            var lista = (from a in tblUsuarioCentroSalud
                         join b in tblCentroSalud on a.csId equals b.csId into c
                         from d in c
                         where a.usrId==_usrId
                         select new { csId=a.csId, csNombre=d.csNombre});

            list.AddRange(new SelectList(lista, "csId", "csNombre"));
            
            ViewData["CentroDeSalud"] = list;
        }
        #endregion

        //Cargar Especialidades
        #region Cargar Especialistas

        private void cargaComboEspecialistas()
        {
            //Agrego Item "TODOS"
            var item = new SelectListItem()
            {
                Value = "0",
                Text = "TODOS"
            };

            var list = new List<SelectListItem>();
            list.Add(item);


            //Agrego todos los especialistas
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _Profesionales = (from a in context.catAgenda.Where(t => t.csId == _csId && t.agActivo)
                                  from b in context.catPersona.Where(t => t.perId == a.perId).DefaultIfEmpty()
                                  from c in context.catPersonaContratados.Where(t => t.conId == a.conId).DefaultIfEmpty()
                                  select new
                                  {
                                      perId = a.perId == null ? c.conId * -1 : b.perId,
                                      perApellidoyNombre = a.perId == null ? c.conApellidoyNombre : b.perApellidoyNombre
                                  }).GroupBy(a => a.perId).Select(grp => grp.FirstOrDefault()).OrderBy(a => a.perApellidoyNombre);

            list.AddRange(new SelectList(_Profesionales.Where(w => w.perId != -1), "perId", "perApellidoyNombre"));

            ViewData["Especialista"] = list;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CargaEspecialistas(int espId, int csId)
        {
            if (espId != 0)
            {
                var _csId = csId;
                var _Profesionales = (from a in context.catAgenda.Where(t => t.csId == _csId && t.agActivo && t.espId == espId)
                                      from b in context.catPersona.Where(t => t.perId == a.perId).DefaultIfEmpty()
                                      from c in context.catPersonaContratados.Where(t => t.conId == a.conId).DefaultIfEmpty()
                                      select new
                                      {
                                          perId = a.perId == null ? c.conId * -1 : b.perId,
                                          perApellidoyNombre = a.perId == null ? c.conApellidoyNombre : b.perApellidoyNombre
                                      }).GroupBy(a => a.perId).Select(grp => grp.FirstOrDefault()).OrderBy(a => a.perApellidoyNombre);
                ViewData["perId_Data"] = new SelectList(_Profesionales.Where(w => w.perId != -1), "perId", "perApellidoyNombre");
            }
            else
            {
                //Agrego todos los especialistas
                var _csId = csId;
                var _Profesionales = (from a in context.catAgenda.Where(t => t.csId == _csId && t.agActivo)
                                      from b in context.catPersona.Where(t => t.perId == a.perId).DefaultIfEmpty()
                                      from c in context.catPersonaContratados.Where(t => t.conId == a.conId).DefaultIfEmpty()
                                      select new
                                      {
                                          perId = a.perId == null ? c.conId * -1 : b.perId,
                                          perApellidoyNombre = a.perId == null ? c.conApellidoyNombre : b.perApellidoyNombre
                                      }).GroupBy(a => a.perId).Select(grp => grp.FirstOrDefault()).OrderBy(a => a.perApellidoyNombre);
                ViewData["perId_Data"] = new SelectList(_Profesionales.Where(w => w.perId != -1), "perId", "perApellidoyNombre");
            }

            return Json((SelectList)ViewData["perId_Data"]);
        }
        #endregion Cargar Especialistas

        //Cargar Especialidades
        #region Cargar Especialidades

        private void cargaComboEspecialidad()
        {
            ViewData["Especialidad"] = getEspecialidades(0, true);
        }

        protected SelectList getEspecialidades(int perId, bool PermiteTodos)
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            if (perId == 0)
            {
                var _Especialidades = new List<catEspecialidades>();
                _Especialidades.Add(new catEspecialidades
                {
                    espId = 0,
                    espNombre = PermiteTodos ? "TODAS" : "SELECCIONE ESPECIALIDAD"
                });

                _Especialidades.AddRange((from s in context.catEspecialidad //.ToList()
                                          join t in context.catAgenda on s.espId equals t.espId
                                          where t.csId == _csId && t.agActivo
                                          select new catEspecialidades
                                          {
                                              espId = s.espId,
                                              espNombre = s.espNombre + " (" + (s.espCodigo ?? "000") + ")"
                                          }).ToList().Distinct().OrderBy(o => o.espNombre));

                return new SelectList(_Especialidades, "espId", "espNombre");
            }
            else
            {
                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CargaEspecialidadesPorCS(int csId)
        {
            var _Especialidades = new List<catEspecialidades>();

            var especialidades  =   ((from s in context.catEspecialidad //.ToList()
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
        #endregion Cargar Especialidades

    }
}
