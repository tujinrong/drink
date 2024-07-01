(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Button
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatButton = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Button(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.Button = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.Button.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Button.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            var uiConfig = smat.globalObject.clone(this.config);

            //uiConfig.text = smat.service.cultureText(uiConfig.text);

            uiConfig.click = function (e) {
                self.trigger(smat.event.CLICK,e);
            }
            $(this.config.target).asmatButton(uiConfig);

            this.uiControl = $(this.config.target).data("asmatButton");

            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            if (this.uiControl) {
                this.uiControl.destroy();
            }

        }, enable: function (enable) {
            this.uiControl.enable(enable);

        },click:function(){
            $(this.config.target).click();
        },
        visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).hide();
            } else {
                $(this.config.target).show();
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Button, smat.UI);
})();