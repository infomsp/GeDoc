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
        public IList<catCentroDeSaludSalaConsultorioWS> AllcatCentroDeSaludSalaConsultorio(Int32? cssId)
        {
            return getDatoscatCentroDeSaludSalaConsultorio(cssId).ToList();
        }

        private IEnumerable<catCentroDeSaludSalaConsultorioWS> getDatoscatCentroDeSaludSalaConsultorio(Int32? cssId)
        {
            var _Datos = (from d in context.catCentroDeSaludSalaConsultorio.Where(w => w.cssId == cssId).ToList()
                          select new catCentroDeSaludSalaConsultorioWS()
                                    {
                                        csscId = d.csscId,
                                        csscIdSala = d.cssId,
                                        csscNombre = d.csscNombre
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catCentroDeSaludSalaConsultorio(Int32? cssId)
        {
            cssId = cssId ?? 1;

            PopulateDropDownListcatCentroDeSaludSalaConsultorio();

            return View(new GridModel<catCentroDeSaludSalaConsultorioWS>
            {
                Data = AllcatCentroDeSaludSalaConsultorio(cssId)
            });
        }

        [GridAction]
        public ActionResult InformescatCentroDeSaludSalaConsultorio()
        {
            PopulateDropDownListcatCentroDeSaludSalaConsultorio();

            return PartialView("catCentroDeSaludSalaConsultorio");
        }

        [GridAction]
        public ActionResult getcatCentroDeSaludSalaConsultorio(string pAccion, Int64? csscId, Int64? cssId)
        {
            var Datos = new catCentroDeSaludSalaConsultorioWS();
            Datos.csscAccion = pAccion;
            Datos.csscId = (int)csscId;
            Datos.csscIdSala = (int)cssId;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catCentroDeSaludSalaConsultorio.Single(w => w.csscId == csscId);

                Datos.csscIdSala = DatosActuales.cssId;
                Datos.csscNombre = DatosActuales.csscNombre;
            }

            PopulateDropDownListcatCentroDeSaludSalaConsultorio();

            return PartialView("CRUDcatCentroDeSaludSalaConsultorio", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatCentroDeSaludSalaConsultorio()
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatCentroDeSaludSalaConsultorio(catCentroDeSaludSalaConsultorioWS Datos)
        {
            if (Datos.csscAccion == "Modificar" || Datos.csscAccion == "Agregar")
            {
                Datos.csscNombre = Datos.csscNombre ?? "";
                if (Datos.csscNombre.Trim().Length == 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar la descripción para el Consultorio.",
                            Foco = "csscNombre"
                        });
                }
            }

            var Info = context.catCentroDeSaludSala.Single(s => s.cssId == Datos.csscIdSala);
            try
            {
                switch (Datos.csscAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catCentroDeSaludSalaConsultorio();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.csscAccion != "Agregar")
                        {
                            Registro = context.catCentroDeSaludSalaConsultorio.Single(s => s.csscId == Datos.csscId);
                        }

                        Registro.csscNombre = Datos.csscNombre;
                        Registro.cssId = Datos.csscIdSala;

                        if (Datos.csscAccion == "Agregar")
                        {
                            context.catCentroDeSaludSalaConsultorio.AddObject(Registro);
                        }

                        break;
                    case "Eliminar":
                        var DeleteCompra = context.catCentroDeSaludSalaConsultorio.Single(w => w.csscId == Datos.csscId);
                        context.catCentroDeSaludSalaConsultorio.DeleteObject(DeleteCompra);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catCentroDeSaludSala", Datos.csscAccion, Datos.csscAccion + " Consultorio a la Sala " + Info.cssNombre);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información de Consultorios en forma correcta.",
                    Foco = "csscNombre"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "csscNombre"
                });
            }
        }
    }
}