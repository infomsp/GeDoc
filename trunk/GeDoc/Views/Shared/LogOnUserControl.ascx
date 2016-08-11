<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    string _Nombre = "";
    string _NombreCompleto = "";
    bool _TieneAlertas = true;
    if (Request.IsAuthenticated && Session["Usuario"] != null)
    {
        _Nombre = Session["Usuario"] == null ? "" : "Hola " + (Session["Usuario"] as GeDoc.Models.sisUsuario).usrApellidoyNombre;
        _NombreCompleto = _Nombre;
        if (_Nombre.Length > 18)
        {
            _Nombre = _Nombre.Substring(0, 19) + "...";
        }
        var _Estilos = new GeDoc.AccountController();
        var _Resoluciones = new GeDoc.proResolucionController();
        if (Session["Estilos"] == null)
        {
            Session["Estilos"] = _Estilos.PopulateDropDownList();
        }
        SelectList _ListaEstilos = (Session["Estilos"] as SelectList);
        //var _Alertas = _Resoluciones.All("").Where(e => e.resEsImportante && (e.resNotificacionVencimiento != null ? e.resNotificacionVencimiento.Value.Date >= DateTime.Now.Date && e.resNotificacionVencimiento.Value.AddDays((double)((e.resNotificacionDiaInicio == null ? 0 : e.resNotificacionDiaInicio) * -1)).Date <= DateTime.Now.Date : false));
        var _Alertas = _Resoluciones.HayNormasLegalesAVencer();
        _TieneAlertas = _Alertas && _Estilos.VerNormasLegalesAVencer((Session["Usuario"] as GeDoc.Models.sisUsuario));
        int _IndiceEstilo = 0;
        foreach (var _ItemEstilo in _ListaEstilos)
        {
            if (_ItemEstilo.Value == (Session["Usuario"] as GeDoc.Models.sisUsuario).estId.ToString())
            {
                break;
            }
            _IndiceEstilo++;
        }
%>
<div align="center">
    <table style="border: none; margin:0px; margin-left:0px">
        <tr style="border: none; margin: 0px;">
            <td style="border: none; margin: 0px;">
                <% if (_TieneAlertas)
                   { %>
                    <div>
                        <a href='<%= Url.Content("~/proResolucion/Alertas") %>'>
                            <img src="<%= Url.Content("~/Content/General/Notificacion/Vista Internet Security Attention.png") %>" width="25px" title="Haga click aquí para ver las Normas Legales por vencer." style="background-position: center center; margin-top:-5px; cursor: pointer; background-image: url('<%= Url.Content("~/Content") %>/General/Notificacion/Vista Internet Security Attention.png'); background-repeat: no-repeat; background-attachment: fixed;" />
                        </a>
                    </div>
                <%} %>
            </td>
            <%
        if (Session["Usuario"] != null)
        {
                %>
            <td style="border: none; margin: 0px;">
                <label class="t-button">
                    <img class="t-icon t-refresh" onclick="onSeleccionarCentroDeSalud();" title="Cambiar de Repartición"/>
                </label>
            </td>
        <% } %>
            <td style="border: none; margin: 0px;">
                <img src="<%= Url.Content("~/Content/General/usuarios.png") %>" width="25px" style="margin-top:-5px;" />
            </td>
            <td style="border: none; margin: 0px;">
                <div id="divMenuLogout">
                    <div id="divLabel" style="border: 1px solid; padding: 5px; padding-bottom: 0px; padding-top: 0px; border-color: transparent;">
                        <label id="lblUserName"><%: _Nombre %></label>
                    </div>
                    <div id="divContenedorLogout" style="display: none; position: absolute;">
                        <div id="divLogout" class="arrow_box" style="border: 2px solid; border-radius: 5px 5px 5px 5px !important; -moz-border-radius: 5px 5px 5px 5px !important; -webkit-border-radius: 5px 5px 5px 5px !important; z-index: 9999; margin-top: 6px;  margin-left: -8px;">
                            <div id="divProfile" style="border: 1px solid; border-radius: 5px 5px 5px 5px !important; -moz-border-radius: 5px 5px 5px 5px !important; -webkit-border-radius: 5px 5px 5px 5px !important;" class="t-animation-container">
                                <ul class="t-group t-reset t-highlighted t-widget t-menu" style="width: 154px; border:0px;">
                                    <li class="t-item t-state-default" style="border: 0px;">
                                        <a id="aChangePass" class="t-link" href="<%= Url.Content("~/Account/ChangePassword") %>" onmouseover="onOver(this);" onmouseout="onOut(this);" style="width:130px;">Cambiar contraseña</a>
                                    </li>
                                    <li class="t-item t-state-default" style="border: 0px;">
                                        <a id="aLogOff" class="t-link" href="<%= Url.Content("~/Account/LogOff") %>" onmouseover="onOver(this);" onmouseout="onOut(this);" style="width:130px;">Salir</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
            <td style="border: none; margin: 0px; vertical-align: middle;">
                <div >
                    <%: Html.Telerik().DropDownList()
                                    .Name("ddlbEstilos")
                                    .BindTo(_ListaEstilos)
                                    .SelectedIndex(_IndiceEstilo)
                                    .ClientEvents(events => events
                                              .OnChange("onDropDownListChange"))
                                                    .HtmlAttributes(new { style = "width: 140px; height: 28px; vertical-align: middle; background-color: transparent;" })
                                    %>
                </div> 
            </td>
        </tr>
    </table>
</div>
<%
    }
%> 

<script type="text/javascript">
    $(document).ready(function () {
        $("#divMenuLogout").mouseenter(function () {
            //$("#lblUserName").css("background-color", "#FFFFCC");
            $("#divLabel").attr("class", "t-item t-state-hover");
            //$("#divLabel").css("border", "1px solid");
            $("#divLabel").css("border-radius", "5px 5px 0px 0px !important");
            $("#divLabel").css("-moz-border-radius", "5px 5px 0px 0px !important");
            $("#divLabel").css("-webkit-border-radius", "0px 5px 5px 0px !important");
            $("#divLabel").css("border-radius", "5px 5px 0px 0px !important");
            $("#divLabel").css("border-color", "inherit");
            //$("#divLogout").show("slide", { direction: "up" }, 900); //fadeIn('slow');// show(900);
            $("#divContenedorLogout").fadeIn('slide');// show(900);
        });
        $("#divMenuLogout").mouseleave(function () {
            //$("#divLogout").hide("slide", { direction: "right" }, 900);//fadeOut('slow'); // hide(900);
            //$("#divLabel").css("border", "");
            $("#divLabel").css("border-radius", "");
            $("#divLabel").css("-moz-border-radius", "");
            $("#divLabel").css("-webkit-border-radius", "");
            $("#divLabel").css("border-radius", "");
            $("#divContenedorLogout").fadeOut('slide'); // hide(900);
            $("#divLabel").css("border-color", "transparent");
            //$("#lblUserName").css("background-color", "");
            $("#divLabel").attr("class", "");
        });
    });

    function onSeleccionarCentroDeSalud() {
        debugger;
        var wSeleccion = $("#wEligeCS").data("tWindow");
        wSeleccion.center().open();

        RecargarPaginaPorCambioDeCentroDeSalud = "Recargar";
    }
    function onDropDownListChange(e) {
        $.post(GetPathApp('Account/ChangeEstilo'), { pEstilo: e.value }, function (data) {
            if (data) {
                location.reload();
            }
        });
    }
    function onAlertas(e) {
        $.post(GetPathApp('proResolucion/getAlertas'), function (data) {
            if (data) {
                location.reload();
            }
        });
    }
    function onOver(e) {
        $(e).attr("class", "t-link t-state-hover");
    }
    function onOut(e) {
        $(e).attr("class", "t-link");
    }
</script>