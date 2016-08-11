<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<%= Html.Telerik().Grid<GeDoc.catProductosImprenta>()
        .Name("Grid")
            .DataKeys(keys =>
            {
                keys.Add(p => p.piId);
            })
             .Localizable("es-AR")
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" }).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProductoImprenta", "Agregar") });
            })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catProductoImprenta")
                .Insert("_InsertEditing", "catProductoImprenta")
                .Update("_SaveEditing", "catProductoImprenta")
                .Delete("_DeleteEditing", "catProductoImprenta");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProductoImprenta", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProductoImprenta", "Eliminar") });
            }).Width("60px").Title("Acciones");
            columns.Bound(c => c.piId).Width("50px").Title("Código").Visible(true);
            columns.Bound(c => c.piDescripcion).Width("300px").Title("Descripción").Visible(true);
        })
            .Editable(editing => editing
                    .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .ClientEvents(events => events.OnEdit("onCommandEdit"))
            .Footer(true)
            .Filterable()
            .Selectable()
                .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
%>
