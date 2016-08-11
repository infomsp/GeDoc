<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    function onAceptarCambioAdmision() {
        var Estado = $("#cbTiposAdmision").data("tDropDownList");
        if (Estado.value().toString() === "") {
            jAlert("Debe seleccionar el un tipo de Admisión", "¡Atención!", function () {
                Estado.focus();
            });
            return;
        }
        AbrirWaiting();
        var _NuevaAdmision = Estado.value();
        var _Post = "<%=Url.Content("~/catTurno/setCambioDeAdmision")%>";
        $.ajax({
            url: _Post,
            data: { idTurno: _DatosRegistroTurno["turId"], pNuevaAdmision: parseInt(_NuevaAdmision) },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert("No se pudo cambiar el tipo de admisión seleccionado. Vuelva a intentarlo por favor.", "¡Atención!");
            },
            success: function (data) {
                CerrarWaiting();
                if (data.IsValid) {
                    Refrescar();
                    $("#wCambioDeAdmision").data("tWindow").close();
                    $(".t-overlay").fadeOut();
                } else {
                    jAlert("Falló el cambio de tipo de Admisión." + data.Mensaje, "¡Atención!");
                }
            }
        });
    }
    function onCancelarCambioAdmision() {
        $("#wCambioDeAdmision").data("tWindow").close();
    }

    function wCambioDeAdmision_OnOpen() {
        var ddl = $("#cbTiposAdmision").data("tDropDownList");
        ddl.reload(function () {
            ddl.select(0);
        });      
    }

    function cbTiposAdmision_OnDataBinding(e) {        
        e.data = $.extend({}, e.data, {
            estadoTurnoId: typeof _DatosRegistroTurno !== "undefined" ? _DatosRegistroTurno.tipoId_Admision : -1,
            tipoAdmisionId: typeof _DatosRegistroTurno !== "undefined" ? _DatosRegistroTurno.tipoId : -1
        });
    }

</script>
<!-- Cambiar Estado -->
<% Html.Telerik().Window()
        .Name("wCambioDeAdmision")
        .Title("Cambio de Admisión...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <div class="BordeRedondeado" tabindex="0" style="overflow: hidden; outline: none; padding: 10px; margin: 10px;" >
                    <table>
                        <tr>
                            <td>
                                <label style="margin-left: 5px; vertical-align: middle;">Tipo de Admisión:</label>
                                 <%=     
                                    Html.Telerik().DropDownList()
                                    .Name("cbTiposAdmision")
                                    .DropDownHtmlAttributes(new {style = "width: 350px;"})
                                    .SelectedIndex(0)
                                    //.BindTo((SelectList) ViewData["tipoId_Admision_Data"])
                                    .DataBinding(b => b.Ajax().Select("_GetComboBoxTipoAdmision","catTurno"))
                                    .HtmlAttributes(new {style = "width: 350px; vertical-align: middle;"})
                                    .ClientEvents(e => e.OnDataBinding("cbTiposAdmision_OnDataBinding"))
                                %>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: center; margin-top: 20px; margin-bottom: 20px;">
                    <button class="t-button t-grid-update" onclick="onAceptarCambioAdmision();">Aceptar</button>
                    <button class="t-button t-grid-cancel" onclick="onCancelarCambioAdmision();">Cancelar</button>
                </div>
            </div>
            <%
        })
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .ClientEvents(events => events.OnOpen("wCambioDeAdmision_OnOpen"))
       .Render();
%>