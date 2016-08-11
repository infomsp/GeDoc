<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var _WindowCRUD;
    function onDataBindingAdjunto(e) {
        var reqId = -1;
        if (_DatosRegistro_catRequerimiento != null) {
            reqId = _DatosRegistro_catRequerimiento.reqId;
        }

        e.data = $.extend(e.data, { reqId: reqId });
    }
    function onSaveAdjunto(e) {
        debugger;
        var values = e.values;

        values.reqId = _DatosRegistro_catRequerimiento.reqId;
    }
    function onCompleteAdjunto(e) {
        debugger;
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            $("#Grid").data("tGrid").ajaxRequest();
        }
    }
    
    function onCommandEditAdjunto(e) {
        _WindowCRUD = $("#gridAdjuntoPopUp").data("tWindow");
        onCommandEdit(e);
    }

    function onCommandAdjunto(e) {
        switch (e.name) {
            case "cmdDeleteAdjunto":
                var grid = $(this).data("tGrid");
                var tr = $("#gridAdjunto tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                jConfirm("¿Confirma Eliminar el Archivo adjunto?", "Eliminar...", function (r) {
                    if (r) {
                        //AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdModificarAdjunto":
                var grid = $(this).data("tGrid");
                var tr = $("#gridAdjunto tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
        }
    }

    function Descargar(e) {
        var _Post = GetPathApp('catRequerimiento/Descargar');
        alert("Descargar el archivo " + e);
        $.post(_Post, { Archivo: e });
    }

</script>
<% ViewData["AltoEditor"] = "200px"; %>
<% var AdjuntoWS = new List<catRequerimientoAdjuntoWS>(); %>
<!-- Adjunto -->
<% Html.Telerik().Window()
        .Name("wAdjunto")
        .Title("Adjunto")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid(AdjuntoWS)
            .Name("gridAdjunto")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.reqaId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0", title = "Agregar archivo adjunto" })%>
                        <label id="lblRequerimiento" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingAdjunto", "catRequerimiento", new { reqId = -1})
                    .Update("_SaveEditingAdjunto", "catRequerimiento")
                    .Insert("_InsertEditingAdjunto", "catRequerimiento")
                    .Delete("_DeleteEditingAdjunto", "catRequerimiento");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Custom("cmdModificarAdjunto")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0 -336px;" })
                        .HtmlAttributes(new { title = "Modificar observaciones del archivo adjunto" })
                        .SendState(false);
                    commands.Custom("cmdDeleteAdjunto")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
                        .HtmlAttributes(new { title = "Eliminar archivo adjunto" })
                        .SendState(false);
                }).Width("80px").Title("Acciones");
            columns.Bound(c => c.reqaFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.reqaFecha).Format("{0:hh:mm:ss}").Width("80px").Title("Hora").Visible(true);
            columns.Bound(c => c.reqaArchivo).Width("150px").Title("Archivo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<a href='" + Url.Content("~/") + "catRequerimiento/Descargar?Archivo=<#= reqaArchivo #>' title='Click para descargar archivo' style='cursor: pointer;' ><#= reqaArchivo #></a>");
            columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("150px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Usuario.usrApellidoyNombre #></label>");
            columns.Bound(c => c.reqaObservaciones).Width("350px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= reqaObservaciones #>' style='cursor: pointer;' ><#= reqaObservaciones #></label>");
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingAdjunto").OnCommand("onCommandAdjunto").OnEdit("onCommandEditAdjunto").OnSave("onSaveAdjunto").OnComplete("onCompleteAdjunto"))
            //.Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1024)
       .Height(400)
       .Render();
%>
