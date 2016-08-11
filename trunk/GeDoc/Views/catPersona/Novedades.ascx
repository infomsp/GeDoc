<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%
    ViewData["PersonaNovedades"] = new List<catPersonaNovedadWS>();
%>

<% Html.RenderPartial("VistaPasesExpedientes"); %>

<script type="text/javascript">
    function onVerPDF(LinkArchivo, Numero) {
        var _Window = $("#verPDF").data("tWindow");
        debugger;
        if (LinkArchivo != "") {
            $("#divPDF").css("display", "none");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/Resoluciones/' + LinkArchivo));
            _Window.title("Ver Resolución N° " + Numero.toString() + "...").center().open();
        }
        else {
            $("#divPDF").css("display", "block");
            _Window.title("Ver Resolución N° " + Numero.toString() + "...").center().open();
        }
    }
    function onVerDecreto(LinkArchivo, Numero) {
        var _Window = $("#verPDF").data("tWindow");
        debugger;
        if (LinkArchivo != "") {
            $("#divPDF").css("display", "none");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/Decretos/' + LinkArchivo));
            _Window.title("Ver Decreto N° " + Numero.toString() + "...").center().open();
        }
        else {
            $("#divPDF").css("display", "block");
            _Window.title("Ver Decreto N° " + Numero.toString() + "...").center().open();
        }
    }
    function onVerPases(idExpediente) {
        VerPases(idExpediente);
    }
</script>

<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("verPDF")
        .Title("Resoluciones")
        .Visible(false)
        .Content(() => 
        {%> 
            <div id="divPDF" style="width: 100%; height: 100%; display: none;">
                <table style="border: none; margin:10px;">
                    <tr style="border: none; margin:0px;">
                        <td style="border: none; margin:0px;">
                            <img src="<%= Url.Content("~/Content") %>/General/Notificacion/Vista Internet Security OFF.png" width="40px" style="margin-top:-5px" />
                        </td>
                        <td style="border: none; margin:0px;">
                            <asp:Label ID="Label1" runat="server" style="margin-left:-5px" ForeColor="Red">Archivo no encontrado...</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <iframe id="framePDF" src='' width="100%" height="100%" >
            </iframe>
        <%})
        .Buttons(b => b.Maximize().Close())
        .Draggable(true)
        .Scrollable(false)
        .Resizable()
        .Width(870)
        .Height(460)
        .Render();
%>

<div>
<% Html.Telerik().Grid((IEnumerable<catPersonaNovedadWS>)ViewData["PersonaNovedades"])
    .Name("gridNovedades")
    .DataKeys(keys =>
    {
        keys.Add(p => p.novId);
    })
    .Localizable("es-AR")
    .ToolBar(commands =>
    {
        commands.Template(cmdTemplate =>
            {
                %>
                <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0", title = "Agregar" })%>
                <label id="lblPersonaNovedades" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
                <%
            });
    })
    .DataBinding(dataBinding =>
    {
        dataBinding.Ajax()
            .Select("_BindingNovedades", "catPersona", new { idPersona = 0 })
            .Update("_SaveEditingNovedades", "catPersona")
            .Insert("_InsertEditingNovedades", "catPersona")
            .Delete("_DeleteEditingNovedades", "catPersona");
    })
    .Columns(columns =>
    {
        columns.Command(commands =>
        {
            commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { title = "Modificar" });
            commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { title = "Eliminar" });
        }).Width("80px").Title("Acciones");
        columns.Bound(c => c.novFecha).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha").Visible(true);
        columns.Bound(c => c.novDescripcion).Width("330px").Title("Detalle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='<#= novDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= novDescripcion #> </label>");
        columns.Bound(c => c.Expediente).Width("120px").Title("Expediente").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='Haga click aquí para ver los pases' onclick=\"onVerPases('<#= idExpediente #>', <#= Expediente #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= Expediente #> </label>");
        columns.Bound(c => c.Resolucion).Width("120px").Title("Resolución").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='Haga click aquí para ver la resolución' onclick=\"onVerPDF('<#= InfoResolucion.resLinkArchivo #>', <#= InfoResolucion.resNumero #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= Resolucion #> </label>");
        columns.Bound(c => c.Decreto).Width("120px").Title("Decreto").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='Haga click aquí para ver el decreto' onclick=\"onVerDecreto('<#= InfoDecreto.decLinkArchivo #>', <#= InfoDecreto.decNumero #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= Decreto #> </label>");
    })
    .Editable(editing => editing
                                .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
    .Pageable((paging) =>
                        paging.Enabled(true)
                            .PageSize(((int)Session["FilasPorPagina"])))
    .Footer(true)
    .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingNovedades").OnEdit("onCommandEditNovedades").OnSave("onSaveNovedades").OnComplete("onCompleteNovedades"))
    .Filterable()
    .Selectable()
    .Resizable(resizing => resizing.Columns(true))
    .Scrollable(scroll => scroll.Enabled(true).Height(310))
    .Sortable()
    .Render();
    %>
</div>
