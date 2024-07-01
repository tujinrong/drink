(function () {
    ///////////////////////////////////////////////////////////////////////
    //  TextBox
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatTextBox = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.TextBox(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.TextBox = function (config) {
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

    smat.TextBox.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.TextBox.prototype
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
            $(this.config.target).asmatMaskedTextBox(uiConfig);

            this.uiControl = $(this.config.target).data("asmatMaskedTextBox");
            if (this.config.enable == false) {
                this.uiControl.enable(false);
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

            $(this.config.target).bind('focus', function () {
                var node = $(this);
                setTimeout(function () { node.select(); }, 1);
            })

            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }
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

        },focus: function () {
            if (this.config.target != undefined) {
                this.config.target.focus();
            }
        },visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).hide();
            } else {
                $(this.config.target).show();
            }
            if (this.config.label != undefined) {
                this.labelDom.hide();
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.TextBox, smat.UI);
})();