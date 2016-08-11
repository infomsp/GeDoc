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

    public class catCentroDeSaludConsultorioController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCentrosDeSaludConsultorios> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catCentroDeSaludConsultorio _Item = context.catCentroDeSaludConsultorio.Where(p => p.cscId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCentroDeSaludConsultorio _Item = new catCentroDeSaludConsultorio();

            if (TryUpdateModel(_Item))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

                _Item.csId = _csId;
                Create(_Item);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id, false);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _ActivaRegistro(int id)
        {
            DeleteConfirmed(id, true);

            return View(new GridModel(All()));
        }

        private IEnumerable<catCentrosDeSaludConsultorios> getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Datos = (from d in context.catCentroDeSaludConsultorio.ToList()
                          where d.csId == _csId
                          select new catCentrosDeSaludConsultorios()
                                    {
                                        cscActivo = d.cscActivo,
                                        cscId = d.cscId,
                                        cscNombre = d.cscNombre,
                                        csId = d.csId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ViewResult Index()
        {
            ViewBag.Catalogo = "Consultorios";

            return View(getDatos());
        }

        private void Create(catCentroDeSaludConsultorio pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.cscActivo = true;
                    pItem.cscNombre = pItem.cscNombre.ToUpper();
                    context.catCentroDeSaludConsultorio.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("cscNombre", ex.Message);
                }
            }

            return;
        }

        private void Edit(catCentroDeSaludConsultorio pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catCentroDeSaludConsultorio", "Modificar", "Modifica consultorio");

                    pItem.cscNombre = pItem.cscNombre.ToUpper();

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("cscNombre", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmed(int id, bool pEstado)
        {
            try
            {
                catCentroDeSaludConsultorio _Item = context.catCentroDeSaludConsultorio.Single(x => x.cscId == id);
                //context.catCentroDeSaludConsultorio.DeleteObject(_Item);
                _Item.cscActivo = pEstado;
                //Registra log de usuario
                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catCentroDeSaludConsultorio", "Eliminar", "Elimina Consultorio");
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("cscNombre", ex.Message);
            }
        }
    }
}