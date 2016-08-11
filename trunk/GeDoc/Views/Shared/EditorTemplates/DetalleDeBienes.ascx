<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    var _RowIndexProCompraDetalle = -1;
    var _DatosRegistroProCompraDetalle;
    var _Index = -1;
    var _Timer;
    var _CProCompraDetalle;
    var _FilasVisiblesProCompraDetalle = 10;
    var _ColumnasVisiblesProCompraDetalle = 3;

    $("#divGridPrincipalProCompraDetalle").keydown(function (e) {
        var _FilaAnterior = _RowIndexProCompraDetalle;
        switch (e.which) {
            case 13: // Enter
                var grid = $("#gridProCompraDetalle").data("tGrid");
                var tr = $("#gridProCompraDetalle tbody tr:eq(" + (_RowIndexProCompraDetalle + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case 40: // Flecha Abajo
                _RowIndexProCompraDetalle++;
                e.preventDefault();
                e.stopPropagation();
                break;
            case 38: // Flecha Arriba
                _RowIndexProCompraDetalle--;
                e.preventDefault();
                e.stopPropagation();
                break;
            case 39: // Flecha Derecha
                _CProCompraDetalle++;
                e.preventDefault();
                e.stopPropagation();
                break;
            case 37: // Flecha Izquierda
                _CProCompraDetalle--;
                e.preventDefault();
                e.stopPropagation();
                break;
        }
        SeleccionaFila(_FilaAnterior);
    });

    function SeleccionaFila(FilaAnterior) {
        if (_RowIndexProCompraDetalle >= $("#gridProCompraDetalle").data('tGrid').pageSize) {
            _RowIndexProCompraDetalle = 0;
        }
        if (_RowIndexProCompraDetalle < 0) {
            _RowIndexProCompraDetalle = $("#gridProCompraDetalle").data('tGrid').pageSize - 1;
        }
        if (_FilaAnterior != _FPlanG) {
            onRowSelectRecordKeyUp(_FilaAnterior, false, "gridProCompraDetalle", _DatosRegistro_PlanG);
            _DatosRegistro_PlanG = onRowSelectRecordKeyUp(_RowIndexProCompraDetalle, true, "gridProCompraDetalle", _DatosRegistroProCompraDetalle);
        }
        if (_CProCompraDetalle > $("#gridProCompraDetalle").data('tGrid').data.length) {
            _CProCompraDetalle = 0;
        }
        if (_CProCompraDetalle < 0) {
            _CProCompraDetalle = $("#gridProCompraDetalle").data('tGrid').data.length;
        }

        onCellSelectKeyUp(_RowIndexProCompraDetalle, _CProCompraDetalle, 'gridProCompraDetalle', _FilasVisiblesProCompraDetalle, _ColumnasVisiblesProCompraDetalle, true);
    }

    function onRowSelectedProCompraDetalle(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndexProCompraDetalle = e.row.rowIndex;
        _DatosRegistroProCompraDetalle = dataItem;
    }

//    function onCommandProCompraDetalle(e) {
//        debugger;
//        if (("cmdEditarProCompraDetalle, cmdEliminarProCompraDetalle").indexOf(e.name) >= 0) {
//            if (_RowIndexProCompraDetalle < 0) {
//                jAlert('Debe seleccionar un Registro.', 'Error...');
//                return;
//            }
//        }
//        switch (e.name) {
//            case "cmdActualizar":
//                var grid = $("#gridProCompraDetalle").data("tGrid");
//                var _Columnas = $("#cbColumnas").data("tDropDownList");
//                debugger;
//                if (_Columnas.value() == "") {
//                    jAlert('Debe seleccionar la columna que quiere Actualizar.', 'Error...');
//                    return;
//                }
//                if ($('#txtImporte').data("tTextBox").value() < 0) {
//                    jAlert('El importe no puede ser negativo.', 'Error...');
//                    return;
//                }

//                AbrirWaiting();
//                _Index = 1;
//                _Timer = setInterval(function () {
//                    var tr = $("#gridProCompraDetalle tbody tr:eq(" + (_Index).toString() + ")");
//                    if (tr == null || $("#gridProCompraDetalle").data("tGrid").total < _Index) {
//                        CerrarWaiting();
//                        //jAlert('Se cambiaron los ProCompraDetalle en forma correcta.', 'Error...');
//                        clearInterval(_Timer);
//                        //                      grid.submitChanges();
//                        if (!_ACambiadoElPrecioDeCosto) {
//                            _ACambiadoElPrecioDeCosto = (_Columnas.value() == 3);
//                        }
//                        return;
//                    }
//                    var Columna = $(tr).find("td:eq(" + _Columnas.value().toString() + ")");
//                    //Calculamos el nuevo valor del precio
//                    grid.editCell(Columna);
//                    debugger;
//                    var dataItem = grid.dataItem(tr);
//                    var _ImporteNuevo = 0.00;
//                    var _PrecioDeCosto = 0.00;
//                    if (_ACambiadoElPrecioDeCosto) {
//                        _PrecioDeCosto = grid.updatedDataItems()[_Index - 1].prodPrecioCosto;
//                    }
//                    else {
//                        _PrecioDeCosto = dataItem.prodPrecioCosto;
//                    }
//                    _ImporteNuevo = Math.ceil(_PrecioDeCosto * (1 + ($('#txtImporte').val() / 100)));
//                    $(".t-grid-edit-cell").find(':input').val(_ImporteNuevo);
//                    grid.saveCell(Columna);
//                    //grid.updateRow(tr);
//                    _Index++;
//                }, 1);
//                break;
//        }
//    }

    function onEditProCompraDetalle(e) {
        if (e.mode == 'edit') {
//            $("#comDetalle").keydown(function (e) {
//                if(e.which == 13){ // Enter
//                    var grid = $("#gridProCompraDetalle").data("tGrid");
//                    $(grid.element).find(".t-grid-edit-cell").each(function () {
//                        grid.saveCell(this);
//                    });

//                    var tr = $("#gridProCompraDetalle tbody tr:eq(" + (_RowIndexProCompraDetalle + 1).toString() + ")");
//                    var Columna = $(tr).find("td:eq(1)");
//                    grid.editCell(Columna);
//                }
//            });
//            $("#comDetalle").focusout(function (e) {
//                var grid = $("#gridProCompraDetalle").data("tGrid");
//                $(grid.element).find(".t-grid-edit-cell").each(function () {
//                    grid.saveCell(this);
//                });
//            });
//            $("#comCantidad").keydown(function (e) {
//                if (e.which == 13) { // Enter
//                    var grid = $("#gridProCompraDetalle").data("tGrid");
//                    $(grid.element).find(".t-grid-edit-cell").each(function () {
//                        grid.saveCell(this);
//                    });

//                    var tr = $("#gridProCompraDetalle tbody tr:eq(" + (_RowIndexProCompraDetalle + 1).toString() + ")");
//                    var Columna = $(tr).find("td:eq(2)");
//                    grid.editCell(Columna);
//                }
//            });
//            $("#comPrecioEstimado").keydown(function (e) {
//                if (e.which == 13) { // Enter
//                    var grid = $("#gridProCompraDetalle").data("tGrid");
//                    $(grid.element).find(".t-grid-edit-cell").each(function () {
//                        grid.saveCell(this);
//                    });

//                    var _FilaAnterior = _RowIndexProCompraDetalle;
//                    _RowIndexProCompraDetalle++;
//                    SeleccionaFila(_FilaAnterior);

//                    var tr = $("#gridProCompraDetalle tbody tr:eq(" + (_RowIndexProCompraDetalle + 1).toString() + ")");
//                    var Columna = $(tr).find("td:eq(3)");
//                    grid.editCell(Columna);
//                }
//            });
        }
//        onCommandEdit(e);
    }
    function onRowDataBoundProCompraDetalle(e) {
        if (_RowIndexProCompraDetalle > -1) {
            var grid = $("#gridProCompraDetalle").data("tGrid");
            var tr = $("#gridProCompraDetalle tbody tr:eq(" + (_RowIndexProCompraDetalle + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistroProCompraDetalle = grid.dataItem(tr);
        }

        //Asignamos Eventos
        $(this).find(".t-grid-content").find('tbody').find('td').click(function (e) {
            _CPlanG = e.currentTarget.cellIndex;
            _FPlanG = e.currentTarget.parentElement.rowIndex;
            $('#gridProCompraDetalle').find('.t-state-selected').removeClass("t-state-selected");

            _DatosRegistroProCompraDetalle = onRowSelectRecordKeyUp(_RowIndexProCompraDetalle, true, "gridProCompraDetalle", _DatosRegistroProCompraDetalle);

            onCellSelectKeyUp(_RowIndexProCompraDetalle, _CProCompraDetalle, 'gridProCompraDetalle', _FilasVisiblesProCompraDetalle, _ColumnasVisiblesProCompraDetalle, true);
        });
    }

</script>

<!-- ProCompraDetalle -->
<div id="divGridPrincipalProCompraDetalle" tabindex="2" style="overflow: hidden; outline:none; width: 520px;" >
<% Html.Telerik().Grid((IEnumerable<GeDoc.proCompraDetalleWS>)Session["DetalleCompra"])
    .Name("gridProCompraDetalle")
    .Localizable("es-AR")
    .DataKeys(keys =>
    {
        keys.Add(p => p.comdId);
    })
    .ToolBar(commands =>
    {
        commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
    })
    .DataBinding(dataBinding =>
    {
        dataBinding.Ajax()
            .Select("_SelectEditingDetalle", "proCompra");
    })
    .Columns(columns =>
    {
        columns.Bound(c => c.comDetalle).Width("180px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
        //.ClientTemplate("<label title='<#= comDetalle #>' style='cursor: pointer;' style='white-space: nowrap;'><#= comDetalle #> </label>");
        columns.Bound(c => c.comCantidad).Width("70px").Title("Cantidad").Visible(true);
        columns.Bound(c => c.comPrecioEstimado).Format("{0:0.00}").Width("100px").Title("Precio Estimado").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
    })
    .Editable(editing => editing.Mode(GridEditMode.InCell))
    .Pageable((paging) =>
                        paging.Enabled(true)
                        .PageSize(20))
    .Footer(true)
    .ClientEvents(clientEvents => clientEvents.OnEdit("onEditProCompraDetalle").OnRowDataBound("onRowDataBoundProCompraDetalle").OnRowSelected("onRowSelectedProCompraDetalle").OnError("onErrorCRUD"))
    .Selectable()
    .Scrollable(scroll => scroll.Enabled(true).Height(280))
    .Sortable()
    .Render();
    %>
</div>
