
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.Filed
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.Filed = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            dragType: "Field",
            reType: ""

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.tool.Filed.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {


        }, toolBuild: function () {

            var self = this;

            //==================Field==============
            var dataFilter = function (item) {

                if (self.config.dragType == "LogicItem") {
                    //return item.IsLogicItem == true;
                }

                if (self.config.dragType == "GroupItem") {
                    return item.IsGroupBy == true;
                }

                return true;
            }

            var fieldData = smat.dynamics.entityFieldTree(this.config.page.config.designer.data, self.config.reType, dataFilter);

            this.config.box.asmatTreeView({
                dataSource: fieldData.items,
                //template: function (dataItem) {
                //    if (dataItem.item.items != undefined) {
                //        return dataItem.item.text;
                //    } else {
                //        return dataItem.item.text + "<img src='/SMAT.UI/images/cursor_drag_hand16.png'><span>→</span>"
                //    }

                //},
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
                                    dataFilter,
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

            this.treeview = this.config.box.data("asmatTreeView");
            this.treeview.expand(this.config.box.find('.s-item:first'));

            this.initDragItem(this.config.box.find(".s-item"))

            //================================

        },
        dragHint: function (dragTarget, item) {

            var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;padding:5px 20px;'></div>");

            var dataItem = dragTarget.options.dataItem;

            var inputElement = $("<span style=''>" + dataItem.text + "</span>").appendTo(hintElement);

            return hintElement;
        },
        dragDataItem: function (dragTarget, item) {

            //var dataItem = dragTarget.options.treeview.dataItem($(item));
            var dataItem = this.treeview.dataItem($(item));

            return dataItem;
        },
        dragChildConfig: function (dragTarget, item) {

            var dataItem = dragTarget.options.dataItem;
            var dataType = "TextBox";
            var childConfig = {
                type: "Field",
                dataType: dataType,
                name: dataItem.FieldName,
                fieldName: dataItem.FieldName,
                label: dataItem.text,
                defaultFieldName: dataItem.FieldName,
                inputBoxClass: "col-fix-1"
            }

            return childConfig;

        },
        dragType: function (dragTarget, item) {
            return this.config.dragType;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.Filed, smat.dynamics.tool.BaseTool);

})();