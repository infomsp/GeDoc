<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    var idGradoCategoria;
    var _Presupuestados;
    var idPersona;
    function onCommand(e) {
        if (e.name == "cmdPresupuestados") {
            var estGrid = $('#gridGradosPresupuestados').data('tGrid');
            var _WindowEst = $("#CRUDGradosPresupuestados").data("tWindow");

            idGradoCategoria = e.dataItem['gracId'];

            $("#lblCargo").text(e.dataItem['Agrupamiento'] + ', ' + e.dataItem['Grado'].toString() + ', ' + e.dataItem['gracDescripcion'].toString());

            estGrid.ajaxRequest();
            _WindowEst.center().open();
        } else if (e.name == "cmdDesignados") {
            var estGrid = $('#gridGradosDesignados').data('tGrid');
            var _WindowDes = $("#CRUDGradosDesignados").data("tWindow");

            idGradoCategoria = e.dataItem['gracId'];

            $("#lblGrado").text(e.dataItem['Agrupamiento'] + ', ' + e.dataItem['Grado'].toString() + ', ' + e.dataItem['gracDescripcion'].toString());

            estGrid.ajaxRequest();
            _WindowDes.center().open();
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
    function onEdit(e) {
        var _Categoria = $('#gracId-input')[0];
        _Categoria.disabled = true;

        onCommandEdit(e);
    }

    function onCommandDesignadosGrados(e) {
        if (e.name == "Consultar") {
            AbrirWaiting();
        }
    }

    function onDataBindingEstados(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onDataBindingPresupuestados(e) {
        e.data = $.extend(e.data, { idGradoCategoria: idGradoCategoria });
    }
    function onSavePresupuestados(e) {
        var values = e.values;

        values.gracId = idGradoCategoria;
    }
    function onCompletePresupuestados(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            var pagGrid = $('#Grid').data('tGrid');
            pagGrid.ajaxRequest();
        }
    }
    
    function onCompleteDesignadosGrados(e) {
        debugger;
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            var pagGrid = $('#Grid').data('tGrid');
            pagGrid.ajaxRequest();
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
            $("#lblperObservaciones").text(_Persona.perObservaciones);
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
            estGrid.ajaxRequest();
            var asGrid = $('#gridAsistencia').data('tGrid');
            asGrid.ajaxRequest();
            CerrarWaiting();
            detailWindow.center().open();
        }
    }
    function onDataBindingGradosDesignados(e) {
        e.data = $.extend(e.data, { idGrado: idGradoCategoria });
    }
    function onDataBindingAsistencia(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onVerPDF(LinkArchivo, Numero) {
        var _Window = $("#verPDF").data("tWindow");
        debugger;
        if (LinkArchivo != "") {
            $("#divPDF").css("display", "none");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/Resoluciones/' + LinkArchivo));
            _Window.title("Ver Resolución N° " + Numero.toString() + "...").center().open();
        }
        else {
            $("#divPDF").css("display", "block");
            _Window.title("Ver Resolución N° " + Numero.toString() + "...").center().open();
        }
    }
    function onVerDecreto(LinkArchivo, Numero) {
        var _Window = $("#verPDF").data("tWindow");
        debugger;
        if (LinkArchivo != "") {
            $("#divPDF").css("display", "none");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/Decretos/' + LinkArchivo));
            _Window.title("Ver Decreto N° " + Numero.toString() + "...").center().open();
        }
    }
</script>
<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("verPDF")
        .Title("Resoluciones")
        .Visible(false)
        .Content(() => 
        {%> 
            <div id="divPDF" style="width: 100%; height: 100%; display: none;">
                <table style="border: none; margin:10px;">
                    <tr style="border: none; margin:0px;">
                        <td style="border: none; margin:0px;">
                            <img src="<%= Url.Content("~/Content") %>/General/Notificacion/Vista Internet Security OFF.png" width="40px" style="margin-top:-5px" />
                        </td>
                        <td style="border: none; margin:0px;">
                            <asp:Label ID="Label1" runat="server" style="margin-left:-5px" ForeColor="Red">Archivo no encontrado...</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <iframe id="framePDF" src='' width="100%" height="100%" >
            </iframe>
        <%})
        .Buttons(b => b.Maximize().Close())
        .Draggable(true)
        .Scrollable(false)
        .Resizable()
        .Width(870)
        .Height(460)
        .Render();
%>

<%= Html.Telerik().Grid<GeDoc.catGradosCategoriaWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.gracId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catGradosCategoria", "Agregar"), title = "Agregar" });
            //commands.Custom().Ajax(true).Name("cmdEstadisticas").ButtonType(GridButtonType.Text).Text("Estadísticas");
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catGradosCategoria")
                .Insert("_InsertEditing", "catGradosCategoria")
                .Update("_SaveEditing", "catGradosCategoria")
                .Delete("_DeleteEditing", "catGradosCategoria");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catGradosCategoria", "Modificar"), title = "Modificar" });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catGradosCategoria", "Eliminar"), title = "Eliminar" });
                commands.Custom("cmdPresupuestados")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -240px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catGradosCategoria", "Examinar"), title = "Cargos Presupuestados" });
                commands.Custom("cmdDesignados")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -257px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catGradosCategoria", "Expedientes"), title = "Designaciones" });
            }).Width("12%").Title("Acciones");
            columns.Bound(c => c.gracLibres).Width("3%").Title("").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<div style='width: 100%;'><img src='" + Url.Content("~/Content") + "/Estados/Verde.png' title='Cargo con <#= gracLibres #> lugares libres' height='22px' width='22px' style='vertical-align:middle; visibility: <#= gracPresupuestado > gracEmpleados ? \"visible\" : \"hidden\" #>;' /></div>");
            columns.Bound(c => c.Agrupamiento).Width("24%").Title("Agrupamiento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Agrupamiento #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Agrupamiento #> </label>");
            columns.Bound(c => c.Grado).Width("24%").Title("Grado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Grado #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Grado #> </label>");
            columns.Bound(c => c.gracDescripcion).Width("24%").Title("Categoría").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= gracDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= gracDescripcion #> </label>");
            columns.Bound(c => c.gracPresupuestado).Width("11%").Title("Presupuestados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.gracEmpleados).Width("8%").Title("Ocupados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.gracLibres).Width("5%").Title("Libres").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
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
        .Name("CRUDGradosPresupuestados")
        .Title("Cargos Presupuestados para:")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catGradosCategoriasPresupuestadosWS>)ViewData["Presupuestados"])
            .Name("gridGradosPresupuestados")
            .DataKeys(keys =>
            {
                keys.Add(p => p.psId);
            })
            .ToolBar(commands =>
            {
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
                    .Select("_SelectEditingPresupuestado", "catGradosCategoria")
                    .Update("_SaveEditingPresupuestado", "catGradosCategoria")
                    .Insert("_InsertEditingPresupuestado", "catGradosCategoria")
                    .Delete("_DeleteEditingPresupuestado", "catGradosCategoria");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.Institucion).Width("20%").Title("Institución").Visible(true);
                columns.Bound(c => c.psFecha).Width("17%").Title("Fecha").Visible(true).HtmlAttributes(new { style = "text-align: center;" })
                    .FooterTemplate(() =>
                            {%>
                                <div align="right">Total Presupuestado: </div>
                            <%}).Visible(true);
                columns.Bound(c => c.psCantidad).Width("9%").Title("Cantidad").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                    .Aggregate(aggreages => aggreages.Sum())
                    .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
                columns.Bound(c => c.psObservaciones).Width("44%").Title("Observaciones").Visible(true).Encoded(false).HtmlAttributes(new { style = "white-space: nowrap;" }).Encoded(false)
                    .ClientTemplate("<label title='<#= psObservaciones #>' style='cursor: pointer;' style='white-space: nowrap;'><#= psObservaciones #> </label>");
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingPresupuestado", "catGradosCategoria", new { idGradoCategoria = 1 }))
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
        .Name("CRUDGradosDesignados")
        .Title("Designaciones a:")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catGradosDesignacionWS>)ViewData["GradosDesignados"])
            .Name("gridGradosDesignados")
            .DataKeys(keys =>
            {
                keys.Add(p => p.gdId);
            })
            .Localizable("es-AR")
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <label id="lblGrado" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_BindingGradosCategoria", "catPersona", new { gracId = 0 });
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Custom("Consultar").ButtonType(GridButtonType.Image)
                                        .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Examinar"), title = "Ver Legajo" })
                                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                                        .DataRouteValues(route => route.Add(o => o.perId).RouteKey("personaId"))
                                        .Ajax(true)
                                        .Action("ViewDetails", "catPersona");
                }).Width("80px").Title("Acciones");
                columns.Bound(c => c.gdFechaHasta).Width("40px").Title("").Visible(true)
                    .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo(2).png' title='Grado de Baja' height='10px' width='10px' style='vertical-align:middle; visibility: <#= gdFechaHasta != null ? \"visible\" : \"hidden\" #>' /></div>");
                columns.Bound(c => c.InfoPersona.perApellidoyNombre).Width("200px").Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= InfoPersona.perApellidoyNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= InfoPersona.perApellidoyNombre #> </label>");
                columns.Bound(c => c.InfoPersona.perDNI).Width("100px").Title("DNI").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= InfoPersona.perDNI #>' style='cursor: pointer;' style='white-space: nowrap;'><#= InfoPersona.perDNI #> </label>");
                //columns.Bound(c => c.Agrupamiento).Width("200px").Title("Agrupamiento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                //.ClientTemplate("<label title='<#= Agrupamiento #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Agrupamiento #> </label>");
                //columns.Bound(c => c.Grado).Width("100px").Title("Grado").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"})
                //.ClientTemplate("<label title='<#= Grado #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Grado #> </label>");
                //columns.Bound(c => c.Categoria).Width("200px").Title("Categoría").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"})
                //.ClientTemplate("<label title='<#= Categoria #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Categoria #> </label>");
                columns.Bound(c => c.gdServicio).Width("200px").Title("Servicio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= gdServicio #>' style='cursor: pointer;' style='white-space: nowrap;'><#= gdServicio #> </label>");
                columns.Bound(c => c.TipoCargo).Width("100px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= TipoCargo #>' style='cursor: pointer;' style='white-space: nowrap;'><#= TipoCargo #> </label>");
                columns.Bound(c => c.gdFecha).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha").Visible(true);
                columns.Bound(c => c.gdFechaDesde).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha Inicial").Visible(true);
                columns.Bound(c => c.gdFechaHasta).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha Baja").Visible(true);
                columns.Bound(c => c.gdHoras).Width("70px").Title("Horas").Visible(true);
                columns.Bound(c => c.gdHorasAdicional).Width("120px").Title("Horas Adicionales").Visible(true);
                columns.Bound(c => c.Resolucion).Width("120px").Title("Resolución").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='Haga click aquí para ver la resolución' onclick=\"onVerPDF('<#= InfoResolucion.resLinkArchivo #>', <#= InfoResolucion.resNumero #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= Resolucion #> </label>");
                columns.Bound(c => c.Decreto).Width("120px").Title("Decreto").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='Haga click aquí para ver el decreto' onclick=\"onVerDecreto('<#= InfoDecreto.decLinkArchivo #>', <#= InfoDecreto.decNumero #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= Decreto #> </label>");
                columns.Bound(c => c.gdObservaciones).Width("120px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= gdObservaciones #>' style='cursor: pointer;' style='white-space: nowrap;'><#= gdObservaciones #> </label>");
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                                paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingGradosDesignados").OnComplete("onCompleteDesignadosGrados").OnCommand("onCommandDesignadosGrados"))
            .Filterable()
            .Selectable()
            .Resizable(resizing => resizing.Columns(true))
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Width(1024)
       .Draggable(true)
       .Scrollable(false)
       .Render();
%>

<% Html.RenderPartial("ConsultaPersona"); %>
<% Html.RenderPartial("MensajeError"); %>
