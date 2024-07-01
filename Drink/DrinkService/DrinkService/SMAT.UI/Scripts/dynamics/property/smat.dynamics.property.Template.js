
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Template 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Template = function (config) {
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

    smat.dynamics.property.Template.prototype = {
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
            this._template_type = this.box.find('#_template_type');
           

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: this.box,
                width: "840px",
                title: self.config.currentDataItem.cmt,
                afterClose: function (result) {

                }
            });

            this._template_type.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: "string",
                        value: "string"
                    },
                    {
                        text: "function",
                        value: "function"
                    }
                ],
                change: function (e) {
                    self.setTypeStatus();
                }
            });
            this._template_type = this.box.find('#_template_type');

            this.btn_ok.bind('click', function () {

                self.templateHandler.type(self.box.find("#_template_type").ui().value());
                self.templateHandler.jsCode(self.jsEditor.doc.getValue());
                var value = self.templateHandler.value();

                if (self.config.currentCell) self.config.currentCell.children('input').val(value);
                self.config.currentControl.propertyChange(self.config.currentDataItem, value, self.config.valueConfig);
                if (self.config.currentCell) self.config.currentCell.children('input').focus();


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
            this.templateHandler = new smat.dynamics.logic.Template({ eventKey: this.config.currentDataItem.eventKey });
            this.templateHandler.fillTemplate(self.config.currentDataItem.value);
            this._template_type.ui().value(this.templateHandler.type());
            self._txtjs.val(this.templateHandler.jsCode());

            this.jsEditor = CodeMirror.fromTextArea(self._txtjs[0], {
                lineNumbers: true,
                styleActiveLine: true,
                matchBrackets: true
            });

            this.box.find(".CodeMirror").css("height", "100%").css("top", "-2px");
            this.box.find(".CodeMirror-gutters").css("height", "100%");

            self.setTypeStatus();
        }, setTypeStatus: function () {
            this.box.find(".template-box").hide();
            this.box.find("#_type_" + this.box.find("#_template_type").ui().value()).show();

            if (this.box.find("#_template_type").ui().value()=="function") {
                this.jsEditor.focus();
            }
        }, getDomStr: function () {

            return '<section id="' + this.uuid + '"  class="panel panel-default " style="margin: 0;padding: 10px;height: 566px; min-width:800px;">'
+'    <div class="col-sm-12" style="margin: 0;padding: 0;height: 36px;">'
+'        <div style="height:36px;"><input id="_template_type" class="" style="width:260px;" /></div>'
+'    </div>'
+'    <div class="col-sm-12" style="margin: 0;padding: 0;height: 460px;">'
+'        <div id="_type_string" class="col-sm-12 template-box" style="margin: 0;padding: 0;height: 100%;border: 1px solid #ccc;">'
+'            '
+'        </div>'
+'        <div id="_type_function" class="col-sm-12 template-box" style="margin: 0;padding: 0;height: 100%;border: 1px solid #ccc;">'
+ '               <div class="row" style="margin:0;height: 100%;"><div class=" form-group" style="height: 100%;" ><textarea id="_txtjs" /></div></div>'
+'        </div>'
+'    </div>'
+ '    <div class="col-sm-12 text-right" style="margin: 5px 0 0 0;padding: 0;height: 50px;">'
+'        <button id="btn_ok" class="btn-primary s-button" style="">ok</button>'
+ '        <button id="btn_cancel" class="btn-danger s-button" style="margin-right:10px;">cancel</button>'
+'    </div>'
+'</section>'
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Template, smat.dynamics.Element);
})();