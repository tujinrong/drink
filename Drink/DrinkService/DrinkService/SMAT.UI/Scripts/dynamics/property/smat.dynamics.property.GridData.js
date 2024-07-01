
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  View 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.GridData = function (config) {
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

    smat.dynamics.property.GridData.prototype = {
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


            this._category = this.box.find('#_category');
            this._dataField = this.box.find('#_dataField');

            this._increasing = this.box.find('#_increasing');
           

            this._series_grid_box = this.box.find('#_series_grid_box');
            this._series_grid = this.box.find('#_series_grid');

            this.dataField_format = this.box.find('#dataField_format');
            this.dataField_title = this.box.find('#_dataField_title');
            
            this.category_format = this.box.find('#category_format');
            this.category_width = this.box.find('#category_width');
            this.category_title = this.box.find('#_category_title');
            
            
            smat.service.openForm({
                //m_opacity: 0,
                contentDom: this.box,
                width: "840px",
                title: self.config.currentDataItem.cmt,
                afterClose: function (result) {

                    self.getFormData("category", self._category);
                    _category_title
                    self.getFormData("dataField", self._dataField);
                    self.getFormData("increasing", self._increasing);
                    self.getFormData("dataField_format", self.dataField_format);
                    self.getFormData("dataField_title", self.dataField_title);
                    self.getFormData("category_format", self.category_format);
                    self.getFormData("category_width", self.category_width);
                    self.getFormData("category_title", self.category_title);
                    self.getFormData("series", self._series_grid);

                    self.config.currentControl.refresh();
                }
            });

            //viewItems
            this.setViewInfo(this.config.currentControl.config.view);

            this.initDataInput();
            this.initSeriesGrid();


            this.category_format.smatComboBox({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: smat.service.optionSet("DyOptionText.YMD"), value: smat.service.optionSet("DyOptionFormat.YMD") },
                    { text: smat.service.optionSet("DyOptionText.YM"), value: smat.service.optionSet("DyOptionFormat.YM") },
                    { text: smat.service.optionSet("DyOptionText.Y"), value: smat.service.optionSet("DyOptionFormat.Y") }
                ]
            });

            this.dataField_format.smatComboBox({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: smat.service.optionSet("DyOptionText.Money"), value: "n0" }
                ]
            });

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
            
            self.setFormData("category", self._category);
            self.setFormData("dataField", self._dataField);
            self.setFormData("increasing", self._increasing);
            self.setFormData("dataField_format", self.dataField_format);
            self.setFormData("dataField_title", self.dataField_title);
            self.setFormData("category_format", self.category_format);
            self.setFormData("category_width", self.category_width);
            self.setFormData("category_title", self.category_title);
            self.setFormData("series", self._series_grid);

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
                
                if (node.ui() instanceof smat.Grid) {
                    if (valStr != null && valStr != undefined && typeof (valStr) == 'object') {
                        for (var k in valStr) {
                            if (valStr[k]["seriesTitle"] == undefined) valStr[k]["seriesTitle"] = valStr[k]["seriesField"];
                        }
                        node.ui().setDataSource(valStr);
                    }
                } else {
                    node.ui().value(valStr);
                }
            } else {
                node.val(valStr);
            }

        }, getFormData: function (valueKey, node) {

            var valStr = undefined;

            if (node.ui() != null) {
                if (node.ui() instanceof smat.Grid) {
                    valStr = node.ui().config.dataSource;
                } else {
                    valStr = node.ui().value();
                }
                
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
                        self.dragModel = "new";
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
                    if (self.dragModel != "new") {
                        e.preventDefault();
                        return;
                    }
                    self.dropTarget = $(e.dropTarget);

                    $(e.dropTarget).append('<div class="grid-drop-box" style="position: absolute;background-color: #19C6F9;opacity: 0.5;text-align: center;top: -1px;left: -1px;width: 100%;height: 100%;z-index: 1099; display: block;"><span style="position: absolute;top: 5px;display: block;left: 65px;color: #fff;font-size: 16px;font-weight: bold;">bind</span></div>');
                },
                dragleave: function (e) {

                    if (self.dragModel != "new") {
                        e.preventDefault();
                        return;
                    }

                    var target = $(e.dropTarget);
                    target.find(".grid-drop-box").remove();
                    self.dropTarget = undefined;
                },
                drop: function (e) {
                    if (self.dragModel != "new") {
                        e.preventDefault();
                        return;
                    }

                    var target = $(e.dropTarget);
                    if (target.find(".grid-drop-box").length == 0) return;
                    target.find(".grid-drop-box").remove();

                    

                    if (target.find('.s-grid').length > 0) {
                        var grid = target.find('.s-grid').ui();
                        var newItem =
                           {
                               seriesField: self.dragDataItem.ItemName,
                               seriesTitle: self.dragDataItem.ItemDesc,
                               format:"",
                               width:""
                           };

                        grid.addRow(newItem);
                    } else {
                        target.find('input.s-value').val(self.dragDataItem.ItemName);
                        target.find('input.s-title').val(self.dragDataItem.ItemDesc);
                    }
                    self.dragModel = null;
                    //self.dragDataItem.Group = "GroupBy";
                   
                }
            });

        }, initSeriesGrid: function () {
            var self = this;
            this._series_grid.smatGrid({
                columns: [
                    //{
                    //    field: "seriesField",
                    //    title:  smat.service.optionSet("DyOptionText.Field")
                    //},
                    {
                        field: "seriesTitle",
                        title: smat.service.optionSet("DyOptionText.Title"),
                        editable: "true"
                    },
                    {
                        field: "seriesFormat",
                        title: smat.service.optionSet("DyOptionText.Format"),
                        width: "160px",
                        editable: "true"
                    },
                    {
                        field: "width",
                        title: smat.service.optionSet("DyOptionText.Width"),
                        width: "100px",
                        editable: "true"
                    },
                    {
                        field: "SeriesField",
                        title: "　",
                        template: function (dataItem) {
                            return '<button class="btn-danger s-button" style="padding: 3px 6px;min-width: 20px;">X</button>';
                        },
                        width: "36px"
                    }
                ],
                dataBound: function (e) {
                    //e.sender.thead.find('tr').remove();
                    e.sender.table.css('table-layout', 'fixed');

                    var gridOrder = e.sender;
                    var trs = e.sender.tbody.children('tr');
                    if (trs.length > 0) trs.eq(0).children('td').css('border-top', 'none');

                    if (trs.length > 1) {
                        var grid = e.sender;
                        grid.table.asmatSortable({
                            filter: ">tbody >tr",
                            hint: $.noop,
                            cursor: "move",
                            ignore: "input",
                            placeholder: function (element) {
                                return element.clone().addClass("s-state-hover").css("opacity", 0.45);
                            },
                            container: "#_series_grid tbody",
                            change: function (e) {
                                var oldIndex = e.oldIndex,
                                    newIndex = e.newIndex,
                                    data = grid.dataSource.data(),
                                    dataItem = grid.dataSource.getByUid(e.item.data("uid")),
                                    dataSourceItem = self._series_grid.ui().config.dataSource[e.oldIndex];

                                grid.dataSource.remove(dataItem);
                                grid.dataSource.insert(newIndex, dataItem);

                                self._series_grid.ui().config.dataSource.splice(e.oldIndex, 1);
                                self._series_grid.ui().config.dataSource.splice(newIndex, 0, dataSourceItem);
                            }
                        });
                    }

                    $.each(trs, function (n, value) {

                        $(this).find('button').bind('click', function () {
                            var trInside = $(this).closest('tr');
                            var rowKey = trInside.index();
                            var dataItemInside = gridOrder.dataItem(trInside);

                            //update OrderBy
                            self._series_grid.ui().delRow($(this).closest('tr').index());
                        });
                    });

                }
            });

        }, getDomStr: function () {

            return '<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 366px; min-width:800px;">'
+'    <div style="margin: 0;padding: 0;height: 100%;position: relative; width:150px;float:left;">'
+ '        <div style="height:20px;">' + smat.service.optionSet("DyOptionText.Data") + '</div>'
+'        <div id="view_item_tree" style="height:290px;border: 1px solid #ccc;"></div>'
+'    </div>'
+'    <div style="margin: 0;padding: 0;height: 100%;width:660px;float:left;">'
+'        <div style="height:20px;"></div>'
+'        <div class="col-sm-12" style="margin: 0;padding: 0;height: 260px;">'
+'            <div id="view_item_grid" style="height:290px; margin:0px 0 0 10px; ">'
+'                <div id="x_row" class="row data-row" style="margin:2px 0;position: relative;">'
+'                    <div class=" form-group">'
+ '                        <label class="control-label text-right" style="width:56px; margin-right:5px;display:none; ">' + smat.service.optionSet("DyOptionText.Column") + '</label><input id="_category" readonly="true" class="s-textbox s-value" style="margin-left: 10px; display:none; width:180px;" />'
+ '                        <label class="control-label text-right" style="width:56px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Column") + '</label><input id="_category_title" class="s-textbox s-title" style="margin-left: 10px; width:180px;" />'
+ '                        <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Format") + '</label><input id="category_format" class="" style="width:140px;">'
+ '                        <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Width") + '</label><input id="category_width" class="s-textbox" style="width:130px;">'

+'                    </div>'
+'                </div>'
+'                <div class="row  data-row" style="margin:2px 0;position: relative;">'
+'                    <div class=" form-group">'
+ '                        <label class="control-label text-right" style="width:56px; margin-right:5px;display:none; ">' + smat.service.optionSet("DyOptionText.Number") + '</label><input id="_dataField" readonly="true" class="s-textbox s-value" style="margin-left: 10px;display:none; width:180px;" />'
+ '                        <label class="control-label text-right" style="width:56px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Number") + '</label><input id="_dataField_title" class="s-textbox s-title" style="margin-left: 10px; width:180px;" />'
+ '                        <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Increasing") + '</label><input id="_increasing" class="" style="width:140px;">'
+ '                        <label class="control-label text-right" style="width:50px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Format") + '</label><input id="dataField_format" class="" style="width:130px;">'
+'                    </div>'
+'                </div>'
+'                <div class="row  data-row" style="margin:2px 0;position: relative;">'
+'                    <div class=" form-group">'
+ '                        <label class="control-label text-right" style="width:56px; margin-right:5px;">' + smat.service.optionSet("DyOptionText.Row") + '</label>'
+'                        <div id="_series_grid_box" style="height:180px;border: 1px solid #ccc; margin:0px 0 0 10px; width:570px;position: relative;margin-left: 70px;" class="grid-drop-order">'
+'                            <div id="_series_grid" style="height:180px;border: none; overflow: auto;overflow-y: auto; width:570px;"></div>'
+'                        </div>'
+'                    </div>'
+'                </div>'
+'            </div>'
+'            <div class="col-sm-12" style="margin: 0;padding: 0;height: 210px; display:none;">'
+'                <div style="height:200px;border: 1px solid #ccc; margin:0px 0 0 10px; width:202px; float:left;"></div>'
+'                <div style="height:200px;border: 1px solid #ccc; margin:0px 0 0 10px; width:202px; float:left;"></div>'
+'                <div style="height:200px;border: 1px solid #ccc; margin:0px 0 0 10px; width:202px; float:left;"></div>'
+'            </div>'
+'            <div class="col-sm-12 text-right" style="margin: 0;padding: 0;height: 50px;">'
+ '                <button id="btn_ok" class="btn-primary s-button" style="">' + smat.service.optionSet("DyOptionText.Ok") + '</button>'
+ '                <button id="btn_cancel" class="btn-danger s-button" style="margin-right:10px;">' + smat.service.optionSet("DyOptionText.Cancel") + '</button>'
+'            </div>'
+'        </div>'
+ '    </div>'
+'</section>'
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.GridData, smat.dynamics.Element);
})();