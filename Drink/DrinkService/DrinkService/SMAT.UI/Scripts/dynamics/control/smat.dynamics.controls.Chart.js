
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Grid
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Chart = function (config) {
        //默认属性
        this.setConfig({
            type: "Chart",
            chartType: "line",
            theme: "material",
            increasing: "false"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.Chart.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) {
                contextOn = $(this.config.contextOn)
            } else {
                this.config.contextOn = contextOn;
            }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            this.body = $('<div id="' + this.getUiId() + '"   class="' + this.designClass + ' ' + cssClassStr + ' "  style="' + styleStr + '"></div>').appendTo(this.config.contextOn);

            var uiConfig = this.getUiConfig();

            if (this.config.designing == true) {

                this.getDesignDatasource(uiConfig);
            }

            this.uiControl = new smat.Chart(uiConfig);
            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }

            var self = this;
            if (this.config.designing == true) {
                //this.body.asmatDropTargetArea({
                //    filter: ".chart-drop",
                //    dragenter: function (e) {
                //        if ($(e.dropTarget).closest('.s-chart').dynamicsUI() != null) $(e.dropTarget).closest('.s-chart').dynamicsUI().droptargetOnDragEnter(e);
                //    },
                //    dragleave: function (e) {
                //        if ($(e.dropTarget).closest('.s-chart').dynamicsUI() != null) $(e.dropTarget).closest('.s-chart').dynamicsUI().droptargetOnDragLeave(e);
                //    },
                //    drop: function (e) {
                //        if ($(e.dropTarget).closest('.s-chart').dynamicsUI() != null) $(e.dropTarget).closest('.s-chart').dynamicsUI().droptargetOnDrop(e);
                //    }
                //});

            }

            this.dragTypes = { "View": 1, "LogicItem": 1, "GroupItem": 1 };

        }, getDesignDatasource: function (uiConfig) {

            if (this.config.chartType == "pie") {

                uiConfig.series = [{
                    startAngle: 150,
                    data: [{
                        category: "s1",
                        value: 23.8
                    }, {
                        category: "s2",
                        value: 16.1
                    }, {
                        category: "s3",
                        value: 9.6
                    }, {
                        category: "s4",
                        value: 5.2
                    }, {
                        category: "s5",
                        value: 3.6
                    }]
                }];

            } else if (this.config.chartType == "donut") {

                uiConfig.series = [{
                    name: "2011",
                    data: [{
                        category: "s1",
                        value: 30.8
                    }, {
                        category: "s2",
                        value: 21.1
                    }, {
                        category: "s3",
                        value: 16.3
                    }, {
                        category: "s4",
                        value: 17.6
                    }, {
                        category: "s5",
                        value: 9.2
                    }]
                }, {
                    name: "2012",
                    data: [{
                        category: "s1",
                        value: 53.8
                    }, {
                        category: "s2",
                        value: 16.1
                    }, {
                        category: "s3",
                        value: 11.3
                    }, {
                        category: "s4",
                        value: 9.6
                    }, {
                        category: "s5",
                        value: 5.2
                    }],
                    labels: {
                        visible: true,
                        background: "transparent",
                        position: "outsideEnd",
                        template: "#= category #: \n #= value#%"
                    }
                }];

            } else if (this.config.chartType == "radarArea"
                || this.config.chartType == "radarLine"
                || this.config.chartType == "radarColumn") {

                uiConfig.series = [{
                    name: "s1",
                    data: [3.907, 7.943, 7.848, 9.284, 9.263, 9.801, 3.890, 8.238, 9.552, 6.855]
                }, {
                    name: "s2",
                    data: [1.988, 2.733, 3.994, 3.464, 4.001, 3.939, 1.333, 2.245, 4.339, 2.727]
                }, {
                    name: "s3",
                    data: [4.743, 7.295, 7.175, 6.376, 8.153, 8.535, 5.247, 7.832, 4.3, 4.3]
                }, {
                    name: "s4",
                    data: [0.253, 0.362, 3.519, 1.799, 2.252, 3.343, 0.843, 2.877, 5.416, 5.590]
                }];

                uiConfig.valueAxis = {
                    labels: {
                        format: "{0}"
                    },
                    line: {
                        visible: true
                    }
                }

                uiConfig.categoryAxis = {
                    categories: ["item1", "item2", "item3", "item4", "item5", "item6", "item7", "item8", "item9", "item10"],
                    majorGridLines: {
                        visible: true
                    }
                }

            } else {
                uiConfig.series = [{
                    name: "s1",
                    data: [3.907, 7.943, 7.848, 9.284, 9.263, 9.801, 3.890, 8.238, 9.552, 6.855]
                }, {
                    name: "s2",
                    data: [1.988, 2.733, 3.994, 3.464, 4.001, 3.939, 1.333, 2.245, 4.339, 2.727]
                }, {
                    name: "s3",
                    data: [4.743, 7.295, 7.175, 6.376, 8.153, 8.535, 5.247, 7.832, 4.3, 4.3]
                }, {
                    name: "s4",
                    data: [0.253, 0.362, 3.519, 1.799, 2.252, 3.343, 0.843, 2.877, -.416, 5.590]
                }];

                var categoryAxisVisible = true;
                if (this.config.chartType == "line") {
                    categoryAxisVisible = false;
                }
                uiConfig.valueAxis = {
                    labels: {
                        format: "{0}"
                    },
                    line: {
                        visible: false
                    }
                }

                uiConfig.categoryAxis = {
                    categories: ["time1", "time2", "time3", "time4", "time5", "time6", "time7", "time8", "time9", "time10"],
                    majorGridLines: {
                        visible: categoryAxisVisible
                    }
                }
            }

        }, onHint: function (hintElement, dragTarget, item) {
            if (1 == 2) {
                e.preventDefault();
            } else {
                hintElement.removeClass('chart-drop');
            }

        }, onDragstart: function (e) {
            if (1 == 2) {
                e.preventDefault();
            } else {
                this.body.removeClass('chart-drop');
            }

        }, onDragend: function (e) {
            if (1 == 2) {
                e.preventDefault();
            } else {
                this.body.addClass('chart-drop');
            }

        },
        droptargetOnDragEnter: function (e) {
            if (this.config.page.dragTarget == undefined || this.dragTypes[this.config.page.dragTarget.options.dragType] != 1) {
                e.preventDefault();
                return;
            }

            var dropType = $(e.dropTarget).attr('drop-type');

            if (this.config.page.dragTarget.options.dragType == "GroupItem") {
                if (dropType != "xField" && dropType != "sField") {
                    e.preventDefault();
                    return;
                }
            }

            if (this.config.page.dragTarget.options.dragType == "LogicItem") {
                if ((dropType != "yField")) {
                    e.preventDefault();
                    return;
                }
            }

            this.config.page.dropTarget = $(e.dropTarget);

            //$(e.dropTarget).addClass('drag-enter');
            $(e.dropTarget).append('<div class="grid-drop-box" style="position: absolute;background-color: #19C6F9;opacity: 0.5;text-align: center;top: -1px;left: -1px;width: 100%;height: 100%;z-index: 1099; display: block;"><span style="position: absolute;top: 30%;display: block;left: 46%;color: #fff;font-size: 36px;font-weight: bold;"></span></div>')
            // $(e.dropTarget).text("pageX:" + e.pageX + " , pageY:"+ e.pageY )

        },
        droptargetOnDragLeave: function (e) {
            if (this.config.page.dragTarget == undefined || this.dragTypes[this.config.page.dragTarget.options.dragType] != 1) {
                e.preventDefault();
                return;
            }
            var target = $(e.dropTarget);
            //target.removeClass('drag-enter');


            target.find(".grid-drop-box").remove();
            this.config.page.dropTarget = undefined;


        },
        droptargetOnDrop: function (e) {
            if (this.config.page.dragTarget == undefined || this.dragTypes[this.config.page.dragTarget.options.dragType] != 1) {
                e.preventDefault();
                return;
            }
            this.body.find('.drag-enter').removeClass('drag-enter');
            this.config.page.dropTarget = undefined;
            //
            var target = $(e.dropTarget);
            if (target.find(".grid-drop-box").length == 0) return;
            target.find(".grid-drop-box").remove();

            var dataItem = this.config.page.dragTarget.options.dataItem;
            if (dataItem == undefined) return;

            var self = this;
            if (target.attr('drop-type') == "xField") {
                self.fillXFieldInfo({
                    title: dataItem.FieldDesc,
                    FieldName: dataItem.FieldName,
                    Alias: dataItem.Alias,
                    Path: dataItem.Path,
                    EntityName: dataItem.EntityName,
                    DataType: self.getUiDataType(dataItem.DataType),
                    format: ""

                });

            } else if (target.attr('drop-type') == "yField") {

                self.fillYFieldInfo({
                    title: dataItem.FieldDesc,
                    FieldName: dataItem.FieldName,
                    Alias: dataItem.Alias,
                    Path: dataItem.Path,
                    EntityName: dataItem.EntityName,
                    DataType: self.getUiDataType(dataItem.DataType),
                    FieldInfo: dataItem,
                    format: ""

                });

            } else if (target.attr('drop-type') == "sField") {
                self.fillSeriesFieldInfo({
                    title: dataItem.FieldDesc,
                    Alias: dataItem.Alias,
                    FieldName: dataItem.FieldName,
                    Path: dataItem.Path,
                    EntityName: dataItem.EntityName,
                    format: ""

                });

            } else {

            }

            //if (this.body.hasClass('chart-drop') == false) {
            //    this.body.addClass('chart-drop');
            //}

            this.refresh(true);

        }, fillXFieldInfo: function (data) {
            var self = this;
            var items = [];

            smat.service.getUserConfig({
                title: smat.service.optionSet("SysMsg.Confirm"),
                items: [
                    {
                        key: "title",
                        title: "項目名",
                        type: "TextBox",
                        readonly: true,
                        required: false,
                        value: data.title

                    }, {
                        key: "type",
                        title: "種別",
                        type: "TextBox",
                        readonly: true,
                        required: false,
                        value: "列データ"

                    }, {
                        key: "dataType",
                        title: "データ型",
                        type: "DropDownList",
                        codeKind: "ChartDataType",
                        emptyItem: false,
                        change: function (e) {
                            //alert(1)
                        },
                        value: data.DataType
                    }, {
                        key: "format",
                        title: "ﾌｫｰﾏｯﾄ",
                        type: "ComboBox",
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: [
                            { text: "年月日", value: "yyyy年MM月dd日" },
                            { text: "年月", value: "yyyy年MM月" },
                            { text: "年", value: "yyyy年" }
                        ],
                        value: data.format
                    }
                ],
                callback: function (result) {
                    var view = self.config.page.getEditView(self.config.view);
                   
                    self.config.XFormat = result.format;
                    self.config.XTitle = result.title;
                    self.config.XDataType = result.dataType;
                    self.config.XField = data.FieldName;

                    //=====todo:=====
                    var formatVal = undefined;
                    if (result.format == "yyyy年MM月dd日") formatVal = "=Date(YMD)";
                    if (result.format == "yyyy年MM月") formatVal = "=Date(YM)";
                    if (result.format == "yyyy年") formatVal = "=Date(Y)";
                    //=====todo:=====

                    //view
                    var viewItem = smat.service.getItemByKey(view.ItemList, "ItemName", data.FieldName);
                    if (viewItem == undefined) {
                        viewItem = {
                            ProjID: view.ProjID,
                            EntityName: view.EntityName,
                            ViewName: view.ViewName,
                            Path: data.Path,
                            ItemName: data.FieldName,
                            ItemSql: data.Alias + "." + data.FieldName,
                            ItemEntityName: data.EntityName,
                            Format: formatVal,
                            EntityAlias: data.EntityName
                        };

                        view.ItemList.push(viewItem);
                    }

                    viewItem.Group = "GroupBy";
                    viewItem.Format = formatVal;
                    viewItem.ItemDesc = result.title;


                    self.refresh();
                },
                check: function (items) {

                }
            })
        }, fillYFieldInfo: function (data) {
            var self = this;

            var groupItem = new Array();

            if (this.config.page.dragTarget.options.dragType == "GroupItem") {
                groupItem.push({ text: "GroupBy", value: "GroupBy" });
            } else if (this.config.page.dragTarget.options.dragType == "LogicItem") {

                if (data.FieldInfo.IsSum) {
                    groupItem.push({ text: "Sum", value: "Sum" });
                }
                groupItem.push({ text: "Count", value: "Count" });
            }

            //groupItem.push({ text: "Count", value: "Count" });

            //if (data.FieldInfo) {
            //    if (data.FieldInfo.IsSum) {
            //        groupItem.push({ text: "Sum", value: "Sum" });
            //    }

            //    if (data.FieldInfo.IsMax) {
            //        groupItem.push({ text: "Max", value: "Max" });
            //    }

            //    if (data.FieldInfo.IsMin) {
            //        groupItem.push({ text: "Min", value: "Min" });
            //    }
            //} else {

            //    groupItem.push({ text: "Sum", value: "Sum" });
            //    groupItem.push({ text: "Max", value: "Max" });
            //    groupItem.push({ text: "Min", value: "Min" });
            //}


            smat.service.getUserConfig({
                title: smat.service.optionSet("SysMsg.Confirm"),
                items: [
                    {
                        key: "title",
                        title: "項目名",
                        type: "TextBox",
                        readonly: true,
                        required: false,
                        value: data.title

                    }, {
                        key: "type",
                        title: "種別",
                        type: "TextBox",
                        readonly: true,
                        required: false,
                        value: "数値"

                    }, {
                        key: "format",
                        title: "ﾌｫｰﾏｯﾄ",
                        type: "ComboBox",
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: [
                            { text: "金額", value: "n0" }
                        ],
                        value: data.format
                    }, {
                        key: "group",
                        title: "集計種別",
                        type: "DropDownList",
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: groupItem,
                        value: data.group
                    }
                ],
                callback: function (result) {
                    var view = self.config.page.getEditView(self.config.view);

                    if (self.config.YField != undefined && self.config.YField != data.FieldName) {
                        smat.service.delItemByKey(view.ItemList, "ItemName", self.config.YField);

                    }

                    self.config.YFormat = result.format;
                    self.config.YTitle = result.title;
                    self.config.YField = data.FieldName;

                    //view
                    var viewItem = smat.service.getItemByKey(view.ItemList, "ItemName", data.FieldName);
                    if (viewItem == undefined) {
                        viewItem = {
                            ProjID: view.ProjID,
                            EntityName: view.EntityName,
                            ViewName: view.ViewName,
                            Path: data.Path,
                            ItemName: data.FieldName,
                            ItemSql: data.Alias + "." + data.FieldName,
                            ItemEntityName: data.EntityName,
                            EntityAlias: data.EntityName
                        };

                        view.ItemList.push(viewItem);
                    }


                    viewItem.Group = result.group;
                    viewItem.ItemDesc = result.title;


                    self.refresh();
                },
                check: function (items) {

                }
            })
        }, fillSeriesFieldInfo: function (data) {
            var self = this;
            smat.service.getUserConfig({
                title: smat.service.optionSet("SysMsg.Confirm"),
                items: [
                    {
                        key: "title",
                        title: "項目名",
                        type: "TextBox",
                        readonly: false,
                        required: false,
                        value: data.title

                    }, {
                        key: "type",
                        title: "種別",
                        type: "TextBox",
                        readonly: true,
                        required: false,
                        value: "行データ"

                    }
                    //, {
                    //    key: "format",
                    //    title: "ﾌｫｰﾏｯﾄ",
                    //    type: "ComboBox",
                    //    dataTextField: "text",
                    //    dataValueField: "value",
                    //    dataSource: [
                    //    ],
                    //    value: data.format
                    //}
                ],
                callback: function (result) {
                    var view = self.config.page.getEditView(self.config.view);
                   
                    self.config.seriesTitle = result.title;
                    self.config.seriesField = data.FieldName;

                    //view
                    var viewItem = smat.service.getItemByKey(view.ItemList, "ItemName", data.FieldName);
                    if (viewItem == undefined) {
                        viewItem = {
                            ProjID: view.ProjID,
                            EntityName: view.EntityName,
                            ViewName: view.ViewName,
                            Path: data.Path,
                            ItemName: data.FieldName,
                            ItemSql: data.Alias + "." + data.FieldName,
                            ItemEntityName: data.EntityName,
                            EntityAlias: data.EntityName
                        };

                        view.ItemList.push(viewItem);
                    }

                    viewItem.Group = "GroupBy";
                    viewItem.ItemDesc = result.title;


                    self.refresh();
                },
                check: function (items) {

                }
            })
        }, refresh: function (isResetProperty) {
            this.uiControl.destroy();


            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var temp = $('<div id="' + this.getUiId() + '"   class="' + this.designClass + ' ' + cssClassStr + ' "  style="' + styleStr + '"></div>')

            this.body.replaceWith(temp);
            this.body.remove();

            this.body = temp;

            var uiConfig = smat.globalObject.clone(this.config, this.optionIgnoreInfo);
            uiConfig.target = this.body;

            if (this.config.designing == true) {
                this.getDesignDatasource(uiConfig);
            }
            this.uiControl = new smat.Chart(uiConfig);
            this.editSkinBody = this.body;
            //设计器初期化
            this.initEditSkin();
            this.body.attr('dy-uuid', this.uuid);
            //Event初期化
            this.iniEvent();

            if (this.config.page.activeControl == this) {
                if (isResetProperty == true) {
                    this.editPropertyConfig = undefined;
                    this.config.page.propertysPanel.clear();
                    this.config.page.propertysPanel.setCurrentControl(this, this.getPropertyConfig(), this.config);
                }

                this.editSkinBox.addClass("edit-skin-box-active");
                this.editSkinBody.children('.edit-skin-zoom-box').show();
                if (this.shortcutMenu) {
                    this.shortcutMenu.show();
                }
            }

            this.config.page.modalClear();

        }, checkPropertyChanging: function (property, value) {
            var isOk = true;

            return isOk;
        },
        initCustomEditSkin: function () {
            var self = this;
            var pageTemplate = this.config.page.config.designer.template;
            if (pageTemplate.config.type == "SimpleGraph") {

                this.customChartEditSkinBox = $('<div id="cus_skin_' + this.uuid + '" class="" style=" z-index:' + this.config.page.skinZindex + ';position: absolute;top: -1px;left: -1px;width: 100%;height: 100%;"></div></div>');
                //this.editSkinBody.find(':first').before(this.editSkinBox);
                this.customChartEditSkinBox.appendTo(this.editSkinBody);


                var vBoxLeft = Number(160);

                var xFieldBoxHeight = 46;

                if (this.config.chartType == "pie") {
                    xFieldBoxHeight = 0;
                }

                this.xFieldBox = $('<div class="chart-drop" drop-type="xField" style="border: 1px solid #1924F9;position: absolute;top: -1px;left: ' + vBoxLeft + 'px;width: calc(100% - ' + vBoxLeft + 'px);height: ' + xFieldBoxHeight + 'px;"></div>').appendTo(this.customChartEditSkinBox);
                $('<div class="" drop-type="xField" style="background-color: #DBDCF9;opacity: 0.5;position: absolute;top: -1px;left: -1px;width: 100%;height:100%;"></div>').appendTo(this.xFieldBox);
                //$('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">列:</span>').appendTo(this.xFieldBox);
                this.emptyBox = $('<div class="" style="position: absolute;top: -1px;left: -1px;width: ' + vBoxLeft + 'px;height: ' + xFieldBoxHeight + 'px;"></div>').appendTo(this.customChartEditSkinBox);

                if (this.config.XField && this.config.chartType != "pie") {
                    $('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">' + this.config.XTitle + '</span>').appendTo(this.xFieldBox);
                    this.xDelBtn = $('<button class="btn-danger s-button" style="padding: 1px 5px;position: absolute;top: 2px;right: 2px;"><i class="fa fa-trash-o"></i></button>').appendTo(this.xFieldBox);
                    this.xDelBtn.bind('click', function (e) {
                        var view = self.config.page.getEditView(self.config.view);
                        
                        if (self.config.XField != undefined) {
                            smat.service.delItemByKey(view.ItemList, "ItemName", self.config.XField);

                        }

                        self.config.XFormat = undefined;
                        self.config.XTitle = undefined;
                        self.config.XField = undefined;

                        self.refresh();
                        e.stopPropagation();
                    });

                    this.xEditBtn = $('<button class="btn-primary s-button" style="padding:1px 2px 1px 3px;position: absolute;top: 2px;right: 32px;"><i class="fa fa-edit"></i></button>').appendTo(this.xFieldBox);
                    this.xEditBtn.bind('click', function (e) {
                        self.fillXFieldInfo({
                            title: self.config.XTitle,
                            FieldName: self.config.XField,
                            DataType: self.config.XDataType,
                            format: self.config.XFormat

                        });

                        e.stopPropagation();
                    });
                }

                var yFieldBoxHeight = this.body.height() - xFieldBoxHeight;
                this.sFieldBox = $('<div class="chart-drop" drop-type="sField" style="border: 1px solid #1924F9;position: absolute;top: ' + xFieldBoxHeight + 'px;left: -1px;width: ' + vBoxLeft + 'px;height: ' + yFieldBoxHeight + 'px;"></div></div>').appendTo(this.customChartEditSkinBox);
                $('<div class="" drop-type="xField" style="background-color: #DBDCF9;opacity: 0.5;position: absolute;top: -1px;left: -1px;width: 100%;height:100%;"></div>').appendTo(this.sFieldBox);
                this.yFieldBox = $('<div class="chart-drop" drop-type="yField" style="border: 1px solid #1924F9;position: absolute;top: ' + xFieldBoxHeight + 'px;left: ' + vBoxLeft + 'px;width: calc(100% - ' + vBoxLeft + 'px);height: ' + yFieldBoxHeight + 'px;"></div></div>').appendTo(this.customChartEditSkinBox);
                $('<div class="" drop-type="xField" style="background-color: #EEF7C2;opacity: 0.5;position: absolute;top: -1px;left: -1px;width: 100%;height:100%;"></div>').appendTo(this.yFieldBox);

                //$('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">行:</span>').appendTo(this.yFieldBox);
                if (this.config.YField) {
                    $('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">' + this.config.YTitle + '</span>').appendTo(this.yFieldBox);
                    this.yDelBtn = $('<button class="btn-danger s-button" style="padding: 1px 5px;position: absolute;top: 2px;right: 2px;"><i class="fa fa-trash-o"></i></button>').appendTo(this.yFieldBox);
                    this.yDelBtn.bind('click', function (e) {
                        var view = self.config.page.getEditView(self.config.view);
                        
                        if (self.config.YField != undefined) {
                            smat.service.delItemByKey(view.ItemList, "ItemName", self.config.YField);

                        }

                        self.config.YFormat = undefined;
                        self.config.YTitle = undefined;
                        self.config.YField = undefined;

                        self.refresh();
                        e.stopPropagation();
                    });

                    this.yEditBtn = $('<button class="btn-primary s-button" style="padding:1px 2px 1px 3px;position: absolute;top: 2px;right: 32px;"><i class="fa fa-edit"></i></button>').appendTo(this.yFieldBox);
                    this.yEditBtn.bind('click', function (e) {

                        var view = self.config.page.getEditView(self.config.view);
                       
                        //view
                        var viewItem = smat.service.getItemByKey(view.ItemList, "ItemName", self.config.YField);

                        self.fillYFieldInfo({
                            title: self.config.YTitle,
                            FieldName: self.config.YField,
                            DataType: self.config.YDataType,
                            format: self.config.YFormat,
                            group: viewItem.Group

                        });

                        e.stopPropagation();
                    });
                }

                //$('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">データ:</span>').appendTo(this.sFieldBox);
                if (this.config.seriesField) {
                    $('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">' + this.config.seriesTitle + '</span>').appendTo(this.sFieldBox);
                    this.sDelBtn = $('<button class="btn-danger s-button" style="padding: 1px 5px;position: absolute;top: 2px;right: 2px;"><i class="fa fa-trash-o"></i></button>').appendTo(this.sFieldBox);
                    this.sDelBtn.bind('click', function (e) {
                        var view = self.config.page.getEditView(self.config.view);
                        
                        if (self.config.seriesField != undefined) {
                            smat.service.delItemByKey(view.ItemList, "ItemName", self.config.seriesField);

                        }

                        self.config.seriesTitle = undefined;
                        self.config.seriesField = undefined;
                        self.refresh();
                        e.stopPropagation();
                    });

                    this.sEditBtn = $('<button class="btn-primary s-button" style="padding:1px 2px 1px 3px;position: absolute;top: 2px;right: 32px;"><i class="fa fa-edit"></i></button>').appendTo(this.sFieldBox);
                    this.sEditBtn.bind('click', function (e) {

                        self.fillSeriesFieldInfo({
                            title: self.config.seriesTitle,
                            FieldName: self.config.seriesField

                        });
                        e.stopPropagation();
                    });
                }

                this.customChartEditSkinBox.asmatDropTargetArea({
                    filter: ".chart-drop",
                    dragenter: function (e) {
                        if ($(e.dropTarget).closest('.s-chart').dynamicsUI() != null) $(e.dropTarget).closest('.s-chart').dynamicsUI().droptargetOnDragEnter(e);
                    },
                    dragleave: function (e) {
                        if ($(e.dropTarget).closest('.s-chart').dynamicsUI() != null) $(e.dropTarget).closest('.s-chart').dynamicsUI().droptargetOnDragLeave(e);
                    },
                    drop: function (e) {
                        if ($(e.dropTarget).closest('.s-chart').dynamicsUI() != null) $(e.dropTarget).closest('.s-chart').dynamicsUI().droptargetOnDrop(e);
                    }
                });

                this.initContextMenu("#" + this.customChartEditSkinBox.attr('id'));

                this.customChartEditSkinBox.find('.chart-drop').bind('click', function (e) {
                    self.active();
                    e.stopPropagation();
                });

            } else {
                //this.body.asmatDropTargetArea({
                //    filter: ".chart-drop",
                //    dragenter: function (e) {
                //        self.droptargetOnDragEnter(e);
                //    },
                //    dragleave: function (e) {
                //        self.droptargetOnDragLeave(e);
                //    },
                //    drop: function (e) {
                //        self.droptargetOnDrop(e);
                //    }
                //});
            }

        }, getCustomPropertyConfig: function () {

            this.editPropertyConfig.push(
            {
                group: 'data',
                caption: 'view',
                type: 'View',
                id: 'view',
                shortcutMenu: true,
                cmt: 'view',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'data',
                caption: 'data',
                type: 'ChartData',
                id: ' ',
                shortcutMenu: true,
                cmt: 'data',
                propType: "prop"
            });

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'refresh',
			         type: 'Logic',
			         id: 'refresh',
			         cmt: 'refresh',
			         eventKey: 'chart_refresh',
			         propType: "event"
			     }
            );
        },
        propertyChange_dataType: function (property, value) {

        },
        propertyChange_columns: function (property, value) {

        },
        propertyChange_view: function (property, value, valueConfig, view) {

            this.refresh(true);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Chart, smat.dynamics.Field);
})();