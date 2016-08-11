<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<%
    Html.RenderPartial("WaitingCRUD");
%>

<script type="text/javascript">
    function onCommandEdit_catHCProblemaCronico(e) {
        _WindowCRUD = $("#gridcatHCProblemaCronicoPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBinding_catHCProblemaCronico(e) {
    }

    function onCommand_catHCProblemaCronico(e) {
        switch (e.name) {
            case "cmdModificar_catHCProblemaCronico":
                onAccion_catHCProblemaCronico("Modificar", e.dataItem['pcroId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminar_catHCProblemaCronico":
                onAccion_catHCProblemaCronico("Eliminar", e.dataItem['pcroId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultar_catHCProblemaCronico":
                onAccion_catHCProblemaCronico("Consultar", e.dataItem['pcroId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }

    function onAccion_catHCProblemaCronico(pAccion, idClinicaMedica) {
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

        var wCliniMedica = $("#wCRUDcatHCProblemaCronico").data("tWindow");
        $('#wCRUDcatHCProblemaCronico').css({ 'height': 280, 'width': 512 });
        $('#wCRUDcatHCProblemaCronico').find('div.t-window-content').css({ 'height': 250, 'width': 500 });
        wCliniMedica.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        wCliniMedica.ajaxRequest(GetPathApp("HistoriaClinica/getcatHCProblemaCronico") + "?pAccion=" + pAccion + "&pcroId=" + idClinicaMedica);
        wCliniMedica.center().title(pAccion).open();
    }

</script>

<%
    ViewData["vd_catHCProblemaCronico"] = new List<catHCProblemaCronicoWS>();
    %>
<div id="divGridPrincipalcatHCProblemaCronico">
<% Html.Telerik().Grid((IEnumerable<catHCProblemaCronicoWS>)ViewData["vd_catHCProblemaCronico"])
.Name("gridcatHCProblemaCronico")
.DataKeys(keys =>
{
    keys.Add(p => p.pcroId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar" class="t-button" onclick="onAccion_catHCProblemaCronico('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px; display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Agregar")%>" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_Binding_catHCProblemaCronico", "HistoriaClinica", new { cssId = 0 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificar_catHCProblemaCronico")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Modificar") });
        commands.Custom("cmdEliminar_catHCProblemaCronico")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Eliminar") });
        commands.Custom("cmdConsultar_catHCProblemaCronico")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar" });
    }).Width("110px").Title("Acciones");
    columns.Bound(c => c.pcroCodigo).Width("100px").Title("Código").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"});
    columns.Bound(c => c.pcroDescripcion).Width("200px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= pcroDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= pcroDescripcion #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding_catHCProblemaCronico").OnCommand("onCommand_catHCProblemaCronico"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>

<% Html.Telerik().Window()
        .Name("wCRUDcatHCProblemaCronico")
        .Title("Problemas Crónicos")
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

