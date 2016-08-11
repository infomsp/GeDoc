<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var _CurrentPage;
    var _OrderBy;
    var _FilterBy;
    var idPersona = -1;
    var _Nombre = "";
    var _RowIndex = -1;
    var _DatosRegistro;
    var _RowIndexHorario = -1;
    var _DatosRegistroHorario;
    var _EsModificar = false;

    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex = e.row.rowIndex;
        idPersona = dataItem.conId;
        _DatosRegistro = dataItem;
        _Nombre = dataItem.conApellidoyNombre;
    }
    function onRowSelectedHorario(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndexHorario = e.row.rowIndex;
        _DatosRegistroHorario = dataItem;
    }
    function onCommand(e) {
        _EsModificar = false;
        if (("cmdEspecialidades, cmdEditarPersona, cmdEliminarPersona, cmdHorarios").indexOf(e.name) >= 0) {
            if (idPersona < 0) {
                jAlert('Debe seleccionar una persona.', 'Error...');
                return;
            }
        }
        if (("cmdEditarHorario, cmdEliminarHorario, cmdActivarHorario").indexOf(e.name) >= 0) {
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
            case "cmdEspecialidades":
                var estGrid = $('#gridEspecialidades').data('tGrid');
                var _WindowEst = $("#CRUDEspecialidades").data("tWindow");

                $("#lblNombrePersona3").text('Especialidades de ' + _Nombre);
                estGrid.rebind();
                _WindowEst.center().open();
                break;
            case "cmdEditarPersona":
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminarPersona":
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar el legajo de "' + _Nombre + '"?', "Eliminar...", function (r) {
                    if (r) {
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdHorarios":
                var estGrid = $('#gridPersonaHorarios').data('tGrid');
                var _WindowEst = $("#CRUDPersonaHorarios").data("tWindow");

                $("#lblProfesional").text('Horarios de ' + _Nombre);
                estGrid.rebind();
                _WindowEst.center().open();
                _RowIndexHorario = -1;
                break;
            case "cmdAgregarHorario":
                var grid = $("#gridPersonaHorarios").data("tGrid");
                grid.addRow();
                break;
            case "cmdEditarHorario":
                var grid = $("#gridPersonaHorarios").data("tGrid");
                var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                grid.editRow(tr);
                break;
            case "cmdEliminarHorario":
                if (!_DatosRegistroHorario.conhActivo) {
                    jAlert('Este horario ya se encuentra Inactivo.', 'Error...');
                    return;
                }
                var grid = $("#gridPersonaHorarios").data("tGrid");
                var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar el horario del día "' + _DatosRegistroHorario.conhDiaSemana + '"?', "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdActivarHorario":
                if (_DatosRegistroHorario.conhActivo) {
                    jAlert('Este horario ya se encuentra Activo.', 'Error...');
                    return;
                }
                var grid = $("#gridPersonaHorarios").data("tGrid");
                var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
                jConfirm('¿Confirma Activar el Horario del día "' + _DatosRegistroHorario.conhDiaSemana + '"?', "Activar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('catPersonaContratados/_ActivaRegistroHorario'), { id: _DatosRegistroHorario.conhId }, function (data) {
                            if (data) {
                                $("#gridPersonaHorarios").data("tGrid").rebind();
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
    function onDataBound() {
        var grid = $(this).data('tGrid');
        _CurrentPage = grid.currentPage;
        _OrderBy = (grid.orderBy || '~');
        _FilterBy = (grid.filterBy || '~');

        var _Boton = $('a.t-button.t-grid-cmdExportar');
        var href = _Boton.attr('href');

        href = href.replace(/page=([^&]*)/, 'page=' + _CurrentPage);
        href = href.replace(/orderBy=([^&]*)/, 'orderBy=' + (_OrderBy || '~'));
        href = href.replace(/filter=(.*)/, 'filter=' + (_FilterBy || '~'));
        _Boton.attr('href', href);
    }

    function onDataBindingEspecialidades(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    function onSaveEspecialidades(e) {
        var values = e.values;
        values.conId = idPersona;
    }
    function onCompleteEspecialidades(e) {
        if (e.name == "update" || e.name == "insert") {
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
    function onRowDataBoundHorario(e) {
        if (_RowIndexHorario > -1) {
            var grid = $("#gridPersonaHorarios").data("tGrid");
            var tr = $("#gridPersonaHorarios tbody tr:eq(" + (_RowIndexHorario + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
            _DatosRegistroHorario = grid.dataItem(tr);
        }
    }
    function onDataBindingPersonaHorario(e) {
        if (_DatosRegistro != null) {
            e.data = $.extend(e.data, { idPersona: _DatosRegistro.conId });
        }
    }
    function onSavePersonaHorario(e) {
        var values = e.values;

        values.conId = _DatosRegistro.conId;
        if (e.mode == "update") {
            values.conhDiaSemana = _DatosRegistroHorario.conhDiaSemana;
        }
    }
    function onCompletePersonaHorario(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
    function onSave(e) {
        //        AbrirWaiting();
    }
    function onDataBinding(e) {
        AbrirWaiting();
    }
    function onComplete(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        } else {
            //            var _Window = $("#GridPopUp").data("tWindow");
            //            if (_Window != null) {
            //                _Window.close();
            //                _Window.close();
            //                CerrarWaiting();
            //            }
        }
    }
    
</script>
<% Html.RenderPartial("Waiting"); %>
<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.catPersonasContratados>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.conId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersonaContratados", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditarPersona").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersonaContratados", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdEliminarPersona").ButtonType(GridButtonType.ImageAndText).Text("Eliminar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersonaContratados", "Eliminar") });
            commands.Custom().Name("cmdEspecialidades")
                .Ajax(true)
                .ButtonType(GridButtonType.ImageAndText)
                .Text("Especialidades")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -208px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersona", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdHorarios").ButtonType(GridButtonType.ImageAndText).Text("Horarios")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersonaContratados", "Horarios") });
            commands.Custom().Action("Exportar", "catPersonaContratados", new { page = 1, orderBy = "~", filter = "~" }).Name("cmdExportar").ButtonType(GridButtonType.ImageAndText).Text("Exportar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -240px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersonaContratados", "Exportar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catPersonaContratados")
                .Insert("_InsertEditing", "catPersonaContratados")
                .Update("_SaveEditing", "catPersonaContratados")
                .Delete("_DeleteEditing", "catPersonaContratados");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.conDeBaja).Width("35px").Title("").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Contrato de Baja' height='22px' width='22px' style='vertical-align:middle; visibility: <#= conFechaBaja != null ? \"visible\" : \"hidden\" #>' /></div>");
            columns.Bound(c => c.conFecha).Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.conApellidoyNombre).Width("230px").Title("Apellido y Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= conApellidoyNombre #>' style='cursor: pointer;' id='txtApellidoyNombre' style='white-space: nowrap;'><#= conApellidoyNombre #> </label>");
            columns.Bound(c => c.conSexo).Width("80px").Title("Sexo").Visible(true);
            columns.Bound(c => c.conDNI).Width("80px").Title("DNI").Visible(true);
            columns.Bound(c => c.conCUIL).Width("120px").Title("CUIL").Visible(true);
            columns.Bound(c => c.conDomicilio).Width("200px").Title("Domicilio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= conDomicilio #>' style='cursor: pointer;' id='txtDomicilio' style='white-space: nowrap;'><#= conDomicilio #> </label>");
            columns.Bound(c => c.conTelefono).Width("120px").Title("Teléfono").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= conTelefono #>' style='cursor: pointer;' style='white-space: nowrap;'><#= conTelefono #> </label>");
            columns.Bound(c => c.conCelular).Width("120px").Title("Celular").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= conCelular #>' style='cursor: pointer;' style='white-space: nowrap;'><#= conCelular #> </label>");
            columns.Bound(c => c.conEmail).Width("200px").Title("Correo Electrónico").Visible(true);
            columns.Bound(c => c.profNombre).Width("220px").Title("Actividad").Visible(true);
            columns.Bound(c => c.espNombre).Width("220px").Title("Especialidad").Visible(true);
            columns.Bound(c => c.conCargaHorariaSemanal).Width("70px").Title("Horas").Visible(true);
            columns.Bound(c => c.conCuotas).Width("70px").Title("Cuotas").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.conMontoMensual).Width("130px").Title("Monto Mensual").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.conMontoAnual).Width("130px").Title("Monto Anual").Visible(true).HtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.repNombre).Width("200px").Title("Zona Sanitaria").Visible(true);
            columns.Bound(c => c.conObservaciones).Width("150px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= conObservaciones #>' style='cursor: pointer;' id='txtconObservaciones' style='white-space: nowrap;'><#= conObservaciones #> </label>");
            columns.Bound(c => c.conFechaBaja).Width("100px").Title("Fecha Baja").Visible(true);
            columns.Bound(c => c.conMotivoBaja).Width("150px").Title("Motivo Baja").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= conMotivoBaja #>' style='cursor: pointer;' id='txtconMotivoBaja' style='white-space: nowrap;'><#= conMotivoBaja #> </label>");
            columns.Bound(c => c.TextoMontoMensual).Width("100px").Title("Texto Monto Mensual").Visible(false);
            columns.Bound(c => c.TextoMontoAnual).Width("100px").Title("Texto Monto Anual").Visible(false);
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit").OnCommand("onCommand").OnDataBound("onDataBound").OnRowSelected("onRowSelected").OnSave("onSave").OnComplete("onComplete").OnDataBinding("onDataBinding"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
            .HtmlAttributes(new { style = "width: 100%;" })
%>
</div>

<!-- Especialidades -->
<% Html.Telerik().Window()
        .Name("CRUDEspecialidades")
        .Title("Especialidades")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catPersonasContratadosEspecialidades>)ViewData["Especialidades"])
            .Name("gridEspecialidades")
            .DataKeys(keys =>
            {
                keys.Add(p => p.coneId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblNombrePersona3" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingEspecialidades", "catPersonaContratados")
                    .Update("_SaveEditingEspecialidades", "catPersonaContratados")
                    .Insert("_InsertEditingEspecialidades", "catPersonaContratados")
                    .Delete("_DeleteEditingEspecialidades", "catPersonaContratados");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersonaContratados", "Modificar") });
                    commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPersonaContratados", "Eliminar") });
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.peEspecialidades.espCodigo).Width("10%").Title("Código").Visible(true);
                columns.Bound(c => c.peEspecialidades.espNombre).Width("20%").Title("Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingEspecialidades", "catPersonaContratados", new { idPersona = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingEspecialidades").OnEdit("onCommandEdit").OnSave("onSaveEspecialidades").OnComplete("onCompleteEspecialidades"))
            .Filterable()
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
       .Width(1024)
       .Height(400)
       .Render();
%>
<!-- Horarios -->
<% Html.Telerik().Window()
        .Name("CRUDPersonaHorarios")
        .Title("Horarios")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.catPersonasContratadosHorarios>)ViewData["PersonaHorarios"])
            .Name("gridPersonaHorarios")
            .DataKeys(keys =>
            {
                keys.Add(p => p.conhId);
            })
            .ToolBar(commands =>
            {
                commands.Custom().Ajax(true).Name("cmdAgregarHorario").ButtonType(GridButtonType.ImageAndText)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                    .Text("Agregar");
                commands.Custom().Ajax(true).Name("cmdEditarHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Modificar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" });
                commands.Custom().Ajax(true).Name("cmdEliminarHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Eliminar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" });
                commands.Custom().Ajax(true).Name("cmdActivarHorario").ButtonType(GridButtonType.ImageAndText)
                    .Text("Activar")
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -336px;" });
                %>
                <label id="lblProfesional" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                <%
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingPersonaHorario", "catPersonaContratados")
                    .Update("_SaveEditingPersonaHorario", "catPersonaContratados")
                    .Insert("_InsertEditingPersonaHorario", "catPersonaContratados")
                    .Delete("_DeleteEditingPersonaHorario", "catPersonaContratados");
            })
            .Columns(columns =>
            {
                columns.Bound(c => c.conhActivo).Width("25px").Title("").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Inactivo' height='22px' width='22px' style='vertical-align:middle; visibility: <#= !conhActivo ? \"visible\" : \"hidden\" #>' /></div>");
                columns.Bound(c => c.conhDiaSemana).Width("30px").Title("Día").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
                columns.Bound(c => c.conhHoras).Width("100px").Title("Horas").Visible(true);
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingPersonaHorario", "catPersonaContratados", new { idPersona = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingPersonaHorario").OnEdit("onEditHorario").OnSave("onSavePersonaHorario").OnComplete("onCompletePersonaHorario").OnCommand("onCommand").OnRowDataBound("onRowDataBoundHorario").OnRowSelected("onRowSelectedHorario"))
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

