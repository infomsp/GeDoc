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

    public partial class proDecretoController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingExpedientes()
        {
            return View(new GridModel(AllExpedientes()));
        }

        public IList<proDecretosExpedientes> AllExpedientes()
        {
            return getDatosExpedientes().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingExpedientes(int id)
        {
            proDecretoExpedientes _Item = context.proDecretoExpedientes.Where(p => p.deceId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllExpedientes().Where(exp => exp.decId == _Item.decId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingExpedientes()
        {
            proDecretoExpedientes _Item = new proDecretoExpedientes();

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllExpedientes().Where(exp => exp.decId == _Item.decId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingExpedientes(int id)
        {
            var _Item = context.proDecretoExpedientes.Where(e => e.deceId == id).First();

            DeleteConfirmedExpedientes(id);

            return View(new GridModel(AllExpedientes().Where(exp => exp.decId == _Item.decId)));
        }

        private IEnumerable<proDecretosExpedientes> getDatosExpedientes()
        {
            var _Datos = (from d in context.proDecretoExpedientes.ToList()
                          select new proDecretosExpedientes()
                                    {
                                        decId = d.decId,
                                        deceExpedienteAnio = d.deceExpedienteAnio,
                                        deceExpedienteNumero = d.deceExpedienteNumero,
                                        deceId = d.deceId,
                                        zonId = d.zonId,
                                        zonNombre = d.catZona.zonNombre,
                                        zonCodigo = d.catZona.zonCodigo
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingExpedientes(Int64? idDecreto)
        {
            idDecreto = idDecreto == null ? 1 : idDecreto;

            return View(new GridModel<proDecretosExpedientes>
            {
                Data = AllExpedientes().Where(exp => exp.decId == idDecreto)
            });
        }

        private void Create(proDecretoExpedientes pItem)
        {
            if (ModelState.IsValid)
            {
                context.proDecretoExpedientes.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(proDecretoExpedientes pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmedExpedientes(int id)
        {
            proDecretoExpedientes _Item = context.proDecretoExpedientes.Single(x => x.deceId == id);
            context.proDecretoExpedientes.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}