<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%@ Import Namespace="GeDoc.Models" %>

<% ViewData["AltoEditor"] = "200px"; %>
<%
    var InstitucionContable = "";
    if ((Session["Usuario"] as sisUsuario).icId != null)
    {
        InstitucionContable = " - " + (Session["Usuario"] as sisUsuario).catInstitucionContable.icDescripcion;
%>
<script>
    $("#divTituloCatalogos").text($("#divTituloCatalogos").text() + "<%=InstitucionContable %>")

    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    function onDataBound() {
        var grid = $(this).data('tGrid');
        _CurrentPage = grid.currentPage;
        _OrderBy = (grid.orderBy || '~');
        _FilterBy = (grid.filterBy || '~');

        var _Boton = $('a.t-button.t-grid-cmdExportar');
        var href = _Boton.attr('href');

        href = href.replace(/page=([^&]*)/, 'page=' + _CurrentPage);
        href = href.replace(/orderBy=([^&]*)/, 'orderBy=' + (_OrderBy || '~'));
        href = href.replace(/filter=(.*)/, 'filter=' + (_FilterBy || '~'));
        _Boton.attr('href', href);
    }
</script>

<%= Html.Telerik().Grid<GeDoc.catCreditosAnuales>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.creId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCreditoAnual", "Agregar") });
            commands.Custom().Action("Exportar", "catCreditoAnual", new { page = 1, orderBy = "~", filter = "~" }).Name("cmdExportar").ButtonType(GridButtonType.Image).Text("Exportar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -240px;" })
                .HtmlAttributes(new { title = "Exportar a Excel el listado de Créditos Anuales" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catCreditoAnual")
                .Insert("_InsertEditing", "catCreditoAnual")
                .Update("_SaveEditing", "catCreditoAnual")
                .Delete("_DeleteEditing", "catCreditoAnual");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCreditoAnual", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCreditoAnual", "Eliminar") });
            }).Width("8%").Title("Acciones");
            columns.Bound(c => c.fteDescripcion).Width("20%").Title("Fuente").Visible(true);
            columns.Bound(c => c.ccDescripcion).Width("20%").Title("Cuenta").Visible(true);
            columns.Bound(c => c.partNombre).Width("20%").Title("Partida").Visible(true);
            columns.Bound(c => c.creResolucion).Visible(true).Width("10%").Title("Resolución")
            .ClientTemplate("<label id='creResolucionTexto' ><#= creResolucion == null ? '' : 'SHF-' + creResolucion.toString() + '-' + (((creFecha.getYear() < 1000 ? 1900 : 0) + creFecha.getYear())) #> </label>");
            columns.Bound(c => c.creFecha).Width("10%").Title("Fecha").Visible(true);
            columns.Bound(c => c.creImporte).Width("12%").Title("Importe").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.creObservaciones).Width("200px").Title("Observaciones").Visible(true)
            .ClientTemplate("<label title='<#= creObservaciones #>' style='cursor: pointer;' ><#= creObservaciones #></label>");
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
            .Resizable(resizing => resizing.Columns(true))
%>
<% }
    else
    {
        Html.RenderPartial("Mensaje", new ParametrosMensaje() { Nombre = "msgAlerta_catCreditoAnual", TipoMensaje = "error", MostrarEmergente = true, Titulo = "¡Atención!", Mensaje = "Usted no tiene asignada una Institución contable, no puede gestionar créditos anuales.", Visible = true});
        %>
<%
    }
    %>