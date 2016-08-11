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

    public class catFuenteController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catFuentes> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catFuente _Item = context.catFuente.Where(p => p.fteId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catFuente _Item = new catFuente();

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

        private IEnumerable<catFuentes> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from d in context.catFuente.Where(w => w.icId == icId)
                          select new catFuentes()
                                    {
                                        fteCodigo = d.fteCodigo,
                                        fteDescripcion = d.fteDescripcion,
                                        fteId = d.fteId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Fuentes de Financiamiento";

            return PartialView();
        }

        private void Create(catFuente pItem)
        {
            if (ModelState.IsValid)
            {
                pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                context.catFuente.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catFuente pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catFuente _Item = context.catFuente.Single(x => x.fteId == id);
            context.catFuente.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}