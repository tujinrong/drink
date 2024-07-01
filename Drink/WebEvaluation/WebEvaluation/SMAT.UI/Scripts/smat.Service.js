(function () {

    smat.service = {


        load: function (config) {
            var url = config.url;
            var target = config.target;
            var params = config.params;
            var success = config.success;

            var timeId = parseInt(new Date().valueOf() / 1000);

            if (url.indexOf("?") >= 0) {
                url = url + '&tid=' + timeId + "&sid=" + smat.global.sid;
            } else {
                url = url + '?tid=' + timeId + "&sid=" + smat.global.sid;
            }

            if (params != null && params != undefined) {
                for (var key in params) {
                    url = url + '&' + key + "=" + params[key];
                }
            }

            smat.service.beginLoding($(target));

            $(target).load(url, function (response, status, XMLHttpRequest) {

                smat.service.setLastSessionTime();
                //window.location.hash=timeId;
                if (status == "error") {
                    smat.service.handleRequestError(XMLHttpRequest);
                } else {
                    if (success != undefined && success != null) {
                        success(response);
                    }
                }

            });

        },


        loadJosnData: function (config) {
            var url = config.url;
            var params = config.params;
            var success = config.success;
            var error = config.error;
            var async = config.async;

            if (params == undefined) params = {}

            var timeId = parseInt(new Date().valueOf() / 1000);
            if (url.indexOf("?") >= 0) {
                url = url + '&tid=' + timeId + "&sid=" + smat.global.sid;
            } else {
                url = url + '?tid=' + timeId + "&sid=" + smat.global.sid;
            }
            //smat.service.openLoding();

            //localServer:
            if (smat.global.localServer == true) {
                var uid = smat.service.uuid();
                smat.global.callCSharpMap.set(uid, { success: success });

                if (async != false) {
                    async = true;
                    window.LoadJsonData(url, JSON.stringify(params).replace(new RegExp("\"null\"", "gm"), "\"\""), uid, async);
                } else {
                    var r = window.LoadJsonData(url, JSON.stringify(params).replace(new RegExp("\"null\"", "gm"), "\"\""), uid, async);
                    eval("smat.service.localCallBack(uid," + r + ")");
                }

            } else {

                $.ajax({
                    url: smat.global.basePath + url,
                    type: "POST",
                    //data: JSON.stringify(params).replace(new RegExp("\"null\"", "gm"), "\"\""),
                    data: JSON.stringify(params),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: async,
                    //crossDomain:true,
                    success: function (result) {
                        smat.service.setLastSessionTime();
                        if (config.closeLoding != false) {
                            smat.service.closeLoding();
                        }

                        if (result && smat.service.isEmpty(result.errorInfo) == false) {
                            smat.service.isNoError();
                            return false;
                        }
                        if (success != null && success != undefined) {
                            success(result);
                        }

                    }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        smat.service.setLastSessionTime();
                        if (error) {
                            error(XMLHttpRequest, textStatus, errorThrown);
                        }
                        if (!config.noHandleRequestError) {
                            smat.service.handleRequestError(XMLHttpRequest);
                        }
                    }

                });


                //$.jsonp({
                //    url: smat.global.basePath + url,
                //    data: params,
                //    callbackParameter: "callback",
                //    success: function (result, textStatus, xOptions) {
                //        debugger
                //    },
                //    error: function (xOptions, textStatus) {
                //        debugger
                //    }
                //});

            }
        },

        localCallBack: function (uid, resultStr) {

            var info = smat.global.callCSharpMap.get(uid)

            smat.service.closeLoding();

            if (info.success != null && info.success != undefined) {
                info.success(resultStr);
            }

            smat.global.callCSharpMap.remove(uid);
            //window.EndAsync(uid)
        }, getPageView: function (config) {
            smat.service.loadJosnData(config);
        }
        ,
        doDownLoad: function (path) {
            //$('body').append('<iframe frameborder="0" width="0" height="0" style="display:none;" src="' + smat.global.basePath +"/downLoad.jsp?path=" +path+'"></iframe>"');
        },


        beginLoding: function (target) {
            $(target).children().remove();
            $(target).append('<div class="loadingBox"><img src="' + smat.global.basePath + '/SMAT.UI/Styles/Material/loading-image.gif" /></div>');
        }
		,
        endLoding: function (target) {
            $(target).find(".loadingBox").remove();
        },
        refer: function (config) {

            var title = config.title;
            var referInfo = config.referInfo;
            var param = config.param;
            var afterSelected = config.afterSelected;
            var referKey = config.referKey;

            if (referInfo == undefined && smat.global.referInfo[referKey] != undefined) {
                referInfo = smat.global.referInfo[referKey];
            }

            if (title == undefined) {
                title = referInfo.title;
            }

            var checkOpenFunction = config.checkOpenFunction;

            if (checkOpenFunction != undefined && checkOpenFunction() != true) {
                return;
            }

            if (referInfo.pageName) {
                //DyReferPage
                smat.service.openPage({
                    title: title,
                    page: {
                        projID: referInfo.projID,
                        entityName: referInfo.entityName,
                        pageName: referInfo.pageName,
                        parentPageId: config.parentPageId
                    },
                    params: param,
                    afterClose: function (result) {
                        if (result != undefined &&
                                afterSelected != undefined &&
                                result.selectedRows != undefined &&
                                result.selectedRows != null) {
                            afterSelected(result);
                        }
                    },
                    width: referInfo.width,
                    height: referInfo.height,
                    modal: true
                });

            } else {
                smat.service.openForm(
			    {
			        title: title,
			        url: referInfo.async.openFormUrl,
			        params: param,
			        afterClose: function (result) {
			            if (result != undefined &&
                                afterSelected != undefined &&
                                result.selectedRows != undefined &&
                                result.selectedRows != null) {
			                afterSelected(result);
			            }
			        },
			        width: referInfo.width,
			        height: referInfo.height,
			        modal: true
			    });
            }
        },
        openPage: function (config) {
            smat.service.openForm(config)
        }
        ,
        openForm: function (config) {

            var title = config.title;
            var url = config.url;
            var params = config.params;
            var afterClose = config.afterClose;
            var i_width = config.width;
            var i_modal = config.modal;
            var i_maximize = config.maximize;
            var showCloseBtn = config.modal;

            var afterOpen = config.afterOpen;

            var fill = config.fill;
            var fillTarget = config.fillTarget;
            var closeBtn = undefined;
            var windowClass = config.windowClass;

            var pointTarget = config.pointTarget;
            var pointLocation = config.pointLocation;

            var page = config.page;

            var iframe = undefined;
            if (config.contentDom == undefined) {
                if (Modernizr.ie9 == true) {
                    iframe = true;
                }
            }

            if (config.iframe) {
                iframe = config.iframe;
            }
            //iframe = true;

            var timeId = parseInt(new Date().valueOf() / 1000);
            if (url != undefined) {
                if (url.indexOf("?") >= 0) {
                    url = url + '&tid=' + timeId + "&sid=" + smat.global.sid;
                } else {
                    url = url + '?tid=' + timeId + "&sid=" + smat.global.sid;
                }

                smat.service.setLastSessionTime();
            }


            if (params != null && params != undefined && url != undefined) {
                for (var key in params) {
                    url = url + '&' + key + "=" + params[key];
                }
            }

            if (page != undefined) {
                if (page.templateUrl != undefined) {
                    url = page.templateUrl + "/" + page.projID + "/" + page.entityName + "/" + page.pageName + '?tid=' + timeId + "&sid=" + smat.global.sid;
                } else {
                    var pageBox = $("<div style='width:100%;height:100%'></div>");
                    smat.dynamics.openPage({
                        projID: page.projID,
                        formName: page.pageName,
                        entityName: page.entityName,
                        contextOn: pageBox,
                        parentPageId: page.parentPageId
                    });
                    config.contentDom = pageBox;
                    iframe = true;
                }

            }

            $('body').append("<div id='MainSubForm_" + timeId + "' style='display: none;'><div class='loadingBox' style='margin-top:10%; text-align: center;'><img class='s-image' src='" + smat.global.basePath + "/SMAT.UI/Styles/Material/loading-image.gif'></div></div>");

            var width = "615px";
            if (i_width != null && i_width != undefined) {
                width = i_width;
            }
            var widthNum = Number(width.replace("px", ""));

            var modal = true;

            if (config.modal != undefined) {
                modal = config.modal;
            }
            if (pointTarget != undefined) {
                modal = false;
            }

            var maximize = false;
            if (i_maximize != null && i_maximize != undefined) {
                maximize = i_maximize;
            }

            var smatWindow = $("#MainSubForm_" + timeId);

            var onClose = function () {

                if (closeBtn != undefined) {
                    $("#" + closeBtn + "_close").remove();
                }
                if (fillTarget != undefined) {
                    $('.form_bg_temp').remove();
                }

                var pLength = $("#" + fillTarget).children(".s-window").length;
                if (pLength > 1) {
                    $("#" + fillTarget).children(".s-window:eq(" + (pLength - 2) + ")").css("opacity", 1);
                }

                if (afterClose != null && afterClose != undefined) {
                    afterClose(smatWindow.data("asmatWindow").options.actionResult);
                    if (smatWindow.data("asmatWindow").options.actionResult == undefined) {
                        if (smat.Guide != undefined && smat.Guide.currentGuide != undefined) {
                            if ($(smatWindow.data("asmatWindow").element).find(smat.Guide.currentStep.formId).length > 0) {
                                smat.Guide.moveBack();
                            }
                        }
                    }
                }
                try {
                    var uis = smatWindow.find('[uuid]');
                    $.each(uis, function () {
                        if ($(this).ui()) {
                            $(this).ui().destroy();
                        }

                    });

                    smatWindow.data("asmatWindow").destroy();
                }
                catch (err) {

                    //在此处理错误
                    $(".s-overlay").remove();
                }

                smatWindow.remove();

            }
            var onActivate = function () {

                $('.sm-touch-scrollbar').hide();

                if (config.contentDom != undefined) {
                    smatWindow.children().remove();
                    config.contentDom.appendTo(smatWindow);
                }

                if (afterOpen != null && afterOpen != undefined) {
                    afterOpen();
                }

            }

            if (fillTarget != undefined) {
                $('#' + fillTarget).css('position', 'relative');
                var content = "SubForm_" + timeId;
                $('#' + fillTarget).append("<div class='form_bg_temp s-content' style='position: absolute;width: 100%;height: 100%;top: 0;left: 0;'></div>");

            } else {
                smatWindow.addClass('s-popup-page');
            }

            var top = 30;
            var left = 30;

            var onOpen = function () {

                if (fillTarget != undefined) {
                    $("#" + fillTarget).css('position', 'relative');
                    if (showCloseBtn != undefined) {
                        closeBtn = "SubForm_" + timeId;
                        $('#' + fillTarget).append('<button type="button" class="s-button s-button" id="' + closeBtn + '_close" style="position: absolute;z-index: 1000000;top:10px;right:10px;">返回</button>');

                        $('#' + closeBtn + '_close').bind('click', function (e) {

                            smatWindow.data("asmatWindow").close();
                        });
                    }

                    smatWindow.parent().css("top", "0");
                    smatWindow.parent().css("left", "0");
                    smatWindow.parent().css("padding", "0");
                    smatWindow.parent().css("border", "none");
                    smatWindow.parent().find('.s-window-titlebar').hide();
                    if (windowClass) {
                        smatWindow.parent().find('.s-window-content').addClass(windowClass);
                    }
                    smatWindow.parent().find('.s-resize-handle').hide();
                    smatWindow.parent().css("-webkit-box-shadow", "none");
                    smatWindow.parent().css("box-shadow", "none");
                    smatWindow.parent().css('z-index', 1);

                    var pLength = $("#" + fillTarget).children(".s-window").length;
                    if (pLength > 1) {
                        $("#" + fillTarget).children(".s-window:eq(" + (pLength-2) + ")").css("opacity", 0);
                    }

                } else if (pointTarget != undefined) {
                    //                	var t_top = pointTarget.offset().top;
                    //    				var t_left = pointTarget.offset().left;
                    //    				var t_boxHeight = pointTarget.outerHeight();
                    //    				var t_boxWidth = pointTarget.outerWidth();
                    //    				var boxH = menuWindowParent.outerHeight();
                    //    				var boxW = menuWindowParent.outerWidth();
                } else {

                    smatWindow.data("asmatWindow").center();
                    var topValue = "10%";

                    var wh = $(window).height();
                    var ph = smatWindow.parent().parent().height();
                    var ps = smatWindow.parent().parent().scrollTop();
                    if (ps > 10 && (ph - wh) > 20) {
                        topValue = (ps + (wh/10))+"px";
                    }

                    if (config.top != undefined) {
                        topValue = config.top;
                    }
                    smatWindow.parent().css("top", topValue);
                    smatWindow.parent().css("left", (document.body.clientWidth - widthNum) / 2 + "px");
                }

                if (maximize) {
                    smatWindow.data("asmatWindow").maximize();
                    smatWindow.parent().css('z-index', 999999999999);
                }

                //                $("body [id]").each(function(){
                //                    var ids = $(this).attr("id");
                //                     if( $("body [id="+ids+"]").length >= 2 ){
                //                                    alert("id为"+ids+" 的重复了。");
                //                   }
                //               });


            }



            var onDragEnd = function () {
                if (smat.Guide != undefined && smat.Guide.currentGuide != undefined) {
                    if ($(window.data("asmatWindow").element).find(smat.Guide.currentStep.formId).length > 0) {
                        smat.Guide.resetPosition(smat.Guide.currentStep);
                    }

                }
            }

            var height = undefined;
            if (config.height != undefined) {
                height = config.height;
            }

            var draggable = true;
            if (fillTarget != undefined) {
                draggable = false;
            }

            var wConfig = {
                width: width,
                height: height,
                autoFocus: false,
                animation: false,
                draggable: draggable,
                title: title,
                content: {
                    url: url,
                    type: "GET",
                    contentType: "charset=utf-8"
                },
                close: onClose,
                open: onOpen,
                refresh: function () {
                    if (smatWindow.html().indexOf('asmat-plase-go-to-login-form') > 0) {
                        smatWindow.hide();
                        window.location.href = "/Home/Login";
                    }
                },
                iframe: iframe,
                activate: onActivate,
                formParam: params,
                dragend: onDragEnd,
                modal: modal,
                error: function (e) {
                    smatWindow.data('asmatWindow').close();
                    smat.service.handleRequestError(e.xhr, true);
                },
                position: {
                    top: top,
                    left: left
                },
                visible: false
            };

            if (fillTarget != undefined) {
                wConfig.appendTo = "#" + fillTarget;
                wConfig.modal = false;
                wConfig.width = "100%";
                wConfig.height = "100%";
            }
            if (config.url == undefined && (page == undefined)) {
                wConfig.content = undefined;
            }

            if (config.height != undefined) {
                wConfig.height = config.height;
            }

            if (fillTarget != undefined) {
                //setTimeout(function () { smatWindow.asmatWindow(wConfig).data("asmatWindow").open(); }, 1);
                smatWindow.asmatWindow(wConfig).data("asmatWindow").open();
            } else {
                smatWindow.asmatWindow(wConfig).data("asmatWindow").open();
            }

            smat.service.noticeClear();
            if (config.m_opacity != undefined) {
                $('.s-overlay').css('opacity', config.m_opacity);
            }


            smatWindow.parent().bind("DOMSubtreeModified", function (e) {
                if (smat.Guide != undefined && smat.Guide.currentGuide != undefined && smatWindow.data("asmatWindow")) {
                    if ($(smatWindow.data("asmatWindow").element).find(smat.Guide.currentStep.formId).length > 0) {
                        if (smatWindow.parent().attr('old-index') != smatWindow.parent().css('z-index')) {
                            var index = Number(smatWindow.parent().css('z-index'))
                            smatWindow.parent().attr('old-index', index);
                            smat.Guide.StepWindowDom.parent().css('z-index', index + 1);
                        }
                    }
                }
            });
        },
        closeForm: function (config) {
            var contentId = config.contentId;
            var result = config.result;
            var windowBox = config.windowBox;
            if (windowBox == undefined) {
                windowBox = $("#" + contentId).closest('.s-window-content');
            }
            if (windowBox.length == 0) {
                windowBox = $(".s-content-frame", parent.document).closest('.s-window-content');

                config.contentId = windowBox.attr('id');
                parent.smat.service.closeForm(config);
            } else {
                var smatWindow = windowBox.data("asmatWindow");

                if (smatWindow != null) {
                    //smat.service.noticeClear();
                    if (result != null && result != undefined) {
                        smatWindow.options.actionResult = result;
                    }

                    smatWindow.close();
                }
            }

        },
        getFormParam: function (contentId) {

            var smatWindow = $("#" + contentId).closest('.s-window-content').data("asmatWindow");

            if (smatWindow != null) {

                return smatWindow.options.formParam;

            }
            //return {};
        },
        enableFormValue: function (formId, enable) {
            if ($("#" + formId).length > 0) {
                //
                var ctls = $("#" + formId).find(".s-input");
                $.each(ctls, function (n, value) {
                    if ($(this).attr("name") != undefined && $(this).attr("name").length > 0) {
                        if ($(this).data("asmatDropDownList")) {
                            $(this).data("asmatDropDownList").enable(enable);
                        } else if ($(this).data("asmatDatePicker")) {
                            $(this).data("asmatDatePicker").enable(enable);
                        } else if ($(this).data("asmatNumericTextBox")) {
                            $(this).data("asmatNumericTextBox").enable(enable);
                        }
                        else {
                            if (enable == false) {
                                $(this).attr("disabled", "disabled");
                            } else {
                                $(this).removeAttr("disabled");
                            }
                        }
                    }
                });
            }
        },
        doCommonCheck: function (targit,showMsg) {
            //dropDownList
            var inputs = $(targit).find("input,textarea.s-input");
            $(".s-fieldError").removeClass("s-fieldError");
            //var errorInfos = {};
            $.each(inputs, function (n, value) {
                var name = this.name;
                var value = $.trim($(this).val());
                //s-required
                if (name != '' && $(this).hasClass("s-required")) {
                    if (value.length == 0) {
                        var cpation = "";
                        //if ($(this).parent().parent()[0].tagName == "DL") {
                        //    cpation = "【" + $(this).parent().parent().find("dt").text() + "】:";
                        //} else if ($(this).parent().parent().parent()[0].tagName == "DL") {
                        //    cpation = "【" + $(this).parent().parent().parent().find("dt").text() + "】:";
                        //} else if ($(this).parent().parent().parent().parent()[0].tagName == "DL") {
                        //    cpation = "【" + $(this).parent().parent().parent().parent().find("dt").text() + "】:";
                        //}

                        var form_group = $(this).closest('.form-group');
                        if (form_group.length > 0) {
                            if (form_group.find('.control-label').length > 0) {
                                cpation =  form_group.find('.control-label').text().replace("*", "");
                            }
                        }


                        smat.service.addErrorInfo(name, $(this), smat.service.format(smat.service.optionSet("SysMsg.Required"), cpation));
                    }
                }
            });

            var grids = $(targit).find(".s-grid");
            $.each(grids, function (n, value) {
                $(this).ui().checkData();
            });

            return smat.service.isNoError(showMsg);
        },
        addErrorInfo: function (key, target, msg) {
            if (smat.global.errorInfos[key] == undefined) {
                var targets = new Array();
                targets.push(target);
                smat.global.errorInfos[key] = {
                    target: targets,
                    msg: msg
                }
            } else {
                smat.global.errorInfos[key].target.push(target);
            }
        },
        isNoError: function (showMsg) {

            if (smat.service.isEmpty(smat.global.errorInfos) == false) {
                //show Error
                var msg = "";
                var firstNode = null;
                for (var key in smat.global.errorInfos) {
                    var nodes = smat.global.errorInfos[key].target;

                    for (var index in nodes) {
                        var node = nodes[index];

                        if (node != undefined && node.length > 0) {

                            smat.service.addErrorBorder(node);

                            if (firstNode == null) {
                                firstNode = node;
                            }
                        }

                    }
                    msg = msg + smat.global.errorInfos[key].msg + "<br />";

                    if (smat.uiConfig.showErrorOnlyOne == true) {
                        break;
                    }
                }

                //show msg
                if (showMsg == true && msg != "") {
                    //alert(msg);
                    smat.service.notice({ msg: msg, type: "error" });
                    if (firstNode != null) {
                        if (Modernizr.ios || Modernizr.android) {

                        } else {
                            if (firstNode.ui() != undefined) {
                                firstNode.ui().focus();
                            } else {
                                firstNode.focus();
                            }
                        }

                        var tabNode = firstNode.closest(".s-tabstrip");

                        if (tabNode != undefined && tabNode.length > 0) {
                            var contentNode = firstNode.closest(".s-content");
                            var tab = $(tabNode).data("asmatTabStrip");
                            //tab.select($(contentNode).index() - 1);
                            tab.select($(contentNode));
                        }
                    }
                    smat.global.errorInfos = {};
                }

                smat.service.closeLoding();
                return false;
            } else {
                smat.global.errorInfos = {};
                return true;
            }


        },
        addErrorBorder: function (node) {
            if (node.ui() instanceof smat.TextBox) {
                node.addClass("s-fieldError");
            } else if (node.hasClass("s-textbox")) {
                node.addClass("s-fieldError");
            } else {
                var defaults = node.closest('.s-widget').find('.s-state-default');
                if (defaults.length > 0) {
                    defaults.addClass("s-fieldError");
                } else {
                    node.addClass("s-fieldError");
                }
            }
        }, clearErrorBorder: function (node) {
            if (node.ui() instanceof smat.TextBox) {
                node.removeClass("s-fieldError");
            } else if (node.hasClass("s-textbox")) {
                node.removeClass("s-fieldError");
            } else {
                var defaults = node.closest('.s-widget').find('.s-state-default');
                if (defaults.length > 0) {
                    defaults.removeClass("s-fieldError");
                } else {
                    node.removeClass("s-fieldError");
                }
            }
        },
        getJsonData: function (data, key) {
            return smat.service.getJsonDataWithEmptyObject(data, key, "");
        },
        getJsonDataWithEmptyObject: function (data, key, empty) {
            var keys = key.split(".");
            var tempData = data;
            var index = 0;
            for (var name in keys) {
                index++;
                if (index == keys.length) {
                    if (tempData[keys[name]]) {
                        return tempData[keys[name]];
                    } else {
                        return empty;
                    }

                } else {
                    if (tempData[keys[name]]) {
                        tempData = tempData[keys[name]];
                    } else {
                        return empty;
                    }
                }
            }
        },
        setJsonData: function (data, key, value) {
            var keys = key.split(".");
            var tempData = data;
            var index = 0;
            for (var name in keys) {
                index++;
                if (index == keys.length) {
                    tempData[keys[name]] = value;

                } else {
                    if (tempData[keys[name]]) {
                        tempData = tempData[keys[name]];
                    } else {
                        tempData[keys[name]] = {};
                        tempData = tempData[keys[name]];
                    }
                }
            }
        },
        setJsonData2: function (data, key, value) {
            var keys = key.split(".");
            var tempData = data;
            var index = 0;
            for (var name in keys) {
                index++;
                if (index == keys.length) {
                    if (tempData[keys[name]] != undefined && tempData[keys[name]] != null) {
                        tempData[keys[name]] = tempData[keys[name]] + "," + value;
                    } else {
                        tempData[keys[name]] = value;
                    }
                } else {
                    if (tempData[keys[name]]) {
                        tempData = tempData[keys[name]];
                    } else {
                        tempData[keys[name]] = {};
                        tempData = tempData[keys[name]];
                    }
                }
            }
        },
        isEmpty: function (obj) {
            for (var name in obj) {
                return false;
            }
            return true;
        }, initTheme: function () {
            if (typeof (localStorage) == 'undefined') {

            } else {
                try {
                    var style = localStorage.getItem("theme");
                    if (style != null && style != undefined) {
                        smat.service.setTheme(style, false);

                    }

                } catch (e) {

                }
            }
        }, setTheme: function (style, needSave, aftersetTheme) {
            var theme = $(".tc-link[data-value = '" + style + "']");
            var link = $("#main-style");

            var link_mobile = $("#main-mobile-style");

            $(".tc-link").removeClass("active");
            theme.addClass("active");
            $("#theme-name").text($(theme.children("span.tc-theme-name")[0]).text());
            //$("#example").fadeTo(100,0.2);

            link.attr('href', smat.global.basePath + '/ui/styles/asmat.' + style + '.min.css');
            link_mobile.attr('href', smat.global.basePath + '/ui/styles/asmat.' + style + '.mobile.min.css');
            //$("#example").fadeTo(200,1);
            if (typeof (localStorage) == 'undefined') {

            } else if (needSave != false) {
                try {
                    localStorage.setItem("theme", style);
                } catch (e) {

                }
            }

            if (aftersetTheme != undefined) {
                aftersetTheme();
            }
        },
        showTip: function (config) {
            if ($("#SYS_tipTemp").length == 0) {
                $('body').append('<div id="SYS_tipTemp" style="display:none;"></div>');
                smat.global.CommonTip = $("#SYS_tipTemp").asmatTooltip({
                    autoHide: true,
                    showOn: "click",
                    position: "top",
                    content: "ASMAT",

                    hide: function () {

                        //smat.global.CommonTip.show();	
                    }
                }).data("asmatTooltip");

            }

            var position = "top";

            if (config.position != undefined) {
                position = config.position;
            }

            smat.global.CommonTip.options.position = position;
            smat.global.CommonTip.options.content = config.msg;
            smat.global.CommonTip.refresh();
            smat.global.CommonTip.show(config.target);
        },
        confirm: function (msg_config) {
            var callback = function (result) {
                if (result == "ok") {
                    msg_config.callback();
                }
            }

            var config = {
                title: smat.service.optionSet("DyOptionText.Confirm"),
                content: msg_config.msg,
                callback: callback,
                buttons: [
                    {
                        lbl: "&nbsp;&nbsp;&nbsp;OK&nbsp;&nbsp;&nbsp;",
                        value: "ok",
                        cls: "btn-danger"
                    },
                    {
                        lbl: smat.service.optionSet("DyOptionText.Cancel"),
                        value: "cancel",
                        cls: "btn-primary"
                    }
                ]
            }
            smat.service.dialog(config);
        },
        dialog: function (config) {
            var uid = smat.service.uuid();
            var box = $('<section id="' + uid + '_confirm" class="panel panel-default " style="margin: 0;padding: 10px;height: auto;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" style=" text-align: center;"><label class="control-label text-left">' + config.content + '</label></div></div>').appendTo(box);
            var btnRow = $('<div class="row" style="margin:8px 0;padding-top: 10px;"></div>').appendTo(box);
            var btnGroup = $('<div class=" form-group" style=" text-align: center;"></div>').appendTo(btnRow);
            var btns = config.buttons;

            for (var i in btns) {
                var btn = btns[i];
                var cls = "btn-info";
                if (btn.cls != undefined) cls = btn.cls;
                var btnControl = $('<button index="' + i + '" class="' + cls + '" style="margin-left:10px;">' + btn.lbl + '</button>');
                btnControl.appendTo(btnGroup);

                btnControl.smatButton({
                    click: function (e) {
                        var index = e.sender.element.attr("index")
                        smat.service.closeForm({
                            contentId: uid + '_confirm',
                            result: btns[index].value
                        });
                    }
                });
            }


            //var result = false;

            //var btnOK = box.find("#_pick_ok");
            //var btnCancel = box.find("#_pick_cancel");
            //btnOK.smatButton({
            //    click: function () {
            //        result = true;
            //        smat.service.closeForm({
            //            contentId: uid + '_confirm'
            //        });
            //    }
            //});

            //btnCancel.smatButton({
            //    click: function () {
            //        result = false;
            //        smat.service.closeForm({
            //            contentId: uid + '_confirm'
            //        });

            //    }
            //});

            var title = "";
            if (config.title != undefined) title = config.title;

            smat.service.openForm({
                contentDom: box,
                width: "410px",
                top: "20%",
                title: title,
                afterClose: function (result) {
                    if (config.callback) {
                        config.callback(result);
                    }

                }
            });

            return false;
        },
        getUserConfig: function (config) {
            var uid = smat.service.uuid();
            var box = $('<section id="' + uid + '_get_config" class="panel panel-default form-horizontal" style="margin: 0;padding: 10px;height: auto;"></section>');
            var items = config.items;
            for (var k in items) {
                var item = items[k];
                var row = $('<div class="row" style="margin:4px;"></div>').appendTo(box);
                var inputBox = $('<div class=" form-group" style=""></div>').appendTo(row);
                $('<label class="input-s-sm control-label">' + item.title + '</label>').appendTo(inputBox);

                var input;
                if (item.type == "Button") {
                    input = $('<button name="' + item.key + '">' + item.text + '</button>').appendTo(inputBox);
                } else {
                    input = $('<input name="' + item.key + '"/>').appendTo(inputBox);
                }
                if (smat[item.type] != undefined) {
                    var tempConfig = smat.globalObject.clone(item);
                    tempConfig.target = input;
                    new smat[item.type](tempConfig);
                }

            }

            var btnRow = $('<div class="row" style="margin:8px 0;padding-top: 10px;"></div>').appendTo(box);
            var btnGroup = $('<div class=" form-group" style=" text-align: center;"></div>').appendTo(btnRow);
            var btns = config.buttons;

            var btnOK = $('<button class="btn-success" style="margin-left:10px;">設定</button>');
            btnOK.appendTo(btnGroup);

            btnOK.smatButton({
                click: function (e) {

                    var r = {};

                    for (var k in items) {
                        var item = items[k];
                        r[item.key] = $("#" + uid + '_get_config').find('input[name="' + item.key + '"]').ui().value();
                    }

                    if (config.checkResult && config.checkResult(r, uid) == false) {
                        return;
                    }

                    smat.service.closeForm({
                        contentId: uid + '_get_config',
                        result: r
                    });
                }
            });

            var btnCanel = $('<button class="btn-success" style="margin-left:10px;">キャンセル</button>');
            btnCanel.appendTo(btnGroup);

            btnCanel.smatButton({
                click: function (e) {
                    smat.service.closeForm({
                        contentId: uid + '_get_config'
                    });
                }
            });

            var btnDel = $('<button class="btn-danger" style="margin-left:10px;">削除</button>');
            btnDel.appendTo(btnGroup);

            btnDel.smatButton({
                click: function (e) {
                    smat.service.closeForm({
                        contentId: uid + '_get_config',
                        result: "del"
                    });
                }
            });


            var title = "";
            if (config.title != undefined) title = config.title;

            smat.service.openForm({
                contentDom: box,
                width: "410px",
                top: "20%",
                title: title,
                afterClose: function (result) {
                    if (result && config.callback) {
                        config.callback(result);
                    }

                }
            });

            return false;
        },
        format: function (formatStr, args) {

            if (arguments.length > 0) {
                var result = formatStr;
                if (arguments.length == 2 && typeof (args) == "object") {
                    for (var key in args) {
                        var reg = new RegExp("(\\{[" + key + "]\\})", "g");
                        result = result.replace(reg, args[key]);
                    }
                } else {
                    for (var i = 1; i < arguments.length; i++) {
                        if (arguments[i] == undefined) {
                            return "";
                        }
                        else {
                            var reg = new RegExp("(\\{[" + (i - 1) + "]\\})", "g");
                            result = result.replace(reg, arguments[i]);
                        }
                    }
                }
                return result;
            }
            else {
                return "";
            }
        },
        msg: function () {
            debugger;
            if (arguments.length == 0) return "";

            var args = [];
            for (var i = 1; i < arguments.length; i++) {
                args.push(arguments[i]);
            }
            return this.optionSet(arguments[0], args);

        },
        optionSet: function (cdKind) {

            if (arguments.length == 0) return "";

            var codeKind = "";
            var cd = undefined;

            if (typeof (arguments[0]) == "string") {
                if (arguments[0].indexOf(".") >= 0) {
                    codeKind = arguments[0].substring(0, arguments[0].indexOf("."));
                    cd = arguments[0].substring(arguments[0].indexOf(".") + 1);
                } else {
                    codeKind = arguments[0];
                }
            } else if (typeof (arguments[0]) == "object") {
                codeKind = arguments[0].codeKind;
                cd = arguments[0].cd;
            }

            var args = [];
            for (var i = 1; i < arguments.length; i++) {
                args.push(arguments[i]);
            }

            var targetLang = this.getSysLanguage();
            var defaultLang = smat.global.language;

            if (smat.global.codeMstMap[codeKind] == undefined) {
                smat.service.loadJosnData({
                    url: smat.uiConfig.CodeMst.codeListUrl,
                    params: { ProjID: smat.global.ProjID, CodeKind: codeKind, TargetLang: targetLang, DefaultLang: defaultLang },
                    async: false,
                    success: function (result) {

                        for (var index in result) {
                            var codeData = result[index];
                            if (codeData[smat.uiConfig.CodeMst.kindField] != undefined) {
                                if (smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] == undefined) {
                                    smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] = new Array();
                                    smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]] = {};
                                }
                                smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]].push(codeData);
                                smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]][codeData[smat.uiConfig.CodeMst.codeField]] = codeData;
                            }
                        }
                    }
                });
            }

            //if (smat.global.codeMstMap[codeKind + "_" + targetLang] != undefined) {
            //    if (cd == undefined) {
            //        return smat.global.codeMst[codeKind + "_" + targetLang];
            //    } else {
            //        if (smat.global.codeMstMap[codeKind + "_" + targetLang][cd] != undefined) {
            //            return smat.service.format(smat.global.codeMstMap[codeKind + "_" + targetLang][cd].Name, args);
            //        }
            //    }
            //}

            //if (smat.global.codeMstMap[codeKind + "_" + defaultLang] != undefined) {
            //    if (cd == undefined) {
            //        return smat.global.codeMst[codeKind + "_" + defaultLang];
            //    } else {
            //        if (smat.global.codeMstMap[codeKind + "_" + defaultLang][cd] != undefined) {
            //            return smat.service.format(smat.global.codeMstMap[codeKind + "_" + defaultLang][cd].Name, args);
            //        }
            //    }
            //}

            if (smat.global.codeMstMap[codeKind] != undefined) {
                if (cd == undefined) {
                    return smat.global.codeMst[codeKind];
                } else {
                    if (smat.global.codeMstMap[codeKind][cd] != undefined) {
                        return smat.service.format(smat.global.codeMstMap[codeKind][cd].Name, args);
                    }
                }
            }

            if (cd == undefined) {
                return [];
            } else {
                return "";
            }
        },
        getSysLanguage: function () {
            var currentLang = navigator.language;
            if (!currentLang) {
                currentLang = navigator.browserLanguage;
            }

            currentLang = currentLang.toLowerCase();
            return currentLang;
        }, cultureText: function (val) {
            var reg = /^codeKind:(\w+)\.(\w+)$/i;
            if (reg.test(val)) {
                var regArr = reg.exec(val);
                var codeKind = regArr[1];
                var cd = regArr[2];

                return this.optionSet(codeKind + "." + cd);
            } else {
                return val;
            }
        }, notice: function (config) {
            if ($("#SYS_notificationTemp").length == 0) {
                $('body').append('<span id="SYS_notificationTemp" style="display:none;"></span>');

                var autoHideAfter = 0;
                if (config.type == "error") {
                    autoHideAfter = 4000 + (config.msg.length * 300);
                } else {
                    autoHideAfter = 2000 + (config.msg.length * 200);
                }

                var top = 40;
                var right = 30;


                smat.global.CommonNotice = $("#SYS_notificationTemp").asmatNotification({
                    position: {
                        pinned: true,
                        top: top,
                        right: right
                    },
                    animation: {
                        open: {
                            effects: "fadeIn"
                        },
                        close: {
                            effects: "none",
                            reverse: false
                        }
                    },
                    autoHideAfter: autoHideAfter,
                    hideOnClick: false,
                    stacking: "down",
                    templates: [{
                        type: "info",
                        template: smat.global.n_emailTemplate
                    }, {
                        type: "error",
                        template: smat.global.n_errorTemplate
                    }, {
                        type: "success",
                        template: smat.global.n_successTemplate
                    }],

                }).data("asmatNotification");

            }

            var type = "success";
            if (config.type != undefined) {
                type = config.type;
            }

            var title = "";
            if (config.title != undefined) {
                title = config.title;
            }
            smat.global.CommonNotice.hide();

            smat.global.CommonNotice.show({
                position: {
                    pinned: true,
                    top: top,
                    right: right
                },
                message: config.msg,
                title: title
            }, type);

            $('.notice-close').bind('click', function () {
                smat.global.CommonNotice.hide();
            });

            if (config.target != undefined) {
                var box = config.target.closest('.s-widget');
                if (box.ui() == null) { box = config.target; }
                var top = box.offset().top;
                //var left = config.target.closest('.s-widget').offset().left;
                //var h = $('.s-animation-container:visible').height();
                h = box.height();
                var w = $('.s-animation-container:visible').width();
                var ww = $(window).width();
                //if ((left + w) > ww) left = ww - w;
                if (Modernizr.ios || Modernizr.android) h = h + 20;
                $('.s-animation-container:visible').css("top", (top + h) + "px");
                //$('.s-animation-container:visible').css("left", left + "px");
            }


            $('.s-animation-container').css("z-index", "999999");
        },
        noticeClear: function () {
            if (smat.global.CommonNotice != null || smat.global.CommonNotice != undefined) {
                smat.global.CommonNotice.hide();
            }
        },
        openLoding: function () {
            if ($("#s_loading_form").length == 0) {
                $('<div class="" id="s_loading_form" style="display: block; z-index: 9999999; opacity: 1;background-color: transparent;position: absolute;width: 100%;height: 100%;top: 0;"><div class="loadingBox" style="margin-top:12%; text-align: center;"><img class="s-image" src="' + smat.global.basePath + '/SMAT.UI/Styles/Material/loading-image.gif"></div></div>').appendTo($('body'));
            }
            //$("#s_loading_form").show();
        }
        ,
        openAppLoding: function () {
            if ($("#s_loading_form").length == 0) {
                $('<div class="" id="s_loading_form" style="display: block; z-index: 9999999; opacity: 1;background-color: transparent;position: absolute;width: 100%;height: 100%;top: 0;"><div class="loadingBox" style="margin-top:12%; text-align: center;"><span id="s_loading_form" class="sm-scroller-pull sm-scroller-refresh" style="top: 50%;width: 50px;background-color: #10C4B2;color: #fff;"><span class="sm-icon" style="background: #fff;animation-delay: .2s;"></span><span class="sm-loading-left" style="background: #fff;animation-delay: .4s;"></span><span class="sm-loading-right" style="background: #fff;animation-delay: .0s;"></span></span></div></div>').appendTo($('body'));
            }

            //$("#s_loading_form").show();
        }
		, closeLoding: function () {

		    //$("#s_loading_form").hide();
		    $("#s_loading_form").remove();
		},
        delItemByKey: function (list, keyName, key) {
            if (list == undefined || list == null) {
                return undefined;
            }
            for (var i in list) {
                if (list[i][keyName] == key) {
                    list.splice(i, 1);
                    return;
                }
            }
        },
        getItemByKey: function (list, keyName, key) {
            if (list == undefined || list == null) {
                return undefined;
            }
            for (var i in list) {
                if (list[i][keyName] == key) {
                    return list[i];
                }
            }
            return undefined;
        },
        getItemByKeyContain: function (list, keyName, key) {
            if (list == undefined || list == null) {
                return undefined;
            }
            for (var i in list) {
                if (("" + list[i][keyName]).indexOf(key) >= 0) {
                    return list[i];
                }
            }
            return undefined;
        },
        fillKeyPath: function (obj, keyName, keyPath, childName) {
            for (var key in obj) {
                var item = obj[key];
                if (keyPath != "") {
                    item["keyPath"] = keyPath + "." + item[keyName];
                } else {
                    item["keyPath"] = item[keyName];
                }

                if (typeof (item[childName]) == 'object') {
                    smat.service.fillKeyPath(item[childName], keyName, item["keyPath"], childName);
                }
            }
            return undefined;
        },
        template: function (templateId, data) {

            return smat.service.fillTemplate($('#' + templateId).html(), data);

        }, fillTemplate: function (templateStr, data) {

            var con = templateStr;
            var t;
            var fields = {};
            var reg = /#:(.*)?#/igm;
            while ((t = reg.exec(con)) != null) {
                fields[t[1]] = data[t[1].trim()]
            }

            for (var key in fields) {

                con = con.replace(new RegExp("#:" + key + "#", "gm"), fields[key]);
            }
            return con;
        },
        strToJson: function (str) {
            try {
                return (new Function("return " + str + ";"))();
            }
            catch (err) {
                return {};
            }

        },
        getDataJoinStr: function (data, fields, joinStr) {
            if (fields == undefined || data == undefined) {
                return null;
            }

            var result = "";
            for (var key in fields) {
                if (result == "") {
                    result = data[fields[key]];
                } else {
                    result += joinStr + data[fields[key]];
                }
            }

            return result;
        }
		, loadHash: function (e) {
		    //alert(e);

		}, setHash: function (tid, url) {

		    smat.global.tidUrlMap.set(tid, url);

		}, uuid: function () {
		    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
		        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
		        return v.toString(16);
		    });
		}, HTMLDecode: function (text) {
		    var temp = document.createElement("div");
		    temp.innerHTML = text;
		    var output = temp.innerText || temp.textContent;
		    temp = null;
		    return output;
		}, obj2String: function (_obj) {
		    var t = typeof (_obj);
		    if (t != 'object' || _obj === null) {
		        // simple data type
		        if (t == 'string') {
		            _obj = '"' + _obj + '"';
		        }
		        return String(_obj);
		    } else {
		        if (_obj instanceof Date) {
		            return _obj.toLocaleString();
		        }
		        // recurse array or object
		        var n, v, json = [], arr = (_obj && _obj.constructor == Array);
		        for (n in _obj) {
		            v = _obj[n];
		            t = typeof (v);
		            if (t == 'string') {
		                v = '"' + v + '"';
		            } else if (t == "object" && v !== null) {
		                v = this.obj2String(v);
		            }
		            json.push((arr ? '' : '"' + n + '":') + String(v));
		        }
		        return (arr ? '[' : '{') + String(json) + (arr ? ']' : '}');
		    }
		}, cookie: function (name, value, options) {
		    if (typeof value != 'undefined') { // name and value given, set cookie
		        options = options || { path: "/", expires: 365 };
		        if (value === null) {
		            value = '';
		            options.expires = -1;
		        }
		        var expires = '';
		        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
		            var date;
		            if (typeof options.expires == 'number') {
		                date = new Date();
		                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
		            } else {
		                date = options.expires;
		            }
		            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
		        }
		        var path = options.path ? '; path=' + options.path : '';
		        var domain = options.domain ? '; domain=' + options.domain : '';
		        var secure = options.secure ? '; secure' : '';
		        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
		    } else { // only name given, get cookie
		        var cookieValue = null;
		        if (document.cookie && document.cookie != '') {
		            var cookies = document.cookie.split(';');
		            for (var i = 0; i < cookies.length; i++) {
		                var cookie = jQuery.trim(cookies[i]);
		                // Does this cookie string begin with the name we want?
		                if (cookie.substring(0, name.length + 1) == (name + '=')) {
		                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
		                    break;
		                }
		            }
		        }
		        return cookieValue;
		    }
		}, handleRequestError: function (XMLHttpRequest, reload) {
		    smat.service.closeLoding();
		    if (XMLHttpRequest.responseText && XMLHttpRequest.responseText.indexOf('asmat-plase-go-to-login-form') > 0) {
		        window.location.href = "/Home/Login";
		    } else {

		        if (XMLHttpRequest.status == 0) {

		            var confirm_config = {
		                title: smat.service.optionSet("SysText.Confirm"),
		                content: smat.service.optionSet("SysMsg.ServiceNotConnect"),
		                buttons: [
                            {
                                lbl: "&nbsp;&nbsp;&nbsp;OK&nbsp;&nbsp;&nbsp;",
                                value: "ok",
                                cls: "btn-primary"
                            }
		                ]
		            }
		            smat.service.dialog(confirm_config);
		        } else if (XMLHttpRequest.responseText == "") {
		            //alert("Error!!!");
		            if (reload == true) {
		                window.location.reload();
		            }
		        } else {

		            if (XMLHttpRequest.responseText.indexOf('JSON JavaScriptSerializer') >= 0) {

		            } else if (XMLHttpRequest.responseText.indexOf('<html>') >= 0 && XMLHttpRequest.responseText.indexOf('</html>') > 0) {
		                //alert(XMLHttpRequest.responseText.substring(XMLHttpRequest.responseText.indexOf('<html>'), XMLHttpRequest.responseText.indexOf('</html>')+7));

		                //var box = $('<section id="error_form" class="panel panel-default " style="margin: 0;padding: 10px;height: 620px;"></section>');
		                //box.html(XMLHttpRequest.responseText.substring(XMLHttpRequest.responseText.indexOf('<html>'), XMLHttpRequest.responseText.indexOf('</html>') + 7));
		                //smat.service.openForm({
		                //    //m_opacity: 0,
		                //    contentDom: box,
		                //    width: "80%",
		                //    top: "10%",
		                //    title: "error"
		                //});

		                var confirm_config = {
		                    title: smat.service.optionSet("DyOptionText.Confirm"),
		                    content: smat.service.optionSet("SysMsg.SystemError"),
		                    buttons: [
                                {
                                    lbl: "&nbsp;&nbsp;&nbsp;OK&nbsp;&nbsp;&nbsp;",
                                    value: "ok",
                                    cls: "btn-primary"
                                }
		                    ]
		                }
		                smat.service.dialog(confirm_config);
		            }
		        }

		    }
		}, uiAfterInit: function (target) {
		    var uis = $(target).find('[uuid]');
		    $.each(uis, function () {
		        var ui = $(this).ui();
		        if (ui.afterInit) {
		            ui.afterInit();
		        }
		    })
		}, beginWarnSessionTime: function () {
		    smat.uiConfig.warnSessionTime = true;
		    smat.uiConfig.lastSessionTime = new Date();

		    window.setInterval(function () {
		        var now = new Date();
		        var secs = smat.uiConfig.sessionLiveTime - Math.floor((now.getTime() - smat.uiConfig.lastSessionTime.getTime()) / 1000);

		        if (secs > 0 && secs <= 120) {

		            smat.service.notice({ msg: "後<span style='color:red;'>" + (secs) + "秒</span>タイムアウトになるので、早く編集データを保存してください。", type: "info" });

		            //window.setTimeout(function () {
		            //    secs = secs - 3;
		            //    smat.service.notice({ msg: "後<span style='color:red;'>" + (secs) + "秒</span>タイムアウトになるので、早く編集データを保存してください。", type: "info" });

		            //}, 3000);
		            //window.setTimeout(function () {
		            //    secs = secs - 3;
		            //    smat.service.notice({ msg: "後<span style='color:red;'>" + (secs) + "秒</span>タイムアウトになるので、早く編集データを保存してください。", type: "info" });

		            //}, 6000);

		        } else if (secs < 0) {
		            smat.service.notice({ msg: "<span style='color:red;'>タイムアウトになりました</span>。", type: "info" });
		        } else {
		            if ((secs <= 660 && secs >= 650)
                        || (secs <= 360 && secs >= 350)
                        || (secs <= 300 && secs >= 290)
                        || (secs <= 240 && secs >= 230)
                        || (secs <= 180 && secs >= 170)
                        ) {
		                smat.service.notice({ msg: "後<span style='color:red;'>" + Math.floor((secs / 60)) + "分</span>タイムアウトになるので、早く編集データを保存してください。", type: "info" });
		            }
		        }

		    }, 10000);
		}, setLastSessionTime: function () {
		    smat.uiConfig.lastSessionTime = new Date();
		}, endWarnSessionTime: function () {
		    smat.uiConfig.warnSessionTime = false;
		}, getServerFile: function (config) {
		    var url = config.url;
		    var params = config.params;
		    var success = config.success;
		    var type = config.type;

		    smat.service.loadJosnData({
		        url: url,
		        params: params,
		        success: function (result) {
		            if (result.ResultType == 0) {
		                smat.service.notice({ msg: result.Message, type: "info" });
		            } else {
		                $('body').append($('<iframe id="iframeDownload" style="width:1px;height:1px;display: none;" src="' + result.Path + '">'));
		            }
		        }
		    });
		},
		setDataItemValue: function (source,fromItem) {
		    if (source) {
		        for (var key in source) {
		            if (fromItem[key] || typeof fromItem[key] == "boolean") {
		                if (typeof source[key] == "boolean") {
		                    source[key] = smat.service.cBool(fromItem[key]);
		                } else {
		                    source[key] = fromItem[key]
		                }
		            }
		        }
		    }
		}, cBool: function (bVal) {
		    if (bVal == "true" || bVal == true) {
		        return true;
		    } else if (bVal == "false" || bVal == false) {
		        return false;
		    }
		}, isEqual: function(a, b) {
		    if (!a && !b) {
		        return true;
		    }

		    if (!(a && b)) {
		        return false;
		    }
          
		    if (typeof (a) == 'object' && typeof (b) == 'object') {
		        // Of course, we can do it use for in 
		        // Create arrays of property names
		        var aProps = Object.getOwnPropertyNames(a);
		        var bProps = Object.getOwnPropertyNames(b);

		        // If number of properties is different,
		        // objects are not equivalent
		        if (aProps.length != bProps.length) {
		            return false;
		        }

		        for (var i = 0; i < aProps.length; i++) {
		            var propName = aProps[i];
		            if (propName == "UpdateUid") {
		                return true;
		            }
		            if (!this.isEqual(a[propName], b[propName])) {
		                return false;
		            }
		        }
		    } else if (typeof (a) != 'object' && typeof (b) != 'object') {
		        if (a.toString() !== b.toString()) {
		            return false;
		        }
		    } else {
		        return false;
		    }
 
		    // If we made it this far, objects
		    // are considered equivalent
		    return true;
		}

    };

    $(window).bind("hashchange", smat.service.loadHash);

    //	jQuery(function($) {
    //           $("body").click(function(e){
    //                 if ($(e.target).is('a')||$(e.target).is('input')){
    //					return;	
    //				}
    //                  
    //           });
    //             
    //        }); 
    //	

    // ----------------------------------------------------------------------
    // <summary>
    // 「文字を左パットする」処理関数
    // </summary>
    // <param name="intLenght">パットの長さ</param>
    // <param name="strParam">パットの文字</param>
    // <returns>文字を左パットする</returns>
    // <remarks>文字を左パットする。</remarks>
    // ----------------------------------------------------------------------
    String.prototype.padLeft = function (intLenght, strParam) {
        if (this.length >= intLenght)
            return this;
        else
            return (strParam + this).padLeft(intLenght, strParam);
    }

    // ----------------------------------------------------------------------
    // <summary>
    // 「前後の空白文字を切り落とす」処理関数
    // </summary>
    // <param name="strParameter">なし</param>
    // <returns>前後の空白文字を切り落とす</returns>
    // <remarks>前後の空白文字を切り落とす。</remarks>
    // ----------------------------------------------------------------------
    String.prototype.trim = function () {
        return this.replace(/^(\s|\u3000)+|(\s|\u3000)+$/g, "");
    }

    // ----------------------------------------------------------------------
    // <summary>
    // 「文字を右パットする」処理関数
    // </summary>
    // <param name="intLenght">パットの長さ</param>
    // <param name="strParam">パットの文字</param>
    // <returns>文字を右パットする</returns>
    // <remarks>文字を右パットする。</remarks>
    // ----------------------------------------------------------------------
    String.prototype.padRight = function (intLenght, strParam) {
        if (this.length >= intLenght)
            return this;
        else
            return (this + strParam).padRight(intLenght, strParam);
    }

    // ----------------------------------------------------------------------
    // <summary>
    // 「日付フォーマット」処理関数
    // </summary>
    // <param name="strParam">フォーマット式（yyyy-MM-dd hh:mm:ss　、yyyy-MM-dd）</param>
    // <returns>日付フォーマット</returns>
    // <remarks>日付フォーマット。</remarks>
    // ----------------------------------------------------------------------
    Date.prototype.format = function (strParam) { //author: meizz 
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(strParam)) strParam = strParam.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(strParam)) strParam = strParam.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return strParam;
    }
    $.fn.formatCalendar = function (isShowMsg) {
        $(this).each(function () {
            var value = $(this).val().trim();
            if ($(this)[0].tagName != "INPUT") {
                value = $(this).text().trim();
            }


            if (value.length == 0) {
                $(this).removeAttr('data-error');
                if ($(this).ui()) {
                    if ($(this).ui().value() != null) {
                        $(this).ui().value("");
                        $(this).ui().uiControl.trigger("change");
                    }
                }
                return;
            }

            if ($(this).ui() && $(this).ui().config.depth == "year") {
                value += "01";
            }
            if ($(this).ui() && $(this).ui().config.depth == "decade") {
                value += "0101";
            }

            var values = value.split('/');

            for (var i = 0; i < values.length; i++) {
                values[i] = values[i].padLeft(2, '0');
            }

            value = values.join('/');
            value = value.replace(/\//g, "");

            if (value.length <= 2) {
                value = (new Date().format("yyyy/MM/")) + value;
            } else if (value.length <= 4) {
                value = value.padLeft(4, "0");
                value = (new Date().format("yyyy/")) + value.substr(0, 2) + "/" + value.substr(2, 2);
            } else if (value.length <= 8) {
                var year = (new Date().format("yyyy"));
                year = year.substr(0, 4 - (value.length - 4));
                value = year + value;
                value = value.substr(0, 4) + "/" + value.substr(4, 2) + "/" + value.substr(6, 2);
            }

            var tempDate = new Date(value);
            if (isNaN(tempDate.getDate()) || value.replace(/\//g, "") != tempDate.format("yyyyMMdd")) {
                if (isShowMsg) {
                    //alert("日期格式不正确！");
                    //$(this).val("")
                    smat.service.notice({ msg: smat.service.optionSet("SysMsg.DateFormatError"), type: "error", target: $(this) });
                    smat.service.addErrorBorder($(this));
                    $(this).attr('data-error', "error");
                    if (Modernizr.ios || Modernizr.android) {
                        if ($(this).ui()) {
                            $(this).ui().value("");
                        }
                        else if ($(this)[0].tagName != "INPUT") {
                            $(this).text("");
                        } else {
                            $(this).val("");
                        }
                        $(this).removeAttr('data-error');
                    } else {

                        //$(this).focus().select();
                        var i = $(this);
                        setTimeout(function () {
                            i.focus().select()
                        }, 100);
                    }

                }
                return false;
            } else {
                smat.service.clearErrorBorder($(this));
            }

            if ($(this).ui()) {
                if (asmat.toString($(this).ui().value(), "yyyy/MM/dd") == value) {
                    $(this).ui().value(tempDate);
                } else {
                    $(this).ui().value(tempDate);
                    $(this).ui().uiControl.trigger("change");
                }

            }
            else if ($(this)[0].tagName != "INPUT") {
                $(this).text(value);
            } else {
                $(this).val(value);
            }
            $(this).removeAttr('data-error');
            return true;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 「時間フォーマット」処理関数
    // </summary>
    // <param name="strParam">メッセージ表示</param>
    // <returns>時間フォーマット</returns>
    // <remarks>時間フォーマット。</remarks>
    // ----------------------------------------------------------------------
    $.fn.formatTime = function (format, isShowMsg) {
        $(this).each(function () {
            var value = $(this).val().trim();
            if ($(this)[0].tagName != "INPUT") {
                value = $(this).text().trim();
            }


            if (value.length == 0) {
                $(this).removeAttr('data-error');
                if ($(this).ui()) {
                    if ($(this).ui().value() != null) {
                        $(this).ui().value("");
                        $(this).ui().uiControl.trigger("change");
                    }
                }
                return;
            }

            var values = value.split(':');

            if (values.length == 1) {
                values = [];
                if (value.length >= 2) {
                    values.push(value.substring(0, 2));
                } else {
                    values.push(value.substring(0));
                }
                if (value.length >= 4) {
                    values.push(value.substring(2, 4));
                } else {
                    values.push(value.substring(2));
                }
                if (value.length >= 6) {
                    values.push(value.substring(4, 6));
                } else {
                    values.push(value.substring(4));
                }
            }

            for (var i = 0; i < values.length; i++) {
                if (i == 0) {
                    values[i] = values[i].padLeft(2, '0');
                } else {
                    values[i] = values[i].padRight(2, '0');
                }
                values[i] = values[i].substring(0, 2);
            }

            while (values.length < 3) {
                values.push("".padLeft(2, '0'));
            }

            if (isNaN(values.join(''))) {
                if (isShowMsg) {
                    smat.service.notice({ msg: "時間入力不正。", type: "error", target: $(this) });
                    smat.service.addErrorBorder($(this));
                    $(this).attr('data-error', "error");
                    if (Modernizr.ios || Modernizr.android) {
                        if ($(this).ui()) {
                            $(this).ui().value("");
                        }
                        else if ($(this)[0].tagName != "INPUT") {
                            $(this).text("");
                        } else {
                            $(this).val("");
                        }
                        $(this).removeAttr('data-error');
                    } else {

                        //$(this).focus().select();
                        var i = $(this);
                        setTimeout(function () {
                            i.focus().select()
                        }, 100);
                    }

                }
                return false;
            } else {
                smat.service.clearErrorBorder($(this));
            }

            switch (format) {
                case "HH":
                    value = values[0];
                    break;
                case "HH:mm":
                    value = values[0] + ":" +values[1];
                    break;
                case "HH:mm:ss":
                    value = values[0] + ":" + values[1] + ":" + values[2];
                    break;
                default:
                    value = values[0] + ":" + values[1] + ":" + values[2];
                    break;

            }

            if ($(this).ui()) {
                if ($(this).ui().value() == value) {
                    $(this).ui().value(value);
                } else {
                    $(this).ui().value(value);
                    $(this).ui().uiControl.trigger("change");
                }

            } else if ($(this)[0].tagName != "INPUT") {
                $(this).text(value);
            } else {
                $(this).val(value);
            }
            $(this).removeAttr('data-error');

            return true;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 数字入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks> 数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyNum = function () {
        //$(this).css('text-align', 'right');
        $(this).bind('keypress', function (event) {
            var eventObj = event || e;
            var keyCode = eventObj.keyCode || eventObj.which;
            if ((keyCode >= 48 && keyCode <= 57))
                return true;
            else
                return false;
        }).bind('input propertychange change', function (event) {
            var val = $(this).val().toString();
            var newvalue = $(this).val().replace(/[^\x00-\xff]/g, "*_*");
            if (newvalue.indexOf("*_*") >= 0) {
                $(this).val(newvalue.replace(/\*_\*/g, ""));
            }
            return;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^\d+$/.test(clipboard))
                return true;
            else
                return false;
        });
    };
    // ----------------------------------------------------------------------
    // <summary>
    // 数字入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks> 数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyDecimal = function () {
        $(this).css('text-align', 'right');
        $(this).bind('keypress', function (event) {
            var eventObj = event || e;
            var keyCode = eventObj.keyCode || eventObj.which;
            if ((keyCode >= 48 && keyCode <= 57) || keyCode == 46)
                return true;
            else
                return false;
        }).bind('input propertychange change', function (event) {
            var val = $(this).val().toString();
            var newvalue = $(this).val().replace(/[^\x00-\xff]/g, "*_*");
            if (newvalue.indexOf("*_*") >= 0) {
                $(this).val(newvalue.replace(/\*_\*/g, ""));
            }
            return;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^(-?\d+)(\.\d+)?$/.test(clipboard))
                return true;
            else
                return false;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 数字入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks> 数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyNumWidthHyphen = function () {
        //$(this).css('text-align', 'right');
        $(this).bind('keypress', function (event) {
            var eventObj = event || e;
            var keyCode = eventObj.keyCode || eventObj.which;
            if ((keyCode >= 48 && keyCode <= 57) || keyCode == 45)
                return true;
            else
                return false;
        }).bind('input propertychange change', function (event) {
            var val = $(this).val().toString();
            var newvalue = $(this).val().replace(/[^\x00-\xff]/g, "*_*");
            if (newvalue.indexOf("*_*") >= 0) {
                $(this).val(newvalue.replace(/\*_\*/g, ""));
            }
            return;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^\d[\d\-]*$/.test(clipboard))
                return true;
            else
                return false;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 数字入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks> 数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.maxByteLength = function (maxLength) {
        var $textBox = this;

        //$textBox.unbind("input propertychange change");

        $textBox.bind("input propertychange change", function () {

            var val = $textBox.val().toString();

            //if (val.length > maxLength) {

            //    $textBox.val(val.substring(0, maxLength));

            //}

            var newvalue = $textBox.val().replace(/[^\x00-\xff]/g, "**");
            var length = newvalue.length;

            //当填写的字节数小于设置的字节数   
            if (length * 1 <= maxLength * 1) {
                return;
            }
            var limitDate = newvalue.substr(0, maxLength);
            var count = 0;
            var limitvalue = "";
            for (var i = 0; i < limitDate.length; i++) {
                var flat = limitDate.substr(i, 1);
                if (flat == "*") {
                    count++;
                }
            }
            var size = 0;
            var istar = newvalue.substr(maxLength * 1 - 1, 1);//校验点是否为“×”   

            //if 基点是×; 判断在基点内有×为偶数还是奇数    
            if (count % 2 == 0) {
                //当为偶数时  
                size = count / 2 + (maxLength * 1 - count);
                limitvalue = val.substr(0, size);
            } else {
                //当为奇数时   
                size = (count - 1) / 2 + (maxLength * 1 - count);
                limitvalue = val.substr(0, size);
            }
            $textBox.val(limitvalue);
            return;

        })
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 英字入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks>英字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyAlpha = function () {
        $(this).keypress(function (event) {
            var eventObj = event || e;
            var keyCode = eventObj.keyCode || eventObj.which;
            if ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
                return true;
            else
                return false;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^[a-zA-Z]+$/.test(clipboard))
                return true;
            else
                return false;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 英数字入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks>英数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyNumAlpha = function () {
        $(this).keypress(function (event) {
            var eventObj = event || e;
            var keyCode = eventObj.keyCode || eventObj.which;
            if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122))
                return true;
            else
                return false;
        }).bind('input propertychange change', function (event) {
            var val = $(this).val().toString();
            var newvalue = $(this).val().replace(/[^\x00-\xff]/g, "*_*");
            if (newvalue.indexOf("*_*") >= 0) {
                $(this).val(newvalue.replace(/\*_\*/g, ""));
            }
            return;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^(\d|[a-zA-Z])+$/.test(clipboard))
                return true;
            else
                return false;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 半角の英字、数字、記号入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks>英数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyHalfNumAlpha = function () {
        $(this).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind('input propertychange change', function (event) {
            var val = $(this).val().toString();
            var newvalue = $(this).val().replace(/[^\x00-\xff]/g, "*_*");
            if (newvalue.indexOf("*_*") >= 0) {
                $(this).val(newvalue.replace(/\*_\*/g, ""));
            }
            return;
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^(\d|[a-zA-Z])+$/.test(clipboard))
                return true;
            else
                return false;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // 半角の英字、数字、記号入力のみ
    // </summary>
    // <returns>なし</returns>
    // <remarks>英数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyNumAlphaMinus = function () {
        $(this).keypress(function (event) {
            var eventObj = event || e;
            var keyCode = eventObj.keyCode || eventObj.which;
            if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || keyCode == 45)
                return true;
            else
                return false;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            var clipboard = window.clipboardData.getData("Text");
            if (/^(\d|[a-zA-Z]|-)+$/.test(clipboard))
                return true;
            else
                return false;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    // focusのみ
    // </summary>
    // <returns>なし</returns>
    // <remarks> 数字入力のみ</remarks>
    // ----------------------------------------------------------------------
    $.fn.onlyFocus = function () {
        $(this).keypress(function (event) {
            return false;
        }).focus(function () {
            this.style.imeMode = 'disabled';
        }).bind("paste", function () {
            return false;
        });
    };

    // ----------------------------------------------------------------------
    // <summary>
    //エンドフォーカス位置
    // </summary>
    // <returns>なし</returns>
    // <remarks>エンドフォーカス位置</remarks>
    // ----------------------------------------------------------------------
    $.fn.focusEnd = function () {
        var rtextRange = event.srcElement.createTextRange();
        rtextRange.moveStart('character', event.srcElement.value.length);
        rtextRange.collapse(true);
        rtextRange.select();
    }

    $.fn.ui = function () {
        var uuid = $(this).attr('uuid');
        if (smat.global.uiMap.contains(uuid)) {
            return smat.global.uiMap.get(uuid);
        } else {
            if (window.SMAT && SMAT.uiMap.contains(uuid)) {
                return SMAT.uiMap.get(uuid);
            } else {
                return null;
            }
        }
    };

    smat.service.math = {

        getDigitObject: function (f) {
            if (f == null) return null;
            var i = f.toString().indexOf(".");
            if (i < 0) {
                i = 0;
            } else {
                //i = f.toString().length - i - 1;
                i = 3;
            }
            return { data: f * Math.pow(10, i), bit: i };
        },

        mul: function (mul1, mul2) {
            var d1 = this.getDigitObject(mul1);
            var d2 = this.getDigitObject(mul2);

            return (d1.data * d2.data) / (Math.pow(10, (d1.bit + d2.bit)));
        }
    };

})();