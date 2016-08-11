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

    public class catIdiomaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catIdiomas> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catIdioma _Item = context.catIdioma.Where(p => p.idiomaId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catIdioma _Item = new catIdioma();

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

        private IEnumerable<catIdiomas> getDatos()
        {
            var _Datos = (from d in context.catIdioma.ToList()
                          select new catIdiomas()
                                    {
                                        idiomaDescripcion = d.idiomaDescripcion,
                                        idiomaId = d.idiomaId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Idiomas";

            return PartialView();
        }

        private void Create(catIdioma pItem)
        {
            if (ModelState.IsValid)
            {
                context.catIdioma.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catIdioma pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catIdioma _Item = context.catIdioma.Single(x => x.idiomaId == id);
            context.catIdioma.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}