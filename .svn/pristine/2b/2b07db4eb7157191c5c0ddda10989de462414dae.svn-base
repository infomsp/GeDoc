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

<%= Html.Telerik().Grid<GeDoc.catSubPartidas>()
        .Name("Grid")
            .DataKeys(keys =>
            {
                keys.Add(p => p.subpId);
            })
             .Localizable("es-AR")
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" }).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catSubPartida", "Agregar") });
            })
        .Localizable("es-AR")
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catSubPartida")
                .Insert("_InsertEditing", "catSubPartida")
                .Update("_SaveEditing", "catSubPartida")
                .Delete("_DeleteEditing", "catSubPartida");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catSubPartida", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catSubPartida", "Eliminar") });
            }).Width("60px").Title("Acciones");
            columns.Bound(c => c.partNombre).Width("100px").Title("Partida").Visible(true);
            columns.Bound(c => c.subpCodigo).Width("50px").Title("Código").Visible(true);
            columns.Bound(c => c.subpNombre).Width("300px").Title("Nombre").Visible(true);
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
        Html.RenderPartial("Mensaje", new ParametrosMensaje() { Nombre = "msgAlerta_catSubPartida", TipoMensaje = "error", MostrarEmergente = true, Titulo = "¡Atención!", Mensaje = "Usted no tiene asignada una Institución contable, no puede gestionar sub partidas.", Visible = true});
        %>
<%
    }
    %>