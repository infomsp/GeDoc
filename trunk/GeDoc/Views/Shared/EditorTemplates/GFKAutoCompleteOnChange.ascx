<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    
    string fieldName = ViewData.TemplateInfo.GetFullHtmlFieldName("");
   
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

    string eventOnChange = fieldName + "_onChangeAutoCompleteCRUD";
    if (ViewData[fieldName + "_OnChange"] != null)
        eventOnChange = ViewData[fieldName + "_OnChange"].ToString();

    string eventOnLoad = fieldName + "_onLoadAutoCompleteCRUD";
    if (ViewData[fieldName + "_OnLoad"] != null)
        eventOnLoad = ViewData[fieldName + "_OnLoad"].ToString();

        
        %>
<%= Html.Telerik().AutoComplete()
    .Name(fieldName)
    .AutoFill(true)
    .Filterable(filtering =>
    {
        filtering.FilterMode(_Filtro);
    })
    .HighlightFirstMatch(true)
    .HtmlAttributes(new { style = _Style })
    .ClientEvents(e => e.OnChange(eventOnChange).OnLoad(eventOnLoad))
    .BindTo((IEnumerable<string>)ViewData[fieldName + "_Data"])
%>