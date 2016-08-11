namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class HistoriaClinicaController : Controller
    {
        //Edición de datos
        public IList<catHCProblemaCronicoWS> AllcatHCProblemaCronico()
        {
            return getDatoscatHCProblemaCronico().ToList();
        }

        private IEnumerable<catHCProblemaCronicoWS> getDatoscatHCProblemaCronico()
        {
            var _Datos = (from d in context.catHCProblemaCronico
                          select new catHCProblemaCronicoWS()
                                    {
                                        pcroId = d.pcroId,
                                        pcroCodigo = d.pcroCodigo,
                                        pcroDescripcion = d.pcroDescripcion
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catHCProblemaCronico()
        {
            PopulateDropDownListcatHCProblemaCronico();

            return View(new GridModel<catHCProblemaCronicoWS>
            {
                Data = AllcatHCProblemaCronico()
            });
        }

        [GridAction]
        public ActionResult ProblemasCronicos()
        {
            PopulateDropDownListcatHCProblemaCronico();

            return PartialView("ProblemasCronicos");
        }

        [GridAction]
        public ActionResult getcatHCProblemaCronico(string pAccion, Int64? pcroId)
        {
            var Datos = new catHCProblemaCronicoWS();
            Datos.pcroAccion = pAccion;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catHCProblemaCronico.Single(w => w.pcroId == pcroId);

                Datos.pcroId = DatosActuales.pcroId;
                Datos.pcroCodigo = DatosActuales.pcroCodigo;
                Datos.pcroDescripcion = DatosActuales.pcroDescripcion;
            }

            PopulateDropDownListcatHCProblemaCronico();

            return PartialView("ProblemasCronicosCRUD", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatHCProblemaCronico()
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatHCProblemaCronico(catHCProblemaCronicoWS Datos)
        {
            if (Datos.pcroAccion == "Modificar" || Datos.pcroAccion == "Agregar")
            {
                Datos.pcroDescripcion = Datos.pcroDescripcion ?? "";
                if (Datos.pcroDescripcion.Trim().Length == 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar la descripción.",
                            Foco = "pcroDescripcion"
                        });
                }
                if (Datos.pcroCodigo <= 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Código incorrecto.",
                        Foco = "pcroCodigo"
                    });
                }
                Datos.pcroDescripcion = Datos.pcroDescripcion.ToUpper();
                var Consulta = context.catHCProblemaCronico.SingleOrDefault(s => s.pcroCodigo == Datos.pcroCodigo && s.pcroId != Datos.pcroId);
                if (Consulta != null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "El código ingresado ya existe.",
                        Foco = "pcroCodigo"
                    });
                }

                Consulta = context.catHCProblemaCronico.SingleOrDefault(s => s.pcroDescripcion == Datos.pcroDescripcion && s.pcroId != Datos.pcroId);
                if (Consulta != null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "La Descripción ingresada ya existe.",
                        Foco = "pcroCodigo"
                    });
                }
            }

            try
            {
                switch (Datos.pcroAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catHCProblemaCronico();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.pcroAccion != "Agregar")
                        {
                            Registro = context.catHCProblemaCronico.Single(s => s.pcroId == Datos.pcroId);
                        }

                        Registro.pcroCodigo = Datos.pcroCodigo;
                        Registro.pcroDescripcion = Datos.pcroDescripcion.ToUpper();

                        if (Datos.pcroAccion == "Agregar")
                        {
                            context.catHCProblemaCronico.AddObject(Registro);
                        }

                        break;
                    case "Eliminar":
                        var EliminarRegistro = context.catHCProblemaCronico.Single(w => w.pcroId == Datos.pcroId);
                        context.catHCProblemaCronico.DeleteObject(EliminarRegistro);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "ProblemasCronicos", "HistoriaClinica", Datos.pcroAccion, Datos.pcroAccion + " Problema crónico " + Datos.pcroDescripcion);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información de Problemas Crónicos en forma correcta.",
                    Foco = "pcroDescripcion"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "pcroDescripcion"
                });
            }
        }
    }
}