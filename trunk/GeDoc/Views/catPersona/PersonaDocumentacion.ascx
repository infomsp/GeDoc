<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>

<script type="text/javascript">
    var _WindowCRUD;
    function onVerPDF(Archivo) {
        var _Window = $("#verPDF").data("tWindow");

        if (Archivo != "") {
            $("#divPDF").css("display", "none");
            $('#framePDF').attr('src', GetPathApp('Content/Archivos/FotosPersonal/' + Archivo));
            _Window.title("Archivo " + Archivo + "...").center().open();
        }
        else {
            $("#divPDF").css("display", "block");
            _Window.title("Archivo " + Archivo + "...").center().open();
        }
    }

    function onDataBindingDocumentacion(e) {
        var perId = -1;
        if (_DatosRegistro_catPersona != null) {
            perId = _DatosRegistro_catPersona.perId;
        }

        e.data = $.extend(e.data, { perId: perId });
    }
    function onSaveDocumentacion(e) {
        var values = e.values;

        values.perId = _DatosRegistro_catPersona.perId;
    }
    function onCompleteDocumentacion(e) {
        if (e.name == "update" || e.name == "insert" || e.name == "delete") {
            //$("#Grid").data("tGrid").ajaxRequest();
        }
    }
    
    function onCommandEditDocumentacion(e) {
        _WindowCRUD = $("#gridDocumentacionPopUp").data("tWindow");
        onCommandEdit(e);
    }

    function onCommandDocumentacion(e) {
        switch (e.name) {
            case "cmdDeleteDocumentacion":
                var grid = $(this).data("tGrid");
                var tr = $("#gridDocumentacion tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                jConfirm("¿Confirma Eliminar este Registro?", "Eliminar...", function (r) {
                    if (r) {
                        //AbrirWaiting();
                        grid.deleteRow(tr);
                    }
                });
                break;
            case "cmdModificarDocumentacion":
                var grid = $(this).data("tGrid");
                var tr = $("#gridDocumentacion tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
                grid.editRow(tr);
                break;
        }
    }

    function Descargar(e) {
        var _Post = GetPathApp('catPersona/Descargar');
        //alert("Descargar el archivo " + e);
        $.post(_Post, { Archivo: e });
    }

</script>
<% ViewData["AltoEditor"] = "200px";
   var LinkDeDocumentacion = "<#= perdVisualizar ? \"<div onclick='onVerPDF(\" + perdArchivoNombre + \")' title='Click para ver el archivo' style='cursor: pointer;' >\" + perdArchivoNombre + \"</div>\" : \"<a href='" + Url.Content("~/") + "catPersona/Descargar?Archivo=\" + perdArchivo + \"' title='Click para descargar archivo' style='cursor: pointer;' >\" + perdArchivoNombre + \"</a>\" #>";
   //LinkDeDocumentacion = LinkDeDocumentacion.Replace("*", "\"");
   %>
<% var DocumentacionWS = new List<catPersonaDocumentacionWS>(); %>
<!-- Documentacion -->
<% Html.Telerik().Window()
        .Name("wDocumentacion")
        .Title("Documentacion")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid(DocumentacionWS)
            .Name("gridDocumentacion")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.perdId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0", title = "Agregar Documentación" })%>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingDocumentacion", "catPersona", new { perId = -1})
                    .Update("_SaveEditingDocumentacion", "catPersona")
                    .Insert("_InsertEditingDocumentacion", "catPersona")
                    .Delete("_DeleteEditingDocumentacion", "catPersona");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Custom("cmdModificarDocumentacion")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0 -336px;" })
                        .HtmlAttributes(new { title = "Modificar datos de la Documentación Adjunta" })
                        .SendState(false);
                    commands.Custom("cmdDeleteDocumentacion")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .SendDataKeys(false)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -16px -336px;" })
                        .HtmlAttributes(new { title = "Eliminar Documentación" })
                        .SendState(false);
                }).Width("80px").Title("Acciones");
            columns.Bound(c => c.perdFecha).Format("{0:dd/MM/yyyy}").Width("100px").Title("Fecha").Visible(true);
            columns.Bound(c => c.perdFecha).Format("{0:hh:mm:ss}").Width("80px").Title("Hora").Visible(true);
            columns.Bound(c => c.perdArchivo).Width("150px").Title("Archivo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<#= perdVisualizar #>");
            columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("150px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' ><#= Usuario.usrApellidoyNombre #></label>");
            columns.Bound(c => c.perdObservaciones).Width("350px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= perdObservaciones #>' style='cursor: pointer;' ><#= perdObservaciones #></label>");
            })
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingDocumentacion").OnCommand("onCommandDocumentacion").OnEdit("onCommandEditDocumentacion").OnSave("onSaveDocumentacion").OnComplete("onCompleteDocumentacion"))
            .Filterable()
            .Resizable(resizing => resizing.Columns(true))
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(310))
            .Sortable()
            .Render();
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Modal(true)
       .Width(1024)
       .Height(400)
       .Render();
%>

<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("verPDF")
        .Title("Documentación")
        .Visible(false)
        .Content(() => 
        {%> 
            <div id="divPDF" style="width: 100%; height: 100%; display: none;">
                <table style="border: none; margin:10px;">
                    <tr style="border: none; margin:0px;">
                        <td style="border: none; margin:0px;">
                            <img src="<%= Url.Content("~/Content") %>/General/Notificacion/Vista Internet Security OFF.png" width="40px" style="margin-top:-5px" />
                        </td>
                        <td style="border: none; margin:0px;">
                            <asp:Label ID="Label1" runat="server" style="margin-left:-5px" ForeColor="Red">Archivo no encontrado...</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <iframe id="framePDF" src='' width="100%" height="100%" >
            </iframe>
        <%})
        .Buttons(b => b.Maximize().Close())
        .Draggable(true)
        .Scrollable(false)
        .Resizable()
        .Width(870)
        .Height(460)
        .Render();
%>
