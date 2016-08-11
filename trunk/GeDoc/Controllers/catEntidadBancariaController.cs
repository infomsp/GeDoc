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

    public class catEntidadBancariaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catEntidadesBancarias> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catEntidadBancaria _Item = context.catEntidadBancaria.Where(p => p.bcoId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catEntidadBancaria _Item = new catEntidadBancaria();

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

        private IEnumerable<catEntidadesBancarias> getDatos()
        {
            var _Datos = (from d in context.catEntidadBancaria.ToList()
                          select new catEntidadesBancarias()
                                    {
                                        bcoCUIT = d.bcoCUIT,
                                        bcoRazonSocial = d.bcoRazonSocial,
                                        bcoNumeroSucursal = d.bcoNumeroSucursal,
                                        bcoId = d.bcoId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Entidades Bancarias";

            return PartialView();
        }

        private void Create(catEntidadBancaria pItem)
        {
            if (ModelState.IsValid)
            {
                context.catEntidadBancaria.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catEntidadBancaria pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catEntidadBancaria _Item = context.catEntidadBancaria.Single(x => x.bcoId == id);
            context.catEntidadBancaria.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}