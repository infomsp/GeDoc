<%@ Control Language = "C#" Inherits= "System.Web.Mvc.ViewUserControl" %>
<%
    AutoCompleteFilterMode _Filtro = AutoCompleteFilterMode.StartsWith;
    if (ViewData["FiltroContains"] != null)
    {
        _Filtro = AutoCompleteFilterMode.Contains;
    }
        %>

