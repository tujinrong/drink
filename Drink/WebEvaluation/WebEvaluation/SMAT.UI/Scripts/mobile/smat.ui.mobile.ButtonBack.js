(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Button
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileButtonBack = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.ButtonBack(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.ButtonBack = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "backbutton"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.ButtonBack.prototype = {


    };
    // extend Node
    smat.globalObject.extend(smat.mobile.ButtonBack, smat.mobile.Base);
})();