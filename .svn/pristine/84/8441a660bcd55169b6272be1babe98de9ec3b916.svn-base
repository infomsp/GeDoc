using System.Drawing.Imaging;
using System.IO;

namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Models;
    using System.Drawing;

    public partial class registroProfesionalController : Controller
    {
        public ActionResult rpEmisionCarnet()
        {
            return PartialView();
        }

        //Edición de datos
        public IList<catCarnetWS> AllcatCarnet()
        {
            return getDatoscatCarnet().ToList();
        }

        private IEnumerable<catCarnetWS> getDatoscatCarnet()
        {
            var _Datos = (from d in context.catCarnet.ToList()
                          select new catCarnetWS()
                                    {
                                        carId = d.carId,
                                        carNombre = d.carNombre,
                                        carNumeroDocumento = d.carNumeroDocumento,
                                        carApellido = d.carApellido,
                                        catMatriculaProfesional = d.catMatriculaProfesional,
                                        carFechaImpresion = d.carFechaImpresion,
                                        carVencimiento = d.carVencimiento,
                                        carCandidadImpresiones = d.carCandidadImpresiones,
                                        tipoIdTipoDocumento = d.tipoIdTipoDocumento,
                                        tipoDocumento = d.sisTipo.tipoDescripcion,
                                        Profesion = d.catProfesion.profNombre,
                                        carFoto = d.carFoto,
                                        profId = d.profId,
                                        carFecha = d.carFecha,
                                        carPersonalInterno = d.carPersonalInterno ?? false,
                                        repId = d.repId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _Binding_catCarnet()
        {
            PopulateDropDownListcatCarnet();

            return View(new GridModel<catCarnetWS>
            {
                Data = AllcatCarnet()
            });
        }

        [GridAction]
        public ActionResult getcatCarnet(string pAccion, Int64? carId)
        {
            var Datos = new catCarnetWS();
            Datos.carAccion = pAccion;
            Datos.carId = (int)carId;

            if(pAccion != "Agregar")
            {
                //Cargamos los datos de la oferta
                var DatosActuales = context.catCarnet.Single(w => w.carId == carId);

                Datos.carId = DatosActuales.carId;
                Datos.carApellido = DatosActuales.carApellido;
                Datos.carNombre = DatosActuales.carNombre;
                Datos.tipoIdTipoDocumento = DatosActuales.tipoIdTipoDocumento;
                Datos.carNumeroDocumento = DatosActuales.tipoIdTipoDocumento;
                Datos.profId = DatosActuales.profId;
                Datos.catMatriculaProfesional = DatosActuales.catMatriculaProfesional;
                Datos.carVencimiento = DatosActuales.carVencimiento;
                Datos.carFechaImpresion = DatosActuales.carFechaImpresion;
                Datos.carCandidadImpresiones = DatosActuales.carCandidadImpresiones;
                Datos.carFoto = DatosActuales.carFoto;
                Datos.carFecha = DatosActuales.carFecha;
                Datos.carPersonalInterno = DatosActuales.carPersonalInterno ?? false;
                Datos.repId = DatosActuales.repId;
            }

            PopulateDropDownListcatCarnet();

            return PartialView("rpEmisionCarnetCRUD", Datos);
        }

        //Datos para el dropdown
        public void PopulateDropDownListcatCarnet()
        {
            ViewData["tipoIdTipoDocumento_Lista_Data"] = (from x in context.sisTipo.Where(w => w.sisTipoEntidad.tipoeCodigo == "TD")
                                                          select new ListaDesplegableWS()
                                                          {
                                                              idLista = x.tipoId,
                                                              Texto = x.tipoDescripcion
                                                          }).ToList();

            ViewData["profId_Lista_Data"] = (from x in context.catProfesion.ToList()
                                                          select new ListaDesplegableWS()
                                                          {
                                                              idLista = x.profId,
                                                              Texto = x.profNombre
                                                          }).ToList();

            ViewData["repId_Lista_Data"] = (from x in context.catReparticion.ToList()
                                             select new ListaDesplegableWS()
                                             {
                                                 idLista = x.repId,
                                                 Texto = x.repNombre
                                             }).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult getValida(int pTipoDoc,string pDocumento)
        {
            var nroDNI = int.Parse(pDocumento);
            var Datos = context.catCarnet.SingleOrDefault(s => s.tipoIdTipoDocumento == pTipoDoc && s.carNumeroDocumento == nroDNI);

            if (Datos == null)
            {
                return Json(new
                {
                    Encontrado = false,
                    Datos = new catCarnetWS()
                });
            }

            return Json(new
            {
                Encontrado = Datos != null,
                Datos = new catCarnetWS()
                            {
                                carId = Datos.carId,
                                carNombre = Datos.carNombre,
                                carNumeroDocumento = Datos.carNumeroDocumento,
                                carApellido = Datos.carApellido,
                                catMatriculaProfesional = Datos.catMatriculaProfesional,
                                carFechaImpresion = Datos.carFechaImpresion,
                                carVencimiento = Datos.carVencimiento,
                                carCandidadImpresiones = Datos.carCandidadImpresiones,
                                tipoIdTipoDocumento = Datos.tipoIdTipoDocumento,
                                tipoDocumento = Datos.sisTipo.tipoDescripcion,
                                Profesion = Datos.catProfesion.profNombre,
                                carFoto = Datos.carFoto,
                                profId = Datos.profId,
                                carFecha = Datos.carFecha,
                                carCodigoQR = Datos.carCodigoQR,
                                carPersonalInterno = Datos.carPersonalInterno ?? false,
                                repId = Datos.repId
                            }
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setImpresionCarnet(int pIdCarnet)
        {
            try
            {
                var Datos = context.catCarnet.Single(s => s.carId == pIdCarnet);
                
                Datos.carCandidadImpresiones++;
                Datos.carFechaImpresion = DateTime.Now;

                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "OK"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setcatCarnet(catCarnetWS Datos)
        {
            if (Datos.carAccion == "Modificar" || Datos.carAccion == "Agregar")
            {
                Datos.carNombre = Datos.carNombre ?? "";
                if (Datos.carNombre.Trim().Length == 0)
                {
                    return Json(new
                        {
                            Error = true,
                            Mensaje = "Debe ingresar la descripción para el Televisor.",
                            Foco = "carNombre"
                        });
                }
            }

            try
            {
                switch (Datos.carAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var Registro = new catCarnet();

                        //Actualizamos los datos personales del paciente\\
                        if (Datos.carId > 0)
                        {
                            Registro = context.catCarnet.Single(s => s.carId == Datos.carId);
                        }
                        else
                        {
                            Registro.carFecha = DateTime.Now;
                        }

                        if (Datos.carFoto != null)
                        {
                            byte[] data = Convert.FromBase64String(Datos.carFoto);
                            MemoryStream ms = new MemoryStream(data);
                            Image returnImage = Image.FromStream(ms);
                            var NombreImagen = Datos.carNumeroDocumento + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                            string Archivo = Path.Combine(Server.MapPath("~/Content/Fotos/Profesionales"), NombreImagen);
                            returnImage.Save(Archivo, ImageFormat.Png);
                            Registro.carFoto = "~/Content/Fotos/Profesionales/" + NombreImagen;
                        }

                        if (Datos.carCodigoQR != null)
                        {
                            byte[] data = Convert.FromBase64String(Datos.carCodigoQR);
                            MemoryStream ms = new MemoryStream(data);
                            Image returnImage = Image.FromStream(ms);
                            var NombreImagen = "QR" + Datos.carNumeroDocumento + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                            string Archivo = Path.Combine(Server.MapPath("~/Content/Fotos/Profesionales"), NombreImagen);
                            returnImage.Save(Archivo, ImageFormat.Png);
                            Registro.carCodigoQR = "~/Content/Fotos/Profesionales/" + NombreImagen;
                        }

                        Registro.carApellido = Datos.carApellido.ToUpper();
                        Registro.carNombre = Datos.carNombre.ToUpper();
                        Registro.carNumeroDocumento = Datos.carNumeroDocumento;
                        Registro.tipoIdTipoDocumento = (short)Datos.tipoIdTipoDocumento;
                        Registro.profId = (short)Datos.profId;
                        Registro.catMatriculaProfesional = Datos.catMatriculaProfesional;
                        Registro.carVencimiento = Datos.carVencimiento;
                        Registro.carPersonalInterno = Datos.carPersonalInterno;
                        Registro.repId = Datos.repId;

                        if (Datos.carId <= 0)
                        {
                            context.catCarnet.AddObject(Registro);
                        }

                        break;
                    case "Eliminar":
                        var DeleteCompra = context.catCarnet.Single(w => w.carId == Datos.carId);
                        context.catCarnet.DeleteObject(DeleteCompra);
                        break;
                }

                new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "rpEmisionCarnet", "registroProfesional", Datos.carAccion, "Agrega carnet de " + Datos.carNombre.ToUpper() + " " + Datos.carApellido.ToUpper() + ", DNI: " + Datos.carNumeroDocumento);
                context.SaveChanges();

                //Obtenemos el ID del Carnet\\
                var _Info = context.catCarnet.Single(s => s.tipoIdTipoDocumento == Datos.tipoIdTipoDocumento && s.carNumeroDocumento == Datos.carNumeroDocumento);

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizó información del carnet en forma correcta.",
                    Foco = "carNombre",
                    IdCarnet = _Info.carId
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "carNombre"
                });
            }
        }
    }
}