<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>

<%
    var MinimaHora = "00:00";
    if (ViewData["MinimaHora"] != null)
    {
        MinimaHora = ViewData["MinimaHora"].ToString();
    }

    var MaximaHora = "23:59";
    if (ViewData["MaximaHora"] != null)
    {
        MaximaHora = ViewData["MaximaHora"].ToString();
    }

    var IntervaloHora = 15;
    if (ViewData["IntervaloHora"] != null)
    {
        IntervaloHora = (int)ViewData["IntervaloHora"];
    }
%>

<%= Html.Telerik().TimePickerFor(m => m)
    .Interval(IntervaloHora)
    .Format("HH:mm")
    .Min(MinimaHora)
    .Max(MaximaHora)
    .OpenOnFocus(true)
    .ShowButton(true)
    .ButtonTitle("Ingrese el horario") %>