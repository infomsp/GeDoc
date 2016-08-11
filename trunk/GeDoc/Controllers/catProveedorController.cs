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
        private efAccesoADatosEntities context = new efAccesoADatosEntities();
        //Edición de datos
        public IList<catProveedorWS> AllcatProveedor()
        {
            return getDatoscatProveedor().ToList();
        }

        private IEnumerable<catProveedorWS> getDatoscatProveedor()
        {
            var _Datos = (from d in context.catProveedor
                          select new catProveedorWS()
                                    {
                                        provCorreoElectronico = d.provCorreoElectronico,
                                        provCUIT = d.provCUIT,
                                        provRazonSocial = d.provRazonSocial,
                                        provDomicilio = d.provDomicilio,
                                        provId = d.provId,
                                        provTelefono = d.provTelefono,
                                        provNombreDeFantasia = d.provNombreDeFantasia,
                                        provRubro = d.provRubro
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catProveedor()
        {
            PopulateDropDownListcatProveedor();

            return View(new GridModel<catProveedorWS>
            {
                Data = AllcatProveedor()
            });
        }

        [GridAction]
        public ActionResult Index()
        {
            PopulateDropDownListcatProveedor();

            return PartialView();
        }

        [GridAction]
        public ActionResult getcatProveedor(string pAccion, Int64? provId)
        {
            var Datos = new catProveedorWS();
            Datos.provAccion = pAccion;
            Datos.provId = (int)provId;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catProveedor.Single(w => w.provId == provId);

                Datos.provId = DatosActuales.provId;
                Datos.provRazonSocial = DatosActuales.provRazonSocial;
                Datos.provCUIT = DatosActuales.provCUIT;
                Datos.provDomicilio = DatosActuales.provDomicilio;
                Datos.provTelefono = DatosActuales.provTelefono;
                Datos.provCorreoElectronico = DatosActuales.provCorreoElectronico;
                Datos.provNombreDeFantasia = DatosActuales.provNombreDeFantasia;
                Datos.provRubro = DatosActuales.provRubro;
            }

            PopulateDropDownListcatProveedor();

            return PartialView("CRUDProveedor", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatProveedor()
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatProveedor(catProveedorWS Datos)
        {
            if (Datos.provAccion == "Modificar" || Datos.provAccion == "Agregar")
            {
                Datos.provRazonSocial = Datos.provRazonSocial ?? "";
                Datos.provNombreDeFantasia = Datos.provNombreDeFantasia ?? "";
                Datos.provRubro = Datos.provRubro ?? "";
                if (Datos.provRazonSocial.Trim().Length < 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar la Razón Social.",
                            Foco = "provRazonSocial"
                        });
                }

                if (Datos.provNombreDeFantasia.Trim().Length < 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Nombre de Fantasía.",
                        Foco = "provNombreDeFantasia"
                    });
                }

                Datos.provCUIT = Datos.provCUIT ?? "";
                if (Datos.provCUIT.Trim().Length < 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Nº de CUIT.",
                        Foco = "provCUIT"
                    });
                }

                if (Datos.provRubro.Trim().Length < 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Rubro.",
                        Foco = "provRubro"
                    });
                }

                Datos.provDomicilio = Datos.provDomicilio ?? "";
                Datos.provTelefono = Datos.provTelefono ?? "";
                Datos.provCorreoElectronico = Datos.provCorreoElectronico ?? "";
            }

            try
            {
                switch (Datos.provAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catProveedor();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.provAccion != "Agregar")
                        {
                            Registro = context.catProveedor.Single(s => s.provId == Datos.provId);
                        }

                        Registro.provRazonSocial = Datos.provRazonSocial.ToUpper();
                        Registro.provNombreDeFantasia = Datos.provNombreDeFantasia.ToUpper();
                        Registro.provRubro = Datos.provRubro.ToUpper();
                        Registro.provCUIT = Datos.provCUIT;
                        Registro.provDomicilio = Datos.provDomicilio;
                        Registro.provTelefono = Datos.provTelefono;
                        Registro.provCorreoElectronico = Datos.provCorreoElectronico;

                        if (Datos.provAccion == "Agregar")
                        {
                            context.catProveedor.AddObject(Registro);
                        }
                        break;
                    case "Eliminar":
                        var DeleteRegistro = context.catProveedor.Single(w => w.provId == Datos.provId);
                        context.catProveedor.DeleteObject(DeleteRegistro);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catProveedor", Datos.provAccion, Datos.provAccion + " Proveedor ");
                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información del Proveedor en forma correcta.",
                    Foco = "provRazonSocial"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "provRazonSocial"
                });
            }
        }
    }
}