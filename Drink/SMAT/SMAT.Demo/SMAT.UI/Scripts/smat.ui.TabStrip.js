(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Button
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatTabStrip = function (config) {

        var uiNode = null;
        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            uiNode = new smat.TabStrip(config);
        });
        return uiNode;
    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.TabStrip = function (config) {
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

    smat.TabStrip.prototype = {

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
            this.params = undefined;

            var uiConfig = smat.globalObject.clone(this.config);
            uiConfig.name = undefined;

            uiConfig.activate = function (e) {
                self.trigger(smat.event.ACTIVATE, e);

                var curTab = $(self.config.target).children(".s-state-active");
                var dyPages = curTab.find('[role="dy-page"]');
                $.each(dyPages, function () {
                    $(this).ui().load(self.params);
                });
            }

            $(this.config.target).asmatTabStrip(uiConfig);

            this.uiControl = $(this.config.target).data("asmatTabStrip");

            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }

            //if ($(this.config.target).find('[role="dy-page"]').length > 0) {
            //    this.hasDyPage = true;
            //}

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            if (this.uiControl) {
                this.uiControl.destroy();
            }

        }, enable: function (enable) {
            this.uiControl.enable(enable);

        }, reLoad: function () {
            var self = this;
            var curTab = $(this.config.target).children(".s-state-active");
            var dyPagesAll = $(this.config.target).find('[role="dy-page"]');
            $.each(dyPagesAll, function () {
                $(this).ui().clear();
            });
            var dyPages = curTab.find('[role="dy-page"]');
            $.each(dyPages, function () {
                $(this).ui().load(self.params);
            });
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
    smat.globalObject.extend(smat.TabStrip, smat.UI);
})();