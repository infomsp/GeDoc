<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<%= Html.Telerik().Grid<GeDoc.catProfesiones>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.profId);
        })
         .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProfesion", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catProfesion")
                .Insert("_InsertEditing", "catProfesion")
                .Update("_SaveEditing", "catProfesion")
                .Delete("_DeleteEditing", "catProfesion");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProfesion", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catProfesion", "Eliminar") });
            }).Width("100px").Title("Acciones");
            columns.Bound(c => c.profNombre).Width("300px").Title("Profesión").Visible(true);
            columns.Bound(c => c.profEmpleados).Width("100px").Title("Empleados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.profContratados).Width("100px").Title("Contratados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
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

