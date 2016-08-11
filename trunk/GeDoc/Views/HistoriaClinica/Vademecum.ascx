<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<%
    Html.RenderPartial("WaitingCRUD");
%>

<script type="text/javascript">
    function onCommandEdit_catHCVademecum(e) {
        _WindowCRUD = $("#gridcatHCVademecumPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBinding_catHCVademecum(e) {
    }

    function onCommand_catHCVademecum(e) {
        switch (e.name) {
            case "cmdModificar_catHCVademecum":
                onAccion_catHCVademecum("Modificar", e.dataItem['vadId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminar_catHCVademecum":
                onAccion_catHCVademecum("Eliminar", e.dataItem['vadId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultar_catHCVademecum":
                onAccion_catHCVademecum("Consultar", e.dataItem['vadId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }

    function onAccion_catHCVademecum(pAccion, idClinicaMedica) {
        switch (pAccion) {
            case "Agregar":
                break;
            case "Modificar":
                break;
            case "Eliminar":
                break;
            case "Consultar":
                break;
        }

        var wCliniMedica = $("#wCRUDcatHCVademecum").data("tWindow");
        $('#wCRUDcatHCVademecum').css({ 'height': 280, 'width': 512 });
        $('#wCRUDcatHCVademecum').find('div.t-window-content').css({ 'height': 250, 'width': 500 });
        wCliniMedica.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        wCliniMedica.ajaxRequest(GetPathApp("HistoriaClinica/getcatHCVademecum") + "?pAccion=" + pAccion + "&vadId=" + idClinicaMedica);
        wCliniMedica.center().title(pAccion).open();
    }

</script>

<%
    ViewData["vd_catHCVademecum"] = new List<catHCVademecumWS>();
    %>
<div id="divGridPrincipalcatHCVademecum">
<% Html.Telerik().Grid((IEnumerable<catHCVademecumWS>)ViewData["vd_catHCVademecum"])
.Name("gridcatHCVademecum")
.DataKeys(keys =>
{
    keys.Add(p => p.vadId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar" class="t-button" onclick="onAccion_catHCVademecum('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px; display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Agregar")%>" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_Binding_catHCVademecum", "HistoriaClinica", new { cssId = 0 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificar_catHCVademecum")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Modificar") });
        commands.Custom("cmdEliminar_catHCVademecum")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Eliminar") });
        commands.Custom("cmdConsultar_catHCVademecum")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar" });
    }).Width("110px").Title("Acciones");
    columns.Bound(c => c.vadCodigo).Width("100px").Title("Código").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"});
    columns.Bound(c => c.vadDescripcion).Width("200px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= vadDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= vadDescripcion #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding_catHCVademecum").OnCommand("onCommand_catHCVademecum"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>

<% Html.Telerik().Window()
        .Name("wCRUDcatHCVademecum")
        .Title("Vademecum")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Modal(true)
       .Scrollable(false)
       .Width(1000)
       .Height(400)
       .Render();
%>

