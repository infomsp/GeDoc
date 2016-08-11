<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<div>
    <% Html.Telerik().Grid<GeDoc.catCargosCategoriasDesignados>()
    .Name("gridConsultaDesignados")
    .DataKeys(keys =>
    {
        keys.Add(p => p.desigId);
    })
    .DataBinding(dataBinding =>
    {
        dataBinding.Ajax()
            .Select("_ConsultaDesignaciones", "catPersona", new { idPersona = 1 });
    })
    .Columns(columns =>
    {
        columns.Bound(c => c.desigVigenciaHasta).Width("20px").Title("").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Designación de Baja' height='10px' width='10px' style='vertical-align:middle; visibility: <#= desigVigenciaHasta != null ? \"visible\" : \"hidden\" #>' /></div>");
        columns.Bound(c => c.carDescripcion).Width("200px").Title("Cargo").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
            .ClientTemplate("<label title='<#= carDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= carDescripcion #> </label>");
        columns.Bound(c => c.desigTipo).Width("90px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
        columns.Bound(c => c.desigVigenciaDesde).Width("90px").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
        columns.Bound(c => c.desigVigenciaHasta).Width("90px").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
        columns.Bound(c => c.perSubRoganciaNombre).Width("200px").Title("Subrogado por").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
            .ClientTemplate("<label title='<#= perSubRoganciaNombre #>' style='cursor: pointer;' id='txtperSubRoganciaNombre' style='white-space: nowrap;'><#= perSubRoganciaNombre #> </label>");
        columns.Bound(c => c.desigSubRoganciaDesde).Width("90px").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
        columns.Bound(c => c.desigSubRoganciaHasta).Width("90px").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
        columns.Bound(c => c.desigObservaciones).Width("250px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
        .ClientTemplate("<label title='<#= desigObservaciones #>' style='cursor: pointer;' id='txtdesigObservaciones' style='white-space: nowrap;'><#= desigObservaciones #> </label>");
    })
    .Editable(editing => editing
                                .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
    .Pageable((paging) =>
                        paging.Enabled(true)
                            .PageSize(((int)Session["FilasPorPagina"])))
    .Footer(true)
    .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingConsultaDesignaciones"))
    .Filterable()
    .Selectable()
    .Resizable(resizing => resizing.Columns(true))
    .Scrollable(scroll => scroll.Enabled(true).Height("500px"))
    .Sortable().Render();
        %>
</div>
