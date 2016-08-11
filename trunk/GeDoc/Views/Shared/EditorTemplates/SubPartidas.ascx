<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<!-- SubPartidas -->
<input id="impuAnuladaParcialPor" name="impuAnuladaParcialPor" style="display:none" type="text" value="0">
<% Html.Telerik().Grid((IEnumerable<GeDoc.proImputacionesDetalles>)Session["DetalleImputaciones"])
    .Name("gridDetalleImputaciones")
    .DataKeys(keys =>
    {
        keys.Add(p => p.impdId);
    })
    .ToolBar(commands =>
    {
        commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
    })
    .DataBinding(dataBinding =>
    {
        dataBinding.Ajax()
            .Select("_BindingDetalleImputaciones", "proImputacion", new { idImputacion = 1, _AnulacionParcial = 0 })
            .Update("_SaveEditingDetalle", "proImputacion")
            .Insert("_InsertEditingDetalle", "proImputacion")
            .Delete("_DeleteEditingDetalle", "proImputacion");
    })
    .Columns(columns =>
    {
        columns.Command(commands =>
        {
            commands.Edit().ButtonType(GridButtonType.Image);
            commands.Delete().ButtonType(GridButtonType.Image);
        }).Width("120px").Title("Acciones");
        //.ForeignKey(o => o.EmployeeID, (IEnumerable)ViewData["employees"],"ID", "Name").Width(230)
        columns.ForeignKey(o => o.subpId, (IEnumerable)ViewData["SubPartidas.subpId_Data"], "Value", "Text").Width("200px").Title("Sub-Partida")
            .HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= subpNombre #>' style='cursor: pointer;' id='txtsubpNombre' style='white-space: nowrap;'><#= subpNombre #> </label>")
            .FooterTemplate(() =>
                    {%>
                        <div align="right">Total Imputado: </div>
                    <%}).Visible(true);
        columns.Bound(c => c.impdImporte).Width("80px").Title("Importe")
            .Visible(true).HtmlAttributes(new { style = "text-align: right;" })
            .Aggregate(aggreages => aggreages.Sum())
            .ClientFooterTemplate("<#= $.telerik.formatString('{0:c}', Sum) #>").FooterHtmlAttributes(new { style = "text-align: right;" });
    })
    .Editable(editing => editing
     .Mode(GridEditMode.InLine).DisplayDeleteConfirmation(true))
    .Footer(true)
    .ClientEvents(clientEvents => clientEvents.OnEdit("onEditSubPartida").OnDataBinding("onDataBindingDetalle").OnSave("onSaveDetalle").OnComplete("onCompleteDetalle"))
    .Selectable()
    .Scrollable(scroll => scroll.Enabled(true).Height(180))
    .HtmlAttributes(new { style = "width: 515px;" })
    .Sortable()
    .Pageable()
    .Render();
%>

<script type="text/javascript">
</script>
