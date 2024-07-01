
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Section
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.TabPage = function (config) {
        //默认属性
        this.setConfig({
            type: "Section"
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

        this.initUiControl();

        return this;
    };

    smat.dynamics.mobile.TabPage.prototype = {
        getUiId: function () {
            var id = this.config.name;
            return id;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.TabPage, smat.dynamics.mobile.View);
})();