using System;
using System.Linq;
using System.Web.Mvc;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public class CatEvolucionController : Controller
    {
        private readonly efAccesoADatosEntities _context = new efAccesoADatosEntities();

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditing(int pacId)
        {
            return View(new GridModel<catEvoluciones>
            {
                Data = (from a in _context.catEvolucion
                       join b in _context.catEspecialidad on a.espId equals b.espId
                       join e in _context.catCentroDeSalud on a.csId equals e.csId
                       from c in _context.catPersona.Where(w => w.perId == a.perId).DefaultIfEmpty()
                       from d in _context.catPersonaContratados.Where(w => w.conId == a.conId).DefaultIfEmpty()
                       from f in _context.sisUsuario.Where(s => s.usrId == a.usrId)
                       where a.pacId == pacId
                       orderby a.evoFecha descending
                       select new catEvoluciones
                       {
                           pacId = a.pacId,
                           evoFecha = a.evoFecha,
                           evoDescripcion = a.evoDescripcion,
                           espId = a.espId,
                           perId = a.perId,
                           conId = a.conId,
                           espNombre = b.espNombre,
                           csNombre = e.csNombre,
                           usrApellidoyNombre = f.usrApellidoyNombre,
                           proApellidoYNombre = a.perId != null ? c.perApellidoyNombre : d.conApellidoyNombre,
                           Diagnosticos = _context.enlTurnoDiagnostico.Where(x => x.turId == a.turId).Join(_context.catDiagnostico, y => y.diagId, z => z.diagId, (y, z) => z.Descripcion),
                           Practicas = _context.enlTurnoPractica.Where(x => x.turId == a.turId).Join(_context.catPractica, y=>y.pracId,z => z.pracId,(y,z) => z.pracDescripcion)
                       }).AsEnumerable().OrderByDescending(o => o.evoFecha)
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void AddNewEntry(int pacId, short espId, int? proId, string evoDescripcion,long? turId)
        {
            var sisUsuariosCentroDeSalud = Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud;
            if (sisUsuariosCentroDeSalud != null)
            {
                var userCsId = sisUsuariosCentroDeSalud.csId;
                var userUsrId = (short?)sisUsuariosCentroDeSalud.usrId;

                catEvolucion newEntry = new catEvolucion
                {
                    pacId = pacId,
                    espId = espId,
                    perId = proId > 0 ? proId : null,
                    conId = proId < 0 ? proId*-1 : null,
                    evoFecha = DateTime.Now,
                    evoDescripcion = evoDescripcion,
                    csId = userCsId,
                    usrId = userUsrId,
                    turId = turId
                };

                _context.catEvolucion.AddObject(newEntry);
                _context.SaveChanges();
            }
            else
            {
                RedirectToAction("LogOff", "Account");
            }
        }


    }
}
