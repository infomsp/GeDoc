<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var _RowIndex_catProveedor = -1;
    var _DatosRegistro_catProveedor;
    
    function onRegistroActual_catProveedor(e) {
        var row = e.row;
        var grid = $("#gridcatProveedor").data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex_catProveedor = e.row.rowIndex;
        _DatosRegistro_catProveedor = dataItem;
    }

    function onCommand_catProveedor(e) {
        switch (e.name) {
        case "cmdModificar_catProveedor":
            onAccion_catProveedor("Modificar", e.dataItem['provId']);
            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdEliminar_catProveedor":
            onAccion_catProveedor("Eliminar", e.dataItem['provId']);
            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdConsultar_catProveedor":
            onAccion_catProveedor("Consultar", e.dataItem['provId']);
            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdContactos_catProveedor":
            debugger;
            onRegistroActual_catProveedor(e);

            var wGrilla = $("#wcatProveedorContacto").data("tWindow");
            $('#wcatProveedorContacto').css({ 'height': 430, 'width': 1000 });
            $('#wcatProveedorContacto').find('div.t-window-content').css({ 'height': 400, 'width': 988 });
            wGrilla.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
            wGrilla.ajaxRequest(GetPathApp("catProveedor/InformescatProveedorContacto"));
            wGrilla.title("Contactos del proveedor " + _DatosRegistro_catProveedor.provRazonSocial).center().open();

            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdRubros_catProveedor":
            onRegistroActual_catProveedor(e);

            var wGrilla = $("#wcatProveedorRubro").data("tWindow");
            $('#wcatProveedorRubro').css({ 'height': 430, 'width': 1000 });
            $('#wcatProveedorRubro').find('div.t-window-content').css({ 'height': 400, 'width': 988 });
            wGrilla.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
            wGrilla.ajaxRequest(GetPathApp("catProveedor/InformescatProveedorRubro"));
            wGrilla.title("Rubros del proveedor " + _DatosRegistro_catProveedor.provRazonSocial).center().open();

            e.preventDefault();
            e.stopPropagation();
            break;
    }
    }
    
    function onAccion_catProveedor(pAccion, idRegistro) {
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

        var wCliniMedica = $("#wCRUDcatProveedor").data("tWindow");
        $('#wCRUDcatProveedor').css({ 'height': 250, 'width': 1012 });
        $('#wCRUDcatProveedor').find('div.t-window-content').css({ 'height': 220, 'width': 1000 });
        wCliniMedica.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        wCliniMedica.ajaxRequest(GetPathApp("catProveedor/getcatProveedor") + "?pAccion=" + pAccion + "&provId=" + idRegistro);
        wCliniMedica.center().title(pAccion).open();
    }

</script>

<%
    ViewData["vd_catProveedor"] = new List<catProveedorWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<catProveedorWS>)ViewData["vd_catProveedor"])
.Name("gridcatProveedor")
.DataKeys(keys =>
{
    keys.Add(p => p.provId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar" class="t-button" onclick="onAccion_catProveedor('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px; display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Agregar")%>" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <label id="lblPaciente" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_Binding_catProveedor", "catProveedor");
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificar_catProveedor")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Modificar") });
        commands.Custom("cmdEliminar_catProveedor")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProveedor", "Eliminar") });
        commands.Custom("cmdConsultar_catProveedor")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar" });
        commands.Custom("cmdContactos_catProveedor")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -192px;" })
            .HtmlAttributes(new { title = "Contactos" });
        commands.Custom("cmdRubros_catProveedor")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -208px;" })
            .HtmlAttributes(new { title = "Rubros" });
    }).Width("180px").Title("Acciones");
    columns.Bound(c => c.provRubro).Width("200px").Title("Rubro(s)").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= provRubro #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provRubro #> </label>");
    columns.Bound(c => c.provRazonSocial).Width("200px").Title("Razón Social").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= provRazonSocial #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provRazonSocial #> </label>");
    columns.Bound(c => c.provNombreDeFantasia).Width("200px").Title("Nombre de Fantasía").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= provNombreDeFantasia #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provNombreDeFantasia #> </label>");
    columns.Bound(c => c.provCUIT).Width("110px").Title("CUIT").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= provCUIT #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provCUIT #> </label>");
    columns.Bound(c => c.provDomicilio).Width("250px").Title("Domicilio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= provDomicilio #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provDomicilio #> </label>");
    columns.Bound(c => c.provTelefono).Width("250px").Title("Teléfono").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= provTelefono #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provTelefono #> </label>");
    columns.Bound(c => c.provCorreoElectronico).Width("250px").Title("Correo Electrónico").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= provCorreoElectronico #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provCorreoElectronico #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnCommand("onCommand_catProveedor"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>

<% Html.Telerik().Window()
        .Name("wCRUDcatProveedor")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(510)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wcatProveedorContacto")
        .Title("Exámenes")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Modal(true)
       .Scrollable(false)
       .Resizable()
       .Width(1000)
       .Height(400)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wCRUDcatProveedorContacto")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(510)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wcatProveedorRubro")
        .Title("Exámenes")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Modal(true)
       .Scrollable(false)
       .Resizable()
       .Width(1000)
       .Height(400)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wCRUDcatProveedorRubro")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(510)
       .Render();
%>
