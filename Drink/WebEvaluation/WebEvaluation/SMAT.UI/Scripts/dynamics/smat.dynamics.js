
(function () {
    smat.dynamicsConfig = {};

    smat.dynamics = {};

    smat.dynamics.template = {};
    smat.dynamics.tool = {};
    smat.dynamics.property = {};
    smat.dynamics.property.set = { };
    smat.dynamics.logic = {};
    smat.dynamics.cache = {};
    smat.dynamics.cacheOn = true;
    smat.dynamics.cache.forms = {};
    smat.dynamics.cache.userControls = {};

    smat.dynamics.uiMap = new smat.hashMap();

    smat.dynamics.role = {
        type: "9"// 9:developer , 1:normal user
    }

    smat.dynamics.commonURL = {
        getEntity: "/Dynamics/GetEntity",
        getEntityList: "/Dynamics/GetEntityList",
        getEntityDataItem: "/Dynamics/GetEntityDataItem",
        getView: "/Dynamics/GetView",
        getViewList: "/Dynamics/GetViewList",
        getForm: "/Dynamics/GetForm",
        getFormControls: "/Dynamics/GetFormControls",
        delForm: "/Dynamics/DelForm",
        save: "/Dynamics/Save",
        getDyDs: "/Dynamics/GetDyDs",
        del: "/Dynamics/Del",
        checkFormExist: "/Dynamics/CheckFormExist",
        formList: "/Dynamics/FormList",
        formEdit: "/Dynamics/FormEdit",
        formPage: "/dy",
        saveForm: "/Dynamics/SaveForm",
        saveProj: "/Dynamics/SaveProj",
        saveView: "/Dynamics/SaveView",
        dyFormPage: "/Dynamics/FormPage",
        saveUserControl: "/Dynamics/SaveUserControl",
        getUserControl: "/Dynamics/GetUserControl",
        getUserControlList: "/Dynamics/GetUserControlList",
        delUserControl: "/Dynamics/DelUserControl",
        checkUserControlExist: "/Dynamics/CheckUserControlExist",
        getPageView: "/Dynamics/GetPageView",
        getGroupPageView: "/Dynamics/GetGroupPageView",
        getFormList: "/Dynamics/GetFormList",
        execEntitySql: "/Dynamics/ExecEntitySql",
        getProjInfo: "/Dynamics/GetProjInfo",
        getMenuList: "/Dynamics/GetMenuList",
        getMenuGroupList: "/Dynamics/GetMenuGroupList",
        saveMenu: "/Dynamics/SaveMenu",
        delMenu: "/Dynamics/DelMenu",
        saveMenuGroup: "/Dynamics/SaveMenuGroup",
        saveControls: "/Dynamics/SaveControls",
        saveOptions: "/Dynamics/SaveOptions",
        getRoleList: "/Dynamics/GetRoleList",
        saveRole: "/Dynamics/SaveRole",
        delRole: "/Dynamics/DelRole",
        getFormListByEntityName: "/Dynamics/GetFormListByEntityName",
        getEntityListWithDetail: "/Dynamics/GetEntityListWithDetail",
        getReferInfo: "/Dynamics/GetReferInfo",
        dataExportToExcel: "/Dynamics/DataExportToExcel",
        dataExportToCsv: "/Dynamics/DataExportToCsv",
        dataMergeExportToExcel: "/Dynamics/DataMergeExportToExcel",
        dataMergeExportToCsv: "/Dynamics/DataMergeExportToCsv",
        dataImport: "/Dynamics/DataImport",
        findResourcePath: "/Dynamics/FindResourcePath",
        deleteResource: "/Dynamics/DeleteResource",
        sendNode: "/Dynamics/SendNode",
        getNodes: "/Dynamics/GetNodes",
        saveNodeStatus: "/Dynamics/SaveNodeStatus",
        getOptionSetAllLang: "/Dynamics/GetOptionSetAllLang",
        reInitGlobal: "/Dynamics/ReInitGlobal"
    }

    smat.dynamics.eventDocs = {
        "page_loaded": {
            name: "loaded",
            paramType: "event",
            params: []
        },
        "page_dataRefresh": {
            name: "dataRefresh",
            paramType: "event",
            params: []
        },
        "form_checkForm": {
            name: "checkForm",
            params: []
        },
        "form_success": {
            name: "success",
            params: [{
                name: "result"
            }]
        },
        "form_confirmFunc": {
            name: "confirmFunc",
            params: []
        },
        "form_getParam": {
            name: "getParam",
            params: [{
                name: "params"
            }]
        },
        "form_getResultMsg": {
            name: "getResultMsg",
            params: [{
                name: "result"
            }]
        },
        "refer_afterSetValue": {
            name: "afterSetValue",
            params: [{
                name: "data"
            }]
        },
        "refer_checkRefer": {
            name: "checkRefer",
            params: []
        },
        "refer_getParam": {
            name: "getParam",
            params: [{
                name: "params"
            }]
        },
        "refer_autoTemplate": {
            name: "autoTemplate",
            params: [{
                name: "dataItem"
            }]
        },
        "dropDownList_change": {
            name: "change",
            paramType: "event",
            params: []
        },
        "textbox_change": {
            name: "change",
            paramType: "event",
            params: []
        },
        "location_locationChange": {
            name: "locationChange",
            paramType: "event",
            params: []
        },
        "grid_actionConfirm": {
            name: "actionConfirm",
            params: [{
                name: "dataItem"
            }]
        },
        "grid_actionClick": {
            name: "click",
            params: [{
                name: "dataItem"
            }, {
                name: "index"
            }]
        },
        "grid_select": {
            name: "select",
            paramType: "event",
            params: []
        },
        "grid_dblclick": {
            name: "dblclick",
            paramType: "event",
            params: []
        },
        "grid_valueChange": {
            name: "valueChange",
            params: [
                {
                    name: "dataItem"
                }, {
                    name: "field"
                }
            ]
        },
        "grid_afterRefer": {
            name: "afterRefer",
            params: [
                {
                    name: "dataItem"
                }, {
                    name: "field"
                }, {
                    name: "data"
                }
            ]
        },
        "grid_dataBound": {
            name: "dataBound",
            paramType: "event",
            params: []
        },
        "grid_excelExport": {
            name: "excelExport",
            paramType: "event",
            params: []
        },
        "grid_column_template": {
            name: "template",
            params: [{
                name: "dataItem"
            }]
        },
        "button_click": {
            name: "click",
            paramType: "event",
            params: []
        },
        "chart_refresh": {
            name: "refresh",
            paramType: "event",
            params: []
        }
        ,
        "upload_success": {
            name: "success",
            paramType: "event",
            params: []
        },
        "upload_error": {
            name: "error",
            paramType: "event",
            params: []
        },
        "upload_select": {
            name: "select",
            paramType: "event",
            params: []
        },
        "upload_upload": {
            name: "upload",
            paramType: "event",
            params: []
        },

        "upload_progress": {
            name: "progress",
            paramType: "event",
            params: []
        },
        "upload_complete": {
            name: "complete",
            paramType: "event",
            params: []
        },
        "upload_remove": {
            name: "remove",
            paramType: "event",
            params: []
        },
        "upload_cancel": {
            name: "cancel",
            paramType: "event",
            params: []
        },
        "box_change": {
            name: "change",
            paramType: "event",
            params: []
        }
        ,
        "view_show": {
            name: "show",
            paramType: "event",
            params: []
        }
        ,
        "view_hide": {
            name: "hide",
            paramType: "event",
            params: []
        },
        "tabStrip_activate": {
            name: "activate",
            paramType: "event",
            params: []
        },
        "treeView-select": {
            name: "select",
            params: [{
                name: "dataItem"
            }]
        },
        "treeView-check": {
            name: "check",
            params: [{
                name: "dataItem"
            }]
        },
        "flow_commit": {
            name: "commit",
            params: [{
                name: "ds"
            }, {
                name: "params"
            }]
        },
        "flow_checkCommit": {
            name: "checkCommit",
            params: [{
                name: "ds"
            }, {
                name: "params"
            }]
        },
        "flow_formLoaded": {
            name: "checkCommit",
            params: [{
                name: "ds"
            }, {
                name: "params"
            }]
        }
    }

    smat.dynamics.BusinessSearchSetting = {
        "SetListSearch": {
            icon: "/SMAT.UI/images/bs/table_2_48.png",
            iconSmall: "/SMAT.UI/images/bs/table_2_32.png"
        },
        "SetSummarySearch": {
            icon: "/SMAT.UI/images/bs/table_s_48.png",
            iconSmall: "/SMAT.UI/images/bs/table_s_32.png"
        },
        "SetSummaryCross": {
            icon: "/SMAT.UI/images/bs/table_c_48.png",
            iconSmall: "/SMAT.UI/images/bs/table_c_32.png"
        },
        "SetPieChart": {
            icon: "/SMAT.UI/images/bs/chart48.png",
            iconSmall: "/SMAT.UI/images/bs/chart32.png"
        },
        "SetLineChart": {
            icon: "/SMAT.UI/images/bs/line48.png",
            iconSmall: "/SMAT.UI/images/bs/line32.png"
        },
        "SetColChart": {
            icon: "/SMAT.UI/images/bs/statistics48.png",
            iconSmall: "/SMAT.UI/images/bs/statistics32.png"
        }
    };

    $.fn.dynamicsUI = function () {
        var uuid = $(this).attr('dy-uuid');
        if (smat.dynamics.uiMap.contains(uuid)) {
            return smat.dynamics.uiMap.get(uuid);
        } else {
            return null;
        }
    };

    smat.dynamics.isDeveloper = function () {

        return smat.dynamics.role.type == "9";
    }

    smat.dynamics.isUser = function () {

        return smat.dynamics.role.type == "1";
    }

    smat.dynamics.openPage = function (config) {

        var page = new smat.dynamics.Page({
            projID: config.projID,
            entityName: config.entityName,
            name: config.formName,
            contextOn: config.contextOn,
            parentPageId: config.parentPageId,
            pageParams: config.pageParams
        });

        page.formKey = config.projID + "_" + config.entityName + "_" + config.formName;
        if (smat.dynamics.cacheOn && smat.dynamics.cache.forms[page.formKey]) {
            setTimeout(function () {
                page.cached = true;
                page.setForm(smat.dynamics.cache.forms[page.formKey]);
                if (config.afterInit) {
                    config.afterInit(page);
                }

                if (config.callBack) {
                    config.callBack(page);
                }
            }, 1);

        } else {
            smat.service.openLoding();
            var timeId = parseInt(new Date().valueOf() / 1000);
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getForm,
                params: {
                    ProjID: config.projID,
                    EntityName: config.entityName,
                    FormName: config.formName
                },
                success: function (form) {
                    page.setForm(form);
                    if (config.afterInit) {
                        config.afterInit(page);
                    }

                    if (config.callBack) {
                        config.callBack(page);
                    }
                    //smat.dynamics.cache.forms[page.formKey] = form;
                }
            });
        }
        return page;
    }

    smat.dynamics.openAppPage = function (config) {

        var page = new smat.dynamics.mobile.Page({
            projID: config.projID,
            entityName: config.entityName,
            name: config.formName,
            contextOn: config.contextOn,
            parentPageId: config.parentPageId,
            pageParams: config.pageParams
        });

        smat.service.openLoding();
        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getForm,
            params: {
                ProjID: config.projID,
                EntityName: config.entityName,
                FormName: config.formName
            },
            success: function (form) {
                page.setForm(form);
                if (config.afterInit) {
                    config.afterInit(page);
                }

                if (config.callBack) {
                    config.callBack(page);
                }
            }
        });
        return page;
    }

    smat.dynamics.printPage = function (config) {

        var pagesBox = $("<div style='width:100%;'></div>");

        var printPages = new Array();

        if (config instanceof Array) {
            printPages = config;
        } else {
            printPages.push(config);
        }

        var btn = $('<button class="btn-primary s-button" style="position: absolute;top: 15px;right: 15px;"  >打印</button>');
        btn.bind("click", function () {
            pagesBox.find(".s-dy-print").jqprint();
        });

        btn.appendTo(pagesBox);



        var ps = {};
        for (var key in printPages) {
            var pageInfo = printPages[key];

            var pageBox = $("<div style='width:100%;margin-bottom: 30px;'></div>");
            pageBox.appendTo(pagesBox);
            var page = new smat.dynamics.Page({
                projID: pageInfo.projID,
                entityName: pageInfo.entityName,
                name: pageInfo.formName,
                contextOn: pageBox,
                parentPageId: pageInfo.parentPageId,
                pageParams: pageInfo.pageParams,
                print: true
            });

            page.body.css("page-break-after", "always");

            ps[page.config.name] = page;

            smat.service.openLoding();
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getForm,
                //async:false,
                params: {
                    ProjID: pageInfo.projID,
                    EntityName: pageInfo.entityName,
                    FormName: pageInfo.formName
                },
                success: function (form) {
                    ps[form.FormName].setForm(form);
                    if (pageInfo.afterInit) {
                        pageInfo.afterInit(page);
                    }
                }
            });
        }



        config.contentDom = pagesBox;

        smat.service.openForm({
            contentDom: pagesBox,
            width: "80%",
            height: "90%",
            top: "5%",
            title: "打印",
            afterClose: function (result) {

            }
        });

        return page;
    }

    smat.dynamics.displayPage = function (config) {

        var pageBox = $("<div id='main_display_box' style='width:100%;height:100%'></div>");

        var page = new smat.dynamics.Page({
            projID: config.projID,
            entityName: config.entityName,
            name: config.formName,
            contextOn: pageBox,
            parentPageId: config.parentPageId,
            pageParams: config.pageParams,
            print: true,
            tabWork: config.tabWork
        });

        smat.service.openLoding();
        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getForm,
            params: {
                ProjID: config.projID,
                EntityName: config.entityName,
                FormName: config.formName
            },
            success: function (form) {
                page.setForm(form);
                if (config.afterInit) {
                    config.afterInit(page);
                }

                if (config.noAction) {
                    return;
                }

                var btn = $('<button class="btn-success s-button" style=""  >' + config.actionTitle + '</button>');
                btn.bind("click", function () {
                    alert(config.actionTitle);
                });

                var btnClose = $('<button class="btn-primary s-button" style="margin-left:10px;"  >閉じる</button>');
                btnClose.bind("click", function () {
                    smat.service.closeForm({
                        contentId: "main_display_box"
                    });
                });

                setTimeout(function () {
                    var pcpntent = pageBox.find(".form-horizontal :first");
                    var row = $('<div class="row text-center" style=""  ></div>').appendTo(pcpntent)
                    btn.appendTo(row);

                    btnClose.appendTo(row);
                }, 300);


            }
        });

        config.contentDom = pageBox;

        var w = "80%";
        if (config.width) w = config.width;
        smat.service.openForm({
            contentDom: pageBox,
            fillTarget:config.fillTarget,
            width: w,
            //height: "85%",
            top: "5%",
            title: config.title,
            afterClose: function (result) {

            }
        });

        return page;
    }

    smat.dynamics.entityFieldTree = function (data, reType, filter, mainEntity, RelaName, RelaDesc, Path, Alias) {

        if (!data) return null;

        var entityData = {
            text: RelaDesc || data.EntityDesc,
            imageUrl: "/SMAT.UI/images/folder.png"
        }

       
        var pathStr = "";
        var items = new Array();
        for (var key in data.FieldList) {
            var item = smat.globalObject.clone(data.FieldList[key]);
            var aliasStr = Alias;
            if (aliasStr == undefined || aliasStr == null || aliasStr == "") aliasStr = item.EntityName;
            item.text = item.FieldDesc;
            item.Alias = aliasStr;

            item.imageUrl = "/SMAT.UI/images/topic_option.png";
            item.Path = null;
            if (mainEntity != undefined) {
                item.Path = reType + ":" + mainEntity + "." + RelaName;
                if (Path && Path != "") {
                    item.Path = Path + "|" + item.Path;
                }
                pathStr = item.Path;
            }

            //
            if (smat.dynamics.isUser()) {
                if (item.IsLogicItem != true) {
                    continue;
                }
            }

            if (filter) {
                if (filter(item) == false) {
                    continue;
                }
            }


            var ignore = false;
            if (reType != "NOTN1" && smat.dynamics.isUser()) {
                var typeStr = "";

                for (var i = data.RelaN1List.length - 1; i >= 0; i--) {
                    
                    var relaItem = data.RelaN1List[i];
                    var tempStr = "," + item.FieldName;
                    if (relaItem.FieldNames == item.FieldName
                        || relaItem.FieldNames.substring(relaItem.FieldNames.length - tempStr.length) == tempStr) {

                        

                        if (pathStr.indexOf(":" + relaItem.RelaEntityName + ".") > 0) {
                            data.RelaN1List.splice(i, 1);
                            break;
                        }

                        ignore = true;

                        //raEntity
                        if (relaItem.RelaEntity == null) {
                            items.push({
                                text: typeStr + relaItem.RelaDesc,
                                imageUrl: "/SMAT.UI/images/folder.png",
                                type: "N1",
                                Alias: relaItem.Alias, items: [
                                    {
                                        text: "　",
                                        lazyLoad: true,
                                        reType: "N1",
                                        Entity: relaItem.RelaEntityName,
                                        mainEntity: data.EntityName,
                                        RelaName: relaItem.RelaName,
                                        RelaDesc: typeStr + relaItem.RelaDesc,
                                        Path: pathStr
                                    }]
                            });
                        } else {
                            var relaEntity = smat.dynamics.entityFieldTree(relaItem.RelaEntity, "N1", filter, data.EntityName, relaItem.RelaName, typeStr + relaItem.RelaDesc, pathStr, relaItem.RelaEntity.Alias);
                            if (relaEntity != null) {
                                items.push(relaEntity);
                            } else {
                                ignore = false;
                            }
                        }

                        data.RelaN1List.splice(i, 1);
                        break;
                    }
                }


            }

            //if (ignore == false) {
            //    items.push(item);
            //}
            items.push(item);

        }

        if (reType != "NOTN1") {
            var typeStr = "N1:";
            if (smat.dynamics.isUser()) {
                typeStr = "";
            }
            //n1ra
            for (var key in data.RelaN1List) {
                //var relaItem = smat.globalObject.clone(data.RelaN1List[key]);
                var relaItem = data.RelaN1List[key]

                if (pathStr.indexOf(":" + relaItem.RelaEntityName + ".") > 0) {
                    continue;
                }

                //raEntity
                if (relaItem.RelaEntity == null) {
                    items.push({
                        text: typeStr + relaItem.RelaDesc,
                        imageUrl: "/SMAT.UI/images/folder.png",
                        type: "N1",
                        Alias: relaItem.Alias, items: [
                            {
                                text: "　",
                                lazyLoad: true,
                                reType: "N1",
                                Entity: relaItem.RelaEntityName,
                                mainEntity: data.EntityName,
                                RelaName: relaItem.RelaName,
                                RelaDesc: typeStr + relaItem.RelaDesc,
                                Path: pathStr
                            }]
                    });
                } else {
                    var relaEntity = smat.dynamics.entityFieldTree(relaItem.RelaEntity, "N1", filter, data.EntityName, relaItem.RelaName, typeStr + relaItem.RelaDesc, pathStr, relaItem.RelaEntity.Alias);
                    if (relaEntity != null) {
                        items.push(relaEntity);
                    }
                }

            }
        } else {
            data.RelaN1List = undefined;
        }

        //if (reType == "" || reType == "1N")
        if (smat.dynamics.isUser() == false && reType != "NOT1N") {
            for (var key in data.Rela1NList) {
                //var relaItem = smat.globalObject.clone(data.Rela1NList[key]);
                var relaItem = data.Rela1NList[key];

                if (pathStr.indexOf(":" + relaItem.RelaEntityName + ".") > 0) {
                    continue;
                }

                //raEntity
                if (relaItem.RelaEntity == null) {
                    items.push({
                        text: "1N:" + relaItem.RelaDesc,
                        type: "1N",
                        imageUrl: "/SMAT.UI/images/folder.png",
                        Alias: relaItem.Alias, items: [{
                            text: "　",
                            lazyLoad: true,
                            reType: "1N",
                            Entity: relaItem.RelaEntityName,
                            mainEntity: data.EntityName,
                            RelaName: relaItem.RelaName,
                            RelaDesc: "1N:" + relaItem.RelaDesc,
                            Path: pathStr
                        }]
                    });
                } else {
                    var relaEntity = smat.dynamics.entityFieldTree(relaItem.RelaEntity, "1N", filter, data.EntityName, relaItem.RelaName, "1N:" + relaItem.RelaDesc, pathStr, relaItem.RelaEntity.Alias);
                    if (relaEntity != null) {
                        items.push(relaEntity);
                    }
                }

            }
        } else {
            data.Rela1NList = undefined;
        }



        if (items.length > 0) {
            entityData.items = items;
            return entityData;
        } else {
            return null;
        }


    }


    ///////////////////////////////////////////////////////////////////////
    //  Element Base
    ///////////////////////////////////////////////////////////////////////
    smat.dynamics.Element = function (config) {

    };

    smat.dynamics.Element.prototype = {
        /**
		 * 设置控件属性
		 * @param {Object} config
		 * @memberOf smat.UI.prototype
		 */
        setConfig: function (config) {
            if (this.config === undefined) {
                this.config = {};
            }
            // set properties from config
            if (config) {
                for (var key in config) {
                    var val = config[key];
                    // handle special keys

                    this.config[key] = config[key];
                }
            }
        }, optionIgnoreInfo: {
            "parent": 1,
            "page": 1,
            "designer": 1,
            "designing": 1
        }
    }

    smat.dynamics.service = {}

    smat.service.dyExport = function (request, config) {

        if (config == undefined) {
            config = {};
        }

        var url = smat.dynamics.commonURL.dataExportToExcel;
        if (config.csv) {
            url = smat.dynamics.commonURL.dataExportToCsv;
        }

        var choseViewRequest = request;
        if (request[0]) {
            choseViewRequest = request[0]
            url = smat.dynamics.commonURL.dataMergeExportToExcel;
            if (config.csv) {
                url = smat.dynamics.commonURL.dataMergeExportToCsv;
            }
        }

        smat.dynamics.service.choseViewItem(
        {
            projID: choseViewRequest.ProjID,
            entityName: choseViewRequest.EntityName,
            viewName: choseViewRequest.ViewName,
            noPickItems: config.noPickItems,
            useDescName: config.useDescName,
            title: config.title,
            noHandleRequestError: config.noHandleRequestError,
            error: function (result) {
                if (config.error) {
                    config.error(result)
                }
            },
            callBack: function (items) {
                var fields = {};
                for (var key in items) {
                    fields[items[key].ItemName] = items[key].FieldTitle
                }

                var setting = {
                    ExportField: fields
                }

                if (config.title) {
                    setting.FileName = config.title;
                }

                var params = {
                    request: request,
                    dataExportSetting: setting
                }
                if (request[0]) {
                    params.request = null;
                    params.requests = request;
                }

                smat.service.loadJosnData({
                    url: url,
                    params: params,
                    success: function (result) {
                        debugger;
                        if (result.ResultType == "NoData") {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.NoData"), type: "info" });
                        } else {
                            var url = "/Dynamics/GetExportedResource?ResourcesID=" + result.ResourceId;
                            $('body').append($('<iframe style="width:1px;height:1px;display: none;" src="' + url + '">'));
                        }
                    }
                });
            }
        }
       );
    };

    smat.service.pickDyForm = function (projID, handler) {
        var uuid = smat.service.uuid();
        var ebox = $('<section id="' + uuid + '_newUrl" class="panel panel-default " style="margin: 0;padding: 10px;height: 100%;"></section>');
        $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.EntityName") + '</label><input id="EntityName" class="input-s" style="width:calc(100% - 200px)" ><button id="_pick_search" class="btn-info s-button" style="margin-left:10px;">' + smat.service.optionSet("DyOptionText.Search") + '</button></div></div>').appendTo(ebox);
        $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><div id="form_grid"></div></div></div>').appendTo(ebox);

        var entityNameInput = ebox.find("#EntityName");
        var searchBtn = ebox.find("#_pick_search");
        var grid = ebox.find("#form_grid");
        searchBtn.smatButton({
            click: function () {
                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.getFormListByEntityName,
                    async: false,
                    params: {
                        ProjID: projID,
                        entityName: entityNameInput.val()
                    },
                    success: function (result) {
                        if (result.length == 0) {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.NoData"), type: "info" });
                        }
                        grid.ui().config.dataSource = result;
                        grid.ui().refresh();
                    }
                });
            }
        });

        entityNameInput.smatDropDownList({
            dataSource: [],
            dataValueField: "EntityName",
            dataTextField: "EntityDesc"
        });

        var columns = [
                {
                    field: "EntityName",
                    title: "EntityName",
                },
                {
                    field: "FormName",
                    title: "FormName",
                },
                {
                    field: "EntityName",
                    title: " ",
                    template: function (dataItem) {
                        return '<button class="btn-primary s-button" entityKey="' + dataItem.EntityName + '"  formKey="' + dataItem.FormName + '" style="padding: 3px 6px;min-width: 20px;">' + smat.service.optionSet("DyOptionText.Select") + '</button>';
                    },
                    width: "90px"
                }
        ]

        grid.smatGrid({
            dataSource: [],
            columns: columns
        });

        grid.delegate("button", "click", function () {
            var EntityName = $(this).attr("entityKey");
            var FormName = $(this).attr("formKey");
            smat.service.closeForm({
                contentId: uuid + '_newUrl',
                result: {
                    entityName: EntityName,
                    formName: FormName
                }
            });
        });

        smat.service.openForm({
            //m_opacity: 0,
            contentDom: ebox,
            width: "500px",
            top: "20%",
            title: smat.service.optionSet("DyOptionText.EntityForm"),
            afterClose: function (result) {
                handler(result);
            }
        });

        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getEntityList,
            async: false,
            params: {
                ProjID: projID
            },
            success: function (result) {
                entityNameInput.ui().uiControl.setDataSource(result);

            }
        });
    }

    smat.dynamics.service.choseViewItem = function (config) {

        //getViewItems
        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getView,
            //async: false,
            params: {
                ProjID: config.projID,
                EntityName: config.entityName,
                ViewName: config.viewName
            },
            noHandleRequestError: config.noHandleRequestError,
            error: function (result) {
                if (config.error) {
                    config.error(result)
                }
            },
            success: function (result) {


                if (config.noPickItems == true) {

                    debugger;
                    var choosedItem = new Array();

                    for (var key in result.ItemList) {
                        var itemInfo = result.ItemList[key];
                        if (config.useDescName == true) {
                            choosedItem.push({
                                ItemName: itemInfo.ItemName,
                                FieldTitle: itemInfo.ItemDesc
                            });
                        } else {
                            choosedItem.push({
                                ItemName: itemInfo.ItemName,
                                FieldTitle: itemInfo.ItemName
                            });
                        }

                    }

                    config.callBack(choosedItem);
                } else {
                    var uid = smat.service.uuid();
                    var ebox = $('<section id="' + this.uid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 400px;"></section>');

                    smat.service.openForm({
                        //m_opacity: 0,
                        contentDom: ebox,
                        width: "410px",
                        top: "10%",
                        title: smat.service.optionSet("DyOptionText.Name"),
                        afterClose: function (result) {
                            if (result) {
                                config.callBack(result);
                            }
                        }
                    });

                    $('<div class="row" style="margin:8px 0;"><div class="item_grid" style="height:360px;"></div></div>').appendTo(ebox);
                    $('<div class="row text-right" style="margin:8px 0;"><button id="okBtn" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(ebox);

                    var itemGridNode = ebox.find(".item_grid");

                    var uiConfig = {
                        target: itemGridNode,
                        columns: [
                            {
                                title: "　",
                                width: "40px",
                                selectedDataName: "item",
                                checkAll: true,
                                dataType: "checkBox-selecter",
                                attributes: {
                                    "class": "text-center"
                                }
                            },
                            {
                                title: "ItemName",
                                field: "ItemName"
                            }
                        ],
                        scrollable: true,
                        dataSource: result.ItemList
                    }

                    var itemGrid = new smat.Grid(uiConfig);

                    itemGridNode.find(".checkAll").click();

                    var okBtn = ebox.find("#okBtn");
                    okBtn.smatButton({
                        click: function () {

                            var selectData = itemGrid.getSelectedDatas();

                            if (selectData.item.length == 0) {
                                smat.service.notice({
                                    msg: smat.service.optionSet("SysMsg.Required", smat.service.optionSet("DyOptionText.Name")),
                                    type: "error"
                                });
                            } else {

                                var choosedItem = new Array();

                                for (var key in selectData.item) {
                                    var itemInfo = selectData.item[key];
                                    choosedItem.push({
                                        ItemName: itemInfo.ItemName,
                                        FieldTitle: itemInfo.ItemName
                                    });
                                }

                                smat.service.closeForm({
                                    contentId: uid,
                                    result: choosedItem
                                });
                            }
                        }
                    })
                }

            }

        });
    }

    smat.dynamics.service.dyImport = function (config) {
        smat.service.openForm({
            page: {
                projID: 1,
                entityName: 'Y_Resources',
                pageName: 'DyImport'
            },
            params: {
                projID: config.projID,
                entityName: config.entityName,
                fromEntityName: config.fromEntityName,
                fromFormName: config.fromFormName
            },
            title: smat.service.optionSet("DyOptionText.DataImport"),
            afterClose: function (result) {
                if (config.success) {
                    config.success(result);
                }

            }
        });
    }

    smat.dynamics.service.addNote = function (note, isRemind) {
        //temp........

        var $msg = '<a href="#" note-cd=' + note.NoteCD + ' action=' + note.Action + ' action-path=' + note.ActionPath + ' action-param=' + note.ActionParam + ' class="media list-group-item">' +
                  '<span class="pull-left thumb-sm text-center">' +
                    '<i class="fa fa-envelope-o fa-2x text-success"></i>' +
                  '</span>' +
                  '<span class="media-body block m-b-none">' +
                    '' + note.NoteContent + '<br>' +
                    '<small class="text-muted">' + asmat.toString(asmat.parseDate(note.SendTime), "yyyy/MM/dd HH:mm:ss") + '</small>' +
                  '</span>' +
                '</a>';

        var $el = $('.nav-user'), $n = $('.count:first', $el), $v = parseInt($n.text());
        $('.count', $el).show();
        if (isRemind == false) {
            $('.count', $el).text($v + 1);
        } else {
            $('.count', $el).fadeOut().fadeIn().text($v + 1);
        }

        $($msg).hide().prependTo($el.find('.list-group')).slideDown().css('display', 'block');
    }

    smat.dynamics.service.readedNote = function (NoteCD) {
        //temp........

        var $msg = $('.nav-user').find("a[note-cd='" + NoteCD + "']");
        if ($msg.length > 0) {
            var $el = $('.nav-user'), $n = $('.count:first', $el), $v = parseInt($n.text());
            $('.count', $el).text($v - 1);

            if (parseInt($n.text()) == 0) {
                $('.count', $el).hide();
            }

            var tempUrl = $msg.attr('action-path');
            var actionType = $msg.attr('action');
            var actionParam = smat.service.strToJson($msg.attr('action-param'));

            if (tempUrl.indexOf("/") == 0) {
                tempUrl = tempUrl.substr(1);
            }
            var strArr = tempUrl.split("/");

            var projID = "1";
            var entityName = strArr[0];
            var formName = strArr[1];

            var fillTarget = undefined;
            if (actionType == 'goto') {
                fillTarget = "content_box";
            }

            if (actionType == 'popup') {
                smat.service.openPage({
                    page: {
                        projID: projID,
                        entityName: entityName,
                        pageName: formName
                    },
                    width: "80%",
                    height: "80%",
                    fillTarget: fillTarget,
                    params: actionParam
                });
            }
            else if (actionType == 'openPage') {

                var pageTabUi = $("#page_tabstrip").data('asmatTabStrip');

                //判断是否已经打开
                var tabSpan = $("#page_tabstrip").find("ul.s-tabstrip-items:first li span[form-name='" + formName + "']");
                if (tabSpan.length > 0) {
                    var liDom = tabSpan.closest('li');
                    pageTabUi.select(liDom);
                    var clickLiDom = $(this);

                    clickLiDom.closest('ul').mouseout();
                    clickLiDom.closest('ul').find('li').hide();
                    setTimeout(function () { clickLiDom.closest('ul').find('li').show(); }, 10);

                } else {
                    debugger
                    //add tab
                    pageTabUi.append({
                        text: '<i class="title fa fa-list-alt"></i><span id="tab_title_' + formName + '" form-name="' + formName + '">' + $("a[url='" + $msg.attr('action-path') + "']").text() + '</span><i class="fa fa-times close"></i>',
                        encoded: false,
                        content: '<section id="content_box_' + formName + '" class="scrollable wrapper" style="padding: 0 0 0 2px;height:100%;background-color: #E8ECEC;"></section>'
                    });

                    pageTabUi.activateTab($("#tab_title_" + formName).closest('li'));

                    smat.service.openPage({
                        page: {
                            projID: projID,
                            entityName: entityName,
                            pageName: formName
                            // templateUrl: smat.dynamics.commonURL.formPage
                        },
                        fillTarget: "content_box_" + formName
                    });

                    handleTab();
                }
            }


            smat.dynamics.service.saveNodeStatus(NoteCD, "readed");

            $msg.remove();
        }
    }

    smat.dynamics.service.getNodes = function (config) {

        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getNodes,
            params: {
                ProjID: config.projID,
                NoteUserCD: config.noteUserCD,
                SendTimeFrom: config.sendTimeFrom
            },
            success: function (result) {
                if (result != null) {
                    var index = 0;
                    for (var key in result) {
                        smat.dynamics.service.addNote(result[key], index == 0);
                        index++;
                    }
                }
            }
        });
    }

    smat.dynamics.service.sendNode = function (note) {

        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.sendNode,
            params: {
                note: note
            },
            success: function (result) {

            }
        });
    }

    smat.dynamics.service.saveNodeStatus = function (noteCD, status) {

        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.saveNodeStatus,
            params: {
                NoteCD: noteCD,
                Status: status
            },
            success: function (result) {

            }
        });
    }

    smat.dynamics.getReferList = function () {
        var referList = [];
        referList.push({
            text: " ",
            value: ""
        });
        for (var key in smat.global.referInfo) {
            referList.push({
                text: key,
                value: key
            });
        }
        return referList;
    }

    smat.dynamics.service.getView = function (projID, entityName, viewName) {
        
        var params = {};
        params.request = {};
        params.request.ProjID = projID;

        params.request.DsRequests = new Array();

        params.request.DsRequests.push(
           {
               TableName: "Y_EntityView",
               Filter: "ProjID = '" + projID + "' and EntityName = '" + entityName + "' and ViewName = '"+viewName+"'"
           }
        );

        params.request.DsRequests.push(
            {
                TableName: "Y_EntityViewItem",
                Filter: "ProjID = '" + projID + "' and EntityName = '" + entityName + "' and ViewName = '" + viewName + "'"
            }
         );

        params.request.DsRequests.push(
            {
                TableName: "Y_EntityViewFilter",
                Filter: "ProjID = '" + projID + "' and EntityName = '" + entityName + "' and ViewName = '" + viewName + "'"
            }
         );

        var view;
        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getDyDs,
            params: params,
            async: false,
            success: function (result) {
                if (result.ds["Y_EntityView"].length == 1) {
                    view = result.ds["Y_EntityView"][0];
                    view.ItemList = result.ds["Y_EntityViewItem"];
                    view.ViewFilterList = result.ds["Y_EntityViewFilter"];
                }
            }

        });

        return view;
    }

    smat.dynamics.service.getEntityFieldTreeData = function (projID, entityName, callBack) {
        
        var params = {};
        params.request = {};
        params.request.ProjID = projID;

        params.request.DsRequests = new Array();

        params.request.DsRequests.push(
           {
               TableName: "Y_Entity",
               Filter: "ProjID = '" + projID + "'"
           }
        );

        params.request.DsRequests.push(
           {
               TableName: "Y_EntityField",
               Filter: "ProjID = '" + projID + "'"
           }
        );

        params.request.DsRequests.push(
           {
               TableName: "Y_EntityRela1N",
               Filter: "ProjID = '" + projID + "'"
           }
        );

        params.request.DsRequests.push(
           {
               TableName: "Y_EntityRelaN1",
               Filter: "ProjID = '" + projID + "'"
           }
        );

        if (callBack) {
            smat.service.data.getDs("EntityFieldTreeData", params, function (result) {
                var treeDatas = smat.dynamics.service._fillEntityFieldTreeData(result, entityName, 1);
                callBack(treeDatas);
            });
        } else {
            var datas = smat.service.data.getDs("EntityFieldTreeData", params);
            var treeDatas = smat.dynamics.service._fillEntityFieldTreeData(datas, entityName, 1);
            return treeDatas;
        }
    }

    smat.dynamics.service._fillEntityFieldTreeData = function (datas, entityName, depth) {

        if (depth > 5) {
            return null;
        }

        var entityDatas = $.Enumerable.From(datas["Y_Entity"]).Where("$.EntityName == '" + entityName + "'").ToArray();
        if (entityDatas.length == 0) {
            return null;
        }

        var entityData = smat.globalObject.clone(entityDatas[0]);

        var fieldList = $.Enumerable.From(datas["Y_EntityField"]).Where("$.EntityName == '" + entityName + "'").OrderBy("$.Seq").ToArray();
        entityData.FieldList = [];
        for (var key in fieldList) {
            entityData.FieldList.push(smat.globalObject.clone(fieldList[key]));
        }

        var relaN1List = $.Enumerable.From(datas["Y_EntityRelaN1"]).Where("$.EntityName == '" + entityName + "'").ToArray();
        entityData.RelaN1List = [];
        for (var key in relaN1List) {
            entityData.RelaN1List.push(smat.globalObject.clone(relaN1List[key]));
        }

        var rela1NList = $.Enumerable.From(datas["Y_EntityRela1N"]).Where("$.EntityName == '" + entityName + "'").ToArray();
        entityData.Rela1NList = [];
        for (var key in rela1NList) {
            entityData.Rela1NList.push(smat.globalObject.clone(rela1NList[key]));
        }

        
        //1N
        for (var key in entityData.Rela1NList) {
            entityData.Rela1NList[key].RelaEntity = smat.dynamics.service._fillEntityFieldTreeData(datas, entityData.Rela1NList[key].RelaEntityName, depth + 1);
        }

        //N1
        for (var key in entityData.RelaN1List) {
            entityData.RelaN1List[key].RelaEntity = smat.dynamics.service._fillEntityFieldTreeData(datas, entityData.RelaN1List[key].RelaEntityName, depth + 1);
        }

        return entityData;
    }

    smat.dynamics.service.getAnalysisFieldTreeData = function (projID, entityName, itemDiv, callBack) {

        var params = {};
        params.request = {};
        params.request.ProjID = projID;

        params.request.DsRequests = new Array();

        params.request.DsRequests.push(
           {
               TableName: "Y_AnalysisField",
               Filter: "ProjID = '" + projID + "'"
           }
        );

        if (callBack) {

            smat.service.data.getDs("AnalysisFieldTreeData", params, function (result) {

                var treeDatas = smat.dynamics.service._getAnalysisFieldTreeData(result, entityName, itemDiv);

                callBack(treeDatas);
            });

        } else {

            var datas = smat.service.data.getDs("AnalysisFieldTreeData", params);

            var treeDatas = smat.dynamics.service._getAnalysisFieldTreeData(datas, entityName, itemDiv);

            return treeDatas;
        }
    }

    smat.dynamics.service._getAnalysisFieldTreeData = function (datas, entityName, itemDiv) {

        var dataList = $.Enumerable.From(datas["Y_AnalysisField"]).Where("$.AnalysisEntityName == '" + entityName + "' && $.AnalysisKeyType == '" + itemDiv + "' ").OrderBy("$.Seq").ToArray()

        var treeDataSource = smat.dynamics.service._fillAnalysisFieldTreeData(dataList, '0');
       
        return treeDataSource;
    }

    smat.dynamics.service._fillAnalysisFieldTreeData = function (dataList, pid) {
        var nodes = [];
        for (var i in dataList) {
            var dataItem = dataList[i];

            if ((dataItem["ParentAnalysisCD"] ? dataItem["ParentAnalysisCD"] : "0") == pid) {
                children = smat.dynamics.service._fillAnalysisFieldTreeData(dataList, dataItem["AnalysisCD"]);
                if (children.length > 0) {

                    dataItem.items = children;
                }

                nodes.push(dataItem);
            }
        }
        return nodes;

    }

    smat.dynamics.service.ExportYFormsDataTojs = function (config) {

        
        var params = {};
        params.request = {};
        params.request.ProjID = 1;

        params.request.DsRequests = new Array();

        params.request.DsRequests.push(
           {
               TableName: "Y_EntityForm",
               Filter: "ProjID = '1' and EntityName not like 'Y_%'"
           }
        );

        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getDyDs,
            params: params,
            async: true,
            success: function (result) {
                debugger;

                var contentStr = "smat.dynamics.cache.forms = { '1':{}"

                var fileCount = 0;
                var formCount = 0;

                for (var key in result.ds["Y_EntityForm"]) {

                    if (key > 50) continue;

                    var formInfo = result.ds["Y_EntityForm"][key];
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.getForm,
                        async:false,
                        params: {
                            ProjID: formInfo.ProjID,
                            EntityName: formInfo.EntityName,
                            FormName: formInfo.FormName
                        },
                        success: function (resultForm) {
                            form = formInfo.ProjID + "_" + formInfo.EntityName + "_" + formInfo.FormName;
                            var key=""
                            contentStr += "\n," + "'" + form + "':" + JSON.stringify(resultForm);
                        }
                    });
                }


                contentStr += "\n}"

                var zip2 = new JSZip();
                zip2.file("forms" + fileCount + ".js", contentStr);
                var content = zip2.generate();
                location.href = "data:application/zip;base64," + content;
            }

        });

    }

})();