using System.Web.Mvc;

namespace GeDoc.Areas.Photo
{
    public class PhotoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Photo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Photo_default",
            //    "Photo/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "myarea_default",
                "Photo/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                new[] { "GeDoc.Areas.Photo.Controllers" }
            );

        }
    }
}
