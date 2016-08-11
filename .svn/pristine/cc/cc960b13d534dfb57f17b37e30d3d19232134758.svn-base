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

    public class catReparticionController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catReparticiones> All()
        {
                return getDatos().ToList();
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catReparticion _Item = context.catReparticion.Where(p => p.repId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catReparticion _Item = new catReparticion();

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }

        private IEnumerable<catReparticiones> getDatos()
        {
            var _Datos = (from rep in context.catReparticion.ToList()
                          select new catReparticiones()
                          {
                              repId = rep.repId,
                              repNombre = rep.repNombre,
                              Sectores = rep.catSector.Count,
                              Empleados = rep.catSector == null ? 0 : context.catPersona.Where(p => p.catSector.repId == rep.repId).Count(),
                              Contratados = rep.catPersonaContratados.Where(c => c.conFechaBaja != null).Count()
                          }).ToList();
            //var _Datos = context.catReparticion;

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Zonas";

            return PartialView();
        }

        private void Create(catReparticion pItem)
        {
            if (ModelState.IsValid)
            {
                context.catReparticion.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }
        
        private void Edit(catReparticion pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int pId)
        {
            catReparticion _Item = context.catReparticion.Single(x => x.repId == pId);
            context.catReparticion.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}