
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Grid
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Grid = function (config) {
        //默认属性
        this.setConfig({
            type: "Grid"
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

    smat.dynamics.Grid.prototype = {
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

            if (this.config.designing == true) {
                this.seriesWidth = "160";
                if (this.config.columns == undefined || this.config.columns.length == 0) {

                    this.config.columns = new Array();

                    //for (var i = 0; i < this.config.page.entity.FieldList.length; i++) {
                    //    if(i>4){
                    //        break;
                    //    }
                    //    var field = this.config.page.entity.FieldList[i];

                    //    this.config.columns.push({
                    //        title: field.FieldName,
                    //        field: field.FieldName,
                    //        Path: "",
                    //        ItemName: field.FieldName,
                    //        ItemSql: field.FieldName,
                    //        ItemEntityName: this.config.page.entity.EntityName,
                    //        EntityAlias: this.config.page.entity.EntityName
                    //    });
                    //}
                    this.config.columns.push({
                        title: "　　",
                        field: "colTemp",
                    });
                }
            }

            var uiConfig = this.getUiConfig();

            if (this.config.designing == true) {

                if (uiConfig.dataSource == undefined || this.config.columns.length == 0) {
                    uiConfig.dataSource = new Array();
                    for (var i = 0; i < 5; i++) {
                        var tempData = {};
                        if (uiConfig.columns != undefined) {
                            for (var j = 0; j < uiConfig.columns.length; j++) {
                                var field = uiConfig.columns[j];
                                tempData[field.field] = "　　";

                            }
                        }
                        tempData["colTemp"] = "　　";
                        uiConfig.dataSource.push(tempData);
                    }
                }

                if (uiConfig.category != undefined && uiConfig.category != ""
                    && uiConfig.dataField != undefined && uiConfig.dataField != ""
                    && uiConfig.series != undefined && uiConfig.series != null && typeof (uiConfig.series) == 'object' && uiConfig.series.length > 0) {
                    var seriesFieldName = uiConfig.series[0].seriesField;

                    uiConfig.series[0].width = this.seriesWidth;
                    uiConfig.category_format = "";
                    uiConfig.category_width = "";

                    var tempV = "　";
                    for (var k in uiConfig.dataSource) {
                        uiConfig.dataSource[k][seriesFieldName] = tempV;
                        uiConfig.dataSource[k][uiConfig.category] = uiConfig.category;
                        tempV += " ";
                    }

                    if (uiConfig.series.length > 1) {
                        uiConfig.series.splice(1, uiConfig.series.length - 1);
                    }
                }
            }

            this.uiControl = new smat.Grid(uiConfig);
            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }

            var self = this;

            this.dragTypes = { "View": 1, "LogicItem": 1, "GroupItem": 1 };

        }, onHint: function (hintElement, dragTarget, item) {
            if (1 == 2) {
                e.preventDefault();
            } else {
                hintElement.removeClass('col-drop');
            }

        }, onDragstart: function (e) {
            if (1 == 2) {
                e.preventDefault();
            } else {
                this.body.removeClass('col-drop');
            }

        }, onDragend: function (e) {
            if (1 == 2) {
                e.preventDefault();
            } else {
                this.body.addClass('col-drop');
            }

        },
        droptargetOnDragEnter: function (e) {
            if (this.config.page.dragTarget == undefined || this.dragTypes[this.config.page.dragTarget.options.dragType] != 1) {
                e.preventDefault();
                return;
            }

            var dropType = $(e.dropTarget).attr('drop-type');

            if (this.config.page.dragTarget.options.dragType == "GroupItem") {
                if (dropType != "xField" && dropType != "yField" && dropType != "statisticsField") {
                    e.preventDefault();
                    return;
                }
            }

            if (this.config.page.dragTarget.options.dragType == "LogicItem") {
                if ((dropType != "vField" && dropType != "searchField" && dropType != "statisticsField")) {
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

            var dropType = $(e.dropTarget).attr('drop-type');

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
                self.fillCategoryInfo({
                    title: dataItem.FieldDesc,
                    FieldName: dataItem.FieldName,
                    Alias: dataItem.Alias,
                    Path: dataItem.Path,
                    EntityName: dataItem.EntityName,
                    format: ""

                });

            } else if (target.attr('drop-type') == "vField") {
                self.fillValueFieldInfo({
                    title: dataItem.FieldDesc,
                    FieldName: dataItem.FieldName,
                    Alias: dataItem.Alias,
                    Path: dataItem.Path,
                    FieldInfo: dataItem,
                    EntityName: dataItem.EntityName,
                    format: ""

                });

            } else if (target.attr('drop-type') == "yField") {
                self.fillSeriesFieldInfo({
                    title: dataItem.FieldDesc,
                    FieldName: dataItem.FieldName,
                    Alias: dataItem.Alias,
                    Path: dataItem.Path,
                    EntityName: dataItem.EntityName,
                    format: ""

                });

            } else if (target.attr('drop-type') == "statisticsField") {
                self.fillColumnsInfo({
                    title: dataItem.FieldDesc,
                    titleTemp: dataItem.FieldDesc,
                    FieldName: dataItem.FieldName,
                    Alias: dataItem.Alias,
                    Path: dataItem.Path,
                    FieldInfo: dataItem,
                    EntityName: dataItem.EntityName,
                    format: "",
                    group: "GroupBy"

                });

            } else if (target.attr('drop-type') == "searchField") {
                self.fillColumnsInfo({
                    title: dataItem.FieldDesc,
                    titleTemp: dataItem.FieldDesc,
                    FieldName: dataItem.FieldName,
                    Alias: dataItem.Alias,
                    Path: dataItem.Path,
                    EntityName: dataItem.EntityName,
                    format: ""
                });

            } else {
                this.config.columns.push({
                    title: dataItem.FieldDesc,
                    field: dataItem.FieldName,
                    Path: "",
                    ItemName: dataItem.FieldName,
                    ItemSql: dataItem.FieldName,
                    ItemEntityName: this.config.page.entity.EntityName,
                    EntityAlias: this.config.page.entity.EntityName
                });
            }

            if (this.body.hasClass('col-drop') == false) {
                this.body.addClass('col-drop');
            }

            this.refresh(true);

        }, fillCategoryInfo: function (data) {
            var self = this;
            smat.service.getUserConfig({
                title: smat.service.optionSet("SysText.Confirm"),
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
                        value: "列データ"

                    }, {
                        key: "width",
                        title: "幅",
                        type: "TextBox",
                        value: data.width

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
                   
                    if (self.config.category != undefined && self.config.category != data.FieldName) {
                        smat.service.delItemByKey(view.ItemList, "ItemName", self.config.category);

                    }

                    self.config.category_format = result.format;
                    self.config.category_width = result.width;
                    self.config.category_title = result.title;
                    self.config.category = data.FieldName;

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
        }, fillValueFieldInfo: function (data) {
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
                title: smat.service.optionSet("SysText.Confirm"),
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
                        title: "集計方法",
                        type: "DropDownList",
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: groupItem,
                        value: data.group
                    }
                ],
                callback: function (result) {
                    var view = self.config.page.getEditView(self.config.view);
                   
                    if (self.config.dataField != undefined && self.config.dataField != data.FieldName) {
                        smat.service.delItemByKey(view.ItemList, "ItemName", self.config.dataField);

                    }

                    self.config.dataField_format = result.format;
                    self.config.dataField_title = result.title;
                    self.config.dataField = data.FieldName;

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
                title: smat.service.optionSet("SysText.Confirm"),
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
                    if (result.format == undefined) {
                        result.format = "";
                    }
                    if (result.width == undefined) {
                        result.width = "";
                    }

                    var view = self.config.page.getEditView(self.config.view);

                    var serie = smat.service.getItemByKey(self.config.series, "seriesField", data.FieldName);
                    if (serie == undefined) {
                        serie = {
                            seriesField: data.FieldName,
                            seriesTitle: result.title,
                            seriesFormat: result.format,
                            width: result.width,
                        }
                        if (self.config.series == undefined) self.config.series = new Array();
                        self.config.series.push(serie);
                    } else {
                        serie.seriesTitle = result.title;
                        serie.seriesFormat = result.format;
                        serie.width = result.width;
                    }

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
        }, fillColumnsInfo: function (data) {
            var self = this;

            var groupItem = new Array();



            if (data.FieldInfo) {
                if (this.config.page.dragTarget.options.dragType == "GroupItem") {
                    groupItem.push({ text: "GroupBy", value: "GroupBy" });
                } else if (this.config.page.dragTarget.options.dragType == "LogicItem") {

                    if (data.FieldInfo.IsSum) {
                        groupItem.push({ text: "Sum", value: "Sum" });
                    }
                    groupItem.push({ text: "Count", value: "Count" });
                }
            } else {
                groupItem.push({ text: "Count", value: "Count" });
                groupItem.push({ text: "Sum", value: "Sum" });
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

            var configItems = [
                    {
                        key: "title_temp",
                        title: "項目名",
                        type: "TextBox",
                        readonly: true,
                        required: false,
                        value: data.titleTemp

                    }, {
                        key: "title",
                        title: "見出し",
                        type: "TextBox",
                        readonly: true,
                        required: false,
                        value: data.title

                    }, {
                        key: "width",
                        title: "幅",
                        type: "TextBox",
                        value: data.width

                    }
                    , {
                        key: "format",
                        title: "ﾌｫｰﾏｯﾄ",
                        type: "ComboBox",
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: [
                        ],
                        value: data.format
                    }, {
                        key: "orderBy",
                        title: "並び替え",
                        type: "ButtonGroup",
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: [
                            { text: "なし", value: "" },
                            { text: "昇順", value: "1" },
                            { text: "降順", value: "-1" }
                        ],
                        value: data.orderBy
                    }
            ];

            if (data.group) {
                configItems.push(
                    {
                        key: "group",
                        title: "集計方法",
                        type: "DropDownList",
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: groupItem,
                        value: data.group
                    }
              )
            }

            smat.service.getUserConfig({
                title: smat.service.optionSet("SysText.Confirm"),
                items: configItems,
                callback: function (result) {
                    var view = self.config.page.getEditView(self.config.view);

                    //remove empty col
                    if (self.config.columns.length == 1 && self.config.columns[0].field == "colTemp") {
                        self.config.columns = new Array();
                    }

                    var col = {
                        field: data.FieldName,
                        title: result.title,
                        width: result.width,
                        format: result.format
                    }
                    self.config.columns.push(col);

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

                    if (result.group) {
                        viewItem.Group = result.group;
                    }

                    viewItem.Format = result.format;
                    viewItem.OrderBy = result.orderBy;
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

            var uiConfig = this.getUiConfig();

            if (uiConfig.columns == undefined || uiConfig.columns.length == 0) {
                uiConfig.columns = [
                    {
                        field: "colTemp",
                        title: "　　"
                    }
                ]
            }
            if (this.config.designing == true) {

                if (uiConfig.dataSource == undefined || uiConfig.columns.length == 0) {
                    uiConfig.dataSource = new Array();
                    for (var i = 0; i < 5; i++) {
                        var tempData = {};

                        if (uiConfig.columns != undefined) {
                            for (var j = 0; j < uiConfig.columns.length; j++) {
                                var field = uiConfig.columns[j];
                                tempData[field.field] = "　　";

                            }
                        }

                        tempData["colTemp"] = "　　";
                        uiConfig.dataSource.push(tempData);
                    }
                }

                if (uiConfig.category != undefined && uiConfig.category != ""
                    && uiConfig.dataField != undefined && uiConfig.dataField != ""
                    && uiConfig.series != undefined && uiConfig.series != null && typeof (uiConfig.series) == 'object' && uiConfig.series.length > 0) {
                    var seriesFieldName = uiConfig.series[0].seriesField;

                    uiConfig.series[0].width = this.seriesWidth;
                    uiConfig.category_format = "";
                    uiConfig.category_width = "";

                    var tempV = "　";
                    for (var k in uiConfig.dataSource) {
                        uiConfig.dataSource[k][seriesFieldName] = tempV;
                        uiConfig.dataSource[k][uiConfig.category] = uiConfig.category;
                        tempV += " ";
                    }

                    if (uiConfig.series.length > 1) {
                        uiConfig.series.splice(1, uiConfig.series.length - 1);
                    }
                }

            }
            this.uiControl = new smat.Grid(uiConfig);
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

        },
        initCustomEditSkin: function () {
            var self = this;
            var pageTemplate = this.config.page.config.designer.template;
            if (pageTemplate.config.type == "SimpleCrossStatistic") {

                //clear text 
                this.body.find('thead th').text("　");

                this.customEditSkinBox = $('<div id="cus_skin_' + this.uuid + '" class="" style=" z-index:' + this.config.page.skinZindex + ';position: absolute;top: -1px;left: -1px;width: 100%;height: 100%;"></div></div>');
                //this.editSkinBody.find(':first').before(this.editSkinBox);
                this.customEditSkinBox.appendTo(this.editSkinBody);


                var vBoxLeft = Number(this.seriesWidth);
                var xFieldBoxHeight = this.body.find('thead').height();

                this.xFieldBox = $('<div class="col-drop" drop-type="xField" style="border: 1px solid #1924F9;position: absolute;top: -1px;left: ' + vBoxLeft + 'px;width: calc(100% - ' + vBoxLeft + 'px);height: ' + xFieldBoxHeight + 'px;"></div></div>').appendTo(this.customEditSkinBox);
                $('<div class="" drop-type="xField" style="background-color: #DBDCF9;opacity: 0.6;position: absolute;top: -1px;left: -1px;width: 100%;height:100%;"></div>').appendTo(this.xFieldBox);

                //$('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">列:</span>').appendTo(this.xFieldBox);
                this.emptyBox = $('<div class="" style="position: absolute;top: -1px;left: -1px;width: ' + vBoxLeft + 'px;height: ' + xFieldBoxHeight + 'px;"></div></div>').appendTo(this.customEditSkinBox);
                if (this.config.category) {
                    $('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">' + this.config.category_title + '</span>').appendTo(this.xFieldBox);
                    this.xDelBtn = $('<button class="btn-danger s-button" style="padding: 1px 5px;position: absolute;top: 2px;right: 2px;"><i class="fa fa-trash-o"></i></button>').appendTo(this.xFieldBox);
                    this.xDelBtn.bind('click', function (e) {
                        var view = self.config.page.getEditView(self.config.view);

                        if (self.config.category != undefined) {
                            smat.service.delItemByKey(view.ItemList, "ItemName", self.config.category);
                        }

                        self.config.category_format = undefined;
                        self.config.category_width = undefined;
                        self.config.category_title = undefined;
                        self.config.category = undefined;
                        self.refresh();
                        e.stopPropagation();
                    });

                    this.xEditBtn = $('<button class="btn-primary s-button" style="padding:1px 2px 1px 3px;position: absolute;top: 2px;right: 32px;"><i class="fa fa-edit"></i></button>').appendTo(this.xFieldBox);
                    this.xEditBtn.bind('click', function (e) {
                        self.fillCategoryInfo({
                            title: self.config.category_title,
                            FieldName: self.config.category,
                            format: self.config.category_format,
                            width: self.config.category_width

                        });

                        e.stopPropagation();
                    });
                }

                var yFieldBoxHeight = this.body.height() - xFieldBoxHeight;
                this.yFieldBox = $('<div class="col-drop" drop-type="yField" style="border: 1px solid #1924F9;position: absolute;top: ' + xFieldBoxHeight + 'px;left: -1px;width: ' + vBoxLeft + 'px;height: ' + yFieldBoxHeight + 'px;"></div></div>').appendTo(this.customEditSkinBox);
                $('<div class="" drop-type="yField" style="background-color: #DBDCF9;opacity: 0.6;position: absolute;top: -1px;left: -1px;width: 100%;height:100%;"></div>').appendTo(this.yFieldBox);
                this.vFieldBox = $('<div class="col-drop" drop-type="vField" style="border: 1px solid #1924F9;position: absolute;top: ' + xFieldBoxHeight + 'px;left: ' + vBoxLeft + 'px;width: calc(100% - ' + vBoxLeft + 'px);height: ' + yFieldBoxHeight + 'px;"></div></div>').appendTo(this.customEditSkinBox);
                $('<div class="" drop-type="vField" style="background-color: #EEF7C2;opacity: 0.6;position: absolute;top: -1px;left: -1px;width: 100%;height:100%;"></div>').appendTo(this.vFieldBox);

                //$('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">行:</span>').appendTo(this.yFieldBox);
                if (this.config.series != undefined && this.config.series != null && typeof (this.config.series) == 'object' && this.config.series.length > 0) {

                    var yTop = 2;
                    for (var k in this.config.series) {
                        var s = this.config.series[k];

                        $('<span style="position: absolute;top: ' + yTop + 'px;display: block;left: 2px;color: #000;font-size: 16px;font-weight: bold;">' + s.seriesTitle + '</span>').appendTo(this.yFieldBox);
                        var yDelBtn = $('<button class="btn-danger s-button" key="' + s.seriesField + '" style="padding: 1px 5px;position: absolute;top: ' + yTop + 'px;right: 2px;"><i class="fa fa-trash-o"></i></button>').appendTo(this.yFieldBox);
                        yDelBtn.bind('click', function (e) {
                            var se = smat.service.getItemByKey(self.config.series, "seriesField", $(this).attr('key'));
                            var view = self.config.page.getEditView(self.config.view);

                            smat.service.delItemByKey(view.ItemList, "ItemName", se.seriesField);

                            smat.service.delItemByKey(self.config.series, "seriesField", se.seriesField);
                            self.refresh();
                            e.stopPropagation();
                        });

                        var yEditBtn = $('<button class="btn-primary s-button" key="' + s.seriesField + '" style="padding:1px 2px 1px 3px;position: absolute;top: ' + yTop + 'px;right: 32px;"><i class="fa fa-edit"></i></button>').appendTo(this.yFieldBox);
                        yEditBtn.bind('click', function (e) {
                            var se = smat.service.getItemByKey(self.config.series, "seriesField", $(this).attr('key'));
                            self.fillSeriesFieldInfo({
                                title: se.seriesTitle,
                                FieldName: se.seriesField
                            });
                            e.stopPropagation();
                        });

                        yTop += 30;
                    }

                }

                //$('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">データ:</span>').appendTo(this.vFieldBox);
                if (this.config.dataField) {
                    $('<span style="position: absolute;top: 2px;display: block;left: 10px;color: #000;font-size: 16px;font-weight: bold;">' + this.config.dataField_title + '</span>').appendTo(this.vFieldBox);
                    this.vDelBtn = $('<button class="btn-danger s-button" style="padding: 1px 5px;position: absolute;top: 2px;right: 2px;"><i class="fa fa-trash-o"></i></button>').appendTo(this.vFieldBox);
                    this.vDelBtn.bind('click', function (e) {
                        var view = self.config.page.getEditView(self.config.view);

                        if (self.config.dataField != undefined) {
                            smat.service.delItemByKey(view.ItemList, "ItemName", self.config.dataField);

                        }

                        self.config.dataField_title = undefined;
                        self.config.dataField_format = undefined;
                        self.config.dataField = undefined;
                        self.refresh();
                        e.stopPropagation();
                    });

                    this.vEditBtn = $('<button class="btn-primary s-button" style="padding:1px 2px 1px 3px;position: absolute;top: 2px;right: 32px;"><i class="fa fa-edit"></i></button>').appendTo(this.vFieldBox);
                    this.vEditBtn.bind('click', function (e) {

                        var view = self.config.page.getEditView(self.config.view);

                        //view
                        var viewItem = smat.service.getItemByKey(view.ItemList, "ItemName", self.config.dataField);

                        self.fillValueFieldInfo({
                            title: self.config.dataField_title,
                            FieldName: self.config.dataField,
                            format: self.config.dataField_format,
                            group: viewItem.Group

                        });
                        e.stopPropagation();
                    });
                }

                this.customEditSkinBox.asmatDropTargetArea({
                    filter: ".col-drop",
                    dragenter: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDragEnter(e);
                    },
                    dragleave: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDragLeave(e);
                    },
                    drop: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDrop(e);
                    }
                });

                this.initContextMenu("#" + this.customEditSkinBox.attr('id'));

                this.customEditSkinBox.find('.col-drop').bind('click', function (e) {
                    self.active();
                    e.stopPropagation();
                });

            } else if (pageTemplate.config.type == "SimpleStatistics") {

                this.customEditSkinBox = $('<div id="cus_skin_' + this.uuid + '" class="" style=" z-index:' + this.config.page.skinZindex + ';position: absolute;top: -1px;left: -1px;width: 100%;height: 100%;"></div></div>');

                this.customEditSkinBox.appendTo(this.editSkinBody);


                this.xFieldBox = $('<div class="col-drop" drop-type="statisticsField" style="position: absolute;top: -1px;left: -1px;width: 100%;height: 100%;"></div></div>').appendTo(this.customEditSkinBox);

                this.customEditSkinBox.asmatDropTargetArea({
                    filter: ".col-drop",
                    dragenter: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDragEnter(e);
                    },
                    dragleave: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDragLeave(e);
                    },
                    drop: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDrop(e);
                    }
                });

                this.initContextMenu("#" + this.customEditSkinBox.attr('id'));

                this.customEditSkinBox.find('.col-drop').bind('click', function (e) {
                    self.active();
                    e.stopPropagation();
                });

            } else if (pageTemplate.config.type == "SimpleSearch") {

                this.customEditSkinBox = $('<div id="cus_skin_' + this.uuid + '" class="" style=" z-index:' + this.config.page.skinZindex + ';position: absolute;top: -1px;left: -1px;width: 100%;height: 100%;"></div></div>');

                this.customEditSkinBox.appendTo(this.editSkinBody);


                this.xFieldBox = $('<div class="col-drop" drop-type="searchField" style="position: absolute;top: -1px;left: -1px;width: 100%;height: 100%;"></div></div>').appendTo(this.customEditSkinBox);

                this.customEditSkinBox.asmatDropTargetArea({
                    filter: ".col-drop",
                    dragenter: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDragEnter(e);
                    },
                    dragleave: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDragLeave(e);
                    },
                    drop: function (e) {
                        if ($(e.dropTarget).closest('.s-grid').dynamicsUI() != null) $(e.dropTarget).closest('.s-grid').dynamicsUI().droptargetOnDrop(e);
                    }
                });

                this.initContextMenu("#" + this.customEditSkinBox.attr('id'));

                this.customEditSkinBox.find('.col-drop').bind('click', function (e) {
                    self.active();
                    e.stopPropagation();
                });

            }

        }, checkPropertyChanging: function (property, value) {
            var isOk = true;

            return isOk;
        }, getCustomPropertyConfig: function () {

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'view',
                type: 'View',
                id: 'view',
                shortcutMenu: true,
                cmt: 'view',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'detailTemplate',
                type: 'Template',
                id: 'detailTemplate',
                cmt: 'detailTemplate',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'data',
                caption: 'groupable',
                type: 'DropDownList',
                id: 'groupable',
                cmt: 'groupable',
                propType: "prop",
                dataSource: [
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: ""
                        }]
            });

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'pageable',
                type: 'Object',
                id: 'pageable',
                cmt: 'pageable',
                shortcutMenu: false,
                propType: "prop",
                titleKey: "field",
                optionConfig: [
                    {
                        group: 'data',
                        caption: 'refresh',
                        type: 'DropDownList',
                        id: 'refresh',
                        cmt: 'refresh',
                        propType: "prop",
                        dataSource: [
                                {
                                    text: "true",
                                    value: true
                                },
                                {
                                    text: "false",
                                    value: false
                                }]
                    }, {
                        group: 'data',
                        caption: 'pageSizes',
                        type: 'DropDownList',
                        id: 'pageSizes',
                        cmt: 'pageSizes',
                        propType: "prop",
                        dataSource: [
                                {
                                    text: "true",
                                    value: true
                                },
                                {
                                    text: "false",
                                    value: false
                                }]
                    }, {
                        group: 'data',
                        caption: 'buttonCount',
                        type: 'number',
                        id: 'buttonCount',
                        cmt: 'buttonCount',
                        propType: "prop"
                    }
                ]
            });

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'select',
			         type: 'Logic',
			         id: 'select',
			         cmt: 'select',
			         eventKey: 'grid_select',
			         propType: "event"
			     }
             );

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'dataBound',
			         type: 'Logic',
			         id: 'dataBound',
			         cmt: 'dataBound',
			         eventKey: 'grid_dataBound',
			         propType: "event"
			     }
             );

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'excelExport',
			         type: 'Logic',
			         id: 'excelExport',
			         cmt: 'excelExport',
			         eventKey: 'grid_excelExport',
			         propType: "event"
			     }
             );

            //this.editPropertyConfig.push(
            //{
            //    group: 'data',
            //    caption: 'selectable',
            //    type: 'DropDownList',
            //    id: 'selectable',
            //    cmt: 'selectable',
            //    propType: "prop",
            //    dataSource: [
            //       {
            //           text: "",
            //           value: ""
            //       },
            //       {
            //           text: "true",
            //           value: "true"
            //       }]
            //});

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'aggregate',
                type: 'SubOptions',
                id: 'aggregate',
                cmt: 'aggregate',
                shortcutMenu: false,
                propType: "prop",
                titleKey: "field",
                optionConfig: [
                    {
                        group: 'data',
                        caption: 'field',
                        type: 'text',
                        id: 'field',
                        cmt: 'field',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'aggregate',
                        type: 'text',
                        id: 'aggregate',
                        cmt: 'aggregate',
                        propType: "prop"
                    }
                ]
            });

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'columns',
                type: 'SubOptions',
                id: 'columns',
                cmt: 'columns',
                shortcutMenu: false,
                propType: "prop",
                titleKey: "title",
                optionConfig: [
                    {
                        group: 'data',
                        caption: 'title',
                        type: 'CultureText',
                        id: 'title',
                        cmt: 'title',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'field',
                        type: 'text',
                        id: 'field',
                        cmt: 'field',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'aggregates',
                        type: 'text',
                        id: 'aggregates',
                        cmt: 'aggregates',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'groupable',
                        type: 'DropDownList',
                        id: 'groupable',
                        cmt: 'groupable',
                        propType: "prop",
                        dataSource: [
                                {
                                    text: "true",
                                    value: "true"
                                },
                                {
                                    text: "false",
                                    value: "false"
                                }]
                    }
                    , {
                        group: 'data',
                        caption: 'rowSpanFields',
                        type: 'text',
                        id: 'rowSpanFields',
                        cmt: 'rowSpanFields',
                        propType: "prop"
                    }

                    , {
                        group: 'data',
                        caption: 'footerTemplate',
                        type: 'Template',
                        id: 'footerTemplate',
                        cmt: 'footerTemplate',
                        eventKey: 'grid_column_template',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'groupHeaderTemplate',
                        type: 'Template',
                        id: 'groupHeaderTemplate',
                        cmt: 'groupHeaderTemplate',
                        eventKey: 'grid_column_template',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'groupFooterTemplate',
                        type: 'Template',
                        id: 'groupFooterTemplate',
                        cmt: 'groupFooterTemplate',
                        eventKey: 'grid_column_template',
                        propType: "prop"
                    }
                    , {
                        group: 'data',
                        caption: 'template',
                        type: 'Template',
                        id: 'template',
                        cmt: 'template',
                        eventKey: 'grid_column_template',
                        propType: "prop"
                    }
                    , {
                        group: 'data',
                        caption: 'dataType',
                        type: 'DropDownList',
                        id: 'dataType',
                        cmt: 'dataType',
                        propType: "prop",
                        dataSource: [
                            {
                                text: "text",
                                value: "text"
                            },
                            {
                                text: "dropDownList",
                                value: "dropDownList"
                            },
                            {
                                text: "Date",
                                value: "Date"
                            }, {
                                text: "checkBox-selecter",
                                value: "checkBox-selecter"
                            },
                            {
                                text: "onlyNum",
                                value: "onlyNum"
                            },
                            {
                                text: "onlyAlpha",
                                value: "onlyAlpha"
                            },
                            {
                                text: "DropDownList",
                                value: "DropDownList"
                            },
                            {
                                text: "onlyNumAlpha",
                                value: "onlyNumAlpha"
                            }
                        ]
                    }, {
                        group: 'data',
                        caption: 'checkAll',
                        type: 'DropDownList',
                        id: 'checkAll',
                        cmt: 'checkAll',
                        propType: "prop",
                        dataSource: [
                            {
                                text: "true",
                                value: "true"
                            },
                            {
                                text: "false",
                                value: ""
                            }]
                    }, {
                        group: 'data',
                        caption: 'editable',
                        type: 'dropDownList',
                        id: 'editable',
                        cmt: 'editable',
                        propType: "prop",
                        dataSource: [
                            {
                                text: "true",
                                value: "true"
                            },
                            {
                                text: "InCell",
                                value: "InCell"
                            }
                        ]
                    }, {
                        group: 'data',
                        caption: 'codeKind',
                        type: 'text',
                        id: 'codeKind',
                        cmt: 'codeKind',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'maxlength',
                        type: 'text',
                        id: 'maxlength',
                        cmt: 'maxlength',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'editorDataSource',
                        type: 'dataSource',
                        id: 'editorDataSource',
                        cmt: 'editorDataSource',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'valueField',
                        type: 'text',
                        id: 'valueField',
                        cmt: 'valueField',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'attributes',
                        type: 'Attributes',
                        id: 'attributes',
                        cmt: 'attributes',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'width',
                        type: 'text',
                        id: 'width',
                        cmt: 'width',
                        propType: "prop"
                    }, {
                        group: 'action',
                        caption: 'actions',
                        type: 'SubOptions',
                        id: 'actions',
                        cmt: 'actions',
                        propType: "prop",
                        titleKey: "text",
                        optionConfig: [
                            {
                                group: 'action',
                                caption: 'text',
                                type: 'text',
                                id: 'text',
                                cmt: 'text',
                                propType: "prop"
                            },
                            {
                                group: 'action',
                                caption: 'actionConfirm',
                                type: 'Logic',
                                id: 'actionConfirm',
                                cmt: 'actionConfirm',
                                eventKey: 'grid_actionConfirm',
                                propType: "prop"
                            },
                            {
                                group: 'action',
                                caption: 'actionType',
                                type: 'text',
                                id: 'actionType',
                                cmt: 'actionType',
                                propType: "prop"
                            },
                            {
                                group: 'action',
                                caption: 'click',
                                type: 'Logic',
                                id: 'click',
                                cmt: 'click',
                                eventKey: 'grid_actionClick',
                                propType: "prop"
                            },
                            {
                                group: 'action',
                                caption: 'template',
                                type: 'text',
                                id: 'template',
                                cmt: 'template',
                                propType: "prop"
                            }
                        ]
                    }
                ]
            });

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'columnsSort',
                type: 'Sortable',
                id: 'columns',
                shortcutMenu: true,
                showInPanel: false,
                cmt: 'columns',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'data',
                caption: 'data',
                type: 'GridData',
                id: ' ',
                shortcutMenu: true,
                cmt: 'data',
                propType: "prop"
            });
        },
        propertyChange_dataType: function (property, value) {

        },
        propertyChange_columns: function (property, value) {
            this.refresh();
        },
        propertyChange_view: function (property, value, valueConfig, view) {

            if (this.config.series != undefined && this.config.series != null && this.config.series.length > 0) {
                return;
            }

            if ((this.config.columns.length == 1 && this.config.columns[0].field == "colTemp") == false) {
                return;
            }

            var cols = this.config.columns;
            var viewItems = view.ItemList;

            this.config.columns = new Array();
            var propDataItem = new Array();
            var propDataItemObj = smat.service.getItemByKey(this.editPropertyConfig, "id", "columns");
            if (propDataItemObj != null) {
                propDataItem = propDataItemObj.value;
                propDataItem.splice(0, propDataItem.length);
            }

            for (var key in cols) {
                if ($.Enumerable.From(viewItems).Any("$.ItemName == '" + cols[key].field + "'")) {
                    this.config.columns.push(cols[key]);
                    propDataItem.push(cols[key]);
                }
            }

            for (var key in viewItems) {
                if ($.Enumerable.From(this.config.columns).Any("$.field == '" + viewItems[key].ItemName + "'") == false) {
                    this.config.columns.push({
                        field: viewItems[key].ItemName,
                        title: viewItems[key].ItemDesc
                    });
                    propDataItem.push({
                        field: viewItems[key].ItemName,
                        title: viewItems[key].ItemDesc
                    });
                }
            }

            this.refresh(true);
        },
        afterGetSaveParams: function (param) {
            //if (this.config.view != undefined) {
            //    var view = smat.service.getItemByKey(this.config.page.editViewList, "ViewName", this.config.view);
            //    if (view == undefined) {
            //        view = smat.service.getItemByKey(this.config.page.entity.ViewList, "ViewName", this.config.view);
            //    }

            //    if (view != undefined) {
            //        view.ItemList = new Array();

            //        for (var index in this.config.columns) {
            //            var col = this.config.columns[index];
            //            view.ItemList.push({
            //                ProjID: view.ProjID,
            //                EntityName: view.EntityName,
            //                ViewName: view.ViewName,
            //                ItemName: col.ItemName,
            //                Seq:index,
            //                Path: col.Path,
            //                ItemDesc: col.ItemDesc,
            //                ItemSql: col.ItemSql,
            //                ItemEntityName: col.ItemEntityName,
            //                Format: col.Format,
            //                OrderBy: col.OrderBy,
            //                EntityAlias: col.EntityAlias
            //            });
            //        }
            //    }
            //}
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Grid, smat.dynamics.Field);
})();