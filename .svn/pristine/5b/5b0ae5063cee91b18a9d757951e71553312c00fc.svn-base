<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<%= Html.Telerik().Grid<GeDoc.Models.sisLogEnvioEmail>()
        .Name("Grid")
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "sisLogEnvioEmail");
        })
        .Localizable("es-AR")
        .Columns(columns =>
        {
            columns.Bound(c => c.leId).Width(20).Title("Número").Visible(true);
            columns.Bound(c => c.leFecha).Width(40).Title("Fecha").Visible(true);
            columns.Bound(c => c.leDestinatario).Width(60).Title("Destinatario").Visible(true);
            columns.Bound(c => c.leDestinatarioEmail).Width(60).Title("Correo Electrónico").Visible(true);
            columns.Bound(c => c.leEnviado).Width(20).Title("Estado").Visible(true)
                            .ClientTemplate("<img src='<#= leEnviado? '" + Url.Content("~/Content") + "/General/Notificacion/Vista Internet Security ON.png' : '" + Url.Content("~/Content") + "/General/Notificacion/Vista Internet Security OFF.png' #>' style='height: 20px;' />");
            columns.Bound(c => c.leMensajeError).Width(100).Title("Error").Visible(true);
        })
            .Pageable((paging) =>
                           paging.Enabled(true)
                           .PageSize(((int)Session["FilasPorPagina"])))
            .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Sortable()
%>

