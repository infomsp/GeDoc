<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    AutoCompleteFilterMode _Filtro = AutoCompleteFilterMode.StartsWith;
    if (ViewData["FiltroContains"] != null)
    {
        _Filtro = AutoCompleteFilterMode.Contains;
    }

    string _Style = "";
    _Style = ViewData["StyleComboBox"] != null ? ViewData["StyleComboBox"].ToString() : "width:Auto;";
    int selectedIndex = 0;
    if(ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_SelectedIndex"] != null)
        selectedIndex = int.Parse(ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_SelectedIndex"].ToString());
        
        %>
<%= Html.Telerik().ComboBox()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .AutoFill(true)
    .SelectedIndex(selectedIndex)
    .DropDownHtmlAttributes(new { style = _Style })
    .Filterable(filtering =>
    {
        filtering.FilterMode(_Filtro);
    })
    .HighlightFirstMatch(true)
    .OpenOnFocus(true)
    .HtmlAttributes(new { style = _Style })
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
%>