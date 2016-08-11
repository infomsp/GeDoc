<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var _RowIndex = -1;
    var _DatosRegistro;

    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
    }
    function onCommand(e) {
        if (("cmdEditar, cmdEliminar, cmdActivar").indexOf(e.name) >= 0) {
            if (_RowIndex < 0) {
                jAlert('Debe seleccionar un Consultorio.', 'Error...');
                return;
            }
        }
        switch (e.name) {
            case "cmdAgregar":
                var grid = $(this).data("tGrid");
                grid.addRow();
                break;
            case "cmdEditar":
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminar":
                if (!_DatosRegistro.cscActivo) {
                    jAlert('Este Consultorio ya se encuentra Inactivo.', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar el consultorio "' + _DatosRegistro.cscNombre + '"?', "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdActivar":
                if (_DatosRegistro.cscActivo) {
                    jAlert('Este Consultorio ya se encuentra Activo.', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Activar el consultorio "' + _DatosRegistro.cscNombre + '"?', "Activar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('catCentroDeSaludConsultorio/_ActivaRegistro'), { id: _DatosRegistro.cscId }, function (data) {
                            if (data) {
                                $("#Grid").data("tGrid").rebind();
                            }
                        });
                    }
                });
                break;
        }
    }
    function onComplete(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
    function onRowDataBound(e) {
        if (_RowIndex > -1) {
            var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
        }
    }
</script>
<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.catCentrosDeSaludConsultorios>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.cscId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludConsultorio", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludConsultorio", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdEliminar").ButtonType(GridButtonType.ImageAndText).Text("Eliminar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludConsultorio", "Eliminar") });
            commands.Custom().Ajax(true).Name("cmdActivar").ButtonType(GridButtonType.ImageAndText).Text("Activar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludConsultorio", "Activar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catCentroDeSaludConsultorio")
                .Insert("_InsertEditing", "catCentroDeSaludConsultorio")
                .Update("_SaveEditing", "catCentroDeSaludConsultorio")
                .Delete("_DeleteEditing", "catCentroDeSaludConsultorio");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.cscActivo).Width("8%").Title("").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Inactivo' height='22px' width='22px' style='vertical-align:middle; visibility: <#= !cscActivo ? \"visible\" : \"hidden\" #>' /></div>");
            columns.Bound(c => c.cscNombre).Width("95%").Title("Consultorio").Visible(true)
            .ClientTemplate("<label title='<#= cscNombre #>' style='cursor: pointer;' id='txtcscNombre' ><#= cscNombre #></label>");
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit").OnRowSelected("onRowSelected").OnCommand("onCommand").OnComplete("onComplete").OnRowDataBound("onRowDataBound"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 99.8%;" })

%>
</div>


<!--
    <style type="text/css" xml:lang="es-AR">
    .t-edit-form-container {
            width: 850px;
        }
    </style>
-->