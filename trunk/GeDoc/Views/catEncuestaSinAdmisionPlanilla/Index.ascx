﻿<!DOCTYPE html>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IdentityModel.Tokens" %>
<%@ Import Namespace="GeDoc" %>
<%@ Import Namespace="GeDoc.Models" %>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>






<% ViewData["FiltroContains"] = true; %>

<% string _PathContent = Url.Content("~/Content"); %>

<style type="text/css">
    
    /* Clases para dar colores a las filas segun el Nivel de Riesgo de la encuesta REDES*/
    .weighingHigh {
        background-color: coral;
    }
    .weighingMid {
        background-color: bisque;
    }
    .weighingLow {
        background-color: aquamarine;
    }

</style>
<script type="text/javascript">
    var _encId;
    var _pacId;
    var _encPerId;
    var _diagId;
    var _enfId;
    var _fecha;
    var _RowIndex;
    var _DatosRegistro;
    var _DatosRegistro_persona;
    var encId;
    var _plaId;
    var plaId;
    var peId;
    var _encuestadorNombre;
    var _plaFechaPlanilla;
    var _centroSaludTexto;
    var fechaPlanilla;

    var valorNoDeInterconsulta,
        valorNoDeDerivado,
        valorNoDeAtendidoLocal,
        valorNoDeProgramado;

    //-------traigo los valores de "NO" de la tabla sisTipo
    $.ajax({
        type: 'POST',
        url: '<%=Url.Action("valoresDeNo","catEncuestaSinAdmisionPlanilla")%>',
                    dataType: 'json',
                    //data: { especialidad: _especialidad, paciente: _pacId },
                    cache: false,
                    sync: false,
                    success: function (data) {
                        valorNoDeInterconsulta = data[0];
                        valorNoDeDerivado = data[1];
                        valorNoDeAtendidoLocal = data[2];
                        valorNoDeProgramado = data[3];
                    },
                    error: function () {
                        console.log("No se pudo acceder al controlador o a la base de datos");
                    }
                });
    //---------------------------------------------------

    var especialidadElegida

    //esconder error de datepicker que queda visible si se entra desde menu reportes
    $("#ui-datepicker-div").hide();


    function onRowSelectedEncuesta(e) {
        //debugger;
        var row = e.row;
        var grid = $("#Grid").data("tGrid");
        var dataItem = grid.dataItem(row);
        //debugger;
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = kludgeDatosRegistro = dataItem;
        _plaId = _DatosRegistro['plaId'];
        _encuestadorNombre = _DatosRegistro['encuestadorApyNom'];
        _plaFechaPlanilla = _DatosRegistro['plaFechaPlanilla'].toLocaleString("es-ar", { day: "2-digit", month: "2-digit", year: "numeric" });
        _centroSaludTexto = _DatosRegistro['centroSaludTexto'];
        plaId = _plaId;
    }
    function onEditEncuesta(e) {
        //debugger;
        var dataItem = e.dataItem;
        var mode = e.mode;
        var form = e.form;
        if (e.mode == "Agregar") {
            $("#plaFechaPlanilla").datepicker({ dateFormat: "dd/mm/yy" }).datepicker("setDate", new Date());


        }
        if (e.mode === 'edit') {
            _plaId = dataItem.plaId;
        }
    }
    function onRowSelectedEncuestados(e) {
        //debugger;
        var row = e.row;
        var grid = $("#gridEncuestados").data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        // _encPerId = _DatosRegistro['encPerId'];
        _pacId = _DatosRegistro['pacId'];
        //_plaId = _DatosRegistro['plaId'];
        _encId = _DatosRegistro['encId'];
    }
    function Refrescar() {

        var grid = $("#gridEncuestados").data("tGrid");
        grid.ajaxRequest();

    }
    function RefrescarEspecialidades() {
        var grid = $("#gridEspecialidades").data("tGrid");
        grid.ajaxRequest();

    }
    function onCommandPaciente(e) {

        var Accion;
        if (e.name == "cmdAgregarPacienteTab") {

            var _WindowPac = $("#wCrudTabPacientes").data("tWindow");
            var _Post = GetPathApp("catEncuestaSinAdmisionPlanilla/TabPaciente");
            Accion = "Agregar";
            AbrirWaiting();
            _WindowPac.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
            $.ajax({
                url: _Post,
                data: { Accion: "Agregar", pacId: -1, plaId: _plaId },
                type: 'POST',
                error: function (xhr, ajaxOptions, thrownError) {
                    CerrarWaiting();
                    jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                    });
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                },
                success: function (data) {

                    CerrarWaiting();
                    _WindowPac.content(data);
                    //  $("#barId").data("tComboBox").select((data.barId - 1));
                    _WindowPac.center().title(Accion).open();

                    $("#pacNumeroDocumento").focus().select();


                }
            });
        }
        if (e.name == "cmdAgregarPaciente") {
            var grid = $("#gridPacientes").data("tGrid");
            grid.addRow();

        }
        if (e.name == "cmdAsignar") {

            var grid = $("#gridPacientes").data("tGrid");
            if ((_pacId < 0) && (e.name == "cmdAsignar")) {
                jAlert('Debe seleccionar una persona.', 'Atencion!');
                return;
            }
            if (_pacId != null) {
                AsignarPaciente(_pacId, _DatosRegistro['pacApellidoyNombre'], 0, []);
            }
        }
    }

    function onCommandEditPaciente(e) {
        var padron = $("#pacPadron");
        padron.prop("disabled", true);
        $("#tipoIdTipoDocumento").bind("valueChange", function () {
            validarTipoDocumento();
        });
        if (e.mode !== "edit") {
            $("#pacNumeroDocumento").bind("valueChange", function () {
                Obtenerdatos_CONPAD();
            });
        }

    }


    function onRowSelectedEspecialidades(e) {
        /*
        debugger;
        var row = e.row;
        var grid = $("#gridEspecialidades").data("tGrid");
        var dataItem = grid.dataItem(row);
        */
        //_RowIndex = e.dataItem.peId;//e.row.rowIndex;
        peId = e.dataItem.peId;
    }

    function test(id) {

        var inp_interconsulta = $("#interconsulta-input").val();
        var inp_derivado = $("#derivado-input").val();

        if ((inp_interconsulta == inp_derivado) && (inp_interconsulta == "SI")) {
            jAlert("No puede ser interconsulta y derivado a la vez", "Especialidades - Información");

            $("#interconsulta-input").val("NO");
            $("#derivado-input").val("NO");

            //estoy hay que cambiarlo según la base de datos
            //ESTOS TIENEN QUE SER LOS VALORES DE NO EN LA TABLA sisTipo

            $("#interconsulta").val(valorNoDeInterconsulta);
            $("#derivado").val(valorNoDeDerivado);

        }
    }


    function testAtendidos(id) {

        var inp_atendido = $("#atendidoLocal-input").val();
        var inp_programado = $("#programado-input").val();

        if ((inp_atendido == inp_programado) && (inp_atendido == "SI")) {
            jAlert("No puede ser atendido y programado a la vez", "Especialidades - Información");

            $("#atendidoLocal-input").val("NO");
            $("#programado-input").val("NO");

            //estoy hay que cambiarlo según la base de datos
            //ESTOS TIENEN QUE SER LOS VALORES DE NO EN LA TABLA sisTipo

            $("#atendidoLocal").val(valorNoDeAtendidoLocal);
            $("#programado").val(valorNoDeProgramado);

        }
    }

    function AbrirComentarioModal() {
        var _WindowEst = $("#wCrudEspecialidadComentario").data("tWindow");
        _WindowEst.center().open();

        tinymce.init({
            selector: '#comment',
            language: "es",
            height: "500px",
            width: "100%",
            theme: "simple",
        });

        $("#comment").css("visibility: visible;");


        if (peId == 0) {
        }
        else {
            //debugger;
            AbrirWaiting();
            $.ajax({
                type: 'POST',
                url: '<%=Url.Action("CargaComentario","catEncuestaSinAdmisionPlanilla")%>',
                dataType: 'json',
                data: { id: peId },
                cache: false,
                sync: false,
                success: function (data, textStatus, jqXHR) {
                    var result = JSON.parse(data);
                    var test = data;
                    //$("#comment").val(decodeURI(result[0].cmt));
                    if (typeof (tinyMCE.activeEditor) == "object" && tinyMCE.activeEditor != null) {
                        tinymce.activeEditor.setContent('');
                        tinymce.activeEditor.setContent(decodeURI(result[0].cmt));
                    }
                    CerrarWaiting();
                },
                error: function () {
                    alert('error');
                }
            });

        }

    }

    function onCommandEditEspecialidades(e) {
        //debugger;
        onRowSelectedEspecialidades(e);
        //console.log(e);
        var label = '<div class ="editor-label"><label for="Comentarios"></label></div>';

        var btn2 = '<div class="editor-field"> ' +
                    '<a href="javascript:;" onClick="javascript:AbrirComentarioModal();" class="t-button t-button-icon">' +
                    '<span class="t-icon t-edit"></span>' +
                    'Comentario</a></div>';


        $(".editor-field").last().after(label);
        $(".editor-label").last().after(btn2);


        //$(".editor-label").append("<p></p>");

        $("#atendidoLocal-input").prop('disabled', true);
        $("#derivado-input").prop('disabled', true);


        $("#interconsulta-input").prop('disabled', true);
        $("#derivado-input").prop('disabled', true);

        //verificación de interconsulta y derivado (no pueden estar ambos en si)
        $('#derivado-input').focus(function () {
            test(e);
        });

        $('#interconsulta-input').focus(function () {
            test(e);
        });

        //verificacion de atendido local y programó turno (no pueden estar ambos en si)
        $('#atendidoLocal-input').focus(function () {
            testAtendidos(e);
        });

        $('#programado-input').focus(function () {
            testAtendidos(e);
        });

        //reemplazo de botón de aceptar en editar
        $(".t-button.t-grid-update.t-button-icon").hide().before("<button id='btnAceptar'>Aceptar y Guardar</button>");
        //reemplazo de botón de cancelar en editar y agregar
        $("#gridEspecialidadesform > div > a.t-button.t-grid-cancel.t-button-icon").hide()//.before("<button id='btnCancelar'>Cancelar</button>");


        //reemplazo de botón de aceptar en agregar
        $("#gridEspecialidadesform > div > a.t-button.t-grid-insert.t-button-icon").hide().before("<button id='btnAceptarAgregar'>Agregar y Guardar</button>");


        //$(".t-button.t-grid-update.t-button-icon").append("Guardar");
        //$(".t-button.t-grid-cancel.t-button-icon").append("Cancelar");

        especialidadElegida = $("#espId").val();
        //evento click en editar
        $("#btnAceptar").on('click', function () {

            verificarEspecialidadRepetida(e);
        });

        //evento click en agregar
        $("#btnAceptarAgregar").on('click', function () {

            verificarEspecialidadRepetida(e);
        });

        //las rayas
        $('label[for="espId"]').after('<span><hr class="style-two"></hr><p style="margin-top: -2px; margin-bottom: 0px;  font-weight: bold; text-align: left;">ingreso</p></span>');
        $('label[for="derivado"]').after('<span><hr class="style-two"></hr><p style="margin-top: -2px; margin-bottom: 0px;  font-weight: bold; text-align: left;">egreso</p></span>');
        $('label[for="programadoCuando"]').after('<hr class="style-two"></hr>');

        $('.t-button.t-grid-update.t-button-icon').after("</br>");


        if (typeof (tinyMCE.activeEditor) == "object" && tinyMCE.activeEditor != null) {
            tinyMCE.activeEditor.setContent('');
            tinymce.EditorManager.execCommand('mceRemoveControl', true, "comment");
        }

        if (e.mode == "insert") {

            var derivado = $("#derivado").data('tComboBox');
            derivado.enable();
            var programadoEn = $("#programadoEn").data('tComboBox');
            programadoEn.disable();
            var programadoCuando = $("#programadoCuando");
            programadoCuando.attr("disabled", true);
            $("#atendidoLocal").bind("valueChange", function () {
                //debugger;
                var valorAtendidoLocal = $("#atendidoLocal").data('tComboBox').value();
                if (valorAtendidoLocal == 197) {
                    derivado.select(0);
                    derivado.disable();
                    programadoEn.disable();
                    programadoCuando.attr("disabled", true);
                }
                if (valorAtendidoLocal == 198) {
                    derivado.enable();
                    var valorDerivado = $("#derivado").data('tComboBox').value();
                    if (valorDerivado == 1) {
                        programadoEn.enable();
                        programadoCuando.attr("disabled", false);
                    }
                    if (valorDerivado == 2) {
                        programadoEn.disable();
                        programadoCuando.attr("disabled", true);
                    }
                }

            });

            $("#derivado").bind("valueChange", function () {
                //debugger;
                valorDerivado = $("#derivado").data('tComboBox').value();
                if (valorDerivado == 1) {
                    programadoEn.enable();
                    programadoCuando.attr("disabled", false);
                    var fecha = _plaFechaPlanilla;
                    programadoCuando.val(fecha);
                }
                if (valorDerivado == 2) {
                    programadoEn.disable();
                    programadoCuando.attr("disabled", true);
                }
            });


        }
    }



    function onSavePaciente(e) {
        debugger
        var values = e.values;
        _pacId = values.pacId;
    }

    function onCommandEncuestado(e) {
        //debugger;

        //console.dir(e)

        var _WindowPac;
        if (e.name == "cmdAgregarPersonas") {

            //var _WPacientess = $("#WPacientes").data("tWindow");
            //var _GPacientes = $('#gridPacientes').data('tGrid');

            //_WPacientess.center().open();
            //_GPacientes.ajaxRequest();
            var _Post = GetPathApp("catEncuestaSinAdmisionPlanilla/TabPaciente");
            _WindowPac = $("#wCrudTabPacientes").data("tWindow");
            Accion = "Agregar";
            AbrirWaiting();
            $.ajax({
                url: _Post,
                data: { Accion: "Agregar", pacId: -1, encId: 0 },
                type: 'POST',
                error: function (xhr, ajaxOptions, thrownError) {
                    CerrarWaiting();
                    jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                    });
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                },
                success: function (data) {

                    CerrarWaiting();
                    ////////////////////////////////////////
                    _WindowPac.content(data);
                    _WindowPac.center().title(Accion).open();
                    _WindowPac.title("Encuesta Nro " + _plaId + "-" + _plaFechaPlanilla + " - " + _centroSaludTexto + " - Encuestador:" + _encuestadorNombre).open();
                    $("#pacNumeroDocumento").focus().select();
                }
            });
        }
        if (e.name == "cmdEspecialidades") {
            var estGrid = $('#gridEspecialidades').data('tGrid');
            var _WindowEst = $("#CRUDEspecialidades").data("tWindow");
            onRowSelectedEncuestados(e);
            // $("#lblNombrePersona3").text('Especialidades de ' + _Nombre);

            estGrid.rebind();
            _WindowEst.center().open();
            //aqui test
        }

        if (e.name == "cmdEditarEncuestado") {

            onRowSelectedEncuestados(e);

            var _Post = GetPathApp("catEncuestaSinAdmisionPlanilla/TabPaciente");
            _WindowPac = $("#wCrudTabPacientes").data("tWindow");
            Accion = "Modificar";
            AbrirWaiting();
            $.ajax({
                url: _Post,
                data: { Accion: "Modificar", pacId: _pacId, encId: _encId },
                type: 'POST',
                error: function (xhr, ajaxOptions, thrownError) {
                    CerrarWaiting();
                    jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                    });
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                },
                success: function (data) {

                    CerrarWaiting();
                    // var _WindowCRUD = $("#wCrudTabPacientes").data("tWindow");     
                    _WindowPac.content(data);
                    //debugger;
                    _WindowPac.center().title(Accion).open();
                    _WindowPac.title("Encuesta Nro " + _plaId + "-" + _plaFechaPlanilla + " - " + _centroSaludTexto + " - Encuestador:" + _encuestadorNombre).open();
                    $("#pacNumeroDocumento").focus().select();
                }
            });
        }
    }

    function onCommandEncuesta(e) {
        //debugger;

        //if(
        //        (typeof(e) === "undefined") || 
        //        (typeof(e.row) === "undefined") ||
        //        (typeof(e.row.getElementsByTagName('td')[2]) === "undefined") ||
        //        (typeof (e.row.getElementsByTagName('td')[2].innerHTML === "undefined"))
        //    ){
        //    console.log("no se trae fecha")
        //}else {
            
        //    fechaPlanilla = e.row.getElementsByTagName('td')[2].innerHTML;
        //    console.log("la fecha es " + e)
                                  
        //}

        
        //if (typeof (e.row.getElementsByTagName('td')[2].innerHTML) == "object" && e.row.getElementsByTagName('td')[2].innerHTML != null) {
        //    fechaPlanilla = e.row.getElementsByTagName('td')[2].innerHTML;
        //}

        <%
        DateTime fecha = DateTime.Now;

        var fechaText = fecha.ToString("dd/MM/yyyy");
        %>
        if (e === "Agregar") {
            var grid = $("#Grid").data("tGrid");
            grid.addRow();
            $("#plaFechaPlanilla").val('<%= fechaText %>');

        }
        if (e.name == "edit") {
            onRowSelectedEncuesta(e);
            // var grid = $("#Grid").data("tGrid");

            //  var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
            //console.log(tr);
            //console.log(_RowIndex);
            //grid.editRow(tr);
            //$("#GridPopUp").data("tWindow").title("Editar Encuesta Nº " + $("#Grid").data("tGrid").data[_RowIndex].plaId);
        }
        if (e.name == "PlanillaProcesar") {
            //debugger;
            onRowSelectedEncuesta(e);

            var _Post = GetPathApp('catEncuestaSinAdmisionPlanilla/ProcesaPlanilla');
            $.ajax({
                url: _Post,
                data: { plaId: _plaId },
                type: 'POST',
                error: function (xhr, ajaxOptions, thrownError) {
                    jAlert('Seleccionar la planilla a procesar.', '¡Atención!', function () {
                    });
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                },
                success: function (data) {
                    if (data) {
                        jAlert(data.Mensaje, 'Mensaje...');
                        var _Grid = $('#Grid').data('tGrid');
                        return;
                        //  GridEncuestados_OnDataBound(e);
                    } else {
                        jAlert('Planilla Procesada', 'Mensaje...');
                        var _Grid = $('#Grid').data('tGrid');
                        return;
                    }
                }
            });

        }
        if (e.name == "cmdPersonas") {
            onRowSelectedEncuesta(e);
            var _WEncuestados = $("#WEncuestados").data("tWindow");
            var _GEncuestados = $('#gridEncuestados').data('tGrid');


            var _Post = GetPathApp('catEncuestaSinAdmisionPlanilla/getValidaSiEstaAsignado');
            $.ajax({
                url: _Post,
                data: { _plaId: _plaId },
                type: 'POST',
                error: function (xhr, ajaxOptions, thrownError) {
                    jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                    });
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                },
                success: function (data) {

                    if (data) {

                        _WEncuestados.center().open();
                        _GEncuestados.ajaxRequest();
                        //  GridEncuestados_OnDataBound(e);
                    } else {
                        jAlert('No se puede Asignar una Persona, esta Encuesta NO está disponible', 'Error...');
                        var _Grid = $('#Grid').data('tGrid');
                        return;
                    }
                }
            });
        }
        // onCommandEdit(e);

        if (e.name == "generarReporte") {
            //;alert('vale por un reporte precioso');
            //ShowPopup();
            //$("#dialog").dialog();
            //_RowIndex selected row.
            onRowSelectedEncuesta(e);
            //console.log(e.row);

            var selectedID = e.row.getElementsByTagName('td')[1].innerHTML;
            var fechaSelected = e.row.getElementsByTagName('td')[2].innerHTML;
            var csNombre = $("#uscs").html();
            var Encuestador = e.row.getElementsByTagName('td')[3].innerHTML;

            if ($("#RowID").length == 0) {
                $(this).append("<input type=\"hidden\" value=\"" + selectedID + "\" name=\"RowID\" id=\"RowID\">  ");
                $(this).append("<input type=\"hidden\" value=\"" + fechaSelected + "\" name=\"fechaSelected\" id=\"fechaSelected\">  ");
                $(this).append("<input type=\"hidden\" value=\"" + csNombre + "\" name=\"csNombre\" id=\"csNombre\">  ");
                $(this).append("<input type=\"hidden\" value=\"" + Encuestador + "\" name=\"Encuestador\" id=\"Encuestador\">  ");
            }
            else {
                $("#RowID").val(selectedID);
                $("#fechaSelected").val(fechaSelected);
                $("#csNombre").val(csNombre);
                $("#Encuestador").val(Encuestador);
            }


            PopupCenter('<%=Url.Action("ReporteProtur","catEncuestaSinAdmisionPlanilla")%>', 'Reporte', screen.width, screen.height);
            //PopupCenter('<%=Url.Action("ReporteProtur","catEncuestaSinAdmisionPlanilla")%>', 'Reporte', '1024', '500');
        }







        function PopupCenter(url, title, w, h) {
            // Fixes dual-screen position                         Most browsers      Firefox
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();
            }
        }

    }
    function AsignarPaciente(pacienteId, Nombre, Preguntar, Enfermedades) {

        var Enfermedades = Enfermedades || [];
        var _Post = GetPathApp('catEncuestaSinAdmisionPlanilla/getValidaSiPacienteAsignado');
        $.ajax({
            url: _Post,
            data: { _pacId: pacienteId },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                });
                $('#popup_container').focus();
                $('#popup_ok').focus();
            },

            success: function (data) {
                if (!data.Error) {
                    if (Preguntar == 1) {
                        jConfirm('¿Confirma Asignación del Paciente "' + Nombre + '"?', "Asignación de Paciente...", function (r) {
                            if (r) {
                                //  AbrirWaiting();
                                onCancelarPaciente();
                                $.post(GetPathApp('catEncuestaSinAdmisionPlanilla/AsignarPacienteEncuesta'), { pacId: pacienteId, plaId: _plaId }, function (data) {
                                    CerrarWaiting();

                                    if (!data.IsValid) {
                                        jAlert(data.Mensaje, 'Error...');
                                    }
                                    Refrescar();

                                    $("#txtBusquedaPaciente").val("");
                                });
                            }
                        });
                    } else {
                        // AbrirWaiting();
                        onCancelarPaciente();
                        $.post(GetPathApp('catEncuestaSinAdmisionPlanilla/AsignarPacienteEncuesta'), { pacId: pacienteId, plaId: _plaId }, function (data) {
                            CerrarWaiting();
                            if (!data.IsValid) {
                                jAlert(data.Mensaje, 'Error...');
                            } else {

                                _encId = data.encId;

                            }
                            Refrescar();

                            $("#txtBusquedaPaciente").val("");
                        });
                    }
                } else {
                    if (data.Error) {
                        jAlert(data.Mensaje, "Alerta!", function () {
                            $("#" + FocusControl)[0].focus().select();
                        });
                    }
                    var _Grid = $('#Grid').data('tGrid');
                    return;
                }
            }
        });
    }
    function onCancelarPaciente() {

        var _Window = $("#WPacientes").data("tWindow");
        _Window.close();
    }
    $("#txtBusquedaPaciente").keydown(function (e) {

        if (e.which == 13) {

            onBuscar();
            e.preventDefault();
            e.stopPropagation();
        }
    });
    function onBuscar() {
        // AbrirWaiting();
        var grid = $('#gridPacientes').data("tGrid");
        grid.ajaxRequest();
    }
    function onActivaveWPersonas(e) {
        var txtBus = $("#txtBusquedaPaciente")[0];
        txtBus.focus();
    }

    function onRowSelectedPaciente(e) {
        var row = e.row;
        var grid = $('#gridPacientes').data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        _pacId = _DatosRegistro['pacId'];
    }

    function validarTipoDocumento() {

        var _tipoDoc = $("#tipoIdTipoDocumento").data("tComboBox").value();
        var _documento = $('#pacNumeroDocumento');
        if ((_tipoDoc == 182)) {
            _documento.prop('disabled', true);
            $("#pacNumeroDocumento").attr("style", "background-color: grey;");
            $("#pacNumeroDocumento").val(11111111);

        }
        else {
            _documento.prop('disabled', false);
            $("#pacNumeroDocumento").attr("style", "background-color: white;");
            $("#pacNumeroDocumento").val();
        }
    }

    function onDataBindingEncuestados(e) {
        e.data = $.extend(e.data, { _plaId: _plaId ? _plaId : 0 });


    }
    function onDataBindingEncuesta(args) {

        //console.log(args)

        //fechaPlanilla = args;
        //debugger;
        var _SoloYo = $("#chbSoloYo").is(":checked");
        var _SoloPendientes = $("#chbTodos").is(":checked");
        // AbrirWaiting();
        args.data = $.extend(args.data, { SoloMisEncuestas: _SoloYo });

    }
    function onSave(e) {
        //debugger;
        //   e.values = $.extend(e.values, { SoloMisEncuestas: $("#chbSoloYo").is(":checked"), afliw: "wilfa" });
    }

    //function onDataBindingenlEncuestasAPSPersonas(e) {
    //    
    //    if (_DatosRegistro_persona != null) {
    //        _encPerId = _DatosRegistro_persona.encPerId;
    //    }
    //    e.data = $.extend(e.data, { _encPerId: _encPerId });
    //}


    //function onDataBindingPaciente(e) {
    //    
    //    var BuscarTexto = "";
    //    var txtBus = $("#txtBusquedaPaciente");
    //    if (txtBus != null) {
    //        BuscarTexto = txtBus.val().trim();
    //    }
    //    e.data = $.extend(e.data, { TextoBuscado: BuscarTexto });
    //}
    function onCompleteEncuesta(e) {
        //debugger;
        //if (respuesta.Error) {
        //    var FocusControl = respuesta.Foco;
        //    jAlert(respuesta.Mensaje, "Error...", function() {
        //        $("#" + FocusControl)[0].focus().select();
        //    });
        //}
        //success: function(respuesta) {
        //    CerrarWaitingCRUD();
        //    if (respuesta.Error) {
        //        var FocusControl = respuesta.Foco;
        //        jAlert(respuesta.Mensaje, "Error...", function() {
        //            $("#" + FocusControl)[0].focus().select();
        //        });
        //    }
        //}
        //if (e.name != "insert") {

        //   // var grid = $("#gridPacientes").data("tGrid");
        //    // AsignarPaciente(_pacId,"",1);
        //    // grid.addRow();
        //}



    }
    //Seleccionamos el primer campo
    $("#DatosGeneral").ready(function () {

        $("form:not(.filter) :input:visible:enabled:first").focus().select();
    });
    function RefrescarEncuesta() {
        var gridEncuesta = $("#Grid").data("tGrid");
        gridEncuesta.ajaxRequest();
        //  $("#Grid").data("tGrid").rebind();
    }
    function onDataBindingEspecialidades(e) {
        //debugger;
        e.data = $.extend(e.data, { encId: _encId });
        Refrescar();
    }
    function onEditEncuestados() {
        var _Wenc = $("#WEncuestados").data("tWindow");

        _Wenc.title = "Personas Encuestadas de la Encuesta Nro " + _plaId;
    }
    function onDataBindingEnfermedadesEncuestados(e) {

        var grid = $('#gridPacienteEnfermdedad2').data('tGrid');
        if (grid != null) {
            if (_DatosRegistro_persona != null) {
                _encPerId = _DatosRegistro_persona.encPerId;
            }
            e.data = $.extend(e.data, { _encPerId: _encPerId });
        }
    }
    function onCompleteEspecialidades(e) {
        if (e.name == "delete" || e.name == "insert") {
            Refrescar();
            RefrescarEspecialidades();
        }
    }
    function OnOpenEncuestados(e) {
        $(e.target).data("tWindow").title("Encuesta Nro: " + _plaId + "- Fecha:" + _plaFechaPlanilla + " - Centro Salud:" + _centroSaludTexto + " - Encuestador:" + _encuestadorNombre);
    }

    function OnCloseEncuestados(e) {
        $("#Grid .t-refresh").click();
    }

    function wCrudTabPacientes_OnClose(e) {
        if ($("#tab4").data("loaded")) {
            $("#tab4").empty().data("loaded", false);
        }
        $("#gridEncuestados .t-refresh").click();
    }


    function wCrudEspecialidadComentario_OnClose(e) {
        //debugger;

        if (typeof (tinyMCE.activeEditor) == "object" && tinyMCE.activeEditor != null) {
            //tinyMCE.activeEditor.setContent('');
            //tinymce.EditorManager.execCommand('mceRemoveControl', true, "comment");
            tinymce.triggerSave();
            var com = encodeURI(tinymce.activeEditor.getContent());
        }


        //AbrirWaiting();
        $.ajax({
            type: 'POST',
            url: '<%=Url.Action("GuardaComentario","catEncuestaSinAdmisionPlanilla")%>',
            dataType: 'json',
            data: { id: peId, txt: com },
            cache: false,
            sync: false,
            success: function (data, textStatus, jqXHR) {
                //Limpio tinymce
                //tinyMCE.activeEditor.setContent('');
                //tinymce.EditorManager.execCommand('mceRemoveControl', true, "comment");
            },
            error: function () {
                alert('error');
            }
        });
        //CerrarWaiting();
        $("#gridEncuestados .t-refresh").click();
    }


    function GridEncuestados_OnDataBound(e) {
        //debugger;
        //var rowData = $(e.target).data("tGrid").data;
        //var rowElement = $(e.target).data("tGrid").$rows();
        var grid = $(e.target).data("tGrid");

        $(grid.data).each(function (i) {
            //debugger;
            if (this.cantesp > 0) {
                var row = $(e.target).find(".t-grid-content tr")[i];
                $(row).find(".t-grid-cmdEspecialidades>span").css("background-position", "-64px -240px").prop("title", "Cargar Especialidades");

            }
        });
        //if (this.cantesp > 0) {
        //    debugger;
        //    var row = $(e.target).find(".t-grid-content tr")[i];
        //    $(row).find(".t-grid-cmdEspecialidades>span").css("background-position", "-64px -240px").prop("title", "Cargar Especialidades");
        //}

    }

    function onEditEncuesta(e) {
        if (e.mode === "edit") {
            var pacObj = {};
            pacObj.value = e.dataItem.depEncId;
            pacObj.locId = e.dataItem.locEncId;
            pacObj.barId = e.dataItem.barEncId;
            depEncIdOnChange(pacObj);
        }
    }

    function searchIndexOnComboBox(cb, id) {
        if (!id) return 0;
        for (var i = 0; i < cb.data.length; i++) {
            if (Number(cb.data[i].Value) === id) {
                return i;
            }
        }
        return 0;
    }
    function onSaveEspecialidades(e) {
        debugger;

        //agrega planilla

        if (typeof (tinyMCE.activeEditor) == "object" && tinyMCE.activeEditor != null) {

            tinymce.triggerSave();
            var com = encodeURI(tinymce.activeEditor.getContent());
            e.values.comentario = com;
            
        }

        var values = e.values;
        values.encId = _encId;
    }
    function depEncIdOnChange(e) {
        $.post(window.GetPathApp("catPaciente/BindingLocalidades"), { depId: e.value }, function (locData) {
            var cbLocId = $("#locEncId").data("tComboBox");
            cbLocId.dataBind(locData);
            var locSelectedIndex = e.locId ? searchIndexOnComboBox(cbLocId, e.locId) : 0;
            cbLocId.select(locSelectedIndex);
            $.post(window.GetPathApp("catPaciente/BindingBarrios"), { depId: e.value }, function (barData) {
                var cbBarId = $("#barEncId").data("tComboBox");
                cbBarId.dataBind(barData);
                var barSelectedIndex = e.barId ? searchIndexOnComboBox(cbBarId, e.barId) : 0;
                cbBarId.select(barSelectedIndex);
            });
        });
    }

    //funcion escencial para el funcionamiento segun mi compañero
    function onChangeDatePicker(e) {
        //debugger;
        Refrescar();
    }


    //funcion para colorear en rojo los resultados de la columna de no resueltos
    function onRow() {
        $('.t-last').each(function () {
            if ($(this).text() != 0) {
                $(this).addClass('enRojo');
            }

        });
    }

    //parsea fechas de string a Date
    function parseDate(input) {
        var parts = input.split('/');
        var result = new Date(parts[2] + '-' + parts[1] + '-' + parts[0]);
        result.setDate(result.getDate() + 1);
        return result;

    }

    //verificacion de especialidades repetidas
    function verificarEspecialidadRepetida(e) {

        var _especialidad = $("#espId").val();

        //fechaPlanilla = $("#WEncuestados > div.t-window-titlebar.t-header > span").text().substr($("#WEncuestados > div.t-window-titlebar.t-header > span").text().indexOf("Fecha:") + 6, 10);

        //llamada ajax a controlador para buscar la fecha de la planilla
        $.ajax({
            type: 'POST',
            url: '<%=Url.Action("fechaDePlanilla","catEncuestaSinAdmisionPlanilla")%>',
            //dataType: 'json',
            data: { numero: _plaId },
            cache: false,
            async: false,
            success: function (data) {
                fechaPlanilla = data.substr(0, 10);
            },
            error: function () {
                console.log("No se pudo acceder al controlador o a la base de datos");
            }
         });
        debugger;

        //console.log(fechaPlanilla)
        //cuando agregan
        if (e.mode == "insert") {

            var verificacionAgregar = false;

            if ($("#programado-input").val() == 'SI') {

                if ($("#programadoCuando").val() != '') {

                    var fechaPlanillaParseada = parseDate(fechaPlanilla);

                    var fechaPlanillaAgregar = $("#programadoCuando").val();

                    fechaPlanillaAgregar = parseDate(fechaPlanillaAgregar);

                    if ((fechaPlanillaAgregar != 'Invalid Date') && (typeof (fechaPlanillaAgregar) != 'undefined')) {

                        if (fechaPlanillaParseada < fechaPlanillaAgregar) {
                            verificacionAgregar = true;
                        }
                    }
                }
            } else {
                verificacionAgregar = true;
            }

            //if (($("#derivado-input").val() == 'NO')) {
            //    verificacionAgregar = true
            //} else {

            //    if ((fechaPlanillaAgregar != 'Invalid Date')&&(typeof(fechaPlanillaAgregar)!= 'undefined')) {

            //        if (fechaPlanillaParseada < fechaPlanillaAgregar) {
            //            verificacionAgregar = true;
            //        }
            //    }
            //}



            if (verificacionAgregar) {


                $.ajax({
                    type: 'POST',
                    url: '<%=Url.Action("verificarEspecialidad","catEncuestaSinAdmisionPlanilla")%>',
                    //dataType: 'json',
                    data: { especialidad: _especialidad, paciente: _pacId },
                    cache: false,
                    sync: false,
                    success: function (data) {


                        if (data == 1) {
                            $("#gridEspecialidadesform > div > a.t-button.t-grid-insert.t-button-icon").trigger('click');
                        } else
                            errorMsg();
                    },
                    error: function () {
                        console.log("No se pudo acceder al controlador o a la base de datos");
                    }
                });

            } else {

                jAlert('La fecha no debe ser menor o igual a la fecha de la planilla', '¡Atención!', function () {
                });
                $("#programadoCuando").css("background", "orange");
            }

        }


        //cuando editan
        if (e.mode == 'edit') {

            var verificacionEditar = false;

            if ($("#programado-input").val() == 'SI') {

                if ($("#programadoCuando").val() != '') {

                    var fechaPlanillaParseada = parseDate(fechaPlanilla);

                    var fechaPlanillaAgregar = $("#programadoCuando").val();

                    fechaPlanillaAgregar = parseDate(fechaPlanillaAgregar);

                    if ((fechaPlanillaAgregar != 'Invalid Date') && (typeof (fechaPlanillaAgregar) != 'undefined')) {

                        if (fechaPlanillaParseada < fechaPlanillaAgregar) {
                            verificacionEditar = true;
                        }
                    }
                }
            } else {
                verificacionEditar = true;
            }


            //if ($("#programadoCuando").val() != '') {

            //    var fechaPlanillaParseada = parseDate(fechaPlanilla);

            //    var fechaPlanillaAgregar = $("#programadoCuando").val();

            //    fechaPlanillaAgregar = parseDate(fechaPlanillaAgregar);

            //}


            //if (($("#derivado-input").val() == 'NO')) {
            //    verificacionEditar = true
            //} else {

            //    if ((fechaPlanillaAgregar != 'Invalid Date') && (typeof (fechaPlanillaAgregar) != 'undefined')) {

            //        if (fechaPlanillaParseada < fechaPlanillaAgregar) {
            //            verificacionEditar = true;
            //        }
            //    }
            //}


            if (verificacionEditar) {

                $.ajax({
                    type: 'POST',
                    url: '<%=Url.Action("verificarEspecialidad","catEncuestaSinAdmisionPlanilla")%>',
                    //dataType: 'json',
                    data: { especialidad: _especialidad, paciente: _pacId },
                    cache: false,
                    sync: false,
                    success: function (data) {

                        //verificación para centro de salud
                        if ($("#programadoEn").val() != '') {

                            if (especialidadElegida == $("#espId").val()) {

                                $(".t-button.t-grid-update.t-button-icon").trigger('click');

                            } else {

                                if (data == 1) {
                                    $(".t-button.t-grid-update.t-button-icon").trigger('click');
                                } else
                                    errorMsg();
                            }

                        } else {

                            jAlert('Debe seleccionar un centro de salud', '¡Atención!', function () {
                            });
                            $("#programadoEn-input").css("background", "orange");
                        }

                    },
                    error: function () {
                        console.log("No se pudo acceder al controlador o a la base de datos");
                    }


                });

            } else {

                jAlert('La fecha no debe ser menor o igual a la fecha de la planilla', '¡Atención!', function () {
                });
                $("#programadoCuando").css("background", "orange");
            }



            

        }



        //mensaje de error
        function errorMsg() {
            jAlert('La especialidad ya está seleccionada anteriormente', '¡Atención!', function () {
            });
            $("#espId-input").css("background", "orange");
        }

        //alert($("#programadoCuando"));
    }



</script>



<!-- listado de Encuestas -->
<% 
    string _Style = "";
    _Style = "<span class='t-window-title'></span>";

    Html.Telerik().Grid<GeDoc.catEncuestasSinAdmisionPlanilla>()
        .Name("Grid")

        .DataKeys(keys =>
        {
            keys.Add(p => p.plaId);
        })

        .Localizable("es-AR")
        .HtmlAttributes(new { style = _Style })

        .ToolBar(commands =>
        {
            commands.Template(temp =>
                {
%>
<div class="t-button" onclick="onCommandEncuesta('Agregar');" style="display: <%= (Session["Permisos"] as Acciones).Visibilidad("catEncuestaSinAdmisionPlanilla", "Agregar")%>">
    <span class="t-icon" style="background: url('<%= _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -49px -321px;"></span>
    Agregar
</div>


<%
                });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catEncuestaSinAdmisionPlanilla")
                .Insert("_InsertEditing", "catEncuestaSinAdmisionPlanilla")
                .Update("_SaveEditing", "catEncuestaSinAdmisionPlanilla");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image)
                .ImageHtmlAttributes(new {title = "Editar" });
                commands.Custom("cmdPersonas")
                .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .Text("Esp.")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -208px;", title = "Agregar persona" });
                commands.Custom("PlanillaProcesar")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)

                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General" + "/planilla.png'); background-size: 16px; ", title = "Procesar planilla" });

                commands.Custom("generarReporte")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General" + "/report.png'); background-size: 16px; ", title = "Generar reporte" });
                    
               
            }).Width("12%").Title("Acciones");
            columns.Bound(c => c.plaId).Width("4%").Title("Nro.").Visible(true);
            columns.Bound(c => c.plaFechaPlanilla).Width("10%").Title("Fecha de Encuesta").Visible(true);
            columns.Bound(c => c.centroSaludTexto).Width("10%").Title("Centro de Salud").Visible(false);
            columns.Bound(c => c.usrNombre).Width("20%").Title("Usuario").Visible(true);
            columns.Bound(c => c.encuestadorApyNom).Width("30%").Title("Encuestador").Visible(true);
            columns.Bound(c => c.encuestadorId).Width("2%").Title("encuestadorId").Visible(false);
            columns.Bound(c => c.cantPacientes).Width("10%").Title("Cant. Personas").Visible(true)
                .HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.resuelto).Width("10%").Title("No resueltos").Visible(true)
                .HtmlAttributes(new { style = "text-align: center;" });
            
         
        })
            .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .ClientEvents(events => events
                .OnCommand("onCommandEncuesta")
                .OnRowSelect("onRowSelectedEncuesta")
                .OnDataBinding("onDataBindingEncuesta")
                .OnEdit("onEditEncuesta")
                .OnComplete("onCompleteEncuesta")
                .OnRowDataBound("onRow")
                
                )
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => 
                scroll.Enabled(true)
                      .Height(((int)Session["AlturaGrilla"]))
             )
             .Resizable(resizing => resizing.Columns(true))
             .Sortable()
             .Render();
             
%>

<!-- Personas Encuestadas -->

<% Html.Telerik().Window()
        .Name("WEncuestados")
        .Title("Personas Encuestadas pertenecientes a la encuesta Nro ")
        .Visible(false)
        .Modal(true)
        .ClientEvents(e => e.OnOpen("OnOpenEncuestados")
                    .OnClose("OnCloseEncuestados"))
        .Content(() =>
        {%>
          
<% Html.Telerik().Grid((IEnumerable<GeDoc.catEncuestaSinadmisionPersonas>)ViewData["Encuestados"])
             .Name("gridEncuestados")
             .DataKeys(keys =>
             {
                 keys.Add(p => p.encId);
             })
             .ToolBar(commands =>
             {
                 commands.Custom().Ajax(true).Name("cmdAgregarPersonas").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar Persona");
                 //commands.Custom().Name("cmdEspecialidades")
                 //    .Ajax(true)
                 //    .ButtonType(GridButtonType.Text)
                 //    .Text("Especialidades")
                 //    .ImageHtmlAttributes(new {style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -208px;"});
                 ////.HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("cat", "Modificar"), title = "Gestionar Especialidades del Paciente seleccionado" });
             })
         .DataBinding(dataBinding =>
         {
             dataBinding.Ajax()
                 .Select("_SelectEditingEncuestasSinAdmisionPersonas", "catEncuestaSinAdmisionPlanilla")
                 .Delete("_DeleteEditingEncuestaSinAdmisionPersonas", "catEncuestaSinAdmisionPlanilla");
         })
         .Localizable("es-AR")
         .Columns(columns =>
         {
             columns.Command(commands =>
           {
               commands.Custom("cmdEditarEncuestado")
                .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -290px;", title = "Editar Encuestado" })
                    .SendState(true)
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.pacId)
                            .RouteKey("pacId");
                    });
               commands.Delete()
                  .ButtonType(GridButtonType.Image)
                  .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as Acciones).Visibilidad("catEncuestaSinAdmisionPlanilla", "Eliminar") });
              
               commands.Custom("cmdEspecialidades")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .Text("Esp.")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -208px;" });
               
               
           }).Width("200px").Title("Acciones");
             columns.Bound(c => c.ApellidoyNombre).Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
             columns.Bound(c => c.tipoDescripcion).Width("85px").Title("Resuelto").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + _PathContent + "/Estados/<#= AtendidoLocal #>' title='<#= tipoDescripcion #>' height='22px' width='22px' style='vertical-align:middle;' /></div>");
             columns.Bound(c => c.Documento).Title("Numero Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
             columns.Bound(c => c.cantesp).Title("Especialidad").Visible(false).HtmlAttributes(new { style = "white-space: nowrap;" });
             columns.Bound(c => c.causaNombre).Title("Causa").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
         })
        .Editable(editing => editing.Enabled(true)
        .Mode(GridEditMode.PopUp))
        .Pageable((paging) => paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
        .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandEncuestado")
                                                  .OnRowSelect("onRowSelectedEncuestados")
                                                  .OnDataBinding("onDataBindingEncuestados")
                                                  .OnEdit("onEditEncuestados")
                                                  .OnDataBound("GridEncuestados_OnDataBound"))
        .Footer(true)
        .Selectable()
         .Filterable()
        .Scrollable(scroll => scroll.Enabled(true).Height(510))
        .Resizable(resizing => resizing.Columns(true))
        .Render();
        })
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1100)
       .Height(630)
       .Render();
%>


<!-- window CRUD tabpaciente -->
<% Html.Telerik().Window()
        .Name("wCrudTabPacientes")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        { })
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Modal(true)
       .ClientEvents(eventos => eventos.OnClose("wCrudTabPacientes_OnClose"))       
       .Scrollable(true)
       .Width(640)
       .Height(410)
       .Render();
%>


<!-- window CRUD tabpaciente comentario -->
<% Html.Telerik().Window()
        .Name("wCrudEspecialidadComentario")
        .Title("Comentarios")
        .Visible(false)
        .Content(() =>
        { 
          %> <textarea id="comment" name="comment"></textarea> <%
        })
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Modal(true)
       .ClientEvents(eventos => eventos.OnClose("wCrudEspecialidadComentario_OnClose"))       
       .Scrollable(true)
       .Width(620)
       .Height(550)
       .Render();
 %>
<!-- Especialidades -->
<% Html.Telerik().Window()
        .Name("CRUDEspecialidades")
        .Title("Especialidades")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catEncuestaSinadmisionPersonasEspecialidades>)ViewData["Especialidades"])
            .Name("gridEspecialidades")
            .DataKeys(keys =>
            {
                keys.Add(p => p.peId);
            })
            .Localizable("es-AR")
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblNombrePersona3" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_BindingEspecialidades", "catEncuestaSinAdmisionPlanilla", new { encId = 0 })
                    .Update("_SaveEditingEspecialidades", "catEncuestaSinAdmisionPlanilla")
                    .Insert("_InsertEditingEspecialidades", "catEncuestaSinAdmisionPlanilla", new { encId = 0 })
                    .Delete("_DeleteEditingEspecialidades", "catEncuestaSinAdmisionPlanilla");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.peId).Width("10%").Title("ID").Visible(false);
                columns.Bound(c => c.peEspecialidades.espCodigo).Width("10%").Title("Código").Visible(true);
                columns.Bound(c => c.peEspecialidades.espNombre).Width("15%").Title("Especialidad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.derivado_desc).Width("10%").Title("Derivado").Visible(true); //.HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.interconsulta_desc).Width("10%").Title("Interconsulta").Visible(true); //.HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.atendidoLocalDescripcion).Width("10%").Title("Atendido Local").Visible(true);
                columns.Bound(c => c.derDescripcion).Width("10%").Title("Programó Turno").Visible(true);
                columns.Bound(c => c.programadoEnNombre).Width("10%").Title("Progamado En").Visible(true);
                columns.Bound(c => c.programadoCuando).Width("10%").Title("Progamado Cuando").Visible(true);
                columns.Bound(c => c.comentario).Width("5%").Title("Comentarios").Visible(true);
                //columns.Bound(c => c.comentario).Width("20%").Title("Progamado Cuando").Visible(true);
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => 
                clientEvents.OnDataBinding("onDataBindingEspecialidades")
                .OnEdit("onCommandEditEspecialidades")
               // .OnRowSelect("onRowSelectedEspecialidades")
                .OnSave("onSaveEspecialidades")
                
                .OnComplete("onCompleteEspecialidades")
                
                )
           // .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
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
       .Render();
%>


<style type="text/css" xml:lang="es-AR">
    input[type="text"] {
        width: 200px;
        border: 1px solid #CCC;
        height: 17px;
        padding: 3px;
        text-transform: uppercase;
    }
</style>

<!-- estilo para colorear los resultados de la columna de no resueltos -->
<style>

    .enRojo {
        color: red;
        font-weight: bold;
    }


    hr.style-two {
    border: 0;
    height: 1px;
    margin: 10px,0px,0px,0px;
    background-image: linear-gradient(to right, rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0));
}

    .editor-field {
        width: 200px !important;
    }
    /*
    #gridEspecialidadesPopUp {
        width: 500px;
    }
    */
    .t-edit-form-container {
        width: 500px;
        margin-left: 10px;
    }
</style>

