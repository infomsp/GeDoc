<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script type="text/javascript">
    var idUsuario;
    var tvValores = "";
    function onCommand(e) {
        if (e.name == "cmdPermisos") {
            var _tvPermisos = $('#tvPermisos').data('tTreeView');
            var _Window = $("#Permisos").data("tWindow");

            idUsuario = e.dataItem['usrId'];

            _tvPermisos.ajaxRequest();

            _Window.title("Permisos para " + e.dataItem['usrApellidoyNombre'].toString() + "...").center().open();
        } else if (e.name == "cmdCentrosDeSalud") {
            var estGrid = $('#gridCentrosDeSalud').data('tGrid');
            var _WindowEst = $("#CRUDCentrosDeSalud").data("tWindow");

            idUsuario = e.dataItem['usrId'];
            $("#lblNombrePersona2").text('Centros de Salud relacionados a ' + e.dataItem['usrApellidoyNombre']);
            estGrid.rebind();
            _WindowEst.center().open();
        } else if (e.name == "cmdOficinas") {
            var estGrid = $('#gridOficinas').data('tGrid');
            var _WindowEst = $("#CRUDOficinas").data("tWindow");

            idUsuario = e.dataItem['usrId'];
            $("#lblNombrePersonaOficinas").text('Oficinas relacionados a ' + e.dataItem['usrApellidoyNombre']);
            estGrid.rebind();
            _WindowEst.center().open();
        }
    }
    function onDataBindingPermisos(e) {
        if (idUsuario == null) {
            return;
        }
        var _tvPermisos = $('#tvPermisos').data('tTreeView');

        tvValores = "";

        $.post(GetPathApp('sisUsuario/getPermisosUsuario'), { idUsuario: idUsuario }, function (data) {
            if (data.data == null) {
                _tvPermisos.bindTo(data);
            }
            else {
                _tvPermisos.bindTo(data.data);
            }
        });
    }
    function onCancelar() {
        var _Window = $("#Permisos").data("tWindow");

        _Window.close();
    }

    function onAceptar() {
        var _Window = $("#Permisos").data("tWindow");
        $.post(GetPathApp('sisUsuario/setPermisosUsuario'), { idUsuario: idUsuario, Datos: tvValores }, function (data) {
            if (data) {
                _Window.close();
            }
            else {
                $('#lblMensaje').text("Error al intentar asignar permisos...");
            }
        });
    }

    function onChequedPermiso(e) {
        var _tvPermisos = $('#tvPermisos').data('tTreeView');
        var _Clave = _tvPermisos.getItemValue(e.item).toString();
        var _Resultado = tvValores.indexOf(_tvPermisos.getItemValue(e.item), 0);

        if (_Resultado == -1) {
            tvValores = tvValores + _Clave + "#upAutorizado:" + (e.checked ? "1" : "0") + "\n";
        }
        else {
            if (e.checked) {
                tvValores = tvValores.replace("upAutorizado:0", "upAutorizado:1");
            }
            else {
                tvValores = tvValores.replace("upAutorizado:1", "upAutorizado:0");
            }
        }
    }

    function onDataBindingCentrosDeSalud(e) {
        e.data = $.extend(e.data, { idUsuario: idUsuario });
    }
    function onSaveCentrosDeSalud(e) {
        var values = e.values;

        values.usrId = idUsuario;
    }
    function onCompleteCentrosDeSalud(e) {
        if (e.name == "update" || e.name == "insert") {
            //            var perGrid = $('#Grid').data('tGrid');
            //            perGrid.rebind();
        }
    }
    function onDataBindingOficinas(e) {
        e.data = $.extend(e.data, { idUsuario: idUsuario });
    }
    function onSaveOficinas(e) {
        var values = e.values;

        values.usrId = idUsuario;
    }
    function onCompleteOficinas(e) {
        if (e.name == "update" || e.name == "insert") {
            //            var perGrid = $('#Grid').data('tGrid');
            //            perGrid.rebind();
        }
    }
    </script>
<% ViewData["AltoEditor"] = "200px"; %>
<%= Html.Telerik().Grid<GeDoc.sisUsuarios>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.usrId);
        })
         .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("sisUsuario", "Agregar") });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "sisUsuario")
                .Insert("_InsertEditing", "sisUsuario")
                .Update("_SaveEditing", "sisUsuario")
                .Delete("_DeleteEditing", "sisUsuario");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("sisUsuario", "Modificar") });
                commands.Delete().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("sisUsuario", "Eliminar") });
                commands.Custom("cmdPermisos")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("sisUsuario", "Examinar") });
                commands.Custom("cmdCentrosDeSalud")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -64px -190px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("sisUsuario", "Expedientes") });
                commands.Custom("cmdOficinas")
                    .Ajax(true)
                    .ButtonType(GridButtonType.Image)
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -32px -289px;" })
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("sisUsuario", "Expedientes") });
            }).Width("180px").Title("Acciones");
            columns.Bound(c => c.usrNombreDeUsuario).Width("170px").Title("Nombre de Usuario").Visible(true);
            columns.Bound(c => c.usrApellidoyNombre).Width("250px").Title("Apellido y Nombre").Visible(true);
            columns.Bound(c => c.estDescripcion).Width("150px").Title("Estilo").Visible(true);
            columns.Bound(c => c.usrEmail).Width("150px").Title("Correo Electrónico").Visible(true);
            columns.Bound(c => c.ZonaSanitaria.repNombre).Width("23%").Title("Zona Sanitaria").Visible(true);
            columns.Bound(c => c.InstitucionContable).Width("23%").Title("Institución Contable").Visible(true);
        })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(events => events.OnCommand("onCommand").OnEdit("onCommandEdit"))
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
            .Resizable(resizing => resizing.Columns(true))
            .RowAction(row => row.Selected = row.DataItem.usrId.Equals(ViewData["idUsuario"]))
%>

<!--Permisos de Usuario-->
<% string _btnAceptar = "";
   string _btnCancelar = "";
   _btnAceptar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -33px -335px;";
   _btnCancelar = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"].ToString() + "/sprite.png') no-repeat -49px -335px;";
%>
<% Html.Telerik().Window()
        .Name("Permisos")
        .Title("Permisos")
        .Visible(false)
        .Content(() => 
        {%>  
            <div style="border-style: solid; border-width: 1px; border-color: inherit; height: 98%; overflow: scroll;">
                <%= Html.Telerik().TreeView()
                                    .Name("tvPermisos")
                                    .ShowCheckBox(true)
                                    .ShowLines(true)
                                    .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingPermisos").OnChecked("onChequedPermiso"))
                %>
                <div style="position: absolute; top: 10px; left: 87%; width: 30px; height: 20px">
                    <table>
                        <tr>
                            <td style="border: none;">
                                <button class="t-button" onclick="onAceptar()">
                                    <img src="<%= Url.Content("~/Content") %>/General/Vacio-Transparente.png" alt="Aceptar" height="15" width="15" style="<%: _btnAceptar %>" />
                                </button>
                            </td>
                            <td style="border: none;">
                                <button class="t-button" onclick="onCancelar()">
                                    <img src="<%= Url.Content("~/Content") %>/General/Vacio-Transparente.png" alt="Cancelar" height="15" width="15" style="<%: _btnCancelar %>" />
                                </button>
                            </td>
                        </tr>
                    </table>
                </div>
                <label id="lblMensaje" style="font-family: Calibri; font-size: medium; font-weight: bold; color: #CC3300"></label>
            </div>
        <%})
        .Buttons(b => b.Maximize().Close())
        .Draggable(true)
        .Scrollable(false)
        .Resizable()
        .Width(870)
        .Height(460)
        .Render();
%>

<!-- Centros de Salud -->
<% Html.Telerik().Window()
        .Name("CRUDCentrosDeSalud")
        .Title("Centros de Salud")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.sisUsuariosCentroDeSalud>)ViewData["uCentrosDeSalud"])
            .Name("gridCentrosDeSalud")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.ucsId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblNombrePersona2" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingCentroDeSalud", "sisUsuario")
                    .Update("_SaveEditingCentroDeSalud", "sisUsuario")
                    .Insert("_InsertEditingCentroDeSalud", "sisUsuario")
                    .Delete("_DeleteEditingCentroDeSalud", "sisUsuario");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.ucsCentroDeSalud).Width("90%").Title("Centro de Salud").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingCentroDeSalud", "sisUsuario", new { idUsuario = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingCentrosDeSalud").OnEdit("onCommandEdit").OnSave("onSaveCentrosDeSalud").OnComplete("onCompleteCentrosDeSalud"))
            .Filterable()
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
       .Width(1024)
       .Height(400)
       .Render();
%>

<!-- Oficinas -->
<% Html.Telerik().Window()
        .Name("CRUDOficinas")
        .Title("Oficinas")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
            <% Html.Telerik().Grid((IEnumerable<GeDoc.enlUsuariosOficinas>)ViewData["uOficinas"])
            .Name("gridOficinas")
            .Localizable("es-AR")
            .DataKeys(keys =>
            {
                keys.Add(p => p.uoId);
            })
            .ToolBar(commands =>
            {
                commands.Template(cmdTemplate =>
                    {
                        %>
                        <%= cmdTemplate.InsertButton(GridButtonType.Image, new { style = "margin-left:0" })%>
                        <label id="lblNombrePersonaOficinas" style="font-size: 14px; font-weight: bold; text-align: center; margin-left: 260px; vertical-align: middle;"></label>
                        <%
                    });
            })
            .DataBinding(dataBinding =>
            {
                dataBinding.Ajax()
                    .Select("_SelectEditingOficinas", "sisUsuario")
                    .Update("_SaveEditingOficinas", "sisUsuario")
                    .Insert("_InsertEditingOficinas", "sisUsuario")
                    .Delete("_DeleteEditingOficinas", "sisUsuario");
            })
            .Columns(columns =>
            {
                columns.Command(commands =>
                {
                    commands.Edit().ButtonType(GridButtonType.Image);
                    commands.Delete().ButtonType(GridButtonType.Image);
                }).Width("10%").Title("Acciones");
                columns.Bound(c => c.Oficina.ofiNombre).Width("90%").Title("Oficina").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" });
            })
            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingOficinas", "sisUsuario", new { idUsuario = 1 }))
            .Editable(editing => editing
                                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
            .Pageable((paging) =>
                               paging.Enabled(true)
                                    .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingOficinas").OnEdit("onCommandEdit").OnSave("onSaveOficinas").OnComplete("onCompleteOficinas"))
            .Filterable()
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
       .Width(1024)
       .Height(400)
       .Render();
%>

<% Html.Telerik().ScriptRegistrar()
            .DefaultGroup(group => group
                .Add("MicrosoftAjax.js")
                .Add("MicrosoftMvcValidation.js")); %>


