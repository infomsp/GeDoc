<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<%  string _AlturaEditor = "100%";
    if(ViewData["AltoEditor"] != null)
    {
        _AlturaEditor = ViewData["AltoEditor"].ToString();
    }
    %>
    <%= Html.Telerik().Editor()
            .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty))
            .Encode(true)
            .Localizable("es-AR")
            .HtmlAttributes(new { style = "height: " + _AlturaEditor + "; padding: 0px;" })
            .Value(Model)
            .FileBrowser(t => t.Upload("Upload", "ImageBrowser")
                       .Browse("Browse", "ImageBrowser")
                       .Thumbnail("Thumbnail", "ImageBrowser")
                       .DeleteFile("DeleteFile", "ImageBrowser")
                       .DeleteDirectory("DeleteDirectory", "ImageBrowser")
                       .CreateDirectory("CreateDirectory", "ImageBrowser")
                       )
    %>
