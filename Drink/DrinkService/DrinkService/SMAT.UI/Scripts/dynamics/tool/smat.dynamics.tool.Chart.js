
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.Chart
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.Chart = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    /**
    * 初期化
    * @name init
    * @methodOf smat.Control.prototype
    */
    smat.dynamics.tool.Chart.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

            var items = [
                {
                    text: "線",
                    controlType: "line"
                },
                {
                    text: "エリア",
                    controlType: "area"
                },
                {
                    text: "柱状",
                    controlType: "column"
                },
                {
                    text: "バー",
                    controlType: "bar"
                },
                {
                    text: "パイ",
                    controlType: "pie"
                }
            ]

            this.treebox = $("<div>").asmatTreeView({
                //template: function (data) {
                //    return data.item.ControlControlDesc + "<span class='s-icon s-i-pencil' style ='height: 18px; margin-left: 5px;'>Edit</span>";
                //},
                dataSource: items
            }).appendTo(this.config.box);

            this.treeview = this.treebox.data("asmatTreeView");

            this.treeview.expand(this.treebox.find('.s-item'));

            this.initDragItem(this.config.box.find(".s-item:not([aria-expanded])"));

        }, dragHint: function (dragTarget, item) {

            var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;'></div>");

            var inputElement = $("<div />");

            var dataItem = dragTarget.options.dataItem;

            var dataType = "Chart";
            var controlType = dataItem.controlType;
            inputElement.appendTo(hintElement);
            var tempConfig = {
                chartType: controlType
            };

            

            if (smat[dataType] != undefined) {
                tempConfig.target = inputElement;

                new smat[dataType](tempConfig);
            }

            return hintElement;
        },
        dragDataItem: function (dragTarget, item) {

            //var dataItem = dragTarget.options.treeview.dataItem($(item));
            var dataItem = this.treeview.dataItem($(item));

            return dataItem;
        },
        dragChildConfig: function (dragTarget, item) {

            var dataItem = dragTarget.options.dataItem;
            var controlType = dataItem.controlType;
            var childConfig = {};

            childConfig = {
                type: "Chart",
                name: "chart1",
                chartType: controlType,
                view: this.config.page.defaultView
            }
            
            return childConfig;

        },
        dragType: function (dragTarget, item) {
            return "Control";
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.Chart, smat.dynamics.tool.BaseTool);

})();