<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GeDoc.catTurnos>" %>

<script type="text/javascript">
    var _DatosRegistroDiagnostico;
    var _DatosRegistroTiposDiagnosticos;
    var _DatosRegistroPractica;
    var idPracticaSeleccionada;
    var PracticaAccion;

    function onComboBoxChangeGrupoDiagnostico() {
        debugger;
        $("#txtBusquedaDiagnosticos").val("");
        onBuscarDiagnostico();
    }

    function onComboBoxChangeNomencladorPractica() {
        debugger;
        $("#txtBusquedaPractica").val("");
        onBuscarPracticas();
    }

    function RefrescarGridDiagnostico() {
        debugger;
        var estGrid = $("#gridDiagnostico").data("tGrid");       
        estGrid.ajaxRequest();
        _DatosRegistroTiposDiagnosticos = null;
    }

    function RefrescarGridPractica() {
        debugger;
        var estGrid = $("#gridPractica").data("tGrid");      
        estGrid.ajaxRequest();
        _DatosRegistroPractica = null;
        PracticaAccion = "";
    }
  
    function onAceptarCrudTurno() {
        debugger;
        var _Window = $("#wCRUDTurno").data("tWindow");
        var _visita = $("#VisitaCbx").val();
        AbrirWaiting();
        var _Post = "<%=Url.Content("~/catTurno/AsignarVisita")%>";
        $.ajax({
            url: _Post,
            data: { Visita: _visita.substring(0,1), turId: _DatosRegistroTurno["turId"], turControlEmbarazo: $("#turControlEmbarazo").is(":checked") },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                jAlert("Falló la actualización del turno.", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaiting();
                if (data.IsValid != null) {
                  //  _Window.close();
                    $(".t-overlay").fadeOut();
                }
            }
        });
    }

    function onCancelarCrudTurno() {
        debugger;
        var _Window2 = $("#wCRUDTurno").data("tWindow");
        _Window2.close();
    }

    function onCommandDiagnostico(e) {
        debugger;
        var _ventanaTiposDiagnosticos = $("#WtiposDiagnosticos").data("tWindow");
        if (e.name === "cmdAgregarDiagnostico") {
            if ($(this).data("tGrid").total >= 5) {
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

    function onCommandTiposDiagnosticos(e) {
        debugger;
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
            data: { diagId: diagId, turId: _DatosRegistroTurno["turId"], pAccion: Accion },
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
            data: { pracId: pracId, turId: _DatosRegistroTurno["turId"], pracCantidad: CantidadPracticas, pAccion: PracticaAccion },
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
        debugger;
        args.data = $.extend(args.data, { turId: _DatosRegistroTurno["turId"] });
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

    function onRowSelectedDiagnostico(e) {
        debugger;
        var row = e.row;
        var grid = $("#gridDiagnostico").data("tGrid");
        var dataItem = grid.dataItem(row);
        _DatosRegistroDiagnostico = dataItem;
    }

    function onRowSelectedPractica(e) {
        debugger;
        var row = e.row;
        var grid = $("#gridPractica").data("tGrid");
        var dataItem = grid.dataItem(row);
        _DatosRegistroPractica = dataItem;
    }

    function onRowSelectedTiposDiagnosticos(e) {
        debugger;
        _DatosRegistroTiposDiagnosticos = $("#gridtiposDiagnosticos").data("tGrid").dataItem(e.row);
    }

    function onRowSelectedTiposPracticas(e) {
        _DatosRegistroPractica = $("#gridtiposPracticas").data("tGrid").dataItem(e.row);
    }

    function ImprimirTurno(e) {
        debugger;
        if (_profId < 0) {
            _conId = _profId * -1;
        }
        var _Parametros = new Array(new Array(_espId, "espId"), new Array(_conId, "conId"), new Array(fecha, "turFecha"), new Array(_profId, "perId"));
        InvocarReporte("rptTurnoAnexo2", _Parametros);
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
    $('#turControlEmbarazo').click(function () {
        if ($(this).is(':checked')) {
            onAceptarCrudTurno();
        } else {
            onAceptarCrudTurno();
        }
    });
  function  onComboBoxChangePrimUlterior () {
        debugger;
        onAceptarCrudTurno();
    };
</script>

<%
    Html.Telerik().TabStrip()
        .Name("tabTurno")
        .HtmlAttributes(new {style = "padding: 3px 0px 0px 0px; background: transparent; border: 0px;"})
        .Items(tabstrip =>
        {
            //Detalles del turno
            tabstrip.Add()
                .Text("Datos del Turno")
                .ContentHtmlAttributes(new {style = "padding: 0px;"})
                .Content(() =>
                { %>
                <div id="DetalleTurno" style="width: auto; height: 100%;">
                        <div class="editor-label" style="vertical-align: middle;">                                       
                            <label id="Label1" style="font-size: 12px; font-weight: normal">Nro. H.C.:</label>
                            <label id="lblnroHC" class="label-consulta" style="width: 100px"><%=Model.nroHC %></label>
                            <label id="Label4" style="font-size: 12px; font-weight: normal; margin-left: 150px;">Sexo:</label>
                            <label id="lbltipoIdSexo" class="label-consulta" style="width: 70px;"><%=Model.tipoSexoDescripcion %></label>
                            <label id="Label2" style="font-size: 12px; font-weight: normal; margin-left: 130px;">Estado del Turno:</label>
                            <label id="EstadoTurno" class="label-consulta" style="width: 180px;"><%=Model.tipoDescripcion %></label>
                            <label id="Label3" style="font-size: 12px; font-weight: normal; margin-left: 210px;">Tipo de Admision:</label>
                            <label id="tipoAdmisionDescripcion" class="label-consulta" style="width: 180px;"><%=Model.tipoAdmisionDescripcion %></label>
                            </div>
                        <br>
                        <div class="editor-label" style="vertical-align: middle;">
                            <label id="Label5" style="font-size: 12px; font-weight: normal">Fecha del turno:</label>
                            <label id="turFechadelTurno" class="label-consulta" style="width: 150px"><%=Model.turFecha.ToString("dd/MM/yyyy hh:mm") %></label>
                        </div>
                        <div class="editor-label" style="vertical-align: middle;"> 
                        <br>
                            <label id="Label7" style="font-size: 12px; font-weight: normal">Tipo de Doc. del Pac:</label>
                            <label id="PacienteDescripcionTipoDocumento" class="label-consulta" style="width: 70px;"><%=Model.PacienteDescripcionTipoDocumento %></label>
                            <label id="Label9" style="font-size: 12px; font-weight: normal; margin-left: 130px;">Nº Doc. del Paciente:</label>
                            <label id="pacNumeroDocumento" class="label-consulta" style="width: 150px;"><%=Model.pacNumeroDocumento %></label>
                            <label id="Label11" style="font-size: 12px; font-weight: normal; margin-left: 200px;">Apellido y Nombre del Paciente:</label>
                            <label id="pacNombre" class="label-consulta" style="width: 290px;"><%= (Model.pacApellido + " " + Model.pacNombre) %></label>
                        </div>
                        <br>
                            <div class="editor-label" style="vertical-align: middle;"> 
                            <label id="Label17" style="font-size: 12px; font-weight: normal">Especialidad:</label>
                            <label id="turEspecialidadNombre" class="label-consulta" style="width: 135px"><%=Model.turEspecialidadNombre %></label>
                            <label id="Label19" style="font-size: 12px; font-weight: normal; margin-left: 190px;">Profesional:</label>
                            <label id="profApellidoyNombre" class="label-consulta" style="width: 270px;"><%=Model.profApellidoyNombre %></label>
                                       
                                       
                            <label id="Label23" style="font-size: 12px; font-weight: normal; margin-left: 290px;">Centro de Salud:</label>
                            <label id="_csNombre" class="label-consulta" style="width: 330px;"><%=Model._csNombre %></label>
                        </div>
                        <br>
                        <div class="editor-label" style="vertical-align: middle; display: <%=Model.pacId == null ? "none" : "inline-block" %>"> 
                            <label id="Label6" style="font-size: 12px; font-weight: normal; vertical-align: super;">Visita:</label>
                            <%= Html.Telerik().DropDownList()
                                    .Name("VisitaCbx")
                                    .ClientEvents(events => events.OnChange("onComboBoxChangePrimUlterior"))
                                    .Items(item =>
                                    {
                                        item.Add().Text("Primera vez").Value("P").Selected(ViewBag.PrimeraVez);
                                        item.Add().Text("Ulterior").Value("U").Selected(ViewBag.Ulterior);
                                    }) %>
                        </div>
                        <div class="editor-label" style="vertical-align: middle; display: <%=Model.pacId == null ? "none" : "inline-block" %>">
                            <label id="Label8" style="font-size: 12px; font-weight: normal; vertical-align: super;">Control de Embarazo:</label>
                            <input type="checkbox" id="turControlEmbarazo" <%=Model.turControlEmbarazo != null ? Model.turControlEmbarazo == true ? "checked" : "" : "" %> />
                        </div>
                    
                    <div style="position: relative; top: 10px; width: 100px; left: 83%;">
                        <table>
                            <tr>
                                <td style="border: none; display: <%=Model.pacId == null ? "none" : "block" %>">
                                    <button class="t-button t-grid-update" onclick="onAceptarCrudTurno();">Aceptar</button>
                                </td>
                                <td style="border: none;">
                                    <button class="t-button t-grid-cancel" onclick="onCancelarCrudTurno();">Cancelar</button>
                                </td>
                            </tr>
                        </table>
                        </div>
                        <div class="BordeRedondeado" style="vertical-align: middle; padding: 10px; margin: 10px; text-align: center; color: #8a6d3b; background-color: #fcf8e3; border-color: #faebcc; display: <%=Model.tipoAdmisionCodigo == "AT" ? "none" : "block" %>">
                            <label style="font-weight: bold;">¡ATENCIÓN! </label>
                            Para agregar Diagnósticos, Prácticas y/o Evolución, el turno debe estar Atendido por Profesional
                        </div>
                    
                    
                    

                </div>
            <% });
            //Diagnosticos
            tabstrip.Add()
                .Text("Diagnóstico")
                .ContentHtmlAttributes(new {style = "padding: 0px;"})
                .Visible(Model.EstadoCodigo == "AD" && Model.tipoAdmisionCodigo == "AT")
                .Content(() =>
                {
            %>
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
                                .Select("_SelectEditingTurnosDiagnosticos", "catTurno", new { turId = 0 });
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
                        })
                        .Pageable((paging) =>
                            paging.Enabled(true)
                                .PageSize(((int) Session["FilasPorPagina"])))
                        .Footer(true)
                        .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandDiagnostico").OnRowSelect("onRowSelectedDiagnostico").OnDataBinding("onDataBindingAmbasGrillas"))
                        .Filterable()
                        .Selectable()
                        .Scrollable(scroll => scroll.Enabled(true).Height(390))
                        .Sortable()
                        .Render();
                %>
            </div>
<%
                });                           
            tabstrip.Add()
                .Text("Prácticas")
                .ContentHtmlAttributes(new {style = "padding: 0px;"})
                .Visible(Model.EstadoCodigo == "AD" && Model.tipoAdmisionCodigo == "AT")
                .Content(() =>
                {
%>
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
                            dataBinding.Ajax().Select("_SelectEditingTurnosPracticas", "catTurno", new { turId = 0 });
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
                        })
                        .Pageable((paging) =>
                            paging.Enabled(true)
                                .PageSize(((int) Session["FilasPorPagina"])))
                        .Footer(true)
                        .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandPractica").OnRowSelect("onRowSelectedPractica").OnDataBinding("onDataBindingAmbasGrillas"))
                        .Filterable()
                        .Selectable()
                        .Scrollable(scroll => scroll.Enabled(true).Height(390))
                        .Sortable()
                        .Render();
                %></div><%
                });
                tabstrip.Add()
                    .Text("Evolución")
                    .ContentHtmlAttributes(new {style = "padding: 0px;"})
                    .Visible(Model.EstadoCodigo == "AD" && Model.tipoAdmisionCodigo == "AT")
                    .Content(() =>
                    {
                        Html.RenderPartial("CRUDTurnoTabEvolucion");
                    });

                tabstrip.Add()
                .Text("Cartilla Básica")
                .ContentHtmlAttributes(new { style = "padding: 0px;" })
                .Visible(Model.EstadoCodigo == "AD" && Model.tipoAdmisionCodigo == "AT")
                .Content(() =>
                {
                    Html.RenderPartial("CRUDTurnoTabCartilla");
                });    
        })
        .SelectedIndex(0)
        .Render();
       %>


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
