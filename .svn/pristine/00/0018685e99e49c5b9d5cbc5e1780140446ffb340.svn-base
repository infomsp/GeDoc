namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class catCentroDeSaludSalaController : Controller
    {
        //Edición de datos
        public IList<catCentroDeSaludSalaTelevisorWS> AllcatCentroDeSaludSalaTelevisor(Int32? cssId)
        {
            return getDatoscatCentroDeSaludSalaTelevisor(cssId).ToList();
        }

        private IEnumerable<catCentroDeSaludSalaTelevisorWS> getDatoscatCentroDeSaludSalaTelevisor(Int32? cssId)
        {
            var _Datos = (from d in context.catCentroDeSaludSalaTelevisor.Where(w => w.cssId == cssId).ToList()
                          select new catCentroDeSaludSalaTelevisorWS()
                                    {
                                        csstId = d.csstId,
                                        cssIdSala = d.cssId,
                                        csstNombre = d.csstNombre
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catCentroDeSaludSalaTelevisor(Int32? cssId)
        {
            cssId = cssId ?? 1;

            PopulateDropDownListcatCentroDeSaludSalaTelevisor();

            return View(new GridModel<catCentroDeSaludSalaTelevisorWS>
            {
                Data = AllcatCentroDeSaludSalaTelevisor(cssId)
            });
        }

        [GridAction]
        public ActionResult InformescatCentroDeSaludSalaTelevisor()
        {
            PopulateDropDownListcatCentroDeSaludSalaTelevisor();

            return PartialView("catCentroDeSaludSalaTelevisor");
        }

        [GridAction]
        public ActionResult getcatCentroDeSaludSalaTelevisor(string pAccion, Int64? csstId, Int64? cssId)
        {
            var Datos = new catCentroDeSaludSalaTelevisorWS();
            Datos.csstAccion = pAccion;
            Datos.csstId = (int)csstId;
            Datos.cssIdSala = (int)cssId;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catCentroDeSaludSalaTelevisor.Single(w => w.csstId == csstId);

                Datos.cssIdSala = DatosActuales.cssId;
                Datos.csstNombre = DatosActuales.csstNombre;
            }

            PopulateDropDownListcatCentroDeSaludSalaTelevisor();

            return PartialView("CRUDcatCentroDeSaludSalaTelevisor", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatCentroDeSaludSalaTelevisor()
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatCentroDeSaludSalaTelevisor(catCentroDeSaludSalaTelevisorWS Datos)
        {
            if (Datos.csstAccion == "Modificar" || Datos.csstAccion == "Agregar")
            {
                Datos.csstNombre = Datos.csstNombre ?? "";
                if (Datos.csstNombre.Trim().Length == 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar la descripción para el Televisor.",
                            Foco = "csstNombre"
                        });
                }
            }

            var Info = context.catCentroDeSaludSala.Single(s => s.cssId == Datos.cssIdSala);
            try
            {
                switch (Datos.csstAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catCentroDeSaludSalaTelevisor();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.csstAccion != "Agregar")
                        {
                            Registro = context.catCentroDeSaludSalaTelevisor.Single(s => s.csstId == Datos.csstId);
                        }

                        Registro.csstNombre = Datos.csstNombre;
                        Registro.cssId = Datos.cssIdSala;

                        if (Datos.csstAccion == "Agregar")
                        {
                            context.catCentroDeSaludSalaTelevisor.AddObject(Registro);
                        }

                        break;
                    case "Eliminar":
                        var DeleteCompra = context.catCentroDeSaludSalaTelevisor.Single(w => w.csstId == Datos.csstId);
                        context.catCentroDeSaludSalaTelevisor.DeleteObject(DeleteCompra);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catCentroDeSaludSala", Datos.csstAccion, Datos.csstAccion + " Televisor a la Sala " + Info.cssNombre);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información de televisores en forma correcta.",
                    Foco = "csstNombre"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "csstNombre"
                });
            }
        }
    }
}