
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleYEdit = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleYEdit"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleYEdit.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.tools = new Array();

            
            //this.tools.push(new smat.dynamics.tool.Control({
            //    page: this.page,
            //    name: "コントロール"
            //}));

            this.tools.push(new smat.dynamics.tool.Filed({
                page: this.page,
                name: "フィールド"
            }));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));
           
        }, templateBuild: function () {
            
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
+ "\n\     var p = e.page.getFormParam();"
+ "\n\     if(p){"
+ "\n\         smat.service.loadJosnData({"
+ "\n\             url: smat.dynamics.commonURL.getEntityDataItem,"
+ "\n\             async: false,"
+ "\n\             params: {"
+ "\n\                 ProjID:p.ProjID,"
+ "\n\                 EntityName:p.EntityName,"
+ "\n\                 dataItem:p.EntityDataItem"
+ "\n\             },"
+ "\n\             success: function (result) {"
+ "\n\                 e.page.ui('edit_form').setFormData(result.dataItem,result.keyFields)"
+ "\n\             }"
+ "\n\         });"
+ "\n\     }"
+ "\n\ }",
                type: "js"
            }
            
            var fields = new Array();
            var rIndex = 0;
            for (var i = 0; i < this.config.page.entity.FieldList.length; i++) {

                var field = this.config.page.entity.FieldList[i];

                var dataType = "TextBox";

                if (field.DataType == "DateTime") {
                    dataType = "DatePicker";
                } else if (field.DataType == "Number"
                    || field.DataType == "SmallInt") {
                    dataType = "NumericTextBox";
                }

                fields.push({
                    type: "Field",
                    dataType: dataType,
                    rowIndex: rIndex,
                    colIndex: 0,
                    fieldName: field.FieldName,
                    name: field.FieldName,
                    label: field.FieldDesc,
                    inputBoxClass: "col-fix-1"
                });

                rIndex++;
            }

            this.toolBar = this.mainSection.addChild({
                type: "ToolBar",
                rowIndex: 0,
                name: "toolBar"
            });

            this.searchForm = this.mainSection.addChild({
                type: "Form",
                rowIndex: 1,
                name: "edit_form",
                rowsCount: fields.length
            });

            for (var key in fields) {
                this.searchForm.addChild(fields[key]);
            }

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 0,
                text: "一時保存",
                name: "save_temp_btn",
                cssClass: "btn-warning",
                click: {
                    eventKey: "button_click",
                    jsCode: "function (e) {"
+ "\n\ e.page.close();"
+ "\n\}",
                    type: "js"
                }
            });

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 1,
                text: "保存",
                name: "save_btn",
                cssClass: "btn-warning",
                click: {
                    eventKey: "button_click",
                    jsCode: "function (e) {"
+ "\n\ e.page.close();"
+ "\n\}",
                    type: "js"
                }
            });

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 2,
                text: "閉じる",
                name: "close_btn",
                cssClass: "btn-primary",
                click: {
                    eventKey: "button_click",
                    jsCode: "function (e) {"
+ "\n\ e.page.close();"
+ "\n\}",
                    type: "js"
                }
            });

            this.toolBar.addChild({
                type: "Button",
                rowIndex: 0,
                colIndex: 3,
                text: "削除",
                name: "del_btn",
                cssClass: "btn-danger",
                click: {
                    eventKey: "button_click",
                    jsCode: "function (e) {"
+ "\n\ e.page.close();"
+ "\n\}",
                    type: "js"
                }
            });

           
            //this.searchForm.addAction({
            //    action: "search",
            //    actionBtn: "search_btn",
            //    resultHandler: "pager",
            //    view: this.page.editViewList[0].ViewName
            //});

            this.searchForm.abjustColsPosition();

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SimpleYEdit, smat.dynamics.template.BaseTemplate);

})();