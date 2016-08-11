<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<script>
    var _AplaId;
    var _espNombre;

    function onDataBindingPlanillaDetalleProf(args) {
        debugger;
        args.data = $.extend(args.data, { espId: _vespId, dia: _vdia, mes: _vmes, anio: _vaño });
    }
    function RefrescarPlanillaProfesionales() {
        debugger;
        var grid = $("#GDetalleProfesionales").data("tGrid");
        grid.ajaxRequest();
    }

    function OnEditPlanillaMesEspecialidad(e) {
        debugger;

        if (e.name === "databinding") {
            $("#GPlanillaMesEspecialidad").data("tGrid").rebind();
        }
    }
    function OnOpenPlanillaProf(e) {
        debugger;
        $(e.target).data("tWindow").title(_centroSaludTexto + " - " + _departamentoTexto + " - " + _vdia +" / " + _vmes + " / " + _vaño);
    }


    function onCommandEditPlanillaEspecialidad(e) {
        debugger;
        switch (e.name) {
            case "insert":
                debugger;
                e.data = $.extend(e.data, { plaId: _AplaId });
                $("#plaId").val(_AplaId);
                break;
            case "cmdEspecialidades":
                debugger;
                onRowSelected_ProEstadisticaPlanilla(e);
                var _WPlanillaMesEspecialidad = $('#WPlanillaMesEspecialidad').data("tWindow");
                _WPlanillaMesEspecialidad.open();
                RefrescarPlanillaMesEspecialidad();
                break;
        }
    }
    function onRowSelected_PlanillaMesEspecialidad(e) {
        debugger;
        var row = e.row;
        var grid = $("#GPlanillaMesEspecialidad").data("tGrid");
        var dataItem = grid.dataItem(row);
        _DatosRegistroPlanilla = dataItem;
        _plaId = dataItem.plaId;
        _espId = dataItem.espId;
        _espNombre = dataItem.Especialidades.espNombre;
    }

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    function onDataBound_PlanillaMesEspecialidad() {
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
    function onCommandPlanillaMesEspecialidad(e) {
        debugger;
        switch (e.name) {
            case "insert":
                debugger;
                e.data = $.extend(e.data, { plaId: _AplaId });
                $("#plaId").val(_AplaId);
                break;
            case "cmdValores":
                console.log(e);
                onRowSelected_PlanillaMesEspecialidad(e);
                var _Wvalores = $('#Wvalores').data("tWindow");
                _Wvalores.open();
                RefrescarValores();

                break;
            case "cmdAgregarValores":
                onRowSelected_PlanillaMesEspecialidad(e);
                if (_plaId > 0) {
                    $.post("<%=Url.Content("~/proEstadisticaPlanilla/generaValores")%>", { plaId: _plaId, espId: _espId }, function (data) {
                        CerrarWaiting();
                        if (data != null) {

                            RefrescarValores();
                            jAlert("Se crearon los valores.", '¡Atención!', function () {
                            });

                        }
                    });
                }
                break;
            case "cmdValidarValores":
                onRowSelected_PlanillaMesEspecialidad(e);
                if (_plaId > 0) {
                    $.post("<%=Url.Content("~/proEstadisticaPlanilla/validaValores")%>", { plaId: _plaId, espId: _espId }, function (data) {
                        CerrarWaiting();
                        RefrescarPlanillaMesEspecialidad();
                        if (data != null) {

                            RefrescarPlanillaMesEspecialidad();
                            jAlert("Planilla Validada correctamente.", '¡Atención!', function () {
                            });
                        }
                    });
                }
                break;
            case "cmdExportarValores":
                debugger;
                onRowSelected_PlanillaMesEspecialidad(e);
                if (_plaId > 0) {
                    $("body").append($("<iframe>", {
                        src: "<%=Url.Content("~/proEstadisticaPlanilla/Exportar")%>?plaId=" + _plaId + "&espId=" + _espId,
                        style: 'display:none'
                    }));
                     };
                     break;
             }
         }
</script>

<!-- Detalles Prof -->
<% Html.Telerik().Window()
        .Name("WDetalleProfesionales")
        .Title("Profesionales")
        .Modal(true)
        .ClientEvents(e => e.OnOpen("OnOpenPlanillaProf"))
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <%
            Html.Telerik().Grid<GeDoc.proEstadisticaProfesionales>()
                        .Name("GDetalleProfesionales")
                        .DataKeys(keys =>
                        {
                            keys.Add(p => p.perId);
                        })
                        .Localizable("es-AR")
                        .ToolBar(commands =>
                        {
                            
                        })
                        .DataBinding(dataBinding =>
                        {
                            dataBinding.Ajax()
                                .Select("_SelectEditinglistadoProfesionalesxTurno", "proEstadisticaPlanilla", new { espId = 0, dia = 0, mes = 0, anio = 0 });


                        })
                        .Columns(columns =>
                        {
                           
                            columns.Bound(c => c.perApellidoyNombre).Width(10).Title("Profesional").Visible(true);
                            columns.Bound(c => c.espNombre).Width(10).Title("Especialidad").Visible(true);
                            columns.Bound(c => c.ControlEmbarazo).Width(10).Title("Control De Emb.").Visible(true);
                           
                        })
                        .ClientEvents(events => events.OnEdit("onCommandEditPlanillaEspecialidad").OnDataBinding("onDataBindingPlanillaDetalleProf"))
                        .Selectable()
                        .Scrollable(scroll => scroll.Enabled(true)
                        .Height(90))
                        .Sortable()
                        
                        .Render();
                %>
</div>
        <% })
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(true)
       .Resizable()
        .Width(580)
       .Height(180)
       .Render();
       
%>