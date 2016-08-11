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

    public class catCuentaContableController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCuentasContables> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catCuentaContable _Item = context.catCuentaContable.Where(p => p.ccId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCuentaContable _Item = new catCuentaContable();

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

        private IEnumerable<catCuentasContables> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from d in context.catCuentaContable.Where(w => w.icId == icId)
                          select new catCuentasContables()
                                    {
                                        ccCodigo = d.ccCodigo,
                                        ccDescripcion = d.ccDescripcion,
                                        ccId = d.ccId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Cuentas";
            Session["PathArchivos"] = Server.MapPath("~/Content/Archivos/Importaciones");

            return PartialView();
        }

        private void Create(catCuentaContable pItem)
        {
            if (ModelState.IsValid)
            {
                pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                context.catCuentaContable.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catCuentaContable pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catCuentaContable _Item = context.catCuentaContable.Single(x => x.ccId == id);
            context.catCuentaContable.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}