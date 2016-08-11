namespace GeDoc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class catCentroDeSaludSalaController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCentroDeSaludSalaWS> All()
        {
            return getDatos().ToList();
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catCentroDeSaludSala _Item = context.catCentroDeSaludSala.Where(p => p.cssId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCentroDeSaludSala _Item = new catCentroDeSaludSala();

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

        private IEnumerable<catCentroDeSaludSalaWS> getDatos()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            var _Datos = (from rep in context.catCentroDeSaludSala.Where(w => w.csId == _csId).ToList()
                          select new catCentroDeSaludSalaWS()
                          {
                              cssId = rep.cssId,
                              cssNombre = rep.cssNombre,
                              Consultorios = rep.catCentroDeSaludSalaConsultorio.Count,
                              Televisores = rep.catCentroDeSaludSalaTelevisor.Count
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Salas";

            return PartialView();
        }

        private void Create(catCentroDeSaludSala pItem)
        {
            if (ModelState.IsValid)
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                pItem.csId = _csId;
                context.catCentroDeSaludSala.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catCentroDeSaludSala pItem)
        {
            if (ModelState.IsValid)
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                pItem.csId = _csId;

                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int pId)
        {
            catCentroDeSaludSala _Item = context.catCentroDeSaludSala.Single(x => x.cssId == pId);
            context.catCentroDeSaludSala.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}