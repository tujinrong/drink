(function () {
    ///////////////////////////////////////////////////////////////////////
    //  MultiSelect
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMultiSelect = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.MultiSelect(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.MultiSelect = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            uitype: "asmatMultiSelect"
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

    smat.MultiSelect.prototype = {

        
    };
    // extend Node
    smat.globalObject.extend(smat.MultiSelect, smat.DropDownList);
})();