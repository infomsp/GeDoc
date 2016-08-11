<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    ViewData["FiltroContains"] = true;
%>

<script type="text/javascript">
    var idPersona = -1;
    function onCompleteDesignados(e) {
        if (e.name === "Consultar") {
            DetallePersona(e);
        }
    }

    function onDataBindingEstados(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
    
    function onDataBindingAsistencia(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }

    function onEdit(e) {
        if (e.mode == "edit") {
            var lCategoria = $('#categId').data("tComboBox");
            lCategoria.disable();
        }
        onCommandEdit(e);
    }

    function onComboBoxLoad() {
        $(this).data("tComboBox").fill();
    }

    function Imprimir(e) {
        var _Parametros = new Array(new Array(idPersona, 'idPersona'));
        InvocarReporte('rptFichaPersonal', _Parametros);
    }
</script>
<div style="overflow: hidden; height: 510px;" >
<%= Html.Telerik().Grid<GeDoc.catCargosCategoriasDesignados>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.desigId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoriaDesignacion", "Agregar"), title = "Agregar" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditingDesignados", "catCargoCategoriaDesignacion")
                .Update("_SaveEditingDesignados", "catCargoCategoriaDesignacion")
                .Insert("_InsertEditingDesignados", "catCargoCategoriaDesignacion")
                .Delete("_DeleteEditingDesignados", "catCargoCategoriaDesignacion");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoriaDesignacion", "Modificar"), title = "Modificar" });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoriaDesignacion", "Eliminar"), title = "Eliminar" });
                commands.Custom("Consultar").ButtonType(GridButtonType.Image)
                                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCargoCategoriaDesignacion", "Examinar"), title = "Consultar" })
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                                    .DataRouteValues(route => route.Add(o => o.perId).RouteKey("personaId"))
                                    .Ajax(true)
                                    .Action("ViewDetails", "catPersona");                    
            }).Width("120px").Title("Acciones");
            columns.Bound(c => c.desigVigenciaHasta).Width("40px").Title("").Visible(true)
                .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Contrato de Baja' height='22px' width='22px' style='vertical-align:middle; visibility: <#= desigVigenciaHasta != null ? \"visible\" : \"hidden\" #>' /></div>");
            columns.Bound(c => c.desigTipo).Width("80px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.perNombre).Width("200px").Title("Empleado").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                .ClientTemplate("<label title='<#= perNombre #>' style='cursor: pointer;' id='txtperNombre' style='white-space: nowrap;'><#= perNombre #> </label>");
            columns.Bound(c => c.Sector.secId).Width("200px").Title("Sector").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                .ClientTemplate("<label title='<#= Sector.secNombre #>' style='cursor: pointer;' id='txtsecNombre' style='white-space: nowrap;'><#= Sector.secNombre #> </label>");
            columns.Bound(c => c.Sector.ccCuentaContable.ccCodigo).Width("200px").Title("Cuenta Contable").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                .ClientTemplate("<label title='<#= Sector.ccCuentaContable.ccDescripcion #>' style='cursor: pointer;' id='txtccCodigo' style='white-space: nowrap;'><#= Sector.ccCuentaContable.ccCodigo #> </label>");
            columns.Bound(c => c.carDescripcion).Width("400px").Title("Cargo").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                .ClientTemplate("<label title='<#= carDescripcion #>' style='cursor: pointer;' id='txtcarDescripcion' style='white-space: nowrap;'><#= carDescripcion #> </label>");
            columns.Bound(c => c.desigVigenciaDesde).Width("100px").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.desigVigenciaHasta).Width("100px").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.perSubRoganciaNombre).Width("200px").Title("Subrogado por").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                .ClientTemplate("<label title='<#= perSubRoganciaNombre #>' style='cursor: pointer;' id='txtperSubRoganciaNombre' style='white-space: nowrap;'><#= perSubRoganciaNombre #> </label>");
            columns.Bound(c => c.desigSubRoganciaDesde).Width("100px").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.desigSubRoganciaHasta).Width("100px").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.desigObservaciones).Width("200px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                .ClientTemplate("<label title='<#= desigObservaciones #>' style='cursor: pointer;' id='txtdesigObservaciones' style='white-space: nowrap;'><#= desigObservaciones #> </label>");
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                    .ClientEvents(events => events.OnEdit("onEdit").OnComplete("onCompleteDesignados"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
            .HtmlAttributes(new { style = "width: 100%;" })
%>
</div>
<% Html.RenderPartial("ConsultaPersona"); %>



