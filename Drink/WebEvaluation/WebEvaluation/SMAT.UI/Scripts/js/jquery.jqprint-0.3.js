﻿
(function($) {
    var opt;

    $.fn.jqprint = function (options) {
        opt = $.extend({}, $.fn.jqprint.defaults, options);

        var $element = (this instanceof jQuery) ? this.clone() : $(this).clone();

        $element.css("margin", "0").css("padding", "0").css("border", "none").css("overflow", "hidden");
        $element.children(".panel").css("margin", "0").css("padding", "0").css("border", "none");

        var $iframe = $("<iframe style='min-width:860px; width:100%;max-height: 1160px;height: 0;border: none;position: absolute;'/>");
        

        //if (!opt.debug) { $iframe.css({ position: "absolute", width: "0px", height: "0px", left: "-600px", top: "-600px" }); }

        $iframe.appendTo("body");
        var doc = $iframe[0].contentWindow.document;
        
        if (opt.importCSS)
        {
            if ($("link[media=print]").length > 0) 
            {
                $("link[media=print]").each( function() {
                    doc.write("<link type='text/css' rel='stylesheet' href='" + $(this).attr("href") + "' media='print' />");
                });
            }
            else 
            {
                $("link").each( function() {
                    doc.write("<link type='text/css' rel='stylesheet' href='" + $(this).attr("href") + "' />");
                });
            }
        }

        if (opt.printContainer) { doc.write($element.outer()); }
        else { $element.each( function() { doc.write($(this).html()); }); }
        
        //panel-body width:100%
        //$(doc).find(".panel-body").css("width", "100%");


        doc.close();
        ($iframe[0].contentWindow).focus();

        setTimeout( function() { ($iframe[0].contentWindow).print(); }, 1000);
    }
    
    $.fn.jqprint.defaults = {
        debug: true,
		importCSS: true, 
		printContainer: true,
		operaSupport: false
	};

    // Thanks to 9__, found at http://users.livejournal.com/9__/380664.html
    jQuery.fn.outer = function() {
      return $($('<div></div>').html(this.clone())).html();
    } 
})(jQuery);