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

    public partial class catFacturaController : Controller
    {
        //Edición de datos
        [GridAction]
        public ActionResult _SelectEditingNotificaciones()
        {
            return View(new GridModel(AllNotificaciones()));
        }

        public IList<catFacturasNotificaciones> AllNotificaciones()
        {
            return getDatosNotificaciones().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingNotificaciones(int id)
        {
            catFacturaNotificacion _Item = context.catFacturaNotificacion.Where(p => p.notId == id).FirstOrDefault();
            catFacturasNotificaciones _Item2 = new catFacturasNotificaciones();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            // Validamos el total de la factura
            if (ValidaDatos(id, _Item2, _Item))
            {
                _Item.tipoId = _Item2.tipoIdNota;
                Edit(_Item);
            }

            return View(new GridModel(AllNotificaciones().Where(exp => exp.facId == _Item.facId)));
        }

        private bool ValidaDatos(int id, catFacturasNotificaciones _Item2, catFacturaNotificacion _Item)
        {
            // Validamos Datos
            var _CartaYaCargada = context.catFacturaNotificacion.Where(w => w.facId == _Item.facId && w.sisTipo.tipoCodigo == "CD" && w.notId != id).Count() > 0;
            var _EsCarta = context.sisTipo.Where(t => t.tipoId == _Item2.tipoIdNota && t.tipoCodigo == "CD").Count() > 0;
            var _EnSSS = context.catFacturaNotificacion.Where(w => w.facId == _Item.facId && w.sisTipo.tipoCodigo == "SS" && w.notId != id).Count() > 0;
            var _EsEnSSS = context.sisTipo.Where(t => t.tipoId == _Item2.tipoIdNota && t.tipoCodigo == "SS").Count() > 0;

            if (_CartaYaCargada && _EsCarta)
            {
                ModelState.AddModelError("tipoIdNota", "Carta documento ya enviada.");
            }
            else if (_EnSSS && _EsEnSSS)
            {
                ModelState.AddModelError("tipoIdNota", "Solicitud ya envidad a la SSS.");
            }

            return true;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingNotificaciones()
        {
            catFacturaNotificacion _Item = new catFacturaNotificacion();
            catFacturasNotificaciones _Item2 = new catFacturasNotificaciones();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                if (ValidaDatos(-1, _Item2, _Item))
                {
                    _Item.tipoId = _Item2.tipoIdNota;
                    Create(_Item);
                }
            }

            return View(new GridModel(AllNotificaciones().Where(exp => exp.facId == _Item.facId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingNotificaciones(int id)
        {
            var _Item = context.catFacturaNotificacion.Where(e => e.notId == id).First();

            DeleteConfirmedNotificaciones(id);

            return View(new GridModel(AllNotificaciones().Where(exp => exp.facId == _Item.facId)));
        }

        private IEnumerable<catFacturasNotificaciones> getDatosNotificaciones()
        {
            var _Datos = (from d in context.catFacturaNotificacion.ToList()
                          select new catFacturasNotificaciones()
                                    {
                                        notId = d.notId,
                                        notDetalle = d.notDetalle,
                                        notFecha = d.notFecha,
                                        notTipo = d.sisTipo.tipoDescripcion,
                                        facId = d.facId,
                                        tipoIdNota = d.tipoId
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingNotificaciones(Int64? idFactura)
        {
            idFactura = idFactura == null ? 1 : idFactura;

            return View(new GridModel<catFacturasNotificaciones>
            {
                Data = AllNotificaciones().Where(exp => exp.facId == idFactura)
            });
        }

        private void Create(catFacturaNotificacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catFacturaNotificacion.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("tipoId", ex.Message);
                }
            }

            return;
        }

        private void Edit(catFacturaNotificacion pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("tipoId", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmedNotificaciones(int id)
        {
            catFacturaNotificacion _Item = context.catFacturaNotificacion.Single(x => x.notId == id);
            context.catFacturaNotificacion.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}