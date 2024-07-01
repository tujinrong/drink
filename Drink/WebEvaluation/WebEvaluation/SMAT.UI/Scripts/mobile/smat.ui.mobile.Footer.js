(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Footer
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileFooter = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.Footer(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.Footer = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "footer"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.Footer.prototype = {


    };
    // extend Node
    smat.globalObject.extend(smat.mobile.Footer, smat.mobile.Base);
})();