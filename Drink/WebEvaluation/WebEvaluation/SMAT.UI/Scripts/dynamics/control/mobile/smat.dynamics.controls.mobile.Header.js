
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Header
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Header = function (config) {
        //默认属性
        this.setConfig({
            type: "Header"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.mobile.Header.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {



        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Header, smat.dynamics.mobile.Base);
})();