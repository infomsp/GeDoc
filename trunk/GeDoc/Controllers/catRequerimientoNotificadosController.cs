using System;

namespace GeDoc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;

    public partial class catRequerimientoController : Controller
    {
        [GridAction]
        public ActionResult _BindingNotificados(int reqId)
        {
            return View(new GridModel(AllNotificados(reqId)));
        }

        private IList<catRequerimientoNotificaWS> AllNotificados(int reqId)
        {
            return getDatosNotificados(reqId).ToList();
        }

        private IEnumerable<catRequerimientoNotificaWS> getDatosNotificados(int reqId)
        {
            var _Datos = (from d in context.spGetRequerimientosNotificados(reqId)
                          orderby d.usrApellidoyNombre ascending
                          select new catRequerimientoNotificaWS()
                                    {
                                        usrId = d.usrId,
                                        reqId = d.reqId,
                                        requId = d.requId,
                                        requNotificar = d.requNotificar,
                                        Usuario =
                                                    new sisUsuarios()
                                                    {
                                                        usrApellidoyNombre = d.usrApellidoyNombre,
                                                        usrEmail = d.usrEmail
                                                    }
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Notificar(int requId, int Notifica)
        {
            try
            {
                var Item = context.catRequerimientoNotificaUsuario.Single(s => s.requId == requId);
                Item.requNotificar = (Notifica == 1);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(false);
            }

            return Json(true);
        }

    }
}