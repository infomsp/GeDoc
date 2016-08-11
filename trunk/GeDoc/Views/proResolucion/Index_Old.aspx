<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:content contentplaceholderid="MainContent" runat="server">
<div id="GrillaPrincipal">
<input type="hidden" id="Inicio" value="false" />
<% ViewData["AltoEditor"] = "600px"; %>
<%= Html.Telerik().Grid<GeDoc.proResoluciones>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.resId);
        })
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            //.Action("AgregarRegistro", "proResolucion")
            commands.Custom()
                .Ajax(true)
                .Name("cmdAgregar")
                .ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "background-image: url('/Content/General/CRUD/add.png'); background-position: center center; background-repeat: no-repeat; margin-left:0" });
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
                    {%>
                    <div id="resultResoluciones"></div>
                    <p>
                        <button onclick="onAceptar()" class="t-button" style="width: 25px; height: 25px;">
                            <img src="../../Content/General/CRUD/aceptar.png" style="vertical-align: text-bottom; margin-left: -1px; margin-top: 2px;" />
                        </button>
                        <button onclick="onCancelar()" class="t-button" style="width: 25px; height: 25px;" >
                            <img src="../../Content/General/CRUD/cancelar.png" style="vertical-align: text-top; margin-left: -2px;" />
                        </button>
                    </p>
                 <% })
        .Render(); %>
</div>
<script type="text/javascript">
    function onCommand(e) {
        var _Window = $("#wEditar").data("tWindow");
        if (e.name == "cmdAgregar") {
            $('#resultResoluciones').load('<%= Url.Action("CRUD", "proResolucion") %>');

            _Window.title("Agregar").center().open();
        }
    }
    function onAceptar() {
        var _Window = $("#wEditar").data("tWindow");
        debugger;
        //alert($("#Model").data("tModel").resConsiderando);

        $.post("/proResolucion/_InsertEditing");

        _Window.close();
    }
    function onCancelar() {
        var _Window = $("#wEditar").data("tWindow");

        _Window.close();
    }
</script>
</asp:content>

<asp:content contentplaceholderid="HeadContent" runat="server">

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
