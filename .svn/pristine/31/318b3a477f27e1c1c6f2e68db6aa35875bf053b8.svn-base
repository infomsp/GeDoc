<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% 
    Html.RenderPartial("WaitingCRUDModel", "wtTurnosRP");
    ViewBag.EsCentroDeAtencion = true;
    ViewBag.CentroDeAtencionMensaje = "Atención de Profesionales";
    ViewBag.CentroDeAtencionEntidad = "Profesionales";
   %>
<style>
</style>

<script type="text/javascript" >
    var _DatosRegistroTurnoRP;
    var ConsultorioSeleccionado = { csscId: 0, csscNombre: "" };
    var SalaSeleccionada = { cssId: 0, cssNombre: "" };
    var _WindowCRUD;

    function Refrescar() {
        var grid = $("#gridTurnosRP").data("tGrid");
        grid.ajaxRequest();
    }

    function onRowSelectedTurnoRP(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);
        _DatosRegistroTurnoRP = dataItem;
    }

    function onDataBindingTurnosRP(args) {
        AbrirWaitingCRUD("wtTurnosRP");
        var grid = $(this).data("tGrid");
        args.data = $.extend(args.data, { Filtro: (grid.filterBy || '~') });
    }

    function onCommandTurnosRP(e) {
        switch (e.name) {
            case "cmdRellamado":
                if (e.dataItem["pturEstado"] !== "Llamado") {
                    jAlert("No puede volver a llamar a este Profesional", "¡Error!");
                    e.preventDefault();
                    e.stopPropagation();
                    return;
                }

                //Es importante que la fecha sea la del servidor y no la del cliente no usar ni jQuery.now() ni nigún
                //método que tome la fecha del cliente, por que si el cliente tiene mal la fecha de su PC, genera problemas
                if (("<%= DateTime.Now.Date.ToString("dd/MM/yyyy")%>") != e.dataItem["pturFecha"].toLocaleString("es-ar", { day: "2-digit", month: "2-digit", year: "numeric" })) {
                    jAlert("No se puede volver a llamar a Profesionales con turnos distintos al día de hoy.", "¡Error!");
                    e.preventDefault();
                    e.stopPropagation();
                    return;
                }

                LlamarProximoProfesional(e.dataItem["pturId"]);
                break;
            case "cmdFinAtencion":
                if (e.dataItem["pturEstado"] !== "Atendiendo") {
                    jAlert("Este profesional no está siendo atendido.", "¡Error!");
                    e.preventDefault();
                    e.stopPropagation();
                    return;
                }

                FinalizarAtencion(e.dataItem["pturId"]);

                break;
            }
        e.preventDefault();
        e.stopPropagation();
    }

    function onCompleteTurnosRP(e) {
        debugger;
        if (e.name === "dataBinding") {
            CerrarWaitingCRUD("wtTurnosRP");
            $("#divProcesandowtTurnosRP").css("display", "none");
        }
    }

    function onErrorTurnoRP(e) {
        //the current XMLHttpRequest object
        var xhr = e.XMLHttpRequest;
        //the text status of the error - "timeout", "error" etc.
        var status = e.textStatus;

        if (status == "error") {
            _Error = true;
        }
        else {
            _Error = false;
        }
    }

    function LlamarProximoProfesional(pturId) {
        var grid = $("#gridTurnosRP").data("tGrid");

        if (grid.total === 0) {
            jAlert("No hay profesionales sin atender.", "¡Error!");
            return;
        }

        AbrirWaitingCRUD("wtTurnosRP");
        var _Post = "<%=Url.Content("~/registroProfesional/getProximoProfesional")%>";
        $.ajax({
            url: _Post,
            data: { Consultorio: ConsultorioSeleccionado.csscId, pturId: pturId },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaitingCRUD("wtTurnosRP");
                jAlert("Falló el llamado del próximo paciente", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaitingCRUD("wtTurnosRP");
                if (data.Mensaje === "OK") {
                    debugger;
                    //Busco que fila es la seleccionada\\
                    var Item;
                    $.each(grid.data, function (index, datos) {
                        Item = datos;
                        if (Item.pturId == data.turId) {
                            return false;
                        }
                    });
                    _DatosRegistroTurnoRP = Item;
                    jConfirm("Comienza atención de " + _DatosRegistroTurnoRP.pturApellidoyNombre + " ?", "Llamado ...", function (Opcion) {
                        if (Opcion) {
                            IniciarAtencion(_DatosRegistroTurnoRP.pturId);
                        } else {
                            Refrescar();
                        }
                    });
                } else {
                    jAlert("Falló el llamado del próximo profesional. " + data.Mensaje, "¡Error!");
                    return;
                }
            }
        });
    }

    function FinalizarAtencion(pturId) {
        AbrirWaitingCRUD("wtTurnosRP");
        var _Post = "<%=Url.Content("~/registroProfesional/setFinalizarAtencion")%>";
        $.ajax({
            url: _Post,
            data: { pturId: pturId },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaitingCRUD("wtTurnosRP");
                jAlert("Falló la Finalización de Atención", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaitingCRUD("wtTurnosRP");
                if (data) {
                    Refrescar();
                } else {
                    jAlert("Falló la Finalización de Atención. ", "¡Error!");
                    return;
                }
            }
        });
    }

    function IniciarAtencion(pturId) {
        AbrirWaitingCRUD("wtTurnosRP");
        var _Post = "<%=Url.Content("~/registroProfesional/setIniciarAtencion")%>";
        $.ajax({
            url: _Post,
            data: { Consultorio: ConsultorioSeleccionado.csscId, pturId: pturId },
            type: "POST",
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaitingCRUD("wtTurnosRP");
                jAlert("Falló el Inicio de Atención", "¡Error!");
                $("#popup_container").focus();
                $("#popup_ok").focus();
            },
            success: function (data) {
                CerrarWaitingCRUD("wtTurnosRP");
                if (data) {
                    Refrescar();
                } else {
                    jAlert("Falló el Inicio de Atención. ", "¡Error!");
                    return;
                }
            }
        });
    }

    function onActivate_wSeleccionDeConsultorio() {
        ConsultorioSeleccionado.csscId = 0;
        ConsultorioSeleccionado.csscNombre = "";
        SalaSeleccionada.cssId = 0;
        SalaSeleccionada.cssNombre = "";
        $(".t-overlay").css("display", "block");
        $("#divProcesandowtTurnosRP").css("display", "none");
    }

    function onLoad_wSeleccionDeConsultorio() {
        var wSelConsultorios = $("#wSeleccionDeConsultorio").data("tWindow");
        wSelConsultorios.center().open();
    }

    function onEjecutaAccionCambiaPrioridadRP(_pturId, _Prioridad, _Paciente, Estado) {
        debugger;
        if (Estado !== "Admisionado") {
            jAlert("Para cambiar la Prioridad de Atención, el turno debe estar Admisionado", "¡Error!");
            return;
        }
        var NewPrioridad = _Prioridad == "Normal" ? "Alta" : "Normal";
        jConfirm('¿Confirma cambiar prioridad de atención de "' + _Paciente + '" ?', "Cambiar Prioridad", function (r) {
            if (r) {
                var _Post = "<%=Url.Content("~/registroProfesional/setCambiaPrioridad")%>";
                $.ajax({
                    url: _Post,
                    data: { pturId: _pturId, Prioridad: NewPrioridad },
                    type: "POST",
                    error: function (xhr, ajaxOptions, thrownError) {
                        jAlert("No se puede cambiar la prioridad", "¡Error!");
                        $("#popup_container").focus();
                        $("#popup_ok").focus();
                    },
                    success: function (data) {
                        if (data) {
                            Refrescar();
                        } else {
                            jAlert("No se puede cambiar la prioridad", "¡Error!");
                            return;
                        }
                    }
                });
            }
        });
    }

    function onNuevoProfesional() {
        var grid = $("#gridTurnosRP").data("tGrid");
        grid.addRow();
    }

    function onCommandEditTurnosRP(e) {
        _WindowCRUD = $("#gridTurnosRPPopUp").data("tWindow");
        $("#gridTurnosRPPopUp").find(".editor-field").css("width", "40%");
        onCommandEdit(e);
    }

</script>
<!-- Turno -->
<%
    ViewData["catTurnosRP"] = new List<GeDoc.catProfesionalTurnoWS>();
    bool Permiso = (Session["Permisos"] as GeDoc.Acciones).Visibilidad("registroProfesional", "Examinar") != "none";
     %>
<div style="overflow: hidden" >
<% Html.Telerik().Grid((IEnumerable<GeDoc.catProfesionalTurnoWS>)ViewData["catTurnosRP"])
       .Name("gridTurnosRP")
        .DataKeys(keys =>
        {
            keys.Add(p => p.pturId);
        })
     .Localizable("es-AR")
     .ToolBar(commands =>
        {
            commands.Template(grid =>
            {
                %>
                <div class="t-button" onclick="onNuevoProfesional();" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/CRUD/agregarProfesional.png" height="48" title="Nuevo profesional para atención" style = "vertical-align: middle;"/>
                </div>
                <div class="t-button" onclick="LlamarProximoProfesional(-1);" style="vertical-align: middle;">
                    <img src="<%= Url.Content("~/Content") %>/General/Proximo.png" height="48" title="Llamar al próximo profesional a Atender" style="vertical-align: middle;" />
                </div>
            <%
            });
        })
        
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Insert("_InsertEditing_GestionTurnos", "registroProfesional")
                .Select("_SelectEditing_GestionTurnos", "registroProfesional", new { Filtro = "~" });
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Custom("cmdRellamado")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Rellamar.png') no-repeat -0px 0px; background-size: 16px 16px;" })
                    .HtmlAttributes(new { title = "Volver a llamar a un Profesional" })
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.pturId).RouteKey("pturId");
                    });
                commands.Custom("cmdFinAtencion")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/General/CRUD/Ok.png') no-repeat -0px 0px; background-size: 16px 16px;" })
                    .HtmlAttributes(new { title = "Terminar de Atender este profesional" })
                    .DataRouteValues(route =>
                    {
                        route.Add(o => o.pturId).RouteKey("pturId");
                    });
            }).Width("64px").Title("Acciones");
            columns.Bound(c => c.pturPrioridad).Width("70px").Title("Prioridad").Filterable(false).Visible(true).HtmlAttributes(new { style = "white-space: nowrap; text-align: center;" })
                .ClientTemplate("<div title='<#= pturPrioridad == \"Normal\" ? \"Sin prioridad de Atención\" : \"Alta prioridad de Atención\" #>' onclick=\"onEjecutaAccionCambiaPrioridadRP(<#= pturId #>, '<#= pturPrioridad #>', '<#= pturApellidoyNombre #>', '<#= pturEstado #>');\"  style='cursor: pointer; text-align: center;'><img src='" + Url.Content("~/Content") + "/General/CRUD/<#= ImagenPrioridad #>' height='18px' width='18px' style='margin-right: 0px; vertical-align: middle;' /></div>");
            columns.Bound(c => c.pturId).Width("15px").Title("").Visible(false).Filterable(false);
            columns.Bound(c => c.pturFecha).Format("{0:dd/MM/yyyy}").Width("90px").Title("Fecha").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.pturFecha).Format("{0:hh:mm}").Width("60px").Title("Hora").Filterable(false).Visible(true).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.pturEstado).Width("120px").Title("Estado").Visible(true).Filterable(false)
            .ClientTemplate("<div title='<#= pturEstado #>' style='cursor: pointer; white-space: nowrap;' ><img src='" + Url.Content("~/Content") + "/Estados/<#= EstadoImagen #>' height='10px' width='10px' style='margin-right: 5px;' /><#= pturEstado #></label>");
            columns.Bound(c => c.Box).Width("150px").Title("Atención en").Visible(true).Filterable(false).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Box #>' style='cursor: pointer; white-space: nowrap;' ><#= Box #></label>");
            columns.Bound(c => c.pturTiempoDeEspera).Width("120px").Title("Tiempo de Espera").Visible(Permiso).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.pturTiempoAtencion).Width("120px").Title("Tiempo de Atención").Visible(Permiso).HtmlAttributes(new { style = "text-align: center;" });
            columns.Bound(c => c.pturApellidoyNombre).Width("250px").Title("Profesional").Visible(true).Filterable(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pturApellidoyNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= pturApellidoyNombre #></label>");
            columns.Bound(c => c.pturDNI).Width("80px").Title("DNI").Visible(false).Filterable(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pturDNI #>' style='cursor: pointer; white-space: nowrap;' ><#= pturDNI #></label>");
         })
         .Editable(editing => editing.Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
         .Pageable((paging) =>
         paging.Enabled(true)
         .PageSize(100))
        .ClientEvents(events => events.OnDataBinding("onDataBindingTurnosRP").OnEdit("onCommandEditTurnosRP").OnComplete("onCompleteTurnosRP").OnRowSelected("onRowSelectedTurnoRP").OnCommand("onCommandTurnosRP").OnError("onErrorTurnoRP"))
        .Selectable()
        .Filterable()
        .Scrollable(scroll => scroll.Enabled(true).Height((int)Session["AlturaGrilla"]))
        .Resizable(resizing => resizing.Columns(true))
        .Render();                     
        %>

</div>

<!-- Selecciona consultorio / centro de atención -->
<%
    Html.Telerik().Window()
      .Name("wSeleccionDeConsultorio")
      .Title("Selección de Centro de Atención")
      .Visible(false)
      .Content(() =>
      {
          Html.RenderPartial("SeleccionaConsultorio");
      })
      .Buttons(b => b.Clear())
      .Draggable(true)
      .Scrollable(false)
      .Modal(true)
      .Width(424)
      .Height(140)
      .ClientEvents(eventos => eventos.OnActivate("onActivate_wSeleccionDeConsultorio").OnLoad("onLoad_wSeleccionDeConsultorio"))
      .Render();
%>

<script>
    CerrarWaiting();
    CerrarWaitingCRUD("wtTurnosRP");
</script>

