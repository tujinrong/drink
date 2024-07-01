(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Button
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileButton = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.Button(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.Button = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "button"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.Button.prototype = {
        
        /**
         * 初期化
         * @name init
         * @methodOf smat.Button.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            //this.setMobileProperty();
            if (this.config.align) {
                $(this.config.target).attr('data-align', this.config.align);
            }
            var uiConfig = smat.globalObject.clone(this.config);
            if (uiConfig.print == true) {
                uiConfig.visible = false;
            }

            //uiConfig.text = smat.service.cultureText(uiConfig.text);

            uiConfig.click = function (e) {
                self.trigger(smat.event.CLICK, e);
            }

            uiConfig.name = undefined;
            $(this.config.target).asmatMobileButton(uiConfig);

        }
    };
    // extend Node
    smat.globalObject.extend(smat.mobile.Button, smat.mobile.Base);
})();