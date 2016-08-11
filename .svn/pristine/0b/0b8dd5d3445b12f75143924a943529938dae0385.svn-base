namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class catPersonaController : Controller
    {
        //Edición de datos
        public IList<catGradosDesignacionWS> AllGrados(Int64? idPersona)
        {
            return getDatosGrados(idPersona).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingGrados(int id)
        {
            var _Item = context.catGradosDesignacion.FirstOrDefault(p => p.gdId == id);
            var _Item2 = new catGradosDesignacionWS();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            _Item2.Resolucion = _Item2.Resolucion ?? "";
            if (_Item2.Resolucion.Trim() == "")
            {
                _Item.resId = null;
            }
            else
            {
                int NumeroResolucion = int.Parse(_Item2.Resolucion.Substring(0, 5));
                int AnioResolucion = int.Parse(_Item2.Resolucion.Substring(6));

                _Item.resId = context.proResolucion.Single(w => w.resNumero == NumeroResolucion && w.resFecha.Value.Year == AnioResolucion).resId;
            }

            _Item2.Decreto = _Item2.Decreto ?? "";
            _Item.gdFechaHasta = _Item2.gdFechaHasta;
            _Item.gdServicio = (_Item.gdServicio ?? "").ToUpper();

            if (_Item2.Decreto.Trim() == "")
            {
                _Item.decId = null;
            }
            else
            {
                int NumeroDecreto = int.Parse(_Item2.Decreto.Substring(0, 5));
                int AnioDecreto = int.Parse(_Item2.Decreto.Substring(6));

                _Item.decId = context.proDecreto.Single(w => w.decNumero == NumeroDecreto && w.decFecha.Value.Year == AnioDecreto).decId;
            }

            Edit(_Item);

            return View(new GridModel(AllGrados(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingGrados()
        {
            var _Item = new catGradosDesignacion();
            var _Item2 = new catGradosDesignacionWS();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                _Item.gdFecha = DateTime.Now;

                _Item2.Resolucion = _Item2.Resolucion ?? "";
                if (_Item2.Resolucion.Trim() == "")
                {
                    _Item.resId = null;
                }
                else
                {
                    int NumeroResolucion = int.Parse(_Item2.Resolucion.Substring(0, 5));
                    int AnioResolucion = int.Parse(_Item2.Resolucion.Substring(6));

                    _Item.resId = context.proResolucion.Single(w => w.resNumero == NumeroResolucion && w.resFecha.Value.Year == AnioResolucion).resId;
                }

                //_Item.gdFechaHasta = _Item2.gdFechaHasta;
                _Item.gdServicio = (_Item.gdServicio ?? "").ToUpper();

                _Item2.Decreto = _Item2.Decreto ?? "";
                if (_Item2.Decreto.Trim() == "")
                {
                    _Item.decId = null;
                }
                else
                {
                    int NumeroDecreto = int.Parse(_Item2.Decreto.Substring(0, 5));
                    int AnioDecreto = int.Parse(_Item2.Decreto.Substring(6));

                    _Item.decId = context.proDecreto.Single(w => w.decNumero == NumeroDecreto && w.decFecha.Value.Year == AnioDecreto).decId;
                }
                Create(_Item);
            }

            return View(new GridModel(AllGrados(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingGrados(int id)
        {
            var _Item = context.catGradosDesignacion.First(e => e.gdId == id);

            DeleteConfirmedGrados(id);

            return View(new GridModel(AllGrados(_Item.perId)));
        }

        private IEnumerable<catGradosDesignacionWS> getDatosGrados(Int64? idPersona)
        {
            var _Datos = (from d in context.catGradosDesignacion.Where(w => w.perId == idPersona).ToList()
                          select new catGradosDesignacionWS()
                                    {
                                        perId = d.perId,
                                        gdConAdicional = d.gdConAdicional,
                                        gdId = d.gdId,
                                        Categoria = d.catGradosCategoria.gracDescripcion,
                                        Grado = d.catGradosCategoria.catGrados.graDescripcion,
                                        Agrupamiento = d.catGradosCategoria.catGrados.catAgrupamientoGradosDeEscalafon.ageDescripcion,
                                        gdFecha = d.gdFecha,
                                        gdHoras = d.gdHoras,
                                        gdHorasAdicional = d.gdHorasAdicional,
                                        gracId = d.gracId,
                                        resId = d.resId,
                                        decId = d.decId,
                                        Decreto = d.decId == null ? "" : d.proDecreto.decNumero.Value.ToString("00000") + "-" + d.proDecreto.decFecha.Value.Year.ToString(),
                                        Resolucion = d.resId == null ? "" : d.proResolucion.resNumero.Value.ToString("00000") + "-" + d.proResolucion.resFecha.Value.Year.ToString(),
                                        InfoResolucion = d.resId == null ? new proResoluciones() { resFecha = DateTime.Now, resLinkArchivo = "", resNumero = 0 } : new proResoluciones() { resFecha = d.proResolucion.resFecha.Value, resLinkArchivo = d.proResolucion.resLinkArchivo, resNumero = d.proResolucion.resNumero },
                                        InfoDecreto = d.decId == null ? new proDecretos() { decFecha = DateTime.Now, decLinkArchivo = "", decNumero = 0 } : new proDecretos() { decFecha = d.proDecreto.decFecha.Value, decLinkArchivo = d.proDecreto.decLinkArchivo, decNumero = d.proDecreto.decNumero },
                                        tipoIdTipo = d.tipoIdTipo,
                                        TipoCargo = d.sisTipo.tipoDescripcion,
                                        gdObservaciones = d.gdObservaciones,
                                        gdServicio = d.gdServicio,
                                        gdFechaDesde = d.gdFechaDesde,
                                        gdFechaHasta = d.gdFechaHasta,
                                        gdFuncion = d.gdFuncion,
                                        gdNumeroRegistro = d.gdNumeroRegistro
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingGrados(Int64? idPersona)
        {
            idPersona = idPersona ?? 1;

            PopulateDropDownListGrados();

            return View(new GridModel<catGradosDesignacionWS>
            {
                Data = AllGrados(idPersona)
            });
        }

        [GridAction]
        public ActionResult _BindingGradosCategoria(Int64? idGrado)
        {
            idGrado = idGrado ?? 1;

            var _Datos = (from d in context.catGradosDesignacion.Where(w => w.gracId == idGrado).ToList()
                          select new catGradosDesignacionWS()
                          {
                              perId = d.perId,
                              gdConAdicional = d.gdConAdicional,
                              gdId = d.gdId,
                              Categoria = d.catGradosCategoria.gracDescripcion,
                              Grado = d.catGradosCategoria.catGrados.graDescripcion,
                              Agrupamiento = d.catGradosCategoria.catGrados.catAgrupamientoGradosDeEscalafon.ageDescripcion,
                              gdFecha = d.gdFecha,
                              gdHoras = d.gdHoras,
                              gdHorasAdicional = d.gdHorasAdicional,
                              gracId = d.gracId,
                              resId = d.resId,
                              decId = d.decId,
                              Decreto = d.decId == null ? "" : d.proDecreto.decNumero.Value.ToString("00000") + "-" + d.proDecreto.decFecha.Value.Year.ToString(),
                              Resolucion = d.resId == null ? "" : d.proResolucion.resNumero.Value.ToString("00000") + "-" + d.proResolucion.resFecha.Value.Year.ToString(),
                              InfoResolucion = d.resId == null ? new proResoluciones() { resFecha = DateTime.Now, resLinkArchivo = "", resNumero = 0 } : new proResoluciones() { resFecha = d.proResolucion.resFecha.Value, resLinkArchivo = d.proResolucion.resLinkArchivo, resNumero = d.proResolucion.resNumero },
                              InfoDecreto = d.decId == null ? new proDecretos() { decFecha = DateTime.Now, decLinkArchivo = "", decNumero = 0 } : new proDecretos() { decFecha = d.proDecreto.decFecha.Value, decLinkArchivo = d.proDecreto.decLinkArchivo, decNumero = d.proDecreto.decNumero },
                              InfoPersona = new catPersonas() { perApellidoyNombre = d.catPersona.perApellidoyNombre, perDNI = d.catPersona.perDNI },
                              tipoIdTipo = d.tipoIdTipo,
                              TipoCargo = d.sisTipo.tipoDescripcion,
                              gdObservaciones = d.gdObservaciones,
                              gdServicio = d.gdServicio,
                              gdFechaHasta = d.gdFechaHasta,
                              gdFechaDesde = d.gdFechaDesde
                          }).ToList();


            return View(new GridModel<catGradosDesignacionWS>
            {
                Data = _Datos
            });
        }

        [GridAction]
        public ActionResult Grados()
        {
            PopulateDropDownListGrados();

            return PartialView("Grados");
        }

        protected void CargarServicios()
        {
            var _Servicios = (from ser in context.catServicio.ToList()
                            orderby ser.serDescripcion
                            select new catServicio()
                            {
                                serId = ser.serId,
                                serDescripcion = ser.serDescripcion
                            }).ToList();

            ViewData["gdServicio_Data"] = _Servicios.Select(s => s.serDescripcion);
        }

        //Datos para el dropdown
        public void PopulateDropDownListGrados()
        {
            var _Gra = (from s in context.catGradosCategoria.ToList()
                        orderby s.catGrados.catAgrupamientoGradosDeEscalafon.ageDescripcion ascending, s.catGrados.graDescripcion ascending, s.gracDescripcion ascending
                        select new catGradosCategoria()
                        {
                            gracId = s.gracId,
                            gracDescripcion = s.catGrados.catAgrupamientoGradosDeEscalafon.ageDescripcion + " - " + s.catGrados.graDescripcion + " - " + s.gracDescripcion
                        }).ToList();

            var _TGra = (from s in context.sisTipo.ToList()
                         where s.sisTipoEntidad.tipoeCodigo == "TG"
                         select new sisTipo()
                         {
                             tipoId = s.tipoId,
                             tipoDescripcion = s.tipoDescripcion
                         }).ToList();

            ViewData["tipoIdTipo_Data"] = new SelectList(_TGra, "tipoId", "tipoDescripcion");
            ViewData["gracId_Data"] = new SelectList(_Gra, "gracId", "gracDescripcion");
            CargarServicios();

            return;
        }

        private void Create(catGradosDesignacion pItem)
        {
            if (ModelState.IsValid)
            {
                context.catGradosDesignacion.AddObject(pItem);
                var UpdatePersona = context.catPersona.Single(w => w.perId == pItem.perId);

                UpdatePersona.gdId = pItem.gdId;
                if (context.catServicio.Count(w => w.serDescripcion == pItem.gdServicio) == 0)
                {
                    context.catServicio.AddObject(new catServicio() { serDescripcion = pItem.gdServicio });
                }

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Agrega Designación a Empleado " + UpdatePersona.perApellidoyNombre);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catGradosDesignacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (context.catServicio.Count(w => w.serDescripcion == pItem.gdServicio) == 0)
                    {
                        context.catServicio.AddObject(new catServicio() { serDescripcion = pItem.gdServicio });
                    }

                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Modifica datos de designación ley 2580.");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedGrados(int id)
        {
            try
            {
                catGradosDesignacion _Item = context.catGradosDesignacion.Single(x => x.gdId == id);
                var UpdatePersona = context.catPersona.Single(w => w.perId == _Item.perId);

                UpdatePersona.gdId = null;
                context.catGradosDesignacion.DeleteObject(_Item);

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona",
                                                     "Modificar", "Elimina cargo ley 2580 al Empleado " + UpdatePersona.perApellidoyNombre);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getExisteResolucion(string Resolucion)
        {
            int Numero = int.Parse(Resolucion.Substring(0, 5));
            int Anio = int.Parse(Resolucion.Substring(6));
            bool _Existe = (context.proResolucion.Count(w => w.resNumero == Numero && w.resFecha.Value.Year == Anio) > 0);

            return Json(new
            {
                Existe = _Existe
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getExisteDecreto(string Decreto)
        {
            if (Decreto.Trim() == "")
            {
                return Json(new
                {
                    Existe = true
                });
            }

            int Numero = int.Parse(Decreto.Substring(0, 5));
            int Anio = int.Parse(Decreto.Substring(6));
            bool _Existe = (context.proDecreto.Count(w => w.decNumero == Numero && w.decFecha.Value.Year == Anio) > 0);

            return Json(new
            {
                Existe = _Existe
            });
        }

    }
}