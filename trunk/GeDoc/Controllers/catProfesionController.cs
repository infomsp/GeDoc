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

    public class catProfesionController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            
            return View(new GridModel(All()));
        }

        private IList<catProfesiones> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catProfesion _Item = context.catProfesion.Where(p => p.profId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catProfesion _Item = new catProfesion();

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

        private IEnumerable<catProfesiones> getDatos()
        {
            var _Datos = (from d in context.catProfesion.ToList()
                          select new catProfesiones()
                                    {
                                        profNombre = d.profNombre,
                                        profId = d.profId,
                                        profEmpleados = d.catPersona.Count,
                                        profContratados = d.catPersonaContratados.Where(c => c.conFechaBaja != null).Count()
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Profesiones";

            return PartialView();
        }

        private void Create(catProfesion pItem)
        {
            if (ModelState.IsValid)
            {
                context.catProfesion.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catProfesion pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catProfesion _Item = context.catProfesion.Single(x => x.profId == id);
            context.catProfesion.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}