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

    public class catSectorController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catSectores> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catSector _Item = context.catSector.Where(p => p.secId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catSector _Item = new catSector();

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

        private IEnumerable<catSectores> getDatos()
        {
            //var _Datos = context.catSector;
            dsAccesoExpedientes dsDatos = new dsAccesoExpedientes();

            var _Datos = (from sec in context.catSector.ToList()
                          select new catSectores()
                                    {
                                        secId = sec.secId,
                                        secLinkMapa = sec.secLinkMapa,
                                        secNombre = sec.secNombre,
                                        secUbicacionGeografica = sec.secUbicacionGeografica,
                                        Empleados = sec.catPersona.Count,
                                        repId = sec.repId,
                                        repNombre = sec.catReparticion.repNombre,
                                        ccId = sec.ccId,
                                        secCodigo = sec.secCodigo,
                                        ccCuentaContable = sec.ccId == null ? new catCuentasContables() { ccCodigo = "", ccDescripcion = "" } : new catCuentasContables() { ccId = (int)sec.ccId, ccCodigo = sec.catCuentaContable.ccCodigo, ccDescripcion = sec.catCuentaContable.ccDescripcion }
                                    }).ToList();
            //expOficinasTableAdapter _Oficinas = new expOficinasTableAdapter();
            //var _Datos = (from sec in _Oficinas.GetData().ToList()
            //              select new catSectores()
            //                        {
            //                            secId = (short)sec.Id,
            //                            //secLinkMapa = sec.secLinkMapa,
            //                            secNombre = sec.nombre,
            //                            //secUbicacionGeografica = sec.secUbicacionGeografica,
            //                            Empleados = context.catPersona.Where(p => p.secId == sec.Id).Count(),
            //                            //repId = sec.repId,
            //                            //repNombre = sec.catReparticion.repNombre
            //                        }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Sectores";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catSector pItem)
        {
            if (ModelState.IsValid)
            {
                context.catSector.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catSector pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int pId)
        {
            catSector _Item = context.catSector.Single(x => x.secId == pId);
            context.catSector.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Cuentas = (from p in context.catCuentaContable.ToList()
                            select new catCuentaContable
                            {
                                ccId = p.ccId,
                                ccDescripcion = p.ccCodigo.ToString() + "-" + p.ccDescripcion
                            }).ToList().OrderBy(p => p.ccCodigo);

            ViewData["repId_Data"] = new SelectList(context.catReparticion, "repId", "repNombre");
            ViewData["ccId_Data"] = new SelectList(_Cuentas, "ccId", "ccDescripcion");

            return;
        }
    }
}