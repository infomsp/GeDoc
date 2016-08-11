<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<%= Html.Telerik().Grid<GeDoc.catSectores>()
        .Name("Grid")
            .DataKeys(keys =>
            {
                keys.Add(p => p.secId);
            })
             .Localizable("es-AR")
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" }).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catSector", "Agregar") });
            })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catSector")
                .Insert("_InsertEditing", "catSector")
                .Update("_SaveEditing", "catSector")
                .Delete("_DeleteEditing", "catSector");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catSector", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catSector", "Eliminar") });
            }).Width("60px").Title("Acciones");
            columns.Bound(c => c.secCodigo).Width("70px").Title("Código").Visible(true);
            columns.Bound(c => c.secNombre).Width("300px").Title("Nombre").Visible(true);
            //columns.Bound(c => c.secUbicacionGeografica).Width(100).Title("Ubicación geográfica").Visible(true);
            columns.Bound(c => c.repNombre).Width("100px").Title("Zona").Visible(true);
            columns.Bound(c => c.ccCuentaContable.ccCodigo).Width("80px").Title("Cuenta Contable").Visible(true)
            .ClientTemplate("<label title='<#= ccCuentaContable.ccDescripcion #>' style='cursor: pointer;' id='lblccCuentaContable_ccCodigo' ><#= ccCuentaContable.ccCodigo #></label>");
            columns.Bound(c => c.Empleados).Width("60px").Title("Empleados").Visible(true);
        })
            .Editable(editing => editing
                    .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .Filterable()
            .Selectable()
                .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
%>

