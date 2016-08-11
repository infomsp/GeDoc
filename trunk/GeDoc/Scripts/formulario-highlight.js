/**
 * jQuery.fieldHighlight - Highlight active form fields
 * David Ackerman, http://www.kryogenix.org/
 * Date: January 24th, 2011
 * @author David Ackerman
 * @version 1.0
 *
 * Basic calling syntax: $("form").fieldHighlight();
 * Defaults to highlighting all inputs on :focus with a red border
 *
 * You can also pass an options object with the following keys:
 *   borderColor: "color"
 *      sets the color of the active fields border. You can pass in a hex value here as well, eg. "#CECECE"
 *   borderStyle : "style"
 *		css property of the border element, http://www.w3schools.com/css/pr_border-style.asp
 *	 borderThickness : "3px|em|..."
 *		string value for border thickness -- add your own value of measurement
 *	 borderClass : ""
 *		attach an additional class you would like to add to the field on :focus
 *  
 */

(function ( $ ) {
	$.fn.fieldHighlight = function(options) {

	// default settings color, and border thickness
	// borderClass defaults to empty, check for this later
	var settings = {
		"borderColor" : "blue",
		"borderStyle" : "solid",
		"borderThickness" : "4px",
		"borderClass": ""
	};
	
	// if we have supplied options, use them
	return this.each(function() {
		if(options) {
			$.extend(settings, options);
		}

		// go through the form and find each input to be highlighted.
		// $(this) is referring to the entire <form> itself
		var inputs = $(this).find("input, select, textarea");

		// on focus, highlight them using settings defined, 
		// $(this) referring to the selected input
		// if they define borderClass, make that take priority
		inputs.focus(function() {
				$(this).addClass(settings.borderClass);
				$(this).css("border", settings.borderThickness + " " + settings.borderStyle + " " + settings.borderColor);
				//$(this).animate({ borderThickness : settings.borderThickness, borderColor: settings.borderColor }, "fast")
		})
		// on blur, remove the border
		.blur(function() {
			$(this).removeClass(settings.borderClass);
			$(this).css("border", "");
		});
		
		

		
	
	});
};

}) ( jQuery );