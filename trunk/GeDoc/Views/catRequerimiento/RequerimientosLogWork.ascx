<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var _WindowCRUD;
    function onDataBindingLogWork(e) {
        var reqId = -1;
        if (_DatosRegistro_catRequerimiento != null) {
            reqId = _DatosRegistro_catRequerimiento.reqId;
        }

        e.data = $.extend(e.data, { reqId: reqId });
    }
    function onSaveLogWork(e) {
        debugger;
        var values = e.values;

        values.reqId = _DatosRegistro_catRequerimiento.reqId;
    }
    function onCompleteLogWork(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            $("#Grid").data("tGrid").ajaxRequest();
        }
    }
    
    function onCommandEditLogWork(e) {
        _WindowCRUD = $("#gridLogWorkPopUp").data("tWindow");
        onCommandEdit(e);
    }

    function onCommandLogWork(e) {
        switch (e.name) {
            case "cmdDeleteLogWork":
                var grid = $(this).data("tGrid");
                var tr = $("#gridLogWork tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                jConfirm("¿Confirma Eliminar el log del trabajo realizado?", "Eliminar...", function (r) {
                    if (r) {
                        //AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdModificarLogWork":
                var grid = $(this).data("tGrid");
                var tr = $("#gridLogWork tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
        }
    }

</script>
<% ViewData["AltoEditor"] = "200px"; %>
<% var LogWorkWS = new List<catRequerimientoLogWorkWS>(); %>
<!-- LogWork -->
<% Html.Telerik().Window()
        .Name("wLogWork")
        .Title("LogWork")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid(LogWorkWS)
            .Name("gridLogWork")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.reqlId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0", title = "Agregar trabajo realizado" })%>
                        <label id="lblRequerimiento" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingLogWork", "catRequerimiento", new { reqId = -1})
                    .Update("_SaveEditingLogWork", "catRequerimiento")
                    .Insert("_InsertEditingLogWork", "catRequerimiento")
                    .Delete("_DeleteEditingLogWork", "catRequerimiento");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Custom("cmdModificarLogWork")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0 -336px;" })
                        .HtmlAttributes(new { title = "Modificar log del trabajo realizado" })
                        .SendState(false);
                    commands.Custom("cmdDeleteLogWork")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
                        .HtmlAttributes(new { title = "Eliminar log del trabajo realizado" })
                        .SendState(false);
                }).Width("80px").Title("Acciones");
            columns.Bound(c => c.reqlFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.reqlFecha).Format("{0:hh:mm:ss}").Width("80px").Title("Hora").Visible(false);
            columns.Bound(c => c.reqlTiempo).Width("120px").Title("Horas Trabajadas").Visible(true);
            columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("150px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Usuario.usrApellidoyNombre #></label>");
            columns.Bound(c => c.reqlObservaciones).Width("350px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= reqlObservaciones #>' style='cursor: pointer;' ><#= reqlObservaciones #></label>");
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingLogWork").OnCommand("onCommandLogWork").OnEdit("onCommandEditLogWork").OnSave("onSaveLogWork").OnComplete("onCompleteLogWork"))
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
