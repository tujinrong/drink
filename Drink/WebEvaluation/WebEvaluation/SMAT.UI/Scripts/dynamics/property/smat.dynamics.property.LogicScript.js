
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  LogicScript 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.LogicScript = function (config) {
        //默认属性
        this.setConfig({
            picker: undefined,
        });

        this.setConfig(config);


        //初期化
        this.initEditer();
        this.init();

        return this;
    };

    smat.dynamics.property.LogicScript.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        initEditer: function () {


            var self = this;

            this.uuid = smat.service.uuid();
            smat.dynamics.uiMap.set(this.uuid, this);

            if (this.config.picker == undefined) {
                this.config.picker = $('<span  class="s-select edit-cell-picker"><span class="s-icon s-i-custom"></span>').appendTo(this.config.currentCell);
            }

            this.config.picker.attr('dy-uuid', this.uuid);


        },
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var self = this;

            this.config.picker.bind('click', function (e) {
                //alert(self.config.currentDataItem.value)
                self.open();
                e.stopPropagation();
            });
        }, open: function () {
            var self = this;

            this.box = $(this.getDomStr());

            this.btn_ok = this.box.find('#btn_ok');
            this.btn_cancel = this.box.find('#btn_cancel');

            this._txtjs = this.box.find('#_txtjs');
            this._logic_type = this.box.find('#_logic_type');

            this._EntityName = this.box.find('#_EntityName');
            this._gridField = this.box.find('#_gridField');
            this._gridCompare = this.box.find('#_gridCompare');
            this._gridOption = this.box.find('#_gridOption');


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: this.box,
                width: "840px",
                title: self.config.currentDataItem.cmt,
                afterClose: function (result) {

                }
            });

            this._logic_type.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    
                    {
                        text: "　script　",
                        value: "js"
                    }
                ],
                change: function (e) {
                    self.setTypeStatus();
                }
            });
            this._logic_type = this.box.find('#_logic_type');

            this.btn_ok.bind('click', function () {

                self.logicHandler.type(self.box.find("#_logic_type").ui().value());
                self.logicHandler.jsCode(self.jsEditor.doc.getValue());
                //var value = self.logicHandler.value();
                var value = self.getCodeText(self.logicHandler.jsCode());

                if (self.config.currentCell) self.config.currentCell.children('textarea').val(value);
                self.config.currentControl.propertyChange(self.config.currentDataItem, value, self.config.valueConfig);
                if (self.config.currentCell) self.config.currentCell.children('textarea').focus();


                smat.service.closeForm({
                    contentId: self.uuid
                });
            });

            this.btn_cancel.bind('click', function () {

                smat.service.closeForm({
                    contentId: self.uuid
                });
            });

            //set value
            this.logicHandler = new smat.dynamics.logic.Logic({ eventKey: this.config.currentDataItem.eventKey });
            this.logicHandler.fillLogic({
                eventKey: self.config.currentDataItem.eventKey,
                jsCode: self.config.currentDataItem.value,
                type: "js"
            });
            this._logic_type.ui().value(this.logicHandler.type());
            self._txtjs.val(this.logicHandler.jsCode());

            this.jsEditor = CodeMirror.fromTextArea(self._txtjs[0], {
                lineNumbers: true,
                styleActiveLine: true,
                matchBrackets: true
            });

            this.box.find(".CodeMirror").css("height", "100%").css("top", "-2px");
            this.box.find(".CodeMirror-gutters").css("height", "100%");

            self._getEntityInfo();

            self.setTypeStatus();
        }, setTypeStatus: function () {
            this.box.find(".logic-box").hide();
            this.box.find("#_type_" + this.box.find("#_logic_type").ui().value()).show();

            if (this.box.find("#_logic_type").ui().value()=="js") {
                this.jsEditor.focus();
            }
        }, getDomStr: function () {

            return '<section id="' + this.uuid + '"  class="panel panel-default " style="margin: 0;padding: 10px;height: 566px; min-width:800px;">'
+'    <div class="col-sm-12" style="margin: 0;padding: 0;height: 36px;">'
+'        <div style="height:36px;"><input id="_logic_type" class="" style="width:260px;" /></div>'
+ '    </div>'
+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 345px;">'
+ '        <div class="row" style=""><label class="control-label text-right" style="width:90px; margin-right:5px;">表单：</label><input id="_EntityName" class="" style="width:380px;"></div>'
+ '        <div class="row" >'
+ '             <div class="col-sm-4" >'
+ '                 <div class="" >'
+ '                     <div id="_gridField" class="" style="height:300px;" ></div>'
+ '                 </div>'
+ '             </div>'
+ '             <div class="col-sm-2" >'
+ '                 <div class="" >'
+ '                     <div id="_gridOption" class="" style="height:300px;" ></div>'
+ '                 </div>'
+ '             </div>'
+ '             <div class="col-sm-2" >'
+ '                 <div class="" >'
+ '                     <div id="_gridCompare" class="" style="height:300px;"></div>'
+ '                 </div>'
+ '             </div>'
+ '        </div>'
+ '    </div>'
+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 20px;">'
+ '        <div class="row" style=""><label class="control-label text-left" style="width:90%; margin-left:20px;">脚本：　　　　　(↑↑↑双击以上选项快捷输入↑↑↑)</label></div>'
+ '    </div>'
+'    <div class="col-sm-12" style="margin: 0;padding: 0;height: 100px;">'
+'        <div id="_type_logic" class="col-sm-12 logic-box" style="margin: 0;padding: 0;height: 100%;border: 1px solid #ccc;">'
+'            '
+'        </div>'
+'        <div id="_type_js" class="col-sm-12 logic-box" style="margin: 0;padding: 0;height: 100%;border: 1px solid #ccc;">'
+ '               <div class="row" style="margin:0;height: 100%;"><div class=" form-group" style="height: 100%;" ><textarea id="_txtjs" /></div></div>'
+'        </div>'
+'        <div id="_type_bind" class="col-sm-12 logic-box" style="margin: 0;padding: 0;height: 100%;border: 1px solid #ccc;">'
+'            '
+'        </div>'
+'    </div>'
+ '    <div class="col-sm-12 text-right" style="margin: 5px 0 0 0;padding: 0;height: 50px;">'
+'        <button id="btn_ok" class="btn-primary s-button" style="">ok</button>'
+ '        <button id="btn_cancel" class="btn-danger s-button" style="margin-right:10px;">cancel</button>'
+'    </div>'
+'</section>'
        }, _getEntityInfo: function () {
            var self = this;
            this._EntityName.smatDropDownList({
                dataSource: [],
                dataValueField: "EntityName",
                dataTextField: "EntityDesc",
                change: function (e) {
                    

                }
            });

            var page = self.config.page;
            var entityInStr = "";
            self.entityDs = [];
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntity,
                async: false,
                params: {
                    ProjID: page.config.projID,
                    EntityName: page.config.entityName
                },
                success: function (result) {

                    

                    self.entityDs.push({
                        EntityName: result.EntityName,
                        EntityDesc: result.EntityDesc
                    });

                    entityInStr += "'" + result.EntityName + "',";

                    for (var key in result.RelaN1List) {
                        var relaItem = smat.globalObject.clone(result.RelaN1List[key]);

                        self.entityDs.push({
                            EntityName: relaItem.RelaEntityName,
                            EntityDesc: relaItem.RelaDesc
                        });

                        entityInStr += "'" + relaItem.RelaEntityName + "',";

                    }

                    for (var key in result.Rela1NList) {
                        var relaItem = smat.globalObject.clone(result.Rela1NList[key]);
                        self.entityDs.push({
                            EntityName: relaItem.RelaEntityName,
                            EntityDesc: relaItem.RelaDesc
                        });

                        entityInStr += "'" + relaItem.RelaEntityName + "',";
                    }
                    self._EntityName.ui().uiControl.setDataSource(self.entityDs);

                }


            });

            debugger;
            entityInStr = entityInStr.substr(0, entityInStr.length - 1);
            this.filedsDs = [];
            var params = {};
            params.request = {};
            params.request.ProjID = page.config.projID;

            params.request.DsRequests = new Array();

            params.request.DsRequests.push(
               {
                   TableName: "Y_EntityField",
                   Filter: "ProjID = '" + page.config.projID + "' and EntityName in (" + entityInStr + ")"
               }
            );

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getDyDs,
                params: params,
                async: false,
                success: function (result) {
                    self.filedsDs = result.ds["Y_EntityField"];

                }

            });

            //grid
            self._gridField.smatGrid({
                columns: [
                    {
                        title: "字段",
                        field: "FieldDesc",
                    }
                ],
                scrollable: true,
                selectable: true,
                dblclick: function (e) {
                    self.appendText("[" + self._EntityName.ui().text()+"・" + e.dataItem.FieldDesc + "]");
                }
            });

            var fds = $.Enumerable.From(self.filedsDs).Where("$.EntityName == '" + self._EntityName.ui().value() + "'").OrderBy("Number($.Seq)").ToArray();

            self._gridField.ui().setDataSource(fds);



            self._gridOption.smatGrid({
                columns: [
                    {
                        title: "操作",
                        field: "text",
                    }
                ],
                scrollable: true,
                selectable: true,
                dblclick: function (e) {
                    if (e.dataItem.value == "clear") {
                        self.clearText();
                    } else {
                        self.appendText(e.dataItem.value);
                    }
                }
            });

            var optionDs = [
                { text: "+", value: "+" },
                { text: "-", value: "-" },
                { text: "*", value: "*" },
                { text: "/", value: "/" },
                { text: "and", value: "&&" },
                { text: "or", value: "||" },
                { text: "!", value: "!" },
                { text: "(", value: "(" },
                { text: ")", value: ")" },
                { text: "clear", value: "clear" }
            ];
            self._gridOption.ui().setDataSource(optionDs);

            self._gridCompare.smatGrid({
                columns: [
                    {
                        title: "比较",
                        field: "text",
                    }
                ],
                scrollable: true,
                selectable: true,
                dblclick: function (e) {
                    self.appendText(e.dataItem.value);
                }
            });

            var compareDs = [
               { text: ">", value: ">" },
               { text: "<", value: "<" },
               { text: ">=", value: ">=" },
               { text: "<=", value: "<=" },
               { text: "!=", value: "!=" },
               { text: "=", value: "=" }
            ];
            self._gridCompare.ui().setDataSource(compareDs);

            this.jsEditor.doc.setValue(this.getDesignText(this.jsEditor.doc.getValue()));

        }, appendText: function (text) {
            this.jsEditor.doc.setValue(this.jsEditor.doc.getValue() + " " + text);
        }, clearText: function () {
            this.jsEditor.doc.setValue("");
        }, getDesignText: function (text) {
            var self = this;
            debugger;
            var con = text;
            var t;
            var fields = {};
            var reg = /\[(.*?)\]/igm;
            while ((t = reg.exec(con)) != null) {

                var fieldKey = t[1];
                if (fieldKey.indexOf(".") < 0) {
                    continue;
                }

                var entityName = fieldKey.split(".")[0];
                var fieldName = fieldKey.split(".")[1];

                var fds = $.Enumerable.From(self.filedsDs).Where("$.EntityName == '" + entityName + "' && $.FieldName == '" + fieldName + "'").ToArray();

                if (fds.length > 0) {
                    var ens = $.Enumerable.From(self.entityDs).Where("$.EntityName == '" + entityName + "'").ToArray();
                    if (ens.length > 0) {
                        fields[fieldKey] = ens[0].EntityDesc + "・" + fds[0].FieldDesc;
                    }
                }


            }

            for (var key in fields) {
                con = con.replace(new RegExp("\\[" + key + "\\]", "gm"), "[" + fields[key] + "]");
            }
            return con;

        }, getCodeText: function (text) {
            var self = this;
            debugger;
            var con = text;
            var t;
            var fields = {};
            var reg = /\[(.*?)\]/igm;
            while ((t = reg.exec(con)) != null) {

                var fieldKey = t[1];
                if (fieldKey.indexOf("・") < 0) {
                    continue;
                }

                var entityDesc = fieldKey.split("・")[0];
                var fieldDesc = fieldKey.split("・")[1];

                var ens = $.Enumerable.From(self.entityDs).Where("$.EntityDesc == '" + entityDesc + "'").ToArray();

                if (ens.length > 0) {
                    var fds = $.Enumerable.From(self.filedsDs).Where("$.EntityName == '" + ens[0].EntityName + "' && $.FieldDesc == '" + fieldDesc + "'").ToArray();

                    if (fds.length > 0) {

                        fields[fieldKey] = ens[0].EntityName + "." + fds[0].FieldName;
                    }
                }

                


            }

            for (var key in fields) {
                con = con.replace(new RegExp("\\[" + key + "\\]", "gm"), "[" + fields[key] + "]");
            }
            return con;

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.LogicScript, smat.dynamics.Element);
})();