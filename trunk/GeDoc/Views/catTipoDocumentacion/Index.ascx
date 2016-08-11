<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Telerik().Grid<GeDoc.catTipoDocumentacionWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.tdId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTipoDocumentacion", "Agregar") });
        })
        .HtmlAttributes(new { style = "height:100%; width: 100%;" })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catTipoDocumentacion")
                .Insert("_InsertEditing", "catTipoDocumentacion")
                .Update("_SaveEditing", "catTipoDocumentacion")
                .Delete("_DeleteEditing", "catTipoDocumentacion");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTipoDocumentacion", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catTipoDocumentacion", "Eliminar") });
            }).Width("60px").Title("Acciones");
            columns.Bound(c => c.tdId).Width("80px").Title("Código").Visible(true);
            columns.Bound(c => c.tdDescripcion).Width("300px").Title("Descripción").Visible(true);
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
