(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Form
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatForm = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.Form(config);
        });
    };
    /**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
    smat.Form = function (config) {
        //默认属性
        this.setConfig({
            cacheParams: null
        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

    };

    smat.Form.prototype = {
        /**
	     * 初期化
	     * @name init
	     * @methodOf smat.Form.prototype
	     */
        init: function () {


            var self = this;

            this.uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', this.uuid);
            smat.global.uiMap.set(this.uuid, this);

            this.actioned = false;

            //dynamics
            if (this.config.dynamics != true) {
                //注册按钮事件
                this.iniEvent();
            }

        }, iniEvent: function () {
            var self = this;

            if (this.config.actionBtn != undefined && this.findHandler(this.config.actionBtn).length > 0) {
                this.findHandler(this.config.actionBtn).bind("click", function (e) {

                    if ($('*[data-error]').length > 0) {
                        e.stopImmediatePropagation();
                        //e.isPropagationStopped();
                        return false;
                    }
                    if (self.doAction() == false) {
                        //终止事件
                        //e.preventDefault();
                        //e.stopPropagation();
                        //e.isDefaultPrevented();
                        //e.isImmediatePropagationStopped();
                        e.stopImmediatePropagation();
                        //e.isPropagationStopped();
                        return false;
                    }

                    //return false;
                });
            }

            if (this.config.actions != undefined) {

                for (var key in this.config.actions) {
                    var actionInfo = this.config.actions[key];

                    if (actionInfo.actionBtn != undefined) {
                        var btn = this.findHandler(actionInfo.actionBtn);
                        if (btn.length > 0) {
                            btn.attr('action-index', key);
                            btn.bind("click", function (e) {

                                if ($('*[data-error]').length > 0) {
                                    e.stopImmediatePropagation();
                                    //e.isPropagationStopped();
                                    return false;
                                }

                                var btnActionInfo = self.config.actions[$(this).attr('action-index')];


                                var confirmMsg = "";

                                if (btnActionInfo.confirmFunc != undefined) {
                                    confirmMsg = self.trigger(btnActionInfo.confirmFunc);
                                }

                                if (btnActionInfo.confirm != undefined) {
                                    confirmMsg = btnActionInfo.confirm;
                                }

                                if (confirmMsg != "") {
                                    var confirm_config = {
                                        msg: confirmMsg,
                                        callback: function () {
                                            self.doAction(btnActionInfo);
                                        }
                                    }
                                    smat.service.confirm(confirm_config);
                                } else {
                                    if (self.doAction(btnActionInfo) == false) {
                                        //终止事件
                                        e.stopImmediatePropagation();
                                        return false;
                                    }
                                }
                            });
                        }
                    }
                }
            }

            if (this.config.resetBtn != undefined && $("#" + this.config.resetBtn).length > 0) {
                $("#" + this.config.resetBtn).bind("click", function (e) {
                    self.resetFormValue();

                });
            }

            //s-input
            if (this.config.doActionOnEnterKey == true) {
                var inputs = $(this.config.target).find("input[name][type!='checkBox'][type!='radio']");
                $.each(inputs, function (n, value) {

                    $(this).keypress(function (e) { //这里给function一个事件参数命名为e，叫event也行，随意的，e就是IE窗口发生的事件。
                        var key = e.which; //e.which是按键的值
                        if (key == 13) {
                            self.doAction();
                            return false;
                        }
                    });

                });
            }
        },

        /**
	     * getConditionParam
	     * @name show
	     * @methodOf smat.Form.prototype
	     */
        getParam: function (actionInfo) {
            var self = this;
            var params = {};

            //s-input
            var inputs = $(this.config.target).find("input[name]");
            $.each(inputs, function (n, value) {

                if ($(this).attr('s-role') == "condition-field") {
                    self.fillConditionParam(params, this);
                } else {
                    self.fillParam(params, this);
                }

            });

            //select
            var selectinputs = $(this.config.target).find("select[name]");
            $.each(selectinputs, function (n, value) {

                if ($(this).attr('s-role') == "condition-field") {
                    self.fillConditionParam(params, this);
                } else {
                    self.fillParam(params, this);
                }

            });

            //textarea
            var selectinputs = $(this.config.target).find("textarea[name]");
            $.each(selectinputs, function (n, value) {

                if ($(this).attr('s-role') == "condition-field") {
                    self.fillConditionParam(params, this);
                } else {
                    self.fillParam(params, this);
                }

            });

            //grid
            var grids = $(this.config.target).find(".s-grid[sendData='true']");
            $.each(grids, function (n, value) {

                var grid = $(this).ui();


                if (grid.config.sendData.updateDataName != undefined) {
                    var updateList = grid.getUpdateDatas();
                    //alert(JSON.stringify(updateList));
                    smat.service.setJsonData(params, grid.config.sendData.updateDataName, updateList);
                }

                if (grid.config.sendData.addDataName != undefined) {
                    var addList = grid.getAddDatas();
                    smat.service.setJsonData(params, grid.config.sendData.addDataName, addList);
                }

                if (grid.config.sendData.deleteDataName != undefined) {
                    var delList = grid.getDeleteDatas();
                    smat.service.setJsonData(params, grid.config.sendData.deleteDataName, delList);
                }

                var selectedDatas = grid.getSelectedDatas();
                for (var key in selectedDatas) {
                    smat.service.setJsonData(params, key, selectedDatas[key]);
                }
            });

            //dynamics
            if (this.config.dynamics == true) {
                params.request = {};
                params.request.ProjID = this.config.ProjID;
                params.request.EntityName = this.config.EntityName;

                if (actionInfo.view != undefined && actionInfo.view != "") {
                    //view:
                    params.request.FilterDic = {}
                    
                    var inputs = $(this.config.target).find("*[dy-filter]");
                    $.each(inputs, function (n, value) {

                        self.fillDyParam(params.request.FilterDic, this);

                    });

                    //preview
                    if (this.config.entity != undefined) {
                        params.request.FilterControlList = this.config.entity.FilterControlList;
                        params.request.FilterList = this.config.entity.FilterList;
                        var view = smat.service.getItemByKey(this.config.editViewList, "ViewName", actionInfo.view);
                        if (view == undefined) {
                            view = smat.service.getItemByKey(this.config.entity.ViewList, "ViewName", actionInfo.view);
                        }
                        params.request.View = view;

                    }

                } else if (actionInfo.dyAction != undefined && actionInfo.dyAction != "") {
                    //dyAction:
                    params.request.SaveData = new Array();
                    
                    var inputs = $(this.config.target).find("input[name]");
                    $.each(inputs, function (n, value) {

                        self.fillDyParam(params.request.SaveData, this);

                    });

                }

                
            }

            return params;
        },
        fillConditionParam: function (params, node) {
            var name = node.name;
            var tempNames = name.split('.');
            var value = $(node).val();

            if ($(node).hasClass("s-calendar")) {
                value = value.replace(/\//g, '');
            }

            if ($(node).hasClass("s-timepicker")) {
                value = value.replace(/:/g, '');
            }

            if ($(node).hasClass("s-date-time")) {
                value = value.replace(/\//g, '').replace(/\:/g, '').replace(/\ /g, '');
            }

            if ($(node).length > 0 && $(node)[0].type == "radio") {
                value = $("input[name='" + $(node)[0].name + "']:checked").val();
            }

            var conditionType = "EQ";
            var valueType = "STRING";

            if ($(node).attr('conditionType') != undefined) {
                conditionType = $(node).attr('conditionType');
            }

            if (params[tempNames[0]] == undefined) {
                params[tempNames[0]] = {};
                params[tempNames[0]].items = new Array();
            }

            var condition = params[tempNames[0]];

            if (value != "") {
                condition.items[condition.items.length] = {
                    fieldName: tempNames[1],
                    value: value,
                    conditionType: conditionType,
                    valueType: valueType
                };
            }
        },
        fillParam: function (params, node) {
            var name = node.name;

            var value = $(node).val();
            if ($(node).data("asmatDropDownList")) {
                value = $(node).data("asmatDropDownList").value();
            }

            if ($(node).hasClass("s-calendar")) {
                value = value.replace(/\//g, '');
            }

            if ($(node).hasClass("s-date-time")) {
                value = value.replace(/\//g, '').replace(/\:/g, '').replace(/\ /g, '');
            }

            if ($(node).ui() instanceof smat.DatePicker) {
                var date = $(node).ui().value();
                value = asmat.toString(date, "yyyyMMdd");
            }

            if ($(node).hasClass("s-timepicker")) {
                value = value.replace(/:/g, '');
            }

            if ($(node).length > 0 && $(node)[0].type == "radio") {
                value = $("input[name='" + $(node)[0].name + "']:checked").val();
            }

            if ($(node).length > 0 && $(node)[0].type == "checkbox") {
                if ($(node)[0].checked == true) {
                    smat.service.setJsonData2(params, name, value);
                }

            } else {
                smat.service.setJsonData(params, name, value);
            }

        }, fillDyParam: function (params, node) {
            var value = "";
            if ($(node).ui()) {
                value = $(node).ui().value();
            }

            if ($(node).attr('dy-filter')) {
                params[$(node).attr('dy-filter')] = value;
            } else {
                var nameStr = $(node).attr('name');
                if (nameStr) {
                    var names = nameStr.split(".");
                    if (names.length == 2) {
                        var item = smat.service.getItemByKey(params, "DyTableName", names[0]);
                        if (item == null) {
                            item = {
                                DyTableName: this.config.EntityName
                            };
                            params.push(item)
                        }

                        item[names[1]] = value;
                    }
                }
            }
            
        },
        /**
	     * getPageParam
	     * @name show
	     * @methodOf smat.Form.prototype
	     */
        setConditionParam: function (condition) {

        },
        doAction: function (actionInfo) {
            //clearMarkers
            smat.global.errorInfos = {};
            //grid
            var grids = $(this.config.target).find(".s-grid[sendData='true']");
            $.each(grids, function (n, value) {
                var grid = $(this).ui().clearMarks();
            });

            var couCheck = true;
           
            if (actionInfo.ignoreCommonCheck == undefined || actionInfo.ignoreCommonCheck == false)
            {
                if (smat.service.doCommonCheck($(this.config.target)) == false) {
                    return false;
                }
            }

            if (actionInfo != undefined && actionInfo.checkForm != undefined) {
                couCheck = this.trigger(actionInfo.checkForm);
            } else if (this.config.checkForm != undefined) {
                couCheck = this.trigger(this.config.checkForm);
            }

            if (smat.service.isEmpty(smat.global.errorInfos) == false) {
                smat.service.isNoError();
                return false;
            }

            if (couCheck == false) {
                return false;
            }
            
            var params = this.getParam(actionInfo);


            if (actionInfo != undefined && actionInfo.getParam != undefined) {
                this.trigger(actionInfo.getParam,params);
            } else if (this.config.getParam != undefined) {
                this.trigger(this.config.getParam,params);
            }

            var actionUrl = this.config.action;

            if (actionInfo != undefined && actionInfo.action != undefined) {
                actionUrl = actionInfo.action;
                //
                if (this.config.dynamics == true) {

                    if (actionInfo.view != undefined && actionInfo.view != "") {
                        //view:
                        actionUrl = smat.dynamics.commonURL.getPageView;
                        params.request.ViewName = actionInfo.view;

                    } else if (actionInfo.dyAction != undefined && actionInfo.dyAction != "") {
                        //dyAction:
                        actionUrl = smat.dynamics.commonURL[actionInfo.dyAction];
                    }

                   
                }
            }

            var self = this;
            smat.service.openLoding();
            //使用jQuery中的$.ajax({});Ajax方法  

            smat.service.loadJosnData({
                url: smat.global.basePath + actionUrl,
                params: params,
                async : actionInfo.async,
                success: function (result) {

                    // Clear the browser to remember your password

                    //if (smat.service.isEmpty(result.errorInfo) == false) {
                    //    smat.service.showErrorInfo(result.errorInfo);
                    //    var passwordObj = $(self.config.target).find("input[type=password]");
                    //    if (passwordObj.length > 0) {
                    //        $(self.config.target).find("input[type=password]").eq(0).val("");
                    //        $(self.config.target).find("input[type=password]").eq(0).focus();
                    //    }
                    //    return false;
                    //}

                    
                    if (result != undefined && result.ReturnValue != undefined) {
                        var type = "success";
                        var msg = "処理完了しました。";
                        var breakFlag = true;
                        switch (result.ReturnValue) {
                            case 0:
                                // ReturnValue.OK = 0
                                type = "success";
                                breakFlag = false;
                                break;
                            case 1:
                                // ReturnValue.Worning = 1
                                type = "info";
                                break;
                            case 2:
                                // ReturnValue.Error = 2
                                type = "error";
                                break;
                            case 3:
                                // ReturnValue.FatalError = 3
                                type = "error";
                                break;
                            case 4:
                                // ReturnValue.Haita = 4
                                type = "haita";
                                break;
                            default:
                                type = "success";
                        }

                        if (result.Message != undefined) {
                            msg = result.Message;
                        }
                        if (type == "haita") {
                            var callback = function (result) {
                                if (result == "ok") {
                                    if (actionInfo.error != null && actionInfo.error != undefined) {
                                        actionInfo.error(result);
                                    }
                                }
                            }

                            var config = {
                                title: "排他確認",
                                content: msg,
                                callback: callback,
                                buttons: [
                                    {
                                        lbl: "&nbsp;&nbsp;&nbsp;OK&nbsp;&nbsp;&nbsp;",
                                        value: "ok",
                                        cls: "btn-primary"
                                    }
                                ]
                            }
                            smat.service.dialog(config);
                            return;
                        } else {
                            smat.service.notice({ msg: msg, type: type });
                        }

                        if (breakFlag)
                        {
                            if (actionInfo.error != null && actionInfo.error != undefined) {
                                actionInfo.error(result);
                            }
                            smat.service.closeLoding();
                            return;
                        }
                    }

                    if (result != undefined && result.refreshKey != undefined) {

                        //alert(result.refreshKey);
                        for (var referKey in smat.global.referInfo) {
                            var tableKey = smat.global.referInfo[referKey].tableKey;
                            for (var index in tableKey) {
                                if (tableKey[index] == result.refreshKey) {

                                    if (smat.global.referDataSourceMap.contains(referKey)) {
                                        var referDataKey = referKey;
                                        smat.global.referDataSourceMap.remove(referDataKey);
                                        smat.global.referDataSourceMap.remove(referDataKey + "_MAP");
                                        smat.service.doJsonURLNormal({
                                            url: smat.global.referInfo[referDataKey].async.loadAllUrl,
                                            params: {},
                                            success: function (result) {
                                                if (result.results != null) {
                                                    var datas = new Array();
                                                    for (var key in result.results) {
                                                        var data = result.results[key];
                                                        data["rkw"] = key;
                                                        //alert(data["refer-key-word"]);
                                                        datas.push(data);
                                                    }

                                                    var ds = new asmat.data.DataSource({
                                                        data: datas
                                                    });
                                                    smat.global.referDataSourceMap.set(referDataKey, ds);
                                                    smat.global.referDataSourceMap.set(referDataKey + "_MAP", result.results);
                                                }
                                            }
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (result != undefined) {
                        result.formId = self.uuid;

                        self.backupFormValue();
                    }

                    self.actioned = true;

                    //hander
                    if (actionInfo != undefined && actionInfo.resultHandler != undefined) {
                        var handler = self.findHandler(actionInfo.resultHandler).ui();
                        self.handlerWork(handler, result, params, actionUrl);
                    }

                    //success
                    if (actionInfo != undefined && actionInfo.success != undefined) {
                        self.trigger(actionInfo.success, result);
                    } else if (self.config.success != null && self.config.success != undefined) {
                        self.trigger(self.config.success, result);
                    }

                    smat.service.closeLoding();

                }

            });

            return true;
        },
        /**
	     * getPageParam
	     * @name show
	     * @methodOf smat.Form.prototype
	     */
        handlerWork: function (handler, result, params, actionUrl) {
            var self = this;
            if (handler instanceof smat.Pager) {
                handler.setActionConfig({
                    action: smat.global.basePath + actionUrl,
                    params: params
                });
                handler.setDataSource(result);
            } else if (handler instanceof smat.Grid) {
                handler.setDataSource(result);
            }
        },
        resetFormValue: function () {

            if (this.backupData != undefined) {

                if ($(this.config.target).length > 0) {
                    //
                    var ctls = $(this.config.target).find(".s-input");
                    $.each(ctls, function (n, value) {

                        if ($(this).attr("name") != undefined && $(this).attr("name").length > 0) {
                            if (this.backupData[$(this).attr("name")] != undefined) {

                                if ($(this).data("asmatDropDownList")) {
                                    $(this).data("asmatDropDownList").value(this.backupData[$(this).attr("name")]);
                                } else if ($(this).data("asmatNumericTextBox")) {
                                    $(this).data("asmatNumericTextBox").value(this.backupData[$(this).attr("name")]);
                                } else if ($(this).data("asmatEditor")) {
                                    $(this).data("asmatEditor").value(this.backupData[$(this).attr("name")]);
                                } else if ($(this).data("asmatDatePicker")) {
                                    $(this).val(asmat.toString(asmat.parseDate(this.backupData[$(this).attr("name")], "yyyyMMdd"), "yyyy/MM/dd"));
                                }
                                else if ($(this).data("asmatDateTimePicker")) {
                                    $(this).val(asmat.toString(asmat.parseDate(this.backupData[$(this).attr("name")], "yyyyMMddHHmm"), "yyyy/MM/dd HH:mm"));
                                }
                                else if ($(this).data("asmatTimePicker")) {
                                    var timeVal = this.backupData[$(this).attr("name")].replace(/:/g, '').padLeft(4, '0');
                                    $(this).data('asmatTimePicker').value(timeVal.substring(0, 2) + ":" + timeVal.substring(2, 4));
                                } else if ($(this).hasClass("s-button-group")) {
                                    $(this).ui().value(this.backupData[$(this).attr("name")]);
                                } else if ($(this).hasClass("s-refer")) {
                                    $(this).ui().value(this.backupData[$(this).attr("name")]);
                                } else {
                                    $(this).val(this.backupData[$(this).attr("name")]);
                                }
                            }
                        }
                    });
                }
            }
        },

        backupFormValue: function () {
            var self = this;
            this.backupData = {};
            var ctls = $(this.config.target).find("*[name]");
            $.each(ctls, function (n, value) {
                if ($(this).attr("name") != undefined && $(this).attr("name").length > 0) {
                    if ($(this).hasClass('s-calendar')) {
                        self.backupData[$(this).attr("name")] = $(this).val().replace(/\//g, '');
                    } if ($(this).hasClass('s-date-time')) {
                        self.backupData[$(this).attr("name")] = $(this).val().replace(/\//g, '').replace(/\:/g, '').replace(/\ /g, '');
                    } else {
                        self.backupData[$(this).attr("name")] = $(this).val();
                    }

                }

                if ($(this).ui()) {
                    $(this).ui().cookie($(this).ui().value());
                }
            });
        }, setFormData: function (dataItem, keys) {
            
            if (this.config.dynamics == true) {

                for (var key in dataItem) {
                    var input = $(this.config.target).find("*[name='" + this.config.EntityName + "." + key + "']");
                    if (input.length > 0) {
                        if (input.ui()) {
                            input.ui().value(dataItem[key]);
                        } else {
                            input.val(dataItem[key]);
                        }
                    }
                }

                this.lockKeyFeild(keys);

            }

            
        }, lockKeyFeild: function (keys) {
            
            if (this.config.dynamics == true) {

                for (var key in keys) {
                    var input = $(this.config.target).find("*[name='" + this.config.EntityName + "." + keys[key].FieldName + "']");
                    if (input.length > 0) {
                        if (input.ui()) {
                            input.ui().enable(false);
                        }
                    }
                }
            }
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Form, smat.UI);
})();