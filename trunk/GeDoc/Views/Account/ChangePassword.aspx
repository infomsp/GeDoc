<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GeDoc.ChangePasswordModel>" UICulture="es-AR" %>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
<% string _btnAceptar = "";
   string _btnCancelar = "";
   _btnAceptar = "background: url('" + Url.Content("~/Content/") + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -33px -335px;";
   _btnCancelar = "background: url('" + Url.Content("~/Content/") + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -49px -335px;";
%>
    <h2>Cambiar contraseña</h2>
    <p>
        La nueva contraseña debe tener un mínimo de <%: Membership.MinRequiredPasswordLength %> caracteres de longitud.
    </p>

    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "El cambio de contraseña no tuvo éxito. Corrija los errores e intente de nuevo.") %>
        <div>
            <fieldset>
                <legend>Información de la cuenta</legend>
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.OldPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.OldPassword) %>
                    <%: Html.ValidationMessageFor(m => m.OldPassword) %>
                </div>
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.NewPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.NewPassword) %>
                    <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                </div>
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                <p>
                    <button class="t-button" type="submit" >
                        <img src="<%=Url.Content("~/Content/General/Vacio-Transparente.png")%>" alt="Aceptar" height="15" width="15" style="<%: _btnAceptar %>" />
                    </button>
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
