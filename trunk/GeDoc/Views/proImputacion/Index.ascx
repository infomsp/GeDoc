<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%@ Import Namespace="GeDoc.Models" %>

<%
    var InstitucionContable = "";
    if ((Session["Usuario"] as sisUsuario).icId != null)
    {
        InstitucionContable = " - " + (Session["Usuario"] as sisUsuario).catInstitucionContable.icDescripcion;
%>
<script>
    $("#divTituloCatalogos").text($("#divTituloCatalogos").text() + "<%=InstitucionContable %>")
</script>

<script type="text/javascript" >
    var idImputacion;
    var _Error = false;
    var _PaginaActual = 0;
    var _Orden = "";
    var _Filtro = "";
    var _UltimoEstado;
    var _AnulacionParcial = 0;
    var ExisteSaldoNegativo = false;

    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        idImputacion = dataItem.impId;
    }

    function onComboBoxChange() {
        var _PaginaActual = 0;
        var _Orden = "";
        var _Filtro = "";

        // Habilitamos o no El botón Agregar
        var _Anio = $("#cbxAnio").data("tComboBox");
        var _PermiteImputacionFutura = $("#PermiteImputacionFutura")[0].value;
        var _AnioActual = parseInt($("#AnioActual")[0].value);
        debugger;
        if (_Anio != null) {
            var _anioId = _Anio.value();
            if (_AnioActual < _anioId) {
                if (_PermiteImputacionFutura == "True") {
                    $('a.t-button.t-grid-add').css('display', 'inline-block');
                }
                else {
                    $('a.t-button.t-grid-add').css('display', 'none');
                }
            }
            else {
                $('a.t-button.t-grid-add').css('display', 'inline-block');
            }
        }
        else {
            $('a.t-button.t-grid-add').css('display', 'inline-block');
        }

        $("#Grid").data("tGrid").ajaxRequest();
        //$("#GridPieImputaciones").data("tGrid").ajaxRequest();
    }
    function onDataBindingImputaciones(args) {
        debugger;
        var _Fuente = $("#cbxFuentes").data("tComboBox");
        var _Cuenta = $("#cbxCuenta").data("tComboBox");
        //        var _Partida = $("#cbxPartida").data("tComboBox");
        var _Anio = $("#cbxAnio").data("tComboBox");
        var _fteId = 0;
        var _ccId = 0;
        //        var _partId = 0;
        var _anioId = 0;
        if (_Fuente != null) {
            _fteId = _Fuente.value();
        }
        if (_Cuenta != null) {
            _ccId = _Cuenta.value();
        }
        //        if (_Partida != null) {
        //            _partId = _Partida.value();
        //        }
        if (_Anio != null) {
            _anioId = _Anio.value();
        }
        AbrirWaiting();
        args.data = $.extend(args.data, { fuenteId: _fteId, cuentaId: _ccId/*, partidaId: _partId*/, anioId: _anioId });
    }

    function onDataBindingPieImputaciones(args) {
        var _Fuente = $("#cbxFuentes").data("tComboBox");
        var _Cuenta = $("#cbxCuenta").data("tComboBox");
        var _Anio = $("#cbxAnio").data("tComboBox");
        var _fteId = 0;
        var _ccId = 0;
        var _anioId = 0;
        if (_Fuente != null) {
            _fteId = _Fuente.value();
        }
        if (_Cuenta != null) {
            _ccId = _Cuenta.value();
        }
        if (_Anio != null) {
            _anioId = _Anio.value();
        }

        args.data = $.extend(args.data, { fuenteId: _fteId, cuentaId: _ccId, anioId: _anioId });
    }

    function onComboBoxLoad() {
        $(this).data("tComboBox").fill();
    }

    function onCompleteImputaciones(e) {
        debugger;
        if (e.name == "dataBinding" || e.name == "insert" || e.name == "update" || e.name == "delete") {
            var _CreditoAnual = "0.00";
            var _Acumulado = "0.00";
            var _Saldo = "0.00";
            var Indice = e.response.total;
            if (Indice == null) {
                Indice = e.response.length - 1;
            }
            else {
                Indice--;
            }
            if (Indice < 0) {
                Indice = 0;
            }
//            if (e.name == "dataBinding") {
//                if (e.response.length > 0) {
//                    _CreditoAnual = e.response[Indice].CreditoTexto;
//                    _Acumulado = e.response[Indice].AcumuladoTexto;
//                    _Saldo = e.response[Indice].SaldoTexto;
//                }
//            }
//            else {
//                if (e.response.total > 0) {
//                    _CreditoAnual = e.response.data[Indice].CreditoTexto;
//                    _Acumulado = e.response.data[Indice].AcumuladoTexto;
//                    _Saldo = e.response.data[Indice].SaldoTexto;
//                }
//            }
//            $("#lblCredito").text(_CreditoAnual);
//            $("#lblAcumulado").text(_Acumulado);
//            $("#lblSaldo").text(_Saldo);
            debugger;
            if (e.name == "insert" || e.name == "update") {
                if (e.response.modelState != null) {
                    MostrarErrores(e.response.modelState.MensajeError.errors);
                }
		else{
		        CerrarWaiting();
		        $('.t-overlay').css('display', 'none');
		}
            }
            
            if (e.name == "insert") {
                Indice = e.response.total - 1;
                if (Indice != null && Indice >= 0) {
                    var _Parametros = new Array(new Array(e.response.data[0].UltimoId, 'impId'));
                    if (e.response.data[0].UltimoId > 0) {
                        InvocarReporte('rptImputacionFinanciera', _Parametros);
                    }
                }
            } else if (e.name == "delete") {
                $("#Grid").data("tGrid").ajaxRequest();
                //Refrescar();
            } else if (e.name == "dataBinding") {
                CerrarWaiting();
            }
        }
    }

    var FunctionCallback = null;
    function RevisaSaldoNegativo(CallBack) {
        debugger;
        FunctionCallback = CallBack;
        if (FunctionCallback != null) {
            FunctionCallback();
        }
    }

    function onCompletePieImputaciones(e) {
        debugger;
        RevisaSaldoNegativo(function() {
            var grillaPie = $("#GridPieImputaciones").data("tGrid");
            var Item = grillaPie.data[2];
            debugger;
            ExisteSaldoNegativo = false;
            if ((Item.ImporteBienesConsumo < 0) || (Item.ImporteBienesDeUso < 0) || (Item.ImporteGastosEnPersonal < 0)
                || (Item.ImporteSDDOP < 0) || (Item.ImporteServiciosNoPersonales < 0) || (Item.ImporteTotal < 0)
                || (Item.ImporteTranferencias < 0)) {
                ExisteSaldoNegativo = true;
                jAlert('Existe saldo negativo, debe corregir el saldo para poder imprimir las Imputaciones', '¡Atención!');
            }
        });
    }

    function Imprimir(e) {
        if (ExisteSaldoNegativo) {
            jAlert('Existe saldo negativo, debe corregir el saldo para poder imprimir las Imputaciones', '¡Atención!');
            return;
        }
        var _Parametros = new Array(new Array(parseInt(e.alt), 'impId'));
        InvocarReporte('rptImputacionFinanciera', _Parametros);
    }

    function CambiarEstado(e) {
        if (e == null) {
            jAlert('No tiene los permisos suficientes para Cambiar el Estado de la Imputación', 'Error...');
            return;
        }
        var _Parametros = parseInt(e.alt);
        if (_Parametros == -1) {
            jAlert('No se puede cambiar el "Estado" a una Imputación Anulada', 'Información...');
            return;
        }
        if (e.src.indexOf("/Content/Estados/Verde.png") >= 0) {
            $(e)[0].src = GetPathApp("Content/Estados/Amarillo.png");
            $(e)[0].alt = "Preventiva";
        }
        else {
            if (e.src.indexOf("/Content/Estados/Amarillo.png") >= 0) {
                $(e)[0].src = GetPathApp("Content/Estados/Verde.png");
                $(e)[0].alt = "Compromiso";
            }
        }
        AbrirWaiting();
        $.post(GetPathApp('proImputacion/setCambioDeEstado'), { idImp: _Parametros }, function (data) {
            if (!data) {
                Refrescar();
            }
            CerrarWaiting();
        });
    }

    function Refrescar() {
        var grid = $("#Grid").data("tGrid");
        grid.ajaxRequest();
    }

    //Detalle de Imputaciones
    function onDataBindingDetalle(e) {
        debugger;
        e.data = $.extend(e.data, { idImputacion: idImputacion, _AnulacionParcial: _AnulacionParcial });
    }
    function onSaveDetalle(e) {
        debugger;
        if (_AnulacionParcial == 1) {
            idImputacion = 0;
        }
        var values = e.values;

        e.values.impId = idImputacion;
        e.values.impdImporte = e.values["SubPartidas.impdImporte"];
        e.values.subpId = e.values["SubPartidas.subpId"];
    }
    function onSaveImputacion(e) {
        debugger;
        if (_AnulacionParcial === 1) {
            e.data = $.extend(e.data, { _AnulacionParcial: _AnulacionParcial });
            idImputacion = 0;
        }

        if (e.mode != 'edit' && e.mode != 'insert') {
		    AbrirWaiting();
        }
	    else{
        	    $('.t-window-content.t-content').append('<div class="t-overlay" id="divGuardando" style="z-index: 11000; opacity: 0.5; display: block; vertical-align: middle; text-align: center;"><img src="' + GetPathApp('Content/General/WaitingIndicator.gif') + '" width="45px" alt="" /></div>');
                if (e.mode === "insert" || e.mode === "edit") {
        	        if ("<%=(Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Editar Fecha")%>" === "none") {
        	            e.values.impFecha = $("#impFecha").data("tDatePicker").value();
        	        }
                }
        }
    }
    function onCompleteDetalle(e) {
        debugger;
      
        if (e.name == "edit") {
        }

    }
    function onCommand(e) {
        debugger;
        switch (e.name) {
            case "add":
                if (_AnulacionParcial == 1) {
                } else {
                    idImputacion = 0;
                }
                break;
            case "edit":
                _AnulacionParcial = 0;
                idImputacion = e.dataItem['impId'];
                break;
            case "cmdEliminar":
                if (e.dataItem['impEsCompromiso']) {
                    jAlert('No se puede "Eliminar" una Imputación en estado "Compromiso"', 'Error...');
                    return;
                }
                _AnulacionParcial = 0;
                var _impImporte = e.dataItem['impImporte'];
                var _impAnuladaPor = e.dataItem['impAnuladaPor'];
                if (_impImporte > 0 && _impAnuladaPor != null) {
                    jAlert('No se puede "Eliminar" una Imputación Anulada, debe eliminar el Registro de Anulación primero', 'Error...');
                    return;
                }
                idImputacion = e.dataItem['impId'];
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                jConfirm("¿Confirma Eliminar la imputación?", "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdEditar":
                _AnulacionParcial = 0;
                var _impImporte = e.dataItem['impImporte'];
                var _impAnuladaPor = e.dataItem['impAnuladaPor'];
                idImputacion = e.dataItem['impId'];
                if (_impImporte < 0 || _impAnuladaPor != null) {
                    jAlert('No se puede "Modificar" una Imputación Anulada', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdCopiar":
                if (e.dataItem['impEsCompromiso']) {
                    jAlert('No se puede "Anular" una Imputación en estado "Compromiso"', 'Error...');
                    return;
                }
                var _impImporte = e.dataItem['impImporte'];
                if (_impImporte < 0) {
                    jAlert('No se puede copiar una Imputación Anulada', 'Error...');
                    return;
                }
                var _impAnuladaPor = e.dataItem['impAnuladaPor'];
                if (_impAnuladaPor != null) {
                    jAlert('Imputación Anulada con anterioridad, no se puede Anular', 'Error...');
                    return;
                }
                jConfirm("¿Confirma Anulación de la imputación?", "Anular...", function (r) {
                    if (r) {
                        idImputacion = e.dataItem['impId'];
                        AbrirWaiting();
                        debugger;
                        $('#imgimpId' + idImputacion.toString())[0].src = GetPathApp("Content/Estados/Rojo.png");
                        $('#imgimpId' + idImputacion.toString())[0].alt = "Imputación anulada";

                        $.post(GetPathApp('proImputacion/Copiar'), { pImpId: idImputacion }, function (data) {
                            if (data.IsValid) {
                                //$("#Grid").data("tGrid").ajaxRequest();
                                //Refrescar();
                                $("#GridPieImputaciones").data("tGrid").ajaxRequest();
                                // Se cierra el "waiting" aquí por que se requiere que se cargue todo antes.
                                CerrarWaiting();
                            } else {
                                CerrarWaiting();
                                Refrescar();
                                MostrarError(data.Mensaje);
                            }
                        });
                    }
                });
                break;
            case "cmdAnulacionParcial":
                debugger;
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                var f = new Date();
                _AnulacionParcial = 1;
                var _impImporte = e.dataItem['impImporte'];
                var _impAnuladaPor = e.dataItem['impAnuladaPor'];
                idImputacion = e.dataItem['impId'];
                if (_impImporte < 0 || _impAnuladaPor != null) {
                    jAlert('No se puede "Anular parcialmente" una Imputación Anulada', 'Error...');
                    return;
                }
                grid.addRow(tr);
                AbrirWaiting();
                $.post(GetPathApp('proImputacion/CopiarParcial'), { pImpId: idImputacion }, function (data) {
                    if (data) {
                        debugger;
                        if (data.tcoId == 2) {
                            $("#tcoId").data("tComboBox").select(0);
                        } else {
                            $("#tcoId").data("tComboBox").select(1);
                        }
                        $("#impComprobante").val(data.impComprobante);
                        $("#fpId").data("tComboBox").select((data.fpId - 1));
                        $("#ceId").data("tComboBox").select((data.ceId - 1));
                        $("#impDetalle").val(data.impDetalle);
                        $(impuAnuladaParcialPor).val(idImputacion);
                        $("#impImporte").val(data.impImporte);
                        CerrarWaiting();
                    } else {
                        debugger;
                        CerrarWaiting();
                    }
                });
                break;
        }
    }

    function onEditSubPartida(e) {
        //debugger;
    }

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    function onDataBound() {
	debugger;
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
        $("#GridPieImputaciones").data("tGrid").ajaxRequest();
    }

    function onCommandEditImputaciones(e) {
        debugger;
        $("#impComprobante").attr("title", 'El formato correcto del comprobante es, por ejemplo: "800-1234-2014"');

        if (e.mode === "insert" || e.mode === "edit") {
            $("#impFecha").data("tDatePicker").value(e.mode === "insert" ? new Date() : e.dataItem.impFecha);
            if ("<%=(Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Editar Fecha")%>" === "none") {
                $("#impFecha").data("tDatePicker").disable();
            } else {
                $("#impFecha").data("tDatePicker").enable();
            }

            if (e.mode === "edit") {
                var _Fondo = $("#fpId").data("tComboBox");
                var _CE = $("#ceId").data("tComboBox");
                if ($("#tcoId").data("tComboBox").text() === "Expediente") {
                    $("#impComprobante").attr("title", 'El formato correcto del comprobante es, por ejemplo: "800-1234-2014"');
                    _Fondo.disable();
                    _CE.enable();
                }
                else {
                    $("#impComprobante").attr("title", '');
                    _Fondo.enable();
                    _CE.disable();
                }
            }
        }

        onCommandEdit(e);
    }

    function MostrarErrores(vError) {
        //e.response.modelState
        var MensajeDeError = "";
        
        for (var i = 0; i < vError.length; i++) {
            MensajeDeError += (i + 1).toString() + ") " + vError[i] + "\n\r";
        }
        CerrarWaiting();
        jAlert(MensajeDeError, 'Errores encontrados...', function () {
            //$("#divGuardando").remove();
            $("#impComprobante").focus();
        });
    }

    function onErrorImputacion(e) {
        debugger;
        //the current XMLHttpRequest object
        var xhr = e.XMLHttpRequest;
        //the text status of the error - "timeout", "error" etc.
        var status = e.textStatus;

        CerrarWaiting();
        $('.t-overlay').css('display', 'none');
        if (status == "error") {
            //xhr.status is the HTTP code returned by the server
            //            if (xhr.status == "404") {
            //                alert("requested url not found")
            //            }
            _Error = true;
        }
        else {
            _Error = false;
        }
    }
</script>
<% ViewData["AltoEditor"] = "200px"; %>
<%
    bool _Permite = (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Agregar Imputación futura") != "none";
    int _AnioActual = DateTime.Now.Year;
    %>
<input type="hidden" id="PermiteImputacionFutura" value="<%: _Permite %>"/>
<input type="hidden" id="AnioActual" value="<%: _AnioActual %>"/>
<div style="overflow: hidden; height: 418px;" >
<% Html.Telerik().Grid((IEnumerable<GeDoc.proImputaciones>)ViewData["Imputaciones"])
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.impId);
        })
     .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Template(grid =>
            { %>
                <%= grid.InsertButton(GridButtonType.Image, new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Agregar"), title = "Agregar Imputación" }, new { style = "margin-left:0" })%>
                <label id="lblAnio" for="Customers-input" style="margin-left: 5px;">Año:</label>
                <%= Html.Telerik().ComboBox()
                    .Name("cbxAnio")
                    .DropDownHtmlAttributes(new { style = "width:100px;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                    })
                    .HighlightFirstMatch(true)
                    .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                    .SelectedIndex(DateTime.Now.Year - 2000)
                    .HtmlAttributes(new { style = "width: 60px;" })
                    .BindTo((SelectList)ViewData["Anio_Data"])%>
                <label class="lblFuente" for="Customers-input" style="margin-left: 5px;">Fuente:</label>
                <%= Html.Telerik().ComboBox()
                    .Name("cbxFuentes")
                    .DropDownHtmlAttributes(new { style = "width:Auto;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                    })
                    .HighlightFirstMatch(true)
                    .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                    .SelectedIndex(0)
                    .HtmlAttributes(new { style = "width: 150px;" })
                    .BindTo((SelectList)ViewData["fteId_Data"])%>
                <label class="lblCuenta" for="Customers-input" style="margin-left: 5px;">Cuenta:</label>
                <%= Html.Telerik().ComboBox()
                    .Name("cbxCuenta")
                    .DropDownHtmlAttributes(new { style = "width:Auto;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                    })
                    .HighlightFirstMatch(true)
                    .HtmlAttributes(new { style = "width: 290px;" })
                    .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                    .SelectedIndex(0)
                    .BindTo((SelectList)ViewData["ccId_Data"])%>
                <%= grid.CustomCommandToolBarButton("cmdExportar", "Exportar", "Exportar", "proImputacion", new { page = 1, orderBy = "~", filter = "~" }, false, GridButtonType.Text, new { title = "Exportar grilla de datos" }, null) %>
                <!--
                <label class="lblPartida" for="Customers-input" style="margin-left: 5px;">Partida:</label>
                -->
                <% //Html.Telerik().ComboBox()
                //                    .Name("cbxPartida")
                //                    .DropDownHtmlAttributes(new { style = "width:Auto;" })
                //                    .OpenOnFocus(true)
                //                    .AutoFill(true)
                //                    .Filterable(filtering =>
                //                    {
                //                        filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                //                    })                    
                //                    .HighlightFirstMatch(true)
                //                    .HtmlAttributes(new { style = "width: 200px;" })
                //                    .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                //                    .SelectedIndex(0)
                //                    .BindTo((SelectList)ViewData["partId_Data"])%>
            <%
            }
              );
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "proImputacion")
                .Insert("_InsertEditing", "proImputacion")
                .Update("_SaveEditing", "proImputacion")
                .Delete("_DeleteEditing", "proImputacion");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Custom("cmdEditar")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(false)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;", title = "Modificar Imputación" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Modificar") })
                    .SendState(false)
                    .DataRouteValues(route =>
                        {
                            route.Add(o => o.impId).RouteKey("ImputacionId");
                        });
                commands.Custom("cmdEliminar")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -337px;", title = "Eliminar Imputación" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Eliminar") });
                //commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Eliminar") });
                commands.Custom("cmdCopiar")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -240px;", title = "Anular Imputación" });
                commands.Custom("cmdAnulacionParcial")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(false)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -336px;", title = "Anular Imputación parcialmente" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Eliminar") })
                    .SendState(false)
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.impId).RouteKey("ImputacionId");
                    }); 
            }).Width("70px").Title("Acciones");
            columns.Bound(c => c.Transferencias).Width("80px").Title("").Visible(false);
            columns.Bound(c => c.ServiciosNoPersonales).Width("80px").Title("").Visible(false);
            columns.Bound(c => c.BienesDeUso).Width("80px").Title("").Visible(false);
            columns.Bound(c => c.BienesDeConsumo).Width("80px").Title("").Visible(false);
            columns.Bound(c => c.GastosEnPersonal).Width("80px").Title("").Visible(false);
            columns.Bound(c => c.impId).Width("15px").Title("").Visible(true).Filterable(false)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/Printer.png' title='Imprimir' id='impId<#= impId #>' alt='<#= impId #>' height='22px' style='vertical-align:top;' onclick='Imprimir(this);' /></div>");
            columns.Bound(c => c.impEsCompromiso).Width("30px").Title("Estado").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/<#= impAnuladaPor != null ? \"Rojo\" : (impEsCompromiso ? \"Verde\" : \"Amarillo\") #>.png' title='<#= impAnuladaPor != null ? \"Imputación anulada\" : (impEsCompromiso ? \"Compromiso\" : \"Preventiva\") #>' id='imgimpId<#= impId #>' alt='<#= impAnuladaPor != null ? -1 : impId #>' height='22px' style='vertical-align:top;' onclick='" + ((Session["Permisos"] as GeDoc.Acciones).Visibilidad("proImputacion", "Compromiso") != "hidden" ? "CambiarEstado(this)" : "CambiarEstado(null)") + ";' /></div>");
            columns.Bound(c => c.tgDescripcion).Width("70px").Title("Tipo de Gasto").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= tgDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= tgDescripcion #> </label>");
            columns.Bound(c => c.impDetalle).Width("70px").Title("Detalle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= impDetalle #>' style='cursor: pointer;' id='txtimpDetalle' style='white-space: nowrap;'><#= impDetalle #> </label>");
            columns.Bound(c => c.tcoDescripcion).Width("80px").Title("Tipo Comp.").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= tcoDescripcion #>' style='cursor: pointer;' id='txttcoDescripcion' style='white-space: nowrap;'><#= tcoDescripcion #> </label>");
            columns.Bound(c => c.impComprobante).Width("80px").Title("N° Comp.").Visible(true);
            columns.Bound(c => c.CEFP).Width("80px").Title("FP/CE").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= CEFP #>' style='cursor: pointer;' id='txtCEFP' style='white-space: nowrap;'><#= CEFP #> </label>");
            columns.Bound(c => c.impFecha).Width("50px").Title("Fecha").Visible(true);
                            //.FooterTemplate(() =>
                            //{%>
                            <!--
                                <div align="right">Crédito: </div>
                                <div align="right">Acumulado: </div>
                                <div align="right" style="font-weight: bold">Saldo: </div>
                                -->
                            <%//})
            //columns.Bound(c => c.Acumulado).Width("10%").Title("Acumulado").Visible(true);
            //columns.Bound(c => c.Saldo).Width("10%").Title("Saldo").Visible(true);
            columns.Bound(c => c.impImporte).Width("70px").Title("Imputado").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
                            //.FooterTemplate(() =>
                            //{%>
                            <!--
                                <div align="right"><label id="lblCredito" ></label></div>
                                <div align="right"><label id="lblAcumulado" ></label></div>
                                <div align="right" style="font-weight: bold"><label id="lblSaldo" ></label></div>
                                -->
                            <%//})
                              //.Visible(true).HtmlAttributes(new { style = "text-align: right;" });
        })
            .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))//3000
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_SelectEditing", "proImputacion", new { fuenteId = 1, cuentaId = 1, AnioId = 2012 }))
            
            .ClientEvents(events => events.OnSave("onSaveImputacion").OnEdit("onCommandEditImputaciones").OnDataBinding("onDataBindingImputaciones").OnComplete("onCompleteImputaciones").OnCommand("onCommand").OnRowSelected("onRowSelected").OnDataBound("onDataBound").OnError("onErrorImputacion"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(330))//360
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
            .Render();
%>
</div>
<div >
<% Html.Telerik().Grid<GeDoc.PieImputaciones>()
        .Name("GridPieImputaciones")
        .Columns(columns =>
        {
            columns.Bound(c => c.Descripcion).Width("12px").Title("").Visible(true);
            columns.Bound(c => c.ImporteBienesConsumo).Width("180px").Visible(true).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" }).HtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.ImporteServiciosNoPersonales).Width("200px").Visible(true).HtmlAttributes(new { style = "text-align: right;" }).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" });
            columns.Bound(c => c.ImporteBienesDeUso).Width("150px").Visible(true).HtmlAttributes(new { style = "text-align: right;" }).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" });
            columns.Bound(c => c.ImporteTranferencias).Width("150px").Visible(true).HtmlAttributes(new { style = "text-align: right;" }).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" });
            columns.Bound(c => c.ImporteGastosEnPersonal).Width("150px").Visible(true).HtmlAttributes(new { style = "text-align: right;" }).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" });
            columns.Bound(c => c.ImporteSDDOP).Width("150px").Visible(true).HtmlAttributes(new { style = "text-align: right;" }).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" });
            columns.Bound(c => c.ImporteTotal).Width("200px").Visible(true).HtmlAttributes(new { style = "text-align: right;" }).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" });
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingPieImputaciones", "proImputacion", new { fuenteId = 1, cuentaId = 1, AnioId = DateTime.Now.Date.Year }))
        .ClientEvents(events => events.OnDataBinding("onDataBindingPieImputaciones").OnDataBound("onCompletePieImputaciones"))
        .Footer(false)
        .Render();
%>
</div>

<% Html.RenderPartial("MensajeError"); %>
<% }
    else
    {
        Html.RenderPartial("Mensaje", new ParametrosMensaje() { Nombre = "msgAlerta_catTipoDeGasto", TipoMensaje = "error", MostrarEmergente = true, Titulo = "¡Atención!", Mensaje = "Usted no tiene asignada una Institución contable, no puede gestionar tipos de gastos.", Visible = true});
        %>
<%
    }
    %>