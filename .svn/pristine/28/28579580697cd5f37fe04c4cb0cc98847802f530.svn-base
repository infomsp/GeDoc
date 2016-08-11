<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%: Url.Content("~/Scripts/CRUDGrillas.js")%>" ></script>
<script type="text/javascript">
    var _RowIndex = -1;
    var _DatosRegistro;
    var _RowIndexHorario = -1;
    var _DatosRegistroHorario;
    var _EsModificar = false;
    function onRowSelected(e) {
        debugger;
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
    }
    function onRowSelectedHorario(e) {
        debugger;
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndexHorario = e.row.rowIndex;
        _DatosRegistroHorario = dataItem;
    }
    function onCommand(e) {
        debugger;
        _EsModificar = false;
        if (("cmdEditar, cmdEliminar, cmdActivar, cmdHorarios").indexOf(e.name) >= 0) {
            if (_RowIndex < 0) {
                jAlert('Debe seleccionar una Agenda.', 'Error...');
                return;
            }
        }
        if (("cmdEditHorario, cmdElimHorario, cmdActivHorario").indexOf(e.name) >= 0) {
            if (_RowIndexHorario < 0) {
                jAlert('Debe seleccionar un Horario.', 'Error...');
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
                if (!_DatosRegistro.agActivo) {
                    jAlert('Esta Agenda ya se encuentra Inactiva.', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma desactivar la Agenda de "' + _DatosRegistro.Profesional + '"?', "Desactivar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdActivar":
                if (_DatosRegistro.agActivo) {
                    jAlert('Esta Agenda ya se encuentra Activa.', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Activar la Agenda de "' + _DatosRegistro.Profesional + '"?', "Activar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        var _Post = GetPathApp('catAgenda/_ActivaRegistro');
                        $.post(_Post, { id: _DatosRegistro.agId }, function (data) {
                            if (data) {
                                $("#Grid").data("tGrid").rebind();
                            }
                        });
                    }
                });
                break;
            case "cmdHorarios":
                var estGrid = $('#gridAgendaHorarios').data('tGrid');
                var _WindowEst = $("#CRUDAgendaHorarios").data("tWindow");

                $("#lblProfesional").text('Horarios de Atención de ' + _DatosRegistro.Profesional + ' / Especialidad: ' + _DatosRegistro.Especialidad.espNombre);
                estGrid.rebind();
                _WindowEst.center().open();
                _RowIndexHorario = -1;
                break;
            case "cmdAgregarHorario":
                var grid = $("#gridAgendaHorarios").data("tGrid");
                grid.addRow();
                break;
            case "cmdEditHorario":
                var grid = $("#gridAgendaHorarios").data("tGrid");
                var tr = $("#gridAgendaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdElimHorario":
                if (!_DatosRegistroHorario.aghActivo) {
                    jAlert('Este horario ya se encuentra Inactivo.', 'Error...');
                    return;
                }
                var grid = $("#gridAgendaHorarios").data("tGrid");
                var tr = $("#gridAgendaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar el horario del día "' + _DatosRegistroHorario.aghDiaSemana + '"?', "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdActivHorario":
                if (_DatosRegistroHorario.aghActivo) {
                    jAlert('Este horario ya se encuentra Activo.', 'Error...');
                    return;
                }
                var grid = $("#gridAgendaHorarios").data("tGrid");
                var tr = $("#gridAgendaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                jConfirm('¿Confirma Activar el Horario del día "' + _DatosRegistroHorario.aghDiaSemana + '"?', "Activar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('catAgenda/_ActivaRegistroHorario'), { id: _DatosRegistroHorario.aghId }, function (data) {
                            if (data) {
                                $("#gridAgendaHorarios").data("tGrid").rebind();
                            }
                        });
                    }
                });
                break;
            case "edit":
                _EsModificar = true;
                break;
        }
    }
    function onEditHorario(e) {
        if (_EsModificar) {
            $(".check-box").each(function (index) {
                $(this).attr('disabled', 'disabled');
            });
        }

        onCommandEdit(e);
    }
    function onComplete(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
    function onRowDataBound(e) {
        if (_RowIndex > -1) {
            var grid = $("#Grid").data("tGrid");
            var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistro = grid.dataItem(tr);
        }
    }
    function onRowDataBoundHorario(e) {
        if (_RowIndexHorario > -1) {
            var grid = $("#gridAgendaHorarios").data("tGrid");
            var tr = $("#gridAgendaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistroHorario = grid.dataItem(tr);
        }
    }
    function onSave(e) {
        var values = e.values;
        var _Especialidad = $("#espId").data("tComboBox");

        values.espId = _Especialidad.value();
    }
    function onDataBindingAgendaHorario(e) {
        if (_DatosRegistro != null) {
            e.data = $.extend(e.data, { idAgenda: _DatosRegistro.agId });
        }
    }
    function onSaveAgendaHorario(e) {
        var values = e.values;

        values.agId = _DatosRegistro.agId;
        if (e.mode == "update") {
            values.aghDiaSemana = _DatosRegistroHorario.aghDiaSemana;
        }
    }
    function onCompleteAgendaHorario(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }

</script>

<% ViewData["FiltroContains"] = true; %>
<% string _PathContent = Url.Content("~/Content"); %>

<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.catAgendas>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.agId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
           
            commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catAgenda", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catAgenda", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdEliminar").ButtonType(GridButtonType.ImageAndText).Text("Desactivar")
                .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catAgenda", "Eliminar") });
            commands.Custom().Ajax(true).Name("cmdActivar").ButtonType(GridButtonType.ImageAndText).Text("Activar")
                .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catAgenda", "Activar") });
            commands.Custom().Ajax(true).Name("cmdHorarios").ButtonType(GridButtonType.ImageAndText).Text("Horarios")
                .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catAgenda", "Horarios") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catAgenda")
                .Insert("_InsertEditing", "catAgenda")
                .Update("_SaveEditing", "catAgenda")
                .Delete("_DeleteEditing", "catAgenda");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.agActivo).Width("25px").Title("").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + _PathContent + "/Estados/Rojo.png' title='Inactivo' height='22px' width='22px' style='vertical-align:middle; visibility: <#= !agActivo ? \"visible\" : \"hidden\" #>' /></div>");
            columns.Bound(c => c.Profesional).Width("250px").Title("Profesional").Visible(true)
            .ClientTemplate("<label title='<#= Profesional #>' style='cursor: pointer;' id='txtProfesional' ><#= Profesional #></label>");
            columns.Bound(c => c.Especialidad.espNombre).Width("200px").Title("Especialidad").Visible(true)
            .ClientTemplate("<label title='<#= Especialidad.espNombre #>' style='cursor: pointer;' id='txtEspecialidad' ><#= Especialidad.espNombre #></label>");
            columns.Bound(c => c.agDuracion).Width("100px").Title("Tiempo de Atención").Visible(true);
            columns.Bound(c => c.agSobreturnos).Width("100px").Title("Sobreturnos").Visible(true);
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit").OnRowSelected("onRowSelected").OnCommand("onCommand").OnComplete("onComplete").OnRowDataBound("onRowDataBound").OnSave("onSave"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 99.8%;" })

%>
</div>
<!-- Horarios -->
<% Html.Telerik().Window()
        .Name("CRUDAgendaHorarios")
        .Title("Horarios")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catAgendasHorarios>)ViewData["AgendaHorarios"])
            .Name("gridAgendaHorarios")
            .DataKeys(keys =>
            {
                keys.Add(p => p.aghId);
            })
            .Localizable("es-AR")
            .ToolBar(commands =>
            {
                commands.Custom().Ajax(true).Name("cmdAgregarHorario").ButtonType(GridButtonType.ImageAndText)
                    .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                    .Text("Agregar");
                commands.Custom().Ajax(true).Name("cmdEditHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Modificar")
                    .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" });
                commands.Custom().Ajax(true).Name("cmdElimHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Eliminar")
                    .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" });
                //commands.Custom().Ajax(true).Name("cmdActivHorario").ButtonType(GridButtonType.ImageAndText)
                //    .Text("Activar")
                //    .ImageHtmlAttributes(new { style = "background: url('" + _PathContent + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -336px;" });
                %>
                <label id="lblProfesional" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                <%
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingAgendaHorario", "catAgenda")
                    .Update("_SaveEditingAgendaHorario", "catAgenda")
                    .Insert("_InsertEditingAgendaHorario", "catAgenda")
                    .Delete("_DeleteEditingAgendaHorario", "catAgenda");
            })
            .Columns(columns =>
            {
                columns.Bound(c => c.aghActivo).Width("25px").Title("").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + _PathContent + "/Estados/Rojo.png' title='Inactivo' height='22px' width='22px' style='vertical-align:middle; visibility: <#= !aghActivo ? \"visible\" : \"hidden\" #>' /></div>");
                columns.Bound(c => c.aghDiaSemana).Width("30px").Title("Día").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.aghFechas).Width("100px").Title("Fechas").Visible(true);
                columns.Bound(c => c.aghHoras).Width("100px").Title("Horas de Atención").Visible(true);
                columns.Bound(c => c.aghCantTurnos).Width("100px").Title("Cant. de Turnos").Visible(true);
                columns.Bound(c => c.aghSobreturnos).Width("100px").Title("Cant. de Sobreturnos").Visible(true);    
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingAgendaHorario", "catAgenda", new { idAgenda = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingAgendaHorario").OnEdit("onEditHorario").OnSave("onSaveAgendaHorario").OnComplete("onCompleteAgendaHorario").OnCommand("onCommand").OnRowDataBound("onRowDataBoundHorario").OnRowSelected("onRowSelectedHorario"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1024)
       .Height(420)
       .Render();
%>


