<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>

<%
    string Nombre = ViewData.TemplateInfo.GetFullHtmlFieldName("");
    if (Nombre == null)
    {
        Nombre = "";
    }
%>

<%= Html.Telerik().Upload()
            .Name("attachments")
            .Multiple(false)
            .ShowFileList(true)
            .Localizable("es-AR")
            .Async(async => async
                .Save("Save", "Upload")
                .Remove("Remove", "Upload")
                .AutoUpload(true)
            )
    %>

