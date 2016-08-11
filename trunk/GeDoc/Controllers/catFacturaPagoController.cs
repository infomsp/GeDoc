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
        public ActionResult _SelectEditingPagos()
        {
            return View(new GridModel(AllPagos()));
        }

        public IList<catFacturasPagos> AllPagos()
        {
            return getDatosPagos().ToList();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingPagos(int id)
        {
            catFacturaPago _Item = context.catFacturaPago.Where(p => p.pagId == id).FirstOrDefault();
            catFacturasPagos _Item2 = new catFacturasPagos();

            TryUpdateModel(_Item2);
            TryUpdateModel(_Item);

            // Validamos el total de la factura
            if (ValidaDatos(id, _Item2, _Item))
            {
                Edit(_Item);
            }

            return View(new GridModel(AllPagos().Where(exp => exp.facId == _Item.facId)));
        }

        private bool ValidaDatos(int id, catFacturasPagos _Item2, catFacturaPago _Item)
        {
            // Validamos el total de la factura
            decimal? _MontoCobrado = 0;
            var _Cobrado = context.catFacturaPago.Where(w => w.facId == _Item.facId && w.pagId != id);
            decimal? _MontoFactura = context.catFactura.Where(w => w.facId == _Item.facId).FirstOrDefault().facImporte;
            if (_Cobrado == null)
            {
                _MontoCobrado = _Cobrado.Sum(p => p.pagImporte);
            }
            _MontoCobrado += _Item2.pagImporte;
            if (_MontoFactura < _MontoCobrado)
            {
                ModelState.AddModelError("pagImporte", "El monto ingresado supera el total Facturado");
            }

            return true;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingPagos()
        {
            catFacturaPago _Item = new catFacturaPago();
            catFacturasPagos _Item2 = new catFacturasPagos();

            TryUpdateModel(_Item2);

            if (TryUpdateModel(_Item))
            {
                if (ValidaDatos(-1, _Item2, _Item))
                {
                    Create(_Item);
                }
            }

            return View(new GridModel(AllPagos().Where(exp => exp.facId == _Item.facId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingPagos(int id)
        {
            var _Item = context.catFacturaPago.Where(e => e.pagId == id).First();

            DeleteConfirmedPagos(id);

            return View(new GridModel(AllPagos().Where(exp => exp.facId == _Item.facId)));
        }

        private IEnumerable<catFacturasPagos> getDatosPagos()
        {
            var _Datos = (from d in context.catFacturaPago.ToList()
                          select new catFacturasPagos()
                                    {
                                        pagId = d.pagId,
                                        pagDetalle = d.pagDetalle,
                                        pagFecha = d.pagFecha,
                                        pagTipo = d.sisTipo.tipoDescripcion,
                                        pagMotivo = d.pagMotivo,
                                        facId = d.facId,
                                        pagNumeroRecibo = d.pagNumeroRecibo,
                                        pagForma = d.tipoIdForma == null ? "" : d.sisTipo1.tipoDescripcion,
                                        tipoId = d.tipoId,
                                        tipoIdForma = d.tipoIdForma,
                                        pagImporte = d.pagImporte
                                    }).ToList();

            return _Datos.AsEnumerable();
        }

        [GridAction]
        public ActionResult _BindingPagos(Int64? idFactura)
        {
            idFactura = idFactura == null ? 1 : idFactura;

            return View(new GridModel<catFacturasPagos>
            {
                Data = AllPagos().Where(exp => exp.facId == idFactura)
            });
        }

        private void Create(catFacturaPago pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.catFacturaPago.AddObject(pItem);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pagImporte", ex.Message);
                }
            }

            return;
        }

        private void Edit(catFacturaPago pItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("pagImporte", ex.Message);
                }
            }
            return;
        }

        private void DeleteConfirmedPagos(int id)
        {
            catFacturaPago _Item = context.catFacturaPago.Single(x => x.pagId == id);
            context.catFacturaPago.DeleteObject(_Item);
            context.SaveChanges();
        }
    }
}