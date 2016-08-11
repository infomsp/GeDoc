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
        public ActionResult _SelectEditingResoluciones()
        {
            return View(new GridModel(AllResoluciones()));
        }

        public IList<proDecretosResoluciones> AllResoluciones()
        {
            return getDatosResoluciones().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingResoluciones(int id)
        {
            proDecretoResoluciones _Item = context.proDecretoResoluciones.Where(p => p.drId == id).FirstOrDefault();

            TryUpdateModel(_Item);

            Edit(_Item);

            return View(new GridModel(AllResoluciones().Where(exp => exp.decId == _Item.decId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingResoluciones()
        {
            proDecretoResoluciones _Item = new proDecretoResoluciones();

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllResoluciones().Where(exp => exp.decId == _Item.decId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingResoluciones(int id)
        {
            var _Item = context.proDecretoResoluciones.First(e => e.drId == id);

            DeleteConfirmedResoluciones(id);

            return View(new GridModel(AllResoluciones().Where(exp => exp.decId == _Item.decId)));
        }

        private IEnumerable<proDecretosResoluciones> getDatosResoluciones()
        {
            var _Datos = (from d in context.getDatosDecretosResoluciones().ToList()
                          select new proDecretosResoluciones()
                                    {
                                        drId = d.drId,
                                        resId = d.resId,
                                        decId = d.decId,
                                        resLinkArchivo = d.resLinkArchivo,
                                        resNumero = d.resNumero
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingResoluciones(Int64? idDecreto)
        {
            idDecreto = idDecreto == null ? 1 : idDecreto;

            return View(new GridModel<proDecretosResoluciones>
            {
                Data = AllResoluciones().Where(exp => exp.decId == idDecreto)
            });
        }

        private void Create(proDecretoResoluciones pItem)
        {
            if (ModelState.IsValid)
            {
                context.proDecretoResoluciones.AddObject(pItem);
                context.SaveChanges();
            }

            return;
        }

        private void Edit(proDecretoResoluciones pItem)
        {
            if (ModelState.IsValid)
            {
                context.SaveChanges();
            }
            return;
        }

        private void DeleteConfirmedResoluciones(int id)
        {
            proDecretoResoluciones _Item = context.proDecretoResoluciones.Single(x => x.drId == id);
            context.proDecretoResoluciones.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}