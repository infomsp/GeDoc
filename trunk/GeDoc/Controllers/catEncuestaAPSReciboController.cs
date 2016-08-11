using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;

namespace GeDoc.Controllers
{
    public class catEncuestaAPSReciboController : Controller
    {
        //
        // GET: /catEncuestaAPSRecibo/

        private readonly efAccesoADatosEntities _db = new efAccesoADatosEntities();

        public PartialViewResult Index()
        {
            ViewBag.Catalogo = "Recibos de Encuestas";
            ViewData["cszId_Data"] = new SelectList(_db.catCentroDeSaludZona, "cszId", "cszNombre");
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel<catEncuestaAPSRecibos>
            {
                Data = from t in _db.catEncuestaAPSRecibo
                       select new catEncuestaAPSRecibos
                       {
                           encReciboId = t.encReciboId,
                           encReciboObservacion = t.encReciboObservacion,
                           encReciboFecha = t.encReciboFecha,
                           cszId = t.cszId,
                           encReciboAnulado = t.encReciboAnulado,
                           cszNombre = t.catCentroDeSaludZona.cszNombre
                       },
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catEncuestaAPSRecibo item = new catEncuestaAPSRecibo();
            item.encReciboFecha = DateTime.Now;

            if (TryUpdateModel(item) && ModelState.IsValid)
            {
                _db.catEncuestaAPSRecibo.AddObject(item);
                _db.SaveChanges();
            }

            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Nuevo Motivo", "ID: "+_Item.tmbId);
            //db.SaveChanges();

            return _SelectEditing();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            return _SaveEditing(id,true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id, bool voidReceipt = false)
        {
            catEncuestaAPSRecibo item = _db.catEncuestaAPSRecibo.FirstOrDefault(p => p.encReciboId == id);
            item.encReciboAnulado = voidReceipt ? true : item.encReciboAnulado;
            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }
            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Edita Motivo", "ID: " + id);
            //db.SaveChanges();
            return _SelectEditing();
        }

    }
}
