namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.IO;

    public class UploadController : Controller
    {
        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachments)
        {
            // The Name of the Upload component is "attachments" 
            string physicalPath = "";
            foreach (var file in attachments)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var fileName = Path.GetFileName(file.FileName);
                physicalPath = Path.Combine(Session["PathArchivos"].ToString(), fileName);

                Session["upArchivo"] = fileName;
                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);
            }
            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            string physicalPath = "";
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                physicalPath = Path.Combine(Session["PathArchivos"].ToString(), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                }
                Session["upArchivo"] = "";
            }
            // Return an empty string to signify success
            return Content("");
        }
    }
}
