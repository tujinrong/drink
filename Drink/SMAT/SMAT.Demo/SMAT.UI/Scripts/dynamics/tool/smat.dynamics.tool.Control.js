
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.Control
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.Control = function (config) {
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
    smat.dynamics.tool.Control.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

            var items = [
                {
                    text: "Button",
                    controlType: "Button"
                },
                {
                    text: "Chart",
                    controlType: "Chart"
                },
                {
                    text: "Field",
                    controlType: "Field"
                },
                {
                    text: "Form",
                    controlType: "Form"
                },
                {
                    text: "Div",
                    controlType: "Div"
                },
                {
                    text: "Grid",
                    controlType: "Grid"
                },
                {
                    text: "Page",
                    controlType: "PageControl"
                },
                {
                    text: "TabStrip",
                    controlType: "TabStrip"
                },
                {
                    text: "ToolBar",
                    controlType: "ToolBar"
                },
                {
                    text: "CheckBox",
                    controlType: "CheckBox"
                },
                {
                    text: "RadioBox",
                    controlType: "RadioBox"
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

            var controlType = dataItem.controlType;
            var dataType = "TextBox";
            var tempConfig = {};

            switch (controlType) {
                case "Button":
                    dataType = "Button";
                    inputElement = $("<button class='btn-primary'>Button</button>").appendTo(hintElement);
                    break;
                case "Chart":
                    dataType = "Chart";
                    inputElement.appendTo(hintElement);
                    tempConfig = {
                    }
                    break;
                case "Field":
                    dataType = "TextBox";
                    inputElement = $("<input />").appendTo(hintElement);
                    tempConfig = {
                        dataType: "TextBox",
                        label: {
                            text: "Input",
                            attributes: {
                                style: "text-align:right; padding-right:5px;"
                            }
                        },
                        inputBox: {
                            attributes: {
                                'class': "col-fix-1"
                            }
                        }
                    }
                    break;
                case "Form":
                    dataType = "Form";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Div":
                    dataType = "Div";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Div":
                    dataType = "Div";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "PageControl":
                    dataType = "PageControl";
                    inputElement = $("<div style='width:300px;height:140px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "RadioBox":
                    dataType = "RadioBox";
                    inputElement = $("<input ></input>").appendTo(hintElement);
                    tempConfig = {
                        text: "RadioBox"
                    }
                    break;
                case "CheckBox":
                    dataType = "CheckBox";
                    inputElement = $("<input ></input>").appendTo(hintElement);
                    tempConfig = {
                        text: "CheckBox"
                    }
                    break;
                case "Grid":
                    dataType = "Grid";
                    inputElement.appendTo(hintElement);
                    var dataSource = [
                        { "colTemp": "　　" },
                        { "colTemp": "　　" },
                        { "colTemp": "　　" },
                        { "colTemp": "　　" },
                        { "colTemp": "　　" }
                    ];

                    var columns = [
                        {
                            field: "colTemp",
                            title: "　　",
                            width: "400px",
                        }
                    ]
                    tempConfig = {
                        dataSource: dataSource,
                        columns: columns
                    }
                    break;
                case "TabStrip":
                    dataType = "TabStrip";
                    inputElement = $("<div style='width:300px;height:240px;'/>");
                    inputElement.appendTo(hintElement);
                    
                    tempConfig = {
                        type: "TabStrip",
                    }
                    tempConfig.tabPages = new Array();
                    tempConfig.tabPages.push({
                        uuid: smat.service.uuid(),
                        title: "tab1",
                        height: "200"
                    });

                    tempConfig.tabPages.push({
                        uuid: smat.service.uuid(),
                        title: "tab2",
                        height: "200"
                    });


                    var ul = $('<ul></ul>').appendTo(inputElement);
                    for (var i = 0; i < tempConfig.tabPages.length; i++) {
                        var tpInfo = tempConfig.tabPages[i];

                        if (i == 0) {
                            $('<li class="s-state-active">' + tpInfo.title + '</li>').appendTo(ul);
                        } else {
                            $('<li>' + tpInfo.title + '</li>').appendTo(ul);
                        }

                        var style = "";
                        if (tpInfo.height && tpInfo.height != "") {
                            style += "height:" + tpInfo.height.replace("px", "") + "px";
                        }
                        $('<div dy-tab-uuid="' + tpInfo.uuid + '" style="' + style + '"></div>').appendTo(inputElement);

                    }

                    break;
                case "ToolBar":
                    dataType = "ToolBar";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                default:
                    break;
            }

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

            switch(controlType)
            {
                case "Button":
                    childConfig = {
                        type: "Button",
                        name: "button1",
                        text: "Button",
                        cssClass: "btn-primary"
                    }
                    break;
                case "Chart":
                    childConfig = {
                        type: "Chart",
                        name: "chart1"
                    }
                    break;
                case "Field":
                    childConfig = {
                        type: "Field",
                        dataType: "TextBox",
                        name: "input1",
                        label: "Input",
                        //defaultFieldName: dataItem.FieldName,
                        inputBoxClass: "col-fix-1"
                    }
                    break;
                case "Form":
                    childConfig = {
                        type: "Form",
                        name: "search_form",
                        rowsCount: 2
                    }
                    break;
                case "Div":
                    childConfig = {
                        type: "Div",
                        name: "div1",
                        rowsCount: 2
                    }
                    break;
                case "PageControl":
                    childConfig = {
                        type: "PageControl",
                        entity: this.config.page.config.entityName,
                        name: "page1"
                    }
                    break;
                case "RadioBox":
                    childConfig = {
                        type: "RadioBox",
                        name: "radioBox1",
                        text: "RadioBox"
                    }
                    break;
                case "CheckBox":
                    childConfig = {
                        type: "CheckBox",
                        name: "checkBox1",
                        text: "CheckBox"
                    }
                    break;
                case "Grid":

                    var columns = [
                        {
                            field: "colTemp",
                            title: "　　"
                        }
                    ]

                    childConfig = {
                        type: "Grid",
                        name: "grid1",
                        columns: columns
                    }
                    break;
                case "TabStrip":
                    childConfig = {
                        type: "TabStrip",
                        name: "tabStrip1"
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
    smat.globalObject.extend(smat.dynamics.tool.Control, smat.dynamics.tool.BaseTool);

})();