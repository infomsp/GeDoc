function InvocarReporte(psReporte, psParametros, EsJuntaMedica) {
    //Verificamos que sea diferente de vacio
    if (psReporte != '' && psReporte != null) {
        var _vParametros = '';
        if (psParametros != null) {
            var _vParametros = '&ci=' + psParametros.length.toString();
            for (var _i = 0; _i < psParametros.length; _i++) {
                _vParametros += '&v' + _i.toString() + '=' + psParametros[_i][0];
                _vParametros += '&n' + _i.toString() + '=' + psParametros[_i][1];
            }
            if (EsJuntaMedica != null) {
                _vParametros += '&JM=true';
            }
        }
        var _src = GetPathApp('Reportes/EjecutarReportes.aspx?sr=' + psReporte + _vParametros);
        $('#frameReportes').attr('src', _src);
        var _WindowImprimir = $("#wImprimir").data("tWindow");
        if (_WindowImprimir.isMaximized) {
            _WindowImprimir.center().open();
        }
        else {
            _WindowImprimir.center().maximize().open();
        }

        AbrirWaiting();
    }
}

function AbrirWaiting() {
    
    var _WindowWait = $("#wWaiting").data("tWindow");
    if (_WindowWait != null) {
        $('#wWaiting').css({ 'height': 80, 'width': 360 });
        $('#wWaiting').find('div.t-window-content').css({ 'height': 50, 'width': 348 });
        _WindowWait.center().open();
    }
    
}
function CerrarWaiting() {

    var _WindowWait = $("#wWaiting").data("tWindow");
    if (_WindowWait != null) {
        _WindowWait.close();
    }

}
function onResizeWindow(e) {
    var h = window.innerHeight - 45;
    var repsystem = document.getElementById("frameReportes");
    repsystem.height= h;
}
