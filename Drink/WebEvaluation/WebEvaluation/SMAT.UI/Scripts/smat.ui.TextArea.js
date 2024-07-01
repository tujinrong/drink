(function () {
    ///////////////////////////////////////////////////////////////////////
    //  TextArea
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatTextArea = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.TextArea(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.TextArea = function (config) {
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

    smat.TextArea.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.TextArea.prototype
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

            if (this.config.enable == false || this.config.enable == 'false') {
                this.enable(false);
            }

            if (!$(this.config.target).hasClass("s-textbox")) {
                $(this.config.target).addClass("s-textbox");
            }

            if ($(this.config.target).hasClass("onlyNum")){
                $(this.config.target).onlyNum();
            } else if($(this.config.target).hasClass("onlyAlpha")) {
                $(this.config.target).onlyAlpha();
            } else if ($(this.config.target).hasClass("onlyHalfNumAlpha")) {
                $(this.config.target).onlyHalfNumAlpha();
            } else if ($(this.config.target).hasClass("onlyNumAlpha")) {
                $(this.config.target).onlyNumAlpha();
            } else if ($(this.config.target).hasClass("onlyNumAlphaMinus")) {
                $(this.config.target).onlyNumAlphaMinus();
            }

            
            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }

            if (uiConfig.cols && uiConfig.cols != "") {
                $(this.config.target).attr('cols', uiConfig.cols);
            }

            if (uiConfig.rows && uiConfig.rows != "") {
                $(this.config.target).attr('rows', uiConfig.rows);
            }

            if (uiConfig.resize == "none") {
                $(this.config.target).css('resize', "none");
            }

        }, value: function (value) {
            if (value == undefined) {
                return this.config.target.val();
            } else {
                this.config.target.val(value);
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));

        },focus: function () {
            if (this.config.target != undefined) {
                this.config.target.focus();
            }
        },visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).hide();
                if (this.config.label != undefined) {
                    this.labelDom.hide();
                }
            } else {
                $(this.config.target).show();
                if (this.config.label != undefined) {
                    this.labelDom.show();
                }
            }
        }, enable: function (ableFlag) {

            if (this.cBool(ableFlag) == false) {
                this.config.target.attr("readonly", "readonly");
                this.config.target.addClass("s-state-disabled");
            } else {
                this.config.target.removeAttr("readonly");
                this.config.target.removeClass("s-state-disabled");
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.TextArea, smat.UI);
})();