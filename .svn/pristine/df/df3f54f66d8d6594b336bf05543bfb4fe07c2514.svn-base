namespace GeDoc
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using GeDoc.Filters;
    using GeDoc.Models;

    public partial class catCentroDeSaludController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCentroDeSaludWS> All()
        {
            return getDatos().ToList();
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            var _Item = context.catCentroDeSalud.FirstOrDefault(p => p.csId == id);
            var _Item2 = new catCentroDeSaludWS();

            TryUpdateModel(_Item2);
            if (TryUpdateModel(_Item))
            {
                _Item.csPublico = _Item2.csPublicoAuxiliar ? 1 : 0;
                Edit(_Item);
            }

            return View(new GridModel(All()));
        }

        [HttpPost]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            var _Item = new catCentroDeSalud();
            var _Item2 = new catCentroDeSaludWS();

            TryUpdateModel(_Item2);
            if (TryUpdateModel(_Item))
            {
                _Item.csPublico = _Item2.csPublicoAuxiliar ? 1 : 0;
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

        private IEnumerable<catCentroDeSaludWS> getDatos()
        {
            var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;

            var _Datos = (from rep in context.catCentroDeSalud.ToList()
                          select new catCentroDeSaludWS()
                          {
                              csId = rep.csId,
                              csCodigo = rep.csCodigo,
                              csNombre = rep.csNombre,
                              depId = rep.depId,
                              depNombre = rep.catDepartamento == null ? "" : rep.catDepartamento.depNombre,
                              csLatitud = rep.csLatitud,
                              csDirector = rep.csDirector,
                              csTipologia = rep.csTipologia,
                              csTelefono = rep.csTelefono,
                              csLongitud = rep.csLongitud,
                              csDomicilio = rep.csDomicilio,
                              csPublicoAuxiliar = rep.csPublico == null ? false : (rep.csPublico == 1 ? true : false),
                              cszId = rep.cszId,
                              CodBioestadistica = rep.CodBioestadistica,
                              CodRemediar = rep.CodRemediar,
                              CantVivienda = rep.CantVivienda,
                              CantVaron = rep.CantVaron,
                              CantMujeres = rep.CantMujeres,
                              Total = rep.Total,
                              ZonaSanitaria = rep.catReparticion == null ? "" : rep.catReparticion.repNombre,
                              admId = rep.admId,
                              Administrador = rep.admId == null ? "" : rep.catAdministrador.admDescripcion
                          }).ToList();

            return _Datos.AsEnumerable();
        }

        private void Create(catCentroDeSalud pItem)
        {
            if (ModelState.IsValid)
            {
                var _csId = (Session["UsuarioCentroDeSalud"] as sisUsuariosCentroDeSalud).csId;
                pItem.csId = _csId;
                context.catCentroDeSalud.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(catCentroDeSalud pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int pId)
        {
            catCentroDeSalud _Item = context.catCentroDeSalud.Single(x => x.csId == pId);
            context.catCentroDeSalud.DeleteObject(_Item);
            context.SaveChanges();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Centros de Salud";
            PopulateDropDownList();

            return PartialView();
        }

        protected void PopulateDropDownList()
        {
            var Departamentos = context.catDepartamento.Where(w => w.provId == 17).ToList();
            var ZonasSanitarias = context.catReparticion.ToList();

            ViewData["depId_Data"] = new SelectList(Departamentos, "depId", "depNombre");
            ViewData["cszId_Data"] = new SelectList(ZonasSanitarias, "repId", "repNombre");
            ViewData["admId_Data"] = new SelectList(context.catAdministrador, "admId", "admDescripcion");
        }
    }
}