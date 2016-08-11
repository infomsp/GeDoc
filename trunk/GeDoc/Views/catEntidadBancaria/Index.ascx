<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<%= Html.Telerik().Grid<GeDoc.catEntidadesBancarias>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.bcoId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catEntidadBancaria", "Agregar"), title = "Agregar" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catEntidadBancaria")
                .Insert("_InsertEditing", "catEntidadBancaria")
                .Update("_SaveEditing", "catEntidadBancaria")
                .Delete("_DeleteEditing", "catEntidadBancaria");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catEntidadBancaria", "Modificar"), title = "Modificar" }); ;
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catEntidadBancaria", "Eliminar"), title = "Eliminar" }); ;
            }).Width(25).Title("Acciones");
            columns.Bound(c => c.bcoRazonSocial).Width(80).Title("Razon Social").Visible(true);
            columns.Bound(c => c.bcoCUIT).Width(80).Title("CUIT").Visible(true);
            columns.Bound(c => c.bcoNumeroSucursal).Width(80).Title("Número Sucursal").Visible(true);
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