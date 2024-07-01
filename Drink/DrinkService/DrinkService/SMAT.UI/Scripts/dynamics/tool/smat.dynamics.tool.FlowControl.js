
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.FlowControl
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.FlowControl = function (config) {
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
    smat.dynamics.tool.FlowControl.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

            var items = [
                {
                    text: smat.dynamics.diagram.FlowTypeSetting["0"].nodeDesc,
                    nodeType: "0",
                    nodeIcon: smat.dynamics.diagram.FlowTypeSetting["0"].nodeIcon
                },
                {
                    text: smat.dynamics.diagram.FlowTypeSetting["1"].nodeDesc,
                    nodeType: "1",
                    nodeIcon: smat.dynamics.diagram.FlowTypeSetting["1"].nodeIcon
                },
                {
                    text: smat.dynamics.diagram.FlowTypeSetting["2"].nodeDesc,
                    nodeType: "2",
                    nodeIcon: smat.dynamics.diagram.FlowTypeSetting["2"].nodeIcon
                },
                {
                    text: smat.dynamics.diagram.FlowTypeSetting["3"].nodeDesc,
                    nodeType: "3",
                    nodeIcon: smat.dynamics.diagram.FlowTypeSetting["3"].nodeIcon
                },
                {
                    text: smat.dynamics.diagram.FlowTypeSetting["5"].nodeDesc,
                    nodeType: "5",
                    nodeIcon: smat.dynamics.diagram.FlowTypeSetting["5"].nodeIcon
                },
                {
                    text: smat.dynamics.diagram.FlowTypeSetting["7"].nodeDesc,
                    nodeType: "7",
                    nodeIcon: smat.dynamics.diagram.FlowTypeSetting["7"].nodeIcon
                },
                {
                    text: smat.dynamics.diagram.FlowTypeSetting["99"].nodeDesc,
                    nodeType: "99",
                    nodeIcon: smat.dynamics.diagram.FlowTypeSetting["99"].nodeIcon
                }
            ]

            this.treebox = $("<div>").asmatTreeView({
                template: function (data) {
                    return "<img class='' style ='' src='" + data.item.nodeIcon + "'/>";
                },
                dataSource: items
            }).appendTo(this.config.box);

            this.treeview = this.treebox.data("asmatTreeView");

            this.treeview.expand(this.treebox.find('.s-item'));

            this.treebox.find('.s-item').css('padding', '0')

            this.initDragItem(this.config.box.find(".s-item:not([aria-expanded])"));

        }, dragHint: function (dragTarget, item) {

            var hintElement = $(dragTarget.element[0]).clone();

            hintElement.find('.s-state-focused').removeClass('s-state-focused');
            hintElement.find('.s-state-hover').removeClass('s-state-hover');

            var dataItem = dragTarget.options.dataItem;

            inputElement = $("<div style='width:100%;text-align:center'>" + dataItem.text + "</div>").appendTo(hintElement);


            return hintElement;
        },
        dragDataItem: function (dragTarget, item) {

            //var dataItem = dragTarget.options.treeview.dataItem($(item));
            var dataItem = this.treeview.dataItem($(item));

            return dataItem;
        },
        dragChildConfig: function (dragTarget, item) {

            var dataItem = dragTarget.options.dataItem;
           
            return dataItem;

        },
        dragType: function (dragTarget, item) {
            return "Control";
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.FlowControl, smat.dynamics.tool.BaseTool);

})();