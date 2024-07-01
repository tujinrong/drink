
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.BusinessSearch = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "BusinessSearch"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.BusinessSearch.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {


            this.designerTab = new Array();

            var tab1 = {
                id:"t1",
                text: "基本情報",
                hboxWidth:"120px",
                notPanelBar:true
            }

            tab1.tools = new Array();

            tab1.tools.push(new smat.dynamics.tool.BusinessSearchType({
                page: this.page,
                name: "BusinessSearchType"
            }));

            this.designerTab.push(tab1);


            this.tab2 = {
                id: "t2",
                text: "表示選択",
                notPanelBar: true
            }

            this.tab2.tools = new Array();


            var reType = "";
            if (smat.dynamics.isUser()) {
                //reType = "NOTN1";
            }

            this.tab2.tools.push(new smat.dynamics.tool.FiledCheckBox({
                page: this.page,
                dragType: "GroupItem",
                name: "集計キー",
                itemDiv: "4"
            }));

            this.tab2.tools.push(new smat.dynamics.tool.FiledCheckBox({
                page: this.page,
                dragType: "GroupItem",
                itemDiv: "3",
                name: "縦集計キー"
            }));

            this.tab2.tools.push(new smat.dynamics.tool.FiledCheckBox({
                page: this.page,
                dragType: "LogicItem",
                itemDiv: "1",
                propertySet: true,
                reType: reType,
                name: "抽出項目"
            }));

            this.tab2.tools.push(new smat.dynamics.tool.FiledCheckBox({
                page: this.page,
                dragType: "SumItem",
                itemDiv: "2",
                name: "集計項目"
            }));

            

            this.designerTab.push(this.tab2);

            var tab3 = {
                id: "t3",
                text: "条件",
                designMain: "page",
                refreshUiName:"result"
            }

            tab3.tools = new Array();

            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.initSearchConditionTool) {
                smat.dynamics.analysisConfig.initSearchConditionTool(tab3, this.page);
            } else {
                tab3.tools.push(new smat.dynamics.tool.UserControl({
                    page: this.page,
                    name: smat.service.optionSet("DyOptionText.SearchCondition"),
                    conditionOnly: true
                }));
            }


            this.designerTab.push(tab3);


            var tab4 = {
                id: "t4",
                text: "検索",
                notPanelBar: true,
                notLeftSplitter: true
            }

            tab4.tools = new Array();

            tab4.tools.push(new smat.dynamics.tool.BusinessSearchPreview({
                page: this.page,
                name: "BusinessSearchPreview",
                template:this
            }));

            this.designerTab.push(tab4);



            //=============================================================
            //this.tools = new Array();

            ////this.tools.push(new smat.dynamics.tool.Filter({
            ////    page: this.page,
            ////    name: smat.service.optionSet("DyOptionText.Filter")
            ////}));

            //this.tools.push(new smat.dynamics.tool.UserControl({
            //    page: this.page,
            //    name: smat.service.optionSet("DyOptionText.SearchCondition")
            //}));

            //this.tools.push(new smat.dynamics.tool.Filed({
            //    page: this.page,
            //    dragType: "LogicItem",
            //    name: smat.service.optionSet("DyOptionText.ListItem")
            //}));

            //this.tools.push(new smat.dynamics.tool.Control({
            //    page: this.page,
            //    name: smat.service.optionSet("DyOptionText.Control")
            //}));

            //this.tools.push(new smat.dynamics.tool.Filed({
            //    page: this.page,
            //    name: "Entity"
            //}));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));

           
        }, templateBuild: function () {

            if (!this.config.page.entity) return;

            this.page.createNewView({
                ViewName: this.page.config.designer.mainFormName ,
                ViewDesc: this.page.config.designer.mainFormName ,
            });

            this.mainSection = this.page.addChild({
                type: "Section",
                page: this.page,
                rowsCount: 3,
                noDel: true,
                name: "main_Section",
                designing: true
            });

            this.searchForm = this.mainSection.addChild({
                type: "Form",
                rowIndex: 0,
                name: "search_form",
                rowsCount: 2,
                noDel: true,
                //tooltip: smat.service.optionSet("DyOptionText.SearchCondition"),
                view: this.page.editViewList[0].ViewName
            });

            this.toolBar = this.mainSection.addChild({
                type: "ToolBar",
                rowIndex: 1,
                name: "toolBar"
            });

            this.resultDiv = this.mainSection.addChild({
                type: "Div",
                rowIndex: 2,
                name: "result_div",
                style:"width:100%;",
                rows:1
            });

            //this.grid = this.mainSection.addChild({
            //    type: "Grid",
            //    rowIndex: 2,
            //    name: "grid1",
            //    //tooltip: "項目設定",
            //    view: this.page.editViewList[0].ViewName
            //});

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 0,
                text: "codeKind:SysText.Search",
                name: "search_btn",
                cssClass: "btn-primary"
            });
            //this.toolBar.addChild({
            //    type: "Button",
            //    rowIndex: 0,
            //    colIndex: 1,
            //    text: "csv",
            //    name: "csv_btn",
            //    cssClass: "btn-primary"
            //});


            //this.toolBar.addChild({
            //    type: "Button",
            //    rowIndex: 0,
            //    text: "codeKind:SysText.Export",
            //    colIndex: 1,
            //    name: "btn_exp",
            //    cssClass: "btn-primary",
            //    click: {
            //        eventKey: "button_click",
            //        jsCode: "function (e) {"
            //    + "\n\  var r = e.page.ui('result');"
            //    + "\n\  if(r.config.resultType == 'SetSummaryCross'){"
            //    + "\n\      r.saveAsExcel();"
            //    + "\n\  }else if(r.config.type == 'Chart'){"
            //    + "\n\      r.saveAsImage(e.page.getPage().config.title);"
            //    + "\n\  } else{"
            //    + "\n\      var form = e.page.ui('search_form');"
            //    + "\n\      var actionInfo = form.config.actions[0];"
            //    + "\n\      var params = form.getParam(actionInfo);"
            //    + "\n\      var pageObj = e.page.getPage();"
            //    + "\n\      var title = pageObj.config.title;"
            //    + "\n\      smat.service.dyExport(params.request,{"
            //    + "\n\          noPickItems:true,"
            //    + "\n\          useDescName:true,"
            //    + "\n\          noHandleRequestError:true,"
            //    + "\n\          error:function(){"
            //    + "\n\              smat.service.notice({ msg: '現在はエクスポートできません。', type: 'error' });"
            //    + "\n\          },"
            //    + "\n\          title:title"
            //    + "\n\      });"
            //    + "\n\  }"
            //+ "\n\}",
            //        type: "js"
            //    }
            //});

            this.toolBar.addChild({
                type: "Pager",
                rowIndex: 1,
                colIndex: 0,
                name: "pager",
                dataHandler: "result"
            });

            //this.searchForm.addChild({
            //    type: "Field",
            //    dataType: "TextBox",
            //    defaultFieldName: "ShopCD",
            //    rowIndex: 0,
            //    colIndex: 1,
            //    name: "field2",
            //    label: "Label2",
            //    inputBoxClass: "col-fix-2"
            //});

            //this.searchForm.addChild({
            //    type: "Field",
            //    dataType: "DatePicker",
            //    rowIndex: 0,
            //    colIndex: 0,
            //    name: "field1",
            //    label: "Label1",
            //    inputBoxClass: "col-fix-1"
            //});

            this.searchForm.addAction({
                action: "search",
                actionBtn: "search_btn",
                resultHandler: "pager",
                view: this.page.editViewList[0].ViewName,
                getParam: {
                    eventKey: "form_getParam",
                    jsCode: "function (params,page) {"
    + "\n\     var resultType = page.ui('result').config.resultType;"
    + "\n\     params.request.GetPageCount = (resultType=='SetListSearch'||resultType=='SetSummarySearch');"
    + "\n\ }",
                    type: "js"
                }
            });


            //
            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.onTemplateBuild) {
                smat.dynamics.analysisConfig.onTemplateBuild(this);
            }


            this.searchForm.abjustColsPosition();

            this.onPageLoad();

        }, _resetPageObj: function () {
            
            if (this.designerTab) {
                var tabs = this.designerTab;
                for (var i in tabs) {

                    for (var key in tabs[i].tools) {
                        var tool = tabs[i].tools[key];
                        tool.config.page = this.page;
                    }
                }
            }

            this.page.filedCheckBoxs = this.tab2.tools

        }, onActivtTab: function (activateTab,tabNode) {

            if (activateTab.id == "t2") {
                if (this.config.page.config.category == "SetListSearch") {
                    activateTab.toolBarUi.expand($("#" + activateTab.id+"_"+"2"));
                } else {
                    activateTab.toolBarUi.expand($("#" + activateTab.id + "_" + "0"));
                }
            }

        }, beforeSave: function () {
            if (this.designerTab) {
                var tabs = this.designerTab;
                for (var i in tabs) {
                    for (var key in tabs[i].tools) {
                        var tool = tabs[i].tools[key];
                        if (tool.beforeSave) {
                            tool.beforeSave();
                        }
                    }
                }
            }
        }, afterPageSave: function () {
            //this.page.config.designer.cancelBtn.click();
        }, checkSave: function (type) {

            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.onCheckSave) {
                smat.dynamics.analysisConfig.onCheckSave(this, type);
            }

            var form = this.page.getControlByName("search_form");
            var view = this.page.getEditView(form.config.view);


            if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.error == null").ToArray().length == 0) {
                smat.service.notice({ msg: "表示項目は最低１件が必要。", type: "error" });
                this.tabBox.ui().uiControl.select(1);
                return false;
            }

            //
            if (this.config.page.config.category == "SetListSearch") {
                
            }

            if (this.config.page.config.category == "SetSummarySearch" || this.config.page.config.category == "SetPieChart") {
                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "集計キー項目は最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='2' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "集計項目は最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }
            }

            //if (this.config.page.config.category == "SetSummaryCross"
            //    || this.config.page.config.category == "SetLineChart"
            //    || this.config.page.config.category == "SetColChart") {
            //    if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length == 0) {
            //        smat.service.notice({ msg: "集計キーは最低１件が必要。", type: "error" });
            //        this.tabBox.ui().uiControl.select(1);
            //        return false;
            //    }

            //    if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3' && $.error == null").ToArray().length == 0) {
            //        smat.service.notice({ msg: "縦集計キーは最低１件が必要。", type: "error" });
            //        this.tabBox.ui().uiControl.select(1);
            //        return false;
            //    }

            //    if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='2' && $.error == null").ToArray().length == 0) {
            //        smat.service.notice({ msg: "集計項目は最低１件が必要。", type: "error" });
            //        this.tabBox.ui().uiControl.select(1);
            //        return false;
            //    }
            //}

            if (this.config.page.config.category == "SetSummaryCross") {

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "横集計キーは最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length > 1) {
                    smat.service.notice({ msg: "横集計キーは最高１件です、不要な横集計キーは非表示に設定まだは削除してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                //if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3' && $.error == null").ToArray().length == 0) {
                //    smat.service.notice({ msg: "縦集計キーは最低１件が必要。", type: "error" });
                //    this.tabBox.ui().uiControl.select(1);
                //    return false;
                //}

                //if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3' && $.error == null").ToArray().length > 1) {
                //    smat.service.notice({ msg: "縦集計キーは最高１件です、不要な縦集計キーは非表示に設定まだは削除してください。", type: "error" });
                //    this.tabBox.ui().uiControl.select(1);
                //    return false;
                //}

                if ($.Enumerable.From(view.ItemList).Where("$.ItemCategory =='2' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "集計項目は最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }
            }

            if (this.config.page.config.category == "SetLineChart") {

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "横集計キーは最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length > 1) {
                    smat.service.notice({ msg: "横集計キーは最高１件です、不要な横集計キーは非表示に設定まだは削除してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3' && $.error == null").ToArray().length > 1) {
                    smat.service.notice({ msg: "縦集計キーは最高１件です、不要な縦集計キーは非表示に設定まだは削除してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.ItemCategory =='2' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "集計項目は最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.ItemCategory =='2' && $.error == null").ToArray().length > 1 && $.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3'").ToArray().length > 0) {
                    smat.service.notice({ msg: "集計項目は復数件の場合、縦集計キーは指定できません。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }
            }

            if (this.config.page.config.category == "SetColChart") {
                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "横集計キーは最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length > 1) {
                    smat.service.notice({ msg: "横集計キーは最高１件です、不要な横集計キーは非表示に設定まだは削除してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3' && $.error == null").ToArray().length > 1) {
                    smat.service.notice({ msg: "縦集計キーは最高１件です、不要な縦集計キーは非表示に設定まだは削除してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.ItemCategory =='2' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "集計項目は最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }


                if ($.Enumerable.From(view.ItemList).Where("$.ItemCategory =='2' && $.error == null").ToArray().length > 1 && $.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3'").ToArray().length > 0) {
                    smat.service.notice({ msg: "集計項目は復数件の場合、縦集計キーは指定できません。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }
            }

            if (this.config.page.config.category == "SetPieChart") {

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "集計キーは最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4' && $.error == null").ToArray().length > 1) {
                    smat.service.notice({ msg: "集計キーは最高１件です、不要な集計キーは非表示に設定まだは削除してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.ItemCategory =='2' && $.error == null").ToArray().length == 0) {
                    smat.service.notice({ msg: "集計項目は最低１件が必要。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                if ($.Enumerable.From(view.ItemList).Where("$.ItemCategory =='2' && $.error == null").ToArray().length > 1) {
                    smat.service.notice({ msg: "集計項目は最高１件です。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

                //if (!form.config.pageSize) {
                //    smat.service.notice({ msg: "件数指定を入力してください。", type: "error" });
                //    this.tabBox.ui().uiControl.select(0);
                //    return false;
                //}
            }

            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.checkSave) {
                var cusCheck = smat.dynamics.analysisConfig.checkSave(this, type);
                if (cusCheck == false) {
                    return false;
                }
            }

            if (type != "preview") {

                //if (this.config.page.config.category == "SetSummaryCross"
                //|| this.config.page.config.category == "SetLineChart"
                //|| this.config.page.config.category == "SetColChart") {
                //    if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='4'").ToArray().length > 1) {
                //        smat.service.notice({ msg: "表示横集計キーは最高１件です、不要な横集計キーは非表示に設定まだは削除してください。", type: "error" });
                //        this.tabBox.ui().uiControl.select(1);
                //        return false;
                //    }

                //    if ($.Enumerable.From(view.ItemList).Where("$.IsHideInView != true && $.ItemCategory =='3'").ToArray().length > 1) {
                //        smat.service.notice({ msg: "表示縦集計キーは最高１件です、不要な縦集計キーは非表示に設定まだは削除してください。", type: "error" });
                //        this.tabBox.ui().uiControl.select(1);
                //        return false;
                //    }
                //}

                if (!this.config.page.config.title) {
                    smat.service.notice({ msg: "リクエスト名を入力してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(0);
                    return false;
                }

                //リクエスト名 check
                var filterStr = "ProjID = '" + this.config.page.config.projID + "' and Belong = '" + this.config.page.config.belong + "' and FormDesc ='" + this.config.page.config.title + "' and FormName <>'" + this.config.page.config.name + "'";
                var ok = true;
                var params = {};
                params.request = {};
                params.request.ProjID = this.config.page.config.projID;

                params.request.DsRequests = new Array();

                params.request.DsRequests.push(
                   {
                       TableName: "Y_EntityForm",
                       Filter: filterStr
                   }
                );

                smat.service.loadJosnData({
                    url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                    params: params,
                    async: false,
                    success: function (result) {
                        ok = result.ds["Y_EntityForm"].length == 0;
                    }

                });

                if (ok == false) {

                    smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit", "リクエスト名"), type: "error" });
                    this.tabBox.ui().uiControl.select(0);
                    return false;
                }


                if (this.config.page.config.groupName && !this.config.page.config.groupLinkDesc) {
                    smat.service.notice({ msg: "関連リクエストの名称を入力してください。", type: "error" });
                    this.tabBox.ui().uiControl.select(0);
                    return false;
                }

                filterStr = "ProjID = '" + this.config.page.config.projID + "' and Belong = '" + this.config.page.config.belong + "' and GroupName ='" + this.config.page.config.groupName + "' and GroupLinkDesc ='" + this.config.page.config.groupLinkDesc + "' and FormName <>'" + this.config.page.config.name + "'";
                ok = true;
                params = {};
                params.request = {};
                params.request.ProjID = this.config.page.config.projID;

                params.request.DsRequests = new Array();

                params.request.DsRequests.push(
                   {
                       TableName: "Y_EntityForm",
                       Filter: filterStr
                   }
                );

                smat.service.loadJosnData({
                    url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                    params: params,
                    async: false,
                    success: function (result) {
                        ok = result.ds["Y_EntityForm"].length == 0;
                    }

                });

                if (ok == false) {

                    smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit", "関連リクエストの名称"), type: "error" });
                    this.tabBox.ui().uiControl.select(0);
                    return false;
                }

                //error check
                debugger
                if ($.Enumerable.From(view.ItemList).Where("$.error != null ").ToArray().length > 0) {
                    smat.service.notice({ msg: "対象外項目(灰色)があり、保存できません。", type: "error" });
                    this.tabBox.ui().uiControl.select(1);
                    return false;
                }

            }

            return true;
        }, setDesignerToolbarState: function () {

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.BusinessSearch, smat.dynamics.template.BaseTemplate);

})();