
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  editer 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Row = function (config) {
        //默认属性
        this.setConfig({
            picker: undefined,
        });

        this.setConfig(config);

        //
        this.openning == false;

        //初期化
        this.initEditer();
        this.init();

        return this;
    };

    smat.dynamics.property.Row.prototype = {
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
        },
        open: function () {
            var self = this;
            this.box = $(this.getDomStr());


            this._initInputs();


            this._setData();

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: this.box,
                width: "660px",
                title: self.config.currentDataItem.id,
                afterClose: function (result) {

                    if (result) {

                        self._setRows();
                        var value = self._row_list.children().length;
                        if (self.config.currentCell) self.config.currentCell.children('input').val(value);
                        self.config.currentControl.propertyChange(self.config.currentDataItem, value, self.config.valueConfig);
                        if (self.config.currentCell) self.config.currentCell.children('input').focus();

                        //var en = self.entity_list.ui().value();
                        //self.config.currentControl.config.viewEntityName = en;

                    }
                }
            });
            
        }, getDomStr: function () {

            return '<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 590px;">'



+ '    <div  class="" style="">'

+ '    <div class="col-sm-12 text-right" style="margin: 0;padding: 0;height: 36px;right: 10px;">'
+ '        <button id="btn_add" class="btn-primary s-button" style="">追加</button>'
+ '    </div>'

+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 100%;position: relative;">'
+ '        <div style="padding: 0 10px;">'
+ '             <div id="style_box" style="height:470px;width: 600px;margin: 0 auto;overflow-y: auto;overflow-x: hidden;">'
+ '                 <div id="_row_list"  class="" style=" width:600px; margin: 0;padding: 0;">'


+ '                 </div>'
+ '             </div>'
+ '        </div>'
+ '    </div>'


+ '    </div>'


+ '    <div class="col-sm-12 text-right" style="margin: 0;padding: 0;height: 36px;position: absolute;bottom: 0px;right: 10px;">'
+ '        <button id="btn_ok" class="btn-primary s-button" style="">確定</button>'
+ '        <button id="btn_cancel" class="btn-danger s-button" style="margin-right:10px;">キャンセル</button>'
+ '    </div>'
+ '</section>'
        }, _initInputs: function () {
            var self = this;
            this._row_list = this.box.find('#_row_list');

            this._width = this.box.find('#_width');


            this._row_list.asmatSortable({
                filter: ".sortable",
                hint: function (element) {
                    return element.clone().addClass("hint");
                },
                placeholder: function (element) {
                    return element.clone().css("opacity", "0.3").html('<span style="margin:10px 30px;display: inline-block;">drop here</span>');
                }
                //,cursorOffset: {
                //    top: -10,
                //    left: -30
                //}
            });

            this._row_list.delegate("a.s-link", "click", function () {
                
                $(this).closest('.sortable').remove();
            });


            this.btn_ok = this.box.find('#btn_ok');

            this.btn_ok.bind('click', function () {
                smat.service.closeForm({
                    contentId: self.uuid,
                    result: true
                });
            });

            this.btn_add = this.box.find('#btn_add');

            this.btn_add.bind('click', function () {
                var h = 35;
                var key = self._row_list.children().length;
                var alinkStr = '<a role="button" href="javascript:void(0)" class="s-link" style="position: absolute;top: 0px;right: 24px;"><span role="presentation" class="s-icon s-i-close">Close</span></a>';
                self._row_list.append('<div class="sortable" index=' + key + ' style="width:600px; min-height:' + h + 'px; background-color: #8BC1F5;cursor: move;margin: 1px 0;color: #ffffff;font-size: 24px;font-weight: bold;position: relative;"><span style="margin:10px 30px;display: inline-block;">' + 'rowNo:' + (key + 1) + '</span>' + alinkStr + '</div>')

            });
        }, _setData: function () {
            var self = this;
            this._row_list = this.box.find('#_row_list');

            var rowCount = Number(this.config.currentDataItem.value);

            var baseH = 45;
            for (var key = 0; key < rowCount; key++) {
                var h = this.config.currentControl.body.children(".row:eq(" + key + ")").height();
                h = 30 * (h / baseH);
                var alinkStr = '<a role="button" href="javascript:void(0)" class="s-link" style="position: absolute;top: 0px;right: 24px;"><span role="presentation" class="s-icon s-i-close">Close</span></a>';
                if (this.config.currentControl.body.children(".row:eq(" + key + ")").children().not(".row-empty-height").length != 0) {
                    alinkStr = "";
                }
                this._row_list.append('<div class="sortable" index=' + key + ' style="width:600px; min-height:' + h + 'px; background-color: #8BC1F5;cursor: move;margin: 1px 0;color: #ffffff;font-size: 24px;font-weight: bold;position: relative;"><span style="margin:10px 30px;display: inline-block;">' + 'rowNo:' + (key + 1) + '</span>' + alinkStr + '</div>')
            }

        }, _setRows: function () {
            var self = this;
            var newChildren = this._row_list.children();

            var children = this.config.currentControl.body.children(".row");
            
            var tempChildren = {};

            $.each(children, function () {
                var rowIndex = $(this).attr('row-index');
                tempChildren[rowIndex] = $(this).detach();
            });

            var newIndexs = {}
            var newIndex = 0;
            $.each(newChildren, function () {
                var rowIndex = $(this).attr('index');
                newIndexs[rowIndex] = newIndex;
                if (tempChildren[rowIndex]) {
                    tempChildren[rowIndex].appendTo(self.config.currentControl.body);
                } else {
                    $('<div class="row designing-drop" dy-uuid="' + self.config.currentControl.uuid + '" row-index = "' + rowIndex + '"><div class="row-empty-height"><div></div>').appendTo(self.config.currentControl.body);
                }
                newIndex++;
            });

            for (var key in tempChildren) {
                if (!newIndexs[key] &&  newIndexs[key] != 0) {
                    tempChildren[key].remove();
                } else {
                    tempChildren[key].attr("row-index", newIndexs[key]);
                    var cuis = tempChildren[key].children("[row-index]");
                    $.each(cuis, function () {
                        $(this).attr("row-index", newIndexs[key]);

                        var dyui = $(this).dynamicsUI();
                        if (dyui) {
                            dyui.config.rowIndex = newIndexs[key];
                        }
                    });

                    //tab
                    cuis = tempChildren[key].children('.s-tabstrip-wrapper').children("[row-index]");
                    $.each(cuis, function () {
                        $(this).attr("row-index", newIndexs[key]);

                        var dyui = $(this).dynamicsUI();
                        if (dyui) {
                            dyui.config.rowIndex = newIndexs[key];
                        }
                    });
                    
                }
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Row, smat.dynamics.Element);
})();