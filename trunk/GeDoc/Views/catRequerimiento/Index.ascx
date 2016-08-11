<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%@ Import Namespace="Telerik.Web.Mvc.UI.Html" %>

<script type="text/javascript">
    var _RowIndex_catRequerimiento = -1;
    var _DatosRegistro_catRequerimiento;
    var _Estado = '';
    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex_catRequerimiento = e.row.rowIndex;
        _DatosRegistro_catRequerimiento = dataItem;
    }
    
    function onCommand(e) {
        switch (e.name) {
            case "cmdComentarios":
                _DatosRegistro_catRequerimiento = e.dataItem;
                $("#gridComentarios").data("tGrid").rebind();

                var windowElement = $('#wComentarios').data('tWindow');
                windowElement.title("Comentarios para el Requerimiento número " + _DatosRegistro_catRequerimiento.reqId.toString());
                windowElement.center();
                windowElement.open();
                break;
            case "cmdLogWork":
                _DatosRegistro_catRequerimiento = e.dataItem;
                $("#gridLogWork").data("tGrid").rebind();

                var windowElement = $('#wLogWork').data('tWindow');
                windowElement.title("Trabajos realizados en el Requerimiento número " + _DatosRegistro_catRequerimiento.reqId.toString());
                windowElement.center();
                windowElement.open();
                break;
            case "cmdAdjuntos":
                _DatosRegistro_catRequerimiento = e.dataItem;
                $("#gridAdjunto").data("tGrid").rebind();

                var windowElement = $('#wAdjunto').data('tWindow');
                windowElement.title("Archivos adjuntos al Requerimiento número " + _DatosRegistro_catRequerimiento.reqId.toString());
                windowElement.center();
                windowElement.open();
                break;
        }
    }

    function onCommandRequerimiento(e) {
        if (("Modificar, CambiarEstado, Notificados").indexOf(e) >= 0) {
            if (_RowIndex_catRequerimiento < 0) {
                jAlert('Debe seleccionar un Requerimiento.', 'Error...');
                return;
            }
        }
        switch (e) {
            case "Agregar":
                var grid = $('#Grid').data("tGrid");
                grid.addRow();
                break;
            case "Modificar":
                if (_DatosRegistro_catRequerimiento.NoEditar) {
                    jAlert('No puede modificar un requerimiento "Anulado", "Terminado", "En Proceso" o "Implementado".', 'Error...');
                    return;
                }
                grid = $('#Grid').data("tGrid");
                tr = $("#Grid tbody tr:eq(" + (_RowIndex_catRequerimiento + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "CambiarEstado":
                if (_DatosRegistro_catRequerimiento.NoCambiarEstado) {
                    jAlert('No se puede cambiar el estado de un requerimiento "Anulado", "Terminado" o "Implementado".', 'Error...');
                    return;
                }

                var _Window = $('#wCambioDeEstado').data("tWindow");
                _Window.center().open();
                break;
            case "Notificados":
                $("#gridNotifica").data("tGrid").rebind();
                
                var _Window = $('#wNotificados').data("tWindow");
                _Window.center().open();
                break;
        }
    }
    
    function onComplete(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
    function onRowDataBound(e) {
        if (e.dataItem != null) {
            if (e.dataItem.CantidadComentarios > 0) {
                e.row.innerHTML = e.row.innerHTML.replace("Comentario.png", "ConComentario.png");
                e.row.innerHTML = e.row.innerHTML.replace("<#Comentario#>", e.dataItem.CantidadComentarios + " Comentarios");
            } else {
                e.row.innerHTML = e.row.innerHTML.replace("<#Comentario#>", "Sin comentarios");
            }
            if (e.dataItem.CantidadAdjuntos > 0) {
                e.row.innerHTML = e.row.innerHTML.replace("Adjuntar.png", "ConAdjuntar.png");
                e.row.innerHTML = e.row.innerHTML.replace("<#Adjunto#>", e.dataItem.CantidadAdjuntos + " archivos adjuntos");
            } else {
                e.row.innerHTML = e.row.innerHTML.replace("<#Adjunto#>", "Sin archivos adjuntos");
            }
            if (e.dataItem.CantidadLogWork > 0) {
                e.row.innerHTML = e.row.innerHTML.replace("LogWork.png", "ConLogWork.png");
                e.row.innerHTML = e.row.innerHTML.replace("<#Trabajo#>", e.dataItem.CantidadLogWork + " items de trabajo realizado");
            } else {
                e.row.innerHTML = e.row.innerHTML.replace("<#Trabajo#>", "Sin trabajo realizado");
            }
        }
        if (_RowIndex_catRequerimiento > -1) {
            var grid = $("#Grid").data("tGrid");
            var tr = $("#Grid tbody tr:eq(" + (_RowIndex_catRequerimiento + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistro_catRequerimiento = grid.dataItem(tr);
        }
    }
    function onSave(e) {
        var values = e.values;
    }
    
    function onComboBoxLoad() {
        $(this).data("tComboBox").fill();
    }

    function onCommandEdit_catRequerimiento(e) {
        if (e.mode == "edit") {
            var _Asignado = $("#usrId").data("tComboBox");
            _Asignado.disable();
        }

        onCommandEdit(e);
    }
    
    function onDataBinding_catRequerimiento(args) {
        var _SoloYo = $("#chbSoloYo").is(":checked");
        var _SoloPendientes = $("#chbTodos").is(":checked");

        AbrirWaiting();
        args.data = $.extend(args.data, { SoloMisRequerimientos: _SoloYo, SoloPendientes: _SoloPendientes });
    }

    function Refrescar() {
        $("#Grid").data("tGrid").ajaxRequest();
    }

</script>
<% ViewData["FiltroContains"] = true; %>
<% ViewData["StyleAutoComplete"] = "width: 250px;"; %>
<% string _PathContent = Url.Content("~/Content"); %>
<% Html.RenderPartial("RequerimientosComentarios"); %>
<% Html.RenderPartial("RequerimientosLogWork"); %>
<% Html.RenderPartial("RequerimientosAdjunto"); %>
<% Html.RenderPartial("CambiaEstado"); %>
<% Html.RenderPartial("RequerimientosNotificados"); %>
<div style="overflow: hidden; height: 510px;" >
<% Html.Telerik().Grid<GeDoc.catRequerimientoWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.reqId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar_catRequerimiento").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catRequerimiento", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar_catRequerimiento").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catRequerimiento", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdHistorial_catRequerimiento").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -240px;" })
                .Text("Historial")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catRequerimiento", "Estado") });
            commands.Template(temp =>
                {
                %>
                    <div class="t-button" onclick="onCommandRequerimiento('Agregar');" style = "display: <%= (Session["Permisos"] as Acciones).Visibilidad("catRequerimiento", "Agregar") %>" >
                        <span class="t-icon" style="background: url('<%= _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] %>/sprite.png') no-repeat -49px -321px;"></span>
                        Agregar
                    </div>
                    <div class="t-button" onclick="onCommandRequerimiento('Modificar');" style = "display: <%= (Session["Permisos"] as Acciones).Visibilidad("catRequerimiento", "Modificar") %>" >
                        <span class="t-icon" style="background: url('<%= _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] %>/sprite.png') no-repeat 0px -336px;"></span>
                        Modificar
                    </div>
                    <div class="t-button" onclick="onCommandRequerimiento('CambiarEstado');" style = "display: <%= (Session["Permisos"] as Acciones).Visibilidad("catRequerimiento", "Cambiar Estado") %>" >
                        <span class="t-icon" style="background: url('<%= _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] %>/sprite.png') no-repeat -64px -208px;"></span>
                        Cambiar Estado
                    </div>
                    <div class="t-button" onclick="onCommandRequerimiento('Notificados');" style = "display: <%= (Session["Permisos"] as Acciones).Visibilidad("catRequerimiento", "Cambiar Estado") %>" title="Administrar Notificaciones" >
                        <img src="<%= _PathContent + "/General/CRUD" %>/e-mail.png" width="18" height="14" style="vertical-align: middle; margin-bottom: 2px;" />
                        Notificar
                    </div>
                    <span style="display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catRequerimiento", "Mis Requerimientos") %>">
                        <input id="chbSoloYo" type="checkbox" onclick="Refrescar();" checked="checked" style="margin-left: 5px; vertical-align: middle;"/>
                        Mis Requerimientos
                    </span>
                    <span>
                        <input id="chbTodos" type="checkbox" onclick="Refrescar();" style="margin-left: 25px; vertical-align: middle;" checked="checked" />
                        Solo Pendientes
                    </span>
                <%
                });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catRequerimiento", new { SoloMisRequerimientos = true, SoloPendientes = true })
                .Insert("_InsertEditing", "catRequerimiento")
                .Delete("_DeleteEditing", "catRequerimiento")
                .Update("_SaveEditing", "catRequerimiento");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Custom("cmdComentarios")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(false)
                    .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/General/CRUD/Comentario.png') no-repeat 0px 0px;" })
                    .HtmlAttributes(new { title = "<#Comentario#>" })
                    .SendState(false);
                commands.Custom("cmdAdjuntos")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(false)
                    .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/General/CRUD/Adjuntar.png') no-repeat 0px 0px;" })
                    .HtmlAttributes(new { title = "<#Adjunto#>" })
                    .SendState(false);
                commands.Custom("cmdLogWork")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(false)
                    .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/General/CRUD/LogWork.png') no-repeat 0px 0px;" })
                    .HtmlAttributes(new { title = "<#Trabajo#>" })
                    .SendState(false);
            }).Width("120px").Title("Acciones");
            columns.Bound(c => c.reqId).Width("50px").Title("Id").Visible(true);
            columns.Bound(c => c.Dependencia).Width("180px").Title("Depende de").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Dependencia #>' style='cursor: pointer;' ><#= Dependencia #></label>");
            columns.Bound(c => c.Tipo.tipoDescripcion).Width("60px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/CRUD/<#= Tipo.tipoImagen #>' title='<#= Tipo.tipoDescripcion #>' height='18px' width='18px' style='vertical-align:middle;' /></div>");
            columns.Bound(c => c.Creado.reqeFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Creado el").Visible(true);
            columns.Bound(c => c.Creado.Usuario.usrApellidoyNombre).Width("150px").Title("Creado por").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Creado.Usuario.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Creado.Usuario.usrApellidoyNombre #></label>");
            columns.Bound(c => c.Estado.Tipo.tipoDescripcion).Width("80px").Title("Estado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/<#= Estado.Tipo.tipoImagen #>' title='<#= Estado.Tipo.tipoDescripcion #>' height='18px' width='18px' style='vertical-align:middle;' /></div>");
            columns.Bound(c => c.Estado.reqeFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Estado Fecha").Visible(true);
            columns.Bound(c => c.TiempoEstimado).Width("100px").Title("Estimado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= TiempoEstimado #>' style='cursor: pointer;' ><#= TiempoEstimado #></label>");
            columns.Bound(c => c.reqAsunto).Width("120px").Title("Asunto").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= reqAsunto #>' style='cursor: pointer;' ><#= reqAsunto #></label>");
            columns.Bound(c => c.Solicitante.perApellidoyNombre).Width("150px").Title("Solicitante").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Solicitante.perApellidoyNombre #>' style='cursor: pointer;' ><#= Solicitante.perApellidoyNombre #></label>");
            columns.Bound(c => c.reqDescripcion).Width("150px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= reqDescripcion #>' style='cursor: pointer;' ><#= reqDescripcion #></label>");
            columns.Bound(c => c.Asignado.usrApellidoyNombre).Width("150px").Title("Responsable").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Asignado.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Asignado.usrApellidoyNombre #></label>");
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit_catRequerimiento").OnRowSelect("onRowSelected").OnCommand("onCommand").OnComplete("onComplete").OnRowDataBound("onRowDataBound").OnSave("onSave").OnDataBinding("onDataBinding_catRequerimiento"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable(o => o.SortMode(GridSortMode.MultipleColumn))
            .HtmlAttributes(new { style = "width: 99.8%;" })
            .Render();
%>
</div>
