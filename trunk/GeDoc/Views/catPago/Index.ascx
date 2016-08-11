<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>

<% Html.RenderPartial("VistaPasesExpedientes"); %>

<script type="text/javascript">
    var _RowIndex_catPago = -1;
    var _DatosRegistro_catPago;
    var _WindowCRUD;

    function onVerPases(idExpediente) {
        VerPases(idExpediente);
    }

    function onRowSelected_catPago(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex_catPago = e.row.rowIndex;
        _DatosRegistro_catPago = dataItem;
    }
    function onComplete_catPago(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }

    function onCommandEdit_catPago(e) {
        $('#pagExpediente').mask("999-99999-9999");
        $('#pagExpediente').blur(function () {
            if ($('#pagExpediente').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('expExpediente/getExisteExpediente');
                $.ajax({
                    url: _Post,
                    data: { Comprobante: $('#pagExpediente').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#pagExpediente').val("");
                            $('#pagExpediente').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('El Expediente ingresado no existe.', '¡Atención!', function () {
                                $('#pagExpediente').val("");
                                $('#pagExpediente').focus();
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
            case "edit":
        default:
        }
        _WindowCRUD = $("#gridcatPagoPopUp").data("tWindow");

        onCommandEdit(e);
    }

    function onCommand_catPago(e) {
        if (("cmdEditar_catPago, cmdEliminar_catPago").indexOf(e.name) >= 0) {
            if (_RowIndex_catPago < 0) {
                jAlert('Debe seleccionar un registro de Pago.', 'Error...');
                return;
            }
        }
        var grid;
        var tr;
        switch (e.name) {
            case "cmdAgregar_catPago":
                grid = $(this).data("tGrid");
                grid.addRow();
                break;
            case "cmdEditar_catPago":
                grid = $(this).data("tGrid");
                tr = $("#gridcatPago tbody tr:eq(" + (_RowIndex_catPago + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminar_catPago":
                grid = $(this).data("tGrid");
                tr = $("#gridcatPago tbody tr:eq(" + (_RowIndex_catPago + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar este Pago?', "Anular...", function (r) {
                    if (r) {
                        grid.deleteRow(tr);
                    }
                });
                break;
        }
        
    }
    
    function onDataBinding_catPago(e) {
        var grid = $(this).data("tGrid");
        e.data = $.extend(e.data, { Filtro: (grid.filterBy || '~') });
    }
    
    function onError_catPago(args) {
        debugger;
        if (args.textStatus == "parsererror") {
            args.preventDefault();
        }
        if (args.textStatus == "error") {
            args.preventDefault();
            var grid = $('#gridcatPago').data("tGrid");
            //AbrirWaiting();
            grid.ajaxRequest();
        }
    }

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;

    function onDataBound_catPago() {
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
        href = href.replace(/filter=(.*)/, 'filter=' + (_FilterBy || '~'));
        _Boton.attr('href', href);
    }

</script>
<% ViewBag.OpenOnFocus = false; %>
<% ViewData["FiltroContains"] = true; %>
<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.catPagoWS>()
        .Name("gridcatPago")
        .DataKeys(keys =>
        {
            keys.Add(p => p.pagId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar_catPago").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPago", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar_catPago").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPago", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdEliminar_catPago").ButtonType(GridButtonType.ImageAndText).Text("Eliminar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPago", "Eliminar") });
            commands.Custom().Action("Exportar", "catPago", new { page = 1, orderBy = "~", filter = "~" }).Name("cmdExportar").ButtonType(GridButtonType.ImageAndText).Text("Exportar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Excel.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPago", "Exportar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catPago", new { Filtro = "~" })
                .Insert("_InsertEditing", "catPago")
                .Update("_SaveEditing", "catPago")
                .Delete("_DeleteEditing", "catPago");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.pagFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.pagExpediente).Width("140px").Title("N° Expediente").Visible(true)
            .ClientTemplate("<label title='Haga click aquí para ver los pases' onclick=\"onVerPases('<#= pagIdExpediente #>', <#= pagExpediente #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= pagExpediente #> </label>");
            columns.Bound(c => c.pagExpedienteCaratula).Width("350px").Title("Carátula Expediente").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pagExpedienteCaratula #>' style='cursor: pointer;' ><#= pagExpedienteCaratula #></label>");
            columns.Bound(c => c.Proveedor).Width("350px").Title("Proveedor").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Proveedor #>' style='cursor: pointer;' ><#= Proveedor #></label>");
            columns.Bound(c => c.ProveedorNombreDeFantasia).Width("350px").Title("Nombre de Fantasía").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= ProveedorNombreDeFantasia #>' style='cursor: pointer;' ><#= ProveedorNombreDeFantasia #></label>");
            columns.Bound(c => c.pagDetalle).Width("350px").Title("Detalle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pagDetalle #>' style='cursor: pointer;' ><#= pagDetalle #></label>");
            columns.Bound(c => c.ceDescripcion).Width("350px").Title("Cuentra Escritural").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= ceDescripcion #>' style='cursor: pointer; white-space: nowrap;'><#= ceDescripcion #></label>");
            columns.Bound(c => c.pagMonto).Format("{0:0.00}").Width("120px").Title("Monto").Visible(true).HtmlAttributes(new {style = "text-align: right;"});
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit_catPago").OnDataBinding("onDataBinding_catPago").OnRowSelected("onRowSelected_catPago").OnComplete("onComplete_catPago").OnCommand("onCommand_catPago").OnError("onError_catPago").OnDataBound("onDataBound_catPago"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 100%;" })
%>
</div>
