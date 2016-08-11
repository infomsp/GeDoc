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

    public class catCargoCategoriaDesignacionController : catCargoCategoriaController
    {
        public ActionResult IndexDesignados()
        {
            ViewBag.Catalogo = "Administración de Designaciones";
            ViewBag.OpenOnFocus = false;
            Session["EsDesignacion"] = true;
            ViewData["StyleComboBox"] = "width: 250px;";

            PopulateDropDownList(false);

            return PartialView();
        }
    }

    public partial class catCargoCategoriaController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditingDesignados()
        {
            return View(new GridModel(AllDesignados()));
        }

        public IList<catCargosCategoriasDesignados> AllDesignados()
        {
                return getDatosDesignados().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingDesignados(int id)
        {
            catCargoDesignacion _Item = context.catCargoDesignacion.FirstOrDefault(p => p.desigId == id);
            catCargosCategoriasDesignados _Item2 = new catCargosCategoriasDesignados();

            TryUpdateModel(_Item2);
            if (ValidaDatosDesignados(id, _Item, _Item2))
            {
                if (_Item.perId != _Item2.perId)
                {
                    if (_Item2.perId == null)
                    {
                        RegistrarLog("Modificar", "Modifica designación (Quita designación de: " + _Item.catPersona.perApellidoyNombre + ")");
                    }
                    else
                    {
                        if (_Item.perId == null)
                        {
                            RegistrarLog("Modificar", "Designa a " + context.catPersona.Single(p => p.perId == _Item2.perId).perApellidoyNombre + ")");
                        }
                        else
                        {
                            RegistrarLog("Modificar", "Modifica designación (" + _Item.catPersona.perApellidoyNombre + " <= por => " + context.catPersona.Single(p => p.perId == _Item2.perId).perApellidoyNombre + ")");
                        }
                    }
                }
                else if (_Item.desigVigenciaDesde.Value.Date != _Item2.desigVigenciaDesde.Value.Date)
                {
                    RegistrarLog("Modificar", "Modifica vigencia inicial (" + _Item.desigVigenciaDesde.Value.Date.ToString("dd/MM/yyyy") + " <= por => " + _Item2.desigVigenciaDesde.Value.Date.ToString("dd/MM/yyyy") + ")");
                }
                else if (_Item.desigVigenciaHasta != _Item2.desigVigenciaHasta)
                {
                    if (_Item.desigVigenciaHasta == null && _Item2.desigVigenciaHasta != null)
                    {
                        RegistrarLog("Modificar", "Baja del cargo (" + _Item.catPersona.perApellidoyNombre + ", Baja a partir de: " + _Item2.desigVigenciaHasta.Value.Date.ToString("dd/MM/yyyy") + ")");
                    }
                    else if (_Item.desigVigenciaHasta != null && _Item2.desigVigenciaHasta == null)
                    {
                        RegistrarLog("Modificar", "Borra fecha de baja del cargo (" + _Item.desigVigenciaHasta.Value.Date.ToString("dd/MM/yyyy") + ")");
                    }
                    else if (_Item.desigVigenciaHasta.Value.Date != _Item2.desigVigenciaHasta.Value.Date)
                    {
                        RegistrarLog("Modificar", "Modifica fecha de baja del cargo (" + _Item.desigVigenciaHasta.Value.Date.ToString("dd/MM/yyyy") + " <= por => " + _Item2.desigVigenciaHasta.Value.Date.ToString("dd/MM/yyyy") + ")");
                    }
                }
                else if (_Item.perIdSubRogancia != _Item2.perIdSubRogancia)
                {
                    if (_Item.perIdSubRogancia == null && _Item2.perIdSubRogancia != null)
                    {
                        RegistrarLog("Modificar", "Agrega SubRogancia (" + context.catPersona.Where(p => p.perId == _Item2.perIdSubRogancia).Single().perApellidoyNombre + ")");
                    }
                    else if (_Item.perIdSubRogancia != null && _Item2.perIdSubRogancia != _Item.perIdSubRogancia)
                    {
                        RegistrarLog("Modificar", "Cambia SubRogancia (" + _Item.catPersona.perApellidoyNombre + " <= por => " + context.catPersona.Where(p => p.perId == _Item2.perId).Single().perApellidoyNombre + ")");
                    }
                }
                TryUpdateModel(_Item);

                _Item.desigVigenciaDesde = _Item2.desigVigenciaDesde;
                _Item.desigVigenciaHasta = _Item2.desigVigenciaHasta;
                _Item.desigSubRoganciaDesde = _Item2.desigSubRoganciaDesde;
                _Item.desigSubRoganciaHasta = _Item2.desigSubRoganciaHasta;

                Edit(_Item);
            }

            if (Session["EsDesignacion"] != null)
            {
                return View(new GridModel(AllDesignados()));
            }
            return View(new GridModel(AllDesignados().Where(p => p.categId == _Item.categId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingDesignados()
        {
            catCargoDesignacion _Item = new catCargoDesignacion();
            catCargosCategoriasDesignados _Item2 = new catCargosCategoriasDesignados();

            TryUpdateModel(_Item2);
            if (TryUpdateModel(_Item))
            {
                if (ValidaDatosDesignados(-1, _Item, _Item2))
                {
                    Create(_Item);
                }
            }

            if (Session["EsDesignacion"] != null)
            {
                return View(new GridModel(AllDesignados()));
            }
            return View(new GridModel(AllDesignados().Where(p => p.categId == _Item.categId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingDesignados(int id)
        {
            var _Item = context.catCargoDesignacion.Where(e => e.desigId == id).First();

            DeleteConfirmedDesignados(id);

            if (Session["EsDesignacion"] != null)
            {
                return View(new GridModel(AllDesignados()));
            }
            return View(new GridModel(AllDesignados().Where(p => p.categId == _Item.categId)));
        }

        private bool ValidaDatosDesignados(int id, catCargoDesignacion _ItemOri, catCargosCategoriasDesignados _Item)
        {
            // Validamos Datos
            if (_Item.perId == _ItemOri.perId && _Item.tipoId == _ItemOri.tipoId)
            {
                return true;
            }

            bool _UnoSolo = false;
            _UnoSolo = context.catCargoDesignacion.Where(w => w.perId == _Item.perId && w.desigVigenciaDesde != null && w.desigVigenciaHasta == null && w.tipoId == _Item.tipoId).Count() >= 1;

            if (_UnoSolo)
            {
                ModelState.AddModelError("perId", "No puede ocupar más de un cargo.");
                return false;
            }

            if (_Item.perId == null && _Item.tipoId == 43)
            {
                ModelState.AddModelError("perId", "Ingrese el empleado.");
                return false;
            }

            int carEmpleados = context.catCargoDesignacion.Where(w => w.categId == _ItemOri.categId && w.desigVigenciaDesde != null && w.desigVigenciaHasta == null && w.sisTipo.tipoCodigo == "DE").Count();
            var _Presupuestado = context.catCargoCategoriaPresupuestado.Where(w => w.categId == _ItemOri.categId);
            int? carPresupuestado = _Presupuestado.Sum(s => s.presCantidad);
            if (carPresupuestado == null || carPresupuestado <= 0)
            {
                ModelState.AddModelError("categId", "Cargo sin presupuesto.");
                return false;
            }
            if (carPresupuestado <= carEmpleados)
            {
                ModelState.AddModelError("categId", "Cargo sin lugares disponibles.");
                return false;
            }

            return true;
        }

        private IEnumerable<catCargosCategoriasDesignados> getDatosDesignados()
        {
            var _Datos = (from d in context.getListadoDesignados()
                          select new catCargosCategoriasDesignados()
                                    {
                                        categId = d.categId,
                                        perId = d.perId,
                                        carDescripcion = d.carDescripcion,
                                        perIdSubRogancia = d.perIdSubRogancia,
                                        perNombre = d.perNombre,
                                        Sector = new catSectores() { secNombre = d.secNombre, ccCuentaContable = new catCuentasContables() { ccDescripcion = d.ccDescripcion, ccCodigo = d.ccCodigo, ccId = d.ccId } },
                                        perSubRoganciaNombre = d.perSubRoganciaNombre,
                                        desigId = d.desigId,
                                        desigSubRoganciaDesde = d.desigSubRoganciaDesde,
                                        desigSubRoganciaHasta = d.desigSubRoganciaHasta,
                                        desigVigenciaDesde = d.desigVigenciaDesde,
                                        desigVigenciaHasta = d.desigVigenciaHasta,
                                        tipoId = d.tipoId,
                                        desigTipo = d.desigTipo,
                                        desigObservaciones = d.desigObservaciones
                                    }).OrderBy(o => o.perNombre).ToList();

            return _Datos.AsEnumerable();
        }

        private void Create(catCargoDesignacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catCargoDesignacion.AddObject(pItem);
                    if (pItem.perId == null)
                    {
                        RegistrarLog("Agregar", "Reserva cargo (" + pItem.desigObservaciones + ")");
                    }
                    else
                    {
                        RegistrarLog("Agregar", "Agrega designación a (" + context.catPersona.Where(p => p.perId == pItem.perId).Single().perApellidoyNombre + ")");
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("perId", "No se pudo realizar la operación. Avise a división Informática.");
                }
            }

            return;
        }

        [GridAction]
        public ActionResult _BindingDesignados(short? idCategoria)
        {
            idCategoria = idCategoria == null ? 1 : idCategoria;

            if (Session["EsDesignacion"] != null)
            {
                return View(new GridModel(AllDesignados()));
            }
            return View(new GridModel<catCargosCategoriasDesignados>
            {
                Data = AllDesignados().Where(exp => exp.categId == idCategoria)
            });
        }

        private void Edit(catCargoDesignacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("perId", "No se pudo realizar la operación. Avise a división Informática.");
                }
            }
            return;
        }

        private void DeleteConfirmedDesignados(int id)
        {
            catCargoDesignacion _Item = context.catCargoDesignacion.Single(x => x.desigId == id);

            try
            {
                if (_Item.perId == null)
                {
                    RegistrarLog("Eliminar", "Elimina designación (" + _Item.desigObservaciones + ")");
                }
                else
                {
                    RegistrarLog("Eliminar", "Elimina designación de (" + _Item.catPersona.perApellidoyNombre + ")");
                }

                context.catCargoDesignacion.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("perId", "No se pudo realizar la operación. Avise a división Informática.");
            }
        }
    }
}