<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true"
    CodeBehind="EjecutarReportes.aspx.cs" Inherits="_EjecutarReportes" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
  <script type="text/javascript" language="javascript">
  </script>
</head>
<body onunload="alert('unload');" onload="alert('load')">
    <form id="frmReportes" runat="server">
    <div>
        <img src="../Content/2013.2.611/Hay/loading.gif" />
        <label>Cargando informe...</label>
    </div>
    <div style="height: 527px; width: 570px">
        <rsweb:ReportViewer ID="ReportViewer1" ShowExportControls="true" ShowFindControls="true" ShowPrintButton="true" ShowReportBody="true" ShowPromptAreaButton="true" ShowToolBar="true" ShowZoomControl="true" ProcessingMode="Remote" runat="server" Height="792px" Width="1200px">
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    </form>
</body>
</html>
