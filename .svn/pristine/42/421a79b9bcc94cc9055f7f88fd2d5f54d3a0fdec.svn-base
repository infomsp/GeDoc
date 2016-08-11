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

    public class catZonaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catZonas> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catZona _Item = context.catZona.Where(p => p.zonId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catZona _Item = new catZona();

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

        private IEnumerable<catZonas> getDatos()
        {
            var _Datos = (from d in context.catZona.ToList()
                          select new catZonas()
                                    {
                                        zonId = d.zonId,
                                        zonCodigo = d.zonCodigo,
                                        zonNombre = d.zonNombre,
                                        zonIdDependencia = d.zonIdDependencia,
                                        zonNombreDependencia = d.zonIdDependencia == null ? "Sin dependencia" : context.catZona.Where(z => z.zonIdDependencia == d.zonIdDependencia).FirstOrDefault().zonNombre
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Zonas";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catZona pItem)
        {
            if (ModelState.IsValid)
            {
                context.catZona.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catZona pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catZona _Item = context.catZona.Single(x => x.zonId == id);
            context.catZona.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            //var _Zonas = (from z in context.catZona.ToList()
            //                 select new catZona
            //                 {
            //                     zonId = z.zonId,
            //                     zonNombre = z.zonNombre
            //                 }).ToList();

            ViewData["zonIdDependencia_Data"] = new SelectList(context.catZona, "zonId", "zonNombre");

            return;
        }
    }
}