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
        [GridAction]
        public ActionResult _SelectEditingPresupuestado()
        {
            return View(new GridModel(AllPresupuestados()));
        }

        private IList<catCargosCategoriasPresupuestados> AllPresupuestados()
        {
                return getDatosPresupuestados().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingPresupuestado(int id)
        {
            catCargoCategoriaPresupuestado _Item = context.catCargoCategoriaPresupuestado.Where(p => p.presId == id).FirstOrDefault();
            catCargosCategoriasPresupuestados _Item2 = new catCargosCategoriasPresupuestados();

            TryUpdateModel(_Item);
            TryUpdateModel(_Item2);
            if (ValidaDatosPresupuestados(id, _Item2))
            {
                RegistrarLog("Modificar", "Actualiza cantidad de categoría (" + _Item.categId.ToString() + ", " + _Item.presCantidad.ToString() + ")");

                Edit(_Item);
            }

            return View(new GridModel(AllPresupuestados().Where(p => p.categId == _Item.categId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingPresupuestado()
        {
            catCargoCategoriaPresupuestado _Item = new catCargoCategoriaPresupuestado();
            catCargosCategoriasPresupuestados _Item2 = new catCargosCategoriasPresupuestados();

            TryUpdateModel(_Item2);
            if (ValidaDatosPresupuestados(-1, _Item2))
            {
                if (TryUpdateModel(_Item))
                {
                    Create(_Item);
                }
            }

            return View(new GridModel(AllPresupuestados().Where(p => p.categId == _Item.categId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingPresupuestado(int id)
        {
            var _Item = context.catCargoCategoriaPresupuestado.Where(e => e.presId == id).First();

            DeleteConfirmedPresupuestados(id);

            return View(new GridModel(AllPresupuestados().Where(p => p.categId == _Item.categId)));
        }

        private bool ValidaDatosPresupuestados(int id, catCargosCategoriasPresupuestados _Item)
        {
            return true;
        }

        private IEnumerable<catCargosCategoriasPresupuestados> getDatosPresupuestados()
        {
            var _Datos = (from d in context.catCargoCategoriaPresupuestado.ToList()
                          select new catCargosCategoriasPresupuestados()
                                    {
                                        categId = d.categId,
                                        presCantidad = d.presCantidad,
                                        presObservaciones = d.presObservaciones,
                                        presFecha = d.presFecha,
                                        presId = d.presId,
                                        repId = d.repId,
                                        repNombre = d.catReparticion.repNombre
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private void Create(catCargoCategoriaPresupuestado pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.presObservaciones = HttpUtility.HtmlDecode(pItem.presObservaciones);
                    context.catCargoCategoriaPresupuestado.AddObject(pItem);
                    RegistrarLog("Agregar", "Agrega presupuestación de cargo (Id = " + pItem.categId + ")");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("categNumero", "Ya existe la categoría que intenta cargar.");
                }
            }

            return;
        }

        [GridAction]
        public ActionResult _BindingPresupuestado(short? idCategoria)
        {
            idCategoria = idCategoria == null ? 1 : idCategoria;

            return View(new GridModel<catCargosCategoriasPresupuestados>
            {
                Data = AllPresupuestados().Where(exp => exp.categId == idCategoria)
            });
        }


        private void Edit(catCargoCategoriaPresupuestado pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.presObservaciones = HttpUtility.HtmlDecode(pItem.presObservaciones);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("categNumero", "Ya existe la categoría que intenta cargar.");
                }
            }
            return;
        }

        private void DeleteConfirmedPresupuestados(int id)
        {
            catCargoCategoriaPresupuestado _Item = context.catCargoCategoriaPresupuestado.Single(x => x.presId == id);

            try
            {
                RegistrarLog("Eliminar", "Elimina cargo presupuestado (Id =" + id.ToString() + ")");

                context.catCargoCategoriaPresupuestado.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("categNumero", "El cargo tiene empleados asignados, no se puede eliminar.");
            }
        }
    }
}