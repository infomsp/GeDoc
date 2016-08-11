<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<%= Html.Telerik().Grid<GeDoc.catCargos>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.carId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargo", "Agregar"), title = "Agregar" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catCargo")
                .Insert("_InsertEditing", "catCargo")
                .Update("_SaveEditing", "catCargo")
                .Delete("_DeleteEditing", "catCargo");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargo", "Modificar"), title = "Modificar" });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargo", "Eliminar"), title = "Eliminar" });
            }).Width("10%").Title("Acciones");
            columns.Bound(c => c.agrDescripcion).Width("20%").Title("Agrupamiento").Visible(true);
            columns.Bound(c => c.carDescripcion).Width("60%").Title("Denominación del Cargo").Visible(true);
            columns.Bound(c => c.carEmpleados).Width("10%").Title("Empleados").Visible(true);
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
