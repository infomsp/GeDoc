namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using GeDoc.Filters;
    using GeDoc.Models;
    using System.IO;
    using NPOI.HSSF.UserModel;

    public partial class proImputacionController : Controller
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
        private double _CreditoAnual = 0;

        private SelectList getAnios()
        {
            List<Anios> _Anios = new List<Anios>();

            for (int _A = 2000; _A <= 2200; _A++)
            {
                _Anios.Add(new Anios() { Anio = _A });
            }

            return new SelectList(_Anios, "Anio", "Anio");
        }
        //Edición de datos
        //[GridAction]
        //public ActionResult _SelectEditing()
        //{
        //    return View(new GridModel(All()));
        //}
        //[HttpPost]
        [GridAction]
        public ActionResult _SelectEditing(int fuenteId, int cuentaId/*, int partidaId*/, int AnioId)
        {
            //object result;
            ViewBag.Credito = 0.00;
            if (ViewData["Anio_Data"] == null)
            {
                ViewData["Anio_Data"] = getAnios();
            }
            if (AnioId == 0)
            {
                AnioId = (int)Session["anioId"];
            }
            else
            {
                Session["anioId"] = AnioId;
            }
            if (fuenteId == 0 || cuentaId == 0 /*|| partidaId == 0*/)
            {
                if (ViewData["fteId_Data"] == null || ViewData["ccId_Data"] == null || ViewData["partId_Data"] == null)
                {
                    PopulateDropDownList();

                    fuenteId = Convert.ToInt32(((SelectList)ViewData["fteId_Data"]).First().Value);
                    cuentaId = Convert.ToInt32(((SelectList)ViewData["ccId_Data"]).First().Value);
                    //partidaId = Convert.ToInt32(((SelectList)ViewData["partId_Data"]).First().Value);
                }
            }
            Session["fuenteId"] = fuenteId;
            Session["cuentaId"] = cuentaId;
            //Session["partidaId"] = partidaId;

            //result = All();
            return View(new GridModel<proImputaciones>
            {
                Data = All()
            });
            //return new JsonResult
            //{
            //    Data = result
            //};
        }

        [GridAction]
        public ActionResult _BindingPieImputaciones(int fuenteId, int cuentaId, int AnioId)
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;

            if (ViewData["Anio_Data"] == null)
            {
                ViewData["Anio_Data"] = getAnios();
            }
            if (AnioId == 0)
            {
                AnioId = (int)Session["anioId"];
            }
            if (fuenteId == 0 || cuentaId == 0)
            {
                if (ViewData["fteId_Data"] == null || ViewData["ccId_Data"] == null || ViewData["partId_Data"] == null)
                {
                    PopulateDropDownList();

                    fuenteId = Convert.ToInt32(((SelectList)ViewData["fteId_Data"]).First().Value);
                    cuentaId = Convert.ToInt32(((SelectList)ViewData["ccId_Data"]).First().Value);
                }
            }

            var _Creditos = context.catCreditoAnual.Where(w => w.icId == icId).ToList().Where(p => p.fteId == fuenteId && p.ccId == cuentaId && p.creFecha.Date.Year == AnioId);
            //var _Acumulado = context.proImputacion.ToList().Where(p => p.fteId == fuenteId && p.ccId == cuentaId && p.impFecha.Date.Year == AnioId);
            var _Acumulado = (from p in context.proImputacion.Where(w => w.icId == icId)
                              join s in context.proImputacionDetalle on p.impId equals s.impId
                              where (p.fteId == fuenteId && p.ccId == cuentaId && p.impAnio == AnioId)
                              select s);


            List<PieImputaciones> _Datos = new List<PieImputaciones>();
            PieImputaciones _One = new PieImputaciones();
            if (_Creditos == null)
            {
                _One.Descripcion = "Crédito";
                _Datos.Add(_One);
                _One = new PieImputaciones();
                _One.Descripcion = "Acumulado";
                _Datos.Add(_One);
                _One = new PieImputaciones();
                _One.Descripcion = "Saldo";
                _Datos.Add(_One);
            }
            else
            {
                //Insertamos los Créditos
                _One.Descripcion = "Crédito";
                var _BC = _Creditos.Where(p => p.catPartida.partCodigo == "2");
                _One.ImporteBienesConsumo = _BC == null ? 0 : _BC.Sum(s => s.creImporte);
                var _SP = _Creditos.Where(p => p.catPartida.partCodigo == "3");
                _One.ImporteServiciosNoPersonales = _SP == null ? 0 : _SP.Sum(s => s.creImporte);
                var _BU = _Creditos.Where(p => p.catPartida.partCodigo == "4");
                _One.ImporteBienesDeUso = _BU == null ? 0 : _BU.Sum(s => s.creImporte);
                var _TR = _Creditos.Where(p => p.catPartida.partCodigo == "5");
                _One.ImporteTranferencias = _TR == null ? 0 : _TR.Sum(s => s.creImporte);
                var _GP = _Creditos.Where(p => p.catPartida.partCodigo == "1");
                _One.ImporteGastosEnPersonal = _GP == null ? 0 : _GP.Sum(s => s.creImporte);
                var _SD = _Creditos.Where(p => p.catPartida.partCodigo == "7");
                _One.ImporteSDDOP = _SD == null ? 0 : _SD.Sum(s => s.creImporte);

                _One.ImporteTotal = _Creditos.Sum(s => s.creImporte);
                _Datos.Add(_One);

                //Insertamos el Acumulado
                _One = new PieImputaciones();
                _One.Descripcion = "Acumulado";
                if (_Acumulado != null)
                {
                    var _BCA = _Acumulado.Where(p => p.catSubPartida.catPartida.partCodigo == "2");
                    _One.ImporteBienesConsumo = _BCA.Count() == 0 ? 0 : _BCA.Sum(s => s.impdImporte);
                    var _SPA = _Acumulado.Where(p => p.catSubPartida.catPartida.partCodigo == "3");
                    _One.ImporteServiciosNoPersonales = _SPA.Count() == 0 ? 0 : _SPA.Sum(s => s.impdImporte);
                    var _BUA = _Acumulado.Where(p => p.catSubPartida.catPartida.partCodigo == "4");
                    _One.ImporteBienesDeUso = _BUA.Count() == 0 ? 0 : _BUA.Sum(s => s.impdImporte);
                    var _TRA = _Acumulado.Where(p => p.catSubPartida.catPartida.partCodigo == "5");
                    _One.ImporteTranferencias = _TRA.Count() == 0 ? 0 : _TRA.Sum(s => s.impdImporte);
                    var _GPA = _Acumulado.Where(p => p.catSubPartida.catPartida.partCodigo == "1");
                    _One.ImporteGastosEnPersonal = _GPA.Count() == 0 ? 0 : _GPA.Sum(s => s.impdImporte);
                    var _SDA = _Acumulado.Where(p => p.catSubPartida.catPartida.partCodigo == "7");
                    _One.ImporteSDDOP = _SDA.Count() == 0 ? 0 : _SDA.Sum(s => s.impdImporte);

                    _One.ImporteTotal = _Acumulado.Count() == 0 ? 0 : _Acumulado.Sum(s => s.impdImporte);
                }
                _Datos.Add(_One);

                //Insertamos el Saldo
                _One = new PieImputaciones();
                _One.Descripcion = "Saldo";
                _One.ImporteBienesConsumo = _Datos[0].ImporteBienesConsumo - _Datos[1].ImporteBienesConsumo;
                _One.ImporteServiciosNoPersonales = _Datos[0].ImporteServiciosNoPersonales - _Datos[1].ImporteServiciosNoPersonales;
                _One.ImporteBienesDeUso = _Datos[0].ImporteBienesDeUso - _Datos[1].ImporteBienesDeUso;
                _One.ImporteTranferencias = _Datos[0].ImporteTranferencias - _Datos[1].ImporteTranferencias;
                _One.ImporteGastosEnPersonal = _Datos[0].ImporteGastosEnPersonal - _Datos[1].ImporteGastosEnPersonal;
                _One.ImporteSDDOP = _Datos[0].ImporteSDDOP - _Datos[1].ImporteSDDOP;
                _One.ImporteTotal = _Datos[0].ImporteTotal - _Datos[1].ImporteTotal;
                _Datos.Add(_One);
            }
            return View(new GridModel<PieImputaciones>
            {
                Data = _Datos
            });
        }

        private IList<proImputaciones> All()
        {
            return getDatos().ToList();
        }

        private proImputacion AsignaDatos(proImputaciones pItem)
        {
            proImputacion _Item2 = new proImputacion();
            if (pItem.impId != 0 && pItem.impId != null)
            {
                _Item2 = context.proImputacion.Where(p => p.impId == pItem.impId).FirstOrDefault();
            }

            _Item2.impFecha = pItem.impFecha;
            _Item2.impFechaImpresion = pItem.impFechaImpresion;
            _Item2.impId = pItem.impId;
            _Item2.impImporte = pItem.impImporte;
            _Item2.fteId = Convert.ToInt16(Session["fuenteId"]);
            _Item2.ccId = Convert.ToInt16(Session["cuentaId"]);
            _Item2.partId = null;// Convert.ToInt16(Session["partidaId"]);
            _Item2.fpId = pItem.fpId;
            _Item2.ceId = pItem.ceId;
            _Item2.impComprobante = pItem.impComprobante;
            _Item2.impDetalle = pItem.impDetalle;
            _Item2.tcoId = pItem.tcoId;
            _Item2.impFechaUltimoCambio = DateTime.Now;
            _Item2.usrId = (Session["Usuario"] as sisUsuario).usrId;
            _Item2.impuAnuladaParcialPor = pItem.impuAnuladaParcialPor;
            _Item2.impAnio = (int)Session["anioId"];
            _Item2.tgId = pItem.tgId;
            return _Item2;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            proImputaciones _Item = new proImputaciones();

            if (TryUpdateModel(_Item))
            {
                    _Item.impId = id;
                    Edit(AsignaDatos(_Item));
            }
            else
            {
                ModelState.AddModelError("MensajeError", "Los campos en rojo tienen errores. Son obligatorios o el valor ingresado es incorrecto.");
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            proImputaciones _Item = new proImputaciones();
            Session["UltimoId"] = -1;
            if (TryUpdateModel(_Item))
            {
                Create(AsignaDatos(_Item));
            }
            else
            {
                ModelState.AddModelError("MensajeError", "Los campos en rojo tienen errores. Son obligatorios o el valor ingresado es incorrecto.");
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCambioDeEstado(int idImp)
        {
            var _Item = context.proImputacion.Where(p => p.impId == idImp).FirstOrDefault();
            _Item.impEsCompromiso = !_Item.impEsCompromiso;
            context.SaveChanges();

            return Json(true);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }

        private IEnumerable<proImputaciones> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            if ((int)Session["fuenteId"] > 0)
            {
                var _Credito = context.catCreditoAnual.Where(w => w.icId == icId).ToList().Where(c => c.fteId == (int)Session["fuenteId"] && c.ccId == (int)Session["cuentaId"] && c.creFecha.Year == (int)Session["anioId"]).Sum(s => s.creImporte);
                if (_Credito != null)
                {
                    ViewBag.Credito = _Credito;
                    _CreditoAnual = Convert.ToDouble(_Credito);
                }
            }

            int FuenteId = (int) Session["fuenteId"];
            int CuentaId = (int)Session["cuentaId"];
            int AnioId = (int)Session["anioId"];

            var _Datos = (from d in context.getImputacionesPresupuestarias(FuenteId, CuentaId, AnioId, icId)
                          select new proImputaciones()
                          {
                              impFecha = d.impFecha,
                              impId = d.impId,
                              impImporte = d.impImporte,
                              fteId = d.fteId,
                              fteDescripcion = d.fteDescripcion,
                              ccId = d.ccId,
                              ccDescripcion = d.ccDescripcion,
                              partId = d.partId,
                              partNombre = d.partNombre,
                              tcoId = d.tcoId,
                              fpId = d.fpId,
                              fpDescripcion = d.fpDescripcion,
                              CEFP = d.CEFP,
                              impComprobante = d.impComprobante,
                              impDetalle = d.impDetalle,
                              tcoDescripcion = d.tcoDescripcion,
                              usrId = d.usrId,
                              impFechaUltimoCambio = d.impFechaUltimoCambio,
                              Acumulado = 0,
                              Saldo = 0,
                              Credito = _CreditoAnual,
                              ceDescripcion = d.ceDescripcion,
                              ceId = d.ceId,
                              impFechaImpresion = d.impFechaImpresion,
                              impEsCompromiso = d.impEsCompromiso,
                              BienesDeConsumo = (double)d.BienesDeConsumo,
                              BienesDeUso = (double)d.BienesDeUso,
                              ServiciosNoPersonales = (double)d.ServiciosNoPersonales,
                              GastosEnPersonal = (double)d.GastosEnPersonal,
                              Transferencias = (double)d.Transferencias,
                              impAnuladaPor = d.impAnuladaPor,
                              tgId = d.tgId,
                              tgDescripcion = d.tgDescripcion
                          }).ToList();

            var _Datos2 = (from d in _Datos
                          select new proImputaciones()
                          {
                              impFecha = d.impFecha,
                              impId = d.impId,
                              impImporte = d.impImporte,
                              fteId = d.fteId,
                              fteDescripcion = d.fteDescripcion,
                              ccId = d.ccId,
                              ccDescripcion = d.ccDescripcion,
                              partId = d.partId,
                              partNombre = d.partNombre,
                              tcoId = d.tcoId,
                              fpId = d.fpId,
                              fpDescripcion = d.fpDescripcion,
                              impComprobante = d.impComprobante,
                              impDetalle = d.impDetalle,
                              tcoDescripcion = d.tcoDescripcion,
                              usrId = d.usrId,
                              Credito = d.Credito,
                              ceDescripcion = d.ceDescripcion,
                              ceId = d.ceId,
                              CEFP = d.CEFP,
                              impEsCompromiso = d.impEsCompromiso,
                              impFechaImpresion = d.impFechaImpresion,
                              impFechaUltimoCambio = d.impFechaUltimoCambio,
                              Acumulado = (double)d.impImporte + (double)_Datos.Where(x => x.impFecha < d.impFecha).Sum(s => s.impImporte),
                              Saldo = d.Credito - (double)d.impImporte - (double)(_Datos.Where(x => x.impFecha < d.impFecha).Sum(s => s.impImporte)),
                              CreditoTexto = d.Credito.ToString("$ #,###.00"),
                              AcumuladoTexto = ((double)d.impImporte + (double)_Datos.Where(x => x.impFecha < d.impFecha).Sum(s => s.impImporte)).ToString("$ #,###.00"),
                              SaldoTexto = (d.Credito - (double)d.impImporte - (double)(_Datos.Where(x => x.impFecha < d.impFecha).Sum(s => s.impImporte))).ToString("$ #,###.00"),
                              UltimoId = Session["UltimoId"] != null ? Convert.ToInt64(Session["UltimoId"]) : d.impId,
                              BienesDeConsumo = d.BienesDeConsumo,
                              BienesDeUso = d.BienesDeUso,
                              ServiciosNoPersonales = d.ServiciosNoPersonales,
                              GastosEnPersonal = d.GastosEnPersonal,
                              Transferencias = d.Transferencias,
                              impAnuladaPor = d.impAnuladaPor,
                              tgId = d.tgId,
                              tgDescripcion = d.tgDescripcion
                          }).OrderBy(o => o.impFecha).ToList();

            return _Datos2.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Imputaciones Presupuestarias";
            ViewBag.Credito = 0.00;
            PopulateDropDownList();

            if (Request.RawUrl.Contains("/proImputacion/Index?ImputacionId="))
            {
                return View();
            }

            Session["fuenteId"] = 0;
            Session["cuentaId"] = 0;
            //Session["partidaId"] = 0;
            Session["anioId"] = DateTime.Now.Year;
            Session["UltimoId"] = -1;
            Session["DetalleImputaciones"] = new List<proImputacionesDetalles>();

            return PartialView();
        }

        private void Create(proImputacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                    context.proImputacion.AddObject(pItem);
                    if (SaveAllDetalle(pItem))
                    {
                        context.SaveChanges();
                        Session["UltimoId"] = pItem.impId;
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("MensajeError", ex.Message);
                }
            }

            return;
        }

        private void Edit(proImputacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (SaveAllDetalle(pItem))
                    {
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("MensajeError", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            proImputacion _Item = context.proImputacion.Single(x => x.impId == id);
            var _ItemDetalle = context.proImputacionDetalle.Where(x => x.impId == id);

            foreach (var _Delete in _ItemDetalle)
            {
                context.proImputacionDetalle.DeleteObject(_Delete);
            }
            if (_Item.impAnuladaPor != null)
            {
                proImputacion _ItemAnulado = context.proImputacion.Single(x => x.impId == _Item.impAnuladaPor);
                _ItemAnulado.impAnuladaPor = null;
            }
            context.proImputacion.DeleteObject(_Item);
            context.SaveChanges();
        }

        private bool SaveAllDetalle(proImputacion _Imputacion)
        {
            try
            {
                bool _ErrorSuma = false;

                if (((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Count == 0)
                {
                    _ErrorSuma = true;
                }
                else
                {
                    var _Importe = ((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Where(p => p.impId == _Imputacion.impId).Sum(p => p.impdImporte);
                    _ErrorSuma = _Importe != _Imputacion.impImporte;
                }
                if (_Imputacion.impId == 0)
                {
                    var _Importe = ((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Sum(p => p.impdImporte);
                    _ErrorSuma = _Importe != _Imputacion.impImporte;
                }

                if (_ErrorSuma)
                {
                    ModelState.AddModelError("impImporte", "       *");
                    ModelState.AddModelError("MensajeError", "Total de Subpartida distinto al Total Imputado");
                    return false;
                }

                var _DeleteDetalle = context.proImputacionDetalle.Where(p => p.impId == _Imputacion.impId).ToList();
                foreach (var _Item in ((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Where(p => p.impId == _Imputacion.impId))
                {
                    proImputacionDetalle _Registro = new proImputacionDetalle();
                    if (_Item.impdId > 0)
                    {
                        //Actualizo Registro.
                        _Registro = context.proImputacionDetalle.Single(p => p.impdId == _Item.impdId);
                        _Registro.impdImporte = _Item.impdImporte;
                        _Registro.subpId = _Item.subpId;

                        //Elimino de la lista de eliminados.
                        var _DeleteOne = _DeleteDetalle.Where(p => p.impdId == _Item.impdId).Single();
                        _DeleteDetalle.Remove(_DeleteOne);
                    }
                    else
                    {
                        //Si el item es una imp anulada
                        if (_Imputacion.impuAnuladaParcialPor > 0)
                        {
                            //las subpartidas mayores a 0 se guardan en 0
                            if (_Item.impdImporte > 0)
                            {
                                _Registro.impdImporte = 0;
                            }
                            else
                            {
                                _Registro.impdImporte = _Item.impdImporte;
                            }
                        }
                        else
                        {
                            //Registro Nuevo.
                            _Registro.impdImporte = _Item.impdImporte;
                           
                        }
                        _Registro.subpId = _Item.subpId;
                        _Registro.impId = _Imputacion.impId;
                        context.proImputacionDetalle.AddObject(_Registro);
                    }
                }
                if (_DeleteDetalle.Count > 0)
                {
                    foreach (var _dOne in _DeleteDetalle)
                    {
                        context.proImputacionDetalle.DeleteObject(_dOne);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("MensajeError", ex.Message);
                return false;
            }

            return true;
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            if (ViewData["fteId_Data"] != null && ViewData["ccId_Data"] != null && ViewData["partId_Data"] != null && ViewData["tcoId_Data"] != null && ViewData["fpId_Data"] != null /*&& ViewData["Anio_Data"] != null*/)
            {
                return;
            }

            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Fuentes = (from p in context.catFuente.Where(w => w.icId == icId).ToList()
                            select new catFuente
                            {
                                fteId = p.fteId,
                                fteDescripcion = p.fteCodigo.ToString() + "-" + p.fteDescripcion
                            }).ToList().OrderBy(p => p.fteDescripcion);

            var _Cuentas = (from p in context.catCuentaContable.Where(w => w.icId == icId).ToList()
                            select new catCuentaContable
                            {
                                ccId = p.ccId,
                                ccDescripcion = p.ccCodigo.ToString() + "-" + p.ccDescripcion
                            }).ToList().OrderBy(p => p.ccDescripcion);

            var _Fondos = (from p in context.catFondoPermanente.Where(w => w.icId == icId).ToList()
                           select new catFondoPermanente
                            {
                                fpId = p.fpId,
                                fpDescripcion = p.fpNumeroCuenta.ToString() + "-" + p.fpDescripcion
                            }).ToList().OrderBy(p => p.fpDescripcion);

            var _CtasEs = (from p in context.catCuentaEscritural.Where(w => w.icId == icId).ToList()
                           select new catCuentaEscritural
                           {
                               ceId = p.ceId,
                               ceDescripcion = p.ceCodigo.ToString() + "-" + p.ceDescripcion
                           }).ToList().OrderBy(p => p.ceDescripcion);

            var _SubPartidas = (from s in context.catSubPartida.Where(w => w.icId == icId).ToList()
                                select new catSubPartida
                                {
                                    subpCodigo = s.subpCodigo,
                                    subpId = s.subpId,
                                    subpNombre = s.subpCodigo + " - " + s.subpNombre
                                }).ToList().OrderBy(o => o.subpCodigo);

            ViewData["fteId_Data"] = new SelectList(_Fuentes, "fteId", "fteDescripcion");
            ViewData["ccId_Data"] = new SelectList(_Cuentas, "ccId", "ccDescripcion");
            //ViewData["partId_Data"] = new SelectList(context.catPartida, "partId", "partNombre");
            ViewData["tcoId_Data"] = new SelectList(context.catTipoDeComprobante, "tcoId", "tcoDescripcion");
            ViewData["fpId_Data"] = new SelectList(_Fondos, "fpId", "fpDescripcion");
            ViewData["Anio_Data"] = getAnios();
            ViewData["ceId_Data"] = new SelectList(_CtasEs, "ceId", "ceDescripcion");
            ViewData["SubPartidas.subpId_Data"] = new SelectList(_SubPartidas, "subpId", "subpNombre");
            ViewData["tgId_Data"] = new SelectList(context.catTipoGasto.Where(w => w.icId == icId), "tgId", "tgDescripcion");
        }

        public ActionResult Exportar(int page, string orderBy, string filter)
        {
            //Get the data representing the current grid state - page, sort and filter
            GridModel model = getDatos().AsQueryable().ToGridModel(page, 9999999, orderBy, string.Empty, filter);
            var _Datos = model.Data.Cast<proImputaciones>();

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 15 * 256);
            sheet.SetColumnWidth(3, 15 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 10 * 256);
            sheet.SetColumnWidth(6, 15 * 256);
            sheet.SetColumnWidth(7, 15 * 256);
            sheet.SetColumnWidth(8, 15 * 256);
            sheet.SetColumnWidth(9, 15 * 256);
            sheet.SetColumnWidth(10, 15 * 256);
            sheet.SetColumnWidth(11, 15 * 256);
            sheet.SetColumnWidth(12, 15 * 256);

            var headerRow1 = sheet.CreateRow(0);
            headerRow1.CreateCell(0).SetCellValue("Fuente:");
            headerRow1.CreateCell(1).SetCellValue(_Datos.First().fteDescripcion);
            headerRow1 = sheet.CreateRow(1);
            headerRow1.CreateCell(0).SetCellValue("Cuenta:");
            headerRow1.CreateCell(1).SetCellValue(_Datos.First().ccDescripcion);
            //Create a header row
            var headerRow = sheet.CreateRow(2);
            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("Estado");
            headerRow.CreateCell(1).SetCellValue("Detalle");
            headerRow.CreateCell(2).SetCellValue("Tipo de Comprobante");
            headerRow.CreateCell(3).SetCellValue("Número de Comprobante");
            headerRow.CreateCell(4).SetCellValue("FP/CE");
            headerRow.CreateCell(5).SetCellValue("Fecha de Imputación");
            headerRow.CreateCell(6).SetCellValue("Bienes de Consumo");
            headerRow.CreateCell(7).SetCellValue("Servicios No Personales");
            headerRow.CreateCell(8).SetCellValue("Bienes de Uso");
            headerRow.CreateCell(9).SetCellValue("Transferencias");
            headerRow.CreateCell(10).SetCellValue("Gastos en Personal");
            headerRow.CreateCell(11).SetCellValue("Total Imputado");
            headerRow.CreateCell(12).SetCellValue("Tipo de Gasto");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 3, 0, 3);

            int rowNumber = 3;

            //Populate the sheet with values from the grid data
            foreach (proImputaciones _Info in _Datos)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber);

                //Set values for the cells
                row.CreateCell(0).SetCellValue(_Info.impAnuladaPor != null ? "Anulada" : (_Info.impEsCompromiso ? "Compromiso" : "Preventiva"));
                row.CreateCell(1).SetCellValue(_Info.impDetalle);
                row.CreateCell(2).SetCellValue(_Info.tcoDescripcion);
                row.CreateCell(3).SetCellValue(_Info.impComprobante);
                row.CreateCell(4).SetCellValue(_Info.fpDescripcion == "" ? _Info.ceDescripcion : _Info.fpDescripcion);
                row.CreateCell(5).SetCellValue(_Info.impFecha.Date.ToString("dd/MM/yyyy"));
                row.CreateCell(6).SetCellValue(_Info.BienesDeConsumo);
                row.CreateCell(7).SetCellValue(_Info.ServiciosNoPersonales);
                row.CreateCell(8).SetCellValue(_Info.BienesDeUso);
                row.CreateCell(9).SetCellValue(_Info.Transferencias);
                row.CreateCell(10).SetCellValue(_Info.GastosEnPersonal);
                row.CreateCell(11).SetCellValue((double)_Info.impImporte);
                row.CreateCell(12).SetCellValue(_Info.tgDescripcion);

                rowNumber++;
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            string _NombreArchivo = "ImputacionesPresupuestarias-" + DateTime.Now.Date.ToString("yyyyMMddhhmmss") + ".xls";
            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                _NombreArchivo);     //Suggested file name in the "Save as" dialog which will be displayed to the end user
        }

        public ActionResult Copiar(int pImpId)
        {
            try
            {
                var _NewCabecera = new proImputacion();
                var _OldCabecera = context.proImputacion.First(w => w.impId == pImpId);

                _NewCabecera.ccId = _OldCabecera.ccId;
                _NewCabecera.ceId = _OldCabecera.ceId;
                _NewCabecera.fpId = _OldCabecera.fpId;
                _NewCabecera.fteId = _OldCabecera.fteId;
                _NewCabecera.impComprobante = _OldCabecera.impComprobante;
                _NewCabecera.impDetalle = _OldCabecera.impDetalle;
                _NewCabecera.impEsCompromiso = _OldCabecera.impEsCompromiso;
                _NewCabecera.impFecha = DateTime.Now;
                _NewCabecera.impFechaImpresion = DateTime.Now;
                _NewCabecera.impFechaUltimoCambio = DateTime.Now;
                _NewCabecera.impImporte = (_OldCabecera.impImporte * (-1));
                _NewCabecera.partId = _OldCabecera.partId;
                _NewCabecera.tcoId = _OldCabecera.tcoId;
                _NewCabecera.usrId = _OldCabecera.usrId;
                _NewCabecera.impAnuladaPor = pImpId;
                _NewCabecera.impAnio = _OldCabecera.impAnio;
                _NewCabecera.icId = _OldCabecera.icId;

                foreach (var _Item in context.proImputacionDetalle.Where(w => w.impId == pImpId).ToList())
                {
                    var _NewDet = new proImputacionDetalle();

                    _NewDet.subpId = _Item.subpId;
                    _NewDet.impdImporte = _Item.impdImporte * (-1);
                    _NewCabecera.proImputacionDetalle.Add(_NewDet);
                }

                context.proImputacion.AddObject(_NewCabecera);
                _OldCabecera.impAnuladaPor = _NewCabecera.impId;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CopiarParcial(int pImpId)
        {
                var _NewCabecera = new proImputacion();
                var _OldCabecera = context.proImputacion.Where(w => w.impId == pImpId).First();
                _NewCabecera.ccId = _OldCabecera.ccId;
                _NewCabecera.ceId = _OldCabecera.ceId;
                _NewCabecera.fpId = _OldCabecera.fpId;
                _NewCabecera.fteId = _OldCabecera.fteId;
                _NewCabecera.impComprobante = _OldCabecera.impComprobante;
                _NewCabecera.impDetalle = _OldCabecera.impDetalle;
                _NewCabecera.impEsCompromiso = _OldCabecera.impEsCompromiso;
                _NewCabecera.impFecha = DateTime.Now;
                _NewCabecera.impFechaImpresion = DateTime.Now;
                _NewCabecera.impFechaUltimoCambio = DateTime.Now;
                _NewCabecera.impImporte = _OldCabecera.impImporte;
                _NewCabecera.partId = _OldCabecera.partId;
                _NewCabecera.tcoId = _OldCabecera.tcoId;
                _NewCabecera.usrId = _OldCabecera.usrId;
                _NewCabecera.impAnuladaPor = pImpId;
                _NewCabecera.impAnio = _OldCabecera.impAnio;
                _NewCabecera.icId = _OldCabecera.icId;
                //_NewCabecera. = d.fpId == null ? d.catCuentaEscritural.ceCodigo + "-" + d.catCuentaEscritural.ceDescripcion : d.catFondoPermanente.fpNumeroCuenta + "-" + d.catFondoPermanente.fpDescripcion;
                var _item = _NewCabecera;
                return Json(_item);           
        }
    }
}