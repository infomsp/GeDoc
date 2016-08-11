<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%=Url.Content("/Scripts/CRUDGrillas.js")%>"></script>
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
        debugger;
        if (("cmdEditar, cmdEliminar, cmdMovimientos").indexOf(e.name) >= 0) {
            if (_RowIndex < 0) {
                jAlert('Debe seleccionar un Bien.', 'Error...');
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
                if (_DatosRegistro.Movimientos > 1) {
                    jAlert('Este Bien NO se puede Eliminar, por que registra más de un movimiento.', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar el bien "' + _DatosRegistro.biCodigo + '"?', "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdMovimientos":
                var estGrid = $('#gridBienMovimientos').data('tGrid');
                var _WindowEst = $("#CRUDBienesMovimientos").data("tWindow");

                $("#lblBien").text('Movimientos del Bien Código N°: ' + _DatosRegistro.biCodigo);
                estGrid.rebind();
                _WindowEst.center().open();
                _RowIndexMovimiento = -1;
                break;
            case "cmdActivar":
                if (_DatosRegistro.biActivo) {
                    jAlert('Este Consultorio ya se encuentra Activo.', 'Error...');
                    return;
                }
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Activar el consultorio "' + _DatosRegistro.biActivo + '"?', "Activar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        $.post(GetPathApp('catCentroDeSaludConsultorio/_ActivaRegistro'), { id: _DatosRegistro.biId }, function (data) {
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
    function onCommandEditBien(e) {
        switch(e.mode){
            case "insert":
                HabilitaBiomedico(false);
                break;
            case "edit":
                HabilitaBiomedico(_DatosRegistro.biEsBiometrico);
                break;
        }
        $('#biEsBiometrico').change(function () {
            HabilitaBiomedico($(this).is(':checked'));
        });

        onCommandEdit(e);
    }

    function HabilitaBiomedico(_esBiomedico) {
        $('#biCodigoBiomedico')[0].disabled = !_esBiomedico;
        $('#biMarca')[0].disabled = !_esBiomedico;
        $('#biModelo')[0].disabled = !_esBiomedico;
        $('#biNroSerie')[0].disabled = !_esBiomedico;
        $('#biManuales')[0].disabled = !_esBiomedico;
        var _Fecha = $("#biFabricacion").data("tDatePicker");

        if (!_esBiomedico) {
            $('#biCodigoBiomedico')[0].value = '';
            $('#biMarca')[0].value = '';
            $('#biModelo')[0].value = '';
            $('#biNroSerie')[0].value = '';
            $('#biManuales')[0].value = '';
            _Fecha.value("");
            _Fecha.disable();
        }
        else {
            _Fecha.enable();
        }
    }

</script>
<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.catBien>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.biId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catBienes", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catBienes", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdEliminar").ButtonType(GridButtonType.ImageAndText).Text("Eliminar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catBienes", "Eliminar") });
            commands.Custom().Ajax(true).Name("cmdActivar").ButtonType(GridButtonType.ImageAndText).Text("Activar")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catBienes", "Activar") });
            commands.Custom().Ajax(true).Name("cmdMovimientos").ButtonType(GridButtonType.ImageAndText).Text("Movimientos")
                .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -240px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catBienes", "Examinar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catBienes")
                .Insert("_InsertEditing", "catBienes")
                .Update("_SaveEditing", "catBienes")
                .Delete("_DeleteEditing", "catBienes");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.Sector).Width("150px").Title("Sector").Visible(true)
            .ClientTemplate("<label title='<#= Sector #>' style='cursor: pointer;' ><#= Sector #></label>");
            columns.Bound(c => c.biCodigo).Width("100px").Title("Código").Visible(true)
            .ClientTemplate("<label title='<#= biCodigo #>' style='cursor: pointer;' ><#= biCodigo #></label>");
            columns.Bound(c => c.biDetalle1).Width("150px").Title("Detalle 1").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biDetalle1 #>' style='cursor: pointer; white-space: nowrap;' ><#= biDetalle1 #></label>");
            columns.Bound(c => c.biDetalle2).Width("150px").Title("Detalle 2").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biDetalle2 #>' style='cursor: pointer; white-space: nowrap;' ><#= biDetalle2 #></label>");
            columns.Bound(c => c.biDescripcion).Width("250px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biDescripcion #>' style='cursor: pointer; white-space: nowrap;' ><#= biDescripcion #></label>");
            columns.Bound(c => c.biEsBiometrico).Width("110px").Title("Es Biomédico").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' disabled='disabled' <#= biEsBiometrico ? checked = 'checked' : '' #> /></div>");
            columns.Bound(c => c.biCodigoBiomedico).Width("150px").Title("Código Biomédico").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biCodigoBiomedico #>' style='cursor: pointer; white-space: nowrap;' ><#= biCodigoBiomedico #></label>");
            columns.Bound(c => c.biMarca).Width("250px").Title("Marca").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biMarca #>' style='cursor: pointer; white-space: nowrap;' ><#= biMarca #></label>");
            columns.Bound(c => c.biModelo).Width("250px").Title("Modelo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biModelo #>' style='cursor: pointer; white-space: nowrap;' ><#= biModelo #></label>");
            columns.Bound(c => c.biNroSerie).Width("250px").Title("Nº de Serie").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biNroSerie #>' style='cursor: pointer; white-space: nowrap;' ><#= biNroSerie #></label>");
            columns.Bound(c => c.biFabricacion).Width("120px").Title("Fabricación").Visible(true);
            columns.Bound(c => c.biManuales).Width("250px").Title("Manuales").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biManuales #>' style='cursor: pointer; white-space: nowrap;' ><#= biManuales #></label>");
            columns.Bound(c => c.biObservaciones).Width("250px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= biObservaciones #>' style='cursor: pointer; white-space: nowrap;' ><#= biObservaciones #></label>");
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEditBien").OnRowSelected("onRowSelected").OnCommand("onCommand").OnComplete("onComplete").OnRowDataBound("onRowDataBound").OnSave("onSaveCRUD"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 99.8%;" })

%>
</div>
<%
Html.RenderPartial("Movimientos");
%>


<!--
    <style type="text/css" xml:lang="es-AR">
    .t-edit-form-container {
            width: 850px;
        }
    </style>
-->