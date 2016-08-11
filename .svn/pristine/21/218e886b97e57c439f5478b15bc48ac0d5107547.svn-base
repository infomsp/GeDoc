<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GeDoc.catTurnos>" %>

<script type="text/javascript">
    var _DatosRegistroPractica;
    var idPracticaSeleccionada;
    var PracticaAccion;

    function onComboBoxChangeNomencladorPractica() {
        debugger;
        $("#txtBusquedaPractica").val("");
        onBuscarPracticas();
    }

    function RefrescarGridPractica() {
        debugger;
        var estGrid = $("#gridPractica").data("tGrid");      
        estGrid.ajaxRequest();
        _DatosRegistroPractica = null;
        PracticaAccion = "";
    }

    function onCommandPractica(e) {
        debugger;
        switch (e.name) {
        case "cmdAgregarPractica":
            var _ventanaTiposPracticas = $("#WtiposPracticas").data("tWindow");
            _ventanaTiposPracticas.center().open();
            PracticaAccion = "Asignar";
            break;
        case "cmdDeleteTurnoPractica":
            onSetPracticaTurno(e.dataItem["pracId"], e.dataItem["PracticaDescripcion"], 1, "Eliminar");
            break;
        case "cmdModificarTurnoPractica":
            PracticaAccion = "Modificar";
            idPracticaSeleccionada = e.dataItem["pracId"];
            PedirCantidad(e.dataItem["turpracCantidad"]);
            break;
        }
        e.preventDefault();
        e.stopPropagation();
    }

    function onSetPracticaTurno(pracId, pPractica, Preguntar, Accion) {
        debugger;
        idPracticaSeleccionada = pracId;
        PracticaAccion = Accion;
        if (Preguntar === 1) {
            jConfirm('¿Confirma ' + Accion + ' la Práctica "' + pPractica + '"?', Accion + " Práctica...", function (r) {
                if (r) {
                    if (Accion === "Asignar") {
                        PedirCantidad(1);
                    } else {
                        irAGuardarPractica(pracId, Accion);
                    }
                }
            });
        } else {
            if (Accion === "Asignar") {
                PedirCantidad(1);
            } else {
                irAGuardarPractica(pracId, Accion);
            }
        }
    }

    function PedirCantidad(QueCantidad) {
        debugger;
        $("#divCantidadPractica").css("display", "block");
        $("#divCantidadPracticaContent").css("display", "block");
        $("#txtCantidadPracticas").val(QueCantidad);
        $("#txtCantidadPracticas").focus();
    }

    function onCerrarCantidadPractica(ConFoco) {
        debugger;
        $("#divCantidadPractica").css("display", "none");
        $("#divCantidadPracticaContent").css("display", "none");
        if (ConFoco) {
            $("#txtBusquedaPractica").focus();
        }
    }

    $("#txtCantidadPracticas").unbind("keydown");
    $("#txtCantidadPracticas").keydown(function (e) {
        debugger;
        if (e.which == 13) {
            debugger;
            if ($("#txtCantidadPracticas").val() <= 0 || $("#txtCantidadPracticas").val() == "") {
                jAlert("Debe ingresar la cantidad de Prácticas.", "¡Error!", function() {
                    $("#txtCantidadPracticas").focus();
                });
                $("#popup_container").focus();
                $("#popup_ok").focus();
                return;
            }
            irAGuardarPractica(idPracticaSeleccionada);
            e.preventDefault();
            e.stopPropagation();
        }
    });

    function irAGuardarPractica(pracId) {
        debugger;
        var CantidadPracticas = 1;
        debugger;
        if (PracticaAccion === "Asignar" || PracticaAccion === "Modificar") {
            AbrirWaitingCRUD();
            CantidadPracticas = $("#txtCantidadPracticas").val();
            $(".es-overlay").css("display", "block");
        } else {
            AbrirWaiting();
        }
        var _Post = "<%=Url.Content("~/catTurno/setTurnoPractica")%>";
        $.ajax({
            url: _Post,
            data: { pracId: pracId, turId: _DatosRegistroHCTurno.turId, pracCantidad: CantidadPracticas, pAccion: PracticaAccion },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert("Falló la actualización de la práctica.", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaiting();
                CerrarWaitingCRUD();
                $(".es-overlay").css("display", "none");
                debugger;
                if (data.IsValid) {
                    var wdiag = $("#WtiposPracticas").data("tWindow");
                    wdiag.close();
                    RefrescarGridPractica();
                    onCerrarCantidadPractica(false);
                } else {
                    jAlert(data.Mensaje, "¡Atención!", function() {
                        $("#divCantidadPractica").css("display", "block");
                        $("#txtCantidadPracticas").focus();
                    });
                }
            }
        });
    }

    function onCommandTiposPracticas(e) {
        debugger;
        if (e.name === "cmdSeleccionarTipoPractica") {
            if (_DatosRegistroPractica == null) {
                jAlert("Debe seleccionar una práctica.", "¡Atención!");
                e.preventDefault();
                e.stopPropagation();
                return;
            }
            onAsignarPractica(_DatosRegistroPractica["pracId"]);
        }
        e.preventDefault();
        e.stopPropagation();
    }

    function onDataBindingAmbasGrillas(args) {
        var _turnoId = 0;
        if (_DatosRegistroHCTurno != null) {
            _turnoId = _DatosRegistroHCTurno.turId;
        } else {
            _turnoId = $("#pacId").val() * -1;
        }

        args.data = $.extend(args.data, { turId: _turnoId });
    }

    function onDataBindingTiposPracticas(args) {
        debugger;
        var _Nomenclador = $("#cbxNomencladorPractica").data("tComboBox");
        var BuscarTexto = "++++++";
        var txtBus = $("#txtBusquedaPractica");
        if (txtBus != null) {
            BuscarTexto = txtBus.val().trim() != "" ? txtBus.val().trim() : BuscarTexto;
        }
        args.data = $.extend(args.data, { nomId: _Nomenclador.value(), TextoBuscado: BuscarTexto });
    }

    function onCompleteAmbasGrillas(args) {
        debugger;
        if (args.name != "update" && args.name != "insert") {
            CerrarWaiting();
        }
    }

    function onDataBoundPracticasHC(e) {
        var grid = $(e.target).data("tGrid");
        var _idTurnoHC = 0;
        var permiteBorrar = false;
        if (_DatosRegistroHCTurno != null) {
            _idTurnoHC = _DatosRegistroHCTurno.turId;
            permiteBorrar = EsMedico;
        } else {
            $(e.target).find(".t-grid-cmdAgregarPractica").css("display", "none");
        }

        $(grid.data).each(function (i) {
            if (this.turId === _idTurnoHC) {
                $("#divHCMensajePracticas").attr("class", "alert-success");
                $("#iHCMensajePracticas").attr("class", "fa fa-2x fa-check");
            } else {
                if (!permiteBorrar) {
                    var row = $(e.target).find(".t-grid-content tr")[i];
                    $(row).find(".t-grid-cmdDeleteTurnoPractica").css("display", "none");
                    $(row).find(".t-grid-cmdModificarTurnoPractica").css("display", "none");
                }
            }
        });
    }

    function onRowSelectedPractica(e) {
        debugger;
        var row = e.row;
        var grid = $("#gridPractica").data("tGrid");
        var dataItem = grid.dataItem(row);
        _DatosRegistroPractica = dataItem;
    }

    function onRowSelectedTiposPracticas(e) {
        _DatosRegistroPractica = $("#gridtiposPracticas").data("tGrid").dataItem(e.row);
    }

    $("#txtBusquedaPractica").keydown(function (e) {
        debugger;
        if (e.which == 13) {
            onBuscarPracticas();
            e.preventDefault();
            e.stopPropagation();
        }
    });

    $("#txtBusquedaPractica").focus(function (e) {
        debugger;
        $(this).select();
    });

    function onBuscarPracticas() {
        debugger;
        AbrirWaiting();
        var grid = $("#gridtiposPracticas").data("tGrid");
        grid.ajaxRequest();
        idPracticaSeleccionada = null;
    }

    function onActivaveWtiposPracticas(e) {
        debugger;
        var txtBus = $("#txtBusquedaPractica")[0];
        $("#txtBusquedaPractica").val("");
        onCerrarCantidadPractica(true);
        txtBus.focus();
    }
</script>

                <div id="Practica" style="width: auto; height: 100%;">
                <%
                    Html.Telerik().Grid((IEnumerable<GeDoc.enlTurnosPracticas>) ViewData["enlTurPractica"])
                        .Name("gridPractica")
                        .DataKeys(keys =>
                        {
                            keys.Add(p => p.turpracId);
                        })
                        .ToolBar(commands =>
                        {
                            commands.Custom().Ajax(true).Name("cmdAgregarPractica").ButtonType(GridButtonType.ImageAndText)
                                .ImageHtmlAttributes(new {style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;"})
                                .Text("Agregar");
                        })
                        .DataBinding(dataBinding =>
                        {
                            dataBinding.Ajax().Select("getPracticasPaciente", "HistoriaClinica", new { turId = 0 });
                        })
                        .Localizable("es-AR")
                        .Columns(columns =>
                        {
                            columns.Command(commands =>
                            {
                                commands.Custom("cmdDeleteTurnoPractica")
                                    .Ajax(true)
                                    .ButtonType(GridButtonType.Image)
                                    .SendDataKeys(true)
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -17px -339px;" })
                                    .HtmlAttributes(new { title = "Eliminar práctica" });
                                commands.Custom("cmdModificarTurnoPractica")
                                    .Ajax(true)
                                    .ButtonType(GridButtonType.Image)
                                    .SendDataKeys(true)
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                                    .HtmlAttributes(new { title = "Modificar cantidad prácticas" });
                            }).Width("80px").Title("Acciones");
                            columns.Bound(c => c.PracticaDescripcion).Width("380px").Title("Práctica").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= PracticaDescripcion #>' style='cursor: pointer;' ><#= PracticaDescripcion #></label>")
                                .FooterHtmlAttributes(new { style = "text-align: right;" })
                                .ClientFooterTemplate("<div>Cantidad total de Prácticas: </div>");
                            columns.Bound(c => c.turpracCantidad).Format("{0:0}").Width("100px").Title("Cantidad").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                                .Aggregate(aggreages => aggreages.Sum())
                                .FooterHtmlAttributes(new { style = "text-align: right;" })
                                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>");
                            columns.Bound(c => c.Fecha).Width("90px").Title("Fecha").Visible(true).Format("{0:dd/MM/yyyy}");
                            //columns.Bound(c => c.Fecha).Width("60px").Title("Hora").Visible(true).Format("{0:HH:mm}");
                            columns.Bound(c => c.Medico).Width("150px").Title("Médico").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= Medico #>' style='cursor: pointer;' ><#= Medico #></label>");
                            columns.Bound(c => c.CentroDeSalud).Width("150px").Title("Centro de Salud").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= CentroDeSalud #>' style='cursor: pointer;' ><#= CentroDeSalud #></label>");
                        })
                        .Pageable((paging) =>
                            paging.Enabled(true)
                                .PageSize(((int) Session["FilasPorPagina"])))
                        .Footer(true)
                        .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandPractica").OnRowSelect("onRowSelectedPractica").OnDataBinding("onDataBindingAmbasGrillas").OnDataBound("onDataBoundPracticasHC"))
                        .Filterable()
                        .Selectable()
                        .Resizable(r => r.Columns(true))
                        .Scrollable(scroll => scroll.Enabled(true).Height(390))
                        .Sortable()
                        .Render();
                %>

</div>

<!-- Tipos de Practicas -->
<% Html.Telerik().Window()
        .Name("WtiposPracticas")
        .Title("Prácticas realizadas")
        .Visible(false)
        .Content(() =>
        {
      %>
        <% Html.RenderPartial("WaitingCRUD"); %>
      <div>
                <div style="margin-top: 5px; margin-bottom: 8px;">
                <label>Nomenclador:</label>
                <%= Html.Telerik().ComboBox()
                    .Name("cbxNomencladorPractica")
                    .DropDownHtmlAttributes(new { style = "width: auto;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)                   
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.Contains);
                    })
                    .ClientEvents(events => events.OnChange("onComboBoxChangeNomencladorPractica"))
                    .HighlightFirstMatch(true)
                    .SelectedIndex(0)
                    .HtmlAttributes(new { style = "width: 140px; vertical-align: middle;" })
                    .BindTo((SelectList)ViewData["nomId_Data"])
                    %>
                    <label style="margin-left: 15px;">Búsqueda:</label>
                    <input type="text" id="txtBusquedaPractica" style="width: 703px;" />
                </div>
    <%             Html.Telerik().Grid((IEnumerable<GeDoc.catPracticas>)ViewData["TiposPracticas"])
                        .Name("gridtiposPracticas")
                        .DataKeys(keys =>
                        {
                            keys.Add(p => p.pracId);
                        })
                            .ToolBar(commands =>
                        {
                            commands.Custom().Ajax(true).Name("cmdSeleccionarTipoPractica").ButtonType(GridButtonType.ImageAndText)
                            .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                            .Text("Seleccionar");

                        })
                        .DataBinding(dataBinding =>
                        {
                            dataBinding.Ajax()
                                .Select("_SelectEditingTiposPractica", "catTurno", new { nomId = 1, TextoBuscado = "+++++" });
                        })
                        .Localizable("es-AR")
                        .Columns(columns =>
                        {
                            columns.Bound(c => c.pracCodigo).Width("70px").Title("Código").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                            .ClientTemplate("<div title='<#= pracCodigo #>' style='cursor: pointer;' ondblclick='onSetPracticaTurno(<#= pracId #>, \"<#= pracDescripcion #>\", 1, \"Asignar\")'><#= pracCodigo #></div>");
                            columns.Bound(c => c.pracDescripcion).Width("380px").Title("Práctica").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                            .ClientTemplate("<div title='<#= pracDescripcion #>' style='cursor: pointer;' ondblclick='onSetPracticaTurno(<#= pracId #>, \"<#= pracDescripcion #>\", 1, \"Asignar\")'><#= pracDescripcion #></div>");
                        })
                     .Pageable((paging) =>
                            paging.Enabled(true)
                            .PageSize(((int)Session["FilasPorPagina"])))
                            .Footer(true)
                            .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandTiposPracticas").OnRowSelect("onRowSelectedTiposPracticas").OnDataBinding("onDataBindingTiposPracticas").OnComplete("onCompleteAmbasGrillas"))
                            .Filterable()
                            .Selectable()
                            .Scrollable(scroll => scroll.Enabled(true).Height(210))
                            .Sortable()
                            .Render();
       %>
  </div>
<%                        })
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1024)
       .Height(400)
       .ClientEvents(eventos => eventos.OnActivate("onActivaveWtiposPracticas"))
       .Render();
%>

<script>
    var wHCMenuOpciones = $("#wHCMenu").data("tWindow");
    if (wHCMenuOpciones.isMaximized == null) {
        wHCMenuOpciones.maximize();
    }

    var _Altura = $("#wHCMenu").height() - 160;
    $("#gridPractica").find(".t-grid-content").css("height", _Altura + "px");
</script>