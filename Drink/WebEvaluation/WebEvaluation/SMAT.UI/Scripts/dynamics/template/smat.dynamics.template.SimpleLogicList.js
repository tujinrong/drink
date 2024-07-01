
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleLogicList = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleLogicList"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleLogicList.prototype = {
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

            this.tools.push(new smat.dynamics.tool.UserControl({
                page: this.page,
                userControlEntity: "Y_Entity",
                category: "UserControl",
                isTemplate: false,
                name: "commonUserControl"
            }));

            this.tools.push(new smat.dynamics.tool.Control({
                page: this.page,
                name: smat.service.optionSet("DyOptionText.Control")
            }));

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
                    ItemFieldName: field.FieldName,
                    ItemSql: this.config.page.entity.EntityName + "." + field.FieldName
                });
            }

            this.mainSection = this.page.addChild({
                type: "Section",
                page: this.page,
                rowsCount: 2,
                name: "main_Section",
                designing: true
            });

            this.page.config.loaded = {
                eventKey: "page_loaded",
                jsCode: "function (e) {"
+ "\n\            var pageEvent = e;"
+ "\n\            var sizeChange = function(e){"
+ "\n\                var tab = pageEvent.page.node('main_Tab');"
+ "\n\                var boxHeight = tab.closest('.s-scrollable').height(); "
+ "\n\                var headHeight = tab.children('ul.s-tabstrip-items').height();"
+ "\n\                tab.children('div').height(boxHeight-headHeight-12);"
+ "\n\                tab.children('div').css('overflow','auto');"
+ "\n\                var splitbar= pageEvent.page.node('main_Section').find('.s-splitbar');"
+ "\n\                splitbar.width(splitbar.width()+30);"
+ "\n\            };"
+ "\n\            e.page.node('main_Section').css('height','calc(100% - 2px)');"
+ "\n\            e.page.node('main_Section').children('.panel-body').css('height','calc(100% - 0px)');"
+ "\n\            e.page.node('main_Section').children('.panel-body').asmatSplitter({"
+ "\n\                orientation: 'vertical',"
+ "\n\                panes: ["
+ "\n\                  { collapsible:true ,size: '60%'},"
+ "\n\                  { collapsible: false,min:'30px' }"
+ "\n\                ],"
+ "\n\                contentLoad: sizeChange,"
+ "\n\                resize: sizeChange"
+ "\n\            });"
+ "\n\            var zoomBox = $('<div class=\"s-zoom-box\" status=\"both\" style=\"position: absolute;top: 4px;right: 0;width: 80px;height: 24px;text-align: right;\"></div>');"
+ "\n\            zoomBox.appendTo(e.page.node('main_Section').find('.panel-body').children('.row:last'));"
+ "\n\            var btnMin = $('<button class=\"btn-primary s-button\" style=\"padding: 3px 7px 2px 7px;\">_</button>').appendTo(zoomBox);"
+ "\n\            var btnReset = $('<button class=\"btn-primary s-button resetZoom\" style=\"padding: 3px 7px 2px 7px;\">-</button>').appendTo(zoomBox);"
+ "\n\            var btnMax = $('<button class=\"btn-primary s-button\" style=\"\">□</button>').appendTo(zoomBox);"
+ "\n\            btnMin.bind('click',function(){"
+ "\n\                var tempElement = $(this).closest('.s-zoom-box');"
+ "\n\                if(tempElement.attr('status') != 'top'){"
+ "\n\                    var sp = pageEvent.page.node('main_Section').children('.panel-body').data('asmatSplitter');"
+ "\n\                    if(tempElement.attr('status') == 'bottom'){"
+ "\n\                        sp.expand('.s-pane:first');"
+ "\n\                    }"
+ "\n\                    sp.size('.s-pane:first', (pageEvent.page.node('main_Section').height()-39)+'px');"
+ "\n\                    tempElement.attr('status','top');"
+ "\n\                }"
+ "\n\            });"
+ "\n\            btnReset.bind('click',function(){"
+ "\n\                var tempElement = $(this).closest('.s-zoom-box');"
+ "\n\                if(tempElement.attr('status') != 'both'){"
+ "\n\                    var sp = pageEvent.page.node('main_Section').children('.panel-body').data('asmatSplitter');"
+ "\n\                    if(tempElement.attr('status') == 'top'){"
+ "\n\                        //sp.expand('.s-pane:last');"
+ "\n\                        sp.size('.s-pane:first', '60%');"
+ "\n\                    }else{"
+ "\n\                        sp.expand('.s-pane:first');"
+ "\n\                    }"
+ "\n\                    tempElement.attr('status','both');"
+ "\n\                }"
+ "\n\            });"
+ "\n\            btnMax.bind('click',function(){"
+ "\n\                var tempElement = $(this).closest('.s-zoom-box');"
+ "\n\                if(tempElement.attr('status') != 'bottom'){"
+ "\n\                    var sp = pageEvent.page.node('main_Section').children('.panel-body').data('asmatSplitter');"
+ "\n\                    if(tempElement.attr('status') == 'top'){"
+ "\n\                        sp.expand('.s-pane:last');"
+ "\n\                    }"
+ "\n\                    sp.collapse('.s-pane:first');"
+ "\n\                    tempElement.attr('status','bottom');"
+ "\n\                }"
+ "\n\            });"
+ "\n\            btnMin.click();"
+ "\n\ }",
                type: "js"
            }

            this.page.config.dataRefresh = {
                eventKey: "page_dataRefresh",
                jsCode: "function (e) {"
+ "\n\   if (e.result == true) {"
+ "\n\    if (e.page.ui('grid1').config.dataSource != undefined && e.page.ui('pager').dataSource.pageData.length > 0) {"
+ "\n\        debugger;"
+ "\n\        var index = e.page.ui('grid1').select().index();"
+ "\n\        e.page.ui('pager').reload(function(){"
+ "\n\            debugger;"
+ "\n\            e.page.ui('grid1').select(index)"
+ "\n\        });"
+ "\n\    }"
+ "\n\    else {"
+ "\n\        e.page.node('search_btn').click();"
+ "\n\    }"
+ "\n\}"
+ "\n\ }",
                type: "js"
            }

            this.mainDiv = this.mainSection.addChild({
                type: "Div",
                rowIndex: 0,
                rowsCount: 3,
                name: "main_Div",
                designing: true
            });

            this.searchForm = this.mainDiv.addChild({
                type: "Form",
                rowIndex: 0,
                name: "search_form",
                rowsCount: 2,
                view: this.page.editViewList[0].ViewName
            });

            this.toolBar = this.mainDiv.addChild({
                type: "ToolBar",
                rowIndex: 1,
                name: "toolBar"
            });

            var gridConfig = {
                type: "Grid",
                rowIndex: 2,
                name: "grid1",
                //tooltip: "項目設定",
                view: this.page.editViewList[0].ViewName,
                select: {
                    eventKey: "grid_select",
                    jsCode: "function (e) {"
+ "\n\            //e.cancel = true;"
+ "\n\            var tabUi = e.page.ui('main_Tab');"
+ "\n\            tabUi.params = {"
+ "\n\                ProjID:e.page.config.ProjID,"
+ "\n\                EntityName:e.page.config.EntityName,"
+ "\n\                EntityDataItem:e.dataItem"
+ "\n\            },"
+ "\n\            tabUi.reLoad();"
+ "\n\            e.page.node('main_Section').find('button.resetZoom').click();"
+ "\n\}",
                    type: "js"
                }
            }
            gridConfig.columns = new Array();

            for (var i = 0; i < this.config.page.entity.FieldList.length; i++) {

                var field = this.config.page.entity.FieldList[i];

                gridConfig.columns.push({
                    title: field.FieldDesc,
                    field: field.FieldName
                });
            }

//            gridConfig.columns.push({
//                title: "　",
//                field: field.FieldName,
//                width:"90px",
//                actions: [{
//                    text: "編集",
//                    click: {
//                        eventKey: "grid_actionClick",
//                        jsCode:"function (dataItem,index,page) {"
//+ "\n\ smat.service.openPage({"
//+ "\n\     page: {"
//+ "\n\                projID: page.config.ProjID,"
//+ "\n\                entityName: page.config.EntityName,"
//+ "\n\                pageName: 'pageName'"
//+ "\n\            },"
//+ "\n\     params:{"
//+ "\n\         ProjID:page.config.ProjID,"
//+ "\n\         EntityName:page.config.EntityName,"
//+ "\n\         EntityDataItem:dataItem"
//+ "\n\     },"
//+ "\n\     fillTarget: page.getFormContentId(),"
//+ "\n\     afterClose: function (result) {"
//+ "\n\         if (result == true) {"
//+ "\n\             if (page.ui('grid1').dataSource != undefined && page.ui('pager').dataSource.pageData.length > 0) {"
//+ "\n\                 page.ui('pager').reload();"
//+ "\n\             }"
//+ "\n\             else {"
//+ "\n\                 page.node('search_btn').click();"
//+ "\n\             }"
//+ "\n\         }"
//+ "\n\     }"
//+ "\n\ });"
//+ "\n\}",
//                        type: "js"
//                    }
//                }]
//            });

            this.grid = this.mainDiv.addChild(gridConfig);

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 0,
                text: "codeKind:SysText.Search",
                name: "search_btn",
                cssClass: "btn-primary"
            });

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 1,
                text: "codeKind:SysText.New",
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

            this.mainTab = this.mainSection.addChild({
                type: "TabStrip",
                rowIndex: 1,
                name: "main_Tab",
                designing: true,
                activate: {
                    eventKey: "tabStrip_activate",
                    jsCode: "function (e) {"
+ "\n\            var tab = e.page.node('main_Tab');"
+ "\n\            var boxHeight = tab.closest('.s-scrollable').height(); "
+ "\n\            var headHeight = tab.children('ul.s-tabstrip-items').height();"
+ "\n\            tab.children('div').height(boxHeight-headHeight-3);"
+ "\n\            tab.children('div').css('overflow','auto');"
+ "\n\}",
                    type: "js"
                }
            });

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SimpleLogicList, smat.dynamics.template.BaseTemplate);

})();