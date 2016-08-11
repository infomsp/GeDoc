<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>

<div style="overflow: hidden; height: 510px;" >
<% Html.Telerik().Grid<GeDoc.expExpedientes>()
        .Name("GridBandejaEntrada")
        .DataKeys(keys =>
        {
            keys.Add(p => p.Id);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Template(grid =>
            { %>
                <div class="t-button" onclick="Imprimir();" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/Printer.png" height="18" alt="Imprimir carátula" style="vertical-align: middle;" />
                    Imprimir
                </div>
                <div class="t-button" onclick="Pases();" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/intercambio.gif" height="18" alt="Pases" style="vertical-align: middle;" />
                    Pases
                </div>
            <%
            }
              );
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditingEntrada", "expExpediente");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.Id).Width("80px").Title("Código").Visible(true);
            columns.Bound(c => c.Tipo).Width("60px").Title("Tipo").Visible(true);
            columns.Bound(c => c.Expediente).Width("150px").Title("Número").Visible(true);
            columns.Bound(c => c.Clave).Width("60px").Title("Clave").Visible(true);
            columns.Bound(c => c.Fecha).Width("140px").Title("Fecha").Visible(true).Format("{0:dd/MM/yyyy hh:mm}");
            //columns.Bound(c => c.DiasCorridos).Width("120px").Title("Días Corridos").Visible(true);
            //columns.Bound(c => c.DiasHabiles).Width("120px").Title("Días Hábiles").Visible(true);
            columns.Bound(c => c.IniciadorNombre).Width("250px").Title("Nombre del Iniciador").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= IniciadorNombre #>' style='cursor: pointer;' id='txtIniciadorNombre' style='white-space: nowrap;'><#= IniciadorNombre #> </label>");
            columns.Bound(c => c.Extracto).Width("250px").Title("Extracto").Visible(true).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Extracto #>' style='cursor: pointer;' id='txtExtracto' style='white-space: nowrap;'><#= Extracto #> </label>");
            columns.Bound(c => c.Comentarios).Width("250px").Title("Comentarios").Visible(true).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Comentarios #>' style='cursor: pointer;' id='txtComentarios' style='white-space: nowrap;'><#= Comentarios #> </label>");
            columns.Bound(c => c.Folios).Width("250px").Title("Folios").Visible(true);
            columns.Bound(c => c.DestinoNombre).Width("250px").Title("De donde Viene").Visible(true).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= DestinoNombre #>' style='cursor: pointer;' id='txtDestinoNombre' style='white-space: nowrap;'><#= DestinoNombre #> </label>");
            columns.Bound(c => c.fechaMovimiento).Width("140px").Title("Fecha de Envío").Visible(true).Format("{0:dd/MM/yyyy hh:mm}");
        })
            .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .ClientEvents(events => events.OnCommand("onCommand").OnRowSelected("onRowSelected").OnDataBinding("onDataBinding").OnComplete("onComplete"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(ViewBag.AlturaGrilla))
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
            .HtmlAttributes(new { style = "width: 99.8%;" })
            .Render();
%>
</div>
