namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using System.Web;
    using Models;
    using System.IO;

    public partial class proCompraController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditingproCompraDocumentacion(int comId)
        {
            return View(new GridModel(AllproCompraDocumentacion(comId)));
        }

        private IList<proCompraDocumentacionWS> AllproCompraDocumentacion(int comId)
        {
            PopulateDropDownListproCompraDocumentacion(comId);

            return getDatosproCompraDocumentacion(comId).ToList();
        }

        protected void PopulateDropDownListproCompraDocumentacion(int comId)
        {
            var _Proveedores = (from p in context.catProveedor
                                //join c in context.proCompraOferta on p.provCUIT equals c.comoProveedorCUIT
                                //where c.comId == comId
                                orderby p.provRazonSocial ascending
                                select new catProveedorWS()
                                {
                                    provId = p.provId,
                                    provRazonSocial = p.provRazonSocial
                                }).ToList();
            ViewData["provId_Data"] = new SelectList(_Proveedores, "provId", "provRazonSocial");
        }

        private IEnumerable<proCompraDocumentacionWS> getDatosproCompraDocumentacion(int comId)
        {
            var _Datos = (from d in context.getListaDocumentacionCompra(comId)
                          orderby d.comaFecha descending
                          select new proCompraDocumentacionWS()
                                    {
                                        usrId = d.usrId,
                                        comacomId = d.comId,
                                        comaFecha = d.comaFecha,
                                        comaId = d.comaId,
                                        comaArchivo = d.comaArchivo,
                                        comaArchivoNombre = d.comaArchivo,
                                        Usuario =
                                                    new sisUsuarios()
                                                    {
                                                        usrApellidoyNombre = d.usrApellidoyNombre
                                                    },
                                        comaFechaDeRetiro = d.comaFechaDeRetiro,
                                        comaPlazoDeEntrega = d.comaPlazoDeEntrega,
                                        comaFechaDeEntrega = d.comaFechaDeEntrega,
                                        provId = d.provId,
                                        Proveedor = d.provRazonSocial,
                                        Vencido = d.Vencido,
                                        comaFechaDeVencimiento = d.FechaDeVencimiento,
                                        comaRenglones = d.comaRenglones
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Descargar()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                string physicalPath = "";
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        fName = Path.GetFileName(file.FileName);
                        physicalPath = Path.Combine(Server.MapPath("~/Content/Archivos/Contable/Documentacion"), fName);

                        Session["upArchivo"] = fName;
                        // The files are not actually saved in this demo
                        file.SaveAs(physicalPath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falló la carga del archivo");
            }

            if (isSavedSuccessfully)
            {
                //throw new Exception(fName);
                return Json(new { Message = fName });
            }
            else
            {
                throw new Exception("Error al guardar el Archivo");
            }
        }

        [GridAction]
        public ActionResult getCompraDocumentacion(string Accion, Int64? comaId, Int64? idCompra)
        {
            var _Datos = new proCompraDocumentacionWS();
            _Datos.comaAccion = Accion;
            _Datos.comacomId = (int)idCompra;
            _Datos.comaId = (int)comaId;

            if (Accion == "Agregar")
            {
                _Datos.comaFecha = DateTime.Now;
                _Datos.comaFechaDeEntrega = DateTime.Now;
                _Datos.comaFechaDeRetiro = DateTime.Now;
            }
            else
            {
                //Cargamos los datos Adjuntos
                var DatosActuales = context.proCompraAdjunto.Single(w => w.comaId == comaId);

                _Datos.comaFecha = DatosActuales.comaFecha;
                _Datos.provId = DatosActuales.provId;
                _Datos.comaArchivo = DatosActuales.comaArchivo;
                _Datos.comaFecha = DatosActuales.comaFecha;
                _Datos.comaPlazoDeEntrega = DatosActuales.comaPlazoDeEntrega;
                _Datos.comaFechaDeRetiro = DatosActuales.comaFechaDeRetiro;
                _Datos.comaArchivo = DatosActuales.comaArchivo;
                _Datos.comaFechaDeEntrega = DatosActuales.comaFechaDeEntrega;
                _Datos.comaRenglones = DatosActuales.comaRenglones;
            }

            PopulateDropDownListproCompraDocumentacion((int)idCompra);

            return PartialView("CRUDproCompraDocumentacion", _Datos);
        }

        public ActionResult GetAttachments(string Archivo)
        {
            //Get the images list from repository
            var attachmentsList = new List<AttachmentsModel>
            {
                new AttachmentsModel {AttachmentID = 1, FileName = Archivo, Path = Server.MapPath("~/Content/Archivos/Contable/Documentacion") + "/" + Archivo},
            }.ToList();

            return Json(new { Data = attachmentsList }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult setCompraDocumentacion(proCompraDocumentacionWS Datos)
        {
            var _PathDocumentacion = Server.MapPath("~/Content/Archivos/Contable/Documentacion");
            if (Datos.comaAccion == "Modificar" || Datos.comaAccion == "Agregar")
            {
                if (Datos.provId == null || Datos.provId == 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar el Proveedor.",
                        Foco = "provId"
                    });
                }

                if (Datos.comaPlazoDeEntrega <= 0)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "El plazo de Entrega es incorrecto.",
                        Foco = "comaPlazoDeEntrega"
                    });
                }
            }else if (Datos.comaAccion == "Recepción")
            {
                if (Datos.comaFechaDeEntrega == null)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "Debe ingresar la fecha de Recepción.",
                        Foco = "comaFechaDeEntrega"
                    });
                }

                if (Datos.comaFechaDeEntrega.Value.Date > DateTime.Now.Date)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "La fecha de Recepción es incorrecta, no puede ser mayor a la fecha actual.",
                        Foco = "comaFechaDeEntrega"
                    });
                }

                if (Datos.comaFechaDeRetiro.Date > Datos.comaFechaDeEntrega.Value.Date)
                {
                    return Json(new
                    {
                        Error = true,
                        Mensaje = "La fecha de Recepción es incorrecta, no puede ser menor a la fecha de Retiro.",
                        Foco = "comaFechaDeEntrega"
                    });
                }
            }

            try
            {
                switch (Datos.comaAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var NewCompra = new proCompraAdjunto();

                        if (Datos.comaAccion == "Agregar")
                        {
                            NewCompra.comaFecha = DateTime.Now;
                        }
                        else
                        {
                            NewCompra = context.proCompraAdjunto.Single(w => w.comaId == Datos.comaId);
                        }

                        NewCompra.provId = Datos.provId;
                        NewCompra.usrId = (Session["Usuario"] as sisUsuario).usrId;
                        NewCompra.comId = Datos.comacomId;
                        NewCompra.comaFechaDeRetiro = Datos.comaFechaDeRetiro;
                        NewCompra.comaPlazoDeEntrega = (short)Datos.comaPlazoDeEntrega;
                        NewCompra.comaFechaDeEntrega = Datos.comaFechaDeEntrega;
                        NewCompra.comaRenglones = Datos.comaRenglones;

                        if (Session["upArchivo"] != null)
                        {
                            NewCompra.comaArchivo = "(" + DateTime.Now.ToString("yyyyMMddhhmmss") + ")" + Session["upArchivo"];
                            System.IO.File.Move(_PathDocumentacion + "/" + Session["upArchivo"], _PathDocumentacion + "/" + NewCompra.comaArchivo);
                            Session["upArchivo"] = null;
                        }

                        if (Datos.comaAccion == "Agregar")
                        {
                            context.proCompraAdjunto.AddObject(NewCompra);
                        }
                        break;
                    case "Eliminar":
                        var DeleteCompra = context.proCompraAdjunto.Single(w => w.comaId == Datos.comaId);
                        context.proCompraAdjunto.DeleteObject(DeleteCompra);
                        break;
                    case "Recepción":
                        var RecepcionCompra = context.proCompraAdjunto.Single(w => w.comaId == Datos.comaId);
                        RecepcionCompra.comaFechaDeEntrega = Datos.comaFechaDeEntrega;

                        if (context.proCompraAdjunto.Count(w => w.comId == RecepcionCompra.comId && w.comaId != Datos.comaId && w.comaFechaDeEntrega == null) == 0)
                        {
                            var Compra = context.proCompra.Single(w => w.comId == RecepcionCompra.comId);
                            var _tipoId =
                                context.sisTipo.First(w => w.sisTipoEntidad.tipoeCodigo == "CM" && w.tipoCodigo == "PA")
                                    .tipoId;

                            Compra.tipoIdEstado = _tipoId;
                            context.proCompraEstado.AddObject(new proCompraEstado()
                            {
                                comeFecha = DateTime.Now,
                                comId = RecepcionCompra.comId,
                                tipoId = _tipoId,
                                usrId = (Session["Usuario"] as sisUsuario).usrId,
                                comeObservaciones = ""
                            });
                        }

                        break;
                }

                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizaron los datos de la documentación en forma correcta",
                    Foco = "comaPlazoDeEntrega"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "comaPlazoDeEntrega"
                });
            }
        }

        public FileResult DescargarAdjunto(string Archivo)
        {
            var _Arch = File(Path.Combine(Server.MapPath("~/Content/Archivos/Contable/Documentacion"), Archivo), "application/force-download", Archivo);

            return _Arch;
        }
    }
}