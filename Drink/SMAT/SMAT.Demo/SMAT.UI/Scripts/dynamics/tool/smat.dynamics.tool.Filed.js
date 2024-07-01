
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.Filed
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.Filed = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            dragType: "Field"

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

           
        },toolBuild:function(){

            var self = this;

            //==================Field==============
            var fieldData = smat.dynamics.entityFieldTree(this.config.page.config.designer.data, function (item) {

                if (self.config.dragType == "LogicItem") {
                    return item.IsLogicItem == true;
                }

                return true;
            });

            this.config.box.asmatTreeView({
                dataSource: [fieldData]
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