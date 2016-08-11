<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    function onAceptarCambioEstado() {
        var Estado = $('#cbEstados').data('tDropDownList');
        if (Estado.value().toString() == '') {
            jAlert("Debe seleccionar el Nuevo Estado", "¡Atención!", function () {
                Estado.focus();
            });
            return;
        }
        $('#wCambioDeEstado').data("tWindow").close();
        AbrirWaiting();
        debugger;
        var _NuevoEstado = Estado.value().toString();
        var _Post = GetPathApp('catRequerimiento/CambiarEstado');
        $.ajax({
            url: _Post,
            data: { reqId: _DatosRegistro_catRequerimiento.reqId, pEstadoNuevo: _NuevoEstado, pObservaciones: $("#txtObservaciones").val() },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert('No se pudo cambiar el estado seleccionado. Vuelva a intentarlo por favor.', '¡Atención!');
            },
            success: function (data) {
                if (data) {
                    jAlert('Se cambió el Estado en forma correcta.', 'Información...', function () {
                        Refrescar();
                    });
                } else {
                    jAlert('Falló la asignación de estado del Requerimiento', '¡Atención!');
                }
            }
        });
    }
    function onCancelarCambioEstado() {
        $('#wCambioDeEstado').data("tWindow").close();
    }
    
    function onActivateCambioDeEstado(e) {
        var combobox = $('#cbEstados').data('tDropDownList');

        debugger;
        var dataSource =
                [
                    { Text: "Pendiente", Value: "PE", Selected: true },
                    { Text: "Anulado", Value: "AN", Selected: false }
                ];
        switch (_DatosRegistro_catRequerimiento.Estado.Tipo.tipoCodigo) {
            case "SO":
                var dataSource =
                        [
                            { Text: "Pendiente", Value: "PE", Selected: true },
                            { Text: "Anulado", Value: "AN", Selected: false }
                        ];
                break;
            case "PE":
                var dataSource =
                        [
                            { Text: "En Proceso", Value: "EP", Selected: true },
                            { Text: "Anulado", Value: "AN", Selected: false }
                        ];
                break;
            case "EP":
                var dataSource =
                        [
                            { Text: "Terminado", Value: "TE", Selected: true },
                            { Text: "Implementado", Value: "IM", Selected: false }
                        ];
                if (_DatosRegistro_catRequerimiento.Tipo.tipoCodigo == "TA") {
                    var dataSource =
                            [
                                { Text: "Terminado", Value: "TE", Selected: true }
                            ];
                    break;
                }
                break;
            case "TE":
                var dataSource =
                        [
                            { Text: "Implementado", Value: "IM", Selected: false }
                        ];
                break;
        }
        
        combobox.dataBind(dataSource);

        $("#txtObservaciones").val("");
        $("#txtObservaciones")[0].focus();
    }
</script>
<!-- Cambiar Estado -->
<% Html.Telerik().Window()
        .Name("wCambioDeEstado")
        .Title("Cambio de Estado")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div style="height: 120px;">
                <div id="divCambioEstado" class="BordeRedondeado" tabindex="0" style="overflow: hidden; outline: none; padding: 10px; margin: 10px;" >
                    <table>
                        <tr>
                            <td>
                                <label style="margin-left: 5px; vertical-align: middle;">Nuevo Estado:</label>
                                <%= Html.Telerik().DropDownList()
                                    .Name("cbEstados")
                                    .DropDownHtmlAttributes(new { style = "width: 350px;" })
                                    .SelectedIndex(0)
                                    .HtmlAttributes(new { style = "width: 350px; vertical-align: middle;" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divObservaciones">
                                    <label style="margin-left: 5px; vertical-align: top;">Observaciones:</label>
                                    <%= Html.TextArea("txtObservaciones", "", 0, 0, new { @class = "text-box multi-line", @style = "width: 338px;" }) %>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: center; margin-top: 20px; margin-bottom: 20px;">
<%-- ReSharper disable UnknownCssClass --%>
                    <button class="t-button t-grid-update" onclick="onAceptarCambioEstado();">Aceptar</button>
                    <button class="t-button t-grid-cancel" onclick="onCancelarCambioEstado();">Cancelar</button>
<%-- ReSharper restore UnknownCssClass --%>
                </div>
            </div>
            <%
        })
       //.Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       //.Width(505)
       .Height(220)
       .ClientEvents(events => events.OnActivate("onActivateCambioDeEstado"))
       .Render();
%>

