
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  editer 
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.DropDownList = function (config) {
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

    smat.dynamics.property.DropDownList.prototype = {
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
                this.config.picker = $('<span  class="s-select edit-cell-picker"><span class="s-icon s-i-arrow-s"></span>').appendTo(this.config.currentCell);
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

            this.dropDownListDom = $('<input style="width:100%;position: absolute;top: 0;left: 0;z-index: -1;opacity: 0;"/>').appendTo(this.config.picker.parent());

            var ds = new Array();
            for (var i = 0; i < this.config.currentDataItem.dataSource.length; i++) {
                ds.push({
                    text: this.config.currentDataItem.dataSource[i].text,
                    value: this.config.currentDataItem.dataSource[i].value
                });
            }

            this.dropDownListDom.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: ds,
                index: 0,
                change: function (e) {
                    self.onchange(e);
                }
            });

            this.dropDownList = this.dropDownListDom.ui();

            //this.dropDownListDom.closest('.s-dropdown').hide();

            this.config.picker.bind('click', function (e) {
                self.dropDownList.uiControl.open();
                e.stopPropagation();
            });

            this.restoreValue();
        },
        onchange: function (e) {
            var self = this;
            var value = this.dropDownList.uiControl.value();

            if (self.config.currentCell) self.config.currentCell.children('input').val(value);
            self.config.currentControl.propertyChange(self.config.currentDataItem, value, self.config.valueConfig);
            if (self.config.currentCell) self.config.currentCell.children('input').focus();

        }, restoreValue: function () {
            this.dropDownList.uiControl.value(this.config.currentDataItem.value);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.DropDownList, smat.dynamics.Element);
})();