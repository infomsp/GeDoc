namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;

    public partial class catAgendaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catAgendas> All()
        {
            return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            catAgenda _Item = context.catAgenda.Where(p => p.agId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catAgenda _Item = new catAgenda();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

                _Item.csId = _csId;
                Create(_Item);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            DeleteConfirmed(id, false);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistro(int id)
        {
            DeleteConfirmed(id, true);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult BindingEspecialidades(int perId)
        {
            ViewData["espId_Data"] = new SelectList(getEspecialidades(perId), "espId", "espNombre");

            return Json((SelectList)ViewData["espId_Data"]);
        }

        private IEnumerable<catAgendas> getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Datos = (from d in context.catAgenda.ToList()
                          where (_csId == 0 ? 0 : d.csId) == _csId
                          select new catAgendas()
                          {
                              agActivo = d.agActivo,
                              agDuracion = d.agDuracion,
                              agId = d.agId,
                              agSobreturnos = d.agSobreturnos,
                              conId = d.conId,
                              perId = d.perId,
                              Profesional = d.perId == null ? d.catPersonaContratados.conApellidoyNombre : d.catPersona.perApellidoyNombre,
                              espId = d.espId,
                              Especialidad = new catEspecialidades()
                              {
                                  espAbreviatura = d.catEspecialidad.espAbreviatura,
                                  espCodigo = d.catEspecialidad.espCodigo,
                                  espDescripcionPlanilla = d.catEspecialidad.espDescripcionPlanilla,
                                  espId = d.catEspecialidad.espId,
                                  espIdMHO = d.catEspecialidad.espIdMHO,
                                  espIdPadre = d.catEspecialidad.espIdPadre,
                                  espNombre = d.catEspecialidad.espNombre
                              },
                              csId = d.csId
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Agenda de Profesionales para Atención";
            ViewData["FiltroContains"] = true;
            PopulateDropDownList();
            return PartialView();
        }

        private void Create(catAgenda pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.agActivo = false;
                    /*valida si una persona de planta o contratado tiene una agenda asignada*/
                    if (pItem.perId < 0)
                    {
                        pItem.conId = (pItem.perId * -1);
                        pItem.perId = null;

                        if (context.catAgenda.Where(w => w.conId == pItem.conId && w.espId == pItem.espId && w.csId == pItem.csId).Count() > 0)
                        { 
                            int agId = pItem.agId;
                            var agh_Item = context.catAgendaHorario.Where(p => p.agId == pItem.agId).ToList();
                           // if(context.catAgendaHorario(agh => agh.aghVigenciaDesde == pItem.
                          //  ModelState.AddModelError("perId", "Ya tiene agenda asignada en este centro de salud, especialidad, dia y horario");
                        }
                    }
                    else
                    {
                        if (context.catAgenda.Where(w => w.perId == pItem.perId && w.espId == pItem.espId && w.csId == pItem.csId).Count() > 0)
                        {
                           // ModelState.AddModelError("perId", "Ya tiene agenda asignada en este centro de salud, especialidad, dia y horario");
                           // return;
                        }
                    }

                    context.catAgenda.AddObject(pItem);

                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Agregar", "Agrega agenda");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("agDuracion", ex.Message);
                }
            }

            return;
        }

        private void Edit(catAgenda pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pItem.perId < 0)
                    {
                        pItem.conId = (pItem.perId * -1);
                        pItem.perId = null;
                    }

                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Modificar", "Modifica agenda");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("agDuracion", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id, bool pEstado)
        {
            try
            {
                catAgenda _Item = context.catAgenda.Single(x => x.agId == id);
                
              var _ItemAgendaHorarios = _Item.catAgendaHorario.ToList();
           
                
              
                 var _AgendaDatos = context.catAgenda.Where(x => (x.csId == _Item.csId) && ((x.perId == _Item.perId) || (x.conId == _Item.conId))).ToList();

            

                _Item.agActivo = pEstado;
                //Registra log de usuario
                if (pEstado == false)
                {
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Eliminar", "Elimina agenda");
                    context.SaveChanges();
                    // eliminaTurnos(id);
                }
                else
                {
                    if (_AgendaDatos.Count() == 0)
                    {
                      // var _AgendaHorariosDatos = context.catAgendaHorario.SkipWhile(m => m.agId != ());
                      //ModelState.AddModelError("agDuracion", "Esta agenda ya esta activada");
                      //  var agh_Item = context.catAgendaHorario.Where(p => p.agId == id).ToList();    
                    }
                    else
                    {
                        new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catAgenda", "Activar", "Activar Agenda");
                        _Item.agActivo = true;
                        context.SaveChanges();
                        generaTurnos(id);
                    }
                }
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("agDuracion", ex.Message);
            }
        }
        //private void eliminaTurnos(int id)
        //{
        //    var _Turno = context.spEliminarTurnos(id);
        //    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Eliminar", "Eliminar turnos"); 

        //}
        private void generaTurnos(int id)
        {
            var _Turno = context.spGenerarTurnos(id);
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Generar", "Genera turnos");

        }
        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var ZonaSanitaria = (Session["Usuario"] as sisUsuario).repId;
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            var _Profesionales = (from p in context.catPersona.ToList()
                                 // where p.catPersonaEspecialidad.Count > 0                                   
                                  join x in context.catPersonaEspecialidad on p.perId equals x.perId
                                  join r in context.vwSectorReparticionZona on p.secId equals r.secId
                                  //where r.repId == ZonaSanitaria  
                                  select new catPersonas
                                  {
                                      perId = p.perId,
                                      perDNI = p.perDNI,
                                      perApellidoyNombre = p.perApellidoyNombre
                                  }).ToList();
            

            _Profesionales.AddRange((from p in context.catPersonaContratados.ToList()
                                     join x in context.catPersonaContratadosEspecialidad on p.conId equals x.conId
                                     join r in context.catReparticion on p.repId equals r.repId
                                     //where r.repId == ZonaSanitaria  
                                     select new catPersonas
                                     {
                                         perId = (p.conId * -1),
                                         perDNI = p.conDNI,
                                         perApellidoyNombre = p.conApellidoyNombre
                                     }).ToList());
            _Profesionales = _Profesionales.OrderBy(o => o.perApellidoyNombre).ToList();

            _Profesionales = (from p in _Profesionales
                              orderby p.perApellidoyNombre ascending
                              select new catPersonas
                              {
                                  perId = p.perId,
                                  perApellidoyNombre = (p.perDNI == null || p.perDNI == 0 ? "" : "(" + p.perDNI + ") ") + p.perApellidoyNombre
                              }).ToList();

            ViewData["perId_Data"] = new SelectList(_Profesionales, "perId", "perApellidoyNombre");
            ViewData["espId_Data"] = new SelectList(getEspecialidades(0), "espId", "espNombre");

            return;
        }
               
        protected List<catEspecialidades> getEspecialidades(int perId)
        {
            if (perId == 0)
            {
                var _Especialidades = (from s in context.catEspecialidad.ToList()
                                       select new catEspecialidades
                                       {
                                           espId = s.espId,
                                           espNombre = "(" + s.espCodigo + ") " + s.espNombre
                                       }).ToList().OrderBy(o => o.espNombre);
                return _Especialidades.ToList();
            }
            else if (perId > 0)
            {
                var _Especialidades = (from s in context.catEspecialidad.ToList()
                                       join x in context.catPersonaEspecialidad on s.espId equals x.espId
                                       where x.perId == perId
                                       select new catEspecialidades
                                       {
                                           espId = s.espId,
                                           espNombre = "(" + s.espCodigo + ") " + s.espNombre
                                       }).ToList().OrderBy(o => o.espNombre);
                return _Especialidades.ToList();
            }

            var _Especialidad = (from s in context.catEspecialidad.ToList()
                                 join x in context.catPersonaContratadosEspecialidad on s.espId equals x.espId
                                 where x.conId == (perId * -1)
                                 select new catEspecialidades
                                 {
                                     espId = s.espId,
                                     espNombre = "(" + s.espCodigo + ") " + s.espNombre
                                 }).ToList().OrderBy(o => o.espNombre);
            return _Especialidad.ToList();
        }
    }
}