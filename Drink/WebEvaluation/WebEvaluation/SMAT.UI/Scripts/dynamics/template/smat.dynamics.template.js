
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.BaseTemplate = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.BaseTemplate.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {


        }, toolsBuild: function () {
            var self = this;
            if (this.designerTab) {

                //this.config.page.config.designer.box.remove();
                this.tabBox = $('<div class=""></div>')


                this.config.page.config.designer.verticalBox.closest(".s-window-content").css("overflow-y", "hidden");
                this.config.page.config.designer.verticalBox.before(this.tabBox);

                this.tabUl = $('<ul></ul>').appendTo(this.tabBox);
                for (var i = 0; i < this.designerTab.length; i++) {
                    var tpInfo = this.designerTab[i];

                    if (i == 0) {
                        $('<li key="' + tpInfo.id + '" class="s-state-active">' + tpInfo.text + '</li>').appendTo(this.tabUl);
                    } else {
                        $('<li key="' + tpInfo.id + '">' + tpInfo.text + '</li>').appendTo(this.tabUl);
                    }

                    var boxHeight = this.config.page.config.designer.box.height() - 140;
                    var style = "height:" + boxHeight + "px;padding:0 2px 0 0;border-width:0;";
                    if (tpInfo.height && tpInfo.height != "") {
                        style += "height:" + tpInfo.height.replace("px", "") + "px";
                    }

                    var contentBox = $('<div id="designerTab_' + tpInfo.id + '" style="' + style + '"></div>').appendTo(this.tabBox);

                    if (tpInfo.designMain == "page") {
                        this.config.page.config.designer.verticalBox.appendTo(contentBox);

                        var toolBar = this.config.page.config.designer.leftPane.find('.panelbar');

                        for (var key in tpInfo.tools) {
                            var tool = tpInfo.tools[key];
                            var li = $('<li>' + tool.config.name + '<div><div class="tool-box"></div></div></li>').appendTo(toolBar);
                            tool.config.box = li.find('.tool-box');
                        }

                        toolBar.asmatPanelBar({
                            expandMode: "single"//single -multiple
                        });



                        for (var key in tpInfo.tools) {
                            var tool = tpInfo.tools[key];
                            tool.toolBuild();
                        }

                        var toolBarUi = toolBar.data("asmatPanelBar");
                        toolBarUi.expand(toolBar.find('.s-item:first-child'));
                    } else {
                        var hboxWidth = "180px";
                        var verticalBox = $('<div class="vertical-box" style="height: 100%; width: 100%;padding: 0;margin: 0;"></div>').appendTo(contentBox);

                        var middlePane = $('<div class="middle-pane"></div>').appendTo(verticalBox);
                        var bottomPane = $('<div class="bottom-pane"></div>').appendTo(verticalBox);


                        var horizontalBox = $('<div class="horizontal-box" style="height: 100%; width: 100%;padding: 0;margin: 0;"></div>').appendTo(middlePane);

                        var leftPane = $('<div class="left-pane"></div>').appendTo(horizontalBox);
                        var centerPane = $('<div class="center-pane designer-panel" style="background:none;padding: 0px;"></div>').appendTo(horizontalBox);

                        verticalBox.asmatSplitter({
                            orientation: "vertical",
                            panes: [
                                { collapsible: false },
                                { collapsible: false, resizable: false, size: "30px" }
                            ]
                        });

                        if (tpInfo.notLeftSplitter != true) {
                            
                            if (tpInfo.hboxWidth) {
                                hboxWidth = tpInfo.hboxWidth;
                            }

                            horizontalBox.asmatSplitter({
                                panes: [
                                    { collapsible: true, size: hboxWidth },
                                    { collapsible: false }
                                ]
                            });
                        } else {
                            leftPane.remove();
                            horizontalBox.asmatSplitter({
                                panes: [
                                    { collapsible: false }
                                ]
                            });
                        }

                        

                        //tools build
                        if (tpInfo.tools && tpInfo.tools.length == 1 && tpInfo.notPanelBar == true) {

                            var li = $('<div><div class="tool-box no-drag"></div></div>').appendTo(leftPane);
                            var tool = tpInfo.tools[0];
                            tool.config.box = leftPane.find('.tool-box');
                            tool.config.propertyContainer = centerPane;
                            tool.toolBuild();

                        } else {
                            var toolBar = $('<ul class="panelbar"></ul>').appendTo(leftPane);

                            for (var key in tpInfo.tools) {
                                var tool = tpInfo.tools[key];
                                var li = $('<li id="' + tpInfo.id + '_'+key+'">' + tool.config.name + '<div><div class="tool-box"></div></div></li>').appendTo(toolBar);
                                tool.config.box = li.find('.tool-box');
                            }

                            toolBar.asmatPanelBar({
                                expandMode: "single"//single -multiple
                            });

                            for (var key in tpInfo.tools) {
                                var tool = tpInfo.tools[key];
                                tool.config.propertyContainer = centerPane;
                                tool.toolBuild();
                            }

                            var toolBarUi = toolBar.data("asmatPanelBar");
                            toolBarUi.expand(toolBar.find('.s-item:first-child'));
                            tpInfo.toolBarUi = toolBarUi;
                        }
                    }
                }

                this.tabBox.smatTabStrip({
                    activate: function (e) {
                        var tabs = $.Enumerable.From(self.designerTab).Where("$.id == '" + $(e.item).attr("key") + "'").ToArray();
                        if (tabs.length > 0) {
                            self.activateTab = tabs[0];
                            if (self.onActivtTab) {
                                self.onActivtTab(self.activateTab, $(e.item))
                            }
                            if (tabs[0].designMain == "page") {
                                if (tabs[0].refreshUiName) {
                                    var r = self.config.page.getControlByName(tabs[0].refreshUiName);
                                    if (r) {
                                        r.refresh();
                                    }
                                }

                                self.config.page.activeControl = undefined;
                                self.config.page.active(self.config.page);
                                
                            }

                            for (var key in tabs[0].tools) {
                                var tool = tabs[0].tools[key];
                                if (tool.onActivate) {
                                    tool.onActivate();
                                }
                            }
                        }
                    }
                });
                self.activateTab = self.designerTab[0];

            } else {
                var toolBar = this.config.page.config.designer.leftPane.find('.panelbar');

                for (var key in this.tools) {
                    var tool = this.tools[key];
                    var li = $('<li>' + tool.config.name + '<div><div class="tool-box"></div></div></li>').appendTo(toolBar);
                    tool.config.box = li.find('.tool-box');
                }

                toolBar.asmatPanelBar({
                    expandMode: "single"//single -multiple
                });



                for (var key in this.tools) {
                    var tool = this.tools[key];
                    tool.toolBuild();
                }

                var toolBarUi = toolBar.data("asmatPanelBar");
                toolBarUi.expand(toolBar.find('.s-item:first-child'));
            }
        }, onPageLoad: function () {
            if (this.designerTab) {
                for (var i = 0; i < this.designerTab.length; i++) {
                    var tpInfo = this.designerTab[i];

                    if (tpInfo.designMain != "page") {
                        for (var key in tpInfo.tools) {
                            var tool = tpInfo.tools[key];
                            if (tool.onPageLoad) {
                                tool.onPageLoad();
                            }
                        }
                    }
                }
            }
        }, activateAlltool: function () {
            if (this.designerTab) {
                var tabs = this.designerTab;
                for (var i in tabs) {
                    if (tabs[i] == this.activateTab) {
                        continue;
                    }

                    if (tabs[i].designMain == "page") {
                        if (tabs[i].refreshUiName) {
                            var r = this.config.page.getControlByName(tabs[i].refreshUiName);
                            if (r) {
                                r.refresh();
                            }
                        }

                        this.config.page.activeControl = undefined;
                        this.config.page.active(this.config.page);

                    } else {
                        for (var key in tabs[i].tools) {
                            var tool = tabs[i].tools[key];
                            if (tool.onActivate) {
                                tool.onActivate();
                            }
                        }
                    }
                }
            }
        }, _resetPageObj: function () {
            if (this.designerTab) {
                var tabs = this.designerTab;
                for (var i in tabs) {
                   
                    for (var key in tabs[i].tools) {
                        var tool = tabs[i].tools[key];
                        tool.config.page = this.page;
                    }
                }
            }
        }, setData: function () {
            if (this.designerTab) {
                var tabs = this.designerTab;
                for (var i in tabs) {
                    for (var key in tabs[i].tools) {
                        var tool = tabs[i].tools[key];
                        if (tool.setData) {
                            //setTimeout(function () { tool.setData(); }, 10);
                            tool.setData();
                        }
                    }
                }
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.BaseTemplate, smat.dynamics.Element);

})();