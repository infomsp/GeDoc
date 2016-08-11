<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Protur</title>

    <link href="<%= Url.Content("~/Scripts/jqgrid/jquery-ui.css") %>" rel="Stylesheet" />
    <link href="<%= Url.Content("~/Scripts/jqgrid/ui.jqgrid.css") %>" rel="Stylesheet" />
    <link href="<%= Url.Content("~/Content/jquery.alerts.css") %>" rel="Stylesheet" />


    <script type="text/javascript" src="<%= Url.Content("~/Scripts/jqgrid/jquery.min.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/jqgrid/jquery.jqGrid.min.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/jqgrid/jquery-ui.min.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/jqgrid/grid.locale-es.js") %>"></script>
    
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/fusioncharts.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/fusioncharts.charts.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/Chart.bundle.min.js") %>"></script>
    <script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.alerts.js") %>"></script>

    
<style type="text/css">

    @media print {

        .tg  {border-collapse:collapse;border-spacing:0; width:99%; font-size:14px}
.tg td{font-family:Arial, sans-serif;font-size:14px;padding:0px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;}
.tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:0px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;}
.tg .tg-baqh{text-align:center;vertical-align:top;font-size:14px;}
.tg .tg-yw4l{vertical-align:top}

    * {
        font-family: Arial;
        font-size:9px;
    }

    th.ui-th-column div {
    word-wrap: break-word; /* IE 5.5+ and CSS3 */
    white-space: pre-wrap; /* CSS3 */
    white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
    white-space: -pre-wrap; /* Opera 4-6 */
    white-space: -o-pre-wrap; /* Opera 7 */
    overflow: hidden;
    height: auto !important;
    vertical-align: middle;
}

    .raphael-group-421-creditgroup,
    .raphael-group-306-creditgroup,
    .raphael-group-110-creditgroup,
    .raphael-group-221-creditgroup {
    display:none;
    }

    .ui-jqgrid tr.jqgrow td {
        height:15px;
        padding: 0;
    }

    }
.tg  {border-collapse:collapse;border-spacing:0; width:99%}
.tg td{font-family:Arial, sans-serif;font-size:14px;padding:0px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;}
.tg th{font-family:Arial, sans-serif;font-size:14px;font-weight:normal;padding:0px 5px;border-style:solid;border-width:1px;overflow:hidden;word-break:normal;}
.tg .tg-baqh{text-align:center;vertical-align:top}
.tg .tg-yw4l{vertical-align:top}

    * {
        font-family: Arial;
        font-size:9px;
    }

    th.ui-th-column div {
    word-wrap: break-word; /* IE 5.5+ and CSS3 */
    white-space: pre-wrap; /* CSS3 */
    white-space: -moz-pre-wrap; /* Mozilla, since 1999 */
    white-space: -pre-wrap; /* Opera 4-6 */
    white-space: -o-pre-wrap; /* Opera 7 */
    overflow: hidden;
    height: auto !important;
    vertical-align: middle;
}

    .raphael-group-421-creditgroup,
    .raphael-group-306-creditgroup,
    .raphael-group-110-creditgroup,
    .raphael-group-221-creditgroup {
    display:none;
    }

    .ui-jqgrid tr.jqgrow td {
        height:15px;
        padding: 0;
    }
    .modal {
        display:    none;
        position:   fixed;
        z-index:    1000;
        top:        0;
        left:       0;
        height:     100%;
        width:      100%;
        background: rgba( 0, 0, 0, .9 ) 
                    url('http://i.stack.imgur.com/FhHRx.gif') 
                    50% 50% 
                    no-repeat;
    }

    /* When the body has the loading class, we turn
       the scrollbar off with overflow:hidden */
    body.loading {
        overflow: hidden;   
    }

    /* Anytime the body has the loading class, our
       modal element will be visible */
    body.loading .modal {
        display: block;
    }    
</style>
        <script type="text/javascript">

            function cerrarPopUp() {
                jAlert("Inicie sesion en GeDoc para ver este reporte");
                this.close();
            }

            var _TotalRecords = 0;
            var _Datos;
            var _TurnosSolicitados = 0;
            var _TotalPacientes = 0;
            var _SinDerivacion=0;
            var _ConDerivacion=0;
            var _PorInterconsulta=0;
            var _Atendidos=0;
            var _Programados=0;
            var _SinResolver=0;
            var _EstaInstitucion = 0;
            var _OtraInstitucion = 0;
            var _Menor = 0;
            var _Mayor = 0;
            var _Promedio = 0;
            var _RowID = 0;
            var _NombreUsuario = "";
            var _MinFecha = 0;
            var _MaxFecha = 0;
            var _cantCuando = 0;
            
            if (window.opener.document.getElementById('RowID')==null) {
                cerrarPopUp();
            }
            var RowID = window.opener.document.getElementById('RowID').value;
            var fechaSelected = window.opener.document.getElementById('fechaSelected').value;
            var csNombre = window.opener.document.getElementById('csNombre').value;
            var Encuestador = window.opener.document.getElementById('Encuestador').value;

            $(window).bind('resize', function () {
                $("#rpt").setGridWidth($(window).width()-20);
            }).trigger('resize');

            jQuery(document).ready(function () {
                jQuery("#rpt").jqGrid({
                    postData: { plaId: RowID },
                    datatype: 'json',
                    mtype: 'POST',
                    async:false,
                    url: '<%=Url.Content("~/catEncuestaSinAdmisionPlanilla/jqGrid")%>',
                    colNames: [
                         "pacId",
                         "DNI",
                         "APELLIDO",
                         "NOMBRE",
                         "SEXO",
                         "EDAD",
                         "DEPARTAMENTO",
                         "LOCALIDAD",
                         "TELEFONO", 
                         "CENTRO DE SALUD",
                         "CAUSA",
                         "ESPECIALIDAD",
                         "DERIVACION",
                         "INTERCONSULTA",
                         "ATENDIDO LOCAL",
                         "PROGRAMADO",
                         "DONDE",
                         "CUANDO",
                         "RESUELTO", 
                    ],
                    colModel: [
                        { name: 'pacId', index: 'pacId', width: 1, resizable: true, align: "left", editable: false, hidden: true, sortable: false },
                        { name: 'DNI', index: 'DNI', width: 47, resizable: true, align: "left", editable: false, sortable: false },
                        { name: 'APELLIDO', index: 'APELLIDO', width: 80, resizable: true, align: "left", edittype: 'text', editable: true, sortable: false },
                        { name: 'NOMBRE', index: 'NOMBRE', width: 80, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'SEXO', index: 'SEXO', width: 50, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'EDAD', index: 'EDAD', width: 35, resizable: true, align: "center", edittype: 'text', sortable: false },
                        { name: 'DEPARTAMENTO', index: 'DEPARTAMENTO', width: 80, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'LOCALIDAD', index: 'LOCALIDAD', width: 80, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'TELEFONO', index: 'TELEFONO', width: 55, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'CENTRO_DE_SALUD', index: 'CENTRO_DE_SALUD', width: 120, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'CAUSA', index: 'CAUSA', width: 60, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'ESPECIALIDAD', index: 'ESPECIALIDAD', width: 70, resizable: true, align: "left", edittype: 'text', sortable: false },
                        { name: 'DERIVACION INTERNA', index: 'DERIVACION_INTERNA', width: 60, resizable: true, align: "center", edittype: 'text', sortable: false },
                        { name: 'INTERCONSULTA', index: 'INTERCONSULTA', width: 75, resizable: true, align: "center", edittype: 'text', sortable: false },
                        { name: 'ATENDIDO_LOCAL', index: 'ATENDIDO_LOCAL', width: 55, resizable: true, align: "center", edittype: 'text', sortable: false },
                        { name: 'PROGRAMADO', index: 'PROGRAMADO', width: 70, resizable: true, align: "center", edittype: 'text', sortable: false },
                        { name: 'DONDE', index: 'DONDE', width: 120, resizable: true, align: "center", edittype: 'text', sortable: false },
                        { name: 'CUANDO', index: 'CUANDO', width: 50, resizable: true, align: "center", edittype: 'text', sortable: false },
                        { name: 'RESUELTO', index: 'RESUELTO', width: 50, resizable: true, align: "center", edittype: 'text', sortable: false },
                    ],
                    //pager: jQuery('#rptPager'),
                    autowidth: true,
                    shrinkToFit: false,
                    height: 'auto',
                    rowList: [10, 40, 100],
                    sortname: 'pacId',
                    sortorder: "asc",
                    viewrecords: true,
                    loadonce: true,
                    gridview: true,
                    emptyrecords: "Sin registros",
                    loadtext: "Cargando...",
                    caption: 'Reporte Protur',
                    'loadComplete': function (data) {
                        if(data== null)
                        {
                            cerrarPopUp();
                        }
                        _TotalRecords = data.records;
                        _Datos = data.rows;
                        _RowID = RowID;
                        _NombreUsuario = data._NombreUsuario;
                        parse_datos(_Datos, data.rows.length);
                    },
                    
                    onSelectRow: function (rowid) {
                        var rowData = $(this).jqGrid("getLocalRow", rowid), str = "", p;
                        for (p in rowData) {
                            if (rowData.hasOwnProperty(p)) {
                                str +=  p + " : " + rowData[p] + "\n";
                            }
                        }
                        jAlert(str);
                    }
                });
                jQuery("#rpt").jqGrid('navGrid', '', { edit: false, add: false, del: false, search: false, refresh: true, closeOnEscape: false, addtext: "Agregar", edittext: "Modificar", deltext: "Eliminar", viewtext: "info", searchtext: "Buscar", refreshtext: "Recargar" });
                $("#rpt").setGridParam({ rowNum: _TotalRecords, datatype: 'json', page: 1 }).trigger('reloadGrid');
                $("#rpt").setGridParam({ height: '400' }).trigger('reloadGrid');
            });


            var tmp;
            function parse_datos(datos, len) {
                for (var i = 0; i < len; i++) {

                    if(tmp != datos[i].cell[0]){
                        _TotalPacientes++;
                        tmp = datos[i].cell[0];
                    }

                    if (datos[i].cell[11] != "") //Especialidad !=""
                    {
                        _TurnosSolicitados++;
                    }

                    if (datos[i].cell[12] != "") //Derivacion
                    {
                        _ConDerivacion++;
                    }
                    if (datos[i].cell[13] != "") //Interconsulta
                    {
                        _PorInterconsulta++;
                    }
                    if (datos[i].cell[14] != "") //Atendido
                    {
                        _Atendidos++;
                    }
                    if (datos[i].cell[15] != "") //Programados
                    {
                        _Programados++;
                        if (datos[i].cell[16] == datos[i].cell[9]) //Centro de Salud
                        {
                            _EstaInstitucion++;
                        }
                        else {
                            _OtraInstitucion++;
                        }
                    }
                    if (datos[i].cell[18] == "" && datos[i].cell[11] == "") //Sin Resolver
                    {
                        _SinResolver++;
                    }

                    if (datos[i].cell[17] != "") //Dias - Cuando
                    {
                        var fecha_inicio = fechaSelected;

                        fecha_inicio = fecha_inicio.split('/');
                        var fecha_pla = datos[i].cell[17].split('/');
                        var date1 = new Date(fecha_inicio[2] + "-" + fecha_inicio[1] + "-" + fecha_inicio[0]);
                        var date2 = new Date(fecha_pla[2] + "-" + fecha_pla[1] + "-" + fecha_pla[0]);
                        var diff = dateDiff(date1, date2);

                        if (_MinFecha == "") {
                            _MinFecha = diff;
                        }
                        if (_MinFecha >= diff)
                        {
                            _MinFecha = diff;
                        }
                        if (_MaxFecha == "") {
                            _MaxFecha = diff;
                        }
                        if (_MaxFecha <= diff) {
                            _MaxFecha = diff;
                        }
                    }
                }

                $("#f_min").html(_MinFecha);
                $("#f_max").html(_MaxFecha);

                /*Promedio entre fechas menoy y mayor*/
                _Promedio = ((_MaxFecha + _MinFecha) / 2).toFixed(2);
                $("#f_prom").html(_Promedio);
                /*Fin*/
                

                /*Sin derivacion Porcentaje*/
                _SinDerivacion = _TurnosSolicitados - (_ConDerivacion + _PorInterconsulta);
                var p_SinDerivacion;
                if (_SinDerivacion > 0) {
                    p_SinDerivacion = (_SinDerivacion / _TurnosSolicitados * 100).toFixed(0) + "%";
                    $("#ps_derivacion").html(p_SinDerivacion);
                }
                else {
                    p_SinDerivacion = 0 + "%";
                    $("#ps_derivacion").html(p_SinDerivacion);
                }
                /*Fin*/

                /*Con derivacion*/
                var p_ConDerivacion;
                if (_ConDerivacion > 0) {
                    p_ConDerivacion = (_ConDerivacion / _TurnosSolicitados * 100).toFixed(0) + "%";
                    $("#pc_derivacion").html(p_ConDerivacion);
                } else {
                    p_ConDerivacion = 0 + "%";
                    $("#pc_derivacion").html(p_ConDerivacion);
                }
                /*Fin*/
                
                /*Por interconsulta porcentaje*/
                var p_PorInterconsulta;
                if (_PorInterconsulta > 0) {
                    p_PorInterconsulta = (_PorInterconsulta / _TurnosSolicitados * 100).toFixed(0) + "%";
                    $("#pp_interconsulta").html(p_PorInterconsulta);
                }
                else {
                    p_PorInterconsulta = 0 + "%";
                    $("#pp_interconsulta").html(p_PorInterconsulta);
                }
                /*Fin*/


                /*Porcentaje atendidos*/
                var p_Atendidos;
                if (_Atendidos > 0){
                    p_Atendidos = (_Atendidos / _TurnosSolicitados * 100).toFixed(0) + "%";
                    $("#p_atendidos").html(p_Atendidos);
                }
                else {
                    p_Atendidos = 0 +"%"; 
                    $("#p_atendidos").html(p_Atendidos);
                }
                /*Fin*/

                /*Porcentaje Programados*/
                var p_Programados;
                if (_Programados > 0)
                {
                    p_Programados = (_Programados / _TurnosSolicitados * 100).toFixed(0) + "%";
                    $("#p_programados").html(p_Programados);
                }
                else {
                    p_Programados = 0 + "%";
                    $("#p_programados").html(p_Programados);
                }
                /*Fin*/

                /*Porcentaje Sin resolver */
                var p_SinResolver;
                if (_SinResolver>0)
                {
                    p_SinResolver = (_SinResolver / _TurnosSolicitados * 100).toFixed(0) + "%";
                    $("#p_sresolver").html(p_SinResolver);
                }
                else
                {
                    p_SinResolver = 0 + "%";
                    $("#p_sresolver").html(p_SinResolver);
                }
                /*Fin*/
                
                /*Porcentaje en esta institucion*/
                var p_EstaInstitucion;
                if (_EstaInstitucion > 0)
                {
                    p_EstaInstitucion = (_EstaInstitucion / _Programados * 100).toFixed(0) + "%";
                    $("#p_estainstitucion").html(p_EstaInstitucion);
                } else {
                    p_EstaInstitucion = 0 + "%";
                    $("#p_estainstitucion").html(p_EstaInstitucion);
                }
                /*Fin*/
                
                /*Porcentaje Otra Institucion*/
                var p_OtraInstitucion;
                if(_OtraInstitucion>0)
                {
                    p_OtraInstitucion = (_OtraInstitucion / _Programados * 100).toFixed(0) + "%";
                    $("#p_otrainstitucion").html(p_OtraInstitucion);
                }else{
                    p_OtraInstitucion = 0 + "%";
                    $("#p_otrainstitucion").html(p_OtraInstitucion);
                }
                /*Fin*/
                

                $("#p_interconsulta").html(_PorInterconsulta);
                $("#c_derivacion").html(_ConDerivacion);
                $("#s_derivacion").html(_SinDerivacion);
                $("#atendidos").html(_Atendidos);
                $("#programados").html(_Programados);
                $("#s_resolver").html(_SinResolver);
                $("#estainstitucion").html(_EstaInstitucion);
                $("#otrainstitucion").html(_OtraInstitucion);
                $("#t_solicitados").html(_TurnosSolicitados);
                $("#t_pacientes").html(_TotalPacientes);

                $("#1").html("Encuesta N° : " + _RowID);
                $("#2").html("Fecha de encuesta: " + fechaSelected  );
                $("#3").html("Encuestador : " + Encuestador);
                $("#header").html(" " + csNombre );
                $("#header_usr").html("Usuario: " + _NombreUsuario);

                FusionCharts.ready(function () {

                    var por_origen = new FusionCharts({
                        type: 'doughnut3d',
                        renderAt: 'por_origen',
                        width: '450',
                        height: '300',
                        dataFormat: 'json',
                        dataSource: {
                            "chart": {
                                "caption": "Por Origen",
                                "subCaption": "",
                                "numberPrefix": " Cantidad ",
                                "paletteColors": "#0075c2,#1aaf5d,#f2c500,#f45b00,#8e0000",
                                "bgColor": "#ffffff",
                                "showBorder": "0",
                                "use3DLighting": "0",
                                "showShadow": "0",
                                "enableSmartLabels": "0",
                                "startingAngle": "310",
                                "showLabels": "0",
                                "showPercentValues": "1",
                                "showLegend": "1",
                                "legendShadow": "0",
                                "legendBorderAlpha": "0",
                                "decimals": "0",
                                "captionFontSize": "14",
                                "subcaptionFontSize": "14",
                                "subcaptionFontBold": "0",
                                "toolTipColor": "#ffffff",
                                "toolTipBorderThickness": "0",
                                "toolTipBgColor": "#000000",
                                "toolTipBgAlpha": "80",
                                "toolTipBorderRadius": "2",
                                "toolTipPadding": "5",
                            },
                            "data": [
                                {
                                    "label": "Sin Derivación",
                                    "value": _SinDerivacion, //"5"
                                },
                                {
                                    "label": "Con Derivación",
                                    "value": _ConDerivacion, //"10"
                                },
                                {
                                    "label": "Por Interconsulta",
                                    "value": _PorInterconsulta, //"17"
                                },
                            ]
                        }
                    }).render();


                    var estado_del_turno = new FusionCharts({
                        type: 'doughnut3d',
                        renderAt: 'estado_del_turno',
                        width: '450',
                        height: '300',
                        dataFormat: 'json',
                        dataSource: {
                            "chart": {
                                "caption": "Estado del Turno",
                                "subCaption": "",
                                "numberPrefix": " Cantidad ",
                                "paletteColors": "#0075c2,#1aaf5d,#f2c500,#f45b00,#8e0000",
                                "bgColor": "#ffffff",
                                "showBorder": "0",
                                "use3DLighting": "0",
                                "showShadow": "0",
                                "enableSmartLabels": "0",
                                "startingAngle": "310",
                                "showLabels": "0",
                                "showPercentValues": "1",
                                "showLegend": "1",
                                "legendShadow": "0",
                                "legendBorderAlpha": "0",
                                "decimals": "0",
                                "captionFontSize": "14",
                                "subcaptionFontSize": "14",
                                "subcaptionFontBold": "0",
                                "toolTipColor": "#ffffff",
                                "toolTipBorderThickness": "0",
                                "toolTipBgColor": "#000000",
                                "toolTipBgAlpha": "80",
                                "toolTipBorderRadius": "2",
                                "toolTipPadding": "5",
                            },
                            "data": [
                                {
                                    "label": "Atendido",
                                    "value": _Atendidos, //"5"
                                },
                                {
                                    "label": "Programado",
                                    "value": _Programados, //"1"
                                },
                                {
                                    "label": "Sin Resolver",
                                    "value": _SinResolver, //"17"
                                },
                            ]
                        }
                    }).render();


                    var programado_donde = new FusionCharts({
                        type: 'doughnut3d',
                        renderAt: 'programado_donde',
                        width: '450',
                        height: '300',
                        dataFormat: 'json',
                        dataSource: {
                            "chart": {
                                "caption": "Programado Donde",
                                "subCaption": "",
                                "numberPrefix": "$",
                                "paletteColors": "#0075c2,#1aaf5d,#f2c500,#f45b00,#8e0000",
                                "bgColor": "#ffffff",
                                "showBorder": "0",
                                "use3DLighting": "0",
                                "showShadow": "0",
                                "enableSmartLabels": "0",
                                "startingAngle": "310",
                                "showLabels": "0",
                                "showPercentValues": "1",
                                "showLegend": "1",
                                "legendShadow": "0",
                                "legendBorderAlpha": "0",
                                "decimals": "0",
                                "captionFontSize": "14",
                                "subcaptionFontSize": "14",
                                "subcaptionFontBold": "0",
                                "toolTipColor": "#ffffff",
                                "toolTipBorderThickness": "0",
                                "toolTipBgColor": "#000000",
                                "toolTipBgAlpha": "80",
                                "toolTipBorderRadius": "2",
                                "toolTipPadding": "5",
                            },
                            "data": [
                                {
                                    "label": "En esta Institución",
                                    "value": _EstaInstitucion, //"5"
                                },
                                {
                                    "label": "En etra Institución",
                                    "value": _OtraInstitucion,//"5"
                                },
                            ]
                        }
                    }).render();


                    FusionCharts.ready(function () {
                        var dias_de_programacion = new FusionCharts({
                            type: 'column3d',
                            renderAt: 'dias_de_programacion',
                            //width: '500',
                            //height: '300',
                            dataFormat: 'json',
                            dataSource: {
                                "chart": {
                                    "caption": "Dias de Programacion",
                                    "subCaption": "",
                                    //"xAxisName": "Month",
                                    //"yAxisName": "Revenues (In USD)",
                                    "paletteColors": "#0075c2",
                                    "valueFontColor": "#ffffff",
                                    "baseFont": "Helvetica Neue,Arial",
                                    "captionFontSize": "14",
                                    "subcaptionFontSize": "14",
                                    "subcaptionFontBold": "0",
                                    "placeValuesInside": "1",
                                    "rotateValues": "1",
                                    "showShadow": "0",
                                    "divlineColor": "#999999",
                                    "divLineIsDashed": "1",
                                    "divlineThickness": "1",
                                    "divLineDashLen": "1",
                                    "divLineGapLen": "1",
                                    "canvasBgColor": "#ffffff"
                                },

                               
                        
                                "data": [
                                    {
                                        "label": "Menor",
                                        "value": _MinFecha,
                                    },
                                    {
                                        "label": "Mayor",
                                        "value": _MaxFecha,
                                    },
                                    {
                                        "label": "Promedio",
                                        "value": _Promedio,
                                    },

                                    
                                ]
                            }
                        }).render();
                    });
                });

                $body = $("body");
                $body.removeClass("loading");
            }
        </script>


</head>
    <body class="loading">
        <div class="modal"><!-- Place at bottom of page --></div>
        <div id="header" style=" height:60px; line-height:60px; font-size:16px; font-weight:700 ;  border: solid 1px #f2f2f2; width:79%; margin-top:20px; text-align:center;float:left "></div>
        <div id="header_usr" style=" height:60px; line-height:60px; font-size:12px; font-weight:700 ;  border: solid 1px #f2f2f2; width:20%; margin-top:20px; text-align:center;float:left "></div>
        

        <div id="1" style="font-size:14px;text-align:center; border: solid 1px #f2f2f2; width:33%; margin-top:20px; margin-bottom:20px; float:left">  </div>
        <div id="2" style="font-size:14px;text-align:center; border: solid 1px #f2f2f2; width:33%; margin-top:20px; margin-bottom:20px; float:left">  </div>
        <div id="3" style="font-size:14px;text-align:center; border: solid 1px #f2f2f2; width:33%; margin-top:20px; margin-bottom:20px; float:left">  </div>

        <div style="text-align:left; font-size:12px; width:99%;" ><button onclick="window.print();">Imprimir</button></div>
        <hr />
        <div id="grid" style="width:99%; background-color:#f2f2f2; float:left">
        <table id="rpt"></table>
        </div>

        
        
        <div style="width:40%; margin-top:25px; float:left;  text-align:center; min-height:300px;">
            <!-- <img src="<%=Url.Content("~/Scripts/jqgrid/1.png")%>" /> -->
            <table class="tg">
              <tr>
                <th class="tg-baqh" colspan="3"><b>Datos Totales</b></th>
              </tr>
              <tr>
                <td class="tg-031e" rowspan="2">Datos Generales</td>
                <td class="tg-yw4l">Total de Pacientes</td>
                <td id="t_pacientes" class="tg-yw4l"></td>

              </tr>
              <tr>
                <td class="tg-yw4l">Turnos Solicitados</td>
                <td id="t_solicitados" class="tg-yw4l"></td>
              </tr>

              <!-- Por Origen -->
              <tr>
                <td class="tg-031e" rowspan="3">Por Origen</td>
                <td class="tg-yw4l">Sin Derivación</td>
                <td id="s_derivacion" class="tg-yw4l"></td>
                <td id="ps_derivacion" class="tg-yw4l"></td>
              </tr>
              <tr>
                <td class="tg-yw4l">Con Derivación</td>
                <td id="c_derivacion" class="tg-yw4l"></td>
                <td id="pc_derivacion" class="tg-yw4l"></td>
              </tr>
              <tr>
                <td class="tg-yw4l">Por Interconsulta</td>
                <td id="p_interconsulta" class="tg-yw4l"></td>
                <td id="pp_interconsulta"  class="tg-yw4l"></td>
              </tr>

              <!-- Estado del Turno -->
              <tr>
                <td class="tg-031e" rowspan="3">Estado del Turno</td>
                <td class="tg-yw4l">Atendidos</td>
                <td id="atendidos" class="tg-yw4l"></td>
                <td id="p_atendidos" class="tg-yw4l"></td>
              </tr>
              <tr>
                <td class="tg-yw4l">Programados</td>
                <td id="programados" class="tg-yw4l"></td>
                <td id="p_programados" class="tg-yw4l"></td>
              </tr>
              <tr>
                <td class="tg-yw4l">Sin Resolver</td>
                <td id="s_resolver" class="tg-yw4l"></td>
                <td id="p_sresolver" class="tg-yw4l"></td>
              </tr>

              <!-- Programado donde -->
              <tr>
                <td class="tg-031e" rowspan="2">Programado Donde</td>
                <td class="tg-yw4l">En esta Institución</td>
                <td id="estainstitucion" class="tg-yw4l"></td>
                <td id="p_estainstitucion" class="tg-yw4l"></td>
              </tr>
              <tr>
                <td class="tg-yw4l">En otra Institución</td>
                <td id="otrainstitucion" class="tg-yw4l"></td>
                <td id="p_otrainstitucion" class="tg-yw4l"></td>
              </tr>


              <!-- Estado del Turno -->
              <tr>
                <td class="tg-031e" rowspan="3">Dias de Programacion</td>
                <td class="tg-yw4l">Menor</td>
                <td id="f_min" class="tg-yw4l"></td>
              </tr>
              <tr>
                <td class="tg-yw4l">Mayor</td>
                <td id="f_max" class="tg-yw4l"></td>
              </tr>
              <tr>
                <td class="tg-yw4l">Promedio</td>
                <td id="f_prom" class="tg-yw4l"></td>
              </tr>

            </table>

       </div>
        <div id="por_origen" name="por_origen" style="width:30%; float:left; background-color:#f4f4f4; text-align:center; min-height:300px;"></div>
        <div id="estado_del_turno" name="estado_del_turno" style="width:30%; float:left; background-color:#f4f4f4; text-align:center; min-height:300px;"></div>
        <div id="programado_donde" name="programado_donde" style="width:30%; float:left; background-color:#f4f4f4; text-align:center; min-height:300px;"></div>
        <div id="dias_de_programacion" name="dias_de_programacion" style="width:30%; float:left; background-color:#f4f4f4; text-align:center; min-height:300px; margin-bottom:80px"></div>
    </body>
</html>

<script type="text/javascript">

   
    function dateDiff(date1, date2) {
        return parseInt((date2 - date1) / (24 * 3600 * 1000));
    }

</script>