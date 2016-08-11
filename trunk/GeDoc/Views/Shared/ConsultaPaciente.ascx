<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src=""<%= Url.Content("~/Scripts/CRUDGrillas.js") %>""></script>

<% Html.Telerik().Window()
        .Name("wConsultaPaciente")
        .Visible(false)
        .Title("Detalle")
        .Modal(true)
        .Scrollable(false)
        .Height(580)
        .Content(() =>
            {
                %>
                <table style="border: none; margin-right: 5px; margin-left: -2px;">
                    <tr>
                        <td style="border: none; width: 100%;">
                            <div style='width: 100%; vertical-align: middle; padding-bottom: 5px; margin-left: 8px;' align="right">
                                <button class="t-button" style="vertical-align: middle; padding: 3px;" onclick='Imprimir(this);' >
                                    <img src='/GeDoc/Content/General/Printer.png' title='Imprimir' id="imgImprimir" height='22px' style='vertical-align:top;' />
                                    <label style="vertical-align: middle; padding-right: 3px; cursor: pointer;">Imprimir</label>
                                </button>
                            </div>
                            <div id="divDatos" class="BordeRedondeado" style="border-color: Silver; padding: 3px 3px 3px 3px; width: 100%;">
                                <table style="border: none;">
                                    <tr>
                                        <td style="border: none;">
                                                <div style="margin-top: -5px; margin-bottom: -5px;"><label id="lblApellidoyNombre" style="font-size: 22px; font-weight: bold;"></label></div>
                                                <div class="div-consulta" >
                                                    <label id="Label1" style="font-size: 12px; font-weight: normal">Nro. HC:</label>
                                                    <label id="lblnroHC"  class="label-consulta" style="width: 75px"></label>
                                                    <label id="Label4" style="font-size: 12px; font-weight: normal; margin-left: 90px;">Sexo:</label>
                                                    <label id="lbltipoIdSexo" class="label-consulta" style="width: 70px;"></label>
                                                </div>
                                                <div class="div-consulta">
                                                    <label id="Label2" style="font-size: 12px; font-weight: normal">Doc.Nº:</label>
                                                    <label id="lblperDNI" class="label-consulta" style="margin-left: 0px; width: 75px;" ></label>
                                                    <label id="Label3" style="font-size: 12px; font-weight: normal; margin-left: 105px;">CUIL:</label>
                                                    <label id="lblperCUIL" class="label-consulta" style="margin-left: 0px; width: 115px;" ></label>
                                                    <label id="Label8" style="font-size: 12px; font-weight: normal; margin-left: 144px;">Tipo Doc.:</label>
                                                    <label id="lblperTipoDoc" class="label-consulta" style="margin-left: 5px; width: 21px;" ></label>
                                                </div>
                                                <div class="div-consulta">
                                                    <label id="Label5" style="font-size: 12px; font-weight: normal">Fecha de Nacimiento:</label>
                                                    <label id="lblperFechaNacimiento" class="label-consulta" style="margin-left: 0px; width: 120px;" ></label>
                                                    <label id="Label6" style="font-size: 12px; font-weight: normal; margin-left: 239px;">Edad:</label>
                                                    <label id="lblperEdad" class="label-consulta" style="margin-left: 4px; width: 20px;" ></label>
                                                </div>
                                                <div class="div-consulta">
                                                    <label id="Label17" style="font-size: 12px; font-weight: normal">Lugar de Nacimiento:</label>
                                                    <label id="lblprovNombre" class="label-consulta" style="margin-left: 4px; width: 301px;" ></label>
                                                </div>
                                                <div class="div-consulta">
                                                    <label id="Label21" style="font-size: 12px; font-weight: normal">Nacionalidad:</label>
                                                    <label id="lblpaisNombre" class="label-consulta" style="margin-left: 4px; width: 350px;" ></label>
                                                </div>
                                                 <div class="div-consulta">
                                                    <label id="Label121" style="font-size: 12px; font-weight: normal">Provincia:</label>
                                                    <label id="lblProvincia" class="label-consulta" style="margin-left: 4px; width: 350px;" ></label>
                                                </div>
                                                <div class="div-consulta">
                                                    <label id="Label18" style="font-size: 12px; font-weight: normal; margin-left: 0px;">Estado Civil:</label>
                                                    <label id="lblEstadoCivil2" class="label-consulta" style="margin-left: 4px; width: 192px;" ></label>
                                                   
                                                </div>
                                                <div class="div-consulta">
                                                    <label id="Label7" style="font-size: 12px; font-weight: normal;">Teléfono:</label>
                                                    <label id="lblperTelefono" class="label-consulta" style="margin-left: 0px; width: 140px;" ></label>
                                                    <label id="Label14" style="font-size: 12px; font-weight: normal; margin-left: 183px;">Celular:</label>
                                                    <label id="lblperCelular" class="label-consulta" style="margin-left: 4px; width: 140px;" ></label>
                                                </div>
                                                
                                                <div class="div-consulta">
                                                    <label id="Label10" style="font-size: 12px;">Lugar de Trabajo:</label>
                                                    <label id="lblsecNombre" class="label-consulta" style="margin-left: 4px; width: 567px;" ></label>
                                                </div>
                                        </td>
                                        <td style="border: none; width: 230px; height: 200px;" >
                                            <div style="width: 100%; height: 100%;" >
                                                <img id="imgFoto" class="BordeRedondeado" style="border-color: #999999; width: 230px; height: 200px;" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin-left: 5px;">
                                    <div class="div-consulta" style="width: 100%; margin-top: 8px;">
                                        <label id="Label19" style="font-size: 12px;">Ocupación:</label>
                                        <label id="lblprofNombre" class="label-consulta" style="margin-left: 0px; width: 280px;" ></label>
                                        <label id="Label20" style="font-size: 12px; margin-left: 322px;">Estudios Alcanzados:</label>
                                        <label id="lblperEstudiosAlcanzados" class="label-consulta" style="margin-left: 4px; width: 150px;" ></label>
                                    </div>
                                    <div class="div-consulta">
                                        <label id="Label25" style="font-size: 12px; font-weight: normal">Obra Social:</label>
                                        <label id="lblosNombre" class="label-consulta" style="margin-left: 4px; width: 621px;" ></label>
                                    </div>
                                    <div class="div-consulta">
                                        <label id="Label22" style="font-size: 12px; font-weight: normal">Domicilio:</label>
                                        <label id="lblperDomicilio" class="label-consulta" style="margin-left: 4px; width: 621px;" ></label>
                                    </div>
                                    <div class="div-consulta" style="width: 100%; margin-top: 13px;">
                                        <label id="Label9" style="font-size: 12px; font-weight: normal">Correo Electrónico:</label>
                                        <label id="lblperEmail" class="label-consulta" style="margin-left: 4px; width: 360px;" ></label>
                                        <label id="Label23" style="font-size: 12px; font-weight: normal; margin-left: 410px;">Permite recibir SMS:</label>
                                        <label id="lblRecibeSMS" class="label-consulta" style="margin-left: 4px; width: 21px;" ></label>
                                    </div>
                                    <div class="div-consulta" style="margin-bottom: 13px;">
                                        <label id="Label11" style="font-size: 12px; font-weight: normal">Observaciones:</label>
                                        <label id="lblObservaciones" class="label-consulta" style="margin-left: 4px; width: 582px;" ></label>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <%
            })
        .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()   
        .Render();
%>


