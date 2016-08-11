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
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;

    public class catCargoController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<catCargos> All()
        {
                return getDatos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditing(int id)
        {
            catCargo _Item = context.catCargo.Where(p => p.carId == id).FirstOrDefault();
            string _carDescripcionAnterior = _Item.carDescripcion;

            TryUpdateModel(_Item);

            RegistrarLog("Modificar", "Actualiza cargo => " + _carDescripcionAnterior + " por " + _Item.carDescripcion);

            Edit(_Item);

            return View(new GridModel(All()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditing()
        {
            catCargo _Item = new catCargo();

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
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

        private IEnumerable<catCargos> getDatos()
        {
            var _Datos = (from d in context.catCargo.ToList()
                          select new catCargos()
                                    {
                                        carDescripcion = d.carDescripcion,
                                        carId = d.carId,
                                        agrDescripcion = d.catAgrupamiento.agrCodigo + ") " + d.catAgrupamiento.agrDescripcion,
                                        agrId = d.agrId,
                                        carEmpleados = (from x in context.catCargoCategoria
                                                        join z in context.catCargoDesignacion on x.categId equals z.categId
                                                        where x.carId == d.carId
                                                        select x).Count()
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Configuración de Cargos";

            PopulateDropDownList();

            return PartialView();
        }

        private void Create(catCargo pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catCargo.AddObject(pItem);
                    RegistrarLog("Agregar", "Agrega cargo => " + pItem.carDescripcion);
                    context.SaveChanges();
                }
                catch (Exception ex)
                { }
            }

            return;
        }

        private void Edit(catCargo pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmed(int id)
        {
            catCargo _Item = context.catCargo.Single(x => x.carId == id);

            RegistrarLog("Eliminar", "Elimina cargo => " + _Item.carDescripcion);

            context.catCargo.DeleteObject(_Item);
            context.SaveChanges();
        }

        //Datos para el dropdown
        protected void PopulateDropDownList()
        {
            var _Agrupamiento = (from d in context.catAgrupamiento.ToList()
                                select new catAgrupamiento()
                                {
                                    agrId = d.agrId,
                                    agrDescripcion = d.agrCodigo + ") " + d.agrDescripcion
                                }).ToList();

            ViewData["agrId_Data"] = new SelectList(_Agrupamiento, "agrId", "agrDescripcion");

            return;
        }

        public void RegistrarLog(string pAccion, string psMensaje)
        {
            //Registra log de usuario
            new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catCargo", pAccion, psMensaje);

            return;
        }
    }
}