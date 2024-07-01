(function () {
    ///////////////////////////////////////////////////////////////////////
    //  TextArea
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileTextArea = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.TextArea(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.TextArea = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();


        $(this.config.target).removeClass("s-textbox")

        return this;
    };

    smat.mobile.TextArea.prototype = {
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

        }
    };
    // extend Node
    smat.globalObject.extend(smat.mobile.TextArea, smat.TextArea);
})();