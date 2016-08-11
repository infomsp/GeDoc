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

    public class catPartidaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catPartidas> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catPartida _Item = context.catPartida.Where(p => p.partId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catPartida _Item = new catPartida();

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

        private IEnumerable<catPartidas> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from d in context.catPartida.Where(w => w.icId == icId)
                          select new catPartidas()
                                    {
                                        partCodigo = d.partCodigo,
                                        partNombre = d.partNombre,
                                        partId = d.partId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Partidas";

            return PartialView();
        }

        private void Create(catPartida pItem)
        {
            if (ModelState.IsValid)
            {
                pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                context.catPartida.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catPartida pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catPartida _Item = context.catPartida.Single(x => x.partId == id);
            context.catPartida.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}