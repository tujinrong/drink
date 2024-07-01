(function () {
    ///////////////////////////////////////////////////////////////////////
    //  TimePicker
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatTimePicker = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.TimePicker(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.TimePicker = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

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

    smat.TimePicker.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.TimePicker.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            var uiConfig = smat.globalObject.clone(this.config);
            if (uiConfig.value == "null") {
                uiConfig.value = undefined;
            }

            uiConfig.change = function (e) {
                self.trigger(smat.event.CHANGE, e);
            }

            uiConfig.animation = false;

            uiConfig.open = function (e) {
                
                self.list = e.sender.timeView.list;

                if (self.list.find(".s-time-pick-box").length == 0) {
                    //var box = $("<div class='s-time-pick-box'>333</div>").appendTo($(list));

                    self.pickBox = $('<div  class="s-widget s-calendar s-time-pick-box"><div class="s-header"><a href="javascript:void(0)" role="button" class="s-link s-nav-prev" ><span class="s-icon s-i-arrow-w"></span></a><a href="javascript:void(0)" role="button" aria-live="assertive" aria-atomic="true" class="s-link s-nav-fast" data-value="">2016</a><a href="javascript:void(0)" role="button" class="s-link s-nav-next" ><span class="s-icon s-i-arrow-e"></span></a></div><table class="s-content s-meta-view" cellspacing="0" style="position: static; top: 0px; left: 0px; transform-origin: 166px 111px 0px; transform: scale(1);" ><tbody></tbody></table><div class="s-footer"><a href="javascript:void(0)" class="s-link s-nav-today" title="">　</a></div></div>').appendTo($(self.list));

                    self.pickHeader = self.pickBox.children("div.s-header");
                    self.pickBtnPrev = self.pickHeader.children("a.s-nav-prev");
                    self.pickBtnFast = self.pickHeader.children("a.s-nav-fast");
                    self.pickBtnNext = self.pickHeader.children("a.s-nav-next");
                    self.pickTable = self.pickBox.children("table");
                    self.pickFooter = self.pickBox.children("div.s-footer");
                    self.pickBtnNow = self.pickFooter.children("a.s-nav-today");


                    self.list.find("ul").hide();


                    self._setPickInfo();
                    self.pickBtnNow.text(asmat.toString(new Date(), "HH:mm"));
                } else {
                    //self._setPickerValue();
                    //hh evetime open set hh
                    self._initHhPickItems();
                }

                //$(list).width((self.pickTable.width()+5)+'px');
                $(self.list).width('272px');

                //if ($(self.list).closest(".s-animation-container").length > 0) {
                //    e.preventDefault();
                //    $(self.list).show();
                //    self.pickBox.show();

                //    $(self.list).closest(".s-list-container").show();
                //    $(self.list).closest(".s-animation-container").show();
                //    $(self.list).closest(".s-animation-container").css("position", "absolute");
                //    $(self.list).closest(".s-animation-container").css("top", "700px");
                //} 

                self.trigger(smat.event.OPEN, e);
            }

            $(this.config.target).asmatTimePicker(uiConfig);

            this.uiControl = $(this.config.target).data("asmatTimePicker");

            //if (this.config.format == "HH:mm") {
            //    var wrap = $(this.config.target).closest(".s-picker-wrap");
            //    wrap.find(".s-i-calendar").remove();
            //    wrap.find(".s-select").css("width", "1.9em");
            //    wrap.css("padding-right", "1.9em");
            //}

            $(this.uiControl.element).attr('maxlength', "8");
            $(this.uiControl.element).keypress(function (event) {
                var eventObj = event || e;
                var keyCode = eventObj.keyCode || eventObj.which;

                if (keyCode == 13) {
                    $(this).formatTime(uiConfig.format,true);
                    return true;
                }

                if ((keyCode >= 48 && keyCode <= 57) || keyCode == 47 || keyCode == 58)
                    return true;
                else
                    return false;
            }).focus(function () {
                this.style.imeMode = 'disabled';
                $(this).select();
            }).bind("paste", function () {
                var clipboard = window.clipboardData.getData("Text");
                if (/^(\d|:)+$/.test(clipboard))
                    return true;
                else
                    return false;
            }).bind("blur", function (e) {
                var ok = $(this).formatTime(uiConfig.format, true);
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
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();

        }, _setPickInfo: function () {
            var self = this;

            if (this.value() && false)
            {
                var hh = asmat.toString(self.value(), "HH");
                self._setHhPickerInfo(hh);
                //mm
                self._initMmPickItems();


            } else {
                //hh
                self._initHhPickItems();
            }



            //event;

            self.pickBtnPrev.bind("mouseover", function () {
                $(this).addClass("s-state-hover");
            });

            self.pickBtnPrev.bind("mouseout", function () {
                $(this).removeClass("s-state-hover");
            });

            self.pickBtnPrev.bind("click", function () {
                var hh = Number(self.pickBtnFast.attr("data-value"));
                self._setHhPickerInfo(hh - 1);
                self._initMmPickItems();
            });

            self.pickBtnNext.bind("mouseover", function () {
                $(this).addClass("s-state-hover");
            });

            self.pickBtnNext.bind("click", function () {
                var hh = Number(self.pickBtnFast.attr("data-value"));
                self._setHhPickerInfo(hh + 1);
                self._initMmPickItems();
            });

            self.pickBtnNext.bind("mouseout", function () {
                $(this).removeClass("s-state-hover");
            });

            self.pickBtnFast.bind("mouseover", function () {
                if ($(this).attr("data-pick-type") == "mm") {
                    $(this).addClass("s-state-hover");
                }
            });

            self.pickBtnFast.bind("mouseout", function () {
                $(this).removeClass("s-state-hover");
            });


            self.pickBtnFast.bind("click", function () {
                if ($(this).attr("data-pick-type") == "mm") {
                    self.pickBtnFast.attr("data-pick-type", "hh");
                    self.pickBtnFast.addClass("s-state-disabled");
                    self._initHhPickItems();
                }
            });

            self.pickBtnNow.bind("click", function () {
                var timeVal = $(this).text();
                self.uiControl.value(timeVal);
                $(self.list).closest(".s-animation-container").hide();
                self._initMmPickItems();
            });


            $(self.pickBox).on('click', 'td[role] a', function (e) {
                var role = $(this).attr("role");
                if (role == "hh") {
                    var hh = $(this).attr("data-value");
                    self._setHhPickerInfo(hh);
                    self._initMmPickItems();

                } else if (role == "mm") {
                    var mm = $(this).attr("data-value");
                    self._pickTimeValue(mm);
                }
            });

            $(self.pickBox).on('mouseover', 'td[role]', function (e) {
                $(this).addClass("s-state-hover");
            });

            $(self.pickBox).on('mouseout', 'td[role]', function (e) {
                $(this).removeClass("s-state-hover");
            });
        }, _initHhPickItems: function () {
            var self = this;
            self.pickTable.children().remove();
            var tbody = $("<tbody class='s-hh-box'></tbody>").appendTo(self.pickTable);
            
            var minHh = 0;
            var maxHh = 23;
            if (this.config.maxHh) maxHh = Number(this.config.maxHh);
            if (this.config.minHh) minHh = Number(this.config.minHh);

            var cellCount = 0;
            var domStr = "";
            for (var row = 0; row < 4; row++) {
                domStr += '<tr role="row">';
                for (var col = 0; col < 6; col++) {
                    var cellValue = cellCount < 10 ? "0" + cellCount : cellCount;
                    if (cellCount < minHh || cellCount > maxHh) {
                        domStr += '<td ><span>&nbsp;</span></td>';
                    } else {
                        domStr += '<td role="hh" ><a class="s-link" href="javascript:void(0)" role="hh" data-value="' + cellValue + '">' + cellValue + '</a></td>';
                    }

                    cellCount++;
                }

                domStr += '</tr>';
            }
            $(domStr).appendTo(tbody);

            //var tds = self.pickTable.find("td[role]");
            //$.each(tds, function () {
            //    $(this).bind("mouseover", function () {
            //        $(this).addClass("s-state-hover");
            //    });

            //    $(this).bind("mouseout", function () {
            //        $(this).removeClass("s-state-hover");
            //    });

            //    $(this).find("a").bind("click", function () {
            //        var hh = $(this).attr("data-value");
            //        self._setHhPickerInfo(hh);
            //        self._initMmPickItems();
            //    });
            //});

            self.pickBtnFast.attr("data-value", "");
            self.pickBtnFast.text(minHh + "-" + maxHh);
            self.pickBtnFast.attr("data-pick-type", "hh");
            self.pickBtnPrev.hide();
            self.pickBtnNext.hide();

        }, _initMmPickItems: function () {
            var self = this;
            self.pickTable.children().remove();
            var tbody = $("<tbody class='s-mm-box'></tbody>").appendTo(self.pickTable);

            var minHh = 0;
            var maxHh = 23;
            if (this.config.maxHh) maxHh = Number(this.config.maxHh);
            if (this.config.minHh) minHh = Number(this.config.minHh);

            var minMm = 0;
            var maxMm = 59;

            if (this.config.maxHh && this.hh == maxHh) maxMm = Number(this.config.maxMm);
            if (this.config.minHh && this.hh == minHh) minMm = Number(this.config.minMm);

            var cellCount = 0;
            var domStr = "";
            for (var row = 0; row < 6; row++) {
                domStr += '<tr role="row">';
                for (var col = 0; col < 10; col++) {
                    var cellValue = cellCount < 10 ? "0" + cellCount : cellCount;

                    if (cellCount < minMm || cellCount > maxMm) {
                        domStr += '<td ><span>&nbsp;</span></td>';
                    } else {
                        domStr += '<td role="mm" ><a class="s-link" href="javascript:void(0)" role="mm" data-value="' + cellValue + '">' + cellValue + '</a></td>';
                    }

                    cellCount++;
                }

                domStr += '</tr>';
            }
            $(domStr).appendTo(tbody);

            //var tds = self.pickTable.find("td[role]");
            //$.each(tds, function () {
            //    $(this).bind("mouseover", function () {
            //        $(this).addClass("s-state-hover");
            //    });

            //    $(this).bind("mouseout", function () {
            //        $(this).removeClass("s-state-hover");
            //    });

            //    $(this).find("a").bind("click", function () {
            //        var mm = $(this).attr("data-value");
            //        self._pickTimeValue(mm);
            //    });
            //});

            self._setValueOnPicker();
        }, _setHhPickerInfo: function (hh) {
            var self = this;
            var hhNum = Number(hh);
            this.hh = hhNum;
            var hhStr = hhNum < 10 ? "0" + hhNum : hhNum;
            self.pickBtnFast.attr("data-value", hhStr);
            self.pickBtnFast.text(hhStr + "時");

            self.pickBtnFast.attr("data-pick-type", "mm");

            var minHh = 0;
            var maxHh = 23;
            if (this.config.maxHh) maxHh = Number(this.config.maxHh);
            if (this.config.minHh) minHh = Number(this.config.minHh);

            if (hhNum == minHh) {
                self.pickBtnPrev.hide();
                self.pickBtnNext.show();
            } else if (hhNum == maxHh) {
                self.pickBtnPrev.show();
                self.pickBtnNext.hide();
            } else {
                self.pickBtnPrev.show();
                self.pickBtnNext.show();
            }

            self._setValueOnPicker();

        }, _pickTimeValue: function (mm) {

            var self = this;
            self.pickTable.children(".s-mm-box").find("td.s-state-selected").removeClass("s-state-selected");
            self.pickTable.children(".s-mm-box").find("a[data-value='" + mm + "']").parent().addClass("s-state-selected");

            var hh = self.pickBtnFast.attr("data-value");

            this.uiControl.value(hh + ":" + mm);
            self.trigger(smat.event.CHANGE, {});
            self.pickBtnNow.text(hh + ":" + mm);
            $(self.list).closest(".s-animation-container").hide();

        }, _setValueOnPicker: function () {
            var self = this;
            self.pickTable.children(".s-mm-box").find("td.s-state-selected").removeClass("s-state-selected");
            self.pickBtnFast.removeClass("s-state-disabled");
            var hh = asmat.toString(self.value(), "HH");
            var mm = asmat.toString(this.value(), "mm");

            if (hh == self.pickBtnFast.attr("data-value")) {
                self.pickTable.children(".s-mm-box").find("a[data-value='" + mm + "']").parent().addClass("s-state-selected");
            }
        }, _setPickerValue: function () {
            var self = this;
            var hh = asmat.toString(self.value(), "HH");
            self._setHhPickerInfo(hh);
            self._setValueOnPicker();
            self.pickBtnNow.text(asmat.toString(new Date(), "HH:mm"));
        }
    };
    // extend Node
    smat.globalObject.extend(smat.TimePicker, smat.UI);
})();