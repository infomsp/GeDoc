<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%
    ViewData["GradosDesignados"] = new List<catGradosDesignacionWS>();
%>
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
<% Html.Telerik().Grid((IEnumerable<catGradosDesignacionWS>)ViewData["GradosDesignados"])
.Name("gridGrados")
.DataKeys(keys =>
{
    keys.Add(p => p.gdId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0", title = "Agregar" })%>
            <label id="lblPersonaGrados" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_BindingGrados", "catPersona", new { idPersona = 0 })
        .Update("_SaveEditingGrados", "catPersona")
        .Insert("_InsertEditingGrados", "catPersona")
        .Delete("_DeleteEditingGrados", "catPersona");
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { title = "Modificar" });
        commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { title = "Eliminar" });
    }).Width("80px").Title("Acciones");
    columns.Bound(c => c.gdFechaHasta).Width("40px").Title("").Visible(true)
        .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Grado de Baja' height='22px' width='22px' style='vertical-align:middle; visibility: <#= gdFechaHasta != null ? \"visible\" : \"hidden\" #>' /></div>");
    columns.Bound(c => c.Agrupamiento).Width("110px").Title("Agrupamiento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= Agrupamiento #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Agrupamiento #> </label>");
    columns.Bound(c => c.Grado).Width("100px").Title("Grado").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"})
    .ClientTemplate("<label title='<#= Grado #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Grado #> </label>");
    columns.Bound(c => c.Categoria).Width("200px").Title("Categoría").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"})
    .ClientTemplate("<label title='<#= Categoria #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Categoria #> </label>");
    columns.Bound(c => c.gdServicio).Width("200px").Title("Servicio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= gdServicio #>' style='cursor: pointer;' style='white-space: nowrap;'><#= gdServicio #> </label>");
    columns.Bound(c => c.TipoCargo).Width("100px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= TipoCargo #>' style='cursor: pointer;' style='white-space: nowrap;'><#= TipoCargo #> </label>");
    columns.Bound(c => c.gdFecha).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha Carga").Visible(true);
    columns.Bound(c => c.gdFechaDesde).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha Inicial").Visible(true);
    columns.Bound(c => c.gdFechaHasta).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha Baja").Visible(true);
    columns.Bound(c => c.gdHoras).Width("70px").Title("Horas").Visible(true);
    //columns.Bound(c => c.gdConAdicional).Width("10%").Title("Con Adicional").Visible(true);
    columns.Bound(c => c.gdFuncion).Width("200px").Title("Cumple Función en").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= gdFuncion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= gdFuncion #> </label>");
    columns.Bound(c => c.gdNumeroRegistro).Width("120px").Title("N° Registro").Visible(true);
    columns.Bound(c => c.gdHorasAdicional).Width("120px").Title("Horas Adicionales").Visible(true);
    columns.Bound(c => c.Resolucion).Width("120px").Title("Resolución").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='Haga click aquí para ver la resolución' onclick=\"onVerPDF('<#= InfoResolucion.resLinkArchivo #>', <#= InfoResolucion.resNumero #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= Resolucion #> </label>");
    columns.Bound(c => c.Decreto).Width("120px").Title("Decreto").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='Haga click aquí para ver el decreto' onclick=\"onVerDecreto('<#= InfoDecreto.decLinkArchivo #>', <#= InfoDecreto.decNumero #>)\" style='cursor: pointer;' style='white-space: nowrap;'><#= Decreto #> </label>");
    columns.Bound(c => c.gdObservaciones).Width("120px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= gdObservaciones #>' style='cursor: pointer;' style='white-space: nowrap;'><#= gdObservaciones #> </label>");
})
.Editable(editing => editing
                            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingGrados").OnEdit("onCommandEditGrados").OnSave("onSaveGrados").OnComplete("onCompleteGrados"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>
