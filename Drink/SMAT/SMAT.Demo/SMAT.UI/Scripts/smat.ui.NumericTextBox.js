(function () {
    ///////////////////////////////////////////////////////////////////////
    //  NumericTextBox
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatNumericTextBox = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.NumericTextBox(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.NumericTextBox = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            format: "n0"

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

    smat.NumericTextBox.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.NumericTextBox.prototype
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

            $(this.config.target).asmatNumericTextBox(uiConfig);

            this.uiControl = $(this.config.target).data("asmatNumericTextBox");

            this.maxLength = $(this.config.target).attr('maxLength');

            if (this.maxLength == undefined) this.maxLength = this.config.maxLength;

            if (this.maxLength != undefined) this.maxLength = Number(this.maxLength);

            this.width = $(this.config.target).attr('width');

            if (this.width == undefined) this.width = this.config.width;

            if (this.width != undefined) this.width = Number(this.width);

            if (this.config.select == false){
                this.initSelect();
            }

            //if (Modernizr.ios || Modernizr.android) {
            //    if (this.config.pick == true) {
            //        this.speed = 30;
            //        this.displayInput = $(this.config.target);
            //        this.initPicker();
            //    }

            //    //this.uiControl.wrapper.find('.s-formatted-value').attr('disabled', 'true');
            //};

            if (this.config.pick == true) {
                this.speed = 30;
                this.displayInput = $(this.config.target);
                this.initPicker();
            }

            $(this.uiControl.element).focus(function () {
                this.style.imeMode = 'disabled';
                var node = $(this);
                setTimeout(function () { node.select(); }, 1);
            });
        }, value: function (value) {
            if (value == undefined) {
                return this.uiControl.value();
            } else {
                this.uiControl.value(value);
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();
        }, initSelect: function () {
            this.wrap = $(this.config.target).closest(".s-numeric-wrap");
            this.selecter = $(this.wrap).find(".s-select");
            this.selecter.remove();
            $(this.wrap).css("padding-right", "1px");
        }, initPicker: function () {
            this.box = $(this.config.target).closest(".s-numerictextbox");
            this.initAnimationDom();
            this.initAnimationItem();
            

        }, initAnimationDom: function () {

            

            this.animation = $('<div class="s-calendar-container s-popup s-group s-reset" id="monthpicker_dateview" data-role="popup" style="display:none; font-size: 14px; font-family: Arial, Helvetica, sans-serif; font-stretch: normal; font-style: normal; font-weight: 500; line-height: normal; position: absolute; z-index:99999;"></div>').appendTo($('body'));
            this.animationBox = $('<div class="s-widget s-calendar" style="width: 210px;"></div>').appendTo(this.animation);
            this.animationHeader = $('<div class="s-header" style="height:45px;text-align: left;"><input class="s-input s-textbox" readonly="true" style="margin-top: 5px;margin-left: 5px;width: 200px;float: left;text-align: right;font-size: 28px;padding: 0 5px;line-height: 36px;height: 36px;" value="0" /></div>').appendTo(this.animationBox);
            this.animationTable = $('<div style="padding: 0 6px 8px 8px;float: left;"></div>').appendTo(this.animationBox);
            for (var i = 1; i < 11 ; i++) {
                var no = i;
                if (no == 10) no = 0;
                //$('<a tabindex="-1" class="s-link" href="#" style="border: 1px rgb(76, 182, 203) solid; padding: 16px 20px;margin: 10px 0 0 10px; border-radius: 50%;outline-style: none; float:left;">' + no + '</a>').appendTo(this.animationTable);
                $('<button class="btn-primary s-button s-button-pick" style="color:#000 !important;border: 1px rgb(76, 182, 203) solid;  padding: 10px 19px;font-size: 30px;  margin: 6px 0 0 6px;  border-radius: 50%;  outline-style: none;  float: left;">' + no + '</button>').appendTo(this.animationTable);
            }

            this.btnPickOk = $('<button tabindex="-1" class="pick-ok s-button btn-primary" href="#" style="padding: 10px 16px;margin: 14px 0 0 8px;background-color: #3F81FA;border-color: #3F81FA;float: left;">OK</button>').appendTo(this.animationTable);
            this.btnPickClear = $('<button tabindex="-1" class="pick-clear s-button btn-danger" href="#" style="padding: 10px 8px;margin: 14px 0 0 8px;background-color: #BE0000;border-color: #BE0000;float: left;">クリア</button>').appendTo(this.animationTable);

            this.btnPickShow = $(this.config.target).closest(".s-numeric-wrap").find(".s-select");
            this.btnPickShow.children().remove();
            this.btnPickShow.attr("style", "cursor:pointer;")
            this.input = this.animationHeader.find(".s-textbox");

            this.input.attr('maxLength',$(this.config.target).attr('maxLength'));
            
            $('<span class="s-icon s-i-pencil" unselectable="on" style="height:18px;"></span>').appendTo(this.btnPickShow);

        }, doAnimation: function () {
            this.doOpenAnimation();
        }, initAnimationItem: function () {
            var self = this;
            var items = this.animationTable.find(".s-button-pick");

            $.each(items, function (n, value) {
                $(this).bind(smat.event.click, function (e) {
                    self.pickNumber($(this).text());
                });
            });

            var btnClose = this.animationHeader.find("a");
            $(btnClose).bind(smat.event.click, function (e) {
                self.doCloseAnimation();
                e.preventDefault();
            });

            this.btnPickOk.bind(smat.event.click, function (e) {
                self.pickOk();
                e.preventDefault();
            });
           

            this.btnPickClear.bind(smat.event.click, function (e) {
                self.pickClear();
                e.preventDefault();
            });
           

            //this.btnPickShow.bind(smat.event.clickOtrouchend, function (e) {
            //    self.doOpenAnimation();
            //    return false;
            //});

            this.uiControl.wrapper.bind(smat.event.click, function (e) {
                self.doOpenAnimation();
                e.preventDefault();
                return false;
            });

            //this.animationBox.bind('blur', function (e) {
            //    self.doOpenAnimation();
            //    return false;
            //});

            //this.input.bind('blur', function (e) {
            //    if (self.animationTable.find(".s-state-hover").length > 0) {

            //    } else
            //    {
            //        self.doCloseAnimation();
            //    }
            //    return false;
            //});

        }, doOpenAnimation: function () {
            if (this.animation.is(":visible") == false) {
                var self = this;
                this.top = this.box.offset().top;
                this.left = this.box.offset().left;
                this.boxHeight = this.box.height();
                this.boxWidth = this.box.width();

                var wH = $(window).height();
                var bH = this.animation.height();

                if ((this.top + this.boxHeight + bH) > wH) {
                    this.animation.css("top", (this.top - bH) + "px");
                } else {
                    this.animation.css("top", (this.top + this.boxHeight) + "px");
                }

                this.animation.css("left", this.left + "px");
                
                this.input.val("");
               
                this.animation.fadeIn(this.speed, function () {
                    self.uiControl.enable(false);

                    var overlay = $('<div class="s-overlay" style="display: block; z-index: 20002; opacity: 0;"></div>').appendTo($('body'));
                    overlay.bind(smat.event.click, function () {
                        self.doCloseAnimation();
                    });
                });
                //this.input.focus();
            }

        }, doCloseAnimation: function () {
            var self = this;
            
            this.animation.fadeOut(this.speed, function () { self.uiControl.enable(true); $(".s-overlay").remove(); });
        }, pickOk: function () {
            var self = this;
            var oldValue = self.uiControl.value();
            self.uiControl.value(this.input.val());
            self.doCloseAnimation();
            if (this.config.change != undefined) {
                if ((String(oldValue == null ? "" : oldValue) != this.input.val() && (oldValue == null && this.input.val() == "") == false) == true) {
                    var e={};
                    e.sender = self.uiControl;
                    self.trigger(smat.event.CHANGE,e);
                }
                
            }
        }, pickClear: function () {
            var self = this;
            this.input.val("");
            //self.uiControl.value(null);
            //this.input.focus();

        }, pickNumber: function (number) {
            var self = this;
            
            var valueNow = this.input.val();
            var valueNew = valueNow + number;

            //check max
            if (self.uiControl.options.max != null) {
                if (Number(valueNew) > self.uiControl.options.max) {
                    //this.input.focus();
                    return;
                }
            }

            //max length 
            if (this.maxLength != undefined) {
                if (valueNew.length > this.maxLength) {
                    //this.input.focus();
                    return;
                }
            }

            this.input.val(valueNew);
            //self.uiControl.value(valueNew);
            //this.input.focus();

            //max length 
            if (this.maxLength != undefined) {
                if (valueNew.length == this.maxLength) {
                    self.pickOk();
                }
            }

            //check max
            if (self.uiControl.options.max != null) {
                if (Number(valueNew) == self.uiControl.options.max) {
                    self.pickOk();
                }
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.NumericTextBox, smat.UI);
})();