<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var idUsuario;
    function onRowSelected(e) {
        var ordersGrid = $('#GridLog').data('tGrid');
        idUsuario = e.row.cells[0].innerHTML;

        // update ui text
        var chart = $('#chart').data('tChart');
        ordersGrid.rebind();
        chart.rebind({ idUsuario: idUsuario });
        AbrirWaiting();
    }
    function onDataBindingLogUsuario(e) {
        e.data = $.extend(e.data, { idUsuario: idUsuario });
    }

    function onCompleteLogUsuario(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }

    function onComboBoxChangeOficinas() {
        var chart = $("#chTopFiveOficinas").data("tChart");
        chart.rebind({ ofiId: $(this).data("tComboBox").value() });
    }

    function onComboBoxLoadOficinas() {
        $(this).data("tComboBox").fill();
    }

</script>
<table>
<tr>
<td>
<%= Html.Telerik().Grid((IEnumerable<GeDoc.sisUsuarios>)ViewData["sisUsuario"])
        .Name("GridUsuarios")
        .DataKeys(keys =>
        {
            keys.Add(p => p.usrId);
        })
        .Localizable("es-AR")
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "sisLogUsuario");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.usrNombreDeUsuario).Width(30).Title("Nombre de Usuario").Visible(true);
            columns.Bound(c => c.usrApellidoyNombre).Width(60).Title("Apellido y Nombre").Visible(true);
        })
            .Footer(false)
            .HtmlAttributes(new { style = "width: 400px"})
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .ClientEvents(events => events.OnRowSelect("onRowSelected"))
            .RowAction(row => row.Selected = row.DataItem.usrId.Equals(ViewData["idUsuario"]))
%>
</td>
<td style="border: none; vertical-align: top;">
<% Html.Telerik().TabStrip()
        .Name("tabResultado")
        .HtmlAttributes(new { style = "height: 98%; padding: 0px; background: transparent; border: 0px; margin-left: -8px;" })
        .Items(tabstrip =>
        {
            tabstrip.Add()
                .Text("Detalle")
                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                .Content(() =>
                { %>
                    <div id="EditarGeneral" style="border: 1px solid #DADBE9; width: auto; height: 100%; vertical-align: top;">
                        <%= Html.Telerik().Grid((IEnumerable<GeDoc.sisLogUsuarios>)ViewData["sisLogUsuario"])
                                .Name("GridLog")
                                .Localizable("es-AR")
                                .DataBinding(dataBinding =>
                                {
                                    dataBinding.Ajax()
                                        .Select("_SelectEditingLog", "sisLogUsuario", new { idUsuario = "gtripolone" });
                                })
                                .Columns(columns =>
                                {
                                    columns.Bound(c => c.FechaCorta).Width("50px").Title("Fecha").Visible(true).Format("{0:dd/MM/yyyy}");
                                    columns.Bound(c => c.Hora).Width("30px").Title("Hora").Visible(true).Format("{0:hh:mm}");
                                    columns.Bound(c => c.Operacion).Width("200px").Title("Tarea realizada").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                    .ClientTemplate("<label title='<#= Operacion #>' style='cursor: pointer;' id='txtOperacion' style='white-space: nowrap;'><#= Operacion #> </label>");
                                })
                                    .Pageable((paging) => paging.Enabled(true).PageSize(20))
                                    .Footer(true)
                                    .HtmlAttributes(new { style = "width: 650px"})
                                    .Filterable()
                                    .Selectable()
                                    .Scrollable(scroll => scroll.Enabled(true).Height("349px"))
                                    .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingLogUsuario").OnComplete("onCompleteLogUsuario"))
                                    .Sortable(order => order.SortMode(GridSortMode.MultipleColumn))
                        %>
                    </div>
                <%});
            tabstrip.Add()
                .Text("Gráfico")
                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                .Content(() =>
                { %>
                    <div id="EditarConsiderando" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                        <%= Html.Telerik().Chart((IEnumerable<GeDoc.GraficoLogUsuario>)ViewData["Grafico"])
                                    .Name("chart")
                                    .Title(title => title
                                        .Text("Tareas diarias de los últimos 7 días")
                                        .Visible(true)
                                    )
                                    .Legend(legend => legend
                                        .Position(ChartLegendPosition.Bottom)
                                        .Visible(true)
                                    )
                                    .SeriesDefaults(series =>
                                    {
                                        series.Line()
                                                .Labels(labels => labels.Visible(true))
                                                .Stack(false);
                                    })
                                    .Series(series =>
                                    {
                                        series.Line(s => s.Cantidad).Name("Tareas realizadas").Markers(markers => markers.Type(ChartMarkerShape.Circle));
                                    })
                                    .CategoryAxis(axis => axis
                                        .Categories(s => s.Fecha.Date.Day)
                                    )
                                    .ValueAxis(axis => axis
                                                        .Numeric().Labels(labels => labels.Visible(true).Format("{0:dd/MM}"))
                                    )
                                    .Tooltip(tooltip => tooltip
                                        .Visible(true)
                                        .Format("{0:#,##0}")
                                    )
                                    .DataBinding(dataBinding => dataBinding.Ajax().Select("RebindEstadisticas", "sisLogUsuario"))
                                    .HtmlAttributes(new { style = "width: 550px; height: 406px;" })
                        %>
                    </div>
                <%});
            tabstrip.Add()
                .Text("Utilización")
                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                .Content(() =>
                { %>
                    <div id="Produccion" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                        <%= Html.Telerik().Chart((IEnumerable<GeDoc.GraficoLogUsuario>)ViewData["TopFive"])
                                    .Name("chTopFive")
                                    .Title(title => title
                                        .Text("5 que más Utilizaron el sistema en los últimos 60 días")
                                        .Visible(true)
                                    )
                                    .Legend(legend => legend
                                        .Position(ChartLegendPosition.Bottom)
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
                                        series.Column(s => s.Cantidad).Name("Tareas realizadas");
                                    })
                                    .CategoryAxis(axis => axis
                                    .Categories(s => s.Usuario)
                                    )
                                    .ValueAxis(axis => axis
                                                        .Numeric().Labels(labels => labels.Visible(true).Format("{0:#,##0}"))
                                    )
                                    .Tooltip(tooltip => tooltip
                                        .Visible(true)
                                        .Format("{0:#,##0}")
                                    )
                                    //.DataBinding(dataBinding => dataBinding.Ajax().Select("RebindEstadisticas", "sisLogUsuario"))
                                    .HtmlAttributes(new { style = "width: 550px; height: 406px;" })
                        %>
                    </div>
                <%});
            tabstrip.Add()
                .Text("Utilización por Oficina")
                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                .Content(() =>
                { %>
                    <label class="lblFuente" for="Customers-input" style="margin-left: 5px; vertical-align: super;">Oficinas:</label>
                    <%= Html.Telerik().ComboBox()
                        .Name("cbxOficinas")
                        .DropDownHtmlAttributes(new { style = "width:Auto;" })
                        .OpenOnFocus(true)
                        .AutoFill(true)
                        .Filterable(filtering =>
                        {
                            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                        })
                        .HighlightFirstMatch(true)
                        .ClientEvents(events => events.OnChange("onComboBoxChangeOficinas").OnLoad("onComboBoxLoadOficinas"))
                        .SelectedIndex(0)
                        .HtmlAttributes(new { style = "width: 350px;" })
                        .BindTo((SelectList)ViewData["ofiId_Data"])%>
                    <div style="border: 1px solid #DADBE9; width: auto; height: 100%">
                        <%= Html.Telerik().Chart((IEnumerable<GeDoc.GraficoLogUsuario>)ViewData["TopFiveOficinas"])
                                    .Name("chTopFiveOficinas")
                                    .Title(title => title
                                        .Text("Top Five por Oficina")
                                        .Visible(true)
                                    )
                                    .Legend(legend => legend
                                        .Position(ChartLegendPosition.Bottom)
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
                                        series.Column(s => s.Cantidad).Name("Tareas realizadas");
                                    })
                                    .CategoryAxis(axis => axis
                                    .Categories(s => s.Usuario)
                                    )
                                    .ValueAxis(axis => axis
                                                        .Numeric().Labels(labels => labels.Visible(true).Format("{0:#,##0}"))
                                    )
                                    .Tooltip(tooltip => tooltip
                                        .Visible(true)
                                        .Format("{0:#,##0}")
                                    )
                                    .DataBinding(dataBinding => dataBinding.Ajax().Select("TopFiveOficinas", "sisLogUsuario", new { ofiId = -1 }))
                                    .HtmlAttributes(new { style = "width: 550px; height: 406px;" })
                        %>
                    </div>
                <%});
        })
    .SelectedIndex(0)
    .Render();
 %>
</td>
</tr>
</table> 

<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>

