namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Filters;
    using Models;
    using System.Transactions;
    using System.Data.Objects;

    public partial class catRequerimientoController : Controller
    {
        [GridAction]
        public ActionResult _SelectEditingLogWork(int reqId)
        {
            return View(new GridModel(AllLogWork(reqId)));
        }

        private IList<catRequerimientoLogWorkWS> AllLogWork(int reqId)
        {
            return getDatosLogWork(reqId).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingLogWork(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            catRequerimientoLogTrabajo _Result = context.catRequerimientoLogTrabajo.Where(p => p.reqlId == id).FirstOrDefault();
            if (TryUpdateModel(_Result))
            {
                EditLogWork(_Result);
            }

            return View(new GridModel(AllLogWork(_Result.reqId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingLogWork()
        {
            catRequerimientoLogTrabajo _Result = new catRequerimientoLogTrabajo();

            if (TryUpdateModel(_Result))
            {
                CreateLogWork(_Result);
            }

            return View(new GridModel(AllLogWork(_Result.reqId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingLogWork(int id)
        {
            int reqId = DeleteConfirmedLogWork(id);

            return View(new GridModel(AllLogWork(reqId)));
        }

        private IEnumerable<catRequerimientoLogWorkWS> getDatosLogWork(int reqId)
        {
            var _Datos = (from d in context.catRequerimientoLogTrabajo.ToList()
                          where d.reqId == reqId
                          orderby d.reqlFecha descending
                          select new catRequerimientoLogWorkWS()
                                    {
                                        usrId = d.usrId,
                                        reqId = d.reqId,
                                        reqlTiempo = d.reqlTiempo,
                                        reqlFecha = d.reqlFecha,
                                        reqlObservaciones = d.reqlObservaciones,
                                        reqlId = d.reqlId,
                                        Usuario =
                                                    new sisUsuarios()
                                                    {
                                                        usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre
                                                    }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private void CreateLogWork(catRequerimientoLogTrabajo pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.catRequerimientoLogTrabajo.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("reqlObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }

            return;
        }

        private void EditLogWork(catRequerimientoLogTrabajo pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("reqlObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
            return;
        }

        private int DeleteConfirmedLogWork(int id)
        {
            int reqId = 0;
            try
            {
                catRequerimientoLogTrabajo _Item = context.catRequerimientoLogTrabajo.Single(x => x.reqlId == id);
                context.catRequerimientoLogTrabajo.DeleteObject(_Item);
                context.SaveChanges();
                reqId = _Item.reqId;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("reqlObservaciones", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

            return reqId;
        }

    }
}