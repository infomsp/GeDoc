<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="<%: Url.Content("~/Scripts/CRUDGrillas.js")%>" ></script>
<script>
    var especialidadId;
    var profesionalId;
    var _espId;
    var _profId;
    var fecha;
    var _fechaServ;
    var idTurno;
    var _Error = false;
    var idPaciente;
    var _idPaciente = -1;
    var _RowIndex;
    var _DatosRegistro;
    var _conId;
    var _DatosRegistroHorario;
    var _EsModificar = false;
    var _pacId = -1;
    var _auxEditar = 0;
    var pacId = 0;
    function HabilitaHora() {
        debugger;
        var _tab = $("#tabFiltroListPac").data("tTabStrip");
        var _tab_select = $('#tabFiltroListPac').select().index();
        _tab.Tabs[1].visible;

    }
    function ImprimirFiltroPac() {
        debugger;
        var reportName = ["rptTurnoListadoPacientesConFiltro", "rptTurnoListadoPracticasPacientesConFiltro"];

        var timeDesde = $('#timeDesde').data('tTimePicker');
        var _horaDesde = timeDesde.inputValue;
        var _rbEstado;
        var _csId;
        var _usr;

     
        idturno = 1;
        debugger;
        if ($('input[name="rbEstado"]:checked'))
        { _rbEstado = $('input[name="rbEstado"]:checked').val(); }
          

        if (_horaDesde == "") {
            jAlert('La Hora inicial es incorrecta','Atencion!');
            return;
        }
        var timeHasta = $('#timeHasta').data('tTimePicker');
        var _horaHasta = timeHasta.inputValue;
        if (_horaHasta == "") {
            jAlert('La Hora final es incorrecta', 'Atencion!');
            return;
        }
        if (timeDesde.value() > timeHasta.value()) {
            jAlert('La hora inicial debe se menor o igual a la hora final', 'Atencion!');
            return;
        }

        if (EsMedico === "NO"){
            _profId = $("#cbxProfesional").data("tComboBox").value();
        } else {
            _profId = MedicoLogueado;
        }
        _espId = $("#cbxEspecialidad").data("tComboBox").value();
        fecha = $("#Agenda").val();
        //fecha = $("#Agenda").data("tDatePicker").value();
        if (_profId < 0) {
            _conId = _profId * -1;
            _profId = 0;
        }
        _csId = "<%=(Session["UsuarioCentroDeSalud"] as GeDoc.sisUsuariosCentroDeSalud).csId%>";
        _usr = "<%=(Session["Usuario"] as GeDoc.Models.sisUsuario).usrNombreDeUsuario%>";
        var _Parametros = new Array(new Array(_csId, 'csId'), new Array(_usr, 'usr'), new Array(_rbEstado, 'Estado'), new Array(_horaDesde, 'horaDesde'), new Array(_horaHasta, 'horaHasta'), new Array(_espId, 'espId'), new Array(_conId, 'conId'), new Array(fecha, 'turFecha'), new Array(_profId, 'perId'));
        InvocarReporte(reportName[$("[name='rbDiagOrPrac']:checked").val()], _Parametros);
    }
  
</script>

<% 
    int _usrId = 0;  
    if (Session["UsuarioId"] != null)
    {
        _usrId = (int)Session["UsuarioId"];
    }
    Html.Telerik().Window()
        .Name("WFiltroListadoPacientes")
        .Visible(false)
        .Title("Filtro Listado de Pacientes")
        .Modal(true)
        .Scrollable(false)
       
        .Height(285)
        .Draggable(true)
        .Content(() =>
            {
                %>
                <div style='width: 100%; position: absolute; right: 10px;' align="right">
                    <button class="t-button" style="vertical-align: middle; padding: 3px;" onclick="ImprimirFiltroPac();" >
                        <img src='<%= Url.Content("~/Content") %>/General/Printer.png' title='Imprimir' id="imgImprimir" height='18px' style='vertical-align:top;' />
                        <label style="vertical-align: middle; padding-right: 3px; cursor: pointer;">Imprimir</label>
                    </button>
                </div>
                <% Html.Telerik().TabStrip()
                        .Name("tabFiltroListPac")
                        .HtmlAttributes(new { style = "height: 70%; padding: 0px; background: transparent; border: 0px; margin-left: -4px;" })
                        .Items(tabstrip =>
                        {
                            tabstrip.Add()
                                .Text("Estados del Turno")
                                .ContentHtmlAttributes(new { style = "height: 110%; padding: 5px;" })
                                .Content(() =>
                                    
                                {
                                    
                                     %>
                                    
                                <table style="border: none; margin-right: 5px; margin-left: -2px;">
                                    <tr>
                                        <td style="border: none;">    
                                                <div class="div-consulta">
                                                    <div class="BordeRedondeado" style="border-color: Silver; padding: 13px 13px 13px 9px; width: 420px; margin: 3px 0px 3px 0px;">
                                                        <input type="radio" name="rbEstado" id="radio_1" value="1"  style=" padding: 8px" checked> Todos
                                                        <input type="radio" name="rbEstado" id="radio_2" value="2"  style=" padding: 8px" > Admisionados
                                                            <input type="radio" name="rbEstado" id="radio_3" value="3" style=" padding: 8px" > Atendidos por Profesional
                                                        </div>
                                                </div>  
                                        </td>  
                                        </tr> 
                                        <tr>                                             
                                            <td style="border: none;">    
                                                <div class="div-consulta">
                                                    <div class="BordeRedondeado" style="border-color: Silver; padding: 13px 13px 13px 9px; width: 420px; margin: 3px 0px 3px 0px;">
                                                        <input type="radio" name="rbHoraCant" value="1"  style=" padding: 8px" onclick='HabilitaHora();' checked > Hora
                                                        <input type="radio" name="rbHoraCant" value="2"  style=" padding: 8px" > Cantidad                                                                         
                                                        </div>
                                                </div>     
                                        </td>                                                       
                                    </tr>
                                    <tr>                                             
                                        <td style="border: none;">    
                                            <div class="div-consulta">
                                                <div class="BordeRedondeado" style="border-color: Silver; padding: 13px 13px 13px 9px; width: 420px; margin: 3px 0px 3px 0px;">
                                                    <input type="radio" name="rbDiagOrPrac" value="0"  style=" padding: 8px" checked > Diagnosticos
                                                    <input type="radio" name="rbDiagOrPrac" value="1"  style=" padding: 8px" > Prácticas                                                                         
                                                    </div>
                                            </div>     
                                       </td>                                                       
                                    </tr>   
                                                                                  
                                </table>
                            <%});
                            tabstrip.Add()
                                .Text("Hora")
                               .Visible(true)
                               .Enabled(true)
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { 
                                    %>
                                    <table style="border: none; margin-right: 5px; margin-left: -2px;">
                                    <tr>
                                        <td style="border: none; width: 100%;">
                                            <div id="div1" class="BordeRedondeado" style="border-color: Silver; padding: 3px 3px 3px 3px; width: 90%;">
                                                <table style="border: none;">
                                                    <tr>
                                                        <td style="border: none;">                                                                                                                                 
                                                                <div class="div-consulta">  
                                                                                                                                     
                                                                    <div  style=" padding: 13px 13px 13px 9px; width: 80%; margin: 3px 0px 3px 0px;">

                                                                     <label id="Label1" style="font-size: 12px; font-weight: normal">Desde:</label>   
                                                                  <% Html.Telerik().TimePicker()
                                                                         .Name("timeDesde")
                                                                         .HtmlAttributes(new { style = "width: 120px; padding: 0px; background: transparent; border: 0px;" })                                                                   
                                                                         .Min("00:00")
                                                                         .Max("23:59")                                                                          
                                                                         .OpenOnFocus(true)
                                                                        // .Format
                                                                         .Format("HH:mm")
                                                                         .ShowButton(true)  
                                                                         .Value("00:00")                                                                                                                                  
                                                                         .Enable(true)                                                                         
                                                                         .Render();
                                                                        %>
                                                                     <label id="Label2" style="font-size: 12px; font-weight: normal">Hasta:</label>   
                                                                      <% Html.Telerik().TimePicker()
                                                                         .Name("timeHasta")
                                                                         .HtmlAttributes(new { style = "width: 120px; padding: 0px; background: transparent; border: 0px;" })                                                                   
                                                                         .Min("00:00")
                                                                         .Max("23:59")                                                                          
                                                                         .OpenOnFocus(true)
                                                                       .Format("HH:mm")
                                                                          .Value("23:59")    
                                                                         .ShowButton(true)                                                                                                                                           
                                                                         .Enable(true)                                                                         
                                                                         .Render();
                                                                        %>
                                                                     </div>
                                                                </div>                                                               
                                                              
                                                        </td>                                                       
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                    <%
                                });
                            tabstrip.Add()
                                .Text("Cantidad")
                                .Visible(true)
                                .Enabled(false)
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { 
                                    %>
                                   <table style="border: none; margin-right: 5px; margin-left: -2px;">
                                    <tr>
                                        <td style="border: none; width: 100%;">
                                            <div id="div2" class="BordeRedondeado" style="border-color: Silver; padding: 3px 3px 3px 3px; width: 100%;">
                                                <table style="border: none;">
                                                    <tr>
                                                        <td style="border: none;">    
                                                                <div class="div-consulta">
                                                                    <div class="BordeRedondeado" style="border-color: Silver; padding: 13px 13px 13px 9px; width: 420px; margin: 3px 0px 3px 0px;">
                                                                      <label id="Label3" style="font-size: 12px; font-weight: normal">Desde:</label> 
                                                                      <input type="Text" name="tCantidadD" value="1" style=" padding: 8px"> 
                                                                       </br>
                                                                      <label id="Label4" style="font-size: 12px; font-weight: normal">Hasta:</label> 
                                                                      <input type="Text" name="tCantidadH" value="1" style=" padding: 8px" >                                                                      
                                                                     </div>
                                                                </div>                                                               
                                                              
                                                        </td>                                                       
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                    <%
                                });
                        })
                .SelectedIndex(0)
                .Render();
            })
        .Render();
%>

