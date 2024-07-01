
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetBusinessResult
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetBusinessResult = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetBusinessResult.prototype = {
        initDom: function () {
            var self = this;
            if ($(this.config.container).hasClass("form-horizontal") == false) {
                $(this.config.container).addClass("form-horizontal");
            }


            this.box = $("<div style='padding: 0 40px 0 20px;'></div>").appendTo($(this.config.container));

            this.bar = $("<div class='row' style='display:none;'></div>").appendTo(this.box);

            this.resultRow = $("<div class='row'></div>").appendTo(this.box);

            this.editModeInput = $("<input />").appendTo(this.bar);

            this.editModeInput.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: "縦表示",
                        value: "1"
                    },
                    {
                        text: "横表示",
                        value: "2"
                    }
                ]
            })


            this.editGridBox = $("<div ></div>").appendTo(this.resultRow);

            this.editGridBox.smatGrid({
                columns: [
                    {
                        title: "　　",
                        field: "colTemp",
                    }
                ],
                dataBound: function (be) {
                    var upBtns = self.editGridBox.find("button.item-up");
                    upBtns.bind("click", function () {
                        self.moveViewItemIndex($(this).attr("seq"), -1);
                    })

                    var downBtns = self.editGridBox.find("button.item-down");
                    downBtns.bind("click", function () {
                        self.moveViewItemIndex($(this).attr("seq"), 1);
                    });

                    var g = be.sender;
                    var trs = self.editGridBox.find("tbody tr");
                    $.each(trs, function () {
                        
                        var dataItem = g.dataItem($(this));
                        if (dataItem.error) {
                            $(this).find("td").css("background-color", "rgb(245, 245, 245)");
                        } else {
                            $(this).find("td").css("background-color", "#fff");
                        }
                    })
                }
            });

            this.editGrid = this.editGridBox.ui();

        }, setData: function () {
            var self = this;
            this.resultUi = this.config.page.getControlByName("result");

            var form = this.config.page.getControlByName("search_form");

            this.view = this.config.page.getEditView(form.config.view);

            if (!this.resultUi) {
                return;
            }

            //縦表示
            if (this.editModeInput.ui().value() == "1") {
                var editColumns = [
                        {
                            title: "区分",
                            field: "itemDiv",
                            width: "100px",
                            rowSpanFields: "itemDiv",
                            template: function (dataItem) {

                                var result = "";

                                if (dataItem.itemDiv == "1") {
                                    result = "抽出項目";
                                } else if (dataItem.itemDiv == "2") {
                                    result = "集計項目";
                                } else if (dataItem.itemDiv == "3") {
                                    result = "縦集計キー";
                                } else if (dataItem.itemDiv == "4") {
                                    if (self.resultUi.config.resultType == "SetSummarySearch"
                                        || self.resultUi.config.resultType == "SetPieChart") {
                                        result = "集計キー";
                                    } else {
                                        result = "横集計キー";
                                    }
                                }

                                return result;
                            }
                        },
                        {
                            title: "項目",
                            field: "itemDesc"
                        },
                        {
                            title: "表示形式",
                            field: "itemFormat",
                            width: "200px",
                            template: function (dataItem) {
                                if (dataItem.IsHideInView == true) return "非表示";

                                if (dataItem.itemFormat == "=Date(YMD)") return "年月日";
                                if (dataItem.itemFormat == "=Date(YM)") return "年月";
                                if (dataItem.itemFormat == "=Date(Year)") return "年";

                                if (dataItem.itemFormat) {

                                    if (dataItem.itemFormat.indexOf("=Name(") == 0) {
                                        var optKey = dataItem.itemFormat.replace("=Name(", "");
                                        optKey = optKey.substring(0, optKey.length - 1);

                                        return smat.service.optionSet("CodeType." + optKey);
                                    } else if (dataItem.itemFormat == "{0:N0}") {
                                        return "カンマ";
                                    }

                                    return dataItem.itemFormat;
                                } else {
                                    return "";
                                }
                            }
                        },
                        {
                            title: "並替え",
                            width: "100px",
                            field: "itemOrderBy",
                            attributes: {
                                "class": "text-center"
                            },
                            template: function (dataItem) {
                                if (dataItem.itemOrderBy == "-1") {
                                    return "降順";
                                } else if (dataItem.itemOrderBy == "1") {
                                    return "昇順";
                                } else {
                                    return "";
                                }
                            }
                        },
                        {
                            title: " ",
                            field: "itemDiv",
                            attributes: {
                                "class": "text-center"
                            },
                            width: "80px",
                            template: function (dataItem) {
                                var hasUp = true, hasDown = true;
                                if (dataItem.seq == 1) {
                                    hasUp = false;
                                }

                                if (dataItem.seq == self.view.ItemList.length) {
                                    hasDown = false;
                                }

                                var preDiv = "", nextDiv = "";
                                if (dataItem.seq > 1) {
                                    preDiv = self.view.ItemList[dataItem.seq - 2].ItemCategory;
                                }

                                if (dataItem.seq < self.view.ItemList.length) {
                                    nextDiv = self.view.ItemList[dataItem.seq].ItemCategory;
                                }

                                if (preDiv != dataItem.itemDiv) {
                                    hasUp = false;
                                }

                                if (nextDiv != dataItem.itemDiv) {
                                    hasDown = false;
                                }

                                var result = "";
                                if (hasUp) {
                                    result = result+'<button class="btn-primary s-button item-up" seq="' + dataItem.seq + '"  style="padding: 8px 2px;min-width: 26px;" >↑</button>';
                                } else {
                                    result = result + '<button class="s-button s-state-disabled" style="padding: 8px 2px;min-width: 26px;" disabled="disabled">↑</button>';
                                }

                                if (hasDown) {
                                    result = result + '<button class="btn-primary s-button item-down"  seq="' + dataItem.seq + '" style="padding: 8px 2px;min-width: 26px;" >↓</button>';
                                } else {
                                    result = result + '<button class=" s-button s-state-disabled" style="padding: 8px 2px;min-width: 26px;" disabled="disabled">↓</button>';
                                }

                                //if (hasUp && hasDown) {
                                //    result = result + '<button class="btn-primary s-button item-up" seq="' + dataItem.seq + '"  style="padding: 8px 2px;min-width: 26px;" >↑</button>';
                                //    result = result + '<button class="btn-primary s-button item-down"  seq="' + dataItem.seq + '" style="padding: 8px 2px;min-width: 26px;" >↓</button>';
                                //} else if (hasUp) {
                                //    result = result + '<button class="btn-primary s-button item-up" seq="' + dataItem.seq + '"  style="padding: 8px 2px;min-width: 52px;" >↑</button>';
                                //} else if (hasDown) {

                                //    result = result + '<button class="btn-primary s-button item-down"  seq="' + dataItem.seq + '" style="padding: 8px 2px;min-width: 52px;" >↓</button>';
                                //}

                                return result;
                            }
                        }, {
                            field: "",
                            title: "",
                            width: "80px",
                            attributes: {
                                "class": "text-center"
                            },
                            actions: [
                                {
                                    text: '詳細',
                                    click: function (dataItem) {
                                        self._editViewItem(dataItem.itemName);
                                    }
                                }
                            ]

                        }
                ]

                var editDataSource = new Array();


                //for (var key in this.resultUi.config.columns) {
                //    var c = this.resultUi.config.columns[key];
                //    if (c.field == "colTemp") {
                //        continue;
                //    }

                //    editDataSource.push({
                //        itemDiv: c.itemDiv,
                //        itemName: c.title,
                //        itemFormat: c.itemFormat,
                //        itemDataType: c.itemDataType,
                //        itemEntity: c.itemEntity,
                //        itemKey: c.field,
                //        itemOrderBy: c.itemOrderBy
                //    })
                //}

                //reOrder viewItems
                this.view.ItemList = $.Enumerable.From(this.view.ItemList).OrderByDescending("$.ItemCategory").ThenBy("Number($.Seq)").ToArray();

                var vseq = 1;
                for (var key in this.view.ItemList) {
                    var c = this.view.ItemList[key];
                    c.Seq = vseq;
                    vseq++;

                    editDataSource.push({
                        itemDiv: c.ItemCategory,
                        itemDesc: c.ItemDesc,
                        itemFormat: c.Format,
                        itemEntity: c.ItemEntityName,
                        itemField: c.ItemFieldName,
                        itemName: c.ItemName,
                        IsHideInView: c.IsHideInView,
                        error: c.error,
                        seq: c.Seq,
                        itemOrderBy: c.OrderBy
                    })
                }



                this.editGrid.config.columns = editColumns;

                //this.editGrid.config.selectable = true;
                this.editGrid.editInfo = this.editGrid.getEditInfo(this.editGrid.config.columns);
                this.editGrid.rowSpanInfo = this.editGrid.getRowSpanInfo(this.editGrid.config.columns);
                this.editGrid.setDataSource(editDataSource);

            } else {

            }

            this._setResultUiConfig();

        }, _editViewItem: function (itemName) {
            var self = this;
            var viewItem = smat.service.getItemByKey(this.view.ItemList, "ItemName", itemName);

            var c;
            if (viewItem.ItemEntityName == this.config.page.config.designer.data.EntityName) {
                fieldItem = smat.service.getItemByKey(this.config.page.config.designer.data.FieldList, "FieldName", viewItem.ItemFieldName);
            } else {
                var params = {};
                params.request = {};
                params.request.ProjID = this.view.ProjID;

                params.request.DsRequests = new Array();

                params.request.DsRequests.push(
                   {
                       TableName: "Y_EntityField",
                       Filter: "ProjID = '" + this.view.ProjID + "' and EntityName = '" + viewItem.ItemEntityName + "' and FieldName = '" + viewItem.ItemFieldName + "'"
                   }
                );

                smat.service.loadJosnData({
                    url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                    params: params,
                    async: false,
                    success: function (result) {
                        fieldItem = result.ds["Y_EntityField"][0];
                        smat.service.closeLoding();

                    }

                });

            }

            if (!this.view.ViewFilterList) {
                this.view.ViewFilterList = [];
            }

            var filterItem, havingFilterItem;
            for (var key in this.view.ViewFilterList) {
                var filterItemTpem = smat.service.getItemByKey(this.config.page.entity.FilterList, "FilterName", this.view.ViewFilterList[key].FilterControlName);

                if (filterItemTpem.FilterDesc != "ViewItemFilter:" + viewItem.ItemName) {
                    continue;
                }

                if (filterItemTpem && filterItemTpem.IsHaving == false) {
                    filterItem = filterItemTpem;
                } else if (filterItemTpem && filterItemTpem.IsHaving == true) {
                    havingFilterItem = filterItemTpem;
                }
            }

            //groupItem
            var configItems = [];
            var groupItem = new Array();

            if (!fieldItem) {
                groupItem.push({ text: "件数", value: "Count" });
            } else if (viewItem.ItemCategory == "2") {
                if (fieldItem && fieldItem.IsSum) {
                    groupItem.push({ text: "集計", value: "Sum" });
                } else {
                    groupItem.push({ text: "件数", value: "Count" });
                }
            } else if (viewItem.ItemCategory == "3") {
                groupItem.push({ text: "集計キー", value: "GroupBy" });
            } else if (viewItem.ItemCategory == "4") {
                groupItem.push({ text: "集計キー", value: "GroupBy" });
            }

            
            

            var descName = viewItem.Title;
            //if (fieldItem) descName = fieldItem.FieldDesc;
            configItems.push({
                key: "FieldDesc",
                title: "項目名",
                style: " width: 340px;",
                type: "TextBox",
                enable: false,
                value: descName

            });
            configItems.push({
                key: "ItemDesc",
                title: "見出",
                style: " width: 340px;",
                type: "TextBox",
                value: viewItem.ItemDesc

            });

            
            var formatItem = [];
            var textField = "text";
            var valueField = "value";
            if (fieldItem && fieldItem.DataType == "Date") {
                formatItem = [
                    { text: "年月日", value: "=Date(YMD)" },
                    { text: "年月", value: "=Date(YM)" },
                    { text: "年", value: "=Date(Year)" }
                ];

            } else if (fieldItem && fieldItem.IsSum) {
                formatItem = [
                    { text: "カンマ", value: "{0:N0}" }
                ];
            } else if (fieldItem && fieldItem.EntityName == "T_Period") {
                formatItem = [

                ];
            } else if (!fieldItem && viewItem.ItemCategory == "2") {
                formatItem = [
                    { text: "カンマ", value: "{0:N0}" }
                ];
            } else if (viewItem.Format == "{0:N0}") {
                formatItem = [
                    { text: "カンマ", value: "{0:N0}" }
                ];
            } else {
                formatItem = [];

                var optionItems = smat.service.optionSet("CodeType");
                formatItem.push({
                    text: " ", value: ""
                })
                for (var key in optionItems) {

                    if (optionItems[key].LogicType != "1") {
                        continue;
                    }

                    if ("=Name(" + optionItems[key][smat.uiConfig.CodeMst.codeField] + ")" != viewItem.Format) {

                        if (fieldItem.OptionSet != optionItems[key].CD)
                        {
                            continue;
                        }

                    }

                    formatItem.push({
                        text: optionItems[key][smat.uiConfig.CodeMst.nameField], value: "=Name(" + optionItems[key][smat.uiConfig.CodeMst.codeField] + ")"
                    })
                }
            }

            

            configItems.push({
                key: "Format",
                title: "表示形式",
                type: "ComboBox",
                dataTextField: textField,
                dataValueField: valueField,
                dataSource: formatItem,
                value: viewItem.Format

            });
            configItems.push({
                key: "OrderBy",
                title: "並び替え",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "なし", value: "" },
                    { text: "昇順", value: "1" },
                    { text: "降順", value: "-1" }
                ],
                value: viewItem.OrderBy
            });


            configItems.push({
                key: "IsHideInView",
                title: "表示",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "表示", value: "false" },
                    { text: "非表示", value: "true" }
                ],
                value: viewItem.IsHideInView
            });

            if (viewItem.ItemCategory == "1") {
                viewItem.Group = null;
            } else if (viewItem.ItemCategory == "2"
                || viewItem.ItemCategory == "3"
                || viewItem.ItemCategory == "4") {
                configItems.push({
                    key: "Group",
                    title: "集計",
                    type: "DropDownList",
                    dataTextField: "text",
                    dataValueField: "value",
                    dataSource: groupItem,
                    value: viewItem.Group
                });
            } 

            

            var filterText = "";
            if (filterItem) {
                filterText = filterItem.FilterSql.replace(fieldItem.EntityName + "." + fieldItem.FieldName + " ", "");
            }

            var havingFilterText = "";
            if (havingFilterItem && viewItem.Group) {
                if (fieldItem) {
                    
                    havingFilterText = havingFilterItem.FilterSql.replace("DISTINCT " + " ", "");
                    havingFilterText = havingFilterItem.FilterSql.replace(viewItem.Group + "(" + fieldItem.EntityName + "." + fieldItem.FieldName + ")" + " ", "");
                } else {
                    havingFilterText = havingFilterItem.FilterSql.replace(" COUNT(*) ", "");
                }
            }

            if (filterText) {
                if (filterText.indexOf("between") >= 0 && filterText.indexOf("and") >= 0) {
                    filterText = filterText.replace("between ", "").replace("and", "～")
                } else if (filterText.indexOf("in(") >= 0 && filterText.indexOf(")") >= 0) {
                    filterText = filterText.replace("in(", "").replace(")", "");
                }
            }

            if (havingFilterText) {
                if (havingFilterText.indexOf("between") >= 0 && havingFilterText.indexOf("and") >= 0) {
                    havingFilterText = havingFilterText.replace("between ", "").replace("and", "～")
                } else if (havingFilterText.indexOf("in(") >= 0 && havingFilterText.indexOf(")") >= 0) {
                    havingFilterText = havingFilterText.replace("in(", "").replace(")", "");
                }
            }

            if (viewItem.ItemSql == "*") {

            } else {
                configItems.push({
                    key: "FilterText",
                    title: "抽出条件",
                    type: "ComboBox",
                    dataTextField: "text",
                    dataValueField: "value",
                    dataSource: [
                        { text: "= ?", value: "= ?" },
                        { text: "> ?", value: "> ?" },
                        { text: ">= ?", value: ">= ?" },
                        { text: "< ?", value: "< ?" },
                        { text: "<= ?", value: "<= ?" },
                        { text: "?,?", value: "?,?" },
                        { text: "<> ?", value: "<> ?" },
                        { text: "? ～ ?", value: "? ～ ?" },
                        { text: "値あり", value: "is not null" },
                        { text: "値なし", value: "is null" }
                    ],
                    //memo: "数字:? =>1 文字：? => '1'",
                    style:" width: 260px;",
                    value: filterText

                });
            }

            
            //configItems.push({
            //    key: "Width",
            //    title: "幅",
            //    type: "TextBox",
            //    value: viewItem.Width

            //});

            if (viewItem.ItemCategory == "1"
                || viewItem.ItemCategory == "3"
                || viewItem.ItemCategory == "4") {
                viewItem.havingFilterText = null;
                viewItem.SumType = null;
            } else if (viewItem.ItemCategory == "2") {

                configItems.push({
                    key: "HavingFilterText",
                    title: "集計後条件",
                    type: "ComboBox",
                    dataTextField: "text",
                    style: " width: 260px;",
                    dataValueField: "value",
                    //memo: "数字:? =>1 文字：? => '1'",
                    dataSource: [
                        { text: "= ?", value: "= ?" },
                        { text: "> ?", value: "> ?" },
                        { text: ">= ?", value: ">= ?" },
                        { text: "< ?", value: "< ?" },
                        { text: "<= ?", value: "<= ?" },
                        { text: "?,?", value: "?,?" },
                        { text: "<> ?", value: "<> ?" },
                        { text: "? ～ ?", value: "? ～ ?" },
                        { text: "値あり", value: "is not null" },
                        { text: "値なし", value: "is null" }
                    ],
                    value: havingFilterText

                });
                //configItems.push({
                //    key: "SumType",
                //    title: "小計項目",
                //    type: "TextBox",
                //    value: viewItem.SumType

                //});
            }


            

            smat.service.getUserConfig({
                title: "詳細",
                width:"640px",
                items: configItems,
                callback: function (result) {

                    //del
                    if (result && result == "del") {
                        smat.service.delItemByKey(self.view.ItemList, "ItemName", viewItem.ItemName);
                        if (self.config.page.filedCheckBoxs) {
                            for (var i in self.config.page.filedCheckBoxs) {
                                self.config.page.filedCheckBoxs[i].removeUnCheckItems();
                            }
                        }
                    }

                    //edit
                    if (result) {
                        viewItem.ItemDesc = result.ItemDesc;
                        viewItem.Format = result.Format;
                        viewItem.OrderBy = result.OrderBy;
                        viewItem.IsHideInView = result.IsHideInView == "true";
                        viewItem.Group = result.Group;
                        viewItem.Width = result.Width;
                        viewItem.SumType = result.SumType;

                        if (result.FilterText) {
                            if (!filterItem) {
                                filterItem = {
                                    ProjID :viewItem.ProjID,
                                    EntityName:viewItem.EntityName,
                                    FilterName:smat.service.uuid(),
                                    Path:viewItem.Path,
                                    ItemEntityAliasName: viewItem.ItemFieldName,
                                    FilterCategory:"viewItem",
                                    IsHaving:false
                                };

                                self.config.page.entity.FilterList.push(filterItem);

                                self.config.page.entity.FilterControlList.push({
                                    ProjID: viewItem.ProjID,
                                    EntityName: viewItem.EntityName,
                                    FilterControlName: filterItem.FilterName,
                                    FilterNames: filterItem.FilterName
                                });

                                self.view.ViewFilterList.push({
                                    ProjID: viewItem.ProjID,
                                    EntityName: viewItem.EntityName,
                                    ViewName: viewItem.ViewName,
                                    FilterControlName: filterItem.FilterName,
                                    Seq: self.view.ViewFilterList.length + 1,
                                });
                            }

                            if (result.FilterText.indexOf("=") < 0
                                && result.FilterText.indexOf("is") < 0
                                && result.FilterText.indexOf("～") < 0
                                && result.FilterText.indexOf(",") < 0
                                && result.FilterText.indexOf("between") < 0
                                && result.FilterText.indexOf(">") < 0
                                && result.FilterText.indexOf("<") < 0) {

                                result.FilterText = "= " + result.FilterText;
                            } else if (result.FilterText.indexOf("～") > 0 && result.FilterText.indexOf("between") < 0) {

                                result.FilterText = "between " + result.FilterText.replace(" ～ ", " and ").replace("～", " and ");
                            } else if (result.FilterText.indexOf(",") > 0 && result.FilterText.indexOf("in") < 0) {

                                result.FilterText = "in(" + result.FilterText+")";
                            }

                            filterItem.FilterSql = fieldItem.EntityName + "." + fieldItem.FieldName + " " + result.FilterText;
                            filterItem.FilterDesc = "ViewItemFilter:" + viewItem.ItemName;

                           
                        } else {
                            if (filterItem) {
                                smat.service.delItemByKey(self.config.page.entity.FilterList, "FilterName", filterItem.FilterName);
                                smat.service.delItemByKey(self.config.page.entity.FilterControlList, "FilterControlName", filterItem.FilterName);
                                smat.service.delItemByKey(self.view.ViewFilterList, "FilterControlName", filterItem.FilterName);
                            }
                        }

                        if (result.HavingFilterText) {
                            if (!havingFilterItem) {
                                havingFilterItem = {
                                    ProjID: viewItem.ProjID,
                                    EntityName: viewItem.EntityName,
                                    FilterName: smat.service.uuid(),
                                    Path: viewItem.Path,
                                    ItemEntityAliasName: viewItem.ItemFieldName,
                                    FilterCategory: "viewItem",
                                    IsHaving: true
                                };

                                self.config.page.entity.FilterList.push(havingFilterItem);
                                self.config.page.entity.FilterControlList.push({
                                    ProjID: viewItem.ProjID,
                                    EntityName: viewItem.EntityName,
                                    FilterControlName: havingFilterItem.FilterName,
                                    FilterNames: havingFilterItem.FilterName
                                });
                                self.view.ViewFilterList.push({
                                    ProjID: viewItem.ProjID,
                                    EntityName: viewItem.EntityName,
                                    ViewName: viewItem.ViewName,
                                    FilterControlName: havingFilterItem.FilterName,
                                    Seq: self.view.ViewFilterList.length + 1,
                                });
                            }

                            if (result.HavingFilterText.indexOf("=") < 0
                                && result.HavingFilterText.indexOf("is") < 0
                                && result.HavingFilterText.indexOf("～") < 0
                                && result.HavingFilterText.indexOf(",") < 0
                                && result.HavingFilterText.indexOf("between") < 0
                                && result.HavingFilterText.indexOf(">") < 0
                                && result.HavingFilterText.indexOf("<") < 0) {

                                result.HavingFilterText = "= " + result.HavingFilterText;
                            } else if (result.HavingFilterText.indexOf("～") > 0 && result.HavingFilterText.indexOf("between") < 0) {

                                result.HavingFilterText = "between " + result.HavingFilterText.replace(" ～ ", " and ").replace("～", " and ");
                            } else if (result.HavingFilterText.indexOf(",") > 0 && result.HavingFilterText.indexOf("in") < 0) {

                                result.HavingFilterText = "in(" + result.HavingFilterText + ")";
                            }


                            if (fieldItem) {
                                if (viewItem.Group == "Count") {
                                    havingFilterItem.FilterSql = viewItem.Group + "(DISTINCT " + fieldItem.EntityName + "." + fieldItem.FieldName + ")" + " " + result.HavingFilterText;
                                } else {
                                    havingFilterItem.FilterSql = viewItem.Group + "(" + fieldItem.EntityName + "." + fieldItem.FieldName + ")" + " " + result.HavingFilterText;
                                }
                            } else {
                                havingFilterItem.FilterSql = " COUNT(*) " + result.HavingFilterText;
                            }

                            havingFilterItem.FilterDesc = "ViewItemFilter:" + viewItem.ItemName;
                        } else {
                            if (havingFilterItem) {
                                smat.service.delItemByKey(self.config.page.entity.FilterList, "FilterName", havingFilterItem.FilterName);
                                smat.service.delItemByKey(self.config.page.entity.FilterControlList, "FilterControlName", havingFilterItem.FilterName);
                                smat.service.delItemByKey(self.view.ViewFilterList, "FilterControlName", havingFilterItem.FilterName);
                            }
                        }

                        self.setData();

                    }
                },
                checkResult: function (result,uid) {
                    if (result.FilterText) {
                        var params = {};
                        params.request = {};
                        params.request.ProjID = self.view.ProjID;

                        params.request.DsRequests = new Array();

                        var fs = result.FilterText;
                        if (result.FilterText.indexOf("=") < 0
                                && result.FilterText.indexOf(">") < 0
                                && result.FilterText.indexOf("～") < 0
                                && result.FilterText.indexOf(",") < 0
                                && result.FilterText.indexOf("between") < 0
                                && result.FilterText.indexOf("is") < 0
                                && result.FilterText.indexOf("<") < 0) {

                            fs = "= " + result.FilterText;
                        } else if (result.FilterText.indexOf("～") > 0 && result.FilterText.indexOf("between") < 0) {

                            fs = " between " + result.FilterText.replace("～", " and ");
                        } else if (result.FilterText.indexOf(",") > 0 && result.FilterText.indexOf("in") < 0) {

                            fs = "in(" + result.FilterText + ")";
                        }

                        fs =  fieldItem.FieldName + " " + fs;
                        debugger
                        params.request.DsRequests.push(
                           {
                               TableName: fieldItem.EntityName,
                               Filter: fs + " and 1=0 "
                           }
                        );
                        
                        var ok = true;
                        smat.service.loadJosnData({
                            url: smat.dynamics.commonURL.getDyDs,
                            params: params,
                            noHandleRequestError:true,
                            async: false,
                            error: function (result) {
                                ok = false;
                            }

                        });

                        if (!ok) {
                            smat.service.notice({ msg: "抽出条件「" + result.FilterText + "」は正しくありません。", type: "error" });
                            return false;
                        }
                    } else if (result.HavingFilterText) {

                        var params = {};
                        params.request = {};
                        params.request.ProjID = self.view.ProjID;

                        params.request.DsRequests = new Array();

                        var fs = result.HavingFilterText;

                        if (result.HavingFilterText.indexOf("=") < 0
                               && result.HavingFilterText.indexOf(">") < 0
                               && result.HavingFilterText.indexOf("～") < 0
                               && result.HavingFilterText.indexOf(",") < 0
                               && result.HavingFilterText.indexOf("between") < 0
                               && result.HavingFilterText.indexOf("is") < 0
                               && result.HavingFilterText.indexOf("<") < 0) {

                            fs = "= " + result.HavingFilterText;
                        } else if (result.HavingFilterText.indexOf("～") > 0 && result.HavingFilterText.indexOf("between") < 0) {

                            fs = " between " + result.HavingFilterText.replace("～", " and ");
                        } else if (result.HavingFilterText.indexOf(",") > 0 && result.HavingFilterText.indexOf("in") < 0) {

                            fs = "in(" + result.HavingFilterText + ")";
                        }

                        var en = "";
                        if (fieldItem) {
                            fs = fieldItem.FieldName + " " + fs;
                            en = fieldItem.EntityName;
                        } else {
                            fs = " 0 " + fs;
                            en = viewItem.ItemEntityName
                        }

                        params.request.DsRequests.push(
                           {
                               TableName: en,
                               Filter: fs + " and 1=0 "
                           }
                        );
                        var ok = true;
                        smat.service.loadJosnData({
                            url: smat.dynamics.commonURL.getDyDs,
                            params: params,
                            noHandleRequestError: true,
                            async: false,
                            error: function (result) {
                                debugger
                                ok = false;

                            }

                        });

                        if (!ok) {
                            smat.service.notice({ msg: "集計後条件「" + result.HavingFilterText + "」は正しくありません。", type: "error" });
                            return false;
                        }
                    }
                }
            })
        }, _setResultUiConfig: function () {
            if (!this.resultUi) {
                return;
            }

            if (this.resultUi.config.type == "Grid") {

                var columns = [];

                var rowSpanFields = "";
                for (var i = 0; i < this.view.ItemList.length; i++) {
                    var viewItem = this.view.ItemList[i];

                    if (viewItem.IsHideInView == true) {
                        continue;
                    }

                    if (viewItem.Group == "GroupBy") {
                        rowSpanFields = rowSpanFields + "," + viewItem.ItemName;
                    }

                    var colConfig = this._getNewColConfig(viewItem, rowSpanFields);
                    columns.push(colConfig);

                }

                if (columns.length == 0) {
                    columns.push({
                        title: "　　",
                        field: "colTemp",
                    })
                }

                this.resultUi.config.columns = columns;

                if (this.resultUi.config.resultType == "SetSummaryCross") {

                    //x
                    var xViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '4' && $.error == null").ToArray();
                    if (xViewItem.length > 0) {
                        this.resultUi.config.category = xViewItem[0].ItemName;
                        this.resultUi.config.category_format = xViewItem[0].Format;
                        this.resultUi.config.category_width = xViewItem[0].Width;
                        this.resultUi.config.category_title = xViewItem[0].ItemDesc;


                        if (xViewItem[0].ItemName.indexOf("Date") > 0 || xViewItem[0].ItemName.indexOf("date") > 0) {
                            this.resultUi.config.category_format = "YMD";
                            this.resultUi.config.categoryDataType = "Date";
                        } else if (xViewItem[0].ItemFieldName == "PeriodMonth") {
                            this.resultUi.config.category_format = "YM";
                            this.resultUi.config.categoryDataType = "Date";
                        }
                    }

                    //y
                    var yViewItems = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '3' && $.error == null").ToArray();
                    if (yViewItems.length > 0) {
                        var series = [];

                        for (var i in yViewItems) {
                            var yViewItem = yViewItems[i];

                            serie = {
                                seriesField: yViewItem.ItemName,
                                seriesTitle: yViewItem.ItemDesc,
                                seriesFormat: yViewItem.Format,
                                width: yViewItem.Width,
                            }

                            if (yViewItem.ItemName.indexOf("Date") > 0 || yViewItem.ItemName.indexOf("date") > 0) {
                                serie.seriesFormat = "YMD";
                                serie.seriesDataType = "Date";
                            } else if (yViewItem.ItemFieldName == "PeriodMonth") {
                                serie.seriesFormat = "YM";
                                serie.seriesDataType = "Date";
                            }

                            series.push(serie);
                        }

                        
                        this.resultUi.config.series = series;

                    }


                    //s
                    var sViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '2' && $.error == null").ToArray();
                    if (sViewItem.length > 0) {

                        this.resultUi.config.dataField = sViewItem[0].ItemName;
                        this.resultUi.config.dataField_format = sViewItem[0].Format;
                        this.resultUi.config.dataField_title = sViewItem[0].ItemDesc;
                    }
                } else {
                    this.resultUi.config.category = undefined;
                    this.resultUi.config.category_format = undefined;
                    this.resultUi.config.category_width = undefined;
                    this.resultUi.config.category_title = undefined;
                    this.resultUi.config.series = undefined;
                    this.resultUi.config.dataField = undefined;
                    this.resultUi.config.dataField_format = undefined;
                    this.resultUi.config.dataField_title = undefined;
                }
            } else if (this.resultUi.config.type == "Chart") {
                this.resultUi.config.XFormat = undefined;
                this.resultUi.config.XTitle = undefined;
                //this.resultUi.config.XDataType = undefined;
                this.resultUi.config.XField = undefined;

                this.resultUi.config.YFormat = undefined;
                this.resultUi.config.YTitle = undefined;
                this.resultUi.config.YField = undefined;

                this.resultUi.config.seriesTitle = undefined;
                this.resultUi.config.seriesField = undefined;

                if (this.resultUi.config.resultType == "SetPieChart") {
                    ////x
                    //var xViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '4' && $.error == null").ToArray();
                    //if (xViewItem.length > 0) {
                    //    this.resultUi.config.XFormat = xViewItem[0].Format;
                    //    this.resultUi.config.XTitle = xViewItem[0].ItemDesc;
                    //    //this.resultUi.config.XDataType = result.dataType;
                    //    this.resultUi.config.XField = xViewItem[0].ItemName;

                    //    //=====todo:=====
                    //    if (xViewItem[0].Format == "=Date(YMD)") this.resultUi.config.XFormat = "YMD";
                    //    if (xViewItem[0].Format == "=Date(YM)") this.resultUi.config.XFormat = "YM";
                    //    if (xViewItem[0].Format == "=Date(Year)") this.resultUi.config.XFormat = "Y";
                    //    if (xViewItem[0].Format && xViewItem[0].Format.indexOf("=Date(") == 0) {
                    //        this.resultUi.config.XDataType = "Date";
                    //    }
                    //    //=====todo:=====
                    //}

                    //y
                    var yViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '2' && $.error == null").ToArray();
                    if (yViewItem.length > 0) {
                        this.resultUi.config.YFormat = yViewItem[0].Format;
                        this.resultUi.config.YTitle = yViewItem[0].ItemDesc;
                        this.resultUi.config.YField = yViewItem[0].ItemName;
                    }


                    //s
                    var sViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '4' && $.error == null").ToArray();
                    if (sViewItem.length > 0) {

                        this.resultUi.config.seriesTitle = sViewItem[0].ItemDesc;
                        this.resultUi.config.seriesField = sViewItem[0].ItemName;

                        if (sViewItem[0].Format == "=Date(YMD)") this.resultUi.config.XFormat = "YMD";
                        if (sViewItem[0].Format == "=Date(YM)") this.resultUi.config.XFormat = "YM";
                        if (sViewItem[0].Format == "=Date(Year)") this.resultUi.config.XFormat = "Y";
                        if (sViewItem[0].Format && sViewItem[0].Format.indexOf("=Date(") == 0) {
                            this.resultUi.config.XDataType = "Date";
                        }

                        if (sViewItem[0].ItemName.indexOf("Date") > 0 || sViewItem[0].ItemName.indexOf("date") > 0) {
                            this.resultUi.config.XFormat = "YMD";
                            this.resultUi.config.XDataType = "Date";
                        } else if (sViewItem[0].ItemFieldName == "PeriodMonth") {
                            this.resultUi.config.XFormat = "YM";
                            this.resultUi.config.XDataType = "Date";
                        }

                    }
                } else {
                    //x
                    var xViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '4' && $.error == null").ToArray();
                    if (xViewItem.length > 0) {
                        this.resultUi.config.XFormat = xViewItem[0].Format;
                        this.resultUi.config.XTitle = xViewItem[0].ItemDesc;
                        //this.resultUi.config.XDataType = result.dataType;
                        this.resultUi.config.XField = xViewItem[0].ItemName;

                        //=====todo:=====
                        if (xViewItem[0].Format == "=Date(YMD)") this.resultUi.config.XFormat = "YMD";
                        if (xViewItem[0].Format == "=Date(YM)") this.resultUi.config.XFormat = "YM";
                        if (xViewItem[0].Format == "=Date(Year)") this.resultUi.config.XFormat = "Y";
                        if (xViewItem[0].Format && xViewItem[0].Format.indexOf("=Date(") == 0) {
                            this.resultUi.config.XDataType = "Date";
                        }

                        if (xViewItem[0].ItemName.indexOf("Date") > 0 || xViewItem[0].ItemName.indexOf("date") > 0) {
                            this.resultUi.config.XFormat = "YMD";
                            this.resultUi.config.XDataType = "Date";
                        } else if (xViewItem[0].ItemFieldName == "PeriodMonth") {
                            this.resultUi.config.XFormat = "YM";
                            this.resultUi.config.XDataType = "Date";
                        } 

                        //=====todo:=====
                    }

                    //y
                    var yViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '2' && $.error == null").ToArray();
                    if (yViewItem.length > 0) {
                        this.resultUi.config.YFormat = yViewItem[0].Format;
                        this.resultUi.config.YTitle = yViewItem[0].ItemDesc;
                        this.resultUi.config.YField = yViewItem[0].ItemName;

                    }


                    //s
                    var sViewItem = $.Enumerable.From(this.view.ItemList).Where("$.IsHideInView != true && $.ItemCategory == '3' && $.error == null").ToArray();
                    if (sViewItem.length > 0) {

                        this.resultUi.config.seriesTitle = sViewItem[0].ItemDesc;
                        this.resultUi.config.seriesField = sViewItem[0].ItemName;
                    }
                }

                
            }



        }, _getNewColConfig: function (viewItem, rowSpanFields) {
            var c = {
                title: viewItem.ItemDesc,
                field: viewItem.ItemName,
            }

            if (viewItem.Format == "=Date(YM)") {
                c.dataType ="DateYM";
            } else if (viewItem.Format == "=Date(YMD)") {
                c.dataType = "Date";
            } else if (viewItem.ItemName.indexOf("Date") > 0 || viewItem.ItemName.indexOf("date") > 0) {
                c.dataType = "Date";
            } else if (viewItem.ItemFieldName == "PeriodMonth") {
                c.dataType = "DateYM";
            } else if (viewItem.ItemFieldName == "PeriodDate") {
                c.dataType = "Date";
            } else if (viewItem.Format == "Money") {
                c.dataType = "Money";
                c.attributes = {
                    "class": "text-right"
                }
            } else if (viewItem.Format == "{0:N0}") {
                c.dataType = "Money";
                c.attributes = {
                    "class": "text-right"
                }
            }

            if (viewItem.Group == "Sum" || viewItem.Group == "Count" || viewItem.ItemSql == "*") {
                c.attributes = {
                    "class": "text-right"
                }
            }

            if (viewItem.Group == "GroupBy" && this.resultUi.config.resultType != "SetSummaryCross") {
                if (rowSpanFields != "") rowSpanFields = rowSpanFields.substring(1, rowSpanFields.length);
                c.rowSpanFields = rowSpanFields;
            }

            

            

            return c;
        }, moveViewItemIndex: function (seq, step) {
            var self = this;
            seq = Number(seq);
            var newIndex = Number(seq);
            if (step > 0) {
                if (seq >= this.view.ItemList.length) {
                    return;
                }
                var curItem = this.view.ItemList[seq - 1];
                var nextItem = this.view.ItemList[seq];

                curItem.Seq = (seq + 1);
                nextItem.Seq = seq;
            } else if (step < 0) {
                if (seq == 1) {
                    return;
                }
                var curItem = this.view.ItemList[seq - 1];
                var preItem = this.view.ItemList[seq - 2];

                curItem.Seq = (seq - 1);
                preItem.Seq = seq;
                newIndex = Number(seq-2);
            }

            this.setData();
            if (this.editModeInput.ui().value() == "1") {
                setTimeout(function () {
                    //self.editGrid.select(newIndex);
                    //self.editGridBox.addClass('animated bounce');
                }, 1000);
                
                

                setTimeout(function () {
                    var tr = self.editGridBox.find('tbody').children("tr:eq(" + newIndex + ")");

                    //tr.find("td").css("background-color", "#fff");
                    if (step > 0) {
                        tr.find("td").not("[rowspan]").addClass('animated slideInDown');
                        var trFrom = self.editGridBox.find('tbody').children("tr:eq(" + (newIndex - 1) + ")");
                        //trFrom.find("td").css("background-color", "#fff");
                        trFrom.find("td").not("[rowspan]").addClass('animated slideInUp');

                    } else {
                        tr.find("td").not("[rowspan]").addClass('animated slideInUp');
                        var trFrom = self.editGridBox.find('tbody').children("tr:eq(" + (newIndex + 1) + ")");
                        //trFrom.find("td").css("background-color", "#fff");
                        trFrom.find("td").not("[rowspan]").addClass('animated slideInDown');
                    }
                }, 1);

                
            }
            
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetBusinessResult, smat.dynamics.property.set.SetBase);

})();