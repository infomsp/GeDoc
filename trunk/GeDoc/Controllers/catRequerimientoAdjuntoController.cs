using System.Net.Mime;

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

    public partial class catRequerimientoController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditingAdjunto(int reqId)
        {
            return View(new GridModel(AllAdjunto(reqId)));
        }

        private IList<catRequerimientoAdjuntoWS> AllAdjunto(int reqId)
        {
            return getDatosAdjunto(reqId).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingAdjunto(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            catRequerimientoAdjunto _Result = context.catRequerimientoAdjunto.Where(p => p.reqaId == id).FirstOrDefault();
            if (TryUpdateModel(_Result))
            {
                if (Session["upArchivo"] != null)
                {
                    _Result.reqaObservaciones = _Result.reqaObservaciones == null ? "" : _Result.reqaObservaciones;
                    _Result.reqaArchivo = Session["upArchivo"].ToString();
                }
                EditAdjunto(_Result);
            }

            return View(new GridModel(AllAdjunto(_Result.reqId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingAdjunto()
        {
            catRequerimientoAdjunto _Result = new catRequerimientoAdjunto();

            if (TryUpdateModel(_Result))
            {
                _Result.reqaObservaciones = _Result.reqaObservaciones == null ? "" : _Result.reqaObservaciones;
                if (Session["upArchivo"] != null)
                {
                    _Result.reqaArchivo = Session["upArchivo"].ToString();
                }
                CreateAdjunto(_Result);
            }

            return View(new GridModel(AllAdjunto(_Result.reqId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingAdjunto(int id)
        {
            int reqId = DeleteConfirmedAdjunto(id);

            return View(new GridModel(AllAdjunto(reqId)));
        }

        private IEnumerable<catRequerimientoAdjuntoWS> getDatosAdjunto(int reqId)
        {
            var _Datos = (from d in context.catRequerimientoAdjunto.ToList()
                          where d.reqId == reqId
                          orderby d.reqaFecha descending
                          select new catRequerimientoAdjuntoWS()
                                    {
                                        usrId = d.usrId,
                                        reqId = d.reqId,
                                        reqaFecha = d.reqaFecha,
                                        reqaObservaciones = d.reqaObservaciones,
                                        reqaId = d.reqaId,
                                        reqaArchivo = d.reqaArchivo,
                                        Usuario =
                                                    new sisUsuarios()
                                                    {
                                                        usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre
                                                    }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private void CreateAdjunto(catRequerimientoAdjunto pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.reqaFecha = DateTime.Now;
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.catRequerimientoAdjunto.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("reqaObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }

            return;
        }

        private void EditAdjunto(catRequerimientoAdjunto pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.reqaFecha = DateTime.Now;
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("reqaObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
            return;
        }

        private int DeleteConfirmedAdjunto(int id)
        {
            int reqId = 0;
            try
            {
                catRequerimientoAdjunto _Item = context.catRequerimientoAdjunto.Single(x => x.reqaId == id);
                context.catRequerimientoAdjunto.DeleteObject(_Item);
                context.SaveChanges();
                reqId = _Item.reqId;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("reqaObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

            return reqId;
        }

        public FileResult Descargar(string Archivo)
        {
            //string Extencion = Archivo.Substring(Archivo.Length - 4);
            //string TipoArchivo = "";

            //switch (Extencion)
            //{
            //    case ".png":
            //        TipoArchivo = "image/png";
            //        break;
            //    case ".pdf":
            //        TipoArchivo = "application/pdf";
            //        break;
            //    case ".doc":
            //    case "docx":
            //        TipoArchivo = "application/msword";
            //        break;
            //    case ".xls":
            //    case "xlsx":
            //        TipoArchivo = "application/vnd.ms-excel";
            //        break;
            //    case ".ppt":
            //    case "pptx":
            //        TipoArchivo = "application/mspowerpoint";
            //        break;
            //}


            //byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(Session["PathArchivos"] + "/" + Archivo));
            //return File(fileBytes, MediaTypeNames.Application.Octet, Archivo);

            //return File(Server.MapPath(Session["PathArchivos"] + "/" + Archivo), TipoArchivo);
            //return File("E:\\Personal\\Proyectos\\MSPGeDoc\\GeDoc\\Content\\Archivos\\Requerimientos\\FINAL (1).pdf", TipoArchivo);

            //return File("<your path>" + ImageName, System.Net.Mime.MediaTypeNames.Application.Octet);
            //var _Arch =
            //    File("E:\\Personal\\Proyectos\\MSPGeDoc\\GeDoc\\Content\\Archivos\\Requerimientos\\FINAL (1).pdf",
            //         "application/force-download", Archivo);

            var _Arch = File(Path.Combine(Session["PathArchivos"].ToString(), Archivo), "application/force-download", Archivo);

            return _Arch;
        }
    }
}