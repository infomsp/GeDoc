using GeDoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeDoc.Controllers
{
    public class catTurnoBloqueoMasivoController : Controller
    {
        private readonly efAccesoADatosEntities context = new efAccesoADatosEntities();

        //
        // GET: /catTurnoBloqueoMasivo/

        public ActionResult Index()
        {
            cargaComboEspecialidad();
            cargaComboEspecialistas();
            cargaComboMotivoBolqueo();

            return View();
        }

       

        private void cargaComboMotivoBolqueo()
        {
            ViewData["MotivoBloqueo"] = new SelectList(context.catTurnoMotivoBloqueo.Where(t => t.tmbDescripcion != null), "tmbId", "tmbDescripcion");
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProcesaBloqueoMasivo(DateTime fDesde, DateTime fHasta, string hraDesde, string hraHasta, int especialistaId, short especialidadId, int motivoBloqueo, string tipoBloqueo)
        {
            int? xconId;
            int? xperId;
            short? espId;
            fHasta = fHasta.AddHours(23).AddMinutes(59);


            if (string.IsNullOrEmpty(hraDesde))
            {
                hraDesde = null;
            }
            if (string.IsNullOrEmpty(hraHasta))
            {
                hraHasta = null;
            }

            //Caso especialista "Contratado"
            if (especialistaId < 0)
            {
                xconId = especialistaId * -1;
                xperId = null;
            }
            else
            {
                //Caso especialista de "Planta"
                if (especialistaId > 0)
                {
                    xconId = null;
                    xperId = especialistaId;
                }
                else
                {
                    xconId = null;
                    xperId = null;
                }
            }




            if (especialidadId == 0)
            {
                espId = null;
            }
            else
            {
                espId = especialidadId;
            }

            //var res = 0;
            var res = context.procBloqueoMasivo(fDesde, fHasta, hraDesde, hraHasta, espId, xperId, xconId, motivoBloqueo, tipoBloqueo).FirstOrDefault();


            string resp = "";

            if (tipoBloqueo.Equals("BLCK"))
            {
                #region Msg BLCK
                if (res.registrosEncontrados != 0)//El proceso de bloqueo encuentra turnos
                {
                    if (res.registrosAfectados == res.registrosEncontrados)//Se bloquearon todos los turnos encontrados
                    {
                        resp = "El Procesos bloqueo " + res.registrosAfectados + " turno/s.";
                    }
                    else//Se bloquearon solo algunos de los turnos encontrados
                    {
                        resp = "Fueron encontrados " + res.registrosEncontrados + " turno/s, pero el procesos solo bloqueo " + res.registrosAfectados + " turno/s. \nEsto se debe a que el o los turnos se encontraban en estado \"Bloqueado\", \"Otorgado\" o \"Admisionado\". Favor de verificar.";
                    }

                }
                else//El proceso de bloqueo NO encuentra turnos
                {
                    resp = "El Procesos no encontro turnos a ser bloqueados.";
                }
                #endregion Msg BLCK
            }
            else
            {
                #region Msg DBLCK
                if (res.registrosEncontrados != 0)//El proceso de desbloqueo encuentra turnos
                {
                    if (res.registrosAfectados == res.registrosEncontrados)//Se desbloquearon todos los turnos encontrados
                    {
                        resp = "El Proceso desbloqueo " + res.registrosAfectados + " turno/s.";
                    }
                    else//Se desbloquearon solo algunos de los turnos encontrados
                    {
                        resp = "Fueron encontrados " + res.registrosEncontrados + " turno/s, pero el procesos solo desbloqueo " + res.registrosAfectados + " turno/s. \nEsto se debe a que el o los turnos se encontraban en estado \"Disponible\", \"Otorgado\" o \"Admisionado\". Favor de verificar.";
                    }
                }
                else//El proceso de desbloqueo NO encuentra turnos
                {
                    resp = "El Procesos no encontro turnos a ser desbloqueados.";
                }
                #endregion Msg DBLCK
            }

            return Json(new { msg = resp });
        }





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
        public ActionResult CargaEspecialistas(int espId )
        {
            if (espId != 0)
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
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
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
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

        #endregion Cargar Especialidades


        
    }
}
