namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class GestionContableController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditing_catTipoGasto()
        {
            return View(new GridModel(All_catTipoGasto()));
        }

        private IList<catTipoGastoWS> All_catTipoGasto()
        {
            return getDatos_catTipoGasto().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing_catTipoGasto(int id)
        {
            catTipoGasto _Item = context.catTipoGasto.First(p => p.tgId == id);

            TryUpdateModel(_Item);

            Edit_catTipoGasto(_Item);

            return View(new GridModel(All_catTipoGasto()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing_catTipoGasto()
        {
            catTipoGasto _Item = new catTipoGasto();

            if (TryUpdateModel(_Item))
            {
                Create_catTipoGasto(_Item);
            }

            return View(new GridModel(All_catTipoGasto()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing_catTipoGasto(int id)
        {
            DeleteConfirmed_catTipoGasto(id);

            return View(new GridModel(All_catTipoGasto()));
        }

        private IEnumerable<catTipoGastoWS> getDatos_catTipoGasto()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from d in context.catTipoGasto.Where(w => w.icId == icId)
                          select new catTipoGastoWS()
                                    {
                                        tgDescripcion = d.tgDescripcion,
                                        tgId = d.tgId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult GrillaTipoDeGastos()
        {
            ViewBag.Catalogo = "Tipos de Gastos";

            return PartialView();
        }

        private void Create_catTipoGasto(catTipoGasto pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                    context.catTipoGasto.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }

            return;
        }

        private void Edit_catTipoGasto(catTipoGasto pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed_catTipoGasto(int id)
        {
            catTipoGasto _Item = context.catTipoGasto.Single(x => x.tgId == id);
            context.catTipoGasto.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}