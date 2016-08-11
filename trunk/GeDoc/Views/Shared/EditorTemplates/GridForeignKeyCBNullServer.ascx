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
    .DropDownHtmlAttributes(new { style = _Style })
    .Filterable(filtering =>
    {
        filtering.FilterMode(AutoCompleteFilterMode.Contains);
    })
    .OpenOnFocus(true)
    .HtmlAttributes(new { style = _Style })
    .DataBinding(dataBinding => dataBinding
            //Ajax binding
            .Ajax()
                //Setting the delay
                .Select(ViewBag.NombreAccion, ViewBag.NombreControlador)
    )
%>
