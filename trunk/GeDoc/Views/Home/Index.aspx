﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" UICulture="es-AR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if (hi_EstadoSession.value == "False") {
                location.reload();
                jQuery(document).empty();
                var _Post = GetPathApp('Account/LogOff');
                window.location.replace(_Post);
            }
        });
    </script>
    <% 
           var EstadoSession = (Session["Usuario"] != null);
    %>

    <input type="hidden" id="hi_EstadoSession" value="<%= EstadoSession %>" />
    <div id="divHome">
        <div class="BordeRedondeado" id="divYsaSS"  style=" width:99% !important; padding: 8px; margin-bottom: 8px; z-index: 0; position: relative; text-align: left;" align="center">
        <%: Html.Partial("YsaUserControls/_YsaSS", new GeDoc.Models.YsaSSModel(xpath: "cat",
                                xmlSource: "/GeDoc/xml/Cats.xml", width: 1224,
                            height:500))
                            %>
        </div>
        <div class="BordeRedondeado" style="text-align: center">
            <p>
                Resolución óptima del sistema "1280 x 800"
            </p>
        </div>
    </div>
<script src="<%= Url.Content("~/Scripts/simplegallery.js") %>" type="text/javascript"></script>
</asp:Content>
