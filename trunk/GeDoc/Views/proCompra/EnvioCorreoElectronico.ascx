<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc.Models" %>

<script type="text/javascript">
    function validaCheckBoxProveedor(IdSeleccionado, cbxSel) {
        if ($('#gridgetProveedoresWS').data("tGrid").total == 0) {
            return;
        }
        //Guardamos el estado
        var _Post = GetPathApp('proCompra/CheckedProveedor');
        $.ajax({
            url: _Post,
            data: { Id: IdSeleccionado, pChecked: cbxSel.checked },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                jAlert('Falló el acceso al servidor', '¡Atención!');
            },
            success: function (data) {
                if (data) {
                }
            }
        });
    }
    
    function validaCheckBoxContacto(IdSeleccionado, cbxSel) {
        if ($('#gridgetProveedoresWS').data("tGrid").total == 0) {
            return;
        }
        //Guardamos el estado
        var _Post = GetPathApp('proCompra/CheckedContacto');
        $.ajax({
            url: _Post,
            data: { Id: IdSeleccionado, pChecked: cbxSel.checked },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                jAlert('Falló el acceso al servidor', '¡Atención!');
            },
            success: function (data) {
                if (data) {
                }
            }
        });
    }
    
    function onAceptarEnvioCorreo() {
        var _Post = GetPathApp('proCompra/setEnvioCorreos');
        AbrirWaiting();
        $.ajax({
            type: "POST",
            url: _Post,
            data: { comId: _DatosRegistro_proCompra.comId },
            error: function () {
                CerrarWaiting();
                jAlert("Error al guardar datos.", "Error...", function () {
                    $("form:not(.filter) :input:visible:enabled:first").focus().select();
                });
            },
            success: function (respuesta) {
                CerrarWaiting();
                if (respuesta.Error) {
                    var FocusControl = respuesta.Foco;
                    jAlert(respuesta.Mensaje, "Error...", function () {
                        $("#" + FocusControl)[0].focus().select();
                    });
                } else {
                    $("#gridSeguimiento").data("tGrid").ajaxRequest();
                    jAlert(respuesta.Mensaje, "Información...", function () {
                        var windowElement = $('#wEnvioCorreoElectronico').data('tWindow');
                        windowElement.close();
                    });
                }
            }
        });
    }

    function onCancelarEnvioCorreo() {
        var windowEnvio = $('#wEnvioCorreoElectronico').data('tWindow');
        windowEnvio.close();
    }
</script>

<div>
<% Html.Telerik().Grid<getProveedoresWS>()
.Name("gridgetProveedoresWS")
.DataKeys(keys =>
{
    keys.Add(p => p.Id);
})
.Localizable("es-AR")
.DataBinding(dataBinding =>
{
    dataBinding.Ajax()
        .Select("onDataBinding_getProveedoresWS", "proCompra");
})
.Columns(columns =>
{
    columns.Bound(c => c.provSeleccionado).Width("70px").Visible(true).Title("Enviar")
        .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' <#= provSeleccionado ? 'checked: checked' : '' #> onclick='validaCheckBoxProveedor(<#= Id #>, this);' style='cursor: pointer; display: <#= Seleccion && provCorreoElectronico != null && provCorreoElectronico != \"\" ? \"inline-block\" : \"none\" #>;' /></div>");
    columns.Bound(c => c.provRazonSocial).Width("250px").Title("Proveedor").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='<#= provRazonSocial #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provRazonSocial #> </label>");
    columns.Bound(c => c.provRubro).Width("250px").Title("Rubro").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='<#= provRubro #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provRubro #> </label>");
    columns.Bound(c => c.provCorreoElectronico).Width("250px").Title("Correo Electrónico").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='<#= provCorreoElectronico #>' style='cursor: pointer;' style='white-space: nowrap;'><#= provCorreoElectronico #> </label>");
    columns.Bound(c => c.pcSeleccionado).Width("70px").Visible(true).Title("Enviar")
        .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' <#= pcSeleccionado ? 'checked: checked' : '' #> onclick='validaCheckBoxContacto(<#= Id #>, this);' style='cursor: pointer; display: <#= pcCorreoElectronico != null && pcCorreoElectronico != \"\" ? \"inline-block\" : \"none\" #>;' /></div>");
    columns.Bound(c => c.pcApellidoyNombre).Width("250px").Title("Contacto").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='<#= pcApellidoyNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= pcApellidoyNombre #> </label>");
    columns.Bound(c => c.pcCorreoElectronico).Width("250px").Title("Correo Electrónico").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
        .ClientTemplate("<label title='<#= pcCorreoElectronico #>' style='cursor: pointer;' style='white-space: nowrap;'><#= pcCorreoElectronico #> </label>");
})
.Pageable((paging) =>
                    paging.Enabled(true)
                        .PageSize(((int)Session["FilasPorPagina"])))
.Footer(true)
//.ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding_getProveedoresWS"))
.Selectable()
.Sortable()
.Filterable()
.Resizable(resizing => resizing.Columns(true))
.Scrollable(scroll => scroll.Enabled(true).Height(380))
.Render();
    %>
    <div style="text-align: center; margin-top: 10px;">
        <div id="btnAceptarEnvio" class="t-button" onclick="onAceptarEnvioCorreo();" title="Confirmar">
            <img src="<%= Url.Content("~/Content/General/Vacio-Transparente.png") %>" alt="Aceptar" height="18" width="18" style="background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] %>/sprite.png') no-repeat -32px -336px; vertical-align: middle;" />
            <label style="cursor: pointer;">Aceptar</label>
        </div>
        <div id="btnCancelarEnvio" class="t-button" onclick="onCancelarEnvioCorreo();" title="Cancelar">
            <img src="<%= Url.Content("~/Content/General/Vacio-Transparente.png") %>" alt="Cancelar" height="18" width="18" style="background: url('<%= Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] %>/sprite.png') no-repeat -46px -336px;  vertical-align: middle;" />
            <label style="cursor: pointer;">Cancelar</label>
        </div>
    </div>
</div>
