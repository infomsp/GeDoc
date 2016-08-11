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
<%= Html.Telerik().Grid<GeDoc.catFuentes>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.fteId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new {style = "margin-left:0"})
                .HtmlAttributes(new {style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFuente", "Agregar")});
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catFuente")
                .Insert("_InsertEditing", "catFuente")
                .Update("_SaveEditing", "catFuente")
                .Delete("_DeleteEditing", "catFuente");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new {style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFuente", "Modificar")});
                ;
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new {style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFuente", "Eliminar")});
                ;
            }).Width(25).Title("Acciones").Visible(true);
            columns.Bound(c => c.fteCodigo).Width(80).Title("Código").Visible(true);
            columns.Bound(c => c.fteDescripcion).Width(80).Title("Descripción").Visible(true);
        })
        .Editable(editing => editing
            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
        .Pageable((paging) =>
            paging.Enabled(true)
                .PageSize(((int) Session["FilasPorPagina"])))
        .ClientEvents(events => events.OnEdit("onCommandEdit"))
        .Footer(true)
        .Filterable()
        .Selectable()
        .Scrollable(scroll => scroll.Enabled(true).Height(((int) Session["AlturaGrilla"])))
        .Sortable()
    %>
<% }
    else
    {
        Html.RenderPartial("Mensaje", new ParametrosMensaje() { Nombre = "msg_catFuente", TipoMensaje = "error", MostrarEmergente = true, Titulo = "¡Atención!", Mensaje = "Usted no tiene asignada una Institución contable, no puede gestionar fuentes de financiamiento.", Visible = true});
        %>
<%
    }
    %>