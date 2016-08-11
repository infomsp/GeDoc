<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    string _Style = "";
    if (ViewData["StyleComboBox"] != null)
    {
        _Style = ViewData["StyleComboBox"].ToString();
    }
    else
    {
        _Style = "width:Auto;";
    }
        
        %>

<%= Html.Telerik().ComboBox()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .AutoFill(true)
    .DropDownHtmlAttributes(new { style = _Style })
    .Filterable(filtering =>
    {
        filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
    })
    .OpenOnFocus(true)
    .HtmlAttributes(new { style = _Style })
    .ClientEvents(eventos => eventos.OnLoad("onLoadComboBox_" + ViewData.TemplateInfo.GetFullHtmlFieldName("")))
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
%>