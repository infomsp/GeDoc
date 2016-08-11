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
        public ActionResult _SelectEditingExpedientes()
        {
            return View(new GridModel(AllExpedientes()));
        }

        public IList<proResolucionesExpedientes> AllExpedientes()
        {
            return getDatosExpedientes().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingExpedientes(int id)
        {
            proResolucionExpedientes _Item = context.proResolucionExpedientes.Where(p => p.reseId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllExpedientes().Where(exp => exp.resId == _Item.resId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingExpedientes()
        {
            proResolucionExpedientes _Item = new proResolucionExpedientes();

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllExpedientes().Where(exp => exp.resId == _Item.resId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingExpedientes(int id)
        {
            var _Item = context.proResolucionExpedientes.Where(e => e.reseId == id).First();

            DeleteConfirmedExpedientes(id);

            return View(new GridModel(AllExpedientes().Where(exp => exp.resId == _Item.resId)));
        }

        private IEnumerable<proResolucionesExpedientes> getDatosExpedientes()
        {
            var _Datos = (from d in context.proResolucionExpedientes.ToList()
                          select new proResolucionesExpedientes()
                                    {
                                        resId = d.resId,
                                        reseExpedienteAnio = d.reseExpedienteAnio,
                                        reseExpedienteNumero = d.reseExpedienteNumero,
                                        reseId = d.reseId,
                                        zonId = d.zonId,
                                        zonNombre = d.catZona.zonNombre,
                                        zonCodigo = d.catZona.zonCodigo
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingExpedientes(Int64? idResolucion)
        {
            idResolucion = idResolucion == null ? 1 : idResolucion;

            return View(new GridModel<proResolucionesExpedientes>
            {
                Data = AllExpedientes().Where(exp => exp.resId == idResolucion)
            });
        }

        private void Create(proResolucionExpedientes pItem)
        {
            if (ModelState.IsValid)
            {
                context.proResolucionExpedientes.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(proResolucionExpedientes pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmedExpedientes(int id)
        {
            proResolucionExpedientes _Item = context.proResolucionExpedientes.Single(x => x.reseId == id);
            context.proResolucionExpedientes.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}