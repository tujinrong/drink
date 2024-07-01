(function () {
    ///////////////////////////////////////////////////////////////////////
    //  NumericTextBox
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatNumericTextBox = function (config) {

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

        //初期化
        this.init();

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

            $(this.config.target).asmatNumericTextBox(this.config);

            this.uiControl = $(this.config.target).data("asmatNumericTextBox");

            this.maxLength = $(this.config.target).attr('maxLength');

            if (this.maxLength != undefined) this.maxLength = Number(this.maxLength);

            if (this.config.pick == true) {
                this.speed = 300;
                this.displayInput = $(this.config.target);
                this.initPicker();
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();
        }, initPicker: function () {
            this.box = $(this.config.target).closest(".s-numerictextbox");
            this.initAnimationDom();
            this.initAnimationItem();


        }, initAnimationDom: function () {

            this.animation = $('<div class="s-calendar-container s-popup s-group s-reset" id="monthpicker_dateview" data-role="popup" style="display:none; font-size: 14px; font-family: Arial, Helvetica, sans-serif; font-stretch: normal; font-style: normal; font-weight: 500; line-height: normal; position: absolute; z-index:99999;"></div>').appendTo($('body'));
            this.animationBox = $('<div class="s-widget s-calendar" style="width: 212px;"></div>').appendTo(this.animation);
            this.animationHeader = $('<div class="s-header" style="height:45px;text-align: left;"><input class="s-input s-textbox" style="margin-top:5px; margin-left:5px; width:170px;float: left;" value="0" /><a href="#" role="button" class="s-link" aria-disabled="false" style="margin-left:11px;float: left;padding: 13px 0;"><span role="presentation" class="s-icon s-i-close" style="height: 20px;">Close</span></a></div>').appendTo(this.animationBox);
            this.animationTable = $('<div style="padding: 0 10px 10px 10px;float: left;"></div>').appendTo(this.animationBox);
            for (var i = 1; i < 11 ; i++) {
                var no = i;
                if (no == 10) no = 0;
                $('<a tabindex="-1" class="s-link" href="#" style="border: 1px rgb(76, 182, 203) solid; padding: 16px 20px;margin: 10px 0 0 10px; border-radius: 50%;outline-style: none; float:left;">' + no + '</a>').appendTo(this.animationTable);
            }

            this.btnPickOk = $('<a tabindex="-1" class="pick-ok" href="#" style=" padding: 16px 16px;margin: 10px 0 0 10px; border-radius: 50%;outline-style: none; float:left;">OK</a>').appendTo(this.animationTable);
            this.btnPickClear = $('<a tabindex="-1" class="pick-clear" href="#" style=" padding: 16px 2px;margin: 10px 0 0 10px; border-radius: 50%;outline-style: none; float:left;">クリア</a>').appendTo(this.animationTable);

            this.btnPickShow = $(this.config.target).closest(".s-numeric-wrap").find(".s-select");
            this.btnPickShow.children().remove();
            this.btnPickShow.attr("style", "cursor:pointer;")
            this.input = this.animationHeader.find(".s-textbox");

            $('<span class="s-icon s-i-pencil" unselectable="on" style="height:18px;"></span>').appendTo(this.btnPickShow);

        }, doAnimation: function () {
            this.doOpenAnimation();
        }, initAnimationItem: function () {
            var self = this;
            var items = this.animationTable.find(".s-link");

            $.each(items, function (n, value) {
                $(this).bind('mouseover', function (e) {
                    if ($(this).hasClass('s-state-hover') == false) {
                        $(this).addClass('s-state-hover');
                    }
                });

                $(this).bind('mouseout', function (e) {
                    $(this).removeClass('s-state-hover');
                });

                $(this).bind('click', function (e) {
                    self.pickNumber($(this).text());
                });
            });

            var btnClose = this.animationHeader.find("a");
            $(btnClose).bind('click', function (e) {
                self.doCloseAnimation();
            });

            this.btnPickOk.bind('click', function (e) {
                self.pickOk();
            });

            this.btnPickClear.bind('click', function (e) {
                self.pickClear();
            });

            this.btnPickShow.bind('click', function (e) {
                self.doOpenAnimation();
            });

            this.animationBox.bind('blur', function (e) {
                self.doOpenAnimation();
                return false;
            });

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

                    var overlay = $('<div class="s-overlay" style="display: block; z-index: 10002; opacity: 0;"></div>').appendTo($('body'));
                    overlay.bind('click', function () {
                        self.doCloseAnimation();
                    });
                });

            }

        }, doCloseAnimation: function () {
            var self = this;
            $(".s-overlay").remove();
            this.animation.fadeOut(this.speed, function () { self.uiControl.enable(true); });
        }, pickOk: function () {
            var self = this;
            self.uiControl.value(this.input.val());
            self.doCloseAnimation();
        }, pickClear: function () {
            var self = this;
            this.input.val("");
            self.uiControl.value("");
            self.doCloseAnimation();

        }, setConfig: function (config) {
            if (this.config === undefined) {
                this.config = {};
            }
            // set properties from config
            if (config) {
                for (var key in config) {
                    var val = config[key];
                    // handle special keys

                    this.config[key] = config[key];
                }
            }
        }, pickNumber: function (number) {
            var self = this;
            var input = this.animationHeader.find(".s-textbox");
            var valueNow = input.val();
            var valueNew = valueNow + number;

            //check max
            if (self.uiControl.options.max != null) {
                if (Number(valueNew) > self.uiControl.options.max) {
                    //input.focus();
                    return;
                }
            }

            //max length 
            if (this.maxLength != undefined) {
                if (valueNew.length > this.maxLength) {
                    //input.focus();
                    return;
                }
            }

            input.val(valueNew);
            //self.uiControl.value(valueNew);
            //input.focus();

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
})();