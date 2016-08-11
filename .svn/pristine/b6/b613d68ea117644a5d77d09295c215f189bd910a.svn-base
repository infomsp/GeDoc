<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GeDoc.proResoluciones>" %>

<% using (Html.BeginForm()) { %>
<%= Html.ValidationSummary(true) %>
<% Html.Telerik().TabStrip()
        .Name("tabResoluciones")
        .HtmlAttributes(new { style = "height: 98%; padding: 0px; background: #FFFFFF; border: 0px;" })
        .Items(tabstrip =>
        {
            tabstrip.Add()
                .Text("General")
                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                .Content(() =>
                { %>
                    <div id="EditarGeneral" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.resNumero) %>
                            <%: Html.EditorFor(model => model.resNumero)%>
                            <%: Html.ValidationMessageFor(model => model.resNumero)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.resFecha) %>
                            <%: Html.EditorFor(model => model.resFecha)%>
                            <%: Html.ValidationMessageFor(model => model.resFecha)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.resEsImportante) %>
                            <%: Html.EditorFor(model => model.resEsImportante)%>
                            <%: Html.ValidationMessageFor(model => model.resEsImportante)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.resNotificacionVencimiento) %>
                            <%: Html.EditorFor(model => model.resNotificacionVencimiento)%>
                            <%: Html.ValidationMessageFor(model => model.resNotificacionVencimiento)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.resNotificacionDiaInicio) %>
                            <%: Html.EditorFor(model => model.resNotificacionDiaInicio)%>
                            <%: Html.ValidationMessageFor(model => model.resNotificacionDiaInicio)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.resLinkArchivo) %>
                            <%: Html.EditorFor(model => model.resLinkArchivo)%>
                            <%: Html.ValidationMessageFor(model => model.resLinkArchivo)%>
                        </div>
                    </div>
                <%});
            tabstrip.Add()
                .Text("Considerando")
                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                .Content(() =>
                { %>
                    <div id="EditarConsiderando" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                        <div class="editor-label">
                            <%: Html.EditorFor(model => model.resConsiderando) %>
                            <%: Html.ValidationMessageFor(model => model.resConsiderando) %>
                        </div>
                    </div>
                <%});
            tabstrip.Add()
                .Text("Resuelve")
                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                .Content(() =>
                { %>
                    <div id="EditarResuelve" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                        <div class="editor-label">
                            <%: Html.EditorFor(model => model.resResuelve)%>
                            <%: Html.ValidationMessageFor(model => model.resResuelve)%>
                        </div>
                    </div>
                <%});
        })
    .SelectedIndex(0)
    .Render();%>
<% }  %>
