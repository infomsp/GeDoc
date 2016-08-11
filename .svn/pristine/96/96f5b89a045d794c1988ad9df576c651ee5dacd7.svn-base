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

    public class catFondoPermanenteController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catFondosPermanentes> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catFondoPermanente _Item = context.catFondoPermanente.Where(p => p.fpId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catFondoPermanente _Item = new catFondoPermanente();

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

        private IEnumerable<catFondosPermanentes> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from d in context.catFondoPermanente.Where(w => w.icId == icId)
                          select new catFondosPermanentes()
                                    {
                                        bcoId = d.bcoId,
                                        bcoRazonSocial = d.catEntidadBancaria.bcoRazonSocial,
                                        fpCBU = d.fpCBU,
                                        fpDescripcion = d.fpDescripcion,
                                        fpId = d.fpId,
                                        fteDescripcion = d.catFuente.fteDescripcion,
                                        fteId = d.fteId,
                                        fpNumeroCuenta = d.fpNumeroCuenta
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Fondos Permanentes";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catFondoPermanente pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                    context.catFondoPermanente.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }

            return;
        }

        private void Edit(catFondoPermanente pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            try
            {
                catFondoPermanente _Item = context.catFondoPermanente.Single(x => x.fpId == id);
                context.catFondoPermanente.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            { }
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            ViewData["bcoId_Data"] = new SelectList(context.catEntidadBancaria, "bcoId", "bcoRazonSocial");
            ViewData["fteId_Data"] = new SelectList(context.catFuente.Where(w => w.icId == icId), "fteId", "fteDescripcion");

            return;
        }
    }
}