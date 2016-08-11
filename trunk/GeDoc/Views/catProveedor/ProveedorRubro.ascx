<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    function onCommandEdit_catProveedorRubro(e) {
        _WindowCRUD = $("#gridcatProveedorRubroPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBinding_catProveedorRubro(e) {
        e.data = $.extend(e.data, { provId: _DatosRegistro_catProveedor.provId });
    }
    
    function onCommand_catProveedorRubro(e) {
        switch (e.name) {
            case "cmdModificar_catProveedorRubro":
                onAccion_catProveedorRubro("Modificar", e.dataItem['prId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminar_catProveedorRubro":
                onAccion_catProveedorRubro("Eliminar", e.dataItem['prId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultar_catProveedorRubro":
                onAccion_catProveedorRubro("Consultar", e.dataItem['prId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }
    
    function onAccion_catProveedorRubro(pAccion, idClinicaMedica) {
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

        var wCliniMedica = $("#wCRUDcatProveedorRubro").data("tWindow");
        $('#wCRUDcatProveedorRubro').css({ 'height': 150, 'width': 412 });
        $('#wCRUDcatProveedorRubro').find('div.t-window-content').css({ 'height': 120, 'width': 400 });
        wCliniMedica.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        wCliniMedica.ajaxRequest(GetPathApp("catProveedor/getcatProveedorRubro") + "?pAccion=" + pAccion + "&prId=" + idClinicaMedica + "&provId=" + _DatosRegistro_catProveedor.provId);
        wCliniMedica.center().title(pAccion).open();
    }

</script>

<%
    ViewData["vd_catProveedorRubro"] = new List<catProveedorRubroWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<catProveedorRubroWS>)ViewData["vd_catProveedorRubro"])
.Name("gridcatProveedorRubro")
.DataKeys(keys =>
{
    keys.Add(p => p.prId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar" class="t-button" onclick="onAccion_catProveedorRubro('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px; display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Agregar")%>" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <label id="lblPaciente" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_Binding_catProveedorRubro", "catProveedor", new { provId = 0 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificar_catProveedorRubro")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Modificar") });
        commands.Custom("cmdEliminar_catProveedorRubro")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Eliminar") });
        commands.Custom("cmdConsultar_catProveedorRubro")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar" });
    }).Width("110px").Title("Acciones");
    columns.Bound(c => c.prRubro).Width("200px").Title("Rubro").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= prRubro #>' style='cursor: pointer;' style='white-space: nowrap;'><#= prRubro #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding_catProveedorRubro").OnCommand("onCommand_catProveedorRubro"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>
