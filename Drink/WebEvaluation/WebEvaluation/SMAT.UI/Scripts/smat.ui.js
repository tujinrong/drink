
(function () {



    //グローバルオブジェクト
    smat.globalObject = {

        extend: function (obj1, obj2) {
            for (var key in obj2.prototype) {
                if (obj2.prototype.hasOwnProperty(key)
						&& obj1.prototype[key] === undefined) {
                    obj1.prototype[key] = obj2.prototype[key];
                }
            }
        },
        clone: function (Obj, ignoreInfo) {
            if (typeof (Obj) != 'object') return Obj;
            if (Obj == null) return Obj;

            var newObj = new Object();
            if (Obj instanceof Array) {
                newObj = new Array();
            }

            for (var i in Obj) {
                if (ignoreInfo != undefined && ignoreInfo[i] != undefined) {
                    continue;
                }

                if (Obj[i] instanceof jQuery) {
                    continue;
                }

                newObj[i] = smat.globalObject.clone(Obj[i], ignoreInfo);
            }
            return newObj;
        }, cloneData: function (Obj, ignoreInfo) {
            if (typeof (Obj) != 'object') return Obj;
            if (Obj == null) return Obj;

            var newObj = new Object();

            for (var i in Obj) {
                if (ignoreInfo != undefined && ignoreInfo[i] != undefined) {
                    continue;
                }

                if (Obj[i] instanceof jQuery || (typeof (Obj[i]) == 'object' && Obj[i] != null) || typeof (Obj[i]) == 'function') {
                    continue;
                }

                newObj[i] = smat.globalObject.cloneData(Obj[i], ignoreInfo);
            }
            return newObj;
        }, cloneFrame: function (Obj, ignoreInfo) {
            if (typeof (Obj) == 'boolean') return false;
            if (typeof (Obj) == 'number') return 0;
            if (typeof (Obj) == 'string') return "";
            if (typeof (Obj) != 'object') return Obj;
            if (Obj == null) return Obj;

            var newObj = new Object();

            for (var i in Obj) {
                if (ignoreInfo != undefined && ignoreInfo[i] != undefined) {
                    continue;
                }

                if (Obj[i] instanceof jQuery) {
                    continue;
                }

                newObj[i] = smat.globalObject.cloneFrame(Obj[i], ignoreInfo);
            }
            return newObj;
        }
    };

    ///////////////////////////////////////////////////////////////////////
    //  smat ui globalconfig
    ///////////////////////////////////////////////////////////////////////
    smat.uiConfig = {};

    smat.uiConfig.CodeMst = {
        codeListUrl: "/Dynamics/GetOptionSet",
        kindField: "OptSetName",
        codeField: "CD",
        nameField: "Name",
        memoField: "Memo"
    }
    smat.uiConfig.ReferBtnEnableShowFlag = true;


    smat.uiConfig.lastSessionTime = new Date();
    smat.uiConfig.warnSessionTime = false;
    smat.uiConfig.warningSessionTime = false;
    smat.uiConfig.sessionLiveTime = 20 * 60;

    smat.global.language = "zn";
    smat.global.ProjID = "0";

    ///////////////////////////////////////////////////////////////////////
    //  smat ui global init
    ///////////////////////////////////////////////////////////////////////

    smat.event.CHANGE = "change";
    smat.event.CLICK = "click";
    smat.event.SELECT = "select";
    smat.event.CLOSE = "close";
    smat.event.DATA_BOUND = "dataBound";
    smat.event.OPEN = "open";
    smat.event.ROW_CLICK = "rowClick";
    smat.event.ROW_DBL_CLICK = "dblclick";
    smat.event.ACTIVATE = "activate";
    smat.event.CANCEL = "cancel";
    smat.event.COMPLETE = "complete";
    smat.event.ERROR = "error";
    smat.event.REMOVE = "remove";
    smat.event.SUCCESS = "success";
    smat.event.UPLOAD = "upload";
    smat.event.REFRESH = "refresh";
    smat.event.EXCEL_EXPORT = "excelExport";
    smat.event.LOCATION_CHANGE = "locationChange";
    smat.event.SHOW = "show";
    smat.event.HIDE = "hide";
    smat.event.PROGRESS = "progress";



    smat.globalInit = function () {
        if (smat.global.ignoreGlobalInit == false) {
            return;
        }
        if (smat.global.codeMst == undefined) smat.global.codeMst = {};
        if (smat.global.codeMstMap == undefined) smat.global.codeMstMap = {};

        if (smat.global.localServer == true) {
            var result = smat.global.codeKindList;
            for (var index in result) {
                var codeData = result[index];

                if (codeData[smat.uiConfig.CodeMst.kindField] != undefined) {
                    if (smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] == undefined) {
                        smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] = new Array();
                        smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]] = {};
                    }
                    smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]].push(codeData);
                    smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]][codeData[smat.uiConfig.CodeMst.codeField]] = codeData;

                    //2016/05/12
                    if (smat.command.afterInitCodeMst) {
                        smat.command.afterInitCodeMst();
                    }

                }
            }

        } else {
            if (smat.uiConfig.CodeMst != undefined) {
                if (smat.uiConfig.CodeMst.cache != undefined && smat.uiConfig.CodeMst.cache == true && smat.uiConfig.CodeMst.codeListUrl != undefined && smat.uiConfig.CodeMst.codeListUrl != "") {

                    var targetLang = smat.service.getSysLanguage();
                    var defaultLang = smat.global.language;

                    smat.service.loadJosnData({
                        url: smat.uiConfig.CodeMst.codeListUrl,
                        async: true,
                        params: { ProjID: smat.global.ProjID, TargetLang: targetLang, DefaultLang: defaultLang },
                        success: function (result) {

                            for (var index in result) {
                                var codeData = result[index];
                                if (codeData[smat.uiConfig.CodeMst.kindField] != undefined) {
                                    if (smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] == undefined) {
                                        smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]] = new Array();
                                        smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]] = {};
                                    }
                                    smat.global.codeMst[codeData[smat.uiConfig.CodeMst.kindField]].push(codeData);
                                    smat.global.codeMstMap[codeData[smat.uiConfig.CodeMst.kindField]][codeData[smat.uiConfig.CodeMst.codeField]] = codeData;

                                }
                            }

                            //2016/05/12
                            if (smat.command.afterInitCodeMst) {
                                smat.command.afterInitCodeMst();
                            }
                        }
                    });
                }
            }
        }

        if (smat.dynamics) {
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getReferInfo,
                async: false,
                params: { ProjID: smat.global.ProjID },
                success: function (result) {
                    for (i in result) {
                        var options = smat.service.strToJson(result[i].FormOptions);
                        var referItem = {
                            title: options.title,
                            async: {
                                openFormUrl: options.loadAllUrl,
                                loadAllUrl: options.loadAllUrl,
                                loadOneUrl: options.loadOneUrl,
                                autoCompleteUrl: options.autoCompleteUrl,
                            },
                            title: options.title,
                            doCache: options.doCache,
                            autoTemplate: options.autoTemplate,
                            valueField: options.valueField,
                            displayField: options.displayField,
                            width: options.width,
                            height: options.height,
                            projID: options.projID,
                            entityName: options.entityName,
                            pageName: options.name
                        };
                        smat.global.referInfo[options.name] = referItem;
                    }

                }
            });
        }
    }
    ///////////////////////////////////////////////////////////////////////
    //  smat ui global init
    ///////////////////////////////////////////////////////////////////////
    smat.touchInit = function () {
        // 
        //if (Modernizr.ios || Modernizr.android) {
        //    $(document).on('touchstart', '.s-overlay', function (e) { e.preventDefault(); });

        //    $(document).on('touchstart', '.s-select', function (e) {
        //        if ($(this).closest('.s-dropdown-wrap').length == 0) {
        //            $(this).click();
        //            e.preventDefault();
        //        }

        //    });

        //    $(document).on('touchend', '.s-link', function (e) { $(this).click(); e.preventDefault(); });
        //    $(document).on('touchend', '.s-button', function (e) { $(this).click(); e.preventDefault(); });
        //    $(document).on('touchstart', '.s-dropdown-wrap', function (e) {
        //        if ($(this).closest('.s-combobox').length == 0) {
        //            $(this).click(); e.preventDefault()
        //        }

        //    });
        //    $(document).on('touchstart', '.checkbox', function (e) { $(this).click(); e.preventDefault(); });
        //    $(document).on('touchstart', 'input[type="checkbox"]', function (e) { $(this).click(); e.preventDefault(); });
        //    $(document).on('touchstart', 'input[type="radio"]', function (e) { $(this).click(); e.preventDefault(); });

        //}


    }
    $(document).ready(function () {

        smat.globalInit();
        smat.touchInit();

        if (smat.uiConfig.warnSessionTime == true) {
            smat.service.beginWarnSessionTime();
        }
    });

    ///////////////////////////////////////////////////////////////////////
    //  UI Base
    ///////////////////////////////////////////////////////////////////////
    smat.UI = function (config) {

    };

    smat.UI.prototype = {
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
            this.bindEvents(config);
            this.bindTemplate(this.config);
        }, bindEvents: function (obj, target) {
            if (obj) {
                for (var key in obj) {
                    var val = obj[key];
                    // handle special keys
                    if (this.events.indexOf(key) >= 0) {
                        this.bind(key, val, target)
                    } else if (typeof (val) == "object") {
                        //this.bindEvents(val, val);

                    }

                }
            }
        }, bindTemplate: function (obj, target) {
            if (obj) {
                for (var key in obj) {
                    var val = obj[key];
                    if (typeof (val) == "object" && (val instanceof jQuery) == false) {
                        if (val) {
                            if (val.isTemplate) {
                                try {
                                    eval("var template = " + val.jsCode);
                                    obj[key] = template;
                                } catch (e) {
                                    debugger
                                }

                            } else {
                                this.bindTemplate(val);
                            }
                        }
                    }
                }
            }
        }, events: [
            smat.event.CHANGE,
            smat.event.CLICK,
            smat.event.SELECT,
            smat.event.CLOSE,
            smat.event.DATA_BOUND,
            smat.event.OPEN,
            smat.event.ROW_DBL_CLICK,
            smat.event.ROW_CLICK,
            smat.event.ACTIVATE,
            smat.event.CANCEL,
            smat.event.COMPLETE,
            smat.event.ERROR,
            smat.event.REMOVE,
            smat.event.SUCCESS,
            smat.event.UPLOAD,
            smat.event.REFRESH,
            smat.event.EXCEL_EXPORT,
            smat.event.LOCATION_CHANGE,
            smat.event.SHOW,
            smat.event.PROGRESS,
            smat.event.HIDE

        ],
        /**
		 * 共通初始化
		 * @param {Object} config
		 * @memberOf smat.UI.prototype
		 */
        initCommon: function () {

            this.page = new smat.pagerSender({
                dynamics: this.config.dynamics,
                EntityName: this.config.EntityName,
                PageName: this.config.PageName,
                ProjID: this.config.ProjID,
                PageId: this.config.PageId,
                parentPageId: this.config.parentPageId,
                pageParams: this.config.pageParams,

                entityName: this.config.EntityName,
                pageName: this.config.PageName,
                projID: this.config.ProjID,
                pageId: this.config.PageId
            });

            //inputBox
            if (this.config.inputBox != undefined) {

                var defaulInputBoxClass = "form-group";

                this.inputBoxDom = $('<div class=""></div>');
                if (this.config.inputBox.attrs != undefined) {

                    for (var aKey in this.config.inputBox.attrs) {
                        this.inputBoxDom.attr(aKey, this.config.inputBox.attrs[aKey]);
                    }

                }

                this.inputBoxDom.addClass(defaulInputBoxClass);

                $(this.config.target).replaceWith(this.inputBoxDom);

                $(this.config.target).appendTo(this.inputBoxDom);
            }
            if (this.config.name) this.config.uiName = this.config.name;
            //$(this.config.target).attr("ui-name", this.config.name);

            //label
            this.initLable();

            //required
            if (this.config.required != undefined && this.config.required == "true") {
                $(this.config.target).addClass("s-required");
            }

            var self = this;

            if ($(this.config.target).length > 0) {

                $.each($(this.config.target)[0].attributes, function (i, attrs) {
                    if (attrs.name != "" && smat.global.ignoreAttrs[attrs.name] == undefined) {
                        self.config[attrs.name] = attrs.value;
                        if (attrs.name == "value") {
                            if (attrs.value == null) {
                                self.config[attrs.name] = "";
                            }
                        }
                    }
                });
            }

        },
        initLable: function () {

            if (this.config.label != undefined) {

                var defaultlabelClass = "input-s-sm";

                var requiredMark = "";
                //required
                if (smat.global.requiredLabelMark && this.config.required != undefined && this.config.required == "true") {
                    requiredMark = "<span class='s-required-mark'>*</span>";
                }

                this.labelDom = $('<label>' + requiredMark + smat.service.cultureText(this.config.label.text) + '</label>');
                if (this.config.label.attrs != undefined) {
                    for (var aKey in this.config.label.attrs) {
                        this.labelDom.attr(aKey, this.config.label.attrs[aKey]);
                    }

                    if (this.config.label.attrs["class"] == undefined) {
                        this.labelDom.addClass(defaultlabelClass);
                    }
                } else {
                    this.labelDom.addClass(defaultlabelClass);
                }

                this.labelDom.addClass("control-label");



                $(this.config.target).before(this.labelDom);
            }

        },
        /**
		 * 共通初始化后
		 * @param {Object} config
		 * @memberOf smat.UI.prototype
		 */
        afterInit: function () {

            //cookit
            this.cookie();

        }
        , cookie: function (value) {

            if (this.config.cookie != undefined) {
                if (value == undefined) {
                    //get cookit value put in value
                    var nowValue = this.value();
                    if (nowValue == null || nowValue == undefined || nowValue == "") {
                        var cookieValue = smat.service.cookie(this.config.cookie);
                        if (cookieValue != null && cookieValue != undefined && cookieValue != "") {
                            this.value(cookieValue);
                        }
                    }

                } else {
                    // put value in cookie
                    smat.service.cookie(this.config.cookie, value);
                }
            }

        }, destroy: function () {
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            if (this.uiControl != undefined) {
                if (this.uiControl.destroy) {
                    this.uiControl.destroy();
                }
            }
        }, trigger: function (options, params) {
            var result = undefined;
            if (options != undefined && options != null) {
                var paramStr = "";
                if (arguments.length > 1) {
                    for (var i = 1; i < arguments.length; i++) {
                        if (i == 1) {
                            paramStr = "arguments[1]";
                        } else {
                            paramStr += ",arguments[" + i + "]";
                        }
                    }
                }
                if (paramStr == "") { paramStr = "this.page"; } else { paramStr += ",this.page"; }

                var eve = { ui: this };
                if (params) {
                    eve.sender = params.sender;
                    eve.dataItem = params.dataItem;
                    eve.item = params.item;
                    eve.page = this.page;
                    eve.data = params.data;
                    eve.node = params.node;
                    eve.files = params.files;
                    eve.workbook = params.workbook;
                    eve.percentComplete = params.percentComplete;

                    eve.preventDefault = function () {
                        params.preventDefault();
                    }
                }

                if (typeof (options) == "function") {
                    //return options(params);
                    this.triggerAction = options;
                    var r = eval("this.triggerAction(" + paramStr + ")")
                    this.triggerAction = undefined;
                    return r;
                } else if (typeof (options) == "object") {
                    if (smat.dynamics.logicHandler) {
                        var p = eve.sender ? "eve" : paramStr;

                        options.uiName = this.config.uiName;

                        var r = undefined;
                        try {
                            r = eval("smat.dynamics.logicHandler.fillLogic(options).action(" + p + ")");
                        } catch (e) {
                            debugger
                            r = undefined;
                        }

                        return r;
                    }
                } else if (typeof (options) == "string") {
                    if (this._events && this._events.contains(options)) {

                        var es = this._events.get(options);
                        for (var key in es) {
                            if (typeof (es[key]) == "object") {
                                es[key].uiName = this.config.uiName;
                                eval("smat.dynamics.logicHandler.fillLogic(es[key]).action(eve)");
                            } else {
                                if (typeof es[key] == 'function') {
                                    es[key](eve);
                                }
                            }

                        }

                        params.sender = eve.sender;
                        params.dataItem = eve.dataItem;
                        params.item = eve.item;
                        params.page = eve.page;
                        params.data = eve.data;
                        params.node = eve.node;
                        params.cancel = eve.cancel;
                    }
                }
            }

            return result;
        }, cBool: function (bVal) {
            if (bVal == "true" || bVal == true) {
                return true;
            } else if (bVal == "false" || bVal == false) {
                return false;
            }
        }
        , findHandler: function (id) {
            return this.page.node(id);
        }
        , value: function (value) {
            if (this.uiControl != undefined) {
                if (value == undefined) {
                    return this.uiControl.value();
                } else {
                    this.uiControl.value(value)
                }
            }
        }, bind: function (eventName, fun, target) {
            if (this.uiControl != undefined) {
                if (this.uiControl.bind) {
                    this.uiControl.bind(eventName, fun);
                }
            }

            var ebody = target || this;
            if (ebody._events == undefined) {
                ebody._events = new smat.hashMap();
            }
            if (ebody._events.contains(eventName) == false) {
                ebody._events.set(eventName, new Array());
            }
            ebody._events.get(eventName).push(fun);
        },
        focus: function () {
            if (this.uiControl != undefined) {
                if (this.uiControl.focus) {
                    this.uiControl.focus();
                }
            } else if (this.config.target != undefined) {
                this.config.target.focus();
            }
        },
        enable: function (ableFlag) {
            if (this.uiControl != undefined) {
                if (this.uiControl.enable) {
                    this.uiControl.enable(ableFlag);
                }
            }
        },
        visible: function (visibleFlag) {

        },
        label: function (labelStr) {
            if (this.labelDom) {
                $(this.labelDom).text(labelStr);
            }
        }
    }

})();