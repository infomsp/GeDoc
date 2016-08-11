using System.Linq;
using System.Web.Mvc;
using GeDoc.Models;

namespace GeDoc.Controllers
{
    public partial class catEncuestaAPSController : Controller
    {
        private readonly efAccesoADatosEntities _db = new efAccesoADatosEntities();

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult Redes()
        {
            ViewData["redes_categorias"] = _db.catEncuestaAPSRedesCategoria.Select(r => r); ;
            ViewData["redes_cuestionario"] = _db.catEncuestaAPSRedesCuestionario.Select(r => r);
            ViewData["redes_ponderacion"] = _db.catEncuestaAPSRedesPonderacion.Select(r=>r);           
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RedesLoad(int? encPerId)
        {
            if (encPerId == null) return null;

            return Json(
                from t in _db.catEncuestaAPSRedes
                where t.encPerId == encPerId
                select new catEncuestaAPSRedesS
                {
                    encPerId = t.encPerId,
                    encRedesRespuesta = t.encRedesRespuesta,
                    encRedesPuntaje = t.encRedesPuntaje
                }
            );
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void RedesSave(int encPerId, long? encRedesRespuesta, short? encRedesPuntaje)
        {
            catEncuestaAPSRedes item = _db.catEncuestaAPSRedes.FirstOrDefault(p => p.encPerId == encPerId);
            bool newItem = false;
            if (item == null)
            {
                item = new catEncuestaAPSRedes
                        {
                            encPerId = encPerId,
                            encRedesRespuesta = encRedesRespuesta,
                            encRedesPuntaje = encRedesPuntaje
                        };
                newItem = true;
            }
                                       
            if (TryUpdateModel(item) && ModelState.IsValid)
            {
                if(newItem)
                    _db.catEncuestaAPSRedes.AddObject(item);
                _db.SaveChanges();
            }
            
            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurnoMotivoBloqueo", "Edita Motivo", "ID: " + id);
            //db.SaveChanges();
        }
       
    }
}
