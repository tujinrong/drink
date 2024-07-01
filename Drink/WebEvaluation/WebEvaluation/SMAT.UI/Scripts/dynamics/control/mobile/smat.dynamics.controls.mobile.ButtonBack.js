
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  ButtonBack
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.ButtonBack = function (config) {
        //默认属性
        this.setConfig({
            type: "ButtonBack"
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

    smat.dynamics.mobile.ButtonBack.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.ButtonBack, smat.dynamics.mobile.Base);
})();