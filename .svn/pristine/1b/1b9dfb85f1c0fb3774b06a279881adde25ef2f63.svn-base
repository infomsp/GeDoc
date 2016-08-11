namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Telerik.Web.Mvc;
    using GeDoc.Models;
    using System.Web.Security;
    using GeDoc.Models.JuntaMedica.Modelo;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("Reportes/{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("Reportes/");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional }, // Parameter defaults
                //solve: Multiple types were found that match the controller named 'Home'
                new string[] { "GeDoc.Controllers" }
            );

        }

        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            //RegisterGlobalFilters(GlobalFilters.Filters);
            //RegisterRoutes(RouteTable.Routes);

#if MVC3
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new WebFormViewEngine());

            AreaRegistration.RegisterAllAreas();
#endif
            //ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());

            RegisterRoutes(RouteTable.Routes);

            SiteMapManager.SiteMaps.Register<XmlSiteMap>("GeDoc", sitmap => sitmap.Load());
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["MenuPrincipal"] = new List<Telerik.Web.Mvc.UI.MenuItem>();
            Session["AlturaGrilla"] = 425;
            Session["FilasPorPagina"] = 20;
            Session["Usuario"] = null;
            Session["Permisos"] = new List<sisUsuarioPermiso>();
            Session["Estilo"] = "vista";
            Session["Estilos"] = null;
            Session["MenuId"] = null;
            Session["Catalogo"] = "";
            Session["PathArchivos"] = "";
            Session["Version"] = "2013.2.611";
            Session["EditandoImputaciones"] = null;
            Session["UsuarioCentroDeSalud"] = null;
            Session["EsMiBandeja"] = false;
            Session["DatosTablero"] = new List<getDatosTableroDeComandoDCRM_Result>();

            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //if (Request.IsAuthenticated)
            //{
            //    FormsAuthentication.SignOut();
            //}
        }
    }
}