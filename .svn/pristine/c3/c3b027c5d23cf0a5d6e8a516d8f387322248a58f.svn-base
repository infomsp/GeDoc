<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Telerik().DropDownList()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .DropDownHtmlAttributes(new { style = "width:Auto;" })
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
%>