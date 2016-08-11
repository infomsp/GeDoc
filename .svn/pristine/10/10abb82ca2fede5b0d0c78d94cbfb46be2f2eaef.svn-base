<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>

<% Html.RenderPartial("Valores"); %>
<% Html.RenderPartial("PlanillaMesEspecialidad"); %>
<script>
    var _DatosRegistroPlanilla;
    var _plaId;
    var _dataItemP;
    var _departamentoTexto;
    var _centroSaludTexto;
    var _mes;
    var _año;
    function OnCommand_ProEstadisticaPlanilla(e) {
        switch (e.name) {
           
            case "cmdEspecialidades":
                debugger;
                onRowSelected_ProEstadisticaPlanilla(e);
                var _WPlanillaMesEspecialidad = $('#WPlanillaMesEspecialidad').data("tWindow");
                _WPlanillaMesEspecialidad.open();
                RefrescarPlanillaMesEspecialidad();
                break;
        }
    }
    function onRowSelected_ProEstadisticaPlanilla(e) {
        debugger;
        var row = e.row;
        var grid = $("#Grid").data("tGrid");
        var _dataItemP = grid.dataItem(row);
        _DatosRegistroPlanilla = _dataItemP;
        _plaId = _dataItemP.plaId;
        _centroSaludTexto = _dataItemP.centroSaludTexto;
        _departamentoTexto = _dataItemP.departamentoTexto;
        _mes = _dataItemP.plaMes;
        _año = _dataItemP.plaAnio;
    }
    
    function  OnOpenDataPicker(e)
    {
    this.radDateTimePicker1.CustomFormat = "MM:yyyy";
    this.radDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
    this.radDateTimePicker1.ShowUpDown = true;
    }
</script>
<%= Html.Telerik().Grid<GeDoc.proEstadisticaPlanillas>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.plaId);
        })
         .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proEstadisticaPlanilla", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "proEstadisticaPlanilla")
                .Insert("_InsertEditing", "proEstadisticaPlanilla")
                .Update("_SaveEditing", "proEstadisticaPlanilla")
                .Delete("_DeleteEditing", "proEstadisticaPlanilla");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proEstadisticaPlanilla", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proEstadisticaPlanilla", "Eliminar") });
                commands.Custom("cmdEspecialidades")
                .Ajax(true)
                   .ButtonType(GridButtonType.Text)
                   .Text("Especialidades")
                   .SendDataKeys(false)
                   .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -320px;", title = "Especialiedades" })
                   .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proEstadisticaPlanilla", "Modificar") })
                   .SendState(false)
                   .DataRouteValues(route =>
                   {
                       route.Add(o => o.plaId).RouteKey("plaId");
                   }); 
                
            }).Width(8).Title("Acciones");
            columns.Bound(c => c.centroSaludTexto).Width(20).Title("Centro de Salud").Visible(true);
            columns.Bound(c => c.departamentoTexto).Width(10).Title("Departamento").Visible(true);
            columns.Bound(c => c.plaFecha).Width(10).Title("Fecha").Visible(false);
            columns.Bound(c => c.plaMes).Width(10).Title("Mes").Visible(false);
            columns.Bound(c => c.plaMesTexto).Width(10).Title("Mes").Visible(true);
            columns.Bound(c => c.plaAnio).Width(10).Title("Año").Visible(true);
            
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .ClientEvents(events => events.OnEdit("onCommandEdit").OnCommand("OnCommand_ProEstadisticaPlanilla").OnRowSelect("onRowSelected_ProEstadisticaPlanilla"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
%>
