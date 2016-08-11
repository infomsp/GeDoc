<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="GeDoc" %>
<% Acciones permisosAcciones = Session["Permisos"] as Acciones; %>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>


<script type="text/javascript">

    function Grid_OnRowDelete(eventData) {
        eventData.preventDefault();
        jConfirm("¿Realmente desea eliminar este motivo?\n(Si el motivo está en uso, no podrá ser eliminado)", "Confirmar", function (e) {
            if (!e) return;
            var _tmbId = eventData.dataItem.tmbId;
            $.post("<%= Url.Content("~/catTurnoMotivoBloqueo/isMotiveInUse") %>", { tmbId: _tmbId }, function (d) {
                if (d) {
                    jAlert("El motivo no puede ser eliminado si ya está en uso.", "¡Atención!");
                } else {
                    var g = $(eventData.currentTarget).data("tGrid");
                    g.deleteRow(eventData.dataItem);
                }
            });
        });
    }

</script>
<%
    
    Html.Telerik().Grid<GeDoc.catTurnoMotivoBloqueos>()
           .Name("Grid")
           .DataKeys(keys =>
           {
               keys.Add(p => p.tmbId);
           })
            .Localizable("es-AR")
           .ToolBar(commands =>
           {
               commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0;" }).HtmlAttributes(new { style = "display: " + permisosAcciones.Visibilidad("catTurnoMotivoBloqueo", "Agregar") });                  
           })
           .DataBinding(dataBinding =>
           {
               dataBinding.Ajax()
                   .Select("_SelectEditing", "catTurnoMotivoBloqueo")
                   .Insert("_InsertEditing", "catTurnoMotivoBloqueo")
                   .Update("_SaveEditing", "catTurnoMotivoBloqueo")
                   .Delete("_DeleteEditing", "catTurnoMotivoBloqueo");
           })
           .Columns(columns =>
           {
               columns.Command(commands =>
               {
                   commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + permisosAcciones.Visibilidad("catTurnoMotivoBloqueo", "Modificar") });
                   commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + permisosAcciones.Visibilidad("catTurnoMotivoBloqueo", "Eliminar") });
               }).Width(5).Title("Acciones");
               columns.Bound(c => c.tmbDescripcion).Width(80).Title("Descripción").Visible(true);

           })
            .Editable(editing => editing
                    .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable(p => p.Enabled(true).Total(96).PageSize(15))
            .ClientEvents(events => events.OnDelete("Grid_OnRowDelete"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .Render();
    
%>
