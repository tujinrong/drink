(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Button
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileView = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.View(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.View = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "view"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.View.prototype = {
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

            this.config.init = "smat.mobile.service.viewInit";

            //scroller
            if (this.config.elastic) {
                this.config.scroller = "{ elastic:" + this.config.elastic + " }";
                this.config.elastic = undefined;
            }

            this.setMobileProperty();
            
            //var uiConfig = smat.globalObject.clone(this.config);

            //uiConfig.name = undefined;
            //$(this.config.target).asmatMobileView(uiConfig);

            //setTimeout(function () {

            //    self.getUiControl();
            //    if (self.config.TabName && self.uiControl) {
            //        self.uiControl.bind("show", function (e) {
            //            debugger;
            //            //alert(self.config.TabName)
            //        })
            //    }

            //}, 1500);

        }, getUiControl: function () {
            if (!this.uiControl) {
                this.uiControl = $(this.config.target).data("asmatMobileView")
            }
        },
        onUiInit: function () {

            var self = this;

            if (!this.config.designing) {
                //init listView
                var lvs = $(this.config.target).find("[uuid][data-role='listview']");
                $.each(lvs, function () {
                    var lvUi = $(this).ui();
                    if (lvUi) lvUi.initUi();
                })
            }

           


            this.initEvent();

        },
        initEvent: function () {

            if (this.config.designing) {
                return;
            }
            var self = this;
            this.getUiControl();

            if (this.uiControl) {
                this.uiControl.bind("show", function (e) {
                    
                    var pageUi = self.page.getPage();

                    if (pageUi.app && pageUi.app.router.isBacking) {
                        self.isBackToThis = true;
                    } else {
                        self.isBackToThis = false;
                    }

                    self.trigger(smat.event.SHOW, e);

                    if (self.isBackToThis) {
                        pageUi.app.router.isBacking = false;
                    }
                });

                //this.uiControl.bind("afterShow", function (e) {
                //    //var pageUi = self.page.getPage();

                //    //if (pageUi.app && pageUi.app.router.isBacking) {
                //    //    pageUi.app.showLoading();
                //    //}
                    
                //    self.trigger(smat.event.SHOW, e);
                //});

                this.uiControl.bind("hide", function (e) {
                    ////clear listview
                    //var lvs = $(self.config.target).find("[uuid][data-role='listview']");
                    //$.each(lvs, function () {
                    //    var lvUi = $(this).ui();
                    //    if (lvUi) lvUi.clear();
                    //})

                    self.trigger(smat.event.HIDE, e);
                });

                this.uiControl.bind("beforeHide", function (e) {
                    //clear listview
                    if (self.page.getPage().app.router.isBacking) {
                        var lvs = $(self.config.target).find("[uuid][data-role='listview']");
                        $.each(lvs, function () {
                            var lvUi = $(this).ui();
                            if (lvUi) lvUi.clear();
                        })
                    }
                });
            }

        }, getViewParam: function () {
            this.getUiControl();

            if (this.uiControl) {
                return this.uiControl.params;
            }
        }

    };
    // extend Node
    smat.globalObject.extend(smat.mobile.View, smat.mobile.Base);
})();