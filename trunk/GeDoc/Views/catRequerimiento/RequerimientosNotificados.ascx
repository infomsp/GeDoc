<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="GeDoc" %>
<%
    ViewData["UsuariosNotificados"] = new List<catRequerimientoNotificaWS>();
%>
<script type="text/javascript">
    var _WindowCRUD;
    function onDataBindingNotificados(e) {
        var reqId = -1;
        if (_DatosRegistro_catRequerimiento != null) {
            reqId = _DatosRegistro_catRequerimiento.reqId;
        }

        e.data = $.extend(e.data, { reqId: reqId });
    }

    function onCheckedNotificado(TieneEMail, idNotificado, e) {
        debugger;
        var _Post = GetPathApp('catRequerimiento/Notificar');
        var Notifica = $(e).is(":checked");
        if (TieneEMail == 0 && Notifica) {
            jAlert('No es posible Notificar un usuario sin Correo Electrónico.', '¡Atención!', function (res) {
                $(e).prop("checked", false);
            });
            return;
        }
        AbrirWaiting();
        $.ajax({
            url: _Post,
            data: { requId: idNotificado, Notifica: (Notifica ? 1 : 0) },
            type: 'POST',
            error: function (xhr, ajaxOptions, thrownError) {
                CerrarWaiting();
                jAlert('No se pudo Marcar el usuario para notificar.', '¡Atención!');
            },
            success: function (data) {
                CerrarWaiting();
                if (data) {
                } else {
                    jAlert('Falló la asignación de estado del Requerimiento', '¡Atención!');
                }
            }
        });
    }
</script>

<% Html.Telerik().Window()
        .Name("wNotificados")
        .Title("Notificados")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div>
                <% Html.Telerik().Grid((IEnumerable<catRequerimientoNotificaWS>)ViewData["UsuariosNotificados"])
                .Name("gridNotifica")
                .DataKeys(keys =>
                {
                    keys.Add(p => p.requId);
                })
                .Localizable("es-AR")
                .DataBinding(dataBinding =>
                {
                    dataBinding.Ajax()
                        .Select("_BindingNotificados", "catRequerimiento", new { reqId = 0 });
                })
                .Columns(columns =>
                    {
                        columns.Bound(c => c.requNotificar).Width("70px").Title("Notificar").Visible(true)
                        .ClientTemplate("<div style='width: 100%; text-align: center;'><input type='checkbox' <#= requId == 0 ? disabled='disabled' : '' #> onclick='onCheckedNotificado(<#= Usuario.usrEmail == null ? 0 : 1 #>, <#= requId #>, this);' <#= requNotificar ? checked = 'checked' : '' #> /></div>");
                        columns.Bound(c => c.Usuario.usrApellidoyNombre).Width("300px").Title("Usuario").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<label title='<#= Usuario.usrApellidoyNombre #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Usuario.usrApellidoyNombre #> </label>");
                        columns.Bound(c => c.Usuario.usrEmail).Width("200px").Title("Correo Electrónico").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                        .ClientTemplate("<label title='<#= Usuario.usrEmail #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Usuario.usrEmail #> </label>");
                })
                .Pageable((paging) =>
                                    paging.Enabled(true)
                                        .PageSize(((int)Session["FilasPorPagina"])))
                .Footer(true)
                .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingNotificados"))
                .Filterable()
                .Selectable()
                .Resizable(resizing => resizing.Columns(true))
                .Scrollable(scroll => scroll.Enabled(true).Height(330))
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
