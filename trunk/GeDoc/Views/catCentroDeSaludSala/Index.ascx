<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script>
    function onCommand_catCentroDeSaludSala(e) {
        switch (e.name) {
            case "cmdEliminar":
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                jConfirm('¿Confirma Eliminar la Sala "?' + e.dataItem["cssNombre"] + '"', "Eliminar...", function (r) {
                    if (r) {
                        AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });

                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdTelevisores":
                _DatosRegistro_catCentroDeSaludSala = e.dataItem;

                var wGrilla = $("#wcatCentroDeSaludSalaTelevisor").data("tWindow");
                $('#wcatCentroDeSaludSalaTelevisor').css({ 'height': 430, 'width': 1000 });
                $('#wcatCentroDeSaludSalaTelevisor').find('div.t-window-content').css({ 'height': 400, 'width': 988 });
                wGrilla.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
                wGrilla.ajaxRequest(GetPathApp("catCentroDeSaludSala/InformescatCentroDeSaludSalaTelevisor"));
                wGrilla.title('Televisores Ubicados en la Sala "' + _DatosRegistro_catCentroDeSaludSala.cssNombre + '"').center().open();

                e.preventDefault();
                e.stopPropagation();
            break;
            case "cmdConsultorios":
                _DatosRegistro_catCentroDeSaludSala = e.dataItem;

                var wGrilla = $("#wcatCentroDeSaludSalaConsultorio").data("tWindow");
                $('#wcatCentroDeSaludSalaConsultorio').css({ 'height': 430, 'width': 1000 });
                $('#wcatCentroDeSaludSalaConsultorio').find('div.t-window-content').css({ 'height': 400, 'width': 988 });
                wGrilla.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
                wGrilla.ajaxRequest(GetPathApp("catCentroDeSaludSala/InformescatCentroDeSaludSalaConsultorio"));
                wGrilla.title('Consultorios Ubicados en la Sala "' + _DatosRegistro_catCentroDeSaludSala.cssNombre + '"').center().open();

                e.preventDefault();
                e.stopPropagation();
                break;
        }
    }

    function onComplete_catCentroDeSaludSala(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
</script>

<%= Html.Telerik().Grid<GeDoc.catCentroDeSaludSalaWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.cssId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Agregar"), title = "Agregar" });
        })
        .HtmlAttributes(new { style = "height:100%; width: 100%;" })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catCentroDeSaludSala")
                .Insert("_InsertEditing", "catCentroDeSaludSala")
                .Update("_SaveEditing", "catCentroDeSaludSala")
                .Delete("_DeleteEditing", "catCentroDeSaludSala");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Modificar"), title = "Modificar" });
                commands.Custom("cmdEliminar").Ajax(true).ButtonType(GridButtonType.Image)
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Eliminar"), title = "Modificar" })
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" });
                commands.Custom("cmdConsultorios").Ajax(true).ButtonType(GridButtonType.Image)
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Agregar"), title = "Administrar consultorios" })
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Cruz.png') no-repeat 0px 0px; background-size: 16px 16px;" });
                commands.Custom("cmdTelevisores").Ajax(true).ButtonType(GridButtonType.Image)
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSaludSala", "Agregar"), title = "Administrar televisores" })
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Televisor.png') no-repeat 0px 0px; background-size: 16px 16px;" });
            }).Width("80px").Title("Acciones");
            columns.Bound(c => c.cssId).Width("50px").Title("Código").Visible(true);
            columns.Bound(c => c.cssNombre).Width("200px").Title("Nombre").Visible(true);
            columns.Bound(c => c.Consultorios).Width("100px").Title("Consultorios").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
            columns.Bound(c => c.Televisores).Width("100px").Title("Televisores").Visible(true).HtmlAttributes(new { style = "text-align: right;" })
                .Aggregate(aggreages => aggreages.Sum())
                .ClientFooterTemplate("<#= $.telerik.formatString('{0:0}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
        })
            .Editable(editing => editing
                    .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                       paging.Enabled(true)
                            .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit").OnCommand("onCommand_catCentroDeSaludSala").OnComplete("onComplete_catCentroDeSaludSala"))
            .Footer(true)
        .Filterable()
        .Selectable()
        .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
        .Sortable()
%>

<% Html.Telerik().Window()
        .Name("wcatCentroDeSaludSalaTelevisor")
        .Title("Televisores")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Modal(true)
       .Scrollable(false)
       .Resizable()
       .Width(1000)
       .Height(400)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wCRUDcatCentroDeSaludSalaTelevisor")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(510)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wcatCentroDeSaludSalaConsultorio")
        .Title("Televisores")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Modal(true)
       .Scrollable(false)
       .Resizable()
       .Width(1000)
       .Height(400)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wCRUDcatCentroDeSaludSalaConsultorio")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(510)
       .Render();
%>
