(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DyUserControl
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDyUserControl = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.DyUserControl(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.DyUserControl = function (config) {
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

    smat.DyUserControl.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.Button.prototype
         */
        init: function () {


            var self = this;

            this.uuid = smat.service.uuid();

            $(this.config.target).attr('role', "dy-userControl");
            $(this.config.target).attr('uuid', this.uuid);
            smat.global.uiMap.set(this.uuid, this);

            this.loaded = false;

            if (this.cBool(this.config.visible) == false) {
                this.visible(false);
            }

            if (!this.config.entity) {
                debugger;
                this.config.entity = this.config.EntityName;
            }

            this.load();

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
            var self = this;
            this.clear();
            if (this.config.userControlName && this.config.userControlName != "") {
                this.page = new smat.dynamics.PageUserControl({
                    projID: this.config.ProjID,
                    entityName: this.config.entity,
                    name: this.config.userControlName,
                    contextOn: $(this.config.target),
                    parentPageId: this.config.parentPageId,
                    noLoadEvent: this.config.designing,
                    pageParams: this.config.pageParams
                });

                this.page.body.removeClass("s-dy-page");
                this.page.body.addClass("s-dy-userControl");

                self.controlKey = this.config.ProjID + "_" + this.config.entity + "_" + this.config.userControlName;
                if (smat.dynamics.cacheOn && smat.dynamics.cache.userControls[self.controlKey]) {
                    self.page.cached = true;
                    self.page.setForm(smat.dynamics.cache.userControls[self.controlKey]);
                } else {
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.getUserControl,
                        params: {
                            ProjID: this.config.ProjID,
                            EntityName: this.config.entity,
                            UserControlName: this.config.userControlName
                        },
                        success: function (form) {
                            self.page.setForm(form);
                            smat.dynamics.cache.userControls[self.controlKey] = form;
                        }
                    });
                }

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
    smat.globalObject.extend(smat.DyUserControl, smat.UI);
})();