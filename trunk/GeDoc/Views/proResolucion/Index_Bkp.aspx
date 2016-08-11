<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GeDoc.proResoluciones>" %>

<asp:content contentplaceholderid="MainContent" runat="server">
<% ViewData["AltoEditor"] = "600px"; %>
<%= Html.Telerik().Grid<GeDoc.proResoluciones>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.resId);
        })
        .ToolBar(commands =>
        {
            //commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            commands.Custom()
                .Ajax(true)
                .Name("cmdAgregar")
                .Action("AgregarRegistro", "proResolucion").ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "background-image: url('/Content/General/CRUD/add.png'); background-position: center center; background-repeat: no-repeat; margin-left:0" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "proResolucion")
                .Insert("_InsertEditing", "proResolucion")
                .Update("_SaveEditing", "proResolucion")
                .Delete("_DeleteEditing", "proResolucion");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image);
                commands.Delete().ButtonType(GridButtonType.Image);
            }).Width(22).Title("Acciones");
            columns.Bound(c => c.resNumero).Width(23).Title("Número").Visible(true);
            columns.Bound(c => c.resFecha).Width(80).Title("Fecha").Visible(true);
            columns.Bound(c => c.resConsiderando).Width(80).Title("Considerando").Visible(true);
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .Filterable()
            .Selectable()
            .ClientEvents(events => events.OnCommand("onCommand"))
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
%>

<% Html.Telerik().Window()
        .Name("wEditar")
        .Visible(false)
        .Title(".....")
        .Modal(true)
        .Width(1024)
        .Height(700)
        .Content(() =>
            {
                %>
                <% using (Html.BeginForm()) { %>
                <%= Html.ValidationSummary(false) %>
                <% Html.Telerik().TabStrip()
                       .Name("tabResoluciones")
                       .HtmlAttributes(new { style = "height: 98%; padding: 0px; background: #FFFFFF; border: 0px;" })
                       .Items(tabstrip =>
                        {
                            tabstrip.Add()
                                .Text("General")
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { %>
                                    <div id="EditarGeneral" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                                        <div class="editor-label">
                                            <%: Html.LabelFor(model => model.resNumero) %>
                                            <%: Html.EditorFor(model => model.resNumero) %>
                                            <%: Html.ValidationMessageFor(model => model.resNumero) %>
                                        </div>
                                        <div class="editor-label">
                                            <%: Html.LabelFor(model => model.resFecha) %>
                                            <%: Html.EditorFor(model => model.resFecha)%>
                                            <%: Html.ValidationMessageFor(model => model.resFecha)%>
                                        </div>
                                        <div class="editor-label">
                                            <%: Html.LabelFor(model => model.resEsImportante) %>
                                            <%: Html.EditorFor(model => model.resEsImportante)%>
                                            <%: Html.ValidationMessageFor(model => model.resEsImportante)%>
                                        </div>
                                        <div class="editor-label">
                                            <%: Html.LabelFor(model => model.resNotificacionVencimiento) %>
                                            <%: Html.EditorFor(model => model.resNotificacionVencimiento)%>
                                            <%: Html.ValidationMessageFor(model => model.resNotificacionVencimiento)%>
                                        </div>
                                        <div class="editor-label">
                                            <%: Html.LabelFor(model => model.resNotificacionDiaInicio) %>
                                            <%: Html.EditorFor(model => model.resNotificacionDiaInicio)%>
                                            <%: Html.ValidationMessageFor(model => model.resNotificacionDiaInicio)%>
                                        </div>
                                        <div class="editor-label">
                                            <%: Html.LabelFor(model => model.resLinkArchivo) %>
                                            <%: Html.EditorFor(model => model.resLinkArchivo)%>
                                            <%: Html.ValidationMessageFor(model => model.resLinkArchivo)%>
                                        </div>
                                    </div>
                                <%});
                            tabstrip.Add()
                                .Text("Considerando")
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { %>
                                    <div id="EditarConsiderando" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                                        <div class="editor-label">
                                            <%: Html.EditorFor(model => model.resConsiderando) %>
                                            <%: Html.ValidationMessageFor(model => model.resConsiderando) %>
                                        </div>
                                    </div>
                                <%});
                            tabstrip.Add()
                                .Text("Resuelve")
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { %>
                                    <div id="EditarResuelve" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                                        <div class="editor-label">
                                            <%: Html.EditorFor(model => model.resResuelve)%>
                                            <%: Html.ValidationMessageFor(model => model.resResuelve)%>
                                        </div>
                                    </div>
                                <%});
                        })
                    .SelectedIndex(0)
                    .Render();%>

                    <% using (Html.BeginForm()) { %>
                    <p>
                        <button onclick="openWindow()" class="t-button">Open</button> /
                        <button onclick="onCancelar()" class="t-button">Close</button>
                    </p>
                    <% } %>

                    <% }  %>
                <% })
        .Render(); %>
<script type="text/javascript">
    function onCommand(e) {
        var _Window = $("#wEditar").data("tWindow");
        if (e.name == "cmdAgregar") {

            _Window.title("Agregar").center().open();
        }
    }
    function onCancelar() {
        var _Window = $("#wEditar").data("tWindow");
        _Window.close();
    }
</script>

</asp:content>

<asp:content contentplaceholderid="HeadContent" runat="server">
<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>

<style type="text/css" xml:lang="es-AR">
    .field-validation-error
    {
        position: absolute;
        display: block;
    }
    
    * html .field-validation-error { position: relative; }
    *+html .field-validation-error { position: relative; }
    
    .field-validation-error span
    {
        position: absolute;
        white-space: nowrap;
        color: red;
        padding: 0px 5px 0px 5px;
        background: no-repeat 0 0;
        vertical-align: middle;
        font-size: small;
        text-decoration: blink;
    }
    
    /* in-form editing */
    .t-edit-form-container
    {
        width: 850px;
        margin: 1em;
    }
    
    .t-edit-form-container .editor-label,
    .t-edit-form-container .editor-field
    {
        padding-bottom: 1em;
        float: center;
    }
    
    .t-edit-form-container .editor-label
    {
        width: 90%;
        text-align: left;
        /*padding-right: 3%;*/
        clear: right;
    }
    
    .t-edit-form-container .editor-field
    {
        width: 90%;
    }
</style>
</asp:content>
