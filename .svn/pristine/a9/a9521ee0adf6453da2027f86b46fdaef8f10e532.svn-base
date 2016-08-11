<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    function onCancelarRepetirTurnos() {
        var windowElement = $("#wRepetirTurno").data("tWindow");
        windowElement.close();
    }

    function onAceptarRepetirTurnos() {
        if ($("#txtCantidadDias").attr("disabled") === "disabled") {
            return;
        }

        jConfirm("¿Confirma Repetición de este Turno por " + $("#txtCantidadDias").val() + " días?", "Repetir...", function (r) {
            if (r) {
                AbrirWaitingCRUD("wtTurnosAmbulatorios");
                $.post("<%=Url.Content("~/catTurno/setRepetirTurno")%>", { pturId: _DatosRegistroTurno["turId"], pDias: $("#txtCantidadDias").val() }, function (data) {
                    Refrescar();
                    CerrarWaitingCRUD("wtTurnosAmbulatorios");
                    if (data.IsValid) {
                        var gridTurno = $("#gridRepetirTurnos").data("tGrid");
                        gridTurno.dataBind(data.Datos);
                        gridTurno.total = data.Datos.length;
                        $("#txtCantidadDias").data("tTextBox").disable();
                        $("#btnAceptarRepetirTurnos").css("color", "darkgray");
                        jAlert("Revise el detalle de turnos generados.", "¡Atención!");
                    } else {
                        jAlert(data.Mensaje, "¡Error!");
                    }
                });
            }
        });
    }

    //Seleccionamos el primer campo
    function onActivateRepetirTurnos(e) {
        //Aplicamos algunos estilos
        $(".t-formatted-value").attr("style", "width: 120px;");
        $("#btnAceptarRepetirTurnos").css("color", "black");
        $("#txtCantidadDias").data("tTextBox").enable();
        $("#txtCantidadDias").val(1);
        $("#txtCantidadDias")[0].focus();
        var gridTurno = $("#gridRepetirTurnos").data("tGrid");
        gridTurno.dataBind(null);
    }
</script>

<% Html.Telerik().Window()
        .Name("wRepetirTurno")
        .Title("Repetir Turno...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <div id="divRepeticionTurno" >
                    <label style="margin-left: 5px;">Cantidad de Días:</label>
                    <%: Html.Telerik().NumericTextBox()
                            .Name("txtCantidadDias")
                            .DecimalDigits(0)
                            .MinValue(1)
                            .Spinners(false)
                            .HtmlAttributes(new { style = "width: 130px;" })
                            .EmptyMessage("")
                            .Value(1)
                            .InputHtmlAttributes(new { style = "width: 130px; text-align: right; margin-right: 5px;" })
                            %>
                    <div id="btnAceptarRepetirTurnos" class="t-button" onclick="onAceptarRepetirTurnos();" title="Generar turnos" style="margin-left: 15px;">
                        <img src="<%= Url.Content("~/Content/General/Vacio-Transparente.png")%>" alt="Generar" height="18" width="18" style="background: url('<%=Url.Content("~/Content/" + Session["Version"] + "/" + Session["Estilo"])%>/sprite.png') no-repeat -32px -336px; vertical-align: middle;" />
                        <label style="cursor: pointer;">Generar</label>
                    </div>
                </div>
            </div>
            <div class="BordeRedondeadoTab">
                Detalle de Turnos Otorgados
            </div>
            <div class="BordeRedondeado BordesGrupoCRUD" style="margin-left: 0%; margin-top: 0px; padding: 8px; width: 97.9%; height: 387px;">
            <% Html.Telerik().Grid<GeDoc.catTurnos>()
                .Name("gridRepetirTurnos")
                .Localizable("es-AR")
                .DataKeys(keys =>
                {
                    keys.Add(p => p.turId);
                })
                .Columns(columns =>
                    {
                        columns.Bound(c => c.turEstadoImagen).Width("20px").Title("").Visible(true)
                        .ClientTemplate("<div style='width: 100%; text-align: left; vertical-align:middle;'><img src='" + Url.Content("~/Content") + "/Estados/<#= turEstadoImagen #>' height='15' width='15' style='vertical-align:middle;' /></div>");
                        columns.Bound(c => c.turFecha).Width("70px").Title("Fecha").Visible(true).Format("{0:dd/MM/yyyy}").HtmlAttributes(new { style = "text-align: center;" });
                        columns.Bound(c => c.turFecha).Width("40px").Title("Hora").Visible(true).Format("{0:HH:mm}").HtmlAttributes(new { style = "text-align: center;" });
                        columns.Bound(c => c.tipoDescripcion).Width("350px").Title("Estado").Visible(true);
                })
                .Pageable((paging) => paging.Enabled(true).PageSize(50))
                .Selectable()
                .Footer(false)
                .Scrollable(scroll => scroll.Enabled(true).Height(360))
                .Render();
                %>
            </div>
            <div style="text-align: center; margin-top: 10px;">
                <div class="t-button" onclick="onCancelarRepetirTurnos();" title="Cerrar ventana">
                    <img src="<%= Url.Content("~/Content/General/Vacio-Transparente.png")%>" alt="Cancelar" height="18" width="18" style="background: url('<%=Url.Content("~/Content/" + Session["Version"] + "/" + Session["Estilo"])%>/sprite.png') no-repeat -46px -336px;  vertical-align: middle;" />
                    <label style="cursor: pointer;">Cerrar</label>
                </div>
            </div>
        <%})
       .Buttons(b => b.Clear())
       .Modal(true)
       .Scrollable(false)
       .Width(760)
       .Height(520)
       .ClientEvents(eventos => eventos.OnActivate("onActivateRepetirTurnos"))
       .Render();
%>
