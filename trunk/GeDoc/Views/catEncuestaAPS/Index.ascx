<!DOCTYPE html>

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
    function onRowSelectedEncuesta(e) {
        
        var row = e.row;
        var grid = $("#Grid").data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = kludgeDatosRegistro = dataItem;
        _encId = _DatosRegistro['encId'];
        encId = _encId;
    }
    function onEditEncuesta(e) {
        
        var dataItem = e.dataItem;
        var mode = e.mode;
        var form = e.form;
        if (e.mode === 'edit') {
            _encId = dataItem.encId;
        }
    }
    function onRowSelectedEncuestados(e) {
        
        var row = e.row;
        var grid = $("#gridEncuestados").data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        _encPerId = _DatosRegistro['encPerId'];
       
        
        _pacId = _DatosRegistro['pacId'];
        _encId = _DatosRegistro['encId'];
    }
    function Refrescar() {
        
        var grid = $("#gridEncuestados").data("tGrid");
        grid.ajaxRequest();
    }

    function onCommandPaciente(e) {
        
        var Accion;
        if (e.name == "cmdAgregarPacienteTab") {
            
            var _WindowPac = $("#wCrudTabPacientes").data("tWindow");
            var _Post = GetPathApp("catEncuestaAPS/TabPaciente");
            Accion = "Agregar";
            AbrirWaiting();
            _WindowPac.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
            $.ajax({
                url: _Post,
                data: { Accion: "Agregar", pacId: -1,  encId: _encId},
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
                    //  $("#barId").data("tComboBox").select((data.barId - 1));
                    _WindowPac.center().title(Accion).open();
                    onCommandEdit();
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
        $("#tipoIdTipoDocumento").bind("valueChange", function() {
             validarTipoDocumento();
        });
        if (e.mode !== "edit") {
            $("#pacNumeroDocumento").bind("valueChange", function() {
                 Obtenerdatos_CONPAD();
            });
        }
    }

    function onSavePaciente(e) {
        
        var values = e.values;
        _pacId = values.pacId;
    }
    function onCommandEncuestado(e) {
        debugger;
        var _WindowPac;
        if (e.name == "cmdAgregarPersonas") {
            
            //var _WPacientess = $("#WPacientes").data("tWindow");
            //var _GPacientes = $('#gridPacientes').data('tGrid');

            //_WPacientess.center().open();
            //_GPacientes.ajaxRequest();
            var _Post = GetPathApp("catEncuestaAPS/TabPaciente");
            _WindowPac = $("#wCrudTabPacientes").data("tWindow");
            Accion = "Agregar";
            AbrirWaiting();
            $.ajax({
                url: _Post,
                data: { Accion: "Agregar", pacId: -1, encId: _encId},
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
                    _WindowPac.title("Personas encuestadas de la encuesta Nro " + _encId).open();
                    $("#pacNumeroDocumento").focus().select();
                }
            });
        }
        if (e.name == "cmdListarEnfermedadEncuestado") {
            
            var _WPacienteEnfermedad = $("#WPacienteEnfermedad").data("tWindow");
            var _gridPacienteEnfermdedad = $('#gridPacienteEnfermdedad').data('tGrid');
            _DatosRegistro_persona = e.dataItem;
            _encPerId = _DatosRegistro_persona.encPerId;
            _WPacienteEnfermedad.center().open();
            _gridPacienteEnfermdedad.ajaxRequest();
        }
        if (e.name == "cmdEditarEncuestado") {

            onRowSelectedEncuestados(e);

            var _Post = GetPathApp("catEncuestaAPS/TabPaciente");
            _WindowPac = $("#wCrudTabPacientes").data("tWindow");
            Accion = "Modificar";
            AbrirWaiting();
            $.ajax({
                url: _Post,
                data: { Accion: "Modificar",  pacId: _pacId, encId: _encId },
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
                      _WindowPac.center().title(Accion).open();
                      _WindowPac.title("Personas encuestadas de la encuesta Nro " + _encId).open();
                      $("#pacNumeroDocumento").focus().select();
                }
            });
        }
    }
    
    function onCommandEncuesta(e) {
        debugger;
        if (e === "Agregar") {
            var grid = $("#Grid").data("tGrid");
            grid.addRow();
        }
        if (e == "cmdEditarEncuesta") {
            var grid = $("#Grid").data("tGrid");
            var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
            console.log(tr);
            console.log(_RowIndex);
            grid.editRow(tr);
            $("#GridPopUp").data("tWindow").title("Editar Encuesta Nº " + $("#Grid").data("tGrid").data[_RowIndex].encId);
        }
        if (e == "cmdPersonas") {
            
            var _WEncuestados = $("#WEncuestados").data("tWindow");
            var _GEncuestados = $('#gridEncuestados').data('tGrid');

            if (_encId == "" || _encId == null) {
                jAlert('Debe seleccionar una Encuesta', '¡Atención!', function () { });
            }
            else {
                var _Post = GetPathApp('catEncuestaAPS/getValidaSiEstaAsignado');
                $.ajax({
                    url: _Post,
                    data: { _encId: _encId },
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
                        } else {
                            jAlert('No se puede Asignar una Persona, esta Encuesta NO está disponible', 'Error...');
                            var _Grid = $('#Grid').data('tGrid');
                            return;
                        }
                    }
                });
            }
            
        }
        // onCommandEdit(e);
    }
    function AsignarPaciente(pacienteId, Nombre, Preguntar, Enfermedades) {
        
        var Enfermedades = Enfermedades || [];
        var _Post = GetPathApp('catEncuestaAPS/getValidaSiPacienteAsignado');
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
                                $.post(GetPathApp('catEncuestaAPS/AsignarPacienteEncuesta'), { pacId: pacienteId, encId: _encId }, function (data) {
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
                        $.post(GetPathApp('catEncuestaAPS/AsignarPacienteEncuesta'), { pacId: pacienteId, encId: _encId }, function (data) {
                            CerrarWaiting();
                            if (!data.IsValid) {
                                jAlert(data.Mensaje, 'Error...');
                            } else {

                                _encPerId = data.encPerId;
                                if (_encPerId != null) {
                                    
                                    for (i in Enfermedades) {
                                        _enfId = Enfermedades[1];
                                        $.post(GetPathApp('catEncuestaAPS/AsignarPersonaEnfermedad'), { enfId: _enfId, encPerId: _encPerId }, function (data) {
                                            CerrarWaiting();
                                            if (!data.IsValid) {
                                                jAlert(data.Mensaje, 'Error...');
                                            }
                                        });
                                    }
                                }
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
        e.data = $.extend(e.data, { _encId: _encId ? _encId : 0 });
    }
    function onDataBindingEncuesta(args) {
        debugger;
        var _SoloYo = $("#chbSoloYo").is(":checked");
        var _SoloPendientes = $("#chbTodos").is(":checked");
        // AbrirWaiting();
        args.data = $.extend(args.data, { SoloMisEncuestas: _SoloYo });
    }
    function onSave(e) {
        debugger;
        e.values = $.extend(e.values, { SoloMisEncuestas: $("#chbSoloYo").is(":checked"), afliw: "wilfa" });
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
    function onCompletePaciente(e) {       
        if (e.name != "insert") {
            var grid = $("#gridPacientes").data("tGrid");
            // AsignarPaciente(_pacId,"",1);
            // grid.addRow();
        }
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

    function onEditEncuestados() {
        var _Wenc = $("#WEncuestados").data("tWindow");
        
        _Wenc.title = "Personas Encuestadas de la Encuesta Nro " + _encId;
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

    function OnOpenEncuestados(e) {
        $(e.target).data("tWindow").title("Personas encuestadas de la encuesta Nro " + _encId);
    }

    function OnCloseEncuestados(e) {
        $("#Grid .t-refresh").click();
    }

    function wCrudTabPacientes_OnClose(e) {
        if ($("#tab4").data("loaded")) {
            $("#tab4").empty().data("loaded",false);
        }
        $("#gridEncuestados .t-refresh").click();
    }

    function GridEncuestados_OnDataBound(e) {
        var rowData = $(e.target).data("tGrid").data;
        var rowElement = $(e.target).data("tGrid").$rows();
        for (var i = 0; i < rowData.length;i++) {
            if (rowData[i].encRedesPuntaje) {
                if (rowData[i].encRedesPuntaje > 3 && rowData[i].encRedesPuntaje < 7) {
                    $(rowElement[i]).removeClass("weighingLow weighingHigh").addClass("weighingMid");
                } else if (rowData[i].encRedesPuntaje < 4) {
                    $(rowElement[i]).removeClass("weighingMid weighingHigh").addClass("weighingLow");
                } else {
                    $(rowElement[i]).removeClass("weighingLow weighingMid").addClass("weighingHigh");
                }
            }
        }
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

    function depEncIdOnChange(e) {
        $.post(window.GetPathApp("catPaciente/BindingLocalidades"), { depId: e.value }, function (locData) {
            var cbLocId = $("#locEncId").data("tComboBox");
            cbLocId.dataBind(locData);
            var locSelectedIndex = e.locId ? searchIndexOnComboBox(cbLocId, e.locId) : 0;
            cbLocId.select(locSelectedIndex);
            $.post(window.GetPathApp("catPaciente/BindingBarrios"), { depId: e.value }, function (barData) {
                var cbBarId = $("#barEncId").data("tComboBox");
                cbBarId.dataBind(barData);
                var barSelectedIndex = e.barId ? searchIndexOnComboBox(cbBarId,e.barId) : 0;
                cbBarId.select(barSelectedIndex);
            });
        });
    }


</script>



<!-- listado de Encuestas -->
<% 
    string _Style = "";
    _Style = "<span class='t-window-title'></span>";

    Html.Telerik().Grid<GeDoc.catEncuestasAPS>()
        .Name("Grid")

        .DataKeys(keys =>
        {
            keys.Add(p => p.encId);
        })

        .Localizable("es-AR")
        .HtmlAttributes(new { style = _Style })

        .ToolBar(commands =>
        {
            commands.Template(temp =>
                {
%>
<div class="t-button" onclick="onCommandEncuesta('Agregar');" style="display: <%= (Session["Permisos"] as Acciones).Visibilidad("catEncuestaAPS", "Agregar")%>">
    <span class="t-icon" style="background: url('<%= _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -49px -321px;"></span>
    Agregar
</div>
<div class="t-button" onclick="onCommandEncuesta('cmdEditarEncuesta');" style="display: <%= (Session["Permisos"] as Acciones).Visibilidad("catEncuestaAPS", "Agregar")%>">
    <span class="t-icon" style="background: url('<%= _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat 0px -336px;"></span>
    Modificar
</div>
<div class="t-button" onclick="onCommandEncuesta('cmdPersonas');" style="display: <%= (Session["Permisos"] as Acciones).Visibilidad("catEncuestaAPS", "Agregar")%>">
    <span class="t-icon" style="background: url('<%= _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -64px -208px;"></span>
    Agregar Personas Encuestadas
</div>
<span style="display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catEncuestaAPS", "Agregar")%>">
    <input id="chbSoloYo" type="checkbox" onclick="RefrescarEncuesta();" checked="checked" style="margin-left: 5px; vertical-align: middle;" />
    Mis Encuestas
</span>
<%
                });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catEncuestaAPS", new { SoloMisEncuestas = true })
                .Insert("_InsertEditing", "catEncuestaAPS", new { SoloMisEncuestas = true })
                .Update("_SaveEditing", "catEncuestaAPS", new { SoloMisEncuestas = true });
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.encId).Width("100px").Title("Nro.").Visible(true);
            columns.Bound(c => c.zonaNombre).Width("100px").Title("Zona").Visible(true);
            columns.Bound(c => c.csNombre).Width("150px").Title("Centro de Salud").Visible(true);
            columns.Bound(c => c.depNombre).Width("100px").Title("Departamento").Visible(true);
            columns.Bound(c => c.barNombre).Width("100px").Title("Barrio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
            columns.Bound(c => c.encDomCalle).Width("100px").Title("Calle").Visible(true);
            columns.Bound(c => c.encDomNro).Width("70px").Title("Numero").Visible(true);
            columns.Bound(c => c.encFechaRetira).Width("90px").Title("Encuestado").Visible(true);
            columns.Bound(c => c.encFechaCarga).Width("90px").Title("Actualizado").Visible(true);
            columns.Bound(c => c.usrNombre).Width("100px").Title("Usuario").Visible(true);
            columns.Bound(c => c.encuestadorNombre).Width("100px").Title("Encuestador").Visible(true);
            columns.Bound(c => c.cantidad).Width("70px").Title("Cantidad").Visible(true);
            columns.Bound(c => c.encRedesPuntaje).Width("150px").Title("Max. Nivel de Riesgo").Visible(true);
            columns.Bound(c => c.encTelFijo).Title("Tel. Fijo").Visible(false);
            columns.Bound(c => c.encTelCel).Title("Celular").Visible(false);
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
                .OnSave("onSave")
                .OnDataBound("GridEncuestados_OnDataBound")
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
        .ClientEvents(e => e.OnOpen("OnOpenEncuestados").OnClose("OnCloseEncuestados"))
        .Content(() =>
        {%>
          
<% Html.Telerik().Grid((IEnumerable<GeDoc.catEncuestasAPSPersonas>)ViewData["Encuestados"])
             .Name("gridEncuestados")
             .DataKeys(keys =>
             {
                 keys.Add(p => p.encPerId);
             })
             .ToolBar(commands =>
             {

                 commands.Custom().Ajax(true).Name("cmdAgregarPersonas").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Personas");

             })
         .DataBinding(dataBinding =>
         {
             dataBinding.Ajax()
                 .Select("_SelectEditingEncuestasAPSPersonas", "catEncuestaAPS")
                 .Delete("_DeleteEditingEncuestasAPSPersonas", "catEncuestaAPS");
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
               commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as Acciones).Visibilidad("catEncuestaAPS", "Agregar") });
           }).Width("80px").Title("Acciones");
             columns.Bound(c => c.ApellidoyNombre).Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
             columns.Bound(c => c.Documento).Title("Numero Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
             columns.Bound(c => c.encDolencias).Title("Dolencias").HtmlAttributes(new { style = "white-space: nowrap;" }).Visible(true).ClientTemplate("<#= encDolencias #>");
             columns.Bound(c => c.encRedesPuntaje).Width("100px").Title("Nivel de Riesgo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" }).ClientTemplate("<#= encRedesPuntaje #>");           
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
        .Scrollable(scroll => scroll.Enabled(true).Height(510))
        .Resizable(resizing => resizing.Columns(true))
        .Render();
        })
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1024)
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
       .Width(1024)
       .Height(480)
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


