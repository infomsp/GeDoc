namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class catGradosCategoriaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catGradosCategoriaWS> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catGradosCategoria _Item = context.catGradosCategoria.Where(p => p.gracId == id).FirstOrDefault();
            catGradosCategoriaWS _Item2 = new catGradosCategoriaWS();

            TryUpdateModel(_Item);
            TryUpdateModel(_Item2);
            RegistrarLog("Modificar", "Actualiza grado categoría (Código " + id.ToString() + ")");

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catGradosCategoria _Item = new catGradosCategoria();
            catGradosCategoriaWS _Item2 = new catGradosCategoriaWS();

            TryUpdateModel(_Item2);
            if (TryUpdateModel(_Item))
            {
                Create(_Item);
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

        private IEnumerable<catGradosCategoriaWS> getDatos()
        {
            var _Datos = (from d in context.catGradosCategoria.ToList()
                          orderby d.catGrados.catAgrupamientoGradosDeEscalafon.ageDescripcion ascending, d.catGrados.graDescripcion ascending, d.gracDescripcion ascending
                          select new catGradosCategoriaWS()
                                    {
                                        Agrupamiento = d.catGrados.catAgrupamientoGradosDeEscalafon.ageDescripcion,
                                        Grado =  d.catGrados.graDescripcion,
                                        gracDescripcion = d.gracDescripcion,
                                        gracId = d.gracId,
                                        graId = d.graId,
                                        gracEmpleados = d.catGradosDesignacion == null ? 0 : d.catGradosDesignacion.Count(c => c.gdFechaHasta == null),
                                        gracPresupuestado = d.catGradosPresupuestado == null ? 0 : d.catGradosPresupuestado.Sum(s => s.psCantidad),
                                        gracLibres = d.catGradosDesignacion == null || d.catGradosPresupuestado == null ? 0 : d.catGradosPresupuestado.Sum(s => s.psCantidad) - d.catGradosDesignacion.Count(c => c.gdFechaHasta == null)
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Administración de Cargos - Ley 71Q";
            ViewData["Presupuestados"] = new List<catGradosCategoriasPresupuestadosWS>();// AllPresupuestados().Where(exp => exp.categId == _categId);
            ViewData["GradosDesignados"] = new List<catGradosDesignacionWS>();
            ViewBag.OpenOnFocus = false;
            ViewData["StyleComboBox"] = "width: 250px;";
            Session["EsDesignacion"] = null;

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catGradosCategoria pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catGradosCategoria.AddObject(pItem);
                    RegistrarLog("Agregar", "Agrega Categoría " + pItem.gracDescripcion);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("gracDescripcion", "Ya existe la categoría que intenta cargar.");
                }
            }

            return;
        }

        private void Edit(catGradosCategoria pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("gracDescripcion", "Ya existe la categoría que intenta cargar.");
                }
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catGradosCategoria _Item = context.catGradosCategoria.Single(x => x.gracId == id);

            try
            {
                RegistrarLog("Eliminar", "Elimina categoría (Id =" + id.ToString() + ")");

                context.catGradosCategoria.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("gracDescripcion", "El cargo tiene empleados asignados, no se puede eliminar.");
            }
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Grado = (from d in context.catGrados.ToList()
                            select new catGradosWS()
                            {
                                graId = d.graId,
                                graDescripcion = d.catAgrupamientoGradosDeEscalafon.ageDescripcion + " - " + d.graDescripcion
                            }).ToList();

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

            var _Servicios = (from ser in context.catServicio.ToList()
                              orderby ser.serDescripcion
                              select new catServicio()
                              {
                                  serId = ser.serId,
                                  serDescripcion = ser.serDescripcion
                              }).ToList();

            ViewData["graId_Data"] = new SelectList(_Grado, "graId", "graDescripcion");
            ViewData["tipoIdTipo_Data"] = new SelectList(_TGra, "tipoId", "tipoDescripcion");
            ViewData["gracId_Data"] = new SelectList(_Gra, "gracId", "gracDescripcion");
            ViewData["gdServicio_Data"] = _Servicios.Select(s => s.serDescripcion);
            ViewData["icId_Data"] = new SelectList(context.catInstitucionContable.ToList(), "icId", "icDescripcion");

            ViewData["tipoId_Data"] = new SelectList(context.sisTipo.Where(t => t.sisTipoEntidad.tipoeCodigo == "TE"), "tipoId", "tipoDescripcion");

            //Para que sirva para la consulta
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
            ViewData["perIdSubRogancia_Data"] = new SelectList(_Perso, "perId", "perNombre");

        }

        public void RegistrarLog(string pAccion, string psMensaje)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catGradosCategoria", pAccion, psMensaje);
        }
    }
}