<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var _RowIndex = -1;
    var _DatosRegistro;
    var _RowIndexHorario = -1;
    var _DatosRegistroHorario;
    var _EsModificar = false;
    var _Estado = '';
    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;

        CargaHistorial();
    }
    function CargaHistorial() {
        var grid = $("#GridHistorial").data("tGrid");
        grid.dataBind(_DatosRegistro.Historial);

        $("#txtId")[0].value = _DatosRegistro.tiqId;
        $("#txtPrioridad")[0].value = _DatosRegistro.Prioridad.tipoDescripcion;
        $("#txtMotivoLlamada")[0].value = _DatosRegistro.MotivoLlamada.tipoDescripcion;
        $("#txtSolicitante")[0].value = _DatosRegistro.Empleado.perApellidoyNombre;
        $("#txtDetalleTiquet")[0].value = _DatosRegistro.tiqDetalle;
        $("#txtOficina")[0].value = _DatosRegistro.Oficina.ofiNombre;
    }
    function onCommand(e) {
        _EsModificar = false;
        _Estado = '';
        if (("cmdEditar, cmdAtender, cmdSolucionado, cmdSinSolución, cmdRellamada, cmdHistorial").indexOf(e.name) >= 0) {
            if (_RowIndex < 0) {
                jAlert('Debe seleccionar un Tickets.', 'Error...');
                return;
            }
        }
        switch (e.name) {
            case "cmdAgregar":
                var grid = $(this).data("tGrid");
                grid.addRow();
                break;
            case "cmdAtender":
                if (_DatosRegistro.Estado.Estado.tipoCodigo == "E1") {
                    jAlert('Este Tickets ya se está Trabajando.', 'Error...');
                    return;
                }
                if (_DatosRegistro.Estado.Estado.tipoCodigo != "E0") {
                    jAlert('Este Tickets se encuentra ' + _DatosRegistro.Estado.Estado.tipoDescripcion + '; no se puede cambiar.', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma dar atención al Tickets Número ' + _DatosRegistro.tiqId.toString() + '?', "Atender...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('catTicketSoporte/_AtenderTickets'), { id: _DatosRegistro.tiqId }, function (data) {
                            if (data) {
                                $("#Grid").data("tGrid").rebind();
                            }
                        });
                    }
                });
                break;
            case "cmdSolucionado":
                if (_DatosRegistro.Estado.Estado.tipoCodigo == "E3") {
                    jAlert('Este Tickets ya está Cerrado.', 'Error...');
                    return;
                }
                if (_DatosRegistro.Estado.Estado.tipoCodigo != "E1") {
                    jAlert('Este Tickets se encuentra ' + _DatosRegistro.Estado.Estado.tipoDescripcion + '; no se puede cerrar.', 'Error...');
                    return;
                }
                _Estado = 'E2';

                var _wTicket = $("#wEstadoTickets").data("tWindow");
                _wTicket.center().open();
                break;
            case "cmdSinSolución":
                if (_DatosRegistro.Estado.Estado.tipoCodigo == "E4") {
                    jAlert('Este Tickets ya está Cerrado.', 'Error...');
                    return;
                }
                if (_DatosRegistro.Estado.Estado.tipoCodigo != "E1") {
                    jAlert('Este Tickets se encuentra ' + _DatosRegistro.Estado.Estado.tipoDescripcion + '; no se puede cerrar.', 'Error...');
                    return;
                }
                _Estado = 'E3';

                var _wTicket = $("#wEstadoTickets").data("tWindow");
                _wTicket.center().open();
                break;
            case "cmdRellamada":
                if (_DatosRegistro.Estado.Estado.tipoCodigo != "E0") {
                    jAlert('Este Tickets se encuentra ' + _DatosRegistro.Estado.Estado.tipoDescripcion + '; no se puede registrar una llamada.', 'Error...');
                    return;
                }
                _Estado = 'E0';

                jConfirm('¿Confirma registrar una nueva llamada sobre el Tickets Número ' + _DatosRegistro.tiqId.toString() + '?', "Rellamada...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('catTicketSoporte/_SolucionarTickets'), { id: _DatosRegistro.tiqId, MotivoSoporte: "Rellamada", idMotivo: -1, pEstado: _Estado }, function (data) {
                            if (data) {
                                $("#Grid").data("tGrid").rebind();
                            }
                        });
                    }
                });
                break;
            case "cmdHistorial":
                var _wHistorial = $("#wTicketsHistorial").data("tWindow");
                _wHistorial.center().open();
                break;
            case "edit":
                _EsModificar = true;
                break;
        }
    }
    function onComplete(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
    function onRowDataBound(e) {
        if (_RowIndex > -1) {
            var grid = $("#Grid").data("tGrid");
            var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistro = grid.dataItem(tr);

            CargaHistorial();
        }
    }
    function onSave(e) {
        var values = e.values;
    }
    function onCancelar() {
        var _wTicket = $("#wEstadoTickets").data("tWindow");

        _wTicket.close();
    }

    function onAceptar() {
        var _Motivo = $("#cbxMotivoSoporte").data("tComboBox");
        var _Detalle = $("#txtDetalle")[0].value;
        var _motsId = 0;
        if (_Motivo != null) {
            _motsId = _Motivo.value();
        }
        if (_motsId == null || _motsId <= 0) {
            jAlert('Debe elegir un motivo del soporte.', 'Error...');
            return;
        }

        if (_Detalle == null || _Detalle == "") {
            jAlert('Escriba el detalle de la solución.', 'Error...');
            return;
        }

        AbrirWaiting();
        $.post(GetPathApp('catTicketSoporte/_SolucionarTickets'), { id: _DatosRegistro.tiqId, MotivoSoporte: _Detalle, idMotivo: _motsId, pEstado: _Estado }, function (data) {
            if (data) {
                onCancelar();
                $("#Grid").data("tGrid").rebind();
            }
        });
    }

    function onComboBoxLoad() {
        $(this).data("tComboBox").fill();
    }

    function onOpenWindow() {
        var _Motivo = $("#cbxMotivoSoporte-input");
        _Motivo.focus();
    }
</script>
<% ViewData["FiltroContains"] = true; %>

<% Html.RenderPartial("Waiting"); %>
<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.catTicketSoportes>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.tiqId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTicketSoporte", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdRellamada").ButtonType(GridButtonType.ImageAndText).Text("Rellamada")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/RellamadaAmarillo.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTicketSoporte", "Estado") });
            commands.Custom().Ajax(true).Name("cmdAtender").ButtonType(GridButtonType.ImageAndText).Text("Atender")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/Estados/Trabajando16_16.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTicketSoporte", "Estado") });
            commands.Custom().Ajax(true).Name("cmdSolucionado").ButtonType(GridButtonType.ImageAndText).Text("Solucionado")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/Estados/SolucionadoVerde16_16.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTicketSoporte", "Estado") });
            commands.Custom().Ajax(true).Name("cmdSinSolución").ButtonType(GridButtonType.ImageAndText).Text("Sin solución")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/Estados/SinSolucionRojo16_16.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTicketSoporte", "Estado") });
            commands.Custom().Ajax(true).Name("cmdHistorial").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -240px;" })
                .Text("Historial")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTicketSoporte", "Estado") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catTicketSoporte")
                .Insert("_InsertEditing", "catTicketSoporte");
                //.Update("_SaveEditing", "catTicketSoporte")
                //.Delete("_DeleteEditing", "catTicketSoporte");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.tiqId).Width("50px").Title("Id").Visible(true);
            columns.Bound(c => c.Estado.Estado.tipoDescripcion).Width("80px").Title("Estado").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img title='<#= Estado.Estado.tipoDescripcion #>' src='" + Url.Content("~/Content") + "/Estados/<#= Estado.Estado.tipoCodigo == \"E0\" ? \"EnEspera.png\" : (Estado.Estado.tipoCodigo == \"E1\" ? \"Trabajando.png\" : (Estado.Estado.tipoCodigo == \"E2\" ? \"SolucionadoVerde.png\" : \"SinSolucionRojo.png\")) #>' height='22px' width='22px' style='vertical-align:middle;' /></div>");
            columns.Bound(c => c.Prioridad.tipoDescripcion).Width("90px").Title("Prioridad").Visible(true)
            .ClientTemplate("<label title='<#= Prioridad.tipoDescripcion #>' style='cursor: pointer;' id='txtPrioridad_tipoDescripcion' ><#= Prioridad.tipoDescripcion #></label>");
            columns.Bound(c => c.tiqFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.tiqHora).Width("80px").Title("Hora").Visible(true);
            columns.Bound(c => c.MotivoLlamada.tipoDescripcion).Width("250px").Title("Motivo llamada").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= MotivoLlamada.tipoDescripcion #>' style='cursor: pointer;' id='txtMotivoLlamada_tipoDescripcion' ><#= MotivoLlamada.tipoDescripcion #></label>");
            columns.Bound(c => c.Empleado.perApellidoyNombre).Width("250px").Title("Solicitante").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Empleado.perApellidoyNombre #>' style='cursor: pointer;' id='txtEmpleado_perApellidoyNombre' ><#= Empleado.perApellidoyNombre #></label>");
            columns.Bound(c => c.tiqDetalle).Width("250px").Title("Detalle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= tiqDetalle #>' style='cursor: pointer;' id='txttiqDetalle' ><#= tiqDetalle #></label>");
            columns.Bound(c => c.Oficina.ofiNombre).Width("200px").Title("Oficina").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Oficina.ofiNombre #>' style='cursor: pointer;' id='txtOficina_ofiNombre' ><#= Oficina.ofiNombre #></label>");
            columns.Bound(c => c.Estado.Usuario.usrApellidoyNombre).Width("250px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Estado.Usuario.usrApellidoyNombre #>' style='cursor: pointer;' id='txtEstado_Usuario_usrApellidoyNombre' ><#= Estado.Usuario.usrApellidoyNombre #></label>");
            columns.Bound(c => c.Estado.tiqeFecha).Format("{0:dd/MM/yyyy}").Width("120px").Title("Fecha Solución").Visible(true);
            columns.Bound(c => c.Estado.tiqeHora).Width("120px").Title("Hora Solución").Visible(true);
            columns.Bound(c => c.Estado.MotivoSoporte.motsDescripcion).Width("250px").Title("Motivo Soporte").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Estado.MotivoSoporte == null ? \"\" : Estado.MotivoSoporte.motsDescripcion #>' style='cursor: pointer;' id='txtEstado_MotivoSoporte_motsDescripcion' ><#= Estado.MotivoSoporte == null ? \"\" : Estado.MotivoSoporte.motsDescripcion #></label>");
            columns.Bound(c => c.Estado.tiqeDetalle).Width("250px").Title("Solución").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Estado.tiqeDetalle #>' style='cursor: pointer;' id='txtEstado_tiqeDetalle' ><#= Estado.tiqeDetalle #></label>");
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit").OnRowSelected("onRowSelected").OnCommand("onCommand").OnComplete("onComplete").OnRowDataBound("onRowDataBound").OnSave("onSave"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable(o => o.SortMode(GridSortMode.MultipleColumn))
            .HtmlAttributes(new { style = "width: 99.8%;" })

%>
</div>
<!-- Detalle de soporte -->
<% string _btnAceptar = "";
   string _btnCancelar = "";
   _btnAceptar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -33px -335px;";
   _btnCancelar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -49px -335px;";
%>
<% Html.Telerik().Window()
        .Name("wEstadoTickets")
        .Title("Cambio de Estado")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div style="margin-bottom: 10px;">
                <label id="lblMotivodelSoporte" style="vertical-align: middle;">Motivo del Soporte:</label>
                <%= Html.Telerik().ComboBox()
                    .Name("cbxMotivoSoporte")
                    .DropDownHtmlAttributes(new { style = "width: 300px;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                    })
                    .HighlightFirstMatch(true)
                    .ClientEvents(events => events.OnLoad("onComboBoxLoad"))
                    .HtmlAttributes(new { style = "width: 300px;" })
                    .BindTo((SelectList)ViewData["motsId_Data"])
                %>
            </div>
            <div>
                <label id="lblDetalle" style="vertical-align: top;">Detalle:</label>
                <%= Html.TextArea("txtDetalle", new { style = "width: 367px;" }) %>
            </div>
            <div style="position: absolute; top: 10px; left: 78%; width: 30px;">
                <table>
                    <tr>
                        <td style="border: none;">
                            <button class="t-button" onclick="onAceptar()">
                                Aceptar
                            </button>
                        </td>
                        <td style="border: none;">
                            <button class="t-button" onclick="onCancelar()">
                                Cancelar
                            </button>
                        </td>
                    </tr>
                </table>
            </div>
        <%})
       .Buttons(b => b.Close())
       .ClientEvents(e => e.OnActivate("onOpenWindow"))
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(724)
       .Height(150)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wTicketsHistorial")
        .Title("Historial")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div class="BordeRedondeado" style="margin-bottom: 8px;">
                <div style="margin: 5px;">
                    <label>Id:</label>
                    <input id="txtId" disabled="disabled" style="width: 50px;" />
                    <label>Prioridad:</label>
                    <input id="txtPrioridad" disabled="disabled" style="width: 80px;" />
                    <label>Motivo llamada:</label>
                    <input id="txtMotivoLlamada" disabled="disabled" style="width: 292px;" />
                    <label>Solicitante:</label>
                    <input id="txtSolicitante" disabled="disabled" style="width: 250px;" />
                    <label>Detalle:</label>
                    <input id="txtDetalleTiquet" disabled="disabled" style="width: 450px;" />
                    <label>Oficina:</label>
                    <input id="txtOficina" disabled="disabled" style="width: 406px;" />
                </div>
            </div>
            <%
            Html.Telerik().Grid((IEnumerable<GeDoc.catTicketSoportesEstados>)ViewData["TicketSoportesEstados"])
                    .Name("GridHistorial")
                    .DataKeys(keys =>
                    {
                        keys.Add(p => p.tiqeId);
                    })
                    .Columns(columns =>
                    {
                        columns.Bound(c => c.tiqeFecha).Width("90px").Title("Fecha").Visible(true).Format("{0:dd/MM/yyyy}");
                        columns.Bound(c => c.tiqeFecha).Width("60px").Title("Hora").Visible(true).Format("{0:hh:mm}");
                        columns.Bound(c => c.Estado.tipoDescripcion).Width("80px").Title("Estado").Visible(true)
                        .ClientTemplate("<div style='width: 100%; text-align: center;'><img title='<#= Estado.tipoDescripcion #>' src='" + Url.Content("~/Content") + "/Estados/<#= Estado.tipoCodigo == \"E0\" ? \"EnEspera.png\" : (Estado.tipoCodigo == \"E1\" ? \"Trabajando.png\" : (Estado.tipoCodigo == \"E2\" ? \"SolucionadoVerde.png\" : \"SinSolucionRojo.png\")) #>' height='22px' width='22px' style='vertical-align:middle;' /></div>");
                        columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("220px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' id='txtUsuario_usrApellidoyNombre' ><#= Usuario.usrApellidoyNombre #></label>");
                        columns.Bound(c => c.MotivoSoporte.motsDescripcion).Width("250px").Title("Motivo Soporte").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<label title='<#= MotivoSoporte == null ? \"\" : MotivoSoporte.motsDescripcion #>' style='cursor: pointer;' id='txtMotivoSoporte_motsDescripcion' ><#= MotivoSoporte == null ? \"\" : MotivoSoporte.motsDescripcion #></label>");
                        columns.Bound(c => c.tiqeDetalle).Width("250px").Title("Solución").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<label title='<#= tiqeDetalle #>' style='cursor: pointer;' id='txttiqeDetalle' ><#= tiqeDetalle #></label>");
                    })
                        .Footer(false)
                        .Selectable()
                        .Scrollable(scroll => scroll.Enabled(true).Height("379px"))
                        .Resizable(resizing => resizing.Columns(true))
                        .HtmlAttributes(new { style = "width: 99.8%;" })
                        .Render();
        })
       .Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(994)
       .Height(486)
       .Render();
%>


