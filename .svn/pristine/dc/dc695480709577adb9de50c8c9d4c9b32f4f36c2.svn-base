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

    public class catTipoNormaLegalController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catTipoNormasLegales> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catTipoNormaLegal _Item = context.catTipoNormaLegal.Where(p => p.tnlId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catTipoNormaLegal _Item = new catTipoNormaLegal();

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

        private IEnumerable<catTipoNormasLegales> getDatos()
        {
            var _Datos = (from d in context.catTipoNormaLegal.ToList()
                          select new catTipoNormasLegales()
                                    {
                                        tnlId = d.tnlId,
                                        tnlNombre = d.tnlNombre
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Tipo de Resoluciones";

            return PartialView();    
        }

        private void Create(catTipoNormaLegal pItem)
        {
            if (ModelState.IsValid)
            {
                context.catTipoNormaLegal.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catTipoNormaLegal pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catTipoNormaLegal _Item = context.catTipoNormaLegal.Single(x => x.tnlId == id);
            context.catTipoNormaLegal.DeleteObject(_Item);
            context.SaveChanges();
        }

    }
}