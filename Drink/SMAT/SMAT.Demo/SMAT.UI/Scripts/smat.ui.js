
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

    smat.global.language ="zn";
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
    smat.event.SELECT = "select";
    smat.event.ROW_DBL_CLICK = "rowDblclick";
    smat.event.ACTIVATE = "activate";
    
    

    smat.globalInit = function () {
        smat.global.codeMst = {};
        smat.global.codeMstMap = {};

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

                }
            }
            
            return;
        } else {
            if (smat.uiConfig.CodeMst != undefined) {
                if (smat.uiConfig.CodeMst.cache != undefined && smat.uiConfig.CodeMst.cache == true && smat.uiConfig.CodeMst.codeListUrl != undefined && smat.uiConfig.CodeMst.codeListUrl != "") {

                    var targetLang = smat.service.getSysLanguage();
                    var defaultLang = smat.global.language;

                    smat.service.loadJosnData({
                        url: smat.uiConfig.CodeMst.codeListUrl,
                        async: false,
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
                        }
                    });
                }
            }
        }

        smat.service.loadJosnData({
            url: smat.dynamics.commonURL.getReferInfo,
            async: false,
            params: { ProjID: smat.global.ProjID},
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
    ///////////////////////////////////////////////////////////////////////
    //  smat ui global init
    ///////////////////////////////////////////////////////////////////////
    smat.touchInit = function () {
        // 
        if (Modernizr.ios || Modernizr.android) {
            $(document).on('touchstart', '.s-overlay', function (e) { e.preventDefault(); });
            
            $(document).on('touchstart', '.s-select', function (e) {
                if ($(this).closest('.s-dropdown-wrap').length == 0) {
                    $(this).click();
                    e.preventDefault();
                }
               
            });
            $(document).on('touchend', '.s-link', function (e) { $(this).click(); e.preventDefault(); });
            $(document).on('touchend', '.s-button', function (e) { $(this).click(); e.preventDefault(); });
            $(document).on('touchstart', '.s-dropdown-wrap', function (e) { $(this).click(); e.preventDefault(); });
            $(document).on('touchstart', '.checkbox', function (e) { $(this).click(); e.preventDefault(); });
            
        }
        
        
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
            this.bindTemplate(config);
        }, bindEvents: function (obj, target) {
            if (obj) {
                for (var key in obj) {
                    var val = obj[key];
                    // handle special keys
                    if (this.events.indexOf(key) >= 0) {
                        this.bind(key, val,target)
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
                                eval("var template = " + val.jsCode);
                                obj[key] = template;
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
            smat.event.SELECT,
            smat.event.ROW_DBL_CLICK,
            smat.event.ACTIVATE
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
                parentPageId: this.config.parentPageId
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

            //label
            if (this.config.label != undefined) {

                var defaultlabelClass = "input-s-sm";
                this.labelDom = $('<label>' + smat.service.cultureText(this.config.label.text) + '</label>');
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

            //required
            if (this.config.required != undefined && this.config.required == "true") {
                $(this.config.target).addClass("s-required");
            }

            var self = this;

            $.each($(this.config.target)[0].attributes, function (i, attrs) {
                if (attrs.name != "" && smat.global.ignoreAttrs[attrs.name] == undefined) {
                    self.config[attrs.name] = attrs.value;
                }
            });

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
                    for (var i = 1; i < arguments.length; i++){
                        if(i==1){
                            paramStr = "arguments[1]";
                        }else{
                            paramStr += ",arguments[" + i + "]";
                        }
                    }
                }
                if (paramStr == "") { paramStr = "this.page"; } else { paramStr += ",this.page"; }

                var eve = {ui:this};
                if (params) {
                    eve.sender = params.sender;
                    eve.dataItem = params.dataItem;
                    eve.item = params.item;
                    eve.page = this.page;
                    eve.data = params.data;
                    eve.node = params.node;
                }

                if (typeof (options) == "function") {
                    //return options(params);
                    return eval("options(" + paramStr + ")");
                } else if (typeof (options) == "object") {
                    if (smat.dynamics.logicHandler) {
                        var p = eve.sender ? "eve" : paramStr;
                        return eval("smat.dynamics.logicHandler.fillLogic(options).action(" + p + ")");
                    }
                } else if (typeof (options) == "string") {
                    if (this._events && this._events.contains(options)) {
                        
                        var es = this._events.get(options);
                        for (var key in es) {
                            if (typeof (es[key]) == "object") {
                                eval("smat.dynamics.logicHandler.fillLogic(es[key]).action(eve)");
                            } else {
                                es[key](eve);
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
        }, bind: function (eventName, fun,target) {
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
            
        }
    }

})();