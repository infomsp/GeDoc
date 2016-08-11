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

    public class catFODAController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catFODAs> All()
        {
                return getDatos().ToList();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[CultureAwareAction]
        //[GridAction]
        //public ActionResult _SaveEditing(int id)
        //{
        //    catFODA _Item = context.catFODA.Where(p => p.fodaId == id).FirstOrDefault();

        //    TryUpdateModel(_Item);

        //    var _usrId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).usrId;
        //    _Item.usrId = (short)_usrId;

        //    Edit(_Item);

        //    return View(new GridModel(All()));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id, string Fortaleza, string Oportunidad, string Debilidad, string Amenaza)
        {
            catFODA _Item = context.catFODA.Where(p => p.fodaId == id).FirstOrDefault();

            _Item.fodaFortaleza = Fortaleza;
            _Item.fodaOportunidad = Oportunidad;
            _Item.fodaDebilidad = Debilidad;
            _Item.fodaAmenaza = Amenaza;

            var _usrId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).usrId;
            _Item.usrId = (short)_usrId;

            Edit(_Item);

            return View(new GridModel(All()));
        }


        private IEnumerable<catFODAs> getDatos()
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _usrId = (Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).usrId;
            var _Datos = (from d in context.catFODA.ToList()
                          //join x in context.enlUsuarioOficina on d.usrId equals x.usrId
                          select new catFODAs()
                                    {
                                        fodaAmenaza = d.fodaAmenaza,
                                        fodaDebilidad = d.fodaDebilidad,
                                        fodaFechaUltimoCambio = d.fodaFechaUltimoCambio,
                                        fodaOportunidad = d.fodaOportunidad,
                                        fodaFortaleza = d.fodaFortaleza,
                                        fodaId = d.fodaId,
                                        PermiteEditar = (d.catOficina.enlUsuarioOficina.Where(w => w.usrId == _usrId && w.ofiId == d.ofiId).Count() > 0),
                                        Usuario = d.usrId == null ? new sisUsuarios() { usrApellidoyNombre = "" } : new sisUsuarios() { usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre, usrId = (short)d.usrId },
                                        usrId = (short)d.usrId,
                                        Oficina = new catOficinas() { ofiNombre = d.catOficina.ofiNombre }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ViewResult Index()
        {
            ViewBag.Catalogo = "FODA";

            return View(getDatos());
        }

        private void Edit(catFODA pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Registra log de usuario
                    new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catFODA", "Modificar", "Modifica registro de FODA");

                    pItem.fodaFechaUltimoCambio = DateTime.Now;

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("fodaAmenaza", ex.Message);
                }
            }
            return;
        }

    }
}