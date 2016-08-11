<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime?>" %>

<script>
    function onChangeDatePicker() {
    }
</script>

<%
    bool _OpenOnFocus = true;
    if (ViewBag.OpenOnFocus != null)
    {
        _OpenOnFocus = false;
    }
    var _eventoOnChange = "onChangeDatePicker";
    if (ViewBag.eventoOnChange != null)
    {
        _eventoOnChange = ViewBag.eventoOnChange + ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty);
        _eventoOnChange = _eventoOnChange.Replace(".", "_");
    }
%>

<%= Html.Telerik().DatePicker()
        .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
        .ShowButton(true)
        .OpenOnFocus(_OpenOnFocus)
        .TodayButton()
        .ClientEvents(e => e.OnChange(_eventoOnChange))
        .HtmlAttributes(new { id = ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty) + "_wrapper" })
%>

<!--.Value(Model > DateTime.MinValue.Date ? Model.Value.Date : DateTime.Today.Date) -->