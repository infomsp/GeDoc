<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var _RowIndex = -1;
    var _DatosRegistro;

    function onRowSelected(e) {
        var row = e.row;
        var grid = $(this).data("tGrid");
        var dataItem = grid.dataItem(row);

        _RowIndex = e.row.rowIndex;
        _DatosRegistro = dataItem;
    }
    function onCommand(e) {
        if (("cmdEditar, cmdEliminar").indexOf(e.name) >= 0) {
            if (_RowIndex < 0) {
                jAlert('Debe seleccionar un Registro.', 'Error...');
                return;
            }
        }
        switch (e.name) {
//            case "cmdAgregar":
//                var grid = $(this).data("tGrid");
//                grid.addRow();
//                break;
            case "cmdEditar":
                var grid = $(this).data("tGrid");
                var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
                //grid.editRow(tr);

                var _Window = $("#wEditarFODA").data("tWindow");
                _Window.center().open();
                break;
            case "cmdMatriz":
                var _Window = $("#verPDF").data("tWindow");
                $("#divPDF").css("display", "none");
                $('#framePDF').attr('src', GetPathApp('Content/Archivos/MatrizFODA.pdf'));
                _Window.title("Ver...").center().open();
                break;
            case "cmdSugerencias":
                var _Window = $("#verPDF").data("tWindow");
                $("#divPDF").css("display", "none");
                $('#framePDF').attr('src', GetPathApp('Content/Archivos/Sugerencias.pdf'));
                _Window.title("Ver...").center().open();
                break;
        }
    }
    function onComplete(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }
    function onRowDataBound(e) {
        if (_RowIndex > -1) {
            var tr = $("#Grid tbody tr:eq(" + (_RowIndex + 1).toString() + ")");
            tr.attr("class", 't-state-selected');
        }
    }
    function onCommandEditFODA(e) {
        if (!_DatosRegistro.PermiteEditar) {
            $('#fodaFortaleza').attr('disabled', 'disabled');
            $('#fodaOportunidad').attr('disabled', 'disabled');
            $('#fodaDebilidad').attr('disabled', 'disabled');
            $('#fodaAmenaza').attr('disabled', 'disabled');
            var _winPopup = $('#wEditarFODA').data('tWindow');
            _winPopup.title('Editar (Solo puede consultar, usted pertenece a otra oficina)');
            $('.t-button.t-grid-update').css('display', 'none');
        }
        
        onCommandEdit(e);
    }

    function onOpenWindow() {
        $('#txtFortaleza').text(_DatosRegistro.fodaFortaleza == null ? '' : _DatosRegistro.fodaFortaleza);
        $('#txtOportunidad').text(_DatosRegistro.fodaOportunidad == null ? '' : _DatosRegistro.fodaOportunidad);
        $('#txtDebilidad').text(_DatosRegistro.fodaDebilidad == null ? '' : _DatosRegistro.fodaDebilidad);
        $('#txtAmenaza').text(_DatosRegistro.fodaAmenaza == null ? '' : _DatosRegistro.fodaAmenaza);
        $('#txtFortaleza')[0].value = _DatosRegistro.fodaFortaleza == null ? '' : _DatosRegistro.fodaFortaleza;
        $('#txtOportunidad')[0].value = _DatosRegistro.fodaOportunidad == null ? '' : _DatosRegistro.fodaOportunidad;
        $('#txtDebilidad')[0].value = _DatosRegistro.fodaDebilidad == null ? '' : _DatosRegistro.fodaDebilidad;
        $('#txtAmenaza')[0].value = _DatosRegistro.fodaAmenaza == null ? '' : _DatosRegistro.fodaAmenaza;
        debugger;
        if (!_DatosRegistro.PermiteEditar) {
            $('#txtFortaleza').attr('disabled', 'disabled');
            $('#txtOportunidad').attr('disabled', 'disabled');
            $('#txtDebilidad').attr('disabled', 'disabled');
            $('#txtAmenaza').attr('disabled', 'disabled');
            var _winPopup = $('#wEditarFODA').data('tWindow');
            _winPopup.title('Consultar (Solo puede consultar, usted pertenece a otra oficina)');
        } else {
            $('#txtFortaleza').removeAttr('disabled');
            $('#txtOportunidad').removeAttr('disabled');
            $('#txtDebilidad').removeAttr('disabled');
            $('#txtAmenaza').removeAttr('disabled');
            var _winPopup = $('#wEditarFODA').data('tWindow');
            _winPopup.title('Editar');

            var _Motivo = $("#txtFortaleza");
            _Motivo.focus();
        }

    }

    function onCancelar() {
        var _wFoda = $("#wEditarFODA").data("tWindow");

        _wFoda.close();
    }

    function onAceptar() {
        debugger;
        if (!_DatosRegistro.PermiteEditar) {
            onCancelar();
            return;
        }

        var _Fortaleza = $('#txtFortaleza')[0].value;
        var _Oportunidad = $('#txtOportunidad')[0].value;
        var _Debilidad = $('#txtDebilidad')[0].value;
        var _Amenaza = $('#txtAmenaza')[0].value;

        if (_Fortaleza == null) {
            _Fortaleza == ""
        }

        if (_Oportunidad == null) {
            _Oportunidad == ""
        }

        if (_Debilidad == null) {
            _Debilidad == ""
        }

        if (_Amenaza == null) {
            _Amenaza == ""
        }
        debugger;
        onCancelar();
        AbrirWaiting();
        $.post(GetPathApp('catFODA/_SaveEditing'), { id: _DatosRegistro.fodaId, Fortaleza: _Fortaleza, Oportunidad: _Oportunidad, Debilidad: _Debilidad, Amenaza: _Amenaza }, function (data) {
            if (data) {
                $("#Grid").data("tGrid").rebind();
                CerrarWaiting();
                //$("#Grid").dataBind(data);

            }
        });
    }

</script>


<!-- TAB  -->
<% Html.Telerik().TabStrip()
        .Name("tabResultado")
        .HtmlAttributes(new { style = "height: 98%; padding: 0px; background: transparent; border: 0px; margin-left: -4px;" })
        .Items(tabstrip =>
        {
            tabstrip.Add()
                .Text("Visión")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() =>
                { %>
                    <iframe id="framePDFVision" src='<%= Url.Content("~/Content") %>/Archivos/Vision.pdf' width="100%" height="100%" >
                    </iframe>
                <%});
            tabstrip.Add()
                .Text("Misión")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() =>
                { %>
                    <iframe id="Iframe2" src='<%= Url.Content("~/Content") %>/Archivos/Mision.pdf' width="100%" height="100%" >
                    </iframe>
                <%});
            tabstrip.Add()
                .Text("Ejes Estratégicos")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() =>
                { %>
                    <iframe id="Iframe1" src='<%= Url.Content("~/Content") %>/Archivos/EjesEstrategicos.pdf' width="100%" height="100%" >
                    </iframe>
                <%});
            tabstrip.Add()
                .Text("FODA")
                .ContentHtmlAttributes(new { style = "height: 480px; padding: 8px;" })
                .Content(() =>
                { %>
                    <div style="overflow: hidden; height: 510px;" >
                    <%= Html.Telerik().Grid<GeDoc.catFODAs>()
                            .Name("Grid")
                            .DataKeys(keys =>
                            {
                                keys.Add(p => p.fodaId);
                            })
                            .Localizable("es-AR")
                            .ToolBar(commands =>
                            {
                                commands.Custom().Ajax(true).Name("cmdEditar").ButtonType(GridButtonType.ImageAndText).Text("Editar")
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catFODA", "Modificar") });
                                commands.Custom().Ajax(true).Name("cmdMatriz").ButtonType(GridButtonType.ImageAndText).Text("Matriz")
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -289px;" });
                                commands.Custom().Ajax(true).Name("cmdSugerencias").ButtonType(GridButtonType.ImageAndText).Text("Sugerencias")
                                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" });
                            })
                            .DataBinding(dataBinding =>
                            {
                                dataBinding.Ajax()
                                    .Select("_SelectEditing", "catFODA")
                                    .Update("_SaveEditing", "catFODA");
                            })
                            .Columns(columns =>
                            {
                                columns.Bound(c => c.Oficina.ofiNombre).Width("150px").Title("Oficina").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= Oficina.ofiNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= Oficina.ofiNombre #></label>");
                                columns.Bound(c => c.fodaFortaleza).Width("150px").Title("Fortaleza").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= fodaFortaleza #>' style='cursor: pointer; white-space: nowrap;' ><#= fodaFortaleza #></label>");
                                columns.Bound(c => c.fodaOportunidad).Width("150px").Title("Oportunidad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= fodaOportunidad #>' style='cursor: pointer; white-space: nowrap;' ><#= fodaOportunidad #></label>");
                                columns.Bound(c => c.fodaDebilidad).Width("150px").Title("Debilidad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= fodaDebilidad #>' style='cursor: pointer; white-space: nowrap;' ><#= fodaDebilidad #></label>");
                                columns.Bound(c => c.fodaAmenaza).Width("250px").Title("Descripción").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= fodaAmenaza #>' style='cursor: pointer; white-space: nowrap;' ><#= fodaAmenaza #></label>");
                                columns.Bound(c => c.fodaFechaUltimoCambio).Width("80px").Format("{0:dd/MM/yyyy}").Title("Modificado").Visible(true);
                                columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("150px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer; white-space: nowrap;' ><#= Usuario.usrApellidoyNombre #></label>");
                            })
                                    .Editable(editing => editing
                                            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                                    .Pageable((paging) =>
                                               paging.Enabled(true)
                                                    .PageSize(((int)Session["FilasPorPagina"])))
                                    .ClientEvents(events => events.OnEdit("onCommandEditFODA").OnRowSelected("onRowSelected").OnCommand("onCommand").OnComplete("onComplete").OnRowDataBound("onRowDataBound"))
                                    .Footer(true)
                                .Filterable()
                                .Selectable()
                                .Scrollable(scroll => scroll.Enabled(true).Height((394)))
                                .Resizable(resizing => resizing.Columns(true))
                                .Sortable()
                                .HtmlAttributes(new { style = "width: 99.8%;" })

                    %>
                    </div>
                <%});
        })
    .SelectedIndex(0)
    .Render();
 %>
<!-- FIN TAB -->

<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("verPDF")
        .Title("Ver...")
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

<!-- Edición -->
<% string _btnAceptar = "";
   string _btnCancelar = "";
   _btnAceptar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -33px -335px;";
   _btnCancelar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -49px -335px;";
%>
<% Html.Telerik().Window()
        .Name("wEditarFODA")
        .Title("Editar FODA")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div style="margin-top: 8px;">
                <label id="lblFortaleza" style="vertical-align: top; margin-left: 8px;">Fortaleza:</label>
                <%= Html.TextArea("txtFortaleza", new { style = "width: 400px; height: 285px;" })%>
                <label id="Label2" style="vertical-align: top; margin-left: 8px;">Oportunidad:</label>
                <%= Html.TextArea("txtOportunidad", new { style = "width: 400px; height: 285px;" })%>
            </div>
            <div style="margin-top: 8px;">
                <label id="Label3" style="vertical-align: top; margin-left: 9px;">Debilidad:</label>
                <%= Html.TextArea("txtDebilidad", new { style = "width: 400px; height: 285px;" })%>
                <label id="Label4" style="vertical-align: top; margin-left: 29px;">Amenaza:</label>
                <%= Html.TextArea("txtAmenaza", new { style = "width: 400px; height: 285px;" })%>
            </div>
            <div class="BordeRedondeado" style="text-align: center; margin-top: 8px; padding: 8px; border-color: lightgray; margin-left: 8px; margin-right: 8px;">
                <button id="btnAceptar" class="t-button" onclick="onAceptar()" style="width: 130px;">
                    Aceptar
                </button>
                <button class="t-button" onclick="onCancelar()" style="width: 130px;">
                    Cancelar
                </button>
            </div>
        <%})
       .Buttons(b => b.Close())
       .ClientEvents(e => e.OnActivate("onOpenWindow"))
       .Draggable(true)
       .Scrollable(false)
       .Modal(true)
       .Width(1000)
       .Height(660)
       .Render();
%>
