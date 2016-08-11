namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;

    public partial class catProveedorController : Controller
    {
        //Edición de datos
        public IList<catProveedorRubroWS> AllcatProveedorRubro(Int32? provId)
        {
            return getDatoscatProveedorRubro(provId).ToList();
        }

        private IEnumerable<catProveedorRubroWS> getDatoscatProveedorRubro(Int32? provId)
        {
            var _Datos = (from d in context.catProveedorRubro.Where(w => w.provId == provId).ToList()
                          select new catProveedorRubroWS()
                                    {
                                        prId = d.prId,
                                        prprovId = d.provId,
                                        prRubro = d.prRubro
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catProveedorRubro(Int32? provId)
        {
            provId = provId ?? 1;

            PopulateDropDownListcatProveedorRubro();

            return View(new GridModel<catProveedorRubroWS>
            {
                Data = AllcatProveedorRubro(provId)
            });
        }

        [GridAction]
        public ActionResult InformescatProveedorRubro()
        {
            PopulateDropDownListcatProveedorRubro();

            return PartialView("ProveedorRubro");
        }

        [GridAction]
        public ActionResult getcatProveedorRubro(string pAccion, Int64? prId, Int64? provId)
        {
            var Datos = new catProveedorRubroWS();
            Datos.prAccion = pAccion;
            Datos.prId = (int)prId;
            Datos.prprovId = (int)provId;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catProveedorRubro.Single(w => w.prId == prId);

                Datos.prprovId = DatosActuales.provId;
                Datos.prRubro = DatosActuales.prRubro;
            }

            PopulateDropDownListcatProveedorRubro();

            return PartialView("CRUDProveedorRubro", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatProveedorRubro()
        {
            ViewData["prRubro_Data"] = context.catRubro.Select(s => s.rubDescripcion);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatProveedorRubro(catProveedorRubroWS Datos)
        {
            if (Datos.prAccion == "Modificar" || Datos.prAccion == "Agregar")
            {
                Datos.prRubro = Datos.prRubro ?? "";
                if (Datos.prRubro.Trim().Length == 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar el Rubro.",
                            Foco = "prRubro"
                        });
                }
            }

            var InfoProveedor = context.catProveedor.Single(s => s.provId == Datos.prprovId);
            try
            {
                switch (Datos.prAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catProveedorRubro();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.prAccion != "Agregar")
                        {
                            Registro = context.catProveedorRubro.Single(s => s.prId == Datos.prId);
                        }

                        Registro.prRubro = Datos.prRubro;
                        Registro.provId = Datos.prprovId;

                        if (Datos.prAccion == "Agregar")
                        {
                            context.catProveedorRubro.AddObject(Registro);
                        }

                        //Reviso que si no existe el rubro, lo agrego
                        if (context.catRubro.Count(s => s.rubDescripcion == Datos.prRubro) == 0)
                        {
                            context.catRubro.AddObject(new catRubro() { rubDescripcion = Datos.prRubro });
                        }

                        break;
                    case "Eliminar":
                        var DeleteCompra = context.catProveedorRubro.Single(w => w.prId == Datos.prId);
                        context.catProveedorRubro.DeleteObject(DeleteCompra);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catProveedor", Datos.prAccion, Datos.prAccion + " Rubro al Proveedor " + InfoProveedor.provRazonSocial + ", CUIT: " + InfoProveedor.provCUIT);
                context.SaveChanges();

                var Rubros = "";
                foreach (var Rubro in context.catProveedorRubro.Where(w => w.provId == Datos.prprovId))
                {
                    Rubros += Rubro.prRubro + ", ";
                }
                var UpdateProveedor = context.catProveedor.Single(s => s.provId == Datos.prprovId);
                UpdateProveedor.provRubro = Rubros.Length == 0 ? "" : Rubros.Substring(0, Rubros.Length-2);

                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información de rubros en forma correcta.",
                    Foco = "prRubro"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "prRubro"
                });
            }
        }
    }
}