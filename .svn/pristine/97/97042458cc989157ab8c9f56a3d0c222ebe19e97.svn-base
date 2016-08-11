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

    public class catCuentaEscrituralController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCuentasEscriturales> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catCuentaEscritural _Item = context.catCuentaEscritural.Where(p => p.ceId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCuentaEscritural _Item = new catCuentaEscritural();

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

        private IEnumerable<catCuentasEscriturales> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from d in context.catCuentaEscritural.Where(w => w.icId == icId)
                          select new catCuentasEscriturales()
                                    {
                                        ceDescripcion = d.ceDescripcion,
                                        ceId = d.ceId,
                                        fteDescripcion = d.catFuente.fteDescripcion,
                                        fteId = d.fteId,
                                        ceCodigo = d.ceCodigo
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Cuentas Escriturales";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catCuentaEscritural pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                    context.catCuentaEscritural.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }

            return;
        }

        private void Edit(catCuentaEscritural pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catCuentaEscritural _Item = context.catCuentaEscritural.Single(x => x.ceId == id);
            context.catCuentaEscritural.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            ViewData["fteId_Data"] = new SelectList(context.catFuente.Where(w => w.icId == icId), "fteId", "fteDescripcion");

            return;
        }
    }
}