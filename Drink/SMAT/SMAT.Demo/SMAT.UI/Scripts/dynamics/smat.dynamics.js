
(function() {
    smat.dynamicsConfig = {};
	
    smat.dynamics = {};

    smat.dynamics.template = {};
    smat.dynamics.tool = {};
    smat.dynamics.property = {};
    smat.dynamics.logic = {};

    smat.dynamics.uiMap = new smat.hashMap();

    smat.dynamics.commonURL = {
        getEntity: "/Dynamics/GetEntity",
        getEntityList: "/Dynamics/GetEntityList",
        getEntityDataItem: "/Dynamics/GetEntityDataItem",
        getView: "/Dynamics/GetView",
        getViewList: "/Dynamics/GetViewList",
        getForm: "/Dynamics/GetForm",
        delForm: "/Dynamics/DelForm",
        save: "/Dynamics/Save",
        del: "/Dynamics/Del",
        checkFormExist: "/Dynamics/CheckFormExist",
        formList: "/Dynamics/FormList",
        formEdit: "/Dynamics/FormEdit",
        formPage: "/dy",
        saveForm: "/Dynamics/SaveForm",
        saveView: "/Dynamics/SaveView",
        saveUserControl: "/Dynamics/SaveUserControl",
        getUserControl: "/Dynamics/GetUserControl",
        getUserControlList: "/Dynamics/GetUserControlList",
        delUserControl: "/Dynamics/DelUserControl",
        checkUserControlExist: "/Dynamics/CheckUserControlExist",
        getPageView: "/Dynamics/GetPageView",
        getFormList: "/Dynamics/GetFormList",
        execEntitySql: "/Dynamics/ExecEntitySql",
        getProjInfo: "/Dynamics/GetProjInfo",
        getMenuList: "/Dynamics/GetMenuList",
        getMenuGroupList: "/Dynamics/GetMenuGroupList",
        saveMenu: "/Dynamics/SaveMenu",
        delMenu: "/Dynamics/DelMenu",
        saveMenuGroup: "/Dynamics/SaveMenuGroup",
        getRoleList: "/Dynamics/GetRoleList",
        saveRole: "/Dynamics/SaveRole",
        delRole: "/Dynamics/DelRole",
        getFormListByEntityName: "/Dynamics/GetFormListByEntityName",
        getEntityListWithDetail: "/Dynamics/GetEntityListWithDetail",
        getReferInfo: "/Dynamics/GetReferInfo",
    }

    smat.dynamics.eventDocs = {
        "page_loaded": {
            name: "loaded",
            paramType:"event",
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
        "form_getParam":{
            name: "getParam",
            params: [{
                name: "params"
            }]
        },
        "refer_afterSetValue": {
            name: "afterSetValue",
            params: [{
                name:"data"
            }]
        },
        "refer_getParam": {
            name: "getParam",
            params: []
        },
        "dropDownList_change": {
            name: "change",
            paramType:"event",
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
            },{
                name: "index"
            }]
        },
        "grid_select": {
            name: "select",
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
            paramType:"event",
            params: []
        }
        ,
        "box_change": {
            name: "change",
            paramType:"event",
            params: []
        },
        "tabStrip_activate": {
            name: "activate",
            paramType: "event",
            params: []
        }
    }
    
    $.fn.dynamicsUI = function () {
        var uuid = $(this).attr('dy-uuid');
        if (smat.dynamics.uiMap.contains(uuid)) {
            return smat.dynamics.uiMap.get(uuid);
        } else {
            return null;
        }
    };

    smat.dynamics.openPage = function (config) {

        var page = new smat.dynamics.Page({
            projID: config.projID,
            entityName: config.entityName,
            name: config.formName,
            contextOn: config.contextOn,
            parentPageId:config.parentPageId,
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
            }
        });

    }

    smat.dynamics.entityFieldTree = function (data,filter, mainEntity, RelaDesc) {


        var entityData = {
            text: RelaDesc||data.EntityDesc
        }

        var items = new Array();
        for (var key in data.FieldList) {
            var item = smat.globalObject.clone(data.FieldList[key]);
            item.text = item.FieldDesc;
            item.Path = null;
            if (mainEntity != undefined) {
                item.Path = "N1:" + mainEntity + "." + data.EntityName;
            }
            if (filter) {
                if(filter(item) == true){
                    items.push(item);
                }
            } else {
                items.push(item);
            }
            
        }

        //n1ra
        for (var key in data.RelaN1List) {
            var relaItem = smat.globalObject.clone(data.RelaN1List[key]);

            //raEntity
            var relaEntity = smat.dynamics.entityFieldTree(relaItem.RelaEntity, filter, data.EntityName, relaItem.RelaDesc);
            if (relaEntity != null) {
                items.push(relaEntity);
            }
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

    smat.dynamics.service ={
        
    }

    smat.dynamics.getReferList = function () {
        var referList = [];
        for (var key in smat.global.referInfo) {
            referList.push({
                text: key,
                value: key
            });
        }
        return referList;
    }
    
})();