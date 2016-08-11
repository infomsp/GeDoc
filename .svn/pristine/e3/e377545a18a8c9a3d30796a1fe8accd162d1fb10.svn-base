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

    public partial class HistoriaClinicaController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditingcatHCAdjunto(int idPaciente)
        {
            return View(new GridModel(AllcatHCAdjunto(idPaciente)));
        }

        private IList<catHCAdjuntoWS> AllcatHCAdjunto(int idPaciente)
        {
            PopulateDropDownListcatHCAdjunto(idPaciente);

            return getDatoscatHCAdjunto(idPaciente).ToList();
        }

        protected void PopulateDropDownListcatHCAdjunto(int idPaciente)
        {
        }

        private IEnumerable<catHCAdjuntoWS> getDatoscatHCAdjunto(int idPaciente)
        {
            var _Datos = (from d in context.getListaDocumentacionHC(idPaciente)
                          orderby d.hcadjFecha descending
                          select new catHCAdjuntoWS()
                                    {
                                        usrId = d.usrId,
                                        hcadjpacId = (int)d.pacId,
                                        hcadjFecha = d.hcadjFecha,
                                        hcadjId = d.hcadjId,
                                        hcadjArchivo = d.hcadjArchivo,
                                        hcadjArchivoNombre = d.hcadjArchivo,
                                        hcadjVisualizar = d.hcadjArchivo.Contains(".pdf") || d.hcadjArchivo.Contains(".png") || d.hcadjArchivo.Contains(".bmp") || d.hcadjArchivo.Contains(".gif") || d.hcadjArchivo.Contains(".jpg") ? "<div onclick=\"onVerPDF('" + d.hcadjArchivo + "')\" title='Click para ver el archivo' style='cursor: pointer;' >" + d.hcadjArchivo.Substring(16) + "</div>" : "<a href='" + Url.Content("~/") + "HistoriaClinica/DescargarAdjunto?Archivo=" + d.hcadjArchivo + "' title='Click para descargar archivo' style='cursor: pointer;' >" + d.hcadjArchivo.Substring(16) + "</a>",
                                        Usuario =
                                                    new sisUsuarios()
                                                    {
                                                        usrApellidoyNombre = d.usrApellidoyNombre
                                                    },
                                        hcadjObservaciones = d.hcadjObservaciones
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
                        physicalPath = Path.Combine(Server.MapPath("~/Content/Archivos/HC"), fName);

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
        public ActionResult getHCDocumentacion(string Accion, Int64? hcadjId, Int64? idPaciente)
        {
            var _Datos = new catHCAdjuntoWS();
            _Datos.hcadjAccion = Accion;
            _Datos.hcadjpacId = (int)idPaciente;
            _Datos.hcadjId = (int)hcadjId;

            if (Accion == "Agregar")
            {
                _Datos.hcadjFecha = DateTime.Now;
            }
            else
            {
                //Cargamos los datos Adjuntos
                var DatosActuales = context.catHCAdjunto.Single(w => w.hcadjId == hcadjId);

                _Datos.hcadjFecha = DatosActuales.hcadjFecha;
                _Datos.hcadjArchivo = DatosActuales.hcadjArchivo;
                _Datos.hcadjFecha = DatosActuales.hcadjFecha;
                _Datos.hcadjArchivo = DatosActuales.hcadjArchivo;
                _Datos.hcadjObservaciones = DatosActuales.hcadjObservaciones;
            }

            PopulateDropDownListcatHCAdjunto((int)idPaciente);

            return PartialView("hcDocumentacionCRUD", _Datos);
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
        public ActionResult setHCDocumentacion(catHCAdjuntoWS Datos)
        {
            var _PathDocumentacion = Server.MapPath("~/Content/Archivos/HC");
            if (Datos.hcadjAccion == "Modificar" || Datos.hcadjAccion == "Agregar")
            {
            }

            try
            {
                switch (Datos.hcadjAccion)
                {
                    case "Agregar":
                    case "Modificar":
                        var NewRegistro = new catHCAdjunto();

                        if (Datos.hcadjAccion == "Agregar")
                        {
                            NewRegistro.hcadjFecha = DateTime.Now;
                        }
                        else
                        {
                            NewRegistro = context.catHCAdjunto.Single(w => w.hcadjId == Datos.hcadjId);
                        }

                        NewRegistro.usrId = (Session["Usuario"] as sisUsuario).usrId;
                        NewRegistro.pacId = Datos.hcadjpacId;
                        NewRegistro.hcadjObservaciones = (Datos.hcadjObservaciones ?? "").ToUpper();

                        if (Session["upArchivo"] != null)
                        {
                            NewRegistro.hcadjArchivo = "(" + DateTime.Now.ToString("yyyyMMddhhmmss") + ")" + Session["upArchivo"];
                            System.IO.File.Move(_PathDocumentacion + "/" + Session["upArchivo"], _PathDocumentacion + "/" + NewRegistro.hcadjArchivo);
                            Session["upArchivo"] = null;
                        }

                        if (Datos.hcadjAccion == "Agregar")
                        {
                            context.catHCAdjunto.AddObject(NewRegistro);
                        }
                        break;
                    case "Eliminar":
                        var DeleteRegistro = context.catHCAdjunto.Single(w => w.hcadjId == Datos.hcadjId);
                        context.catHCAdjunto.DeleteObject(DeleteRegistro);
                        break;
                }

                context.SaveChanges();

                return Json(new
                {
                    Error = false,
                    Mensaje = "Se actualizaron los datos de la documentación en forma correcta",
                    Foco = "hcadjObservaciones"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message,
                    Foco = "hcadjObservaciones"
                });
            }
        }

        public FileResult DescargarAdjunto(string Archivo)
        {
            var _Arch = File(Path.Combine(Server.MapPath("~/Content/Archivos/HC"), Archivo), "application/force-download", Archivo);

            return _Arch;
        }
    }
}