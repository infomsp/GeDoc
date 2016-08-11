<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    function onAceptarCambioEstadosJM() {
        var Estado = $('#cbEstadosJM').data('tDropDownList');
        if (Estado.value().toString() == '') {
            jAlert("Debe seleccionar el Nuevo Estado", "¡Atención!", function () {
                Estado.focus();
            });
            return;
        }
        AbrirWaiting();
        
        debugger;
        var _NuevoEstado = Estado.value().toString();
        var _Post = GetPathApp('JuntaMedica/CambiarEstado');
        $.ajax({
            url: _Post,
            data: { pIdCompra: _DatosRegistro_TurnosJuntaMedica.turId, pEstadoNuevo: _NuevoEstado },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert('No se pudieron cambiar los estados seleccionados. Vuelva a intentarlo por favor.', '¡Atención!');
            },
            success: function (data) {
                CerrarWaiting();
                if (data) {
                    jAlert('Se cambió el Estado en forma correcta.', 'Información...', function () {
                        $('#wCambioDeEstadoJM').data("tWindow").close();
                        var grid = $('#Grid').data("tGrid");
                        grid.rebind();
                    });
                } else {
                    jAlert('Falló la asignación de estados.', '¡Atención!');
                }
            }
        });
    }
    function onCancelarCambioEstadosJM() {
        $('#wCambioDeEstadoJM').data("tWindow").close();
    }
    
    function onActivateCambioDeEstadoJM(e) {
        //$("#txtObservaciones")[0].focus();
    }
</script>
<!-- Cambiar Estado -->
<% Html.Telerik().Window()
        .Name("wCambioDeEstadoJM")
        .Title("Cambio de Estado")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div style="height: 120px;">
                <div class="BordeRedondeado" tabindex="0" style="overflow: hidden; outline: none; padding: 10px; margin: 10px;" >
                    <table>
                        <tr>
                            <td>
                                <label style="margin-left: 5px; vertical-align: middle;">Nuevo Estado:</label>
                                <%= Html.Telerik().DropDownList()
                                    .Name("cbEstadosJM")
                                    .DropDownHtmlAttributes(new { style = "width: 350px;" })
                                    .SelectedIndex(0)
                                    .BindTo((SelectList)ViewData["EstadosJM"])
                                    .HtmlAttributes(new { style = "width: 350px; vertical-align: middle;" })%>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                <div id="divObservaciones">
                                    <label style="margin-left: 5px; vertical-align: top;">Observaciones:</label>
                                    <%= Html.TextArea("txtObservaciones", "", 0, 0, new { @class = "text-box multi-line", @style = "width: 338px;" }) %>
                                </div>
                            </td>
                        </tr>--%>
                    </table>
                </div>
                <div style="text-align: center; margin-top: 20px; margin-bottom: 20px;">
<%-- ReSharper disable UnknownCssClass --%>
                    <button class="t-button t-grid-update" onclick="onAceptarCambioEstadosJM();">Aceptar</button>
                    <button class="t-button t-grid-cancel" onclick="onCancelarCambioEstadosJM();">Cancelar</button>
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
       .Height(210)
       .ClientEvents(events => events.OnActivate("onActivateCambioDeEstadoJM"))
       .Render();
%>

