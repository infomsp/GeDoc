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
        public IList<catHCVademecumWS> AllcatHCVademecum()
        {
            return getDatoscatHCVademecum().ToList();
        }

        private IEnumerable<catHCVademecumWS> getDatoscatHCVademecum()
        {
            var _Datos = (from d in context.catHCVademecum
                          select new catHCVademecumWS()
                                    {
                                        vadId = d.vadId,
                                        vadCodigo = d.vadCodigo,
                                        vadDescripcion = d.vadDescripcion
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catHCVademecum()
        {
            PopulateDropDownListcatHCVademecum();

            return View(new GridModel<catHCVademecumWS>
            {
                Data = AllcatHCVademecum()
            });
        }

        [GridAction]
        public ActionResult Vademecum()
        {
            PopulateDropDownListcatHCVademecum();

            return PartialView("Vademecum");
        }

        [GridAction]
        public ActionResult getcatHCVademecum(string pAccion, Int64? vadId)
        {
            var Datos = new catHCVademecumWS();
            Datos.vadAccion = pAccion;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catHCVademecum.Single(w => w.vadId == vadId);

                Datos.vadId = DatosActuales.vadId;
                Datos.vadCodigo = DatosActuales.vadCodigo;
                Datos.vadDescripcion = DatosActuales.vadDescripcion;
            }

            PopulateDropDownListcatHCVademecum();

            return PartialView("VademecumCRUD", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatHCVademecum()
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatHCVademecum(catHCVademecumWS Datos)
        {
            if (Datos.vadAccion == "Modificar" || Datos.vadAccion == "Agregar")
            {
                Datos.vadDescripcion = Datos.vadDescripcion ?? "";
                Datos.vadCodigo = Datos.vadCodigo ?? "";
                if (Datos.vadCodigo.Trim().Length == 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Código incorrecto.",
                        Foco = "vadCodigo"
                    });
                }
                if (Datos.vadDescripcion.Trim().Length == 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar la descripción.",
                        Foco = "vadDescripcion"
                    });
                }
                Datos.vadDescripcion = Datos.vadDescripcion.ToUpper();
                var Consulta = context.catHCVademecum.SingleOrDefault(s => s.vadCodigo == Datos.vadCodigo && s.vadId != Datos.vadId);
                if (Consulta != null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "El código ingresado ya existe.",
                        Foco = "vadCodigo"
                    });
                }

                Consulta = context.catHCVademecum.SingleOrDefault(s => s.vadDescripcion == Datos.vadDescripcion && s.vadId != Datos.vadId);
                if (Consulta != null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "La Descripción ingresada ya existe.",
                        Foco = "vadCodigo"
                    });
                }
            }

            try
            {
                switch (Datos.vadAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catHCVademecum();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.vadAccion != "Agregar")
                        {
                            Registro = context.catHCVademecum.Single(s => s.vadId == Datos.vadId);
                        }

                        Registro.vadCodigo = Datos.vadCodigo;
                        Registro.vadDescripcion = Datos.vadDescripcion.ToUpper();

                        if (Datos.vadAccion == "Agregar")
                        {
                            context.catHCVademecum.AddObject(Registro);
                        }

                        break;
                    case "Eliminar":
                        var EliminarRegistro = context.catHCVademecum.Single(w => w.vadId == Datos.vadId);
                        context.catHCVademecum.DeleteObject(EliminarRegistro);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Vademecum", "HistoriaClinica", Datos.vadAccion, Datos.vadAccion + " Problema crónico " + Datos.vadDescripcion);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información de Problemas Crónicos en forma correcta.",
                    Foco = "vadDescripcion"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "vadDescripcion"
                });
            }
        }
    }
}