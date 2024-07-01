var mvcParamMatch = (function () {
    var MvcParameterAdaptive = {};
    //
    MvcParameterAdaptive.isArray = Function.isArray || function (o) {
        return typeof o === "object" &&
                Object.prototype.toString.call(o) === "[object Array]";
    };

    //
    MvcParameterAdaptive.convertArrayToObject = function (arrName, array, saveOjb) {
        var obj = saveOjb || {};

        function func(name, arr) {
            for (var i in arr) {
                if (!MvcParameterAdaptive.isArray(arr[i]) && typeof arr[i] === "object") {
                    for (var j in arr[i]) {
                        if (MvcParameterAdaptive.isArray(arr[i][j])) {
                            func(name + "[" + i + "]." + j, arr[i][j]);
                        } else if (typeof arr[i][j] === "object") {
                            MvcParameterAdaptive.convertObject(name + "[" + i + "]." + j + ".", arr[i][j], obj);
                        } else {
                            obj[name + "[" + i + "]." + j] = arr[i][j];
                        }
                    }
                } else {
                    obj[name + "[" + i + "]"] = arr[i];
                }
            }
        }

        func(arrName, array);

        return obj;
    };

    //
    MvcParameterAdaptive.convertObject = function (objName, turnObj, saveOjb) {
        var obj = saveOjb || {};

        function func(name, tobj) {
            for (var i in tobj) {
                if (MvcParameterAdaptive.isArray(tobj[i])) {
                    MvcParameterAdaptive.convertArrayToObject(i, tobj[i], obj);
                } else if (typeof tobj[i] === "object") {
                    func(name + i + ".", tobj[i]);
                } else {
                    obj[name + i] = tobj[i];
                }
            }
        }

        func(objName, turnObj);
        return obj;
    };

    return function (json, arrName) {
        arrName = arrName || "";
        if (typeof json !== "object") throw new Error("");
        if (MvcParameterAdaptive.isArray(json) && !arrName) throw new Error("");

        if (MvcParameterAdaptive.isArray(json)) {
            return MvcParameterAdaptive.convertArrayToObject(arrName, json);
        }
        return MvcParameterAdaptive.convertObject("", json);
    };
})();
(function () {
	
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "10000",//—›—À   2014/09/12   ---3000---
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    SMAT.Service = 
    {
        getReferValue: function (value, text,paramCD) {

            window.parent.SMAT.returnValue = {
                value: value,
                text: text,
                paramCD: paramCD
            };

            
            $("#btn_" + window.parent.SMAT.CurrentCodeName.config.field, parent.document).click();
        }
        ,showTempWindow : function (url){
            var cw = window.screen.width - 130;
            var ch = window.screen.height - 180;
            window.open(url, 'newwindow', 'height=1, width=10, top='+ch+', left='+cw+', toolbar=no, menubar=no, scrollbars=no,resizable=yes,location=no, status=no');

        }
        ///
        ///type: success(default) , info , error , warning
        ///
        , toastr: function (msg, type) {
            $('.toast').hide();
            toastr.clear();
            var toastrType = "success";
            if (type != undefined) {
                toastrType = type;
            }
            toastr[toastrType](msg);
        }
        , toastrTarget: function (target, msg, type) {
            var t_top = target.offset().top;
            var t_left = target.offset().left;
            var t_boxHeight = target.outerHeight();
            var t_boxWidth = target.outerWidth();

            //alert(t_top + "_" + t_left + "_" + t_boxHeight + "_" + t_boxWidth)

            $('.toast').hide();
            toastr.clear();
            var toastrType = "success";
            if (type != undefined) {
                toastrType = type;
            }
            toastr[toastrType](msg);

            var boxW = $('.toast').outerWidth();

            $('.toast').parent().css("margin-left", "0");
            $('.toast').parent().css("top", (t_top - t_boxHeight - 30 - $(document).scrollTop()) + "px");
            $('.toast').parent().css("left", (t_left - ((boxW - t_boxWidth) / 2)) + "px");
        }
        , checkAction: function (target) {

            if ($(target).attr('checkAction') != undefined) {
                var mothedName = $(target).attr('checkAction');

                //eval_r(funName);
                return eval(mothedName);

            }

            return true;
        },doJsonURLNormal: function (config){	
            var url = config.url;
            var params = config.params;
            var success = config.success;
            var error = config.error;
            var async = config.async;
			
            var timeId = parseInt(new Date().valueOf() / 1000);

            if (url.indexOf("?") >= 0) {
                url = url + '&tid=' + timeId;
            } else {
                url = url + '?tid=' + timeId;
            }

            $.ajax({  
                url:url,
                type:"POST",  
                data: mvcParamMatch(params),
                dataType:"json",
                async:async,
                success: function (result) {

                    
                    if(success !=null && success!=undefined){
                        success(result);
                    }
				
                },error: function(XMLHttpRequest, textStatus, errorThrown){  
                    var ss = XMLHttpRequest;
                }
				
            });
			
        }, getServerFile: function (config) {
            var url = config.url;
            var params = config.params;
            var success = config.success;
            var type = config.type;
            debugger;
            if (window.smat) {
                smat.service.openLoding();
            }

            SMAT.Service.doJsonURLNormal({
                url: url,
                params: params,
                success: function (result) {
                    if (result.ResultType == 0) {
                        SMAT.Service.toastr(result.Message,"info");
                    } else
                    {
                        $('body').append($('<iframe id="iframeDownload" style="width:1px;height:1px;display: none;" src="' + result.Path + '">'));
                    }
                    if (window.smat) {
                        smat.service.closeLoding();
                    }
                }
            });
        }
    }
    
    $.fn.ui = function () {
        var uuid = $(this).attr('uuid');
        if (SMAT.uiMap.contains(uuid)) {
            return SMAT.uiMap.get(uuid);
        } else {
            return null;
        }
    };

})();