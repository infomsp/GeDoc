<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var _WindowCRUD;
    function onDataBindingproCompraDocumentacion(e) {
        var comId = -1;
        if (_DatosRegistro_proCompra != null) {
            comId = _DatosRegistro_proCompra.comId;
        }

        e.data = $.extend(e.data, { comId: comId });
    }
    function onCompleteproCompraDocumentacion(e) {
        if (e.name === "update" || e.name === "insert" || e.name === "delete") {
        }
    }
    
    function onCommandproCompraDocumentacion(e) {
        switch (e.name) {
            case "cmdModificarproCompraDocumentacion":
                if (e.dataItem['comaFechaDeEntrega'] != null) {
                    jAlert('Esta compra ya fué entregada, no se puede modificar.', '¡Atención!', function () {
                    });
                    $('#popup_container').focus();
                    $('#popup_ok').focus();
                } else {
                    onAccionDocumentacion("Modificar", e.dataItem['comaId']);
                }
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdDeleteproCompraDocumentacion":
                onAccionDocumentacion("Eliminar", e.dataItem['comaId']);
                e.preventDefault();
                e.stopPropagation();
                break;
            case "cmdCambioproCompraDocumentacion":
                onAccionDocumentacion("Recepción", e.dataItem['comaId']);
                e.preventDefault();
                e.stopPropagation();
                break;
                //case "cmdConsultarSeguimiento":
                //    onAccionDocumentacion("Consultar", e.dataItem['comaId']);
            //    e.preventDefault();
            //    e.stopPropagation();
            //    break;
        }
    }

    function onAccionDocumentacion(Accion, idDocumentacion) {
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

        var _WindowSeguimiento = $("#wCRUDDocumentacionCompras").data("tWindow");
        $('#wCRUDDocumentacionCompras').css({ 'height': Altura, 'width': 552 });
        $('#wCRUDDocumentacionCompras').find('div.t-window-content').css({ 'height': Altura-30, 'width': 540 });
        _WindowSeguimiento.content('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" /><strong> Cargando...</strong>');

        ////////////////////////////////////////
        var _Post = GetPathApp("proCompra/getCompraDocumentacion");
        $.ajax({
            url: _Post,
            data: { Accion: Accion, comaId: idDocumentacion, idCompra: _DatosRegistro_proCompra.comId },
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
                var _WindowCRUD = $("#wCRUDDocumentacionCompras").data("tWindow");
                _WindowCRUD.content(data);
            }
        });
        _WindowSeguimiento.center().title(Accion).open();

    }

    //function Descargar(e) {
    //    var _Post = GetPathApp('catPersona/Descargar');
    //    $.post(_Post, { Archivo: e });
    //}

</script>
<% ViewData["AltoEditor"] = "200px"; %>
<% var proCompraDocumentacionWS = new List<proCompraDocumentacionWS>(); %>
<!-- proCompraDocumentacion -->
<% Html.Telerik().Window()
        .Name("wproCompraDocumentacion")
        .Title("proCompraDocumentacion")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<proCompraDocumentacionWS>)proCompraDocumentacionWS)
            .Name("gridproCompraDocumentacion")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.comaId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <div title="Agregar Documentación" class="t-button" onclick="onAccionDocumentacion('Agregar', -1)" style="padding: 0px; min-width: 26px; min-height: 24px; display: <%=(Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Agregar Documentación")%>;" >
                            <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" height="16" width="16" style="padding: 0px; vertical-align: middle; margin-left: -3px; background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"]%>/sprite.png') no-repeat -48px -319px;" />
                        </div>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingproCompraDocumentacion", "proCompra", new { comId = -1 });
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Custom("cmdModificarproCompraDocumentacion")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0 -336px;" })
                        .HtmlAttributes(new { title = "Modificar datos de la Documentación Adjunta", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Modificar Documentación") })
                        .SendState(false);
                    commands.Custom("cmdDeleteproCompraDocumentacion")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
                        .HtmlAttributes(new { title = "Eliminar Documentación", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Eliminar Documentación") })
                        .SendState(false);
                    commands.Custom("cmdCambioproCompraDocumentacion")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -240px;" })
                        .HtmlAttributes(new { title = "Recepción", style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proCompra", "Recepción de Compra") })
                        .SendState(false);
                }).Width("110px").Title("Acciones");
            columns.Bound(c => c.Vencido).Width("25px").Title("").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<div title='<#= comaFechaDeEntrega == null ? (Vencido == \"SI\" ? \"Vencido\" : \"\") : \"Entregado\" #>' style='width: 100%; text-align: left;'><img src='" + Url.Content("~/Content") + "/Estados/<#= comaFechaDeEntrega == null ? \"Rojo(2).png\" : \"Verde-2.png\" #>' height='10px' width='10px' style='margin-right: 5px; <#= comaFechaDeEntrega == null ? (Vencido == \"SI\" ? \"\" : \"display: none;\") : \"\" #>' /></div>");
            columns.Bound(c => c.comaFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.comaFecha).Format("{0:hh:mm:ss}").Width("80px").Title("Hora").Visible(false);
            columns.Bound(c => c.Proveedor).Width("350px").Title("Proveedor").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Proveedor #>' style='cursor: pointer;' ><#= Proveedor #></label>");
            columns.Bound(c => c.comaFechaDeRetiro).Format("{0:dd/MM/yyyy}").Width("120px").Title("Fecha de Retiro").Visible(true);
            columns.Bound(c => c.comaPlazoDeEntrega).Width("120px").Title("Plazo de Entrega").Visible(true);
            columns.Bound(c => c.comaFechaDeVencimiento).Format("{0:dd/MM/yyyy}").Width("120px").Title("Vencimiento").Visible(true);
            columns.Bound(c => c.comaFechaDeEntrega).Format("{0:dd/MM/yyyy}").Width("120px").Title("Fecha de Recepción").Visible(true);
            columns.Bound(c => c.comaRenglones).Width("150px").Title("Renglones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= comaRenglones #>' style='cursor: pointer;' ><#= comaRenglones #></label>");
            columns.Bound(c => c.comaArchivo).Width("150px").Title("Archivo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<a class='t-arrow-down' href='" + Url.Content("~/") + "proCompra/DescargarAdjunto?Archivo=<#= comaArchivo #>' title='Click para descargar archivo' style='cursor: pointer;' >Click aquí para descargar el archivo</a>");
            columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("150px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Usuario.usrApellidoyNombre #></label>");
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            //.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingproCompraDocumentacion").OnCommand("onCommandproCompraDocumentacion").OnEdit("onCommandEditproCompraDocumentacion").OnSave("onSaveproCompraDocumentacion").OnComplete("onCompleteproCompraDocumentacion"))
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingproCompraDocumentacion").OnCommand("onCommandproCompraDocumentacion").OnComplete("onCompleteproCompraDocumentacion"))
            //.Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Render();
                %>
            </div>
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
