
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  FormAuth
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.FormAuth = function (config) {
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

    smat.dynamics.property.FormAuth.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.dynamics.property.FormAuth.prototype
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
            debugger;
            var page = self.config.page;
            var currentDataItem = self.config.currentControl.activeNode.dataItem;

            var EntityName = self.config.page.config.entityName;
            if (currentDataItem.FormName) {
                EntityName = currentDataItem.FormName.split("/")[0];
            }

            smat.service.openPage({
                page: {
                    projID: page.config.projID,
                    entityName: "Y_Flow",
                    pageName: "FlowFormAuth"
                },
                params: {
                    FlowName: currentDataItem.FlowName,
                    NodeName: currentDataItem.NodeName,
                    EntityName: EntityName,
                    formFieldAuthData: self.config.currentDataItem.value
                },
                width: "80%",
                title: "FormAuth",
                afterClose: function (result) {
                    if (result != undefined) {

                        if (self.config.currentCell) self.config.currentCell.children('input').val(result);
                        if (self.config.currentControl) self.config.currentControl.propertyChange(self.config.currentDataItem, result, self.config.valueConfig);
                        if (self.config.currentCell) self.config.currentCell.children('input').focus();
                    }

                }
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.FormAuth, smat.dynamics.property.SubOptions);
})();