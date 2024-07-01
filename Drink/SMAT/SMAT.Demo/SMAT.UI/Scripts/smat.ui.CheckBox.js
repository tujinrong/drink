(function () {
    ///////////////////////////////////////////////////////////////////////
    //  CheckBox
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatCheckBox = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.CheckBox(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.CheckBox = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            boxtype: "checkbox"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.CheckBox.prototype = {

        
    };
    // extend Node
    smat.globalObject.extend(smat.CheckBox, smat.RadioBox);
})();