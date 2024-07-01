﻿
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleCrossStatistic = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleCrossStatistic"

        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleCrossStatistic.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.tools = new Array();

            //this.tools.push(new smat.dynamics.tool.Filter({
            //    page: this.page,
            //    name: smat.service.optionSet("DyOptionText.Filter")
            //}));

            this.tools.push(new smat.dynamics.tool.UserControl({
                page: this.page,
                name: smat.service.optionSet("DyOptionText.SearchCondition")
            }));

            var reType = "";
            if (smat.dynamics.isUser()) {
                reType = "NOTN1";
            }

            this.tools.push(new smat.dynamics.tool.Filed({
                page: this.page,
                dragType: "GroupItem",
                name: "グループ項目"
            }));

            this.tools.push(new smat.dynamics.tool.Filed({
                page: this.page,
                dragType: "LogicItem",
                reType: reType,
                name: smat.service.optionSet("DyOptionText.StatisticsItem")
            }));

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
            this.page.createNewView({
                ViewName: this.page.config.designer.mainFormName,
                ViewDesc: this.page.config.designer.mainFormName,
            });

            this.mainSection = this.page.addChild({
                type: "Section",
                page: this.page,
                rowsCount: 3,
                name: "main_Section",
                designing: true
            });

            this.searchForm = this.mainSection.addChild({
                type: "Form",
                rowIndex: 0,
                name: "search_form",
                rowsCount: 2,
                tooltip: smat.service.optionSet("DyOptionText.SearchCondition"),
                view: this.page.editViewList[0].ViewName
            });

            this.toolBar = this.mainSection.addChild({
                type: "ToolBar",
                rowIndex: 1,
                name: "toolBar"
            });

            this.grid = this.mainSection.addChild({
                type: "Grid",
                rowIndex: 2,
                name: "grid1",
                //tooltip: "項目設定",
                view: this.page.editViewList[0].ViewName
            });

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 0,
                text: "検索",
                name: "search_btn",
                cssClass: "btn-primary"
            });

            //this.toolBar.addChild({
            //    type: "Button",
            //    rowIndex: 0,
            //    text: "csv",
            //    colIndex: 1,
            //    name: "csv_btn",
            //    cssClass: "btn-primary"
            //});

            this.toolBar.addChild({
                type: "Pager",
                rowIndex: 1,
                colIndex: 0,
                name: "pager",
                dataHandler: "grid1"
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
    + "\n\     params.request.GetPageCount = false;"
    + "\n\ }",
                    type: "js"
                }
            });

            //this.searchForm.addAction({
            //    action: "csv",
            //    actionBtn: "csv_btn",
            //    view: this.page.editViewList[0].ViewName
            //});

            this.searchForm.abjustColsPosition();

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SimpleCrossStatistic, smat.dynamics.template.BaseTemplate);

})();