<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src=""<%= Url.Content("~/Scripts/CRUDGrillas.js") %>""></script>
<% Html.RenderPartial("Waiting"); %>
<% Html.RenderPartial("CRUDPaciente"); %>
<% Html.RenderPartial("ConsultaPaciente"); %>
<div style="overflow: hidden; height: 510px;" >
<script type="text/javascript">
    var _RowIndex = -1;
    var _DatosRegistro;
    var _RowIndexHorario = -1;
    var _DatosRegistroHorario;
    var _EsModificar = false;
    var _pacId = -1;
    function onRowSelected(e) {
        debugger;
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        _pacId = _DatosRegistro['pacId'];
    }
    function onCommand(e) {
        _EsModificar = false;
        if (("cmdEditar").indexOf(e.name) >= 0) {
            if (_RowIndex < 0) {
                jAlert('Debe seleccionar un Paciente.', 'Error...');
                return;
            }
        }
        switch (e.name) {
            case "cmdAgregar":
                var _CRUD = $("#CRUDPaciente").data("tWindow");
                _CRUD.open();
                _WindowCRUD = _CRUD;
                break;
            case "cmdEditar":
                debugger;
                var grid = $("#Grid").data("tGrid");
                // grid.data("tComboBox").fill();
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                grid.editRow(tr);
                debugger;
                jAlert('prueba', 'Error');
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
        }
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
            //  _Foto = GetPathApp('Content/Archivos/FotosPersonal/' + _Paciente.perFoto);
        }

        _pacId = _Paciente.pacId;

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
    //    $('#tipoIdTipoDocumento')
    //    .bind('valueChange', function (e) {
    //        debugger;
    //        // Obtenerdatos_CONPAD();
    //        alert("cambio");
    //    });
    function onCommandEditPaciente(e) {
        debugger;
        var dataItem = e.dataItem;
        var mode = e.mode;
        var form = e.form;
        onCommandEdit(e);
    }
</script>
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
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Examinar") })
                .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catPaciente")
                .Insert("_InsertEditing", "catPaciente")
                .Update("_SaveEditing", "catPaciente")
                .Delete("_DeleteEditing", "catPaciente");
        })
        .Localizable("es-AR")
        .Columns(columns =>
        {
           columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Eliminar") });
            }).Width("80px").Title("Acciones");             
            columns.Bound(c => c.pacApellido).Width("250px").Title("Apellido").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pacApellido #>' style='cursor: pointer;' id='txtpacApellido3' ><#= pacApellido #></label>");
            
            columns.Bound(c => c.pacNombre).Width("250px").Title("Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pacNombre #>' style='cursor: pointer;' id='txtPacientepacNombre2' ><#= pacNombre #></label>");
                               
             columns.Bound(c => c.pacNumeroDocumento).Width("250px").Title("Numero Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacNumeroDocumento #>' style='cursor: pointer;' id='txtpacNumeroDocumento2' ><#= pacNumeroDocumento #></label>");

             columns.Bound(c => c.tipoDescDocumento).Width("250px").Title("Tipo Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= tipoDescDocumento #>' style='cursor: pointer;' id='txttipoDescDocumento' ><#= tipoDescDocumento #></label>");
                 
             columns.Bound(c => c.pacCUIL).Width("250px").Title("C.U.I.L.").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })                 
             .ClientTemplate("<label title='<#= pacCUIL #>' style='cursor: pointer;' id='txtpacCUIL2' ><#= pacCUIL #></label>");

             columns.Bound(c => c.paisNombre).Width("250px").Title("Pais").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= paisNombre #>' style='cursor: pointer;' id='paisNombre' ><#= paisNombre #></label>");

             columns.Bound(c => c.tipoSexoNombre).Width("250px").Title("Sexo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= tipoSexoNombre #>' style='cursor: pointer;' id='tipoSexoNombre' ><#= tipoSexoNombre #></label>");              
             // columns.Bound(c => c.pacFechaNacimiento).Width("250px").Title("Fecha de Nacimiento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })                 
              //.ClientTemplate("<label title='<#= pacFechaNacimiento #>' style='cursor: pointer;' id='pacFechaNacimiento' ><#= pacFechaNacimiento #></label>");

              columns.Bound(c => c.DescEstadoCivil).Width("250px").Title("Estado Civil").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= DescEstadoCivil #>' style='cursor: pointer;' id='DescEstadoCivil' ><#= DescEstadoCivil #></label>");
                       
           
            /* columns.Bound(c => c.pacCalle).Width("250px").Title("Calle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacCalle #>' style='cursor: pointer;' id='pacCalle' ><#= pacCalle #></label>");  
            
             columns.Bound(c => c.pacCalleNumero).Width("250px").Title("Calle Numero").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacCalleNumero #>' style='cursor: pointer;' id='pacCalleNumero' ><#= pacCalleNumero #></label>");  
            
             columns.Bound(c => c.pacDomicilioReferencias).Width("250px").Title("Domicilio Ref.").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacDomicilioReferencias #>' style='cursor: pointer;' id='pacDomicilioReferencias' ><#= pacDomicilioReferencias #></label>");

             columns.Bound(c => c.depNombre).Width("250px").Title("Departamento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= depNombre #>' style='cursor: pointer;' id='depNombre' ><#= depNombre #></label>");  
            
             columns.Bound(c => c.depNombre).Width("250px").Title("Localidad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= locNombre #>' style='cursor: pointer;' id='locNombre' ><#= locNombre #></label>");  
            
             columns.Bound(c => c.barId).Width("250px").Title("Barrio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= barNombre #>' style='cursor: pointer;' id='barNombre' ><#= barNombre #></label>");  
            
            // columns.Bound(c => c.pacTelefonoCasa).Width("250px").Title("Telefono Casa").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             //.ClientTemplate("<label title='<#= pacTelefonoCasa #>' style='cursor: pointer;' id='pacTelefonoCasa' ><#= pacTelefonoCasa #></label>");  
            
             //columns.Bound(c => c.pacTelefonoTrabajo).Width("250px").Title("Telefono Trabajo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             //.ClientTemplate("<label title='<#= pacTelefonoTrabajo #>' style='cursor: pointer;' id='pacTelefonoTrabajo' ><#= pacTelefonoTrabajo #></label>");  
            
             //columns.Bound(c => c.pacTelefonoCelular).Width("250px").Title("Telefono Celular").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
           //  .ClientTemplate("<label title='<#= pacTelefonoCelular #>' style='cursor: pointer;' id='pacTelefonoCelular' ><#= pacTelefonoCelular #></label>");      
                
           
             //columns.Bound(c => c.osNombre).Width("250px").Title("Obra Social").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             //.ClientTemplate("<label title='<#= osNombre #>' style='cursor: pointer;' id='osNombre' ><#= osNombre #></label>");            
           //  columns.Bound(c => c.pacTransfusionesDeSangreUltima).Width("250px").Title("Ultima transfusiones de sangre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
           // .ClientTemplate("<label title='<#= pacTransfusionesDeSangreUltima #>' style='cursor: pointer;' id='pacTransfusionesDeSangreUltima' ><#= pacTransfusionesDeSangreUltima #></label>");  */

               })
                .DataBinding(dataBinding => dataBinding.Ajax().Update("_SaveEditing", "catPaciente", new { id = 1 }))
                .Editable(editing => editing.Enabled(true)
                .Mode(GridEditMode.PopUp)
                .Enabled(true)
                .DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEditPaciente").OnRowSelected("onRowSelected").OnCommand("onCommand").OnRowDataBound("onRowDataBound").OnSave("onSave").OnDataBinding("onDataBindingPacientes"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()               
           
%>
</div>


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

