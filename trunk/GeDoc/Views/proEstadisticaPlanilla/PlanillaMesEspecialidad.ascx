<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<%--<script runat="server">
    private object _plaId;
</script>--%>

<script>
    var _AplaId;
    var _espId;
    var _espNombre;
   
    function  getMes(_mes)
    {
             switch (_mes)
            {
                case 1:
                    return ("Enero");
                    break;
                case 2:
                    return ("Febrero");
                    break;
                case 3:
                    return ("Marzo");
                    break;
                case 4:
                    return ("Abril");
                    break;
                case 5:
                    return ("Mayo");
                    break;
                case 6:
                    return ("Junio");
                    break;
                case 7:
                    return ("Julio");
                    break;
                case 8:
                    return ("Agosto");
                    break;
                case 9:
                    return ("Septiembre");
                    break;
                case 10:
                    return ("Octubre");
                    break;
                case 11:
                    return ("Noviembre");
                    break;
                case 12:
                    return ("Diciembre");
                    break;
             }

          return "";
    }

    function onDataBindingPlanillaMesEspecialidad(args) {
        debugger;
       
        if (_plaId > 0) {
            debugger;
            _AplaId = _plaId;}
        args.data = $.extend(args.data, { plaId: _AplaId });
    }

    function onSavePlanillaEspecialidad(args) {

        //if (_plaId > 0) {
        //    debugger;
        //    _AplaId = _plaId;
        //}
        //args.data = $.extend(args.data, { plaId: _AplaId });
    }
    function RefrescarPlanillaMesEspecialidad() {
        debugger;
        var grid = $("#GPlanillaMesEspecialidad").data("tGrid");
        grid.ajaxRequest();
    }

    function OnEditPlanillaMesEspecialidad(e) {
        debugger;
       
        if (e.name === "databinding") {
            $("#GPlanillaMesEspecialidad").data("tGrid").rebind();
        }
    }
    function OnOpenPlanilla(e) {

        $(e.target).data("tWindow").title(_centroSaludTexto + " - " + _departamentoTexto + " - " + getMes(_mes) + " de " + _año);
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
        _planillaValidada = dataItem.planillaValidada;
        _planillaGenerada = dataItem.planillaGenerada;
    }

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    function onDataBound_PlanillaMesEspecialidad() {
        var grid = $(this).data('GPlanillaMesEspecialidad');
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

        case "cmdAgregarEspecialidad":
            debugger;
            var grid = $('#GPlanillaMesEspecialidad').data('tGrid');
            grid.addRow();

            break;
        case "insert":
            var _espe = $("#espId").data('tComboBox').value();
            $.post("<%=Url.Content("~/proEstadisticaPlanilla/_InsertEditing_enlEstadisticaPlanillaEspecialidad")%>", { plaId: _AplaId, espId: _espe }, function(data) {
                if (data != null) {
                    RefrescarPlanillaMesEspecialidad();
                    jAlert("Se agrego especialidad.", '¡Atención!', function() {
                    });
                }
            });

            break;
        case "cmdValores":
            console.log(e);
            onRowSelected_PlanillaMesEspecialidad(e);
            if (_planillaGenerada == 1) {
                if (_planillaValidada == 1) {
                    jAlert("No se puede editar. Planilla Validada!.", '¡Atención!', function() {
                    });
                }
                if (_planillaValidada != 1) {

                    var _Wvalores = $('#Wvalores').data("tWindow");
                    _Wvalores.open();
                    _Wvalores.refresh();
                }
              
                RefrescarValores();
            } else {
                    
                jAlert("No se puede editar. Planilla No Generada!. Generar Valores", '¡Atención!', function () {
                });
            }
            break;
            case "cmdAgregarValores":
                onRowSelected_PlanillaMesEspecialidad(e);
                if (_planillaValidada == 1) {
                    jAlert("No se puede agregar valores. Planilla Validada!.", '¡Atención!', function () {
                    });
                    break;
                }
                if (_plaId > 0) {
                    AbrirWaiting();
                    $.post("<%=Url.Content("~/proEstadisticaPlanilla/generaValores")%>", { plaId: _plaId, espId: _espId }, function (data) {
                        CerrarWaiting();
                        debugger;
                        if (data != null) {
                            debugger;
                            RefrescarValores();
                            if (data.data[0].plaId == null) {
                                jAlert("Planilla creada anteriormente, para modificar Edite Valores.", '¡Atención!', function () {
                                });
                                
                            }
                            if (data.data[0].plaId != null) {
                                jAlert("Se crearon los valores, para modificar Edite Valores.", '¡Atención!', function() {
                                });
                                RefrescarPlanillaMesEspecialidad();
                            }
                        };
                       
                    });
                }
                break;
            case "cmdValidarValores":
                onRowSelected_PlanillaMesEspecialidad(e);
                debugger;
                if (_planillaGenerada == 1) {
                    if (_planillaValidada == 1) {
                        jAlert("La Planilla se encuentra validada!.", '¡Atención!', function() {
                        });
                        break;
                    }
                    if (_plaId > 0) {
                        $.post("<%=Url.Content("~/proEstadisticaPlanilla/validaValores")%>", { plaId: _plaId, espId: _espId }, function(data) {
                            CerrarWaiting();
                            RefrescarPlanillaMesEspecialidad();
                            if (data != null) {
                                RefrescarPlanillaMesEspecialidad();
                                jAlert("Planilla Validada correctamente.", '¡Atención!', function() {
                                });
                            }
                        });
                    }
                } else {
                    jAlert("No se puede Validar. Planilla No Generada!. Generar Valores", '¡Atención!', function () {
                    });
                }
                break;
            case "cmdExportarValores":
                onRowSelected_PlanillaMesEspecialidad(e);
                if (_planillaGenerada == 1) {
                    if (_planillaValidada != 1) {
                        jAlert("La Planilla NO se encuentra validada, Validar antes de exportar.", '¡Atención!', function() {
                        });
                        break;
                    }
                    if (_plaId > 0) {
                        debugger;
                        $("body").append($("<iframe>", {
                            src: "<%=Url.Content("~/proEstadisticaPlanilla/Exportar")%>?plaId=" + _plaId + "&espId=" + _espId,
                            style: 'display:none'
                        }));
                   
                    }
                } else {
  
                        jAlert("No se puede Exportar. Planilla No Generada!. Generar Valores", '¡Atención!', function() {
                        });
                }

                break;
        }
    }
</script>
<!-- Mes Esp -->
<% Html.Telerik().Window()
        .Name("WPlanillaMesEspecialidad")
        .Title("Especialidades")
        .Modal(true)
         .ClientEvents(e => e.OnOpen("OnOpenPlanilla"))
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
               
                <%
                    Html.Telerik().Grid<GeDoc.enlEstadisticaPlanillaEspecialidades>()
                        .Name("GPlanillaMesEspecialidad")
                        .DataKeys(keys =>
                        {
                            keys.Add(p => p.peId);
                        })
                        .Localizable("es-AR")
                        .ToolBar(commands =>
                        {
                           

                            commands.Custom().Ajax(true).Name("cmdAgregarEspecialidad").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar");

                        })
                        .DataBinding(dataBinding =>
                        {
                            dataBinding.Ajax()
                                .Select("_SelectEditing_enlEstadisticaPlanillaEspecialidad", "proEstadisticaPlanilla", new {plaId = 0})
                               // .Insert("_InsertEditing_enlEstadisticaPlanillaEspecialidad", "proEstadisticaPlanilla", new {plaId = 0})
                                .Update("_SaveEditing_enlEstadisticaPlanillaEspecialidad", "proEstadisticaPlanilla")
                                .Delete("_Delete_enlEstadisticaPlanillaEspecialidad", "proEstadisticaPlanilla");
                        })
                        .Columns(columns =>
                        {
                            columns.Command(commands =>
                            {
                                //commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new {style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catZona", "Modificar")});
                               
                                commands.Custom("cmdAgregarValores")
                                    .Ajax(true)
                                    .ButtonType(GridButtonType.Text)
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                                    .Text("Generar Valores");
                                commands.Custom("cmdValores")
                                       .Ajax(true)
                                       .ButtonType(GridButtonType.Text)
                                       .Text("Editar Valores")
                                       .SendDataKeys(false)
                                       .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -320px;", title = "Valores" })
                                       .HtmlAttributes(new { style = "disabled: disabled" })
                                       .SendState(false)
                                       
                                       .DataRouteValues(route =>
                                       {
                                           route.Add(o => o.plaId).RouteKey("plaId");
                                       });
                                commands.Custom("cmdValidarValores")
                                    .Ajax(true)
                                    .ButtonType(GridButtonType.Text)
                                     .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                                     // .HtmlAttributes(new { style = "display: none " })
                                     .Text("Validar");
                                commands.Custom("cmdExportarValores")
                                    .Ajax(true)
                                    .ButtonType(GridButtonType.Text)
                                    .ImageHtmlAttributes(new {style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;"})
                                    .Text("Exportar");
                                   // .HtmlAttributes(new {style = "display: none "});
                                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proEstadisticaPlanilla", "Eliminar") });
                               
                            }).Width(8).Title("Acciones");
                            columns.Bound(c => c.Especialidades.espNombre).Width(10).Title("Especialidad").Visible(true);
                            columns.Bound(c => c.planillaValidada).Width(10).Title("Validada").Visible(false);
                            columns.Bound(c => c.planillaGenerada).Width(10).Title("Validada").Visible(false);
                            columns.Bound(c => c.planillaValidadaImagen).Width("8px").Title("Validada").Visible(true)
          .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/<#= planillaValidadaImagen #>' title='<#= planillaValidada #>' height='22px' width='22px' style='vertical-align:middle;' /><#= planillaValidada #></div>");        
                            columns.Bound(c => c.Especialidades.espCodigo).Width(10).Title("Esp. Codigo").Visible(false);
                            columns.Bound(c => c.Especialidades.espNombrePadre).Width(10).Title("Esp. Padre").Visible(false);
                            columns.Bound(c => c.espId).Width(10).Title("espId").Visible(false);
                        })
        .Editable(editing => editing
                            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                        .Pageable((paging) =>
                            paging.Enabled(true)
                                .PageSize(((int) Session["FilasPorPagina"])))
                        .ClientEvents(events => events.OnSave("onSavePlanillaEspecialidad").OnEdit("onCommandEditPlanillaEspecialidad").OnDataBinding("onDataBindingPlanillaMesEspecialidad").OnCommand("onCommandPlanillaMesEspecialidad"))
                        .Footer(true)
                        .Filterable()
                        .Selectable()
                        .Scrollable(scroll => scroll.Enabled(true).Height(((int) Session["AlturaGrilla"])))
                        .Sortable()
                        .Render();
                %>
               
</div>
        <% })
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(true)
       .Resizable()
       .Height(550)
       .Render();
       
%>

