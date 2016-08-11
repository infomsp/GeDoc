<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var idDecreto;
    function onCommand(e) {
        if (e.name == "cmdverPDF") {
            var _Window = $("#verPDF").data("tWindow");
            if (e.dataItem['decLinkArchivo'] != "") {
                $("#divPDF").css("display", "none");
                $('#framePDF').attr('src', GetPathApp('Content/Archivos/Decretos/' + e.dataItem['decLinkArchivo'].toString()));

                $.post(GetPathApp('proDecreto/RegistrarLog'), { pAccion: "Examinar" }, function (data) {
                    if (data) {

                    }
                });
            }
            else {
                $("#divPDF").css("display", "block");
            }
            _Window.title("Ver Decreto N° " + e.dataItem['decNumero'].toString() + "...").center().open();
        }
        if (e.name == "cmdCRUDExpedientes") {
            var expGrid = $('#gridExpedientes').data('tGrid');
            var _WindowExp = $("#CRUDExpedientes").data("tWindow");

            idDecreto = e.dataItem['decId'];
            expGrid.rebind();
            _WindowExp.center().open();
        }
        if (e.name == "cmdCRUDResoluciones") {
            var empGrid = $('#gridResoluciones').data('tGrid');
            var _WindowEmp = $("#CRUDResoluciones").data("tWindow");

            idDecreto = e.dataItem['decId'];
            empGrid.rebind();
            _WindowEmp.center().open();
        }
    }
    function onCommandResoluciones(e) {
        if (e.name == "cmdverPDF") {
            var _Window = $("#verPDF").data("tWindow");

            if (e.dataItem['resLinkArchivo'] != "" && e.dataItem['resLinkArchivo'] != null) {
                $("#divPDF").css("display", "none");
                $('#framePDF').attr('src', GetPathApp('Content/Archivos/Resoluciones/' + e.dataItem['resLinkArchivo'].toString()));

                $.post(GetPathApp('proResolucion/RegistrarLog'), { pAccion: "Norma Legal" }, function (data) {
                    if (data) {

                    }
                });

                _Window.title("Ver Resolución N° " + e.dataItem['resNumero'].toString() + "...").center().open();
            }
            else {
                $("#divPDF").css("display", "block");
                _Window.title("Ver Resolución N° " + e.dataItem['resNumero'].toString() + "...").center().open();
            }
        }
    }
    function onDataBindingDecretos(e) {
        var _FiltrarPorConsiderando = "";
        for (var Indice = 0; Indice < e.filteredColumns.length; Indice++) {
            if (e.filteredColumns[Indice].member == "decConsiderando") {
                _FiltrarPorConsiderando = e.filteredColumns[Indice].filters[0].value;
            }
        }

        e.data = $.extend(e.data, { lstFiltros: _FiltrarPorConsiderando });
    }
    function onDataBinding(e) {
        e.data = $.extend(e.data, { idDecreto: idDecreto });
    }
    function onDataBindingResoluciones(e) {
        e.data = $.extend(e.data, { idDecreto: idDecreto });
    }
    function onSaveExpedientes(e) {
        var values = e.values;

        values.decId = idDecreto;
    }
    function onSaveResoluciones(e) {
        var values = e.values;

        values.decId = idDecreto;
    }
</script>
<% ViewData["AltoEditor"] = "200px"; %>
<%= Html.Telerik().Grid<GeDoc.proDecretos>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.decId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proDecreto", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                //.Select("_SelectEditing", "proDecreto")
                .Insert("_InsertEditing", "proDecreto")
                .Update("_SaveEditing", "proDecreto")
                .Delete("_DeleteEditing", "proDecreto");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proDecreto", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proDecreto", "Eliminar") });
                commands.Custom("cmdverPDF")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proDecreto", "Examinar") });
                commands.Custom("cmdCRUDExpedientes")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -33px -289px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proDecreto", "Expedientes") });
                commands.Custom("cmdCRUDResoluciones")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .SendDataKeys(true)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -190px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proDecreto", "Norma Legal") });
            }).Width("15%").Title("Acciones");
            columns.Bound(c => c.decEsAcuerdo).Width("10%").Title("Dec. Acuerdo").Visible(true)
            .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' disabled='disabled' name='cbx_decEsAcuerdo' <#= decEsAcuerdo ? checked = 'checked' : '' #> /></div>");
            columns.Bound(c => c.decNumero).Width("8%").Title("Número").Visible(true);
            columns.Bound(c => c.decFecha).Width("10%").Format("{0:dd/MM/yyyy}").Title("Fecha").Visible(true);
            columns.Bound(c => c.decConsiderando).Width("42%").Title("Detalle").Encoded(false).Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                .ClientTemplate("<label title='<#= decResuelve #>' style='cursor: pointer;' id='txtdecResuelve' style='white-space: nowrap;'><#= decResuelve #> </label>");
            columns.Bound(c => c.Resoluciones).Width("25%").Title("Resoluciones").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
        })
                .Editable(editing => editing
                        .DisplayDeleteConfirmation(true).Mode(GridEditMode.PopUp))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingDecretos", "proDecreto", new { lstFiltros = "" }))
            .ClientEvents(events => events.OnCommand("onCommand").OnEdit("onCommandEdit").OnDataBinding("onDataBindingDecretos"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .RowAction(row => row.Selected = row.DataItem.decId.Equals(ViewData["idDecreto"]))
%>

<!--Visor del PDF-->
<% Html.Telerik().Window()
        .Name("verPDF")
        .Title("Decreto")
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
<!-- Asignación Expedientes -->
<% Html.Telerik().Window()
        .Name("CRUDExpedientes")
        .Title("Asignación de Expedientes")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <%= Html.Telerik().Grid((IEnumerable<GeDoc.proDecretosExpedientes>)ViewData["Expedientes"])
            .Name("gridExpedientes")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.deceId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingExpedientes", "proDecreto")
                    .Insert("_InsertEditingExpedientes", "proDecreto")
                    .Update("_SaveEditingExpedientes", "proDecreto")
                    .Delete("_DeleteEditingExpedientes", "proDecreto");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width(20).Title("Acciones");
                columns.Bound(c => c.zonNombre).Width(80).Title("Zona").Visible(true);
                columns.Bound(c => c.zonCodigo).Width(30).Title("Código").Visible(true);
                columns.Bound(c => c.deceExpedienteNumero).Width(30).Title("Número").Visible(true);
                columns.Bound(c => c.deceExpedienteAnio).Width(30).Title("Año").Encoded(false).Visible(true);
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingExpedientes", "proDecreto", new { idDecreto = 1 }))
            .KeyboardNavigation(keyNav => keyNav.Enabled(true))
            .Editable(editing => editing
                            .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBinding").OnSave("onSaveExpedientes"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(770)
       .Height((int)Session["AlturaGrilla"] + 100)
       .Render();
%>
<!-- Resoluciones -->
<% Html.Telerik().Window()
        .Name("CRUDResoluciones")
        .Title("Resoluciones")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <%= Html.Telerik().Grid((IEnumerable<GeDoc.proDecretosResoluciones>)ViewData["Resoluciones"])
            .Name("gridResoluciones")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.drId);
            })
            .ToolBar(commands =>
            {
                commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingResoluciones", "proDecreto")
                    .Update("_SaveEditingResoluciones", "proDecreto")
                    .Insert("_InsertEditingResoluciones", "proDecreto")
                    .Delete("_DeleteEditingResoluciones", "proDecreto");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                    commands.Custom("cmdverPDF")
                        .Ajax(true)
                        .ButtonType(GridButtonType.Image)
                        .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                        .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proDecreto", "Examinar") });
                }).Width(20).Title("Acciones");
                columns.Bound(c => c.resNumero).Width(80).Title("Resolución").Visible(true);
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingResoluciones", "proDecreto", new { idDecreto = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnCommand("onCommandResoluciones").OnDataBinding("onDataBindingResoluciones").OnSave("onSaveResoluciones"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(200))
            .Sortable()
                %>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(570)
       .Height(300)
       .Render();
%>

<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>

