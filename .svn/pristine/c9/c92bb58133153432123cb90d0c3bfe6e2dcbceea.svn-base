namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class GestionContableController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catAdministradorWS> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catAdministrador _Item = context.catAdministrador.First(p => p.admId == id);

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catAdministrador _Item = new catAdministrador();

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

        private IEnumerable<catAdministradorWS> getDatos()
        {
            var _Datos = (from d in context.catAdministrador
                          select new catAdministradorWS()
                                    {
                                        ceDescripcion = d.catCuentaEscritural.ceDescripcion,
                                        ceId = d.ceId,
                                        admDescripcion = d.admDescripcion,
                                        admId = d.admId,
                                        admCodigo = d.admCodigo
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult GrillaAdministradores()
        {
            ViewBag.Catalogo = "Administradores";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catAdministrador pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catAdministrador.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }

            return;
        }

        private void Edit(catAdministrador pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catAdministrador _Item = context.catAdministrador.Single(x => x.admId == id);
            context.catAdministrador.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _CtasEs = (from p in context.catCuentaEscritural.Where(w => w.icId == icId).ToList()
                           select new catCuentaEscritural
                           {
                               ceId = p.ceId,
                               ceDescripcion = p.ceCodigo + "-" + p.ceDescripcion
                           }).ToList().OrderBy(p => p.ceDescripcion);

            ViewData["ceId_Data"] = new SelectList(_CtasEs, "ceId", "ceDescripcion");
        }
    }
}