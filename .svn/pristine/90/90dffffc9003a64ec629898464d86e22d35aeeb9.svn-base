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
    using System.Data.OleDb;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;

    public class catProductoImprentaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catProductosImprenta> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catProductoImprenta _Item = context.catProductoImprenta.Where(p => p.piId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catProductoImprenta _Item = new catProductoImprenta();

            if (TryUpdateModel(_Item))
            {
                _Item.piActivo = true;
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

        private IEnumerable<catProductosImprenta> getDatos()
        {
            dsAccesoExpedientes dsDatos = new dsAccesoExpedientes();

            var _Datos = (from pi in context.catProductoImprenta.ToList()
                          where pi.piActivo
                          select new catProductosImprenta()
                                    {
                                        piId = pi.piId,
                                        piDescripcion = pi.piDescripcion,
                                        piActivo = pi.piActivo
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Productos de Imprenta";

            return PartialView();
        }

        private void Create(catProductoImprenta pItem)
        {
            if (ModelState.IsValid)
            {
                context.catProductoImprenta.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catProductoImprenta pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int pId)
        {
            catProductoImprenta _Item = context.catProductoImprenta.Single(x => x.piId == pId);
            _Item.piActivo = false;
            context.SaveChanges();
        }
    }
}