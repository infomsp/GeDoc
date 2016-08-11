<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<!-- TAB  -->
<input type="hidden" id="h_Catalogo" value="<%: ViewBag.Catalogo %>" />
<% Html.Telerik().TabStrip()
        .Name("tabResultado")
        .HtmlAttributes(new { style = "height: 98%; padding: 0px; background: transparent; border: 0px; margin-left: -4px;" })
        .Items(tabstrip =>
        {
            tabstrip.Add()
                .Text("Bandeja de Entrada")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() => { Html.RenderPartial("BandejaDeEntrada"); });
            tabstrip.Add()
                .Text("Mi Bandeja")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() => { Html.RenderPartial("Index"); });
            tabstrip.Add()
                .Text("Iniciados por Mi")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() => { Html.RenderPartial("BandejaIniciadosPorMi"); });
            tabstrip.Add()
                .Text("Bandeja de Salida")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() => { Html.RenderPartial("BandejaDeSalida"); });
        })
    .SelectedIndex(0)
    .Render();
 %>
<!-- FIN TAB -->
