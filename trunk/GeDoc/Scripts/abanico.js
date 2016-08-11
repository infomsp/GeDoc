//"use strict";
//
// class Abanico
//---- new ---------
function Abanico(selectDOMElem) {

    var parent = selectDOMElem.form.parentElement; // el parent del form

    var abanicoInx = Abanico.list.length;

    /*	var asHidden = '<input type=hidden name="' + selectDOMElem.name + '"'
     + ' id="' + selectDOMElem.name + '-abanico-hidden-input"'
     + ' value=' + selectDOMElem.selectedIndex + '>';
     */

    var actual=$(selectDOMElem).removeClass("abanico");

    var asDiv = '<div class="abanico-group '+actual.attr("class")+'" id=abanico-' + abanicoInx
         + ' style="width:' + (selectDOMElem.clientWidth||selectDOMElem.parentElement.clientWidth||110) + 'px">';

    $.each(selectDOMElem, function(inx, item) { //para cada option
        var extra = "";
        if (inx === selectDOMElem.selectedIndex) {
            extra = ' tabindex=0 style="z-index:10"';
        }
        asDiv += '<div class=abanico-opt ' + Abanico.elemEvents
             + extra + ' data-index=' + inx + ' value="' + item.value + '" >' + item.innerText + '</div>';
    });

    var new_sel = $(asDiv); // create new div, to replace old <select>

    //le saco la clase, lo oculto
    actual.css("display", "none").after(new_sel);
    //inserto luego el nuevo div
    //$(parent).append(new_sel);
    //$(parent).append(asHidden);

    //connect child-parent (parent is DOM Element)
    this.sel = new_sel[0];
    this.theHidden = selectDOMElem;
    Abanico.list.push(this);
}
;


//shared
Abanico.list = []; //lista de instancias creadas;

Abanico.elemEvents='onkeydown=Abanico.keydown(event,this)'
			+' onkeypress=Abanico.keypress(event,this)'
			+' onmouseover=Abanico.mouseOver(this)'
			+' onmouseout=Abanico.mouseOut(this)'
			+' onclick=Abanico.click(this)';

//shared fn
Abanico.convertAll = function() {
	// si ya esta convertido, toma el valor actual (para que tome bien form.reset)
	$(".abanico-group").each(function(inx, sel) {
		Abanico.selectItem(sel.children[Abanico.getSelectedIndex(sel)]);
	});
	// si es la 1ra vez, los convierte
	$("select.abanico").each(function() {
		new Abanico(this);
	});

};

//methods
Abanico.prototype.clearTimer = function() {
	if (this.timer) {
		//console.log('Clear timer ', this.id);
		clearTimeout(this.timer);
		this.timer = null;
	}
};

Abanico.open = function(sel) {

        //console.log('open');

        var selIndex = Abanico.getSelectedIndex(sel);

        if (!selIndex || selIndex===-1) selIndex=0;
        var item = sel.children[selIndex];
        var size = {w: item.clientWidth, h: item.clientHeight};

        var cant = sel.children.length;

        var angle = Math.PI / 2 - Math.PI / 8;// - (cant/2)*anglestep; // -anglestep * (cant-1)/2;
        var anglestep = -2 * angle / Math.min(cant - 1, 8);

        var dist_x = size.w + 8;
        var dy = size.h * 1.2;
        //var dist_y = dy * selIndex;//Math.floor(cant/2);
        //var offset = $(item).offset();

        $(sel).append(
             '<div class=abanico-ghost>' + item.innerHTML + '</div>');

        var y = -dy * (cant / 2 - 1) - size.h / 2;

        $(sel).css("z-index",11);

        $(sel).children(".abanico-opt")

            .each(function(inx, itemElem) {

                itemElem.animating = 1;

                var item = $(itemElem);
                item.addClass("abanico-opened").css("z-index",11)
                    .css("left",0).css("top", 0).css("opacity",1)
                    .attr("tabindex", "0");

                x = dist_x * Math.cos(angle);
                var animate_opt = {"top": y, "left": x};
                //sel.abanico.centers.push( {x: offset.left + x + size.w/2,   y:  offset.top + y + size.h / 2});
                item.animate(animate_opt, {
                    duration: 260
                         /*, step: function(now, fx) {
                          if (fx.prop === 'marginTop') {
                          pos = $(fx.elem).offset();
                          fx.elem.abanico.line.setTo(pos.left + 4, pos.top + fx.elem.clientHeight / 2);
                          //var itemIndex = fx.elem.getAttribute('data-index');
                          //sel.lines[itemIndex].setTo(pos.left+6, pos.top+fx.elem.clientHeight/2);
                          }
                          }*/
                         , done: function() {
                        this.animating = 0;
                        //for (var n = 1; n < sel.abanico.centers.length; n++) {
                        //	sel.abanico.connectors.addLine(sel.abanico.centers[n], sel.abanico.centers[n - 1]);
                        //}
                    }
                });
                y += dy;
                angle += anglestep;
            });

    sel.abanicoOpened = true;
    Abanico.selOpened=sel;

};


//-----------------------------
Abanico.getInstance = function(sel) {
	return Abanico.list[sel.id.split("-").slice(-1)]; // el DIV tiene id=abanico-n
};

//-----------------------------
Abanico.getSelectedIndex = function(sel) {
	return Abanico.getInstance(sel).theHidden.selectedIndex;
};

//-----------------------------
Abanico.setSelectedIndex = function(sel, newIndex) {
	Abanico.getInstance(sel).theHidden.selectedIndex = newIndex;
};

//-----------------------------
Abanico.selectAndclose = function(itemElem) {

	//console.log('closing ');
	//sel.abanico.connectors.clear(); //clear lines

	var sel = itemElem.parentElement;
	var abanico = Abanico.getInstance(sel);
	abanico.clearTimer();
	var newIndex = parseInt(itemElem.getAttribute('data-index'));
	var oldSelected = parseInt(Abanico.getSelectedIndex(sel));

	Abanico.selectItem(itemElem);

	$(itemElem).animate({"left": 0, "top": 0}, 300);

	$.each(sel.children, function(inx, item) {
		if (inx !== newIndex) {
			item.animating = 1;
			var animate_opt = {"opacity": 0};
			if (inx !== oldSelected)
				animate_opt["left"] = "+=40";
			$(item).removeClass("abanico-opened")
				 .animate(animate_opt, 400, function() {
				this.animating = 0;
				$(this).css("left",0).css("right",0);
			});
		}
	});//end each

	Abanico.cleanAfterClose(sel);

};

//--------------------------
//shared fns (no instance)
//--------------------------
Abanico.cleanAfterClose = function(sel) {
    var abanico = Abanico.getInstance(sel);
    abanico.clearTimer();
    sel.abanicoOpened = false;
    Abanico.selOpened = undefined;
    var selIndex = Abanico.getSelectedIndex(sel);
    $(sel).css("z-index",10);
    $(sel).children(".abanico-opt").attr("tabindex", "").eq(selIndex).attr("tabindex", "0");
    $(sel).children(".abanico-ghost").remove();
};


Abanico.close = function(sel) {
    clearTimeout(Abanico.timer);
    Abanico.timer=undefined;
    Abanico.timerClose(sel);
};

Abanico.timerClose = function(sel) {
	var oldSelected = parseInt(Abanico.getSelectedIndex(sel));
	var abanico = Abanico.getInstance(sel);
	$(sel).children(".abanico-opt").removeClass("abanico-opened").css("z-index", "9").eq(oldSelected).css("z-index", "10");
	$.each(sel.children, function(inx, item) {
		abanico.animating++;
		var animate_opt = {"left": 0, "top": 0};
		$(item).animate(animate_opt, 400, function() {
			abanico.animating--;
		});
	});//end each

	Abanico.cleanAfterClose(sel);
};

//-----------------------------
Abanico.selectItem = function(itemElem) {

	var sel = itemElem.parentElement;

	var inx = parseInt(itemElem.getAttribute("data-index"));

	$(sel).children(".abanico-opt").css("z-index", "9");
	$(itemElem).css("z-index", "10");

	Abanico.setSelectedIndex(sel, inx);
};

//-----------------------------
Abanico.click = function(item) {
	if (item.animating)
		return false;
	var sel = item.parentElement;
	if (!sel.abanicoOpened){
        if (Abanico.selOpened) Abanico.close(Abanico.selOpened);
		Abanico.open(sel);
        return;
	};

	//actual selection
	var selIndex = Abanico.getSelectedIndex(sel);
	if (selIndex>=0) sel.children[selIndex].focus();
	// new selected
	var newSel = item.getAttribute('data-index') || selIndex;

	if (newSel === selIndex)
		Abanico.timerClose(sel);
	else {
		Abanico.selectAndclose(item);
	}

};

Abanico.mouseOut = function(item) {
	$(item).removeClass("abanico-hover");
	var sel = item.parentElement;
	if (!sel) return; //a veces es el ghost que ya fue removido (sin parent)
    var abanico = Abanico.getInstance(sel);
	abanico.clearTimer();
	abanico.timer = setTimeout(function() {
		Abanico.timerClose(sel);
	}, 1000);
	//console.log('set close timer',sel.abanico.timer);
};

Abanico.mouseOver = function(item) {
	var sel = item.parentElement;
	var abanico = Abanico.getInstance(sel);
	abanico.clearTimer();
	if (item.animating) return;
	$(item).addClass("abanico-hover");
	//console.log('mouseOver',item.innerText,"animating",sel.abanico?sel.abanico.animating:-1);
	/*	$(sel).children(".abanico-opt").css("background", "").css("z-index", "").css("left", "");;
	 item.style["z-index"] = 10;
	 item.style["background"] = "rgba(255,155,0,.6)";
	 item.style["left"] = parseInt(item.style["left"])+4;
	 */
	if (!sel.abanicoOpened);
		//Abanico.open(sel);
};

Abanico.keydown = function(e, item) {
	var sel = item.parentElement;
	switch (e.keyCode) {
		case 13: //enter
			if (sel.abanicoOpened)
				Abanico.click(item);
			else
				Abanico.open(sel);

			break;

		case 40: //down
			$(item).next().focus();
			e.preventDefault();
			break;

		case 38: //up
			$(item).prev().focus();
			e.preventDefault();
			break;

		default:
	}

};

Abanico.keypress = function(e, item) {
	var sel = item.parentElement;
	var charCode = typeof e.which === "number" ? e.which : e.keyCode;
	if (charCode && charCode > 32) {
		e.preventDefault();
		$item = $(item);
		var start = parseInt(item.getAttribute("data-index") || "0");
		var inx = start + 1;
		while (true) {
			if (inx === sel.children.length - 1)
				inx = 0; //last is ghost
			if (inx === start)
				break;
			if (sel.children[inx].innerText.startsWith(String.fromCharCode(charCode))) {
				sel.children[inx].focus();
				return;
			}
			inx++;
		}
	}
};
