
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  TabStrip
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.TabStrip = function (config) {
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
            var h = this.ul.height();
            this.tabHeaderSkinBox = $('<div  style=" z-index:' + zIndex + ';height:' + (h) + 'px;position: absolute;top: -1px;left: -1px;width: 100%;"></div>');

            this.editSkinBox.css("z-index", zIndex);
            this.editSkinBox.css("top", (h + 1) + "px");
            this.editSkinBox.css("height", "calc(100% - " + (h + 2) + "px)");

            this.editSkinBox.before(this.tabHeaderSkinBox);

            this.fillTabHandleBox();
        }
        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.TabStrip.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

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

            this.body = $('<div id="' + this.getUiId() + '" class="' + this.designClass + ' ' + cssClassStr + ' col-drop" style="' + styleStr + '"></div>').appendTo(this.config.contextOn);

            var newtab = false;
            if (this.config.tabPages == undefined || this.config.tabPages.length == 0) {
                this.config.tabPages = new Array();

                this.config.tabPages.push({
                    uuid: smat.service.uuid(),
                    title: "tab1",
                    height: "400"
                });

                this.config.tabPages.push({
                    uuid: smat.service.uuid(),
                    title: "tab2",
                    height: "400"
                });

                newtab = true;
            }


            this.ul = $('<ul></ul>').appendTo(this.body);
            for (var i = 0; i < this.config.tabPages.length; i++) {
                var tpInfo = this.config.tabPages[i];

                if (i == 0) {
                    $('<li class="s-state-active">' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.ul);
                } else {
                    $('<li>' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.ul);
                }

                var style = "";
                if (tpInfo.height && tpInfo.height != "") {
                    style += "height:" + tpInfo.height.replace("px","")+"px";
                }
                $('<div dy-tab-uuid="' + tpInfo.uuid + '" style="' + style + '"></div>').appendTo(this.body);

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

            uiConfig.tabWork = this.config.page.config.tabWork;

            this.uiControl = new smat.TabStrip(uiConfig);

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

            var tabUuid = config.uuid;


            var contextOn = this.body.children("div[dy-tab-uuid=" + tabUuid + "]");
            if (contextOn.length == 0) {
                $('<li>' + config.tabTitle + '</li>').appendTo(this.ul);
                contextOn = $('<div dy-tab-index="' + tabUuid + '" style="height:400px;"></div>').appendTo(this.body);
            }
            
            config.parent = this;
            config.contextOn = contextOn;

            var child = new smat.dynamics[config.type](config);
            this.children.set(child.uuid, child);

            child.editSkinBody.attr('dy-uuid', child.uuid);
            this.fillTabHandleBox();
            return child;
        }, fillTabHandleBox: function () {
            var self = this;
            if (this.tabHeaderSkinBox != undefined) {
                this.tabHeaderSkinBox.children().remove();
                var lis = this.ul.children('li');
                var index = 0;
                $.each(lis, function (e) {
                    var w = $(this).width();
                    var hb = $('<div tab-index="' + index + '" style="float:left;background: #fff;opacity: 0;cursor: pointer;height:100%;border: 1px solid #000;width:' + w + 'px;"></div>');
                    hb.appendTo(self.tabHeaderSkinBox);
                    hb.bind("click", function (e) {
                        self.uiControl.uiControl.select(Number($(this).attr('tab-index')));
                        e.stopPropagation();
                    });
                    index++;
                });

                this.tabHeaderSkinBox.bind('click', function (e) {
                    self.active();
                    e.stopPropagation();
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
                group: 'tabStrip',
                caption: 'tabPages',
                type: 'SubOptions',
                id: 'tabPages',
                cmt: 'tabPages',
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
                    }, {
                        group: 'data',
                        caption: 'icon',
                        type: 'text',
                        id: 'icon',
                        cmt: 'icon',
                        propType: "prop"
                    }
                ]
            });

            this.editPropertyConfig.push(
            {
                group: 'tabStrip',
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
			             eventKey: 'tabStrip_activate',
			             propType: "event"
			         }
                );
        },
        propertyChange_tabPages: function (property, value) {
            
            this.refresh();
        }, refresh: function (isResetProperty) {
            this.uiControl.destroy();

            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var temp = $('<div id="' + this.getUiId() + '" class="' + this.designClass + ' ' + cssClassStr + ' col-drop" style="' + styleStr + '"></div>');

            this.body.replaceWith(temp);
            this.body.remove();

            this.body = temp;

            this.tabHeaderSkinBox = undefined;
            var zIndex = this.config.page.skinZindex;
            this.config.page.skinZindex = zIndex + 1;
            var childControls = this.getSaveControlsTree();
            this.children = new smat.hashMap();

            this.ul = $('<ul></ul>').appendTo(this.body);
            for (var i = 0; i < this.config.tabPages.length; i++) {
                var tpInfo = this.config.tabPages[i];

                if (i == 0) {
                    $('<li class="s-state-active">' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.ul);
                } else {
                    $('<li>' + smat.service.cultureText(tpInfo.title) + '</li>').appendTo(this.ul);
                }

                var style = "";
                if (tpInfo.height && tpInfo.height != "") {
                    style += "height:" + tpInfo.height.replace("px", "") + "px";
                }
                $('<div dy-tab-uuid="' + tpInfo.uuid + '" style="' + style + '"></div>').appendTo(this.body);

                
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
                for (var i = 0; i < this.config.tabPages.length; i++) {
                    var tpInfo = this.config.tabPages[i];
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

            this.uiControl = new smat.TabStrip(uiConfig);
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
                var h = this.ul.height();
                this.tabHeaderSkinBox = $('<div  style=" z-index:' + zIndex + ';height:' + (h) + 'px;position: absolute;top: -1px;left: -1px;width: 100%;"></div>');

                this.editSkinBox.css("z-index", zIndex);
                this.editSkinBox.css("top", (h + 1) + "px");
                this.editSkinBox.css("height", "calc(100% - " + (h + 2) + "px)");

                this.editSkinBox.before(this.tabHeaderSkinBox);

                this.fillTabHandleBox();
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
    smat.globalObject.extend(smat.dynamics.TabStrip, smat.dynamics.Field);
})();