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

    public partial class catCargoCategoriaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCargosCategorias> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catCargoCategoria _Item = context.catCargoCategoria.Where(p => p.categId == id).FirstOrDefault();
            catCargosCategorias _Item2 = new catCargosCategorias();

            TryUpdateModel(_Item);
            TryUpdateModel(_Item2);
            if (ValidaDatos(id, _Item2))
            {
                RegistrarLog("Modificar", "Actualiza categoría (Código " + id.ToString() + ")");

                Edit(_Item);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCargoCategoria _Item = new catCargoCategoria();
            catCargosCategorias _Item2 = new catCargosCategorias();

            TryUpdateModel(_Item2);
            if (ValidaDatos(-1, _Item2))
            {
                if (TryUpdateModel(_Item))
                {
                    Create(_Item);
                }
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }

        private bool ValidaDatos(int id, catCargosCategorias _Item)
        {
            // Validamos Datos
            var _RangoIncorrecto = context.catCargoCategoria.Where(w => w.categId != id && w.categNumero == _Item.categNumero && w.carId == _Item.carId && ((_Item.categAntiguedadHasta >= w.categAntiguedadDesde && _Item.categAntiguedadHasta <= w.categAntiguedadHasta) || (_Item.categAntiguedadDesde >= w.categAntiguedadDesde && _Item.categAntiguedadDesde <= w.categAntiguedadHasta))).Count() > 0;

            if (_RangoIncorrecto)
            {
                ModelState.AddModelError("categAntiguedadHasta", "Rango de antigüedades Incorrecto.");
            }

            return true;
        }

        private IEnumerable<catCargosCategorias> getDatos()
        {
            var _Datos = (from d in context.catCargoCategoria.ToList()
                          select new catCargosCategorias()
                                    {
                                        carDescripcion = d.catCargo.catAgrupamiento.agrCodigo + ") " + d.catCargo.carDescripcion,
                                        carId = d.carId,
                                        categAntiguedadDesde = d.categAntiguedadDesde,
                                        categAntiguedadHasta = d.categAntiguedadHasta,
                                        categHoras = d.categHoras,
                                        categId = d.categId,
                                        categNumero = d.categNumero,
                                        carEmpleados = d.catCargoDesignacion == null ? 0 : d.catCargoDesignacion.Where(w => w.sisTipo.tipoCodigo == "DE" && w.desigVigenciaHasta == null).Count(),
                                        carPresupuestado = d.catCargoCategoriaPresupuestado == null ? 0 : d.catCargoCategoriaPresupuestado.Sum(s => s.presCantidad),
                                        carLibres = d.catCargoDesignacion == null || d.catCargoCategoriaPresupuestado == null ? 0 : d.catCargoCategoriaPresupuestado.Sum(s => s.presCantidad) - d.catCargoDesignacion.Where(w => w.sisTipo.tipoCodigo == "DE" && w.desigVigenciaHasta == null).Count()
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            var _Categoria = All();
            int _categId = -1;
            if (_Categoria.Count > 0)
            {
                _categId = _Categoria.First().categId;
            }
            ViewBag.Catalogo = "Administración de Cargos";
            ViewData["Presupuestados"] = AllPresupuestados().Where(exp => exp.categId == _categId);
            ViewData["Designados"] = AllDesignados().Where(exp => exp.categId == _categId);
            ViewBag.OpenOnFocus = false;
            ViewData["StyleComboBox"] = "width: 250px;";
            Session["EsDesignacion"] = null;

            PopulateDropDownList(true);

            return PartialView();
        }

        private void Create(catCargoCategoria pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catCargoCategoria.AddObject(pItem);
                    RegistrarLog("Agregar", "Agrega Categoría (Id = " + pItem.categNumero + ")");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("categNumero", "Ya existe la categoría que intenta cargar.");
                }
            }

            return;
        }

        private void Edit(catCargoCategoria pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("categNumero", "Ya existe la categoría que intenta cargar.");
                }
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catCargoCategoria _Item = context.catCargoCategoria.Single(x => x.categId == id);

            try
            {
                RegistrarLog("Eliminar", "Elimina categoría (Id =" + id.ToString() + ")");

                context.catCargoCategoria.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("categNumero", "El cargo tiene empleados asignados, no se puede eliminar.");
            }
        }

        //Datos para el dropdown
        public void PopulateDropDownList(bool pEsCategoria)
        {
            var _Cargo = (from d in context.catCargo.ToList()
                            select new catCargo()
                            {
                                carId = d.carId,
                                carDescripcion = d.catAgrupamiento.agrCodigo + ") " + d.carDescripcion
                            }).ToList();

            var _Perso = (from d in context.getListaDesplegablePersonas()
                          select new catCargosCategoriasDesignados()
                          {
                              perId = d.perId,
                              perNombre = d.perApellidoyNombre
                          }).ToList();

            var _Agrupamiento = (from d in context.catAgrupamiento.ToList()
                                 select new catAgrupamiento()
                                 {
                                     agrId = d.agrId,
                                     agrDescripcion = d.agrCodigo + ") " + d.agrDescripcion
                                 }).ToList();

            var _Categorias = (from d in context.catCargoCategoria.ToList()
                                 select new catCargoCategoria()
                                 {
                                     categId = d.categId,
                                     categNumero = d.categNumero
                                 }).ToList();

            ViewData["agrId_Data"] = new SelectList(_Agrupamiento, "agrId", "agrDescripcion");
            ViewData["carId_Data"] = new SelectList(_Cargo, "carId", "carDescripcion");
            ViewData["perId_Data"] = new SelectList(_Perso, "perId", "perNombre");
            ViewData["categId_Data"] = new SelectList(_Categorias, "categId", "categNumero");
            ViewData["repId_Data"] = new SelectList(context.catReparticion, "repId", "repNombre");
            ViewData["secId_Data"] = new SelectList(context.catSector, "secId", "secNombre");
            //ViewData["perIdSubRogancia_Data"] = new SelectList(_Subro, "perIdSubRogancia", "perSubRoganciaNombre");
            ViewData["perIdSubRogancia_Data"] = new SelectList(_Perso, "perId", "perNombre");
            ViewData["tipoId_Data"] = new SelectList(context.sisTipo.Where(t => t.sisTipoEntidad.tipoeCodigo == "TE"), "tipoId", "tipoDescripcion");

            if (!pEsCategoria)
            {
                var _CargoCateg = (from d in context.catCargoCategoria.ToList()
                                   join x in context.catCargo.ToList() on d.carId equals x.carId
                                  select new catCargosCategorias()
                                  {
                                      categId = d.categId,
                                      carDescripcion = d.catCargo.catAgrupamiento.agrCodigo + ") " + d.catCargo.carDescripcion + " (Categoría " + d.categNumero + "-" + d.categHoras.ToString() + ")"
                                  }).ToList();
                ViewData["categId_Data"] = new SelectList(_CargoCateg, "categId", "carDescripcion");
            }

            return;
        }

        public void RegistrarLog(string pAccion, string psMensaje)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catCargoCategoria", pAccion, psMensaje);

            return;
        }
    }
}