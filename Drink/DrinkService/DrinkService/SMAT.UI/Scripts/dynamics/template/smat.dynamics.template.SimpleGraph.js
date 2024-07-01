
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleGraph = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleGraph"

        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleGraph.prototype = {
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

            this.tools.push(new smat.dynamics.tool.Filed({
                page: this.page,
                dragType: "GroupItem",
                name: "グループ項目"
            }));

            var reType = "";
            if (smat.dynamics.isUser()) {
                reType = "NOTN1";
            }

            this.tools.push(new smat.dynamics.tool.Filed({
                page: this.page,
                dragType: "LogicItem",
                reType: reType,
                name: smat.service.optionSet("DyOptionText.StatisticsItem")
            }));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));

        }, templateBuild: function () {

            var view = this.page.createNewView({
                ViewName: this.page.config.designer.mainFormName,
                ViewDesc: this.page.config.designer.mainFormName,
            });

            var viewName = view.ViewName;

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
                view: viewName
            });

            this.toolBar = this.mainSection.addChild({
                type: "ToolBar",
                rowIndex: 1,
                name: "toolBar"
            });

            var title = this.page.config.designer.mainFormDesc;
            if (title == "" || title == undefined) title = this.page.config.designer.mainFormName;
            this.grid = this.mainSection.addChild({
                type: "Chart",
                title: {
                    text: title
                },
                rowIndex: 2,
                name: "chart1",
                //tooltip: "項目設定",
                view: viewName
            });

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 0,
                text: "検索",
                name: "search_btn",
                cssClass: "btn-primary"
            });

            this.toolBar.addChild({
                type: "Pager",
                rowIndex: 1,
                colIndex: 0,
                name: "pager",
                dataHandler: "chart1"
            });


            this.searchForm.addAction({
                action: "search",
                actionBtn: "search_btn",
                resultHandler: "pager",
                view: viewName,
                getParam: {
                    eventKey: "form_getParam",
                    jsCode: "function (params,page) {"
    + "\n\     params.request.GetPageCount = false;"
    + "\n\ }",
                    type: "js"
                }
            });

            this.searchForm.abjustColsPosition();

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SimpleGraph, smat.dynamics.template.BaseTemplate);

})();