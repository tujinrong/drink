(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Label
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatLabel = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Label(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Label = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            label: {
                text:"label"
            }

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

    smat.Label.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Label.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            var uiConfig = smat.globalObject.clone(this.config);
           

            $(this.config.target).hide();
            $(this.config.target).appendTo(this.labelDom);

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
            //this.uiControl.destroy();

        },focus: function () {
            if (this.config.target != undefined) {
                this.config.target.focus();
            }
        },visible: function (visibleFlag) {
            if (visibleFlag == false) {
                if (this.config.label != undefined) {
                    this.labelDom.hide();
                }
            } else {
                if (this.config.label != undefined) {
                    this.labelDom.show();
                }
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Label, smat.UI);
})();