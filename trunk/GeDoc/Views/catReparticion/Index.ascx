<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Telerik().Grid<GeDoc.catReparticiones>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.repId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catReparticion", "Agregar") });
        })
        .HtmlAttributes(new { style = "height:100%; width: 100%;" })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catReparticion")
                .Insert("_InsertEditing", "catReparticion")
                .Update("_SaveEditing", "catReparticion")
                .Delete("_DeleteEditing", "catReparticion");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catReparticion", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catReparticion", "Eliminar") });
            }).Width("60px").Title("Acciones");
            columns.Bound(c => c.repId).Width("50px").Title("Código").Visible(true);
            columns.Bound(c => c.repNombre).Width("200px").Title("Nombre").Visible(true);
            columns.Bound(c => c.Sectores).Width("100px").Title("Sectores").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.Empleados).Width("100px").Title("Empleados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.Contratados).Width("100px").Title("Contratados").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
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
