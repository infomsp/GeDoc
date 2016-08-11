using System;

using ReportingServiceLib.ReportingWebService;

namespace ReportingServiceLib
{
    #region enum ExportFormat
    /// <summary>
    /// Export Formats.
    /// </summary>
    public enum ExportFormat
    {
        /// <summary>XML</summary>
        XML,
        /// <summary>Comma Delimitted File
        CSV,
        /// <summary>TIFF image</summary>
        Image,
        /// <summary>PDF</summary>
        PDF,
        /// <summary>HTML (Web Archive)</summary>
        MHTML,
        /// <summary>HTML 4.0</summary>
        HTML4,
        /// <summary>HTML 3.2</summary>
        HTML32,
        /// <summary>Excel</summary>
        Excel,
        /// <summary>Word</summary>
        Word
    }
    #endregion

    /// <summary>
    /// Utility class that renders and exports a SQL Reporting Services report into the specified output format.
    /// </summary>
    public static class ReportExporter
    {
        #region Method: GetExportFormatString(ExportFormat f)
        /// <summary>
        /// Gets the string export format of the specified enum.
        /// </summary>
        /// <param name="f">export format enum</param>
        /// <returns>enum equivalent string export format</returns>
        private static string GetExportFormatString(ExportFormat f)
        {
            switch (f)
            {
                case ExportFormat.XML: return "XML";
                case ExportFormat.CSV: return "CSV";
                case ExportFormat.Image: return "IMAGE";
                case ExportFormat.PDF: return "PDF";
                case ExportFormat.MHTML: return "MHTML";
                case ExportFormat.HTML4: return "HTML4.0";
                case ExportFormat.HTML32: return "HTML3.2";
                case ExportFormat.Excel: return "EXCEL";
                case ExportFormat.Word: return "WORD";

                default:
                    return "PDF";
            }
        }
        #endregion

        #region Method: Export( ... )
        /// <summary>
        /// Exports a Reporting Service Report to the specified format using Windows Communication Foundation (WCF) endpoint configuration specified.
        /// </summary>
        /// <param name="wcfEndpointConfigName">WCF Endpoint name</param>
        /// <param name="clientCredentials">Network Credential to use to connect to the web service</param>
        /// <param name="report">Reporting Services Report to execute</param>
        /// <param name="parameters">report parameters</param>
        /// <param name="format">export output format (e.g., XML, CSV, IMAGE, PDF, HTML4.0, HTML3.2, MHTML, EXCEL, and HTMLOWC)</param>
        /// <param name="output">rendering output result in bytes</param>
        /// <param name="extension">output format file extension</param>
        /// <param name="mimeType">output MIME type</param>
        /// <param name="encoding">output encoding</param>
        /// <param name="warnings">warnings (if any)</param>
        /// <param name="streamIds">stream identifiers for external resources (images, etc) that are associated with a given report</param>
        public static void Export(
            string wcfEndpointConfigName, System.Net.NetworkCredential clientCredentials, string report, ParameterValue[] parameters,
            ExportFormat format, out byte[] output, out string extension, out string mimeType, out string encoding, out Warning[] warnings, out string[] streamIds)
        {
            using (var webServiceProxy = new ReportExecutionServiceSoapClient(wcfEndpointConfigName))
            {
                webServiceProxy.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
                webServiceProxy.ClientCredentials.Windows.ClientCredential = clientCredentials;

                // Init Report to execute
                ServerInfoHeader serverInfoHeader;
                ExecutionInfo executionInfo;
                ExecutionHeader executionHeader = webServiceProxy.LoadReport(null, report, null, out serverInfoHeader, out executionInfo);

                // Attach Report Parameters
                webServiceProxy.SetExecutionParameters(executionHeader, null, parameters, null, out executionInfo);

                // Render
                webServiceProxy.Render(executionHeader, null, GetExportFormatString(format), null, out output, out extension, out mimeType, out encoding, out warnings, out streamIds);
            }
        }
        #endregion
    }
}