
//function chart_obra(elem){
//    var id = elem.id;
//    var title = $(elem).children(".dial-title").text();
    
//    //default - ruta 6
//    var plan = [0,10,15,18,32,34,50,60,90,95,100];
//    var realData = [0,12,18,20,34,35,40,45,55];
//    if (id==="hosp"){
//        plan = [0,10,18,25,32,40,50,60,75,90,100];
//        realData = [0,12,18,26,34,45];
//    }
//    else if (id==="dial3"){
//        plan =     [0,20,30,35,45,50,55,60,70,90,100];
//        realData = [0,12,18,26,34,45,52,70,80];
//    }
//    else if (id==="dial4"){
//        plan =     [0,20,30,35,45,50,55,60,70,90,100];
//        realData = [0,12,18,26,34,45,52,65];
//    }
//    else if (id==="dial5"){
//        plan =     [0,10,18,25,32,40,50,60,75,90,100];
//        realData = [0,12,18,22,21,31,41,45];
//    }
//    else if (id==="dial6"){
//        plan =     [0,20,38,55,70,80,85,90,93,96,100];
//        realData = [0,12,18,26,36,48,55,70,80,98];
//    }
//    create_chart_obra(title,
//            plan,realData);
//}

function microchart_popup(event){
    var elem = event.currentTarget;
    var id = elem.id;
    var title = $(elem).children(".boxed-title").text();
    
    var chartCode='crude-oil';
    if (title==='Oro'){chartCode='gold';}
    if (title==='Soja'){chartCode='soybeans';}
    else if (title==='Trigo'){chartCode='Wheat';}
    else if (title==='Maíz'){chartCode='Corn';}
        
    $("#chart-popup").html(
            '<img alt="'+title+' - Monthly Price - Commodity Prices - Price Charts, Data, and News - IndexMundi"'+ 
                 ' src="http://www.indexmundi.com/commodities/image.aspx?commodity='+chartCode+'"'+
                 ' title="Click to play with this data at IndexMundi">');
         
    $("#chart-popup").removeClass("hidden");
    open_popup();
}

//function create_EnFoco(){
//    debugger;
//    //microchar_create_petroleo();
//    //microchar_create_oro();
//    //microchar_create_soja();
//    //microchar_create_trigo();
//    //microchar_create_maiz();
    
//    ruta6 = {
//        id:"ruta6",
//        title:"Obra Ruta Prov Nº6",
//        label:"Desvío",
//        type:6,
//        value:-22,
//        max:25,
//        units:"%",
//        decPlaces:0,
//        color:function(){
//            if (Math.abs(this.value)>=20) return Gauges.colors.danger;
//            if (Math.abs(this.value)>=10) return Gauges.colors.warning;
//            }
//    };
//    Gauges.add(ruta6);
    
//    Gauges.add({
//        id:"hosp",
//        title:"Hospital Zonal Noroeste",
//        label:"Desvío",
//        type:6,
//        value:+2,
//        min:-24,max:25,
//        units:"%",
//        decPlaces:0,
//        color:function(){
//            if (Math.abs(this.value)>=20) return Gauges.colors.danger;
//            if (Math.abs(this.value)>=10) return Gauges.colors.warning;
//            }
//    });

//    //Gauges.add({
//    //    id:"dial3",
//    //    title:"Sala 1ros Aux",
//    //    label:"Desvío",
//    //    type:6,
//    //    value:+7,
//    //    min:-24,max:25,
//    //    units:"%",
//    //    width:140,
//    //});
//    //Gauges.add({
//    //    title:"Ingreso Circ.",
//    //    id:"dial4",
//    //    label:"Desvío",
//    //    type:6,
//    //    min:-24,max:25,
//    //    value:0.5,
//    //    units:"%",
//    //    width:140,
//    //});
//    //Gauges.add({
//    //    title:"Edificio Legislat",
//    //    id:"dial5",
//    //    label:"Desvío",
//    //    type:6,
//    //    min:-24,max:25,
//    //    value:-15,
//    //    units:"%",
//    //    width:140,
//    //    color:function(){
//    //        if (Math.abs(this.value)>=20) return Gauges.colors.danger;
//    //        if (Math.abs(this.value)>=10) return Gauges.colors.warning;
//    //        }
//    //});
//    //Gauges.add({
//    //    title:"Parquizado",
//    //    id:"dial6",
//    //    label:"Desvío",
//    //    type:6,
//    //    min:-24,max:25,
//    //    value:+3.4,
//    //    units:"%",
//    //    width:140,
//    //});
    
//    Gauges.init();
//    Gauges.animate(-20, 0.5);

//    chart_obra(document.getElementById("ruta6"));
//}
function open_popup(){
    //$("#obscure-background").removeClass("hidden");
    $("#obscure-background").fadeIn(200,function(){
        $("#obscure-background").noClickDelay().click(close_popup);
    });
    //$("#popup").removeClass("hidden");
    $("#popup").fadeIn();
    //
    //al cliquear fuera del popup, se cierra
}    

function close_popup(){
    //$("#obscure-background").addClass("hidden");
    $("#obscure-background").fadeOut(200);
    $("#popup").addClass("hidden");
    $("#obscure-background").off("click");
}

Date.prototype.addDays = function(days) {
    this.setDate(this.getDate() + days);
    return this;
};

function DateFmt(fstr) {
    this.formatString = fstr;

    var mthNames = ["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"];
    var dayNames = ["Sun","Mon","Tue","Wed","Thu","Fri","Sat"];
    var zeroPad = function(number) {
       return ("0"+number).substr(-2,2);
    };

    var dateMarkers = {
      d:['getDate',function(v) { return zeroPad(v);}],
      m:['getMonth',function(v) { return zeroPad(v+1);}],
      n:['getMonth',function(v) { return mthNames[v]; }],
      w:['getDay',function(v) { return dayNames[v]; }],
      y:['getFullYear'],
      H:['getHours',function(v) { return zeroPad(v);}],
      M:['getMinutes',function(v) { return zeroPad(v);}],
      S:['getSeconds',function(v) { return zeroPad(v);}],
      i:['toISOString']
    };

    this.format = function(date) {
      var dateTxt = this.formatString.replace(/%(.)/g, function(m, p) {
        var rv = date[(dateMarkers[p])[0]]()

        if ( dateMarkers[p][1] !== null ) rv = dateMarkers[p][1](rv)

        return rv

      });

      return dateTxt;
    }
};

(function($) {
    $.fn.noClickDelay = function() {
        var $wrapper = this;
        var $target = this;
        var moved = false;
        $wrapper.bind('touchstart mousedown', function(e) {
            e.preventDefault();
            moved = false;
            $target = $(e.target);
            if ($target.nodeType === 3) {
                $target = $($target.parent());
            }
            $target.addClass('pressed');
            $wrapper.bind('touchmove mousemove', function(e) {
                moved = true;
                $target.removeClass('pressed');
            });
            $wrapper.bind('touchend mouseup', function(e) {
                $wrapper.unbind('mousemove touchmove');
                $wrapper.unbind('mouseup touchend');
                if (!moved && $target.length) {
                    $target.removeClass('pressed');
                    $target.trigger('click');
                    $target.focus();
                }
            });
        });
    return this;
    };
})(jQuery);

//$(document).ready(function() {

//    // this is all you need to initialize the plugin
//    //$('.tabtab').tabTab();

//    //var df = new DateFmt("%d-%n");

//    //$("#arrivals tbody tr td:nth-child(3)").each(function(inx,elem){
//    //    $(elem).html(df.format(new Date()));
//    //});

//    //$("#arrivals tbody tr td:nth-child(4)").each(function(inx,elem){
//    //    $(elem).html(df.format(new Date().addDays(7+Math.random()*21)));
//    //});

//    //Abanico.convertAll();

//    //$('button').noClickDelay();
//    //$('a').noClickDelay();

//    //$("#arrivals tbody tr")
//    //.noClickDelay()
//    //.click(function(){
//    //        $("#reservation").removeClass("hidden");
//    //        $("#obscure-background").removeClass("hidden");
//    //        //$("#obscure-background").fadeIn();
//    //        $("#popup").removeClass("hidden");
//    //        //$("#popup").fadeIn();
//    //})
//    //;

//    ////.microchart.boxed -> click -> microchart_popup()
//    //$(".microchart.boxed").noClickDelay().click(microchart_popup);

//    create_EnFoco();
    
//    //

///*    .mousedown(function(){
//        var $self=$(this);
//        $self.css("background","yellow");
//        setTimeout(function(){$self.css("background","");},600);
//    })
//    */
//});


