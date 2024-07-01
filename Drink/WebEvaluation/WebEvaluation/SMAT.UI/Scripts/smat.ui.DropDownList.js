(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DropDownList
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDropDownList = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.DropDownList(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.DropDownList = function (config) {
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

        //共通初始化后
        //this.afterInit();

        return this;
    };

    smat.DropDownList.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.DropDownList.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            //code mst dataSource

            if (this.config.dataSource == undefined
                && this.config.codeKind != undefined) {

                this.config.dataValueField = smat.uiConfig.CodeMst.codeField;
                this.config.dataTextField = smat.uiConfig.CodeMst.nameField;


                var dataSource = smat.service.optionSet(this.config.codeKind).slice();

                if (this.config.emptyItem == undefined || this.cBool(this.config.emptyItem) == true) {
                    var emptyText = "";
                    if (this.config.emptyText != undefined) {
                        emptyText = this.config.emptyText;
                    }
                    var emptyItem = {};
                    emptyItem[smat.uiConfig.CodeMst.codeField] = "";
                    emptyItem[smat.uiConfig.CodeMst.nameField] = emptyText;

                    if (self.config.uitype != "asmatMultiSelect") {
                        dataSource.unshift(emptyItem);
                    }
                }
                this.config.dataSource = dataSource;
            }

            var uiConfig = smat.globalObject.clone(this.config);

            uiConfig.change = function (e) {
                if (self.config.uitype == "asmatComboBox" && self.cBool(self.config.optionRequired)) {
                    var optionFlag = false;
                    for (var i = 0; i < self.config.dataSource.length; i++) {
                        if (self.config.dataSource[i][self.config.dataTextField] == self.uiControl.text()) {
                            optionFlag = true;
                        }
                    }

                    if (optionFlag == false) {
                        self.value(self.oldValue);
                    }
                }

                self.trigger(smat.event.CHANGE, e);

                self.oldValue = self.value();
            }
            uiConfig.select = function (e) {
                self.trigger(smat.event.SELECT, e);
            }
            uiConfig.close = function (e) {
                self.trigger(smat.event.CLOSE, e);
            }
            uiConfig.open = function (e) {
                self.trigger(smat.event.OPEN, e);
            }
            uiConfig.filter = undefined;


            this.initUiControl(uiConfig);

            if (this.config.dataSource == undefined) {
                this.config.dataSource = {};
            }

            if (this.config.visible) {
                this.visible(this.cBool(this.config.visible));
            }

            this.uiControl = $(this.config.target).data(this.config.uitype);

            var item = this.config.dataSource[0];
            if (item != undefined) {
                $(self.config.target).attr("value", item[self.config.dataValueField]);
                $(self.config.target).attr("text", item[self.config.dataTextField]);
            }

            this.uiControl.bind("select", function (e) {
                var dataItem = this.dataItem(e.sender._current);
                if (dataItem) {
                    self.config.value = dataItem[self.config.dataValueField];
                    $(self.config.target).attr("value", dataItem[self.config.dataValueField]);
                    $(self.config.target).attr("text", dataItem[self.config.dataTextField]);
                }
            });

            if (this.config.value != undefined) {
                this.value(this.config.value);
                $(self.config.target).attr("value", this.config.value);
                self.oldValue = self.value();
            }

            if (this.cBool(uiConfig.enable) == false) {
                this.enable(this.cBool(uiConfig.enable));
            }

            if (this.cBool(this.config.autoLoadViewData) == true) {
                this.setViewDataSource();
            }
        }, initUiControl: function (uiConfig) {

            if (this.config.uitype == "asmatDropDownList") {
                $(this.config.target).asmatDropDownList(uiConfig);

            } else if (this.config.uitype == "asmatComboBox") {
                $(this.config.target).asmatComboBox(uiConfig);

            } else if (this.config.uitype == "asmatMultiSelect") {
                $(this.config.target).asmatMultiSelect(uiConfig);

            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            try {
                this.uiControl.destroy();
            } catch (e) {
            }

        }, setDataSource: function (data) {
            this.config.dataSource = data;

            if (this.config.getIgnoreItem != undefined) {
                var temp = new Array();

                var IgnoreItem = this.config.getIgnoreItem();
                for (var i = 0; i < data.length; i++) {
                    if (IgnoreItem.indexOf(data[i][this.config.dataValueField]) == -1) {
                        temp.push(data[i]);
                    }
                }

                this.uiControl.setDataSource(temp);
            } else {
                this.uiControl.setDataSource(data);
            }

            if (this.config.value != undefined) {
                this.value(this.config.value);
                $(this.config.target).attr("value", this.uiControl.value());
                $(this.config.target).attr("text", this.uiControl.text());
            }

        }, value: function (value) {
            if (value == undefined) {
                return this.uiControl.value();
            } else {
                this.uiControl.value(value);

            }
        }, dataItem: function (index) {
            return this.uiControl.dataItem(index);
        }, text: function () {
            return this.uiControl.text();
        }, refresh: function () {
            this.setDataSource(this.config.dataSource);
        }, setIgnoreFunc: function (func) {
            this.config["getIgnoreItem"] = func;
        }, visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).closest(".s-dropdown").hide();
                if (this.config.label != undefined) {
                    this.labelDom.hide();
                }

                if (this.config.inputBox != undefined) {
                    this.inputBoxDom.hide();
                }
            } else {
                $(this.config.target).closest(".s-dropdown").show();
                if (this.config.label != undefined) {
                    this.labelDom.show();
                }

                if (this.config.inputBox != undefined) {
                    this.inputBoxDom.show();
                }
            }
        }, setViewDataSource: function (view, viewEntityName) {
            var self = this;
            if (!view) view = this.config.view;
            if (!viewEntityName) viewEntityName = this.config.viewEntityName;
            if (!viewEntityName) viewEntityName = this.config.EntityName;

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
                params.request.GetPageCount = false;

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
                    async: false,
                    success: function (result) {
                        //tottotalSize
                        var totalSize = result.totalSize;


                        if (self.config.emptyItem == undefined || self.cBool(self.config.emptyItem) == true) {
                            var emptyText = "";
                            if (self.config.emptyText != undefined) {
                                emptyText = self.config.emptyText;
                            }
                            var emptyItem = {};
                            emptyItem[self.config.dataValueField] = "";
                            emptyItem[self.config.dataTextField] = emptyText;

                            if (self.config.uitype != "asmatMultiSelect") {
                                result.pageData.unshift(emptyItem);
                            }
                        }

                        //datasource
                        self.setDataSource(result.pageData)
                    }
                }
               );

            }
        }, setActionConfig: function (actionConfig) {

            this.actionConfig = actionConfig;

        }
    };
    // extend Node
    smat.globalObject.extend(smat.DropDownList, smat.UI);
})();