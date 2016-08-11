<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    function onCommandEdit_catCentroDeSaludSalaConsultorio(e) {
        _WindowCRUD = $("#gridcatCentroDeSaludSalaConsultorioPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBinding_catCentroDeSaludSalaConsultorio(e) {
        e.data = $.extend(e.data, { cssId: _DatosRegistro_catCentroDeSaludSala.cssId });
    }

    function onCommand_catCentroDeSaludSalaConsultorio(e) {
        switch (e.name) {
            case "cmdModificar_catCentroDeSaludSalaConsultorio":
                onAccion_catCentroDeSaludSalaConsultorio("Modificar", e.dataItem['csscId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminar_catCentroDeSaludSalaConsultorio":
                onAccion_catCentroDeSaludSalaConsultorio("Eliminar", e.dataItem['csscId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultar_catCentroDeSaludSalaConsultorio":
                onAccion_catCentroDeSaludSalaConsultorio("Consultar", e.dataItem['csscId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }

    function onAccion_catCentroDeSaludSalaConsultorio(pAccion, idClinicaMedica) {
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

        var wCliniMedica = $("#wCRUDcatCentroDeSaludSalaConsultorio").data("tWindow");
        $('#wCRUDcatCentroDeSaludSalaConsultorio').css({ 'height': 150, 'width': 412 });
        $('#wCRUDcatCentroDeSaludSalaConsultorio').find('div.t-window-content').css({ 'height': 120, 'width': 400 });
        wCliniMedica.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        wCliniMedica.ajaxRequest(GetPathApp("catCentroDeSaludSala/getcatCentroDeSaludSalaConsultorio") + "?pAccion=" + pAccion + "&csscId=" + idClinicaMedica + "&cssId=" + _DatosRegistro_catCentroDeSaludSala.cssId);
        wCliniMedica.center().title(pAccion).open();
    }

</script>

<%
    ViewData["vd_catCentroDeSaludSalaConsultorio"] = new List<catCentroDeSaludSalaConsultorioWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<catCentroDeSaludSalaConsultorioWS>)ViewData["vd_catCentroDeSaludSalaConsultorio"])
.Name("gridcatCentroDeSaludSalaConsultorio")
.DataKeys(keys =>
{
    keys.Add(p => p.csscId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar" class="t-button" onclick="onAccion_catCentroDeSaludSalaConsultorio('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px; display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Agregar")%>" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <label id="lblPaciente" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_Binding_catCentroDeSaludSalaConsultorio", "catCentroDeSaludSala", new { cssId = 0 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificar_catCentroDeSaludSalaConsultorio")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Modificar") });
        commands.Custom("cmdEliminar_catCentroDeSaludSalaConsultorio")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Eliminar") });
        commands.Custom("cmdConsultar_catCentroDeSaludSalaConsultorio")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar" });
    }).Width("110px").Title("Acciones");
    columns.Bound(c => c.csscNombre).Width("200px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= csscNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= csscNombre #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding_catCentroDeSaludSalaConsultorio").OnCommand("onCommand_catCentroDeSaludSalaConsultorio"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>
