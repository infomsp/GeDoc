<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">   
    //Configuramos el estilo de algunos campos\\
    $("div.t-widget.t-combobox.t-header").focusin(function () {
        $(this).addClass("ObjetoSombreado");
    });
    $("div.t-widget.t-combobox.t-header").focusout(function () {
        $(this).removeClass('ObjetoSombreado');
    });

    //Tabpage
    $("ul.t-reset.t-tabstrip-items").css('margin-left', '-1px');

    //Campos
    $("#txtpacApellido").css('margin-right', '8px');
    $("#txtpacNombre").css('margin-right', '8px');
    //$("div.t-widget.t-combobox.t-header").css('margin-right', '8px');
    $("#txtpacNumeroDocumento").css('width', '80px');
    $("#txtpacApellido").css('margin-right', '8px');
    $("#txtpacCUIL").css('width', '100px');
    $("#txtpacCUIL").css('margin-right', '40px');
    $("#txt").css('margin-right', '40px');
    $("#chkDonaOrganos").css('vertical-align', 'middle');
    $("#lbltelefonocasa").css('margin-right', '0px');
    $("#lbltelefonocasa").css('vertical-align', 'middle');
    $("#lblTelefonotrabajo").css('vertical-align', 'middle');
    $("#lblTelefonotrabajo").css('margin-right', '0px');
    $("#lbltelefonocelular").css('vertical-align', 'middle');
    $("#lbltelefonocelular").css('margin-right', '0px');

    $("#chkDonaOrganos").css('vertical-align', 'middle');
    $("#chkDonaOrganos").css('vertical-align', 'middle');
    $("#chkDonaOrganos").css('vertical-align', 'middle');
    //Fin de configuración de campos\\
    $(document).ready(function () {
       // $('input[type=radio]').attr('disabled', true);
        $('#GObstetricos').attr('disabled', true);
    });

    function onSuccess(e) {
        alert('Formulario Guardado');
    }
    function onActivatePaciente(e) {
       // onCommandEdit(e);
    }
   
    function onCancelar() {
        var _Window = $("#CRUDPaciente").data("tWindow");      
        $(':input').not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');       
        _Window.close();
    }
    $('#onAceptar').live({

        click: function () {
            debugger;
            if (_auxEditar == 0) {
                // debugger;
                var pacApellido = jQuery("#txtpacApellido2").val().trim().toUpperCase();
                var pacNombre = jQuery("#txtpacNombre").val().trim().toUpperCase();
                //var pacApellido = jQuery("#txtpacApellido2").val();
                var pacNumeroDocumento = jQuery("#txtpacNumeroDocumento").val();
                var tipoIdTipoDocumento = jQuery("#tipoIdTipoDocumento").val();
                var pacCUIL = jQuery("#txtpacCUIL").val();
                var paisId = jQuery("#paisId").val();
                var _tipoIdSexo = jQuery("#tipoIdSexo").val();
                var pacFechaNacimiento = jQuery("#txtpacFechaNacimiento").val();
                var tipoIdEstadoCivil = jQuery("#tipoIdEstadoCivil").val();
                var tipoIdOcupacion = jQuery("#tipoIdOcupacion").val();
                var pacDonaOrganos = jQuery("#chkDonaOrganos").val();
                var tipoIdGrupoSanguineo = jQuery("#tipoIdGrupoSanguineo").val();
                var pacCalle = jQuery("#txtpacCalle").val();
                var pacCalleNumero = jQuery("#txtpacCalleNumero").val();
                var pacDomicilioReferencias = jQuery("#txtpacDomicilioReferencias").val();
                var depId = jQuery("#depId").val();
                var locId = jQuery("#locId").val();
                var barId = jQuery("#barId").val();
                var pacTelefonoCasa = jQuery("#txtpacTelefonoCasa").val();
                var pacTelefonoTrabajo = jQuery("#txtpacTelefonoTrabajo").val();
                var pacTelefonoCelular = jQuery("#txtpacTelefonoCelular").val();
                var pacNotificarXSMS = jQuery("#chkNotificarxSMS").val();
                var osId = jQuery("#osId").val();
                var pacHospitalizado = jQuery("#chkpacHospitalizado").val();
                //var pacTransfusionesDeSangre = jQuery("#pacTransfusionesDeSangre").val();
                //var pacTransfusionesDeSangreUltima = jQuery("#pacTransfusionesDeSangreUltima2").val();
                objectPac = {
                    pacApellido: pacApellido,
                    pacNombre: pacNombre,
                    pacNumeroDocumento: pacNumeroDocumento,
                    tipoIdTipoDocumento: tipoIdTipoDocumento,
                    pacCUIL: pacCUIL,
                    paisId: paisId,
                    tipoIdSexo: _tipoIdSexo,
                    pacFechaNacimiento: pacFechaNacimiento,
                    tipoIdEstadoCivil: tipoIdEstadoCivil,
                    tipoIdOcupacion: tipoIdOcupacion,
                    pacDonaOrganos: pacDonaOrganos,
                    tipoIdGrupoSanguineo: tipoIdGrupoSanguineo,
                    pacCalle: pacCalle,
                    pacCalleNumero: pacCalleNumero,
                    pacDomicilioReferencias: pacDomicilioReferencias,
                    depId: depId,
                    locId: locId,
                    barId: barId,
                    pacTelefonoCasa: pacTelefonoCasa,
                    pacTelefonoTrabajo: pacTelefonoTrabajo,
                    pacTelefonoCelular: pacTelefonoCelular,
                    pacNotificarXSMS: pacNotificarXSMS,
                    osId: osId,
                    pacHospitalizado: pacHospitalizado,
                  // pacTransfusionesDeSangre: pacTransfusionesDeSangre,
                   // pacTransfusionesDeSangreUltima: pacTransfusionesDeSangreUltima
                };
                debugger;
                if (pacNumeroDocumento == "") {
                    jAlert('Debe ingresar documento del paciente.', 'Error...');
                    return;
                }
                if (pacApellido == "") {
                    jAlert('Debe ingresar apellido del paciente.', 'Error...');
                    return;
                }
                if (pacNombre == "") {
                    jAlert('Debe ingresar nombre del paciente.', 'Error...');
                    return;
                }


                crearPaciente(objectPac,
		                function (paciente) {
		                    jalert("Paciente creado con id" + paciente.pacId);
		                },
		                function (errores) {
		                    var str = '';
		                    $.each(errores, function (index, error) {
		                        str += '*' + error + '</br>';
		                    });
		                    //$('#resultadoValidacion').html(str);
		                });

                $(':input').not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');
                var _Window = $("#CRUDPaciente").data("tWindow");
                _Window.close();
                Refrescar();

            }
       
         function Refrescar() {
        var grid = $("#Grid").data("tGrid");
        // var gridPacientes = $("#GridPacientes").data("tGrid");
        grid.rebind();
        // gridPacientes.rebind();
    }

//          if (_auxEditar == 1) {
//                 debugger;
//                
//               var pacApellido = jQuery("#txtpacApellido2_edit").val().trim().toUpperCase();
//                var pacNombre = jQuery("#txtpacNombre_edit").val().trim().toUpperCase();              
//                var pacNumeroDocumento = jQuery("#txtpacNumeroDocumento_edit").val();
//                var tipoIdTipoDocumento = jQuery("#tipoIdTipoDocumentoedit").val();
//                var pacCUIL = jQuery("#txtpacCUIL_edit").val();
//                var paisId = jQuery("#paisIdedit").val();
//                var _tipoIdSexo = jQuery("#tipoIdSexoedit").val();
//                var pacFechaNacimiento = jQuery("#txtpacFechaNacimiento_edit").val();
//                var tipoIdEstadoCivil = jQuery("#tipoIdEstadoCiviledit").val();
//                var tipoIdOcupacion = jQuery("#tipoIdOcupacionedit").val();
//                var pacDonaOrganos = jQuery("#chkDonaOrganos_edit").val();
//                var tipoIdGrupoSanguineo = jQuery("#tipoIdGrupoSanguineoedit").val();
//                var pacCalle = jQuery("#txtpacCalle_edit").val();
//                var pacCalleNumero = jQuery("#txtpacCalleNumero_edit").val();
//                var pacDomicilioReferencias = jQuery("#txtpacDomicilioReferencias_edit").val();
//                var depId = jQuery("#depIdedit").val();
//                var locId = jQuery("#locIdedit").val();
//                var barId = jQuery("#barIdedit").val();
//                var pacTelefonoCasa = jQuery("#txtpacTelefonoCasa_edit").val();
//                var pacTelefonoTrabajo = jQuery("#txtpacTelefonoTrabajo_edit").val();
//                var pacTelefonoCelular = jQuery("#txtpacTelefonoCelular_edit").val();
//                var pacNotificarXSMS = jQuery("#chkNotificarxSMS_edit").val();
//                var osId = jQuery("#osIdedit").val();
//                var pacHospitalizado = jQuery("#chkpacHospitalizado_edit").val();
////                var pacTransfusionesDeSangre = jQuery("#pacTransfusionesDeSangre").val();
////                var pacTransfusionesDeSangreUltima = jQuery("#pacTransfusionesDeSangreUltima2").val();
//                objectPac = {
//                    
//                    pacApellido: pacApellido,
//                    pacNombre: pacNombre,
//                    pacNumeroDocumento: pacNumeroDocumento,
//                    tipoIdTipoDocumento: tipoIdTipoDocumento,
//                    pacCUIL: pacCUIL,
//                    paisId: paisId,
//                    tipoIdSexo: _tipoIdSexo,
//                    pacFechaNacimiento: pacFechaNacimiento,
//                    tipoIdEstadoCivil: tipoIdEstadoCivil,
//                    tipoIdOcupacion: tipoIdOcupacion,
//                    pacDonaOrganos: pacDonaOrganos,
//                    tipoIdGrupoSanguineo: tipoIdGrupoSanguineo,
//                    pacCalle: pacCalle,
//                    pacCalleNumero: pacCalleNumero,
//                    pacDomicilioReferencias: pacDomicilioReferencias,
//                    depId: depId,
//                    locId: locId,
//                    barId: barId,
//                    pacTelefonoCasa: pacTelefonoCasa,
//                    pacTelefonoTrabajo: pacTelefonoTrabajo,
//                    pacTelefonoCelular: pacTelefonoCelular,
//                    pacNotificarXSMS: pacNotificarXSMS,
//                    osId: osId,
//                    pacHospitalizado: pacHospitalizado,
//                    pacTransfusionesDeSangre: pacTransfusionesDeSangre,
//                    pacTransfusionesDeSangreUltima: pacTransfusionesDeSangreUltima
//                };
//                debugger;
//                if (pacNumeroDocumento == "") {
//                    jAlert('Debe ingresar documento del paciente.', 'Error...');
//                    return;
//                }
//                if (pacApellido == "") {
//                    jAlert('Debe ingresar apellido del paciente.', 'Error...');
//                    return;
//                }
//                if (pacNombre == "") {
//                    jAlert('Debe ingresar nombre del paciente.', 'Error...');
//                    return;
//                }


//                GuardarEditPaciente(objectPac,
//		                function (paciente) {
//		                    jalert("Paciente creado con id" + paciente.pacId);
//		                },
//		                function (errores) {
//		                    var str = '';
//		                    $.each(errores, function (index, error) {
//		                        str += '*' + error + '</br>';
//		                    });
//		                    //$('#resultadoValidacion').html(str);
//		                });

//                $(':input').not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');
//                var _Window = $("#CRUDEDITPaciente").data("tWindow");
//                _Window.close();
//                 Refrescar();
//            }
       }

    });

    $('#onAceptar2').live({

        click: function () {
            debugger;
          

            if (_auxEditar == 1) {
                debugger;
                 var pacId = _pacId;
                var pacApellido = jQuery("#txtpacApellido2_edit").val().trim().toUpperCase();
                var pacNombre = jQuery("#txtpacNombre_edit").val().trim().toUpperCase();
                var pacNumeroDocumento = jQuery("#txtpacNumeroDocumento_edit").val();
                var tipoIdTipoDocumento = jQuery("#tipoIdTipoDocumentoedit").val();
                var pacCUIL = jQuery("#txtpacCUIL_edit").val();
                var paisId = jQuery("#paisIdedit").val();
                var _tipoIdSexo = jQuery("#tipoIdSexoedit").val();
                var pacFechaNacimiento = jQuery("#txtpacFechaNacimiento_edit").val();
                var tipoIdEstadoCivil = jQuery("#tipoIdEstadoCiviledit").val();
                var tipoIdOcupacion = jQuery("#tipoIdOcupacionedit").val();
                var pacDonaOrganos = jQuery("#chkDonaOrganos_edit").val();
                var tipoIdGrupoSanguineo = jQuery("#tipoIdGrupoSanguineoedit").val();
                var pacCalle = jQuery("#txtpacCalle_edit").val();
                var pacCalleNumero = jQuery("#txtpacCalleNumero_edit").val();
                var pacDomicilioReferencias = jQuery("#txtpacDomicilioReferencias_edit").val();
                var depId = jQuery("#depIdedit").val();
                var locId = jQuery("#locIdedit").val();
                var barId = jQuery("#barIdedit").val();
                var pacTelefonoCasa = jQuery("#txtpacTelefonoCasa_edit").val();
                var pacTelefonoTrabajo = jQuery("#txtpacTelefonoTrabajo_edit").val();
                var pacTelefonoCelular = jQuery("#txtpacTelefonoCelular_edit").val();
                var pacNotificarXSMS = jQuery("#chkNotificarxSMS_edit").val();
                var osId = jQuery("#osIdedit").val();
                var pacHospitalizado = jQuery("#chkpacHospitalizado_edit").val();
                //                var pacTransfusionesDeSangre = jQuery("#pacTransfusionesDeSangre").val();
                //                var pacTransfusionesDeSangreUltima = jQuery("#pacTransfusionesDeSangreUltima2").val();
                var objectPac = {
                pacId : pacId,                                                                                                                               
                    pacApellido: pacApellido,
                    pacNombre: pacNombre,
                    pacNumeroDocumento: pacNumeroDocumento,
                    tipoIdTipoDocumento: tipoIdTipoDocumento,
                    pacCUIL: pacCUIL,
                    paisId: paisId,
                    tipoIdSexo: _tipoIdSexo,
                    pacFechaNacimiento: pacFechaNacimiento,
                    tipoIdEstadoCivil: tipoIdEstadoCivil,
                    tipoIdOcupacion: tipoIdOcupacion,
                    pacDonaOrganos: pacDonaOrganos,
                    tipoIdGrupoSanguineo: tipoIdGrupoSanguineo,
                    pacCalle: pacCalle,
                    pacCalleNumero: pacCalleNumero,
                    pacDomicilioReferencias: pacDomicilioReferencias,
                    depId: depId,
                    locId: locId,
                    barId: barId,
                    pacTelefonoCasa: pacTelefonoCasa,
                    pacTelefonoTrabajo: pacTelefonoTrabajo,
                    pacTelefonoCelular: pacTelefonoCelular,
                    pacNotificarXSMS: pacNotificarXSMS,
                    osId: osId,
                    pacHospitalizado: pacHospitalizado,
                    //pacTransfusionesDeSangre: pacTransfusionesDeSangre,
                    //pacTransfusionesDeSangreUltima: pacTransfusionesDeSangreUltima
                };
                debugger;
                if (pacNumeroDocumento == "") {
                    jAlert('Debe ingresar documento del paciente.', 'Error...');
                    return;
                }
                if (pacApellido == "") {
                    jAlert('Debe ingresar apellido del paciente.', 'Error...');
                    return;
                }
                if (pacNombre == "") {
                    jAlert('Debe ingresar nombre del paciente.', 'Error...');
                    return;
                }


                GuardarEditPaciente(objectPac,
		                function (paciente) {
		                    jalert("Paciente creado con id" + paciente.pacId);
		                },
		                function (errores) {
		                    var str = '';
		                    $.each(errores, function (index, error) {
		                        str += '*' + error + '</br>';
		                    });
		                    //$('#resultadoValidacion').html(str);
		                });

                $(':input').not(':button, :submit, :reset, :hidden').val('').removeAttr('checked').removeAttr('selected');
                var _Window = $("#CRUDEDITPaciente").data("tWindow");
                _Window.close();
                 var grid = $("#Grid").data("tGrid");       
                 grid.rebind();
            }
        }

    });
    function crearPaciente(objectPac, callbackSucces, callbackFail) {
        debugger;
             var msg = "";
		   
             var _Window = $("#CRUDPaciente").data("tWindow");
                         $.ajax({
                             type: 'POST',
                             url: "/GeDoc/catPaciente/_InsertEditing",
                             data: JSON.stringify(objectPac),
                             contentType: "application/json;charset=utf-8",
                             statusCode: {
                                 201: function (cliente) {
                                     callbackSucces(cliente);
                                 },
                                 400: function (xhr) {
                                     var errores = JSON.parse(xhr.responseText);
                                     callbackFail(errores);
                                 }
                             }
                          
                         });
                         return false;
                       
       //limpiaForm($("#frmPaciente"));     
        _Window.close();

    }

    function GuardarEditPaciente(objectPac, callbackSucces, callbackFail) {
        debugger;
        var msg = "";

        var _Window = $("#CRUDEDITPaciente").data("tWindow");
         //AbrirWaiting();
         $.ajax({            
            type: 'POST',
            url: "/GeDoc/catPaciente/_SaveEditing",
            data: JSON.stringify(objectPac),
            contentType: "application/json;charset=utf-8",
            statusCode: {
                201: function (cliente) {
                    callbackSucces(cliente);
                },
                400: function (xhr) {
                    var errores = JSON.parse(xhr.responseText);
                    callbackFail(errores);
                }
            }

        });
        return false;

       // limpiaForm($("#frmPaciente"));
        _Window.close();

    }
    //evento para seleccion de sexo 
    $('#tipoIdSexo')
    .bind('valueChange', function (e) {
        debugger;
        if (e.val == "")
            alert("Vacio");
    });

    //evento para departamento
    $('#depId')
    .bind('valueChange', function (e) {
        debugger;
       // alert("cambio");
    });

    //evento para departamento
    $('#locId')
    .bind('valueChange', function (e) {
        debugger;
       // alert("cambio");
    });

    //evento para ingreso del dni
    $('#txtpacNumeroDocumento')
    .bind('valueChange', function (e) {
        debugger;
        Obtenerdatos_CONPAD();
        //alert("cambio");
    });
 
</script>
<%  
   using (Ajax.BeginForm("Create", "catPaciente", null, new AjaxOptions() { OnSuccess = "onSuccess" }, new { id = "frmPaciente", name = "frmPaciente" }))
   //using (Ajax.BeginForm("Create", new AjaxOptions() { OnSuccess="onSuccess" }))
   { 
      GeDoc.catPacientes _model = (ViewData["Paciente"] as GeDoc.catPacientes);    
      //_model.pacApellido = "";       
%>
<% string _btnAceptar = "";
   string _btnCancelar = "";
   _btnAceptar = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -33px -335px;";
   _btnCancelar = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -49px -335px;";
%>    
<!-- Pacientes -->
<%:Html.ValidationSummary(true)%>
<% Html.RenderPartial("Waiting"); %>
<% Html.Telerik().Window()
        .Name("CRUDPaciente")
        .Title("Ficha de Pacientes")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div class="">
            <%
            Html.Telerik().TabStrip()
                .Name("tabPaciente")
                .HtmlAttributes(new { style = "padding: 3px 0px 0px 0px; background: transparent; border: 0px;" })
                .Items(tabstrip =>
                {
                    tabstrip.Add()
                        .Text("Datos Filiatorios")
                        .ContentHtmlAttributes(new { style = "padding: 0px;" })
                        .Content(() =>                            
                        { %>
                            <div id="DatosFiliatorios" style="width: auto; height: 100%;">
                                <div class="editor-label" style="vertical-align: middle;">
                                    <div id="resultadoValidacion" style="color:Red"></div>
                                   
                                    <%: Html.LabelFor(model => _model.tipoIdTipoDocumento, "Tipo de Documento: ")%>
                                    <%: Html.EditorFor(model => _model.tipoIdTipoDocumento, "", "tipoIdTipoDocumento")%>
                                    <%: Html.ValidationMessageFor(model => _model.tipoIdTipoDocumento)%>

                                    <label id="lblNumeroDocumento" style="margin-left: 8px;">
                                    <%: Html.LabelFor(model => _model.pacNumeroDocumento, "Número: ")%>
                                    </label>
                                    <%: Html.EditorFor(model => _model.pacNumeroDocumento, "", "txtpacNumeroDocumento")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacNumeroDocumento)%>


                                     <%: Html.LabelFor(model => _model.pacApellido)%>
                                    <%: Html.EditorFor(model => _model.pacApellido,"","txtpacApellido2")%>   
                                    <%: Html.ValidationMessageFor(model => _model.pacApellido)%>

                                    <%: Html.LabelFor(model => _model.pacNombre, "Nombre: ")%>
                                    <%: Html.EditorFor(model => _model.pacNombre, "LimitedTextBox", "txtpacNombre")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacNombre)%>
                                </div>
                                <div class="editor-label" style="vertical-align: middle;">
                                    <%: Html.LabelFor(model => _model.pacCUIL, "CUIL/CUIT:")%>
                                    <%: Html.EditorFor(model => _model.pacCUIL, "", "txtpacCUIL")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacCUIL)%>

                                    <%: Html.LabelFor(model => _model.paisId, "Nacionalidad:")%>
                                    <%: Html.EditorFor(model => _model.paisId, "GridForeignKeyComboBox", "paisId")%>
                                    <%: Html.ValidationMessageFor(model => _model.paisId)%>

                                 <label id="lblSexo" style="margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.tipoIdSexo, "Sexo:")%>
                                 </label>
                                    <%: Html.EditorFor(model => _model.tipoIdSexo, "GridForeignKeyComboBox", "tipoIdSexo")%>


                                 <label id="lblpacFechaNacimiento" style="margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.pacFechaNacimiento)%>
                                  
                                 </label>
                                    <%: Html.EditorFor(model => _model.pacFechaNacimiento, "", "txtpacFechaNacimiento")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacFechaNacimiento)%>
                                 </div>
                                 <div class="editor-label" style="vertical-align: middle;">
                                 <label id="lblEstadoCivil" style="vertical-align: middle;">
                                      <%: Html.LabelFor(model => _model.tipoIdEstadoCivil, "Estado Civil:")%>
                                  </label>
                                    <%: Html.EditorFor(model => _model.tipoIdEstadoCivil, "GridForeignKeyComboBox", "tipoIdEstadoCivil")%>
                                  <label id="lblOcupacion" style="margin-left: 13px; margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.tipoIdOcupacion, "Ocupación:")%>
                                  </label>
                                    <%: Html.EditorFor(model => _model.tipoIdOcupacion, "GridForeignKeyComboBox", "tipoIdOcupacion")%>
                                  <label id="lblDonaOrganos" style="vertical-align: middle;">
                                    <%: Html.LabelFor(model => _model.pacDonaOrganos, "Dona Órganos:")%>
                                  </label>
                                    <%: Html.EditorFor(model => _model.pacDonaOrganos, "", "chkDonaOrganos")%>
                                  <label id="lblGrupoSanguineo" style="vertical-align: middle; margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.tipoIdGrupoSanguineo, "Grupo Sanguíneo:") %>
                                  </label>
                                    <%: Html.EditorFor(model => _model.tipoIdGrupoSanguineo, "GridForeignKeyComboBox", "tipoIdGrupoSanguineo")%>
                                </div>
                                <div class="BordeSombreado" style="margin: 8px ;vertical-align: middle;">
                                </div>
                                <div class="editor-label" style="vertical-align: middle;">
                                    <div style="vertical-align: middle; height: 40px; margin-top: 5px;">
                                 
                                    <label id="lblCalle" style="margin-left: 0px;">
                                         <%: Html.LabelFor(model => _model.pacCalle, "Calle:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.pacCalle, "LimitedTextBox", "txtpacCalle") %>
                                         <%: Html.ValidationMessageFor(model => _model.pacCalle)%>
                                    <label id="lblCalleNumero" style="margin-left: 40px;">
                                         <%: Html.LabelFor(model => _model.pacCalleNumero, "Número:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.pacCalleNumero, "Integer", "txtpacCalleNumero") %>
                                         <%: Html.ValidationMessageFor(model => _model.pacCalle)%>
                                    <label id="lblDomicilioReferencias" style="margin-left: 34px; vertical-align:middle" >
                                         <%: Html.LabelFor(model => _model.pacDomicilioReferencias, "Referencia de Calle:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.pacDomicilioReferencias, "LimitedTextBox", "txtpacDomicilioReferencias", new { style = "width: 70px;" })%>
                                         <%: Html.ValidationMessageFor(model => _model.pacDomicilioReferencias)%>                                       
                                    </div>
                                    <label id="lblDepartamento" style="margin-left: 0px;vertical-align:middle" >
                                         <%: Html.LabelFor(model => _model.depId, "Departamento:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.depId, "GridForeignKeyComboBoxOnChange", "depId")%>                                    
                                    <label id="lblLocalidad" style="margin-left: 40px;vertical-align:middle" >
                                         <%: Html.LabelFor(model => _model.locId, "Localidad:")%>
                                     </label>
                                         <%: Html.EditorFor(model => _model.locId, "GridForeignKeyComboBoxOnChange", "locId")%> 
                                    
                                     <label id="lblBarrio" style="margin-left: 40px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.barId, "Barrio:")%>
                                     </label>
                                        <%: Html.EditorFor(model => _model.barId, "GridForeignKeyComboBox", "barId")%> 
                                        <br>
                                    </br>  
                                    <div style="vertical-align: middle; height: 30px; margin-top: 10px;">
                                    <label id="lblTelefonoCasa" style="margin-left:0px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.pacTelefonoCasa, "Teléfono de Casa:")%>
                                    </label>
                                        <%: Html.EditorFor(model => _model.pacTelefonoCasa, "LimitedTextBox", "txtpacTelefonoCasa")%>                                                                            
                                        <%: Html.ValidationMessageFor(model => _model.pacTelefonoCasa)%>                                     
                                    <label id="lblTelefonotrabajo" style="margin-left: 60px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.pacTelefonoTrabajo, "Teléfono de Trabajo:")%>
                                    </label>                                    
                                         <%: Html.EditorFor(model => _model.pacTelefonoTrabajo, "LimitedTextBox", "txtpacTelefonoTrabajo")%>  
                                         <%: Html.ValidationMessageFor(model => _model.pacTelefonoTrabajo)%>     
                                    </div>                                   
                                    <div style="vertical-align: middle; height: 30px; margin-top: 5px;">
                                    <label id="lbltelefonocelular" style="margin-left: 0px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.pacTelefonoCelular, "Teléfono Celular:")%>
                                    </label>
                                        <%: Html.EditorFor(model => _model.pacTelefonoCelular, "LimitedTextBox", "txtpacTelefonoCelular")%> 
                                       <%: Html.ValidationMessageFor(model => _model.pacTelefonoCelular)%>                                                  
                                    
                                    <label id="lblNotxSMS" style="vertical-align: middle; margin-left: 30px;">
                                        <%: Html.LabelFor(model => _model.pacNotificarXSMS, "Notificar por SMS:")%>
                                     </label>
                                         <%: Html.EditorFor(model => _model.pacNotificarXSMS, "", "chkNotificarxSMS")%>
                                     <label id="lblObraSocial" style="vertical-align: middle; margin-left: 30px;">
                                         <%: Html.LabelFor(model => _model.osId, "Obra Social:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.osId, "GridForeignKeyComboBox", "osId") %>  
                                     <label id="lblHospitalizado" style="vertical-align: middle; margin-left: 30px;">
                                        <%: Html.LabelFor(model => _model.pacHospitalizado, "Hospitalizado:")%>
                                     </label>          
                                       <%: Html.EditorFor(model => _model.pacHospitalizado, "", "chkpacHospitalizado")%>                       
                                    </div>
                                    
                                    
                                </div>
                            </div>   
                        <%});
                })
            .SelectedIndex(0)
            .Render();
            %>          
            </div>
            <div style="position: relative; top: 10px; left: 93%; width: 30px; height: 20px">
                    <table>
                        <tr>
                            <td style="border: none;">
                                <button class="t-button" id="onAceptar">
                                    <img src="/GeDoc/Content/General/Vacio-Transparente.png" alt="Aceptar" height="15" width="15" style="<%: _btnAceptar %>" />                                    
                                </button>
                            </td>
                            <td style="border: none;">
                                <button class="t-button" onclick="onCancelar()">
                                    <img src="/GeDoc/Content/General/Vacio-Transparente.png" alt="Cancelar" height="15" width="15" style="<%: _btnCancelar %>" />                                    
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
                           
            <%
        })
       .Buttons(b => b.Close())       
       .Draggable(true)
       .ClientEvents(eventos => eventos.OnActivate("onActivatePaciente"))       
       .Scrollable(true)     
       .Width(1124)
       .Height(400)
       .Render();

 } 
 %>



 <%  
   using (Ajax.BeginForm("Edit", "catPaciente", null, new AjaxOptions() { OnSuccess = "onSuccess" }, new { id = "frmEditPaciente", name = "frmEditPaciente" }))
   //using (Ajax.BeginForm("Create", new AjaxOptions() { OnSuccess="onSuccess" }))
   { 
      GeDoc.catPacientes _model = (ViewData["Paciente"] as GeDoc.catPacientes);    
      //_model.pacApellido = "";       
%>
<% string _btnAceptar = "";
   string _btnCancelar = "";
   _btnAceptar = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -33px -335px;";
   _btnCancelar = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -49px -335px;";
%>    
 <% Html.Telerik().Window()
        .Name("CRUDEDITPaciente")
        .Title("Ficha de Pacientes")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div class="">
            <%
            Html.Telerik().TabStrip()
                .Name("tabEditPaciente")
                .HtmlAttributes(new { style = "padding: 3px 0px 0px 0px; background: transparent; border: 0px;" })
                .Items(tabstrip =>
                {
                    tabstrip.Add()
                        .Text("Datos Filiatorios")
                        .ContentHtmlAttributes(new { style = "padding: 0px;" })
                        .Content(() =>                            
                        { %>
                            <div id="Div1" style="width: auto; height: 100%;">
                                <div class="editor-label" style="vertical-align: middle;">
                                    <div id="Div2" style="color:Red"></div>
                                   
                                    <%: Html.LabelFor(model => _model.tipoIdTipoDocumento, "Tipo de Documento: ")%>
                                    <%: Html.EditorFor(model => _model.tipoIdTipoDocumento, "", "tipoIdTipoDocumentoedit")%>
                                    <%: Html.ValidationMessageFor(model => _model.tipoIdTipoDocumento)%>

                                    <label id="Label1" style="margin-left: 8px;">
                                    <%: Html.LabelFor(model => _model.pacNumeroDocumento, "Número: ")%>
                                    </label>
                                    <%: Html.EditorFor(model => _model.pacNumeroDocumento, "", "txtpacNumeroDocumento_edit")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacNumeroDocumento)%>


                                     <%: Html.LabelFor(model => _model.pacApellido)%>
                                    <%: Html.EditorFor(model => _model.pacApellido,"","txtpacApellido2_edit")%>   
                                    <%: Html.ValidationMessageFor(model => _model.pacApellido)%>

                                    <%: Html.LabelFor(model => _model.pacNombre, "Nombre: ")%>
                                    <%: Html.EditorFor(model => _model.pacNombre, "LimitedTextBox", "txtpacNombre_edit")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacNombre)%>
                                </div>
                                <div class="editor-label" style="vertical-align: middle;">
                                    <%: Html.LabelFor(model => _model.pacCUIL, "CUIL/CUIT:")%>
                                    <%: Html.EditorFor(model => _model.pacCUIL, "", "txtpacCUIL_edit")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacCUIL)%>

                                    <%: Html.LabelFor(model => _model.paisId, "Nacionalidad:")%>
                                    <%: Html.EditorFor(model => _model.paisId, "GridForeignKeyComboBox", "paisIdedit")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacCUIL)%>

                                 <label id="Label2" style="margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.tipoIdSexo, "Sexo:")%>
                                 </label>
                                    <%: Html.EditorFor(model => _model.tipoIdSexo, "", "tipoIdSexoedit")%>


                                 <label id="Label3" style="margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.pacFechaNacimiento)%>
                                  
                                 </label>
                                    <%: Html.EditorFor(model => _model.pacFechaNacimiento, "", "txtpacFechaNacimiento_edit")%>
                                    <%: Html.ValidationMessageFor(model => _model.pacFechaNacimiento)%>
                                 </div>
                                 <div class="editor-label" style="vertical-align: middle;">
                                 <label id="Label4" style="vertical-align: middle;">
                                      <%: Html.LabelFor(model => _model.tipoIdEstadoCivil, "Estado Civil:")%>
                                  </label>
                                    <%: Html.EditorFor(model => _model.tipoIdEstadoCivil, "GridForeignKeyComboBox", "tipoIdEstadoCiviledit")%>
                                  <label id="Label5" style="margin-left: 13px; margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.tipoIdOcupacion, "Ocupación:")%>
                                  </label>
                                    <%: Html.EditorFor(model => _model.tipoIdOcupacion, "GridForeignKeyComboBox", "tipoIdOcupacionedit")%>
                                  <label id="Label6" style="vertical-align: middle;">
                                    <%: Html.LabelFor(model => _model.pacDonaOrganos, "Dona Órganos:")%>
                                  </label>
                                    <%: Html.EditorFor(model => _model.pacDonaOrganos, "", "chkDonaOrganos_edit")%>
                                  <label id="Label7" style="vertical-align: middle; margin-left: 40px;">
                                    <%: Html.LabelFor(model => _model.tipoIdGrupoSanguineo, "Grupo Sanguíneo:") %>
                                  </label>
                                    <%: Html.EditorFor(model => _model.tipoIdGrupoSanguineo, "GridForeignKeyComboBox", "tipoIdGrupoSanguineo")%>
                                </div>
                                <div class="BordeSombreado" style="margin: 8px ;vertical-align: middle;">
                                </div>
                                <div class="editor-label" style="vertical-align: middle;">
                                    <div style="vertical-align: middle; height: 40px; margin-top: 5px;">
                                 
                                    <label id="Label8" style="margin-left: 0px;">
                                         <%: Html.LabelFor(model => _model.pacCalle, "Calle:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.pacCalle, "LimitedTextBox", "txtpacCalle_edit")%>
                                         <%: Html.ValidationMessageFor(model => _model.pacCalle)%>
                                    <label id="Label9" style="margin-left: 40px;">
                                         <%: Html.LabelFor(model => _model.pacCalleNumero, "Número:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.pacCalleNumero, "Integer", "txtpacCalleNumero_edit")%>
                                         <%: Html.ValidationMessageFor(model => _model.pacCalle)%>
                                    <label id="Label10" style="margin-left: 34px; vertical-align:middle" >
                                         <%: Html.LabelFor(model => _model.pacDomicilioReferencias, "Referencia de Calle:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.pacDomicilioReferencias, "LimitedTextBox", "txtpacDomicilioReferencias_edit", new { style = "width: 70px;" })%>
                                         <%: Html.ValidationMessageFor(model => _model.pacDomicilioReferencias)%>                                       
                                    </div>
                                    <label id="Label11" style="margin-left: 0px;vertical-align:middle" >
                                         <%: Html.LabelFor(model => _model.depId, "Departamento:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.depId, "GridForeignKeyComboBoxOnChange", "depIdedit")%>                                    
                                    <label id="Label12" style="margin-left: 40px;vertical-align:middle" >
                                         <%: Html.LabelFor(model => _model.locId, "Localidad:")%>
                                     </label>
                                         <%: Html.EditorFor(model => _model.locId, "GridForeignKeyComboBox", "locIdedit")%> 
                                    
                                     <label id="Label13" style="margin-left: 40px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.barId, "Barrio:")%>
                                     </label>
                                        <%: Html.EditorFor(model => _model.barId, "GridForeignKeyComboBox", "barIdedit")%> 
                                        <br>
                                    </br>  
                                    <div style="vertical-align: middle; height: 30px; margin-top: 10px;">
                                    <label id="Label14" style="margin-left:0px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.pacTelefonoCasa, "Teléfono de Casa:")%>
                                    </label>
                                        <%: Html.EditorFor(model => _model.pacTelefonoCasa, "LimitedTextBox", "txtpacTelefonoCasa_edit")%>                                                                            
                                        <%: Html.ValidationMessageFor(model => _model.pacTelefonoCasa)%>                                     
                                    <label id="Label15" style="margin-left: 60px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.pacTelefonoTrabajo, "Teléfono de Trabajo:")%>
                                    </label>                                    
                                         <%: Html.EditorFor(model => _model.pacTelefonoTrabajo, "LimitedTextBox", "txtpacTelefonoTrabajo_edit")%>  
                                         <%: Html.ValidationMessageFor(model => _model.pacTelefonoTrabajo)%>     
                                    </div>                                   
                                    <div style="vertical-align: middle; height: 30px; margin-top: 5px;">
                                    <label id="Label16" style="margin-left: 0px;vertical-align:middle" >
                                        <%: Html.LabelFor(model => _model.pacTelefonoCelular, "Teléfono Celular:")%>
                                    </label>
                                        <%: Html.EditorFor(model => _model.pacTelefonoCelular, "LimitedTextBox", "txtpacTelefonoCelular_edit")%> 
                                       <%: Html.ValidationMessageFor(model => _model.pacTelefonoCelular)%>                                                  
                                    
                                    <label id="Label17" style="vertical-align: middle; margin-left: 30px;">
                                        <%: Html.LabelFor(model => _model.pacNotificarXSMS, "Notificar por SMS:")%>
                                     </label>
                                         <%: Html.EditorFor(model => _model.pacNotificarXSMS, "", "chkNotificarxSMS_edit")%>
                                     <label id="Label18" style="vertical-align: middle; margin-left: 30px;">
                                         <%: Html.LabelFor(model => _model.osId, "Obra Social:")%>
                                    </label>
                                         <%: Html.EditorFor(model => _model.osId, "GridForeignKeyComboBox", "osIdedit")%>  
                                     <label id="Label19" style="vertical-align: middle; margin-left: 30px;">
                                        <%: Html.LabelFor(model => _model.pacHospitalizado, "Hospitalizado:")%>
                                     </label>          
                                       <%: Html.EditorFor(model => _model.pacHospitalizado, "", "chkpacHospitalizado_edit")%>                       
                                    </div>
                                    
                                    
                                </div>
                            </div>   
                        <%});
                })
            .SelectedIndex(0)
            .Render();
            %>          
            </div>
            <div style="position: relative; top: 10px; left: 93%; width: 30px; height: 20px">
                    <table>
                        <tr>
                            <td style="border: none;">
                                <button class="t-button" id="onAceptar2">
                                    <img src="/GeDoc/Content/General/Vacio-Transparente.png" alt="Aceptar" height="15" width="15" style="<%: _btnAceptar %>" />                                    
                                </button>
                            </td>
                            <td style="border: none;">
                                <button class="t-button" onclick="onCancelar()">
                                    <img src="/GeDoc/Content/General/Vacio-Transparente.png" alt="Cancelar" height="15" width="15" style="<%: _btnCancelar %>" />                                    
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
                           
            <%
        })
       .Buttons(b => b.Close())       
       .Draggable(true)
       .ClientEvents(eventos => eventos.OnActivate("onActivatePaciente"))       
       .Scrollable(true)     
       .Width(1124)
       .Height(400)
       .Render();

 } %>
 
<style type="text/css" xml:lang="es-AR">
    input[type="text"] 
    {
        text-transform: uppercase;
    }
</style>