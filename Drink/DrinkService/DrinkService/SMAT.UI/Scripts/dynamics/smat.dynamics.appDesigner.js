
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  AppDesinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdAppDesigner = function (config) {

        var uiNode = null;
        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            uiNode = new smat.dynamics.AppDesigner(config);
        });
        return uiNode;
    };

    smat.dynamics.AppDesigner = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            toolPanelWidth: "180px"

        });

        this.setConfig(config);


        //初期化
        var result = this.init();

        return result;
    };

    smat.dynamics.AppDesigner.prototype = {
        initPage: function () {
            this.page = new smat.dynamics.mobile.Page({
                projID: this.mainProjID,
                entityName: this.mainEntityName,
                name: this.mainFormName,
                title: this.mainFormDesc,
                contextOn: this.centerPane,
                type: this.mainTemplateType,
                belong: this.config.belong,
                createdBy: this.config.createdBy,
                designer: this,
                designing: true,
                isMobile: this.config.isMobile
            });


            this.page.propertysPanel = this.propertysPanel;

            if (this.config.titleTarget != undefined) {
                this.config.titleTarget.text(this.mainFormName);
            }

            this.page.setEntity(this.data);
        }, openForm: function () {
            var self = this;
            smat.service.openLoding();
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getForm,
                params: {
                    ProjID: this.mainProjID,
                    EntityName: this.mainEntityName,
                    FormName: this.mainFormName
                },
                success: function (form) {
                    //alert(result);
                    self.page.propertysPanel = self.propertysPanel;

                    //if (self.page.config.cssClass) {
                    //    self.page.config.cssClass = self.page.config.cssClass + " " + self.screenSize.value();
                    //} else {
                    //    self.page.config.cssClass = self.screenSize.value();
                    //}

                    self.page.body.addClass(self.screenSize.value());

                    self.page.setForm(form);

                    if (self.template.onPageLoad) {
                        self.template.onPageLoad();
                    }

                    //container control get
                    var csDs = self.getContainerControlDs();

                    self.containerControlTreeview.setDataSource(csDs);
                }
            });
        }, getContainerControlDs: function () {
            var self = this;
            //container control get
            var cs = self.page.getControlByTypes(["Layout", "View"]);
            var csDs = [];
            if (cs) {
                for (var key in cs) {
                    csDs.push({
                        text: cs[key].config.name,
                        key: cs[key].config.name,
                        nodeIcon: smat.dynamics.BusinessSearchSetting["SetListSearch"].icon
                    })
                }
            }

            return csDs;
        }, afterInitDom: function () {
            var self = this;
            var topToolBar = this.centerPane.find(".sm-flat");

            var screenSizeBox = $('<div style="position: absolute;top: 0;"></div>').appendTo(topToolBar);
            var sizeTypeInput = $('<input />').appendTo(screenSizeBox);
            sizeTypeInput.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "<span class='sm-text' title='phone-v'><i class='fa fa-mobile-phone'></i></span>", value: "s-screen-phone-v" },
                    { text: "<span class='sm-text' title='phone-h'><i class='fa fa-mobile-phone rotate90'></i></span>", value: "s-screen-phone-h" },
                    { text: "<span class='sm-text' title='pad-v'><i class='fa fa fa-tablet'></i></span>", value: "s-screen-pad-v" },
                    { text: "<span class='sm-text' title='pad-h'><i class='fa fa fa-tablet rotate90'></i></span>", value: "s-screen-pad-h" },
                    { text: "<span class='sm-text' title='pc'><i class='icon-screen-desktop'></i></span>", value: "s-screen-pc" }
                ],
                change: function (e) {
                    self.page.body.removeClass(function (index, css) {
                        return (css.match(/\bs-screen-\S+/g) || []).join(' ');
                    });
                    self.page.body.addClass(self.screenSize.value());
                }
            });

            this.screenSize = sizeTypeInput.ui();


            var containerControlBox = $('<div style="position: absolute;top: 80px; left:20px;text-align: center;"></div>').appendTo(topToolBar);

            this.addContainerBtn = $('<button class="btn-primary">add view</button>').appendTo(containerControlBox);

            this.containerControlTreebox = $("<div class='list-tree'>").asmatTreeView({
                template: function (data) {
                    return "<img key='" + data.item.key + "' class='' style ='' src='" + data.item.nodeIcon + "'/><br /><span style='width: 60px;display: inline-block;'>" + data.item.text + "</span>";
                },
                dataSource: [],
                select: function (e) {
                    var dataItem = self.containerControlTreeview.dataItem(e.node);
                    var ctl = self.page.getControlByName(dataItem.key);
                    if (ctl) {
                        ctl.active();
                        if (ctl instanceof smat.dynamics.mobile.Layout) {
                            self.page.app.navigate("#");
                        } else {

                            self.page.app.navigate("#" + ctl.getUiId());
                        }
                    }
                }
            }).appendTo(containerControlBox);

            this.addContainerBtn.smatButton({
                click: function (e) {
                    
                    smat.service.getUserConfig({
                        title: smat.service.optionSet("SysMsg.Confirm"),
                        items: [
                            {
                                key: "containerType",
                                title: "type",
                                type: "ButtonGroup",
                                dataTextField: "text",
                                dataValueField: "value",
                                dataSource: [
                                    { text: "View", value: "View" },
                                    { text: "Drawer", value: "Drawer" }
                                ]

                            }
                        ],
                        callback: function (result) {
                            self.page.addChild({
                                type: result.containerType,
                                page: self.page,
                                rowsCount: 2,
                                name: result.containerType,
                                designing: true
                            });

                                //container control get
                                var csDs = self.getContainerControlDs();
                                self.containerControlTreeview.setDataSource(csDs);
                        },
                        check: function (items) {

                        }
                    })

                }
            });

            this.containerControlTreeview = this.containerControlTreebox.data("asmatTreeView");
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.AppDesigner, smat.dynamics.Designer);

})();