namespace GeDoc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using GeDoc.Filters;
    using GeDoc.Models;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;
    using System.Data.Objects.DataClasses;
    using NPOI.HSSF.UserModel;
    using System.IO;
    using System.Collections.Specialized;
    using System.Runtime.Serialization.Json;
    using System.Net.Http;
    using System.Net;
    using System.Web.Helpers;
    using System.Json;
    public partial class catCentrosDeSaludController : Controller
    {
        public efAccesoADatosEntities context = new efAccesoADatosEntities();
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SelectEditingenlCentrodeSaludBarrio(int _csId)
        {
            return View(new GridModel(AllenlCentrodeSaludBarrio(_csId)));
        }

        public IList<enlCentrosdeSaludBarrios> AllenlCentrodeSaludBarrio(int _csId)
        {
            return getenlCentrodeSaludBarrio(_csId).ToList();
        }

        private IEnumerable<enlCentrosdeSaludBarrios> getenlCentrodeSaludBarrio(int _csId)
        {
            var _Datos = (from d in context.vwCentrodeSaludBarrios.ToList()
                          where d.csaId == _csId
                          select new enlCentrosdeSaludBarrios()
                          {
                              barId = d.barId,
                              cbId = d.cbId,
                              csId = d.csaId,
                              barrioNombre = d.barNombre,
                              centroSaludNombre = d.csaNombre

                          }).ToList();

            return _Datos.AsEnumerable();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CultureAwareAction]
        [GridAction]
        public ActionResult _InsertEditingenlCentrodeSaludBarrio()
        {
            enlTurnoDiagnostico _Item = new enlTurnoDiagnostico();


            TryUpdateModel(_Item);

            if (TryUpdateModel(_Item))
            {
                Create(_Item);
            }

            return View(new GridModel(AllenlCentrodeSaludBarrio((short)_Item.turId)));
        }

        private void Create(enlTurnoDiagnostico pItem)
        {
            if (ModelState.IsValid)
            {
                context.enlTurnoDiagnostico.AddObject(pItem);
                //Registra log de usuario
                //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "catTurno", "Agregar Diagnostico", "Agrega Diagnostico al paciente");
                context.SaveChanges();
            }

            return;
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteenlCentrodeSaludBarrio(int id)
        {
            int turId = (short)DeleteConfirmed(id);

            return View(new GridModel(AllenlCentrodeSaludBarrio(turId)));
        }


        private int DeleteConfirmed(int id)
        {
            enlTurnoDiagnostico _Item = context.enlTurnoDiagnostico.Single(x => x.tdId == id);
            context.enlTurnoDiagnostico.DeleteObject(_Item);
            int turId = (short)_Item.turId;
            //Registra log de usuario
            //new AccountController().RegistrarLog((Session["Usuario"] as sisUsuario), "Index", "enlTurnoDiagnostico", "Eliminar", "");  
            context.SaveChanges();
            return turId;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AsignarenlCentrodeSaludBarrio(int _barId, int _csId)
        {
            try
            {
                //catTurno turno = context.catTurno.Where(w => w.turId == turId).First();
                //string _tipoId_estadoTurno = (from d in context.sisTipo
                //                              where d.tipoId == turno.tipoId
                //                              select d.tipoCodigo).First();
                //string _tipoId_admisionTurno = (from d in context.sisTipo
                //                                where d.tipoId == turno.tipoId_Admision
                //                                select d.tipoCodigo).First();


                //if (_tipoId_admisionTurno == "AT")
                //{
                //    enlTurnoDiagnostico _Item = new enlTurnoDiagnostico();
                //    _Item.diagId = diagId;
                //    _Item.turId = turId;
                //    Create(_Item);
                //}
                //else
                    return Json(new { IsValid = true, Mensaje = "El paciente debe estar atendido" });


            }

            catch (Exception ex)
            {
                return Json(new { IsValid = false, Mensaje = ex.Message });
            }

            return Json(new { IsValid = true, Mensaje = "OK" });

        }



    }
}

