namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Filters;
    using Models;

    public partial class catRequerimientoController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing(bool SoloMisRequerimientos, bool SoloPendientes)
        {
            return View(new GridModel(All(SoloMisRequerimientos, SoloPendientes)));
        }

        private IList<catRequerimientoWS> All(bool SoloMisRequerimientos, bool SoloPendientes)
        {
                return getDatos(SoloMisRequerimientos, SoloPendientes).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catRequerimiento _Item = new catRequerimiento();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                Create(_Item);
            }

            return View(new GridModel(All((bool)Session["SoloMisRequerimientos"], (bool)Session["SoloPendientes"])));
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
            catRequerimiento _Item = context.catRequerimiento.Where(p => p.reqId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All((bool)Session["SoloMisRequerimientos"], (bool)Session["SoloPendientes"])));
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

            return View(new GridModel(All((bool)Session["SoloMisRequerimientos"], (bool)Session["SoloPendientes"])));
        }

        private IEnumerable<catRequerimientoWS> getDatos(bool SoloMisRequerimientos, bool SoloPendientes)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _Datos = new List<catRequerimientoWS>();
            var Usuario = (Session["Usuario"] as sisUsuario);
            Session["SoloMisRequerimientos"] = SoloMisRequerimientos;
            Session["SoloPendientes"] = SoloPendientes;

            if (SoloMisRequerimientos && SoloPendientes)
            {
                _Datos = (from d in context.catRequerimiento.ToList()
                          where d.usrId == Usuario.usrId && ("SO#PE#EP").Contains(d.catRequerimientoEstado.OrderByDescending(o => o.reqeFecha).First().sisTipo.tipoCodigo)
                          select new catRequerimientoWS()
                              {
                                  reqDescripcion = d.reqDescripcion,
                                  reqId = d.reqId,
                                  reqAsunto = d.reqAsunto,
                                  tipoIdTipo = d.tipoIdTipo,
                                  perIdSolicitante = d.perIdSolicitante,
                                  reqLinkId = d.reqLinkId,
                                  Dependencia = d.reqLinkId == null ? "" : context.catRequerimiento.Where(x => x.reqId == d.reqLinkId).First().sisTipo.tipoDescripcion + " Nº " + d.reqLinkId.ToString(),
                                  Solicitante = new catPersonas() {perApellidoyNombre = d.catPersona.perApellidoyNombre},
                                  Tipo =
                                      new sisTipos()
                                          {
                                              tipoId = d.tipoIdTipo,
                                              tipoDescripcion = d.sisTipo.tipoDescripcion,
                                              tipoCodigo = d.sisTipo.tipoCodigo,
                                              tipoImagen = d.sisTipo.tipoImagen
                                          },
                                  reqTiempoEstimado = d.reqTiempoEstimado,
                                  TiempoEstimado =
                                      d.reqTiempoEstimado == null
                                          ? ""
                                          : (d.reqTiempoEstimado > 6
                                                 ? Math.Ceiling(d.reqTiempoEstimado.Value/6.00).ToString() + " día(s)"
                                                 : d.reqTiempoEstimado.Value.ToString()),
                                  Asignado = new sisUsuarios() {usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre},
                                  usrId = d.usrId,
                                  reqFechaEstimado = d.reqFechaEstimado,
                                  Estado = (from x in d.catRequerimientoEstado
                                            orderby x.reqeFecha descending
                                            select
                                                new catRequerimientoEstadoWS()
                                                    {
                                                        reqeFecha = x.reqeFecha,
                                                        tipoIdEstado = x.tipoIdEstado,
                                                        usrId = x.usrId,
                                                        Tipo =
                                                            new sisTipos()
                                                                {
                                                                    tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                                    tipoImagen = x.sisTipo.tipoImagen,
                                                                    tipoCodigo = x.sisTipo.tipoCodigo
                                                                },
                                                        Usuario =
                                                            new sisUsuarios()
                                                                {
                                                                    usrApellidoyNombre = x.sisUsuario.usrApellidoyNombre
                                                                }
                                                    })
                              .First(),
                                  Creado = (from x in d.catRequerimientoEstado
                                            orderby x.reqeFecha ascending
                                            select
                                                new catRequerimientoEstadoWS()
                                                    {
                                                        reqeFecha = x.reqeFecha,
                                                        tipoIdEstado = x.tipoIdEstado,
                                                        usrId = x.usrId,
                                                        Tipo =
                                                            new sisTipos()
                                                                {
                                                                    tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                                    tipoImagen = x.sisTipo.tipoImagen,
                                                                    tipoCodigo = x.sisTipo.tipoCodigo
                                                                },
                                                        Usuario =
                                                            new sisUsuarios()
                                                                {
                                                                    usrApellidoyNombre = x.sisUsuario.usrApellidoyNombre
                                                                }
                                                    })
                              .First(),
                              CantidadAdjuntos = d.catRequerimientoAdjunto.Count,
                              CantidadComentarios = d.catRequerimientoComentarios.Count,
                              CantidadLogWork = d.catRequerimientoLogTrabajo.Count
                              }).ToList();
            }
            else
            {
                if (SoloMisRequerimientos && !SoloPendientes)
                {
                    _Datos = (from d in context.catRequerimiento.ToList()
                              where d.usrId == Usuario.usrId
                              select new catRequerimientoWS()
                              {
                                  reqDescripcion = d.reqDescripcion,
                                  reqId = d.reqId,
                                  reqAsunto = d.reqAsunto,
                                  tipoIdTipo = d.tipoIdTipo,
                                  perIdSolicitante = d.perIdSolicitante,
                                  reqLinkId = d.reqLinkId,
                                  Dependencia = d.reqLinkId == null ? "" : context.catRequerimiento.Where(x => x.reqId == d.reqLinkId).First().sisTipo.tipoDescripcion + " Nº " + d.reqLinkId.ToString(),
                                  Solicitante = new catPersonas() { perApellidoyNombre = d.catPersona.perApellidoyNombre },
                                  Tipo =
                                      new sisTipos()
                                      {
                                          tipoId = d.tipoIdTipo,
                                          tipoDescripcion = d.sisTipo.tipoDescripcion,
                                          tipoCodigo = d.sisTipo.tipoCodigo,
                                          tipoImagen = d.sisTipo.tipoImagen
                                      },
                                  reqTiempoEstimado = d.reqTiempoEstimado,
                                  TiempoEstimado =
                                      d.reqTiempoEstimado == null
                                          ? ""
                                          : (d.reqTiempoEstimado > 6
                                                 ? Math.Ceiling(d.reqTiempoEstimado.Value / 6.00).ToString() + " día(s)"
                                                 : d.reqTiempoEstimado.Value.ToString()),
                                  Asignado = new sisUsuarios() { usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre },
                                  usrId = d.usrId,
                                  reqFechaEstimado = d.reqFechaEstimado,
                                  Estado = (from x in d.catRequerimientoEstado
                                            orderby x.reqeFecha descending
                                            select
                                                new catRequerimientoEstadoWS()
                                                {
                                                    reqeFecha = x.reqeFecha,
                                                    tipoIdEstado = x.tipoIdEstado,
                                                    usrId = x.usrId,
                                                    Tipo =
                                                        new sisTipos()
                                                        {
                                                            tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                            tipoImagen = x.sisTipo.tipoImagen,
                                                            tipoCodigo = x.sisTipo.tipoCodigo
                                                        },
                                                    Usuario =
                                                        new sisUsuarios()
                                                        {
                                                            usrApellidoyNombre = x.sisUsuario.usrApellidoyNombre
                                                        }
                                                })
                              .First(),
                                  Creado = (from x in d.catRequerimientoEstado
                                            orderby x.reqeFecha ascending
                                            select
                                                new catRequerimientoEstadoWS()
                                                {
                                                    reqeFecha = x.reqeFecha,
                                                    tipoIdEstado = x.tipoIdEstado,
                                                    usrId = x.usrId,
                                                    Tipo =
                                                        new sisTipos()
                                                        {
                                                            tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                            tipoImagen = x.sisTipo.tipoImagen,
                                                            tipoCodigo = x.sisTipo.tipoCodigo
                                                        },
                                                    Usuario =
                                                        new sisUsuarios()
                                                        {
                                                            usrApellidoyNombre = x.sisUsuario.usrApellidoyNombre
                                                        }
                                                })
                              .First(),
                                    CantidadAdjuntos = d.catRequerimientoAdjunto.Count,
                                    CantidadComentarios = d.catRequerimientoComentarios.Count,
                                    CantidadLogWork = d.catRequerimientoLogTrabajo.Count
                              }).ToList();
                }
                else
                {
                    if (!SoloMisRequerimientos && SoloPendientes)
                    {
                        _Datos = (from d in context.catRequerimiento.ToList()
                                  where
                                      ("SO#PE#EP").Contains(
                                          d.catRequerimientoEstado.OrderByDescending(o => o.reqeFecha)
                                           .First()
                                           .sisTipo.tipoCodigo)
                                  select new catRequerimientoWS()
                                      {
                                          reqDescripcion = d.reqDescripcion,
                                          reqId = d.reqId,
                                          reqAsunto = d.reqAsunto,
                                          tipoIdTipo = d.tipoIdTipo,
                                          perIdSolicitante = d.perIdSolicitante,
                                          reqLinkId = d.reqLinkId,
                                          Dependencia = d.reqLinkId == null ? "" : context.catRequerimiento.Where(x => x.reqId == d.reqLinkId).First().sisTipo.tipoDescripcion + " Nº " + d.reqLinkId.ToString(),
                                          Solicitante =
                                              new catPersonas() {perApellidoyNombre = d.catPersona.perApellidoyNombre},
                                          Tipo =
                                              new sisTipos()
                                                  {
                                                      tipoId = d.tipoIdTipo,
                                                      tipoDescripcion = d.sisTipo.tipoDescripcion,
                                                      tipoCodigo = d.sisTipo.tipoCodigo,
                                                      tipoImagen = d.sisTipo.tipoImagen
                                                  },
                                          reqTiempoEstimado = d.reqTiempoEstimado,
                                          TiempoEstimado =
                                              d.reqTiempoEstimado == null
                                                  ? ""
                                                  : (d.reqTiempoEstimado > 6
                                                         ? Math.Ceiling(d.reqTiempoEstimado.Value/6.00).ToString() +
                                                           " día(s)"
                                                         : d.reqTiempoEstimado.Value.ToString()),
                                          Asignado =
                                              new sisUsuarios() {usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre},
                                          usrId = d.usrId,
                                          reqFechaEstimado = d.reqFechaEstimado,
                                          Estado = (from x in d.catRequerimientoEstado
                                                    orderby x.reqeFecha descending
                                                    select
                                                        new catRequerimientoEstadoWS()
                                                            {
                                                                reqeFecha = x.reqeFecha,
                                                                tipoIdEstado = x.tipoIdEstado,
                                                                usrId = x.usrId,
                                                                Tipo =
                                                                    new sisTipos()
                                                                        {
                                                                            tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                                            tipoImagen = x.sisTipo.tipoImagen,
                                                                            tipoCodigo = x.sisTipo.tipoCodigo
                                                                        },
                                                                Usuario =
                                                                    new sisUsuarios()
                                                                        {
                                                                            usrApellidoyNombre =
                                                                                x.sisUsuario.usrApellidoyNombre
                                                                        }
                                                            })
                                      .First(),
                                          Creado = (from x in d.catRequerimientoEstado
                                                    orderby x.reqeFecha ascending
                                                    select
                                                        new catRequerimientoEstadoWS()
                                                            {
                                                                reqeFecha = x.reqeFecha,
                                                                tipoIdEstado = x.tipoIdEstado,
                                                                usrId = x.usrId,
                                                                Tipo =
                                                                    new sisTipos()
                                                                        {
                                                                            tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                                            tipoImagen = x.sisTipo.tipoImagen,
                                                                            tipoCodigo = x.sisTipo.tipoCodigo
                                                                        },
                                                                Usuario =
                                                                    new sisUsuarios()
                                                                        {
                                                                            usrApellidoyNombre =
                                                                                x.sisUsuario.usrApellidoyNombre
                                                                        }
                                                            })
                                      .First(),
                                          CantidadAdjuntos = d.catRequerimientoAdjunto.Count,
                                          CantidadComentarios = d.catRequerimientoComentarios.Count,
                                          CantidadLogWork = d.catRequerimientoLogTrabajo.Count
                                      }).ToList();
                    }
                    else
                    {
                        _Datos = (from d in context.catRequerimiento.ToList()
                                  select new catRequerimientoWS()
                                      {
                                          reqDescripcion = d.reqDescripcion,
                                          reqId = d.reqId,
                                          reqAsunto = d.reqAsunto,
                                          tipoIdTipo = d.tipoIdTipo,
                                          perIdSolicitante = d.perIdSolicitante,
                                          reqLinkId = d.reqLinkId,
                                          Dependencia = d.reqLinkId == null ? "" : context.catRequerimiento.Where(x => x.reqId == d.reqLinkId).First().sisTipo.tipoDescripcion + " Nº " + d.reqLinkId.ToString(),
                                          Solicitante =
                                              new catPersonas() {perApellidoyNombre = d.catPersona.perApellidoyNombre},
                                          Tipo =
                                              new sisTipos()
                                                  {
                                                      tipoId = d.tipoIdTipo,
                                                      tipoDescripcion = d.sisTipo.tipoDescripcion,
                                                      tipoCodigo = d.sisTipo.tipoCodigo,
                                                      tipoImagen = d.sisTipo.tipoImagen
                                                  },
                                          reqTiempoEstimado = d.reqTiempoEstimado,
                                          TiempoEstimado =
                                              d.reqTiempoEstimado == null
                                                  ? ""
                                                  : (d.reqTiempoEstimado > 6
                                                         ? Math.Ceiling(d.reqTiempoEstimado.Value/6.00).ToString() +
                                                           " día(s)"
                                                         : d.reqTiempoEstimado.Value.ToString()),
                                          Asignado =
                                              new sisUsuarios() {usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre},
                                          usrId = d.usrId,
                                          reqFechaEstimado = d.reqFechaEstimado,
                                          Estado = (from x in d.catRequerimientoEstado
                                                    orderby x.reqeFecha descending
                                                    select
                                                        new catRequerimientoEstadoWS()
                                                            {
                                                                reqeFecha = x.reqeFecha,
                                                                tipoIdEstado = x.tipoIdEstado,
                                                                usrId = x.usrId,
                                                                Tipo =
                                                                    new sisTipos()
                                                                        {
                                                                            tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                                            tipoImagen = x.sisTipo.tipoImagen,
                                                                            tipoCodigo = x.sisTipo.tipoCodigo
                                                                        },
                                                                Usuario =
                                                                    new sisUsuarios()
                                                                        {
                                                                            usrApellidoyNombre =
                                                                                x.sisUsuario.usrApellidoyNombre
                                                                        }
                                                            })
                                      .First(),
                                          Creado = (from x in d.catRequerimientoEstado
                                                    orderby x.reqeFecha ascending
                                                    select
                                                        new catRequerimientoEstadoWS()
                                                            {
                                                                reqeFecha = x.reqeFecha,
                                                                tipoIdEstado = x.tipoIdEstado,
                                                                usrId = x.usrId,
                                                                Tipo =
                                                                    new sisTipos()
                                                                        {
                                                                            tipoDescripcion = x.sisTipo.tipoDescripcion,
                                                                            tipoImagen = x.sisTipo.tipoImagen,
                                                                            tipoCodigo = x.sisTipo.tipoCodigo
                                                                        },
                                                                Usuario =
                                                                    new sisUsuarios()
                                                                        {
                                                                            usrApellidoyNombre =
                                                                                x.sisUsuario.usrApellidoyNombre
                                                                        }
                                                            })
                                      .First(),
                                          CantidadAdjuntos = d.catRequerimientoAdjunto.Count,
                                          CantidadComentarios = d.catRequerimientoComentarios.Count,
                                          CantidadLogWork = d.catRequerimientoLogTrabajo.Count
                                      }).ToList();
                    }
                }

            }

            _Datos = (from d in _Datos
                      select new catRequerimientoWS()
                        {
                            reqDescripcion = d.reqDescripcion,
                            reqId = d.reqId,
                            reqAsunto = d.reqAsunto,
                            tipoIdTipo = d.tipoIdTipo,
                            perIdSolicitante = d.perIdSolicitante,
                            Solicitante = d.Solicitante,
                            Tipo = d.Tipo,
                            reqTiempoEstimado = d.reqTiempoEstimado,
                            TiempoEstimado = d.TiempoEstimado,
                            Asignado = d.Asignado,
                            usrId = d.usrId,
                            reqFechaEstimado = d.reqFechaEstimado,
                            Estado = d.Estado,
                            Creado = d.Creado,
                            reqLinkId = d.reqLinkId,
                            Dependencia = d.Dependencia,
                            NoCambiarEstado = "AN#TE#IM".Contains(d.Estado.Tipo.tipoCodigo),
                            NoEditar = "EP#AN#TE#IM".Contains(d.Estado.Tipo.tipoCodigo),
                            CantidadAdjuntos = d.CantidadAdjuntos,
                            CantidadComentarios = d.CantidadComentarios,
                            CantidadLogWork = d.CantidadLogWork
                        }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Tareas / Requerimientos";
            Session["PathArchivos"] = Server.MapPath("~/Content/Archivos/Requerimientos");
            ViewBag.NombreAccion = "getRequerimientosAVincular";
            ViewBag.NombreControlador = "catRequerimiento";
            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catRequerimiento pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string TipoCodigo = context.sisTipo.Where(w => w.tipoId == pItem.tipoIdTipo).First().tipoCodigo == "TA" ? "PE" : "SO";
                    var _tipoId = context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "ER" && w.tipoCodigo == TipoCodigo).First().tipoId;
                    pItem.reqAsunto = pItem.reqAsunto.ToUpper();

                    if (context.catAsunto.Where(w => w.asuDescripcion == pItem.reqAsunto).Count() == 0)
                    {
                        context.catAsunto.AddObject(new catAsunto(){ asuDescripcion = pItem.reqAsunto });
                    }

                    pItem.reqTiempoEstimado = 0;
                    context.catRequerimiento.AddObject(pItem);
                    context.catRequerimientoEstado.AddObject(new catRequerimientoEstado() { reqeFecha = DateTime.Now, reqId = pItem.reqId, tipoIdEstado = _tipoId, usrId = (Session["Usuario"] as sisUsuario).usrId, reqeObservaciones = "" });

                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catRequerimiento", "Agregar", "Agrega un requerimiento de software");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Solicitante", ex.Message);
                }
            }

            return;
        }

        private void Edit(catRequerimiento pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catRequerimiento", "Modificar", "Modifica requerimiento");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Solicitante", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id, bool pEstado)
        {
            try
            {
                catRequerimiento _Item = context.catRequerimiento.Single(x => x.reqId == id);
                var _tipoId = context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "ER" && w.tipoCodigo == "AN").First().tipoId;
                
                context.catRequerimientoEstado.AddObject(new catRequerimientoEstado() { reqeFecha = DateTime.Now, reqId = id, tipoIdEstado = _tipoId, usrId = (Session["Usuario"] as sisUsuario).usrId, reqeObservaciones = "" });
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra", "Eliminar", "Elimina compra");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Solicitante", ex.Message);
            }
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var _Empleados = (from p in context.catPersona.ToList()
                              orderby p.perApellidoyNombre
                                select new catPersonas
                                {
                                perId = p.perId,
                                perDNI = p.perDNI,
                                perApellidoyNombre = p.perApellidoyNombre
                                }).ToList();

            CargarAsunto();

            ViewData["perIdSolicitante_Data"] = new SelectList(_Empleados, "perId", "perApellidoyNombre");
            ViewData["tipoIdTipo_Data"] = new SelectList(context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "TR").OrderBy(o => o.tipoDescripcion), "tipoId", "tipoDescripcion");
            ViewData["usrId_Data"] = new SelectList(context.sisUsuario, "usrId", "usrApellidoyNombre");
        }

        protected void CargarAsunto()
        {
            var _Asuntos = (from asu in context.catAsunto.ToList()
                            orderby asu.asuDescripcion
                            select new catAsunto()
                            {
                                asuId = asu.asuId,
                                asuDescripcion = asu.asuDescripcion
                            }).ToList();

            ViewData["reqAsunto_Data"] = _Asuntos.Select(s => s.asuDescripcion);
        }

        public ActionResult getRequerimientosAVincular(string text)
        {
            var _Requerimientos = (from d in context.catRequerimiento.ToList()
                                   where ("NF#MO").Contains(d.sisTipo.tipoCodigo) && ("PE#EP").Contains(d.catRequerimientoEstado.OrderByDescending(o => o.reqeFecha).First().sisTipo.tipoCodigo)
                                    select new catRequerimientoWS()
                                    {
                                        reqId = d.reqId,
                                        reqDescripcion = "+" + d.reqId + " - " + d.reqDescripcion
                                    }).ToList();

            return new JsonResult { Data = new SelectList(_Requerimientos, "reqId", "reqDescripcion") };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CambiarEstado(int reqId, string pEstadoNuevo, string pObservaciones)
        {
            try
            {
                var _tipoId = context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "ER" && w.tipoCodigo == pEstadoNuevo).First().tipoId;

                context.catRequerimientoEstado.AddObject(new catRequerimientoEstado() { reqeFecha = DateTime.Now, reqId = reqId, tipoIdEstado = _tipoId, usrId = (Session["Usuario"] as sisUsuario).usrId, reqeObservaciones = pObservaciones });

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catRequerimiento", "Modificar", "Cambia estado de un requerimiento de software");

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Solicitante", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return Json(false);
            }

            return Json(true);
        }

    }
}