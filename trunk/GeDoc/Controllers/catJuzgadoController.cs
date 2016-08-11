namespace GeDoc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Filters;
    using Models;

    public class catJuzgadoController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catJuzgadoWS> All()
        {
            return GetDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            var item = context.catJuzgado.FirstOrDefault(p => p.juzId == id);

            TryUpdateModel(item);

            Edit();

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            var item = new catJuzgado();

            if (TryUpdateModel(item))
            {
                Create(item);
            }

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditing(int id)
        {
            DeleteConfirmed(id);

            return View(new GridModel(All()));
        }

        private IEnumerable<catJuzgadoWS> GetDatos()
        {
            var datos = (from d in context.catJuzgado
                          select new catJuzgadoWS()
                                    {
                                        juzDenominacion = d.juzDenominacion,
                                        juzId = d.juzId
                                    }).ToList();

            return datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Juzgados";

            return PartialView();
        }

        private void Create(catJuzgado pItem)
        {
            if (ModelState.IsValid)
            {
                context.catJuzgado.AddObject(pItem);
                context.SaveChanges();
            }
        }

        private void Edit()
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
        }

        private void DeleteConfirmed(int id)
        {
            var item = context.catJuzgado.Single(x => x.juzId == id);
            context.catJuzgado.DeleteObject(item);
            context.SaveChanges();
        }
    }
}