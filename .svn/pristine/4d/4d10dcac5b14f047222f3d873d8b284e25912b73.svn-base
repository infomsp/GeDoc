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

    public class catObraSocialController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catObrasSociales> All()
        {
            return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catObraSocial _Item = context.catObraSocial.Where(p => p.osId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catObraSocial _Item = new catObraSocial();

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

        private IEnumerable<catObrasSociales> getDatos()
        {
            var _Datos = (from d in context.catObraSocial.ToList()
                          select new catObrasSociales()
                          {
                              osId = d.osId,
                              osCodigo = d.osCodigo,
                              osCodigoPostal = d.osCodigoPostal,
                              osDenominacion = d.osDenominacion,
                              osDomicilio = d.osDomicilio,
                              osMail = d.osMail,
                              osSigla = d.osSigla,
                              osTelefono = d.osTelefono,
                              osWeb = d.osWeb,
                              provId = d.provId,
                              provNombre = d.catProvincia.provNombre,
                              osEsPrepaga = (bool)d.osEsPrepaga
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Obras Sociales";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catObraSocial pItem)
        {
            if (ModelState.IsValid)
            {
                context.catObraSocial.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catObraSocial pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catObraSocial _Item = context.catObraSocial.Single(x => x.osId == id);
            context.catObraSocial.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Provincias = (from p in context.catProvincia.ToList()
                               select new catProvincia
                               {
                                    provId = p.provId,
                                    provNombre = p.provNombre
                               }).ToList().OrderBy(p => p.provNombre);

            ViewData["provId_Data"] = new SelectList(_Provincias, "provId", "provNombre");

            return;
        }
    }
}