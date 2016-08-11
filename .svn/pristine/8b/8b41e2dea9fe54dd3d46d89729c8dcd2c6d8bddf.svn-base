<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GeDoc.LogOnModel>" UICulture="es-AR" %>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function onIngresarAlSistema() {
            if (!isMobile()) {
                AbrirWaiting();
            }
        }
    </script>
    <table style="border: none;">
        <tr style="border: none;">
            <td style="vertical-align: middle; border: none;" colspan="0" rowspan="0" valign="middle"><img src="<%: Url.Content("~/Content/General/Seguridad.png")%>" alt="Autenticación..." width="60px" /></td>
            <td style="vertical-align: middle; font-size: large; font-weight: bold; color: #333300; border: none;"
                colspan="0" rowspan="0" valign="middle"><label>Autenticación de Usuario</label></td>
        </tr>
    </table>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

    <% using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new { enctype = "multipart/form-data", onsubmit = "onIngresarAlSistema();" }))
      { %>
        <%: Html.ValidationSummary(true, "Datos de Autenticación incorrectos. Corrija los errores e intente nuevamente por favor.") %>
        <div class="BordeRedondeado">
            <fieldset style="border: none;">
                <legend></legend>
                <table width="100%" style="border: none;">
                    <tr>
                        <td style="border: none;">
                            <div>
                                <%: Html.LabelFor(m => m.UserName) %>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none;">
                            <div style="position: relative; top: -10px">
                                <%: Html.TextBoxFor(m => m.UserName) %>
                                <%: Html.ValidationMessageFor(m => m.UserName) %>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none;">
                            <div>
                                <%: Html.LabelFor(m => m.Password) %>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: none;">
                            <div style="position: relative; top: -10px">
                                <%: Html.PasswordFor(m => m.Password) %>
                                <%: Html.ValidationMessageFor(m => m.Password) %>
                            </div>
                        </td>
                    </tr>
                    <tr><td style="border: none;"></td></tr>
                    <tr>
                        <td style="border: none;">
                            <input type="submit" value="Ingresar" class="t-button" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
