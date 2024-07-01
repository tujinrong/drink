
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  PanelBar
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.PanelBar = function (config) {
        //默认属性
        this.setConfig({
            type: "Grid"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        var zIndex = this.config.page.skinZindex;
        this.config.page.skinZindex = zIndex + 1;

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        if (this.config.designing == true && this.editSkinBody != undefined) {

            this.fillPanelHandleBox();
        }

        var es = $(this.body).children(".s-state-active").children("div[dy-panel-uuid]");
        if (this.editSkinBox) {
            this.editSkinBox.detach();
            this.editSkinBox.appendTo(es);
        }
        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.PanelBar.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {
            var self = this;
            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) {
                contextOn = $(this.config.contextOn)
            } else {
                this.config.contextOn = contextOn;
            }
            this.children = new smat.hashMap();

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            this.body = $('<ul id="' + this.getUiId() + '" class="' + this.designClass + ' ' + cssClassStr + ' col-drop" style="' + styleStr + '"></ul>').appendTo(this.config.contextOn);

            var newtab = false;
            if (this.config.panelPages == undefined || this.config.panelPages.length == 0) {
                this.config.panelPages = new Array();

                this.config.panelPages.push({
                    uuid: smat.service.uuid(),
                    title: "panel1",
                    height: "200"
                });

                this.config.panelPages.push({
                    uuid: smat.service.uuid(),
                    title: "panel2",
                    height: "200"
                });

                newtab = true;
            }


            for (var i = 0; i < this.config.panelPages.length; i++) {
                var tpInfo = this.config.panelPages[i];

                var li = null;
                if (i == 0) {
                    li = $('<li class="s-state-active">' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.body);
                } else {
                    li = $('<li >' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.body);
                }

                var style = "";
                if (tpInfo.height && tpInfo.height != "") {
                    style += "height:" + tpInfo.height.replace("px","")+"px";
                }
                $('<div dy-panel-uuid="' + tpInfo.uuid + '" style="' + style + '"></div>').appendTo(li);

                if (newtab == true) {
                    this.addChild({
                        uuid: tpInfo.uuid,
                        tabTitle: smat.service.cultureText(tpInfo.title),
                        type: "TabPage",
                        name: smat.service.cultureText(tpInfo.title) + "_page",
                        rowsCount: 2
                    });
                }
            }

            var uiConfig = this.getUiConfig();

            if (this.config.designing == true) {
                uiConfig.activate = function (e) {
                    var es = $(self.body).children(".s-state-active").children("div[dy-panel-uuid]");
                    if (self.editSkinBox) {
                        self.editSkinBox.detach();
                        self.editSkinBox.appendTo(es);
                    }
                }
            }

            this.uiControl = new smat.PanelBar(uiConfig);

            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }
        },
        addChild: function (config) {
            //designing
            config.designing = this.config.designing;

            //page: this.page,
            config.page = this.config.page;

            config.seq = this.children.length + 1;

            var panelUuid = config.uuid;


            var contextOn = this.body.find("div[dy-panel-uuid=" + panelUuid + "]");
            if (contextOn.length == 0) {
                $('<li >' + config.tabTitle + '</li>').appendTo(this.body);
                contextOn = $('<div dy-panel-index="' + panelUuid + '" style="height:400px;"></div>').appendTo(this.body);
            }
            
            config.parent = this;
            config.contextOn = contextOn;

            var child = new smat.dynamics[config.type](config);
            this.children.set(child.uuid, child);

            child.editSkinBody.attr('dy-uuid', child.uuid);
            this.fillPanelHandleBox();
            return child;
        }, fillPanelHandleBox: function () {
            if (this.config.designing == true && this.editSkinBody != undefined) {

                var self = this;
                var lis = this.body.children('li');
                $(self.body).find("div[panel-index]").remove();
                var index = 0;
                $.each(lis, function (e) {
                    var hb = $('<div panel-index="' + index + '" style="z-index:' + (self.config.page.skinZindex+2) + ';float:left;position: absolute;top:0;background: #fff;opacity: 0;cursor: pointer;height:40px;border: 1px solid #000;width:100%;"></div>');
                    $(this).css('position', 'relative');
                    hb.appendTo($(this));
                    hb.bind("click", function (e) {
                        self.uiControl.uiControl.expand(self.body.children(":eq(" + Number($(this).attr('panel-index')) + ")"));
                        self.active();
                        e.stopPropagation();
                    });
                    index++;
                });
            }
           

        }, onHint: function (hintElement, dragTarget, item) {
            if (1 == 1) {
                e.preventDefault();
            } else {
                hintElement.removeClass('col-drop');
            }
            
        }, onDragstart: function (e) {
            if (1 == 1) {
                e.preventDefault();
            } else {
                this.body.removeClass('col-drop');
            }
            
        }, onDragend: function (e) {
            if (1 == 1) {
                e.preventDefault();
            } else {
                this.body.addClass('col-drop');
            }
            
        }, checkPropertyChanging: function (property, value) {
            var isOk = true;

            return isOk;
        }, getCustomPropertyConfig: function () {

            this.editPropertyConfig.push(
            {
                group: 'PanelBar',
                caption: 'panelPages',
                type: 'SubOptions',
                id: 'panelPages',
                cmt: 'panelPages',
                propType: "prop",
                titleKey: "title",
                optionConfig: [
                    {
                        group: 'data',
                        caption: 'title',
                        type: 'text',
                        id: 'title',
                        cmt: 'title',
                        propType: "prop"
                    }, {
                        group: 'data',
                        caption: 'height',
                        type: 'text',
                        id: 'height',
                        cmt: 'height',
                        propType: "prop"
                    }
                ]
            });

            this.editPropertyConfig.push(
            {
                group: 'PanelBar',
                caption: 'stepTab',
                type: 'DropDownList',
                id: 'stepTab',
                cmt: 'stepTab',
                propType: "prop",
                dataSource: [
                    {
                        text: " ",
                        value: undefined
                    },
                    {
                        text: "true",
                        value: "true"
                    },
                    {
                        text: "false",
                        value: "false"
                    }
                ]
            });
            
            this.editPropertyConfig.push(
			         {
			             group: 'base',
			             caption: 'activate',
			             type: 'Logic',
			             id: 'activate',
			             cmt: 'activate',
			             eventKey: 'PanelBar_activate',
			             propType: "event"
			         }
                );
        },
        propertyChange_panelPages: function (property, value) {
            
            this.refresh();
        }, refresh: function (isResetProperty) {
            this.uiControl.destroy();

            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var temp = $('<ul id="' + this.getUiId() + '" class="' + this.designClass + ' ' + cssClassStr + ' col-drop" style="' + styleStr + '"></ul>');

            this.body.replaceWith(temp);
            this.body.remove();

            this.body = temp;

            this.tabHeaderSkinBox = undefined;
            var zIndex = this.config.page.skinZindex;
            this.config.page.skinZindex = zIndex + 1;
            var childControls = this.getSaveControlsTree();
            this.children = new smat.hashMap();

            for (var i = 0; i < this.config.panelPages.length; i++) {
                var tpInfo = this.config.panelPages[i];

                var li = null;
                if (i == 0) {
                    li = $('<li class="s-state-active">' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.body);
                } else {
                    li = $('<li >' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.body);
                }

                var style = "";
                if (tpInfo.height && tpInfo.height != "") {
                    style += "height:" + tpInfo.height.replace("px", "") + "px";
                }
                $('<div dy-panel-uuid="' + tpInfo.uuid + '" style="' + style + '"></div>').appendTo(li);

                
                var isNew = true;
                for (var key in childControls) {
                    var cConfig = smat.service.strToJson(childControls[key].ControlOptions);
                    if (cConfig.uuid == tpInfo.uuid) {
                        isNew = false;
                        break;
                    }
                }
                if (isNew == true) {
                    this.addChild({
                        uuid: tpInfo.uuid,
                        tabTitle: smat.service.cultureText(tpInfo.title),
                        type: "TabPage",
                        name: smat.service.cultureText(tpInfo.title) + "_page",
                        rowsCount: 2
                    });
                }
            }

            //del
            var tempCs = new Array();
            for (var key in childControls) {
                var cConfig = smat.service.strToJson(childControls[key].ControlOptions);

                var has = false;
                for (var i = 0; i < this.config.panelPages.length; i++) {
                    var tpInfo = this.config.panelPages[i];
                    if (tpInfo.uuid == cConfig.uuid) {
                        has = true;
                        break;
                    }
                    
                }
                if (has == true) {
                    tempCs.push(childControls[key]);
                }
            }
            childControls = tempCs;

            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.PanelBar(uiConfig);
            this.editSkinBody = this.body;

            //设计器初期化
            this.initEditSkin();

            //Event初期化
            this.iniEvent();

            if (this.config.page.activeControl == this) {
                if (isResetProperty == true) {
                    this.editPropertyConfig = undefined;
                    this.config.page.propertysPanel.clear();
                    this.config.page.propertysPanel.setCurrentControl(this, this.getPropertyConfig(), this.config);
                }
                
                this.editSkinBox.addClass("edit-skin-box-active");
                this.editSkinBody.children('.edit-skin-zoom-box').show();
                if (this.shortcutMenu) {
                    this.shortcutMenu.show();
                }
            }

            if (this.config.designing == true && this.editSkinBody != undefined) {

                this.fillPanelHandleBox();
            }

            var es = $(this.body).children(".s-state-active").children("div[dy-panel-uuid]");
            if (this.editSkinBox) {
                this.editSkinBox.detach();
                this.editSkinBox.appendTo(es);
            }

            //addChild
            this.setFormData(childControls);

            this.config.page.modalClear();

        },
        propertyChange_stepTab: function (property, value) {
            this.refresh();
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.PanelBar, smat.dynamics.Field);
})();