<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GeDoc.proResoluciones>" %>
<%= Html.Telerik().Window()
            .Name("Window")
            .Title("Edición")
            .Content(() => {%>
            <%})
            .Width(400)
            .Draggable(true)
            .Modal(true)
            .Visible(false);
    %>
<style type="text/css">
    .field-validation-error
    {
        position: absolute;
        display: block;
    }
    
    * html .field-validation-error { position: relative; }
    *+html .field-validation-error { position: relative; }
    
    .field-validation-error span
    {
        position: absolute;
        white-space: nowrap;
        color: red;
        padding: 0px 5px 0px 5px;
        background: no-repeat 0 0;
        vertical-align: middle;
        font-size: small;
        text-decoration: blink;
    }
    
    /* in-form editing */
    .t-edit-form-container
    {
        width: 850px;
        margin: 1em;
    }
    
    .t-edit-form-container .editor-label,
    .t-edit-form-container .editor-field
    {
        padding-bottom: 1em;
        float: left;
    }
    
    .t-edit-form-container .editor-label
    {
        width: 30%;
        text-align: right;
        padding-right: 3%;
        clear: left;
    }
    
    .t-edit-form-container .editor-field
    {
        width: 60%;
    }
</style>
