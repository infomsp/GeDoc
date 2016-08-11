using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.Configuration;

namespace GeDoc.Controllers
{
    public class ReporteController : Controller
    {
        //
        // GET: /Reporte/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Método que devuelve la información en formato JSON
        /// </summary>
        /// <param name="id">Parámetro que permitirá filtrar el reporte</param>
        /// <returns>Retorna el iframe construido en el stringbuilder en formato json</returns>
        /// 

        

        public JsonResult VerReporte(string id)
        {
            //URL Visor del Servidor de Reporting Services
            string sServidor = "http://10.64.65.200/ReportServer_MSPSQL2008";
            //Carpeta donde tenemos los reportes
            string sCarpeta = "GeDoc_Reportes";
            //Nombre del Reporte
            string sReporte = "rptImputacionFinanciera";
            //Los parámetros con sus respectivos valores
            string sParametroValor = "&impId=" + id.Trim();
            //Comandos a pasar al Visor de Reporting Services
            //Esos comandos los consigue en: http://technet.microsoft.com/es-ve/library/ms152835.aspx
            string sComandosRS = "&rs:Command=Render&rs:Format=HTML4.0&rc:Parameters=false";
            //StringBuilder para crear un iFrame
            StringBuilder sb = new StringBuilder();
            sb.Append("<iframe id='ifReporte' width='100%' style='height: 480px' frameborder='0'");
            sb.AppendFormat("src='{0}?/{1}/{2}{3}{4}'", sServidor, sCarpeta, sReporte, sParametroValor, sComandosRS);
            sb.Append("></iframe>");
            //Retorna el stringBuilder en JSON y se permite todas las peticiones GET
            return this.Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}
