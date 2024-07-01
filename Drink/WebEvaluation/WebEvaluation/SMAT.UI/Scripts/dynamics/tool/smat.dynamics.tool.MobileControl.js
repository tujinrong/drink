
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.MobileControl
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.MobileControl = function (config) {
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
    smat.dynamics.tool.MobileControl.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

            var items = [
                //{
                //    text: "Tabstrip",
                //    controlType: "Tabstrip"
                //},
                //{
                //    text: "Layout",
                //    controlType: "Layout"
                //}
                //,
                {
                    text: "Navbar",
                    controlType: "Navbar"
                },
                {
                    text: "ListView",
                    controlType: "ListView"
                },
                {
                    text: "Button",
                    controlType: "Button"
                },
                {
                    text: "Label",
                    controlType: "Label",
                    notMobile: true
                },
                {
                    text: "Div",
                    controlType: "Div",
                    notMobile: true
                },
                {
                    text: "Chart",
                    controlType: "Chart",
                    notMobile: true
                },
                {
                    text: "Tag",
                    controlType: "Tag",
                    notMobile: true
                },
                {
                    text: "Field",
                    controlType: "Field"
                }
            ]

            this.treebox = $("<div>").asmatTreeView({
                dataSource: items
            }).appendTo(this.config.box);

            this.treeview = this.treebox.data("asmatTreeView");

            this.treeview.expand(this.treebox.find('.s-item'));

            this.initDragItem(this.config.box.find(".s-item:not([aria-expanded])"));

        }, dragHint: function (dragTarget, item) {

            var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;'></div>");

            var inputElement = $("<div />");

            var dataItem = dragTarget.options.dataItem;

            var controlType = dataItem.controlType;
            var dataType = "TextBox";
            var tempConfig = {};

            switch (controlType) {
                case "Tabstrip":
                    dataType = "Tabstrip";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break; 
                case "Navbar":
                    dataType = "Navbar";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Layout":
                    dataType = "Layout";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "ListView":
                    dataType = "ListView";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Button":
                    dataType = "Button";
                    inputElement = $("<div style='width:100px;height:32px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Field":
                    dataType = "Field";
                    inputElement = $("<div style='width:100px;height:32px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Label":
                    dataType = "Label";
                    inputElement = $("<label />").appendTo(hintElement);
                    tempConfig = {
                        label: {
                            text: "label"
                        }
                    }
                    break;

                case "Div":
                    dataType = "Div";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Chart":
                    dataType = "Chart";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Tag":
                    dataType = "Tag";
                    inputElement = $("<div style='width:60px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                default:
                    break;
            }

            if (dataItem.notMobile &&  smat[dataType] != undefined) {
                tempConfig.target = inputElement;

                if (dataType == "TreeView") {
                    var ui = new smat[dataType](tempConfig);
                    ui.expand(".s-item");
                } else if (dataType == "Editor") {

                } else if (dataType == "Location") {

                } else {
                    new smat[dataType](tempConfig);
                }
            }

            return hintElement;
        },
        dragDataItem: function (dragTarget, item) {

            var dataItem = this.treeview.dataItem($(item));

            return dataItem;
        },
        dragChildConfig: function (dragTarget, item) {

            var dataItem = dragTarget.options.dataItem;
            var controlType = dataItem.controlType;
            var childConfig = {};

            switch(controlType)
            {
                case "Tabstrip":
                    childConfig = {
                        type: "Tabstrip",
                        name: "tabstrip"
                    }
                    break;
                case "Navbar":
                    childConfig = {
                        type: "Navbar",
                        name: "navbar"
                    }
                    break;
                case "Layout":
                    childConfig = {
                        type: "Layout",
                        name: "layout"
                    }
                    break;
                case "ListView":
                    childConfig = {
                        type: "ListView",
                        name: "listView"
                    }
                    break;
                case "Button":
                    childConfig = {
                        type: "Button",
                        name: "button"
                    }
                    break;
                case "Field":
                    childConfig = {
                        type: "Field",
                        dataType: "text",
                        name: "input",
                        label: "Input"
                    }
                    break;
                case "Label":
                    childConfig = {
                        type: "Label",
                        name: "label",
                        label: "label",
                        notMobile: true
                    }
                    break;

                case "Div":
                    childConfig = {
                        type: "Div",
                        name: "div",
                        rowsCount: 2,
                        notMobile: true
                    }
                case "Chart":
                    childConfig = {
                        type: "Chart",
                        name: "chart",
                        notMobile: true
                    }
                    break;
                case "Tag":
                    childConfig = {
                        type: "Tag",
                        name: "tag",
                        notMobile: true
                    }
                    break;
                default:
                    break;
            }
            
            return childConfig;

        },
        dragType: function (dragTarget, item) {
            return "Control";
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.MobileControl, smat.dynamics.tool.BaseTool);

})();