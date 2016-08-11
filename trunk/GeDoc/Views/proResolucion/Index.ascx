<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    var idResolucion;

    function onCommand(e) {
        if (e.name == "cmdverPDF") {
            var _Window = $("#verPDF").data("tWindow");

            if (e.dataItem['resLinkArchivo'] != "") {
                $("#divPDF").css("display", "none");
                $('#framePDF').attr('src', GetPathApp('Content/Archivos/Resoluciones/' + e.dataItem['resLinkArchivo'].toString()));
                $.post(GetPathApp('proResolucion/RegistrarLog'), { pAccion: "Examinar" }, function (data) {
                    if (data) {

                    }
                });

                _Window.title("Ver Resolución N° " + e.dataItem['resNumero'].toString() + "...").center().open();
            }
            else {
                $("#divPDF").css("display", "block");
                _Window.title("Ver Resolución N° " + e.dataItem['resNumero'].toString() + "...").center().open();
            }
        }
        if (e.name == "cmdCRUDExpedientes") {
            var expGrid = $('#gridExpedientes').data('tGrid');
            var _WindowExp = $("#CRUDExpedientes").data("tWindow");

            idResolucion = e.dataItem['resId'];
            expGrid.rebind();
            _WindowExp.center().open();
        }
        if (e.name == "cmdCRUDEmpleados") {
            var empGrid = $('#gridEmpleados').data('tGrid');
            var _WindowEmp = $("#CRUDEmpleados").data("tWindow");

            idResolucion = e.dataItem['resId'];
            empGrid.rebind();
            _WindowEmp.center().open();
        }
        if (e.name == "cmdverDecreto") {
            var _WindowDecreto = $("#verPDF").data("tWindow");

            if (e.dataItem['decLinkArchivo'] != "") {
                $("#divPDF").css("display", "none");
                $('#framePDF').attr('src', GetPathApp('Content/Archivos/Decretos/' + e.dataItem['decLinkArchivo'].toString()));

                $.post(GetPathApp('proResolucion/RegistrarLog'), { pAccion: "Norma Legal" }, function (data) {
                    if (data) {

                    }
                });

                _WindowDecreto.title("Ver Decreto N° " + e.dataItem['decNumero'].toString() + "...").center().open();
            }
            else {
                $("#divPDF").css("display", "block");
                _WindowDecreto.title("Ver Decreto N° ...").center().open();
            }

        }
        if (e.name == "cmdEstadisticas") {
            var _WindowEstadisticas = $("#wEstadisticas").data("tWindow");

            if (_WindowEstadisticas.isMaximized) {
                _WindowEstadisticas.center().open();
            }
            else {
                _WindowEstadisticas.center().maximize().open();
            }
        }
    }

    function onDataBindingResoluciones(e) {
        var _FiltrarPorConsiderando = "";
        for (var Indice = 0; Indice < e.filteredColumns.length; Indice++) {
            if (e.filteredColumns[Indice].member == "resConsiderando") {
                _FiltrarPorConsiderando = e.filteredColumns[Indice].filters[0].value;
            }
        }

        e.data = $.extend(e.data, { lstFiltros: _FiltrarPorConsiderando });
    }
    function onDataBinding(e) {
        e.data = $.extend(e.data, { idResolucion: idResolucion });
    }
    function onDataBindingEmpleados(e) {
        e.data = $.extend(e.data, { idResolucion: idResolucion });
    }
    function onSaveExpedientes(e) {
        var values = e.values;

        values.resId = idResolucion;
    }
    function onSaveEmpleados(e) {
        var values = e.values;

        values.resId = idResolucion;
    }
    function onDataBound(e) {
        //        alert("onDataBound");
    }
    function onRowDataBound(e) {
        //        alert("onRowDataBound");
    }

//    $("#spFiltrarTexto").click(function (event) {
//        var _divFiltro = $("#divFiltrarDetalle").css("display");
//        if (_divFiltro == "none") {
//            $("#divFiltrarDetalle").css("display", "block");
//            alert("Debería verse");
//        }
//        else {
//            $("#divFiltrarDetalle").css("display", "none");
//        }
//        event.preventDefault();
//        event.stopPropagation();
//    });

    function AbrirFiltro(e) {
        debugger;
        var _divFiltro = $("#divFiltrarDetalle").css("display");
        if (_divFiltro == "none") {
            $("#divFiltrarDetalle").css("display", "block");
        }
        else {
            $("#divFiltrarDetalle").css("display", "none");
        }
        e.preventDefault();
        e.stopPropagation();
    }
    </script>
<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>

<%--<% if (Session["Usuario"] == null)
   {
       Html.Action("LogOff", "Account");
       return;
   } %>--%>
<%
 //string _Busqueda = "<div id=\"divFiltrarDetalle\" class=\"t-animation-container\" style=\"top: 55px; left: 391px; display: none; width: 156px; height: 208px;\"><div class=\"t-filter-options t-group t-popup\" style=\"display: none; width: 148px; margin-top: 0px;\"><button class=\"t-button t-button-icontext t-button-expand t-clear-button"><span class="t-icon t-clear-filter"></span>Limpiar filtro</button><div class="t-filter-help-text">Mostrar filas con valor que</div><input type="text"><button class="t-button t-button-icontext t-button-expand t-filter-button\"><span class=\"t-icon t-filter\"></span>Filtrar</button></div></div>";
 ViewData["AltoEditor"] = "200px"; %>

<%= Html.Telerik().Grid<GeDoc.proResoluciones>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.resId);
        })
        .Localizable("es-AR") 
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEstadisticas").ButtonType(GridButtonType.Text).Text("Estadísticas");
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                //.Select("_SelectEditing", "proResolucion")
                .Insert("_InsertEditing", "proResolucion")
                .Update("_SaveEditing", "proResolucion")
                .Delete("_DeleteEditing", "proResolucion");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Eliminar") });
                commands.Custom("cmdverPDF")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Examinar") });
                commands.Custom("cmdCRUDExpedientes")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -33px -289px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Expedientes") });
                commands.Custom("cmdCRUDEmpleados")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -336px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Alertas") });
                commands.Custom("cmdverDecreto")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -190px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Norma Legal") });
            }).Width("190px").Title("Acciones");
            columns.Bound(c => c.tnlNombre).Width("120px").Title("Tipo").Visible(true);
            columns.Bound(c => c.resNumero).Width("80px").Title("Número").Visible(true);
            columns.Bound(c => c.resFecha).Width("100px").Title("Fecha").Visible(true);
            //columns.Bound(c => c.resConsiderando).Width("420px").Title("Detalle").Encoded(false).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
            columns.Bound(c => c.resConsiderando).Width("420px").Title("Detalle").Visible(true).Filterable(true)//.HeaderTemplate("<div class=\"t-grid-filter t-state-default\" style=\"margin-top: -5px;\"><span class=\"t-icon t-filter\" id=\"spFiltrarTexto\" >Filtrar</span></div><div id=\"divFiltrarDetalle\" class=\"t-animation-container\" style=\"top: 55px; left: 391px; display: none; width: 156px; height: 208px;\"><div class=\"t-filter-options t-group t-popup\" style=\"width: 148px; margin-top: 0px;\"><button class=\"t-button t-button-icontext t-button-expand t-clear-button\"><span class=\"t-icon t-clear-filter\"></span>Limpiar filtro</button><div class=\"t-filter-help-text\">Mostrar filas con valor que</div><input type=\"text\"><button class=\"t-button t-button-icontext t-button-expand t-filter-button\"><span class=\"t-icon t-filter\"></span>Filtrar</button></div></div>")
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/PDF2.png' height='22px' style='vertical-align:top;' /></div>");
            //columns.Bound(c => c.Expedientes).Width("25%").Title("Expedientes").Encoded(false).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
        })
                .Editable(editing => editing
                        .DisplayDeleteConfirmation(true).Mode(GridEditMode.PopUp))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(events => events.OnCommand("onCommand").OnEdit("onCommandEdit").OnDataBound("onDataBound").OnRowDataBound("onRowDataBound").OnDataBinding("onDataBindingResoluciones"))
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingResoluciones", "proResolucion", new { lstFiltros = "" }))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .RowAction(row => row.Selected = row.DataItem.resId.Equals(ViewData["idResolucion"]))
%>
<!--<span class="t-icon t-filter" id="spFiltrarTexto" onclick="alert('Por Aquí');">Filtrar</span>-->
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
<!-- Asignación Expedientes -->
<% Html.Telerik().Window()
        .Name("CRUDExpedientes")
        .Title("Asignación de Expedientes")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <%= Html.Telerik().Grid((IEnumerable<GeDoc.proResolucionesExpedientes>)ViewData["Expedientes"])
            .Name("gridExpedientes")
            .DataKeys(keys =>
            {
                keys.Add(p => p.reseId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingExpedientes", "proResolucion")
                    .Insert("_InsertEditingExpedientes", "proResolucion")
                    .Update("_SaveEditingExpedientes", "proResolucion")
                    .Delete("_DeleteEditingExpedientes", "proResolucion");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width(20).Title("Acciones");
                columns.Bound(c => c.zonNombre).Width(80).Title("Zona").Visible(true);
                columns.Bound(c => c.zonCodigo).Width(30).Title("Código").Visible(true);
                columns.Bound(c => c.reseExpedienteNumero).Width(30).Title("Número").Visible(true);
                columns.Bound(c => c.reseExpedienteAnio).Width(30).Title("Año").Encoded(false).Visible(true);
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingExpedientes", "proResolucion", new { idResolucion = 1 }))
            //.KeyboardNavigation(keyNav => keyNav.Enabled(true))
            .Editable(editing => editing
                            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding").OnSave("onSaveExpedientes"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(770)
       .Height((int)Session["AlturaGrilla"] + 100)
       .Render();
%>
<!-- Empleados -->
<% Html.Telerik().Window()
        .Name("CRUDEmpleados")
        .Title("Notificaciones a Empleados")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <%= Html.Telerik().Grid((IEnumerable<GeDoc.proResolucionesNotificacionEmpleado>)ViewData["Empleados"])
            .Name("gridEmpleados")
            .DataKeys(keys =>
            {
                keys.Add(p => p.resneId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingEmpleados", "proResolucion")
                    .Update("_SaveEditingEmpleados", "proResolucion")
                    .Insert("_InsertEditingEmpleados", "proResolucion")
                    .Delete("_DeleteEditingEmpleados", "proResolucion");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);//.HtmlAttributes(new { style = "visibility: hidden" });
                    commands.Delete().ButtonType(GridButtonType.Image);//.HtmlAttributes(new { style = "margin-left: -28px" });
                }).Width(20).Title("Acciones");
                columns.Bound(c => c.perNombre).Width(80).Title("Empleado").Visible(true);
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingEmpleados", "proResolucion", new { idResolucion = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingEmpleados").OnSave("onSaveEmpleados"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(200))
            .Sortable()
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(570)
       .Height(300)
       .Render();
%>

<!-- Empleados -->
<% Html.Telerik().Window()
        .Name("wEstadisticas")
        .Title("Estadistica de Resoluciones...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div id="Estadística" style="border: 1px solid #DADBE9; width: auto; height: 100%; overflow: scroll;">
            <% Html.Telerik().Chart((IEnumerable<GeDoc.GraficoLogUsuario>)ViewData["Estadisticas"])
                        .Name("chResoluciones")
                        .Title(title => title
                            .Text("Cantidad de Resoluciones por Tipo de Resolución")
                            .Visible(true)
                            .Align(ChartTextAlignment.Center)
                        )
                        .Legend(legend => legend
                            .Position(ChartLegendPosition.Right)
                            .Visible(true)
                        )
                        .SeriesDefaults(series =>
                        {
                            series.Column()
                                    .Labels(labels => labels.Visible(true))
                                    .Stack(false);
                        })
                        .Series(series =>
                        {
                            string[] _Color = { "#13558E", "#E21219", "#DE7E42", "#057589", "#0066CC", "#CCCCFF", "#CCFFCC", 
                                                  "#CC99FF", "#9900CC", "#66FF66", "#660066", "#00CCFF", "#FFCC66", "#006666", 
                                                  "#FFFF66", "#006600", "#996633", "#FF3300", "#000099", "#339933", "#333300",
                                                  "#669999", "#FFCCCC", "#99CC00", "#993333", "#666699", "#660066", "#BEB6B9",
                                                  "#0066FF", "#CC66FF", "#FFFF66", "#993300", "#92A471", "#D2BA6F", "#171717",
                                                  "#245B47", "#004080", "#F1F0C0", "#D5FFD5", "", "", "", "", "", "", "", "", "", "" };
                            int C = 0;
                            int _Sumador = 520;
                            foreach (var x in (IEnumerable<GeDoc.GraficoLogUsuario>)ViewData["Estadisticas"])
                            {
                                foreach (var z in x.Datos)
                                {
                                    series.Column(new int[] { z.Cantidad }).Name(z.Usuario + " (" + z.Cantidad.ToString() + ")").Labels(true).Color(_Color[C]);
                                    C++;
                                    if (C >= _Color.Length)
                                    {
                                        C = _Color.Length - 1;
                                        _Color[C] = "#" + (Convert.ToInt32(_Color[0].Substring(1, 5)) + _Sumador).ToString() + "E";
                                        _Sumador += 520;
                                    }
                                }
                            }
                        })
                        .CategoryAxis(axis => axis
                            .Categories(s => s.Usuario).Labels(true)
                        )
                        .ValueAxis(axis => axis
                                .Numeric().Labels(labels => labels.Visible(true).Format("{0:#,##0}"))
                        )
                        .Tooltip(tooltip => tooltip
                            .Visible(true)
                            .Format("{0:#,##0}")
                            .Template("<#= series.name #>")
                        )
                        .HtmlAttributes(new { style = "width: 1000px; height: 600px;" })
                        .Render();
            %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(570)
       .Height(300)
       .Render();
%>
