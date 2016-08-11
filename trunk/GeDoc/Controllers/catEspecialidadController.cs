namespace GeDoc
{
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

    public class catEspecialidadController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catEspecialidades> All()
        {
                return getDatos().ToList();
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catEspecialidad _Item = context.catEspecialidad.Where(p => p.espId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catEspecialidad _Item = new catEspecialidad();

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }
        private string getEspecialidadPadre(short _espId)
        {
            var _Especialidad = (from s in context.catEspecialidad
                                 where s.espId == _espId
                                   select new catEspecialidades
                                   {
                                       espNombre = s.espNombre
                                   }).First();
            return _Especialidad.espNombre;
            
        }
        private IEnumerable<catEspecialidades> getDatos()
        {
            var _Datos = (from rep in context.catEspecialidad.ToList()
                          select new catEspecialidades()
                          {
                              espId = rep.espId,
                              espAgrup = false,
                              espNombre = rep.espNombre,
                              espCodigo = rep.espCodigo,
                              espIdPadre = rep.espIdPadre,
                              espNombrePadre = rep.espIdPadre == null ? "" : getEspecialidadPadre((short)rep.espIdPadre),
                              espAbreviatura = rep.espAbreviatura,
                              espDescripcionPlanilla = rep.espDescripcionPlanilla,
                              Contratados = rep.catPersonaContratadosEspecialidad.Count
                          }).ToList();
            //var _Datos = context.catReparticion;

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Especialidades";
            PopulateDropDownList();
            return PartialView();
        }

        private void Create(catEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                context.catEspecialidad.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catEspecialidad pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int pId)
        {
            catEspecialidad _Item = context.catEspecialidad.Single(x => x.espId == pId);
            context.catEspecialidad.DeleteObject(_Item);
            context.SaveChanges();
        }
        protected List<catEspecialidades> getEspecialidades(int perId)
        {
            var _Especialidades = (from s in context.catEspecialidad.ToList()
                                   where s.espIdPadre == null
                                   select new catEspecialidades
                                   {
                                       espId = s.espId,
                                       espNombre = "(" + s.espCodigo + ") " + s.espNombre
                                   }).ToList().OrderBy(o => o.espCodigo);
            return _Especialidades.ToList();
        }
          //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            ViewData["espIdPadre_Data"] = new SelectList(getEspecialidades(0), "espId", "espNombre");
        }



    }
}