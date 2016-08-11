<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    AutoCompleteFilterMode _Filtro = AutoCompleteFilterMode.StartsWith;
    if (ViewData["FiltroContains"] != null)
    {
        _Filtro = AutoCompleteFilterMode.Contains;
    }

    string _Style = "";
    if (ViewData["StyleAutoComplete"] != null)
    {
        _Style = ViewData["StyleAutoComplete"].ToString();
    }
    else
    {
        _Style = "width: Auto;";
    }
        
        %>
<%= Html.Telerik().AutoComplete()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .AutoFill(true)
    .Filterable(filtering =>
    {
        filtering.FilterMode(_Filtro);
    })
    .HighlightFirstMatch(true)
    .HtmlAttributes(new { style = _Style })
    .BindTo((IEnumerable<string>)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
%>