<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GeDoc.catTurnos>" %>

<script type="text/javascript">
    var _DatosRegistroDiagnostico;
    var _DatosRegistroTiposDiagnosticos;

    function onComboBoxChangeGrupoDiagnostico() {
        debugger;
        $("#txtBusquedaDiagnosticos").val("");
        onBuscarDiagnostico();
    }

    function RefrescarGridDiagnostico() {
        debugger;
        var estGrid = $("#gridDiagnostico").data("tGrid");       
        estGrid.ajaxRequest();
        _DatosRegistroTiposDiagnosticos = null;
    }

    function onCommandDiagnostico(e) {
        var _ventanaTiposDiagnosticos = $("#WtiposDiagnosticos").data("tWindow");
        if (e.name === "cmdAgregarDiagnostico") {
            var _idTurnoHC = 0;
            var ContadorTurnoHC = 0;
            if (_DatosRegistroHCTurno != null) {
                _idTurnoHC = _DatosRegistroHCTurno.turId;
            }
            $($(this).data("tGrid").data).each(function (i) {
                if (this.turId === _idTurnoHC) {
                    ContadorTurnoHC++;
                }
            });

            if (ContadorTurnoHC >= 5) {
                jAlert("No puede agregar más diagnósticos, solo se permiten 5 diagnósticos por turno.", "¡Información!");
            } else {
                _ventanaTiposDiagnosticos.center().open();
            }
        }
        if (e.name === "cmdDeleteTurnoDiagnostico") {
            onSetDiagnosticoTurno(e.dataItem["diagId"], e.dataItem["diagnosticoDescripcion"], 1, "Eliminar");
        }
        e.preventDefault();
        e.stopPropagation();
    }

    function onCommandTiposDiagnosticos(e) {
        if (e.name === "cmdSeleccionarTipo") {
            if (_DatosRegistroTiposDiagnosticos == null) {
                jAlert("Debe seleccionar un diagnóstico.", "¡Atención!");
                e.preventDefault();
                e.stopPropagation();
                return;
            }
            onSetDiagnosticoTurno(_DatosRegistroTiposDiagnosticos["diagId"],"",0,"Asignar");
        }
        e.preventDefault();
        e.stopPropagation();
    }

    function onSetDiagnosticoTurno(diagId, pDiagnostico, Preguntar, Accion) {
        debugger;
        if (Preguntar === 1) {
            jConfirm('¿Confirma ' + Accion + ' el Diagnóstico "' + pDiagnostico + '"?', Accion + " Diagnóstico...", function (r) {
                if (r) {
                    irAGuardarDiagnostico(diagId, Accion);
                }
            });
        } else {
            irAGuardarDiagnostico(diagId, Accion);
        }
    }

    function irAGuardarDiagnostico(diagId, Accion) {
        debugger;
        if (Accion === "Asignar" || Accion === "Modificar") {
            AbrirWaitingCRUD();
        } else {
            AbrirWaiting();
        }
        var _Post = "<%=Url.Content("~/catTurno/setTurnoDiagnostico")%>";
        $.ajax({
            url: _Post,
            data: { diagId: diagId, turId: _DatosRegistroHCTurno.turId, pAccion: Accion },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert("Falló la actualización del diagnóstico.", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaiting();
                CerrarWaitingCRUD();
                debugger;
                if (data.IsValid) {
                    var wdiag = $("#WtiposDiagnosticos").data("tWindow");
                    wdiag.close();
                    RefrescarGridDiagnostico();
                } else {
                    jAlert(data.Mensaje, "¡Atención!", function () {
                        $("#txtBusquedaDiagnosticos").focus();
                    });
                }
            }
        });
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

    function onDataBindingTiposDiagnosticos(args) {
        debugger;
        var _GrupoDiagnostico = $("#cbxGrupoDiagnostico").data("tComboBox");
        var BuscarTexto = "++++++";
        var txtBus = $("#txtBusquedaDiagnosticos");
        if (txtBus != null) {
            BuscarTexto = txtBus.val().trim() != "" ? txtBus.val().trim() : BuscarTexto;
        }
        args.data = $.extend(args.data, { adId: _GrupoDiagnostico.value(), TextoBuscado: BuscarTexto });
    }

    function onCompleteAmbasGrillas(args) {
        debugger;
        if (args.name != "update" && args.name != "insert") {
            CerrarWaiting();
        }
    }

    function onDataBoundDiagnosticosHC(e) {
        debugger;
        var grid = $(e.target).data("tGrid");
        var _idTurnoHC = 0;
        var permiteBorrar = false;
        if (_DatosRegistroHCTurno != null) {
            _idTurnoHC = _DatosRegistroHCTurno.turId;
            permiteBorrar = EsMedico;
        } else {
            $(e.target).find(".t-grid-cmdAgregarDiagnostico").css("display", "none");
        }

        $(grid.data).each(function (i) {
            if (this.turId === _idTurnoHC) {
                $("#divHCMensajeDiagnostico").attr("class", "alert-success");
                $("#iHCMensajeDiagnostico").attr("class", "fa fa-2x fa-check");
            } else {
                if (!permiteBorrar) {
                    var row = $(e.target).find(".t-grid-content tr")[i];
                    $(row).find(".t-grid-cmdDeleteTurnoDiagnostico").css("display", "none");
                }
            }
        });
    }

    function onRowSelectedDiagnostico(e) {
        debugger;
        var row = e.row;
        var grid = $("#gridDiagnostico").data("tGrid");
        var dataItem = grid.dataItem(row);
        _DatosRegistroDiagnostico = dataItem;
    }

    function onRowSelectedTiposDiagnosticos(e) {
        debugger;
        _DatosRegistroTiposDiagnosticos = $("#gridtiposDiagnosticos").data("tGrid").dataItem(e.row);
    }

    $("#txtBusquedaDiagnosticos").keydown(function (e) {
        debugger;
        if (e.which == 13) {
            onBuscarDiagnostico();
            e.preventDefault();
            e.stopPropagation();
        }
    });

    $("#txtBusquedaDiagnosticos").focus(function (e) {
        debugger;
        $(this).select();
    });

    function onBuscarDiagnostico() {
        debugger;
        AbrirWaiting();
        var grid = $("#gridtiposDiagnosticos").data("tGrid");
        grid.ajaxRequest();
    }

    function onActivaveWDiagnosticos(e) {
        debugger;
        var txtBus = $("#txtBusquedaDiagnosticos")[0];
        $("#txtBusquedaDiagnosticos").val("");
        txtBus.focus();
    }
</script>

                <div id="Diagnostico" style="width: auto; height: 100%;">
                <%
                    Html.Telerik().Grid((IEnumerable<GeDoc.enlTurnosDiagnosticos>) ViewData["enlTurDiagnostico"])
                        .Name("gridDiagnostico")
                        .DataKeys(keys =>
                        {
                            keys.Add(p => p.tdId);
                        })
                        .ToolBar(commands =>
                        {
                            commands.Custom().Ajax(true).Name("cmdAgregarDiagnostico").ButtonType(GridButtonType.ImageAndText)
                                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                                .Text("Agregar");
                        })
                        .DataBinding(dataBinding =>
                        {
                            dataBinding.Ajax()
                                .Select("getDiagnosticosPaciente", "HistoriaClinica", new { turId = 0 });
                        })
                        .Localizable("es-AR")
                        //.Editable(editing => editing.Mode(GridEditMode.PopUp))
                        .Columns(columns =>
                        {
                            columns.Command(commands =>
                            {
                                commands.Custom("cmdDeleteTurnoDiagnostico")
                                    .Ajax(true)
                                    .ButtonType(GridButtonType.Image)
                                    .SendDataKeys(true)
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -17px -339px;" })
                                    .HtmlAttributes(new { title = "Eliminar diagnóstico" })
                                    .DataRouteValues(route =>
                                    {
                                        route.Add(o => o.turId).RouteKey("turId");
                                    });
                            }).Width("60px").Title("Acciones");
                            columns.Bound(c => c.diagnosticoDescripcion).Width("180px").Title("Diagnostico").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"})
                                .ClientTemplate("<label title='<#= diagnosticoDescripcion #>' style='cursor: pointer;' ><#= diagnosticoDescripcion #></label>");
                            columns.Bound(c => c.diagnosticoTipo1_Nombre).Width("180px").Title("Cápitulo").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"})
                                .ClientTemplate("<label title='<#= diagnosticoTipo1_Nombre #>' style='cursor: pointer;' ><#= diagnosticoTipo1_Nombre #></label>");
                            columns.Bound(c => c.diagnosticoTipo2_Nombre).Width("150px").Title("Sub Cápitulo").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"})
                                .ClientTemplate("<label title='<#= diagnosticoTipo2_Nombre #>' style='cursor: pointer;' ><#= diagnosticoTipo2_Nombre #></label>");
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
                        .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandDiagnostico").OnRowSelect("onRowSelectedDiagnostico").OnDataBinding("onDataBindingAmbasGrillas").OnDataBound("onDataBoundDiagnosticosHC"))
                        .Filterable()
                        .Selectable()
                        .Resizable(r => r.Columns(true))
                        .Scrollable(scroll => scroll.Enabled(true).Height(390))
                        .Sortable()
                        .Render();
                %>
            </div>

 <!-- Tipos de Diagnosticos -->

<% Html.Telerik().Window()
        .Name("WtiposDiagnosticos")
        .Title("Diagnósticos")
        .Visible(false)
        .Content(() =>
        {
      %>
        <% Html.RenderPartial("WaitingCRUD"); %>
      <div>
                <div style="margin-top: 5px; margin-bottom: 8px;">
                <label>Grupo de Diagnóstico:</label>
                <%= Html.Telerik().ComboBox()
                    .Name("cbxGrupoDiagnostico")
                    .DropDownHtmlAttributes(new { style = "width: auto;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)                   
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.Contains);
                    })
                    .ClientEvents(events => events.OnChange("onComboBoxChangeGrupoDiagnostico"))
                    .HighlightFirstMatch(true)
                    .SelectedIndex(0)
                    .HtmlAttributes(new { style = "width: 90px; vertical-align: middle;" })
                    .BindTo((SelectList)ViewData["adId_Data"])
                    %>
                    <label style="margin-left: 15px;">Búsqueda:</label>
                    <input type="text" id="txtBusquedaDiagnosticos" style="width: 705px;" />
                </div>
    <%          Html.Telerik().Grid((IEnumerable<GeDoc.catDiagnosticos>)ViewData["TiposDiagnosticos"])
                    .Name("gridtiposDiagnosticos")
                    .DataKeys(keys =>
                    {
                        keys.Add(p => p.diagId);
                    })
                        .ToolBar(commands =>
                    {
                        commands.Custom().Ajax(true).Name("cmdSeleccionarTipo").ButtonType(GridButtonType.ImageAndText)
                        .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                        .Text("Seleccionar");

                    })
                    .DataBinding(dataBinding =>
                    {
                        dataBinding.Ajax()
                            .Select("_SelectEditingTiposDiagnostico", "catTurno", new { adId = 1, TextoBuscado = "+++++" }
                    );
                    })
                 .Localizable("es-AR")
                 .Columns(columns =>
                    {
                        columns.Bound(c => c.DiagnosticoID).Width("22px").Title("Código").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                         .ClientTemplate("<div title='<#= DiagnosticoID #>' style='cursor: pointer;' ondblclick='onSetDiagnosticoTurno(<#= diagId #>, \"<#= Descripcion #>\", 1, \"Asignar\")'><#= DiagnosticoID #></div>");
                        columns.Bound(c => c.Descripcion).Width("150px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<div title='<#= Descripcion #>' style='cursor: pointer;' ondblclick='onSetDiagnosticoTurno(<#= diagId #>, \"<#= Descripcion #>\", 1, \"Asignar\")'><#= Descripcion #></div>");
                        columns.Bound(c => c.td1Descripcion ).Width("130px").Title("Capítulo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<div title='<#= td1Descripcion #>' style='cursor: pointer;' ondblclick='onSetDiagnosticoTurno(<#= diagId #>, \"<#= Descripcion #>\", 1, \"Asignar\")'><#= td1Descripcion #></div>");
                        columns.Bound(c => c.td2Descripcion).Width("130px").Title("Sub Capítulo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<div title='<#= td2Descripcion #>' style='cursor: pointer;' ondblclick='onSetDiagnosticoTurno(<#= diagId #>, \"<#= Descripcion #>\", 1, \"Asignar\")'><#= td2Descripcion #></div>");  
                    })
                .Pageable((paging) =>
                        paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
                .Footer(true)
                .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandTiposDiagnosticos").OnRowSelect("onRowSelectedTiposDiagnosticos").OnDataBinding("onDataBindingTiposDiagnosticos").OnComplete("onCompleteAmbasGrillas"))
                //.Filterable()
                .Selectable()
                .Scrollable(scroll => scroll.Enabled(true).Height(210))
                //.Sortable()
                .Render();
       %>
  </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1024)
       .Height(400)
       .ClientEvents(eventos => eventos.OnActivate("onActivaveWDiagnosticos"))
       .Render();
       %>

<script>
    var wHCMenuOpciones = $("#wHCMenu").data("tWindow");
    if (wHCMenuOpciones.isMaximized == null) {
        wHCMenuOpciones.maximize();
    }

    var _Altura = $("#wHCMenu").height() - 130;
    $("#gridDiagnostico").find(".t-grid-content").css("height", _Altura + "px");
</script>