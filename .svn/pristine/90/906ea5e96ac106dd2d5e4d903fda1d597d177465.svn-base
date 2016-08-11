<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    AutoCompleteFilterMode _Filtro = AutoCompleteFilterMode.StartsWith;
    if (ViewData["FiltroContains"] != null)
    {
        _Filtro = AutoCompleteFilterMode.Contains;
    }

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
        filtering.FilterMode(_Filtro);
    })
    .OpenOnFocus(true)
    .HtmlAttributes(new { style = _Style })
    .ClientEvents(eventos => eventos.OnLoad("onLoadComboBox_" + ViewData.TemplateInfo.GetFullHtmlFieldName("")).OnChange("onChangeComboBox_" + ViewData.TemplateInfo.GetFullHtmlFieldName("")))
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
%>