<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc.Models" %>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>

<% Html.RenderPartial("Waiting"); %>

<% Html.RenderPartial("ConsultaPaciente"); %>
<script type="text/javascript">
    var _RowIndex = -1;
    var _DatosRegistro;
    var _RowIndexHorario = -1;
    var _DatosRegistroHorario;
    var _EsModificar = false;
    var _pacId = -1;
    var _auxEditar = 0;

    function RefrescarPaciente() {
        var grid = $("#Grid").data("tGrid");
        // var gridPacientes = $("#GridPacientes").data("tGrid");
        grid.rebind();
        // gridPacientes.rebind();
    } 
    function onRowSelectedPac(e) {
        debugger;
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        _pacId = _DatosRegistro['pacId'];
        grid.SelectedId = _pacId;
    }
    function onCommandPac(e) {
        _EsModificar = false;
        if (("cmdEditar, cmdHistoriaClinica").indexOf(e.name) >= 0) {
            if (_RowIndex < 0) {
                jAlert('Debe seleccionar un Paciente.', 'Error...');
                return;
            }
        }
        switch (e.name) {
            case "cmdAgregar":
                
                //var grid = $(this).data("tGrid");
                //grid.addRow();
                abrirFichaPaciente();

                break;
            case "cmdEditar":
                debugger;
                var grid = $(this).data("tGrid");
                //var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                //grid.editRow(tr);

                abrirFichaPaciente(grid.SelectedId);

                break;
            case "cmdVerFichaPaciente":
                var grid = $(this).data("tGrid");
                if (_pacId != null) {
                    AbrirWaiting();
                    debugger;
                    $.post(GetPathApp('catPaciente/ViewDetails'), { pacienteId: _pacId }, function (data) {
                        if (data.InfoPaciente != null) {
                            CerrarWaiting();
                            VerLegajo(data.InfoPaciente);
                        }
                    });
                } else {
                    jAlert('Debe seleccionar el Paciente', 'Error...');
                    return;
                }
                break;
            case "cmdHistoriaClinica":
                AbrirWaitingCRUD();
                var _Post = GetPathApp("HistoriaClinica/getMenuHC");
                $.ajax({
                    url: _Post,
                    data: { pacId: _pacId },
                    type: 'POST',
                    error: function (xhr, ajaxOptions, thrownError) {
                        debugger;
                        CerrarWaiting();
                        jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                        });
                        $('#popup_container').focus();
                        $('#popup_ok').focus();
                    },
                    success: function (data) {
                        CerrarWaitingCRUD();
                        var _WindowHC = $("#divHistoriaClinica");
                        _WindowHC.html(data);
                    }
                });

                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }

    function abrirFichaPaciente(pacId) {
        var contentUrl = "../catPaciente/Paciente" + (pacId ? "?pacId=" + pacId : "");
        var thePatient = $.telerik.window.create({
            title: "Un momento...",
            html: "<img width='150px' src='/GeDoc/Content/General/WaitingIndicator.gif'/>",
            contentUrl: contentUrl,
            modal: true,
            resizable: false,
            draggable: true,
            scrollable: false,
            onLoad: function () {
                $(this).data("tWindow").center();
            },
            onRefresh: function () {
                $(this).data("tWindow").center();
            },
            onClose: function () {
                this.remove();
            }
        });
        window.fichaPaciente = $(thePatient).data("tWindow");
    }

    function VerLegajo(DatosPaciente) {
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
        else {            
        }
        _pacId = _Paciente.pacId;
        $("#lblnroHC").text(_Paciente.nroHC);
        $("#lbltipoIdSexo").text(_Paciente.tipoSexoNombre);
        $("#lblperTipoDoc").text(_Paciente.tipoDescDocumento);
        $('#imgFoto').attr('src', _Foto);
        $("#lblApellidoyNombre").text(_Paciente.pacApellido + ' ' + _Paciente.pacNombre);       
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
        $("#lblpacDonaOrganos").text(_Paciente.pacDonaOrganos ? 'SI' : 'NO');
        $("#lblcentroSalud").text(_Paciente.csId);
      //  $("#lblOcupacionNombre").text(_Paciente.);
        
        $("#lblRecibeSMS").text(_Paciente.pacNotificarXSMS ? 'SI' : 'NO');
        $("#lblperEmail").text(_Paciente.pacMail);
        detailWindow.center().open();
    }

    function onComplete(e) {
        debugger;
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
        }
    }
    function onSave(e) {
        debugger;
        var values = e.values;
        _pacId = values.pacId;
    }
    function onDataBindingPacientes(e) {
        debugger;
        var id;
        e.data = $.extend(e.data, { id: _pacId });
    }

    function onComboBoxLoad(e) {
        debugger;
        //$("#Grid").data("tComboBox").fill();
    }
    //evento para ingreso del dni
    $('#tipoIdTipoDocumento')
       .bind('valueChange', function (e) {
           debugger;
           // Obtenerdatos_CONPAD();
           alert("cambio");
       });

    function onCommandEditPaciente(e) {
        debugger;
        var _Padron = $('#pacPadron');
        _Padron.prop('disabled', true);
        $("#tipoIdTipoDocumento").bind('valueChange',function () { validarTipoDocumento(); });
        if (e.mode != 'edit') {          
            $("#pacNumeroDocumento").bind('valueChange', function () { Obtenerdatos_CONPAD(); });
        }

        if(e.mode === "edit")
            rebindAddressComboBox(e.dataItem);
    }

    function rebindAddressComboBox(pacObj) {
        
        provIdOnChange({ value: pacObj.provId }, pacObj.depId, function() {
            depIdOnChange({ value: pacObj.depId }, pacObj.locId, pacObj.barId);
        });
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

    function provIdOnChange(e, depId, callback) {
        $.post(window.GetPathApp("catPaciente/BindingDepartamento"), { provId: e.value }, function (d) {
            var cbDepId = $("#depId").data("tComboBox");
            cbDepId.dataBind(d);
            var selectedIndex = searchIndexOnComboBox(cbDepId, depId);
            cbDepId.select(selectedIndex);
            $(cbDepId).focus();
            if (callback && typeof callback === "function") callback();
        });
    }

    function depIdOnChange(e, locId, barId) {
        $.post(window.GetPathApp("catPaciente/BindingLocalidades"), { depId: e.value }, function (locData) {
            var cbLocId = $("#locId").data("tComboBox");
            cbLocId.dataBind(locData);
            var locSelectedIndex = searchIndexOnComboBox(cbLocId, locId);
            cbLocId.select(locSelectedIndex);
            $.post(window.GetPathApp("catPaciente/BindingBarrios"), { depId: e.value }, function (barData) {
                var cbBarId = $("#barId").data("tComboBox");
                cbBarId.dataBind(barData);
                var barSelectedIndex = searchIndexOnComboBox(cbBarId, barId);
                cbBarId.select(barSelectedIndex);
            });
        });       
    }

    function validarTipoDocumento() {
        debugger;
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
    function Obtenerdatos_Paciente(pacId) {
        debugger;
        var _documento = $('#txtpacNumeroDocumento').val();
        var _tipoDoc = $('#tipoIdTipoDocumento').val();
      
        AbrirWaiting();

        var _Post = GetPathApp('catPaciente/_BindingPaciente');
        $.getJSON(_Post, { id: pacId }, function (json) {
            debugger;

            if (json) {
                var newDate = JSONDate(json.pacFechaNacimiento);
                var tipoIdTipoDocumento = json.tipoIdTipoDocumento;
                if (tipoIdTipoDocumento > 0) {
                    switch (tipoIdTipoDocumento) {
                        case 37:
                            $("#tipoIdTipoDocumento").data("tComboBox").select(0);
                            break;
                        case 38:
                            $("#tipoIdTipoDocumento").data("tComboBox").select(1);
                            break;
                        case 39:
                            $("#tipoIdTipoDocumento").data("tComboBox").select(2);
                            break;
                        case 40:
                            $("#tipoIdTipoDocumento").data("tComboBox").select(3);
                            break;
                        default:
                            $("#tipoIdTipoDocumento").data("tComboBox").select(0);
                    }
                }
                jQuery("#paisId").data("tComboBox").select(0); //elegir el pais 
                jQuery("#txtpacNumeroDocumento_edit").val(json.pacNumeroDocumento);
                jQuery("#txtpacApellido2_edit").val(json.pacApellido);
                jQuery("#txtpacNombre_edit").val(json.pacNombre);
                jQuery("#txtpacFechaNacimiento_edit").val(newDate);
                jQuery("#txtpacCalle_edit").val(json.pacCalle);
                jQuery("#txtpacCalleNumero_edit").val(json.pacCalleNumero);
                jQuery("#txtpacDomicilioReferencias_edit").val(json.pacDomicilioReferencias);
                jQuery("#txtpacCUIL_edit").val(json.pacCUIL);
                if (json.tipoIdSexo == 9) {
                    $('#tipoIdSexoedit').data("tComboBox").select(0);
                }
                if (json.tipoIdSexo == 10) {
                    $('#tipoIdSexoedit').data("tComboBox").select(1);
                }

                var estadoCivil = json.tipoIdEstadoCivil;
                if (estadoCivil > 0) {
                    switch (estadoCivil) {
                        case 26:
                            $("#tipoIdEstadoCiviledit").data("tComboBox").select(0);
                            break;
                        case 27:
                            $("#tipoIdEstadoCiviledit").data("tComboBox").select(1);
                            break;
                        case 28:
                            $("#tipoIdEstadoCiviledit").data("tComboBox").select(2);
                            break;
                        case (estadoCivil == 29):
                            $("#tipoIdEstadoCiviledit").data("tComboBox").select(3);
                            break;
                        case 30:
                            $("#tipoIdEstadoCiviledit").data("tComboBox").select(4);
                            break;
                        default:
                            $("#tipoIdEstadoCiviledit").data("tComboBox").select(0);
                   }

                }

                var tipoIdocupacion = json.tipoIdOcupacion;
                if (tipoIdocupacion > 0) {
                    switch (tipoIdocupacion) {
                        case 56:
                            $("#tipoIdOcupacionedit").data("tComboBox").select(0);
                            break;
                        case 57:
                            $("#tipoIdOcupacionedit").data("tComboBox").select(1);
                            break;
                        case 58:
                            $("#tipoIdOcupacionedit").data("tComboBox").select(2);
                            break;
                        case 59:
                            $("#tipoIdOcupacionedit").data("tComboBox").select(3);
                            break;
                        case 60:
                            $("#tipoIdOcupacionedit").data("tComboBox").select(4);
                            break;
                        default:
                            $("#tipoIdOcupacionedit").data("tComboBox").select(0);
                    }
                }
                jQuery("#txtpacTelefonoCasa_edit").val(json.pacTelefonoCasa);
                jQuery("#txtpacTelefonoTrabajo_edit").val(json.pacTelefonoTrabajo);
                jQuery("#txtpacTelefonoCelular_edit").val(json.pacTelefonoCelular);
                jQuery("#chkNotificarxSMS_edit").prop('checked', json.pacNotificarXSMS);
                jQuery("#chkDonaOrganos_edit").prop('checked', json.pacDonaOrganos);
                jQuery("#chkpacHospitalizado_edit").prop('checked', json.pacHospitalizado);

            }
            else {
                jAlert("No se encontraron datos en el Padron de Personas, para el Nro.: " + _documento, 'Mensaje...');
                CerrarWaiting();
                return;
            }
            CerrarWaiting();
        }, "json");
    }
</script>
<div style="overflow: hidden; height: 510px;" >
<%: Html.Telerik().Grid<GeDoc.catPacientes>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.pacId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdVerFichaPaciente").ButtonType(GridButtonType.ImageAndText).Text("Consultar")
                .HtmlAttributes(new {style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Examinar")})
                .ImageHtmlAttributes(new {style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;"});
            commands.Custom().Ajax(true).Name("cmdHistoriaClinica").ButtonType(GridButtonType.ImageAndText).Text("Historia Clínica")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Examinar") })
                .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -240px;" });
            
            //commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
            //        .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
            //        .Text("HC Adulto")
            //        .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Agregar") });
        
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catPaciente")
                .Insert("_InsertEditing", "catPaciente")
                .Update("_SaveEditing", "catPaciente");               
        })
        .Localizable("es-AR")
        .Columns(columns =>
        {
           
            columns.Bound(c => c.pacApellidoyNombre).Width("250px").Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
          .ClientTemplate("<label title='<#= pacApellidoyNombre #>' style='cursor: pointer;' id='txtpacApellidoyNombre' ><#= pacApellidoyNombre #></label>");

            columns.Bound(c => c.pacNumeroDocumento).Width("250px").Title("Numero Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
               
                .ClientTemplate("<label title='<#= pacNumeroDocumento #>' style='cursor: pointer;' id='txtpacNumeroDocumento2' ><#= pacNumeroDocumento #></label>");
             columns.Bound(c => c.tipoDescDocumento).Width("250px").Title("Tipo Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= tipoDescDocumento #>' style='cursor: pointer;' id='txttipoDescDocumento' ><#= tipoDescDocumento #></label>");                 
                columns.Bound(c => c.tipoSexoNombre).Width("250px").Title("Sexo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= tipoSexoNombre #>' style='cursor: pointer;' id='tipoSexoNombre' ><#= tipoSexoNombre #></label>");              
            
                            })  
             .Editable(editing => editing.Enabled(true)
                .Mode(GridEditMode.PopUp)      
                   
                )
                .Pageable((paging) =>
                           paging.Enabled(true)
                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(clientEvents => clientEvents.OnRowSelected("onRowSelectedPac").OnEdit("onCommandEditPaciente").OnCommand("onCommandPac").OnRowDataBound("onRowDataBound").OnSave("onSave"))
                
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true)
            .Height(((int)Session["AlturaGrilla"])-50))
            
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()               
           
%>

<% Html.RenderPartial("WaitingCRUD"); %>

<div id="divHistoriaClinica"></div>

<style type="text/css" xml:lang="es-AR">
    input[type="text"] 
    {
        width: 200px;
        border: 1px solid #CCC;
        height: 17px;
        padding: 3px;
        text-transform: uppercase;
    }
</style>
<% Html.Telerik().ScriptRegistrar()
  .DefaultGroup(group => group
   .Add("MicrosoftAjax.js")
   .Add("MicrosoftMvcValidation.js")); %>
