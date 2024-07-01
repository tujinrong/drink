(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DropDownList
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileDropDownList = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.DropDownList(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.DropDownList = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            uitype: "asmatDropDownList"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.DropDownList.prototype = {
        initLable: function () {

            if (this.config.label != undefined) {


                var requiredMark = "";
                //required
                if (smat.global.requiredLabelMark && this.config.required != undefined && this.config.required == "true") {
                    requiredMark = "<span class='s-required-mark'>*</span>";
                }

                this.labelDom = $('<label>' + requiredMark + smat.service.cultureText(this.config.label.text) + '</label>');


                if (this.config.label.attrs != undefined) {
                    for (var aKey in this.config.label.attrs) {
                        this.labelDom.attr(aKey, this.config.label.attrs[aKey]);
                    }

                }
                $(this.config.target).replaceWith(this.labelDom);

                $(this.config.target).appendTo(this.labelDom);

            }

        }, initUiControl: function (uiConfig) {

            var body = $(this.config.target).closest(".s-dy-page");

            uiConfig.popup ={ appendTo: body }
            uiConfig.animation = { open: { effects: body.hasClass("km-android") ? "fadeIn" : body.hasClass("km-ios") || body.hasClass("km-wp") ? "slideIn:up" : "slideIn:down" } }

            if (this.config.uitype == "asmatDropDownList") {
                $(this.config.target).asmatDropDownList(uiConfig);

            } else if (this.config.uitype == "asmatComboBox") {
                $(this.config.target).asmatComboBox(uiConfig);

            } else if (this.config.uitype == "asmatMultiSelect") {
                $(this.config.target).asmatMultiSelect(uiConfig);

            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.mobile.DropDownList, smat.DropDownList);
})();