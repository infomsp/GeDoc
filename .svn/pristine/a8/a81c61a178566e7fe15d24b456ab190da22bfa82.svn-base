
var color = {
    verde:'#89A54E',
    azul:'#4572A7', 
    rojo:'#AA4643'
    };

function microchar_create_petroleo(){
    microchart_do(
        "micro-petroleo",
        [83, 65, 58, 54, 42]
        );
}

function microchar_create_oro(){
    microchart_do(
        "micro-oro",
        [1140, 1360, 1090]
        );
}

function microchar_create_soja(){
    microchart_do(
        "micro-soja",
        [383, 465, 358, 354, 320]
        );
}

function microchar_create_trigo(){
    microchart_do(
        "micro-trigo",
        [300, 175, 164, 165, 156]
        );
}

function microchar_create_maiz(){
    microchart_do(
        "micro-maiz",
        [123, 135, 158, 154, 142]
        );
}


function microchart_do(id, values){
    var o = microchartObj(
        id,
        ['Ene', 'Feb', 'Mar', 'Abr', 'May'],
        values
        );

    //set microchart-value
    $("#"+id).siblings(".microchart-value").html(values[values.length-1]);
    //create chart
    var chart = new Highcharts.Chart(o);
}

function microchartObj(id,categ,data) {
    
    if (!categ) {categ=['Ene', 'Feb', 'Mar', 'Abr', 'May'];}
    if (!data) {data=[83, 65, 58, 54, 43];}
    
    return {
        chart: {
            renderTo: id,
            type: 'line',
            plotBorderWidth: null,
            marginTop: 0,
            marginBottom: 0,
            marginLeft:0,
            marginRight:0,
            plotShadow: false,
            borderWidth: 0,
            plotBorderWidth: 0,
            width: 120,
            height: 70
        },
        tooltip: {
             userHTML: false,
             style: {
                 padding: 3,
                 width: 0,
                 height: 0,
             },
             formatter: function () {
                return ""; //this.x +": "+this.y;
             },
             
         },
        title: {
            text: ''
        },
        yAxis: {
            min: 0,
            title: {
                text: ''
            },
            gridLineWidth:0,
            showEmpty:false,
            enabled:false
        },
        credits: {
            enabled: false
        },
        legend: {
            enabled:false
        },
        plotOptions: {
            line:{
                lineWidth:1.5,
            },
             showInLegend: false,
             tooltip: {}
        },
        xAxis: {
            enabled:false,
            showEmpty:false,
            categories: categ
        },
        series: [{
                 marker: {
                    enabled: true
                },
            animation:false,
            name: '',
            enableMouseTracking: false,
            data: data
        }]
    };
};

function create_chart_obra(title,planData,realData)
{
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'chart-obra',
                height: 350,
            },
            labels: {
                items: [{
                    html: title,
                    style: {
                        left: '20px',
                        top: '14px',
                        color: 'gray',
                        "font-size":"16px",
                    }
                }]
            },
            title: {
                text: ''
            },
            yAxis: {
                title: {text:"% avance"},
                min:0,
            },
            xAxis: {
                categories: ['Ene', 'Feb', 'Mar', 'Abr', 'May','Jun','Jul','Ago','Sep','Oct','Nov']
            },
            tooltip: {
                formatter: function() {
                    var s;
                    if (this.point.name) { // the pie chart
                        s = ''+
                            this.point.name +': '+ this.y ;
                    } else {
                        s = ''+
                            this.series.name+" "+this.x  +': '+ this.y +"%";
                    }
                    return s;
                }
            },
            series: [{
                type: 'spline',
                name: 'Plan',
                color: 'darkgray',
                data: planData,
                dashStyle: 'shortdot',
                marker: {
                    lineWidth: 2,
                	lineColor: Highcharts.getOptions().colors[3],
                	fillColor: 'white'
                }
            }, {
                type: 'spline',
                name: 'Real',
                color: 'darkGreen',
                data: realData,
                marker: {
                    lineWidth: 2,
                	lineColor: Highcharts.getOptions().colors[3],
                	fillColor: 'yellow'
                }
            }]
        });

};


