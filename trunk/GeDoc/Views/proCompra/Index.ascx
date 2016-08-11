<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>

<% Html.RenderPartial("VistaPasesExpedientes");
   ViewData["MinimaHora"] = "08:00";
   ViewData["MaximaHora"] = "13:00";
   ViewData["IntervaloHora"] = 30;
%>

<script type="text/javascript">
    var _RowIndex_proCompra = -1;
    var _DatosRegistro_proCompra;

    function onVerPDFproCompra(LinkArchivo, Numero) {
        var _Window = $("#wverPDFproCompra").data("tWindow");
        debugger;
        if (LinkArchivo != "") {
            $("#divPDF").css("display", "none");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/Resoluciones/' + LinkArchivo));
            _Window.title("Ver Resolución N° " + Numero + "...").center().open();
        }
        else {
            $("#divPDF").css("display", "block");
            _Window.title("Ver Resolución N° " + Numero + "...").center().open();
        }
    }

    function onVerPases(idExpediente) {
        VerPases(idExpediente);
    }

    function onRowSelected_proCompra(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex_proCompra = e.row.rowIndex;
        _DatosRegistro_proCompra = dataItem;
    }
    function onComplete_proCompra(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
            $("#txtBusquedaBusqueda_proCompra").focus();
        }
    }

    function onDataBinding_proCompra(args) {
        AbrirWaiting();
        var _Anios = $("#cbxAnioCompra").data("tComboBox");
        var grid = $(this).data("tGrid");
        var _anioId = 0;
        if (_Anios != null) {
            _anioId = _Anios.value();
        }

        args.data = $.extend(args.data, { filtro: (grid.filterBy || "~"), Anio: _anioId });
    }

    function onCommandEdit_proCompra(e) {
        $('#comResolucion').mask("99999-9999");
        $('#comComprobante').mask("999-99999-9999");
        $('#comComprobante').blur(function () {
            if ($('#comComprobante').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('expExpediente/getExisteExpediente');
                $.ajax({
                    url: _Post,
                    data: { Comprobante: $('#comComprobante').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#comComprobante').val("");
                            $('#comComprobante').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('El comprobante ingresado no existe.', '¡Atención!', function () {
                                $('#comComprobante').val("");
                                $('#comComprobante').focus();
                            });
                            $('#popup_container').focus();
                            $('#popup_ok').focus();
                        }
                    }
                });
            }
        });
        debugger;
        switch (e.mode) {
            case "insert":
                $('#tipoIdEncuadreLegal').data("tComboBox").disable();
            case "edit":
                if (_DatosRegistro_proCompra != null) {
                    if (_DatosRegistro_proCompra.CompraAnulada) {
                        $('#GridPopUp').find('input').attr('disabled', 'disabled');
                        $('#GridPopUp').find('.t-window-content.t-content').find('span').css('display', 'none');
                        $('#GridPopUp').find('.t-button.t-grid-update').css('display', 'none');
                    } else if (!_DatosRegistro_proCompra.AutorizadoEncuadreLegal) {
                        $('#tipoIdEncuadreLegal').data("tComboBox").disable();
                    }
                }
        default:
        }
        
        onCommandEdit(e);
    }

    function onCommand_proCompra(e) {
        if (("cmdEditar_proCompra, cmdEliminar_proCompra, cmdDetalle_proCompra, cmdCambioEstado_proCompra, cmdImprimirAnexo_proCompra, cmdImprimirPP_proCompra, cmdOfertas_proCompra, cmdSeguimiento_proCompra, cmdImprimirMatriz_proCompra, cmdAdjunto_proCompra").indexOf(e.name) >= 0) {
            if (_RowIndex_proCompra < 0) {
                jAlert('Debe seleccionar un registro de compra.', 'Error...');
                return;
            }
        }
        var grid;
        var tr;
        switch (e.name) {
        case "cmdAgregar_proCompra":
            grid = $(this).data("tGrid");
            grid.addRow();
            break;
        case "cmdEditar_proCompra":
            //if (_DatosRegistro_proCompra.TieneDetalle) {
            //    jAlert('No puede modificar esta compra por que tiene bienes asociados.', 'Error...');
            //    return;
            //}
            grid = $(this).data("tGrid");
            tr = $("#Grid tbody tr:eq(" + (_RowIndex_proCompra + 1).toString() + ")");
            grid.editRow(tr);
            break;
        case "cmdCambioEstado_proCompra":
            if (_DatosRegistro_proCompra.CompraAnulada) {
                jAlert('Esta compra está Anulada, no puede cambiar el estado.', 'Error...');
                return;
            }

            var _Window = $('#wCambioDeEstado_proCompra').data("tWindow");
            _Window.center().open();
            break;
        case "cmdImprimirAnexo_proCompra":
            jConfirm('¿Imprime Anexo con Valores?', "Imprimir...", function(r) {
                if (r) {
                    var _Parametros = new Array(new Array(parseInt(_DatosRegistro_proCompra.comId), 'comId'), new Array(true, 'ConValores'));
                    InvocarReporte('rptComprasAnexo', _Parametros);
                } else {
                    var _Parametros = new Array(new Array(parseInt(_DatosRegistro_proCompra.comId), 'comId'), new Array(false, 'ConValores'));
                    InvocarReporte('rptComprasAnexo', _Parametros);
                }
            });
            break;
        case "cmdAdjunto_proCompra":
            //_DatosRegistro_catPersona = e.dataItem;
            $("#gridproCompraDocumentacion").data("tGrid").rebind();

            var windowElement = $("#wproCompraDocumentacion").data("tWindow");
            windowElement.title("Documentación adjunta de Compra (Trámite N°: " + _DatosRegistro_proCompra.comComprobante + ")");
            windowElement.center();
            windowElement.open();

            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdImprimirMatriz_proCompra":
            jConfirm('¿Imprime Matriz Comparativa?', "Matriz Comparativa...", function(r) {
                if (r) {
                    var _Parametros = new Array(new Array(parseInt(_DatosRegistro_proCompra.comId), 'comId'));
                    InvocarReporte('rptComprasCuadroComparativo', _Parametros);
                }
            });

            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdImprimirPP_proCompra":
            jConfirm('¿Confirma Imprimir Pedido de Presupuesto?', "Imprimir...", function(r) {
                if (r) {
                    var _Parametros = new Array(new Array(parseInt(_DatosRegistro_proCompra.comId), 'comId'));
                    InvocarReporte('rptComprasPedidoPresupuesto', _Parametros);

                    var _NuevoEstado = "142";
                    var _Post = GetPathApp('proCompra/CambiarEstado');
                    $.ajax({
                        url: _Post,
                        data: { pIdCompra: _DatosRegistro_proCompra.comId, pEstadoNuevo: _NuevoEstado, pObservaciones: "Se imprime el Pedido de Presupuesto" },
                        type: 'POST',
                        error: function(xhr, ajaxOptions, thrownError) {
                            jAlert('No se pudieron cambiar los estados seleccionados. Vuelva a intentarlo por favor.', '¡Atención!');
                        },
                        success: function(data) {
                            if (data) {
                                var grid = $('#Grid').data("tGrid");
                                grid.ajaxRequest();
                            } else {
                                jAlert('Falló la asignación de estados.', '¡Atención!');
                            }
                        }
                    });
                }
            });

            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdEliminar_proCompra":
            //if (_DatosRegistro_proCompra.CompraAnulada) {
            //    jAlert('Esta compra ya se encuentra Anulada.', 'Error...');
            //    return;
            //}
            grid = $(this).data("tGrid");
            tr = $("#Grid tbody tr:eq(" + (_RowIndex_proCompra + 1).toString() + ")");
            jConfirm('¿Confirma ' + (_DatosRegistro_proCompra.CompraAnulada ? "Eliminar" : "Anular") + ' esta Compra?', (_DatosRegistro_proCompra.CompraAnulada ? "Eliminar" : "Anular") + "...", function (r) {
                if (r) {
                    grid.deleteRow(tr);
                }
            });
            break;
        case "cmdDetalle_proCompra":
            grid = $('#gridComprasDetalle').data("tGrid");
            grid.ajaxRequest();

            if (_DatosRegistro_proCompra.CompraAnulada) {
                $("#gridComprasDetalle").find(".t-button.t-button-icontext.t-ajax").css("visibility", "hidden");
            } else {
                $("#gridComprasDetalle").find(".t-button.t-button-icontext.t-ajax").css("visibility", "visible");
            }

            ConstruirEventoImportacion();
            var wProductos = $('#wComprasDetalle').data("tWindow");
            wProductos.center().open();

            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdSeguimiento_proCompra":
            debugger;
            var _WindowSeguimiento = $("#wSeguimientoCompras").data("tWindow");
            $('#wSeguimientoCompras').css({ 'height': 450, 'width': 1012 });
            $('#wSeguimientoCompras').find('div.t-window-content').css({ 'height': 420, 'width': 1000 });
            _WindowSeguimiento.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
            ////////////////////////////////////////
            var _Post = GetPathApp("proCompra/Seguimiento");
            $.ajax({
                url: _Post,
                type: 'POST',
                error: function(xhr, ajaxOptions, thrownError) {
                    CerrarWaiting();
                    jAlert('Falló el acceso al servidor', '¡Atención!', function() {
                    });
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                },
                success: function(data) {
                    CerrarWaiting();
                    var _WindowSeg = $("#wSeguimientoCompras").data("tWindow");
                    _WindowSeg.content(data);
                }
            });
            ////////////////////////////////////////
                //_WindowSeguimiento.ajaxRequest(GetPathApp("proCompra/Seguimiento"));
            _WindowSeguimiento.center().open();

            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdOfertas_proCompra":
            debugger;
            var _WindowOfertas = $("#wOfertasCompras").data("tWindow");
            _WindowOfertas.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
            _WindowOfertas.ajaxRequest(GetPathApp("proCompra/Ofertas"));
            _WindowOfertas.center().open();

            e.preventDefault();
            e.stopPropagation();
            break;
        }
    }

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;

    function onDataBound_proCompra() {
        debugger;
        var grid = $(this).data('tGrid');
        _CurrentPage = grid.currentPage;
        _OrderBy = (grid.orderBy || '~');
        _FilterBy = (grid.filterBy || '~');

        var _Boton = $('a.t-button.t-grid-cmdExportar');
        var href = _Boton.attr('href');

        var _Anios = $("#cbxAnioCompra").data("tComboBox");
        var grid = $(this).data("tGrid");
        var _anioId = 0;
        if (_Anios != null) {
            _anioId = _Anios.value();
        }

        href = href.replace(/page=([^&]*)/, 'page=' + _CurrentPage);
        href = href.replace(/orderBy=([^&]*)/, 'orderBy=' + (_OrderBy || '~'));
        href = href.replace(/filter=([^&]*)/, 'filter=' + (_FilterBy || '~'));
        href = href.replace(/anio=(.*)/, 'anio=' + (_anioId || '~'));
        _Boton.attr('href', href);
    }

    $("#txtBusquedaBusqueda_proCompra").keydown(function (e) {
        if (e.which === 13) {
            var textoBuscado = $("#txtBusquedaBusqueda_proCompra").val() == null ? "" : $("#txtBusquedaBusqueda_proCompra").val();
            grid = $('#Grid').data("tGrid");
            grid.filter("substringof(comComprobante,'" + textoBuscado + "')");

            e.preventDefault();
            e.stopPropagation();
        }
    });

    $("#txtBusquedaBusqueda_proCompra").focus(function (e) {
        $(this).select();
    });

    function onComboBoxLoadCompra() {
        $(this).data("tComboBox").fill();
    }

    function onComboBoxChangeCompra() {
        $("#Grid").data("tGrid").ajaxRequest();
    }
</script>
<% ViewBag.OpenOnFocus = false; %>
<% Html.RenderPartial("proCompraDocumentacion"); %>
<% ViewData["FiltroContains"] = true; %>
<% Html.RenderPartial("Productos"); %>
<% Html.RenderPartial("CambiaEstado"); %>
<div style="overflow: hidden; height: 510px;" >
<div style="margin-top: 5px; margin-left: 10px; margin-bottom: 5px;">
    <label style="">Búsqueda por N° de Trámite:</label>
    <input type="text" id="txtBusquedaBusqueda_proCompra" style="width: 500px;"/>
    <label style="margin-left: 5px;">Año:</label>
    <%= Html.Telerik().ComboBox()
        .Name("cbxAnioCompra")
        .DropDownHtmlAttributes(new { style = "width:100px;" })
        .OpenOnFocus(true)
        .AutoFill(true)
        .Filterable(filtering =>
        {
            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
        })
        .HighlightFirstMatch(true)
        .ClientEvents(events => events.OnChange("onComboBoxChangeCompra").OnLoad("onComboBoxLoadCompra"))
        .SelectedIndex(DateTime.Now.Year - 2000)
        .HtmlAttributes(new { style = "width: 60px; vertical-align: middle;" })
        .BindTo((SelectList)ViewData["Anio_Data"])%>
</div>
<%= Html.Telerik().Grid<GeDoc.proCompraWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.comId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar_proCompra").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdEliminar_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Eliminar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Eliminar") });
            commands.Custom().Ajax(true).Name("cmdDetalle_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Productos")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -239px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Eliminar") });
            commands.Custom().Ajax(true).Name("cmdCambioEstado_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Estado")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -208px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Cambiar Estado") });
            commands.Custom().Ajax(true).Name("cmdImprimirAnexo_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Anexo")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -239px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Cambiar Estado") });
            commands.Custom().Ajax(true).Name("cmdImprimirPP_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Pedido Pres.")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -239px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Cambiar Estado") });
            commands.Custom().Ajax(true).Name("cmdSeguimiento_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Pedidos")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/Estados/Camion.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Ofertas") });
            commands.Custom().Ajax(true).Name("cmdOfertas_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Ofertas")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/Estados/Oferta.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Ofertas") });
            commands.Custom().Ajax(true).Name("cmdImprimirMatriz_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Matriz Comparativa")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -239px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Cambiar Estado") });
            commands.Custom().Ajax(true).Name("cmdAdjunto_proCompra").ButtonType(GridButtonType.ImageAndText).Text("Documentación")
                .ImageHtmlAttributes(new {style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -239px;"});
                //.HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Cambiar Estado") });
            commands.Custom().Action("Exportar", "proCompra", new { page = 1, orderBy = "~", filter = "~", anio = DateTime.Now.Year }).Name("cmdExportar").ButtonType(GridButtonType.ImageAndText).Text("Exportar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -240px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Exportar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "proCompra")
                .Insert("_InsertEditing", "proCompra")
                .Update("_SaveEditing", "proCompra")
                .Delete("_DeleteEditing", "proCompra");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.Estado.tipoDescripcion).Width("160px").Title("Estado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<div title='<#= Estado.tipoDescripcion #>' style='width: 100%; text-align: left;'><img src='" + Url.Content("~/Content") + "/Estados/<#= EstadoImagen #>' height='10px' width='10px' style='margin-right: 5px;' /><#= Estado.tipoDescripcion #></div>");
            columns.Bound(c => c.Pagado).Width("40px").Title("").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" }).Filterable(true)
            .ClientTemplate("<div <#= !Pagado ? \"\" : \"title='Pagado'\" #> style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/<#= !Pagado ? \"Vacio-Transparente.png\" : \"SignoPesos.png\" #>' height='18px' width='18px' style='margin-right: 0px;' /></div>");
            columns.Bound(c => c.pagFecha).Width("120px").Title("Fecha de Pago").Visible(true);
            columns.Bound(c => c.TipoComprobante.tipoDescripcion).Width("120px").Title("Tipo de Trámite").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= TipoComprobante.tipoDescripcion #>' style='cursor: pointer;' ><#= TipoComprobante.tipoDescripcion #></label>");
            columns.Bound(c => c.comComprobante).Width("140px").Title("N° Trámite").Visible(true)
            .ClientTemplate("<label title='Haga click aquí para ver los pases' onclick=\"onVerPases('<#= IdExpediente #>', <#= comComprobante #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= comComprobante #> </label>");
            columns.Bound(c => c.expUbicacion).Width("140px").Title("Ubicación actual").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= expUbicacion #>' style='cursor: pointer;' ><#= expUbicacion #></label>");
            columns.Bound(c => c.expDiasCorridos).Width("110px").Title("Días corridos").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.expDiasHabiles).Width("100px").Title("Días hábiles").Visible(true).HtmlAttributes(new { style = "text-align: center;" })
            .ClientTemplate("<div class='BordeRedondeado' style='cursor: pointer; text-align: center; <#= expDiasHabiles >= 4 && expDiasHabiles <= 6 ? \"background-color: rgb(248, 224, 49);\" : (expDiasHabiles >= 7 ? \"background-color: rgb(243, 57, 57);\" : \"\") #>  <#= expDiasHabiles >= 4 && expDiasHabiles <= 6 ? \"border-color: rgb(184, 177, 8);\" : (expDiasHabiles >= 7 ? \"border-color: rgb(187, 58, 58);\" : \"border: none;\") #> <#= expDiasHabiles == null ? \"display: none;\" : \"\" #>' ><#= expDiasHabiles #></div>");
            columns.Bound(c => c.EncuadreLegal.tipoDescripcion).Width("150px").Title("Encuadre Legal").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= EncuadreLegal.tipoDescripcion #>' style='cursor: pointer;' ><#= EncuadreLegal.tipoDescripcion #></label>");
            columns.Bound(c => c.comFechaApertura).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha Apertura").Visible(true);
            columns.Bound(c => c.comHoraApertura).Width("70px").Title("Hora Ap.").Visible(true);
            columns.Bound(c => c.comResolucion).Width("120px").Title("Resolución").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='Haga click aquí para ver la resolución' onclick=\"onVerPDFproCompra('<#= InfoResolucion.resLinkArchivo #>', '<#= comResolucion #>')\" style='cursor: pointer;' style='white-space: nowrap;'><#= comResolucion #> </label>");
            columns.Bound(c => c.comDescripcion).Width("120px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= comDescripcion #>' style='cursor: pointer;' ><#= comDescripcion #></label>");
            columns.Bound(c => c.Solicitante.perApellidoyNombre).Width("120px").Title("Solicitante").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Solicitante.perApellidoyNombre #>' style='cursor: pointer; white-space: nowrap;'><#= Solicitante.perApellidoyNombre #></label>");
            columns.Bound(c => c.comFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.ImporteEstimado).Format("{0:0.00}").Width("120px").Title("Importe").Visible(true).HtmlAttributes(new {style = "text-align: right;"});
            columns.Bound(c => c.comDestino).Width("150px").Title("Destino").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= comDestino #>' style='cursor: pointer;' style='white-space: nowrap;'><#= comDestino #> </label>");
            columns.Bound(c => c.comLugarDeEntrega).Width("150px").Title("Lugar de Entrega").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= comLugarDeEntrega #>' style='cursor: pointer;' style='white-space: nowrap;'><#= comLugarDeEntrega #> </label>");
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit_proCompra").OnRowSelected("onRowSelected_proCompra").OnComplete("onComplete_proCompra").OnCommand("onCommand_proCompra").OnDataBound("onDataBound_proCompra").OnDataBinding("onDataBinding_proCompra"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])-35))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 100%;" })

%>
</div>

<% Html.Telerik().Window()
        .Name("wOfertasCompras")
        .Title("Administración de Ofertas")
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
        .Name("wCRUDOfertasCompras")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(650)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wSeguimientoCompras")
        .Title("Seguimiento de Pedidos de Presupuesto")
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
        .Name("wCRUDSeguimientoCompras")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(600)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wCRUDDocumentacionCompras")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       //.Width(1000)
       //.Height(600)
       .Render();
%>

<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("wverPDFproCompra")
        .Title("Resoluciones")
        .Visible(false)
        .Content(() => 
        {%> 
            <div id="divPDF" style="width: 100%; height: 100%; display: none;">
                <table style="border: none; margin:10px;">
                    <tr style="border: none; margin:0px;">
                        <td style="border: none; margin:0px;">
                            <img src="<%= Url.Content("~/Content") %>/General/Notificacion/Vista Internet Security OFF.png" width="40px" style="margin-top:-5px" />
                        </td>
                        <td style="border: none; margin:0px;">
                            <asp:Label ID="Label1" runat="server" style="margin-left:-5px" ForeColor="Red">Archivo no encontrado...</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <iframe id="framePDF" src='' width="100%" height="100%" >
            </iframe>
        <%})
        .Buttons(b => b.Maximize().Close())
        .Draggable(true)
        .Modal(true)
        .Scrollable(false)
        .Resizable()
        .Width(870)
        .Height(460)
        .Render();
%>
