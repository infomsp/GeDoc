<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    string fieldName = ViewData.TemplateInfo.GetFullHtmlFieldName("");
    
    AutoCompleteFilterMode _Filtro = AutoCompleteFilterMode.StartsWith;
    
    if (ViewData["FiltroContains"] != null)
        _Filtro = AutoCompleteFilterMode.Contains;
    
    int selectedIndex = 0;
    if (ViewData[fieldName + "_SelectedIndex"] != null)
        selectedIndex = int.Parse(ViewData[fieldName + "_SelectedIndex"].ToString());

    string eventOnChange = "onChangeComboBoxCRUD";
    if (ViewData[fieldName + "_eventOnChange"] != null)
        eventOnChange = ViewData[fieldName + "_eventOnChange"].ToString() ;

    string eventOnLoad = "onComboBoxLoadCRUD";
    if (ViewData[fieldName + "_eventOnLoad"] != null)
        eventOnLoad = ViewData[fieldName + "_eventOnLoad"].ToString();

    string dropDownStyle = "width:Auto;";
    if (ViewData[fieldName + "_dropDownStyle"] != null)
        dropDownStyle = ViewData[fieldName + "_dropDownStyle"].ToString();

    string style = "width:Auto;";
    if (ViewData[fieldName + "_style"] != null)
        style = ViewData[fieldName + "_style"].ToString();
    
%>


<%= Html.Telerik().ComboBox()
    .Name(fieldName)
    .AutoFill(true)
    .SelectedIndex(selectedIndex)
    .DropDownHtmlAttributes(new { style = dropDownStyle})
    .HtmlAttributes(new { style = style })
    .Filterable(filtering =>
    {
        filtering.FilterMode(_Filtro);
    })
    .HighlightFirstMatch(true)
    .OpenOnFocus(true)
    .ClientEvents(e => e.OnChange(eventOnChange).OnLoad(eventOnLoad))
    .BindTo((SelectList)ViewData[fieldName + "_Data"])
%>
