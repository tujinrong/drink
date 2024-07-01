(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Layout
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileLayout = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.Layout(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.Layout = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "layout"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.Layout.prototype = {
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


            this.config.init = "smat.mobile.service.layoutInit";
            this.setMobileProperty();
            //var uiConfig = smat.globalObject.clone(this.config);

            //uiConfig.name = undefined;
            //$(this.config.target).asmatMobileLayout(uiConfig);

        },
        onUiInit: function () {

            //this.initEvent();

        },
        initEvent: function () {

            if (this.config.designing) {
                return;
            }

            //this.getUiControl();

            //if (this.uiControl) {
            //    this.uiControl.bind("show", function (e) {
            //        debugger;
            //    })
            //}

        }

    };
    // extend Node
    smat.globalObject.extend(smat.mobile.Layout, smat.mobile.Base);
})();