<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%@ Import Namespace="GeDoc.Models" %>

<%
    var InstitucionContable = "";
    if ((Session["Usuario"] as sisUsuario).icId != null)
    {
        InstitucionContable = " - " + (Session["Usuario"] as sisUsuario).catInstitucionContable.icDescripcion;
%>
<script>
    $("#divTituloCatalogos").text($("#divTituloCatalogos").text() + "<%=InstitucionContable %>")
</script>
<%= Html.Telerik().Grid<GeDoc.catTipoGastoWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.tgId);
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
                .Select("_SelectEditing_catTipoGasto", "GestionContable")
                .Insert("_InsertEditing_catTipoGasto", "GestionContable")
                .Update("_SaveEditing_catTipoGasto", "GestionContable")
                .Delete("_DeleteEditing_catTipoGasto", "GestionContable");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("GestionContable", "Modificar") }); ;
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("GestionContable", "Eliminar") }); ;
            }).Width("10%").Title("Acciones");
            columns.Bound(c => c.tgId).Width("10%").Title("Código").Visible(true);
            columns.Bound(c => c.tgDescripcion).Width("40%").Title("Descripción").Visible(true);
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
<% }
    else
    {
        Html.RenderPartial("Mensaje", new ParametrosMensaje() { Nombre = "msgAlerta_catTipoDeGasto", TipoMensaje = "error", MostrarEmergente = true, Titulo = "¡Atención!", Mensaje = "Usted no tiene asignada una Institución contable, no puede gestionar tipos de gastos.", Visible = true});
        %>
<%
    }
    %>