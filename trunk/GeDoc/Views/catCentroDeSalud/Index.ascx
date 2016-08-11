<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script>
    var MapaUbicacionActual = { Latitud: 0, Longitud: 0, SetearUbicacionActual: true }

    function onCommand_catCentroDeSalud(e) {
        switch (e.name) {
        case "cmdEliminar":
            var grid = $(this).data("tGrid");
            var tr = $("#Grid tbody tr:eq(" + (e.row.rowIndex + 1).toString() + ")");
            jConfirm('¿Confirma Eliminar la Sala "?' + e.dataItem["cssNombre"] + '"', "Eliminar...", function (r) {
                if (r) {
                    AbrirWaiting();
                    grid.deleteRow(tr);
                }
            });

            e.preventDefault();
            e.stopPropagation();
            break;
        }
    }

    function onComplete_catCentroDeSalud(e) {
        if (e.name != "update" && e.name != "insert") {
            CerrarWaiting();
        }
    }

    function onCommandEdit_catCentroDeSalud(e) {
        debugger;
        if (e.mode === "edit") {
            MapaUbicacionActual.Latitud = parseFloat(e.dataItem.csLatitud);
            MapaUbicacionActual.Longitud = parseFloat(e.dataItem.csLongitud);
            MapaUbicacionActual.SetearUbicacionActual = false;
        } else {
            MapaUbicacionActual.Latitud = 0;
            MapaUbicacionActual.Longitud = 0;
            MapaUbicacionActual.SetearUbicacionActual = true;
        }
        onCommandEdit(e);
    }

    //navigator.geolocation.watchPosition(exitoRegistroPosicion, falloRegistroPosicion, {
    //    enableHighAccuracy: true,
    //    maximumAge: 1000,
    //    timeout: 700
    //});

    //function exitoRegistroPosicion(position) {
    //    debugger;
    //    $("#csLatitud").val(position);
    //}

    //function falloRegistroPosicion() {
    //    alert('No se pudo determinar la ubicación');
    //}

    function ObtieneUbicacionActualDelMapa(Latitud, Longitud) {
        $("#csLatitud").val(Latitud);
        $("#csLongitud").val(Longitud);
    }
    function onSave_catCentroDeSalud(e){
    }
</script>

<%= Html.Telerik().Grid<GeDoc.catCentroDeSaludWS>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.csId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {
            commands.Insert().ButtonType(GridButtonType.Image).ImageHtmlAttributes(new { style = "margin-left:0" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSalud", "Agregar"), title = "Agregar" });
        })
        .HtmlAttributes(new { style = "height:100%; width: 100%;" })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catCentroDeSalud")
                .Insert("_InsertEditing", "catCentroDeSalud")
                .Update("_SaveEditing", "catCentroDeSalud")
                .Delete("_DeleteEditing", "catCentroDeSalud");
        })
        .Columns(columns =>
        {
            columns.Command(commands =>
            {
                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSalud", "Modificar"), title = "Modificar" });
                commands.Custom("cmdEliminar").Ajax(true).ButtonType(GridButtonType.Image)
                    .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catCentroDeSalud", "Eliminar"), title = "Eliminar" })
                    .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -15px -336px;" });
            }).Width("80px").Title("Acciones");
            columns.Bound(c => c.csId).Width("50px").Title("Id").Visible(true);
            columns.Bound(c => c.csCodigo).Width("100px").Title("Código").Visible(true);
            columns.Bound(c => c.csNombre).Width("200px").Title("Nombre").Visible(true);
            columns.Bound(c => c.depNombre).Width("200px").Title("Departamento").Visible(true);
            columns.Bound(c => c.csLatitud).Width("200px").Title("Latitud").Visible(true);
            columns.Bound(c => c.csLongitud).Width("200px").Title("Longitud").Visible(true);
            columns.Bound(c => c.csDirector).Width("200px").Title("Director").Visible(true);
            columns.Bound(c => c.csTipologia).Width("200px").Title("Tipología").Visible(true);
            columns.Bound(c => c.csTelefono).Width("200px").Title("Teléfono").Visible(true);
            columns.Bound(c => c.csDomicilio).Width("200px").Title("Domicilio").Visible(true);
            columns.Bound(c => c.csPublicoAuxiliar).Width("200px").Title("Público").Visible(true);
            columns.Bound(c => c.ZonaSanitaria).Width("200px").Title("Zona Sanitaria").Visible(true);
            columns.Bound(c => c.CodBioestadistica).Width("200px").Title("Código Bioestadistica").Visible(true);
            columns.Bound(c => c.CodRemediar).Width("200px").Title("Código Remediar").Visible(true);
            columns.Bound(c => c.CantVivienda).Width("200px").Title("Cantidad Viviendas").Visible(true);
            columns.Bound(c => c.CantVaron).Width("200px").Title("Cantidad Varones").Visible(true);
            columns.Bound(c => c.CantMujeres).Width("200px").Title("Cantidad Mujeres").Visible(true);
            columns.Bound(c => c.Total).Width("200px").Title("Total").Visible(true);
            columns.Bound(c => c.Administrador).Width("200px").Title("Administrador").Visible(true);
            })
            .Editable(editing => editing
                    .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
            .Pageable((paging) =>
                       paging.Enabled(true)
                            .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit_catCentroDeSalud").OnCommand("onCommand_catCentroDeSalud").OnComplete("onComplete_catCentroDeSalud").OnSave("onSave_catCentroDeSalud"))
            .Footer(true)
        .Filterable()
        .Selectable()
        .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
        .Sortable()
%>
