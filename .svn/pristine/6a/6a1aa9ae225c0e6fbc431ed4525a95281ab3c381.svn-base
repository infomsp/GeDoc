namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class catGradosCategoriaController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditingPresupuestado()
        {
            return View(new GridModel(AllPresupuestados()));
        }

        private IList<catGradosCategoriasPresupuestadosWS> AllPresupuestados()
        {
                return getDatosPresupuestados().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingPresupuestado(int id)
        {
            catGradosPresupuestado _Item = context.catGradosPresupuestado.Where(p => p.psId == id).FirstOrDefault();
            catGradosCategoriasPresupuestadosWS _Item2 = new catGradosCategoriasPresupuestadosWS();

            TryUpdateModel(_Item);
            TryUpdateModel(_Item2);
            if (ValidaDatosPresupuestados(id, _Item2))
            {
                RegistrarLog("Modificar", "Actualiza cantidad de categoría (" + _Item.gracId.ToString() + ", " + _Item.psCantidad.ToString() + ")");

                Edit(_Item);
            }

            return View(new GridModel(AllPresupuestados().Where(p => p.gracId == _Item.gracId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingPresupuestado()
        {
            catGradosPresupuestado _Item = new catGradosPresupuestado();
            catGradosCategoriasPresupuestadosWS _Item2 = new catGradosCategoriasPresupuestadosWS();

            TryUpdateModel(_Item2);
            if (ValidaDatosPresupuestados(-1, _Item2))
            {
                if (TryUpdateModel(_Item))
                {
                    Create(_Item);
                }
            }

            return View(new GridModel(AllPresupuestados().Where(p => p.gracId == _Item.gracId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingPresupuestado(int id)
        {
            var _Item = context.catGradosPresupuestado.Where(e => e.psId == id).First();

            DeleteConfirmedPresupuestados(id);

            return View(new GridModel(AllPresupuestados().Where(p => p.gracId == _Item.gracId)));
        }

        private bool ValidaDatosPresupuestados(int id, catGradosCategoriasPresupuestadosWS _Item)
        {
            return true;
        }

        private IEnumerable<catGradosCategoriasPresupuestadosWS> getDatosPresupuestados()
        {
            var _Datos = (from d in context.catGradosPresupuestado.ToList()
                          select new catGradosCategoriasPresupuestadosWS()
                                    {
                                        gracId = d.gracId,
                                        psCantidad = d.psCantidad,
                                        psObservaciones = d.psObservaciones,
                                        psFecha = d.psFecha,
                                        psId = d.psId,
                                        icId = d.icId,
                                        Institucion = d.icId == null ? "" : d.catInstitucionContable.icDescripcion
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private void Create(catGradosPresupuestado pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.psObservaciones = HttpUtility.HtmlDecode(pItem.psObservaciones);
                    context.catGradosPresupuestado.AddObject(pItem);
                    RegistrarLog("Agregar", "Agrega presupuestación de cargo (Id = " + pItem.gracId + ")");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("psCantidad", "Ya existe la categoría que intenta cargar.");
                }
            }

            return;
        }

        [GridAction]
        public ActionResult _BindingPresupuestado(short? idGradoCategoria)
        {
            idGradoCategoria = idGradoCategoria == null ? 1 : idGradoCategoria;

            return View(new GridModel<catGradosCategoriasPresupuestadosWS>
            {
                Data = AllPresupuestados().Where(exp => exp.gracId == idGradoCategoria)
            });
        }


        private void Edit(catGradosPresupuestado pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.psObservaciones = HttpUtility.HtmlDecode(pItem.psObservaciones);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("psCantidad", "Ya existe la categoría que intenta cargar.");
                }
            }
            return;
        }

        private void DeleteConfirmedPresupuestados(int id)
        {
            catGradosPresupuestado _Item = context.catGradosPresupuestado.Single(x => x.psId == id);

            try
            {
                RegistrarLog("Eliminar", "Elimina cargo presupuestado (Id =" + id.ToString() + ")");

                context.catGradosPresupuestado.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("psCantidad", "El cargo tiene empleados asignados, no se puede eliminar.");
            }
        }
    }
}