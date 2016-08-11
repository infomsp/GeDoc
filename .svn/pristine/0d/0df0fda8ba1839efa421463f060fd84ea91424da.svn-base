using System.Linq;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public class catEncuestaAPSEncuestadorController : Controller
    {
        private readonly efAccesoADatosEntities _db = new efAccesoADatosEntities();
        



        public PartialViewResult Index()
        {
            ViewBag.Catalogo = "Motivos para Bloquear Turnos";
            ViewData["cszId_Data"] = new SelectList(_db.catCentroDeSaludZona, "cszId", "cszNombre");
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing()
        {
            
            var user = (Session["Usuario"] as GeDoc.Models.sisUsuario);
            if (user.esExterno==true)
            {
                var data = from a in _db.catEncuestaAPSEncuestador
                           where a.encuestadorEsExterno == true
                           select new catEncuestaAPSEncuestadores
                           {
                               encuestadorId = a.encuestadorId,
                               encuestadorApyNom = a.encuestadorApyNom,
                               encuestadorDoc = a.encuestadorDoc,
                               cszId = a.cszId,
                               repNombre = a.catCentroDeSaludZona.cszNombre

                           };

                return View(new GridModel<catEncuestaAPSEncuestadores>
                {
                    Data = data
                });
            }
            else
            {
                var data = from a in _db.catEncuestaAPSEncuestador
                           where (a.encuestadorEsExterno == null || a.encuestadorEsExterno == false)
                           select new catEncuestaAPSEncuestadores
                           {
                               encuestadorId = a.encuestadorId,
                               encuestadorApyNom = a.encuestadorApyNom,
                               encuestadorDoc = a.encuestadorDoc,
                               cszId = a.cszId,
                               repNombre = a.catCentroDeSaludZona.cszNombre

                           };

                return View(new GridModel<catEncuestaAPSEncuestadores>
                {
                    Data = data
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            var user = (Session["Usuario"] as GeDoc.Models.sisUsuario);

            catEncuestaAPSEncuestador item = new catEncuestaAPSEncuestador();

            if (user.esExterno==true)
            {
                item.encuestadorEsExterno = true;
            }

            if (TryUpdateModel(item) && ModelState.IsValid)
            {
                _db.catEncuestaAPSEncuestador.AddObject(item);
                _db.SaveChanges();
            }

            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Nuevo Motivo", "ID: "+_Item.tmbId);
            //db.SaveChanges();

            return _SelectEditing();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {   // VALIDAR SI EXISTE ALGUNA ENCUESTA CON ESTE ENCUESTADOR
            if (!_db.catEncuestaAPSReciboDetalle.Any(p => p.encuestadorId == id))
            {
                catEncuestaAPSEncuestador item = _db.catEncuestaAPSEncuestador.Single(x => x.encuestadorId == id);
                _db.catEncuestaAPSEncuestador.DeleteObject(item);
                _db.SaveChanges();
                //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Borra Motivo", "ID: " + id);
                //db.SaveChanges();
            }
            return _SelectEditing();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catEncuestaAPSEncuestador item = _db.catEncuestaAPSEncuestador.FirstOrDefault(p => p.encuestadorId == id);

            TryUpdateModel(item);

            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }
            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Edita Motivo", "ID: " + id);
            //db.SaveChanges();
            return _SelectEditing();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult IsEncuestadorInUse(int encuestadorId)
        {
            return Json(_db.catEncuestaAPSEncuestador.Any(p => p.encuestadorId == encuestadorId));
        }

    }
}