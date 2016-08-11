
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;
namespace GeDoc.Controllers
{
    public partial class catEncuestaSinAdmisionPlanillaController : Controller
    {
        //Edición de datos
        public IList<catEncuestaSinadmisionPersonasEspecialidades> AllEspecialidades(Int64? encId)
        {
            return getDatosEspecialidades(encId).ToList();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public int GuardaComentario(int id,string txt)
        {
            if (id != 0)
            {
                GeDoc.Models.efAccesoADatosEntities context = new efAccesoADatosEntities();

                var result = (from p in context.catEncuestaSinadmisionPersonaEspecialidad
                              where p.peId == id
                              select p).SingleOrDefault();

                result.comentario = txt;

                context.SaveChanges();

            }
            return 0;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CargaComentario(int id) 
        {
            GeDoc.Models.efAccesoADatosEntities context = new efAccesoADatosEntities();
            var _Datos = (from d in context.catEncuestaSinadmisionPersonaEspecialidad
                          where d.peId == id
                          select d.comentario).ToList();
            var cmt = "[{";
            cmt += "\"cmt\":" + "\""+ _Datos[0] + "\"" + "";

            cmt += "}]";
            return new JsonResult() { Data = cmt };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingEspecialidades(int id)
        {
            catEncuestaSinadmisionPersonaEspecialidad _Item = context.catEncuestaSinadmisionPersonaEspecialidad.Where(p => p.peId == id).FirstOrDefault();
            catEncuestaSinadmisionPersonasEspecialidades _Item2 = new catEncuestaSinadmisionPersonasEspecialidades();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllEspecialidades(_Item.encId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingEspecialidades(int encId)
        {
            catEncuestaSinadmisionPersonaEspecialidad _Item = new catEncuestaSinadmisionPersonaEspecialidad();
            catEncuestaSinadmisionPersonasEspecialidades _Item2 = new catEncuestaSinadmisionPersonasEspecialidades();

            _Item.encId = (short) encId;
            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllEspecialidades(_Item.encId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingEspecialidades(int id)
        {
            var _Item = context.catEncuestaSinadmisionPersonaEspecialidad.Where(e => e.peId == id).First();

            DeleteConfirmedEspecialidades(id);

            return View(new GridModel(AllEspecialidades(_Item.encId)));
        }


        private IEnumerable<catEncuestaSinadmisionPersonasEspecialidades> getDatosEspecialidades(Int64? encId)
        {
            var _Datos = (from d in context.catEncuestaSinadmisionPersonaEspecialidad
                          where d.encId == encId
                          select new catEncuestaSinadmisionPersonasEspecialidades()
                                    {
                                        encId = d.encId,
                                        espId = d.espId,
                                        peId = d.peId,
                                        
                                        programado = d.programado,
                                        derDescripcion = d.programado == 1 ? "SI" : "NO",
                                        
                                        atendidoLocal = d.atendidoLocal,
                                        atendidoLocalDescripcion = d.atendidoLocal > 0 ? context.sisTipo.FirstOrDefault(e => e.tipoId == d.atendidoLocal ).tipoDescripcion  : "NO",
                                        
                                        programadoEn = d.programadoEn,
                                        
                                        programadoEnNombre = d.catCentroDeSalud.csNombre,
                                        programadoCuando = d.programadoCuando,

                                        derivado = d.derivado, 
                                        derivado_desc = d.derivado > 0 ? context.sisTipo.FirstOrDefault(e => e.tipoId == d.derivado).tipoDescripcion : "NO",//== 203 ? "SI" : "NO", 

                                        interconsulta = d.interconsulta,
                                        interconsulta_desc = d.interconsulta > 0 ? context.sisTipo.FirstOrDefault(e => e.tipoId == d.interconsulta).tipoDescripcion : "NO", //== 201 ? "SI" : "NO", 
                                        comentario = d.comentario.Trim() != "" ? "SI" : "NO",

                                        peEspecialidades = new catEspecialidades()
                                                                {
                                                                    espAbreviatura = d.catEspecialidad.espAbreviatura,
                                                                    espCodigo = d.catEspecialidad.espCodigo,
                                                                    espDescripcionPlanilla = d.catEspecialidad.espDescripcionPlanilla,
                                                                    espId = (short) d.espId,
                                                                    espIdMHO = d.catEspecialidad.espIdMHO,
                                                                    espIdPadre = d.catEspecialidad.espIdPadre,
                                                                    espNombre = d.catEspecialidad.espNombre,
                                                                },
                                    }).ToList();
            var x = "";                                     
            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingEspecialidades(Int64? encId)
        {
            encId = encId == null ? 1 : encId;
            return View(new GridModel<catEncuestaSinadmisionPersonasEspecialidades>
            {
                Data = AllEspecialidades(encId)
            });
        }

        private void Create(catEncuestaSinadmisionPersonaEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                context.catEncuestaSinadmisionPersonaEspecialidad.AddObject(pItem);
                context.SaveChanges();
            }
            return;
        }

        private void Edit(catEncuestaSinadmisionPersonaEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }
            return;
        }

        private void DeleteConfirmedEspecialidades(int id)
        {
            catEncuestaSinadmisionPersonaEspecialidad _Item = context.catEncuestaSinadmisionPersonaEspecialidad.Single(x => x.peId == id);
            context.catEncuestaSinadmisionPersonaEspecialidad.DeleteObject(_Item);
            context.SaveChanges();
        }

    }
}