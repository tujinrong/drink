
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Page
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Page = function (config) {
        //默认属性
        this.setConfig({
            type: "Page"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        return this;
    };

    smat.dynamics.mobile.Page.prototype = {
        addChild: function (config) {
            var type = config.type;
            if (smat.dynamics.mobile[config.type] != undefined) {

                //designing
                config.designing = this.config.designing;
                config.seq = this.children.length + 1;
                
                config.name = this.getControlUniqueName(config.name);

                //page: this,
                config.page = this;
                config.parent = this;

                var child = new smat.dynamics.mobile[config.type](config);
                this.children.set(config.name, child);

                return child;
            }
        },
        initApplication: function () {
            var self = this;
            if (this.settingForm != true && this.body.children("[data-role='view']").length > 0) {

                this.app = new asmat.mobile.Application($('#page_' + this.uuid),
                    {
                        skin: "flat",
                        //transition: "slide",
                        init: function (e) {
                            if (!self.config.designing) {
                                self.uiAfterInit();

                                if (!self.config.noLoadEvent) {
                                    smat.service.uiAfterInit(self.body);
                                    self.trigger(self.config.loaded, { sender: self });
                                    self.triggerChildrenLoaded();
                                    if (smat.dynamics.afterPageLoad) {
                                        smat.dynamics.afterPageLoad(self);
                                    }
                                }

                            }
                        }
                    }
                    );

                //this.app.router.bind("change", function (e) {
                //    debugger;
                //});

                this.app.router.bind("back", function (e) {
                    if (e.url.indexOf("tab") >= 0) {
                        e.preventDefault()
                    }
                    e.sender.isBacking = true;
                });
            } else {
                this.body.addClass("sm-widget sm-flat sm-7 sm-m0 sm-flat-dark sm-web sm-black-status-bar sm-vertical sm-pane");
            }
        },
        refreshApplication: function () {

            if (this.app) {
                return;
            }

            try {

                asmat.destroy($('#page_' + this.uuid));
            } catch (e) {

            }

            var uis = $('#page_' + this.uuid).find("[dy-uuid]");
            $.each(uis, function () {
                var dyui = $(this).dynamicsUI();
                if (dyui && dyui.iniEvent) {
                    dyui.iniEvent();
                }
            })

            this.initApplication();
            //var self = this;
            //setTimeout(function () { self.initApplication(); }, 100);
        },
        refresh: function (activeCtlName) {
            try {

                asmat.destroy($('#page_' + this.uuid));
            } catch (e) {

            }


            var uis = $('#page_' + this.uuid).find("[dy-uuid]");
            $.each(uis, function () {
                var dyui = $(this).dynamicsUI();
                if (dyui && dyui.iniEvent) {
                    dyui.iniEvent();
                }
            })

            this.body = $('#page_' + this.uuid);

            this.body.children().remove();

            var classStr = this.getClassStr();
            if (classStr) {
                this.body.addClass(classStr);
            }

            var styleStr = this.getStyleStr();
            if (styleStr) {
                this.body.attr("style", this.body.attr("style") + styleStr);
            }


            var Controls = this.getSaveParamsTree().Form.Controls;


            this.children = new smat.hashMap();

            this.settingData = true;
            this.setFormData(Controls);
            this.settingData = false;

            this.initApplication();

            if (activeCtlName) {
                var c = this.getControlByName(activeCtlName);
                if (c) c.active();
            }
        }, setForm: function (form) {
            this.settingForm = true;
            if (form.FormOptions) {
                this.setConfig(smat.service.strToJson(form.FormOptions));
            } else if (form.UserControlOptions) {
                this.setConfig(smat.service.strToJson(form.UserControlOptions));
            }

            var classStr = this.getClassStr();
            if (classStr) {
                this.body.addClass(classStr);
            }

            this.settingData = true;
            this.setFormData(form.Controls);
            this.settingData = false;

            if (this.config.designing == true) {
                this.propertysPanel.refreshControlList();
                this.active();
            } 
            this.settingForm = false;
            this.initApplication();


            if (this.config.designing == true) {
                this.refresh();
            }
        }, uiAfterInit: function () {
            //var uis = $('#page_' + this.uuid).find('[uuid]');
            //$.each(uis, function () {
            //    var ui = $(this).ui();
            //    if (ui.initEvent) {
            //        ui.initEvent();
            //    }
            //})
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Page, smat.dynamics.Page);
})();