
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetListSearch
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetListSearch = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            resultType: "SetListSearch",
            uiType: "Grid"
        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetListSearch.prototype = {
        getPropertiesConfig: function () {
            var self = this;
            var form = this.config.page.getControlByName("search_form");

            var belongStr = "";
            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.belong) {
                belongStr = smat.dynamics.analysisConfig.belong(this);
            }
            var belongEnable = true;
            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.belongEnable) {
                belongEnable = smat.dynamics.analysisConfig.belongEnable(this, belongStr);
            }

            var entityNameEnable = false;
            if (this.config.page.config.designer.mode == "new") {
                entityNameEnable = true;
            }

            var createdByEnable = false;
            var createdByDataSource = [];
            var createdByDefaultValue = "1";

            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.createdByEnable) {
                createdByEnable = smat.dynamics.analysisConfig.createdByEnable(this, createdByDefaultValue);
            }
            

            createdByDataSource.push({
                text: "店舗共通",
                value: "0"
            });

            createdByDataSource.push({
                text: "本部共通",
                value: "2"
            });

            createdByDataSource.push({
                text: "個別",
                value: "1"
            });

            //createdByDataSource.push({
            //    text: "テンプレート",
            //    value: "9"
            //});

            var belongVisible = false;
            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.belongVisible) {
                belongVisible = smat.dynamics.analysisConfig.belongVisible(this);
            }

            var belongDataSource = [];
            belongDataSource.push({
                text: "部門",
                value: belongStr
            });
            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.belongDataSource) {
                belongDataSource = smat.dynamics.analysisConfig.belongDataSource(this,belongStr);
            }

            if (!this.config.page.entityNames) {
                this.config.page.entityNames = [];

                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.getEntityList,
                    async: false,
                    params: {
                        ProjID: self.config.page.config.projID
                    },
                    success: function (result) {
                        var d = new Array();
                        for (var k in result) {
                            if (result[k].EntityName != "M_Code") {
                                d.push(result[k]);
                            }
                        }
                        var datas = null;
                        if (smat.dynamics.isUser()) {
                            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.getUserEntityList) {
                                datas = smat.dynamics.analysisConfig.getUserEntityList(result);
                            } else {
                                datas = $.Enumerable.From(result).Where("$.EntityType == '1' ").OrderBy("Number($.EntityState)").ToArray();
                            }
                        } else {
                            datas = d;
                        }

                        self.config.page.entityNames = datas;
                    }
                });

            }

            //
            if (this.config.page.config.title) {
                self.config.page.config.designer.titlePane.find(".nav-title").text(this.config.page.config.title);
            }


            var configGroups = [];

            var baseGroup = {
                group: "基本設定",
                legend: "基本設定",
                items: [
                            //{
                            //    key: "No",
                            //    title: "管理番号",
                            //    type: "TextBox",
                            //    control: this.config.page,
                            //    valueConfig: this.config.page.config

                            //},
                            {
                                key: "title",
                                title: "リクエスト名",
                                style: "width:460px;",
                                onChange: function (ev) {
                                    self.config.page.config.designer.titlePane.find(".nav-title").text(self.config.page.config.title);
                                },
                                control: this.config.page,
                                valueConfig: this.config.page.config,
                                type: "TextBox"

                            }
                            ,
                            {
                                key: "entityName",
                                title: "リクエスト種別",
                                type: "DropDownList",
                                control: this.config.page,
                                valueConfig: this.config.page.config,
                                dataValueField: "EntityName",
                                enable: entityNameEnable,
                                onChange: function (ev) {
                                    self.config.page.config.designer.initNewPage(ev.ui.value(), self.config.page.config.title);
                                },
                                dataTextField: "EntityDesc",
                                dataSource: self.config.page.entityNames
                            }
                            ,
                            {
                                key: "createdBy",
                                title: "分類",
                                type: "DropDownList",
                                control: this.config.page,
                                visible: belongVisible,
                                valueConfig: this.config.page.config,
                                dataTextField: "text",
                                enable: createdByEnable,
                                dataValueField: "value",
                                defaultValue: createdByDefaultValue,
                                onChange: function (ev) {
                                    self.config.page.groupNames = undefined;
                                    self.config.page.config.designer.template.onPageLoad();
                                },
                                dataSource: createdByDataSource
                            }
                            //, {
                            //    key: "belong",
                            //    title: "範囲",
                            //    type: "DropDownList",
                            //    control: this.config.page,
                            //    enable: belongEnable,
                            //    visible: belongVisible,
                            //    valueConfig: this.config.page.config,
                            //    defaultValue: belongStr,
                            //    dataTextField: "text",
                            //    dataValueField: "value",
                            //    dataSource: belongDataSource
                            //}
                ]
            }

            var pageSizeDefaultValue = undefined;
            var pageSizeMemo = "件";
            if (this.config.resultType == "SetPieChart"
                || this.config.resultType == "SetLineChart"
                || this.config.resultType == "SetColChart") {
                pageSizeDefaultValue = "10";
                pageSizeMemo = "件";

                baseGroup.items.push({
                    key: "graphSize",
                    title: "件数指定",
                    type: "TextBox",
                    style: "width:60px;",
                    attr: "maxlength=2",
                    cssClass: "onlyNum",
                    defaultValue: pageSizeDefaultValue,
                    memo: pageSizeMemo,
                    control: form,
                    valueConfig: form.config
                });

                baseGroup.items.push({
                    key: "showOther",
                    title: " ",
                    type: "CheckBox",
                    style: "width:360px;margin-left: 0;",
                    attr: "maxlength=2",
                    cssClass: "onlyNum",
                    defaultValue: true,
                    text: "残りのデータは「その他」にまとめる。",
                    control: form,
                    valueConfig: form.config
                });
            } else {

                baseGroup.items.push({
                    key: "pageSize",
                    title: "件数指定",
                    type: "TextBox",
                    style: "width:60px;",
                    attr: "maxlength=2",
                    cssClass: "onlyNum",
                    defaultValue: pageSizeDefaultValue,
                    memo: pageSizeMemo,
                    control: form,
                    valueConfig: form.config
                });
            }

           

            configGroups.push(baseGroup);

            if (!this.config.page.config.belong && smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.belongIfEmpty) {
                this.config.page.config.belong = smat.dynamics.analysisConfig.belongIfEmpty(this)
            }

            var linkGroup = {
                group: "関連リクエスト",
                legend: "関連リクエスト",
                items: []
            }
            
            if (!this.config.page.groupNames) {
                var createdByValue = createdByDefaultValue;
                if (this.config.page.config.createdBy) createdByValue = this.config.page.config.createdBy
                this.config.page.groupNames = [];

                var params = {};
                params.request = {};
                params.request.ProjID = this.config.page.config.projID;

                params.request.DsRequests = new Array();
                
                var fs = "ProjID = '" + this.config.page.config.projID + "' and (GroupName <> '' or GroupName is not null) and Belong = '" + this.config.page.config.belong + "' and EntityName = '" + this.config.page.config.entityName + "' and CreatedBy = '" + createdByValue + "'";
                if (createdByValue != "1") {
                    fs = "ProjID = '" + this.config.page.config.projID + "' and (GroupName <> '' or GroupName is not null) and EntityName = '" + this.config.page.config.entityName + "' and CreatedBy = '" + createdByValue + "'";
                }
                params.request.DsRequests.push(
                   {
                       TableName: "Y_EntityForm",
                       Filter: fs
                   }
                );

                smat.service.loadJosnData({
                    url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                    params: params,
                    async: false,
                    success: function (result) {
                        var gs = $.Enumerable.From(result.ds["Y_EntityForm"]).GroupBy("$.GroupName").OrderBy("$.GroupName").ToArray();

                        for (var i in gs) {
                            self.config.page.groupNames.push({
                                text: gs[i].Key(),
                                value: gs[i].Key()
                            });
                        }
                    }

                });
            }

            linkGroup.items.push({
                key: "groupName",
                title: "グループ名",
                type: "ComboBox",
                control: this.config.page,
                style: "width:360px;",
                valueConfig: this.config.page.config,
                dataTextField: "text",
                dataValueField: "value",
                dataSource: self.config.page.groupNames
            });

            linkGroup.items.push({
                key: "groupSeq",
                title: "順番",
                type: "TextBox",
                cssClass: "onlyNum",
                style: "width:60px;",
                control: this.config.page,
                valueConfig: this.config.page.config
            });

            linkGroup.items.push({
                key: "groupLinkDesc",
                title: "名称",
                type: "TextBox",
                control: this.config.page,
                valueConfig: this.config.page.config
            });


            configGroups.push(linkGroup);



            var detailGroup = {
                group: "詳細設定",
                legend: "詳細設定",
                items: []
            }

            if ((this.config.resultType == "SetListSearch" || this.config.resultType == "SetSummarySearch") == false) {
                //detailGroup.items.push({
                //    key: "showSum",
                //    title: "合計表示",
                //    text: "合計表示",
                //    control: this.config.page,
                //    valueConfig: this.config.page.config,
                //    type: "CheckBox"
                //});

                //detailGroup.items.push({
                //    key: "showEmptyData",
                //    title: "データ表示",
                //    text: "空白データ表示",
                //    control: this.config.page,
                //    valueConfig: this.config.page.config,
                //    type: "CheckBox"
                //});
            }
            

            //detailGroup.items.push({
            //    key: "pageSize",
            //    title: "件数指定",
            //    type: "TextBox",
            //    style: "width:60px;",
            //    attr: "maxlength=2",
            //    cssClass: "onlyNum",
            //    control: form,
            //    valueConfig: form.config
            //});


            //configGroups.push(detailGroup);

            var propertiesConfig = {
                title: smat.service.optionSet("SysText.Confirm"),
                groups: configGroups,
                callback: function (result) {
                   
                },
                check: function (items) {

                }
            }

            return propertiesConfig;
        },noGroupTypes: {
            "SetListSearch":1
        },
        groupXTypes: {
            "SetSummarySearch": 1
        },
        groupXYTypes: {
            "SetColChart": 1,
            "SetLineChart": 1,
            "SetSummaryCross": 1
        },
        groupPieTypes: {
            "SetPieChart": 1
        }, _setAbjustUi: function () {
            var div = this.config.page.getControlByName("result_div");
            var r = this.config.page.getControlByName("result");

            if (r) {

                //abjust group
                if ((this.noGroupTypes[r.config.resultType] == 1 && this.noGroupTypes[this.config.resultType] == 1) == false
                    && (this.groupXTypes[r.config.resultType] == 1 && this.groupXTypes[this.config.resultType] == 1) == false
                    && (this.groupPieTypes[r.config.resultType] == 1 && this.groupPieTypes[this.config.resultType] == 1) == false
                    && (this.groupXYTypes[r.config.resultType] == 1 && this.groupXYTypes[this.config.resultType] == 1) == false) {
                    //alert(this.config.resultType);
                    
                    var form = this.config.page.getControlByName("search_form");
                    this.view = this.config.page.getEditView(form.config.view);

                    for (var i = 0; i < this.view.ItemList.length; i++) {
                        var viewItem = this.view.ItemList[i];

                        if (!viewItem.oldItemCategory) viewItem.oldItemCategory = viewItem.ItemCategory;

                        if (this.config.resultType == "SetListSearch") {
                            viewItem.Group = null;
                            if (viewItem.ItemFieldName == "count") {
                                viewItem.ItemSql = "' '";
                            } else if (viewItem.ItemCategory == "2") {
                                viewItem.ItemSql = viewItem.ItemSql.replace("DISTINCT","");
                            }


                            viewItem.ItemCategory = "1";
                            var tempItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "1";
                            if ($.Enumerable.From(this.view.ItemList).Where("$.ItemName == '" + tempItemName + "'").ToArray() == 0) {
                                viewItem.ItemName = tempItemName;
                                viewItem.error = "empty";
                            } else {
                                //viewItem.ItemName = "empty";
                                viewItem.error = undefined;
                            }

                        }
                        //else if (this.config.resultType == "SetPieChart") {
                        //    viewItem.Group = null;
                        //    if (viewItem.ItemFieldName == "count") {
                        //        viewItem.ItemSql = "' '";
                        //    } else if (viewItem.ItemCategory == "2") {
                        //        viewItem.ItemSql = viewItem.ItemSql.replace("DISTINCT", "");
                        //    }


                        //    viewItem.ItemCategory = "1";
                        //    var tempItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "1";
                        //    if ($.Enumerable.From(this.view.ItemList).Where("$.ItemName == '" + tempItemName + "'").ToArray() == 0) {
                        //        viewItem.ItemName = tempItemName;
                        //        viewItem.error = "empty";
                        //    } else {
                        //        //viewItem.ItemName = "empty";
                        //        viewItem.error = undefined;
                        //    }

                        //}
                        else {
                            viewItem.ItemCategory = viewItem.oldItemCategory;
                            viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + viewItem.oldItemCategory;

                            if (!viewItem.Group) {
                                var fieldItem;
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

                                

                                if (this.config.resultType == "SetSummarySearch" || this.config.resultType == "SetPieChart") {
                                    if (viewItem.oldItemCategory == "3") {
                                        viewItem.ItemCategory = "4";
                                        viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "4";
                                    }
                                } 

                                if (fieldItem && fieldItem.IsSum) {
                                    viewItem.Group = "Sum";
                                } else if (fieldItem) {
                                    viewItem.Group = "GroupBy";
                                }

                                if (viewItem.ItemFieldName == "count") {
                                    viewItem.Group = "Count";
                                    viewItem.ItemSql = "*";
                                } else if (viewItem.ItemCategory == "2") {
                                    if (fieldItem && fieldItem.IsSum) {
                                       
                                    } else if (fieldItem) {
                                        viewItem.Group = "Count";
                                        viewItem.ItemSql = " DISTINCT " + viewItem.ItemSql;
                                    }
                                }

                                //list => group
                                if (viewItem.oldItemCategory == "1") {
                                    if (fieldItem && fieldItem.IsSum) {
                                        viewItem.ItemCategory = "2";
                                        viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "2";
                                    } else if (viewItem.ItemFieldName == "count") {
                                        viewItem.ItemCategory = "2";
                                        viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "2";
                                    } else {

                                        if (this.config.resultType == "SetSummarySearch" || this.config.resultType == "SetPieChart") {
                                            viewItem.ItemCategory = "4";
                                            viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "4";
                                        } else {
                                            var gitems = $.Enumerable.From(this.view.ItemList).Where("$.ItemFieldName != 'count'").ToArray();

                                            if (i == gitems.length - 1) {
                                                viewItem.ItemCategory = "3";
                                                viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "3";
                                            } else {
                                                viewItem.ItemCategory = "4";
                                                viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "4";
                                            }
                                        }
                                    }
                                }

                            } else if (viewItem.oldItemCategory == "1") {

                                var fieldItem;
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

                                if (fieldItem && fieldItem.IsSum) {
                                    viewItem.ItemCategory = "2";
                                    viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "2";
                                } else if (viewItem.ItemFieldName == "count") {
                                    viewItem.ItemCategory = "2";
                                    viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "2";
                                } else {

                                    if (this.config.resultType == "SetSummarySearch" || this.config.resultType == "SetPieChart") {
                                        viewItem.ItemCategory = "4";
                                        viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "4";
                                    } else {
                                        var gitems = $.Enumerable.From(this.view.ItemList).Where("$.ItemFieldName != 'count'").ToArray();

                                        if (i == gitems.length - 1) {
                                            viewItem.ItemCategory = "3";
                                            viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "3";
                                        } else {
                                            viewItem.ItemCategory = "4";
                                            viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "4";
                                        }
                                    }
                                }
                            } else if (this.config.resultType == "SetSummarySearch" || this.config.resultType == "SetPieChart") {
                                if (viewItem.oldItemCategory == "3") {
                                    viewItem.ItemCategory = "4";
                                    viewItem.ItemName = viewItem.ItemName.substring(0, viewItem.ItemName.length - 1) + "4";
                                } 
                            }
                        }

                    }

                    if (this.config.page.filedCheckBoxs) {
                        for (var i in this.config.page.filedCheckBoxs) {
                            this.config.page.filedCheckBoxs[i].onPageLoad();
                        }
                    }
                    
                }
                

                if (r.config.type != this.config.uiType) {
                    r.del();
                    div.addChild(this._getNewUiConfig());

                } else {
                    r.config.resultType = this.config.resultType;
                    r.config.chartType = this.config.chartType;
                }
            } else {
                div.addChild(this._getNewUiConfig());
            }
        }, _getNewUiConfig: function () {


            var form = this.config.page.getControlByName("search_form");


            if (this.config.resultType == "SetListSearch"
                || this.config.resultType == "SetSummarySearch") {
                
            } else {

            }

            var c = {
                type: this.config.uiType,
                rowIndex: 0,
                name: "result",
                resultType: this.config.resultType,
                view: form.config.view
            };


    //        if (this.config.uiType == "Grid") {
    //            c.excelExport = {
    //                eventKey: "grid_excelExport",
    //                jsCode: "function (e) {"
    //+ "\n\     e.workbook.fileName = e.page.getPage().config.title + '.xlsx';"
    //+ "\n\ }",
    //                type: "js"
    //            }
    //        }

            return c;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetListSearch, smat.dynamics.property.set.SetBase);

})();