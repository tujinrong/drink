(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DyPage
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDyPage = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.DyPage(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.DyPage = function (config) {
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

    smat.DyPage.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Button.prototype
         */
        init: function () {


            var self = this;

            this.uuid = smat.service.uuid();

            $(this.config.target).attr('role', "dy-page");
            $(this.config.target).attr('uuid', this.uuid);
            smat.global.uiMap.set(this.uuid, this);

            this.loaded = false;

            if (this.cBool(this.config.visible) == false) {
                this.visible(false);
            }

            if (this.cBool(this.config.autoLoad) == true) {
                this.load();
            }

        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            if (this.uiControl) {
                this.uiControl.destroy();
            }

        },
        visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).hide();
            } else {
                $(this.config.target).show();
            }
        }, load: function (pageParams,afterInit) {
            if (this.loaded == true) {
                return;
            }
            this.clear();
            if (this.config.pageName && this.config.pageName != "") {
                this.dyPage = smat.dynamics.openPage({
                    projID: this.config.ProjID,
                    formName: this.config.pageName,
                    entityName: this.config.entity,
                    contextOn: $(this.config.target),
                    parentPageId:this.config.parentPageId,
                    pageParams: pageParams,
                    afterInit: afterInit
                });

                this.page = new smat.pagerSender({
                    dynamics: true,
                    EntityName: this.config.entity,
                    PageName: this.config.pageName,
                    ProjID: this.config.ProjID,
                    PageId: "page_" + this.dyPage.uuid,
                    parentPageId: this.config.parentPageId,
                    pageParams: pageParams
                });

                this.loaded = true;
            }
        }, clear: function () {
            var uis = $(this.config.target).find('[uuid]');
            $.each(uis, function () {
                if ($(this).ui()) {
                    $(this).ui().destroy();
                }

            });

            $(this.config.target).children().remove();
            this.loaded = false;
        }, visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).css("display", "none");
            } else {
                $(this.config.target).css("display", "block");
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.DyPage, smat.UI);
})();