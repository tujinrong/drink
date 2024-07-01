
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleMasterList = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleMasterList"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleMasterList.prototype = {
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

            //this.tools.push(new smat.dynamics.tool.Control({
            //    page: this.page,
            //    name: "コントロール"
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
            var view = this.page.createNewView({
                ViewName: this.page.config.designer.mainFormName ,
                ViewDesc: this.page.config.designer.mainFormName ,
            });

            for (var i = 0; i < this.config.page.entity.FieldList.length; i++) {
                
                var field = this.config.page.entity.FieldList[i];

                view.ItemList.push({
                    ProjID:field.ProjID,
                    EntityName: field.EntityName,
                    ViewName:view.ViewName,
                    ItemName: field.FieldName,
                    ItemDesc: field.FieldDesc,
                    ItemEntityName: this.config.page.entity.EntityName,
                    ItemSql: this.config.page.entity.EntityName + "." + field.FieldName
                });
            }

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
                view: this.page.editViewList[0].ViewName
            });

            this.toolBar = this.mainSection.addChild({
                type: "ToolBar",
                rowIndex: 1,
                name: "toolBar"
            });

            var gridConfig = {
                type: "Grid",
                rowIndex: 2,
                name: "grid1",
                //tooltip: "項目設定",
                view: this.page.editViewList[0].ViewName
            }
            gridConfig.columns = new Array();

            for (var i = 0; i < this.config.page.entity.FieldList.length; i++) {

                var field = this.config.page.entity.FieldList[i];

                gridConfig.columns.push({
                    title: field.FieldDesc,
                    field: field.FieldName
                });
            }

            gridConfig.columns.push({
                title: "　",
                field: field.FieldName,
                width:"90px",
                actions: [{
                    text: "編集",
                    click: {
                        eventKey: "grid_actionClick",
                        jsCode:"function (dataItem,index,page) {"
+ "\n\ smat.service.openPage({"
+ "\n\     page: {"
+ "\n\                projID: page.config.ProjID,"
+ "\n\                entityName: page.config.EntityName,"
+ "\n\                pageName: 'pageName'"
+ "\n\            },"
+ "\n\     params:{"
+ "\n\         ProjID:page.config.ProjID,"
+ "\n\         EntityName:page.config.EntityName,"
+ "\n\         EntityDataItem:dataItem"
+ "\n\     },"
+ "\n\     fillTarget: page.getFormContentId(),"
+ "\n\     afterClose: function (result) {"
+ "\n\         if (result == true) {"
+ "\n\             if (page.ui('grid1').dataSource != undefined && page.ui('pager').dataSource.pageData.length > 0) {"
+ "\n\                 page.ui('pager').reload();"
+ "\n\             }"
+ "\n\             else {"
+ "\n\                 page.node('search_btn').click();"
+ "\n\             }"
+ "\n\         }"
+ "\n\     }"
+ "\n\ });"
+ "\n\}",
                        type: "js"
                    }
                }]
            });

            this.grid = this.mainSection.addChild(gridConfig);

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 0,
                text: "検索",
                name: "search_btn",
                cssClass: "btn-primary"
            });

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 1,
                text: "新規",
                name: "new_btn",
                cssClass: "btn-primary",
                click: {
                    eventKey: "button_click",
                    jsCode: "function (e) {"
+ "\n\ smat.service.openForm({"
+ "\n\     page: {"
+ "\n\                projID: e.page.config.ProjID,"
+ "\n\                entityName: e.page.config.EntityName,"
+ "\n\                pageName: 'pageName'"
+ "\n\            },"
+ "\n\     fillTarget: e.page.getFormContentId(),"
+ "\n\     afterClose: function (result) {"
+ "\n\         if (result == true) {"
+ "\n\             if (e.page.ui('grid1').dataSource != undefined && e.page.ui('pager').dataSource.pageData.length > 0) {"
+ "\n\                 e.page.ui('pager').reload();"
+ "\n\             }"
+ "\n\             else {"
+ "\n\                 e.page.node('search_btn').click();"
+ "\n\             }"
+ "\n\         }"
+ "\n\     }"
+ "\n\ });"
+ "\n\}",
                    type: "js"
                }
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
                view: this.page.editViewList[0].ViewName
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
    smat.globalObject.extend(smat.dynamics.template.SimpleMasterList, smat.dynamics.template.BaseTemplate);

})();