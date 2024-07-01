
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  PropertyCultureText
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.CultureText = function (config) {
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

    smat.dynamics.property.CultureText.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.dynamics.property.CultureText.prototype
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

            var page = self.config.page;
            smat.service.openPage({
                page: {
                    projID: page.config.projID,
                    entityName: "Y_OptionSet",
                    pageName: "OptionSelect"
                },
                width: "1000px",
                title: "label",
                afterClose: function (result) {
                    if (result != undefined) {
                        var dataItem = result.selectedRows;
                        var value = "codeKind:" + dataItem.OptSetName + "." + dataItem.CD;

                        if (self.config.currentCell) self.config.currentCell.children('input').val(value);
                        if (self.config.currentControl) self.config.currentControl.propertyChange(self.config.currentDataItem, value, self.config.valueConfig);
                        if (self.config.currentCell) self.config.currentCell.children('input').focus();
                    }

                }
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.CultureText, smat.dynamics.property.SubOptions);
})();