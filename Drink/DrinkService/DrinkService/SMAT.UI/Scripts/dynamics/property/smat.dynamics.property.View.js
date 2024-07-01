
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  View 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.View = function (config) {
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

    smat.dynamics.property.View.prototype = {
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

            var box = $(this.getDomStr());

            this.view_list = box.find('#view_list');
            this.entity_list = box.find('#entity_list');
            this.content_type = box.find('#content_type');
            this.filter_box = box.find('#filter_box');
            
            this.btn_add = box.find('#btn_add');
            this.btn_copy = box.find('#btn_copy');
            this.btn_ok = box.find('#btn_ok');
            this.btn_preview = box.find('#btn_preview');
            this.btn_cancel = box.find('#btn_cancel');
            this.view_field_tree = box.find('#view_field_tree');
            this.view_item_grid = box.find('#view_item_grid');
            this.view_item_grid_box = box.find('#view_item_grid_box');
            this.view_group_grid = box.find('#view_group_grid');
            this.view_group_grid_box = box.find('#view_group_grid_box');
            this.view_orderby_grid = box.find('#view_orderby_grid');
            this.view_orderby_grid_box = box.find('#view_orderby_grid_box');
            this.view_item_btn_box = box.find('#view_item_btn_box');

            this.btnUp = $('<button class="btn-primary " style="padding: 8px 5px;margin-bottom: 5px;">↑</button>').appendTo(this.view_item_btn_box);
            this.btnDown = $('<button class="btn-primary " style="padding: 8px 5px;">↓</button>').appendTo(this.view_item_btn_box);


            this._ItemName = box.find('#_ItemName');
            this._ItemDesc = box.find('#_ItemDesc');
            this._Path = box.find('#_Path');
            this._ItemSql = box.find('#_ItemSql');
            this._ItemEntityName = box.find('#_ItemEntityName');
            this._Format = box.find('#_Format');
            this._Group = box.find('#_Group');
            this._FilterSql = box.find('#_FilterSql');
            this._HavingFilterSql = box.find('#_HavingFilterSql');

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: box,
                width: "840px",
                title: self.config.currentDataItem.id,
                afterClose: function (result) {

                    if (!self.curView) {
                        return;
                    }

                    var value = self.curView.ViewName;
                    if (self.config.currentCell) self.config.currentCell.children('input').val(value);
                    self.config.currentControl.propertyChange(self.config.currentDataItem, value, self.config.valueConfig, self.curView);
                    if (self.config.currentCell) self.config.currentCell.children('input').focus();

                    var en = self.entity_list.ui().value();
                    self.config.currentControl.config.viewEntityName = en;
                }
            });

            this.content_type.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "data view", value: "view" },
                    { text: "filter", value: "filter" }
                ],
                change: function (e) {
                    if (self.content_type.ui().value() == "view") {
                        $("#view_panel").show();
                        $("#filter_panel").hide();
                    } else {
                        $("#view_panel").hide();
                        $("#filter_panel").show();
                    }
                }
            });

            this.entity_list.smatDropDownList({
                dataSource: [],
                dataValueField: "EntityName",
                dataTextField: "EntityDesc",
                change: function (e) {
                    var en = self.entity_list.ui().value();
                    if (en == self.config.page.config.entityName) {
                        var views = new Array()

                        if (self.config.page != undefined && self.config.page.editViewList != undefined) {
                            for (var vKey in self.config.page.editViewList) {
                                if (self.config.page.editViewList[vKey].EntityName == en) {
                                    views.push(self.config.page.editViewList[vKey]);
                                }
                            }
                        }

                        for (var key in self.config.page.entity.ViewList) {
                            if ($.Enumerable.From(views).Any("$.ViewName == '" + self.config.page.entity.ViewList[key].ViewName + "'") == false) {
                                views.push(self.config.page.entity.ViewList[key]);
                            }
                        }

                        self.view_list.ui().setDataSource(views);

                        var fieldData = smat.dynamics.entityFieldTree(self.config.page.config.designer.data, "");
                        self.treeview.setDataSource([fieldData]);
                        self.treeview.expand(self.view_field_tree.find('.s-item:first'));
                        self.initDragItem(self.view_field_tree.find(".s-item"));

                        self.filterTool.entity = self.config.page.entity;
                        self.filterTool.editEntity = self.config.page.entity;
                        self.filterTool.initEditItem();
                    } else {
                        //===================entity=======================
                        smat.service.loadJosnData({
                            url: smat.dynamics.commonURL.getEntity,
                            async: false,
                            params: {
                                ProjID: self.config.page.config.projID,
                                EntityName: en
                            },
                            success: function (result) {
                                var views = new Array();

                                if (self.config.page != undefined && self.config.page.editViewList != undefined) {
                                    for (var vKey in self.config.page.editViewList) {
                                        if (self.config.page.editViewList[vKey].EntityName == en) {
                                            views.push(self.config.page.editViewList[vKey]);
                                        }
                                    }
                                }

                                for (var key in result.ViewList) {
                                    if ($.Enumerable.From(views).Any("$.ViewName == '" + result.ViewList[key].ViewName + "'") == false) {
                                        views.push(result.ViewList[key]);
                                    }
                                }

                                self.view_list.ui().setDataSource(views);

                                var fieldData = smat.dynamics.entityFieldTree(result, "");
                                self.treeview.setDataSource([fieldData]);
                                self.treeview.expand(self.view_field_tree.find('.s-item:first'));
                                self.initDragItem(self.view_field_tree.find(".s-item"));

                                //self.filterTool.entity = result;
                                for (var key in result.FilterControlList) {
                                    if ($.Enumerable.From(self.config.page.entity.FilterControlList).Any("$.FilterControlName == '" + result.FilterControlList[key].FilterControlName + "' && $.EntityName =='" + result.FilterControlList[key].EntityName + "'") == false) {
                                        self.config.page.entity.FilterControlList.push(result.FilterControlList[key]);
                                    }
                                }
                                for (var key in result.FilterList) {
                                    if ($.Enumerable.From(self.config.page.entity.FilterList).Any("$.FilterName == '" + result.FilterList[key].FilterControlName + "' && $.EntityName =='" + result.FilterList[key].EntityName + "'") == false) {
                                        self.config.page.entity.FilterList.push(result.FilterList[key]);
                                    }
                                }
                                self.filterTool.editEntity = result;
                                self.filterTool.initEditItem();
                            }
                        });
                    }
                    self.setViewInfo();
                }
            });

            this.view_list.smatDropDownList({
                dataTextField: "ViewName",
                dataValueField: "ViewName",
                change: function () {
                    self.setViewInfo(self.view_list.ui().value());
                }
            });

            this.view_field_tree.asmatTreeView({
                dataSource: [],
                expand: function (e) {
                    var dataParentItem = self.treeview.dataItem($(e.node));
                    if (dataParentItem.items != null && dataParentItem.items[0].lazyLoad == true) {
                        var dataItem = dataParentItem.items[0];
                        smat.service.loadJosnData({
                            url: smat.dynamics.commonURL.getEntity,
                            params: {
                                ProjID: self.config.page.config.projID,
                                EntityName: dataItem.Entity
                            },
                            success: function (result) {
                                var rData = smat.dynamics.entityFieldTree(result,
                                    dataItem.reType,
                                    undefined,
                                    dataItem.mainEntity,
                                    dataItem.RelaName,
                                    dataItem.RelaDesc,
                                    dataItem.Path,
                                    dataParentItem.Alias);

                                if (rData != null) {
                                    var tempNode = $(e.node).find('.s-item');
                                    self.treeview.append(rData.items, $(e.node));
                                    //appand

                                    self.treeview.remove(tempNode);
                                    self.initDragItem($(e.node).find('.s-item'));
                                }
                            }
                        });

                    }
                }
            });

            this.treeview = this.view_field_tree.data("asmatTreeView");
            //this.treeview.expand(this.view_field_tree.find('.s-item:first'));

            //this.initDragItem(this.view_field_tree.find(".s-item"))

            this.filterTool = new smat.dynamics.tool.Filter({
                page: this.config.page,
                name: smat.service.optionSet("DyOptionText.Filter")
            });
            this.filterTool.config.box = this.filter_box;
            this.filterTool.toolBuild();


            //===================entity=======================
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntityList,
                async: false,
                params: {
                    ProjID: self.config.page.config.projID
                },
                success: function (result) {
                    var en = self.config.page.config.entityName;
                    if (self.config.currentControl.config.viewEntityName) en = self.config.currentControl.config.viewEntityName;
                    self.entity_list.ui().uiControl.setDataSource(result);
                    self.entity_list.ui().value(en);
                    self.entity_list.ui().trigger(smat.event.CHANGE, {});

                }
            });

            //smat.service.loadJosnData({
            //    url: smat.dynamics.commonURL.getViewList,
            //    params: {
            //        ProjID: this.config.page.entity.ProjID,
            //        EntityName: this.config.page.entity.EntityName
            //    },
            //    success: function (result) {

            //        var views = new Array()

            //        if (self.config.page != undefined && self.config.page.editViewList != undefined) {
            //            for (var vKey in self.config.page.editViewList) {
            //                views.push(self.config.page.editViewList[vKey]);
            //            }
            //        }

            //        for (var key in result) {
            //            if ($.Enumerable.From(views).Any("$.ViewName == '" + result[key].ViewName + "'") == false) {
            //                views.push(result[key]);
            //            }
            //        }

            //        self.view_list.ui().setDataSource(views);
            //        self.view_list.ui().value(self.config.currentDataItem.value);

            //    }
            //});

            //var views = new Array()

            //if (self.config.page != undefined && self.config.page.editViewList != undefined) {
            //    for (var vKey in self.config.page.editViewList) {
            //        views.push(self.config.page.editViewList[vKey]);
            //    }
            //}

            //for (var key in self.config.page.entity.ViewList) {
            //    if ($.Enumerable.From(views).Any("$.ViewName == '" + self.config.page.entity.ViewList[key].ViewName + "'") == false) {
            //        views.push(self.config.page.entity.ViewList[key]);
            //    }
            //}

            //self.view_list.ui().setDataSource(views);

            self.view_list.ui().value(self.config.currentDataItem.value);

            this._Group.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: " ", value: "", },
                    { text: "GroupBy", value: "GroupBy", },
                    { text: "Sum", value: "Sum", },
                    { text: "Count", value: "Count", },
                    { text: "Max", value: "Max", },
                    { text: "Min", value: "Min", },
                    { text: "Average", value: "Average", }
                ],
                change: function (e) {
                    self.updateItemField("Group", self._Group.ui().value());
                    self.checkGroupInfo();
                }
            });

            this.btnUp.smatButton({
                click: function () {
                    self.moveUpDown(-1);
                }
            });


            this.btnDown.smatButton({
                click: function () {
                    self.moveUpDown(1);
                }
            });


            //================================

            //==========item group=======
            this.initItemGrid();
            //===========================

            //==========group=======
            this.initGroupGrid();
            //===========================

            //==========order=======
            this.initOrderGrid();
            //===========================

            this.initInputEvent();

            this.btn_add.bind('click', function () {
                self.newView();
            });

            this.btn_copy.bind('click', function () {
                self.copyView();
            });


            this.btn_ok.bind('click', function () {
                self.saveView();
            });
            this.btn_cancel.bind('click', function () {
                smat.service.closeForm({
                    contentId: self.uuid
                });
            });

            this.btn_preview.bind('click', function () {
                self.previewView();
            });

            this.setViewInfo(this.config.currentDataItem.value);
        }, setViewInfo: function (viewname) {
            var self = this;

            if (!viewname) {
                viewname = self.view_list.ui().value();
            }
            if (!viewname) {
                self.view_list.ui().value("");
                if (self._Format.ui()) {
                    self.view_item_grid.ui().setDataSource([]);
                    self.view_orderby_grid.ui().setDataSource([]);
                    self.view_group_grid.ui().setDataSource([]);
                    self.clearItemInfo();
                }
                return;
            }

            if (this.config.page != undefined && this.config.page.editViewList != undefined) {
                var view = smat.service.getItemByKey(this.config.page.editViewList, "ViewName", viewname);
                if (view != undefined) {
                    self.doSetViewInfo(view);
                    return;
                }
            }

            //smat.service.loadJosnData({
            //    url: smat.dynamics.commonURL.getView,
            //    //async: false,
            //    params: {
            //        ProjID: this.config.page.entity.ProjID,
            //        EntityName: this.config.page.entity.EntityName,
            //        ViewName: viewname
            //    },
            //    success: function (result) {
            //        if (self.config.page != undefined && self.config.page.editViewList != undefined) {
            //            if ($.Enumerable.From(self.config.page.editViewList).Any("$.ViewName == '" + viewname + "'") == false) {
            //                self.config.page.editViewList.push(result)
            //            }
            //        }

            //        self.doSetViewInfo(result);
            //    }

            //});
            //var result = $.Enumerable.From(self.config.page.entity.ViewList).First("$.ViewName == '" + viewname + "'");
            var result = $.Enumerable.From(self.view_list.ui().config.dataSource).First("$.ViewName == '" + viewname + "'");
            
            if (self.config.page != undefined && self.config.page.editViewList != undefined) {
                if ($.Enumerable.From(self.config.page.editViewList).Any("$.ViewName == '" + viewname + "'") == false) {
                    self.config.page.editViewList.push(result)
                }
            }
            self.doSetViewInfo(result);

        }, doSetViewInfo: function (result) {
            var self = this;

            if (!self.view_item_grid.ui()) return;

            self.curView = result;
            self.view_item_grid.ui().setDataSource(result.ItemList);

            var orderedItems = $.Enumerable.From(result.ItemList).Where("$.OrderBy != '' && $.OrderBy != null")
             .OrderBy("$.Seq")
            .ToArray();

            self.view_orderby_grid.ui().setDataSource(orderedItems);

            var groupedItems = $.Enumerable.From(result.ItemList).Where("$.Group == 'GroupBy'")
            .OrderBy("$.Seq")
           .ToArray();

            self.view_group_grid.ui().setDataSource(groupedItems);

            self.curRowIndex = -1;
            self.selectItemRow(0)

        }, checkItemName: function () {

            smat.service.clearErrorBorder(this._ItemName);

            var newName = this._ItemName.val();
            var oldName = this._ItemName.attr('oldValue');

            if (newName == oldName) {
                return true;
            }

            var dataSource = this.view_item_grid.ui().config.dataSource;

            if ($.Enumerable.From(dataSource).Any("$.ItemName == '" + newName + "' && $.ItemName !='" + oldName + "'")) {
                smat.service.notice({ msg: "ItemName Has been used", type: "error" });
                smat.service.addErrorBorder(this._ItemName);
                this._ItemName.focus()
                return false;
            }

            return true;

        }, updateItemField: function (fieldName, value) {

            if (this.curDataItem == null) return;

            this.curDataItem[fieldName] = value;
            this.curDataSourceItem[fieldName] = value;

            //Gropu
            if (fieldName == "Group") {
                //need add to group grid?
                var groupedItems = this.view_group_grid.ui().config.dataSource;
                if (value == "GroupBy") {
                    if ($.Enumerable.From(groupedItems).Any("$.ItemName == '" + this.curDataItem.ItemName + "'") == false) {
                        this.view_group_grid.ui().addRow(this.curDataItem);
                    }
                } else {
                    this.delGroupRowByName(this.curDataItem.ItemName);
                }

                var tr = this.view_item_grid.find('tbody').children('tr:eq(' + this.curRowIndex + ')');
                tr.children('td:eq(1)').html(this.templateGroup(this.curDataItem));
                this.checkGroupInfo();
            }

            //ItemName
            if (fieldName == "ItemDesc") {

                var tr = this.view_item_grid.find('tbody').children('tr:eq(' + this.curRowIndex + ')');
                tr.children('td:eq(0)').html(this.templateItemName(this.curDataItem));
            }

        }, saveView: function () {

            if (this.checkItemName() == false) {
                return;
            }

            if (this.checkGroupInfo() == false) {
                smat.service.notice({ msg: "group error", type: "error" })
                return;
            }

            var self = this;
            //var view = this.curView;
            //view.ItemList = this.view_item_grid.ui().config.dataSource;
            //smat.service.loadJosnData({
            //    url: smat.dynamics.commonURL.saveView,
            //    params: { View: view },
            //    success: function (result) {
            //        smat.service.closeForm({
            //            contentId: self.uuid
            //        });
            //    }

            //});

            smat.service.closeForm({
                contentId: self.uuid
            });

        }, initDragItem: function (dragItems) {

            var self = this;
            $.each(dragItems, function (n, value) {

                if ($(this).children('ul').length > 0) {
                    return;
                }

                $(this).asmatDraggable({
                    //treeview: treeview,
                    hint: function (item) {

                        //alert(treeview.dataItem($(item)).text);
                        self.dragTarget = this;
                        self.dragModel = "new";
                        self.dragDataItem = self.treeview.dataItem($(item));

                        var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;padding:10px 20px;'>" + self.dragDataItem.text + "</div>");

                        self.view_item_grid_box.append('<div class="grid-drop-hint-box" ></div>');


                        return hintElement;
                    },
                    dragstart: function (e) {

                    },
                    dragend: function (e) {
                        self.view_item_grid_box.find(".grid-drop-hint-box").remove();
                    }
                });
            });

        }, initInputEvent: function () {
            var self = this;
            //itemName
            //this._ItemName = box.find('#_ItemName');
            //this._ItemDesc = box.find('#_ItemDesc');
            //this._Path = box.find('#_Path');
            //this._ItemSql = box.find('#_ItemSql');
            //this._ItemEntityName = box.find('#_ItemEntityName');
            //this._Format = box.find('#_Format');
            //this._Group = box.find('#_Group');

            this._ItemName.bind('blur', function (e) {
                smat.service.clearErrorBorder($(this));
                if (self.checkItemName() == false) {
                    e.preventDefault();
                    return false;
                } else {
                    self.updateItemField("ItemName", $(this).val());
                    $(this).attr('oldValue', $(this).val());
                }
            });

            this._ItemDesc.bind('blur', function (e) {
                self.updateItemField("ItemDesc", $(this).val());
            });

            this._Path.bind('blur', function (e) {
                self.updateItemField("Path", $(this).val());
            });

            this._ItemSql.bind('blur', function (e) {
                self.updateItemField("ItemSql", $(this).val());
            });

            this._ItemEntityName.bind('blur', function (e) {
                self.updateItemField("ItemEntityName", $(this).val());
            });

            //this._Format.bind('blur', function (e) {
            //    self.updateItemField("Format", $(this).val());
            //});

            this._Format.smatComboBox({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "年月日", value: "=Date(YMD)" },
                    { text: "年月", value: "=Date(YM)" },
                    { text: "年", value: "=Date(Year)" }
                ],
                change: function (e) {
                    self.updateItemField("Format", self._Format.ui().value());
                }
            });

            this._FilterSql.bind('blur', function (e) {
                var val = self._FilterSql.val();
                if (val) {
                    var en = self.curView.EntityName;
                    if (self.config.currentControl.config.viewEntityName) en = self.config.currentControl.config.viewEntityName;

                    if (!self.filterItem) {
                        self.filterItem = {
                            ProjID: self.config.page.config.projID,
                            EntityName: en,
                            FilterName: smat.service.uuid(),
                            Path: self._Path.val(),
                            ItemEntityAliasName: self.curDataItem.EntityAlias,
                            FilterCategory: "viewItem",
                            IsHaving: false
                        };

                        self.config.page.entity.FilterList.push(self.filterItem);

                        self.config.page.entity.FilterControlList.push({
                            ProjID: self.config.page.config.projID,
                            EntityName: en,
                            FilterControlName: self.filterItem.FilterName,
                            FilterNames: self.filterItem.FilterName
                        });

                        self.curView.ViewFilterList.push({
                            ProjID: self.curView.ProjID,
                            EntityName: self.curView.EntityName,
                            ViewName: self.curView.ViewName,
                            FilterControlName: self.filterItem.FilterName,
                            Seq: self.curView.ViewFilterList.length + 1,
                        });
                    }

                    self.filterItem.FilterSql = val;
                    self.filterItem.FilterDesc = "ViewItemFilter:" + self.curDataItem.ItemName;


                } else {
                    if (self.filterItem) {
                        smat.service.delItemByKey(self.config.page.entity.FilterList, "FilterName", self.filterItem.FilterName);
                        smat.service.delItemByKey(self.config.page.entity.FilterControlList, "FilterControlName", self.filterItem.FilterName);
                        smat.service.delItemByKey(self.curView.ViewFilterList, "FilterControlName", self.filterItem.FilterName);
                    }
                }
                
            });

            this._HavingFilterSql.bind('blur', function (e) {
                var val = self._HavingFilterSql.val();
                if (val) {
                    var en = self.curView.EntityName;

                    if (!self.havingFilterItem) {
                        self.havingFilterItem = {
                            ProjID: self.config.page.config.projID,
                            EntityName: en,
                            FilterName: smat.service.uuid(),
                            Path: self._Path.val(),
                            ItemEntityAliasName: self.curDataItem.EntityAlias,
                            FilterCategory: "viewItem",
                            IsHaving: true
                        };

                        self.config.page.entity.FilterList.push(self.havingFilterItem);

                        self.config.page.entity.FilterControlList.push({
                            ProjID: self.config.page.config.projID,
                            EntityName: en,
                            FilterControlName: self.havingFilterItem.FilterName,
                            FilterNames: self.havingFilterItem.FilterName
                        });

                        self.curView.ViewFilterList.push({
                            ProjID: self.curView.ProjID,
                            EntityName: self.curView.EntityName,
                            ViewName: self.curView.ViewName,
                            FilterControlName: self.havingFilterItem.FilterName,
                            Seq: self.curView.ViewFilterList.length + 1,
                        });
                    }

                    self.havingFilterItem.FilterSql = val;
                    self.havingFilterItem.FilterDesc = "ViewItemFilter:" + self.curDataItem.ItemName;


                } else {
                    if (self.havingFilterItem) {
                        smat.service.delItemByKey(self.config.page.entity.FilterList, "FilterName", self.havingFilterItem.FilterName);
                        smat.service.delItemByKey(self.config.page.entity.FilterControlList, "FilterControlName", self.havingFilterItem.FilterName);
                        smat.service.delItemByKey(self.curView.ViewFilterList, "FilterControlName", self.havingFilterItem.FilterName);
                    }
                }
            });

        }, templateItemName: function (dataItem) {
            return '<span key="' + dataItem.ItemName + '">' + dataItem.ItemDesc + '</span>';
        }, templateGroup: function (dataItem) {
            if (dataItem.Group == null || dataItem.Group == "" || dataItem.Group == undefined) {
                return "";
            }

            var gName = dataItem.Group;
            var bgColor = "rgb(16, 196, 178)";

            if (dataItem.Group == "GroupBy") {
                bgColor = "rgb(54, 139, 234)";
                gName = "Group";
            }
            return '<span class="design-tag" style="background-color:' + bgColor + ';border-color:' + bgColor + '">' + gName + '</span>';
        }, delGroupRowByName: function (ItemName) {
            var nameSpan = this.view_group_grid.find("span[key='" + ItemName + "']");
            if (nameSpan.length > 0) {
                this.view_group_grid.ui().delRow(nameSpan.closest('tr').index());
            }
        }, delOrderRowByName: function (ItemName) {
            var nameSpan = this.view_orderby_grid.find("span[key='" + ItemName + "']");
            if (nameSpan.length > 0) {
                this.view_orderby_grid.ui().delRow(nameSpan.closest('tr').index());
            }
        }, checkGroupInfo: function () {
            var self = this;
            var ok = true;
            this.view_item_grid.find('.design-warning').removeClass('design-warning');

            var trs = this.view_item_grid.find('tr').not(".s-empty-row");

            if (this.view_group_grid.find('tr').not(".s-empty-row").length == 0) {
                $.each(trs, function () {
                    var dataItem = self.view_item_grid.ui().uiControl.dataItem($(this));
                    if (dataItem.Group != null && dataItem.Group != "" && dataItem.Group != undefined && dataItem.Group != "GroupBy") {
                        $(this).addClass('design-warning');
                        ok = false;
                    }
                });
            } else {
                $.each(trs, function () {
                    var dataItem = self.view_item_grid.ui().uiControl.dataItem($(this));
                    if (dataItem.Group == null || dataItem.Group == "" || dataItem.Group == undefined) {
                        $(this).addClass('design-warning');
                        ok = false;
                    }
                });
            }
            return ok;
        }, initItemGrid: function () {
            var self = this;
            this.view_item_grid.smatGrid({
                columns: [
                    {
                        field: "ItemName",
                        title: "ItemName",
                        template: self.templateItemName
                    },
                    {
                        field: "ItemName",
                        title: "1",
                        width: "66px",
                        attributes: {
                            "class": "text-center"
                        },
                        template: self.templateGroup
                    },
                    {
                        field: "ItemName",
                        title: "2",
                        template: function (dataItem) {
                            return '<button class="btn-danger s-button" style="padding: 3px 6px;min-width: 20px;">X</button>';
                        },
                        width: "36px"
                    }
                ],
                scrollable: true,
                dataBound: function (e) {
                    e.sender.thead.find('tr').remove();
                    e.sender.table.css('table-layout', 'fixed');

                    self.view_item_grid.find('.s-grid-content').css('overflow-y', 'auto');
                    self.view_item_grid.find('.s-grid-content').height(self.view_item_grid.find('.s-grid-content').height() + 36);

                    var grid = e.sender;
                    var trs = e.sender.tbody.children('tr');
                    if (trs.length > 0) trs.eq(0).children('td').css('border-top', 'none');
                    $.each(trs, function (n, value) {
                        $(this).bind('click', function () {
                            var rowKey = $(this).index();

                            //select row to edit item info
                            self.selectItemRow(rowKey);
                        });
                        $(this).find('button').bind('click', function () {
                            var trInside = $(this).closest('tr');
                            var rowKey = trInside.index();
                            var dataItemInside = grid.dataItem(trInside);
                            self.view_item_grid.ui().delRow(rowKey);
                            self.delGroupRowByName(dataItemInside.ItemName);
                            self.delOrderRowByName(dataItemInside.ItemName);
                            self.clearItemInfo();
                            self.selectItemRow(rowKey == self.view_item_grid.find("button").length ? (rowKey - 1) : rowKey);
                        });
                        $(this).asmatDraggable({
                            //treeview: treeview,
                            hint: function (item) {

                                //alert(treeview.dataItem($(item)).text);
                                self.dragTarget = this;
                                self.dragModel = "move";
                                self.dragDataItem = grid.dataItem($(item));

                                var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;padding:10px 20px;'>" + self.dragDataItem.ItemName + "</div>");

                                self.view_group_grid_box.append('<div class="grid-drop-hint-box" ></div>');
                                self.view_orderby_grid_box.append('<div class="grid-drop-hint-box" ></div>');

                                return hintElement;
                            },
                            dragstart: function (e) {
                            },
                            dragend: function (e) {
                                self.view_group_grid_box.find(".grid-drop-hint-box").remove();
                                self.view_orderby_grid_box.find(".grid-drop-hint-box").remove();
                            }
                        });
                    });
                },
                selectable: true
            });


            this.view_item_grid_box.asmatDropTargetArea({
                filter: ".grid-drop-hint-box",
                dragenter: function (e) {
                    if (self.dragModel != "new") {
                        e.preventDefault();
                        return;
                    }
                    self.dropTarget = $(e.dropTarget);

                    $(e.dropTarget).append('<div class="grid-drop-box" style="position: absolute;background-color: #19C6F9;opacity: 0.5;text-align: center;top: -1px;left: -1px;width: 100%;height: 100%;z-index: 1099; display: block;"><span style="position: absolute;top: 30%;display: block;left: 65px;color: #fff;font-size: 36px;font-weight: bold;">追加</span></div>');
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

                    if (!self.view_list.ui().value()) {
                        smat.service.notice({msg:"please add new view fire!!"})
                        e.preventDefault();
                        return;
                    }

                    var target = $(e.dropTarget);
                    if (target.find(".grid-drop-box").length == 0) return;
                    target.find(".grid-drop-box").remove();

                    var iSource = self.view_item_grid.ui().config.dataSource;
                    var newItemName = self.dragDataItem.FieldName;
                    var newItemDesc = self.dragDataItem.FieldDesc;
                    var nIndex = 1;
                    while ($.Enumerable.From(iSource).Any("$.ItemName == '" + newItemName + "'")) {
                        newItemName = self.dragDataItem.FieldName + ("" + nIndex);
                        nIndex++;
                    }


                    var newItem =
                    {
                        ProjID: self.curView.ProjID,
                        EntityName: self.curView.EntityName,
                        ViewName: self.curView.ViewName,
                        Path: self.dragDataItem.Path,
                        ItemName: newItemName,
                        ItemDesc: newItemDesc,
                        ItemSql: self.dragDataItem.Alias + "." + self.dragDataItem.FieldName,
                        ItemEntityName: self.dragDataItem.EntityName,
                        ItemFieldName: self.dragDataItem.FieldName,
                        EntityAlias: self.dragDataItem.EntityName,
                        Group: null
                    };

                    self.view_item_grid.ui().addRow(newItem);

                    for (var i = 0; i < self.view_item_grid.ui().config.dataSource.length ; i++) {
                        self.view_item_grid.ui().config.dataSource[i].Seq = i + 1;
                    }

                    self.selectItemRow(self.view_item_grid.find('tbody').children('tr').length - 1);
                }
            });

        }, initGroupGrid: function () {
            var self = this;
            this.view_group_grid.smatGrid({
                columns: [
                    {
                        field: "ItemName",
                        title: "ItemName",
                        template: self.templateItemName
                    },
                    {
                        field: "ItemName",
                        title: "2",
                        template: function (dataItem) {
                            return '<button class="btn-danger s-button" style="padding: 3px 6px;min-width: 20px;">X</button>';
                        },
                        width: "36px"
                    }
                ],
                dataBound: function (e) {
                    e.sender.thead.find('tr').remove();
                    e.sender.table.css('table-layout', 'fixed');

                    var gridGroup = e.sender;
                    var trs = e.sender.tbody.children('tr');
                    if (trs.length > 0) trs.eq(0).children('td').css('border-top', 'none');

                    $.each(trs, function (n, value) {
                        $(this).find('button').bind('click', function () {
                            var trInside = $(this).closest('tr');
                            var rowKey = trInside.index();
                            var dataItemInside = gridGroup.dataItem(trInside);

                            //update Group
                            var itemTr = self.view_item_grid.find("span[key='" + dataItemInside.ItemName + "']").closest('tr');
                            var dataItemMain = self.view_item_grid.ui().uiControl.dataItem(itemTr);
                            var dataSourceItemMain = self.view_item_grid.ui().config.dataSource[itemTr.index()];

                            dataItemMain["Group"] = null;
                            dataSourceItemMain["Group"] = null;
                            if (self.curDataItem.ItemName == dataItemMain.ItemName) {
                                self._Group.ui().value("");
                            }
                            var nameSpan = self.view_item_grid.find("span[key='" + dataItemMain.ItemName + "']");
                            if (nameSpan.length > 0) {
                                nameSpan.closest('tr').children('td:eq(1)').html("");
                            }

                            self.delGroupRowByName(dataItemInside.ItemName);
                            self.checkGroupInfo();
                        });
                    });
                },
                selectable: false
            });


            this.view_group_grid_box.asmatDropTargetArea({
                filter: ".grid-drop-hint-box",
                dragenter: function (e) {
                    if (self.dragModel != "move") {
                        e.preventDefault();
                        return;
                    }
                    self.dropTarget = $(e.dropTarget);

                    $(e.dropTarget).append('<div class="grid-drop-box" style="position: absolute;background-color: #19C6F9;opacity: 0.5;text-align: center;top: -1px;left: -1px;width: 100%;height: 100%;z-index: 1099; display: block;"><span style="position: absolute;top: 30%;display: block;left: 65px;color: #fff;font-size: 36px;font-weight: bold;">追加</span></div>');
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

                    var groupedItems = self.view_group_grid.ui().config.dataSource;
                    if ($.Enumerable.From(groupedItems).Any("$.ItemName == '" + self.dragDataItem.ItemName + "'")) {
                        e.preventDefault();
                        return;
                    }

                    self.dragDataItem.Group = "GroupBy";
                    self.view_group_grid.ui().addRow(self.dragDataItem);
                    self.dragModel = null;

                    var nameSpan = self.view_item_grid.find("span[key='" + self.dragDataItem.ItemName + "']");
                    if (nameSpan.length > 0) {
                        nameSpan.closest('tr').children('td:eq(1)').html(self.templateGroup(self.dragDataItem));
                    }

                    if (self.curDataItem.ItemName == self.dragDataItem.ItemName) {
                        self._Group.ui().value("GroupBy");
                    }
                    self.groupItem();
                }
            });

        }, initOrderGrid: function () {
            var self = this;
            this.view_orderby_grid.smatGrid({
                columns: [
                    {
                        field: "ItemName",
                        title: "ItemName",
                        template: self.templateItemName
                    },
                    {
                        field: "Orderby",
                        title: "2",
                        template: function (dataItem) {
                            return '<input type="text" style="width:100%" class="" value="' + dataItem.OrderBy + '">';
                        },
                        width: "100px"
                    },
                    {
                        field: "ItemName",
                        title: "2",
                        template: function (dataItem) {
                            return '<button class="btn-danger s-button" style="padding: 3px 6px;min-width: 20px;">X</button>';
                        },
                        width: "36px"
                    }
                ],
                dataBound: function (e) {
                    e.sender.thead.find('tr').remove();
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
                            placeholder: function (element) {
                                return element.clone().addClass("s-state-hover").css("opacity", 0.45);
                            },
                            container: "#view_orderby_grid tbody",
                            change: function (e) {
                                var oldIndex = e.oldIndex,
                                    newIndex = e.newIndex,
                                    data = grid.dataSource.data(),
                                    dataItem = grid.dataSource.getByUid(e.item.data("uid")),
                                    dataSourceItem = self.view_orderby_grid.ui().config.dataSource[e.oldIndex];

                                grid.dataSource.remove(dataItem);
                                grid.dataSource.insert(newIndex, dataItem);

                                self.view_orderby_grid.ui().config.dataSource.splice(e.oldIndex, 1);
                                self.view_orderby_grid.ui().config.dataSource.splice(newIndex, 0, dataSourceItem);

                                self.orderItemSeq();
                            }
                        });
                    }

                    $.each(trs, function (n, value) {
                        var orderInput = $(this).find('input');
                        orderInput.smatButtonGroup({
                            dataTextField: "text",
                            dataValueField: "value",
                            dataSource: [
                                { text: "Asc", value: "1", },
                                { text: "Desc", value: "-1", }
                            ],
                            change: function (e, value) {
                                var tr = e.sender.wrapper.closest('tr');
                                var rowKey = tr.index()
                                var dataItem = self.view_orderby_grid.ui().uiControl.dataItem(tr);
                                dataItem["OrderBy"] = e.ui.value();
                                self.view_orderby_grid.ui().config.dataSource[rowKey] = dataItem;
                                self.orderItemSeq();
                            }
                        });

                        $(this).find('button').bind('click', function () {
                            var trInside = $(this).closest('tr');
                            var rowKey = trInside.index();
                            var dataItemInside = gridOrder.dataItem(trInside);

                            //update OrderBy
                            var itemTr = self.view_item_grid.find("span[key='" + dataItemInside.ItemName + "']").closest('tr');
                            var dataItemMain = self.view_item_grid.ui().uiControl.dataItem(itemTr);
                            var dataSourceItemMain = self.view_item_grid.ui().config.dataSource[itemTr.index()];

                            dataItemMain["OrderBy"] = null;
                            dataSourceItemMain["OrderBy"] = null;

                            self.delOrderRowByName(dataItemInside.ItemName);
                            self.orderItemSeq();
                        });
                    });

                }
            });

            this.view_orderby_grid_box.asmatDropTargetArea({
                filter: ".grid-drop-hint-box",
                dragenter: function (e) {
                    if (self.dragModel != "move") {
                        e.preventDefault();
                        return;
                    }
                    self.dropTarget = $(e.dropTarget);

                    $(e.dropTarget).append('<div class="grid-drop-box" style="position: absolute;background-color: #19C6F9;opacity: 0.5;text-align: center;top: -1px;left: -1px;width: 100%;height: 100%;z-index: 1099; display: block;"><span style="position: absolute;top: 30%;display: block;left: 65px;color: #fff;font-size: 36px;font-weight: bold;">追加</span></div>');
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

                    var orderedItems = self.view_orderby_grid.ui().config.dataSource;
                    if ($.Enumerable.From(orderedItems).Any("$.ItemName == '" + self.dragDataItem.ItemName + "'")) {
                        e.preventDefault();
                        return;
                    }

                    self.dragDataItem.OrderBy = 1;
                    self.view_orderby_grid.ui().addRow(self.dragDataItem);
                    self.orderItemSeq();
                    self.dragModel = null;
                }
            });


        }, orderItemSeq: function () {

            var orderedSource = this.view_orderby_grid.ui().config.dataSource;

            for (var i = 0; i < orderedSource.length ; i++) {
                orderedSource[i].Seq = i + 1;
            }

            var skip = orderedSource.length + 1;

            var source = this.view_item_grid.ui().config.dataSource;
            for (var key in source) {
                var data = source[key];
                var dataOrder = $.Enumerable.From(orderedSource).Where("$.ItemName =='" + data.ItemName + "'").FirstOrDefault(null);

                if (dataOrder == null) {
                    data.Seq = skip;
                    skip++;
                } else {
                    data.Seq = dataOrder.Seq;
                    data.OrderBy = dataOrder.OrderBy;
                }
            }

            this.view_item_grid.ui().config.dataSource

        }, groupItem: function () {
            var groupedSource = this.view_group_grid.ui().config.dataSource;

            var source = this.view_item_grid.ui().config.dataSource;
            for (var key in source) {
                var data = source[key];
                var dataOrder = $.Enumerable.From(groupedSource).Where("$.ItemName =='" + data.ItemName + "'").FirstOrDefault(null);

                if (dataOrder == null) {
                } else {
                    data.Group = dataOrder.Group;
                }
            }
            this.checkGroupInfo();

        }, newView: function () {
            var self = this;
            this.getNewName(function (result) {
                if (result) {
                    self.config.page.createNewView({
                        ViewName: result.name,
                        ViewDesc: result.desc,
                    });

                    var newView = self.config.page.editViewList[self.config.page.editViewList.length - 1];
                    var views = self.view_list.ui().config.dataSource;
                    newView.EntityName = self.entity_list.ui().value();
                    views.push(newView);

                    self.view_list.ui().setDataSource(views);
                    self.view_list.ui().value(newView.ViewName);
                    self.setViewInfo(self.view_list.ui().value());
                }
            });

        }, copyView: function () {
            var self = this;
            this.getNewName(function (result) {
                if (result) {
                    self.config.page.createNewView({
                        ViewName: result.name,
                        ViewDesc: result.desc,
                    });

                    var newView = self.config.page.editViewList[self.config.page.editViewList.length - 1];

                    var views = self.view_list.ui().config.dataSource;
                    var viewFrom = smat.service.getItemByKey(views, "ViewName", self.view_list.ui().value());

                    newView.ItemList = smat.globalObject.clone(viewFrom.ItemList);
                    for (var key in newView.ItemList) {
                        newView.ItemList[key].ViewName = newView.ViewName;
                    }

                    views.push(newView);

                    self.view_list.ui().setDataSource(views);
                    self.view_list.ui().value(newView.ViewName);
                    self.setViewInfo(self.view_list.ui().value());
                }
            }, true);

        }, getNewName: function (handle, isCopy) {
            var self = this;
            var box = $('<section id="' + this.uuid + '_newName" class="panel panel-default " style="margin: 0;padding: 10px;height: 120px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">Name</label><input id="_Name_NEW" class="s-textbox input-s" ></div></div>').appendTo(box);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">Desc</label><input id="_Desc_NEW" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(box);
            var newNameInput = box.find("#_Name_NEW");
            var newDescInput = box.find("#_Desc_NEW");

            if (isCopy) {
                var views = self.view_list.ui().config.dataSource;
                var dataItem = smat.service.getItemByKey(views, "ViewName", self.view_list.ui().value());
                newNameInput.val(dataItem.ViewName);
                newDescInput.val(dataItem.ViewDesc);
            }

            var newNameBtn = box.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if (name == "") {
                        alert("【名称】を入力してください。");
                        newNameInput.focus();
                        return;
                    }

                    var isExist = false;

                    var dataSource = self.view_list.ui().config.dataSource;

                    if ($.Enumerable.From(dataSource).Any("$.ViewName == '" + name + "'")) {

                        isExist = true;
                    }

                    if (isExist == true) {
                        alert("名称:【" + name + "】 が既に使用しています。");
                        newNameInput.focus();
                        return;
                    }

                    smat.service.closeForm({
                        contentId: self.uuid + '_newName',
                        result: {
                            name: name,
                            desc: newDescInput.val()
                        }
                    });
                }
            })


            smat.service.openForm({
                contentDom: box,
                width: "410px",
                top: "20%",
                title: "New View",
                afterClose: function (result) {
                    handle(result);
                }
            });


        }, selectItemRow: function (rowIndex) {

            this.clearItemInfo();
            var itemGrid = this.view_item_grid.ui().uiControl;

            if (this.checkItemName() == false) {
                itemGrid.select(this.view_item_grid.find('tbody').children('tr:eq(' + this.curRowIndex + ')'));
                return false;
            }

            var tr = this.view_item_grid.find('tbody').children('tr:eq(' + rowIndex + ')');
            if (tr.length == 0) return;
            this.btn_ok.focus();
            this.curDataItem = itemGrid.dataItem(tr);
            this.curDataSourceItem = this.view_item_grid.ui().config.dataSource[rowIndex];
            if (this.curRowIndex == rowIndex) {
                return;
            }
            this.curRowIndex = rowIndex;

            var row = itemGrid.select();
            if (row.index() != rowIndex) itemGrid.select(tr);
            this.setItemInfo(this.curDataItem);
            this._ItemName.focus();

            this.setButtonEnable();

            this.checkGroupInfo();
        }, clearItemInfo: function () {
            this.curRowIndex = -1;
            this.curDataItem = null;
            this.curDataSourceItem = null;

            this._ItemName.val("");
            this._ItemName.attr('oldValue', "");
            this._ItemDesc.val("");
            this._Path.val("");
            this._ItemSql.val("");
            this._ItemEntityName.val("");
            //this._Format.val("");
            this._Format.ui().value("");
            this._Group.ui().value("");

            this._FilterSql.val("");
            this._HavingFilterSql.val("");
        }, setItemInfo: function (dataItem) {
            if (!dataItem) {
                return;
            }
            this._ItemName.val(dataItem.ItemName);
            this._ItemName.attr('oldValue', dataItem.ItemName);
            this._ItemDesc.val(dataItem.ItemDesc);
            this._Path.val(dataItem.Path);
            this._ItemSql.val(dataItem.ItemSql);
            this._ItemEntityName.val(dataItem.ItemEntityName);
            //this._Format.val(dataItem.Format);

            this._Format.ui().value("");
            this._Format.ui().value(dataItem.Format);

            this._Group.ui().value("");
            this._Group.ui().value(dataItem.Group);

            //filter
            var view = this.curView;
            this.filterItem = undefined, this.havingFilterItem = undefined;
            for (var key in view.ViewFilterList) {
                var filterItemTpem = smat.service.getItemByKey(this.config.page.entity.FilterList, "FilterName", view.ViewFilterList[key].FilterControlName);

                if (filterItemTpem.FilterDesc != "ViewItemFilter:" + dataItem.ItemName) {
                    continue;
                }

                if (filterItemTpem && filterItemTpem.IsHaving == false) {
                    this.filterItem = filterItemTpem;
                } else if (filterItemTpem && filterItemTpem.IsHaving == true) {
                    this.havingFilterItem = filterItemTpem;
                }
            }

            this._FilterSql.val("");
            if (this.filterItem) {
                this._FilterSql.val(this.filterItem.FilterSql);
            }

            this._HavingFilterSql.val("");
            if (this.havingFilterItem) {
                this._HavingFilterSql.val(this.havingFilterItem.FilterSql);
            }

        }, previewView: function () {
            var self = this;
            var previewBox = $(this.getPreviewDomStr());

            this.btn_search = previewBox.find('#btn_search');
            this.view_preview_grid = previewBox.find('#view_preview_grid');

            smat.service.openForm({
                contentDom: previewBox,
                width: "840px",
                title: smat.service.optionSet("DyOptionText.Preview")
            });

            this.view_preview_pager = previewBox.find('#view_preview_pager').smatPager({
                dynamics: true,
                dataHandler: "view_preview_grid"
            });

            var pview = smat.globalObject.clone(self.curView);
            pview.ItemList = self.view_item_grid.ui().config.dataSource;
            var cols = new Array();
            for (var key in pview.ItemList) {
                cols.push({
                    field: pview.ItemList[key].ItemName,
                    title: pview.ItemList[key].ItemName
                });
            }

            this.view_preview_grid.smatGrid({
                columns: cols
            });

            this.btn_search.bind('click', function () {
                var actionUrl = smat.dynamics.commonURL.getPageView;
                var params = {};
                params.request = {};
                params.request.FilterDic = {}
                params.request.ProjID = self.config.page.entity.ProjID;
                params.request.EntityName = self.entity_list.ui().value();
                params.request.View = pview;

                smat.service.loadJosnData({
                    url: actionUrl,
                    params: params,
                    success: function (result) {
                        self.view_preview_pager.setActionConfig({
                            action: actionUrl,
                            params: params
                        });
                        self.view_preview_pager.setDataSource(result);
                    }

                });
            });


        }, moveUpDown: function (step) {
            var newIndex = this.curRowIndex + step;


            var temp = this.view_item_grid.ui().config.dataSource[newIndex];


            this.view_item_grid.ui().config.dataSource[newIndex] = this.view_item_grid.ui().config.dataSource[this.curRowIndex];
            this.view_item_grid.ui().config.dataSource[this.curRowIndex] = temp;

            for (var i = 0; i < this.view_item_grid.ui().config.dataSource.length ; i++) {
                this.view_item_grid.ui().config.dataSource[i].Seq = i + 1;
            }
            this.view_item_grid.ui().setDataSource(this.view_item_grid.ui().config.dataSource);


            this.curRowIndex = newIndex;
            this.setButtonEnable();
            this.selectItemRow(this.curRowIndex);
        }, setButtonEnable: function () {

            var dataRowsCount = this.view_item_grid.ui().config.dataSource.length;

            if (dataRowsCount < 2) {
                this.btnUp.ui().enable(false);
                this.btnDown.ui().enable(false);
            } else if (this.curRowIndex == 0) {
                this.btnUp.ui().enable(false);
                this.btnDown.ui().enable(true);
            } else if (this.curRowIndex == dataRowsCount - 1) {
                this.btnUp.ui().enable(true);
                this.btnDown.ui().enable(false);
            }
            else {
                this.btnUp.ui().enable(true);
                this.btnDown.ui().enable(true);
            }


        }, getDomStr: function () {

            return '<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 590px; min-width:800px;">'
+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 40px;position: absolute;left: 0; top:6px;z-index: 99;">'
+ '        <div class="col-sm-10" style="margin: 0;padding: 0;">'
+ '            <input id="content_type" style="margin-left: 10px; width:200px;" />'
+ '        </div>'
+ '    </div>'

+ '    <div id="view_panel" class="" style="">'

+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 40px;position: absolute;left: 0; top:38px;z-index: 99;">'
+ '        <div class="col-sm-10" style="margin: 0;padding: 0;">'
+ '            <input id="entity_list" style="margin-left: 10px; width:200px;" />'
+ '            <input id="view_list" style="margin-left: 10px; width:360px;" />'
+ '        </div>'
+ '        <div class="col-sm-2 text-right" style="margin: 0;padding: 0;">'
+ '            <button id="btn_add" class="btn-primary s-button" style="margin-right:10px;">新規</button>'
+ '            <button id="btn_copy" class="btn-primary s-button" style="margin-right:10px;">コピー</button>'
+ '        </div>'
+ '    </div>'
+ '    <div class="col-sm-3" style="margin: 0;padding: 0;height: 100%;position: relative;">'
+ '        <div style="height:20px;margin-top: 62px;">' + smat.service.optionSet("DyOptionText.Field") + '</div>'
+ '        <div id="view_field_tree" style="height:470px;border: 1px solid #ccc;"></div>'
+ '    </div>'
+ '    <div class="col-sm-9" style="margin: 0;padding: 0;height: 100%;">'
+ '        <div style="height:20px;margin-top: 62px;margin-left:10px;">項目</div>'
+ '        <div class="col-sm-12" style="margin: 0;padding: 0;height: 320px;">'
+ '            <div id="view_item_grid_box" style="height:308px;border: 1px solid #ccc; margin:0px 0 0 10px; width:200px; float:left;position: relative;" class="grid-drop"><div id="view_item_grid" style="height:298px;border: none;"></div></div>'
+ '            <div id="view_item_btn_box" style="height: 120px;margin: 0px 0 0 10px;z-index: 19000;width: 40px;float: left;position: absolute;left: 205px;" class=""></div>'
+ '            <div  style="height:290px;margin:0px 0 0 10px; width:380px;float:left;">'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">名称</label><input id="_ItemName" class="s-textbox " style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">記述</label><input id="_ItemDesc" class="s-textbox " style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">パス</label><input id="_Path" class="s-textbox " disabled="disabled" style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">Sql文</label><input id="_ItemSql" class="s-textbox " style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">実体</label><input id="_ItemEntityName" disabled="disabled" class="s-textbox " style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">フォーマット</label><input id="_Format" class="" style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">グループ</label><input id="_Group" class="" style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">条件</label><input id="_FilterSql" class="s-textbox " style="width:280px;"></div></div>'
+ '               <div class="row" style="margin:0;"><div class=" form-group" ><label class="control-label text-right" style="width:90px; margin-right:5px;">集計後条件</label><input id="_HavingFilterSql" class="s-textbox " style="width:280px;"></div></div>'
+ '              </div>'
+ '        </div>'
+ '        <div class="col-sm-12" style="margin: 0;padding: 0;height: 160px;">'
+ '            <div style="height:20px;"><span style="margin-left:10px;">グループ</span><span style ="margin-left:150px;">ソート</spanstyle></div>'
+ '            <div id="view_group_grid_box" style="height:130px;border: 1px solid #ccc; margin:0px 0 0 10px; width:200px; float:left;position: relative;" class="grid-drop-group"><div id="view_group_grid" style="height:130px;border: none;overflow: auto;overflow-y: auto;  width:200px;"></div></div>'
+ '             <div id="view_orderby_grid_box" style="height:130px;border: 1px solid #ccc; margin:0px 0 0 10px; width:380px;float:left;position: relative;" class="grid-drop-order"><div id="view_orderby_grid" style="height:130px;border: none; overflow: auto;overflow-y: auto; width:380px;"></div></div>'
+ '        </div>'
+ '    </div>'

+ '    </div>'

+ '    <div id="filter_panel" class="" style="display:none;">'

+ '    <div class="col-sm-3" style="margin: 0;padding: 0;height: 100%;position: relative;">'
+ '        <div id="filter_box" style="height:500px;border: 1px solid #ccc;overflow: auto;margin-top: 30px;"></div>'
+ '    </div>'

+ '    </div>'


+ '    <div class="col-sm-12 text-right" style="margin: 0;padding: 0;height: 36px;position: absolute;bottom: 0px;right: 10px;">'
+ '        <button id="btn_preview" class="btn-primary s-button" style="">' + smat.service.optionSet("DyOptionText.Preview") + '</button>'
+ '        <button id="btn_ok" class="btn-primary s-button" style="">確定</button>'
+ '        <button id="btn_cancel" class="btn-danger s-button" style="margin-right:10px;">キャンセル</button>'
+ '    </div>'
+ '</section>'
        }
        , getPreviewDomStr: function () {

            return '<section class="panel panel-default " style="margin: 0;padding: 10px;height: 576px; min-width:800px;">'
+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 40px;position: absolute;left: 0; top:6px;z-index: 99;">'
+ '        <div class="col-sm-4" style="margin: 0;padding: 0;">'
+ '            <button id="btn_search" class="btn-primary s-button" style="margin-right:10px;">' + smat.service.optionSet("DyOptionText.Search") + '</button>'
+ '        </div>'
+ '        <div class="col-sm-8 text-right" style="margin: 0;padding: 0;">'
+ '            <div id="view_preview_pager"></div>'
+ '        </div>'
+ '    </div>'
+ '    <div class="col-sm-12" style="margin: 0;padding: 0;height: 40px;position: absolute;left: 0; top:46px;z-index: 99;">'
+ '        <div id="view_preview_grid"></div>'
+ '   </div>'
+ '</section>'
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.View, smat.dynamics.Element);
})();