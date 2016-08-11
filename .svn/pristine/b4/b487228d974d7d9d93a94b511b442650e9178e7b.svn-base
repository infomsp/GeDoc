<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    var idTurno;
    var _barId;
    var _csId;
    function onClickAsignarBarrio(e, pId, pNombre, pApellido) {
       // debugger;
        var grid = $(this).data("tGrid");
        _Checkeado = e.checked;
        _IdPersona = pId;
        //jConfirm('¿Confirma ' + (e.checked ? 'asignar a este ' : 'quitar de este ') + 'Centro de Salud a "' + pApellido + " " + pNombre + '"?', (e.checked ? 'Asignar...' : 'Quitar...'), function (r) {
            debugger;
           // if (r) {
                debugger;
                AbrirWaiting();
                if (e.checked) {
                    $.post(GetPathApp('catCentrosDeSalud/_InsertEditingenlCentrodeSaludBarrio'), { _barId: _IdPersona, _csId: _csId }, function (data) {
                        if (!data.IsValid) {
                            $('input[name=cbxClasificado_' + _IdPersona.toString() + ']').attr('checked', data.Clasificacion);
                        }
                        $('#lblCS_' + _IdPersona.toString()).text(data.CS);
                        CerrarWaiting();
                    });
                }
                else {
                    $.post(GetPathApp('catCentrosDeSalud/_DeleteenlCentrodeSaludBarrio'), { _barId: _IdPersona, _csId: _csId }, function (data) {
                        if (!data.IsValid) {
                            $('input[name=cbxClasificado_' + _IdPersona.toString() + ']').attr('checked', data.Clasificacion);
                        }
                        $('#lblCS_' + _IdPersona.toString()).text(data.CS);
                        CerrarWaiting();
                    });

                }
          //  }
           // else {
            //    $('input[name=cbxClasificado_' + _IdPersona.toString() + ']').attr('checked', !e.checked);
           // }
       // }//);
    }
    function onCommand_CentrosSalud(e) {
        debugger;
        if (e.name == "cmdAlcanceBarrios") {
            var grid = $("#gridbarriosporDepto").data("tGrid");
            var wBarrioDpto = $("#WbarriosporDepto").data("tWindow");
            grid.rebind();
            wBarrioDpto.center().open();
        }
    }
    function onCommandBarriosporCentro(e) {
        debugger;
        var grid = $("#gridbarriosporDepto").data("tGrid");
        var wBarrioDpto = $("#WbarriosporDepto").data("tWindow");
        if (e.name == "cmdAgregarBarrios") {
            grid.rebind();
            wBarrioDpto.center().open();
        }
      
    }
    function onCommandTiposDiagnosticos(e) {
        debugger;
        var grid = $("#gridbarriosporDepto").data("tGrid");
        var wBarrioDpto = $("#WbarriosporDepto").data("tWindow");
        var _diag = null;
        if (e.name == "cmdSeleccionarBarrio") {
            if (_diagId != null) {
                AbrirWaiting();
                debugger;
                $.post('/GeDoc/enlCentrodeSaludBarrio/AsignarenlCentrodeSaludBarrio', { barId: _barId, csId: _csId }, function (data) {
                    if (data != null) {
                        debugger;
                        CerrarWaiting();
                        wBarrioDpto.close();
                        //RefrescarGridDiagnostico();
                    }
                });
            }
        }
    }
    function onDataBindingTiposDiagnosticos() {
        var grid = $("#gridTiposDiagnosticos").data("tGrid");
    }
    function onRowSelectedTiposDiagnosticos(e) {
        debugger;
        var row = e.row;
        var grid = $("#gridtiposDiagnosticos").data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        _diagId = _DatosRegistro['diagId'];
    }
    function onDataBindingTurnosDiagnosticos(args) {
        debugger;
        _turId = idTurno;
        args.data = $.extend(args.data, { turId: _turId });

    }
    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    function onDataBoundTurnoDiagnostico() {
        var grid = $(this).data('tGrid');
        _CurrentPage = grid.currentPage;
        _OrderBy = (grid.orderBy || '~');
        _FilterBy = (grid.filterBy || '~');
        var _Boton = $('a.t-button.t-grid-cmdExportar');
        var href = _Boton.attr('href');
        _Boton.attr('href', href);
    }
    
    function onRowSelectedDiagnostico(e) {
        debugger;
        var row = e.row;
        var grid = $("#gridDiagnostico").data("tGrid");
        var dataItem = grid.dataItem(row);
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        _diagId = _DatosRegistro['diagId'];
        _tdId = DatosRegistro['tdId'];
    }
    function onRowSelectCentroSalud(e) {
        debugger;
        var row = e.row;
        var grid = $("#Grid").data("tGrid");
        var dataItem = grid.dataItem(row);
        var _DatosRegistro;
        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
        _csId = _DatosRegistro['sucId'];
        _sucCod = _DatosRegistro['sucCodigo'];
    }
    function onDataBindingCentroSalud(e) {
        e.data = $.extend(e.data, { _csId: _csId });
    }
</script>
<% ViewData["AltoEditor"] = "400px"; %>
<%= Html.Telerik().Grid<GeDoc.catCentrosDeSalud>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.sucId);
        })
        
        .Localizable("es-AR")
              .ToolBar(commands =>
              {
                  commands.Custom().Ajax(true).Name("cmdAlcanceBarrios").ButtonType(GridButtonType.ImageAndText)
                  .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                      .Text("Alcance por Barrios");
                  //.HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Agregar") });                
              })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
            .Select("_SelectEditing", "catCentrosDeSalud");
        })
        .Columns(columns =>
        {
            
            columns.Bound(c => c.sucCodigo).Width(20).Title("Código").Visible(true);
            columns.Bound(c => c.sucNombre).Width(60).Title("Nombre").Visible(true);
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                   .ClientEvents(events => events.OnCommand("onCommand_CentrosSalud").OnRowSelect("onRowSelectCentroSalud"))                 
                                
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
%>


 <%--<%
     Html.Telerik().Window()
        .Name("WBarriosporCentro")
        .Title("Barrios por Centro de Salud")
        .Visible(false)
        .Content(() =>
        {
            %>            
      <div>    
            <%
                            Html.Telerik().Grid((IEnumerable<GeDoc.enlCentrosdeSaludBarrios>)ViewData["enlCentrosdeSaludBarrios"])
                                    .Name("gridBarriosporCentro")
                                    
                                    .DataKeys(keys =>
                                    {
                                        keys.Add(p => p.barId);
                                    })
                                     .ToolBar(commands =>
                                    {
                                        commands.Custom().Ajax(true).Name("cmdAgregarBarrios").ButtonType(GridButtonType.ImageAndText)
                                       .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                                       .Text("Agregar");                                       
                                    })
                                     .DataBinding(dataBinding =>
                                    {
                                        dataBinding.Ajax()
                                            .Select("_SelectEditingTurnosDiagnosticos", "catTurno")
                                            .Insert("_InsertEditingTurnosDiagnosticos", "catTurno")
                                            .Delete("_DeleteTurnosDiagnosticos", "catTurno");                                      
                                     })
                                      .Localizable("es-AR")

             .Columns(columns =>
             {            
              columns.Bound(c => c.barrioNombre).Width("180px").Title("Barrio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= barrioNombre #>' style='cursor: pointer;' id='txtbarrioNombre' ><#= barrioNombre #></label>");
        })   
        
     .Pageable((paging) =>
            paging.Enabled(true)
            .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_SelectEditingTurnosDiagnosticos", "catTurno", new { turId = 0 }))
            .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandBarriosporCentro").OnRowSelect("onRowSelectedDiagnostico").OnDataBinding("onDataBindingTurnosDiagnosticos").OnDataBound("onDataBoundTurnoDiagnostico"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))                
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
       %>--%>


<!-- Listado de Barrios -->
<%--<% Html.Telerik().Window()
        .Name("WbarriosporDepto")
        .Title("Barrios")
        .Visible(false)
        .Content(() =>
        {
      %>
             <div>
                <div class="BordeRedondeado" style="margin-bottom: 8px;">
                    <div style="margin: 5px;">
                        <label>Centro de Salud:</label>
                        <input id="txtUbicacionActual" disabled="disabled" style="width: 892px; font-size: 20px; color: darkslategray; vertical-align: middle;" />
                    </div>
                </div>
    <%                              Html.Telerik().Grid((IEnumerable<GeDoc.catBarrios>)ViewData["barriosporDepto"])
                                    .Name("gridbarriosporDepto")                                  
                                    .DataKeys(keys =>
                                    {
                                        keys.Add(p => p.barId);
                                    })
                                    // .ToolBar(commands =>
                                    //{
                                        
                                    //    commands.Custom().Ajax(true).Name("cmdSeleccionarBarrio").ButtonType(GridButtonType.ImageAndText)
                                    //   .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                                    //   .Text("Seleccionar");
                                    //})
                                     .DataBinding(dataBinding =>
                                    {
                                        dataBinding.Ajax()
                                       .Select("_SelectEditingBarrios", "catBarrio");                     
                                    })
                                      .Localizable("es-AR")
        .Columns(columns =>
        {
            
              columns.Bound(c => c.barId).Width("50px").Title("").Visible(true)
           .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' name='cbxClasificado_<#= barId #>' id='cbxClasificado_<#= barId #>' <#= csId_asignado ? checked = 'checked' : '' #>   onclick='onClickAsignarBarrio(this, <#= barId #>, \"<#= barNombre #>\", \"<#= csId_asignado #>\");' /></div>");

              columns.Bound(c => c.AsignadoaCentroSalud).Width("250px").Title("Asignado a").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
               .ClientTemplate("<label title='<#= AsignadoaCentroSalud #>' style='cursor: pointer;' id='txtAsignadoaCentroSalud' ><#= AsignadoaCentroSalud #></label>");
            columns.Bound(c => c.barNombre).Width("250px").Title("Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= barNombre #>' style='cursor: pointer;' id='txtbarNombre' ><#= barNombre #></label>");
            columns.Bound(c => c.depId).Width("150px").Title("Departamento").Visible(false).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= depId #>' style='cursor: pointer;' id='txtdepId' ><#= depId #></label>");
            columns.Bound(c => c.depNombre).Width("150px").Title("Departamento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= depNombre #>' style='cursor: pointer;' id='txtdepNombre' ><#= depNombre #></label>");
            columns.Bound(c => c.locCod ).Width("130px").Title("Codigo de Localidad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= locCod #>' style='cursor: pointer;' id='txtlocCod' ><#= locCod #></label>");
           
        })      
     .Pageable((paging) =>
            paging.Enabled(true)
            .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
             .DataBinding(dataBinding => dataBinding.Ajax().Select("_SelectEditingBarrios", "catBarrio", new { _csId = 4}))
            .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandTiposDiagnosticos").OnRowSelect("onRowSelectedTiposDiagnosticos").OnDataBinding("onDataBindingCentroSalud"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))     
             .HtmlAttributes(new { style = "height: 99.8%;" })  
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
       .Height(600)
       .Render();
       %>--%>

<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>
