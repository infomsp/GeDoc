<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    function onCancelarImportacionDeArchivo() {
        $('#wImportacionDeArchivo').data("tWindow").close();
    }

    function onAceptarImportacionDeArchivo() {
        $('#wImportacionDeArchivo').data("tWindow").close();
        $("#GridImportacion").trigger("onAceptarDesdeArchivo");
    }

    function onActivateImportacionDeArchivo(e) {
        LimpiarImportacion();
    }
    
    function onCloseImportacionDeArchivo(e) {
    }

    function onUpload(e) {
        // Array with information about the uploaded files
        var files = e.files;

        // Check the extension of each file and abort the upload if it is not .jpg
        $.each(files, function () {
            if (this.extension != ".txt" && this.extension != ".xls" && this.extension != ".xlsx" && this.extension != ".csv") {
                jAlert("El archivo seleccionado es Incorrecto", "Error...");
                e.preventDefault();
                return false;
            }
        });
    }
    
    function onSuccess(e) {
        // Array with information about the uploaded files
        var files = e.files;
        debugger;
        if (e.operation == "upload") {
            var _Post = GetPathApp('Utiles/_GetArchivoImportado');
            $('#GridImportacion').html('<img src="<%= Url.Content("~/Content/General/WaitingIndicator.gif") %>" width="22px" alt="" />');
            $.ajax({
                url: _Post,
                data: { ArchivoImportado: "" },
                type: 'POST',
                error: function (xhr, ajaxOptions, thrownError) {
                    debugger;
                    jAlert('Falló el acceso al servidor', '¡Atención!');
                },
                success: function (data) {
                    debugger;
                    if (data.length > 0) {
                        $('#GridImportacion').html(data);
                        $('#wImportacionDeArchivo').css({ 'height': 630, 'width': 1000 });
                        $('#wImportacionDeArchivo').find('div.t-window-content').css({ 'height': 600, 'width': 988 });
                        
                        var windowElement = $('#wImportacionDeArchivo').data('tWindow');
                        windowElement.center();

                        $("#GridImportacion").trigger('onSuccessDesdeArchivo', {
                            // Lanzamos el evento y pasamos un objeto como parametro (puede ser tambien un array).
                            Tipo: "OK"
                        });
                    }
                    else {
                        jAlert("Archivo no reconocido", '¡Atención!');
                    }
                }
            });
        } else {
            if (e.operation == "remove") {
                LimpiarImportacion();
                
                var windowElement = $('#wImportacionDeArchivo').data('tWindow');
                windowElement.center();
                debugger;
                $("#GridImportacion").trigger('onSuccessDesdeArchivo', {
                    // Lanzamos el evento y pasamos un objeto como parametro (puede ser tambien un array).
                    Tipo: "Remove"
                });
            }
        }
    }

    function LimpiarImportacion() {
        $('#GridImportacion').html("");
        $('#wImportacionDeArchivo').css({ 'height': 168, 'width': 302 });
        $('#wImportacionDeArchivo').find('div.t-window-content').css({ 'height': 138, 'width': 290 });
    }
    
    function onError(e) {
        // Array with information about the uploaded files
        var files = e.files;

        if (e.operation == "upload") {
            jAlert("No se pudo cargar el archivo", "Error");
        }

        // Suppress the default error message
        e.preventDefault();
    }
</script>
<!-- Filtrar desde Archivo -->
<% Html.Telerik().Window()
        .Name("wImportacionDeArchivo")
        .Title("Importación...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div id="divUpArchivo">
                <%= Html.Telerik().Upload()
                            .Name("attachments")
                            .Multiple(false)
                            .ShowFileList(true)
                            .Localizable("es-AR")
                            .ClientEvents(events => events
                                            .OnUpload("onUpload")
                                            .OnSuccess("onSuccess")
                                            .OnError("onError"))
                            .Async(async => async
                                .Save("Save", "Upload")
                                .Remove("Remove", "Upload")
                                .AutoUpload(true)
                            )
                    %>
            </div>
            <div id="GridImportacion" style="margin-bottom: 10px;"></div>
            <div style="text-align: center;">
<%-- ReSharper disable UnknownCssClass --%>
                <button class="t-button t-grid-cancel" onclick="onAceptarImportacionDeArchivo();">Aceptar</button>
                <button class="t-button t-grid-cancel" onclick="onCancelarImportacionDeArchivo();">Cancelar</button>
<%-- ReSharper restore UnknownCssClass --%>
            </div>
            <%
        })
       //.Buttons(b => b.Close())
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       //.Width(505)
       .Height(130)
       .ClientEvents(events => events.OnActivate("onActivateImportacionDeArchivo").OnClose("onCloseImportacionDeArchivo"))
       .Render();
%>

