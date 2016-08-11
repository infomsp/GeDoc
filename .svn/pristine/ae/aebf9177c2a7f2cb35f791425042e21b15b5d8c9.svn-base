using System.Web.Mvc;
using org.bouncycastle.asn1.cms;
using Telerik.Web.Mvc;

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
    using System.Net;


    public class catCentroDeSaludPublicidadController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCentroDeSaludPublicidades> All()
        {
            return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            catCentroDeSaludPublicidad _Item =
                context.catCentroDeSaludPublicidad.Where(p => p.cspId == id).FirstOrDefault();

            catCentroDeSaludPublicidades _Item2 = new catCentroDeSaludPublicidades();

            TryUpdateModel(_Item2);

            _Item2.horaDesde = DateTime.Parse(_Item2.horaDesde).ToString("HH:mm");
            DateTime dtd = Convert.ToDateTime(_Item2.horaDesde);

            _Item2.horaHasta = DateTime.Parse(_Item2.horaHasta).ToString("HH:mm");
            DateTime dth = Convert.ToDateTime(_Item2.horaHasta);


            _Item.cspDescripcion = _Item2.cspDescripcion;
            _Item.cspDesde = new DateTime(_Item2.cspDesde.Value.Year, _Item2.cspDesde.Value.Month, _Item2.cspDesde.Value.Day, dtd.Hour, dtd.Minute, dtd.Second);

            _Item.cspHasta = new DateTime(_Item2.cspHasta.Value.Year, _Item2.cspHasta.Value.Month, _Item2.cspHasta.Value.Day, dth.Hour, dth.Minute, dth.Second);

            if (_Item.cspDesde > _Item.cspHasta)
                _Item.cspDesde = _Item.cspHasta;

            Edit(_Item);
            return View(new GridModel(All()));
        }

        private void Edit(catCentroDeSaludPublicidad pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catCentroDeSaludPublicidad", "Modificar", "Modifica Mensaje");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("csId", ex.Message);
                }
            }
            return;
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCentroDeSaludPublicidad _Item = new catCentroDeSaludPublicidad();
            catCentroDeSaludPublicidades _Item2 = new catCentroDeSaludPublicidades();
            if (TryUpdateModel(_Item2))
            {
                if (Session["UsuarioCentroDeSalud"] == null)
                {
                    RedirectToAction("LogOff", "Account");
                    return null;
                }
                var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;

                _Item.csId = _csId;
                _Item2.horaDesde = DateTime.Parse(_Item2.horaDesde).ToString("HH:mm");
                DateTime dtd = Convert.ToDateTime(_Item2.horaDesde);
                _Item.cspDescripcion = _Item2.cspDescripcion;
                _Item2.horaHasta = DateTime.Parse(_Item2.horaHasta).ToString("HH:mm");
                DateTime dth = Convert.ToDateTime(_Item2.horaHasta);


                _Item.cspDesde = new DateTime(_Item2.cspDesde.Value.Year, _Item2.cspDesde.Value.Month, _Item2.cspDesde.Value.Day, dtd.Hour, dtd.Minute, dtd.Second);

                _Item.cspHasta = new DateTime(_Item2.cspHasta.Value.Year, _Item2.cspHasta.Value.Month, _Item2.cspHasta.Value.Day, dth.Hour, dth.Minute, dth.Second);

                if (_Item.cspDesde > _Item.cspHasta)
                    _Item.cspDesde = _Item.cspHasta;

                Create(_Item);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }


        private void DeleteConfirmed(int id)
        {
            try
            {
                catCentroDeSaludPublicidad _Item = context.catCentroDeSaludPublicidad.Single(x => x.cspId == id);
                context.catCentroDeSaludPublicidad.DeleteObject(_Item);
                context.SaveChanges();

                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("csId", ex.Message);
            }
        }


        private IEnumerable<catCentroDeSaludPublicidades> getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _csId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId;
            var _Datos = (from d in context.catCentroDeSaludPublicidad.ToList()
                          where (_csId == 0 ? 0 : d.csId) == _csId
                          select new catCentroDeSaludPublicidades()
                          {
                              cspId = d.cspId,
                              cspDescripcion = d.cspDescripcion,
                              cspDesde = d.cspDesde.Date,
                              horaDesde = d.cspDesde.ToString("hh:mm"),
                              cspHasta = d.cspHasta.Date,
                              horaHasta = d.cspHasta.ToString("hh:mm"),
                              csId = d.csId,
                              csNombre = context.catCentroDeSalud.Single(x => x.csId == d.csId).csNombre
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Mensajes del LLamador";
            ViewData["FiltroContains"] = true;
            // PopulateDropDownList();
            return PartialView();
        }

        private void Create(catCentroDeSaludPublicidad pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (pItem.csId > 0)
                    {
                        context.catCentroDeSaludPublicidad.AddObject(pItem);

                        new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index",
                            "catCentroDeSaludPublicidad", "Agregar", "Agrega Mensaje");

                        context.SaveChanges();
                    };

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("cspDescripcion", ex.Message);
                }

                return;
            }



        }
    }
}