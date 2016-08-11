<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc.Models" %>
<script type="text/javascript">
    var idPersona = -1;
    var _RowIndex = -1;
    var _DatosRegistro_catPersona;
    var tvValores = "";
    var _Nombre = "";
    var _RowIndexHorario = -1;
    var _DatosRegistroHorario;
    var _EsModificar = false;
    var _RowIndexCertificado = -1;
    var _DatosRegistroCertificado;
    var _WindowCRUD;

    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        debugger;
        _DatosRegistro_catPersona = dataItem;
        idPersona = dataItem.perId;
       _Nombre = dataItem.perApellidoyNombre;
    }
    function onRowSelectedHorario(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndexHorario = e.row.rowIndex;
        _DatosRegistroHorario = dataItem;
    }
    function onRowSelectedCertificado(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndexCertificado = e.row.rowIndex;
        _DatosRegistroCertificado = dataItem;
    }
    function onCompletePersona(e) {
        debugger;
        if (e.name == "Consultar") {
            VerLegajo(e.response.InfoPersona);
        }
    }

    function VerLegajo(DatosPersona) {
        var detailWindow = $("#wConsulta").data("tWindow");
        var _Persona = DatosPersona;
        var _Foto = "";

        if (_Persona.perFoto == null) {
            if (_Persona.tipoIdSexoTexto == 'Femenino') {
                _Foto = GetPathApp('Content/General/Mujer.jpg');
            }
            else {
                _Foto = GetPathApp('Content/General/Hombre.jpg');
            }
        }
        else {
            _Foto = GetPathApp('Content/Archivos/FotosPersonal/' + _Persona.perFoto);
        }

        idPersona = _Persona.perId;

        $('#imgFoto').attr('src', _Foto);
        $("#lblApellidoyNombre").text(_Persona.perApellidoyNombre);
        $("#lblperPadron").text(_Persona.perPadron);
        $("#lblperDNI").text(_Persona.perDNI);
        $("#lblperCUIL").text(_Persona.perCUIL);
        $("#lbltipoIdSexo").text(_Persona.tipoIdSexoTexto);
        $("#lblperFechaNacimiento").text(_Persona.perFechaNacimientoTexto);
        $("#lblperEdad").text(_Persona.perEdad);
        $("#lblperTelefono").text(_Persona.perTelefono);
        $("#lblperEmail").text(_Persona.perEmail);
        $("#lblperAntiguedad").text(_Persona.perAntiguedad);
        $("#lblperAntiguedadPago").text(_Persona.perAntiguedadPago);
        $("#lblperAntiguedadVacaciones").text(_Persona.perAntiguedadVacaciones);
        $("#lblperEsContratado").text(_Persona.perEsContratado ? 'SI' : 'NO');
        $("#lblsecNombre").text(_Persona.secNombre);
        $("#lblOficina").text(_Persona.Oficina.ofiNombre);
        $("#lblperObservaciones").text(_Persona.perObservaciones == null ? "" : _Persona.perObservaciones);
        $("#lblcarDescripcion").text(_Persona.carDescripcion);
        $("#lblEstadoCivil").text(_Persona.perEstadoCivil);
        $("#lblprovNombre").text(_Persona.provNombre);
        $("#lblpaisNombre").text(_Persona.paisNombre);
        $("#lblperFechaCasado").text(_Persona.perFechaCasamientoTexto);
        $("#lblperCelular").text(_Persona.perCelular);
        $("#lblprofNombre").text(_Persona.profNombre);
        $("#lblperLeeoEscribe").text(_Persona.perLeeoEscribe ? 'SI' : 'NO');
        $("#lblperDomicilio").text(_Persona.perDomicilio);
        $("#lblRecibeSMS").text(_Persona.perAutorizaNotificarSMS ? 'SI' : 'NO');
        $("#lblperIdiomas").text(_Persona.perIdiomas);

        var estGrid = $('#gridLegajoEstados').data('tGrid');
        estGrid.rebind();
        var asGrid = $('#gridAsistencia').data('tGrid');
        asGrid.rebind();

        detailWindow.center().open();
    }
    function isDefined(variable) {
        return (typeof (window[variable]) == "undefined") ? false : true;
    }
    function onCommand(e) {
        _EsModificar = false;
        if (("cmdEstados, cmdIdiomas, cmdGrupoFamiliar, cmdEspecialidades, cmdEditarPersona, cmdEliminarPersona, cmdHorarios, Consultar, cmdCertificados, cmdDesignacion, cmdAdjuntos").indexOf(e.name) >= 0) {
            if (idPersona < 0) {
                jAlert('Debe seleccionar una persona.', 'Error...');
                return;
            }
        }
        if (("cmdEditarHorario, cmdEliminarHorario, cmdActivarHorario").indexOf(e.name) >= 0) {
            if (_RowIndexHorario < 0) {
                jAlert('Debe seleccionar un Horario.', 'Error...');
                return;
            }
        }
        var estaDefinido = isDefined(_DatosRegistro_catPersona);
        if (estaDefinido) {
            _Nombre = _DatosRegistro_catPersona.perApellidoyNombre;
        }
        else {
            _Nombre = "";
        }
        debugger;
        switch (e.name) {
            case "cmdAgregar":
                var grid = $(this).data("tGrid");
                grid.addRow();
                break;
            case "Consultar":
                AbrirWaiting();
                $.post(GetPathApp('catPersona/ViewDetails'), { personaId: idPersona }, function (data) {
                    if (data.InfoPersona != null) {
                        CerrarWaiting();
                        VerLegajo(data.InfoPersona);
                    }
                });
                break;
            case "cmdCertificados":
                if (_DatosRegistro_catPersona.perEstado.indexOf("Baja") > -1) {
                    jAlert('No se puede Emitir un certificado de trabajo a empleados de Baja.', 'Error...');
                    return;
                }
                var _WindowCertificado = $("#CRUDPersonaCertificados").data("tWindow");
                var cerGrid = $('#gridPersonaCertificados').data('tGrid');
                cerGrid.rebind();
                _WindowCertificado.center().open();
                break;
            case "cmdGrados":
                //var graGrid = $('#gridGrados').data('tGrid');
                //if (_DatosRegistro_catPersona.perEstado.indexOf("Baja") > -1) {
                //    jAlert('No se puede Designar Grados a Empleados de Baja.', 'Error...');
                //    return;
                //}
                $("#lblPersonaGrados").text(_Nombre);
                debugger;
                var _WindowGrados = $("#wGrados").data("tWindow");
                _WindowGrados.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
                if (_DatosRegistro_catPersona.perPertenecePlantaAdministrativa) {
                    _WindowGrados.title("Administración de Cargos");
                    _WindowGrados.ajaxRequest(GetPathApp("catPersona/Grados"));
                    //Hasta aquí//
                } else {
                    _WindowGrados.title("Administración de Grados");
                    _WindowGrados.ajaxRequest(GetPathApp("catPersona/Grados"));
                }
                _WindowGrados.center().open();
                
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdNovedades":
                if (_DatosRegistro_catPersona.perEstado.indexOf("Baja") > -1) {
                    jAlert('No es posible Gestionar Novedades sobre Empleados de Baja.', 'Error...');
                    return;
                }
                $("#lblPersonaNovedades").text(_Nombre);
                var _WindowNovedades = $("#wNovedades").data("tWindow");
                _WindowNovedades.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
                _WindowNovedades.ajaxRequest(GetPathApp("catPersona/Novedades"));
                _WindowNovedades.center().open();

                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdAddCertificado":
                var _WindowCertificado = $("#wCertificado").data("tWindow");
                _WindowCertificado.center().open();
                $("#txtBruto").focus();
                break;
            case "cmdAddCertificadoLAR":
                jConfirm('¿Confirma la Impresión del Certificado?', "Certificado LAR...", function (r) {
                    if (r) {
                        var _Parametros = new Array();
                        _Parametros[0] = new Array(idPersona, 'idPersona');
                        InvocarReporte('rptCertificadoPersonalLAR', _Parametros);

                        $.post(GetPathApp('catPersona/setCertificadoLAR'),
                            {
                                idPersona: idPersona,
                                Antiguedad: _DatosRegistro_catPersona.perAntiguedadVacaciones
                            }, function(data) {
                                if (data.isValid) {
                                    var grid = $("#gridPersonaCertificados").data("tGrid");
                                    grid.rebind();
                                } else {
                                    jAlert('No se pudo actualizar el registro de impresión del certificado.', 'Error...');
                                    return;
                                }
                            });
                    }
                });
                break;
            case "cmdEstados":
                var estGrid = $('#gridEstados').data('tGrid');
                var _WindowEst = $("#CRUDEstados").data("tWindow");

                $("#lblNombrePersona").text(_Nombre);
                estGrid.rebind();
                _WindowEst.center().open();
                break;
            case "cmdIdiomas":
                var _tvDatos = $('#tvIdiomas').data('tTreeView');
                var _Window = $("#wIdiomas").data("tWindow");

                _tvDatos.ajaxRequest();

                _Window.title("Idiomas de " + _Nombre + "...").center().open();
                break;
            case "cmdGrupoFamiliar":
                debugger;
                var estGrid = $('#gridGrupoFamiliar').data('tGrid');
                var _WindowEst = $("#CRUDGrupoFamiliar").data("tWindow");

                $("#lblNombrePersona2").text('Grupo Familiar de ' + _Nombre);
                estGrid.rebind();
                _WindowEst.center().open();
                break;
            case "cmdEspecialidades":
                var estGrid = $('#gridEspecialidades').data('tGrid');
                var _WindowEst = $("#CRUDEspecialidades").data("tWindow");

                $("#lblNombrePersona3").text('Especialidades de ' + _Nombre);
                estGrid.rebind();
                _WindowEst.center().open();
                break;
            case "cmdEditarPersona":
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminarPersona":
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar el legajo de "' + _Nombre + '"?', "Eliminar...", function (r) {
                    if (r) {
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdHorarios":
                var estGrid = $('#gridPersonaHorarios').data('tGrid');
                var _WindowEst = $("#CRUDPersonaHorarios").data("tWindow");

                $("#lblProfesional").text('Horarios de ' + _Nombre);
                estGrid.rebind();
                _WindowEst.center().open();
                _RowIndexHorario = -1;
                break;
            case "cmdAgregarHorario":
                var grid = $("#gridPersonaHorarios").data("tGrid");
                grid.addRow();
                break;
            case "cmdEditarHorario":
                var grid = $("#gridPersonaHorarios").data("tGrid");
                var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminarHorario":
                if (!_DatosRegistroHorario.perhActivo) {
                    jAlert('Este horario ya se encuentra Inactivo.', 'Error...');
                    return;
                }
                var grid = $("#gridPersonaHorarios").data("tGrid");
                var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar el horario del día "' + _DatosRegistroHorario.perhDiaSemana + '"?', "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdActivarHorario":
                if (_DatosRegistroHorario.perhActivo) {
                    jAlert('Este horario ya se encuentra Activo.', 'Error...');
                    return;
                }
                var grid = $("#gridPersonaHorarios").data("tGrid");
                var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                jConfirm('¿Confirma Activar el Horario del día "' + _DatosRegistroHorario.perhDiaSemana + '"?', "Activar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('catPersona/_ActivaRegistroHorario'), { id: _DatosRegistroHorario.perhId }, function (data) {
                            if (data) {
                                $("#gridPersonaHorarios").data("tGrid").rebind();
                            }
                        });
                    }
                });
                break;
            case "cmdAdjuntos":
                //_DatosRegistro_catPersona = e.dataItem;
                $("#gridDocumentacion").data("tGrid").rebind();

                var windowElement = $("#wDocumentacion").data("tWindow");
                windowElement.title("Documentación adjunta al Legajo de " + _DatosRegistro_catPersona.perApellidoyNombre);
                windowElement.center();
                windowElement.open();
                break;
            case "edit":
                _EsModificar = true;
                break;
        }
    }
    function onCommandEditGrados(e) {
        $('#Resolucion').mask("99999-9999");
        //$('#gdHorasAdicional').attr('disabled', 'disabled');
        $('#Decreto').mask("99999-9999");
        $('#Resolucion').blur(function () {
            if ($('#Resolucion').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('catPersona/getExisteResolucion');
                $.ajax({
                    url: _Post,
                    data: { Resolucion: $('#Resolucion').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#Resolucion').val("");
                            $('#Resolucion').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('La Resolución que ha ingresado no existe.', '¡Atención!', function () {
                                $('#Resolucion').val("");
                                $('#Resolucion').focus();
                            });
                            $('#popup_container').focus();
                            $('#popup_ok').focus();
                        }
                    }
                });
            }
        });

        $('#Decreto').blur(function () {
            if ($('#Decreto').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('catPersona/getExisteDecreto');
                $.ajax({
                    url: _Post,
                    data: { Decreto: $('#Decreto').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#Decreto').val("");
                            $('#Decreto').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('El Decreto que ha ingresado no existe.', '¡Atención!', function () {
                                $('#Decreto').val("");
                                $('#Decreto').focus();
                            });
                            $('#popup_container').focus();
                            $('#popup_ok').focus();
                        }
                    }
                });
            }
        });

        _WindowCRUD = $("#gridGradosPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBindingGrados(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onCommandEditNovedades(e) {
        $('#Resolucion').mask("99999-9999");
        $('#Decreto').mask("99999-9999");
        $('#Expediente').mask("999-99999-9999");
        $('#Expediente').blur(function () {
            if ($('#Expediente').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('expExpediente/getExisteExpediente');
                $.ajax({
                    url: _Post,
                    data: { Comprobante: $('#Expediente').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#Expediente').val("");
                            $('#Expediente').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('El Expediente ingresado no existe.', '¡Atención!', function () {
                                $('#Expediente').val("");
                                $('#Expediente').focus();
                            });
                            $('#popup_container').focus();
                            $('#popup_ok').focus();
                        }
                    }
                });
            }
        });
        $('#Resolucion').blur(function () {
            if ($('#Resolucion').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('catPersona/getExisteResolucion');
                $.ajax({
                    url: _Post,
                    data: { Resolucion: $('#Resolucion').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#Resolucion').val("");
                            $('#Resolucion').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('La Resolución que ha ingresado no existe.', '¡Atención!', function () {
                                $('#Resolucion').val("");
                                $('#Resolucion').focus();
                            });
                            $('#popup_container').focus();
                            $('#popup_ok').focus();
                        }
                    }
                });
            }
        });

        $('#Decreto').blur(function () {
            if ($('#Decreto').val() != "") {
                AbrirWaiting();
                var _Post = GetPathApp('catPersona/getExisteDecreto');
                $.ajax({
                    url: _Post,
                    data: { Decreto: $('#Decreto').val() },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                            $('#Decreto').val("");
                            $('#Decreto').focus();
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaiting();
                        if (!data.Existe) {
                            jAlert('El Decreto que ha ingresado no existe.', '¡Atención!', function () {
                                $('#Decreto').val("");
                                $('#Decreto').focus();
                            });
                            $('#popup_container').focus();
                            $('#popup_ok').focus();
                        }
                    }
                });
            }
        });

        _WindowCRUD = $("#gridGradosPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBindingNovedades(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onDataBindingEstados(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onDataBindingAsistencia(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onSaveEstados(e) {
        var values = e.values;
        values.perId = idPersona;
    }
    function onSaveGrados(e) {
        var values = e.values;
        values.perId = idPersona;
    }
    function onSaveNovedades(e) {
        var values = e.values;
        values.perId = idPersona;
    }
    function onCompleteEstado(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            var perGrid = $('#Grid').data('tGrid');
            perGrid.ajaxRequest();
        }
    }
    function onCompleteGrados(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
        }
    }
    function onCompleteNovedades(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
        }
    }
    function onDataBindingGrupoFamiliar(e) {
        debugger;
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onSaveGrupoFamiliar(e) {
        debugger;
        var values = e.values;

        values.perId = idPersona;
    }
    function onCompleteGrupoFamiliar(e) {
        debugger;
        if (e.name == "update" || e.name == "insert") {
        }
    }
    function onDataBindingEspecialidades(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onSaveEspecialidades(e) {
        var values = e.values;

        values.perId = idPersona;
    }
    function onCompleteEspecialidades(e) {
        if (e.name == "update" || e.name == "insert") {
        }
    }

    function onDataBindingIdiomas(e) {
        if (idPersona == null) {
            return;
        }
        var _tvIdiomas = $('#tvIdiomas').data('tTreeView');

        tvValores = "";

        $.post(GetPathApp('catPersona/getIdiomas'), { idPersona: idPersona }, function (data) {
            if (data.data == null) {
                _tvIdiomas.bindTo(data);
            }
            else {
                _tvIdiomas.bindTo(data.data);
            }
        });
    }
    function onCancelar() {
        var _Window = $("#wIdiomas").data("tWindow");

        _Window.close();
    }

    function onAceptar() {
        var _Window = $("#wIdiomas").data("tWindow");
        $.post(GetPathApp('catPersona/setIdiomas'), { idPersona: idPersona, Datos: tvValores }, function (data) {
            if (data) {
                _Window.close();
            }
            else {
                $('#lblMensaje').text("Error al intentar asignar permisos...");
            }
        });
    }

    function onChequedIdiomas(e) {
        var _tvIdiomas = $('#tvIdiomas').data('tTreeView');
        var _Clave = _tvIdiomas.getItemValue(e.item).toString();
        var _Resultado = tvValores.indexOf(_tvIdiomas.getItemValue(e.item), 0);

        if (_Resultado == -1) {
            tvValores = tvValores + _Clave + "#upAutorizado:" + (e.checked ? "1" : "0") + "\n";
        }
        else {
            if (e.checked) {
                tvValores = tvValores.replace("upAutorizado:0", "upAutorizado:1");
            }
            else {
                tvValores = tvValores.replace("upAutorizado:1", "upAutorizado:0");
            }
        }
    }

    function Imprimir(e) {
        var _Parametros = new Array(new Array(idPersona, 'idPersona'));
        InvocarReporte('rptFichaPersonal', _Parametros);
    }

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    function onDataBound() {
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
    }
    function onEditHorario(e) {
        if (_EsModificar) {
            $(".check-box").each(function (index) {
                $(this).attr('disabled', 'disabled');
            });
        }

        onCommandEdit(e);
    }
    function onRowDataBound(e) {
        debugger;
        if (_RowIndex > -1) {
            onRowSelectRecord("Grid", _RowIndex);
        }
    }
    function onRowDataBoundHorario(e) {
        if (_RowIndexHorario > -1) {
            var grid = $("#gridPersonaHorarios").data("tGrid");
            var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistroHorario = grid.dataItem(tr);
        }
    }
    function onDataBindingPersonaHorario(e) {
        if (_DatosRegistro_catPersona != null) {
            e.data = $.extend(e.data, { idPersona: _DatosRegistro_catPersona.perId });
        }
    }
    function onSavePersonaHorario(e) {
        var values = e.values;

        values.perId = _DatosRegistro_catPersona.perId;
        if (e.mode == "update") {
            values.perhDiaSemana = _DatosRegistroHorario.perhDiaSemana;
        }
    }
    function onCompletePersonaHorario(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }

    function onRowDataBoundCertificado(e) {
        if (_RowIndexCertificado > -1) {
            var grid = $("#gridPersonaCertificados").data("tGrid");
            var tr = $("#gridPersonaCertificados tbody tr:eq(" + (_RowIndexCertificado + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistroCertificado = grid.dataItem(tr);
        }
    }
    function onDataBindingPersonaCertificado(e) {
        if (_DatosRegistro_catPersona != null) {
            e.data = $.extend(e.data, { idPersona: _DatosRegistro_catPersona.perId });
        }
    }
    function onCompletePersonaCertificado(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }

    function onChangeSueldo(e) {
        var _checked = $("#chkSueldos")[0];
        if (_checked.checked) {
            $("#txtBruto").data("tTextBox").enable();
            $("#txtNeto").data("tTextBox").enable();
            $("#txtBruto").focus();
        }
        else {
            $("#txtBruto").data("tTextBox").disable();
            $("#txtNeto").data("tTextBox").disable();
            $("#chkEmbargos").focus();
        }
    }

    function ImprimirCertificado(e) {
        var _checked = $("#chkSueldos")[0];

        if (_checked.checked) {
            if ($("#txtBruto").data("tTextBox").value() == null || $("#txtBruto").data("tTextBox").value() <= 0) {
                jAlert('Debe ingresar un valor correcto para el Sueldo Bruto.', 'Error...', function (r) {
                    $("#txtBruto").focus();
                });
                return;
            }

            if ($("#txtNeto").data("tTextBox").value() == null || $("#txtNeto").data("tTextBox").value() <= 0) {
                jAlert('Debe ingresar un valor correcto para el Sueldo Neto.', 'Error...', function (r) {
                    $("#txtNeto").focus();
                });
                return;
            }
        }
        if ($("#txtLugarPresentacion")[0].value.trim() == "") {
            jAlert('Debe ingresar el Lugar donde va ha presentar el Certificado.', 'Error...', function (r) {
                $("#txtLugarPresentacion").focus();
            });
            return;
        }
        if (_checked.checked) {
            var _Parametros = new Array();
            _Parametros[0] = new Array(idPersona, 'idPersona');
            _Parametros[1] = new Array($("#txtBruto").data("tTextBox").value(), 'SueldoBruto');
            _Parametros[2] = new Array($("#txtNeto").data("tTextBox").value(), 'SueldoNeto');
            _Parametros[3] = new Array($("#chkEmbargos")[0].checked, 'Embargo');
            _Parametros[4] = new Array($("#txtLugarPresentacion")[0].value.trim().toUpperCase(), 'PresentarEn');
            _Parametros[5] = new Array($("#chkImprimeEmbargo")[0].checked, 'ImprimeEmbargo');
            _Parametros[6] = new Array($("#chkImprimeGuardias")[0].checked, 'ImprimeGuardias');
            _Parametros[7] = new Array($("#chkGuardias")[0].checked, 'PoseeGuardias');
            InvocarReporte('rptCertificadoPersonal', _Parametros);
        }
        else {
            var _Parametros = new Array();
            _Parametros[0] = new Array(idPersona, 'idPersona');
            _Parametros[1] = new Array($("#chkEmbargos")[0].checked, 'Embargo');
            _Parametros[2] = new Array($("#txtLugarPresentacion")[0].value.trim().toUpperCase(), 'PresentarEn');
            _Parametros[3] = new Array($("#chkImprimeEmbargo")[0].checked, 'ImprimeEmbargo');
            _Parametros[4] = new Array($("#chkImprimeGuardias")[0].checked, 'ImprimeGuardias');
            _Parametros[5] = new Array($("#chkGuardias")[0].checked, 'PoseeGuardias');
            InvocarReporte('rptCertificadoPersonalSinSueldos', _Parametros);
        }
        debugger;
        $.post(GetPathApp('catPersona/setCertificado'),
                    {
                        idPersona: idPersona,
                        ImprimeSueldos: _checked.checked,
                        SueldoBruto: $("#txtBruto").data("tTextBox").value() == null ? 0 : $("#txtBruto").data("tTextBox").value(),
                        SueldoNeto: $("#txtNeto").data("tTextBox").value() == null ? 0 : $("#txtNeto").data("tTextBox").value(),
                        PoseeEmbargos: $("#chkEmbargos")[0].checked,
                        PresentarEn: $("#txtLugarPresentacion")[0].value.trim().toUpperCase(),
                        ImprimeEmbargos: $("#chkImprimeEmbargo")[0].checked,
                        ImprimeGuardias: $("#chkImprimeGuardias")[0].checked,
                        PoseeGuardias: $("#chkGuardias")[0].checked
                    }, function (data) {
                        if (data.isValid) {
                            var grid = $("#gridPersonaCertificados").data("tGrid");
                            grid.rebind();
                            Cancelar();
                        }
                        else {
                            jAlert('No se pudo actualizar el registro de impresión del certificado.', 'Error...');
                            return;
                        }
                    });
    }

    function Cancelar() {
        var _Window = $("#wCertificado").data("tWindow");

        _Window.close();
    }

</script>
<div style="overflow: hidden; height: 510px;" >
<% Html.RenderPartial("PersonaDocumentacion"); %>
<%= Html.Telerik().Grid<GeDoc.catPersonas>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.perId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.Image)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -49px -321px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Agregar"), title = "Agregar un Empleado" });
            commands.Custom().Ajax(true).Name("cmdEditarPersona").ButtonType(GridButtonType.Image).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Modificar"), title = "Modificar información del Empleado seleccionado" });
            commands.Custom().Ajax(true).Name("cmdEliminarPersona").ButtonType(GridButtonType.Image).Text("Eliminar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Eliminar"), title = "Eliminar el Empleado seleccionado" });
            commands.Custom().Name("cmdGrados").ButtonType(GridButtonType.Image)
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Cargos"), title = "Gestionar cargos del Empleado seleccionado" })
                .Text("Cargos")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -287px;" })
                .Ajax(true);
            commands.Custom().Name("Consultar").ButtonType(GridButtonType.Image)
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Examinar"), title = "Consultar Legajo del Empleado seleccionado" })
                .Text("Legajo")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                .Ajax(true);
            commands.Custom().Name("cmdCertificados").ButtonType(GridButtonType.Image)
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Certificados"), title = "Administrar certificados del Empleado seleccionado" })
                .Text("Certificados")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -192px;" })
                .Ajax(true);
            commands.Custom().Name("cmdEstados")
                .Ajax(true)
                .ButtonType(GridButtonType.Image)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -240px;" })
                .Text("Estados")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Alertas"), title = "Gestionar cambios de estado del Empleado seleccionado" });
            commands.Custom().Name("cmdIdiomas")
                .Ajax(true)
                .ButtonType(GridButtonType.Image)
                .Text("Idiomas")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -240px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Modificar"), title = "Administrar Idiomas del Empleado seleccionado" });
            commands.Custom().Name("cmdGrupoFamiliar")
                .Ajax(true)
                .ButtonType(GridButtonType.Image)
                .Text("Grupo Familiar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -288px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Modificar"), title = "Administrar Grupo Familiar del Empleado seleccionado" });
            commands.Custom().Name("cmdEspecialidades")
                .Ajax(true)
                .ButtonType(GridButtonType.Image)
                .Text("Especialidades")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -208px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Modificar"), title = "Gestionar Especialidades del Empleado seleccionado" });
            commands.Custom().Ajax(true).Name("cmdHorarios").ButtonType(GridButtonType.Image).Text("Horarios")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Horarios"), title = "Gestionar Horarios del Empleado seleccionado" });
            commands.Custom().Ajax(true).Name("cmdNovedades").ButtonType(GridButtonType.Image).Text("Novedades")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -256px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Novedades"), title = "Administrar Novedades del Empleado seleccionado" });
            commands.Custom().Ajax(true).Name("cmdAdjuntos").ButtonType(GridButtonType.Image).Text("Documentación")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Adjuntar.png') no-repeat 0px 0px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Novedades"), title = "Administrar Documentación del Empleado seleccionado" });
            commands.Custom().Action("Exportar", "catPersona", new { page = 1, orderBy = "~", filter = "~" }).Name("cmdExportar").ButtonType(GridButtonType.Image).Text("Exportar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -240px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Alertas"), title = "Exportar a Excel el listado de Empleados" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catPersona")
                .Insert("_InsertEditing", "catPersona")
                .Update("_SaveEditing", "catPersona")
                .Delete("_DeleteEditing", "catPersona");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.AsistenciaEstado).Width("80px").Title("Asistencia").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/<#= AsistenciaImagen #>' title='<#= AsistenciaEstado #>' height='22px' width='22px' style='vertical-align:middle;' /></div>");
            columns.Bound(c => c.perEstado).Width("70px").Title("Estado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/<#= perEstadoImagen #>' title='<#= perEstado #>' height='22px' width='22px' style='vertical-align:middle; visibility: <#= VisibilidadImagen #>;' /></div>");
            columns.Bound(c => c.perFechaEstado).Width("120px").Title("Fecha Estado").Visible(true);
            columns.Bound(c => c.ZonaSanitaria).Width("200px").Title("Zona Sanitaria").Visible((Session["Usuario"] as sisUsuario).repId == null).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= ZonaSanitaria #>' style='cursor: pointer; white-space: nowrap;' ><#= ZonaSanitaria #></label>");
            columns.Bound(c => c.secNombre).Width("200px").Title("Sector").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= secNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= secNombre #></label>");
            columns.Bound(c => c.Oficina.ofiNombre).Width("200px").Title("Lugar de Trabajo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Oficina.ofiNombre #>' style='cursor: pointer; white-space: nowrap;'><#= Oficina.ofiNombre #></label>");
            columns.Bound(c => c.perPadron).Width("80px").Title("Padrón").Visible(true);
            columns.Bound(c => c.perApellidoyNombre).Width("200px").Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= perApellidoyNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= perApellidoyNombre #> </label>");
            columns.Bound(c => c.tipoIdSexoTexto).Width("80px").Title("Sexo").Visible(true);
            columns.Bound(c => c.perDNI).Width("80px").Title("DNI").Visible(true);
            columns.Bound(c => c.perFuncion).Width("200px").Title("Función").Visible(true)
            .ClientTemplate("<label title='<#= perFuncion #>' style='cursor: pointer;' ><#= perFuncion #></label>");
            //columns.Bound(c => c.perCUIL).Width("12%").Title("CUIL").Visible(true);
            //columns.Bound(c => c.perFechaNacimiento).Width("10%").Title("Fecha Nac.").Visible(true);
            columns.Bound(c => c.perEdad).Width("54px").Title("Edad").Visible(true);
            columns.Bound(c => c.perAntiguedad).Width("150px").Title("Antigüedad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= perAntiguedad #>' style='cursor: pointer;' style='white-space: nowrap;'><#= perAntiguedad #> </label>");
            columns.Bound(c => c.perAntiguedadAnios).Width("60px").Title("Años").Visible(true);
            //columns.Bound(c => c.perEsContratado).Width("7%").Title("Contratado").Visible(true)
            //.ClientTemplate("<label style='cursor: pointer;' id='EsContratado' ><#= perEsContratado ? 'SI' : 'NO' #> </label>");
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit").OnRowSelected("onRowSelected").OnComplete("onCompletePersona").OnCommand("onCommand").OnDataBound("onDataBound").OnRowDataBound("onRowDataBound"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 100%;" })

%>
</div>
<%
Html.RenderPartial("ConsultaPersona");
%>
<!-- Estados -->
<% Html.Telerik().Window()
        .Name("CRUDEstados")
        .Title("Historial de Cambios")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catPersonasEstados>)ViewData["Estados"])
            .Name("gridEstados")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.pereId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblNombrePersona" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_BindingEstados", "catPersona", new { idPersona = 0 })
                    .Update("_SaveEditingEstados", "catPersona")
                    .Insert("_InsertEditingEstados", "catPersona")
                    .Delete("_DeleteEditingEstados", "catPersona");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);//.HtmlAttributes(new { style = "visibility: hidden" });
                    commands.Delete().ButtonType(GridButtonType.Image);//.HtmlAttributes(new { style = "margin-left: -28px" });
                }).Width("7%").Title("Acciones");
                columns.Bound(c => c.pereFecha).Width("10%").Title("Fecha").Visible(true);
                columns.Bound(c => c.perEstado).Width("15%").Title("Estado").Visible(true)
                .ClientTemplate("<label title='<#= perEstado #>' style='cursor: pointer;' id='txtEstado' style='white-space: nowrap;'><#= perEstado #> </label>");
                columns.Bound(c => c.perMotivo).Width("15%").Title("Motivo").Visible(true)
                .ClientTemplate("<label title='<#= perMotivo #>' style='cursor: pointer;' id='txtMotivo' style='white-space: nowrap;'><#= perMotivo #> </label>");
                columns.Bound(c => c.pereObservaciones).Width("15%").Title("Observaciones").Visible(true)
                .ClientTemplate("<label title='<#= pereObservaciones #>' style='cursor: pointer;' id='txtObservaciones' style='white-space: nowrap;'><#= pereObservaciones #> </label>");
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingEstados").OnEdit("onCommandEdit").OnSave("onSaveEstados").OnComplete("onCompleteEstado"))
            .Filterable()
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
       .Width(870)
       .Height(400)
       .Render();
%>

<% 
   string _btnAceptar = "";
   string _btnCancelar = "";
   _btnAceptar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -33px -335px;";
   _btnCancelar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -49px -335px;";
%>
<% Html.Telerik().Window()
        .Name("wIdiomas")
        .Title("Idiomas")
        .Visible(false)
        .Content(() => 
        {%>  
            <div style="border-style: solid; border-width: 1px; border-color: inherit; height: 98%; overflow: scroll;">
                <%= Html.Telerik().TreeView()
                                    .Name("tvIdiomas")
                                    .ShowCheckBox(true)
                                    .ShowLines(true)
                                    .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingIdiomas").OnChecked("onChequedIdiomas"))
                %>
                <div style="position: absolute; top: 10px; left: 67%; width: 30px; height: 20px">
                    <table>
                        <tr>
                            <td style="border: none;">
                                <button class="t-button" onclick="onAceptar()">
                                    <img src="<%= Url.Content("~/Content") %>/General/Vacio-Transparente.png" alt="Aceptar" height="15" width="15" style="<%: _btnAceptar %>" />
                                </button>
                            </td>
                            <td style="border: none;">
                                <button class="t-button" onclick="onCancelar()">
                                    <img src="<%= Url.Content("~/Content") %>/General/Vacio-Transparente.png" alt="Cancelar" height="15" width="15" style="<%: _btnCancelar %>" />
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
                <label id="lblMensaje" style="font-family: Calibri; font-size: medium; font-weight: bold; color: #CC3300"></label>
            </div>
        <%})
        .Buttons(b => b.Maximize().Close())
        .Draggable(true)
        .Scrollable(false)
        .Resizable()
        .Width(320)
        .Height(260)
        .Render();
%>

<!-- Grupo Familiar -->
<% Html.Telerik().Window()
        .Name("CRUDGrupoFamiliar")
        .Title("Grupo Familiar")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catPersonasGrupoFamiliar>)ViewData["GrupoFamiliar"])
            .Name("gridGrupoFamiliar")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.gfId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblNombrePersona2" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_BindingGrupoFamiliar", "catPersona", new { idPersona = 0 })
                    .Update("_SaveEditingGrupoFamiliar", "catPersona")
                    .Insert("_InsertEditingGrupoFamiliar", "catPersona")
                    .Delete("_DeleteEditingGrupoFamiliar", "catPersona");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.gfParentesco).Width("10%").Title("Parentesco").Visible(true);
                columns.Bound(c => c.gfApellidoyNombre).Width("20%").Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.gfSexo).Width("10%").Title("Sexo").Visible(true);
                columns.Bound(c => c.gfFechaNacimiento).Width("10%").Title("Nacimiento").Visible(true);
                columns.Bound(c => c.gfFechaFallecimiento).Width("12%").Title("Fallecimiento").Visible(true);
                columns.Bound(c => c.gfTipoDocumento).Width("14%").Title("Tipo documento").Visible(true);
                columns.Bound(c => c.gfNumeroDocumento).Width("14%").Title("N° documento").Visible(true);
            })
            .Editable(editing => editing
            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
            paging.Enabled(true)
            .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingGrupoFamiliar").OnEdit("onCommandEdit").OnSave("onSaveGrupoFamiliar").OnComplete("onCompleteGrupoFamiliar"))
            .Filterable()
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

<!-- Especialidades -->
<% Html.Telerik().Window()
        .Name("CRUDEspecialidades")
        .Title("Especialidades")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catPersonasEspecialidades>)ViewData["Especialidades"])
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
                    .Select("_BindingEspecialidades", "catPersona", new { idPersona = 0 })
                    .Update("_SaveEditingEspecialidades", "catPersona")
                    .Insert("_InsertEditingEspecialidades", "catPersona")
                    .Delete("_DeleteEditingEspecialidades", "catPersona");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.peEspecialidades.espCodigo).Width("10%").Title("Código").Visible(true);
                columns.Bound(c => c.peEspecialidades.espNombre).Width("20%").Title("Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingEspecialidades").OnEdit("onCommandEdit").OnSave("onSaveEspecialidades").OnComplete("onCompleteEspecialidades"))
            .Filterable()
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
<!-- Horarios -->
<% Html.Telerik().Window()
        .Name("CRUDPersonaHorarios")
        .Title("Horarios")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catPersonasHorarios>)ViewData["PersonaHorarios"])
            .Name("gridPersonaHorarios")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.perhId);
            })
            .ToolBar(commands =>
            {
                commands.Custom().Ajax(true).Name("cmdAgregarHorario").ButtonType(GridButtonType.ImageAndText)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                    .Text("Agregar");
                commands.Custom().Ajax(true).Name("cmdEditarHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Modificar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" });
                commands.Custom().Ajax(true).Name("cmdEliminarHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Eliminar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" });
                commands.Custom().Ajax(true).Name("cmdActivarHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Activar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -336px;" });
                %>
                <label id="lblProfesional" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                <%
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_BindingPersonaHorario", "catPersona", new { idPersona = 0 })
                    .Update("_SaveEditingPersonaHorario", "catPersona")
                    .Insert("_InsertEditingPersonaHorario", "catPersona")
                    .Delete("_DeleteEditingPersonaHorario", "catPersona");
            })
            .Columns(columns =>
            {
                columns.Bound(c => c.perhActivo).Width("25px").Title("").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Inactivo' height='22px' width='22px' style='vertical-align:middle; visibility: <#= !perhActivo ? \"visible\" : \"hidden\" #>' /></div>");
                columns.Bound(c => c.perhDiaSemana).Width("30px").Title("Día").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.perhHoras).Width("100px").Title("Horas").Visible(true);
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingPersonaHorario").OnEdit("onEditHorario").OnSave("onSavePersonaHorario").OnComplete("onCompletePersonaHorario").OnCommand("onCommand").OnRowDataBound("onRowDataBoundHorario").OnRowSelected("onRowSelectedHorario"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1024)
       .Height(420)
       .Render();
%>

<!-- Certificados -->
<% Html.Telerik().Window()
        .Name("CRUDPersonaCertificados")
        .Title("Emisión de Certificados")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catPersonaCertificados>)ViewData["PersonaCertificados"])
            .Name("gridPersonaCertificados")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.percId);
            })
            .ToolBar(commands =>
            {
                commands.Custom().Ajax(true).Name("cmdAddCertificado").ButtonType(GridButtonType.ImageAndText)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                    .Text("Certificado de Trabajo");
                commands.Custom().Ajax(true).Name("cmdAddCertificadoLAR").ButtonType(GridButtonType.ImageAndText)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                    .Text("Certificado para LAR");
                %>
                <label id="lblPersonaCertificado" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                <%
            })
            .Columns(columns =>
            {
                columns.Bound(c => c.Tipo.tipoDescripcion).Width("200px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= Tipo.tipoDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Tipo.tipoDescripcion #> </label>");
                columns.Bound(c => c.percFecha).Width("90px").Title("Fecha").Format("{0:dd/MM/yyyy}").Visible(true);
                columns.Bound(c => c.percFecha).Width("80px").Title("Hora").Format("{0:hh:mm:ss}").Visible(true);
                columns.Bound(c => c.percImprimeSueldos).Width("140px").Title("Imprime Sueldos").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' disabled='disabled' <#= percImprimeSueldos ? checked = 'checked' : '' #> /></div>");
                columns.Bound(c => c.percSueldoBruto).Width("120px").Title("Sueldo Bruto").Format("{0:#,##0.00}").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
                columns.Bound(c => c.percSueldoNeto).Width("120px").Title("Sueldo Neto").Format("{0:#,##0.00}").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
                columns.Bound(c => c.percImprimeEmbargos).Width("140px").Title("Imprime embargos").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' disabled='disabled' <#= percImprimeEmbargos ? checked = 'checked' : '' #> /></div>");
                columns.Bound(c => c.percPoseeEmbargos).Width("140px").Title("Posee embargos").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' disabled='disabled' <#= percPoseeEmbargos ? checked = 'checked' : '' #> /></div>");
                columns.Bound(c => c.percLugarDePresentacion).Width("200px").Title("Lugar de Presentación").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= percLugarDePresentacion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= percLugarDePresentacion #> </label>");
                columns.Bound(c => c.percImprimeGuardias).Width("190px").Title("Imprime Guardias Rotativas").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' disabled='disabled' <#= percImprimeGuardias ? checked = 'checked' : '' #> /></div>");
                columns.Bound(c => c.percPoseeGuardias).Width("190px").Title("Posee Guardias Rotativas").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' disabled='disabled' <#= percPoseeGuardias ? checked = 'checked' : '' #> /></div>");
                columns.Bound(c => c.percAntiguedadVacaciones).Width("200px").Title("Antigüedad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= percAntiguedadVacaciones #>' style='cursor: pointer;' style='white-space: nowrap;'><#= percAntiguedadVacaciones #> </label>");
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingPersonaCertificado", "catPersona", new { idPersona = 0 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingPersonaCertificado").OnComplete("onCompletePersonaCertificado").OnCommand("onCommand").OnRowDataBound("onRowDataBoundCertificado").OnRowSelected("onRowSelectedCertificado"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1024)
       .Height(420)
       .Render();
%>

<% Html.RenderPartial("CertificadoTrabajo"); %>

<% Html.Telerik().Window()
        .Name("wGrados")
        .Title("Administración de Grados")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1000)
       .Height(400)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wNovedades")
        .Title("Administración de Novedades")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1000)
       .Height(400)
       .Render();
%>
