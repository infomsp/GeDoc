<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% Html.Telerik().Window()
        .Name("MensajeError")
        .Title("Atención...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <table style="border: none; margin:0px; margin-left:0px">
                    <tr style="border: none; margin: 0px;">
                        <td align="center" valign="middle" style="border: none; margin: 0px;">
                            <img src="<%= Url.Content("~/Content") %>/General/Notificacion/Vista Internet Security OFF.png" width="45px" style="background-position: center center; margin-top:-5px; background-image: url('<%= Url.Content("~/Content") %>/General/Notificacion/Vista Internet Security OFF.png'); background-repeat: no-repeat; background-attachment: fixed;" />
                        </td>
                        <td height="100%" style="border: none; margin: 10px;">
                            <div id="divError" class="field-validation-error" style="width: 80%; vertical-align:middle; margin-top:-20px; margin-left: 0px; text-align: justify; font-size: 14px;">
                                <label id="lblErrorMessage" ></label>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        <%})
       .Buttons(b => b.Close())
       .Draggable(true)
       .Modal(true)
       .Scrollable(false)
       .Width(470)
       .Height(200)
       .Render();
%>
