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
    public partial class catDiagnosticoController : Controller
    {
     
        //
        // GET: /catDiagnostico/
        private efAccesoADatosEntities context = new efAccesoADatosEntities();
        [GridAction]
        private ActionResult getTiposDiagnosticos()
        {
            var _Item = (from d in context.vwDiagnosticosTipoDiagnosticos.ToList()
                         select new catDiagnosticos()
                         {
                             DiagnosticoID = d.dDiagnosticoID,
                             td1Descripcion = d.td1Descripcion,
                             td2Descripcion = d.td2Descripcion,
                             Descripcion = d.dDescripcion
                         }).ToList();
            return Json(new { _Datos = _Item });

        }

    }
}
