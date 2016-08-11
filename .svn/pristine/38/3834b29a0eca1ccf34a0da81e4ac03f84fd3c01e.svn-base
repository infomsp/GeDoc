<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<% ViewData["AltoEditor"] = "200px"; %>
<%= Html.Telerik().Grid<GeDoc.catObrasSociales>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.osId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catObraSocial", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catObraSocial")
                .Insert("_InsertEditing", "catObraSocial")
                .Update("_SaveEditing", "catObraSocial")
                .Delete("_DeleteEditing", "catObraSocial");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catObraSocial", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catObraSocial", "Eliminar") });
            }).Width(22).Title("Acciones");
            columns.Bound(c => c.osCodigo).Width(20).Title("Código").Visible(true);
            columns.Bound(c => c.osDenominacion).Width(80).Title("Razón Social").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= osDenominacion #>' style='cursor: pointer;' id='txtosDenominacion' style='white-space: nowrap;'><#= osDenominacion #> </label>");
            columns.Bound(c => c.osSigla).Width(30).Title("Sigla").Visible(true);
            columns.Bound(c => c.osEsPrepaga).Width(30).Title("Es Prepaga").Visible(true)
            .ClientTemplate("<label style='cursor: pointer;' id='EsPrepaga' ><#= osEsPrepaga ? 'SI' : 'NO' #> </label>");
            columns.Bound(c => c.provNombre).Width(50).Title("Provincia").Visible(true);
            columns.Bound(c => c.osTelefono).Width(50).Title("Teléfono").Visible(true);
            columns.Bound(c => c.osMail).Width(50).Title("Correo Electrónico").Visible(true);
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
<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>


