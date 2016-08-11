<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%@ Import Namespace="GeDoc.Models" %>

<% Html.RenderPartial("CambiaAdmision"); %>
<% Html.RenderPartial("RepetirTurno"); %>
<% Html.RenderPartial("ConsultaPaciente"); %>
<% Html.RenderPartial("FiltroListadoPacientes"); %>
<% Html.RenderPartial("WaitingCRUDModel", "wtTurnosAmbulatorios"); %>
<%
    bool EsMedico = ((Session["Usuario"] as sisUsuario).perId != null || (Session["Usuario"] as sisUsuario).conId != null);
    ViewBag.EsCentroDeAtencion = false;
    int MedicoLogueado = 0;
   
    if ((Session["Usuario"] as sisUsuario).perId != null)
    {
        MedicoLogueado = (int)(Session["Usuario"] as sisUsuario).perId;
    }
    else
    {
        if ((Session["Usuario"] as sisUsuario).conId != null)
        {
            MedicoLogueado = (int)(Session["Usuario"] as sisUsuario).conId * -1;
        }
    }
%>
<style>
    
    .TurnoStatusValue {
        vertical-align: super;
        margin-left: 5px;
        float: right;
    }

    .TurnoStatusImage {
        width: 18px;
    }

    #TurnosStatusBar {
        text-align: center;
        line-height: normal;
    }

    .TurnoStatus {
        display: inline-block;
        margin-right: 10%;
    }

</style>

<script type="text/javascript" >
    var _DatosRegistroTurno;
    var ConsultorioSeleccionado = { csscId: 0, csscNombre: "" };
    var SalaSeleccionada = { cssId: 0, cssNombre: "" };
    var EsMedico = "<%= (EsMedico ? "SI" : "NO") %>";
    var MedicoLogueado = "<%=MedicoLogueado %>";

    function wCRUDTurno_OnClose(e) {
        if ($("#evoNewDate").data("intervalId")) {
            clearInterval($("#evoNewDate").data("intervalId"));
        }
    }

    function Refrescar() {
        debugger;
        var grid = $("#gridTurnos").data("tGrid");

        grid.ajaxRequest();
    }
    function checkForOtherTurns() {
        p_turId = _DatosRegistroTurno.turId;
        p_pacId = _DatosRegistro.pacId;
        $.post("<%=Url.Content("~/catTurno/patientAlreadyHasATurnOnThatDay")%>", { _pacId: p_pacId, _turId: p_turId }, function (d) {
        debugger;
        if (d.length == 0) {
            AsignarPaciente(_DatosRegistro["pacId"], _DatosRegistro.pacApellido + ", " + _DatosRegistro.pacNombre, 0);
        } else {
            var msg = "<span style='margin-bottom:5px'>El paciente <b>" + _DatosRegistro.pacApellido + ", " + _DatosRegistro.pacNombre + "</b> ya posee otros turnos para el día de la fecha:</span>";
            msg += "<div class='ot-anotherTurns'><table><thead><th>Hora</th><th>Especialidad</th><th>Medico</th><th>Estado</th></thead>";
            msg += "<tbody>";
            $(d).each(function () {
                msg += "<tr>";
                msg += "<td>" + this.Hora + "</td>";
                msg += "<td>" + this.Especialidad + "</td>";
                msg += "<td>" + this.Profesional + "</td>";
                msg += "<td>" + this.Estado + "</td>";
            });
            msg += "</tbody></table></div>";
            msg += "<span style='margin-top: 5px;font-weight: bold;display: block;text-align: center;font-size: 13px;'>¿Desea asignarlo de todas formas?</span>";
            jConfirm(msg, "¡Atención!", function (r) {
                if (r) {
                    AsignarPaciente(_DatosRegistro["pacId"], _DatosRegistro.pacApellido + ", " + _DatosRegistro.pacNombre, 0);
                } else {
                    return false;
                }
            });
        }
    });
    }
    //function CargarPie() {
    //    //Ahora cargo el pie, no me gusta la forma pero...\\
    //    var grid2 = $("#GridPieTurnos").data("tGrid");
    //    grid2.rebind();
    //}

    function onRowSelectedTurno(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);
        _DatosRegistroTurno = dataItem;
    }
    function onComboBoxChangeProfesional() {
        debugger;
        Refrescar();
    }
    function onComboBoxChangeEspecialidad() {
        AbrirWaitingCRUD("wtTurnosAmbulatorios");
        var _Especialidad = $("#cbxEspecialidad").data("tComboBox");
        debugger;
        if (EsMedico === "SI") {
            Refrescar();
        } else {
            debugger;
            var _Profesionales = $("#cbxProfesional").data("tComboBox");
            $.post("<%=Url.Content("~/catTurno/BindingProfesionales")%>", { espId: _Especialidad.value(), profesionalId: -1 }, function (data) {
                CerrarWaitingCRUD("wtTurnosAmbulatorios");
                if (data != null) {
                    _Profesionales.dataBind(data.data);
                    if (data.total < 1) {
                        _Profesionales.select(0);
                        _Profesionales.disable();
                        $("#cbxEspecialidad").focus(0);
                        Refrescar();
                    }
                    else {
                        _Profesionales.enable();
                        _Profesionales.select(0);
                        $("#cbxProfesional").focus(0);
                        Refrescar();
                    }
                }
            });
        }
    }

    function onDataBindingTurnos(args) {
        var _especialidadId = $("#cbxEspecialidad").data("tComboBox");
        var _profesionalId = $("#cbxProfesional").data("tComboBox");
        var _fecha = $("#Agenda").val();
        var _espId = "0";
        var _profId = "0";
        debugger;
        if (_especialidadId.value() != "") {
            _espId = _especialidadId.value();
        }
        if (EsMedico === "SI") {
            _profId = MedicoLogueado;
        } else {
            if (_profesionalId.value() != "") {
                _profId = _profesionalId.value();
            }
        }
        AbrirWaitingCRUD("wtTurnosAmbulatorios");
        args.data = $.extend(args.data, { especialidadId: _espId, profesionalId: _profId, fecha: _fecha });
    }

    function onCommandTurnos(e) {
        debugger;
        switch (e.name) {
        case "cmdAsignarPaciente":
            if (e.dataItem["pacId"] != null || e.dataItem.tipoId == 169) {
                jAlert("No se puede Asignar un Paciente, este turno NO está disponible", "¡Error!");
                return;
            }
            _DatosRegistroTurno = e.dataItem;
            var _AsignaPaciente = $("#WPacientes").data("tWindow");
            _AsignaPaciente.center().open();
            $("#WPacientes").css({ 'height': 660, 'width': 1034 });
            $("#WPacientes").find('div.t-window-content').css({ 'height': 630, 'width': 1024 });
            _AsignaPaciente.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
            var _Post = "<%=Url.Content("~/catTurno/getBusquedaPaciente")%>";
            $.ajax({
                url: _Post,
                data: { turId: _DatosRegistroTurno["turId"] },
                type: "POST",
                error: function (xhr, ajaxOptions, thrownError) {
                    jAlert("No se puede Asignar un Paciente, este turno NO está disponible", "¡Error!");
                    $("#popup_container").focus();
                    $("#popup_ok").focus();
                },
                success: function (data) {
                    if (data) {
                        _AsignaPaciente.content(data);
                        var txtBus = $("#txtBusquedaPaciente")[0];
                        txtBus.focus();
                    } else {
                        jAlert("No se puede Asignar un Paciente, este turno NO está disponible", "Error...");
                        return;
                    }
                }
            });

            break;
        case "cmdCambiarAdmision":
            debugger;
            if (e.dataItem["pacId"] == null) {
                jAlert("No se puede Cambiar el tipo de Admisión al turno, NO tiene asignado un Paciente", "Error...");
                return;
            }
            
            if (EsMedico === "NO") {
                if (e.dataItem["csId"] != 167) {
                    if (e.dataItem["tipoAdmisionCodigo"] !== null) {
                        jAlert("Este turno ya está Admisionado.", "¡Error!");
                        return;
                    }
                }
            } else {
               
                 if (e.dataItem["csId"] != 167) {
                    //if (e.dataItem["EstadoCodigo"] !== "AD" && e.dataItem["EstadoCodigo"] !== "OT") {
                    if (e.dataItem["tipoAdmisionCodigo"] !== "AT") {
                        jAlert("No puede cambiar el tipo de Admisión, si el paciente aún no fué llamado.", "¡Error!");
                        return;
                    }
                }
            }

            if (e.dataItem["EstadoCodigo"] === "OT") {
                //Es importante que la fecha sea la del servidor y no la del cliente no usar ni jQuery.now() ni nigún
                //método que tome la fecha del cliente, por que si el cliente tiene mal la fecha de su PC, genera problemas
                if (("<%= DateTime.Now.Date.ToString("dd/MM/yyyy")%>") < e.dataItem["turFecha"].toLocaleString("es-ar", { day: "2-digit", month: "2-digit", year: "numeric" })) {
                    jAlert("No se puede cambiar el tipo de admisión a un turno a futuro.", "¡Error!");
                    return;
                }
            }

            _DatosRegistroTurno = e.dataItem;
            var cambiaAdmision = $("#wCambioDeAdmision").data("tWindow");
            cambiaAdmision.title("Admisión del Paciente: " + e.dataItem["pacNombre"]).center().open();
            $("#wCambioDeAdmision").css({ "height": 160, "width": 534 });
            $("#wCambioDeAdmision").find("div.t-window-content").css({ "height": 100, "width": 524 });

            break;
        case "cmdBloquearTurno":
            _DatosRegistroTurno = e.dataItem;
            if (_DatosRegistroTurno["pacId"] == null) {
                if (_DatosRegistroTurno["EstadoCodigo"] != "BL") {
                    motivoBloqueo();
                }
                else {
                    cambiarEstadoADisponible();
                    return;
                }
            }
            else {
                jAlert("No se puede Bloquear el turno, tiene asignado un Paciente", "Error...");
                return;
            }
            break;
        case "cmdEliminarTurno":
            _DatosRegistroTurno = e.dataItem;
            if (_DatosRegistroTurno["EstadoCodigo"] != "DI" && _DatosRegistroTurno["EstadoCodigo"] != "BL") {
                jConfirm("¿Confirma que quiere desasignar este turno?", "Desasignando...", function (r) {
                    if (r) {
                        cambiarEstadoADisponible();
                    }
                });
            }
            else {
                jAlert("No hay ningún paciente asignado", "Error...");
                return;
            }
            break;
        case "cmdVerFichaPaciente":
            _DatosRegistroTurno = e.dataItem;
            if (_DatosRegistroTurno["EstadoCodigo"] != "BL") {
                if (_DatosRegistroTurno["pacId"] != null) {
                    AbrirWaitingCRUD("wtTurnosAmbulatorios");
                    var _Post = "<%=Url.Content("~/catPaciente/ViewDetails")%>";
                    $.ajax({
                        url: _Post,
                        data: { pacienteId: _DatosRegistroTurno["pacId"] },
                        type: "POST",
                        error: function (xhr, ajaxOptions, thrownError) {
                            jAlert("No se puede Asignar un Paciente, este turno NO está disponible", "¡Error!");
                            $("#popup_container").focus();
                            $("#popup_ok").focus();
                        },
                        success: function (data) {
                            CerrarWaitingCRUD("wtTurnosAmbulatorios");
                            if (data.InfoPaciente != null) {
                                VerLegajoPaciente(data.InfoPaciente);
                            }
                        }
                    });
                }
                else {
                    jAlert("Debe asignar el Paciente", "Error...");
                    return;
                }
            }
            else {
                jAlert("Este Turno esta bloqueado", "Error...");
                return;
            }
            break;
        case "cmdRepetirTurno":
            _DatosRegistroTurno = e.dataItem;
            if (_DatosRegistroTurno["pacId"] == null) {
                jAlert("El Turno seleccionado debe ser Otorgado a un Paciente para poder Repetirlo.", "Error...");
                return;
            }
            var wRepetir = $("#wRepetirTurno").data("tWindow");
            wRepetir.title("Repetir Turno del Paciente: " + _DatosRegistroTurno["pacNombre"]).center().open();
            break;
        case "cmdMasDetalle":
            _DatosRegistroTurno = e.dataItem;
            onVerDetalleDelTurno();
            break;
        case "cmdRellamado":
            debugger;
            if (e.dataItem["tipoAdmisionCodigo"] === "AD" || e.dataItem["tipoAdmisionCodigo"] === "AT") {
                jAlert("No puede volver a llamar a este Paciente", "¡Error!");
                return;
            }

            //Es importante que la fecha sea la del servidor y no la del cliente no usar ni jQuery.now() ni nigún
            //método que tome la fecha del cliente, por que si el cliente tiene mal la fecha de su PC, genera problemas
            if (("<%= DateTime.Now.Date.ToString("dd/MM/yyyy")%>") != e.dataItem["turFecha"].toLocaleString("es-ar", { day: "2-digit", month: "2-digit", year: "numeric" })) {
                jAlert("No se puede volver a llamar a Pacientes con turnos distintos al día de hoy.", "¡Error!");
                return;
            }

            LlamarProximoPaciente(e.dataItem["turId"]);
            break;
        case "cmdHistoriaClinica":
            _DatosRegistroTurno = e.dataItem;
            if (_DatosRegistroTurno["pacId"] == null) {
                jAlert("Debe asignar un Paciente para poder ver la Historia Clínica.", "Error...");
                return;
            }
            AbrirWaitingCRUD();
            debugger;
            var _Post = "<%=Url.Content("~/HistoriaClinica/hcPaciente")%>";
            $.ajax({
                type: "POST",
                url: _Post,
                //data: { IdPaciente: $("#txtHCNumeroDocumento").val() * -1, tipoDocumento: $("#tipoIdTipoDocumento_Lista").val() },
                error: function () {
                    CerrarWaitingCRUD();
                },
                success: function (respuesta) {
                    CerrarWaitingCRUD();
                    var wHistoriaClinica = $("#wHistoriaClinica").data("tWindow");
                    wHistoriaClinica.content(respuesta);
                    //$("#divHCCargaDeDocumento").hide();
                    $("#txtHCNumeroDocumento").val(_DatosRegistroTurno.pacNumeroDocumento);
                    onBuscarPacienteHC();
                    if (wHistoriaClinica.isMaximized != null) {
                        wHistoriaClinica.center().open();
                    } else {
                        wHistoriaClinica.maximize().center().open();
                    }
                }
            });
            break;
        }
        e.preventDefault();
        e.stopPropagation();
    }
    
    function onVerDetalleDelTurno() {
        debugger;
        var _wCRUDTurno = $("#wCRUDTurno").data("tWindow");
        _wCRUDTurno.center().open();
        $("#wCRUDTurno").css({ 'height': 400, 'width': 1134 });
        $("#wCRUDTurno").find('div.t-window-content').css({ 'height': "auto", 'width': "auto" });
        debugger;
        _wCRUDTurno.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        var _Post = "<%=Url.Content("~/catTurno/getTurno")%>";
        debugger;
        $.ajax({
            url: _Post,
            data: { turId: _DatosRegistroTurno["turId"] },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                jAlert("Falló el acceso al servidor", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                debugger;
                if (data != null) {
                    _wCRUDTurno.content(data);
                } else {
                    jAlert("No se pudo cargar el detalle del turno", "Error...");
                    return;
                }
            }
        });
    }

    function cambiarEstadoADisponible() {
        debugger;
        AbrirWaitingCRUD("wtTurnosAmbulatorios");
        var _Post = "<%=Url.Content("~/catTurno/setCambiarEstadoTurno")%>";
        $.ajax({
            url: _Post,
            data: { turId: _DatosRegistroTurno["turId"], CodigoEstado: "DI" },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                jAlert("No se pudo dejar disponible este turno.", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaitingCRUD("wtTurnosAmbulatorios");
                if (!data.IsValid) {
                    jAlert(data.Mensaje, "¡Atención!");
                    $("#popup_container").focus();
                    $("#popup_ok").focus();
                } else {                    
                    Refrescar();
                }
            }
        });
    }

    function VerLegajoPaciente(DatosPaciente) {
        debugger;
        var detailWindow = $("#wConsultaPaciente").data("tWindow");
        var _Paciente = DatosPaciente;
        var _Foto = "";

        if (_Paciente.perFoto == null) {
            if (_Paciente.tipoIdSexoTexto == 'Femenino') {
                _Foto = GetPathApp('Content/General/Mujer.jpg');
            }
            else {
                _Foto = GetPathApp('Content/General/Hombre.jpg');
            }
        }

        $("#lblnroHC").text(_Paciente.nroHC);
        $("#lbltipoIdSexo").text(_Paciente.tipoSexoNombre);
        $("#lblperTipoDoc").text(_Paciente.tipoDescDocumento);
        $('#imgFoto').attr('src', _Foto);
        $("#lblApellidoyNombre").text(_Paciente.pacApellido + ' ' + _Paciente.pacNombre);
        $("#lblperPadron").text(_Paciente.perPadron);
        $("#lblperDNI").text(_Paciente.pacNumeroDocumento);
        $("#lblperCUIL").text(_Paciente.pacCUIL);
        $("#lblperFechaNacimiento").text(_Paciente.pacFechaNacimientoTexto);
        $("#lblperEdad").text(_Paciente.pacEdad);
        $("#lblprovNombre").text(_Paciente.provNombre);
        $("#lblpaisNombre").text(_Paciente.paisNombre);
        $("#lblProvincia").text(_Paciente.provNombre);
        $("#lblEstadoCivil2").text(_Paciente.DescEstadoCivil);
        $("#lblperTelefono").text(_Paciente.pacTelefonoCasa);
        $("#lblperCelular").text(_Paciente.pacTelefonoCelular);

        $("#lblosNombre").text(_Paciente.osNombre);
        $("#lblperDomicilio").text(_Paciente.pacCalle + ' Nº ' + _Paciente.pacCalleNumero + ' Dpto ' + _Paciente.depNombre + ' Localidad ' + _Paciente.locNombre);
        $("#lblRecibeSMS").text(_Paciente.pacNotificarXSMS ? 'SI' : 'NO');
        $("#lblperEmail").text(_Paciente.pacMail);
        $(".t-overlay").fadeIn(function() {
            detailWindow.center().open();
        });        
    }

    function onCompleteTurnos(e) {
        debugger;
        if (e.name === "dataBinding") {
            CerrarWaitingCRUD("wtTurnosAmbulatorios");
            $("#divProcesandowtTurnosAmbulatorios").css("display", "none");
            //CargarPie();
            refreshStatusBar(e.response.data);
        }
    }


    function refreshStatusBar(data) {
        debugger;
        if (typeof data === "undefined" || data.length === 0)
            return;
        var rowStatesCount = {};
        data.map(function(e) {
            if (e.tipoId in rowStatesCount) {
                rowStatesCount[e.tipoId]++;
            } else {
                rowStatesCount[e.tipoId] = 1;
            }
        });
        buildStatusBar(rowStatesCount);
    }

    function buildStatusBar(data) {
        debugger;
        if ($("#TurnosStatusBar").length !== 0) {
            $("#TurnosStatusBar").remove();
        }
        var imgUrl = "/GeDoc/Content/Estados/";
        var statuses = { 168: "Celeste.png", 169: "Rojo.png", 170: "Amarillo.png", 171: "Verde.png" };
        $("#gridTurnos .t-grid-pager.t-grid-bottom").append($("<div>", { id: "TurnosStatusBar" }));
        for (status in statuses) {
            $("#TurnosStatusBar").append($("<div>", { class: "TurnoStatus" }));
            $(".TurnoStatus:last-child").append($("<img>", { class: "TurnoStatusImage", src: imgUrl + statuses[status] }));
            $(".TurnoStatus:last-child").append($("<span>", { class: "TurnoStatusValue", text: data[status] || 0 }));
        }
    }

    function onChangeDatePicker(e) {
        debugger;
        Refrescar();
    }
    function onErrorTurno(e) {
        debugger;
        //the current XMLHttpRequest object
        var xhr = e.XMLHttpRequest;
        //the text status of the error - "timeout", "error" etc.
        var status = e.textStatus;

        if (status == "error") {
            _Error = true;
        }
        else {
            _Error = false;
        }
    }
    function Imprimir(e) {
        debugger;
        _conId = 0;
        if (_profId < 0) {
            _conId = _profId * -1;
            _profId = 0;
        }
        var _wFiltroImpP = $('#WFiltroListadoPacientes').data("tWindow");
        _wFiltroImpP.center().open();
        onCommandEdit(e);
    }

    function verDetalleTurno() {
        debugger;
    }

    function gridTurnos_OnDataBound(e) {
        debugger;
        var grid = $(e.target).data("tGrid");
        $(grid.data).each(function (i) {
            if (this.EstadoCodigo === "BL") {
                var row = $(e.target).find(".t-grid-content tr")[i];
                $(row).find(".t-grid-cmdBloquearTurno>span").css("background-position", "-64px -240px").prop("title", "Desbloquear Turno");
            }
        });
    }

    function motivoBloqueo() {
        debugger;
        $.post("<%= Url.Content("~/catTurno/getMotivoBloqueoCombo") %>", function (d) {
            $("#wMotivoBloqueo div.t-window-content").html(d);
            $("#wMotivoBloqueo").data("tWindow").center().open();
        });
    }

    function gridTurnos_OnLoad(e) {
        debugger;
        console.log(e);
    }

    function LlamarProximoPaciente(turId) {
        debugger;
        var _especialidadId = $("#cbxEspecialidad").data("tComboBox");
        var _fecha = $("#Agenda").val();
        var _espId = "0";
        var grid = $("#gridTurnos").data("tGrid");
        debugger;

        if (grid.total === 0) {
            jAlert("No hay pacientes sin atender.", "¡Error!");
            return;
        }

        if (_especialidadId.value() != "") {
            _espId = _especialidadId.value();
        }

        AbrirWaitingCRUD("wtTurnosAmbulatorios");
        var _Post = "<%=Url.Content("~/catTurno/getProximoPaciente")%>";
        $.ajax({
            url: _Post,
            data: { especialidadId: _espId, profesionalId: MedicoLogueado, fecha: _fecha, Consultorio: ConsultorioSeleccionado.csscId, turId: turId },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaitingCRUD("wtTurnosAmbulatorios");
                jAlert("Falló el llamado del próximo paciente", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaitingCRUD("wtTurnosAmbulatorios");
                if (data.Mensaje === "OK") {
                    //Busco que fila es la seleccionada\\
                    var Item;
                    $.each(grid.data, function (index, datos) {
                        Item = datos;
                        if (Item.turId == data.turId) {
                            return false;
                        }
                    });
                    _DatosRegistroTurno = Item;
                    onVerDetalleDelTurno();
                    Refrescar();
                } else {
                    jAlert("Falló el llamado del próximo paciente. " + data.Mensaje, "¡Error!");
                    return;
                }
            }
        });
    }

    function onActivate_wReAgendar() {
        debugger;
        $(".t-overlay").css("display", "block");
        $("#divProcesandowtTurnosAmbulatorios").css("display", "none");
        var grid = $("#gridTurnosRA").data("tGrid");
        grid.ajaxRequest();

        RecargarProfesionales();
    }

    function onLoad_wReAgendar() {
        debugger;
    }

    function onActivate_wSeleccionDeConsultorio() {
        debugger;
        ConsultorioSeleccionado.csscId = 0;
        ConsultorioSeleccionado.csscNombre = "";
        SalaSeleccionada.cssId = 0;
        SalaSeleccionada.cssNombre = "";
        $(".t-overlay").css("display", "block");
        $("#divProcesandowtTurnosAmbulatorios").css("display", "none");
    }

    function onLoad_wSeleccionDeConsultorio() {
        debugger;
        if (EsMedico === "SI") {
            var wSelConsultorios = $("#wSeleccionDeConsultorio").data("tWindow");
            wSelConsultorios.center().open();
        }
    }

    function onActivate_wHistoriaClinica() {
        debugger;
        $(".t-overlay").css("display", "block");
        $("#divProcesandowtTurnosAmbulatorios").css("display", "none");
    }

    function onEjecutaAccionCambiaPrioridad(_turId, _Prioridad, _Paciente, TipoAdmision) {
        debugger;
        if (_Paciente === "") {
            jAlert("El Turno seleccionado debe ser Otorgado a un Paciente para poder cambiar de prioridad de Atención.", "Error...");
            return;
        }
        if (TipoAdmision !== "AD") {
            jAlert("Para cambiar la Prioridad de Atención, el turno debe estar Admisionado", "¡Error!");
            return;
        }
        var NewPrioridad = _Prioridad == "Normal" ? "Alta" : "Normal";
        jConfirm('¿Confirma cambiar prioridad de atención del paciente "' + _Paciente + '" ?', "Cambiar Prioridad", function (r) {
            if (r) {
                var _Post = "<%=Url.Content("~/catTurno/setCambiaPrioridad")%>";
                $.ajax({
                    url: _Post,
                    data: { turId: _turId, Prioridad: NewPrioridad },
                    type: "POST",
                    error: function (xhr, ajaxOptions, thrownError) {
                        jAlert("No se puede cambiar la prioridad de este Paciente", "¡Error!");
                        $("#popup_container").focus();
                        $("#popup_ok").focus();
                    },
                    success: function (data) {
                        if (data) {
                            Refrescar();
                        } else {
                            jAlert("No se puede cambiar la prioridad de este Paciente", "¡Error!");
                            return;
                        }
                    }
                });
            }
        });
    }

    function onReAgendar() {
        debugger;
        var wAgendar = $("#wReAgendar").data("tWindow");
        wAgendar.center().open();
    }

</script>
<!-- Turno -->  
<div style="overflow: hidden" >
<% Html.Telerik().Grid((IEnumerable<GeDoc.catTurnos>)ViewData["catTurnos"])
       .Name("gridTurnos")
        .DataKeys(keys =>
        {
            keys.Add(p => p.turId);
        })
     .Localizable("es-AR")
     .ToolBar(commands =>
        {
            commands.Template(grid =>
            {                             
                %>
                <div class="t-button" onclick="Imprimir();" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/Printer.png" height="18" alt="Imprimir carátula" style="vertical-align: middle;" />
                    Imprimir
                </div>
                <% if (EsMedico)
                   { %>
                <div class="t-button" onclick="LlamarProximoPaciente(-1);" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/Proximo.png" height="18" title="Llamar el próximo Paciente a Atender" style="vertical-align: middle;" />
                    Próximo
                </div>
                <% } %>
                <div class="t-button" onclick="onReAgendar();" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/Crud/ReAgendar.png" height="18" title="Re-Agendar pacientes" style="vertical-align: middle;" />
                    Re-Agendar
                </div>
                 <label style="margin-left: 5px;">Fecha:</label>
                 <%=                  
                 Html.Telerik().DatePicker() 
                    .Name("Agenda")
                    .ClientEvents(events => events.OnChange("onChangeDatePicker"))
                    .Value(DateTime.Now)
                    .HtmlAttributes(new { style = "width: 97px; vertical-align: middle; display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Seleccionar Fecha") })
                 %>
                <label style="margin-left: 15px;">Especialidad:</label>
                <%=
                     Html.Telerik().ComboBox()
                    .Name("cbxEspecialidad")                
                    .DropDownHtmlAttributes(new { style = "width:Auto;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)                   
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.Contains);
                    })
                    .HighlightFirstMatch(true)
                    .Enable(((SelectList)ViewData["espId_Data"]).Count() > 1)
                    .ClientEvents(events => events.OnChange("onComboBoxChangeEspecialidad"))
                    .SelectedIndex(0)
                    .HtmlAttributes(new { style = "width: 300px; vertical-align: middle; display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Seleccionar Especialidad") })   
                    .BindTo((SelectList)ViewData["espId_Data"])
                    %>
                <% if (!EsMedico)
                   { %>
                    <label style="margin-left: 5px;">Profesional:</label>        
                <%= Html.Telerik().ComboBox()
                    .Name("cbxProfesional")
                    .DropDownHtmlAttributes(new { style = "width:Auto;" })
                    .OpenOnFocus(true)
                    .AutoFill(true)
                    .Filterable(filtering =>
                    {
                        filtering.FilterMode(AutoCompleteFilterMode.Contains);
                    })
                    .HighlightFirstMatch(true)
                    .ClientEvents(events => events.OnChange("onComboBoxChangeProfesional"))
                    .SelectedIndex(0)
                    .HtmlAttributes(new { style = "width: 250px; vertical-align: middle; display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Seleccionar Profesional") })   
                    .BindTo((SelectList)ViewData["perId_Data"])
                   %>            
                <% } %>
            <%
            });
        })
        
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catTurno", new { especialidadId = 0, profesionalId = 0, fecha = DateTime.Now });
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Custom("cmdAsignarPaciente")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(false)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -290px;", title = "Asignar Paciente" })
                    .HtmlAttributes(new {style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Asignar Paciente") })                   
                    .SendState(false)
                    .DataRouteValues(route =>
                        {
                            route.Add(o => o.turId).RouteKey("turId");                            
                        });
                commands.Custom("cmdCambiarAdmision")
                   .Ajax(true)
                   .ButtonType(GridButtonType.Image)                   
                   .SendDataKeys(false)
                   .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -240px;", title = "Cambiar Admision" })
                   //.HtmlAttributes(new { style = "display: " + (EsMedico ? "inline-block" : "none") })
                   .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Cambiar Admision") })
                   .SendState(false)
                   .DataRouteValues(route =>
                   {
                       route.Add(o => o.turId).RouteKey("turId");
                   });
                commands.Custom("cmdVerFichaPaciente")
                   .Ajax(true)
                   .ButtonType(GridButtonType.Image)
                   .SendDataKeys(false)
                   .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -47px -240px;", title = "Ver ficha Paciente" })
                   //.HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Ver Ficha Paciente") })
                   .SendState(false)
                   .DataRouteValues(route =>
                   {
                       route.Add(o => o.turId).RouteKey("turId");
                   });
                commands.Custom("cmdBloquearTurno")
                     .Ajax(true)
                     .ButtonType(GridButtonType.Image)
                     .SendDataKeys(false)
                     .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -320px;", title = "Bloquear Turno" })
                     .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Bloquear Turno") })
                     .SendState(false)
                     .DataRouteValues(route =>
                     {
                         route.Add(o => o.turId).RouteKey("turId");
                     });
                commands.Custom("cmdEliminarTurno")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(false)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -17px -339px;", title = "Desasignar Paciente" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Eliminar Turno") })
                    .SendState(false)
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.turId).RouteKey("turId");
                    });
                commands.Custom("cmdRepetirTurno")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -209px;" })
                    .HtmlAttributes(new { title = "Repetir el Turno", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Repetir Turno") })
                    .DataRouteValues(route =>
                        {
                            route.Add(o => o.turId).RouteKey("turId");                            
                        });
                commands.Custom("cmdMasDetalle")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -177px;" })
                    .HtmlAttributes(new { title = "Ver detalle del turno, asignar diagnósticos y/o prácticas", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Ver Ficha Paciente") })
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.turId).RouteKey("turId");
                    });
                commands.Custom("cmdRellamado")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Rellamar.png') no-repeat -0px 0px; background-size: 16px 16px;" })
                    .HtmlAttributes(new { title = "Volver a llamar a un Paciente", style = "display: " + (EsMedico ? "inline-block" : "none") })
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.turId).RouteKey("turId");
                    });
                commands.Custom("cmdHistoriaClinica")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Maletin.png') no-repeat -0px 0px; background-size: 16px 16px;" })
                    .HtmlAttributes(new { title = "Ver historia clínica"/*, style = "display: " + (EsMedico ? "inline-block" : "none")*/ })
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.turId).RouteKey("turId");
                    });
            }).Width("264px").Title("Acciones");
            columns.Bound(c => c.turPrioridad).Width("70px").Title("Prioridad").Filterable(false).Visible(true).HtmlAttributes(new { style = "white-space: nowrap; text-align: center;" })
                .ClientTemplate("<div title='<#= turPrioridad == \"Normal\" ? \"Sin prioridad de Atención\" : \"Alta prioridad de Atención\" #>' " + ((Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTurno", "Prioridad") != "none" ? "onclick=\"onEjecutaAccionCambiaPrioridad(<#= turId #>, '<#= turPrioridad #>', '<#= pacNombre #>', '<#= tipoAdmisionCodigo #>');\"" : "") + " style='cursor: pointer; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/CRUD/<#= ImagenPrioridad #>' height='18px' width='18px' style='margin-right: 0px; vertical-align: middle;' /></div>");
            columns.Bound(c => c.turId).Width("15px").Title("").Visible(false).Filterable(false);
            columns.Bound(c => c.tipoId).Width("15px").Title("").Visible(false).Filterable(false);
            columns.Bound(c => c.aghId).Width("15px").Title("").Visible(false).Filterable(false);
            columns.Bound(c => c.pacId).Width("15px").Title("").Visible(false).Filterable(false);
            columns.Bound(c => c.tipoAdmisionCodigo).Width("15px").Title("").Visible(false).Filterable(false);
            columns.Bound(c => c.EstadoCodigo).Width("15px").Title("").Visible(false).Filterable(false);
            columns.Bound(c => c.horayminutos).Width("60px").Title("Hora").Visible(true).HtmlAttributes(new { style = "text-align: center;" })
            .ClientTemplate("<div style='width: 100%; text-align: center;'><#= horayminutos #></div>");
            columns.Bound(c => c.tipoDescripcion).Width("120px").Title("Estado").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: left; vertical-align:middle;'><img src='" + Url.Content("~/Content") + "/Estados/<#= turEstadoImagen #>' title='<#= tmbDescripcion || tipoDescripcion #>' height='15' width='15' style='vertical-align:middle;' />  <#= tipoDescripcion #></div>");
            columns.Bound(c => c.tipoAdmisionDescripcion).Width("165px").Title("Admisión").Visible(true);
            columns.Bound(c => c.HoraAdmisionado).Width("60px").Title("Hora").Visible(true).HtmlAttributes(new { style = "text-align: center;" })
            .ClientTemplate("<div style='width: 100%; text-align: center;'><#= HoraAdmisionado #></div>");
            columns.Bound(c => c.pacNombre).Width("250px").Title("Paciente").Visible(true).Filterable(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pacNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= pacNombre #></label>");
            columns.Bound(c => c.pacNumeroDocumento).Width("80px").Title("DNI").Visible(true).Filterable(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pacNumeroDocumento #>' style='cursor: pointer; white-space: nowrap;' ><#= pacNumeroDocumento #></label>");
            columns.Bound(c => c.nroHC).Width("70px").Title("H.C.").Visible(true).Filterable(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><#= nroHC #></div>").Format("{0:N}");
         })
         .Editable(editing => editing.Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
         .Pageable((paging) =>
         paging.Enabled(true)
         .PageSize(100))
        .ClientEvents(events => events.OnDataBinding("onDataBindingTurnos").OnComplete("onCompleteTurnos").OnRowSelected("onRowSelectedTurno").OnCommand("onCommandTurnos").OnError("onErrorTurno").OnDataBound("gridTurnos_OnDataBound").OnLoad("gridTurnos_OnLoad"))
        .Selectable()
        .Scrollable(scroll => scroll.Enabled(true).Height((int)Session["AlturaGrilla"]))
        .Resizable(resizing => resizing.Columns(true))
        .Render();                     
        %>

<% //Html.Telerik().Grid<GeDoc.PieTurnos>()
   //     .Name("GridPieTurnos")      
   //     .Columns(columns =>
   //     {
   //         columns.Bound(c => c.turEstadoImagen).Width("8px").Title("Estado del Turno").Visible(true)
   //         .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/<#= turEstadoImagen #>' title='<#= tipoDescripcion #>' height='22px' width='22px' style='vertical-align:middle;' /><#= tipoDescripcion #></div>");        
   //         columns.Bound(c => c.CantidadTurnos).Width("8px").Visible(true).HtmlAttributes(new { style = "text-align: center; font-weight: bold;" }).HeaderHtmlAttributes(new { style = "text-align: center; font-weight: bold;" });           
   //     })
   //     .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingPieTurnos", "catTurno"))
   //     .Footer(false)
   //     .Render();
%>
</div>

<!-- Paciente -->
<% Html.Telerik().Window()
        .Name("WPacientes")
        .Title("Asignación de Pacientes")
        .Visible(false)
        .Modal(true)
        .Content(() =>
        {
            %>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1024)
       .Height(630)
       .Render();
%>

<!-- Detalles del Turno -->
<% Html.Telerik().Window()
        .Name("wCRUDTurno")
        .Title("Detalle del Turno")
        .Visible(false)
        .Content(() =>
        {
        })
       .Buttons(b => b.Close())
       .Modal(true)
       .Draggable(true)
       .Scrollable(true)
       .Width(1124)
       .Height(580)
       .ClientEvents(e => e.OnClose("wCRUDTurno_OnClose"))
       .Render();
 %>

<!-- Motivo para bloquear turno -->
<% Html.Telerik().Window()
        .Name("wMotivoBloqueo")
        .Title("Motivo para Bloquear Turno")
        .Visible(false)
        .Content(() =>
        {
        })
       .Buttons(b => b.Close())
       .Modal(true)
       .Draggable(true)
       .Scrollable(true)
       .Width(450)
       .Height(110)
       .Render();
 %>

<!-- Selecciona consultorio / centro de atención -->
<%
    Html.Telerik().Window()
      .Name("wSeleccionDeConsultorio")
      .Title("Selección de Consultorio / Centro de Atención")
      .Visible(false)
      .Content(() =>
      {
          Html.RenderPartial("SeleccionaConsultorio");
      })
      .Buttons(b => b.Clear())
      .Draggable(true)
      .Scrollable(false)
      .Modal(true)
      .Width(424)
      .Height(140)
      .ClientEvents(eventos => eventos.OnActivate("onActivate_wSeleccionDeConsultorio").OnLoad("onLoad_wSeleccionDeConsultorio"))
      .Render();
%>

<!-- Permite re agendar pacientes -->
<%
    Html.Telerik().Window()
      .Name("wReAgendar")
      .Title("Re-Agendar turnos")
      .Visible(false)
      .Content(() =>
      {
          Html.RenderPartial("ReAgendar");
      })
      .Buttons(b => b.Close())
      .Draggable(true)
      .Scrollable(false)
      .Modal(true)
      //.Width(424)
      //.Height(140)
      .ClientEvents(eventos => eventos.OnActivate("onActivate_wReAgendar").OnLoad("onLoad_wReAgendar"))
      .Render();
%>

<!-- Historia Clínica -->
<%
    Html.Telerik().Window()
      .Name("wHistoriaClinica")
      .Title("Historia Clínica")
      .Visible(false)
      .Content(() =>
      {
      })
      .Buttons(b => b.Maximize().Close())
      .Draggable(true)
      .Scrollable(false)
      .Modal(true)
      //.Width(424)
      //.Height(140)
      .ClientEvents(eventos => eventos.OnActivate("onActivate_wHistoriaClinica"))
      .Render();
%>

<% Html.RenderPartial("WaitingCRUD"); %>
<div id="divCantidadPractica" class="es-overlay" style="display: none; text-align: center; z-index: 19998;">
</div>
<div id="divCantidadPracticaContent" class="BordeRedondeado" style="display: none; background-color: white; padding: 20px; margin-left: 38%;position: absolute;top: 40%;z-index: 19998;">
    <label style="vertical-align: middle;">Cantidad:</label>
    <%=Html.Telerik().NumericTextBox().EmptyMessage("").DecimalDigits(0).Spinners(false).MinValue(0).Name("txtCantidadPracticas") %>
    <div style="position: relative; top: 10px; left: 28%;">
        <table>
            <tr>
                <td style="border: none;">
                    <button class="t-button t-grid-update" onclick="irAGuardarPractica(idPracticaSeleccionada);">Aceptar</button>
                </td>
                <td style="border: none;">
                    <button class="t-button t-grid-cancel" onclick="onCerrarCantidadPractica(true);">Cancelar</button>
                </td>
            </tr>
        </table>
    </div>
</div>

<script>
    CerrarWaiting();
    CerrarWaitingCRUD("wtTurnosAmbulatorios");
    $("#divCantidadPractica").ready(function () {
    });

</script>