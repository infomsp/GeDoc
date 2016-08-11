function generateChart(chartData, labels, type, selector) {
    //inicializo data object
    var data = {
        labels: [],
        datasets: []
    };

    //por cada elemento
    for (k in chartData) {

        data.datasets.push(
            {
                label: chartData[k].label,
                data: chartData[k].data
            }
        );
    }

    //paleta de colores
    var colorChart = [
        'rgba(255,127,80,1)',
        'rgba(255,140,0,1)',
        'rgba(128,128,0,1',
        'rgba(46,139,87,1)',
        'rgba(32,178,170,1)',
        'rgba(100,149,237,1)',
        'rgba(218,112,214,1)',
        'rgba(192,192,192,1)',
        'rgba(230,230,250,1)',
        'rgba(0,0,128,1)',
        'rgba(30,144,255,1)'
    ];

    //agrego labels
    data.labels = labels;

    //agrego propiedades
    for (k in chartData) {

        var random = Math.floor((Math.random() * colorChart.length) + 1);

        data.datasets[k].lineTension = 0;
        data.datasets[k].backgroundColor = colorChart[random];
        data.datasets[k].borderColor = colorChart[random];
        data.datasets[k].fill = false;
    }

    ctx = $(selector);

    var scatterChart = new Chart(ctx, {
        type: type,
        data: data,
        options: {
            responsive: false
        }
    });
}

/*
parámemtros necesarios

chartData, labels, type, selector

chartData: es un arreglo de json, debe tener el siguiente formato

var chartData = [
                    {
                        label: 'Htal Rawson',
                        data: [12,32]
                    },

                    {
                        label: 'Mcial Quiroga',
                        data: [4,19]
                    }
                ];
                
labels: es un arreglo de strings que DEBE tener la misma cantidad de valores que el arreglo data de chartData

var labels = ['unos','otros']

type: una string que puede ser
	line
	radar
	bar


el archivo de origen tiene que incluir el script chart (que tenga incluído el módulo de modernizr, generalmente se llama chart.bundle.js)
selector: tiene que ser el selector a un tag tipo canvas
*/