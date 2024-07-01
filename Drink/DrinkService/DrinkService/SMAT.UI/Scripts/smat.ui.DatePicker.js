﻿(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DatePicker
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDatePicker = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.DatePicker(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.DatePicker = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            culture: "ja"
        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        //共通初始化后
        //this.afterInit();


        return this;
    };

    smat.DatePicker.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.DatePicker.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            var uiConfig = smat.globalObject.clone(this.config);
            uiConfig.change = function (e) {
                self.trigger(smat.event.CHANGE, e);
            }
            uiConfig.close = function (e) {
                self.trigger(smat.event.CLOSE, e);
            }
            uiConfig.open = function (e) {
                var div = e.sender.dateView.div;

                var wrap = e.sender.element.closest('.s-picker-wrap');
                var divBox = div.parent();

                var topnum = wrap.offset().top + wrap.height();
                var boxH = 403;
                var winH = $(window).height();

                if (topnum + boxH > winH) {
                    if (wrap.offset().top > (boxH)) {
                        $(div).css('top', '0');
                    } else {
                        $(div).css('top', (winH - (topnum + boxH) + 3) + 'px');
                    }

                } else {
                    $(div).css('top', '0');
                }

                self.trigger(smat.event.OPEN, e);
            }

            if (this.config.format == undefined) uiConfig.format = "yyyy/MM/dd";
            if (this.config.max == undefined) uiConfig.max = new Date(9999, 12, 31);

            //culture
            //var lang = smat.service.getSysLanguage();
            //if (lang.indexOf("zh") >= 0) {
            //    uiConfig.culture = "zh";
            //} else if (lang.indexOf("ja") >= 0) {
            //    uiConfig.culture = "ja";
            //} else {
            //    uiConfig.culture = "en";
            //}

            $(this.config.target).asmatDatePicker(uiConfig);

            this.uiControl = $(this.config.target).data("asmatDatePicker");

            $(this.uiControl.element).attr('uuid', uuid);
            $(this.uiControl.element).attr('maxlength', "10");
            $(this.uiControl.element).keypress(function (event) {
                var eventObj = event || e;
                var keyCode = eventObj.keyCode || eventObj.which;

                if (keyCode == 13) {
                    $(this).formatCalendar(true);
                    return true;
                }

                if ((keyCode >= 48 && keyCode <= 57) || keyCode == 47)
                    return true;
                else
                    return false;
            }).focus(function () {
                this.style.imeMode = 'disabled';
                $(this).select();
            }).bind("paste", function () {
                var clipboard = window.clipboardData.getData("Text");
                if (/^(\d|\/)+$/.test(clipboard))
                    return true;
                else
                    return false;
            }).bind("blur", function (e) {
                var ok = $(this).formatCalendar(true);
                if ($(this).attr('data-error') == "error") {
                    //终止事件
                    e.preventDefault();
                    e.stopPropagation();    //标准   
                    e.cancelBubble = true;  //IE    
                    //e.stopPropagation();
                    //e.isDefaultPrevented();
                    //e.isImmediatePropagationStopped();
                    //e.stopImmediatePropagation();
                    //e.isPropagationStopped();
                    return false;
                }
            });

            if (this.cBool(uiConfig.enable) == false) {
                this.enable(this.cBool(uiConfig.enable));
            }

            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();

        }, focus: function () {
            $(this.uiControl.element).focus();
        }, visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).closest('.s-datepicker').hide();
                if (this.config.label != undefined) {
                    this.labelDom.hide();
                }
            } else {
                $(this.config.target).closest('.s-datepicker').show();
                if (this.config.label != undefined) {
                    this.labelDom.show();
                }
            }
        }, value: function (value) {
            if (this.uiControl != undefined) {
                if (value == undefined) {
                    return this.uiControl.value();
                } else {
                    this.uiControl.value(asmat.parseDate(value))
                }
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.DatePicker, smat.UI);
})();