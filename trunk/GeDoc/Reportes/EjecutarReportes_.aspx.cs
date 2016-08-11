using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;

//<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EjecutarReportes.aspx.cs" Inherits="EjecutarReportes" %>

public partial class EjecutarReportes : System.Web.Mvc.ViewPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Le indicamos al Control que la invocación del reporte será de modo remoto
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings.Get("WebReportingService"));//("");

            ReportViewer1.ServerReport.ReportServerCredentials = new ReportingCredentials.ReportServerCredentials
                (ConfigurationManager.AppSettings.Get("CredencialesUser"),
                ConfigurationManager.AppSettings.Get("CredencialesPassword"),
                ConfigurationManager.AppSettings.Get("CredencialesDomain"));


            string lNombreReporte = Request.QueryString["sr"];
            ReportViewer1.ServerReport.ReportPath = lNombreReporte;
            ReportViewer1.Page.Culture = ConfigurationManager.AppSettings.Get("Cultura");

            if (Request.QueryString["ci"] != null)
            {
                int lCantParametros = Convert.ToInt32(Request.QueryString["ci"].ToString());
                string parametros = "";
                for (int x = 0; x < lCantParametros; x++)
                {
                    ReportParameter parametro = new ReportParameter();
                    //Definimos los parámetros
                    parametro = new ReportParameter();
                    parametro.Name = Request.QueryString["n" + x.ToString()];
                    parametro.Values.Add(Request.QueryString["v" + x.ToString()]);//txtContactID.Text
                    parametros += Request.QueryString["v" + x.ToString()] + " ----- ";
                    //Aqui le indicaremos si queremos que el parámetro 
                    //sea visible para el usuario o no
                    parametro.Visible = false;
                    //Crearemos un arreglo de parámetros
                    ReportParameter[] rp = { parametro };
                    //Ahora agregamos el parámetro en al reporte
                    //StreamWriter sr = new StreamWriter("C:\\inetpub\\wwwroot\\archivo.txt");
                    //Vas escribiendo el texto
                    //sr.WriteLine(parametros.ToString());
                    //Lo cierras       
                    //sr.Close();
                    ReportViewer1.ServerReport.SetParameters(rp);
                }
            }
            ReportViewer1.ServerReport.Refresh();
        }
    }
}