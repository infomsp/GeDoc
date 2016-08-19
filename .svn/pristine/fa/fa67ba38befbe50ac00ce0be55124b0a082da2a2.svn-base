using System.Web.UI.WebControls;

namespace GeDoc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class catTurnoController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();
        
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult BindingProfesionales(int espId, int profesionalId)
        {
            //var ZonaSanitaria = (Session["Usuario"] as sisUsuario).repId;
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            //var _Profesionales = context.getProfesionalesxEspZona(espId, ZonaSanitaria).OrderBy(o => o.perApellidoyNombre);
            var _Profesionales = (from a in context.catAgenda.Where(t => t.csId == _csId && t.agActivo && t.espId == espId)
                                  from b in context.catPersona.Where(t => t.perId == a.perId).DefaultIfEmpty()
                                  from c in context.catPersonaContratados.Where(t => t.conId == a.conId).DefaultIfEmpty()
                                  select new
                                  {
                                      perId = a.perId == null ? c.conId*-1 : b.perId,
                                      perApellidoyNombre = a.perId == null ? c.conApellidoyNombre : b.perApellidoyNombre
                                  }).GroupBy(a => a.perId).Select(grp => grp.FirstOrDefault()).OrderBy(a => a.perApellidoyNombre);
            ViewData["perId_Data"] = new SelectList(_Profesionales.Where(w => w.perId != profesionalId).ToList(), "perId", "perApellidoyNombre");
            return Json((SelectList)ViewData["perId_Data"]);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult BindingProfesionalesAtencionInmediata(int espId, int profesionalId)
        {
            //var ZonaSanitaria = (Session["Usuario"] as sisUsuario).repId;
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
            //var _Profesionales = context.getProfesionalesxEspZona(espId, ZonaSanitaria).OrderBy(o => o.perApellidoyNombre);
            var _Profesionales = (from a in context.catAgenda.Where(t => t.csId == _csId && t.agActivo && t.espId == espId)
                                  from b in context.catPersona.Where(t => t.perId == a.perId).DefaultIfEmpty()
                                  from c in context.catPersonaContratados.Where(t => t.conId == a.conId).DefaultIfEmpty()
                                 
                                  select new
                                  {
                                      perId = a.perId == null ? c.conId * -1 : b.perId,
                                      perApellidoyNombre = a.perId == null ? c.conApellidoyNombre : b.perApellidoyNombre
                                  }).GroupBy(a => a.perId).Select(grp => grp.FirstOrDefault()).OrderBy(a => a.perApellidoyNombre);
            ViewData["perId_Data"] = new SelectList(_Profesionales.Where(w => w.perId != profesionalId).ToList(), "perId", "perApellidoyNombre");
            return Json((SelectList)ViewData["perId_Data"]);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _BindingProfesionalesRA(int espId, int profesionalId)
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


            var Datos = _Profesionales.Where(w => w.perId != profesionalId).ToList();

            string listaDesplegable = "<select id=\"perId_Lista\" class=\"csscId_Lista\"" + (Datos.Count == 0 ? " disabled " : "") + " >";
            foreach (var Item in Datos)
            {
                listaDesplegable += "<option value=\"" + Item.perId + "\">" + Item.perApellidoyNombre + "</option>";
            }
            if (!Datos.Any())
            {
                listaDesplegable += "<option value=\"-1\" class=\"ES-PlaceHolder\">Ningún Item seleccionado</option>";
            }
            listaDesplegable += "</select>";

            return Json(listaDesplegable);
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Turnos";
            ViewData["FiltroContains"] = true;
            Session["especialidadId"] = 0;
            Session["profesionalId"] = 0;
            Session["fecha"] = DateTime.Now;
            ViewData["catTurnosPacientes"] = new List<catTurnosPacientes>();
            PopulateDropDownList();
            Session["UltimoId"] = -1;
            CargarSalas();
            cargaComboEspecialidad();
            cargaComboEspecialistas();
           // PopulateDropDownList_CartillaBasica();

            return PartialView();
           
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
        public ActionResult CargaEspecialistas(int espId)
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
            ViewData["Especialidad"] = getEspecialidadesTC(0, true);
        }

        protected SelectList getEspecialidadesTC(int perId, bool PermiteTodos)
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

        public ActionResult TableroDeComandos()
        {
            ViewBag.Catalogo = "Tablero de Comandos";
            CargarListasTableroComando();

            return PartialView();
        }

        public void CargarListasTableroComando()
        {
            ViewData["espId_Lista_Data"] = (from x in getEspecialidades(0, true)
                                           select new ListaDesplegableWS
                                            {
                                                idLista = x.espId,
                                                Texto = x.espNombre
                                            }).ToList();

            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            var _Profesionales = new List<ListaDesplegableWS>();
            _Profesionales.Add(new ListaDesplegableWS
            {
                idLista = 0,
                Texto = "TODOS"
            });
            _Profesionales.AddRange((from a in context.catAgenda.Where(t => t.csId == _csId && t.agActivo)
                                  from b in context.catPersona.Where(t => t.perId == a.perId).DefaultIfEmpty()
                                  from c in context.catPersonaContratados.Where(t => t.conId == a.conId).DefaultIfEmpty()
                                     select new ListaDesplegableWS
                                  {
                                      idLista = a.perId == null ? c.conId*-1 : b.perId,
                                      Texto = a.perId == null ? c.conApellidoyNombre : b.perApellidoyNombre
                                  }).GroupBy(a => a.idLista).Select(grp => grp.FirstOrDefault()).OrderBy(a => a.Texto));

            ViewData["perId_Lista_Data"] = _Profesionales.ToList();

            ViewData["Umbrales"] = context.catUmbralTiemposTurno.First(w => w.csId == _csId);
        }

        private IList<catTurnos> getTurnos(int especialidadId, int profesionalId, DateTime fecha)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            int perId = -1;
            int conId = -1;
            if (profesionalId < 0)
            {
                conId = profesionalId * -1;
            }
            else
            {
                perId = profesionalId;
            }

            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            var _Datos = (from d in context.spGetListadoTurnos(_csId, fecha, especialidadId, perId, conId).ToList()
                          orderby d.turFecha ascending
                          select new catTurnos()
                          {
                              turId = d.turId,
                              aghId = d.aghId,
                              tipoId = d.tipoId,
                              tipoDescripcion = d.tipoDescripcion,
                              turEstadoImagen = d.turEstadoImagen,
                              tipoId_Admision = d.tipoId_Admision == null ? -1 : (int)d.tipoId_Admision,
                              tipoAdmisionDescripcion = d.tipoId_Admision == null ? "" : d.AdmisionDescripcion,
                              tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                              pacNumeroDocumento = d.pacNumeroDocumento,
                              pacId = d.pacId,
                              pacNombre = d.pacId == null ? "" : d.pacApellido.ToUpper() + " " + d.pacNombre.ToUpper(),
                              turEsProgramado = d.turEsProgramado,
                              turEsSobreturno = d.turEsSobreturno,
                              turEsSobreturno_descripcion = d.turEsSobreturno == false ? "NO" : "SI",
                              turFecha = d.turFecha,
                              hora = d.turFecha.Hour,
                              horayminutos = DateTime.Parse(d.turFecha.Hour + ":" + d.turFecha.Minute).ToString("HH:mm"),
                              _HoraAtendido = d.turHoraAtendido == null ? "" : DateTime.Parse(d.turHoraAtendido.Value.Hour + ":" + d.turHoraAtendido.Value.Minute).ToString("HH:mm"),
                              nroHC = d.nroHistClin,
                              turPrimUlt = d.turPrimUlt,
                              tipoAdmisionCodigo = d.AdmisionCodigo,
                              EstadoCodigo = d.EstadoCodigo,
                              tmbDescripcion = d.tmbDescripcion,
                              turPrioridad = d.turPrioridad,
                              ImagenPrioridad = d.ImagenPrioridad,
                              HoraAdmisionado = d.turFechaAdmisionado == null ? "" : d.turFechaAdmisionado.Value.ToString("HH:mm"),
                              // turControlEmbarazo = d.turControlEmbarazo
                               csId = _csId
                          }).ToList();


            Session["ListadoTurnosSeleccionado"] = _Datos;
            return _Datos;
        }

        public string pacienteOtroTurno(long? _pacId, long _turId)
        {
            int _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            string img;
            string especialidad;
            var _Datos = (from d in context.spGetPacienteColumnaOtrosTurnos(_csId, _pacId, _turId)
                        
                          select new catTurnos()
                           {
                               OtroTurno = d.turId == null ? "No" : d.Especialidad.ToString(),
                               OtroTurnoImg = d.turId== null ?"gris.png" : "rojo.png"
                           }).FirstOrDefault();
            if (_Datos == null)
            {
                img = "gris.png";
                especialidad = "NO";
            }
            else
            {
                 img = "rojo.png";
                 especialidad = _Datos.OtroTurno;
            }


            string OtroTurnoImg = "/Estados/<#=" + img +
                                  "#>' title='<#= " + especialidad + " ||" + especialidad + " #>' height='15' width='15' style='vertical-align:middle;' /></div>";
            return (OtroTurnoImg);
        }

        private IList<catTurnos> All(int especialidadId, int profesionalId, DateTime fecha)
        {
            return getTurnos(especialidadId, profesionalId, fecha).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getBusquedaPaciente(int? turId)
        {
            return context.catTurno.Any(a => a.turId == turId && a.pacId != null) ? null : PartialView("AsignacionPaciente");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getMotivoBloqueoCombo()
        {
            ViewData["motivoBloqueo_Data"] = new SelectList(context.catTurnoMotivoBloqueo.Where(t => t.tmbDescripcion != null), "tmbId", "tmbDescripcion");
            return PartialView("MotivoBloqueo");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setMotivoBloqueoTurno(int _turId, int _tmbId)
        {
            var _Item = context.catTurno.Single<catTurno>(p => p.turId.Equals(_turId));
            if (_Item != null)
            {
                _Item.tmbId = _tmbId;
                context.SaveChanges();
                setCambiarEstadoTurno(_turId, "BL");             
            }
            return Json( (_Item != null) );
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCambiarEstadoTurno(int turId, string CodigoEstado)
        {
            try
            {
                if (context.enlTurnoDiagnostico.Any(a => a.turId == turId) || context.enlTurnoPractica.Any(a => a.turId == turId))
                {
                    throw new Exception("El turno no puede ser desasignado, primero debe remover cualquier práctica o diagnostico que posea el mismo.");
                }

                var turno = context.catTurno.First(w => w.turId == turId);

                if (CodigoEstado == "DI")
                {
                    turno.turControlEmbarazo = null;
                    var cE = context.catEvolucion.Where(w => w.turId == turId);
                    foreach (var c in cE)
                    {
                        c.turId = null;
                    }
                }

                turno.tipoId_Admision = null;
                turno.turHoraAtendido = null;
                turno.turFechaAdmisionado = null;
                turno.turFechaInicioAtencion = null;
                turno.turFechaFinAtencion = null;
                turno.turProgramado = null;
                turno.turTiempoAtencion = 0;
                turno.turTiempoDeEspera = 0;

                var _tipoDisponible = context.sisTipo.First(f => f.sisTipoEntidad.tipoeCodigo == "ET" && f.tipoCodigo == CodigoEstado).tipoId;

                turno.pacId = null;
                turno.tipoId = _tipoDisponible;
                turno.tmbId = CodigoEstado != "BL" ? null : turno.tmbId;

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Eliminar Turno", CodigoEstado == "DI" ? "Elimina un Turno " + turId : "Bloquea un Turno " + turId);

                if (CodigoEstado == "DI")
                {
                    RegistrarLogTurnos((Session["Usuario"] as sisUsuario), turId, 2);
                }
                context.SaveChanges();
            }

            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _GetComboBoxTipoAdmision(int estadoTurnoId, int tipoAdmisionId)
        {
            IQueryable<sisTipo> data;

            if (estadoTurnoId == 172)
            {
                data = context.sisTipo.Where(w => w.tipoeId == 35)
                                  .OrderByDescending(t => t.tipoId == 177).ThenBy(t => t.tipoId);
            }else if (tipoAdmisionId == 171)
            {
                data = context.sisTipo.Where(w => w.tipoId == 172);
            }
            else
            {
                data = context.sisTipo.Where(w => w.tipoeId == 35);
            }

           
            return Json(new SelectList(data, "tipoId", "tipoDescripcion"));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setRepetirTurno(int pturId, int pDias)
        {
            var Resultado = new List<catTurnos>();
            try
            {
                var Estado = context.sisTipo.First(w => w.tipoCodigo == "OT" && w.sisTipoEntidad.tipoeCodigo == "ET");
                var Turno = context.catTurno.First(w => w.turId == pturId);
                var FechaTurno = Turno.turFecha.AddDays(1).Date;
                var TurnoDisponibles = context.catTurno.Where(w =>  w.pacId == null && 
                                                                    w.catAgendaHorario.catAgenda.espId == Turno.catAgendaHorario.catAgenda.espId && 
                                                                    (Turno.catAgendaHorario.catAgenda.conId == null ? true : (w.catAgendaHorario.catAgenda.conId == Turno.catAgendaHorario.catAgenda.conId)) && 
                                                                    w.catAgendaHorario.catAgenda.perId == Turno.catAgendaHorario.catAgenda.perId 
                                                                    && w.turFecha >= FechaTurno 
                                                                    && w.sisTipo.tipoCodigo == "DI").ToList();
                FechaTurno = Turno.turFecha;
                for (int Dias = 1; Dias <= pDias; Dias++)
                {
                    FechaTurno = FechaTurno.AddDays(1);
                    var TurnoAsignado = TurnoDisponibles.FirstOrDefault(o => o.turFecha == FechaTurno);
                    var unTurno = new catTurnos();
                    unTurno.turFecha = FechaTurno;
                    unTurno.tipoDescripcion = "No hay turnos disponibles en esta fecha.";
                    unTurno.turEstadoImagen = "Rojo.png";
                    if (TurnoAsignado != null)
                    {
                        //Turno para la misma hora\\
                        TurnoAsignado.pacId = Turno.pacId;
                        TurnoAsignado.tipoId = Estado.tipoId;
                        TurnoAsignado.turProgramado = true;
                        unTurno.turFecha = FechaTurno;
                        unTurno.tipoDescripcion = "Otorgado";
                        unTurno.turEstadoImagen = "Verde.png";
                        unTurno.turProgramado = true;
                        FechaTurno = unTurno.turFecha;
                    }
                    else
                    {
                        var TurnoDespuesDelHorario = TurnoDisponibles.Where(o => o.turFecha >= FechaTurno).OrderBy(o => o.turFecha).FirstOrDefault();
                        if (TurnoDespuesDelHorario != null)
                        {
                            //Turno para la misma hora\\
                            TurnoDespuesDelHorario.pacId = Turno.pacId;
                            TurnoDespuesDelHorario.tipoId = Estado.tipoId;
                            TurnoDespuesDelHorario.turProgramado = true;
                            unTurno.turFecha = TurnoDespuesDelHorario.turFecha;
                            unTurno.tipoDescripcion = "Otorgado";
                            unTurno.turEstadoImagen = "Verde.png";
                            unTurno.turProgramado = true;
                            FechaTurno = unTurno.turFecha;
                        }
                        else
                        {
                            var TurnoAlgunHorario = TurnoDisponibles.Where(o => o.turFecha.Date >= FechaTurno.Date).OrderBy(o => o.turFecha).FirstOrDefault();
                            if (TurnoAlgunHorario != null)
                            {
                                //Turno para la misma hora\\
                                TurnoAlgunHorario.pacId = Turno.pacId;
                                TurnoAlgunHorario.tipoId = Estado.tipoId;
                                TurnoAlgunHorario.turProgramado = true;
                                unTurno.turFecha = TurnoAlgunHorario.turFecha;
                                unTurno.tipoDescripcion = "Otorgado";
                                unTurno.turEstadoImagen = "Verde.png";
                                unTurno.turProgramado = true;
                                FechaTurno = unTurno.turFecha;
                            }
                        }
                    }
                    Resultado.Add(unTurno);
                }

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Repetir Turno", "Repite Turno " + pturId + " por " + pDias + " días");
                context.SaveChanges();
            }

            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK", Datos = Resultado });
        }

        [AcceptVerbs(HttpVerbs.Post)]       
        public ActionResult AsignarPaciente(int pacId, int turId, DateTime turFecha)
        {
            catTurno turno = context.catTurno.First(w => w.turId == turId);
            if (turno.pacId != null)
            {
                return Json(new { IsValid = false, Mensaje = "No se puede Asignar un Paciente, este turno NO está disponible" });
            }

                int _tipoId_admisionAD = (from d in context.sisTipo
                                          join x in context.sisTipoEntidad on d.tipoeId equals x.tipoeId   
                                          where d.tipoCodigo == "AD" && x.tipoeCodigo == "TA"
                              select d.tipoId).First();

                int _tipoId_estadoTurnoAD = (from d in context.sisTipo
                                             join x in context.sisTipoEntidad on d.tipoeId equals x.tipoeId
                                             where d.tipoCodigo == "AD" && "ET" == x.tipoeCodigo
                                           select d.tipoId).First();

                int _tipoId_estadoTurnoOT = (from d in context.sisTipo
                                             join x in context.sisTipoEntidad on d.tipoeId equals x.tipoeId
                                             where d.tipoCodigo == "OT" && "ET" ==  x.tipoeCodigo
                                           select d.tipoId).First();
                turno.pacId = pacId;
              try
             {
                if (turFecha.Date > DateTime.Now.Date)
                {
                    turno.tipoId = (short)_tipoId_estadoTurnoOT;
                    turno.turProgramado = true;
                }
                else
                {
                    ///Se comenta el codigo por pedido de Walter. Solo se tomarán como otorgados aquellos turnos a futuro, es decir, para el día de mañana.
                    //if (turno.turFecha.Hour >= (DateTime.Now.Hour + 3))
                    //{
                    //    turno.tipoId = (short) _tipoId_estadoTurnoOT;
                    //}
                    //else
                    //{
                        turno.tipoId = (short) _tipoId_estadoTurnoAD; // estado turno admisionado 
                        turno.tipoId_Admision = (short) _tipoId_admisionAD; //tipo de admision admisionado 
                        turno.turProgramado = false;
                    //}
                }
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Asignar Paciente","Asigna un Paciente al Turno "+turId);

                //Registra log de cambios en turnos
                RegistrarLogTurnos((Session["Usuario"] as sisUsuario), turId, 1);
                context.SaveChanges();
            }

            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });
        }

        private void RegistrarLogTurnos(sisUsuario _usrId, int _turId, byte _tipoLog)
        {
            catTurnoLogAsignacion newItem = new catTurnoLogAsignacion();
            newItem.turId = _turId;
            newItem.fechaCarga = DateTime.Now;
            newItem.usrId = _usrId.usrId;
            newItem.logTipo = _tipoLog;//1-Asignacion;2-liberacion
            context.catTurnoLogAsignacion.AddObject(newItem);
            context.SaveChanges();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCambiaPrioridad(int turId, string Prioridad)
        {
            catTurno turno = context.catTurno.First(w => w.turId == turId);
            turno.turPrioridad = Prioridad;
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
        public ActionResult AsignarVisita(string Visita, int turId, bool turControlEmbarazo)
        {
            catTurno turno = context.catTurno.First(w => w.turId == turId);
            turno.turPrimUlt = Visita;
            turno.turControlEmbarazo = turControlEmbarazo;
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.Message });
            }
            return Json(new { IsValid = true, Mensaje = "OK" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getTurno(int turId)
        {
            var _NombreCentroSalud = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).ucsCentroDeSalud;
            var _UsuarioApyNombre = (Session["Usuario"] as GeDoc.Models.sisUsuario).usrApellidoyNombre;
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

            ViewBag.PrimeraVez = true;
            ViewBag.Ulterior = false;

            var TurnoEncontrado = context.catTurno.Single(w => w.turId == turId);
            var _Item = new catTurnos()
                                  {
                                      turId = TurnoEncontrado.turId,
                                      tipoSexoDescripcion = TurnoEncontrado.pacId == null ? "" : TurnoEncontrado.catPaciente.sisTipo.tipoDescripcion,
                                      tipoDescripcion = TurnoEncontrado.sisTipo.tipoDescripcion,
                                      EstadoCodigo = TurnoEncontrado.sisTipo.tipoCodigo,
                                      tipoAdmisionDescripcion = TurnoEncontrado.tipoId_Admision == null ? "" : TurnoEncontrado.sisTipo2.tipoDescripcion,
                                      tipoAdmisionCodigo = TurnoEncontrado.tipoId_Admision == null ? "" : TurnoEncontrado.sisTipo2.tipoCodigo,
                                      tipoIdTipoDocumento = (short)(TurnoEncontrado.catPaciente == null ? -1 : TurnoEncontrado.catPaciente.tipoIdTipoDocumento),
                                      PacienteDescripcionTipoDocumento = (TurnoEncontrado.catPaciente == null ? "" : TurnoEncontrado.catPaciente.sisTipo1.tipoDescripcion),
                                      pacNumeroDocumento = TurnoEncontrado.pacId == null ? 0 : TurnoEncontrado.catPaciente.pacNumeroDocumento,
                                      pacId = TurnoEncontrado.pacId,
                                      pacNombre = TurnoEncontrado.pacId == null ? "" : TurnoEncontrado.catPaciente.pacApellido.ToUpper() + " " + TurnoEncontrado.catPaciente.pacNombre.ToUpper(),
                                      turFecha = TurnoEncontrado.turFecha,
                                      turEspecialidadNombre = TurnoEncontrado.catAgendaHorario.catAgenda.catEspecialidad.espNombre,
                                      profApellidoyNombre = TurnoEncontrado.catAgendaHorario.catAgenda.conId == null ? TurnoEncontrado.catAgendaHorario.catAgenda.catPersona.perApellidoyNombre.ToUpper() : TurnoEncontrado.catAgendaHorario.catAgenda.catPersonaContratados.conApellidoyNombre.ToUpper(),
                                      _csNombre = _NombreCentroSalud,
                                      _usNombre = _UsuarioApyNombre,
                                      nroHC = getNroHc(TurnoEncontrado.pacId ?? 0, _csId),
                                      turControlEmbarazo = TurnoEncontrado.turControlEmbarazo
                                  };
             var Turnos = context.catTurno.Count(c => c.pacId == _Item.pacId && c.turId != turId && c.turFecha <= _Item.turFecha);
             if (Turnos > 0)
             {
                 ViewBag.PrimeraVez = false;
                 ViewBag.Ulterior = true;
             }
             ViewData["adId_Data"] = new SelectList(context.catDiagnosticoAgrupamiento.OrderBy(o => o.adId), "adId", "adDescripcion");
             ViewData["nomId_Data"] = new SelectList(context.catNomenclador.OrderBy(o => o.nomId), "nomId", "nomDescripcion");
            ViewData["evolucion_pacId"] = _Item.pacId;
            ViewData["minId_Data"] = new SelectList(context.catMinisterio, "minId", "minNombre");
            catCartillaBasica catillaBasica;
          
            ViewData["select_minId"] = new SelectList(context.catMinisterio, "minId", "minNombre");
          
             return PartialView("CRUDTurno", _Item);
        }     

        public string getNroHc(long _pacId, long _csId)
        {
            var _Item = (from s in context.enlPacienteCtroSalud.ToList()
                         where s.csId == _csId && s.pacId == _pacId
                         select new enlPacientesCtroSalud()
                         {
                             nroHC = s.nroHC
                         }).FirstOrDefault();
            if (_Item == null)
            {
                return " ";
            }
            return _Item.nroHC;
        }
       
        [GridAction]
        public ActionResult _SelectEditing(int especialidadId, int profesionalId, DateTime fecha)
        {
            return View(new GridModel<catTurnos>
            {
                Data = All(especialidadId, profesionalId, fecha)
            });
        }

        [GridAction]
        public ActionResult _SelectEditingRA(int especialidadId, int profesionalId, DateTime fecha)
        {
            return View(new GridModel<catTurnos>
            {
                Data = All(especialidadId, profesionalId, fecha).Where(w => (w.EstadoCodigo == "AD" || w.EstadoCodigo == "OT") && (w.tipoAdmisionCodigo == "" || w.tipoAdmisionCodigo == "AD" || w.tipoAdmisionCodigo == null) && (w.turFecha.Date >= DateTime.Now.Date) && (w.pacId != null))
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getTurnosDisponibles(int especialidadId, int profesionalId, DateTime fecha)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            int perId = -1;
            int conId = -1;
            if (profesionalId < 0)
            {
                conId = profesionalId * -1;
            }
            else
            {
                perId = profesionalId;
            }

            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            var _Datos = context.spGetListadoTurnos(_csId, fecha, especialidadId, perId, conId).Count(w => w.EstadoCodigo == "DI");

            return Json(_Datos);
        }

        [GridAction]
        public ActionResult _BindingPieTurnos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            var _Prof_cAdmisionado = 0;
            var _Prof_cBloqueado = 0;
            var _Prof_cDisponible = 0;
            var _Prof_cOtorgado = 0;
            if (Session["ListadoTurnosSeleccionado"] != null && (Session["ListadoTurnosSeleccionado"] as List<catTurnos>).Count > 0)
            {
                var ListadoDeTurnos = (Session["ListadoTurnosSeleccionado"] as List<catTurnos>);
                _Prof_cAdmisionado = ListadoDeTurnos.Count(x => x.EstadoCodigo == "AD");
                _Prof_cBloqueado = ListadoDeTurnos.Count(x => x.EstadoCodigo == "BL");
                _Prof_cDisponible = ListadoDeTurnos.Count(x => x.EstadoCodigo == "DI");
                _Prof_cOtorgado = ListadoDeTurnos.Count(x => x.EstadoCodigo == "OT");
            }

            List<PieTurnos> _Datos = new List<PieTurnos>();
            PieTurnos _One = new PieTurnos();
            _One.tipoDescripcion = "DISPONIBLES";
            _One.CantidadTurnos = (short)_Prof_cDisponible;
            _One.turEstadoImagen = "Amarillo.png";
            _Datos.Add(_One);
            _One = new PieTurnos();
            _One.tipoDescripcion = "ADMISIONADOS";
            _One.CantidadTurnos = (short)_Prof_cAdmisionado;
            _Datos.Add(_One);
             _One.turEstadoImagen = "Celeste.png";
            _One = new PieTurnos();
            _One.tipoDescripcion = "OTORGADOS";
            _One.CantidadTurnos = (short)_Prof_cOtorgado;
            _Datos.Add(_One);
             _One.turEstadoImagen = "Verde.png";
            _One = new PieTurnos();
            _One.tipoDescripcion = "BLOQUEADOS";
            _One.CantidadTurnos = (short)_Prof_cBloqueado;
             _One.turEstadoImagen = "Rojo.png";
            _Datos.Add(_One);
            return View(new GridModel<PieTurnos>
            {
                Data = _Datos
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getProximoPaciente(int especialidadId, int profesionalId, DateTime fecha, int Consultorio, int turId)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            var perId = -1;
            var conId = -1;
            if (profesionalId < 0)
            {
                conId = profesionalId * -1;
            }
            else
            {
                perId = profesionalId;
            }
            try
            {
                var Proximo = context.getProximoPaciente(fecha, especialidadId, perId, conId, Consultorio, turId).First();

                return Json(new { turId = Proximo.Turno, Mensaje = Proximo.Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new { turId = 0, Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCambioDeAdmision(int idTurno, int pNuevaAdmision)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            var _Item = context.catTurno.FirstOrDefault(p => p.turId == idTurno);
            var _TipoEstadoTurno = context.sisTipo.Where(e => e.sisTipoEntidad.tipoeCodigo == "ET").ToList();

            try
            {
                var _TipoAdmision = new sisTipo();
                if ((_Item.sisTipo.tipoCodigo == "OT"))
                {
                    _Item.tipoId = _TipoEstadoTurno.First(f => f.tipoCodigo == "AD").tipoId;
                }
                else
                {
                    _TipoAdmision = context.sisTipo.First(w => w.tipoId == pNuevaAdmision);
                }
                if (_TipoAdmision.tipoCodigo == "AT")
                {
                    _Item.turHoraAtendido = DateTime.Now;
                }
                if (_TipoAdmision.tipoCodigo != "AT")
                {
                    _Item.turHoraAtendido = null;
                }
                _Item.tipoId_Admision = (short)pNuevaAdmision;
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno",
                    "Cambiar Admision", "Cambiar Admisión del Turno " + idTurno);
                context.SaveChanges();
                return Json(new { IsValid = true, Mensaje = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new {IsValid = false, Mensaje = ex.Message});
            }
        }
   
        protected void PopulateDropDownList()
        {
           var _TA = context.sisTipo.Where(s => s.sisTipoEntidad.tipoeCodigo == "TA").ToList();

           int MedicoLogueado = 0;
           if ((Session["Usuario"] as sisUsuario).perId != null)
           {
               MedicoLogueado = (int)(Session["Usuario"] as sisUsuario).perId;
           }
           else
           {
               if ((Session["Usuario"] as sisUsuario).conId != null)
               {
                   MedicoLogueado = (int)(Session["Usuario"] as sisUsuario).conId * -1;
               }
           }

           ViewData["espId_Data"] = new SelectList(getEspecialidades(MedicoLogueado, false), "espId", "espNombre");
           ViewData["perId_Data"] = new SelectList(new List<catPersona>(), "perId", "perApellidoyNombre");
           ViewData["tipoId_Admision_Data"] = new SelectList(_TA, "tipoId", "tipoDescripcion");
         
        }
        //public ActionResult PopulateDropDownList_CartillaBasica()
        //{

        //    var _MIN = (from s in context.catMinisterio.ToList()
        //                select new catMinisterio()
        //                {
        //                    minId = s.minId,
        //                    minNombre = s.minNombre
        //                }).ToList();
        //    ViewData["minId_Data"] = new SelectList(_MIN, "minId", "minNombre");
        //}

        //private string getDescripcionAdmision(int _idTipoAdmision)
        //{
        //    return context.sisTipo.First(f => f.tipoId == _idTipoAdmision).tipoDescripcion;
        //}

        //private string getCodigoEstadoTurno(int _idTipo)
        //{
        //    return context.sisTipo.First(f => f.tipoId == _idTipo).tipoDescripcion;
        //}

        //private string getEstado(int _idTipo)
        //{

        //    if (_idTipo != null)
        //    {
        //        switch (getCodigoEstadoTurno(_idTipo))
        //        {
        //            case "DI":
        //                return "Amarillo.png";
        //            case "OT":
        //                return "Verde.png";
        //            case "AD":
        //                return "Celeste.png";
        //            case "BL":
        //                return "Rojo.png";

        //        }
        //    }

        //    return "Celeste.png";
        //}

        //especialiedades
        protected List<catEspecialidades> getEspecialidades(int perId, bool PermiteTodos)
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
              
                _Especialidades.AddRange ((from s in context.catEspecialidad //.ToList()
                                           join t in context.catAgenda on s.espId equals t.espId
                                           where t.csId == _csId && t.agActivo
                                       select new catEspecialidades
                                       {
                                           espId = s.espId,
                                           espNombre = s.espNombre + " (" + (s.espCodigo ?? "000") + ")"
                                       }).ToList().Distinct().OrderBy(o => o.espNombre));
                return _Especialidades.ToList();
            }
            if (perId > 0)
            {
                var _Especialidades = new List<catEspecialidades>();
                //_Especialidades.Add(new catEspecialidades
                //{
                //    espId = 0,
                //    espNombre = "SELECCIONE ESPECIALIDAD"
                //});
                _Especialidades.AddRange((from s in context.catEspecialidad
                                          join x in context.catPersonaEspecialidad.Where(z => z.perId == perId) on s.espId equals x.espId
                                           select new catEspecialidades
                                           {
                                               espId = s.espId,
                                               espNombre = s.espNombre + " (" + (s.espCodigo ?? "000") + ")"
                                           }).ToList().Distinct().OrderBy(o => o.espNombre));
                return _Especialidades;
            }
            var _conId = (perId*-1);
            var _Especialidad = (from s in context.catEspecialidad
                                 join x in context.catPersonaContratadosEspecialidad.Where(z => z.conId == _conId) on s.espId equals x.espId
                                 select new catEspecialidades
                                 {
                                     espId = s.espId,
                                     espNombre = s.espNombre + " (" + (s.espCodigo ?? "000") + ")"
                                 }).ToList().Distinct().OrderBy(o => o.espNombre);
            return _Especialidad.ToList();

        }

        //diagnosticos
        [GridAction]
        public ActionResult _SelectEditingTiposDiagnostico(int adId, string TextoBuscado)
        {
            var _Item = (from d in context.getBusquedaDiagnosticos(adId, TextoBuscado)
                         select new catDiagnosticos()
                         {
                             diagId = d.diagId,
                             DiagnosticoID = d.diagCodigo,
                             td1Descripcion = d.tdDescripcion,
                             td2Descripcion = d.subDescripcion,
                             Descripcion = d.diagDescripcion
                         }).ToList();

            return View(new GridModel<catDiagnosticos>
            {
                Data = _Item
            });
        }

        //practicas
        [GridAction]
        public ActionResult _SelectEditingTiposPractica(int nomId, string TextoBuscado)
        {
            var _Item = (from d in context.getBusquedaPracticas(nomId, TextoBuscado)
                         select new catPracticas()
                         {
                             pracId = d.pracId,
                             pracCosto = d.pracCosto,
                             nomId = d.nomId,
                             pracCodigo = d.pracCodigo,
                             pracDescripcion = d.pracDescripcion
                         }).ToList();

            return View(new GridModel<catPracticas>
            {
                Data = _Item
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setReAgendar(int especialidadId, int profesionalId, string Turno)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            try
            {
                context.setReAgendar(especialidadId, profesionalId, Turno);
            }
            catch (Exception ex)
            {
                return Json(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

            return Json("OK");
        }


        //[GridAction]
        //public ActionResult _SelectEditingTiposPractica()
        //{
        //    return View(new GridModel(AllTiposPractica()));
        //}

        //public IList<catPracticas> AllTiposPractica()
        //{
        //    return getTiposPracticas().ToList();
        //}
        //[GridAction]
        //public IEnumerable<catPracticas> getTiposPracticas()
        //{
        //    var _Item = (from d in context.vwNomencladorPracticas.ToList()
        //                 select new catPracticas()
        //                 {
        //                     pracId = d.pracId,
        //                     pracCosto = d.pracCosto,
        //                     nomId = d.nomId,
        //                     pracCodigo = d.pracCodigo,
        //                     pracDescripcion = d.pracDescripcion,
        //                     NomencladorDescripcion = d.nomDescripcion                           
        //                 }).ToList();
        //    return _Item.AsEnumerable();

        //}
    }
}
