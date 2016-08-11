<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GeDoc.LogOnModel>" UICulture="es-AR" %>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $("#title").css("display", "none");
        $("#main").css("background", "transparent");
        $("#main").css("background-color", "transparent");
        function onIngresarAlSistema() {
            if (!isMobile()) {
                AbrirWaiting();
            }
        }
    </script>
<link href="<%=Url.Content("~/Content/StyleLogon.css")%>" rel="stylesheet" type="text/css" />
<div style="text-align: center;">
    <img src="<%=Url.Content("~/Content/General/LogoGobMSP.png")%>" alt="">
</div>
<div class="message warning">
<div class="contact-form">
	<div class="logo">
		<h1>Autenticación</h1>
	</div>	
    <script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

    <% using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new { id = "contact_form", @class = "form", enctype = "multipart/form-data", OnFailure = "alert('Error');", onsubmit = "onIngresarAlSistema();" }))
      { %>
	<ul>
		<li>
			<label><img src="<%=Url.Content("~/Content/LogIn/contact.png")%>" alt=""></label>
            <%: Html.TextBoxFor(m => m.UserName, new { @class = "User", placeholder="Nombre de Usuario" }) %>
            <%--<%: Html.ValidationMessageFor(m => m.UserName) %>--%>
		 </li>
		 <li>
			 <label><img src="<%=Url.Content("~/Content/LogIn/lock.png")%>" alt=""></label>
            <%: Html.PasswordFor(m => m.Password, new { name="Password", placeholder="Contraseña" }) %>
            <%--<%: Html.ValidationMessageFor(m => m.Password) %>--%>
		 </li>
		 <li class="style">
		     <input type="Submit" value="Ingresar"/>
		 </li>
	</ul>
	<div class="clear"></div>
    <%: Html.ValidationSummary(true, "") %>
    <% } %>
</div>
<div class="alert-close"></div>
</div>
<div class="clear"></div>
<script type="text/javascript">
    $(document).ready(function (c) {
        $("#piedepagina").css("display", "none");
    });

</script>
</asp:Content>
