using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web.Mvc;

namespace GeDoc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Bienvenido al sistema de Gestión Administrativa";

            return View();
        }

        public ActionResult About()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);

            ViewBag.VersionSistema = fvi.ProductVersion;

            return View();
        }
    }
}
