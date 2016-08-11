<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var _RowIndex = -1;
    var _DatosRegistro;
    var _EsModificar = false;
    var _IdPersona;
    var _Checkeado = false;
    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
    }
    function onClickClasificado(e, pId, pNombre, pApellido) {
        var grid = $(this).data("tGrid");
        _Checkeado = e.checked;
        _IdPersona = pId;
        jConfirm('¿Confirma ' + (e.checked ? 'asignar a este ' : 'quitar de este ') + 'CAPS a "' + pApellido + " " + pNombre + '"?', (e.checked ? 'Asignar...' : 'Quitar...'), function (r) {
            if (r) {
                AbrirWaiting();
                $.post(GetPathApp('conpadPadronPersonas/_SaveEditing'), { id: _IdPersona, pClasificacion: _Checkeado }, function (data) {
                    if (!data.IsValid) {
                        $('input[name=cbxClasificado_' + _IdPersona.toString() + ']').attr('checked', data.Clasificacion);
                    }
                    $('#lblCS_' + _IdPersona.toString()).text(data.CS);
                    CerrarWaiting();
                });
            }
            else {
                $('input[name=cbxClasificado_' + _IdPersona.toString() + ']').attr('checked', !e.checked);
            }
        });
    }
    function onDataBinding(e) {
        if (e.type == "dataBinding") {
            AbrirWaiting();
        }
    }
    function onLoad(e) {
        AbrirWaiting();
        $("#divTituloCatalogos").html("Población a Cargo " + $("#h1Titulo").text());
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

</script>

<% ViewData["FiltroContains"] = true; %>

<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.conpadPadronDePersonas>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.ppId);
        })
        .Localizable("es-AR")
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "conpadPadronPersonas")
                .Update("_SaveEditing", "conpadPadronPersonas");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.Clasificado).Width("50px").Title("").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' name='cbxClasificado_<#= ppId #>' id='cbxClasificado_<#= ppId #>' <#= Clasificado ? checked = 'checked' : '' #> onclick='onClickClasificado(this, <#= ppId #>, \"<#= ppNombre #>\", \"<#= ppApellido #>\");' /></div>");
            columns.Bound(c => c.CentroDeSalud).Width("100px").Title("CAPS").Visible(true)
            .ClientTemplate("<label id='lblCS_<#= ppId #>' title='<#= CentroDeSalud #>' style='cursor: pointer; white-space: nowrap;' ><#= CentroDeSalud #></label>");
            columns.Bound(c => c.ppApellido).Width("100px").Title("Apellido").Visible(true)
            .ClientTemplate("<label title='<#= ppApellido #>' style='cursor: pointer; white-space: nowrap;' ><#= ppApellido #></label>");
            columns.Bound(c => c.ppNombre).Width("100px").Title("Nombre").Visible(true)
            .ClientTemplate("<label title='<#= ppNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= ppNombre #></label>");
            columns.Bound(c => c.Documento).Width("150px").Title("Documento").Visible(true)
            .ClientTemplate("<label title='<#= Documento #>' style='cursor: pointer; white-space: nowrap;' ><#= Documento #></label>");
            columns.Bound(c => c.ppDomCalle).Width("290px").Title("Calle").Visible(true)
            .ClientTemplate("<label title='<#= ppDomCalle #>' style='cursor: pointer; white-space: nowrap;' ><#= ppDomCalle #></label>");
            columns.Bound(c => c.ppDomNroCalle).Width("90px").Title("N° Calle").Visible(true);
            columns.Bound(c => c.ppDomPiso).Width("60px").Title("Piso").Visible(true);
            columns.Bound(c => c.ppDomDepto).Width("70px").Title("Dpto").Visible(true);
            columns.Bound(c => c.ppDomBarrio_Loc).Width("140px").Title("Barrio").Visible(true)
            .ClientTemplate("<label title='<#= ppDomBarrio_Loc #>' style='cursor: pointer; white-space: nowrap;' ><#= ppDomBarrio_Loc #></label>");
            columns.Bound(c => c.ppCP).Width("120px").Title("Código Postal").Visible(true);
            columns.Bound(c => c.ppDepartamento).Width("140px").Title("Departamento").Visible(true)
            .ClientTemplate("<label title='<#= ppDepartamento #>' style='cursor: pointer; white-space: nowrap;' ><#= ppDepartamento #></label>");
            columns.Bound(c => c.Localidad).Width("140px").Title("Localidad").Visible(true)
            .ClientTemplate("<label title='<#= Localidad #>' style='cursor: pointer; white-space: nowrap;' ><#= Localidad #></label>");
            columns.Bound(c => c.ppCUIL).Width("120px").Title("CUIL").Visible(true)
            .ClientTemplate("<label title='<#= ppCUIL #>' style='cursor: pointer; white-space: nowrap;' ><#= ppCUIL #></label>");
            columns.Bound(c => c.ppSexo).Width("90px").Title("Sexo").Visible(true)
            .ClientTemplate("<label title='<#= ppSexo #>' style='cursor: pointer; white-space: nowrap;' ><#= ppSexo #></label>");
            columns.Bound(c => c.ppEdad).Width("60px").Title("Edad").Visible(true);
            columns.Bound(c => c.ppTipoEdad).Width("140px").Title("Unidad de Tiempo").Visible(true)
            .ClientTemplate("<label title='<#= ppTipoEdad #>' style='cursor: pointer; white-space: nowrap;' ><#= ppTipoEdad #></label>");
            columns.Bound(c => c.ppFechaNacimiento).Width("160px").Title("Fecha de Nacimiento").Visible(true);
        })
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnRowSelected("onRowSelected").OnDataBinding("onDataBinding").OnComplete("onComplete").OnRowDataBound("onRowDataBound").OnLoad("onLoad"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(455))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 99.8%;" })

%>
</div>
