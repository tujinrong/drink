
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Handler
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.Handler = function (config) {
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

    smat.dynamics.property.Handler.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.dynamics.property.Handler.prototype
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
                    entityName: "Y_Org",
                    pageName: "OrgRefer"
                },
                params:{
                    selectedList: self.config.currentDataItem.value
                },
                width: "1000px",
                title: "Handler",
                afterClose: function (result) {
                    if (result != undefined) {
                        var selectedList = result;
                        var handlers = new Array();
                        for (var key in selectedList) {

                            handlers.push({
                                KeyCD: selectedList[key]["KeyCD"],
                                OrgCD: selectedList[key]["OrgCD"],
                                ProjID: self.config.page.config.projID,
                                OrgType: selectedList[key]["OrgType"],
                                FlowName: self.config.currentControl.activeNode.dataItem.FlowName,
                                NodeName: self.config.currentControl.activeNode.dataItem.NodeName
                            });
                        }

                        if (self.config.currentCell) self.config.currentCell.children('input').val(handlers);
                        if (self.config.currentControl) self.config.currentControl.propertyChange(self.config.currentDataItem, handlers, self.config.valueConfig);
                        if (self.config.currentCell) self.config.currentCell.children('input').focus();
                    }

                }
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Handler, smat.dynamics.property.SubOptions);
})();