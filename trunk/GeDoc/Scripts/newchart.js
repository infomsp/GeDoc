
var color = {
    verde:'#89A54E',
    azul:'#4572A7', 
    rojo:'#AA4643'
    };

function create_chart_cashflow()
{
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'chart_cashflow'
            },
            title: {
                text: ''
            },
            yAxis: {
                title: {text:"en miles de millones"}
            },
            xAxis: {
                categories: ['Ene', 'Feb', 'Mar', 'Abr', 'May']
            },
            /*tooltip: {
                formatter: function() {
                    var s;
                    if (this.point.name) { // the pie chart
                        s = ''+
                            this.point.name +': '+ this.y ;
                    } else {
                        s = ''+
                            this.x  +': '+ this.y;
                    }
                    return s;
                }
            },*/
            labels: {
                items: [{
                    html: 'Relación Ingresos/Egresos',
                    style: {
                        left: '40px',
                        top: '8px',
                        color: 'gray'
                    }
                }]
            },
            series: [{
                type: 'column',
                name: 'Ingresos',
                color: color.verde,
                data: [3.13, 2.2, 1.11, 3.15, 4.22]
            }, {
                type: 'column',
                name: 'Egresos',
                color: color.rojo,
                data: [2.75, 3.21, 5.3, 2.5, 3.34]
            }, {
                type: 'column',
                name: 'Neto',
                color: color.azul,
                data: [0.26, -0.45, -1.33, 0.22, 0.34]
            }, {
                type: 'spline',
                name: 'Reservas',
                color: 'darkgray',
                data: [2.55, 3.17, 1.85, 2.33, 2.63],
                marker: {
                    lineWidth: 2,
                	lineColor: Highcharts.getOptions().colors[3],
                	fillColor: 'white'
                }
            }, {
                type: 'pie',
                name: 'Total',
                data: [{
                    name: 'Ingresos',
                    y: 23.20,
                    color: color.verde
                }, {
                    name: 'Egresos',
                    y: 12.25,
                    color: color.rojo
                }],
                center: [100, 80],
                size: 100,
                showInLegend: false,
                dataLabels: {
                    enabled: false
                }
            }]
        });

};


function create_chart_recursos()
{
        
    
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'chart_recursos'
            },
            title: {
                text: ''
            },
            yAxis: {
                title: {text:"en miles de millones"}
            },
            xAxis: {
                categories: ['Ene', 'Feb', 'Mar', 'Abr', 'May']
            },
            /*tooltip: {
                formatter: function() {
                    var s;
                    if (this.point.name) { // the pie chart
                        s = ''+
                            this.point.name +': '+ this.y ;
                    } else {
                        s = ''+
                            this.x  +': '+ this.y;
                    }
                    return s;
                }
            },
            */
            labels: {
                items: [{
                    html: 'Total por Fuente',
                    style: {
                        left: '50px',
                        top: '8px',
                        color: 'gray'
                    }
                }]
            },
            series: [{
                type: 'column',
                name: 'Propios',
                color: color.verde,
                data: [3.13, 2.2, 1.11, 3.15, 4.22]
            }, {
                type: 'column',
                name: 'Nacionales',
                color: color.azul,
                data: [2.75, 3.21, 5.3, 7.5, 6.34]
            }, {
                type: 'column',
                name: 'Internacionales',
                color: color.rojo,
                data: [4.66, 3.45, 3.33, 9.12, 4.2]
            }, {
                type: 'spline',
                name: 'Promedio',
                color: 'darkgray',
                data: [3.55, 2.67, 3.85, 6.33, 3.33],
                marker: {
                    lineWidth: 2,
                	lineColor: Highcharts.getOptions().colors[3],
                	fillColor: 'white'
                }
            }, {
                type: 'pie',
                name: 'Total',
                data: [{
                    name: 'Propias',
                    y: 12.20,
                    color: color.verde
                }, {
                    name: 'Nacionales',
                    y: 23.25,
                    color: color.azul
                }, {
                    name: 'Internacionales',
                    y: 19.1,
                    color: color.rojo
                }],
                center: [100, 80],
                size: 100,
                showInLegend: false,
                dataLabels: {
                    enabled: false
                }
            }]
        });

};


function create_chart_gastos()
{
        var colors = ['#4572A7', '#AA4643', '#89A54E', '#80699B', '#3D96AE', 
   '#DB843D', '#92A8CD', '#A47D7C', '#B5CA92'];
   
    //new version    var colors = ['#7cb5ec', '#434348', '#90ed7d', '#f7a35c', '#8085e9', 
   //'#f15c80', '#e4d354', '#8085e8', '#8d4653', '#91e8e1'];
    
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'chart_gastos'
            },
            title: {
                text: ''
            },
            yAxis: {
                title: {text:"en miles de millones"}
            },
            xAxis: {
                categories: ['Ene', 'Feb', 'Mar', 'Abr', 'May']
            },
            /*tooltip: {
                formatter: function() {
                    var s;
                    if (this.point.name) { // the pie chart
                        s = ''+
                            this.point.name +': '+ this.y ;
                    } else {
                        s = ''+
                            this.x  +': '+ this.y;
                    }
                    return s;
                }
            },*/
            labels: {
                items: [{
                    html: 'Composición del Gasto',
                    style: {
                        left: '60px',
                        top: '0px',
                        color: 'gray'
                    }
                }]
            },
            series: [{
                type: 'column',
                name: 'Gastos en Personal',
                data: [4.66, 4.45, 4.40, 4.50, 4.52]
            }, {
                type: 'column',
                name: 'Bienes y Servicios',
                data: [2.75, 3.21, 2.3, 3.5, 3.34]
            }, {
                type: 'column',
                name: 'Bienes de Uso',
                data: [3.13, 2.2, 1.11, 3.15, 4.22]
            }, {
                type: 'column',
                name: 'Transferencias',
                data: [3.33, 4.12, 3.8, 3.66, 3.45 ]
            }, {
                type: 'column',
                name: 'Servicios de la Deuda',
                data: [1.3, 1.5, 1.34, 1.12, 1.2]
            }, {
                type: 'spline',
                name: 'Tendencia',
                color: 'darkgray',
                data: [2.55, 2.67, 2.85, 3.33, 3.40],
                marker: {
                    lineWidth: 2,
                	lineColor: Highcharts.getOptions().colors[3],
                	fillColor: 'white'
                }
            }, {
                type: 'pie',
                name: 'Total',
                data: [{
                    name: 'Gastos en Personal',
                    y: 42.20,
                    color: colors[0]
                }, {
                    name: 'Bienes y Servicios',
                    y: 16.25,
                    color: colors[1]
                }, {
                    name: 'Bienes de Uso',
                    y: 12,
                    color: colors[2]
                }, {
                    name: 'Transferencias',
                    y: 11.1,
                    color: colors[3]
                }, {
                    name: 'Serv. de la Deuda',
                    y: 6.1,
                    color: colors[4]
                }],
                center: [130, 70],
                size: 100,
                showInLegend: false,
                dataLabels: {
                    enabled: false
                }
            }]
        });

};

function create_chart_recaud()
{
    
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'chart_recaud'
            },
            title: {
                text: ''
            },
            yAxis: {
                title: {text:"en miles de millones"}
            },
            xAxis: {
                categories: ['Ene', 'Feb', 'Mar', 'Abr', 'May','Jun']
            },
            /*tooltip: {
                formatter: function() {
                    var s;
                    if (this.point.name) { // the pie chart
                        s = ''+ this.point.name +': '+ this.y ;
                        return s;
                    } else {
                        s = ''+
                            this.x  +': '+ this.y;
                    }
                    ;
                    
                
            },}*/
            labels: {
                items: [{
                    html: 'Total por Impuesto',
                    style: {
                        left: '50px',
                        top: '8px',
                        color: 'gray'
                    }
                }]
            },
            series: [{
                type: 'column',
                name: 'Inmob',
                color: color.verde,
                data: [1.13, 1.2, 1.11, 1.15, 1.22,1.12]
            }, {
                type: 'column',
                name: 'IIBB',
                color: color.azul,
                data: [2.75, 2.21, 2.3, 2.5, 2.34, 2.2]
            }, {
                type: 'column',
                name: 'Otros',
                color: color.rojo,
                data: [0.66, 0.45, 0.33, 0.12, 0.7, 1.11]
            }, {
                type: 'spline',
                name: 'Proyectado',
                color: 'darkgray',
                dashStyle: 'shortdot',
                data: [3.55, 5.67, 7.85, 9.33, 11.33, 13.29],
                marker: {
                    lineWidth: 2,
                	lineColor: Highcharts.getOptions().colors[3],
                	fillColor: 'white'
                }
            }, {
                type: 'spline',
                name: 'Real',
                color: 'darkblue',
                data: [3.25, 5.68, 6.95, 8.98, 10.4, 11.7],
                marker: {
                    lineWidth: 2,
                	lineColor: Highcharts.getOptions().colors[3],
                	fillColor: 'darkblue',
                }
            }, {
                type: 'pie',
                name: 'Total',
                data: [{
                    name: 'Inmob',
                    y: 12.20,
                    color: color.verde
                }, {
                    name: 'IIBB',
                    y: 23.25,
                    color: color.azul
                }, {
                    name: 'Otros',
                    y: 19.1,
                    color: color.rojo
                }],
                center: [100, 80],
                size: 100,
                showInLegend: false,
                dataLabels: {
                    enabled: false
                }
            }]
        });

};
