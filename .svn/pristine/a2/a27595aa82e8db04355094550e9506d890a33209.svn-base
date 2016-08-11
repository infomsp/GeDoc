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
        public ActionResult _SelectEditingComentarios(int reqId)
        {
            return View(new GridModel(AllComentarios(reqId)));
        }

        private IList<catRequerimientoComentariosWS> AllComentarios(int reqId)
        {
            return getDatosComentarios(reqId).ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingComentarios(int id)
        {
            if (Session["UsuarioCentroDeSalud"] == null)
            {
                RedirectToAction("LogOff", "Account");
                return null;
            }
            catRequerimientoComentarios _Result = context.catRequerimientoComentarios.Where(p => p.reqcId == id).FirstOrDefault();
            if (TryUpdateModel(_Result))
            {
                Edit(_Result);
            }

            return View(new GridModel(AllComentarios(_Result.reqId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingComentarios()
        {
            catRequerimientoComentarios _Result = new catRequerimientoComentarios();

            if (TryUpdateModel(_Result))
            {
                Create(_Result);
            }

            return View(new GridModel(AllComentarios(_Result.reqId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingComentarios(int id)
        {
            int reqId = DeleteConfirmed(id);

            return View(new GridModel(AllComentarios(reqId)));
        }

        private IEnumerable<catRequerimientoComentariosWS> getDatosComentarios(int reqId)
        {
            var _Datos = (from d in context.catRequerimientoComentarios.ToList()
                          where d.reqId == reqId
                          orderby d.reqcFecha descending
                          select new catRequerimientoComentariosWS()
                                    {
                                        usrId = d.usrId,
                                        reqId = d.reqId,
                                        reqcComentario = d.reqcComentario,
                                        reqcFecha = d.reqcFecha,
                                        reqcId = d.reqcId,
                                        Usuario =
                                                    new sisUsuarios()
                                                    {
                                                        usrApellidoyNombre = d.sisUsuario.usrApellidoyNombre
                                                    }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        private void Create(catRequerimientoComentarios pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.reqcFecha = DateTime.Now;
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.catRequerimientoComentarios.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("reqcComentario", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }

            return;
        }

        private void Edit(catRequerimientoComentarios pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pItem.reqcFecha = DateTime.Now;
                    pItem.usrId = (Session["Usuario"] as sisUsuario).usrId;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("reqcComentario", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                }
            }
            return;
        }

        private int DeleteConfirmed(int id)
        {
            int reqId = 0;
            try
            {
                catRequerimientoComentarios _Item = context.catRequerimientoComentarios.Single(x => x.reqcId == id);
                context.catRequerimientoComentarios.DeleteObject(_Item);
                context.SaveChanges();
                reqId = _Item.reqId;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("reqcComentario", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

            return reqId;
        }

    }
}