
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleGraph = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type:"SimpleGraph"

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
            //    name: "フィルタ"
            //}));

            this.tools.push(new smat.dynamics.tool.UserControl({
                page: this.page,
                name: "検索条件"
            }));

            this.tools.push(new smat.dynamics.tool.Filed({
                page: this.page,
                dragType: "LogicItem",
                name: "集計項目"
            }));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));
           
        }, templateBuild: function () {
            var viewName = this.page.config.designer.mainFormName;
            this.page.createNewView({
                ViewName: viewName,
                ViewDesc: viewName,
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
                tooltip: "検索条件",
                view: viewName
            });

            this.toolBar = this.mainSection.addChild({
                type: "ToolBar",
                rowIndex: 1,
                name: "toolBar"
            });

            this.grid = this.mainSection.addChild({
                type: "Chart",
                title: {
                    text: this.page.config.designer.mainFormName
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
                view: viewName
            });

            this.searchForm.abjustColsPosition();

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SimpleGraph, smat.dynamics.template.BaseTemplate);

})();