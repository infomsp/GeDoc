using GeDoc.Controllers;

namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class registroProfesionalController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditing_GestionTurnos(string Filtro)
        {
            return View(new GridModel(All_GestionTurnos(Filtro)));
        }

        private IList<catProfesionalTurnoWS> All_GestionTurnos(string Filtro)
        {
            return getDatos_GestionTurnos(Filtro).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertEditing_GestionTurnos()
        {
            var _Item = new catProfesionalTurno();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }

                Create(_Item);
            }

            return View(new GridModel(All_GestionTurnos("~")));
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

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCambiaPrioridad(int pturId, string Prioridad)
        {
            catProfesionalTurno turno = context.catProfesionalTurno.First(w => w.pturId == pturId);
            turno.pturPrioridad = Prioridad;
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            return Json(true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setIniciarAtencion(int Consultorio, int pturId)
        {
            catProfesionalTurno turno = context.catProfesionalTurno.First(w => w.pturId == pturId);
            turno.pturEstado = "Atendiendo";
            turno.csscId = Consultorio;
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            return Json(true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setFinalizarAtencion(int pturId)
        {
            catProfesionalTurno turno = context.catProfesionalTurno.First(w => w.pturId == pturId);
            turno.pturEstado = "Atendido";
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            return Json(true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getProximoProfesional(int Consultorio, int pturId)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            try
            {
                var Proximo = context.getProximoProfesional(Consultorio, pturId).First();

                return Json(new { turId = Proximo.Turno, Mensaje = Proximo.Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new { turId = 0, Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        private IEnumerable<catProfesionalTurnoWS> getDatos_GestionTurnos(string Filtro)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var FechaActual = DateTime.Now.Date;
            var Estados = "#Admisionado#Atendiendo#Llamado#";
            if (Filtro != "~")
            {
                FechaActual = DateTime.Now.Date.AddYears(-100);
                Estados = "#Admisionado#Atendiendo#Llamado#Atendido#";
            }

            var _Datos = (from d in context.catProfesionalTurno.Where(w => w.pturFecha >= FechaActual && Estados.Contains(w.pturEstado)).ToList()
                          orderby d.pturFecha
                          select new catProfesionalTurnoWS()
                                    {
                                        pturEstado = d.pturEstado,
                                        pturFechaAdmisionado = d.pturFechaAdmisionado,
                                        pturFecha = d.pturFecha,
                                        pturApellidoyNombre = d.pturApellidoyNombre,
                                        pturDNI = d.pturDNI,
                                        pturFechaFinAtencion = d.pturFechaFinAtencion,
                                        pturFechaInicioAtencion = d.pturFechaInicioAtencion,
                                        pturId = d.pturId,
                                        pturPrioridad = d.pturPrioridad,
                                        pturTiempoAtencion = d.pturTiempoAtencion,
                                        pturTiempoDeEspera = d.pturTiempoDeEspera,
                                        csId = d.csId,
                                        EstadoImagen = d.pturEstado == "Llamado" ? "Rojo(2).png" : (d.pturEstado == "Atendido" ? "Verde-2.png" : (d.pturEstado == "Atendiendo" ? "ConDetalle.png" : "Gris-E.png")),
                                        ImagenPrioridad = d.pturPrioridad == "Normal" ? "Normal.png" : "Alta.png",
                                        Box = d.catCentroDeSaludSalaConsultorio == null ? "" : d.catCentroDeSaludSalaConsultorio.csscNombre
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult rpGestionTurnos()
        {
            ViewBag.Catalogo = "Atención de Profesionales";
            CargarSalas();

            return View();
        }

        private void CargarSalas()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            var Salas = (from x in context.catCentroDeSaludSala.Where(x => x.csId == _csId)
                         select new ListaDesplegableWS()
                         {
                             idLista = x.cssId,
                             Texto = x.cssNombre
                         }).ToList();

            ViewData["cssId_Lista_Data"] = Salas;
            var UnaSala = -1;
            if (Salas.Count > 0)
            {
                UnaSala = Salas.First().idLista;
            }
            ViewData["csscId_Lista_Data"] = (from x in context.catCentroDeSaludSalaConsultorio.Where(x => x.cssId == UnaSala)
                                             select new ListaDesplegableWS()
                                             {
                                                 idLista = x.csscId,
                                                 Texto = x.csscNombre
                                             }).ToList();

            ViewData["csstId_Lista_Data"] = (from x in context.catCentroDeSaludSalaTelevisor.Where(x => x.cssId == UnaSala)
                                             select new ListaDesplegableWS()
                                             {
                                                 idLista = x.csstId,
                                                 Texto = x.csstNombre
                                             }).ToList();
        }

        private void Create(catProfesionalTurno pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                    pItem.csId = _csId;
                    pItem.pturFecha = DateTime.Now;
                    pItem.pturFechaAdmisionado = DateTime.Now;
                    pItem.pturEstado = "Admisionado";
                    pItem.pturPrioridad = "Normal";
                    pItem.pturDNI = DateTime.Now.ToString("HHmmssttzzz");
                    pItem.pturApellidoyNombre = pItem.pturApellidoyNombre.ToUpper();

                    context.catProfesionalTurno.AddObject(pItem);

                    //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "rpGestionProfesional", "registroProfesional", "Agregar", "Agrega turno de Profesional");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pturApellidoyNombre", ex.Message);
                }
            }
        }

        private void DeleteConfirmed(int id, bool pEstado)
        {
            try
            {
                catProfesionalTurno _Item = context.catProfesionalTurno.Single(x => x.pturId == id);

                context.catProfesionalTurno.DeleteObject(_Item);
                //Registra log de usuario
                //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "rpGestionProfesional", "registroProfesional", "Eliminar", "Elimina turno");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("pturApellidoyNombre", ex.Message);
            }
        }

        public ActionResult rpVisualizadorDeTurnos()
        {
            ViewBag.Catalogo = "Llamador de profesionales";
            Session["TurnosLlamados"] = new List<catCentroDeSaludLLamadorWS>();
            CargarSalas();

            return PartialView();
        }

    }
}