<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    function onCommandEditOfertas(e) {
        $('#Resolucion').blur(function () {
            if ($('#Resolucion').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('catPersona/getExisteResolucion');
                $.ajax({
                    url: _Post,
                    data: { Resolucion: $('#Resolucion').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#Resolucion').val("");
                            $('#Resolucion').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('La Resolución que ha ingresado no existe.', '¡Atención!', function () {
                                $('#Resolucion').val("");
                                $('#Resolucion').focus();
                            });
                            $('#popup_container').focus();
                            $('#popup_ok').focus();
                        }
                    }
                });
            }
        });

        _WindowCRUD = $("#gridOfertasPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBindingOfertas(e) {
        e.data = $.extend(e.data, { idCompra: _DatosRegistro_proCompra.comId });
    }
    
    function onCommandOfertas(e) {
        switch (e.name) {
            case "cmdModificarOferta":
                onAccionOfertas("Modificar", e.dataItem['comoId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdEliminarOferta":
                onAccionOfertas("Eliminar", e.dataItem['comoId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdConsultarOferta":
                onAccionOfertas("Consultar", e.dataItem['comoId']);
                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }
    
    function onAccionOfertas(Accion, idOferta) {
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

        var _WindowOfertas = $("#wCRUDOfertasCompras").data("tWindow");
        _WindowOfertas.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        _WindowOfertas.ajaxRequest(GetPathApp("proCompra/getOferta") + "?Accion=" + Accion + "&comoId=" + idOferta + "&idCompra=" + _DatosRegistro_proCompra.comId);
        _WindowOfertas.center().title(Accion).open();

    }

</script>

<%
    ViewData["ComprasOfertas"] = new List<proCompraOfertaWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<proCompraOfertaWS>)ViewData["ComprasOfertas"])
.Name("gridOfertas")
.DataKeys(keys =>
{
    keys.Add(p => p.comoId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar una Oferta" class="t-button" onclick="onAccionOfertas('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px;" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <label id="lblOfertasCompras" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_BindingOfertas", "proCompra", new { idCompra = 0 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificarOferta")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
            .HtmlAttributes(new { title = "Modificar Oferta" });
        commands.Custom("cmdEliminarOferta")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar Oferta" });
        commands.Custom("cmdConsultarOferta")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar Oferta" });
    }).Width("80px").Title("Acciones");
    columns.Bound(c => c.comoNumeroPresupuesto).Width("110px").Title("Nº Presupuesto").Visible(true);
    columns.Bound(c => c.comoFechaPresupuesto).Format("{0:dd/MM/yyyy}").Width("120px").Title("Fecha Presupuesto").Visible(true);
    columns.Bound(c => c.comoDiasValidezOferta).Width("100px").Title("Validez Oferta").Visible(true);
    columns.Bound(c => c.comoProveedorNombre).Width("200px").Title("Proveedor").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= comoProveedorNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= comoProveedorNombre #> </label>");
    columns.Bound(c => c.comoProveedorCUIT).Width("100px").Title("CUIT").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= comoProveedorCUIT #>' style='cursor: pointer;' style='white-space: nowrap;'><#= comoProveedorCUIT #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingOfertas").OnCommand("onCommandOfertas"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>
