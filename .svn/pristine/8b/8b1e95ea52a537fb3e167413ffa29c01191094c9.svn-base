<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GeDoc.catPersonas>" %>

<% using (Ajax.BeginForm("Create", new AjaxOptions() { OnSuccess="onSuccess" }))
   { %>
<%= Html.ValidationSummary(true)%>
<% Html.Telerik().TabStrip()
        .Name("tabEmpleados")
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
                            <%: Html.LabelFor(model => model.perPadron)%>
                            <%: Html.EditorFor(model => model.perPadron)%>
                            <%: Html.ValidationMessageFor(model => model.perPadron)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perApellidoyNombre)%>
                            <%: Html.EditorFor(model => model.perApellidoyNombre)%>
                            <%: Html.ValidationMessageFor(model => model.perApellidoyNombre)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.tipoIdSexo)%>
                            <%: Html.EditorFor(model => model.tipoIdSexo)%>
                            <%: Html.ValidationMessageFor(model => model.tipoIdSexo)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perDNI)%>
                            <%: Html.EditorFor(model => model.perDNI)%>
                            <%: Html.ValidationMessageFor(model => model.perDNI)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perCUIL)%>
                            <%: Html.EditorFor(model => model.perCUIL)%>
                            <%: Html.ValidationMessageFor(model => model.perCUIL)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perFechaNacimiento)%>
                            <%: Html.EditorFor(model => model.perFechaNacimiento)%>
                            <%: Html.ValidationMessageFor(model => model.perFechaNacimiento)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perTelefono)%>
                            <%: Html.EditorFor(model => model.perTelefono)%>
                            <%: Html.ValidationMessageFor(model => model.perTelefono)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perEmail)%>
                            <%: Html.EditorFor(model => model.perEmail)%>
                            <%: Html.ValidationMessageFor(model => model.perEmail)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perObservaciones)%>
                            <%: Html.EditorFor(model => model.perObservaciones)%>
                            <%: Html.ValidationMessageFor(model => model.perObservaciones)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.secId)%>
                            <%: Html.EditorFor(model => model.secId)%>
                            <%: Html.ValidationMessageFor(model => model.secId)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.perFoto)%>
                            <%: Html.EditorFor(model => model.perFoto)%>
                            <%: Html.ValidationMessageFor(model => model.perFoto)%>
                        </div>
                    </div>
                <%});
        })
    .SelectedIndex(0)
    .Render();%>
<% }  %>

<script type="text/javascript">
    function onSuccess(e) {
        alert('Formulario Guardado');
    }
</script>
