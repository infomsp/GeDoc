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
        public IList<catPersonaNovedadWS> AllNovedades(Int64? idPersona)
        {
            return getDatosNovedades(idPersona).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingNovedades(long id)
        {
            var _Item = context.catPersonaNovedad.FirstOrDefault(p => p.novId == id);
            var _Item2 = new catPersonaNovedadWS();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            _Item2.Expediente = _Item2.Expediente ?? "";
            if (_Item2.Expediente.Trim() == "")
            {
                _Item.idExpediente = null;
                _Item.novExpediente = null;
            }
            else
            {
                int Prefijo = int.Parse(_Item2.Expediente.Substring(0, 3));
                int Sufijo = int.Parse(_Item2.Expediente.Substring(4, 5));
                int Ciclo = int.Parse(_Item2.Expediente.Substring(10));
                
                _Item.idExpediente = context.vwExpedientes.Single(w => w.prefijo == Prefijo && w.sufijo == Sufijo && w.ciclo == Ciclo).Id;
                _Item.novExpediente = _Item2.Expediente;
            }

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
            _Item.novDescripcion = (_Item.novDescripcion ?? "").ToUpper();

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

            return View(new GridModel(AllNovedades(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingNovedades()
        {
            var _Item = new catPersonaNovedad();
            var _Item2 = new catPersonaNovedadWS();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                _Item.novFecha = DateTime.Now;

                _Item2.Expediente = _Item2.Expediente ?? "";
                if (_Item2.Expediente.Trim() == "")
                {
                    _Item.idExpediente = null;
                    _Item.novExpediente = null;
                }
                else
                {
                    int Prefijo = int.Parse(_Item2.Expediente.Substring(0, 3));
                    int Sufijo = int.Parse(_Item2.Expediente.Substring(4, 5));
                    int Ciclo = int.Parse(_Item2.Expediente.Substring(10));

                    _Item.idExpediente = context.vwExpedientes.Single(w => w.prefijo == Prefijo && w.sufijo == Sufijo && w.ciclo == Ciclo).Id;
                    _Item.novExpediente = _Item2.Expediente;
                }

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

                _Item.novDescripcion = (_Item.novDescripcion ?? "").ToUpper();

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

            return View(new GridModel(AllNovedades(_Item.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingNovedades(long id)
        {
            var _Item = context.catPersonaNovedad.First(e => e.novId == id);

            DeleteConfirmedNovedades(id);

            return View(new GridModel(AllNovedades(_Item.perId)));
        }

        private IEnumerable<catPersonaNovedadWS> getDatosNovedades(Int64? idPersona)
        {
            var _Datos = (from d in context.getDatosNovedades(idPersona)
                          select new catPersonaNovedadWS()
                                    {
                                        perId = d.perId,
                                        resId = d.resId,
                                        decId = d.decId,
                                        Decreto = d.Decreto,
                                        Resolucion = d.Resolucion,
                                        InfoResolucion = d.resId == null ? new proResoluciones() { resFecha = DateTime.Now, resLinkArchivo = "", resNumero = 0 } : new proResoluciones() { resFecha = d.resFecha.Value, resLinkArchivo = d.resLinkArchivo, resNumero = d.resNumero },
                                        InfoDecreto = d.decId == null ? new proDecretos() { decFecha = DateTime.Now, decLinkArchivo = "", decNumero = 0 } : new proDecretos() { decFecha = d.decFecha.Value, decLinkArchivo = d.decLinkArchivo, decNumero = d.decNumero },
                                        novDescripcion = d.novDescripcion,
                                        novFecha = d.novFecha,
                                        novId = d.novId,
                                        idExpediente = d.idExpediente,
                                        Expediente = d.Expediente
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingNovedades(Int64? idPersona)
        {
            idPersona = idPersona ?? 1;

            return View(new GridModel<catPersonaNovedadWS>
            {
                Data = AllNovedades(idPersona)
            });
        }

        [GridAction]
        public ActionResult Novedades()
        {
            return PartialView("Novedades");
        }

        private void Create(catPersonaNovedad pItem)
        {
            if (ModelState.IsValid)
            {
                context.catPersonaNovedad.AddObject(pItem);

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Agrega Novedad a empleado");
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catPersonaNovedad pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Modifica Novedad de empleados.");

                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedNovedades(long id)
        {
            try
            {
                catPersonaNovedad _Item = context.catPersonaNovedad.Single(x => x.novId == id);
                context.catPersonaNovedad.DeleteObject(_Item);

                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catPersona", "Modificar", "Elimina novedad de un empleado");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

    }
}