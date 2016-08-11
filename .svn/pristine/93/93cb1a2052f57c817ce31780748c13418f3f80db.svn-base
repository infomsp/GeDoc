<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var _WindowCRUD;
    function onDataBindingcatHCAdjunto(e) {

        e.data = $.extend(e.data, { idPaciente: $("#pacId").val() });
    }
    function onCompletecatHCAdjunto(e) {
        if (e.name === "update" || e.name === "insert" || e.name === "delete") {
        }
    }
    
    function onCommandcatHCAdjunto(e) {
        switch (e.name) {
            case "cmdModificarcatHCAdjunto":
                onAccioncatHCAdjunto("Modificar", e.dataItem['hcadjId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdDeletecatHCAdjunto":
                onAccioncatHCAdjunto("Eliminar", e.dataItem['hcadjId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdCambiocatHCAdjunto":
                onAccioncatHCAdjunto("Recepción", e.dataItem['hcadjId']);
                e.preventDefault();
                e.stopPropagation();
                break;
                //case "cmdConsultarSeguimiento":
                //    onAccioncatHCAdjunto("Consultar", e.dataItem['hcadjId']);
            //    e.preventDefault();
            //    e.stopPropagation();
            //    break;
        }
    }

    function onAccioncatHCAdjunto(Accion, idDocumentacion) {
        var Altura = 500;
        switch (Accion) {
            case "Agregar":
                break;
            case "Modificar":
                break;
            case "Eliminar":
                //Altura = 220;
                break;
            case "Consultar":
                //Altura = 220;
                break;
            case "Recepción":
                Altura = 530;
                break;
        }

        var _WindowSeguimiento = $("#wcatHCAdjunto").data("tWindow");
        $('#wcatHCAdjunto').css({ 'height': Altura, 'width': 552 });
        $('#wcatHCAdjunto').find('div.t-window-content').css({ 'height': Altura-30, 'width': 540 });
        _WindowSeguimiento.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');

        ////////////////////////////////////////
        var _Post = GetPathApp("HistoriaClinica/getHCDocumentacion");
        $.ajax({
            url: _Post,
            data: { Accion: Accion, hcadjId: idDocumentacion, idPaciente: $("#pacId").val() },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert('Falló el acceso al servidor', '¡Atención!', function () {
                });
                $('#popup_container').focus();
                $('#popup_ok').focus();
            },
            success: function (data) {
                CerrarWaiting();
                var _WindowCRUD = $("#wcatHCAdjunto").data("tWindow");
                _WindowCRUD.content(data);
            }
        });
        _WindowSeguimiento.center().title(Accion).open();

    }

    //function Descargar(e) {
    //    var _Post = GetPathApp('catPersona/Descargar');
    //    $.post(_Post, { Archivo: e });
    //}
    function onVerPDF(Archivo) {
        var _Window = $("#verPDF").data("tWindow");

        if (Archivo != "") {
            $("#divPDF").css("display", "none");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/HC/' + Archivo));
            _Window.title("Archivo " + Archivo.substring(16) + "...").center().open();
        }
        else {
            $("#divPDF").css("display", "block");
            _Window.title("Archivo " + Archivo + "...").center().open();
        }
    }
</script>
<% ViewData["AltoEditor"] = "200px"; %>
<% var catHCAdjuntoWS = new List<catHCAdjuntoWS>(); %>

<div>
<% Html.Telerik().Grid((IEnumerable<catHCAdjuntoWS>)catHCAdjuntoWS)
.Name("gridcatHCAdjunto")
.Localizable("es-AR")
.DataKeys(keys =>
{
    keys.Add(p => p.hcadjId);
})
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
            <div title="Agregar Documentación" class="t-button" onclick="onAccioncatHCAdjunto('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px;" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_SelectEditingcatHCAdjunto", "HistoriaClinica", new { comId = -1 });
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        commands.Custom("cmdModificarcatHCAdjunto")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .SendDataKeys(false)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0 -336px;" })
            .HtmlAttributes(new { title = "Modificar datos de la Documentación Adjunta" })
            .SendState(false);
        commands.Custom("cmdDeletecatHCAdjunto")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .SendDataKeys(false)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
            .HtmlAttributes(new { title = "Eliminar Documentación" })
            .SendState(false);
    }).Width("110px").Title("Acciones");
columns.Bound(c => c.hcadjFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
columns.Bound(c => c.hcadjFecha).Format("{0:hh:mm:ss}").Width("80px").Title("Hora").Visible(true);
columns.Bound(c => c.hcadjObservaciones).Width("150px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
.ClientTemplate("<label title='<#= hcadjObservaciones #>' style='cursor: pointer;' ><#= hcadjObservaciones #></label>");
columns.Bound(c => c.hcadjArchivo).Width("150px").Title("Archivo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
.ClientTemplate("<#= hcadjVisualizar #>");
columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("150px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
.ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Usuario.usrApellidoyNombre #></label>");
})
.Editable(editing => editing
                            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
//.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingcatHCAdjunto").OnCommand("onCommandcatHCAdjunto").OnEdit("onCommandEditcatHCAdjunto").OnSave("onSavecatHCAdjunto").OnComplete("onCompletecatHCAdjunto"))
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingcatHCAdjunto").OnCommand("onCommandcatHCAdjunto").OnComplete("onCompletecatHCAdjunto"))
//.Filterable()
.Selectable()
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>

<!-- catHCAdjunto -->
<% Html.Telerik().Window()
        .Name("wcatHCAdjunto")
        .Title("catHCAdjunto")
        .Visible(false)
        .Content(() =>
        {
            %>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1024)
       .Height(400)
       .Render();
%>

<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("verPDF")
        .Title("Documentación")
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
