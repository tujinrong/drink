(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Header
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileHeader = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.Header(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.Header = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "header"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.Header.prototype = {


    };
    // extend Node
    smat.globalObject.extend(smat.mobile.Header, smat.mobile.Base);
})();