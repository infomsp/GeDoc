<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" >
    function VerPases(idExpediente) {
        AbrirWaiting();
        $.post(GetPathApp('expExpediente/getPases'), { pExpedienteId: idExpediente }, function (result) {
            // TODO: use the results returned from your controller action
            // to update the UI
            if (result) {
                var _gridPases = $("#gridPases").data("tGrid");
                _gridPases.dataBind(result.Data);
                $("#txtUbicacionActual")[0].value = result.Encabezado.UbicacionActual;

                var _WindowPases = $("#ExpedientesPases").data("tWindow");
                _WindowPases.center().open();
                CerrarWaiting();
            }
        });
    }
</script>

<!-- Pases -->
<% Html.Telerik().Window()
        .Name("ExpedientesPases")
        .Title("Pases")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <div class="BordeRedondeado" style="margin-bottom: 8px;">
                    <div style="margin: 5px;">
                        <label>Ubicación actual:</label>
                        <input id="txtUbicacionActual" disabled="disabled" style="width: 892px; font-size: 20px; color: darkslategray; vertical-align: middle;" />
                    </div>
                </div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.expExpedientesPases>)ViewData["expPases"])
            .Name("gridPases")
            .DataKeys(keys =>
            {
                keys.Add(p => p.Id);
            })
            .Localizable("es-AR")
            .Columns(columns =>
            {
                columns.Bound(c => c.NumeroPase).Width("50px").Title("Pase").Visible(true);
                columns.Bound(c => c.origenFecha).Width("140px").Title("Enviado").Visible(true).Format("{0:dd/MM/yyyy hh:mm}");
                columns.Bound(c => c.origenAlias).Width("150px").Title("Enviado por").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= origenAlias #>' style='cursor: pointer;' id='txtorigenAlias' style='white-space: nowrap;'><#= origenAlias #> </label>");
                columns.Bound(c => c.origenNombre).Width("250px").Title("Origen").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= origenNombre #>' style='cursor: pointer;' id='txtorigenNombre' style='white-space: nowrap;'><#= origenNombre #> </label>");
                columns.Bound(c => c.destinoFecha).Width("140px").Title("Recibido").Visible(true).Format("{0:dd/MM/yyyy hh:mm}");
                columns.Bound(c => c.destinoAlias).Width("150px").Title("Recibido por").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= destinoAlias #>' style='cursor: pointer;' id='txtdestinoAlias' style='white-space: nowrap;'><#= destinoAlias #> </label>");
                columns.Bound(c => c.destinoNombre).Width("250px").Title("Destino").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= destinoNombre #>' style='cursor: pointer;' id='txtdestinoNombre' style='white-space: nowrap;'><#= destinoNombre #> </label>");
            })
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(100))
            .Footer(false)
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(370))
            .Resizable(resizing => resizing.Columns(true))            
            .HtmlAttributes(new { style = "width: 99.8%;" })
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Modal(true)
       .Scrollable(false)
       .Width(1024)
       .Height(460)
       .Render();
%>
