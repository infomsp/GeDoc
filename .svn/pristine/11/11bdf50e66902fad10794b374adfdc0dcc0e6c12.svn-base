<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var idCategoria;
    var _Presupuestados;
    var idPersona;
    function onCommand(e) {
        if (e.name == "cmdPresupuestados") {
            var estGrid = $('#gridPresupuestados').data('tGrid');
            var _WindowEst = $("#CRUDPresupuestados").data("tWindow");

            idCategoria = e.dataItem['categId'];

            $("#lblCargo").text(e.dataItem['carDescripcion'] + ', Categoría ' + e.dataItem['categNumero'].toString() + ', Antigüedad entre ' + e.dataItem['categAntiguedadDesde'].toString() + ' y ' + e.dataItem['categAntiguedadHasta'].toString() + ' Años');

            estGrid.rebind();
            _WindowEst.center().open();
        } else if (e.name == "cmdDesignados") {
            var estGrid = $('#gridDesignados').data('tGrid');
            var _WindowDes = $("#CRUDDesignados").data("tWindow");

            idCategoria = e.dataItem['categId'];
            _Presupuestados = e.dataItem['carPresupuestado'];

            //if (_Presupuestados == 0) {
            //    MostrarError('No puede gestionar las designaciones, por que este cargo no tiene cupo presupuestado.');
            //    return;
            //}

            $("#lblCargo2").text(e.dataItem['carDescripcion'] + ', Categoría ' + e.dataItem['categNumero'].toString() + ', Antigüedad entre ' + e.dataItem['categAntiguedadDesde'].toString() + ' y ' + e.dataItem['categAntiguedadHasta'].toString() + ' Años');

            estGrid.rebind();
            //if (_WindowDes.isMaximized) {
                _WindowDes.center().open();
            //}
            //else {
            //    _WindowDes.center().maximize().open();
            //}
        } else if (e.name == "cmdEstadisticas") {
            var _WindowEstadisticas = $("#wEstadisticas").data("tWindow");

            if (_WindowEstadisticas.isMaximized) {
                _WindowEstadisticas.center().open();
            }
            else {
                _WindowEstadisticas.center().maximize().open();
            }
        }
    }
    function onCommandDesignados(e) {
        if (e.name == "add") {
            var estGrid = $('#gridDesignados').data('tGrid');

            if (_Presupuestados == estGrid.total) {
                MostrarError('No puede agregar más designaciones, por que este cargo no tiene lugares disponibles.');
                var _Err = $('#XXXXX').data("tGrid");

                _Err.rebind();

                return;
            }
        }
    }

    function onEdit(e) {
        var _Categoria = $('#categId-input')[0];
        _Categoria.disabled = true;

        onCommandEdit(e);
    }

    function onDataBindingEstados(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onDataBindingPresupuestados(e) {
        e.data = $.extend(e.data, { idCategoria: idCategoria });
    }
    function onSavePresupuestados(e) {
        var values = e.values;

        values.categId = idCategoria;
    }
    function onCompletePresupuestados(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            var pagGrid = $('#Grid').data('tGrid');
            pagGrid.rebind();
        }
    }
    function onDataBindingDesignados(e) {
        e.data = $.extend(e.data, { idCategoria: idCategoria });
    }
    function onSaveDesignados(e) {
        var values = e.values;
        values.categId = idCategoria;
    }
    function onCompleteDesignados(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            var pagGrid = $('#Grid').data('tGrid');
            pagGrid.rebind();
        }
        if (e.name == "Consultar") {
            var detailWindow = $("#wConsulta").data("tWindow");
            var _Persona = e.response.InfoPersona;
            var _Foto = "";

            if (_Persona.perFoto == null) {
                _Foto = GetPathApp('Content/General/ImagenNoDisponible.jpg');
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
    }

    function onDataBindingAsistencia(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onComboBoxLoad() {
        $(this).data("tComboBox").fill();
    }

    function onComboBoxChange() {
        //$("#Grid").data("tGrid").rebind();
    }

    function Imprimir(e) {
        var _Parametros = new Array(new Array(idPersona, 'idPersona'));
        InvocarReporte('rptFichaPersonal', _Parametros);
    }
</script>
<%= Html.Telerik().Grid<GeDoc.catCargosCategorias>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.categId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoria", "Agregar"), title = "Agregar" });
            //commands.Custom().Ajax(true).Name("cmdEstadisticas").ButtonType(GridButtonType.Text).Text("Estadísticas");
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catCargoCategoria")
                .Insert("_InsertEditing", "catCargoCategoria")
                .Update("_SaveEditing", "catCargoCategoria")
                .Delete("_DeleteEditing", "catCargoCategoria");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoria", "Modificar"), title = "Modificar" });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoria", "Eliminar"), title = "Eliminar" });
                commands.Custom("cmdPresupuestados")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -240px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoria", "Examinar"), title = "Cargos Presupuestados" });
                commands.Custom("cmdDesignados")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -257px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoria", "Expedientes"), title = "Designaciones" });
            }).Width("12%").Title("Acciones");
            columns.Bound(c => c.carLibres).Width("3%").Title("").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<div style='width: 100%;'><img src='" + Url.Content("~/Content") + "/Estados/Verde.png' title='Cargo con <#= carLibres #> lugares libres' height='22px' width='22px' style='vertical-align:middle; visibility: <#= carPresupuestado > carEmpleados ? \"visible\" : \"hidden\" #>;' /></div>");
            columns.Bound(c => c.carDescripcion).Width("24%").Title("Cargo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= carDescripcion #>' style='cursor: pointer;' id='txtcarDescripcion' style='white-space: nowrap;'><#= carDescripcion #> </label>");
            columns.Bound(c => c.categNumero).Width("8%").Title("Categoría").Visible(true);
            columns.Bound(c => c.categAntiguedadDesde).Width("12%").Title("Antigüedad Desde").Visible(true);
            columns.Bound(c => c.categAntiguedadHasta).Width("12%").Title("Antigüedad Hasta").Visible(true);
            columns.Bound(c => c.categHoras).Width("5%").Title("Horas").Visible(true);
            columns.Bound(c => c.carPresupuestado).Width("11%").Title("Presupuestados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.carEmpleados).Width("8%").Title("Ocupados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.carLibres).Width("5%").Title("Libres").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
        })
            .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .ClientEvents(events => events.OnEdit("onCommandEdit").OnCommand("onCommand"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
%>

<!-- Presupuestado -->
<% Html.Telerik().Window()
        .Name("CRUDPresupuestados")
        .Title("Cargos Presupuestados para:")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catCargosCategoriasPresupuestados>)ViewData["Presupuestados"])
            .Name("gridPresupuestados")
            .DataKeys(keys =>
            {
                keys.Add(p => p.presId);
            })
            .ToolBar(commands =>
            {
                //commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblCargo" style="font-size: 13px; font-weight: bold; text-align: center; margin-left: 30px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingPresupuestado", "catCargoCategoria")
                    .Update("_SaveEditingPresupuestado", "catCargoCategoria")
                    .Insert("_InsertEditingPresupuestado", "catCargoCategoria")
                    .Delete("_DeleteEditingPresupuestado", "catCargoCategoria");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.repNombre).Width("20%").Title("Zona Sanitaria").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" }).Encoded(false)
                    .ClientTemplate("<label title='<#= repNombre #>' style='cursor: pointer;' id='txtrepNombre' style='white-space: nowrap;'><#= repNombre #> </label>");
                columns.Bound(c => c.presFecha).Width("17%").Title("Fecha").Visible(true).HtmlAttributes(new { style = "text-align: center;" })
                    .FooterTemplate(() =>
                            {%>
                                <div align="right">Total Presupuestado: </div>
                            <%}).Visible(true);
                columns.Bound(c => c.presCantidad).Width("9%").Title("Cantidad").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                    .Aggregate(aggreages => aggreages.Sum())
                    .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
                columns.Bound(c => c.presObservaciones).Width("44%").Title("Observaciones").Visible(true).Encoded(false).HtmlAttributes(new { style = "white-space: nowrap;" }).Encoded(false)
                    .ClientTemplate("<label title='<#= presObservaciones #>' style='cursor: pointer;' id='txtpresObservaciones' style='white-space: nowrap;'><#= presObservaciones #> </label>");
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingPresupuestado", "catCargoCategoria", new { idCategoria = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingPresupuestados").OnEdit("onCommandEdit").OnSave("onSavePresupuestados").OnComplete("onCompletePresupuestados"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(280))
            .Sortable().Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(1000)
       .Height(400)
       .Render();
%>

<!-- Designaciones -->
<% Html.Telerik().Window()
        .Name("CRUDDesignados")
        .Title("Designaciones a:")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catCargosCategoriasDesignados>)ViewData["Designados"])
            .Name("gridDesignados")
            .DataKeys(keys =>
            {
                keys.Add(p => p.desigId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblCargo2" style="font-size: 13px; font-weight: bold; text-align: center; margin-left: 30px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingDesignados", "catCargoCategoria")
                    .Update("_SaveEditingDesignados", "catCargoCategoria")
                    .Insert("_InsertEditingDesignados", "catCargoCategoria")
                    .Delete("_DeleteEditingDesignados", "catCargoCategoria");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                    commands.Custom("Consultar").ButtonType(GridButtonType.Image)
                                        .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Examinar") })
                                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                                        .DataRouteValues(route => route.Add(o => o.perId).RouteKey("personaId"))
                                        .Ajax(true)
                                        .Action("ViewDetails", "catPersona");
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.desigVigenciaHasta).Width("3%").Title("").Visible(true)
                    .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Contrato de Baja' height='22px' width='22px' style='vertical-align:middle; visibility: <#= desigVigenciaHasta != null ? \"visible\" : \"hidden\" #>' /></div>");
                columns.Bound(c => c.desigTipo).Width("8%").Title("Tipo").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                columns.Bound(c => c.perNombre).Width("20%").Title("Empleado").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                    .ClientTemplate("<label title='<#= perNombre #>' style='cursor: pointer;' id='txtperNombre' style='white-space: nowrap;'><#= perNombre #> </label>");
                columns.Bound(c => c.desigVigenciaDesde).Width("8%").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                columns.Bound(c => c.desigVigenciaHasta).Width("8%").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                columns.Bound(c => c.perSubRoganciaNombre).Width("10%").Title("Subrogado por").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                    .ClientTemplate("<label title='<#= perSubRoganciaNombre #>' style='cursor: pointer;' id='txtperSubRoganciaNombre' style='white-space: nowrap;'><#= perSubRoganciaNombre #> </label>");
                columns.Bound(c => c.desigSubRoganciaDesde).Width("8%").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                columns.Bound(c => c.desigSubRoganciaHasta).Width("8%").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                columns.Bound(c => c.desigObservaciones).Width("15%").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                .ClientTemplate("<label title='<#= desigObservaciones #>' style='cursor: pointer;' id='txtdesigObservaciones' style='white-space: nowrap;'><#= desigObservaciones #> </label>");
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingDesignados", "catCargoCategoria", new { idCategoria = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingDesignados").OnEdit("onEdit").OnSave("onSaveDesignados").OnComplete("onCompleteDesignados").OnCommand("onCommandDesignados"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height("500px"))
            .Sortable().Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Render();
%>

<% Html.RenderPartial("ConsultaPersona"); %>
<% Html.RenderPartial("MensajeError"); %>
<% Html.RenderPartial("Estadisticas"); %>

