
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
            var box = $('<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 420px;"></section>');
            var leftBox = $('<div class="col-sm-4" style="margin: 0;padding: 0;height: 100%;position: relative;"></div>').appendTo(box);
            var rightBox = $('<div class="col-sm-8" style="margin: 0;padding: 0;height: 100%;"></div>').appendTo(box);

            var leftTop = $('<div style="height:20px;">Filters</div>').appendTo(leftBox);
            var objectBox = $('<div style="padding-right: 40px;padding-bottom: 50px;"></div>').appendTo(leftBox);
            var btnUpDownBox = $('<div style="position: absolute;top: 10px; right: 0;width: 40px;text-align: center;padding: 10px 0;"></div>').appendTo(leftBox);
            var btnNewDelBox = $('<div style="position: absolute;bottom: 10px; left: 0;width: 90%;padding: 0 8px;"></div>').appendTo(leftBox);

            //this.btnUp = $('<button class="btn-primary " style="padding: 8px 5px;margin-bottom: 5px;">↑</button>').appendTo(btnUpDownBox);
            //this.btnDown = $('<button class="btn-primary " style="padding: 8px 5px;">↓</button>').appendTo(btnUpDownBox);

            this.btnNew = $('<button class="btn-primary s-button" style="margin-right: 5px;">new</button>').appendTo(btnNewDelBox);
            this.btnDel = $('<button class="btn-danger " style="">Delete</button>').appendTo(btnNewDelBox);

            this.objectTree = $('<div style="height: 350px;border: 1px solid #ccc;"></div>').appendTo(objectBox);

            var rightTop = $('<div style="height:20px;">Propertys</div>').appendTo(rightBox);
            var propertyBox = $('<div style=""></div>').appendTo(rightBox);
            var btnOkCancelBox = $('<div style="text-align:right; position: absolute;bottom: 10px; left: 0;width: 90%;padding: 0 8px;"></div>').appendTo(rightBox);

            this.btnOk = $('<button class="btn-primary s-button" style="">ok</button>').appendTo(btnOkCancelBox);
            this.btnClear = $('<button class="btn-info s-button" style="margin-left: 5px;">Clear</button>').appendTo(btnOkCancelBox);

            var propertyGrid = $('<div style="height: 346px;overflow: auto;"></div>').appendTo(propertyBox);

            $('<div class="row" style="margin:30px 0 0 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">FilterName</label><input id="_FilterName" class="s-textbox input-s s-state-disabled" disabled="disabled"></div></div>').appendTo(propertyGrid);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">EntityAliasName</label><input id="_EntityAliasName" class="s-textbox input-s" ></div></div>').appendTo(propertyGrid);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">FilterDesc</label><input id="_FilterDesc" class="s-textbox" style="width:300px;"></div></div>').appendTo(propertyGrid);

            $('<div class="line line-dashed b-b line-lg "></div>').appendTo(propertyGrid);

            $('<div class="row" style="margin:8px 0;"><input id="_FieldList" class="" style="width:180px;"/><input id="_SQLOption" class="" style="width:120px;"/><button id="_pick" class="btn-info " style="margin-left:10px;">pick</button></div>').appendTo(propertyGrid);
            $('<div class="row" style="margin:8px 0;">Created SQL:&nbsp;&nbsp;<label id="_SQLStr" class="input-s " style="margin-right:5px;"></label></div>').appendTo(propertyGrid);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">FilterSql</label><input id="_FilterSql" class="s-textbox" style="width:300px;"></div></div>').appendTo(propertyGrid);

            this.FilterNameInput = propertyGrid.find('input#_FilterName');
            this.FilterDescInput = propertyGrid.find('input#_FilterDesc');
            this.EntityAliasNameInput = propertyGrid.find('input#_EntityAliasName');
            this.FilterSqlInput = propertyGrid.find('input#_FilterSql');

            this.FilterDescInput.bind('blur', function (e) {
                self.changeValue("FilterDesc", $(this).val());
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
            for (var key in this.config.currentControl.config.page.entity.FieldList) {
                var item = smat.globalObject.clone(this.config.currentControl.config.page.entity.FieldList[key]);
                item.text = item.FieldName;
                fieldItems.push(item);
            }

            fieldItems.unshift({
                FieldName: "",
                text:"--select field!--"
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
                    text: "-option-",
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

            this.btnNew.smatButton({
                click: function () {
                    self.newNode();
                }
            });

            this.btnClear.smatButton({
                click: function () {
                    self.clear();
                }
            });

            this.btnDel.smatButton({
                click: function () {
                    self.delNode();
                }
            });

            this.btnOk.smatButton({
                click: function () {
                    smat.service.closeForm({
                        contentId: self.uuid
                    });
                }
            });

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: box,
                width:"700px",
                title: self.config.currentDataItem.id,
                afterClose: function (result) {
                    var value = self.FilterNameInput.val();
                    if (self.config.currentCell)self.config.currentCell.children('input').val(value);
                    self.config.currentControl.propertyChange(self.config.currentDataItem, value);
                    if (self.config.currentCell) self.config.currentCell.children('input').focus();
                }
            });


            //data
            var datas = this.getTreeViewData();

            this.objectTree.attr('dy-uuid', this.uuid);

            this.objectTree.asmatTreeView({
                dataSource: datas,
                template: '<label filter-id="#= item.data.FilterName #" class="radio i-checks" style="margin-top: 5px;"><input type="radio" class="chs-item" name="filter"><i style="font-size: 14px;"></i>#= item.data.FilterName #</label>',
                select: function (e) {
                    var editer = $(e.sender.wrapper).dynamicsUI();
                    editer.setProInfo($(e.node));
                }
            });
            this.tree = this.objectTree.data('asmatTreeView');


            if (datas.length > 0) {
                //默认选中
                if (self.config.currentDataItem.value != "") {
                    var node = this.objectTree.find("label[filter-id='" + self.config.currentDataItem.value + "']").closest('.s-item');
                    this.tree.select(node);
                    
                    this.setProInfo(node);
                }
            } else {
                this.setButtonEnable();
            }


        }, createSql: function () {
            var field = this.FieldList.ui().value();
            var option = this.SQLOption.ui().value();
            var sql = field + " ";
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
            var data = this.tree.dataItem(node).data;
            this.node = node;
            this.currentFilterData = smat.service.getItemByKey(this.config.currentControl.config.page.entity.FilterList, "FilterName", data.FilterName);

            this.FilterNameInput.val(this.currentFilterData.FilterName);
            this.FilterDescInput.val(this.currentFilterData.FilterDesc);
            this.EntityAliasNameInput.val(this.currentFilterData.ItemEntityAliasName);
            this.FilterSqlInput.val(this.currentFilterData.FilterSql);

            this.node.find('input[type="radio"]').prop("checked", true);

            this.setButtonEnable();
        }, clear: function () {

            this.FilterNameInput.val("");
            this.FilterDescInput.val("");
            this.EntityAliasNameInput.val("");
            this.FilterSqlInput.val("");
            this.FieldList.ui().value("");
            this.SQLOption.ui().value("");
            this.currentFilterData = undefined;
            if (this.node != undefined) {
                this.node.find('input[type="radio"]').prop("checked", false);
            }
            this.node = undefined;

        }, changeValue: function (key, value) {
            if (value != this.currentFilterData[key]) {
                this.currentFilterData[key] = value;
            }
        }, setButtonEnable: function () {

        }, getTreeViewData: function () {

            var datas = new Array();

            if (this.config.currentControl.config.page.entity.FilterList != undefined) {
                for (var i = 0; i < this.config.currentControl.config.page.entity.FilterList.length; i++) {
                    var item = this.config.currentControl.config.page.entity.FilterList[i];
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
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">FilterName</label><input id="_FilterName_NEW" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(box);
            var newNameInput = box.find("#_FilterName_NEW");
            var newNameBtn = box.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if ( name == "") {
                        alert("plase input name!");
                        newNameInput.focus();
                        return;
                    }

                    if (smat.service.getItemByKey(self.config.currentControl.config.page.entity.FilterList, "FilterName", name) != undefined) {
                        alert("name:【" + name + "】 has bean used!");
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
                title: "New Filter",
                afterClose: function (result) {
                    if (result != undefined) {
                        var newIndex = self.config.currentControl.config.page.entity.FilterList.length;

                        var newObj = {
                            ProjID: self.config.currentControl.config.page.config.projID,
                            EntityName: self.config.currentControl.config.page.config.entityName,
                            FilterName: result,
                            FilterDesc: result,
                            ItemEntityAliasName: self.config.currentControl.config.page.config.entityName
                        };

                        self.config.currentControl.config.page.entity.FilterList.push(smat.globalObject.clone(newObj));

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

            this.config.currentControl.config.page.entity.FilterList.splice(currentIndex, 1);

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
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Filter, smat.dynamics.property.SubOptions);
})();