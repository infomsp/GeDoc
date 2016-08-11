<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<decimal?>" %>

<%= Html.Telerik().CurrencyTextBoxFor(m => m)
        .InputHtmlAttributes(new {style="width: 100%; text-align: right;"})
        .HtmlAttributes(new {style="text-align: right;"})
        .IncrementStep(0)
        .Spinners(false)
        .MinValue(-99999999)
%>
