(function () {
    ///////////////////////////////////////////////////////////////////////
    //  Form
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatForm = function (config) {

        config.target = $(this);

        new smat.Form(config);

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

            //注册按钮事件
            var self = this;
            if (this.config.actionBtn != undefined && $("#" + this.config.actionBtn).length > 0) {
                $("#" + this.config.actionBtn).bind("click", function (e) {
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
        getParam: function () {
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

        },
        /**
	     * getPageParam
	     * @name show
	     * @methodOf smat.Form.prototype
	     */
        setConditionParam: function (condition) {

        },
        doAction: function () {
            //clearMarkers
            smat.global.errorInfos = {};
            //grid
            var grids = $(this.config.target).find(".s-grid[sendData='true']");
            $.each(grids, function (n, value) {
                var grid = $(this).ui().clearMarks();
            });


            var couCheck = true;
            if (this.config.checkForm != undefined) {
                couCheck = this.config.checkForm();
            }

            if (smat.service.doCommonCheck($(this.config.target)) == false) {
                return false;
            }

            if (couCheck != true) {
                return false;
            }

            var params = this.getParam();

            if (this.config.getParam != undefined) {
                this.config.getParam(params);
            }

            var self = this;
            smat.service.openLoding();
            //使用jQuery中的$.ajax({});Ajax方法  
            $.ajax({
                url: smat.global.basePath + this.config.action + "?sid=" + smat.global.sid,
                type: "POST",
                data: JSON.stringify(params).replace(new RegExp("\"null\"", "gm"), "\"\""),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    
                    if (smat.service.isEmpty(result.errorInfo) == false) {
                        smat.service.showErrorInfo(result.errorInfo);
                        var passwordObj = $(self.config.target).find("input[type=password]");
                        if (passwordObj.length > 0) {
                            $(self.config.target).find("input[type=password]").eq(0).val("");
                            $(self.config.target).find("input[type=password]").eq(0).focus();
                        }
                        return false;
                    }

                    if (result.refreshKey != undefined) {

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

                    //hander
                    if (self.config.resultHandler != undefined) {
                        var handler = $("#" + self.config.resultHandler).ui();

                        if (handler instanceof smat.Pager) {
                            handler.setActionConfig({
                                action: smat.global.basePath + self.config.action,
                                params: params
                            });
                            handler.setDataSource(result);
                        }
                    }


                    if (self.config.success != null && self.config.success != undefined) {
                        self.config.success(result);
                    }

                    smat.service.closeLoding();

                }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                    smat.service.closeLoding();
                }
            });

            return true;
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
            });
        }
    };
    // extend Node
    smat.globalObject.extend(smat.Form, smat.UI);
})();