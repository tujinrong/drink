
(function () {
    ///////////////////////////////////////////////////////////////////////
    //  PropertySubOptions 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.SubOptions = function (config) {
        //默认属性
        this.setConfig({
            picker: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.SubOptions.prototype = {
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
                this.config.picker = $('<span  class="s-select edit-cell-picker"><span class="s-icon s-i-pencil"></span>').appendTo(this.config.currentCell);
            }
            
            this.config.picker.attr('dy-uuid', this.uuid);
        },
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            this.initEditer();

            var self = this;

            if (this.config.currentDataItem != undefined)
            {
                this.config.dataSource = this.config.valueConfig[this.config.currentDataItem.id]
            }

            this.config.picker.bind('click', function (e) {
                //alert(self.config.currentDataItem.value)
                self.open();
                e.stopPropagation();
            });

            
        },
        onchange: function (e) {
            var input = this.config.picker.parent().children('input');
            input.focus();
            input.val(this.dropDownList.uiControl.value());
        }, restoreValue: function () {
            //this.dropDownList.uiControl.value(this.config.currentDataItem.value);
        }, open: function () {
            var self = this;
            var box = $('<section id="' + this.uuid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 420px;"></section>');
            var leftBox = $('<div class="col-sm-5" style="margin: 0;padding: 0;height: 100%;position: relative;"></div>').appendTo(box);
            var rightBox = $('<div class="col-sm-7" style="margin: 0;padding: 0;height: 100%;"></div>').appendTo(box);

            var leftTop = $('<div style="height:20px;">Items</div>').appendTo(leftBox);
            var objectBox = $('<div style="padding-right: 40px;padding-bottom: 50px;"></div>').appendTo(leftBox);
            var btnUpDownBox = $('<div style="position: absolute;top: 10px; right: 0;width: 40px;text-align: center;padding: 10px 0;"></div>').appendTo(leftBox);
            var btnNewDelBox = $('<div style="position: absolute;bottom: 10px; left: 0;width: 90%;padding: 0 8px;"></div>').appendTo(leftBox);

            this.btnUp = $('<button class="btn-primary " style="padding: 8px 5px;margin-bottom: 5px;">↑</button>').appendTo(btnUpDownBox);
            this.btnDown = $('<button class="btn-primary " style="padding: 8px 5px;">↓</button>').appendTo(btnUpDownBox);

            this.btnNew = $('<button class="btn-primary s-button" style="margin-right: 5px;">new</button>').appendTo(btnNewDelBox);
            this.btnDel = $('<button class="btn-danger " style="">delete</button>').appendTo(btnNewDelBox);

            this.objectTree = $('<div style="height: 350px;border: 1px solid #ccc;"></div>').appendTo(objectBox);

            var rightTop = $('<div style="height:20px;">Propertys</div>').appendTo(rightBox);
            var propertyBox = $('<div style=";border: 1px solid #ccc;"></div>').appendTo(rightBox);
            var btnOkCancelBox = $('<div style="text-align:right; position: absolute;bottom: 10px; left: 0;width: 90%;padding: 0 8px;"></div>').appendTo(rightBox);

            var infoBox = $('<div style=";border: 1px solid #ccc;position: absolute;bottom: 47px; left: 0;width: 100%;padding: 0; height:30px;"></div>').appendTo(rightBox);
            this.btnOk = $('<button class="btn-primary s-button" style="">ok</button>').appendTo(btnOkCancelBox);
            //this.btnCancel = $('<button class="btn-danger s-button" style="margin-left: 5px;">cancel</button>').appendTo(btnOkCancelBox);

            var propertyGrid = $('<div style="height: 316px;overflow: auto;"></div>').appendTo(propertyBox);

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

            this.btnDel.smatButton({
                click: function () {
                    self.delNode();
                }
            });
            this.btnNew.smatButton({
                click: function () {
                    self.newNode();
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
                title: self.config.currentDataItem.id,
                afterClose: function (result) {
                    //if (self.config.currentControl["propertyChange_" + self.config.currentDataItem.id] != undefined) {
                    //    self.config.currentControl["propertyChange_" + self.config.currentDataItem.id](self.config.currentDataItem,self.valueConfig, self.config.dataSource)
                    //}
                    self.config.currentControl.propertyChange(self.config.currentDataItem, self.config.dataSource,self.config.valueConfig);
                    if (self.config.currentCell) self.config.currentCell.children('input').val(self.config.dataSource);
                }
            });

            this.propertysPanel = new smat.dynamics.property.Panel({
                target: propertyGrid,
                page: this.config.page,
                valueConfig: this.config.valueConfig,
                afterEndEdit: function (item) {
                    if (item.id == self.config.currentDataItem.titleKey) {
                        var title = smat.service.cultureText(self.valueConfig[self.config.currentDataItem.titleKey]);
                        $(self.node).find('span').text(title);
                    }
                    
                }
            });

            //data
            var datas = this.getTreeViewData();

            this.objectTree.attr('dy-uuid', this.uuid);

            this.objectTree.asmatTreeView({
                dataSource: datas,
                select: function (e) {
                   
                    var editer = $(e.sender.wrapper).dynamicsUI();
                    editer.setProInfo($(e.node));
                    //alert("Selecting", data.uuid);
                }
            });
            this.tree = this.objectTree.data('asmatTreeView');
            if (this.config.currentDataItem.value != undefined && this.config.currentDataItem.value.length > 0) {
                //默认选中第一个
                this.tree.select(this.objectTree.find('.s-item:first'));
                this.setProInfo(this.objectTree.find('.s-item:first'));
            } else {
                this.setButtonEnable();
            }
            

        },setProInfo:function(node){
            var data = this.tree.dataItem(node).data;
            var propertys = this.config.currentDataItem.optionConfig;

            this.valueConfig = smat.service.getItemByKey(this.config.dataSource, "uuid", data.uuid);

            this.propertysPanel.clear();
            this.propertysPanel.setCurrentControl(this.config.currentControl, propertys, this.valueConfig);

            this.node = node;

            this.setButtonEnable();
        }, setButtonEnable: function () {

            if (this.config.currentDataItem.value == undefined || this.config.currentDataItem.value.length < 2) {
                this.btnUp.ui().enable(false);
                this.btnDown.ui().enable(false);
            } else if (this.node != undefined && this.node.index() == 0) {
                this.btnUp.ui().enable(false);
                this.btnDown.ui().enable(true);
            } else if (this.node != undefined && this.node.index() == this.config.currentDataItem.value.length - 1) {
                this.btnUp.ui().enable(true);
                this.btnDown.ui().enable(false);
            }
            else{
                this.btnUp.ui().enable(true);
                this.btnDown.ui().enable(true);
            }

            if (this.config.currentDataItem.value == undefined || this.config.currentDataItem.value.length == 0) {
                this.btnDel.ui().enable(false);
            }

        }, getTreeViewData: function () {

            var datas = new Array();

            if (this.config.currentDataItem.value != "" && this.config.currentDataItem.value != undefined) {
                for (var i = 0; i < this.config.dataSource.length; i++) {
                    var item = smat.globalObject.clone(this.config.dataSource[i]);
                    datas.push({
                        text: smat.service.cultureText(item[this.config.currentDataItem.titleKey]),
                        data: item
                    });
                }
            }

            return datas;

        }, moveUpDown: function (step) {
            var currentIndex = this.node.index();

            var newIndex = currentIndex + step;

            var temp = this.config.currentDataItem.value[newIndex];
            this.config.currentDataItem.value[newIndex] = this.config.currentDataItem.value[currentIndex];
            this.config.currentDataItem.value[currentIndex] = temp;

            var temp = this.config.dataSource[newIndex];
            this.config.dataSource[newIndex] = this.config.dataSource[currentIndex];
            this.config.dataSource[currentIndex] = temp;


            var data = this.getTreeViewData();
            this.tree.setDataSource(data);

            this.node = this.objectTree.find('.s-item').eq(newIndex);
            this.tree.select(this.node);
            this.setButtonEnable();
        }, newNode: function () {
            var newIndex = 0;
            if (this.config.currentDataItem.value != undefined) { 
                newIndex = this.config.currentDataItem.value.length;
            }
            
            var newObj ={}
            newObj[this.config.currentDataItem.titleKey] = "new_" + this.config.currentDataItem.titleKey;
            newObj.uuid = smat.service.uuid();

            if (this.config.currentDataItem.value == undefined || this.config.currentDataItem.value == "") {
                this.config.currentDataItem.value = new Array();
            }
            this.config.currentDataItem.value.push(newObj);

            if (this.config.dataSource == undefined ) {
                this.config.dataSource = new Array();
            }

            if (this.config.currentDataItem.value != this.config.dataSource) {
                this.config.dataSource.push(smat.globalObject.clone(newObj));
            }


            var data = this.getTreeViewData();
            this.tree.setDataSource(data);

            this.node = this.objectTree.find('.s-item').eq(newIndex);
            this.tree.select(this.node);
            this.setProInfo(this.node);

            this.setButtonEnable();

        }, delNode: function () {
            var currentIndex = this.node.index();
            var newIndex = currentIndex==0?0: currentIndex - 1;

            this.config.currentDataItem.value.splice(currentIndex, 1);

            this.config.dataSource.splice(currentIndex, 1);


            var data = this.getTreeViewData();
            this.tree.setDataSource(data);

            if (newIndex >= 0 && this.config.currentDataItem.value.length > 0) {
                this.node = this.objectTree.find('.s-item').eq(newIndex);
                this.tree.select(this.node);
                this.setProInfo(this.node);
            } else {
                this.propertysPanel.clear();
            }
            

            this.setButtonEnable();
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.SubOptions, smat.dynamics.Element);

})();