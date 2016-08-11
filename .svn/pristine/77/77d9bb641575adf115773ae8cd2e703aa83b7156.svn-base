<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    AutoCompleteFilterMode _Filtro = AutoCompleteFilterMode.StartsWith;
    if (ViewData["FiltroContains"] != null)
    {
        _Filtro = AutoCompleteFilterMode.Contains;
    }
        %>
<%= Html.Telerik().ComboBox()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .AutoFill(true)
    .SelectedIndex(0)
    .DropDownHtmlAttributes(new { style = "width:Auto;" })
    .Filterable(filtering =>
    {
        filtering.FilterMode(_Filtro);
    })
    .HighlightFirstMatch(true)
    .OpenOnFocus(true)
    .ClientEvents(e => e.OnChange("onChangeComboBoxCRUD_" + ViewData.TemplateInfo.GetFullHtmlFieldName(""))
                        .OnLoad("onComboBoxLoadCRUD_" + ViewData.TemplateInfo.GetFullHtmlFieldName("")))
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
%>
