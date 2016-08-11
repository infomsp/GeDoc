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

    public partial class catTicketSoporteController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catTicketSoportes> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catTicketSoporte _Item = new catTicketSoporte();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                Create(_Item);
            }

            return View(new GridModel(All()));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[GridAction]
        //public ActionResult _DeleteEditing(int id)
        //{
        //    if (Session["UsuarioCentroDeSalud"] == null)
        //    {
        //        RedirectToAction("LogOff", "Account");
        //        return null;
        //    }
        //    //DeleteConfirmed(id, false);

        //    return View(new GridModel(All()));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _AtenderTickets(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            catTicketSoporte _Item = context.catTicketSoporte.Where(p => p.tiqId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item, "E1", "", null);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SolucionarTickets(int id, string MotivoSoporte, short? idMotivo, string pEstado)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            if (idMotivo <= 0)
            {
                idMotivo = null;
            }
            catTicketSoporte _Item = context.catTicketSoporte.Where(p => p.tiqId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item, pEstado, MotivoSoporte, idMotivo);

            return View(new GridModel(All()));
        }

        private IEnumerable<catTicketSoportes> getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _Datos = (from d in context.catTicketSoporte.ToList()
                          select new catTicketSoportes()
                                    {
                                        conId = d.conId,
                                        Empleado = getEmpleado(d.conId, d.perId),
                                        Estado = getUltimoEstado(d.tiqId),
                                        MotivoLlamada = new sisTipos() { tipoId = d.tipoIdLlamada, tipoDescripcion = d.sisTipo1.tipoDescripcion, tipoCodigo = d.sisTipo1.tipoCodigo },
                                        tipoIdLlamada = d.tipoIdLlamada,
                                        tiqDetalle = d.tiqDetalle,
                                        Oficina = new catOficinas() { ofiId = d.ofiId, ofiNombre = d.catOficina.ofiNombre },
                                        ofiId = d.ofiId,
                                        perId = d.perId,
                                        Prioridad = new sisTipos() { tipoId = d.sisTipo.tipoId, tipoDescripcion = d.sisTipo.tipoDescripcion, tipoCodigo = d.sisTipo.tipoCodigo },
                                        tipoIdPrioridad = d.tipoIdPrioridad,
                                        tiqId = d.tiqId,
                                        tiqFecha = (d.catTicketSoporteEstado == null ? DateTime.Now : d.catTicketSoporteEstado.Where(w => w.sisTipo.tipoCodigo == "E0").First().tiqeFecha).Date,
                                        tiqHora = (d.catTicketSoporteEstado == null ? DateTime.Now : d.catTicketSoporteEstado.Where(w => w.sisTipo.tipoCodigo == "E0").First().tiqeFecha).ToString("hh:mm:ss"),
                                        Historial = getEstados(d.tiqId)
                                    }).ToList();

            var _DatosOrdenados = from o in _Datos
                                  orderby o.Estado.Estado.tipoCodigo, o.Prioridad.tipoCodigo, o.Estado.tiqeFecha
                                  select o;

            return _DatosOrdenados.AsEnumerable();
        }

        private catPersonas getEmpleado(int? pconId, int? pperId)
        {
            catPersonas _Resultado = new catPersonas();

            if (pperId != null && pperId > 0)
            {
                _Resultado = (from x in context.catPersona.ToList()
                              where x.perId == pperId
                              select new catPersonas()
                              {
                                  perApellidoyNombre = x.perApellidoyNombre,
                                  perId = x.perId
                              }).First();
            }
            else
            {
                _Resultado = (from x in context.catPersonaContratados.ToList()
                              where x.conId == pconId
                              select new catPersonas()
                              {
                                  perApellidoyNombre = x.conApellidoyNombre,
                                  perId = x.conId
                              }).First();
            }

            return _Resultado;
        }

        private catTicketSoportesEstados getUltimoEstado(long ptiqId)
        {
            catTicketSoportesEstados _Resultado = new catTicketSoportesEstados();

            if (context.catTicketSoporteEstado.Where(w => w.tiqId == ptiqId).Count() > 0)
            {
                _Resultado = (from x in context.catTicketSoporteEstado.ToList()
                              where x.tiqId == ptiqId
                              orderby x.tiqeFecha descending
                              select new catTicketSoportesEstados()
                              {
                                  Estado = new sisTipos() { tipoId = x.sisTipo.tipoId, tipoDescripcion = x.sisTipo.tipoDescripcion, tipoCodigo = x.sisTipo.tipoCodigo },
                                  MotivoSoporte = x.motsId == null ? null : new catMotivosSoporte() { motsId = x.motsId, motsDescripcion = x.catMotivoSoporte.motsDescripcion },
                                  tipoIdEstado = x.tipoIdEstado,
                                  tiqeDetalle = x.tiqeDetalle,
                                  tiqeFecha = x.tiqeFecha.Date,
                                  tiqeHora = x.tiqeFecha.ToString("hh:mm:ss"),
                                  tiqeId = x.tiqeId,
                                  tiqId = x.tiqId,
                                  usrId = x.usrId,
                                  motsId = x.motsId,
                                  Usuario = new sisUsuarios() { usrId = x.sisUsuario.usrId, usrApellidoyNombre = x.sisUsuario.usrApellidoyNombre }
                              }).First();
            }
            return _Resultado;
        }

        private List<catTicketSoportesEstados> getEstados(long ptiqId)
        {
            List<catTicketSoportesEstados> _Resultado = new List<catTicketSoportesEstados>();

            if (context.catTicketSoporteEstado.Where(w => w.tiqId == ptiqId).Count() > 0)
            {
                _Resultado = (from x in context.catTicketSoporteEstado.ToList()
                              where x.tiqId == ptiqId
                              orderby x.tiqeFecha ascending
                              select new catTicketSoportesEstados()
                              {
                                  Estado = new sisTipos() { tipoId = x.sisTipo.tipoId, tipoDescripcion = x.sisTipo.tipoDescripcion, tipoCodigo = x.sisTipo.tipoCodigo },
                                  MotivoSoporte = x.motsId == null ? null : new catMotivosSoporte() { motsId = x.motsId, motsDescripcion = x.catMotivoSoporte.motsDescripcion },
                                  tipoIdEstado = x.tipoIdEstado,
                                  tiqeDetalle = x.tiqeDetalle,
                                  tiqeFecha = x.tiqeFecha,
                                  tiqeId = x.tiqeId,
                                  tiqId = x.tiqId,
                                  usrId = x.usrId,
                                  motsId = x.motsId,
                                  Usuario = new sisUsuarios() { usrId = x.sisUsuario.usrId, usrApellidoyNombre = x.sisUsuario.usrApellidoyNombre }
                              }).ToList();
            }
            return _Resultado;
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Tickets de Soporte Informático";
            ViewData["TicketSoportesEstados"] = getEstados(-1);
            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catTicketSoporte pItem)
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

                    Edit(pItem, "E0", "", null);
                    context.catTicketSoporte.AddObject(pItem);

                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTicketSoporte", "Agregar", "Agrega un tickets de soporte");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Empleado", ex.Message);
                }
            }

            return;
        }

        private void Edit(catTicketSoporte pItem, string ptipoCodigo, string pDetalle, short? pmotsId)
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

                    var _ItemEstado = new catTicketSoporteEstado();
                    var _Estado = context.sisTipo.Where(w => w.tipoCodigo == ptipoCodigo).First();
                    var _Prioridad = context.sisTipo.Where(w => w.tipoId == pItem.tipoIdPrioridad).First();

                    if (ptipoCodigo == "E0" && _Prioridad.tipoCodigo != "P0")
                    {
                        if (context.catTicketSoporteEstado.Where(w => w.tiqId == pItem.tiqId && w.tipoIdEstado == _Estado.tipoId).Count() >= 2)
                        {
                            var _PrioridadAlta = context.sisTipo.Where(w => w.tipoCodigo == "P0").First();
                            pItem.tipoIdPrioridad = _PrioridadAlta.tipoId;
                        }
                    }

                    _ItemEstado.tiqeFecha = DateTime.Now;
                    _ItemEstado.tiqeDetalle = pDetalle;
                    _ItemEstado.motsId = pmotsId;
                    _ItemEstado.tipoIdEstado = _Estado.tipoId;
                    _ItemEstado.usrId = (Session["Usuario"] as sisUsuario).usrId;

                    pItem.catTicketSoporteEstado.Add(_ItemEstado);

                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTicketSoporte", "Estado", "Cambia el ticket número " + pItem.tiqId.ToString() + " a estado " + _Estado.tipoDescripcion.ToUpper());

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Empleado", ex.Message);
                }
            }
            return;
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            dcAccesoCompadDataContext _Centros = new dcAccesoCompadDataContext();
            var _Empleados = (from p in context.catPersona.ToList()
                                  select new catPersonas
                                  {
                                    perId = p.perId,
                                    perDNI = p.perDNI,
                                    perApellidoyNombre = p.perApellidoyNombre
                                  }).ToList();

            _Empleados.AddRange((from p in context.catPersonaContratados.ToList()
                                      where p.conFechaBaja == null
                                      select new catPersonas
                                      {
                                          perId = (p.conId * -1),
                                          perDNI = p.conDNI,
                                          perApellidoyNombre = p.conApellidoyNombre
                                      }).ToList());
            _Empleados = _Empleados.OrderBy(o => o.perApellidoyNombre).ToList();

            _Empleados = (from p in _Empleados
                              orderby p.perApellidoyNombre ascending
                              select new catPersonas
                              {
                                  perId = p.perId,
                                  perApellidoyNombre = (p.perDNI == null || p.perDNI == 0 ? "" : "(" + p.perDNI + ") ") + p.perApellidoyNombre
                              }).ToList();

            ViewData["perId_Data"] = new SelectList(_Empleados, "perId", "perApellidoyNombre");
            ViewData["ofiId_Data"] = new SelectList(context.catOficina.OrderBy(o => o.ofiNombre), "ofiId", "ofiNombre");
            ViewData["motsId_Data"] = new SelectList(context.catMotivoSoporte.OrderBy(o => o.motsDescripcion), "motsId", "motsDescripcion");
            ViewData["tipoIdLlamada_Data"] = new SelectList(context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "ML").OrderBy(o => o.tipoDescripcion), "tipoId", "tipoDescripcion");
            ViewData["tipoIdPrioridad_Data"] = new SelectList(context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "PY").OrderBy(o => o.tipoCodigo), "tipoId", "tipoDescripcion");

            return;
        }
    }
}