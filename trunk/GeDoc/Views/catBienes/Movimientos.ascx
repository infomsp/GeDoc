<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    var _RowIndexMovimiento = -1;
    var _DatosRegistroMovimiento;
    var _EsModificar = false;
    var _WindowCRUD;
    function onRowSelectedMovimiento(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndexMovimiento = e.row.rowIndex;
        _DatosRegistroMovimiento = dataItem;
    }
    function onCommandMovimiento(e) {
        _EsModificar = false;
        if (("cmdEditarMovimiento, cmdEliminarMovimiento, cmdActivarMovimiento").indexOf(e.name) >= 0) {
            if (_RowIndexMovimiento < 0) {
                jAlert('Debe seleccionar un Movimiento.', 'Error...');
                return;
            }
        }
        switch (e.name) {
            case "cmdAgregarMovimiento":
                var grid = $("#gridBienMovimientos").data("tGrid");
                grid.addRow();
                break;
            case "cmdEditarMovimiento":
                var grid = $("#gridBienMovimientos").data("tGrid");
                var tr = $("#gridBienMovimientos tbody tr:eq(" + (_RowIndexMovimiento + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminarMovimiento":
                debugger;
                if (_DatosRegistroMovimiento.Tipo.tipoCodigo == "AL") {
                    jAlert('Este Movimiento no se puede Eliminar.', 'Error...');
                    return;
                }
                var grid = $("#gridBienMovimientos").data("tGrid");
                var tr = $("#gridBienMovimientos tbody tr:eq(" + (_RowIndexMovimiento + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar este Movimiento?', "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "edit":
                _EsModificar = true;
                break;
        }
    }
    function onEditMovimiento(e) {
        debugger;
        _WindowCRUD = $('#gridBienMovimientosPopUp').data('tWindow');

        var _bimCodigo = $("#bimCodigo")[0];
        var _cscIdDestino = $("#cscIdDestino").data("tComboBox");
        var _tipoId = $("#tipoId").data("tComboBox");

        if (_tipoId.text() != "Transferencia") {
            _cscIdDestino.disable();
            _bimCodigo.disabled = true;
        }
        else {
            _cscIdDestino.enable();
            _bimCodigo.disabled = false;
        }

        onCommandEdit(e);
    }
    function onRowDataBoundMovimiento(e) {
        if (_RowIndexMovimiento > -1) {
            var grid = $("#gridBienMovimientos").data("tGrid");
            var tr = $("#gridBienMovimientos tbody tr:eq(" + (_RowIndexMovimiento + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistroMovimiento = grid.dataItem(tr);
        }
    }
    function onDataBindingMovimiento(e) {
        if (_DatosRegistro != null) {
            e.data = $.extend(e.data, { idBien: _DatosRegistro.biId });
        }
    }
    function onSaveMovimiento(e) {
        var values = e.values;

        values.biId = _DatosRegistro.biId;
    }
    function onCompleteMovimiento(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }

</script>

<% ViewData["FiltroContains"] = true; %>

<!-- Movimientos -->
<% Html.Telerik().Window()
        .Name("CRUDBienesMovimientos")
        .Title("Movimientos")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catBienMovimientos>)ViewData["BienesMovimientos"])
            .Name("gridBienMovimientos")
            .DataKeys(keys =>
            {
                keys.Add(p => p.bimId);
            })
            .Localizable("es-AR")
            .ToolBar(commands =>
            {
                commands.Custom().Ajax(true).Name("cmdAgregarMovimiento").ButtonType(GridButtonType.ImageAndText)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                    .Text("Agregar")
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catBienes", "Agregar") });
                commands.Custom().Ajax(true).Name("cmdEliminarMovimiento").ButtonType(GridButtonType.ImageAndText)
                    .Text("Eliminar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catBienes", "Eliminar") });
                %>
                <label id="lblBien" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                <%
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingMovimiento", "catBienes")
                    .Update("_SaveEditingBienesMovimiento", "catBienes")
                    .Insert("_InsertEditingBienesMovimiento", "catBienes")
                    .Delete("_DeleteEditingMovimiento", "catBienes");
            })
            .Columns(columns =>
            {
                columns.Bound(c => c.Tipo.tipoDescripcion).Width("80px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.bimfecha).Width("90px").Format("{0:dd/MM/yyyy}").Title("Fecha").Visible(true);
                columns.Bound(c => c.bimCodigo).Width("120px").Title("Código").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= bimCodigo #>' style='cursor: pointer; white-space: nowrap;' ><#= bimCodigo #></label>");
                columns.Bound(c => c.bimCodigoAnterior).Width("120px").Title("Código Anterior").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= bimCodigoAnterior #>' style='cursor: pointer; white-space: nowrap;' ><#= bimCodigoAnterior #></label>");
                columns.Bound(c => c.CSOrigen.CentroDeSalud.sucNombre).Width("250px").Title("Centro de Salud Origen").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= CSOrigen.CentroDeSalud.sucNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= CSOrigen.CentroDeSalud.sucNombre #></label>");
                columns.Bound(c => c.CSOrigen.cscNombre).Width("120px").Title("Sector Origen").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= CSOrigen.cscNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= CSOrigen.cscNombre #></label>");
                columns.Bound(c => c.CSDestino.CentroDeSalud.sucNombre).Width("250px").Title("Centro de Salud Destino").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= CSDestino.CentroDeSalud.sucNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= CSDestino.CentroDeSalud.sucNombre #></label>");
                columns.Bound(c => c.CSDestino.cscNombre).Width("120px").Title("Sector Destino").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= CSDestino.cscNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= CSDestino.cscNombre #></label>");
                columns.Bound(c => c.bimObservaciones).Width("150px").Title("Observaciones").Visible(true)
                .ClientTemplate("<label title='<#= bimObservaciones #>' style='cursor: pointer; white-space: nowrap;' ><#= bimObservaciones #></label>");
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingMovimiento", "catBienes", new { idBien = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingMovimiento").OnEdit("onEditMovimiento").OnSave("onSaveMovimiento").OnComplete("onCompleteMovimiento").OnCommand("onCommandMovimiento").OnRowDataBound("onRowDataBoundMovimiento").OnRowSelected("onRowSelectedMovimiento"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1024)
       .Height(420)
       .Render();
%>

