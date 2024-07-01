(function () {
    ///////////////////////////////////////////////////////////////////////
    //  ListView
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatMobileListView = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.mobile.ListView(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.mobile.ListView = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            role: "listview",
            autoLoadViewData: true

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.mobile.ListView.prototype = {
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

            //this.setMobileProperty();
            $(this.config.target).attr('data-role', 'listview');
            if (this.config.listType) {
                $(this.config.target).attr('data-type', this.config.listType);
            }
            
            if (this.config.designing) {
                setTimeout(function () {
                    self.initUi();
                }, 10);
            }
            
        }, initUi: function () {
            var self = this;
            var uiConfig = this._getUiConfig();

            if (this.cBool(self.config.autoLoadViewData) && self.config.view) {
                self.setViewDataSource();
            } else {
                //$(self.config.target).asmatMobileListView(uiConfig);
                self.initEvent();
            }
        }, getUiControl: function () {
            if (!this.uiControl) {
                this.uiControl = $(this.config.target).data("asmatMobileListView")
            }
        }, setViewDataSource: function (view, viewEntityName) {
            var self = this;
            //smat.service.openAppLoding();
            setTimeout(function () {
                self._setViewDataSource(view, viewEntityName);
            }, 10);
        }, _setViewDataSource: function (view, viewEntityName) {
            var self = this;
            if (!view) view = this.config.view;
            if (!viewEntityName) viewEntityName = this.config.viewEntityName;

            //entity view
            if (view && viewEntityName) {
                var actionUrl = smat.dynamics.commonURL.getPageView;
                var params = {};

                if (this.config.getParam != undefined) {
                    this.trigger(this.config.getParam, params);
                }

                params.request = {};
                params.request.ProjID = this.config.ProjID;
                params.request.EntityName = viewEntityName;
                params.request.ViewName = view;
                params.request.FilterDic = {};

                for (var key in params) {
                    var val = params[key];
                    // handle special keys
                    if (typeof (val) != 'object') {
                        params.request.FilterDic[key] = val;
                    }

                }

                this.setActionConfig({
                    action: actionUrl,
                    params: params
                });

                smat.service.loadJosnData({
                    url: actionUrl,
                    params: params,
                    success: function (result) {
                        //tottotalSize
                        var totalSize = result.totalSize;

                        var pageSize = 80;

                        //datasource
                        var dsConfig = {
                            transport: {
                                read: function (options) {
                                    var params = self.actionConfig.params;
                                    if (params == undefined) {
                                        params = {};
                                    }

                                    params.request.pageNumber = options.data.page;
                                    params.request.GetPageSize = options.data.pageSize;

                                    smat.service.loadJosnData(
						                {
						                    url: self.actionConfig.action,
						                    params: params,
						                    success: function (result) {

						                        options.success(result.pageData)

						                    }
						                }
					                );
                                }
                            },

                            pageSize: pageSize,
                            serverPaging: true,
                            schema: {
                                total: function () { return totalSize; }
                            }
                        }

                        if (self.config.groupField) {
                            dsConfig.group = self.config.groupField;
                        }

                        var ds = new asmat.data.DataSource(dsConfig);

                        var uiConfig = self._getUiConfig();

                        if (totalSize <= (pageSize * 1.5)) {
                            uiConfig.endlessScroll = false;
                        } else {
                            uiConfig.endlessScroll = true;
                        }

                        uiConfig.dataSource = ds;

                        if (self.uiControl) {
                            $(self.config.target).closest('.sm-scroll-container').css("transform", "translate3d(0px, 0px, 0px) scale(1)");

                            self.uiControl.destroy();
                            $(self.config.target).children().remove();
                            $(self.config.target).closest('.sm-listview-wrapper').children('.sm-filter-form').remove();
                            $(self.config.target).closest('.sm-scroll-container').children('.sm-scroller-pull').remove();

                            

                        }

                        $(self.config.target).asmatMobileListView(uiConfig);

                        self.uiControl = $(self.config.target).data("asmatMobileListView");

                    }
                }
               );

            } else {
                self.initEvent();
            }
        },_getUiConfig:function(){
        
            var uiConfig = smat.globalObject.clone(this.config);

           
            uiConfig.name = undefined;

            if (this.config.groupField) {
                uiConfig.endlessScroll = false;
            } 

            if (this.config.listType) {
                uiConfig.type = this.config.listType;
            }

            return uiConfig;

        },
        initEvent: function () {

            if (this.config.designing) {
                return;
            }
            var self = this;
            this.getUiControl();

            if (this.uiControl) {

                this.uiControl.bind("click", function (e) {

                });

                this.uiControl.bind("dataBound", function (e) {

                });

                this.uiControl.bind("dataBinding", function (e) {

                });

                this.uiControl.bind("itemChange", function (e) {

                });
            }

        }, setActionConfig: function (actionConfig) {

            this.actionConfig = actionConfig;

        }, clear: function () {
            var self = this;
            if (!this.cBool(self.config.autoLoadViewData) && self.config.view) {
                if (self.uiControl) {

                    //var emptyDs = new asmat.data.DataSource({ data: [{ name: "foo" }] });
                    self.uiControl.replace([]);

                    //scroll  back to top
                    $(self.config.target).closest('.sm-scroll-container').css("transform", "translate3d(0px, 0px, 0px) scale(1)");

                    //self.uiControl.destroy();
                    ////self.uiControl.setDataSource([])
                    //$(self.config.target).children().remove();
                    //$(self.config.target).closest('.sm-listview-wrapper').children('.sm-filter-form').remove();
                    //self.uiControl = null;

                }
            }
        }

    };
    // extend Node
    smat.globalObject.extend(smat.mobile.ListView, smat.mobile.Base);
})();