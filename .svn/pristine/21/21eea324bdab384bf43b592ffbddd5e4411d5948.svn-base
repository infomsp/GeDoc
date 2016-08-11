<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script>
    function onCommandEditPublicidad() {
        debugger;
    
        var _desde = $("#cspDesde").val();
        jAlert(_desde);
    }
</script>

<%= Html.Telerik().Grid<GeDoc.catCentroDeSaludPublicidades>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.cspId);
        })
       .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludPublicidad", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catCentroDeSaludPublicidad")
                .Insert("_InsertEditing", "catCentroDeSaludPublicidad")
                .Update("_SaveEditing", "catCentroDeSaludPublicidad")
                .Delete("_DeleteEditing", "catCentroDeSaludPublicidad");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludPublicidad", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludPublicidad", "Eliminar") });
            }).Width(35).Title("Acciones");
            columns.Bound(c => c.cspDescripcion).Width(100).Title("Mensaje").Visible(true);
            columns.Bound(c => c.cspDesde).Width(50).Title("Fecha Desde").Visible(true);
            columns.Bound(c => c.horaDesde).Width(50).Title("Hora Desde").Visible(true);
            columns.Bound(c => c.cspHasta).Width(50).Title("Fecha Hasta").Visible(true);
            columns.Bound(c => c.horaHasta).Width(50).Title("Hora Hasta").Visible(true);
            columns.Bound(c => c.csNombre).Width(80).Title("Centro").Visible(true);
        })
            .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
           // .ClientEvents(events => events.OnEdit("onCommandEditPublicidad"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
%>

