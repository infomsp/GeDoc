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
    public partial class catPacienteController : Controller
    {
      

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
       
        public void CreateEnlPCS(enlPacienteCtroSalud _Item)
        {
            if (_Item.pcId == 0)
            {
                if (ModelState.IsValid)
                {

                    context.enlPacienteCtroSalud.AddObject(_Item);
                    context.SaveChanges();
                }
            }
            return;
        }
        public void editEnlPCS(enlPacienteCtroSalud _Item)
        {
            if (ModelState.IsValid)
            {
                if (_Item.pcId == 0)
                {
                    try
                    {
                        context.enlPacienteCtroSalud.AddObject(_Item);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }

                }
                else
                {

                    enlPacienteCtroSalud _EItem = context.enlPacienteCtroSalud.Where(p => p.pacId == _Item.pacId && p.csId == _Item.csId).FirstOrDefault();
                    _EItem.nroHC = _Item.nroHC;
                    
                    context.SaveChanges();

                }
       
             
        }
            return;

        }
      

    }
}
