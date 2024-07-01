(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Tabstrip
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileTabstrip = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.Tabstrip(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.Tabstrip = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "tabstrip"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.Tabstrip.prototype = {
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

            this.setMobileProperty();
            //var uiConfig = smat.globalObject.clone(this.config);

            //uiConfig.name = undefined;
            //$(this.config.target).asmatMobileTabStrip(uiConfig);

            //setTimeout(function () {

            //    self.getUiControl();
            //    self.uiControl.bind("select", function (e) {
            //    })

            //}, 1000);

        }, getUiControl: function () {
            if (!this.uiControl) {
                this.uiControl = $(this.config.target).data("asmatMobileTabStrip")
            }
        }, badge: function (index, num) {
            this.getUiControl();
            this.uiControl.badge(index, num);
        }

    };
    // extend Node
    smat.globalObject.extend(smat.mobile.Tabstrip, smat.mobile.Base);
})();