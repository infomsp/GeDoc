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
        public IList<catProveedorContactoWS> AllcatProveedorContacto(Int32? provId)
        {
            return getDatoscatProveedorContacto(provId).ToList();
        }

        private IEnumerable<catProveedorContactoWS> getDatoscatProveedorContacto(Int32? provId)
        {
            var _Datos = (from d in context.catProveedorContacto.Where(w => w.provId == provId).ToList()
                          select new catProveedorContactoWS()
                                    {
                                        pcApellidoyNombre = d.pcApellidoyNombre,
                                        pcCorreoElectronico = d.pcCorreoElectronico,
                                        pcTelefono = d.pcTelefono,
                                        pcId = d.pcId,
                                        pcprovId = d.provId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catProveedorContacto(Int32? provId)
        {
            provId = provId ?? 1;

            PopulateDropDownListcatProveedorContacto();

            return View(new GridModel<catProveedorContactoWS>
            {
                Data = AllcatProveedorContacto(provId)
            });
        }

        [GridAction]
        public ActionResult InformescatProveedorContacto()
        {
            PopulateDropDownListcatProveedorContacto();

            return PartialView("ProveedorContacto");
        }

        [GridAction]
        public ActionResult getcatProveedorContacto(string pAccion, Int64? pcId, Int64? provId)
        {
            var Datos = new catProveedorContactoWS();
            Datos.pcAccion = pAccion;
            Datos.pcId = (int)pcId;
            Datos.pcprovId = (int)provId;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catProveedorContacto.Single(w => w.pcId == pcId);

                Datos.pcTelefono = DatosActuales.pcTelefono;
                Datos.pcprovId = DatosActuales.provId;
                Datos.pcApellidoyNombre = DatosActuales.pcApellidoyNombre;
                Datos.pcCorreoElectronico = DatosActuales.pcCorreoElectronico;
            }

            PopulateDropDownListcatProveedorContacto();

            return PartialView("CRUDProveedorContacto", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatProveedorContacto()
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatProveedorContacto(catProveedorContactoWS Datos)
        {
            if (Datos.pcAccion == "Modificar" || Datos.pcAccion == "Agregar")
            {
                Datos.pcApellidoyNombre = Datos.pcApellidoyNombre ?? "";
                Datos.pcCorreoElectronico = Datos.pcCorreoElectronico ?? "";
                Datos.pcTelefono = Datos.pcTelefono ?? "";
                if (Datos.pcApellidoyNombre.Trim().Length == 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar el Apellido y Nombre.",
                            Foco = "pcApellidoyNombre"
                        });
                }
            }

            var InfoProveedor = context.catProveedor.Single(s => s.provId == Datos.pcprovId);
            try
            {
                switch (Datos.pcAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catProveedorContacto();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.pcAccion != "Agregar")
                        {
                            Registro = context.catProveedorContacto.Single(s => s.pcId == Datos.pcId);
                        }

                        Registro.pcApellidoyNombre = Datos.pcApellidoyNombre;
                        Registro.pcCorreoElectronico = Datos.pcCorreoElectronico;
                        Registro.pcTelefono = Datos.pcTelefono;
                        Registro.provId = Datos.pcprovId;

                        if (Datos.pcAccion == "Agregar")
                        {
                            context.catProveedorContacto.AddObject(Registro);
                        }
                        break;
                    case "Eliminar":
                        var DeleteCompra = context.catProveedorContacto.Single(w => w.pcId == Datos.pcId);
                        context.catProveedorContacto.DeleteObject(DeleteCompra);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catProveedor", Datos.pcAccion, Datos.pcAccion + " Contacto al Proveedor " + InfoProveedor.provRazonSocial + ", CUIT: " + InfoProveedor.provCUIT);
                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información de contacto en forma correcta.",
                    Foco = "pcApellidoyNombre"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "pcApellidoyNombre"
                });
            }
        }
    }
}