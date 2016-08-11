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

    public partial class proResolucionController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingEmpleados()
        {
            return View(new GridModel(AllEmpleados()));
        }

        public IList<proResolucionesNotificacionEmpleado> AllEmpleados()
        {
            return getDatosEmpleados().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingEmpleados(int id)
        {
            proResolucionNotificacionEmpleado _Item = context.proResolucionNotificacionEmpleado.Where(p => p.resneId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllEmpleados().Where(exp => exp.resId == _Item.resId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingEmpleados()
        {
            proResolucionNotificacionEmpleado _Item = new proResolucionNotificacionEmpleado();

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllEmpleados().Where(exp => exp.resId == _Item.resId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingEmpleados(int id)
        {
            var _Item = context.proResolucionNotificacionEmpleado.Where(e => e.resneId == id).First();

            DeleteConfirmedEmpleados(id);

            return View(new GridModel(AllEmpleados().Where(exp => exp.resId == _Item.resId)));
        }

        private IEnumerable<proResolucionesNotificacionEmpleado> getDatosEmpleados()
        {
            var _Datos = (from d in context.proResolucionNotificacionEmpleado.ToList()
                          select new proResolucionesNotificacionEmpleado()
                                    {
                                        resId = d.resId,
                                        perId = d.perId,
                                        perNombre = d.catPersona.perApellidoyNombre,
                                        resneId = d.resneId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingEmpleados(Int64? idResolucion)
        {
            idResolucion = idResolucion == null ? 1 : idResolucion;

            return View(new GridModel<proResolucionesNotificacionEmpleado>
            {
                Data = AllEmpleados().Where(exp => exp.resId == idResolucion)
            });
        }

        private void Create(proResolucionNotificacionEmpleado pItem)
        {
            if (ModelState.IsValid)
            {
                context.proResolucionNotificacionEmpleado.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(proResolucionNotificacionEmpleado pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmedEmpleados(int id)
        {
            proResolucionNotificacionEmpleado _Item = context.proResolucionNotificacionEmpleado.Single(x => x.resneId == id);
            context.proResolucionNotificacionEmpleado.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}