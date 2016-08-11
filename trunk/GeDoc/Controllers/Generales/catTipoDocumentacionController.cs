using System;

namespace GeDoc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Filters;
    using Models;

    public class catTipoDocumentacionController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            return View(new GridModel(All()));
        }

        private IList<catTipoDocumentacionWS> All()
        {
                return getDatos().ToList();
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            try
            {
                var _Item = context.catTipoDocumentacion.Single(p => p.tdId == id);

                TryUpdateModel(_Item);

                if (ModelState.IsValid)
                {
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("tdDescripcion", ex.Message);
            }

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            try
            {
                var _Item = new catTipoDocumentacion();

                if (TryUpdateModel(_Item))
                {
                    if (ModelState.IsValid)
                    {
                        context.catTipoDocumentacion.AddObject(_Item);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("tdDescripcion", ex.Message);
            }

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }

            try
            {
                var _Item = context.catTipoDocumentacion.Single(x => x.tdId == id);
                context.catTipoDocumentacion.DeleteObject(_Item);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("tdDescripcion", ex.Message);
            }

            return View(new GridModel(All()));
        }

        private IEnumerable<catTipoDocumentacionWS> getDatos()
        {
            var _Datos = (from info in context.catTipoDocumentacion.ToList()
                          select new catTipoDocumentacionWS()
                          {
                              tdId = info.tdId,
                              tdDescripcion = info.tdDescripcion
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Tipos de Documentación";

            return PartialView();
        }
    }
}