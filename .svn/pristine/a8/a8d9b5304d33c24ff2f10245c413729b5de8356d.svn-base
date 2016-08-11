using System.Data;

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
        //Edición de datos
        public IList<proCompraOfertaWS> AllOfertas(Int64? idCompra)
        {
            return getDatosOfertas(idCompra).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getCUITProveedor(string Proveedor)
        {
            string CUIT = "";
            string RazonSocial = Proveedor.Substring(0,Proveedor.IndexOf(" ("));
            var InfoProveedor = context.catProveedor.FirstOrDefault(w => w.provRazonSocial == RazonSocial);

            if (InfoProveedor != null)
            {
                CUIT = InfoProveedor.provCUIT;
            }

            return Json(new
            {
                CUIT = CUIT
            });
        }

        private IEnumerable<proCompraOfertaWS> getDatosOfertas(Int64? idCompra)
        {
            var _Datos = (from d in context.proCompraOferta.Where(w => w.comId == idCompra).ToList()
                          select new proCompraOfertaWS()
                                    {
                                        comId = d.comId,
                                        comoDiasValidezOferta = d.comoDiasValidezOferta,
                                        comoFechaPresupuesto = d.comoFechaPresupuesto,
                                        comoId = d.comoId,
                                        comoNumeroPresupuesto = d.comoNumeroPresupuesto,
                                        comoProveedorCUIT = d.comoProveedorCUIT,
                                        comoProveedorNombre = d.comoProveedorNombre,
                                        comoEsCompraSanJuan = d.comoEsCompraSanJuan,
                                        comoEsCompraSanJuanPorcentaje = d.comoEsCompraSanJuanPorcentaje,
                                        comoEsAlternativa = d.comoEsAlternativa
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult __SelectEditingProductosOferta()
        {
            return View(new GridModel<proCompraOfertaDetalleWS>
            {
                Data = (Session["DetalleDeOferta"] as List<proCompraOfertaDetalleWS>)
            });
        }

        [GridAction]
        public ActionResult _BindingOfertas(Int64? idCompra)
        {
            idCompra = idCompra ?? 1;

            PopulateDropDownListOfertas();

            return View(new GridModel<proCompraOfertaWS>
            {
                Data = AllOfertas(idCompra)
            });
        }

        [GridAction]
        public ActionResult Ofertas()
        {
            PopulateDropDownListOfertas();
            Session["DetalleDeOferta"] = new List<proCompraOfertaDetalleWS>();

            return PartialView("Ofertas");
        }

        [GridAction]
        public ActionResult getOferta(string Accion, Int64? comoId, Int64? idCompra)
        {
            var _Datos = new proCompraOfertaWS();
            _Datos.Accion = Accion;
            _Datos.comId = (int)idCompra;
            _Datos.comoId = (int)comoId;

            if(Accion == "Agregar")
            {
                _Datos.comoFechaPresupuesto = DateTime.Now;
                _Datos.DetalleOferta = (from x in context.proCompraDetalle.Where(w => w.comId == idCompra && w.comdActivo).ToList()
                                        select new proCompraOfertaDetalleWS()
                                            {
                                                comoId = x.comdId,
                                                codCantidad = x.comdCantidad,
                                                codDetalle = "",
                                                codObservaciones = "",
                                                codId = x.comdId,
                                                codPrecioUnitario = x.comdPrecioEstimado,
                                                comdId = x.comdId,
                                                DetalleOriginal = new proCompraDetalleWS(){ comdDetalle = x.comdDetalle },
                                                comoSubTotal = x.comdCantidad * x.comdPrecioEstimado
                                            }).ToList();
            }
            else
            {
                //Cargamos los datos de la oferta
                var DatosOferta = context.proCompraOferta.Single(w => w.comoId == comoId);

                _Datos.comoNumeroPresupuesto = DatosOferta.comoNumeroPresupuesto;
                _Datos.comoProveedorNombre = DatosOferta.comoProveedorNombre;
                _Datos.comoDiasValidezOferta = DatosOferta.comoDiasValidezOferta;
                _Datos.comoProveedorCUIT = DatosOferta.comoProveedorCUIT;
                _Datos.comoFechaPresupuesto = DatosOferta.comoFechaPresupuesto;
                _Datos.comoEsAlternativa = DatosOferta.comoEsAlternativa;
                _Datos.comoEsCompraSanJuan = DatosOferta.comoEsCompraSanJuan;
                _Datos.comoEsCompraSanJuanPorcentaje = DatosOferta.comoEsCompraSanJuanPorcentaje;

                _Datos.DetalleOferta = (from x in context.proCompraOfertaDetalle.Where(w => w.comoId == comoId).ToList()
                                        select new proCompraOfertaDetalleWS()
                                        {
                                            comoId = x.comoId,
                                            codCantidad = x.codCantidad,
                                            codDetalle = x.codDetalle,
                                            codObservaciones = x.codObservaciones,
                                            codId = x.codId,
                                            codPrecioUnitario = x.codPrecioUnitario,
                                            comdId = x.comdId,
                                            DetalleOriginal = new proCompraDetalleWS() { comdDetalle = x.proCompraDetalle.comdDetalle },
                                            comoSubTotal = x.codCantidad * x.codPrecioUnitario
                                        }).ToList();
            }

            Session["DetalleDeOferta"] = _Datos.DetalleOferta;

            PopulateDropDownListOfertas();

            return PartialView("CRUDOfertas", _Datos);
        }

        [GridAction]
        public ActionResult _SaveEditingProductosOferta(int id, proCompraOfertaDetalleWS Datos)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            var _Info =
                (Session["DetalleDeOferta"] as List<proCompraOfertaDetalleWS>).Single(w => w.codId == id);
            _Info.codDetalle = Datos.codDetalle;
            _Info.codObservaciones = Datos.codObservaciones;
            _Info.codPrecioUnitario = Datos.codPrecioUnitario;
            _Info.comoSubTotal = _Info.codCantidad*Datos.codPrecioUnitario;

            return View(new GridModel<proCompraOfertaDetalleWS>
            {
                Data = (Session["DetalleDeOferta"] as List<proCompraOfertaDetalleWS>)
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult _SaveEditingProductosOfertaImportados(int comId)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            try
            {
                var ArchivoImportado = (Session["ArchivoImportado"] as DataSet);
                for (int Fila = 0; Fila < ArchivoImportado.Tables[0].Rows.Count; Fila++)
                {
                    var Marca = ArchivoImportado.Tables[0].Rows[Fila]["Marca"].ToString();
                    var Detalle = ArchivoImportado.Tables[0].Rows[Fila]["Detalle"].ToString();
                    var Precio = Convert.ToDecimal(ArchivoImportado.Tables[0].Rows[Fila]["Precio"].ToString());
                    var Observaciones = ArchivoImportado.Tables[0].Rows[Fila]["Observaciones"].ToString();

                    var _Info = (Session["DetalleDeOferta"] as List<proCompraOfertaDetalleWS>).Single(w => w.DetalleOriginal.comdDetalle == Detalle);
                    _Info.codDetalle = Marca;
                    _Info.codObservaciones = Observaciones;
                    _Info.codPrecioUnitario = Precio;
                    _Info.comoSubTotal = _Info.codCantidad * Precio;
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });
        }

        //Datos para el dropdown
        public void PopulateDropDownListOfertas()
        {
            var _Proveedor = (from p in context.catProveedor.ToList()
                              orderby p.provRazonSocial
                              select new catProveedor()
                              {
                                  provId = p.provId,
                                  provRazonSocial = p.provRazonSocial + " (" + (p.provNombreDeFantasia ?? "") + ")",
                                  provCUIT = p.provCUIT
                              }).ToList();

            ViewData["comoProveedorNombre_Data"] = _Proveedor.Select(s => s.provRazonSocial);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setOferta(proCompraOfertaWS Datos)
        {
            if (Datos.Accion == "Modificar" || Datos.Accion == "Agregar")
            {
                if (Datos.comoDiasValidezOferta <= 10)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Los días de validez de la oferta son incorrectos.",
                            Foco = "comoDiasValidezOferta"
                        });
                }

                if (Datos.comoProveedorNombre == null)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar el Proveedor",
                            Foco = "comoProveedorNombre"
                        });
                }
                else
                {
                    Datos.comoProveedorNombre = Datos.comoProveedorNombre.Trim();
                    if (Datos.comoProveedorNombre.Trim().Length == 0)
                    {
                        return Json(new
                            {
                                Error = true,
                                Mensaje = "Debe ingresar el Proveedor",
                                Foco = "comoProveedorNombre"
                            });
                    }
                }

                if (!(Datos.comoFechaPresupuesto == null))
                {
                    if (Datos.comoFechaPresupuesto.Date < DateTime.Now.AddDays(-30).Date ||
                        Datos.comoFechaPresupuesto.Date > DateTime.Now.Date)
                    {
                        return Json(new
                            {
                                Error = true,
                                Mensaje = "Fecha de Presupuesto Incorrecta",
                                Foco = "comoFechaPresupuesto"
                            });
                    }
                }

                if (Datos.comoEsCompraSanJuan)
                {
                    if (Datos.comoEsCompraSanJuanPorcentaje <= 0)
                    {
                        return Json(new
                        {
                            Error = true,
                            Mensaje = "El porcentaje de Compra San Juan es incorrecto.",
                            Foco = "comoEsCompraSanJuanPorcentaje"
                        });
                    }
                }

                var OfertasAControlar = context.proCompraOferta.Where(w => w.comId == Datos.comId && w.comoId != Datos.comoId);
                if (OfertasAControlar.Any())
                {
                    if (OfertasAControlar.Any(w => w.comoNumeroPresupuesto == Datos.comoNumeroPresupuesto && w.comoProveedorCUIT != Datos.comoProveedorCUIT))
                    {
                        return Json(new
                        {
                            Error = true,
                            Mensaje = "El número de presupuesto ingresado ya existe.",
                            Foco = "comoNumeroPresupuesto"
                        });
                    }

                    var ExisteOferta = OfertasAControlar.Where(w =>
                            (w.comoProveedorCUIT == Datos.comoProveedorCUIT ||
                             w.comoProveedorNombre == Datos.comoProveedorNombre));

                    if (ExisteOferta.Count() > 1)
                    {
                        return Json(new
                        {
                            Error = true,
                            Mensaje = "No puede cargar más de dos ofertas por proveedor",
                            Foco = "comoProveedorCUIT"
                        });
                    }

                    if (ExisteOferta.Any())
                    {
                        if (ExisteOferta.First().comoEsAlternativa == Datos.comoEsAlternativa)
                        {
                            return Json(new
                            {
                                Error = true,
                                Mensaje = "Solo se acepta 1 Oferta Principal y 1 Alternativa, revise la información ingresada.",
                                Foco = "comoEsAlternativa"
                            });
                        }
                    }
                }
            }

            try
            {
                switch (Datos.Accion)
                {
                    case "Agregar":
                    case "Modificar":
                        var NewCompra = new proCompraOferta();
                        short? NumeroPresupuesto = 1;

                        Datos.comoProveedorNombre = Datos.comoProveedorNombre.Substring(0, Datos.comoProveedorNombre.IndexOf(" ("));

                        if (Datos.Accion == "Agregar")
                        {
                            var Ofertas = context.proCompraOferta.Where(w => w.comId == Datos.comId);

                            //if (Ofertas.Count(w => w.comoProveedorCUIT == Datos.comoProveedorCUIT || w.comoProveedorNombre == Datos.comoProveedorNombre) > 0)
                            //{
                            //    return Json(new
                            //    {
                            //        Error = true,
                            //        Mensaje = "No puede cargar más de una oferta el mismo proveedor",
                            //        Foco = "comoProveedorNombre"
                            //    });
                            //}

                            if (Ofertas.Count() > 0)
                            {
                                NumeroPresupuesto = Ofertas.Max(m => m.comoNumeroPresupuesto);
                                NumeroPresupuesto++;
                            }
                            NewCompra.comoNumeroPresupuesto = Datos.comoNumeroPresupuesto == 0 ? (NumeroPresupuesto ?? 1) : (short)Datos.comoNumeroPresupuesto;
                        }
                        else
                        {
                            NewCompra = context.proCompraOferta.Single(w => w.comoId == Datos.comoId);
                            NewCompra.comoNumeroPresupuesto = (short)Datos.comoNumeroPresupuesto;
                        }

                        NewCompra.comoDiasValidezOferta = (short)Datos.comoDiasValidezOferta;
                        NewCompra.comoFechaPresupuesto = Datos.comoFechaPresupuesto;
                        NewCompra.comoProveedorNombre = Datos.comoProveedorNombre;
                        NewCompra.comoProveedorCUIT = Datos.comoProveedorCUIT;
                        NewCompra.comId = Datos.comId;
                        NewCompra.comoEsAlternativa = Datos.comoEsAlternativa;
                        NewCompra.comoEsCompraSanJuan = Datos.comoEsCompraSanJuan;
                        NewCompra.comoEsCompraSanJuanPorcentaje = Datos.comoEsCompraSanJuanPorcentaje;

                        AdministraProveedor(Datos.comoProveedorNombre, Datos.comoProveedorCUIT);

                        foreach (var OfertaDetalle in (Session["DetalleDeOferta"] as List<proCompraOfertaDetalleWS>))
                        {
                            if (Datos.Accion == "Agregar")
                            {
                                NewCompra.proCompraOfertaDetalle.Add(new proCompraOfertaDetalle()
                                    {
                                        codCantidad = OfertaDetalle.codCantidad,
                                        comdId = OfertaDetalle.comdId,
                                        codDetalle = OfertaDetalle.codDetalle,
                                        codObservaciones = OfertaDetalle.codObservaciones ?? "",
                                        codPrecioUnitario = OfertaDetalle.codPrecioUnitario
                                    });
                            }
                            else
                            {
                                var ModificaDetalle = NewCompra.proCompraOfertaDetalle.Single(w => w.codId == OfertaDetalle.codId);
                                ModificaDetalle.codCantidad = OfertaDetalle.codCantidad;
                                ModificaDetalle.comdId = OfertaDetalle.comdId;
                                ModificaDetalle.codDetalle = OfertaDetalle.codDetalle;
                                ModificaDetalle.codObservaciones = OfertaDetalle.codObservaciones ?? "";
                                ModificaDetalle.codPrecioUnitario = OfertaDetalle.codPrecioUnitario;
                            }
                        }

                        if (Datos.Accion == "Agregar")
                        {
                            context.proCompraOferta.AddObject(NewCompra);
                        }
                        break;
                    case "Eliminar":
                        foreach (var RemoveDetalle in context.proCompraOfertaDetalle.Where(w => w.comoId == Datos.comoId))
                        {
                            //var DeleteDetalle = context.proCompraOfertaDetalle.Single(w => w.codId == RemoveDetalle.codId);
                            context.proCompraOfertaDetalle.DeleteObject(RemoveDetalle);
                        }

                        var DeleteCompra = context.proCompraOferta.Single(w => w.comoId == Datos.comoId);
                        context.proCompraOferta.DeleteObject(DeleteCompra);
                        break;
                }

                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizaron los datos de compra en forma correcta",
                    Foco = "comoFechaPresupuesto"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "comoFechaPresupuesto"
                });
            }
        }

        private void AdministraProveedor(string NombreProveedor, string CUIT)
        {
            if (context.catProveedor.Count(w => w.provRazonSocial == NombreProveedor) == 0)
            {
                context.catProveedor.AddObject(new catProveedor() { provRazonSocial = NombreProveedor, provCUIT = CUIT });
            }

        }

    }
}