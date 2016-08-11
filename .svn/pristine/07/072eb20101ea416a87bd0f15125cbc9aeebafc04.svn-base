<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript" >
    var idExpediente = -1;
    var _Error = false;
    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        idExpediente = dataItem.Id;
    }

    function Pases() {
        if (idExpediente < 0) {
            jAlert('Debe seleccionar un Expediente.', 'Error...');
            return;
        }
        VerPases(idExpediente);
    }

    function Imprimir() {
        if (idExpediente < 0) {
            jAlert('Debe seleccionar un Expediente.', 'Error...');
            return;
        }
        var _Parametros = new Array(new Array(idExpediente, 'IdExpediente'));
        InvocarReporte('rptCaratulaMesaDeEntrada', _Parametros);
    }

    function onCommand(e) {
        if (e.name == "cmdImprimir") {
            Imprimir();
        }
    }
    function onComplete(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
    function onDataBinding(e) {
        AbrirWaiting();
    }

    function onLoad(e) {
        //AbrirWaiting();
        debugger;
        $("#divTituloCatalogos").html($("#h_Catalogo")[0].value);
    }
</script>
<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>

<div style="overflow: hidden; height: 510px;" >
<% Html.Telerik().Grid<GeDoc.expExpedientes>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.Id);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Template(grid =>
            { %>
                <div class="t-button" onclick="Imprimir();" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/Printer.png" height="18" alt="Imprimir carátula" style="vertical-align: middle;" />
                    Imprimir
                </div>
                <div class="t-button" onclick="Pases();" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/intercambio.gif" height="18" alt="Pases" style="vertical-align: middle;" />
                    Pases
                </div>
            <%
            }
              );
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "expExpediente");
        })
        .Columns(columns =>
        {
            columns.Bound(c => c.Id).Width("80px").Title("Código").Visible(true);
            columns.Bound(c => c.Tipo).Width("60px").Title("Tipo").Visible(true);
            columns.Bound(c => c.Expediente).Width("150px").Title("Número").Visible(true);
            columns.Bound(c => c.Clave).Width("60px").Title("Clave").Visible(true);
            columns.Bound(c => c.Fecha).Width("120px").Title("Fecha").Visible(true);
            columns.Bound(c => c.pagFecha).Width("40px").Title("").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" }).Filterable(false)
            .ClientTemplate("<div title='Pagado' style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/<#= pagFecha == null ? \"Vacio-Transparente.png\" : \"SignoPesos.png\" #>' height='18px' width='18px' style='margin-right: 0px;' /></div>");
            columns.Bound(c => c.pagFecha).Width("120px").Title("Fecha de Pago").Visible(true);
            //columns.Bound(c => c.DiasCorridos).Width("120px").Title("Días Corridos").Visible(true);
            //columns.Bound(c => c.DiasHabiles).Width("120px").Title("Días Hábiles").Visible(true);
            columns.Bound(c => c.IniciadorNombre).Width("250px").Title("Nombre del Iniciador").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= IniciadorNombre #>' style='cursor: pointer;' id='txtIniciadorNombre' style='white-space: nowrap;'><#= IniciadorNombre #> </label>");
            columns.Bound(c => c.Extracto).Width("250px").Title("Extracto").Visible(true).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Extracto #>' style='cursor: pointer;' id='txtExtracto' style='white-space: nowrap;'><#= Extracto #> </label>");
            columns.Bound(c => c.Comentarios).Width("250px").Title("Comentarios").Visible(true).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Comentarios #>' style='cursor: pointer;' id='txtComentarios' style='white-space: nowrap;'><#= Comentarios #> </label>");
            columns.Bound(c => c.Folios).Width("250px").Title("Folios").Visible(true);
            columns.Bound(c => c.DestinoNombre).Width("250px").Title("Destinos").Visible(true).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= DestinoNombre #>' style='cursor: pointer;' id='txtDestinoNombre' style='white-space: nowrap;'><#= DestinoNombre #> </label>");
        })
            .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .ClientEvents(events => events.OnCommand("onCommand").OnRowSelected("onRowSelected").OnDataBinding("onDataBinding").OnComplete("onComplete"))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(ViewBag.AlturaGrilla))
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
            .HtmlAttributes(new { style = "width: 99.8%;" })
            .Render();
%>
</div>

<% Html.RenderPartial("VistaPasesExpedientes"); %>
<% Html.RenderPartial("MensajeError"); %>

