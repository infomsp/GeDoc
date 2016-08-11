<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.Telerik().Window()
        .Name("wCertificado")
        .Visible(false)
        .Title("Certificado de Trabajo")
        .Modal(true)
        .Scrollable(false)
        .Height(245)
        .Content(() =>
            {
                %>
                <table style="border: none; margin-right: 5px; margin-left: -2px;">
                    <tr>
                        <td style="border: none; width: 100%;">
                            <div id="divDatos" class="BordeRedondeado" style="border-color: Silver; padding: 3px 3px 3px 3px; width: 100%;">
                                <div style="margin: 8px 0px 0px 0px;">
                                    <label id="lblSueldos" for="Customers-input" style="margin-left: 5px;">Imprimir Sueldos:</label>
                                    <input id="chkSueldos" onchange="onChangeSueldo();" type="checkbox" checked="checked" />
                                </div>
                                <div style="margin: 8px 0px 8px 0px;">
                                    <label id="lblBruto" for="Customers-input" style="margin-left: 5px;">Sueldo Bruto:</label>
                                    <% Html.Telerik().CurrencyTextBox().Name("txtBruto").Render(); %>
                                </div>
                                <div style="margin: 8px 0px 8px 0px;">
                                    <label id="lblNeto" for="Customers-input" style="margin-left: 5px;">Sueldo Neto:</label>
                                    <% Html.Telerik().CurrencyTextBox().Name("txtNeto").Render(); %>
                                </div>
                                <div style="margin: 8px 0px 8px 0px;">
                                    <input id="chkImprimeEmbargo" type="checkbox" checked="checked" />
                                    <label id="lblEmbargos" for="Customers-input" style="margin-left: 5px;">Posee embargos:</label>
                                    <input id="chkEmbargos" type="checkbox" />
                                </div>
                                <div style="margin: 8px 0px 8px 0px;">
                                    <input id="chkImprimeGuardias" type="checkbox" checked="checked" />
                                    <label id="Label1" for="Customers-input" style="margin-left: 5px;">Incluido en el Sistema de Guardias Rotativas:</label>
                                    <input id="chkGuardias" type="checkbox" />
                                </div>
                                <div style="margin: 0px 0px 8px 0px;">
                                    <label id="lblPresentarEn" for="Customers-input" style="margin-left: 5px;">Presentar en:</label>
                                    <input type="text" id="txtLugarPresentacion" />
                                </div>
                            </div>
                            <div style='width: 100%; vertical-align: middle; text-align: right; margin-top: 8px; padding-bottom: 5px; margin-left: 8px;' align="right">
                                <button class="t-button" style="vertical-align: middle; width: 110px; padding: 3px;" onclick='ImprimirCertificado(this);' >
                                    <img src='<%= Url.Content("~/Content") %>/General/Printer.png' title='Imprimir' id="imgImprimir" height='22px' style='vertical-align:top;' />
                                    <label style="vertical-align: middle; padding-right: 3px; cursor: pointer;">Imprimir</label>
                                </button>
                                <button class="t-button" style="vertical-align: middle; width: 110px; padding: 3px;" onclick='Cancelar();' >
                                    <img src='<%= Url.Content("~/Content") %>/General/No.png' title='Cancelar' id="imgCancelar" height='22px' style='vertical-align:top;' />
                                    <label style="vertical-align: middle; padding-right: 3px; cursor: pointer;">Cancelar</label>
                                </button>
                            </div>
                        </td>
                    </tr>
                </table>
                <%
            })
        .Render();
%>
