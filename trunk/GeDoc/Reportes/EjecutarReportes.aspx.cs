using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;

public partial class _EjecutarReportes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Le indicamos al Control que la invocación del reporte será de modo remoto
        ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        if (Request.QueryString["JM"] != null)
        {
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings.Get("WebReportingServiceJM"));
        }
        else
        {
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings.Get("WebReportingService"));
        }

        ReportViewer1.ServerReport.ReportServerCredentials = new ReportingCredentials.ReportServerCredentials
            (ConfigurationManager.AppSettings.Get("CredencialesUser"),
            ConfigurationManager.AppSettings.Get("CredencialesPassword"),
            ConfigurationManager.AppSettings.Get("CredencialesDomain"));

        string lNombreReporte = Request.QueryString["sr"];
        string _PathReportes = string.Format(@"/{0}/{1}", 
            Request.QueryString["JM"] != null ? 
            ConfigurationManager.AppSettings.Get("PathReportesJM") :
            ConfigurationManager.AppSettings.Get("PathReportes"), 
            lNombreReporte);

        ReportViewer1.ServerReport.ReportPath = _PathReportes;
        ReportViewer1.Page.Culture = ConfigurationManager.AppSettings.Get("Cultura");

        if (Request.QueryString["ci"] != null)
        {
            int lCantParametros = Convert.ToInt32(Request.QueryString["ci"].ToString());
            string parametros = "";
            for (int x = 0; x < lCantParametros; x++)
            {
                Microsoft.Reporting.WebForms.ReportParameter parametro = new Microsoft.Reporting.WebForms.ReportParameter();
                //Definimos los parámetros
                parametro.Name = Request.QueryString["n" + x.ToString()];
                parametro.Values.Add(Request.QueryString["v" + x.ToString()]);//txtContactID.Text
                parametros += Request.QueryString["v" + x.ToString()] + " ----- ";
                //Aqui le indicaremos si queremos que el parámetro 
                //sea visible para el usuario o no
                parametro.Visible = false;
                //Crearemos un arreglo de parámetros
                Microsoft.Reporting.WebForms.ReportParameter[] rp = { parametro };
                ReportViewer1.ServerReport.SetParameters(rp);
            }
        }

        //Warning[] warnings = null;
        //string[] streamids = null;
        //string mimeType = null;
        //string encoding = null;
        //string extension = null;
        byte[] result;
        //result = ReportViewer1.ServerReport.Render("PDF", null, PageCountMode.Estimate, out mimeType, out encoding, out extension, out streamids, out warnings);
        result = ReportViewer1.ServerReport.Render("PDF");

        var ms = new MemoryStream(result);
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(ms.ToArray());
        Response.Flush();
        Response.End();
    }
}
