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
<%= Html.Telerik().Grid<GeDoc.catFondosPermanentes>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.fpId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFondoPermanente", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catFondoPermanente")
                .Insert("_InsertEditing", "catFondoPermanente")
                .Update("_SaveEditing", "catFondoPermanente")
                .Delete("_DeleteEditing", "catFondoPermanente");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFondoPermanente", "Modificar") }); ;
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFondoPermanente", "Eliminar") }); ;
            }).Width(25).Title("Acciones");
            columns.Bound(c => c.bcoRazonSocial).Width(60).Title("Banco").Visible(true);
            columns.Bound(c => c.fpNumeroCuenta).Width(60).Title("Número de Cuenta").Visible(true);
            columns.Bound(c => c.fpDescripcion).Width(80).Title("Descripción").Visible(true);
            columns.Bound(c => c.fpCBU).Width(60).Title("CBU").Visible(true);
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
        Html.RenderPartial("Mensaje", new ParametrosMensaje() { Nombre = "msgAlerta_catFondoPermanente", TipoMensaje = "error", MostrarEmergente = true, Titulo = "¡Atención!", Mensaje = "Usted no tiene asignada una Institución contable, no puede gestionar fondos permanentes.", Visible = true});
        %>
<%
    }
    %>