
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.View
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.View = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.tool.View.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

           
        },toolBuild:function(){

            var self = this;

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getViewList,
                params: {
                    ProjID: this.config.page.entity.ProjID,
                    EntityName: this.config.page.entity.EntityName
                },
                success: function (result) {

                    var items = new Array();
                    for (var key in result) {
                        var item = smat.globalObject.clone(result[key]);
                        item.text = item.ViewName;
                        items.push(item);
                    }

                    if (self.config.page != undefined && self.config.page.editViewList != undefined) {
                        for (var vKey in self.config.page.editViewList) {
                            var item = smat.globalObject.clone(self.config.page.editViewList[vKey]);
                            item.text = item.ViewName;
                            items.push(item);
                        }
                    }

                    self.config.box.asmatTreeView({
                        dataSource: items
                    });

                    self.treeview = self.config.box.data("asmatTreeView");
                    self.treeview.expand(self.config.box.find('.s-item'));

                    self.initDragItem(self.config.box.find(".s-item:not([aria-expanded])"));

                }
            });
        },
        dragHint: function (dragTarget, item) {

            this.config.page.dragTarget

            var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;padding:10px 20px;'>" + this.config.page.dragTarget.options.dataItem.ViewName + "</div>");

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
                name: dataItem.FilterControlName,
                label: dataItem.text,
                //defaultFieldName: dataItem.FieldName,
                filter: dataItem.FilterControlName,
                inputBoxClass: "col-fix-1"
            }

            return childConfig;

        },
        dragType: function (dragTarget, item) {
            return "View";
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.View, smat.dynamics.tool.BaseTool);

})();