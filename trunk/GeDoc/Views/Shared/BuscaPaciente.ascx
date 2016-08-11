<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<script type="text/javascript" src="/GeDoc/Scripts/CRUDGrillas.js"></script>
<% ViewData["FiltroContains"] = true; %>

<% Html.RenderPartial("Waiting"); %>
<% Html.RenderPartial("CRUDPaciente"); %>
<div style="overflow: hidden; height: 510px;" >
<% Html.Telerik().Window()
        .Name("wBuscaPaciente")
        .Visible(false)
        .Title("Detalle")
        .Modal(true)
        .Scrollable(false)
        .Height(540)
        .Content(() =>
            {
                %>
        <%: Html.Telerik().Grid<GeDoc.catPacientes>()
        .Name("Grid")
        .DataKeys(keys =>
        {
            keys.Add(p => p.pacId);
        })
        .Localizable("es-AR")
        .ToolBar(commands =>
        {%>
            <%=Html.Telerik().NumericTextBox()
                    .Name("Documento")
                    .ClientEvents(events => events.OnChange("onChangeDatePicker").OnLoad("onLoadDatePicker"))                   
                   // .Min(DateTime.Now)                                    
                    .HtmlAttributes(new { style = "width: 97px;" })                 
                 %>
                 <%=Html.Telerik().NumericTextBox()
                    .Name("HC")
                    .ClientEvents(events => events.OnChange("onChangeDatePicker").OnLoad("onLoadDatePicker"))                   
                   // .Min(DateTime.Now)                                    
                    .HtmlAttributes(new { style = "width: 97px;" })                 
                 %>
                 
          <%  commands.Custom().Ajax(true).Name("cmdAgregar").ButtonType(GridButtonType.ImageAndText)
                .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                .Text("Agregar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Agregar") });
            commands.Custom().Ajax(true).Name("cmdEditar").ButtonType(GridButtonType.ImageAndText).Text("Modificar")
                .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat 0px -336px;" })
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Modificar") });
            commands.Custom().Ajax(true).Name("cmdConsultar").ButtonType(GridButtonType.ImageAndText).Text("Consultar")
                .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("catPaciente", "Examinar") })
                .ImageHtmlAttributes(new { style = "background: url('/GeDoc/Content/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" });
        })
        .DataBinding(dataBinding =>
        {
            dataBinding.Ajax()
                .Select("_SelectEditing", "catPaciente")
                .Insert("_InsertEditing", "catPaciente")
                .Update("_SaveEditing", "catPaciente")
                .Delete("_DeleteEditing", "catPaciente");
        })
        .Localizable("es-AR")
        .Columns(columns =>
        {                            
            columns.Bound(c => c.pacApellido).Width("250px").Title("Apellido").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pacApellido #>' style='cursor: pointer;' id='txtpacApellido3' ><#= pacApellido #></label>");
            
            columns.Bound(c => c.pacNombre).Width("250px").Title("Nombre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pacNombre #>' style='cursor: pointer;' id='txtPacientepacNombre2' ><#= pacNombre #></label>");
                               
             columns.Bound(c => c.pacNumeroDocumento	).Width("250px").Title("Numero Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacNumeroDocumento #>' style='cursor: pointer;' id='txtpacNumeroDocumento2' ><#= pacNumeroDocumento #></label>");

             columns.Bound(c => c.tipoDescDocumento).Width("250px").Title("Tipo Documento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= tipoDescDocumento #>' style='cursor: pointer;' id='txttipoDescDocumento' ><#= tipoDescDocumento #></label>");
                 
             columns.Bound(c => c.pacCUIL).Width("250px").Title("C.U.I.L.").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })                 
             .ClientTemplate("<label title='<#= pacCUIL #>' style='cursor: pointer;' id='txtpacCUIL2' ><#= pacCUIL #></label>");

             columns.Bound(c => c.paisNombre).Width("250px").Title("Pais").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= paisNombre #>' style='cursor: pointer;' id='paisNombre' ><#= paisNombre #></label>");

             columns.Bound(c => c.tipoSexoNombre).Width("250px").Title("Sexo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= tipoSexoNombre #>' style='cursor: pointer;' id='tipoSexoNombre' ><#= tipoSexoNombre #></label>");  
            
              columns.Bound(c => c.pacFechaNacimiento).Width("250px").Title("Fecha de Nacimiento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })                 
              .ClientTemplate("<label title='<#= pacFechaNacimiento #>' style='cursor: pointer;' id='pacFechaNacimiento' ><#= pacFechaNacimiento #></label>");

              columns.Bound(c => c.DescEstadoCivil).Width("250px").Title("Estado Civil").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= DescEstadoCivil #>' style='cursor: pointer;' id='DescEstadoCivil' ><#= DescEstadoCivil #></label>");
                       
           
             columns.Bound(c => c.pacCalle).Width("250px").Title("Calle").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacCalle #>' style='cursor: pointer;' id='pacCalle' ><#= pacCalle #></label>");  
            
             columns.Bound(c => c.pacCalleNumero).Width("250px").Title("Calle Numero").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacCalleNumero #>' style='cursor: pointer;' id='pacCalleNumero' ><#= pacCalleNumero #></label>");  
            
             columns.Bound(c => c.pacDomicilioReferencias).Width("250px").Title("Domicilio Ref.").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacDomicilioReferencias #>' style='cursor: pointer;' id='pacDomicilioReferencias' ><#= pacDomicilioReferencias #></label>");

             columns.Bound(c => c.depNombre).Width("250px").Title("Departamento").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= depNombre #>' style='cursor: pointer;' id='depNombre' ><#= depNombre #></label>");  
            
             columns.Bound(c => c.depNombre).Width("250px").Title("Localidad").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= locNombre #>' style='cursor: pointer;' id='locNombre' ><#= locNombre #></label>");  
            
             columns.Bound(c => c.barId).Width("250px").Title("Barrio").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= barNombre #>' style='cursor: pointer;' id='barNombre' ><#= barNombre #></label>");  
            
             columns.Bound(c => c.pacTelefonoCasa).Width("250px").Title("Telefono Casa").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacTelefonoCasa #>' style='cursor: pointer;' id='pacTelefonoCasa' ><#= pacTelefonoCasa #></label>");  
            
             columns.Bound(c => c.pacTelefonoTrabajo).Width("250px").Title("Telefono Trabajo").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacTelefonoTrabajo #>' style='cursor: pointer;' id='pacTelefonoTrabajo' ><#= pacTelefonoTrabajo #></label>");  
            
             columns.Bound(c => c.pacTelefonoCelular).Width("250px").Title("Telefono Celular").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= pacTelefonoCelular #>' style='cursor: pointer;' id='pacTelefonoCelular' ><#= pacTelefonoCelular #></label>");      
                
           
             columns.Bound(c => c.osNombre).Width("250px").Title("Obra Social").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
             .ClientTemplate("<label title='<#= osNombre #>' style='cursor: pointer;' id='osNombre' ><#= osNombre #></label>");            
             
           
             columns.Bound(c => c.pacTransfusionesDeSangreUltima).Width("250px").Title("Ultima transfusiones de sangre").Visible(true).HtmlAttributes(new { style = "white-space: nowrap;" })
            .ClientTemplate("<label title='<#= pacTransfusionesDeSangreUltima #>' style='cursor: pointer;' id='pacTransfusionesDeSangreUltima' ><#= pacTransfusionesDeSangreUltima #></label>");  

               })
                .Editable(editing => editing
                        .Mode(GridEditMode.PopUp).DisplayDeleteConfirmation(false))
                .Pageable((paging) =>
                           paging.Enabled(true)
                                .PageSize(((int)Session["FilasPorPagina"])))
                .ClientEvents(events => events.OnEdit("onCommandEdit").OnRowSelected("onRowSelected").OnCommand("onCommand").OnComplete("onComplete").OnRowDataBound("onRowDataBound").OnSave("onSave"))
                .Footer(true)
            .Filterable()
            .Selectable()
            .Scrollable(scroll => scroll.Enabled(true).Height(((int)Session["AlturaGrilla"])))
            .Resizable(resizing => resizing.Columns(true))
            .Sortable()
            .HtmlAttributes(new { style = "width: 99.8%;" })
%>
               
 <%
            })
        .Render();
%>


