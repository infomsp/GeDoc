<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Telerik().Grid<GeDoc.catAdministradorWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.admId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("GestionContable", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "GestionContable")
                .Insert("_InsertEditing", "GestionContable")
                .Update("_SaveEditing", "GestionContable")
                .Delete("_DeleteEditing", "GestionContable");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("GestionContable", "Modificar") }); ;
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("GestionContable", "Eliminar") }); ;
            }).Width("10%").Title("Acciones");
            columns.Bound(c => c.admCodigo).Width("10%").Title("Código").Visible(true);
            columns.Bound(c => c.admDescripcion).Width("40%").Title("Descripción").Visible(true);
            columns.Bound(c => c.ceDescripcion).Width("40%").Title("Cuenta Escritural").Visible(true);
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