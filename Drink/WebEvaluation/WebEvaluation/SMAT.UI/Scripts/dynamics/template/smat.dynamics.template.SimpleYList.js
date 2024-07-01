
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleYList = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleYList"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleYList.prototype = {
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
                    ItemName: field.FieldDesc,
                    ItemEntityName: this.config.page.entity.EntityName,
                    ItemFieldName: field.FieldName,
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
                tooltip: smat.service.optionSet("DyOptionText.SearchCondition"),
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
                    field: field.FieldDesc
                });
            }

            gridConfig.columns.push({
                title: "　",
                field: field.FieldDesc,
                width:"90px",
                actions: [{
                    text: "codeKind:SysText.Edit",
                    click: {
                        eventKey: "grid_actionClick",
                        jsCode:"function (dataItem,index,page) {"
+ "\n\ smat.service.openForm({"
+ "\n\     formName:'formName',"
+ "\n\     page:page,"
+ "\n\     params:{"
+ "\n\         ProjID:page.config.ProjID,"
+ "\n\         EntityName:page.config.EntityName,"
+ "\n\         EntityDataItem:dataItem"
+ "\n\     },"
+ "\n\     fillTarget: page.getFormContentId()"
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
                text: "codeKind:SysText.Search",
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
    smat.globalObject.extend(smat.dynamics.template.SimpleYList, smat.dynamics.template.BaseTemplate);

})();