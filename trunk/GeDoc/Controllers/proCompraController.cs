using System.Data;
using System.Diagnostics;
using System.IO;
using System.Transactions;
using NPOI.HSSF.UserModel;
using Telerik.Web.Mvc.Extensions;

namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class proCompraController : Controller
    {
        private class Anios
        {
            public int Anio
            {
                set;
                get;
            }
        }

        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing(string filtro, int Anio)
        {
            Session["FiltroCompra"] = filtro;
            Session["AñoCompra"] = Anio;
            return View(new GridModel(All(filtro, Anio)));
        }

        [GridAction]
        public ActionResult _SelectEditingProductos(int idCompra)
        {
            return View(new GridModel(AllProductos(idCompra).AsEnumerable()));
        }

        private List<proCompraDetalleWS> AllProductos(int idCompra)
        {
            var _Resultado = from x in context.proCompraDetalle.ToList()
                             where x.comId == idCompra
                             select new proCompraDetalleWS()
                             {
                                 comId = x.comId,
                                 comdCantidad = x.comdActivo ? x.comdCantidad : 0,
                                 comdDetalle = x.comdDetalle,
                                 comdDetallePresupuestado = x.comdDetallePresupuestado,
                                 comdPrecioEstimado = x.comdActivo ? x.comdPrecioEstimado : 0,
                                 comdPrecioPresupuestado = x.comdActivo ? x.comdPrecioPresupuestado : 0,
                                 comdActivo = x.comdActivo,
                                 comdObservaciones = x.comdObservaciones,
                                 comdId = x.comdId,
                                 comdSubTotal = x.comdActivo ? x.comdCantidad * x.comdPrecioEstimado : 0
                             };
            return _Resultado.ToList();
        }

        private IList<proCompraWS> All(string filtro, int Anio)
        {
                return getDatos(filtro, Anio).ToList();
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
            var _Item = context.proCompra.FirstOrDefault(p => p.comId == id);

            TryUpdateModel(_Item);

            //Se valida el número de comprobante que no exista\\
            var YaExiste =
                context.proCompra.Count(
                    c =>
                        c.tipoId == _Item.tipoId && c.comComprobante == _Item.comComprobante &&
                        c.comId != id &&
                        c.sisTipo2.tipoCodigo != "AN");
            if (YaExiste > 0)
            {
                ModelState.AddModelError("comComprobante", "Comprobante repetido");
            }
            else
            {
                _Item.comLugarDeEntrega = _Item.comLugarDeEntrega ?? "";
                Edit(_Item);
            }

            return View(new GridModel(All(Session["FiltroCompra"].ToString(), (int)Session["AñoCompra"])));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingProductos(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _Item = context.proCompraDetalle.FirstOrDefault(p => p.comdId == id);

            TryUpdateModel(_Item);

            EditProductos(_Item);

            return View(new GridModel(AllProductos(_Item.comId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            var _Item = new proCompra();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }

                //Se valida el número de comprobante que no exista\\
                var YaExiste =
                    context.proCompra.Count(
                        c =>
                            c.tipoId == _Item.tipoId && c.comComprobante == _Item.comComprobante &&
                            c.sisTipo2.tipoCodigo != "AN");
                if (YaExiste > 0)
                {
                    ModelState.AddModelError("comComprobante", "Comprobante repetido");
                }
                else
                {
                    _Item.comLugarDeEntrega = _Item.comLugarDeEntrega ?? "";
                    Create(_Item);
                }
            }

            return View(new GridModel(All(Session["FiltroCompra"].ToString(), (int)Session["AñoCompra"])));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertEditingProductos()
        {
            proCompraDetalle _Item = new proCompraDetalle();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                CreateProductos(_Item);
            }

            return View(new GridModel(AllProductos(_Item.comId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _InsertEditingProductosImportados(int comId)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            int Fila = 0;
            //string SeFrena = "";
            decimal ResultadoSalida;

            try
            {
                var _tipoId = context.sisTipo.First(w => w.sisTipoEntidad.tipoeCodigo == "CM" && w.tipoCodigo == "CD").tipoId;
                var ItemCompra = context.proCompra.Single(w => w.comId == comId);
                var pItem = new proCompraDetalle();
                var DetalleDeProductos = context.proCompraDetalle.Where(c => c.comId == comId).ToList();
                var ArchivoImportado = (Session["ArchivoImportado"] as DataSet);
                //using (var transaction = new TransactionScope())
                //{
                    for (Fila = 0; Fila < ArchivoImportado.Tables[0].Rows.Count; Fila++)
                    {
                        //if (Fila >= 230)
                        //{
                        //    SeFrena = "Se Frena";
                        //}
                        var Detalle = ArchivoImportado.Tables[0].Rows[Fila]["Detalle"].ToString();
                        if (Detalle == "" 
                            || !decimal.TryParse(ArchivoImportado.Tables[0].Rows[Fila]["Precio"].ToString(), out ResultadoSalida)
                            || !decimal.TryParse(ArchivoImportado.Tables[0].Rows[Fila]["Cantidad"].ToString(), out ResultadoSalida))
                        {
                            continue;
                        }
                        var Precio = Convert.ToDecimal(ArchivoImportado.Tables[0].Rows[Fila]["Precio"].ToString());
                        var Cantidad = Convert.ToDecimal(ArchivoImportado.Tables[0].Rows[Fila]["Cantidad"].ToString());

                        if (DetalleDeProductos.Count(c => c.comdDetalle == Detalle) > 0)
                        {
                            pItem = context.proCompraDetalle.Single(c => c.comId == comId && c.comdDetalle == Detalle);
                        }
                        else
                        {
                            pItem = new proCompraDetalle();
                        }

                        pItem.comdActivo = true;
                        pItem.comdObservaciones = "";
                        pItem.comdDetallePresupuestado = "";
                        pItem.comdPrecioPresupuestado = 0;
                        pItem.comId = comId;
                        pItem.comdCantidad = Cantidad;
                        pItem.comdPrecioEstimado = Precio;
                        pItem.comdDetalle = Detalle;

                        if (DetalleDeProductos.Count(c => c.comdDetalle == Detalle) == 0)
                        {
                            context.proCompraDetalle.AddObject(pItem);
                        }
                        context.SaveChanges();
                    }

                    ItemCompra.tipoIdEstado = _tipoId;

                    context.proCompraEstado.AddObject(new proCompraEstado()
                    {
                        comeFecha = DateTime.Now,
                        comId = comId,
                        tipoId = _tipoId,
                        usrId = (Session["Usuario"] as sisUsuario).usrId,
                        comeObservaciones = ""
                    });

                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra",
                        "Agregar", "Agrega detalle de compra a partir de archivo xls");

                    context.SaveChanges();
                //    transaction.Complete();
                //}
            }
            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });
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

            return View(new GridModel(All(Session["FiltroCompra"].ToString(), (int)Session["AñoCompra"])));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingProductos(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            int idCompra = DeleteConfirmedProductos(id, false);

            return View(new GridModel(AllProductos(idCompra)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistro(int id)
        {
            DeleteConfirmed(id, true);

            return View(new GridModel(All(Session["FiltroCompra"].ToString(), (int)Session["AñoCompra"])));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistroProducto(int id)
        {
            int idCompra = DeleteConfirmedProductos(id, true);

            return View(new GridModel(AllProductos(idCompra)));
        }

        private SelectList getAnios()
        {
            List<Anios> _Anios = new List<Anios>();

            for (int _A = 2000; _A <= 2200; _A++)
            {
                _Anios.Add(new Anios() { Anio = _A });
            }

            return new SelectList(_Anios, "Anio", "Anio");
        }

        private IEnumerable<proCompraWS> getDatos(string filtro, int Anio)
        {
            if (ViewData["Anio_Data"] == null)
            {
                ViewData["Anio_Data"] = getAnios();
            }

            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var Estado = "SD,CD,PP,PO,PR,AD,OE,CA,PS";
            if (filtro != "~")
            {
                Estado = "SD,CD,AN,PP,PO,PR,AD,OE,PA,CA,PS";
            }

            var usrId = (Session["Usuario"] as sisUsuario).usrId;
            var _Datos = (from d in context.getListadoDeCompras(usrId, Estado, Anio).ToList()
                          select new proCompraWS()
                                    {
                                        comComprobante = d.comComprobante,
                                        comDescripcion = d.comDescripcion,
                                        comDestino = d.comDestino,
                                        comFecha = d.comFecha,
                                        comFechaApertura = d.comFechaApertura,
                                        comFechaIngreso = d.comFechaIngreso,
                                        comId = d.comId,
                                        comLugarDeEntrega = d.comLugarDeEntrega,
                                        TipoComprobante = new sisTipos() { tipoDescripcion = d.TipoComprobante, tipoId = d.tipoId },
                                        Estado = new sisTipos() { tipoDescripcion = d.Estado, tipoId = d.tipoIdEstado },
                                        perId = d.perId,
                                        Solicitante = new catPersonas() { perApellidoyNombre = d.Solicitante, perId = d.perId },
                                        tipoId = d.tipoId,
                                        tipoIdEncuadreLegal = d.tipoIdEncuadreLegal,
                                        EncuadreLegal = new sisTipos() { tipoDescripcion = d.EncuadreLegal },
                                        tipoIdEstado = d.tipoIdEstado,
                                        TieneDetalle = d.TieneDetalle == 1,
                                        EstadoImagen = d.EstadoImagen,
                                        CompraAnulada = d.CompraAnulada == 1,
                                        AutorizadoEncuadreLegal = d.AutorizadoEncuadreLegal == 1,
                                        ImporteEstimado = d.ImporteEstimado,
                                        expDiasCorridos = d.expDiasCorridos,
                                        expDiasHabiles = d.expDiasHabiles,
                                        expUbicacion = d.expUbicacion,
                                        IdExpediente = d.idExpediente,
                                        comHoraApertura = d.comHoraApertura,
                                        comResolucion = d.comResolucion,
                                        resId = d.resId,
                                        InfoResolucion = new proResoluciones(){ resLinkArchivo = d.resLinkArchivo },
                                        pagFecha = d.pagFecha,
                                        Pagado = d.pagFecha != null
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Compras";
            PopulateDropDownList();
            Session["DetalleCompra"] = new List<proCompraDetalleWS>();
            Session["PathArchivos"] = Server.MapPath("~/Content/Archivos/Importaciones");
            PopulateDropDownListproCompraDocumentacion(-1);

            return View();
        }

        private void Create(proCompra pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _tipoId = context.sisTipo.First(w => w.sisTipoEntidad.tipoeCodigo == "CM" && w.tipoCodigo == "SD").tipoId;

                    pItem.comFecha = DateTime.Now;
                    //pItem.comFechaIngreso = pItem.comFechaIngreso == null ? DateTime.Now;
                    pItem.tipoIdEstado = _tipoId;
                    pItem.tipoIdEncuadreLegal = null;
                    pItem.idExpediente = (Session["ExpedienteEncontrado"] as vwExpedientes).Id;
                    var UltimaUbicacion = context.getExpedienteUltimaUbicacion(pItem.idExpediente).FirstOrDefault();
                    pItem.expUbicacion = UltimaUbicacion == null ? "" : UltimaUbicacion.Ubicacion;
                    pItem.expDiasCorridos = UltimaUbicacion == null ? 0 : UltimaUbicacion.DiasCorridos;
                    pItem.expDiasHabiles = UltimaUbicacion == null ? 0 : UltimaUbicacion.DiasHabiles;
                    pItem.comHoraApertura = pItem.comHoraApertura ?? "";
                    if (pItem.comHoraApertura.Length > 0)
                    {
                        pItem.comHoraApertura = pItem.comHoraApertura.Substring(0, 5);
                    }

                    pItem.comResolucion = pItem.comResolucion ?? "";
                    if (pItem.comResolucion.Trim() == "")
                    {
                        pItem.resId = null;
                    }
                    else
                    {
                        int NumeroResolucion = int.Parse(pItem.comResolucion.Substring(0, 5));
                        int AnioResolucion = int.Parse(pItem.comResolucion.Substring(6));

                        pItem.resId = null;
                        var ResEncontrada = context.proResolucion.FirstOrDefault(w => w.resNumero == NumeroResolucion && w.resFecha.Value.Year == AnioResolucion);
                        if (ResEncontrada != null)
                        {
                            pItem.resId = ResEncontrada.resId;
                        }
                    }

                    context.proCompra.AddObject(pItem);
                    context.proCompraEstado.AddObject(new proCompraEstado() { comeFecha = DateTime.Now, comId = pItem.comId, tipoId = _tipoId, usrId = (Session["Usuario"] as sisUsuario).usrId, comeObservaciones = "" });

                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra", "Agregar", "Agrega compra");

                    context.SaveChanges();

                    Session["ExpedienteEncontrado"] = null;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("comDestino", ex.Message);
                }
            }

            return;
        }

        private void CreateProductos(proCompraDetalle pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _tipoId = context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "CM" && w.tipoCodigo == "CD").First().tipoId;
                    var ItemCompra = context.proCompra.Single(w => w.comId == pItem.comId);

                    pItem.comdActivo = true;
                    pItem.comdObservaciones = "";
                    pItem.comdDetallePresupuestado = "";
                    pItem.comdPrecioPresupuestado = 0;
                    ItemCompra.tipoIdEstado = _tipoId;

                    context.proCompraDetalle.AddObject(pItem);
                    context.proCompraEstado.AddObject(new proCompraEstado() { comeFecha = DateTime.Now, comId = pItem.comId, tipoId = _tipoId, usrId = (Session["Usuario"] as sisUsuario).usrId, comeObservaciones = ""});

                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra", "Agregar", "Agrega detalle de compra");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("comDetalle", ex.Message);
                }
            }

            return;
        }

        private void Edit(proCompra pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pItem.idExpediente != ((Session["ExpedienteEncontrado"] as vwExpedientes) == null ? pItem.idExpediente : (Session["ExpedienteEncontrado"] as vwExpedientes).Id))
                    {
                        var UltimaUbicacion = context.getExpedienteUltimaUbicacion((Session["ExpedienteEncontrado"] as vwExpedientes).Id).FirstOrDefault();
                        pItem.expUbicacion = UltimaUbicacion == null ? "" : UltimaUbicacion.Ubicacion;
                        pItem.expDiasCorridos = UltimaUbicacion == null ? 0 : UltimaUbicacion.DiasCorridos;
                        pItem.expDiasHabiles = UltimaUbicacion == null ? 0 : UltimaUbicacion.DiasHabiles;
                        pItem.idExpediente = (Session["ExpedienteEncontrado"] as vwExpedientes).Id;

                        Session["ExpedienteEncontrado"] = null;
                    }
                    pItem.comHoraApertura = pItem.comHoraApertura ?? "";
                    if (pItem.comHoraApertura.Length > 0)
                    {
                        pItem.comHoraApertura = pItem.comHoraApertura.Substring(0, 5);
                    }

                    pItem.comResolucion = pItem.comResolucion ?? "";
                    if (pItem.comResolucion.Trim() == "")
                    {
                        pItem.resId = null;
                    }
                    else
                    {
                        int NumeroResolucion = int.Parse(pItem.comResolucion.Substring(0, 5));
                        int AnioResolucion = int.Parse(pItem.comResolucion.Substring(6));

                        pItem.resId = null;
                        var ResEncontrada = context.proResolucion.FirstOrDefault(w => w.resNumero == NumeroResolucion && w.resFecha.Value.Year == AnioResolucion);
                        if (ResEncontrada != null)
                        {
                            pItem.resId = ResEncontrada.resId;
                        }
                    }


                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra", "Modificar", "Modifica compra");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("comDestino", ex.Message);
                }
            }
            return;
        }

        private void EditProductos(proCompraDetalle pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra", "Modificar", "Modifica detalle de compra");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("comDetalle", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id, bool pEstado)
        {
            try
            {
                var _Item = context.proCompra.Single(x => x.comId == id);
                var _tipoId = context.sisTipo.First(w => w.sisTipoEntidad.tipoeCodigo == "CM" && w.tipoCodigo == "AN").tipoId;

                using (var transaction = new TransactionScope())
                {
                    if (!pEstado)
                    {
                        //Se debe eliminar el registro fisicamente\\
                        context.procEliminarCompra(id);
                    }
                    else
                    {
                        _Item.tipoIdEstado = _tipoId;
                        context.proCompraEstado.AddObject(new proCompraEstado()
                        {
                            comeFecha = DateTime.Now,
                            comId = _Item.comId,
                            tipoId = _tipoId,
                            usrId = (Session["Usuario"] as sisUsuario).usrId,
                            comeObservaciones = ""
                        });
                    }

                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra",
                        "Eliminar", "Elimina compra");
                    context.SaveChanges();
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("comDestino", ex.Message);
            }
        }

        private int DeleteConfirmedProductos(int id, bool pEstado)
        {
            int _idCompra = 0;
            try
            {
                proCompraDetalle _Item = context.proCompraDetalle.Single(x => x.comdId == id);

                //_Item.comdActivo = pEstado;
                _idCompra = _Item.comId;
                context.proCompraDetalle.DeleteObject(_Item);
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra", "Eliminar", "Elimina un producto del detalle de compra");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("comDetalle", ex.Message);
            }

            return _idCompra;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CambiarEstado(int pIdCompra, string pEstadoNuevo, string pObservaciones)
        {
            try
            {
                proCompra _Item = context.proCompra.Single(x => x.comId == pIdCompra);
                var _tipoId = short.Parse(pEstadoNuevo);

                _Item.tipoIdEstado = _tipoId;
                context.proCompraEstado.AddObject(new proCompraEstado() { comeFecha = DateTime.Now, comId = _Item.comId, tipoId = _tipoId, usrId = (Session["Usuario"] as sisUsuario).usrId, comeObservaciones = pObservaciones });
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "proCompra", "Cambiar Estado", "Cambia el estado de la compra");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("comDetalle", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return Json(false);
            }

            return Json(true);
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Solicitantes = (from p in context.vwPersonaCompras.ToList()
                                 orderby p.perApellidoyNombre ascending
                                 select new catPersonas
                                  {
                                    perId = p.perId,
                                    perApellidoyNombre = p.perDNI + " - " + p.perApellidoyNombre
                                  }).ToList();

            var _TipoTramite = (from p in context.sisTipo
                                 where p.sisTipoEntidad.tipoeCodigo == "TT"
                                 select new sisTipos
                                 {
                                     tipoId = p.tipoId,
                                     tipoDescripcion = p.tipoDescripcion
                                 }).ToList();

            var _EstadoCompra = (from p in context.sisTipo
                                 where p.sisTipoEntidad.tipoeCodigo == "CM" && !("SD, CD, AN").Contains(p.tipoCodigo)
                                orderby p.tipoId
                                select new sisTipos
                                {
                                    tipoId = p.tipoId,
                                    tipoDescripcion = p.tipoDescripcion
                                }).ToList();

            var _EncuadreLegal = (from p in context.sisTipo
                                where p.sisTipoEntidad.tipoeCodigo == "EL"
                                select new sisTipos
                                {
                                    tipoId = p.tipoId,
                                    tipoDescripcion = p.tipoDescripcion
                                }).ToList();


            ViewData["perId_Data"] = new SelectList(_Solicitantes, "perId", "perApellidoyNombre");
            ViewData["tipoId_Data"] = new SelectList(_TipoTramite, "tipoId", "tipoDescripcion");
            ViewData["tipoIdEncuadreLegal_Data"] = new SelectList(_EncuadreLegal, "tipoId", "tipoDescripcion");
            ViewData["EstadosCompras"] = new SelectList(_EstadoCompra, "tipoId", "tipoDescripcion");
            ViewData["Anio_Data"] = getAnios();
        }

        public ActionResult Exportar(int page, string orderBy, string filter, int anio)
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = getDatos(filter, anio).AsQueryable().ToGridModel(page, 9999999, orderBy, string.Empty, filter);
            var _Datos = model.Data.Cast<proCompraWS>();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 10 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 10 * 256);
            sheet.SetColumnWidth(3, 10 * 256);
            sheet.SetColumnWidth(4, 13 * 256);
            sheet.SetColumnWidth(5, 13 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 20 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 20 * 256);
            sheet.SetColumnWidth(11, 20 * 256);
            sheet.SetColumnWidth(12, 20 * 256);
            sheet.SetColumnWidth(13, 20 * 256);
            sheet.SetColumnWidth(14, 20 * 256);

            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Estado");
            headerRow.CreateCell(1).SetCellValue("Tipo de Trámite");
            headerRow.CreateCell(2).SetCellValue("N° Trámite");
            headerRow.CreateCell(3).SetCellValue("Ubicación actual");
            headerRow.CreateCell(4).SetCellValue("Días corridos");
            headerRow.CreateCell(5).SetCellValue("Días hábiles");
            headerRow.CreateCell(6).SetCellValue("Encuadre Legal");
            headerRow.CreateCell(7).SetCellValue("Descripción");
            headerRow.CreateCell(8).SetCellValue("Solicitante");
            headerRow.CreateCell(9).SetCellValue("Fecha");
            headerRow.CreateCell(10).SetCellValue("Importe");
            headerRow.CreateCell(11).SetCellValue("Fecha Apertura");
            headerRow.CreateCell(12).SetCellValue("Hora Apertura");
            headerRow.CreateCell(13).SetCellValue("Destino");
            headerRow.CreateCell(14).SetCellValue("Lugar de Entrega");
            headerRow.CreateCell(15).SetCellValue("Resolución");
            sheet.CreateFreezePane(0, 1, 0, 1);
            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (proCompraWS _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells   == null ? "" : _Info.perFechaNacimiento.Value.ToString("dd/MM/yyyy")
                row.CreateCell(0).SetCellValue(_Info.Estado.tipoDescripcion);
                row.CreateCell(1).SetCellValue(_Info.TipoComprobante.tipoDescripcion);
                row.CreateCell(2).SetCellValue(_Info.comComprobante);
                row.CreateCell(3).SetCellValue(_Info.expUbicacion);
                row.CreateCell(4).SetCellValue(_Info.expDiasCorridos.ToString());
                row.CreateCell(5).SetCellValue(_Info.expDiasHabiles.ToString());
                row.CreateCell(6).SetCellValue(_Info.EncuadreLegal.tipoDescripcion);
                row.CreateCell(7).SetCellValue(_Info.comDescripcion);
                row.CreateCell(8).SetCellValue(_Info.Solicitante.perApellidoyNombre);
                row.CreateCell(9).SetCellValue(_Info.comFecha.ToString("dd/MM/yyyy"));
                row.CreateCell(10).SetCellValue((double)_Info.ImporteEstimado);
                row.CreateCell(11).SetCellValue(_Info.comFechaApertura == null ? "" :_Info.comFechaApertura.Value.ToString("dd/MM/yyyy"));
                row.CreateCell(12).SetCellValue(_Info.comHoraApertura);
                row.CreateCell(13).SetCellValue(_Info.comDestino);
                row.CreateCell(14).SetCellValue(_Info.comLugarDeEntrega);
                row.CreateCell(15).SetCellValue(_Info.comResolucion);

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "Compras-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }
    }
}