
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  View 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.ChartData = function (config) {
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

    smat.dynamics.property.ChartData.prototype = {
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

            this.view_item_tree = this.box.find('#view_item_tree');
            this.btn_ok = this.box.find('#btn_ok');
            this.btn_cancel = this.box.find('#btn_cancel');

            this._chart_type = this.box.find('#_chart_type');
            this._X = this.box.find('#_X');
            this._Y = this.box.find('#_Y');
            this._data = this.box.find('#_data');
            this._series = this.box.find('#_series');
            this._series_title = this.box.find('#_series_title');
            this.title_text = this.box.find('#title_text');
            this.title_position = this.box.find('#title_position');
            this.legend_visible = this.box.find('#legend_visible');
            this._increasing = this.box.find('#_increasing');
            this.legend_position = this.box.find('#legend_position');
            this.x_row = this.box.find('#x_row');

            this.X_format = this.box.find('#X_format');
            this.Y_format = this.box.find('#Y_format');
            this.X_title = this.box.find('#X_title');
            this.Y_title = this.box.find('#Y_title');
            
            smat.service.openForm({
                //m_opacity: 0,
                contentDom: this.box,
                width: "840px",
                title: self.config.currentDataItem.cmt,
                afterClose: function (result) {
                    self.getFormData("chartType", self._chart_type);
                    self.getFormData("XField", self._X);
                    self.getFormData("YField", self._Y);
                    self.getFormData("seriesField", self._series);
                    self.getFormData("title.text", self.title_text);
                    self.getFormData("title.position", self.title_position);
                    self.getFormData("legend.visible", self.legend_visible);
                    self.getFormData("increasing", self._increasing);
                    self.getFormData("legend.position", self.legend_position);
                    self.getFormData("XFormat", self.X_format);
                    self.getFormData("YFormat", self.Y_format);

                    self.getFormData("seriesTitle", self._series_title);
                    self.getFormData("XTitle", self.X_title);
                    self.getFormData("YTitle", self.Y_title);

                    self.config.currentControl.refresh();
                }
            });

            //viewItems
            this.setViewInfo(this.config.currentControl.config.view);

            this.initDataInput();

            this._chart_type.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: smat.service.optionSet("DyOptionCharType.CharLine"),
                        value: "line"
                    },
                    {
                        text: smat.service.optionSet("DyOptionCharType.CharArea"),
                        value: "area"
                    },
                    {
                        text: smat.service.optionSet("DyOptionCharType.CharColumn"),
                        value: "column"
                    },
                    {
                        text: smat.service.optionSet("DyOptionCharType.CharBar"),
                        value: "bar"
                    },
                    {
                        text: smat.service.optionSet("DyOptionCharType.CharDonut"),
                        value: "donut"
                    },
                    {
                        text: smat.service.optionSet("DyOptionCharType.CharPie"),
                        value: "pie"
                    },{
                        text: smat.service.optionSet("DyOptionCharType.CharRadarArea"),
                        value: "radarArea"
                    },{
                        text: smat.service.optionSet("DyOptionCharType.CharRadarLine"),
                        value: "radarLine"
                    },{
                        text: smat.service.optionSet("DyOptionCharType.CharRadarColumn"),
                        value: "radarColumn"
                    }
                ],
                    change: function (e) {
                        self.setTypeState();
                    }
            });

            this.X_format.smatComboBox({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: smat.service.optionSet("DyOptionText.YMD"), value: smat.service.optionSet("DyOptionFormat.YMD") },
                    { text: smat.service.optionSet("DyOptionText.YM"), value: smat.service.optionSet("DyOptionFormat.YM") },
                    { text: smat.service.optionSet("DyOptionText.Y"), value: smat.service.optionSet("DyOptionFormat.Y") }
                ]
            });

            this.Y_format.smatComboBox({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: smat.service.optionSet("DyOptionText.Money"), value: "n0" }
                ]
            });

            this.title_position.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: smat.service.optionSet("DyOptionText.Top"),
                        value: "top"
                    },
                    {
                        text: smat.service.optionSet("DyOptionText.Bottom"),
                        value: "bottom"
                    }
                ]
            });

            this.legend_visible.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: smat.service.optionSet("DyOptionText.True"),
                        value: "true"
                    },
                    {
                        text: smat.service.optionSet("DyOptionText.False"),
                        value: "false"
                    }
                ]
            });

            this.legend_visible.ui().value("true");

            this._increasing.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: smat.service.optionSet("DyOptionText.True"),
                        value: "true"
                    },
                    {
                        text: smat.service.optionSet("DyOptionText.False"),
                        value: "false"
                    }
                ]
            });

            this._increasing.ui().value("false");
            

            this.legend_position.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: " ",
                        value: ""
                    },
                    {
                        text: smat.service.optionSet("DyOptionText.Top"),
                        value: "top"
                    },
                    {
                        text: smat.service.optionSet("DyOptionText.Bottom"),
                        value: "bottom"
                    },
                    {
                        text: smat.service.optionSet("DyOptionText.Left"),
                        value: "left"
                    },
                    {
                        text: smat.service.optionSet("DyOptionText.Right"),
                        value: "right"
                    }
                ]
            });

            this.setFormData("chartType", this._chart_type);
            this.setFormData("XField", this._X);
            this.setFormData("YField", this._Y);
            this.setFormData("seriesField", this._series);
            this.setFormData("title.text", this.title_text);
            this.setFormData("title.position", this.title_position);
            this.setFormData("legend.visible", this.legend_visible);
            this.setFormData("increasing", self._increasing);
            this.setFormData("legend.position", this.legend_position);
            this.setFormData("XFormat", this.X_format);
            this.setFormData("YFormat", this.Y_format);

            this.setFormData("seriesTitle", this._series_title);
            this.setFormData("XTitle", this.X_title);
            this.setFormData("YTitle", this.Y_title);

            this.btn_ok.bind('click', function () {
                smat.service.closeForm({
                    contentId: self.uuid
                });
            });

            this.btn_cancel.bind('click', function () {
                smat.service.closeForm({
                    contentId: self.uuid
                });
            });

            self.setTypeState();
            
        }, setTypeState: function () {
            if (this._chart_type.ui().value() == "pie") {
                this.x_row.hide();
            } else {
                this.x_row.show();
            }
        }, setViewInfo: function (viewname) {
            var self = this;

            if (this.config.page != undefined && this.config.page.editViewList != undefined) {
                var view = smat.service.getItemByKey(this.config.page.editViewList, "ViewName", viewname);
                if (view != undefined) {
                    self.doSetViewInfo(view);
                    return;
                }
            }

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getView,
                //async: false,
                params: {
                    ProjID: this.config.page.entity.ProjID,
                    EntityName: this.config.page.entity.EntityName,
                    ViewName: viewname
                },
                success: function (result) {

                    self.doSetViewInfo(result);
                }

            });

        }, setFormData: function (valueKey, node) {

            var valStr = smat.service.getJsonData(this.config.currentControl.config, valueKey);

            if (node.ui() != null) {
                node.ui().value(valStr)
            } else {
                node.val(valStr);
            }

        }, getFormData: function (valueKey, node) {

            var valStr = undefined;

            if (node.ui() != null) {
                valStr = node.ui().value()
            } else {
                valStr = node.val();
            }

            smat.service.setJsonData(this.config.currentControl.config, valueKey, valStr);

        }, doSetViewInfo: function (result) {

            var itemData = new Array();

            for (var key in result.ItemList) {
                var item = smat.globalObject.clone(result.ItemList[key]);
                item.text = item.ItemDesc;
                itemData.push(item);
            }

            this.view_item_tree.asmatTreeView({
                dataSource: itemData
            });

            this.treeview = this.view_item_tree.data("asmatTreeView");
            this.treeview.expand(this.view_item_tree.find('.s-item'));

            this.initDragItem(this.view_item_tree.find(".s-item:not([aria-expanded])"))
        }, initDragItem: function (dragItems) {

            var self = this;
            $.each(dragItems, function (n, value) {
                $(this).asmatDraggable({
                    //treeview: treeview,
                    hint: function (item) {

                        //alert(treeview.dataItem($(item)).text);
                        self.dragTarget = this;
                        self.dragModel = "move";
                        self.dragDataItem = self.treeview.dataItem($(item));
                        
                        var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;padding:10px 20px;'>" + self.dragDataItem.text + "</div>");

                        self.box.find('.data-row').append('<div class="grid-drop-hint-box" ></div>');


                        return hintElement;
                    },
                    dragstart: function (e) {

                    },
                    dragend: function (e) {
                        self.box.find(".grid-drop-hint-box").remove();
                    }
                });
            });

        }, initDataInput: function () {
            var self = this;

            self.box.find('.data-row').asmatDropTargetArea({
                filter: ".row",
                dragenter: function (e) {
                    if (self.dragModel != "move") {
                        e.preventDefault();
                        return;
                    }
                    self.dropTarget = $(e.dropTarget);

                    $(e.dropTarget).append('<div class="grid-drop-box" style="position: absolute;background-color: #19C6F9;opacity: 0.5;text-align: center;top: -1px;left: -1px;width: 100%;height: 100%;z-index: 1099; display: block;"><span style="position: absolute;top: 5px;display: block;left: 65px;color: #fff;font-size: 16px;font-weight: bold;">bind</span></div>');
                },
                dragleave: function (e) {

                    if (self.dragModel != "move") {
                        e.preventDefault();
                        return;
                    }

                    var target = $(e.dropTarget);
                    target.find(".grid-drop-box").remove();
                    self.dropTarget = undefined;
                },
                drop: function (e) {
                    if (self.dragModel != "move") {
                        e.preventDefault();
                        return;
                    }

                    var target = $(e.dropTarget);
                    if (target.find(".grid-drop-box").length == 0) return;
                    target.find(".grid-drop-box").remove();

                    target.find('input.s-value').val(self.dragDataItem.ItemName);
                    target.find('input.s-title').val(self.dragDataItem.ItemDesc);
                    //self.dragDataItem.Group = "GroupBy";
                   
                }
            });

        }, getDomStr: function () {

            return '<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 366px; min-width:800px;">'
+'    <div  style="margin: 0;padding: 0;height: 100%;position: relative; width:150px;float:left;">'
+ '        <div style="height:20px;">' + smat.service.optionSet("DyOptionText.Data") + '</div>'
+'        <div id="view_item_tree" style="height:290px;border: 1px solid #ccc;"></div>'
+'    </div>'
+'    <div  style="margin: 0;padding: 0;height: 100%;width:660px;float:left;">'
+ '        <div style="height:20px;"></div>'
+ '        <div >'
 + '                <div class="row" style="margin:0;"><div class=" form-group"><label class="control-label text-right" style="width:76px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Title") + '</label><input id="title_text" class="s-textbox " style="width:340px;"><label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Position") + '</label><input id="title_position" class="" style="width:108px;"></div></div>'
+ '         </div>'
+'        <div class="col-sm-12" style="margin: 0;padding: 0;height: 260px;">'
+'            <div id="view_item_grid" style="height:290px; margin:0px 0 0 10px; ">'
+ '                <div class="row" style="margin:0;"><div class=" form-group"><label class="control-label text-right" style="width:56px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Type") + '</label><input id="_chart_type" style="margin-left: 10px; width:180px;" /></div></div>'
+ '                <div id="x_row" class="row data-row" style="margin:2px 0;position: relative;"><div class=" form-group">'
+ '                <label class="control-label text-right" style="width:56px; margin-right:5px;display:none; ">' + smat.service.optionSet("DyOptionText.X-axis") + '</label><input id="_X" class="s-textbox s-value" readonly="true" style="margin-left: 10px;display:none; width:180px;" />'
+ '                <label class="control-label text-right" style="width:56px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.X-axis") + '</label><input id="X_title" class="s-textbox s-title" style="margin-left: 10px; width:180px;" />'
+ '                    <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Format") + '</label><input id="X_format" class="" style="width:140px;">'
+ '                    </div></div>'
+ '                <div class="row  data-row" style="margin:2px 0;position: relative;"><div class=" form-group">'
+ '                    <label class="control-label text-right" style="width:56px; margin-right:5px;display:none; ">' + smat.service.optionSet("DyOptionText.Number") + '</label><input id="_Y" class="s-textbox s-value"　readonly="true"  style="margin-left: 10px;display:none; width:180px;" />'
+ '                    <label class="control-label text-right" style="width:56px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Number") + '</label><input id="Y_title" class="s-textbox s-title" style="margin-left: 10px; width:180px;" />'
+ '                    <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Increasing") + '</label><input id="_increasing" class="" style="width:140px;">'
+ '                    <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Format") + '</label><input id="Y_format" class="" style="width:130px;">'
+ '                </div></div>'
+ '                <div class="row  data-row" style="margin:2px 0;position: relative;"><div class=" form-group">'
+ '                    <label class="control-label text-right" style="width:56px; margin-right:5px;display:none; ">シリーズ</label><input id="_series" class="s-textbox s-value" readonly="true" style="margin-left: 10px;display:none; width:180px;" />'
+ '                    <label class="control-label text-right" style="width:56px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Series") + '</label><input id="_series_title" class="s-textbox s-title" style="margin-left: 10px; width:180px;" />'
+ '                    <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Display") + '</label><input id="legend_visible" class="" style="width:140px;">'
+ '                    <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Position") + '</label><input id="legend_position" class="" style="width:130px;"/>'
+ '             </div></div>'
+'            </div>'
+'        </div>'
+'        <div class="col-sm-12" style="margin: 0;padding: 0;height: 210px; display:none;">'
+'            <div style="height:200px;border: 1px solid #ccc; margin:0px 0 0 10px; width:202px; float:left;"></div>'
+'            <div style="height:200px;border: 1px solid #ccc; margin:0px 0 0 10px; width:202px; float:left;"></div>'
+'            <div style="height:200px;border: 1px solid #ccc; margin:0px 0 0 10px; width:202px; float:left;"></div>'
+'        </div>'
+'        <div class="col-sm-12 text-right" style="margin: 0;padding: 0;height: 50px;">'
+ '            <button id="btn_ok" class="btn-primary s-button" style="">' + smat.service.optionSet("DyOptionText.Ok") + '</button>'
+ '            <button id="btn_cancel" class="btn-danger s-button" style="margin-right:10px;">' + smat.service.optionSet("DyOptionText.Cancel") + '</button>'
+ '        </div>'
+'    </div>'
+'</section>'
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.ChartData, smat.dynamics.Element);
})();