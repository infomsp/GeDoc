<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" UICulture="es-AR" %>

<asp:content contentplaceholderid="MainContent" runat="server">
<%--<% if (Session["Usuario"] == null)
   {
       Html.Action("LogOff", "Account");
       return;
   } %>--%>
<% ViewData["AltoEditor"] = "200px"; %>
<%= Html.Telerik().Grid<GeDoc.proResoluciones>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.resId);
        })
         .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Agregar") });
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
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Eliminar") });
                commands.Custom("cmdverPDF")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Examinar") });
                commands.Custom("cmdCRUDExpedientes")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -33px -289px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Expedientes") });
                commands.Custom("cmdCRUDEmpleados")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -336px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proResolucion", "Alertas") });
            }).Width("190px").Title("Acciones");
            columns.Bound(c => c.tnlNombre).Width("120px").Title("Tipo").Visible(true);
            columns.Bound(c => c.resNumero).Width("80px").Title("Número").Visible(true);
            columns.Bound(c => c.resFecha).Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.resConsiderando).Width("420px").Title("Detalle").Encoded(false).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
        })
                .Editable(editing => editing
                        .DisplayDeleteConfirmation(true).Mode(GridEditMode.PopUp))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(events => events.OnCommand("onCommand").OnEdit("onCommandEdit"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .RowAction(row => row.Selected = row.DataItem.resId.Equals(ViewData["idResolucion"]))
%>

<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("verPDF")
        .Title("Resoluciones")
        .Visible(false)
        .Content(() => 
        {%>  
            <iframe id="framePDF" src='' width="100%" height="100%" >
            </iframe>
        <%})
        .Buttons(b => b.Maximize().Close())
        .Draggable(true)
        .Scrollable(false)
        .Resizable()
        .Width(870)
        .Height(460)
        .Render();
%>
<!-- Asignación Expedientes -->
<% Html.Telerik().Window()
        .Name("CRUDExpedientes")
        .Title("Asignación de Expedientes")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <%= Html.Telerik().Grid((IEnumerable<GeDoc.proResolucionesExpedientes>)ViewData["Expedientes"])
            .Name("gridExpedientes")
            .DataKeys(keys =>
            {
                keys.Add(p => p.reseId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingExpedientes", "proResolucion")
                    .Insert("_InsertEditingExpedientes", "proResolucion")
                    .Update("_SaveEditingExpedientes", "proResolucion")
                    .Delete("_DeleteEditingExpedientes", "proResolucion");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width(20).Title("Acciones");
                columns.Bound(c => c.zonNombre).Width(80).Title("Zona").Visible(true);
                columns.Bound(c => c.zonCodigo).Width(30).Title("Código").Visible(true);
                columns.Bound(c => c.reseExpedienteNumero).Width(30).Title("Número").Visible(true);
                columns.Bound(c => c.reseExpedienteAnio).Width(30).Title("Año").Encoded(false).Visible(true);
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingExpedientes", "proResolucion", new { idResolucion = 1 }))
            .KeyboardNavigation(keyNav => keyNav.Enabled(true))
            .Editable(editing => editing
                            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding").OnSave("onSaveExpedientes"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(770)
       .Height((int)Session["AlturaGrilla"] + 100)
       .Render();
%>
<!-- Empleados -->
<% Html.Telerik().Window()
        .Name("CRUDEmpleados")
        .Title("Notificaciones a Empleados")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <%= Html.Telerik().Grid((IEnumerable<GeDoc.proResolucionesNotificacionEmpleado>)ViewData["Empleados"])
            .Name("gridEmpleados")
            .DataKeys(keys =>
            {
                keys.Add(p => p.resneId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingEmpleados", "proResolucion")
                    .Update("_SelectEditingEmpleados", "proResolucion")
                    .Insert("_InsertEditingEmpleados", "proResolucion")
                    .Delete("_DeleteEditingEmpleados", "proResolucion");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);//.HtmlAttributes(new { style = "visibility: hidden" });
                    commands.Delete().ButtonType(GridButtonType.Image);//.HtmlAttributes(new { style = "margin-left: -28px" });
                }).Width(20).Title("Acciones");
                columns.Bound(c => c.perNombre).Width(80).Title("Empleado").Visible(true);
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingEmpleados", "proResolucion", new { idResolucion = 1 }))
            .Editable(editing => editing
                            .DisplayDeleteConfirmation(true).Window(wVentana => wVentana.Name("wResoluciones").Title("Probando")))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingEmpleados").OnSave("onSaveEmpleados"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(200))
            .Sortable()
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(570)
       .Height(300)
       .Render();
%>

<script type="text/javascript">
    var idResolucion;
    function onCommand(e) {
        if (e.name == "cmdverPDF") {
            var _Window = $("#verPDF").data("tWindow");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/Resoluciones/' + e.dataItem['resLinkArchivo'].toString()));

            $.post(GetPathApp('proResolucion/RegistrarLog'), { pAccion: "Examinar" }, function (data) {
                if (data) {
                    
                }
            });

            _Window.title("Ver Resolución N° " + e.dataItem['resNumero'].toString() + "...").center().open();
        }
        if (e.name == "cmdCRUDExpedientes") {
            var expGrid = $('#gridExpedientes').data('tGrid');
            var _WindowExp = $("#CRUDExpedientes").data("tWindow");

            idResolucion = e.dataItem['resId'];
            expGrid.rebind();
            _WindowExp.center().open();
        }
        if (e.name == "cmdCRUDEmpleados") {
            var empGrid = $('#gridEmpleados').data('tGrid');
            var _WindowEmp = $("#CRUDEmpleados").data("tWindow");

            idResolucion = e.dataItem['resId'];
            empGrid.rebind();
            _WindowEmp.center().open();
        }
    }
    function onDataBinding(e) {
        e.data = $.extend(e.data, { idResolucion: idResolucion });
    }
    function onDataBindingEmpleados(e) {
        e.data = $.extend(e.data, { idResolucion: idResolucion });
    }
    function onSaveExpedientes(e) {
        var values = e.values;
        
        values.resId = idResolucion;
    }
    function onSaveEmpleados(e) {
        var values = e.values;

        values.resId = idResolucion;
    }
    function onCommandEdit(e) {
//        var dataItem = e.dataItem;
//        var mode = e.mode;
//        var form = e.form;
//        var _Window = $("#wResoluciones").data("tWindow");

//        _Window.center().open();
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
    }
    
    .t-edit-form
    {
        width: 100%;
        height: 100%;
        margin: 0px;
        margin-top: 0px;
        margin-left: -80px;
    }
    
    .t-edit-form-container .editor-label,
    .t-edit-form-container .editor-field
    {
        /*padding-bottom: 1em;*/
        /*float: center;*/
    }
    
    .t-edit-form-container .editor-label
    {
        /*width: 90%;*/
        /*text-align: left;*/
        /*padding-right: 3%;*/
        /*clear: right;*/
    }
    
    .t-edit-form-container .editor-field
    {
        /*width: 90%;*/
    }
</style>
</asp:content>
