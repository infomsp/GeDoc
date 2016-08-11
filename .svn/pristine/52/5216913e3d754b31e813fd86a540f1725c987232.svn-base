//document.write("<script type='text/javascript' src='Reportes.js'></script>");
function GetPathApp(url) {
    var fullPathApp = this.document.URL.replace('Home/Index', '');
    fullPathApp = fullPathApp.toLowerCase().replace('home/index', '');
    return fullPathApp + url;
}

//Tooltip
function onToolTip() {
    $(document).tooltip({
        position: {
            my: "center bottom-15",
            at: "center top",
            using: function (position, feedback) {
                //position.top = position.top - 10;
                $(this).css(position);
                $(this).css("z-index", "22999");
                $("<div>")
            .addClass("arrow")
            .addClass(feedback.vertical)
            .addClass(feedback.horizontal)
            .appendTo(this);
            }
        }
    });
}
//Fin de Tooltip
function onRowSelectRecord(pGrilla, pFila) {
    var grid = $("#" + pGrilla).data("tGrid");
    var tr = $("#" + pGrilla + " tbody tr:eq(" + (pFila + 1).toString() + ")");
    $("#" + pGrilla + " tbody tr.t-alt.t-state-selected").attr("class", 't-alt');
    $("#" + pGrilla + " tbody tr.t-state-selected").attr("class", 't-alt');
    tr.attr("class", 't-alt t-state-selected');
}

function onRowSelectRecordKeyUp(pFila, pSelecciona, pGrilla, pRegistro) {
    var grid = $("#" + pGrilla).data("tGrid");
    //$(grid).find('.t-state-selected').removeClass("t-state-selected");
    var tr = $("#" + pGrilla + " tbody tr:eq(" + (pFila + 1).toString() + ")");
    if (pSelecciona) {
        tr.attr("class", 't-state-selected');
    } else {
        if ((pFila % 2) == 0) {
            tr.attr("class", '');
        } else {
            tr.attr("class", 't-alt');
        }
    }
    pRegistro = grid.dataItem(tr);
    return pRegistro;
}

function onCellSelectKeyUp(pFila, pColumna, pGrilla, pFilasVisibles, pColumnasVisibles, Editable) {
    var grid = $("#" + pGrilla).data("tGrid");
    var _tr = $("#" + pGrilla + " tbody tr:eq(" + (pFila + 1).toString() + ")");
    var _td = $(_tr).find("td");
    //td.attr("class", 't-state-selected');

    $("#" + pGrilla).find('.t-state-focused').removeClass("t-state-focused");
    //_td.addClass("t-state-focused");
    _td[pColumna].className = "t-state-focused";

    if (Editable) {
        $(grid.element).find(".t-grid-edit-cell").each(function () {
            grid.saveCell(this);
        });

        var Columna = $(_tr).find("td:eq(" + pColumna.toString() + ")");
        grid.editCell(Columna);
    }
    else {
        var _ScrollV = pFila + 1 - pFilasVisibles;
        var _ScrollH = pColumna + 1 - pColumnasVisibles;
        _ScrollV = (_ScrollV < 0 ? 0 : _ScrollV) * 30;
        _ScrollH = _ScrollH < 0 ? 0 : _ScrollH;
        if (_ScrollH > 0) {
            _ScrollH = 0;
            for (var i = 0; i < pColumna; i++) {
                _ScrollH += _td[i].clientWidth;
            }
        }

        $("#" + pGrilla).find('.t-grid-content')[0].scrollTop = _ScrollV;
        $("#" + pGrilla).find('.t-grid-content')[0].scrollLeft = _ScrollH;
    }

    return;
}

function onCommandEdit(e) {
    //console.log(e);
    var _Window = $("#GridPopUp").data("tWindow");

    if (_Window == null) {
        _Window = $("#gridEstadosPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridPagosPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridNotificacionesPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridGrupoFamiliarPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridPresupuestadosPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridDesignadosPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridAgendaHorariosPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridPersonaHorariosPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#wCrudTabPacientes").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#gridPacientesPopUp").data("tWindow");
    }
    if (_Window == null) {
        _Window = $("#WFiltroListadoPacientes").data("tWindow");
    }
    if (_Window == null && _WindowCRUD != null) {
        _Window = _WindowCRUD;
    }
    if (_Window != null) {
        _Window.center();
    }

    //Oculta el botón de refresh de la grilla de carga de subpartidas.
    var _Boton = $('a.t-icon.t-refresh');
    var href = _Boton.attr('href');
    if (href == GetPathApp("proImputacion/Index")) {
        var _div = $('div.t-grid-pager.t-grid-bottom')[1];
        _div.style.display = "none";
    }

    if ($("form:not(.filter) :input:visible:enabled:first").length > 0) {
        $("form:not(.filter) :input:visible:enabled:first").focus().select();
    } else if ($("#EditarGeneral:not(.filter) :input:visible:enabled:first").length > 0) {
        $("#EditarGeneral:not(.filter) :input:visible:enabled:first").focus().select();
    }
    //    $("#GridPopUp").keyup(function (event) {
    //        switch (event.which) {
    //            case 27:
    //                _Window.close();
    //                event.preventDefault();
    //                event.stopPropagation();
    //                return;
    //            case 38:
    //                event.preventDefault();
    //                event.stopPropagation();
    //                Retroceder(this);
    //                break;
    //            case 40:
    //                event.preventDefault();
    //                event.stopPropagation();
    //                Avanzar(this);
    //                break;
    //        }
    //    });

    //var _Fondo = $("#fpId").data("tComboBox");
    //if (_Fondo != null) {
    //    _Fondo.disable();
    //}

    var _pagMotivo = $("#pagMotivo")[0];
    if (_pagMotivo != null) {
        _pagMotivo.disabled = true;
    }

    $(':input').bind('keypress', function (e) {
        if (e.which == 13) {
            e.preventDefault();
            e.stopPropagation();
            Avanzar(this);
        }
    });

    if ($(e.target).data("tGrid").ajax.insertUrl.indexOf("proResolucion")) {
        $(e.form).find(".t-button.t-button-icon").css("display", "");
    }
}

function Retroceder(objeto) {
    var _TextBox = $(':input').get(($(':input').index($(objeto))) - 2);
    if (_TextBox != null) {
        if (_TextBox.baseURI.indexOf("catFactura") > -1) {
            if (_TextBox.name.indexOf("sucId") == -1 && _TextBox.name.indexOf("osId") == -1 && _TextBox.name.indexOf("facPeriodoMes") == -1 && _TextBox.name.indexOf("facPeriodoAnio") == -1) {
                _TextBox = $(':input').get(($(':input').index($(objeto))) - 1);
            }
        }
        else {
            _TextBox = $(':input').get(($(':input').index($(objeto))) - 1);
        }
    }
    else {
        _TextBox = $(':input').get(($(':input').index($(objeto))) - 1);
    }

    if (_TextBox == null) {
        var _Boton = $("a.t-button.t-grid-update.t-button-icon");
        if (_Boton != null) {
            if (_Boton.length == 0) {
                _Boton = $("a.t-button.t-grid-insert.t-button-icon");
            }
            if (_Boton != null) {
                if (_Boton.length > 0) {
                    _Boton.focus();
                }
            }
            return;
        }
    }

    _TextBox.focus();
    _TextBox.select();
}


function Avanzar(objeto) {
    var _TextBox = $(':input').get(($(':input').index($(objeto))) + 1);
    var _TextBoxOri = $(':input').get(($(':input').index($(objeto))));
    if (_TextBox != null) {
        if ((_TextBox.className == "" && _TextBox.localName != "input") || _TextBox.className == "input-validation-error") {
            _TextBox = $(':input').get(($(':input').index($(objeto))) + 2);
        }
        var i = 1;
        while (true) {
            if (_TextBox.disabled || _TextBoxOri.id.indexOf(_TextBox.id) >= 0) {
                _TextBox = $(':input').get(($(':input').index($(objeto))) + i);
                if (_TextBox == null) {
                    break;
                }
                i = i + 1;
            }
            else {
                break;
            }
        }
    }

    if (_TextBox == null) {
        var _Boton = $("a.t-button.t-grid-update.t-button-icon");
        if (_Boton != null) {
            if (_Boton.length == 0) {
                _Boton = $("a.t-button.t-grid-insert.t-button-icon");
            }
            if (_Boton != null) {
                if (_Boton.length > 0) {
                    _Boton.focus();
                }
                else {
                    _Boton = $("a.t-button.t-grid-insert");
                    if (_Boton != null) {
                        if (_Boton.length > 0) {
                            _Boton.focus();
                        } else {
                            _Boton = $(".GeDoc-Boton-Aceptar");
                            if (_Boton != null) {
                                if (_Boton.length > 0) {
                                    _Boton.focus();
                                }
                            }
                        }
                    }
                }
            }
            return;
        }
    }

    _TextBox.focus();
    _TextBox.select();
}

//-- Evento onChangeComboBoxCRUD --\\
function onComboBoxLoadCRUD() {
    $(this).data("tComboBox").fill();
}

function onErrorCRUD(args) {
    if (args.textStatus == "parsererror") {
        args.preventDefault();
    }
    if (args.textStatus == "error") {
        CerrarWaiting();
        jAlert("No se puede eliminar este registro por que tiene asociado registros.", '¡Atención!', function () {
        }
        );
    }
}

//-- Controlamos el tipo de comprobante --\\
function onChangeComboBoxCRUD() {
    var _comboBox = $(this).data("tComboBox");
    if (_comboBox != null) {
        switch (_comboBox.$element[0].id) {
            case "depEncId":
                var _Localidad = $("#locEncId").data("tComboBox");
                var _Barrio = $("#barId").data("tComboBox");
                var _Ctro = $("#csId").data("tComboBox");
                var e = window.event;
                if (e.keyCode == 9) {
                    //e.preventDefault();
                    e.stopPropagation();
                    // Avanzar(this);
                }
                else {
                    AbrirWaiting();
                    var _Post = GetPathApp('catPaciente/BindingLocalidades');
                    $.post(_Post, { depId: _comboBox.value() }, function (data) {
                        CerrarWaiting();
                        if (data != null) {
                            _Localidad.dataBind(data.data);
                            if (data.total < 1) {
                                _Localidad.select(0);
                                _Localidad.disable();
                                $("#depEncId-input").focus();
                            }
                            else {
                                _Localidad.enable();
                                _Localidad.select();
                                $("#locEncId-input").focus();
                            }
                        }
                    });

                    var _Post = GetPathApp('catPaciente/BindingBarrios');
                    $.post(_Post, { depId: _comboBox.value() }, function (data) {
                        // CerrarWaiting();
                        if (data != null) {
                            _Barrio.dataBind(data.data);
                            if (data.total < 1) {
                                _Barrio.select(0);
                                _Barrio.disable();
                            }
                            else {
                                _Barrio.enable();
                                _Barrio.select();
                            }
                        }
                    });
                    //var _Post = GetPathApp('catPaciente/BindingCtrosDeSalud');
                    //$.post(_Post, { depId: _comboBox.value() }, function (data) {
                    //    // CerrarWaiting();
                    //    if (data != null) {
                    //        _Ctro.dataBind(data.data);
                    //        if (data.total < 1) {
                    //            _Ctro.select(0);
                    //            _Ctro.disable();
                    //        }
                    //        else {
                    //            debugger;
                    //            _Ctro.enable();
                    //            _Ctro.select();
                    //        }
                    //    }
                    //});
                }
                break;
            case "depaId":
                var _Localidad = $("#locId").data("tComboBox");
                var _Barrio = $("#barId").data("tComboBox");
                var _Ctro = $("#csId").data("tComboBox");
                var e = window.event;
                if (e.keyCode == 9) {
                    //e.preventDefault();
                    e.stopPropagation();
                    // Avanzar(this);
                }
                else {
                    AbrirWaiting();
                    var _Post = GetPathApp('catPaciente/BindingLocalidades');
                    $.post(_Post, { depId: _comboBox.value() }, function (data) {
                        CerrarWaiting();
                        if (data != null) {
                            _Localidad.dataBind(data.data);
                            if (data.total < 1) {
                                _Localidad.select(0);
                                _Localidad.disable();
                                $("#depId-input").focus();
                            }
                            else {
                                _Localidad.enable();
                                _Localidad.select();
                                $("#locId-input").focus();
                            }
                        }
                    });

                    var _Post = GetPathApp('catPaciente/BindingBarrios');
                    $.post(_Post, { depId: _comboBox.value() }, function (data) {
                        // CerrarWaiting();
                        if (data != null) {
                            _Barrio.dataBind(data.data);
                            if (data.total < 1) {
                                _Barrio.select(0);
                                _Barrio.disable();
                            }
                            else {
                                _Barrio.enable();
                                _Barrio.select();
                            }
                        }
                    });
                    var _Post = GetPathApp('catPaciente/BindingCtrosDeSalud');
                    $.post(_Post, { depId: _comboBox.value() }, function (data) {
                        // CerrarWaiting();
                        if (data != null) {
                            _Ctro.dataBind(data.data);
                            if (data.total < 1) {
                                _Ctro.select(0);
                                _Ctro.disable();
                            }
                            else {
                                _Ctro.enable();
                                _Ctro.select();
                            }
                        }
                    });
                }
                break;

            case "pacNumeroDocumento":
                var _PacNumDoc = $("#pacNumeroDocumento").data("tComboBox");
                var _ApyNomb = $("#pacId").data("tComboBox");
                _ApyNomb.disable();
                if (_comboBox.value() > 0) {
                    _ApyNomb.disable();

                    //_CE.enable();
                }
                else {
                    _ApyNomb.disable();
                }
                break;
            case "tcoId":
                var _Fondo = $("#fpId").data("tComboBox");
                var _CE = $("#ceId").data("tComboBox");
                if (_comboBox.text() === "Expediente") {
                    $("#impComprobante").attr("title", 'El formato correcto del comprobante es, por ejemplo: "800-1234-2014"');
                    _Fondo.disable();
                    _CE.enable();
                }
                else {
                    $("#impComprobante").attr("title", '');
                    _Fondo.enable();
                    _CE.disable();
                }
                break;
            case "tipoId":
                if ($("#tipoIdForma").data("tComboBox") != null) {
                    var _pagNumeroRecibo = $("#pagNumeroRecibo")[0];
                    var _tipoIdForma = $("#tipoIdForma").data("tComboBox");
                    var _pagDetalle = $("#pagDetalle")[0];
                    var _pagMotivo = $("#pagMotivo")[0];

                    _pagNumeroRecibo.disabled = (_comboBox.value() == 11);
                    if (_comboBox.value() == 11) {
                        _tipoIdForma.disable();
                    }
                    else {
                        _tipoIdForma.enable();
                    }
                    _pagDetalle.disabled = (_comboBox.value() == 11);
                    _pagMotivo.disabled = (_comboBox.value() == 12);
                }
                if ($("#cscIdDestino").data("tComboBox") != null) {
                    var _bimCodigo = $("#bimCodigo")[0];
                    var _cscIdDestino = $("#cscIdDestino").data("tComboBox");

                    if (_comboBox.text() != "Transferencia") {
                        _cscIdDestino.disable();
                        _bimCodigo.disabled = true;
                        _bimCodigo.value = '';
                    }
                    else {
                        _cscIdDestino.enable();
                        _bimCodigo.disabled = false;
                    }
                }
                break;

         
            case "perId":
                var _Especialidad = $("#espId").data("tComboBox");
                AbrirWaiting();
                var _Post = GetPathApp('catAgenda/BindingEspecialidades');
                $.post(_Post, { perId: _comboBox.value() }, function (data) {
                    CerrarWaiting();
                    if (data != null) {
                        _Especialidad.dataBind(data.data);
                        if (data.total < 2) {
                            _Especialidad.select(0);
                            _Especialidad.disable();
                            $("#agDuracion").focus();
                        }
                        else {
                            _Especialidad.enable();
                            $("#espId-input").focus();
                        }
                    }
                });

                break;
        }
    }
}

function MostrarError(Mensaje) {
    var _WindowError = $("#MensajeError").data("tWindow");
    if (_WindowError != null) {
        $("#lblErrorMessage").text(Mensaje);
        _WindowError.center().open();
    }
}

function DetallePersona(e) {
    var detailWindow = $("#wConsulta").data("tWindow");
    var _Persona = e.response.InfoPersona;
    var _Foto = "";
    var _Post = GetPathApp('Content');

    if (detailWindow == null)
        return;
    if (_Persona.perId == null)
        return;
    if (_Persona.perFoto == null) {
        if (_Persona.tipoIdSexoTexto == 'Femenino') {
            _Foto = _Post + '/General/Mujer.jpg';
        }
        else {
            _Foto = _Post + '/General/Hombre.jpg';
        }
    }
    else {
        _Foto = _Post + '/Archivos/FotosPersonal/' + _Persona.perFoto;
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
    $("#lblperObservaciones").text(_Persona.perObservaciones == null ? "" : _Persona.perObservaciones);
    $("#lblcarDescripcion").text(_Persona.carDescripcion);
    $("#lblEstadoCivil").text(_Persona.perEstadoCivil);
    $("#lblprovNombre").text(_Persona.provNombre);
    $("#lblpaisNombre").text(_Persona.paisNombre);
    $("#lblperFechaCasado").text(_Persona.perFechaCasamiento);
    $("#lblperCelular").text(_Persona.perCelular);
    $("#lblprofNombre").text(_Persona.profNombre);
    $("#lblperLeeoEscribe").text(_Persona.perLeeoEscribe ? 'SI' : 'NO');
    $("#lblperDomicilio").text(_Persona.perDomicilio);
    $("#lblRecibeSMS").text(_Persona.perAutorizaNotificarSMS ? 'SI' : 'NO');
    $("#lblperIdiomas").text(_Persona.perIdiomas);
    detailWindow.center().open();
}
function JSONDate(dateStr) {
    var m, day;
    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return (day + '/' + m + '/' + d.getFullYear())
}
function FormatoCuil(_dni, _cuil) {
    var _Cdni = _dni.toString();
    var n = _cuil.indexOf(_Cdni);
    var n2 = _cuil.substr(n, _Cdni.length);
    var n3 = _cuil.substr(0, n);
    var n4 = _cuil.substr((n2.length + n3.length), (_cuil.length - (n2.length + n3.length)));
    var _1Ccuil = n3 + "-" + n2 + "-" + n4
    return _1Ccuil;

}
function Obtenerdatos_CONPADxEncuestaSinAdmision() {
    var _documento = $('#pacNumeroDocumento').val();
    var _tipoDoc = $('#tipoIdTipoDocumento').val();
    var _Accion = $("#pacAccion").val();
    //var _plaId = $('#p
    if ((_documento != 11111) & (_tipoDoc < 182) & (_tipoDoc > 1)) {
        AbrirWaiting();
        var _Post = GetPathApp('catEncuestaSinAdmisionPlanilla/getPadron');
        $.ajax({
            url: _Post,
            data: { doc: _documento, tipoDoc: _tipoDoc, plaId: _plaId, Accion: _Accion },
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
                if (data.length > 1 && data.pacApellido != null) {
                    jAlert(data, '¡Atención!');

                    jConfirm('¿Desea Modificar datos del  Paciente "' + data.pacApellido + '"?', "¡Atencion!", function (r) {
                        if (r) {
                            $("#pacAccion").val("Modificar");
                            $("#lblbtnAgregar").text("Modificar")
                        }
                    });

                }
                if (data.length > -1 && data.pacApellido != null) {// SOLO PACIENTE
                    jConfirm('¿Desea Editar datos del  Paciente "' + data.pacApellido + ' ' + data.pacNombre + '"?', "¡Atencion!", function (r) {
                        if (r) {
                            //OCULTOS
                            $("#lblbtnAgregar").text("Agregar")
                            $("#pacAccion").val(data.pacAccion);
                            $("#encId").val(_encId);
                            $("#csId").val(data.csId);

                            //General
                            $("#pacId").val(data.pacId);
                            $("#pacApellido").val(data.pacApellido);
                            $("#pacNombre").val(data.pacNombre);
                            $("#pacFechaNacimiento").val(data.pacFechaNacimientoTexto);
                            //if (data.pacCUIL.indexOf("-") != -1) {
                            //    $("#pacCUIL").val(data.pacCUIL);
                            //}
                            //if (data.pacCUIL != null && data.pacCUIL != "" && data.pacCUIL.indexOf("-") == -1) {
                            //    $("#pacCUIL").val(FormatoCuil(data.pacNumeroDocumento, data.pacCUIL));
                            //}
                            $("#tipoIdSexo").data("tComboBox").value(data.tipoIdSexo);
                            //$("#tipoIdOcupacion").data("tComboBox").value(data.tipoIdOcupacion);
                            //$("#paisId").data("tComboBox").value(data.paisId);
                            $("#tipoIdEstadoCivil").data("tComboBox").value(data.tipoIdEstadoCivil);
                            //$("#osId").data("tComboBox").value(data.osId);
                            //$("#pacMail").val(data.pacMail);

                            //Domicilio
                            //  $("#provId").data("tComboBox").value(data.provId);
                            $("#depId").data("tComboBox").value(data.depId);
                            $("#locId").data("tComboBox").value(data.locId);
                            //$("#barId").data("tComboBox").value(data.barId);
                            /*$("#pacCalle").val(data.pacCalle);
                            $("#pacCalleNumero").val(data.pacCalleNumero);
                            $("#pacDepto").val(data.pacDepto);
                            $("#pacManzana").val(data.pacManzana);
                            $("#pacPiso").val(data.pacPiso);
                            $("#pacNotificarxSMS").val(data.pacNotificarxSMS);
                            $("#pacTelefonoTrabajo").val(data.pacTelefonoTrabajo);*/
                            $("#pacTelefonoCelular").val(data.pacTelefonoCelular);


                            //Salud
                            $("#etnId").data("tComboBox").value(data.etnId);
                            $("#tipoIdGrupoSanguineo").data("tComboBox").value(data.tipoIdGrupoSanguineo);


                        }
                    });
                }
                /*caso de PACIENTE EN PADRON*/
                if (data.pacApellido != null) {
                    //OCULTOS
                    $("#lblbtnAgregar").text("Agregar");
                    $("#pacAccion").val(data.pacAccion);
                    $("#pacId").val(data.pacId);
                    $("#encId").val(_encId);
                    $("#csId").val(data.csId);

                    //General                 
                    $("#pacApellido").val(data.pacApellido);
                    $("#pacNombre").val(data.pacNombre);
                    $("#pacFechaNacimiento").val(data.pacFechaNacimientoTexto);
                    //if (data.pacCUIL.indexOf("-") != -1) {
                    //    $("#pacCUIL").val(data.pacCUIL);
                    //}
                    //if (data.pacCUIL != null && data.pacCUIL != "" && data.pacCUIL.indexOf("-") == -1) {
                    //    $("#pacCUIL").val(FormatoCuil(data.pacNumeroDocumento, data.pacCUIL));
                    //}

                    $("#tipoIdSexo").data("tComboBox").value(data.tipoIdSexo);
                    //  $("#tipoIdOcupacion").data("tComboBox").value(data.tipoIdOcupacion);
                    // $("#paisId").data("tComboBox").value(data.paisId);
                    $("#tipoIdEstadoCivil").data("tComboBox").value(data.tipoIdEstadoCivil);
                    /* $("#osId").data("tComboBox").value(data.osId);
                     $("#pacMail").val(data.pacMail);*/

                    //Domicilio
                    //    $("#provId").data("tComboBox").value(data.provId);
                    $("#depId").data("tComboBox").value(data.depId);
                    $("#locId").data("tComboBox").value(data.locId);
                    /*  $("#barId").data("tComboBox").value(data.barId);
                      $("#pacCalle").val(data.pacCalle);
                      $("#pacCalleNumero").val(data.pacCalleNumero);
                      $("#pacDepto").val(data.pacDepto);
                      $("#pacManzana").val(data.pacManzana);
                      $("#pacPiso").val(data.pacPiso);
                      $("#pacNotificarxSMS").val(data.pacNotificarxSMS);
                      $("#pacTelefonoTrabajo").val(data.pacTelefonoTrabajo);*/
                    $("#pacTelefonoCelular").val(data.pacTelefonoCelular);


                    //Salud
                    //    $("#etnId").data("tComboBox").value(data.etnId);
                    //  $("#tipoIdGrupoSanguineo").data("tComboBox").value(data.tipoIdGrupoSanguineo);



                    /*general*/
                    $("#pacApellido").attr("disabled", false);
                    $("#pacNombre").attr("disabled", false);
                    //   $("#pacCUIL").attr("disabled", false);
                    $("#tipoIdSexo").data("tComboBox").enable();
                    //$("#tipoIdOcupacion").data("tComboBox").enable();
                    $("#pacFechaNacimiento").attr("disabled", false);
                    $("#tipoIdEstadoCivil").data("tComboBox").enable();
                    //$("#pacMail").attr("disabled", false);
                    // $("#paisId").data("tComboBox").enable();
                    //   $("#osId").data("tComboBox").enable();

                }
                /*caso de PACIENTE NUEVO*/
                if (data.pacApellido == null) {
                    jAlert("No se encontraron datos para el Nro. de doc. " + _documento, '¡Atención!');
                    //OCULTOS
                    $("#lblbtnAgregar").text("Agregar")
                    $("#pacAccion").val("Agregar");
                    $("#encId").val(_encId);
                    $("#csId").val(data.csId);
                    $("#pacCalle").val(data.pacCalle);
                    $("#pacCalleNumero").val(data.pacCalleNumero);
                    $("#pacDepto").val(data.pacDepto);
                    $("#pacManzana").val(data.pacManzana);
                    $("#pacPiso").val(data.pacPiso);
                    $("#pacApellido").val('');
                    $("#pacNombre").val('');
                    $("#pacFechaNacimiento").val('');
                    $("#pacCUIL").val('');

                    //Domicilio
                    $("#provId").data("tComboBox").value(data.provId);
                    $("#depId").data("tComboBox").value(data.depId);
                    $("#locId").data("tComboBox").value(data.locId);
                    $("#barId").data("tComboBox").value(data.barId);


                    /*general*/
                    $("#pacApellido").attr("disabled", false);
                    $("#pacNombre").attr("disabled", false);
                    $("#pacCUIL").attr("disabled", false);
                    $("#tipoIdSexo").data("tComboBox").enable();
                    $("#tipoIdOcupacion").data("tComboBox").enable();
                    $("#pacFechaNacimiento").attr("disabled", false);
                    $("#tipoIdEstadoCivil").data("tComboBox").enable();
                    $("#pacMail").attr("disabled", false);
                    $("#paisId").data("tComboBox").enable();
                    $("#osId").data("tComboBox").enable();
                }

                /*general*/
                $("#pacApellido").attr("disabled", false);
                $("#pacNombre").attr("disabled", false);
                $("#pacCUIL").attr("disabled", false);
                $("#tipoIdSexo").data("tComboBox").enable();
                $("#tipoIdOcupacion").data("tComboBox").enable();
                $("#pacFechaNacimiento").attr("disabled", false);
                $("#tipoIdEstadoCivil").data("tComboBox").enable();
                $("#pacMail").attr("disabled", false);
                $("#paisId").data("tComboBox").enable();
                $("#osId").data("tComboBox").enable();
            }
        });
        /*general*/
        $("#pacApellido").attr("disabled", false);
        $("#pacNombre").attr("disabled", false);
        //$("#pacCUIL").attr("disabled", false);
        $("#tipoIdSexo").data("tComboBox").enable();
        // $("#tipoIdOcupacion").data("tComboBox").enable();
        $("#pacFechaNacimiento").attr("disabled", false);
        //   $("#tipoIdEstadoCivil").data("tComboBox").enable();
        /*  $("#pacMail").attr("disabled", false);
          $("#paisId").data("tComboBox").enable();        
          $("#osId").data("tComboBox").enable();*/
        //domicilio
        //  $("#provId").data("tComboBox").disable();
        $("#depId").data("tComboBox").enable();
        $("#locId").data("tComboBox").enable();
        //$("#barId").data("tComboBox").disable();



    }


}
function Obtenerdatos_CONPADxEncuesta() {
    var _documento = $('#pacNumeroDocumento').val();
    var _tipoDoc = $('#tipoIdTipoDocumento').val();
    var _Accion = $("#pacAccion").val();
    var _encId = $('#encId').val();
    if ((_documento != 11111) & (_tipoDoc < 182) & (_tipoDoc > 1)) {
        AbrirWaiting();
        var _Post = GetPathApp('catEncuestaAPS/getPadron');
        $.ajax({
            url: _Post,
            data: { doc: _documento, tipoDoc: _tipoDoc, encId: _encId, Accion: _Accion },
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
                if (data.length > 1 && data.pacApellido != null) {
                    jAlert(data, '¡Atención!');

                    jConfirm('¿Desea Modificar datos del  Paciente "' + data.pacApellido + '"?', "¡Atencion!", function (r) {
                        if (r) {
                            $("#pacAccion").val("Modificar");
                            $("#lblbtnAgregar").text("Modificar")
                        }
                    });

                }
                if (data.length > -1 && data.pacApellido != null) {// SOLO PACIENTE
                    jConfirm('¿Desea Editar datos del  Paciente "' + data.pacApellido + ' ' + data.pacNombre + '"?', "¡Atencion!", function (r) {
                        if (r) {
                            //OCULTOS
                            $("#lblbtnAgregar").text("Agregar")
                            $("#pacAccion").val(data.pacAccion);
                            $("#encId").val(_encId);
                            $("#csId").val(data.csId);

                            //General
                            $("#pacId").val(data.pacId);
                            $("#pacApellido").val(data.pacApellido);
                            $("#pacNombre").val(data.pacNombre);
                            $("#pacFechaNacimiento").val(data.pacFechaNacimientoTexto);
                            if (data.pacCUIL.indexOf("-") != -1) {
                                $("#pacCUIL").val(data.pacCUIL);
                            }
                            if (data.pacCUIL != null && data.pacCUIL != "" && data.pacCUIL.indexOf("-") == -1) {
                                $("#pacCUIL").val(FormatoCuil(data.pacNumeroDocumento, data.pacCUIL));
                            }
                            $("#tipoIdSexo").data("tComboBox").value(data.tipoIdSexo);
                            $("#tipoIdOcupacion").data("tComboBox").value(data.tipoIdOcupacion);
                            $("#paisId").data("tComboBox").value(data.paisId);
                            $("#tipoIdEstadoCivil").data("tComboBox").value(data.tipoIdEstadoCivil);
                            $("#osId").data("tComboBox").value(data.osId);
                            $("#pacMail").val(data.pacMail);

                            //Domicilio
                            $("#provId").data("tComboBox").value(data.provId);
                            $("#depId").data("tComboBox").value(data.depId);
                            $("#locId").data("tComboBox").value(data.locId);
                            $("#barId").data("tComboBox").value(data.barId);
                            $("#pacCalle").val(data.pacCalle);
                            $("#pacCalleNumero").val(data.pacCalleNumero);
                            $("#pacDepto").val(data.pacDepto);
                            $("#pacManzana").val(data.pacManzana);
                            $("#pacPiso").val(data.pacPiso);
                            $("#pacNotificarxSMS").val(data.pacNotificarxSMS);
                            $("#pacTelefonoTrabajo").val(data.pacTelefonoTrabajo);
                            $("#pacTelefonoCelular").val(data.pacTelefonoCelular);


                            //Salud
                            $("#etnId").data("tComboBox").value(data.etnId);
                            $("#tipoIdGrupoSanguineo").data("tComboBox").value(data.tipoIdGrupoSanguineo);


                        }
                    });
                }
                /*caso de PACIENTE EN PADRON*/
                if (data.pacApellido != null) {
                    //OCULTOS
                    $("#lblbtnAgregar").text("Agregar");
                    $("#pacAccion").val(data.pacAccion);
                    $("#pacId").val(data.pacId);
                    $("#encId").val(_encId);
                    $("#csId").val(data.csId);

                    //General                 
                    $("#pacApellido").val(data.pacApellido);
                    $("#pacNombre").val(data.pacNombre);
                    $("#pacFechaNacimiento").val(data.pacFechaNacimientoTexto);
                    if (data.pacCUIL.indexOf("-") != -1) {
                        $("#pacCUIL").val(data.pacCUIL);
                    }
                    if (data.pacCUIL != null && data.pacCUIL != "" && data.pacCUIL.indexOf("-") == -1) {
                        $("#pacCUIL").val(FormatoCuil(data.pacNumeroDocumento, data.pacCUIL));
                    }

                    $("#tipoIdSexo").data("tComboBox").value(data.tipoIdSexo);
                    $("#tipoIdOcupacion").data("tComboBox").value(data.tipoIdOcupacion);
                    $("#paisId").data("tComboBox").value(data.paisId);
                    $("#tipoIdEstadoCivil").data("tComboBox").value(data.tipoIdEstadoCivil);
                    $("#osId").data("tComboBox").value(data.osId);
                    $("#pacMail").val(data.pacMail);

                    //Domicilio
                    $("#provId").data("tComboBox").value(data.provId);
                    $("#depId").data("tComboBox").value(data.depId);
                    $("#locId").data("tComboBox").value(data.locId);
                    $("#barId").data("tComboBox").value(data.barId);
                    $("#pacCalle").val(data.pacCalle);
                    $("#pacCalleNumero").val(data.pacCalleNumero);
                    $("#pacDepto").val(data.pacDepto);
                    $("#pacManzana").val(data.pacManzana);
                    $("#pacPiso").val(data.pacPiso);
                    $("#pacNotificarxSMS").val(data.pacNotificarxSMS);
                    $("#pacTelefonoTrabajo").val(data.pacTelefonoTrabajo);
                    $("#pacTelefonoCelular").val(data.pacTelefonoCelular);


                    //Salud
                    $("#etnId").data("tComboBox").value(data.etnId);
                    $("#tipoIdGrupoSanguineo").data("tComboBox").value(data.tipoIdGrupoSanguineo);



                    /*general*/
                    $("#pacApellido").attr("disabled", false);
                    $("#pacNombre").attr("disabled", false);
                    $("#pacCUIL").attr("disabled", false);
                    $("#tipoIdSexo").attr("disabled", false);
                    $("#tipoIdOcupacion").attr("disabled", false);
                    $("#pacFechaNacimiento").attr("disabled", false);
                    $("#tipoIdEstadoCivil").attr("disabled", false);
                    $("#pacMail").attr("disabled", false);
                    $("#paisId").data("tComboBox").enable();
                    $("#osId").data("tComboBox").enable();

                }
                /*caso de PACIENTE NUEVO*/
                if (data.pacApellido == null) {
                    jAlert("No se encontraron datos para el Nro. de doc. " + _documento, '¡Atención!');
                    //OCULTOS
                    $("#lblbtnAgregar").text("Agregar")
                    $("#pacAccion").val("Agregar");
                    $("#encId").val(_encId);
                    $("#csId").val(data.csId);
                    $("#pacCalle").val(data.pacCalle);
                    $("#pacCalleNumero").val(data.pacCalleNumero);
                    $("#pacDepto").val(data.pacDepto);
                    $("#pacManzana").val(data.pacManzana);
                    $("#pacPiso").val(data.pacPiso);
                    $("#pacApellido").val('');
                    $("#pacNombre").val('');
                    $("#pacFechaNacimiento").val('');
                    $("#pacCUIL").val('');

                    //Domicilio
                    $("#provId").data("tComboBox").value(data.provId);
                    $("#depId").data("tComboBox").value(data.depId);
                    $("#locId").data("tComboBox").value(data.locId);
                    $("#barId").data("tComboBox").value(data.barId);


                    /*general*/
                    $("#pacApellido").attr("disabled", false);
                    $("#pacNombre").attr("disabled", false);
                    $("#pacCUIL").attr("disabled", false);
                    $("#tipoIdSexo").attr("disabled", false);
                    $("#tipoIdOcupacion").attr("disabled", false);
                    $("#pacFechaNacimiento").attr("disabled", false);
                    $("#tipoIdEstadoCivil").attr("disabled", false);
                    $("#pacMail").attr("disabled", false);
                    $("#paisId").data("tComboBox").enable();
                    $("#osId").data("tComboBox").enable();
                }

                /*general*/
                $("#pacApellido").attr("disabled", false);
                $("#pacNombre").attr("disabled", false);
                $("#pacCUIL").attr("disabled", false);
                $("#tipoIdSexo").attr("disabled", false);
                $("#tipoIdOcupacion").attr("disabled", false);
                $("#pacFechaNacimiento").attr("disabled", false);
                $("#tipoIdEstadoCivil").attr("disabled", false);
                $("#pacMail").attr("disabled", false);
                $("#paisId").data("tComboBox").enable();
                $("#osId").data("tComboBox").enable();
            }
        });
        /*general*/
        $("#pacApellido").attr("disabled", false);
        $("#pacNombre").attr("disabled", false);
        $("#pacCUIL").attr("disabled", false);
        $("#tipoIdSexo").attr("disabled", false);
        $("#tipoIdOcupacion").attr("disabled", false);
        $("#pacFechaNacimiento").attr("disabled", false);
        $("#tipoIdEstadoCivil").attr("disabled", false);
        $("#pacMail").attr("disabled", false);
        $("#paisId").data("tComboBox").enable();
        $("#paisId").data("tComboBox").enable();
        $("#osId").data("tComboBox").enable();
        //domicilio
        $("#provId").data("tComboBox").disable();
        $("#depId").data("tComboBox").disable();
        debugger;
        $("#locId").attr("disabled", true);
        $("#barId").data("tComboBox").disable();
        
       

    }


}


function Obtenerdatos_CONPAD() {
    var _documento = $('#pacNumeroDocumento').val();
    var _tipoDoc = $('#tipoIdTipoDocumento').val();

    if ((_documento != 11111) & (_tipoDoc < 182) & (_tipoDoc > 1)) {
        AbrirWaiting();
        var _Post = GetPathApp('catPaciente/getPadron');
        $.ajax({
            url: _Post,
            data: { doc: _documento, tipoDoc: _tipoDoc },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                    //
                });
                $('#popup_container').focus();
                $('#popup_ok').focus();
            },

            success: function (data) {
                CerrarWaiting();
                if (data.length > 1) {
                    jAlert(data, '¡Atención!');
                }
                if (data.length == 1) {
                    var _Fcuil = FormatoCuil(_documento, data[0].ppCUIL);
                    var newDate = JSONDate(data[0].ppFechaNacimiento);
                    jQuery("#pacPadron").val(data[0].ppId);
                    jQuery("#pacApellido").val(data[0].ppApellido);
                    jQuery("#pacNombre").val(data[0].ppNombre);
                    jQuery("#pacFechaNacimiento").val(newDate);
                    jQuery("#pacCalle").val(data[0].ppDomCalle);
                    jQuery("#pacCalleNumero").val(data[0].ppDomNroCalle);
                    // jQuery("#pacCUIL").val(data[0].ppCUIL);
                    jQuery("#pacCUIL").val(_Fcuil);
                    if (data[0].ppSexo == 'F') {
                        $('#tipoIdSexo').data("tComboBox").select(0);
                    }
                    if (data[0].ppSexo == 'M') {
                        $('#tipoIdSexo').data("tComboBox").select(1)
                    }
                    if (data[0].depId > 0) {
                        $('#depId').data("tComboBox").value(data[0].depId)
                    }

                    jQuery("#pacTelefonoCasa").val(data[0].ppTelefono1);
                }
                if (data.length == 0) {
                    jAlert("No se encontraron datos para el Nro. de doc. " + _documento, '¡Atención!');
                    jQuery("#pacPadron").val('');
                    jQuery("#pacApellido").val('');
                    jQuery("#pacNombre").val('');
                    jQuery("#pacFechaNacimiento").val('');
                    jQuery("#pacCalle").val('');
                    jQuery("#pacCalleNumero").val('');
                    jQuery("#pacCUIL").val('');
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                }
            }
        });
    }


}

function onSaveCRUD(e) {
    $('.t-window-content.t-content').append('<div class="t-overlay" style="z-index: 11000; opacity: 0.5; display: block; vertical-align: middle; text-align: center;"><img src="' + GetPathApp("~/Content/General/WaitingIndicator.gif") + '" width="45px" alt="" /></div>');
}
function onImportarArchivo() {
    var _Window = $('#wImportacionDeArchivo').data("tWindow");
    _Window.center().open();
}

function isMobile() {
    var IsMobile = (
        (navigator.userAgent.match(/Android/i)) ||
        (navigator.userAgent.match(/webOS/i)) ||
        (navigator.userAgent.match(/iPhone/i)) ||
        (navigator.userAgent.match(/iPod/i)) ||
        (navigator.userAgent.match(/iPad/i)) ||
        (navigator.userAgent.match(/BlackBerry/)));
    return IsMobile == null ? false : true;
}

function ProcesandoListaDesplegable(Control) {
    $("#" + Control).append('<div><img src="' + GetPathApp("Content") + '/General/WaitingIndicator.gif" alt="Cargando..." style="height: 18px; margin-top: -23px; margin-left: 5px; position: absolute;" ></div>');
}

//Cálculo de edad a partir de una fecha\\
/*----------Funcion para obtener la edad------------*/
function CalcularEdad(fecha) {
    var fechaActual = new Date();
    var diaActual = fechaActual.getDate();
    var mmActual = fechaActual.getMonth() + 1;
    var yyyyActual = fechaActual.getFullYear();
    FechaNac = fecha.substring(0, 10).split("/");
    var diaCumple = FechaNac[0];
    var mmCumple = FechaNac[1];
    var yyyyCumple = FechaNac[2];
    //retiramos el primer cero de la izquierda
    if (mmCumple.substr(0, 1) == 0) {
        mmCumple = mmCumple.substring(1, 2);
    }
    //retiramos el primer cero de la izquierda
    if (diaCumple.substr(0, 1) == 0) {
        diaCumple = diaCumple.substring(1, 2);
    }
    var edad = yyyyActual - yyyyCumple;

    //validamos si el mes de cumpleaños es menor al actual
    //o si el mes de cumpleaños es igual al actual
    //y el dia actual es menor al del nacimiento
    //De ser asi, se resta un año
    if ((mmActual < mmCumple) || (mmActual == mmCumple && diaActual < diaCumple)) {
        edad--;
    }
    return edad;
};

function CalcularCUIL(rut, Sexo) {
    // type check
    if (!rut || !rut.length || typeof rut !== 'string') {
        return -1;
    }
    var DNI = rut;
    rut = (Sexo === "Femenino" ? "27" : "20") + rut;
    // serie numerica
    var secuencia = [5,4,3,2,7,6,5,4,3,2];
    var sum = 0;
    //
    for (var i=0; i <10; i++) {
        var d = rut.charAt(i);
        sum += new Number(d)*secuencia[i];
    };
    // sum mod 11
    var rest = (sum % 11);
    // si es 11, retorna 0, sino si es 10 retorna K,
    // en caso contrario retorna el numero
    var Resultado = 0;
    var Pre = (Sexo === "Femenino" ? "27" : "20");
    if (rest === 10) {
        Pre = "23";
        Resultado = (Sexo === "Femenino" ? 9 : 4);
    } else if (rest < 10 && rest > 0) {
        Resultado = 11 - rest;
    }

    return Pre + "-" + DNI + "-" + Resultado;
};

function InicialicarDatePickerCastellano() {
    /**
     * Spanish translation for bootstrap-datepicker
     */
    //(function($) {
    $.fn.datepicker.dates["es"] = {
        days: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"],
        daysShort: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom"],
        daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa", "Do"],
        months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"]
    };
    //}(jQuery));
}

function MensajeEmergente(titulo, mensaje, imagen, sonido) {
    if (sonido && imagen != null) {
        $.gritter.add({
            // (string | mandatory) the heading of the notification
            title: titulo,
            // (string | mandatory) the text inside the notification
            text: mensaje,
            image: imagen,
            after_open: function (e) {
                $(".gritter-with-image").append('<audio id="audioMensajeEmergente" > <source src="' + GetPathApp("Content") + '/Audio/flash-message.mp3" type="audio/mpeg">Su browser no soporta audio.</audio>');
                $("#audioMensajeEmergente").trigger("play");
            }
        });
        return;
    }
    if (!sonido && imagen != null) {
        $.gritter.add({
            // (string | mandatory) the heading of the notification
            title: titulo,
            // (string | mandatory) the text inside the notification
            text: mensaje,
            image: imagen
        });
        return;
    }
    if (!sonido && imagen == null) {
        $.gritter.add({
            // (string | mandatory) the heading of the notification
            title: titulo,
            // (string | mandatory) the text inside the notification
            text: mensaje
        });
        return;
    }

    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: titulo,
        // (string | mandatory) the text inside the notification
        text: mensaje,
        after_open: function (e) {
            $(".gritter-without-image").append('<audio id="audioMensajeEmergente" > <source src="' + GetPathApp("Content") + '/Audio/flash-message.mp3" type="audio/mpeg">Su browser no soporta audio.</audio>');
            $("#audioMensajeEmergente").trigger("play");
        }
    });
}

//Inicializamos objeto de datos de videos\\
//Este objeto guardará algunos datos sobre la cámara
function InicializaDatosCamara() {
    window.datosVideo = {
        "StreamVideo": null,
        "url": null
    };
    //Nos aseguramos que estén definidas
    //algunas funciones básicas
    window.URL = window.URL || window.webkitURL;
    navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia ||
        function () {
            alert("Su navegador no soporta el manejo de video.");
            return;
        };
}

function FrenarVideo() {
    if (window.datosVideo != null) {
        if (window.datosVideo.StreamVideo) {
            var mtCamara = window.datosVideo.StreamVideo.getVideoTracks();
            mtCamara[0].stop();
            window.URL.revokeObjectURL(datosVideo.url);
        }
    }
}

function validarEmail(email) {
    var expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!expr.test(email)) {
        return false;
        //Correo incorrecto.
    }
    return true;
}

function validaCuit(sCUIT) {
    var aMult = '6789456789';
    var aMult = aMult.split("");
    //var sCUIT = String(sCUIT);
    var iResult = 0;
    var aCUIT = sCUIT.replace("-", "").replace("-", "").split("");

    if (aCUIT.length == 11) {
        // La suma de los productos 
        for (var i = 0; i <= 9; i++) {
            iResult += aCUIT[i] * aMult[i];
        }
        // El módulo de 11 
        iResult = (iResult % 11);

        // Se compara el resultado con el dígito verificador 
        return (iResult == aCUIT[10]);
    }
    return false;
}

function ValidaNomenclacion(Nombre) {
    var aPalabras = "SINCALLE/nombre de calle/sin nombre/apellido/nombre/adad/qwqw/qaqa/asas/SIN CALLE/en mi casa/en casa/ni idea/quien sabe/NOSE/NO SE/SINDATO/SIN DATO/ASDLÑFKASD/ASD/ASDA/ASDAD/CUALQUIER/cualquiera/ere/ESC/la casa/mi lugar de laburo/sdadadsad/sdfgsdf".toLowerCase();
    var arrayPalabras = aPalabras.split("/");
    Nombre = Nombre.toLowerCase();
    for (var i = 0; i <= arrayPalabras.length; i++) {
        if (arrayPalabras[i] === Nombre || Nombre.indexOf(arrayPalabras[i]) >= 0) {
            return false;
        }
    }
    return true;
}

function EjecutarPantallaCompleta(element) {
    //if (element.requestFullScreen) {
    //    element.requestFullScreen();
    //} else if (element.mozRequestFullScreen) {
    //    element.mozRequestFullScreen();
    //} else if (element.webkitRequestFullScreen) {
    //    element.webkitRequestFullScreen();
    //}
    var el = document.documentElement
        , rfs = // for newer Webkit and Firefox
               el.requestFullScreen
            || el.webkitRequestFullScreen
            || el.mozRequestFullScreen
            || el.msRequestFullscreen
    ;
    if (typeof rfs != "undefined" && rfs) {
        rfs.call(el);
    } else if (typeof window.ActiveXObject != "undefined") {
        // for Internet Explorer
        var wscript = new ActiveXObject("WScript.Shell");
        if (wscript != null) {
            wscript.SendKeys("{F11}");
        }
    }
}

//Función para solo números\\
function SoloEnteros(pNombreInput) {
    $("#" + pNombreInput).on("keypress", function (e) {
        //var keynum = window.event ? window.event.keyCode : evento.which;
        var key = window.Event ? e.which : e.keyCode;
        return (key >= 48 && key <= 57);
    });
}

//Función para números negativos y positivos
function SoloEnterosPositivosYNegativos(pNombreInput) {
    $("#" + pNombreInput).on("keypress", function (e) {
        //var keynum = window.event ? window.event.keyCode : evento.which;
        var key = window.Event ? e.which : e.keyCode;
        return ((key >= 48 && key <= 57) || key == 45);
    });
}

//Función para números negativos y positivos
function SoloNumerosConPunto(pNombreInput) {
    $("#" + pNombreInput).on("keypress", function (e) {
        //var keynum = window.event ? window.event.keyCode : evento.which;
        var key = window.Event ? e.which : e.keyCode;
        return ((key >= 48 && key <= 57) || key == 46);
    });
}

//Función para verificar si una variable esta definida
function isDefined(variable) {
    return (typeof (window[variable]) == "undefined") ? false : true;
}
