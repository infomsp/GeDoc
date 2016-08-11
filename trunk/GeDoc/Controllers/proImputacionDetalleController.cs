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

    public partial class proImputacionController : Controller
    {
        private IList<proImputacionesDetalles> AllDetalle(Int64? idImputacion)
        {
            return getDatosDetalle(idImputacion).ToList();
        }
        private IList<proImputacionesDetalles> AllDetalleNeg(Int64?  idImputacion)
        {
            return getDatosDetalleNeg(idImputacion).ToList();
        }

        private IList<proImputacionesDetalles> AllImputaciones()
        {
            return (IList<proImputacionesDetalles>)Session["DetalleImputaciones"];
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _SaveEditingDetalle(int id, proImputacionesDetalles pItem)
        {
            pItem.impdId = id;
            Edit(pItem);

            return View(new GridModel(AllImputaciones().Where(p => p.impId == pItem.impId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingDetalle()
        {
            proImputacionesDetalles _Item = new proImputacionesDetalles();

            if (TryUpdateModel(_Item, null, null, new[] { "SubPartida" }))
            {
                Create(_Item);
            }

            return View(new GridModel(AllImputaciones().Where(p => p.impId == _Item.impId)));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteEditingDetalle(int id, proImputacionesDetalles pItem)
        {
            DeleteDetalleConfirmed(id);

            return View(new GridModel(AllImputaciones()));
        }
        [GridAction]
        public ActionResult _BindingDetalleImputaciones(Int64? idImputacion, int _AnulacionParcial)
        {
            idImputacion = idImputacion == null ? 0 : idImputacion;

            var _Datos = AllDetalle(idImputacion);

            if (_AnulacionParcial == 1)
            {
                _Datos = AllDetalleNeg(idImputacion);
            }

            Session["DetalleImputaciones"] = _Datos.ToList();
            Session["EditandoImputaciones"] = "S";
            return View(new GridModel<proImputacionesDetalles>
            {
                Data = _Datos
            });
        }
        private IEnumerable<proImputacionesDetalles> getDatosDetalleNeg(Int64? idImputacion)
        {
            var _Datos = (from d in context.proImputacionDetalle.ToList()
                          where d.impId == idImputacion
                          select new proImputacionesDetalles()
                          {
                              impdId = d.impdId*(-1),
                              impdImporte = d.impdImporte,
                              impId = 0,
                              subpId = d.subpId,
                              subpNombre = d.catSubPartida.subpCodigo + " - " + d.catSubPartida.subpNombre
                          }).ToList();

            Session["DetalleImputaciones"] = _Datos;

            return _Datos.AsEnumerable();
        }
        private IEnumerable<proImputacionesDetalles> getDatosDetalle(Int64? idImputacion)
        {
            var _Datos = (from d in context.proImputacionDetalle
                          where d.impId == idImputacion
                          select new proImputacionesDetalles()
                                    {
                                        impdId = d.impdId,
                                        impdImporte = d.impdImporte,
                                        impId = d.impId,
                                        subpId = d.subpId,
                                        subpNombre = d.catSubPartida.subpCodigo + " - " + d.catSubPartida.subpNombre
                                    }).ToList();

            Session["DetalleImputaciones"] = _Datos;

            return _Datos.AsEnumerable();
        }

        private void Create(proImputacionesDetalles pItem)
        {
            if (ModelState.IsValid)
            {
                var _SubPartida = context.catSubPartida.FirstOrDefault(p => p.subpId == pItem.subpId);
                pItem.subpNombre = _SubPartida.subpCodigo + " - " + _SubPartida.subpNombre;
                pItem.impdId = (((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Count + 1) * (-1);
                ((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Add(pItem);
            }

            return;
        }

        private void Edit(proImputacionesDetalles pItem)
        {
            if (ModelState.IsValid)
            {
                var _Update = ((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).SingleOrDefault(p => p.impdId == pItem.impdId);
                
                _Update.subpId = pItem.subpId;
                var _SubPartida = context.catSubPartida.SingleOrDefault(p => p.subpId == pItem.subpId);

                _Update.subpNombre = _SubPartida.subpCodigo + " - " + _SubPartida.subpNombre;
                _Update.impId = pItem.impId;
                _Update.impdImporte = pItem.impdImporte;
                _Update.impdId = pItem.impdId;
            }
            return;
        }

        private void DeleteDetalleConfirmed(int id)
        {
            var _dOne = ((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Where(p => p.impdId == id).Single();
            ((IList<proImputacionesDetalles>)Session["DetalleImputaciones"]).Remove(_dOne);
        }
    }
}