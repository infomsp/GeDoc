namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Filters;
    using Models;
    using System.IO;

    public partial class catPersonaController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditingDocumentacion(int perId)
        {
            return View(new GridModel(AllDocumentacion(perId)));
        }

        private IList<catPersonaDocumentacionWS> AllDocumentacion(int perId)
        {
            return getDatosDocumentacion(perId).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingDocumentacion(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            var _Result = context.catPersonaDocumentacion.FirstOrDefault(p => p.perdId == id);
            if (TryUpdateModel(_Result))
            {
                _Result.perdObservaciones = _Result.perdObservaciones == null ? "" : _Result.perdObservaciones;
                if (Session["upArchivo"] != null)
                {
                    _Result.perdArchivo = "(" + DateTime.Now.ToString("yyyyMMddhhmmss") + ")" + Session["upArchivo"];
                    System.IO.File.Move(Session["PathArchivos"] + "/" + Session["upArchivo"], Session["PathArchivos"] + "/" + _Result.perdArchivo);
                    Session["upArchivo"] = null;
                }
                EditDocumentacion(_Result);
            }

            return View(new GridModel(AllDocumentacion(_Result.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingDocumentacion()
        {
            var _Result = new catPersonaDocumentacion();

            if (TryUpdateModel(_Result))
            {
                _Result.perdObservaciones = _Result.perdObservaciones == null ? "" : _Result.perdObservaciones;
                if (Session["upArchivo"] != null)
                {
                    _Result.perdArchivo = "(" + DateTime.Now.ToString("yyyyMMddhhmmss") + ")" + Session["upArchivo"];
                    System.IO.File.Move(Session["PathArchivos"] + "/" + Session["upArchivo"], Session["PathArchivos"] + "/" + _Result.perdArchivo);
                    Session["upArchivo"] = null;
                }
                CreateDocumentacion(_Result);
            }

            return View(new GridModel(AllDocumentacion(_Result.perId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingDocumentacion(int id)
        {
            int perId = DeleteConfirmedDocumentacion(id);

            return View(new GridModel(AllDocumentacion(perId)));
        }

        private IEnumerable<catPersonaDocumentacionWS> getDatosDocumentacion(int perId)
        {
            var _Datos = (from d in context.catPersonaDocumentacion.Where(w => w.perId == perId).ToList()
                          orderby d.perdFecha descending
                          select new catPersonaDocumentacionWS()
                                    {
                                        usrId = d.usrId,
                                        tdId = d.tdId,
                                        perdTipo = d.catTipoDocumentacion.tdDescripcion,
                                        perId = d.perId,
                                        perdFecha = d.perdFecha,
                                        perdObservaciones = d.perdObservaciones,
                                        perdId = d.perdId,
                                        perdArchivo = d.perdArchivo,
                                        perdArchivoNombre = d.perdArchivo.Substring(16),
                                        perdVisualizar = d.perdArchivo.Contains(".pdf") || d.perdArchivo.Contains(".png") || d.perdArchivo.Contains(".bmp") || d.perdArchivo.Contains(".gif") || d.perdArchivo.Contains(".jpg") ? "<div onclick=\"onVerPDF('" + d.perdArchivo + "')\" title='Click para ver el archivo' style='cursor: pointer;' >" + d.perdArchivo.Substring(16) + "</div>" : "<a href='" + Url.Content("~/") + "catPersona/Descargar?Archivo=" + d.perdArchivo + "' title='Click para descargar archivo' style='cursor: pointer;' >" + d.perdArchivo.Substring(16) + "</a>",
                                        Usuario =
                                                    new sisUsuarios()
                                                    {
                                                        usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre
                                                    }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private void CreateDocumentacion(catPersonaDocumentacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.perdFecha = DateTime.Now;
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.catPersonaDocumentacion.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("perdObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }

            return;
        }

        private void EditDocumentacion(catPersonaDocumentacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.perdFecha = DateTime.Now;
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("perdObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
        }

        private int DeleteConfirmedDocumentacion(int id)
        {
            int perId = 0;
            try
            {
                catPersonaDocumentacion _Item = context.catPersonaDocumentacion.Single(x => x.perdId == id);
                context.catPersonaDocumentacion.DeleteObject(_Item);
                context.SaveChanges();
                perId = _Item.perId;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("perdObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

            return perId;
        }

        public FileResult Descargar(string Archivo)
        {
            var _Arch = File(Path.Combine(Session["PathArchivos"].ToString(), Archivo), "application/force-download", Archivo);

            return _Arch;
        }
    }
}