<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    var idFactura;
    function formatoFecha(date, format) {
        format = format || '{0:d}';
        return $.telerik.formatString(format, date);
    }

    function onCommand(e) {
        if (e.name == "cmdPagos") {
            var estGrid = $('#gridPagos').data('tGrid');
            var _WindowEst = $("#CRUDPagos").data("tWindow");

            idFactura = e.dataItem['facId'];
            var _Facturado = e.dataItem['facImporteTexto'];

            $("#lblFacturado").text(_Facturado);

            estGrid.rebind();
            _WindowEst.center().open();
        } else if (e.name == "cmdReferencias") {
            var _WindowRef = $("#CRUDReferencia").data("tWindow");

            _WindowRef.center().open();
        } else if (e.name == "cmdReportes") {
            VerReporte();
        } else if (e.name == "cmdExportar") {
            var _WindowRef = $("#wExportar").data("tWindow");

            _WindowRef.center().open();
        }
    }

    function onSeleccionarTipoListado() {
        var dpDesde = $('#dtFechaDesde').data('tDatePicker');
        var dpHasta = $('#dtFechaHasta').data('tDatePicker');
        if ($('#TipoListado:checked').val() === "F") {
            dpDesde.disable();
            dpHasta.disable();
        } else {
            dpDesde.enable();
            dpHasta.enable();
        }
    }
    function onExportar() {
        var grid = $("#Grid").data('tGrid');
        _OrderBy = (grid.orderBy || '~');
        _FilterBy = (grid.filterBy || '~');
        debugger;

        var dpDesde = $('#dtFechaDesde').data('tDatePicker');
        var dpHasta = $('#dtFechaHasta').data('tDatePicker');
        var FechaDesde = formatoFecha(dpDesde.value(), "{0:yyyy/MM/dd}");
        var FechaHasta = formatoFecha(dpHasta.value(), "{0:yyyy/MM/dd}");

        switch ($('#TipoListado:checked').val()) {
        case "F":
            window.open(GetPathApp("catFactura/Exportar") + "?Filtro=" + _FilterBy + "&Orden=" + _OrderBy);
            break;
        case "D":
            window.open(GetPathApp("catFactura/ExportarDebitado") + "?Desde=" + FechaDesde + "&Hasta=" + FechaHasta);
            break;
        case "C":
            window.open(GetPathApp("catFactura/ExportarCobrado") + "?Desde=" + FechaDesde + "&Hasta=" + FechaHasta);
            break;
        case "P":
            window.open(GetPathApp("catFactura/ExportarPendiente") + "?Desde=" + FechaDesde + "&Hasta=" + FechaHasta);
            break;
        }

        var _WindowRef = $("#wExportar").data("tWindow");
        _WindowRef.close();
    }

    function onDataBindingPagos(e) {
        e.data = $.extend(e.data, { idFactura: idFactura });
    }
    function onDataBindingNotificaciones(e) {
        e.data = $.extend(e.data, { idFactura: idFactura });
    }
    function Historial() {
        var _WindowEst = $("#CRUDEstados").data("tWindow");
        _WindowEst.center().open();
    }
    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var notGrid = $('#gridNotificaciones').data('tGrid');
        var dataItem = grid.dataItem(row);

        idFactura = dataItem.facId;


        notGrid.rebind();
    }
    function onSavePagos(e) {
        var values = e.values;

        values.facId = idFactura;
    }
    function onSaveNotificaciones(e) {
        var values = e.values;

        values.facId = idFactura;
    }
    function onCompletePagos(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            var pagGrid = $('#Grid').data('tGrid');
            pagGrid.rebind();
        }
    }
    function onCompleteNotificaciones(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            var pagGrid = $('#Grid').data('tGrid');
            pagGrid.rebind();
        }
    }
    function VerReporte() {
        var _WindowImprimir = $("#wImprimir").data("tWindow");
        if (_WindowImprimir.isMaximized) {
            _WindowImprimir.center().open();
        }
        else {
            _WindowImprimir.center().maximize().open();
        }
        var dpDesde = $('#dtDesde').data('tDatePicker');
        var dpHasta = $('#dtHasta').data('tDatePicker');
        var _Desde = new Date(2010, 0, 1);
        dpDesde.value(_Desde);
        dpHasta.value(new Date());

        InvocarReporte();
    }
    function AbrirWaiting() {
        var _WindowWait = $("#wWaiting").data("tWindow");
        if (_WindowWait != null) {
            _WindowWait.center().open();
        }
    }
    function CerrarWaiting() {
        var _WindowWait = $("#wWaiting").data("tWindow");
        if (_WindowWait != null) {
            _WindowWait.close();
        }
    }
    function InvocarReporte() {
        //Verificamos que sea diferente de vacio
        var dpDesde = $('#dtDesde').data('tDatePicker');
        var _Desde = dpDesde.inputValue;
        if (_Desde == null) {
            MostrarError('La fecha inicial es incorrecta');
            return;
        }
        var dpHasta = $('#dtHasta').data('tDatePicker');
        var _Hasta = dpHasta.inputValue;
        if (_Hasta == null) {
            MostrarError('La fecha final es incorrecta');
            return;
        }
        if (dpDesde.value() > new Date()) {
            MostrarError('La fecha inicial debe se menor o igual a la fecha actual');
            return;
        }
        if (dpHasta.value() > new Date()) {
            MostrarError('La fecha final debe se menor o igual a la fecha actual');
            return;
        }
        if (dpHasta.value() < dpDesde.value()) {
            MostrarError('Las fechas no deben superponerse');
            return;
        }
        var psParametros = new Array(new Array(_Desde, 'Desde'), new Array(_Hasta, 'Hasta'));
        var psReporte = "rptEstadisticasFacturacionOS";
        if (psReporte != '' && psReporte != null) {
            var _vParametros = '';
            if (psParametros != null) {
                var _vParametros = '&ci=' + psParametros.length.toString();
                for (var _i = 0; _i < psParametros.length; _i++) {
                    _vParametros += '&v' + _i.toString() + '=' + psParametros[_i][0];
                    _vParametros += '&n' + _i.toString() + '=' + psParametros[_i][1];
                }
            }
            var _src = GetPathApp('Reportes/EjecutarReportes.aspx?sr=' + psReporte + _vParametros);
            $('#frameReportes').attr('src', _src);
            var _WindowImprimir = $("#wImprimir").data("tWindow");
            if (_WindowImprimir.isMaximized) {
                _WindowImprimir.center().open();
            }
            else {
                _WindowImprimir.center().maximize().open();
            }
            AbrirWaiting();
        }
    }

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    function onDataBound() {
        var grid = $(this).data('tGrid');  
        _CurrentPage = grid.currentPage;
        _OrderBy = (grid.orderBy || '~');
        _FilterBy = (grid.filterBy || '~');

        var _Boton = $('a.t-button.t-grid-cmdExportar');
        var href = _Boton.attr('href');

        href = href.replace(/page=([^&]*)/, 'page=' + _CurrentPage);
        href = href.replace(/orderBy=([^&]*)/, 'orderBy=' + (_OrderBy || '~'));
        href = href.replace(/filter=(.*)/, 'filter=' + (_FilterBy || '~'));
        _Boton.attr('href', href);
    }
</script>
<% ViewData["AltoEditor"] = "200px";
%>
<div style="overflow: hidden; height: 510px;" >
<% Html.Telerik().Grid<GeDoc.catFacturas>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.facId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
            .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFactura", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdReferencias").ButtonType(GridButtonType.Text).Text("Referencia de Estados");
            commands.Custom().Ajax(true).Name("cmdReportes").ButtonType(GridButtonType.Text).Text("Estadísticas");
            commands.Custom().Ajax(true).Name("cmdExportar").ButtonType(GridButtonType.Text).Text("Exportar")
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -240px;" });
            //commands.Custom().Action("Exportar", "catFactura", new { page = 1, orderBy = "~", filter = "~" }).Name("cmdExportar").ButtonType(GridButtonType.ImageAndText).Text("Exportar")
            //.ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -240px;" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catFactura")
                .Insert("_InsertEditing", "catFactura")
                .Update("_SaveEditing", "catFactura")
                .Delete("_DeleteEditing", "catFactura");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFactura", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFactura", "Eliminar") });
                commands.Custom("cmdPagos")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFactura", "Examinar") });
            }).Width("110px").Title("Acciones");
            columns.Bound(c => c.facEstado).Width("120px").Title("Estado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<div style='width: 100%;'><img src='" + Url.Content("~/Content") + "/Estados/<#= facEstadoImagen #>' title='<#= facEstado #>' height='22px' width='22px' style='vertical-align:middle; visibility: <#= VisibilidadImagen #>;' onclick='Historial();' /><label title='<#= facEstado #>' style='cursor: pointer; white-space: nowrap; margin-left: 5px;' id='txtfacEstado' ><#= facEstado #> </label></div>");
            columns.Bound(c => c.facDiasVtoSSS).Width("80px").Title("Días Restantes").Visible(true);
            columns.Bound(c => c.osNombre).Width("220px").Title("Obra Social").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= osNombre #>' style='cursor: pointer;' id='txtosNombre' style='white-space: nowrap;'><#= osNombre #> </label>");
            columns.Bound(c => c.sucNombre).Width("220px").Title("Centro de Salud").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= sucNombre #>' style='cursor: pointer;' id='txtsucNombre' style='white-space: nowrap;'><#= sucNombre #> </label>");
            columns.Bound(c => c.Periodo).Width("130px").Title("Periodo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Periodo #>' style='cursor: pointer;' id='txtPeriodo' style='white-space: nowrap;'><#= Periodo #> </label>");
            columns.Bound(c => c.facNumero).Width("150px").Title("Número").Visible(true);
            columns.Bound(c => c.facFecha).Width("90px").Title("Fecha").Visible(true);
            columns.Bound(c => c.facFechaRecepcion).Width("90px").Title("Recepción").Visible(true);
            columns.Bound(c => c.facImporte).Width("120px").Title("Debe")
                .Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:c}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.facHaber).Width("120px").Title("Haber")
                .Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:c}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.facSaldo).Width("120px").Title("Saldo")
                .Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:c}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
        })
            .Editable(editing => editing
            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) => paging.Enabled(true)
            .PageSize(((int)Session["FilasPorPagina"])))
            .ClientEvents(events => events.OnEdit("onCommandEdit").OnCommand("onCommand").OnRowSelected("onRowSelected").OnDataBound("onDataBound"))            
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(400))
            .Sortable()
            .HtmlAttributes(new { style = "width: 100%;" })
            .Resizable(resizing => resizing.Columns(true)).Render();
%>
</div>
<!-- Pagos -->
<% Html.Telerik().Window()
        .Name("CRUDPagos")
        .Title("Cobro de Facturas")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catFacturasPagos>)ViewData["Pagos"])
            .Name("gridPagos")
            .DataKeys(keys =>
            {
                keys.Add(p => p.pagId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingPagos", "catFactura")
                    .Update("_SaveEditingPagos", "catFactura")
                    .Insert("_InsertEditingPagos", "catFactura")
                    .Delete("_DeleteEditingPagos", "catFactura");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("7%").Title("Acciones");
                columns.Bound(c => c.pagTipo).Width("7%").Title("Tipo").Visible(true);
                columns.Bound(c => c.pagFecha).Width("10%").Title("Fecha").Visible(true);
                columns.Bound(c => c.pagNumeroRecibo).Width("9%").Title("N° Recibo").Visible(true);
                columns.Bound(c => c.pagForma).Width("10%").Title("Forma Pago")
                    .FooterTemplate(() =>
                            {%>
                                <div align="right">Facturado: </div>
                            <%}).Visible(true);
                columns.Bound(c => c.pagDetalle).Width("10%").Title("Detalle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                    .ClientTemplate("<label title='<#= pagDetalle #>' style='cursor: pointer;' id='txtpagDetalle' style='white-space: nowrap;'><#= pagDetalle #> </label>")
                    .FooterTemplate(() =>
                    {%>
                        <div align="right"><label id="lblFacturado" ></label></div>
                    <%});
                columns.Bound(c => c.pagMotivo).Width("10%").Title("Motivo Débito")
                    .HtmlAttributes(new { style = "white-space: nowrap;" })
                    .ClientTemplate("<label title='<#= pagMotivo #>' style='cursor: pointer;' id='txtpagMotivo' style='white-space: nowrap;'><#= pagMotivo #> </label>")
                    .FooterTemplate(() =>
                            {%>
                                <div align="right">Total Imputado: </div>
                            <%}).Visible(true);
                columns.Bound(c => c.pagImporte).Width("10%").Title("Importe")
                .Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:c}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingPagos", "catFactura", new { idFactura = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingPagos").OnEdit("onCommandEdit").OnSave("onSavePagos").OnComplete("onCompletePagos"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(280))
            .Sortable().Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(870)
       .Height(400)
       .Render();
%>

<!-- Estados -->
<% Html.Telerik().Window()
        .Name("CRUDEstados")
        .Title("Historial de Novedades")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catFacturasNotificaciones>)ViewData["Notificaciones"])
            .Name("gridNotificaciones")
            .DataKeys(keys =>
            {
                keys.Add(p => p.notId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingNotificaciones", "catFactura")
                    .Update("_SaveEditingNotificaciones", "catFactura")
                    .Insert("_InsertEditingNotificaciones", "catFactura")
                    .Delete("_DeleteEditingNotificaciones", "catFactura");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("9%").Title("Acciones");
                columns.Bound(c => c.notTipo).Width("21%").Title("Tipo").Visible(true);
                columns.Bound(c => c.notFecha).Width("12%").Title("Fecha").Visible(true);
                columns.Bound(c => c.notDetalle).Width("58%").Title("Detalle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                    .ClientTemplate("<label title='<#= notDetalle #>' style='cursor: pointer;' id='txtnotDetalle' style='white-space: nowrap;'><#= notDetalle #> </label>");
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingNotificaciones", "catFactura", new { idFactura = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingNotificaciones").OnEdit("onCommandEdit").OnSave("onSaveNotificaciones").OnComplete("onCompleteNotificaciones").OnDataBound("onDataBound"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(300))
            .Sortable().Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(870)
       .Height(400)
       .Render();
%>

<!-- Referencia de Estados -->
<% Html.Telerik().Window()
        .Name("CRUDReferencia")
        .Title("Estados...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Gris.png")%>' title='En término' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='txtEnTermino' >En término</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Verde.png")%>' title='Cobrado' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label1' >Cobrado</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Celeste.png")%>' title='Cobrado y Debitado' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label2' >Cobrado y Debitado</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/VerdeClaro.png")%>' title='Cobrado parcialmente' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label3' >Cobrado parcialmente</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Turquesa.png")%>' title='Debitado' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label9' >Debitado</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Amarillo.png")%>' title='Vencida' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label4' >Vencida</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Naranja.png")%>' title='Carta documento' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label5' >Carta documento</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Rojo.png")%>' title='Carta documento vencida' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label6' >Carta documento vencida</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Azul.png")%>' title='Reclamo enviado a la Super Intendencia de Seguro de Salud' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label7' >Reclamo enviado a la Super Intendencia de Seguro de Salud</label></div>
            <div style='width: 20px; white-space: nowrap; margin: 5px 5px 0px 5px; vertical-align: middle;'><img src='<%= Url.Content("~/Content/Estados/Violeta.png")%>' title='Sin reclamo a la Super Intendencia de Seguro de Salud' height='22px' width='22px' style='vertical-align:middle; visibility: visible;' onclick='Historial();' /><label style='white-space: nowrap; margin-left: 5px;' id='Label8' >Sin reclamo a la Super Intendencia de Seguro de Salud</label></div>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Modal(true)
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(430)
       .Height(280)
       .Render();
%>

<!-- Reportes -->
<% Html.Telerik().Window()
        .Name("wImprimir")
        .Title("Estadísticas...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <div>
                <label id="lblDesde" for="Customers-input" style="margin-left: 5px;">Desde:</label>
                <% Html.Telerik().DatePicker().Name("dtDesde").Render(); %>
                <label id="lblHasta" for="Customers-input" style="margin-left: 5px;">Hasta:</label>
                <% Html.Telerik().DatePicker().Name("dtHasta").Render(); %>
                <button class="t-button" name="cmdVer" title="Pulse aquí para ver las estadísticas" onclick="InvocarReporte();">Ver Informe</button>
                </div>
                <iframe id="frameReportes" onload="CerrarWaiting();" src='' width="100%" height="93%" style="margin-top: 5px" frameborder="1">
                    <div>
                        <label>Cargando informe...</label>
                    </div>
                </iframe>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Modal(true)
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(430)
       .Height(280)
       .Render();
%>

<!-- Exportar -->
<% Html.Telerik().Window()
        .Name("wExportar")
        .Title("Exportar...")
        .Visible(false)
        .Content(() =>
        {
            %>
              <div class="BordeRedondeado" style="border-color: #999; padding: 10px;">
                    <table style="border: none;">
                        <tr style="border: none;">
                            <td style="border: none;">
                                <table style="border: none;">
                                    <tr style="border: none;">
                                        <td colspan="4" style="text-align: left; border: none;">
                                            <div>
                                                <input type="radio" name="TipoListado" id="TipoListado" onclick="onSeleccionarTipoListado();" value="F" checked>Facturas
                                                <input type="radio" name="TipoListado" id="TipoListado" onclick="onSeleccionarTipoListado();" style="margin-left: 40px;" value="D">Debitado
                                                <input type="radio" name="TipoListado" id="TipoListado" onclick="onSeleccionarTipoListado();" style="margin-left: 40px;" value="C">Cobrado
                                                <input type="radio" name="TipoListado" id="TipoListado" onclick="onSeleccionarTipoListado();" style="margin-left: 40px;" value="P">Pendiente
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border: none;"><label>Desde:</label></td>
                                        <td style="border: none;">
                                            <%=Html.Telerik().DatePicker()
                                                  .Name("dtFechaDesde")
                                                  .Value(DateTime.Now)
                                                  .ShowButton(true)
                                                  .OpenOnFocus(true)
                                                  .Enable(false)
                                                  .TodayButton()%>
                                        </td>
                                        <td style="border: none;"><label>Hasta:</label></td>
                                        <td style="border: none;">
                                            <%=Html.Telerik().DatePicker()
                                                  .Name("dtFechaHasta")
                                                  .Value(DateTime.Now)
                                                  .ShowButton(true)
                                                  .OpenOnFocus(true)
                                                  .Enable(false)
                                                  .TodayButton()%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </div>
            <div style="text-align: center; margin-top: 20px;">
                <label class="t-button" title="Exportar listado a Excel" onclick="onExportar();">
                    <img src="<%=Url.Content("~/Content/General/Excel2013.png")%>" />
                </label>
            </div>
<!-- Fin de Filtros -->
        <%})
       .Buttons(b => b.Maximize().Close())
       .Modal(true)
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(460)
       .Height(240)
       .Render();
%>

<% Html.RenderPartial("MensajeError"); %>
