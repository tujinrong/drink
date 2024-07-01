(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Input
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileInput = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.Input(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.Input = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            dataType: "text"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.Input.prototype = {
        
        /**
         * 初期化
         * @name init
         * @methodOf smat.Input.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            //this.setMobileProperty();
            var uiConfig = smat.globalObject.clone(this.config);
            if (uiConfig.print == true) {
                uiConfig.visible = false;
            }

            //uiConfig.text = smat.service.cultureText(uiConfig.text);

            uiConfig.click = function (e) {
                self.trigger(smat.event.CLICK, e);
            }

            uiConfig.name = undefined;

            $(this.config.target).attr('type', this.config.dataType);

            var placeholderStr = this.config.dataType;
            if (this.config.placeholder) {
                placeholderStr = this.config.placeholder;
            }
            $(this.config.target).attr('placeholder', placeholderStr);
            

        },
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
    smat.globalObject.extend(smat.mobile.Input, smat.mobile.Base);
})();