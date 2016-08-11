/* 
 * (C) 1999-2015 LMT
 */

var Gauges = {
        colors : {
            fondo: "#666867", //background
            danger : "#FF0000", //red "#FF9F4A", //orange
            warning: "#F1D100" //yellow
        },
        dials:[]
    };

Gauges.add = function(dial){
    
    //add conveniency .draw function
    dial.draw=function(){Gauges.draw(this);};
    //add to list
    Gauges.dials.push(dial);
    
};

Gauges.init = function(){
    //para cada div clase "dial", agregar "canvas"
    for (var n=0;n<Gauges.dials.length;n++){
        var dial=Gauges.dials[n];
        var elem = document.getElementById(dial.id);
        if (elem){
            var w=elem.offsetWidth;
            elem.innerHTML=
                '<div class="dial-title">'+(dial.title||'')+'</div>'+
                '<canvas width=150 height=150></canvas>'+
//                '<canvas width='+w+' height='+w+'></canvas>'+
                '<div class="dial-label">'+(dial.label||'')+'</div>';
        }
    };
    /*$(".dial").each(function(inx,elem){
        var label = $(elem).$(".dial-label");
        $(elem).append('<canvas width="200" height="200"></canvas>');
        $(elem).append(label);
    });
    */
};

Gauges.drawAll=function(){
    for (var n=0;n<Gauges.dials.length;n++){
        Gauges.draw ( Gauges.dials[n] );
    };
};

Gauges.draw = function (dial) {
    //debugger;
    div=document.getElementById(dial.id);
    canvas=$(div).children("canvas").get(0);
    if (canvas){
        draw_gauge(canvas, dial);
    }
    else {
        alert("no canvas for dial "+dial.id);
    }
};

// --------------
function draw_gauge(canvas,dial) { //width,height,position,maxvalue,units,type, offset, graduationBool){
    //debugger;
  ctx=canvas.getContext("2d");
  if (!ctx) return;

  // if (1 * maxvalue) == false: 3000. Else 1 * maxvalue
  var maxvalue = dial.max || 100;
  var minvalue = dial.min || 0;
  if (dial.min===undefined && dial.type===6){
      minvalue= -maxvalue;
  }
      
  // if units == false: "". Else units
  var units = dial.units || "";
  var offset = dial.offset || 0;
  
  var val = dial.value||0;
  if (isNaN(val)) {val = 0;};
  if (Gauges.animationDelta){
    val += Gauges.animationDelta;
  }
    
  var position = val-offset;
  if (position<minvalue) {position=minvalue;};
  if (position>maxvalue) {position=maxvalue;};
  
  //var width = dial.width||200;
  //var height= dial.height||200;
  
  var width = canvas.width;
  var height= canvas.height;

  var size = 0;
  if (width<height) {
      size = width/2;
  }
  else {
      size = height/2;
  }
  size = size - (size*0.058/2);

  x = width*.5;
  y = height*.55;

  ctx.clearRect(0,0,width,height);
  //ctx.rect(0,0,width,height);
  //ctx.fillStyle = "#FFF"; //default
  //ctx.fill();
  

  if (!position) position = 0;

  var angleOffset = 0;
  var segment = ["#c0e392","#9dc965","#87c03f","#70ac21","#378d42","#046b34"];

  var type = dial.type || 0;

  // Depending on type the start value is calculated.
  // The maximum value is defined via settings and is defined as the value to reach needle's end limit.
  // Depending on dial's type start limit is either 0 or a negative value.
  // Note: Should consider a type which enables defiable min value in futur extension.  

// values for type:
//[0,    "Light <-> dark green, Zero at left"],
//[1,    "Red <-> Green, Zero at center"],
//[2,    "Green <-> Red, Zero at left"],
//[3,    "Green <-> Red, Zero at center"],
//[4,    "Red <-> Green, Zero at left"],
//[5,    "Red <-> Green, Zero at center"],
//[6,    "Green center <-> orange edges, Zero at center "],
//[7,    "Light <-> Dark blue, Zero at left"],
//[8,    "Light blue <-> Red, Zero at mid-left"],
//[9,    "Red <-> Dark Red, Zero at left"],
//[10,   "Black <-> White, Zero at left"],
//[11,   "Blue <-> Red, Zero at upper-left"]
    
  if (type == 0){ // Standard dial from 0 to maxvalue if offset is not set.
    // TODO: seperate between needle position at maximum/minimum and the value displayed.
    // TODO: Do we need to limit the value being displayed? Only needle position should be limited.
    if (position<0)
      position = 0;
  }
  else if (type == 1){
    angleOffset = -0.75;
    segment = ["#e61703","#ff6254","#ffa29a","#70ac21","#378d42","#046b34"];
  }
  else if (type == 2){
    if (position<0)
      position = 0;
    segment = ["#046b34","#378d42","#87c03f","#f8a01b","#f46722","#bf2025"];
  }
  else if (type == 3){
    angleOffset = -0.75;
    segment = ["#046b34","#378d42","#87c03f","#f8a01b","#f46722","#bf2025"];
  }
  else if (type == 4){
    if (position<0)
      position = 0;
    segment = ["#bf2025","#f46722","#f8a01b","#87c03f","#378d42","#046b34"];
  }
  else if (type == 5){
    angleOffset = -0.75;
    segment = ["#bf2025","#f46722","#f8a01b","#87c03f","#378d42","#046b34"];
  }
  else if (type == 6){
    angleOffset = -0.75;
    segment = ["#f46722","#f8a01b","#87c03f","#87c03f","#f8a01b","#f46722"];
  }
  else if (type == 7){
    if (position<0)
      position = 0;
    segment = ["#a7cbe2","#68b7eb","#0d97f3","#0f81d0","#0c6dae","#08578e"];
  }
  else if (type == 8){  //temperature dial blue-red, first segment blue should mean below freezing C
    angleOffset = -0.25;
    segment = ["#b7beff","#ffd9d9","#ffbebe","#ff9c9c","#ff6e6e","#ff3d3d"];
  }
  else if (type == 9){  //temperature dial blue-red, first segment blue should mean below freezing C
    angleOffset = 0;
    segment = ["#e94937","#da4130","#c43626","#ad2b1c","#992113","#86170a"];
  }
  else if (type == 10){ //light: from dark grey to white
    if (position<0) {position = 0;}
    segment = ["#202020","#4D4D4D","#7D7D7D","#EEF0F3","#F7F7F7", "#FFFFFF"];
  }
  else if (type == 11){  //temperature dial blue-red, first 2 segments blue should mean below freezing C
    angleOffset = -0.5;
    segment = ["#0d97f3","#a7cbe2","#ffbebe","#ff8383","#ff6464","#ff3d3d"];
  }
  angleOffset =0;
  
  if (position>maxvalue) position = maxvalue;
  if (position<minvalue) position = minvalue;

  // needle values and their corresponding direction
  // South West (limit start) a = 1.75
  // West: .. ............... a = 1.5
  // North West: ............ a = 1.25
  // North: ................. a = 1
  // North East: ............ a = 0.75
  // East: .................. a = 0.5
  // South East (limit stop)  a = 0.25
  var range = maxvalue-minvalue;
  var posInRange = position - minvalue;
  var needle = 1.75 - ((posInRange/range) * (1.5+angleOffset)) + angleOffset;
  //debug: 
  //canvas.setAttribute("debug",needle);
  
  width = 0.785;
  var c=3*0.785;
  var pos = 0;
  var inner = size * 0.48;

  // Segments
  for (var z in segment){
    ctx.fillStyle = segment[z];
    ctx.beginPath();
    ctx.arc(x,y,size,c+pos,c+pos+width+0.01,false);
    ctx.lineTo(x,y);
    ctx.closePath();
    ctx.fill();
    pos += width;
  }
  pos -= width;
  ctx.lineWidth = (size*0.058).toFixed(0);
  pos += width;
  ctx.strokeStyle = "#fff";
  ctx.beginPath();
  ctx.arc(x,y,size,c,c+pos,false);
  ctx.lineTo(x,y);
  ctx.closePath();
  ctx.stroke();

  //---------------------------------------------------------------

  var color;
  if (dial.color){
    if (typeof dial.color === "function"){
        color = dial.color();
    }
    else {
        color = dial.color;
    }
  };
  //si no se define...
  if (color===undefined){
    color = Gauges.colors.fondo; //default
  }
    
  ////circle & needle
  ctx.fillStyle = color; "#FF0000";//Gauges.colors.fondo; //color fondo;
  ctx.beginPath();
  ctx.arc(x,y,inner,0,Math.PI*2,true);
  ctx.closePath();
  ctx.fill();

  ctx.lineWidth = (size*0.052).toFixed(0);
  
  ctx.beginPath();
  ctx.moveTo(x+Math.sin(Math.PI*needle-0.2)*inner,y+Math.cos(Math.PI*needle-0.2)*inner);
  ctx.lineTo(x+Math.sin(Math.PI*needle)*size,y+Math.cos(Math.PI*needle)*size);
  ctx.lineTo(x+Math.sin(Math.PI*needle+0.2)*inner,y+Math.cos(Math.PI*needle+0.2)*inner);
  ctx.arc(x,y,inner,1-(Math.PI*needle-0.2),1-(Math.PI*needle+5.4),true);
  ctx.closePath();
  ctx.fill();
  ctx.strokeStyle = "#FFF";
  ctx.stroke();
  //---------------------------------------------------------------
  
  var decPlaces=2;
  if (dial.decPlaces===undefined){
    if (val>=10||val<=-10) {
       decPlaces=1;
    }
    else if (val>=100||val<=-100) {
       decPlaces=0;
    }
  }
  else {
      decPlaces = dial.decPlaces;
  }
  val = val.toFixed(decPlaces);
    
  var dialtext = Math.abs(val)+units;
  var textsize = ((size-20) / (dialtext.length+2)) * 2;
  if (textsize>16) {textsize=16;}

  if (Gauges.animationDelta) {
      dialtext="";
  }
  else {
    ctx.fillStyle = "#fff"; //color;//
    ctx.textAlign    = "center";
    ctx.font = "bold "+textsize+"px arial";
    ctx.fillText(val+units,x,y+textsize*.7/2);
    //debug: 
    //ctx.fillText(maxvalue+" "+position  ,x,y+textsize*.7/2);
  }

  ctx.fillStyle = "#000";
  /*if (dial.label){
    var labelsize = (size / (dial.label.length+2))*2;
    ctx.font = labelsize+"px arial";
    ctx.fillText(dial.label,x,height-labelsize-10);  
  }
  */
  
  var spreadAngle = 32;

  var showMinMax = dial.showMinMax===undefined?false:true; //default false
  if (showMinMax){
    ctx.font = "normal "+(size*0.12)+"px arial";

    var posStrt = polar_to_cart(size/1.15, 90+spreadAngle, x, y-size*0.14);
    var posStop = polar_to_cart(size/1.15, 90-spreadAngle, x, y-size*0.14);

    ctx.save();
    ctx.translate(posStrt[0], posStrt[1]);
    ctx.rotate(deg_to_radians(-45));
   
    // graduation text - start limit
    //// Since we've translated the entire context, the coords we want to draw at are now at [0,0]  
    //ctx.fillText(""+round1decimal( (2*angleOffset/3*1.5*maxvalue)+offset )+units, 0, 0);        
    ctx.fillText(""+round1decimal( minvalue+offset )+units, 0, 0);        
    ctx.restore();
    
    ctx.save(); // each ctx.save is only good for one restore, apparently.
    ctx.translate(posStop[0], posStop[1]);
    ctx.rotate(deg_to_radians(45));
    
    // graduation text - end limit
    ctx.fillText(""+round1decimal(offset+maxvalue)+units, 0, 0);  
    ctx.restore();
  }
}
    
function round1decimal(x) {
    Ergebnis = Math.round(x * 10) / 10 ;
    return Ergebnis;
}
  
function round2decimal(x) {
  Ergebnis = Math.round(x * 100) / 100 ;
  return Ergebnis;
}


function deg_to_radians(deg){
  return deg * (Math.PI / 180);
}

function polar_to_cart(mag, ang, xOff, yOff){
  ang = deg_to_radians(ang);
  var x = mag * Math.cos(ang) + xOff;
  var y = mag * Math.sin(ang) + yOff;
  return [x, y];
}

Gauges.animate = function(start, step){
    Gauges.animationStep= step;
    Gauges.animationDelta = start;
    Gauges.animationSpeed = 10;
    Gauges.drawAll();
    setTimeout(Gauges.animateStep,Gauges.animationSpeed);
};

Gauges.animateStep = function(){
    Gauges.animationDelta+=Gauges.animationStep;
    Gauges.drawAll();
    if (Gauges.animationDelta===0){
        return;
    }
    Gauges.animationSpeed+=1;
    setTimeout(Gauges.animateStep,Gauges.animationSpeed);
};

