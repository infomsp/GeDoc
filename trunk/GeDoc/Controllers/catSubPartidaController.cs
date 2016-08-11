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
    using System.Data.OleDb;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;

    public class catSubPartidaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catSubPartidas> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catSubPartida _Item = context.catSubPartida.Where(p => p.subpId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catSubPartida _Item = new catSubPartida();

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

        private IEnumerable<catSubPartidas> getDatos()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            var _Datos = (from sec in context.catSubPartida.Where(w => w.icId == icId).ToList()
                          select new catSubPartidas()
                                    {
                                        subpId = sec.subpId,
                                        partId = sec.partId,
                                        partNombre = sec.catPartida.partCodigo + " - " + sec.catPartida.partNombre,
                                        subpCodigo = sec.subpCodigo,
                                        subpNombre = sec.subpNombre
                                    }).ToList();
            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Sub-Partidas";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catSubPartida pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.icId = (Session["Usuario"] as sisUsuario).icId;
                    context.catSubPartida.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("subpId", ex.Message);
                }
            }

            return;
        }

        private void Edit(catSubPartida pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("subpId", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int pId)
        {
            catSubPartida _Item = context.catSubPartida.Single(x => x.subpId == pId);
            context.catSubPartida.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var icId = (Session["Usuario"] as sisUsuario).icId;
            ViewData["partId_Data"] = new SelectList(context.catPartida.Where(w => w.icId == icId), "partId", "partNombre");

            return;
        }
    }
}