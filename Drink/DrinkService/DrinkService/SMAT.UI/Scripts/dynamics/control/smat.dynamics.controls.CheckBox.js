
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  RadioBox
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.CheckBox = function (config) {
        //默认属性
        this.setConfig({
            type: "CheckBox"
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

    smat.dynamics.CheckBox.prototype = {
        initUI: function (uiConfig) {
            this.uiControl = new smat.CheckBox(uiConfig);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.CheckBox, smat.dynamics.RadioBox);
})();