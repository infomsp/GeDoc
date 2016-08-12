<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    function onActivateConsultaPersona(e) {
        var estGrid = $('#gridConsultaDesignados').data('tGrid');
        estGrid.rebind();
    }

    function onDataBindingConsultaDesignaciones(e) {
        e.data = $.extend(e.data, { idPersona: idPersona });
    }
</script>

<% Html.Telerik().Window()
        .Name("wConsulta")
        .Visible(false)
        .Title("Detalle")
        .Modal(true)
        .Scrollable(false)
        .Height(640)
        .Draggable(true)
        .Content(() =>
            {
                %>
                <div style='width: 100%; position: absolute; right: 10px;' align="right">
                    <button class="t-button" style="vertical-align: middle; padding: 3px;" onclick='Imprimir(this);' >
                        <img src='<%= Url.Content("~/Content") %>/General/Printer.png' title='Imprimir' id="imgImprimir" height='18px' style='vertical-align:top;' />
                        <label style="vertical-align: middle; padding-right: 3px; cursor: pointer;">Imprimir</label>
                    </button>
                </div>
                <% Html.Telerik().TabStrip()
                        .Name("tabLegajo")
                        .HtmlAttributes(new { style = "height: 100%; padding: 0px; background: transparent; border: 0px; margin-left: -8px;" })
                        .Items(tabstrip =>
                        {
                            tabstrip.Add()
                                .Text("General")
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { %>
                                <table style="border: none; margin-right: 5px; margin-left: -2px;">
                                    <tr>
                                        <td style="border: none; width: 100%;">
                                            <div id="divDatos" class="BordeRedondeado" style="border-color: Silver; padding: 3px 3px 3px 3px; width: 100%;">
                                                <table style="border: none;">
                                                    <tr>
                                                        <td style="border: none;">
                                                                <div style="margin-top: -5px; margin-bottom: -5px;"><label id="lblApellidoyNombre" style="font-size: 22px; font-weight: bold;"></label></div>
                                                                <div class="div-consulta" >
                                                                    <label id="Label1" style="font-size: 12px; font-weight: normal">Padrón:</label>
                                                                    <label id="lblperPadron"  class="label-consulta" style="width: 75px"></label>
                                                                    <label id="Label4" style="font-size: 12px; font-weight: normal; margin-left: 90px;">Sexo:</label>
                                                                    <label id="lbltipoIdSexo" class="label-consulta" style="width: 70px;"></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label2" style="font-size: 12px; font-weight: normal">DNI:</label>
                                                                    <label id="lblperDNI" class="label-consulta" style="margin-left: 0px; width: 75px;" ></label>
                                                                    <label id="Label3" style="font-size: 12px; font-weight: normal; margin-left: 105px;">CUIL:</label>
                                                                    <label id="lblperCUIL" class="label-consulta" style="margin-left: 0px; width: 115px;" ></label>
                                                                    <label id="Label8" style="font-size: 12px; font-weight: normal; margin-left: 144px;">Es contratado:</label>
                                                                    <label id="lblperEsContratado" class="label-consulta" style="margin-left: 5px; width: 21px;" ></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label5" style="font-size: 12px; font-weight: normal">Fecha de Nacimiento:</label>
                                                                    <label id="lblperFechaNacimiento" class="label-consulta" style="margin-left: 0px; width: 120px;" ></label>
                                                                    <label id="Label6" style="font-size: 12px; font-weight: normal; margin-left: 239px;">Edad:</label>
                                                                    <label id="lblperEdad" class="label-consulta" style="margin-left: 4px; width: 20px;" ></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label17" style="font-size: 12px; font-weight: normal">Lugar de Nacimiento:</label>
                                                                    <label id="lblprovNombre" class="label-consulta" style="margin-left: 4px; width: 301px;" ></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label21" style="font-size: 12px; font-weight: normal">Nacionalidad:</label>
                                                                    <label id="lblpaisNombre" class="label-consulta" style="margin-left: 4px; width: 350px;" ></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label18" style="font-size: 12px; font-weight: normal; margin-left: 0px;">Estado Civil:</label>
                                                                    <label id="lblEstadoCivil" class="label-consulta" style="margin-left: 4px; width: 89px;" ></label>
                                                                    <label id="Label24" style="font-size: 12px; font-weight: normal; margin-left: 113px;">Fecha Casamiento:</label>
                                                                    <label id="lblperFechaCasado" class="label-consulta" style="margin-left: 4px; width: 119px;" ></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label7" style="font-size: 12px; font-weight: normal;">Teléfono:</label>
                                                                    <label id="lblperTelefono" class="label-consulta" style="margin-left: 0px; width: 140px;" ></label>
                                                                    <label id="Label14" style="font-size: 12px; font-weight: normal; margin-left: 183px;">Celular:</label>
                                                                    <label id="lblperCelular" class="label-consulta" style="margin-left: 4px; width: 140px;" ></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label16" style="font-size: 12px; font-weight: normal">Antigüedad:</label>
                                                                    <div class="BordeRedondeado" style="border-color: Silver; padding: 13px 13px 13px 9px; width: 669px; margin: 3px 0px 3px 0px;">
                                                                        <label id="Label12" style="font-size: 12px;">MSP:</label>
                                                                        <label id="lblperAntiguedad" class="label-consulta" style="margin-left: 4px; width: 255px;" ></label>
                                                                        <label id="Label13" style="font-size: 12px; margin-left: 335px;">Pago:</label>
                                                                        <label id="lblperAntiguedadPago" class="label-consulta" style="margin-left: 4px; width: 255px;" ></label>
                                                                        <div class="div-consulta" style="width: 669px;">
                                                                            <label id="Label15" style="font-size: 12px;">Vacaciones:</label>
                                                                            <label id="lblperAntiguedadVacaciones" class="label-consulta" style="margin-left: 4px; width: 255px;" ></label>
                                                                            <label id="Label26" style="font-size: 10px; margin-left: 274px; color: #96968B;">(Vacaciones cálculadas al 31/12/<%: (DateTime.Now.Year - 1).ToString()%>)</label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label10" style="font-size: 12px;">Sector:</label>
                                                                    <label id="lblsecNombre" class="label-consulta" style="margin-left: 4px; width: 635px;" ></label>
                                                                </div>
                                                                <div class="div-consulta">
                                                                    <label id="Label28" style="font-size: 12px;">Lugar de Trabajo:</label>
                                                                    <label id="lblOficina" class="label-consulta" style="margin-left: 4px; width: 567px;" ></label>
                                                                </div>
                                                                <div class="div-consulta" style="height: 30px;">
                                                                    <label id="Label27" style="font-size: 12px;">Cargo:</label>
                                                                    <label id="lblcarDescripcion" class="label-consulta" style="margin-left: 4px; width: 637px; height: 30px;" ></label>
                                                                </div>
                                                        </td>
                                                        <td style="border: none; width: 230px; height: 200px;" >
                                                            <div style="width: 100%; height: 100%;" >
                                                                <img id="imgFoto" class="BordeRedondeado" style="border-color: #999999; width: 230px; height: 200px;" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="margin-left: 5px;">
                                                    <div class="div-consulta" style="width: 100%; margin-top: 8px;">
                                                        <label id="Label19" style="font-size: 12px;">Profesión:</label>
                                                        <label id="lblprofNombre" class="label-consulta" style="margin-left: 0px; width: 280px;" ></label>
                                                        <label id="Label20" style="font-size: 12px; margin-left: 465px;">Sabe leer o escribir:</label>
                                                        <label id="lblperLeeoEscribe" class="label-consulta" style="margin-left: 4px; width: 21px;" ></label>
                                                    </div>
                                                    <div class="div-consulta">
                                                        <label id="Label25" style="font-size: 12px; font-weight: normal">Idiomas:</label>
                                                        <label id="lblperIdiomas" class="label-consulta" style="margin-left: 4px; width: 625px;" ></label>
                                                    </div>
                                                    <div class="div-consulta">
                                                        <label id="Label22" style="font-size: 12px; font-weight: normal">Domicilio:</label>
                                                        <label id="lblperDomicilio" class="label-consulta" style="margin-left: 4px; width: 621px;" ></label>
                                                    </div>
                                                    <div class="div-consulta" style="width: 100%; margin-top: 13px;">
                                                        <label id="Label9" style="font-size: 12px; font-weight: normal">Correo Electrónico:</label>
                                                        <label id="lblperEmail" class="label-consulta" style="margin-left: 4px; width: 360px;" ></label>
                                                        <label id="Label23" style="font-size: 12px; font-weight: normal; margin-left: 410px;">Permite recibir SMS:</label>
                                                        <label id="lblRecibeSMS" class="label-consulta" style="margin-left: 4px; width: 21px;" ></label>
                                                    </div>
                                                    <div class="div-consulta" style="margin-bottom: 13px;">
                                                        <label id="Label11" style="font-size: 12px; font-weight: normal">Observaciones:</label>
                                                        <label id="lblperObservaciones" class="label-consulta" style="margin-left: 4px; width: 582px;" ></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            <%});
                            tabstrip.Add()
                                .Text("Estado")
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { 
                                    %>
                                    <div>
                                        <% Html.Telerik().Grid(new List<GeDoc.catPersonasEstados>())
                                            .Name("gridLegajoEstados")
                                            .DataKeys(keys =>
                                            {
                                                keys.Add(p => p.pereId);
                                            })
                                            .Columns(columns =>
                                            {
                                                columns.Bound(c => c.pereFecha).Width("10%").Title("Fecha").Visible(true);
                                                columns.Bound(c => c.perEstado).Width("15%").Title("Estado").Visible(true)
                                                .ClientTemplate("<label title='<#= perEstado #>' style='cursor: pointer;' id='txtEstado' style='white-space: nowrap;'><#= perEstado #> </label>");
                                                columns.Bound(c => c.perMotivo).Width("15%").Title("Motivo").Visible(true)
                                                .ClientTemplate("<label title='<#= perMotivo #>' style='cursor: pointer;' id='txtMotivo' style='white-space: nowrap;'><#= perMotivo #> </label>");
                                                columns.Bound(c => c.pereObservaciones).Width("15%").Title("Observaciones").Visible(true)
                                                .ClientTemplate("<label title='<#= pereObservaciones #>' style='cursor: pointer;' id='txtObservaciones' style='white-space: nowrap;'><#= pereObservaciones #> </label>");
                                            })
                                            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingEstados", "catPersona", new { idPersona = 1 }))
                                            .Pageable((paging) =>
                                                               paging.Enabled(true)
                                                                    .PageSize(((int)Session["FilasPorPagina"])))
                                            .Footer(true)
                                            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingEstados"))
                                            .Filterable()
                                            .Selectable()
                                            .Scrollable(scroll => scroll.Enabled(true).Height(470))
                                            .Sortable()
                                            .Render();
                                                %>
                                    </div>
                                    <%
                                });
                            tabstrip.Add()
                                .Text("Asistencia")
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { 
                                    %>
                                    <div>
                                        <% Html.Telerik().Grid(new List<GeDoc.Asistencia>())
                                            .Name("gridAsistencia")
                                            .DataKeys(keys =>
                                            {
                                                keys.Add(p => p.Fecha);
                                            })
                                            .Columns(columns =>
                                            {
                                                columns.Bound(c => c.Fecha).Width("10%").Title("Fecha").Visible(true);
                                                columns.Bound(c => c.Estado).Width("80px").Title("Estado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                                .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/<#= AsistenciaImagen #>' title='<#= Estado #>' height='22px' width='22px' style='vertical-align:middle; white-space: nowrap;' /></div>");
                                                columns.Bound(c => c.Estado).Width("15%").Title("Descripcion del Estado").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
                                                .ClientTemplate("<label title='<#= Estado #>' style='cursor: pointer;' style='white-space: nowrap;'><#= Estado #> </label>");
                                                columns.Bound(c => c.Entrada).Width("10%").Title("Entrada").Format("{0:HH:mm:ss}").Visible(true);
                                                columns.Bound(c => c.Salida).Width("15%").Title("Salida").Format("{0:HH:mm:ss}").Visible(true);
                                            })
                                            .DataBinding(dataBinding => dataBinding.Ajax().Select("_BindingAsistencia", "catPersona", new { idPersona = 0 }))
                                            .Pageable((paging) =>
                                                               paging.Enabled(true)
                                                                    .PageSize(((int)Session["FilasPorPagina"])))
                                            .Footer(true)
                                            .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingAsistencia"))
                                            .Filterable()
                                            .Selectable()
                                            .Scrollable(scroll => scroll.Enabled(true).Height(470))
                                            .Sortable()
                                            .Render();
                                                %>
                                    </div>
                                    <%
                                });
                            tabstrip.Add()
                                .Text("Historial de Cargos")
                                .ContentHtmlAttributes(new { style = "height: 90%; padding: 8px;" })
                                .Content(() =>
                                { 
                                    %>
                                    <div>
                                        <% Html.Telerik().Grid<GeDoc.catCargosCategoriasDesignados>()
                                        .Name("gridConsultaDesignados")
                                        .DataKeys(keys =>
                                        {
                                            keys.Add(p => p.desigId);
                                        })
                                        .DataBinding(dataBinding =>
                                        {
                                            dataBinding.Ajax()
                                                .Select("_ConsultaDesignaciones", "catPersona", new { idPersona = 1 });
                                        })
                                        .Columns(columns =>
                                        {
                                            columns.Bound(c => c.desigVigenciaHasta).Width("20px").Title("").Visible(true)
                                                .ClientTemplate("<div style='width: 100%; text-align: center;'><img src='" + Url.Content("~/Content") + "/Estados/Rojo.png' title='Designación de Baja' height='10px' width='10px' style='vertical-align:middle; visibility: <#= desigVigenciaHasta != null ? \"visible\" : \"hidden\" #>' /></div>");
                                            columns.Bound(c => c.carDescripcion).Width("200px").Title("Cargo").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                                                .ClientTemplate("<label title='<#= carDescripcion #>' style='cursor: pointer;' style='white-space: nowrap;'><#= carDescripcion #> </label>");
                                            columns.Bound(c => c.desigTipo).Width("90px").Title("Tipo").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                                            columns.Bound(c => c.desigVigenciaDesde).Width("90px").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                                            columns.Bound(c => c.desigVigenciaHasta).Width("90px").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                                            columns.Bound(c => c.perSubRoganciaNombre).Width("200px").Title("Subrogado por").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                                                .ClientTemplate("<label title='<#= perSubRoganciaNombre #>' style='cursor: pointer;' id='txtperSubRoganciaNombre' style='white-space: nowrap;'><#= perSubRoganciaNombre #> </label>");
                                            columns.Bound(c => c.desigSubRoganciaDesde).Width("90px").Title("Alta").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                                            columns.Bound(c => c.desigSubRoganciaHasta).Width("90px").Title("Baja").Visible(true).HtmlAttributes(new { style = "text-align: center;" });
                                            columns.Bound(c => c.desigObservaciones).Width("250px").Title("Observaciones").Visible(true).HtmlAttributes(new { style = "text-align: left; white-space: nowrap;" })
                                            .ClientTemplate("<label title='<#= desigObservaciones #>' style='cursor: pointer;' id='txtdesigObservaciones' style='white-space: nowrap;'><#= desigObservaciones #> </label>");
                                        })
                                        .Editable(editing => editing
                                                                    .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(true))
                                        .Pageable((paging) =>
                                                           paging.Enabled(true)
                                                                .PageSize(((int)Session["FilasPorPagina"])))
                                        .Footer(true)
                                        .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingConsultaDesignaciones"))
                                        .Filterable()
                                        .Selectable()
                                        .Resizable(resizing => resizing.Columns(true))
                                        .Scrollable(scroll => scroll.Enabled(true).Height("500px"))
                                        .Sortable().Render();
                                            %>
                                    </div>
                                    <%
                                });
                        })
                .SelectedIndex(0)
                .Render();
            })
            .ClientEvents(e => e.OnActivate("onActivateConsultaPersona"))
        .Render();
%>
