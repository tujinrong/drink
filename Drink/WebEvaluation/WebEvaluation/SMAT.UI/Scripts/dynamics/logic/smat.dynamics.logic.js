
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
                if (this.config.jsCode == undefined || this.config.jsCode == null || this.config.jsCode == "") {
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
            var self = this;
            var result = undefined;
            var pStr = "";
            if (arguments[0].sender) {
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

            var actionOptions = smat.globalObject.clone(this.config);
            if (this.config.type == "js" || this.config.type == "function") {
                try {
                    eval("var l = " + this.config.jsCode)
                    result = eval("l(" + pStr + ");");
                } catch (ex) {
                    debugger;
                    if (smat.global.debug) {

                        var box = $(self.getDomStr());

                        var _txtjs = box.find('#_txtjs');


                        var _error_msg = box.find('#_error_msg');

                        var errorStr = ex.stack.substring(0, ex.stack.indexOf("at l ") - 5);
                        var rowcol = ex.stack.substring((ex.stack.indexOf("<anonymous>:") + 12), (ex.stack.indexOf("at eval ") - 6));

                        var row = rowcol.substring(0, rowcol.indexOf(":"));
                        var col = rowcol.substr(rowcol.indexOf(":") + 1);

                        _error_msg.html("<span style='color:red;font-size: 16px;'>UI:" + actionOptions.uiName + "</span><span style=''>　　event :" + actionOptions.eventKey + "</span><br /><span style='color:red;font-size: 20px;'>" + errorStr + "</span><span style=''>　　code at row:" + row + "　col:" + col + "</span>");

                        smat.service.openForm({
                            //m_opacity: 0,
                            contentDom: box,
                            width: "840px",
                            title: "error",
                            afterClose: function (result) {

                            }
                        });

                        _txtjs.val(actionOptions.jsCode);

                        var jsEditor = CodeMirror.fromTextArea(_txtjs[0], {
                            lineNumbers: true,
                            styleActiveLine: true,
                            matchBrackets: true,
                            readOnly: true
                        });

                        jsEditor.doc.addLineClass(Number(row) - 1, "text-danger", "text-danger");

                    } else {
                        smat.service.notice({ msg: e.message });
                    }

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
        }, getDomStr: function () {

            return '<section id="' + this.uuid + '"  class="panel panel-default " style="margin: 0;padding: 10px;height: 566px; min-width:800px;">'
+ '    <div class="col-sm-12" style="margin: 5px;padding: 0;">'
+ '        <div><div id="_error_msg"></div></div>'
+ '    </div>'
+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 460px;">'
+ '        <div id="_type_js" class="col-sm-12 logic-box" style="margin: 0;padding: 0;height: 100%;border: 1px solid #ccc;">'
+ '               <div class="row" style="margin:0;height: 100%;"><div class=" form-group" style="height: 100%;" ><textarea id="_txtjs" /></div></div>'
+ '        </div>'
+ '    </div>'
+ '    <div class="col-sm-12 text-right" style="margin: 5px 0 0 0;padding: 0;height: 50px;">'
+ '        <button id="btn_ok" class="btn-primary s-button" style="">ok</button>'
+ '    </div>'
+ '</section>'
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.logic.Logic, smat.dynamics.Element);
})();