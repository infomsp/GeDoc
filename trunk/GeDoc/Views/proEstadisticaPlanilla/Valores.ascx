<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<% Html.RenderPartial("DetalleProfesionales"); %>
<style>
#pop {
   z-index:500000;
   position:absolute;
   border: 1px solid #333333;
   text-align:center;
   background:#cccccc;
}
#cerrar {
   float:right;
   margin-right:5px;
  
   /*font:Verdana, Arial, Helvetica, sans-serif;*/
   font-size:12px;
   font-weight:bold;
   color:#FFFFFF;
   background-color:#666666;
   width:12px;
   position:relative;
   margin-top:-1px;
   text-align:center;
}
    </style>
<style type="text/css" xml:lang="es-AR">
    .tituloColumna {
        color: black;
    }
</style>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/CRUDGrillas.js") %>"></script>
<script>
    function mostrar() {
        debugger;
        $("#pop").fadeIn('slow');
    };//checkHover
    $(document).ready(function () {
        debugger;
        //Conseguir valores de la img
        var img_w = 250;
        var img_h = 100;

        //Darle el alto y ancho
        $("#pop").css('width', img_w + 'px');
        $("#pop").css('height', img_h + 'px');

        //Esconder el popup
        $("#pop").hide();
        //Consigue valores de la ventana del navegador
        var w = $(this).width();
        var h = $(this).height();

        //Centra el popup   
        w = (w / 2) - (img_w / 2);
        h = (h / 2) - (img_h / 2);
        $("#pop").css("left", w + "px");
        $("#pop").css("top", h + "px");
    });

    //Función para cerrar el popup
    $("#pop").click(function (){
        $(this).fadeOut('slow');
    });

</script>
<script>
    var _vespId;
    var _vmes;
    var _vaño;
    var _vdia;
    var _valoresId;
    var _resId;
    function onRowSelected_Valores(e) {
        debugger;
        var row = e.row;
        var grid = $("#Gvalores").data("tGrid");
        var dataItem = grid.dataItem(row);
       // onRowSelected_ProEstadisticaPlanilla(e);
        //  _DatosRegistroPlanilla = dataItem;
        _vdia = dataItem.resDia;
        _resId = dataItem.resId;
        _vmes = _mes;
        _vaño = _año;
        _vespId = _espId;

    }

    function onDataBindingValores(args) {
        debugger;
        var _AplaId = 0;
        if (_plaId > 0) {
            _AplaId = _plaId;
        }
        args.data = $.extend(args.data, {
            plaId: _plaId,espId :_espId
        });
    }
    function RefrescarValores() {
        debugger;
        var grid = $("#Gvalores").data("tGrid");
        grid.ajaxRequest();
    }
    function RefrescarPlanillaProfesionales() {
        debugger;
        var grid = $("#GDetalleProfesionales").data("tGrid");
        grid.ajaxRequest();
    }

    function OnEditValores(e) {
        debugger;
        if (e.name === "databinding") {
            $("#Gvalores").data("tGrid").rebind();

        }
    }
    function PopupCenter(pageURL, title, w, h) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no,     status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    }
    function OnCloseValores(e) {
        $("#btn1").hide();
    }
    function OnOpenValores(e) {
        debugger;
        if (_plaId > 0) {
            $.post("<%=Url.Content("~/proEstadisticaPlanilla/Especialidades_Hijas")%>", { plaId: _plaId }, function (data) {
                CerrarWaiting();
                if (data != null) {
                    debugger;
                    $("#lbltxt").text(data);

                }
            });
        }
        $(e.target).data("tWindow").title(_centroSaludTexto + " - " + _departamentoTexto + " - " +getMes(_mes) + " / " + _año + " - Planilla de Valores para " + _espNombre );
        $(e.target).find(".t-window-title").after($("<div>", { id: "btn1" }).append($("<input>", { type: "button", value: "Especialidades Hijas", style: "background: orange", onClick: "mostrar()" })));

    }
   
    function ActualizaTotalValores() {
        debugger;
        var resTotEdadesM=0;
        var resTotEdadesF = 0;
        var resMenos1DM = $("#resMenos1DM").val() == "" ? 0 : parseInt($("#resMenos1DM").val());
        $("#resMenos1DM").val(resMenos1DM);
        var resMenos1MM = $("#resMenos1MM").val() == "" ? 0 : parseInt($("#resMenos1MM").val());
        $("#resMenos1MM").val(resMenos1MM);
        var resde1a4M = $("#resde1a4M").val() == "" ? 0 : parseInt($("#resde1a4M").val());
        $("#resde1a4M").val(resde1a4M);
        var resde5a14M = $("#resde5a14M").val() == "" ? 0 : parseInt($("#resde5a14M").val());
        $("#resde5a14M").val(resde5a14M);
        var resde15a19M = $("#resde15a19M").val() == "" ? 0 : parseInt($("#resde15a19M").val());
        $("#resde15a19M").val(resde15a19M);
        var resde20a39M = $("#resde20a39M").val() == "" ? 0 : parseInt($("#resde20a39M").val());
        $("#resde20a39M").val(resde20a39M);
        var resde40a69M = $("#resde40a69M").val() == "" ? 0 : parseInt($("#resde40a69M").val());
        $("#resde40a69M").val(resde40a69M);
        var resde70ymasM = $("#resde70ymasM").val() == "" ? 0 : parseInt($("#resde70ymasM").val());
        $("#resde70ymasM").val(resde70ymasM);
        resTotEdadesM = resMenos1DM + resMenos1MM + resde1a4M + resde5a14M + resde15a19M + resde20a39M + resde40a69M + resde70ymasM;

        var resMenos1DF = $("#resMenos1DF").val() == "" ? 0 : parseInt($("#resMenos1DF").val());
        $("#resMenos1DF").val(resMenos1DF);
        var resMenos1MF = $("#resMenos1MF").val() == "" ? 0 : parseInt($("#resMenos1MF").val());
        $("#resMenos1MF").val(resMenos1MF);
        var resde1a4F = $("#resde1a4F").val() == "" ? 0 : parseInt($("#resde1a4F").val());
        $("#resde1a4F").val(resde1a4F);
        var resde5a14F = $("#resde5a14F").val() == "" ? 0 : parseInt($("#resde5a14F").val());
        $("#resde5a14F").val(resde5a14F);
        var resde15a19F = $("#resde15a19F").val() == "" ? 0 : parseInt($("#resde15a19F").val());
        $("#resde15a19F").val(resde15a19F);
        var resde20a39F = $("#resde20a39F").val() == "" ? 0 : parseInt($("#resde20a39F").val());
        $("#resde20a39F").val(resde20a39F);
        var resde40a69F = $("#resde40a69F").val() == "" ? 0 : parseInt($("#resde40a69F").val());
        $("#resde40a69F").val(resde40a69F);
        var resde70ymasF = $("#resde70ymasF").val() == "" ? 0 : parseInt($("#resde70ymasF").val());
        $("#resde70ymasF").val(resde70ymasF);

        resTotEdadesF = resMenos1DF + resMenos1MF + resde1a4F + resde5a14F + resde15a19F + resde20a39F + resde40a69F + resde70ymasF;
       

        $("#resTotEdadesM").val(resTotEdadesM);
        $("#resTotEdadesF").val(resTotEdadesF);
        $("#resTotTotal").val(resTotEdadesM + resTotEdadesF);
    }

    function onCommandValores(e) {
        debugger;
        switch (e.name) {
        case "cmdAgregarValores":
            if (_plaId > 0) {
                $.post("<%=Url.Content("~/proEstadisticaPlanilla/generaValores")%>", { plaId: _plaId, espId: _espId }, function(data) {
                    CerrarWaiting();
                    if (data != null) {

                        RefrescarValores();

                    }
                });
            }
            break;
        case "cmdValidarValores":

            break;
        case "update":
            ActualizaTotalValores();
            break;

        case
            "cmdVerDetalles":
            verDetalles(e);
            break;
        }
    }
    function verDetalles(e) {
        debugger;
        var _WDetalleProf = $('#WDetalleProfesionales').data("tWindow");
        onRowSelected_Valores(e);
        if (_plaId > 0) {
            _WDetalleProf.center().open();
            debugger;
            RefrescarPlanillaProfesionales();
              
        }

    }
    function onCommandEdit(e) {
        $("#resDia").attr("disabled", true);
       // $("#resNroHojas").attr("disabled", true);
       // $("#resHorasAtencion").attr("disabled", true);
      //  $("#resCMNuevas").attr("disabled", true);
      //  $("#resCMRepetidas").attr("disabled", true);
        $("#resTotEdadesM").attr("disabled", true);
        $("#resTotEdadesF").attr("disabled", true);
        $("#resTotTotal").attr("disabled", true);
       // $("#resTotControlEmbarazo").attr("disabled", .0true);
    }
    $(function() {
        $("#dialog").dialog();
    });
</script>

<div id="pop">
   <div id="cerrar">X</div>
     <div id="esp">Especialidades Hijas</div>
  <table>
<tbody><tr>
    <th class="t-header" scope="col" width="200px">
        <span class="t-link"><label id="lbltxt"></label></span>
</th>

</tr>
</tbody>
    </table>

</div>

<!-- Valores -->
<% Html.Telerik().Window()
       .Name("Wvalores")
       .Title("Valores")
         .Modal(true)
         .ClientEvents(e => e.OnOpen("OnOpenValores").OnClose("OnCloseValores"))
       .Visible(false)
       .Content(() =>
       {
%>

               <%  
             
           Html.Telerik().Grid<GeDoc.proEstadisticaResultadosRHS>()
                        .Name("Gvalores")
                        .DataKeys(keys =>
                        {
                            keys.Add(p => p.resId);
                        })
                        .Localizable("es-AR")
                          
                        .ToolBar(commands =>
                        {
                          //  commands.Custom().Ajax(true).Name("cmdAgregarValores").ButtonType(GridButtonType.ImageAndText)
                            //    .ImageHtmlAttributes(new {style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;"})
                              //  .Text("Generar Valores");
                            //commands.Custom().Ajax(true).Name("cmdValidarValores").ButtonType(GridButtonType.ImageAndText)
                            //   .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                            //   .Text("Validar");
                            //commands.Custom().Ajax(true).Name("cmdExportarValores").ButtonType(GridButtonType.ImageAndText)
                            // .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -48px -319px;" })
                            // .Text("Exportar");
                         
                        })
                        
                        .DataBinding(dataBinding =>
                        {
                            dataBinding.Ajax()
                           .Select("_SelectEditingReslutados", "proEstadisticaPlanilla", new {plaId = 0, espId = 0})
                           .Insert("_InsertEditingResultados", "proEstadisticaPlanilla")
                           .Update("_SaveEditingResultados", "proEstadisticaPlanilla");

                        })
                   
                        
                        .Columns(columns =>
                        {
                         
                            columns.Command(commands =>
                            {
                                commands.Edit().ButtonType(GridButtonType.Image).HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proEstadisticaPlanilla", "Modificar") });
                                commands.Custom("cmdVerDetalles")
                   .Ajax(true)
                   .ButtonType(GridButtonType.Image)
                   .ImageHtmlAttributes(new { style = "background: url('" + Url.Content("~/Content") + "/" + Session["Version"] + "/" + Session["Estilo"] + "/sprite.png') no-repeat -63px -176px;" })
                   .HtmlAttributes(new { style = "display: " + (Session["Permisos"] as GeDoc.Acciones).Visibilidad("proEstadisticaPlanilla", "Modificar") });
                                
                            }).Width(11).Title("");
                            
                        
                           
                            columns.Bound(c => c.resDia).Width(5).Title("").Visible(true);

                            columns.Bound(c => c.resNroHojas).Width(5).Title("").Visible(true);

                            columns.Bound(c => c.resHorasAtencion).Width(6).Title("Horas").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                                .ClientFooterTemplate("<#=  Sum #>");

                            columns.Bound(c => c.resCMNuevas).Width(3).Title("N").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resCMRepetidas).Width(4).Title("R").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>");
                            %>
<table>
<tbody><tr>
    <th class="t-header" scope="col" width="75px">
        <span>Accion.</span>
</th><th class="t-header" scope="col" width="32px">
    <a class="t-link">Dia</a>
</th>
    <th class="t-header" scope="col" width="30px">
        <a>Hoj.</a>
    </th>
   <th class="t-header" scope="col" width="35px">
    <a>Hs. de At.</a>
</th><th class="t-header" scope="col" width="45px">
    <a>Con. Med.</a>
</th>
    
    <th class="t-header" scope="col" width="67px">
        <a>menos de1 dias</a>
    </th>
    <th class="t-header" scope="col" width="69px">
        <a>menos de1 mes</a>
    </th><th class="t-header" scope="col" width="70px">
    <a>1 a 4</a>
</th><th class="t-header" scope="col" width="67px">
    <a>5 a 14 </a>
</th><th class="t-header" scope="col" width="67px">
    <a>15 a 19 </a>
</th><th class="t-header" scope="col" width="67px">
    <a>20 a 39 </a>
</th><th class="t-header" scope="col" width="67px">
    <a>40 a 69 </a>
</th><th class="t-header" scope="col" width="67px">
    <a>70 y mas </a>
</th><th class="t-header" scope="col" width="67px">
    <a>Total Edades</a>
</th><th class="t-header" scope="col" width="54px">
    <a>Total</a>
</th><th class="t-header" scope="col" width="99px">
    <a>Total Control Embarazo</a>
</th><th class="t-header" scope="col" width="99px">
    <a>Cant. Prof.</a>
</th><th class="t-header" scope="col" width="95px">
    <a>Cant. Prof. C.E.</a>
</th>
</tr>
</tbody>
    </table>
                                <%
                            columns.Bound(c => c.resMenos1DM).Width(5).Title("M").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                                .ClientFooterTemplate("<#=  Sum #>");
                           
                            columns.Bound(c => c.resMenos1DF).Width(5).Title("F").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                             
                            columns.Bound(c => c.resMenos1MM).Width(5).Title("M").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resMenos1MF).Width(5).Title("F").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde1a4M).Width(5).Title("M").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde1a4F).Width(5).Title("F").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde5a14M).Width(5).Title("M").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde5a14F).Width(5).Title("F").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde15a19M).Width(5).Title("M").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde15a19F).Width(5).Title(" F").Visible(true)
                                .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde20a39M).Width(5).Title(" M").Visible(true)
                             .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde20a39F).Width(5).Title(" F").Visible(true)
                            .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); ;
                            columns.Bound(c => c.resde40a69M).Width(5).Title(" M").Visible(true)
                            .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde40a69F).Width(5).Title(" F").Visible(true)
                            .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde70ymasM).Width(5).Title("M").Visible(true)
                            .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resde70ymasF).Width(5).Title("F").Visible(true)
                            .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>");
                            columns.Bound(c => c.resTotEdadesM).Width(5).Title("M").Visible(true)
                                 .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>"); 
                            columns.Bound(c => c.resTotEdadesF).Width(5).Title("F").Visible(true)
                            .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("<#=  Sum #>");
                            columns.Bound(c => c.resTotTotal).Width(8).Title("Total").Visible(true)
                            .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("Total :<#=  Sum #>");
                            columns.Bound(c => c.resTotControlEmbarazo).Width(14).Title("Total Control Embarazo").Visible(true)
                           .Aggregate(aggreages => aggreages.Sum()) 
                           .ClientFooterTemplate("Tot. Cont. Emb. :<#=  Sum #>");
                           columns.Bound(c => c.resCantProf).Width(14).Title("Cant Prof.").Visible(true)
                           .Aggregate(aggreages => aggreages.Sum())
                           .ClientFooterTemplate("Cant. Prof :<#=  Sum #>");
                            columns.Bound(c => c.resCantProfCE).Width(14).Title("Cant Prof. CE").Visible(true)
                           .Aggregate(aggreages => aggreages.Sum())
                            .ClientFooterTemplate("Cant. Prof :<#=  Sum #>");
                        })
                        .Editable(editing => editing
                            .Mode(GridEditMode.InLine)
                            .BeginEdit(GridBeginEditEvent.Click)
                            .DisplayDeleteConfirmation(true))
                           
                        .Pageable((paging) =>
                            paging.Enabled(false)
                                .PageSize(60))
                        .ClientEvents(events => events.OnEdit("onCommandEdit").OnDataBinding("onDataBindingValores").OnCommand("onCommandValores"))
                        
                        .Footer(true)
                        .Resizable(resizing => resizing.Columns(false))
                        .Selectable()
                        .Scrollable(scroll => scroll.Enabled(true).Height(((int) Session["AlturaGrilla"]+80)))
                        .Sortable()
                        .Render();
                %>
</div>
        <% })
       .Buttons(b => b.Maximize().Close())
       .Scrollable(true)
       .Draggable(true)
      // .Resizable()
       .Height(670)
      .Render();

        %>