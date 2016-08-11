using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeDoc.Models;
using Telerik.Web.Mvc;
using GeDoc.Filters;

namespace GeDoc.Controllers
{ 
    public class catTurnoMotivoBloqueoController : Controller
    {
        private efAccesoADatosEntities db = new efAccesoADatosEntities();

        public PartialViewResult Index()
        {
            ViewBag.Catalogo = "Motivos para Bloquear Turnos";
            return PartialView();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing(int? _tmbId, string _tmbDescripcion) 
        {
            //int page = int.Parse(Request["page"]) - 1;
            //int size = int.Parse(Request["size"]);

            var _data = from a in db.catTurnoMotivoBloqueo
                        //.OrderBy(b => b.tmbId).Skip(page * size).Take(size)
                        select new catTurnoMotivoBloqueos
                        {
                            tmbId = a.tmbId,
                            tmbDescripcion = a.tmbDescripcion

                        };

            return View(new GridModel<catTurnoMotivoBloqueos>
            {
                Data = _data
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catTurnoMotivoBloqueo _Item = new catTurnoMotivoBloqueo();

            if (TryUpdateModel(_Item) && ModelState.IsValid)
            {
                db.catTurnoMotivoBloqueo.AddObject(_Item);
                db.SaveChanges();
            }

            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Nuevo Motivo", "ID: "+_Item.tmbId);
            //db.SaveChanges();

            return _SelectEditing(null,"");
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            if (!db.catTurno.Any<catTurno>(p => p.tmbId == id))
            {
                catTurnoMotivoBloqueo _Item = db.catTurnoMotivoBloqueo.Single(x => x.tmbId == id);
                db.catTurnoMotivoBloqueo.DeleteObject(_Item);
                db.SaveChanges();
                //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Borra Motivo", "ID: " + id);
                //db.SaveChanges();
            }          
            return _SelectEditing(null, "");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catTurnoMotivoBloqueo _Item = db.catTurnoMotivoBloqueo.Where(p => p.tmbId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            if (ModelState.IsValid)
            {
                db.SaveChanges();
            }
            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Edita Motivo", "ID: " + id);
            //db.SaveChanges();
            return _SelectEditing(null, "");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult isMotiveInUse(int tmbId)
        {
            return Json(db.catTurno.Any<catTurno>(p => p.tmbId == tmbId));
        }
    
    }
}