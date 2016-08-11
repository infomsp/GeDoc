using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public class catEncuestaAPSReciboDetalleController : Controller
    {
        private readonly efAccesoADatosEntities _db = new efAccesoADatosEntities();
        
        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult Index(string _item = "")
        {
            if (_item.Length > 0)
            {
                ViewData["recibo"] = new JavaScriptSerializer().Deserialize<catEncuestaAPSRecibos>(_item);
            }
            ViewData["cszId"] = new SelectList(_db.catCentroDeSaludZona, "cszId", "cszNombre");
            ViewData["encuestadorId_Data"] = new SelectList(_db.catEncuestaAPSEncuestador, "encuestadorId", "encuestadorApyNom");
            ViewData["depId_Data"] = new SelectList(_db.catDepartamento.Where(x => x.provId.Equals(17)), "depId", "depNombre");
            ViewData["FiltroContains"] = true;
            return PartialView();         
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void UpdateReciboId(int p_encReciboId)
        {
            ViewData["encReciboId"] = p_encReciboId;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing(int? encReciboId)
        {
            if (encReciboId == null) return null;

            return View(new GridModel<catEncuestaAPSReciboDetalles>
            {
                Data = from t in _db.catEncuestaAPSReciboDetalle
                       where t.encReciboId == encReciboId
                       select new catEncuestaAPSReciboDetalles
                       {
                           encReciboDetalleId = t.encReciboDetalleId,
                           encReciboId = t.encReciboId,
                           cantidad = t.cantidad,
                           encuestadorId = t.encuestadorId,
                           depId = t.depId,
                           depNombre = t.catDepartamento.depNombre,
                           encuestadorApyNom = t.catEncuestaAPSEncuestador.encuestadorApyNom                         
                       }
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing(int encReciboId)
        {
            catEncuestaAPSReciboDetalle item = new catEncuestaAPSReciboDetalle();
            item.encReciboId = encReciboId;
            if (TryUpdateModel(item) && ModelState.IsValid)
            {
                _db.catEncuestaAPSReciboDetalle.AddObject(item);
                _db.SaveChanges();
            }

            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Nuevo Motivo", "ID: "+_Item.tmbId);
            //db.SaveChanges();

            return _SelectEditing(encReciboId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            catEncuestaAPSReciboDetalle item = _db.catEncuestaAPSReciboDetalle.Single(x => x.encReciboDetalleId == id);
            _db.catEncuestaAPSReciboDetalle.DeleteObject(item);
            _db.SaveChanges();
            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Borra Motivo", "ID: " + id);
            //db.SaveChanges();

            return _SelectEditing(item.encReciboId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catEncuestaAPSReciboDetalle item = _db.catEncuestaAPSReciboDetalle.FirstOrDefault(p => p.encReciboDetalleId == id);

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }
            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Edita Motivo", "ID: " + id);
            //db.SaveChanges();
            return _SelectEditing(item.encReciboId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PopulateDepartmentDropDownList(int cszId)
        {
            var data = from a in _db.enlZonaDepartamento
                join b in _db.catDepartamento on a.depId equals b.depId
                where a.cszId == cszId
                select new
                {
                    Text = b.depNombre,
                    Value = b.depId
                };

            return Json(data);
        }


    }
}
