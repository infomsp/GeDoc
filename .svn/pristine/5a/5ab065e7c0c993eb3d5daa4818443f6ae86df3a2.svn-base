<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var IdCarnet = -1;

    function onCommandEdit_catCarnet(e) {
        _WindowCRUD = $("#gridcatCarnetPopUp").data("tWindow");
        onCommandEdit(e);
    }
    function onDataBinding_catCarnet(e) {
    }
    
    function onCommand_catCarnet(e) {
        switch (e.name) {
        case "cmdModificar_catCarnet":
            onAccion_catCarnet("Modificar", e.dataItem['carId']);
            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdEliminar_catCarnet":
            onAccion_catCarnet("Eliminar", e.dataItem['carId']);
            e.preventDefault();
            e.stopPropagation();
            break;
        case "cmdConsultar_catCarnet":
            onAccion_catCarnet("Consultar", e.dataItem['carId']);
            e.preventDefault();
            e.stopPropagation();
            break;
        }
    }
    
    function onAccion_catCarnet(pAccion, idClinicaMedica) {
        switch (pAccion) {
        case "Agregar":
            break;
        case "Modificar":
            break;
        case "Eliminar":
            break;
        case "Consultar":
            break;
        }

        IdCarnet = -1;
        var wCliniMedica = $("#wCRUDcatCarnet").data("tWindow");
        wCliniMedica.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');
        wCliniMedica.ajaxRequest(GetPathApp("registroProfesional/getcatCarnet") + "?pAccion=" + pAccion + "&carId=" + idClinicaMedica);
        if (wCliniMedica.isMaximized) {
            wCliniMedica.center().title(pAccion).open();
        }
        else {
            wCliniMedica.center().title(pAccion).maximize().open();
        }
    }

    function onClose_wCRUDcatCarnet(e) {
        FrenarVideo();
    }

    function onImprimirFrentecatCarnet() {
        var _url = GetPathApp('registroProfesional/setImpresionCarnet');
        AbrirWaitingCRUD();
        $.ajax({
            type: "POST",
            url: _url,
            data: { pIdCarnet: IdCarnet },
            error: function () {
                CerrarWaitingCRUD();
                jAlert("Error al guardar datos.", "Error...", function () {
                    $("form:not(.filter) :input:visible:enabled:first").focus().select();
                });
            },
            success: function (respuesta) {
                CerrarWaitingCRUD();
                if (respuesta.Error) {
                    jAlert(respuesta.Mensaje, "Error...", function () {
                        var windowElement = $('#wCRUDcatCarnet').data('tWindow');
                        windowElement.close();
                        var windowPrint = $('#wImprimircatCarnet').data('tWindow');
                        windowPrint.close();
                    });
                } else {
                    var _Parametros = new Array(new Array(IdCarnet, 'idCarnet'));
                    InvocarReporte('rptCarnetFrente', _Parametros);
                    $("#gridcatCarnet").data("tGrid").ajaxRequest();
                    $("#btnFrenteCarnet").hide();
                    $("#btnDorsoCarnet").show();
                }
            }
        });
    }
    function onImprimirDorsocatCarnet() {
        var windowElement = $('#wCRUDcatCarnet').data('tWindow');
        windowElement.close();
        var windowPrint = $('#wImprimircatCarnet').data('tWindow');
        windowPrint.close();
        var _Parametros = new Array(new Array(IdCarnet, 'idCarnet'));
        InvocarReporte('rptCarnetBack', _Parametros);
    }
</script>

<%
    ViewData["vd_catCarnet"] = new List<catCarnetWS>();
    %>
<div>
<% Html.Telerik().Grid((IEnumerable<catCarnetWS>)ViewData["vd_catCarnet"])
.Name("gridcatCarnet")
.DataKeys(keys =>
{
    keys.Add(p => p.carId);
})
.Localizable("es-AR")
.ToolBar(commands =>
{
    commands.Template(cmdTemplate =>
        {
            %>
    <%--display: <%= (Session["Permisos"] as GeDoc.Acciones).Visibilidad("registroProfesional", "Agregar")%>--%>
            <div title="Agregar" class="t-button" onclick="onAccion_catCarnet('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px;" >
                <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
            </div>
            <label id="lblPaciente" style="font-size: 14px; font-weight: bold; text-align: justify; margin-left: 260px; vertical-align: middle;"></label>
            <%
        });
})
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("_Binding_catCarnet", "registroProfesional");
})
.Columns(columns =>
{
    columns.Command(commands =>
    {
        //commands.Custom("cmdModificar_catCarnet")
        //    .Ajax(true)
        //    .ButtonType(GridButtonType.Image)
        //    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
        //    .HtmlAttributes(new { title = "Modificar"/*, style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("registroProfesional", "Modificar")*/ });
        //commands.Custom("cmdEliminar_catCarnet")
        //    .Ajax(true)
        //    .ButtonType(GridButtonType.Image)
        //    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
        //    .HtmlAttributes(new { title = "Eliminar", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("registroProfesional", "Eliminar") });
        commands.Custom("cmdConsultar_catCarnet")
            .Ajax(true)
            .ButtonType(GridButtonType.Image)
            .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
            .HtmlAttributes(new { title = "Consultar" });
    }).Width("60px").Title("Acciones");
    columns.Bound(c => c.carId).Width("60px").Title("Código").Visible(true).HtmlAttributes(new {style = "white-space: nowrap;"});
    columns.Bound(c => c.carFecha).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha Alta").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
    columns.Bound(c => c.tipoDocumento).Width("100px").Title("Tipo Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= tipoDocumento #>' style='cursor: pointer;' style='white-space: nowrap;'><#= tipoDocumento #> </label>");
    columns.Bound(c => c.carNumeroDocumento).Width("90px").Title("Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
    columns.Bound(c => c.carApellido).Width("150px").Title("Apellido").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= carApellido #>' style='cursor: pointer;' style='white-space: nowrap;'><#= carApellido #> </label>");
    columns.Bound(c => c.carNombre).Width("150px").Title("Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= carNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= carNombre #> </label>");
    columns.Bound(c => c.Profesion).Width("150px").Title("Profesión").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
    .ClientTemplate("<label title='<#= Profesion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Profesion #> </label>");
    columns.Bound(c => c.catMatriculaProfesional).Width("90px").Title("Matricula").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
    columns.Bound(c => c.carVencimiento).Format("{0:dd/MM/yyyy}").Width("90px").Title("Vencimiento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
    columns.Bound(c => c.carFechaImpresion).Format("{0:dd/MM/yyyy}").Width("90px").Title("Última Impresión").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
    columns.Bound(c => c.carCandidadImpresiones).Width("90px").Title("Cant. Impresiones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding_catCarnet").OnCommand("onCommand_catCarnet"))
.Filterable()
.Selectable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(310))
.Sortable()
.Render();
    %>
</div>

<% Html.Telerik().Window()
        .Name("wCRUDcatCarnet")
        .Title("Acción")
        .Visible(false)
        .Content(() =>
        {})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .ClientEvents(ev => ev.OnClose("onClose_wCRUDcatCarnet"))
       //.Width(1000)
       //.Height(510)
       .Render();
%>

<% Html.Telerik().Window()
        .Name("wImprimircatCarnet")
        .Title("Impresión...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div class="alert-info BordeRedondeado" style="text-align: center;">
                <span style="cursor: pointer;"><i style="color: cornflowerblue;" class="fa fa-4x fa-print"></i></span>
                <label style="cursor: pointer; vertical-align: super; font-size: large;">Impresión del Carnet</label>
            </div>
            <hr/>
            <div style="text-align: center; margin-top: 30px; margin-bottom: 30px;">
                <div id="btnFrenteCarnet" style="width: 140px;" class="t-button" onclick="onImprimirFrentecatCarnet();" title="Imprimir frente del carnet.">
                    <span style="cursor: pointer;"><i style="color: cornflowerblue;" class="fa fa-2x fa-credit-card"></i></span>
                    <label style="cursor: pointer; vertical-align: super;">Frente</label>
                </div>
                <div id="btnDorsoCarnet" style="width: 140px; display: none;" class="t-button" onclick="onImprimirDorsocatCarnet();" title="Imprimir dorso del carnet.">
                    <span style="cursor: pointer;"><i style="color: cornflowerblue;" class="fa fa-2x fa-qrcode"></i></span>
                    <label style="cursor: pointer; vertical-align: super;">Dorso</label>
                </div>
            </div>
            <hr/>

            <%
        })
       .Buttons(b => b.Clear())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(400)
       .Height(210)
       .Render();
%>

