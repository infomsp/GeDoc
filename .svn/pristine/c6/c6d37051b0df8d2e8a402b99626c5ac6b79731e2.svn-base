<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    function onDataBindingSeguimiento(e) {
        e.data = $.extend(e.data, { idCompra: _DatosRegistro_proCompra.comId });
    }
    
    function onCommandSeguimiento(e) {
        switch (e.name) {
            case "cmdModificarSeguimiento":
                onAccionSeguimiento("Modificar", e.dataItem['segId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminarSeguimiento":
                onAccionSeguimiento("Eliminar", e.dataItem['segId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultarSeguimiento":
                onAccionSeguimiento("Consultar", e.dataItem['segId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }
    
    function onAccionSeguimiento(Accion, idSeguimiento) {
        switch (Accion) {
            case "Agregar":
                break;
            case "Modificar":
                break;
            case "Eliminar":
                break;
            case "Consultar":
                break;
        }

        var _WindowSeguimiento = $("#wCRUDSeguimientoCompras").data("tWindow");
        $('#wCRUDSeguimientoCompras').css({ 'height': 510, 'width': 552 });
        $('#wCRUDSeguimientoCompras').find('div.t-window-content').css({ 'height': 480, 'width': 540 });
        _WindowSeguimiento.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        
        ////////////////////////////////////////
        var _Post = GetPathApp("proCompra/getSeguimiento");
        $.ajax({
            url: _Post,
            data: { Accion: Accion, segId: idSeguimiento, idCompra: _DatosRegistro_proCompra.comId },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                });
                $('#popup_container').focus();
                $('#popup_ok').focus();
            },
            success: function (data) {
                CerrarWaiting();
                var _WindowCRUD = $("#wCRUDSeguimientoCompras").data("tWindow");
                _WindowCRUD.content(data);
            }
        });
        ////////////////////////////////////////
        //_WindowSeguimiento.ajaxRequest(GetPathApp("proCompra/getSeguimiento") + "?Accion=" + Accion + "&segId=" + idSeguimiento + "&idCompra=" + _DatosRegistro_proCompra.comId);
        _WindowSeguimiento.center().title(Accion).open();

    }

</script>

<%
    ViewData["ComprasSeguimiento"] = new List<proCompraSeguimientoWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<proCompraSeguimientoWS>)ViewData["ComprasSeguimiento"])
.Name("gridSeguimiento")
.DataKeys(keys =>
{
    keys.Add(p => p.segId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar Notificación" class="t-button" onclick="onAccionSeguimiento('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px;" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <label id="lblSeguimientoCompras" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_BindingSeguimiento", "proCompra", new { idCompra = 0 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificarSeguimiento")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar Seguimiento" });
        commands.Custom("cmdEliminarSeguimiento")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar Seguimiento" });
        commands.Custom("cmdConsultarSeguimiento")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar Seguimiento" });
    }).Width("110px").Title("Acciones");
    columns.Bound(c => c.TipoNotificacion.tipoDescripcion).Width("80px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "<#= TipoNotificacion.tipoImagen #>' title='<#= TipoNotificacion.tipoDescripcion #>' height='22px' width='22px' style='vertical-align:middle;' /></div>");
    columns.Bound(c => c.segFecha).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha").Visible(true);
    columns.Bound(c => c.segFecha).Format("{0:hh:mm:ss}").Width("80px").Title("Hora").Visible(true);
    columns.Bound(c => c.Proveedor).Width("200px").Title("Proveedor").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= Proveedor #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Proveedor #> </label>");
    columns.Bound(c => c.Contacto).Width("200px").Title("Contacto").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= Contacto #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Contacto #> </label>");
    columns.Bound(c => c.segRespuesta).Width("200px").Title("Respuesta").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= segRespuesta #>' style='cursor: pointer;' style='white-space: nowrap;'><#= segRespuesta #> </label>");
    columns.Bound(c => c.segObservaciones).Width("200px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= segObservaciones #>' style='cursor: pointer;' style='white-space: nowrap;'><#= segObservaciones #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingSeguimiento").OnCommand("onCommandSeguimiento"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>

<% Html.Telerik().Window()
        .Name("wEnvioCorreoElectronico")
        .Title("Envío de Correo Electrónico")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Clear())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(600)
       .Render();
%>

