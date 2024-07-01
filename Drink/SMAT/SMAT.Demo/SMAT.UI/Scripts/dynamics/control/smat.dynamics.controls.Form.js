
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Form
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Form = function (config) {
        //默认属性
        this.setConfig({
            rowsCount: 2,
            type: "Section"
        });

        this.setConfig(config);


        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.Form.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.children = new smat.hashMap();

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag " : "";
            this.body = $('<div id="' + this.getUiId() + '"  class="form-horizontal ' + designClass + this.getClassStr() + '" style="width: 100%;float: left;' + this.getStyleStr() + '"></div>').appendTo(contextOn);
            this.editSkinBody = this.body;

            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }

            if (this.config.actions == undefined) {
                this.config.actions = new Array();
            }

            for (var i = 0; i < this.config.rowsCount; i++) {

                $('<div class="row designing-drop" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }

            var uiConfig = this.getUiConfig();

            if (this.config.page.config.preview == true) {
                uiConfig.entity = this.config.page.entity;
                uiConfig.editViewList = this.config.page.editViewList;
            }

            this.uiControl = new smat.Form(uiConfig);

            this.config.page.handleOnPageLoad(this);
        }, onPageLoad: function () {
            this.uiControl.iniEvent();
        },
        getCustomPropertyConfig: function () {

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'view',
                type: 'text',
                id: 'view',
                cmt: 'view',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'content',
                caption: 'rows',
                type: 'Row',
                id: 'rowsCount',
                cmt: 'rowsCount',
                propType: "prop"
            });

            this.editPropertyConfig.push(
            {
                group: 'entity',
                caption: 'actions',
                type: 'SubOptions',
                id: 'actions',
                cmt: 'actions',
                propType: "prop",
                titleKey: "action",
                optionConfig: [
                    {
                        group: 'action',
                        caption: 'action',
                        type: 'text',
                        id: 'action',
                        cmt: 'action',
                        propType: "prop"
                    },
                    {
                        group: 'action',
                        caption: 'actionBtn',
                        type: 'text',
                        id: 'actionBtn',
                        cmt: 'actionBtn',
                        propType: "prop"
                    },
                    {
                        group: 'result',
                        caption: 'resultHandler',
                        type: 'text',
                        id: 'resultHandler',
                        cmt: 'resultHandler',
                        propType: "prop"
                    },
                    {
                        group: 'action',
                        caption: 'view',
                        type: 'text',
                        id: 'view',
                        cmt: 'view',
                        propType: "prop"
                    },
                    {
                        group: 'action',
                        caption: 'dyAction',
                        type: 'text',
                        id: 'dyAction',
                        cmt: 'dyAction',
                        propType: "prop"
                    },
                    {
                        group: 'action',
                        caption: 'confirm',
                        type: 'text',
                        id: 'confirm',
                        cmt: 'confirm',
                        propType: "prop"
                    },
                    {
                        group: 'action',
                        caption: 'checkForm',
                        type: 'Logic',
                        id: 'checkForm',
                        eventKey: 'form_checkForm',
                        cmt: 'checkForm',
                        propType: "prop"
                    }
                    ,
                    {
                        group: 'action',
                        caption: 'getParam',
                        type: 'Logic',
                        id: 'getParam',
                        eventKey: 'form_getParam',
                        cmt: 'getParam',
                        propType: "prop"
                    },
                    {
                        group: 'action',
                        caption: 'success',
                        type: 'Logic',
                        id: 'success',
                        eventKey: 'form_success',
                        cmt: 'success',
                        propType: "prop"
                    }
                ]
            });
        }, onAddChild: function (child) {
            //if (child.config.status == undefined) {
            //    child.config.status = "0";

            //    //get or create entity filter
            //    if (child.config.defaultFieldName != undefined) {
            //        //alert(this.config.page.entity.FilterControlList);
            //        var filter = smat.service.getItemByKeyContain(this.config.page.entity.FilterList, "FilterSql", child.config.defaultFieldName);

            //        if (filter == undefined) {
            //            //new
            //            filter = {
            //                ProjID: this.config.page.config.projID,
            //                EntityName: this.config.page.config.entityName,
            //                FilterName: child.config.defaultFieldName + "_F",
            //                FilterDesc: child.config.defaultFieldName + "_F",
            //                ItemEntityAliasName: this.config.page.config.entityName,
            //                FilterSql: child.config.defaultFieldName + " = '{0}'",
            //            }

            //            this.config.page.entity.FilterList.push(filter);

            //            filterControl = {
            //                ProjID: this.config.page.config.projID,
            //                EntityName: this.config.page.config.entityName,
            //                FilterControlName: child.config.defaultFieldName + "_F",
            //                FilterDesc: child.config.defaultFieldName + "_F",
            //                FilterControlDesc: child.config.defaultFieldName + "_F",
            //                FilterNames: child.config.defaultFieldName + "_F",
            //            }

            //            this.config.page.entity.FilterControlList.push(filterControl);
            //        }

            //        child.config.filter = filter.FilterName;
            //    }
            //}
        }, addAction: function (actionInfo) {
            this.config.actions.push(actionInfo);
        }, propertyChange_style: function (property, value) {
            this.body.attr('style', "width: 100%;float: left;" + value);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Form, smat.dynamics.Section);
})();