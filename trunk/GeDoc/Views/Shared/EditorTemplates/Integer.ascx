<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<int?>" %>

<% bool _Spinner = false;
   if (ViewData["ShowSpinner"] != null)
   {
      _Spinner = (bool)ViewData["ShowSpinner"]; 
   }
        %>
<%= Html.Telerik().IntegerTextBoxFor(m => m)
        .InputHtmlAttributes(new { style = "width:100%; text-align: right;" })
        .HtmlAttributes(new {style="text-align: right;"})
        .Spinners(_Spinner)
        .NumberGroupSeparator("")
        .MinValue(int.MinValue)
        .MaxValue(int.MaxValue)
%>
