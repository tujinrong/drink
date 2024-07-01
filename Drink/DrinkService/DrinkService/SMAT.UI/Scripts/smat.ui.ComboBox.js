(function () {
    ///////////////////////////////////////////////////////////////////////
    //  ComboBox
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatComboBox = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.ComboBox(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.ComboBox = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            uitype: "asmatComboBox"
        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        //共通初始化后
        //this.afterInit();

        return this;
    };

    smat.ComboBox.prototype = {

        
    };
    // extend Node
    smat.globalObject.extend(smat.ComboBox, smat.DropDownList);
})();