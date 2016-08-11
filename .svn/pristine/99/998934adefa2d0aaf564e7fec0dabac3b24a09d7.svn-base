<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<% ViewData["FiltroContains"] = true; %>
<% string _PathContent = Url.Content("~/Content"); %>
<script type="text/javascript">
    function onCommandEditEspecialidad(e) {
        debugger;
        if (e.mode == 'edit') {
            $('[name="espNombre"]').attr("readonly", true);
            $('[name="espCodigo"]').attr("readonly", true);
        }

        $('#espAgrup').change(function () {
               // alert("cambia");
                //$('#espIdPadre').visible(false)
            });
            debugger;

            //Invocas el onCommandEdit del CRUD
            //onCommandEdit(e);
       
//        $("#espAgrup").bind('valueChange', function (e) { 
//        alert("cambia");
       
      //  })
        onCommandEdit(e);
    }
</script>
<%= Html.Telerik().Grid<GeDoc.catEspecialidades>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.espId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catEspecialidad", "Agregar") });
        })
        .HtmlAttributes(new { style = "height:100%; width: 100%;" })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catEspecialidad")
                .Insert("_InsertEditing", "catEspecialidad")
                .Update("_SaveEditing", "catEspecialidad")
                .Delete("_DeleteEditing", "catEspecialidad");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catEspecialidad", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catEspecialidad", "Eliminar") });
            }).Width("60px").Title("Acciones");
            columns.Bound(c => c.espCodigo).Width("50px").Title("Código").Visible(true);
            columns.Bound(c => c.espNombre).Width("200px").Title("Nombre").Visible(true);
            columns.Bound(c => c.espNombrePadre).Width("100px").Title("Esp. Padre").Visible(true);
           // columns.Bound(c => c.Contratados).Width("100px").Title("Contratados").Visible(true);
        })
            .Editable(editing => editing
                    .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                       paging.Enabled(true)
                            .PageSize(((int)Session["FilasPorPagina"])))
                    .ClientEvents(events => events.OnEdit("onCommandEditEspecialidad"))
            .Footer(true)
        .Filterable()
        .Selectable()
        .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
        .Sortable()
%>

