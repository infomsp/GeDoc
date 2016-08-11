using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeDoc.Filters;
using GeDoc.Models;
using Telerik.Web.Mvc;

namespace GeDoc.Controllers
{
    public partial class catEncuestaAPSController : Controller
    {
        public ActionResult PacienteEnfermedades(int encPerId)
        {
            ViewData["enfId_Data"] = new SelectList(context.catEncuestaAPSEnfermedad.OrderBy(a => a.enfDescripcion), "enfId", "enfDescripcion");
            ViewData["encPerId"] = encPerId;
            return PartialView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReloadIllnessCombo(int? pencPerId)
        {
            var posData = context.enlEncuestaAPSPersonaEnfermedad.Where(c => c.encPerId == pencPerId) ;
            var data = from a in context.catEncuestaAPSEnfermedad
                       join b in posData on a.enfId equals b.enfId into bs
                       from b in bs.DefaultIfEmpty() 
                       where b.enfId == null                      
                       select new catEncuestaAPSEnfermedades
                       {
                           enfId = a.enfId,
                           enfDescripcion = a.enfDescripcion
                       };
            return Json(new SelectList(data, "enfId", "enfDescripcion"));
        }

        //diagnosticos
        //[GridAction]
        //public ActionResult _SelectEditingTiposEnfermedades()
        //{
        //    return View(new GridModel(AllTiposEnfermedades()));
        //}

        //public IList<catEncuestaAPSEnfermedades> AllTiposEnfermedades()
        //{
        //    return getTiposEnfermedades().ToList();
        //}

        //[GridAction]
        //public IEnumerable<catEncuestaAPSEnfermedades> getTiposEnfermedades()
        //{
        //    var _Item = (from d in context.catEncuestaAPSEnfermedad.ToList()
        //                 select new catEncuestaAPSEnfermedades
        //                 {
        //                     enfId = d.enfId,
        //                     enfCodigo = d.enfCodigo,
                             
        //                     enfDescripcion = d.enfDescripcion                            
        //                 }).ToList();
        //    return _Item.AsEnumerable();

        //}
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditingenlEncuestasAPSPersonasEnf(int _encPerId)
        {
            return View(new GridModel<enlEncuestaAPSPersonasEnfermedades>
            {
                Data = from d in context.enlEncuestaAPSPersonaEnfermedad
                              join x in context.catEncuestaAPSEnfermedad on d.enfId equals x.enfId
                              where d.encPerId == _encPerId
                              select new enlEncuestaAPSPersonasEnfermedades()
                              {
                                  enfId = (short)d.enfId,
                                  encPerId = d.encPerId,
                                  encEnfId = d.encEnfId,
                                  encEnfDescripcionComentario = d.encEnfDescripcionComentario,
                                  enfDescripcion = d.catEncuestaAPSEnfermedad.enfDescripcion
                              }
            });
        }

        //public IList<enlEncuestaAPSPersonasEnfermedades> AllenlEncuestasAPSPersonasEnf(int encPerId)
        //{
        //    return getDatosenlEncuestasAPSPersonasEnf(encPerId).ToList();
        //}

        private IEnumerable<enlEncuestaAPSPersonasEnfermedades> getDatosenlEncuestasAPSPersonasEnf(int encPerId)
        {
            var _Datos = (from d in context.enlEncuestaAPSPersonaEnfermedad.ToList()
                          join x in context.catEncuestaAPSEnfermedad on d.enfId equals x.enfId
                          where d.encPerId == encPerId
                          && encPerId > -1
                          select new enlEncuestaAPSPersonasEnfermedades()
                          {
                              enfId = (short)d.enfId,
                              encPerId = d.encPerId,
                              encEnfId = d.encEnfId,
                              encEnfDescripcionComentario = d.encEnfDescripcionComentario,
                              enfDescripcion = d.catEncuestaAPSEnfermedad.enfDescripcion
                          }).ToList();
            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingenlEncuestasAPSPersonasEnf()
        {
            enlEncuestaAPSPersonaEnfermedad _Item = new enlEncuestaAPSPersonaEnfermedad();
            if (TryUpdateModel(_Item))
            {
                context.enlEncuestaAPSPersonaEnfermedad.AddObject(_Item);
                context.SaveChanges();
            }
            return _SelectEditingenlEncuestasAPSPersonasEnf((int) _Item.encPerId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteenlEncuestasAPSPersonasEnf(int id)
        {
            enlEncuestaAPSPersonaEnfermedad _Item = context.enlEncuestaAPSPersonaEnfermedad.Single(x => x.encEnfId == id);
            int encPerId = (int)_Item.encPerId;
            context.enlEncuestaAPSPersonaEnfermedad.DeleteObject(_Item);
            context.SaveChanges();
            return _SelectEditingenlEncuestasAPSPersonasEnf(encPerId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveenlEncuestasAPSPersonasEnf(int id)
        {
            enlEncuestaAPSPersonaEnfermedad _Item = context.enlEncuestaAPSPersonaEnfermedad.Single(x => x.encEnfId == id);
            int encPerId = (int)_Item.encPerId;

            if (TryUpdateModel(_Item) && ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return _SelectEditingenlEncuestasAPSPersonasEnf(encPerId);
        }

        private int DeleteConfirmedenlEncuestasAPSPersonasEnfxPersona(int _encPerId)
        {
            context.ExecuteStoreCommand("delete enlEncuestaAPSPersonaEnfermedad where encPerId = " + _encPerId);
            return (short)_encPerId;
        }
         
    }
}
