<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.RenderPartial("Waiting"); %>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/Reportes.js") %>"></script>
<!-- Reportes -->
<% Html.Telerik().Window()
        .Name("wImprimir")
        .Title("Imprimir...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <iframe id="frameReportes" onload="CerrarWaiting();" src='' width="100%" height="100%" >
                </iframe>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Modal(true)
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(430)
       .Height(600)
       //.ContentHtmlAttributes(new { style = "height: 100%;" })
       .ClientEvents(ev => ev.OnResize("onResizeWindow"))
       .Render();
%>
