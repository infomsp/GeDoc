<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.RenderPartial("FiltroDesdeArchivo"); %>
<script type="text/javascript">
    var _RowIndexProductos = -1;
    var _DatosRegistroProductos;
    var _FilasVisiblesProductosPedido = 18;
    var _ColumnasVisiblesProductosPedido = 3;
    var _CProductos;
    var _FilaAnteriorProductosPedido;
    var _WindowCRUD;
    
    function TeclaPulsada(e) {
        _FilaAnteriorProductosPedido = _RowIndexProductos;
        switch (e.which) {
            case 13: // Enter
                var grid = $("#gridComprasDetalle").data("tGrid");
                var tr = $("#gridComprasDetalle tbody tr:eq(" + (_RowIndexProductos + 1).toString() + ")");
                grid.editRow(tr);
                
                e.preventDefault();
                e.stopPropagation();
                break;
            case 40: // Flecha Abajo
                _RowIndexProductos++;
                e.preventDefault();
                e.stopPropagation();
                break;
            case 45: // Insert
                  var grid = $("#gridComprasDetalle").data("tGrid");
                  grid.addRow();
                  break;
            case 46: // Delete
                EliminarRegistroProducto();
                break;
            case 38: // Flecha Arriba
                _RowIndexProductos--;
                e.preventDefault();
                e.stopPropagation();
                break;
            case 39: // Flecha Derecha
                _CProductos++;
                e.preventDefault();
                e.stopPropagation();
                break;
            case 37: // Flecha Izquierda
                _CProductos--;
                e.preventDefault();
                e.stopPropagation();
                break;
        }

        switch (e.which) {
            case 13: // Enter
            case 37: // Flecha Izquierda
            case 38: // Flecha Arriba
            case 39: // Flecha Derecha
            case 40: // Flecha Abajo
            case 45: // Insert
            case 46: // Delete
                if (_RowIndexProductos >= $("#gridComprasDetalle").data('tGrid').pageSize || _RowIndexProductos >= ($("#gridComprasDetalle").data('tGrid').total)) {
                    _RowIndexProductos = 0;
                }
                if (_RowIndexProductos < 0) {
                    if (($("#gridComprasDetalle").data('tGrid').total - 1) > ($("#gridComprasDetalle").data('tGrid').pageSize - 1)) {
                        _RowIndexProductos = $("#gridComprasDetalle").data('tGrid').pageSize - 1;
                    } else {
                        _RowIndexProductos = $("#gridComprasDetalle").data('tGrid').total - 1;
                    }
                }
                if (_FilaAnteriorProductosPedido != _RowIndexProductos) {
                    onRowSelectRecordKeyUp(_FilaAnteriorProductosPedido, false, "gridComprasDetalle", _DatosRegistroProductos);
                    _DatosRegistroProductos = onRowSelectRecordKeyUp(_RowIndexProductos, true, "gridComprasDetalle", _DatosRegistroProductos);
                }
                if (_CProductos > $("#gridComprasDetalle").data('tGrid').columns.length - 1) {
                    _CProductos = 0;
                }
                if (_CProductos < 0) {
                    _CProductos = $("#gridComprasDetalle").data('tGrid').columns.length - 1;
                }

                onCellSelectKeyUp(_RowIndexProductos, _CProductos, 'gridComprasDetalle', _FilasVisiblesProductosPedido, _ColumnasVisiblesProductosPedido, false);
                break;
        }
    }
    $("#divGridPrincipalProductos").keydown(function (e) {
        TeclaPulsada(e);
    });

    function EliminarRegistroProducto() {
        if (!_DatosRegistroProductos.comdActivo) {
            jAlert('Este producto ya se encuentra Anulado.', 'Error...', function (r) {
                if (r) {
                    SetFocusGrilla();
                }
            });
            return;
        }
        
        var grid = $("#gridComprasDetalle").data("tGrid");
        var tr = $("#gridComprasDetalle tbody tr:eq(" + (_RowIndexProductos + 1).toString() + ")");
        jConfirm('¿Confirma Eliminar el producto "' + _DatosRegistroProductos.comdDetalle + '"?', "Eliminar...", function (r) {
            if (r) {
                AbrirWaiting();
                grid.deleteRow(tr);
            } else {
                SetFocusGrilla();
            }
        });
    }

    function onRowSelectedProductos(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndexProductos = e.row.rowIndex;
        _DatosRegistroProductos = dataItem;
    }

    function SetFocusGrilla() {
        debugger;
        _RowIndexProductos = _RowIndexProductos < 0 || _RowIndexProductos == null ? 0 : _RowIndexProductos;
        _CProductos = _CProductos < 0 || _CProductos == null ? 0 : _CProductos;
        if ($("#gridComprasDetalle").data("tGrid").total > 0) {
            onCellSelectKeyUp(_RowIndexProductos, _CProductos, 'gridComprasDetalle', _FilasVisiblesProductosPedido, _ColumnasVisiblesProductosPedido, false);
        }

        $("#gridComprasDetalle").attr('tabindex', 0).focus();
    }

    function onCommandProductos(e) {
        if (("cmdEditarProductos, cmdEliminarProductos, cmdActivarProductos").indexOf(e.name) >= 0) {
            if (_RowIndexProductos < 0) {
                jAlert('Debe seleccionar un Registro.', 'Error...', function (e) {
                    if (e) {
                        SetFocusGrilla();
                    }
                });
                return;
            }
        }
        var _WindowEst;
        switch (e.name) {
            case "cmdAgregarProductos":
                var grid = $("#gridComprasDetalle").data("tGrid");
                grid.addRow();
                break;
            case "cmdEditarProductos":
                var grid = $("#gridComprasDetalle").data("tGrid");
                var tr = $("#gridComprasDetalle tbody tr:eq(" + (_RowIndexProductos + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminarProductos":
                EliminarRegistroProducto();
                break;
            case "cmdActivarProductos":
                if (_DatosRegistroProductos.comdActivo) {
                    jAlert('Este producto ya se encuentra Activo.', 'Error...', function (e) {
                        if (e) {
                            SetFocusGrilla();
                        }
                    });
                    return;
                }
                jConfirm('¿Confirma Activar el producto "' + _DatosRegistroProductos.comdDetalle + '"?', "Activar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('proCompra/_ActivaRegistroProducto'), { id: _DatosRegistroProductos.comdId }, function (data) {
                            if (data) {
                                $("#gridComprasDetalle").data("tGrid").ajaxRequest();
                            }
                        });
                    } else {
                        SetFocusGrilla();
                    }
                });
                break;
            case "cmdImportar":
                onImportarArchivo();
                break;
            case "cancel":
                SetFocusGrilla();
                break;
        }

        e.preventDefault();
        e.stopPropagation();
    }

    $('#GridImportacion').on('onSuccessDesdeArchivo', function (event, resultado) {
        // Agregamos el evento al div que escuchará
        if (resultado.Tipo === "Remove") {
            $("#imgDesdeArchivo").show();
            $("#imgDesdeArchivoOK").hide();
            $("#btnDesdeArchivo").attr("title", "Importar información desde un archivo");
            $("#lblDesdeArchivo").text("Importar Archivo");
        } else {
            $("#imgDesdeArchivo").hide();
            $("#imgDesdeArchivoOK").show();
            $("#lblDesdeArchivo").text("Ver Archivo Importado");
            $("#btnDesdeArchivo").attr("title", "Ver información resultante de la importación");
        }
    });

    function ConstruirEventoImportacion() {
        $("#GridImportacion").unbind("onAceptarDesdeArchivo");
        $("#GridImportacion").on("onAceptarDesdeArchivo", function (event, resultado) {
            if (ValidaCampos() !== 3) {
                jAlert("El archivo importado no tiene las columnas de Detalle, Precio y Cantidad, revise el Archivo por favor.", "¡Atención!", function (e) {
                    if (e) {
                        SetFocusGrilla();
                    }
                });
                return;
            } else {
                ///////////////
                AbrirWaiting();
                var _Post = GetPathApp("proCompra/_InsertEditingProductosImportados");
                $.ajax({
                    url: _Post,
                    data: { comId: _DatosRegistro_proCompra.comId },
                    type: "POST",
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert("Falló el acceso al servidor", "¡Atención!", function () {
                            SetFocusGrilla();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.IsValid) {
                            jAlert(data.Mensaje, "¡Atención!", function () {
                                SetFocusGrilla();
                            });
                            $("#popup_container").focus();
                            $("#popup_ok").focus();
                        } else {
                            $("#gridComprasDetalle").data("tGrid").ajaxRequest();
                            jAlert("Se procesó el archivo importado en forma correcta", "¡Atención!", function () {
                                SetFocusGrilla();
                            });
                            $("#popup_container").focus();
                            $("#popup_ok").focus();
                        }
                    }
                });
            }
        });
    }

    function ValidaCampos() {
        var Campos = 0;

        Campos += $("#GridImportacion").find("td:[id='Detalle']").length;
        Campos += $("#GridImportacion").find("td:[id='Precio']").length;
        Campos += $("#GridImportacion").find("td:[id='Cantidad']").length;

        return Campos;
    }

    function onEditProductos(e) {
        _WindowCRUD = $("#gridComprasDetallePopUp").data("tWindow");
        onCommandEdit(e);
    }

    function onRowDataBoundProductos(e) {
        if (_RowIndexProductos > -1) {
            var grid = $("#gridComprasDetalle").data("tGrid");
            var tr = $("#gridComprasDetalle tbody tr:eq(" + (_RowIndexProductos + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistroProductos = grid.dataItem(tr);
        }

        //Asignamos Eventos
        $(this).find(".t-grid-content").find('tbody').find('td').click(function (e) {
            _CProductos = e.currentTarget.cellIndex;
            _RowIndexProductos = e.currentTarget.parentElement.rowIndex;
            $('#gridComprasDetalle').find('.t-state-selected').removeClass("t-state-selected");

            _DatosRegistroProductos = onRowSelectRecordKeyUp(_CProductos, true, "gridComprasDetalle", _DatosRegistroProductos);

            onCellSelectKeyUp(_RowIndexProductos, _CProductos, 'gridComprasDetalle', _FilasVisiblesProductosPedido, _ColumnasVisiblesProductosPedido);
        });
    }

    function onDataBindingProductos(e) {
        var grid = $('#Grid').data('tGrid');
        if (grid != null) {
            e.data = $.extend(e.data, { idCompra: _DatosRegistro_proCompra.comId });
        }
    }

    function onSaveProductos(e) {
        var values = e.values;
        values.comId = _DatosRegistro_proCompra.comId;
    }
    function onActivateProductos(e) {
    }
    function onCompleteProductos(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        } else {
            var grid = $("#Grid").data("tGrid");
            grid.ajaxRequest();
        }
    }

    function onDataBound_proCompraDetalle() {
        debugger;
        if (_DatosRegistro_proCompra != null) {
            var _Boton = $('a.t-button.t-grid-cmdExportarPedidoDetalle');
            var href = _Boton.attr('href');

            href = href.replace(/comId=([^&]*)/, 'comId=' + _DatosRegistro_proCompra.comId);
            _Boton.attr('href', href);
        }
        SetFocusGrilla();
    }

</script>

<!-- Productos -->
<% Html.Telerik().Window()
        .Name("wComprasDetalle")
        .Title("Productos")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div id="divGridPrincipalProductos" tabindex="1" style="overflow: hidden; outline:none;" >
            <% Html.Telerik().Grid((IEnumerable<GeDoc.proCompraDetalleWS>)ViewData["DetalleCompra"])
            .Name("gridComprasDetalle")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.comdId);
            })
            .ToolBar(commands =>
            {
                commands.Custom().Ajax(true).Name("cmdAgregarProductos").ButtonType(GridButtonType.ImageAndText)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                    .Text("Agregar");
                commands.Custom().Ajax(true).Name("cmdEditarProductos").ButtonType(GridButtonType.ImageAndText)
                    .Text("Modificar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" });
                commands.Custom().Ajax(true).Name("cmdEliminarProductos").ButtonType(GridButtonType.ImageAndText)
                    .Text("Eliminar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" });
                //commands.Custom().Ajax(true).Name("cmdActivarProductos").ButtonType(GridButtonType.ImageAndText)
                //    .Text("Activar")
                //    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -336px;" });
                commands.Custom().Ajax(true).Name("cmdImportar").ButtonType(GridButtonType.ImageAndText)
                    .Text("Importar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Excel.png') no-repeat 0px 0px;" });
                commands.Custom().Action("ExportarPedidoDetalle", "proCompra", new { comId = 1 }).Name("cmdExportarPedidoDetalle").ButtonType(GridButtonType.ImageAndText).Text("Exportar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -240px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Exportar") });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingProductos", "proCompra", new { idCompra = 0 })
                    .Insert("_InsertEditingProductos", "proCompra")
                    .Update("_SaveEditingProductos", "proCompra")
                    .Delete("_DeleteEditingProductos", "proCompra");
            })
            .Columns(columns =>
                {
                    columns.Bound(c => c.comdActivo).Width("60px").Title("Estado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                    .ClientTemplate("<div style='width: 100%; text-align: center;'><#= comdActivo ? \"\" : \"<img src='" + Url.Content("~/Content") + "/Estados/Rojo(2).png' title='Anulado' height='15px' width='15px' style='vertical-align:middle;' />\" #></div>");
                    columns.Bound(c => c.comdDetalle).Width("350px").Title("Detalle").Visible(true)
                        .FooterHtmlAttributes(new { style = "text-align: right;" })
                        .ClientFooterTemplate("TOTALES: ");
                    columns.Bound(c => c.comdCantidad).Format("{0:0}").Width("100px").Title("Cantidad").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                        .Aggregate(aggreages => aggreages.Sum())
                        .FooterHtmlAttributes(new { style = "text-align: right;" })
                        .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>");
                    columns.Bound(c => c.comdPrecioEstimado).FooterTemplate(f => f.Sum).Format("{0:0.00}").Width("120px").Title("Precio").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                        .Aggregate(aggreages => aggreages.Sum())
                        .FooterHtmlAttributes(new { style = "text-align: right;" })
                        .ClientFooterTemplate("<#= $.telerik.formatString('{0:0.00}', Sum) #>");
                    columns.Bound(c => c.comdSubTotal).FooterTemplate(f => f.Sum).Format("{0:0.00}").Width("120px").Title("Sub Total").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                        .Aggregate(aggreages => aggreages.Sum())
                        .FooterHtmlAttributes(new { style = "text-align: right;" })
                        .ClientFooterTemplate("<#= $.telerik.formatString('{0:0.00}', Sum) #>");
                })
            .Editable(editing => editing.Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                               .PageSize(200))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingProductos").OnEdit("onEditProductos").OnSave("onSaveProductos").OnComplete("onCompleteProductos").OnCommand("onCommandProductos").OnRowDataBound("onRowDataBoundProductos").OnError("onErrorCRUD").OnDataBound("onDataBound_proCompraDetalle"))
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(480))
            .Sortable()
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .ClientEvents(events => events.OnActivate("onActivateProductos"))
       .Draggable(false)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1000)
       .Height(600)
       .Render();
%>
