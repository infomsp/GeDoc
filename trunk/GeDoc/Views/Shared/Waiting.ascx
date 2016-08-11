<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    string _Loading = Url.Content("~/Content/General/WaitingIndicator.gif");
%>
<% Html.Telerik().Window()
        .Name("wWaiting")
        //.Title("Procesando...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <div id="divWaiting" style="width: 465px; margin-top: 15px; margin-left: 5px;">
                    <img src="<%= _Loading %>" width="28px" alt="" style="margin-top: 10px;" />
                    <div style="margin-top: -25px; margin-left: 35px;">
                        <label style="width: 465px;color: chocolate; font-size: 13px;">Trabajando, espere un momento por favor...</label>
                    </div>
                </div>
            </div>
        <%})
       .Buttons(b => b.Clear())
       .Modal(true)
       .ClientEvents(eventos => eventos.OnLoad("onLoadwWaiting"))
       .Scrollable(false)
       .Width(360)
       .Height(60)
       .Render();
%>
<script>
    function onLoadwWaiting() {
        $("#wWaiting").find(".t-window-titlebar.t-header").css("display", "none");
    }
</script>