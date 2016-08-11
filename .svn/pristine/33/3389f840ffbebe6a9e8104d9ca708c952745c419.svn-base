<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MatrizComparativaColumnasWS[]>" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    function onDataBindingMatrizComparativa(e) {
        e.data = $.extend(e.data, { idCompra: _DatosRegistro_proCompra.comId });
    }
    
    function onCommandMatrizComparativa(e) {
        switch (e.name) {
            case "cmdModificarMatrizComparativa":
                onAccionMatrizComparativa("Modificar", e.dataItem['segId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminarMatrizComparativa":
                onAccionMatrizComparativa("Eliminar", e.dataItem['segId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultarMatrizComparativa":
                onAccionMatrizComparativa("Consultar", e.dataItem['segId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }
    
    function onAccionMatrizComparativa(Accion, idMatrizComparativa) {
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

        var _WindowMatrizComparativa = $("#wCRUDMatrizComparativaCompras").data("tWindow");
        $('#wCRUDMatrizComparativaCompras').css({ 'height': 510, 'width': 552 });
        $('#wCRUDMatrizComparativaCompras').find('div.t-window-content').css({ 'height': 480, 'width': 540 });
        _WindowMatrizComparativa.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        
        ////////////////////////////////////////
        var _Post = GetPathApp("proCompra/getMatrizComparativa");
        $.ajax({
            url: _Post,
            data: { Accion: Accion, segId: idMatrizComparativa, idCompra: _DatosRegistro_proCompra.comId },
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
                var _WindowCRUD = $("#wCRUDMatrizComparativaCompras").data("tWindow");
                _WindowCRUD.content(data);
            }
        });
        ////////////////////////////////////////
        //_WindowMatrizComparativa.ajaxRequest(GetPathApp("proCompra/getMatrizComparativa") + "?Accion=" + Accion + "&segId=" + idMatrizComparativa + "&idCompra=" + _DatosRegistro_proCompra.comId);
        _WindowMatrizComparativa.center().title(Accion).open();

    }

</script>

<%
    ViewData["MatrizComparativa"] = new List<MatrizComparativaWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<MatrizComparativaWS>)ViewData["MatrizComparativa"])
.Name("gridMatrizComparativa")
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <label id="lblMatrizComparativa" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_BindingMatrizComparativa", "proCompra", new { idCompra = 0 });
})
.Columns(columns =>
{
    columns.Bound(c => c.comdDetalle).Width("80px").Title("Detalle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= comdDetalle #>' style='cursor: pointer;' style='white-space: nowrap;'><#= comdDetalle #> </label>");
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
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingMatrizComparativa").OnCommand("onCommandMatrizComparativa"))
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

