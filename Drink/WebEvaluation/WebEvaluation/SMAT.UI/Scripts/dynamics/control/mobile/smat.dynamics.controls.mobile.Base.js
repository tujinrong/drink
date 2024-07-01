
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  mobile Base
    ///////////////////////////////////////////////////////////////////////
    smat.dynamics.mobile = {};

    ///////////////////////////////////////////////////////////////////////
    //  Base
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Base = function (config) {
        //默认属性
        this.setConfig({
            type: "Base"
        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.dynamics.mobile.Base.prototype = {
        
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Base, smat.dynamics.Field);
})();