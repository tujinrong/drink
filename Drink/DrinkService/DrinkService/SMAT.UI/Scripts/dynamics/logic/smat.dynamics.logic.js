
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  logic 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.logic.Logic = function (config) {
        //默认属性
        this.setConfig({
            
        });

        this.setConfig(config);

        this.init();

        return this;
    };

    smat.dynamics.logic.Logic.prototype = {
        
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var self = this;

        }, fillLogic: function (logicConfig) {
            this.setConfig(logicConfig);

            if (this.config.eventKey) {
                if (this.config.jsCode == undefined || this.config.jsCode == null) {
                    var parstr = "";
                    var eveDoc = smat.dynamics.eventDocs[this.config.eventKey];
                    if (eveDoc) {
                        if (eveDoc.paramType == "event") {
                            parstr = "e";
                        } else {
                            for (var key in eveDoc.params) {
                                if (parstr == "") {
                                    parstr = eveDoc.params[key].name;
                                } else {
                                    parstr += "," + eveDoc.params[key].name;
                                }
                            }
                            parstr == "" ? parstr = "page" : parstr += ",page";
                        }
                        
                    }

                    this.config.jsCode = "function (" + parstr + ") {\r\n\r\n\r\n\r\n}"
                }
            }

            return this;
        }, action: function (e) {
            var result = undefined;
            var pStr = "";
            if ((!arguments[0] === null) && arguments[0].sender) {
                pStr = "arguments[0]"
            } else {
                if (arguments.length > 0) {
                    for (var i = 0; i < arguments.length; i++) {
                        if (i == 0) {
                            pStr = "arguments[0]";
                        } else {
                            pStr += ",arguments[" + i + "]";
                        }
                    }
                }
            }

            if (this.config.type == "js" || this.config.type == "function") {
                try {
                    eval("var l = " + this.config.jsCode)
                    result = eval("l(" + pStr + ");");
                } catch (e) {
                    result = undefined;
                }
            }
            return result;
        }, jsCode: function (code) {
            if (code) {
                this.config.jsCode = code;
            } else {
                return this.config.jsCode;
            }
        }, value: function () {

            return this.config;
        }, type: function (type) {
            if (type) {
                this.config.type = type;
            } else {
                return this.config.type;
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.logic.Logic, smat.dynamics.Element);
})();