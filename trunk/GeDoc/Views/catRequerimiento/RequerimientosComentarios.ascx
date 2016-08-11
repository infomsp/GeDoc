<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var _WindowCRUD;
    function onDataBindingComentarios(e) {
        var reqId = -1;
        if (_DatosRegistro_catRequerimiento != null) {
            reqId = _DatosRegistro_catRequerimiento.reqId;
        }

        e.data = $.extend(e.data, { reqId: reqId });
    }
    function onSaveComentarios(e) {
        debugger;
        var values = e.values;

        values.reqId = _DatosRegistro_catRequerimiento.reqId;
    }
    function onCompleteComentarios(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            $("#Grid").data("tGrid").ajaxRequest();
        }
    }
    
    function onCommandEditComentarios(e) {
        _WindowCRUD = $("#gridComentariosPopUp").data("tWindow");
        onCommandEdit(e);
    }

    function onCommandComentarios(e) {
        switch (e.name) {
            case "cmdDeleteComentario":
                var grid = $(this).data("tGrid");
                var tr = $("#gridComentarios tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                jConfirm("¿Confirma Eliminar el Comentario?", "Eliminar...", function (r) {
                    if (r) {
                        //AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdModificarComentario":
                var grid = $(this).data("tGrid");
                var tr = $("#gridComentarios tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
        }
    }

</script>
<% ViewData["AltoEditor"] = "200px"; %>
<% var ComentariosWS = new List<catRequerimientoComentariosWS>(); %>
<!-- Comentarios -->
<% Html.Telerik().Window()
        .Name("wComentarios")
        .Title("Comentarios")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid(ComentariosWS)
            .Name("gridComentarios")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.reqcId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0", title = "Agregar Comentario" })%>
                        <label id="lblRequerimiento" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingComentarios", "catRequerimiento", new { reqId = -1})
                    .Update("_SaveEditingComentarios", "catRequerimiento")
                    .Insert("_InsertEditingComentarios", "catRequerimiento")
                    .Delete("_DeleteEditingComentarios", "catRequerimiento");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Custom("cmdModificarComentario")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0 -336px;" })
                        .HtmlAttributes(new { title = "Modificar Comentario" })
                        .SendState(false);
                    commands.Custom("cmdDeleteComentario")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
                        .HtmlAttributes(new { title = "Eliminar Comentario" })
                        .SendState(false);
                }).Width("80px").Title("Acciones");
            columns.Bound(c => c.reqcFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.reqcFecha).Format("{0:hh:mm:ss}").Width("80px").Title("Hora").Visible(true);
            columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("150px").Title("Comentado por").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Usuario.usrApellidoyNombre #></label>");
            columns.Bound(c => c.reqcComentario).Width("350px").Title("Comentario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= reqcComentario #>' style='cursor: pointer;' ><#= reqcComentario #></label>");
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingComentarios").OnCommand("onCommandComentarios").OnEdit("onCommandEditComentarios").OnSave("onSaveComentarios").OnComplete("onCompleteComentarios"))
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
