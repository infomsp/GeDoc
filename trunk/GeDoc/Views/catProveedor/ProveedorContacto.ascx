<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    function onCommandEdit_catProveedorContacto(e) {
        _WindowCRUD = $("#gridcatProveedorContactoPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBinding_catProveedorContacto(e) {
        e.data = $.extend(e.data, { provId: _DatosRegistro_catProveedor.provId });
    }
    
    function onCommand_catProveedorContacto(e) {
        switch (e.name) {
            case "cmdModificar_catProveedorContacto":
                onAccion_catProveedorContacto("Modificar", e.dataItem['pcId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminar_catProveedorContacto":
                onAccion_catProveedorContacto("Eliminar", e.dataItem['pcId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultar_catProveedorContacto":
                onAccion_catProveedorContacto("Consultar", e.dataItem['pcId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }
    
    function onAccion_catProveedorContacto(pAccion, idClinicaMedica) {
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

        var wCliniMedica = $("#wCRUDcatProveedorContacto").data("tWindow");
        $('#wCRUDcatProveedorContacto').css({ 'height': 230, 'width': 462 });
        $('#wCRUDcatProveedorContacto').find('div.t-window-content').css({ 'height': 200, 'width': 450 });
        wCliniMedica.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        wCliniMedica.ajaxRequest(GetPathApp("catProveedor/getcatProveedorContacto") + "?pAccion=" + pAccion + "&pcId=" + idClinicaMedica + "&provId=" + _DatosRegistro_catProveedor.provId);
        wCliniMedica.center().title(pAccion).open();
    }

</script>

<%
    ViewData["vd_catProveedorContacto"] = new List<catProveedorContactoWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<catProveedorContactoWS>)ViewData["vd_catProveedorContacto"])
.Name("gridcatProveedorContacto")
.DataKeys(keys =>
{
    keys.Add(p => p.pcId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar" class="t-button" onclick="onAccion_catProveedorContacto('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px; display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Agregar")%>" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <label id="lblPaciente" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_Binding_catProveedorContacto", "catProveedor", new { provId = 0 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificar_catProveedorContacto")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Modificar") });
        commands.Custom("cmdEliminar_catProveedorContacto")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Eliminar") });
        commands.Custom("cmdConsultar_catProveedorContacto")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar" });
    }).Width("110px").Title("Acciones");
    columns.Bound(c => c.pcApellidoyNombre).Width("200px").Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= pcApellidoyNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= pcApellidoyNombre #> </label>");
    columns.Bound(c => c.pcTelefono).Width("250px").Title("Teléfono").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= pcTelefono #>' style='cursor: pointer;' style='white-space: nowrap;'><#= pcTelefono #> </label>");
    columns.Bound(c => c.pcCorreoElectronico).Width("250px").Title("Correo Electrónico").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= pcCorreoElectronico #>' style='cursor: pointer;' style='white-space: nowrap;'><#= pcCorreoElectronico #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding_catProveedorContacto").OnCommand("onCommand_catProveedorContacto"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>
