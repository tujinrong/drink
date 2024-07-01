
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
                     text: "Editor",
                     controlType: "Editor"
                 },
                {
                    text: "Label",
                    controlType: "Label"
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
                    text: "Fieldset",
                    controlType: "Fieldset"
                },
                {
                    text: "Tag",
                    controlType: "Tag"
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
                    text: "PanelBar",
                    controlType: "PanelBar"
                },
                {
                    text: "ToolBar",
                    controlType: "ToolBar"
                },
                {
                    text: "Pager",
                    controlType: "Pager"
                }
                ,
                
                {
                    text: "TreeView",
                    controlType: "TreeView"
                },
                {
                    text: "CheckBox",
                    controlType: "CheckBox"
                },
                {
                    text: "RadioBox",
                    controlType: "RadioBox"
                },
                {
                    text: "Upload",
                    controlType: "Upload"
                },
                {
                    text: "ResourceImg",
                    controlType: "ResourceImg"
                },
                {
                    text: "Location",
                    controlType: "Location"
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
                case "Editor":
                    dataType = "Editor";
                    inputElement = $('<table style="width:600px;" cellspacing="4" cellpadding="0" class="s-widget s-editor s-header s-editor-widget" role="presentation"><tbody><tr role="presentation"><td class="s-editor-toolbar-wrap" role="presentation"><ul class="s-editor-toolbar" role="toolbar" aria-controls="M_Client_客户一览画面9999_editor1" data-role="editortoolbar"><li class="s-tool-group" role="presentation"><span class="s-editor-dropdown s-group-start s-group-end"><span class="s-widget s-dropdown s-header s-editor-widget" unselectable="on" role="listbox" aria-haspopup="true" aria-expanded="false" tabindex="0" aria-owns="" aria-disabled="false" aria-readonly="false" aria-busy="false" style="width: 110px;"><span unselectable="on" class="s-dropdown-wrap s-state-default"><span unselectable="on" class="s-input">Format</span><span unselectable="on" class="s-select"><span unselectable="on" class="s-icon s-i-arrow-s">select</span></span></span><select title="Format" class="s-formatting s-decorated" data-role="selectbox" unselectable="on" style="width: 110px; display: none;"><option value="p" selected="selected">Paragraph</option><option value="blockquote">Quotation</option><option value="h1">Heading 1</option><option value="h2">Heading 2</option><option value="h3">Heading 3</option><option value="h4">Heading 4</option><option value="h5">Heading 5</option><option value="h6">Heading 6</option></select></span></span></li><li class="s-tool-group s-button-group" role="presentation"><a href="" role="button" class="s-tool s-group-start" unselectable="on" title="Bold" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-bold">Bold</span></a><a href="" role="button" class="s-tool" unselectable="on" title="Italic" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-italic">Italic</span></a><a href="" role="button" class="s-tool s-group-end" unselectable="on" title="Underline" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-underline">Underline</span></a></li><li class="s-tool-group s-button-group" role="presentation"><a href="" role="button" class="s-tool s-group-start" unselectable="on" title="Align text left" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-justifyLeft">Justify Left</span></a><a href="" role="button" class="s-tool" unselectable="on" title="Center text" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-justifyCenter">Justify Center</span></a><a href="" role="button" class="s-tool s-group-end" unselectable="on" title="Align text right" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-justifyRight">Justify Right</span></a></li><li class="s-tool-group s-button-group" role="presentation"><a href="" role="button" class="s-tool s-group-start" unselectable="on" title="Insert unordered list" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-insertUnorderedList">Insert unordered list</span></a><a href="" role="button" class="s-tool" unselectable="on" title="Insert ordered list" aria-pressed="false"><span unselectable="on" class="s-tool-icon s-insertOrderedList">Insert ordered list</span></a><a href="" role="button" class="s-tool s-group-end" unselectable="on" title="Indent"><span unselectable="on" class="s-tool-icon s-indent">Indent</span></a><a href="" role="button" class="s-tool s-group-end s-state-disabled" unselectable="on" title="Outdent" style="display: none;"><span unselectable="on" class="s-tool-icon s-outdent">Outdent</span></a></li><li class="s-tool-group s-button-group" role="presentation"><a href="" role="button" class="s-tool s-group-start" unselectable="on" title="Insert hyperlink"><span unselectable="on" class="s-tool-icon s-createLink">Create Link</span></a><a href="" role="button" class="s-tool s-state-disabled" unselectable="on" title="Remove hyperlink" style="display: none;"><span unselectable="on" class="s-tool-icon s-unlink">Remove Link</span></a><a href="" role="button" class="s-tool s-group-end" unselectable="on" title="Insert image"><span unselectable="on" class="s-tool-icon s-insertImage">Insert Image</span></a></li><li class="s-tool-group s-button-group" role="presentation"><a href="" role="button" class="s-tool s-group-start s-group-end" data-popup="" unselectable="on" title="Create table"><span unselectable="on" class="s-tool-icon s-createTable">Create table</span></a><a href="" role="button" class="s-tool s-state-disabled" unselectable="on" title="Add column on the left" style="display: none;"><span unselectable="on" class="s-tool-icon s-addColumnLeft">Add column on the left</span></a><a href="" role="button" class="s-tool s-state-disabled" unselectable="on" title="Add column on the right" style="display: none;"><span unselectable="on" class="s-tool-icon s-addColumnRight">Add column on the right</span></a><a href="" role="button" class="s-tool s-state-disabled" unselectable="on" title="Add row above" style="display: none;"><span unselectable="on" class="s-tool-icon s-addRowAbove">Add row above</span></a><a href="" role="button" class="s-tool s-state-disabled" unselectable="on" title="Add row below" style="display: none;"><span unselectable="on" class="s-tool-icon s-addRowBelow">Add row below</span></a><a href="" role="button" class="s-tool s-state-disabled" unselectable="on" title="Delete row" style="display: none;"><span unselectable="on" class="s-tool-icon s-deleteRow">Delete row</span></a><a href="" role="button" class="s-tool s-group-end s-state-disabled" unselectable="on" title="Delete column" style="display: none;"><span unselectable="on" class="s-tool-icon s-deleteColumn">Delete column</span></a></li></ul></td></tr><tr><td class="s-editable-area"><iframe class="s-content" ></iframe><textarea style="display: none;" ></textarea></td></tr></tbody></table>');
                    inputElement.appendTo(hintElement);
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
                case "Label":
                    dataType = "Label";
                    inputElement = $("<label />").appendTo(hintElement);
                    tempConfig = {
                        label: {
                            text: "label"
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
                case "Fieldset":
                    dataType = "Div";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "Tag":
                    dataType = "Tag";
                    inputElement = $("<div style='width:60px;height:30px;'/>");
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
                case "Upload":
                    dataType = "Upload";
                    inputElement = $("<input ></input>").appendTo(hintElement);
                    tempConfig = {
                        text: "Upload"
                    }
                    break;
                case "ResourceImg":
                    dataType = "ResourceImg";
                    inputElement = $("<input ></input>").appendTo(hintElement);
                    tempConfig = {
                        text: "ResourceImg"
                    }
                    break;
                case "Location":
                    dataType = "Location";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
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
                case "PanelBar":
                    dataType = "PanelBar";
                    inputElement = $("<ul style='width:300px;'/>");
                    inputElement.appendTo(hintElement);

                    tempConfig = {
                        type: "PanelBar",
                    }
                    tempConfig.panelPages = new Array();
                    tempConfig.panelPages.push({
                        uuid: smat.service.uuid(),
                        title: "panel1",
                        height: "60"
                    });

                    tempConfig.panelPages.push({
                        uuid: smat.service.uuid(),
                        title: "panel2",
                        height: "100"
                    });


                    for (var i = 0; i < tempConfig.panelPages.length; i++) {
                        var tpInfo = tempConfig.panelPages[i];

                        var li = null;
                        if (i == 0) {
                            li = $('<li class="s-state-active">' + tpInfo.title + '</li>').appendTo(inputElement);
                        } else {
                            li = $('<li>' + tpInfo.title + '</li>').appendTo(inputElement);
                        }

                        var style = "";
                        if (tpInfo.height && tpInfo.height != "") {
                            style += "height:" + tpInfo.height.replace("px", "") + "px";
                        }
                        $('<div dy-panel-uuid="' + tpInfo.uuid + '" style="' + style + '"></div>').appendTo(li);

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
                case "Pager":
                    dataType = "Pager";
                    inputElement = $("<div style='width:300px;height:40px;'/>");
                    inputElement.appendTo(hintElement);
                    break;
                case "TreeView":
                    dataType = "TreeView";
                    inputElement.appendTo(hintElement);
                    var dataSource = [
                        {
                            text: "Node1", items: [
                              { text: "Node11" },
                              { text: "Node12" },
                              { text: "Node13" }
                            ]
                        },
                        {
                            text: "Node2", items: [
                              { text: "Node21" },
                              { text: "Node22" },
                              { text: "Node23" }
                            ]
                        }
                    ]
                    tempConfig = {
                        dataSource: dataSource
                    }

                    break;
                default:
                    break;
            }

            if (smat[dataType] != undefined) {
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
                        name: "button",
                        text: "Button",
                        cssClass: "btn-primary"
                    }
                    break;
                case "Chart":
                    childConfig = {
                        type: "Chart",
                        name: "chart"
                    }
                    break;
                case "Editor":
                    childConfig = {
                        type: "Editor",
                        name: "editor"
                    }
                    break;
                case "Upload":
                    childConfig = {
                        type: "Upload",
                        name: "upload"
                    }
                    break;
                case "ResourceImg":
                    childConfig = {
                        type: "ResourceImg",
                        name: "resourceImg"
                    }
                    break;
                case "Location":
                    childConfig = {
                        type: "Location",
                        name: "location"
                    }
                    break;
                case "Field":
                    childConfig = {
                        type: "Field",
                        dataType: "TextBox",
                        name: "input",
                        label: "Input",
                        //defaultFieldName: dataItem.FieldName,
                        inputBoxClass: "col-fix-1"
                    }
                    break;
                case "Label":
                    childConfig = {
                        type: "Label",
                        name: "label",
                        label: "label"
                    }
                    break;
                case "Form":
                    childConfig = {
                        type: "Form",
                        name: "form",
                        rowsCount: 2
                    }
                    break;
                case "Div":
                    childConfig = {
                        type: "Div",
                        name: "div",
                        rowsCount: 2
                    }
                    break;
                case "Fieldset":
                    childConfig = {
                        type: "Div",
                        name: "div",
                        legend: "Fieldset",
                        rowsCount: 2
                    }
                    break;
                case "Tag":
                    childConfig = {
                        type: "Tag",
                        name: "tag"
                    }
                    break;
                case "PageControl":
                    childConfig = {
                        type: "PageControl",
                        entity: this.config.page.config.entityName,
                        name: "page"
                    }
                    break;
                case "RadioBox":
                    childConfig = {
                        type: "RadioBox",
                        name: "radioBox",
                        text: "RadioBox"
                    }
                    break;
                case "CheckBox":
                    childConfig = {
                        type: "CheckBox",
                        name: "checkBox",
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
                        name: "grid",
                        columns: columns
                    }
                    break;
                case "TabStrip":
                    childConfig = {
                        type: "TabStrip",
                        name: "tabStrip"
                    }
                    break;
                case "PanelBar":
                    childConfig = {
                        type: "PanelBar",
                        name: "panelBar"
                    }
                    break;
                case "ToolBar":
                    childConfig = {
                        type: "ToolBar",
                        name: "toolBar"
                    }
                    break;
                case "Pager":
                    childConfig = {
                        type: "Pager",
                        name: "pager"
                    }
                    break; 
                case "TreeView":
                    childConfig = {
                        type: "TreeView",
                        name: "treeView"
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