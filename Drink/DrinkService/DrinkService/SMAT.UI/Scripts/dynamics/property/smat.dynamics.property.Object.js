
(function () {
    ///////////////////////////////////////////////////////////////////////
    //  PropertyObject 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Object = function (config) {
        //默认属性
        this.setConfig({
            picker: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.Object.prototype = {
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
            
            var rightTop = $('<div style="height:20px;">Propertys</div>').appendTo(rightBox);
            var propertyBox = $('<div style=";border: 1px solid #ccc;"></div>').appendTo(rightBox);
            var btnOkCancelBox = $('<div style="text-align:right; position: absolute;bottom: 10px; left: 0;width: 90%;padding: 0 8px;"></div>').appendTo(rightBox);

            var infoBox = $('<div style=";border: 1px solid #ccc;position: absolute;bottom: 47px; left: 0;width: 100%;padding: 0; height:30px;"></div>').appendTo(rightBox);
            this.btnOk = $('<button class="btn-primary s-button" style="">ok</button>').appendTo(btnOkCancelBox);
            this.btnClear = $('<button class="btn-danger s-button" style="margin-left: 5px;">clear</button>').appendTo(btnOkCancelBox);

            //this.btnCancel = $('<button class="btn-danger s-button" style="margin-left: 5px;">cancel</button>').appendTo(btnOkCancelBox);

            var propertyGrid = $('<div style="height: 316px;overflow: auto;"></div>').appendTo(propertyBox);

            
            this.btnOk.smatButton({
                click: function () {
                    smat.service.closeForm({
                        contentId: self.uuid
                    });
                }
            });

            this.btnClear.smatButton({
                click: function () {
                    self.config.dataSource = undefined;
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
            this.setProInfo();
            

        },setProInfo:function(){
            var propertys = this.config.currentDataItem.optionConfig;

            if (this.config.dataSource == undefined) this.config.dataSource = {};
            
            this.propertysPanel.clear();
            this.propertysPanel.setCurrentControl(this.config.currentControl, propertys, this.config.dataSource);

            this.node = node;

            this.setButtonEnable();
        }, setButtonEnable: function () {

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Object, smat.dynamics.Element);

})();