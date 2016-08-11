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
    using System.Data.OleDb;
    using GeDoc.Models.dsAccesoExpedientesTableAdapters;

    public class sisLogEnvioEmailController : Controller
    {
        private efAccesoADatosEntities context = new efAccesoADatosEntities();

        //Edición de datos

        [GridAction]
        public ActionResult _SelectEditing()
        {
            return View(new GridModel(All()));
        }

        private IList<sisLogEnvioEmail> All()
        {
                return getDatos().ToList();
        }

        private IEnumerable<sisLogEnvioEmail> getDatos()
        {
            return context.sisLogEnvioEmail;
        }

        public ActionResult Index()
        {
            ViewBag.Catalogo = "Alertas";

            return PartialView();   
        }
    }
}