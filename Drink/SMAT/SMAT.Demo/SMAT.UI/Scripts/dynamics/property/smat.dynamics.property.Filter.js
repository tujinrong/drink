
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  PropertyFilter 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Filter = function (config) {
        //默认属性
        this.setConfig({
            picker: undefined,
            dataSource: []

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.Filter.prototype = {
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
        open: function () {
            var self = this;
            
            if (this.config.mode == undefined) {
                if (self.config.currentCell != undefined) {
                    this.config.mode = "read";
                } else {
                    this.config.mode = "edit";
                }
            }

            if (this.config.mode == "edit")
            {
                this.config.oldFilterName = this.config.filterName;
            }

            var box = $('<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 460px;"></section>');

            
            var leftBox = $('<div class="col-sm-4" style="margin: 0;padding: 0;height: 100%;position: relative;"></div>');

            var rightBoxClass = "col-sm-12";
            if (this.config.mode != "read")
            {
                leftBox.appendTo(box);
                rightBoxClass = "col-sm-8";
            }
            
            //var midBox = $('<div class="col-sm-3" style="margin: 0;padding: 0;height: 100%;position: relative;"></div>').appendTo(box);
            var rightBox = $('<div class="' + rightBoxClass + '" style="margin: 0;padding: 0;height: 100%;"></div>').appendTo(box);

            var leftTop = $('<div style="height:20px;">' + smat.service.optionSet("DyOptionText.Field") + '</div>').appendTo(leftBox);
            var filedTree = $('<div id="view_field_tree" style="height:350px;margin-right: 40px;border: 1px solid #ccc;"></div>').appendTo(leftBox);

            this.view_field_tree = leftBox.find('#view_field_tree');

            //==================Field==============
            var fieldData = smat.dynamics.entityFieldTree(this.config.page.config.designer.data);

            this.view_field_tree.asmatTreeView({
                dataSource: [fieldData]
            });

            this.treeview = this.view_field_tree.data("asmatTreeView");
            this.treeview.expand(this.view_field_tree.find('.s-item:first'));
            this.initDragItem(this.view_field_tree.find(".s-item"))

            //var midTop = $('<div style="height:20px;">Filters</div>').appendTo(midBox);
            //this.objectBox = $('<div style="padding-right: 40px;padding-bottom: 50px;"></div>').appendTo(midBox);
            //var btnUpDownBox = $('<div style="position: absolute;top: 10px; right: 0;width: 40px;text-align: center;padding: 10px 0;"></div>').appendTo(midBox);
            //var btnNewDelBox = $('<div style="position: absolute;bottom: 10px; left: 0;width: 90%;padding: 0 8px;"></div>').appendTo(midBox);

            //this.btnUp = $('<button class="btn-primary " style="padding: 8px 5px;margin-bottom: 5px;">↑</button>').appendTo(btnUpDownBox);
            //this.btnDown = $('<button class="btn-primary " style="padding: 8px 5px;">↓</button>').appendTo(btnUpDownBox);

            //this.btnNew = $('<button class="btn-primary s-button" style="margin-right: 5px;">new</button>').appendTo(btnNewDelBox);
            //this.btnDel = $('<button class="btn-danger " style="">Delete</button>').appendTo(btnNewDelBox);

            //this.objectTree = $('<div style="height: 350px;border: 1px solid #ccc; position: relative;" class="tree-drop"></div>').appendTo(this.objectBox);

        

            var rightTop = $('<div style="height:20px;">' + smat.service.optionSet("DyOptionText.Property") + '</div>').appendTo(rightBox);
            this.propertyBox = $('<div style="" class="property-drop"></div>').appendTo(rightBox);
            var btnOkCancelBox = $('<div style="text-align:right; position: absolute;bottom: 10px; left: 0;width: 90%;padding: 0 8px;"></div>').appendTo(rightBox);

            this.btnOk = $('<button class="btn-primary s-button" style="">' + smat.service.optionSet("DyOptionText.Ok") + '</button>').appendTo(btnOkCancelBox);
            this.btnClear = $('<button class="btn-info s-button" style="margin-left: 5px;">' + smat.service.optionSet("DyOptionText.Clear") + '</button>');

            if (this.config.mode != "read")
            {
                this.btnClear.appendTo(btnOkCancelBox);
            }

            var propertyGrid = $('<div style="height: 386px;overflow: auto;"></div>').appendTo(this.propertyBox);

            var filterName = $('<div class="row" style="margin:30px 0 0 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Name") + '</label><input id="_FilterName" class="s-textbox input-s"></div></div>');
            var filterDesc = $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Alias") + '</label><input id="_EntityAliasName" class="s-textbox input-s" ></div></div>');
            var filterPath = $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Desc") + '</label><input id="_FilterDesc" class="s-textbox" style="width:300px;"></div></div>');
            var EntityAliasName = $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Path") + '</label><input id="_FilterPath" class="s-textbox" style="width:300px;"></div></div>');

            var propertyLine = $('<div class="line line-dashed b-b line-lg "></div>');

            var fieldList = $('<div class="row" style="margin:8px 0;"><input id="_FieldList" class="" style="width:180px;"/><input id="_SQLOption" class="" style="width:120px;"/><button id="_pick" class="btn-info " style="margin-left:10px;">' + smat.service.optionSet("DyOptionText.Select") + '</button></div>');
            var createSql = $('<div class="row" style="margin:8px 0;"> ' + smat.service.optionSet("DyOptionText.SQLCreate") + ':&nbsp;&nbsp;<label id="_SQLStr" class="input-s " style="margin-right:5px;"></label></div>');
            var filterSql = $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">SQL</label><input id="_FilterSql" class="s-textbox" style="width:300px;"></div></div>');

            filterName.appendTo(propertyGrid);
            filterDesc.appendTo(propertyGrid);
            filterPath.appendTo(propertyGrid);
            EntityAliasName.appendTo(propertyGrid);
            if (this.config.mode != "read") {
                propertyLine.appendTo(propertyGrid);
                fieldList.appendTo(propertyGrid);
                createSql.appendTo(propertyGrid);
            }
            filterSql.appendTo(propertyGrid);


            this.FilterNameInput = propertyGrid.find('input#_FilterName');
            this.FilterDescInput = propertyGrid.find('input#_FilterDesc');
            this.FilterPathInput = propertyGrid.find('input#_FilterPath');
            this.EntityAliasNameInput = propertyGrid.find('input#_EntityAliasName');
            this.FilterSqlInput = propertyGrid.find('input#_FilterSql');

            if (this.config.mode == "read")
            {
                this.FilterNameInput.addClass("s-state-disabled");
                this.FilterNameInput.attr("disabled", "disabled");
                this.FilterDescInput.addClass("s-state-disabled");
                this.FilterDescInput.attr("disabled", "disabled");
                this.FilterPathInput.addClass("s-state-disabled");
                this.FilterPathInput.attr("disabled", "disabled");
                this.EntityAliasNameInput.addClass("s-state-disabled");
                this.EntityAliasNameInput.attr("disabled", "disabled");
                this.FilterSqlInput.addClass("s-state-disabled");
                this.FilterSqlInput.attr("disabled", "disabled");
            }

                disabled = "";
            //s-state-disabled" disabled="disabled"

            this.FilterNameInput.bind('blur', function (e) {
                smat.service.clearErrorBorder($(this));
                if (self.checkFilterName() == false) {
                    e.preventDefault();
                    return false;
                } else {
                    self.changeValue("FilterName", $(this).val());
                    $(this).attr('oldValue', $(this).val());
                }
            });

            this.FilterDescInput.bind('blur', function (e) {
                self.changeValue("FilterDesc", $(this).val());
            });

            this.FilterPathInput.bind('blur', function (e) {
                self.changeValue("Path", $(this).val());
            });

            this.EntityAliasNameInput.bind('blur', function (e) {
                self.changeValue("ItemEntityAliasName", $(this).val());
            });

            this.FilterSqlInput.bind('blur', function (e) {
                self.changeValue("FilterSql", $(this).val());
            });

            this.FieldList = propertyGrid.find('input#_FieldList');
            this.SQLOption = propertyGrid.find('input#_SQLOption');
            this.btnPick = propertyGrid.find('#_pick');
            this.SQLStr = propertyGrid.find('#_SQLStr');
            

            var fieldItems = new Array();
            for (var key in this.config.page.entity.FieldList) {
                var item = smat.globalObject.clone(this.config.page.entity.FieldList[key]);
                item.text = item.FieldDesc;
                fieldItems.push(item);
            }

            fieldItems.unshift({
                FieldName: "",
                text: ""
            });

            this.FieldList.smatDropDownList({
                dataSource: fieldItems,
                dataValueField: "FieldName",
                dataTextField: "text",
                change: function () {
                    self.createSql();
                }
            });

            this.btnPick.smatButton({
                click: function () {
                    self.pickSql();
                }
            });

            var sqlOptionDatas = [
                {
                    text: "-" + smat.service.optionSet("DyOptionText.Option")+ "-",
                    value:""
                }, {
                    text: " = ",
                    value: "="
                }, {
                    text: " > ",
                    value: ">"
                }, {
                    text: " < ",
                    value: "<"
                }, {
                    text: " >= ",
                    value: ">="
                }, {
                    text: " <= ",
                    value: "<="
                }, {
                    text: " <> ",
                    value: "<>"
                }, {
                    text: " like ",
                    value: "like"
                }
            ];
            this.SQLOption.smatDropDownList({
                dataSource: sqlOptionDatas,
                dataValueField: "value",
                dataTextField: "text",
                change: function () {
                    self.createSql();
                }
            });

            //this.btnNew.smatButton({
            //    click: function () {
            //        self.newNode();
            //    }
            //});

            this.btnClear.smatButton({
                click: function () {
                    self.clear();
                }
            });

            //this.btnDel.smatButton({
            //    click: function () {
            //        self.delNode();
            //    }
            //});

            this.btnOk.smatButton({
                click: function () {
                    smat.service.closeForm({
                        contentId: self.uuid,
                        result:"ok"
                    });
                }
            });

            if (self.config.currentDataItem == undefined) {
                if (self.config.mode == "new") {
                    self.title = smat.service.optionSet("DyOptionText.FilterAdd")
                } else {
                    self.title = smat.service.optionSet("DyOptionText.FilterEdit")
                }
            }
            else {
                self.title = self.config.currentDataItem.id;
            }

            self.width = "700px";
            if (self.config.mode == "read")
            {
                self.width = "500px";
            }

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: box,
                width: self.width,
                title: self.title,
                afterClose: function (result) {
                    if (result != undefined) {
                        var value = self.FilterNameInput.val();
                        if (self.config.currentCell) self.config.currentCell.children('input').val(value);
                        if (self.config.currentControl) self.config.currentControl.propertyChange(self.config.currentDataItem, value);
                        if (self.config.currentCell) self.config.currentCell.children('input').focus();

                        if (self.config.mode == "edit") {
                            var filterControl = smat.service.getItemByKey(self.config.page.entity.FilterControlList, "FilterControlName", self.config.oldFilterName);
                            var filter = smat.service.getItemByKey(self.config.page.entity.FilterList, "FilterName", self.config.oldFilterName);

                            filterControl.ProjID = self.filter.ProjID;
                            filterControl.EntityName = self.filter.EntityName;
                            filterControl.FilterControlDesc = self.filter.FilterDesc;
                            filterControl.FilterControlName = self.filter.FilterName;
                            filterControl.FilterNames = self.filter.FilterName;

                            filter.EntityName = self.filter.EntityName;
                            filter.FilterDesc = self.filter.FilterDesc;
                            filter.FilterName = self.filter.FilterName;
                            filter.FilterSql = self.filter.FilterSql;
                            filter.ItemEntityAliasName = self.filter.ItemEntityAliasName;
                            filter.Path = self.filter.Path;
                            filter.ProjID = self.filter.ProjID;

                        }

                        if (self.config.mode == "new") {
                            var filterControl = {
                                ProjID: self.filter.ProjID,
                                EntityName: self.filter.EntityName,
                                FilterControlDesc: self.filter.FilterDesc,
                                FilterControlName: self.filter.FilterName,
                                FilterNames: self.filter.FilterName
                            }
                            self.config.page.entity.FilterControlList.push(smat.globalObject.clone(filterControl));
                            self.config.page.entity.FilterList.push(smat.globalObject.clone(self.filter));
                        }
                        if (self.config.mode == "edit" || self.config.mode == "new") {
                            self.config.toolFilter.initEditItem();

                            //if (self.config.toolFilter != undefined) {
                            //    var items = new Array();
                            //    for (var key in self.config.page.entity.FilterControlList) {
                            //        var item = smat.globalObject.clone(self.config.page.entity.FilterControlList[key]);
                            //        item.text = item.FilterControlDesc;
                            //        items.push(item);
                            //    }
                            //    self.config.toolFilter.treeview.setDataSource(items);

                            //    var treeItems = self.config.toolFilter.treebox.find('button');

                            //    $.each(treeItems, function (n, value) {

                            //        new smat.dynamics.property["Filter"]({
                            //            mode: "edit",
                            //            picker: $(this),
                            //            toolFilter: self,
                            //            filterName: self.config.toolFilter.treeview.dataItem($(this).closest('.s-item')).FilterControlName,
                            //            page: self.config.page
                            //        });
                            //    });

                            //    self.config.toolFilter.initDragItem(self.config.toolFilter.config.box.find(".s-item:not([aria-expanded])"));
                            //}
                        }
                    }
                }
            });

            this.initFiterProperty();

            if (this.config.currentDataItem != undefined && this.config.currentDataItem.value != "") {
                this.filter = smat.globalObject.clone(smat.service.getItemByKey(this.config.page.entity.FilterList, "FilterName", this.config.currentDataItem.value));
                this.setProInfo();
            }

            if (this.config.filterName != undefined)
            {
                this.filter = smat.globalObject.clone(smat.service.getItemByKey(this.config.page.entity.FilterList, "FilterName", this.config.filterName));
                this.setProInfo();
            }

            //data
            //var datas = this.getTreeViewData();

            //this.objectTree.attr('dy-uuid', this.uuid);

            //this.objectTree.asmatTreeView({
            //    dataSource: datas,
            //    template: '<label filter-id="#= item.data.FilterName #" class="radio i-checks" style="margin-top: 5px;"><input type="radio" class="chs-item" name="filter"><i style="font-size: 14px;"></i>#= item.data.FilterName #</label>',
            //    select: function (e) {
            //        var editer = $(e.sender.wrapper).dynamicsUI();
            //        editer.setProInfo($(e.node));
            //    }
            //});
            //this.tree = this.objectTree.data('asmatTreeView');

            //this.initFiterTree();

            //if (datas.length > 0) {
            //    //默认选中
            //    if (self.config.currentDataItem.value != "") {
            //        var node = this.objectTree.find("label[filter-id='" + self.config.currentDataItem.value + "']").closest('.s-item');
            //        this.tree.select(node);
                    
            //        this.setProInfo(node);
            //    }
            //} else {
            //    this.setButtonEnable();
            //}


        }, checkFilterName: function () {
            var self = this;
            smat.service.clearErrorBorder(this.FilterNameInput);

            var newName = this.FilterNameInput.val();
            var oldName = this.FilterNameInput.attr('oldValue');

            if (newName == oldName) {
                return true;
            }
 
            if (smat.service.getItemByKey(self.config.page.entity.FilterControlList, "FilterControlName", newName) != undefined) {
                smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit",newName), type: "error" });
                smat.service.addErrorBorder(this.FilterNameInput);
                this.FilterNameInput.focus();
                return false;
            }

            return true;

        }, createSql: function () {
            var field = this.FieldList.ui().value();
            var option = this.SQLOption.ui().value();
            var sql = this.filter.ItemEntityAliasName +"."+ field + " ";
            if (field == "" || option == "") {
                this.SQLStr.text("");
                return;
            }

            var dataItem = this.FieldList.ui().dataItem();
            if (option == "like") {
                sql = sql + option + " '%{0}%'";
            } else if (option == "=") {
                //if (dataItem.DataType == "String") {
                //    sql = sql + option + " '{0}'";
                //} else {
                //    sql = sql + option + " {0}";
                //}
                sql = sql + option + " '{0}'";
            } else {
                sql = sql + option + " {0}";
            }


            this.SQLStr.text(sql);
        }, pickSql: function () {
            var sql = this.SQLStr.text();
            if (sql == "" ) {
                return;
            }

            this.FilterSqlInput.val(sql);
            this.FilterSqlInput.focus();
        }, setProInfo: function (node) {
            //var data = this.tree.dataItem(node).data;
            //this.node = node;
            //this.currentFilterData = smat.service.getItemByKey(this.config.page.entity.FilterList, "FilterName", data.FilterName);

            this.FilterNameInput.val(this.filter.FilterName);
            this.FilterNameInput.attr('oldValue', this.filter.FilterName);
            this.FilterDescInput.val(this.filter.FilterDesc);
            this.FilterPathInput.val(this.filter.Path);
            this.EntityAliasNameInput.val(this.filter.ItemEntityAliasName);
            this.FilterSqlInput.val(this.filter.FilterSql);

            //this.node.find('input[type="radio"]').prop("checked", true);

            //this.setButtonEnable();
        }, clear: function () {

            this.FilterNameInput.val("");
            this.FilterDescInput.val("");
            this.FilterPathInput.val("");
            this.EntityAliasNameInput.val("");
            this.FilterSqlInput.val("");
            this.FieldList.ui().value("");
            this.SQLOption.ui().value("");
            this.filter = undefined;
            if (this.node != undefined) {
                this.node.find('input[type="radio"]').prop("checked", false);
            }
            this.node = undefined;

        }, changeValue: function (key, value) {
            if (value != this.filter[key]) {
                this.filter[key] = value;
            }
        }, setButtonEnable: function () {

        }, getTreeViewData: function () {

            var datas = new Array();

            if (this.config.page.entity.FilterList != undefined) {
                for (var i = 0; i < this.config.page.entity.FilterList.length; i++) {
                    var item = this.config.page.entity.FilterList[i];
                    datas.push({
                        text: item.FilterName,
                        data: item
                    });
                }
            }

            return datas;

        }, newNode: function () {
            var self = this;
            var box = $('<section id="' + this.uuid + '_newName" class="panel panel-default " style="margin: 0;padding: 10px;height: 80px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Name") + '</label><input id="_FilterName_NEW" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(box);
            var newNameInput = box.find("#_FilterName_NEW");
            var newNameBtn = box.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if ( name == "") {
                        alert(smat.service.optionSet("SysMsg.Required", smat.service.optionSet("DyOptionText.Name")));
                        newNameInput.focus();
                        return;
                    }

                    if (smat.service.getItemByKey(self.config.page.entity.FilterList, "FilterName", name) != undefined) {
                        alert(smat.service.optionSet("SysMsg.Exit",name));
                        newNameInput.focus();
                        return;
                    }

                    smat.service.closeForm({
                        contentId: self.uuid + '_newName',
                        result: name
                    });
                }
            })


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: box,
                width: "410px",
                top:"20%",
                title: smat.service.optionSet("DyOptionText.Title"),
                afterClose: function (result) {
                    if (result != undefined) {
                        var newIndex = self.config.page.entity.FilterList.length;

                        var newObj = {
                            ProjID: self.config.page.config.projID,
                            EntityName: self.config.page.config.entityName,
                            FilterName: result,
                            FilterDesc: result,
                            ItemEntityAliasName: self.config.page.config.entityName
                        };

                        self.config.page.entity.FilterList.push(smat.globalObject.clone(newObj));

                        var data = self.getTreeViewData();
                        self.tree.setDataSource(data);

                        self.node = self.objectTree.find('.s-item').eq(newIndex);
                        self.tree.select(self.node);
                        self.setProInfo(self.node);

                        self.setButtonEnable();

                    }
                }
            });
        }, delNode: function () {
            var currentIndex = this.node.index();
            var newIndex = currentIndex == 0 ? 0 : currentIndex - 1;

            this.config.page.entity.FilterList.splice(currentIndex, 1);

            var data = this.getTreeViewData();
            this.tree.setDataSource(data);

            if (newIndex >= 0 && this.config.currentDataItem.value.length > 0) {
                this.node = this.objectTree.find('.s-item').eq(newIndex);
                this.tree.select(this.node);
                this.setProInfo(this.node);
            } else {
                this.clear();
            }


            this.setButtonEnable();
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

                        self.propertyBox.append('<div class="property-drop-hint-box" ></div>');


                        return hintElement;
                    },
                    dragstart: function (e) {

                    },
                    dragend: function (e) {
                        self.propertyBox.find(".property-drop-hint-box").remove();
                    }
                });
            });

        }, initFiterProperty: function () {
            var self = this;
            this.propertyBox.asmatDropTargetArea({
                filter: ".property-drop",
                dragenter: function (e) {
                    if (self.dragModel != "new") {
                        e.preventDefault();
                        return;
                    }
                    self.dropTarget = $(e.dropTarget);

                    $(e.dropTarget).append('<div class="property-drop-box" style="position: absolute;background-color: #19C6F9;opacity: 0.5;text-align: center;top: -1px;left: -1px;width: 100%;height: 100%;z-index: 1099; display: block;"><span style="position: absolute;top: 30%;display: block;left: 65px;color: #fff;font-size: 36px;font-weight: bold;">追加</span></div>');
                },
                dragleave: function (e) {

                    if (self.dragModel != "new") {
                        e.preventDefault();
                        return;
                    }

                    var target = $(e.dropTarget);
                    target.find(".property-drop-box").remove();
                    self.dropTarget = undefined;
                },
                drop: function (e) {
                    if (self.dragModel != "new") {
                        e.preventDefault();
                        return;
                    }
                    var target = $(e.dropTarget);
                    if (target.find(".property-drop-box").length == 0) return;
                    target.find(".property-drop-box").remove();

                    var iSource = self.config.page.entity.FilterControlList;
                    var newFilterName = self.dragDataItem.FieldName + "Filter";
                    var nIndex = 1;
                    while ($.Enumerable.From(iSource).Any("$.FilterControlName == '" + newFilterName + "'")) {
                        newFilterName = self.dragDataItem.FieldName + "Filter" + ("" + nIndex);
                        nIndex++;
                    }

                    //var newIndex = self.config.page.entity.FilterList.length;

                    self.filter = {
                        ProjID: self.config.page.config.projID,
                        EntityName: self.config.page.config.entityName,
                        FilterName: newFilterName,
                        FilterDesc: self.dragDataItem.FieldDesc,
                        Path: self.dragDataItem.Path,
                        ItemEntityAliasName: self.dragDataItem.EntityName
                    };
                    var fieldList = [];
                    if (self.dragDataItem.EntityName == self.config.page.config.entityName) {
                        fieldList = self.config.page.entity.FieldList;
                    }
                    else {
                        fieldList = smat.service.getItemByKey(self.config.page.config.designer.data.RelaN1List, "RelaName", self.filter.ItemEntityAliasName).RelaEntity.FieldList;
                    }

                    var fieldItems = new Array();
                    for (var key in fieldList) {
                        var item = smat.globalObject.clone(fieldList[key]);
                        item.text = item.FieldDesc;
                        fieldItems.push(item);
                    }

                    fieldItems.unshift({
                        FieldName: "",
                        text: ""
                    });

                    self.FieldList.ui().setDataSource(fieldItems);

                    //self.config.page.entity.FilterList.push(smat.globalObject.clone(self.filter));

                    //var data = self.getTreeViewData();
                    //self.tree.setDataSource(data);

                    //self.node = self.objectTree.find('.s-item').eq(newIndex);
                    //self.tree.select(self.node);
                    self.setProInfo();

                    self.setButtonEnable();
                }
            });
        }, templateFilterName: function (dataItem) {
            return '<span key="' + dataItem.FilterName + '">' + dataItem.FilterName + '</span>';
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Filter, smat.dynamics.property.SubOptions);
})();