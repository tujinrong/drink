
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Control
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Control = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.Control.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

        },/**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        initCommon: function () {


            var self = this;

            this.uuid = smat.service.uuid();
            smat.dynamics.uiMap.set(this.uuid, this);

        },/**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        initEditSkin: function () {

            var self = this;

            if (this.config.designing == true && this.editSkinBody != undefined) {

                var zIndex = this.config.page.skinZindex;
                this.config.page.skinZindex = zIndex + 1;
                var boxStyle = (this.editSkinBoxStyle) ? this.editSkinBoxStyle : ""
                this.editSkinBox = $('<div id="skin_' + this.uuid + '" class="edit-skin-box" style="' + boxStyle + ' z-index:' + zIndex + ';"><div class="edit-skin-box-fill"></div></div>');
                //this.editSkinBody.find(':first').before(this.editSkinBox);
                this.editSkinBox.appendTo(this.editSkinBody);

                this.editSkinBox.attr('dy-uuid', this.uuid);

                if (this.config.tooltip != undefined) {
                    this.editSkinBox.asmatTooltip({
                        content: this.config.tooltip,
                        showOn: "mouseenter",
                        show: function () {
                            //$('.s-callout').css('left', '20px');
                        },
                        position: "top"
                    });
                }

                //zoomBox
                //$('<div class="edit-skin-zoom-box zoom-box-lt" style="display:none; z-index:' + zIndex + ';"></div>').appendTo(this.editSkinBody);
                //$('<div class="edit-skin-zoom-box zoom-box-rt" style="display:none; z-index:' + zIndex + ';"></div>').appendTo(this.editSkinBody);
                //$('<div class="edit-skin-zoom-box zoom-box-rb" style="display:none; z-index:' + zIndex + ';"></div>').appendTo(this.editSkinBody);
                //$('<div class="edit-skin-zoom-box zoom-box-lb" style="display:none; z-index:' + zIndex + ';"></div>').appendTo(this.editSkinBody);

                this.editSkinBox.bind('click', function (e) {
                    self.active();
                    e.stopPropagation();
                });

                //Shortcut menu
                this.initShortcutMenu();
                //ContextMenu
                this.initContextMenu();

                this.initCustomEditSkin();
            }

        },/**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        initShortcutMenu: function () {

            var self = this;
            //データ抽出 不要
            return;

            //Shortcut menu
            var sConfig = this.getShortcutPropertyConfig();
            if (sConfig.length > 0) {
                this.editSkinBody.find('.design-shortcutMenu').remove();
                this.shortcutMenu = $('<div class="design-shortcutMenu s-animation-container" style="width: 500px; margin-left: -2px; padding-left: 2px; padding-right: 2px; padding-bottom: 4px; overflow: visible; display: none; position: absolute; top: -44px; z-index: 10002; right: 0px; box-sizing: content-box;"><div class="s-widget s-tooltip s-popup s-group s-reset s-state-border-up  property-shortcut"  style="display: block; opacity: 1; position: absolute;"><div class="s-tooltip-content"></div><div class="s-callout s-callout-s" style="left: 31px;"></div></div></div>');
                for (var key in sConfig) {
                    var cdataItem = sConfig[key];
                    var citem = $('<span class="property-shortcut-item s-select edit-cell-picker" ><span class="s-icon s-i-pencil"></span>' + cdataItem.caption + '</span>');
                    //citem.bind('click', function (e) {
                    //    alert(3);
                    //});

                    if (smat.dynamics.property[cdataItem.type] != undefined) {
                        this.shortcutMenu.find('.s-tooltip-content').append(citem);

                        new smat.dynamics.property[cdataItem.type]({
                            currentControl: this,
                            currentDataItem: cdataItem,
                            valueConfig: this.config,
                            picker: citem,
                            page: this.config.page
                        });
                    }
                }
                this.shortcutMenu.css('width', "100%");
                this.shortcutMenu.appendTo(this.editSkinBody);
                this.shortcutMenu.show();
                var sWidth = this.shortcutMenu.find('.s-tooltip-content').width() + 20
                this.shortcutMenu.css('width', (sWidth) + "px");
                this.shortcutMenu.hide();
            }

        },/**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        initContextMenu: function (target) {
            var contextMenuTarget = "#skin_" + this.uuid;
            if (target) contextMenuTarget = target;
            var self = this;

            //ContextMenu
            var cConfig = this.getContextMenuConfig();
            if (cConfig.length > 0) {
                this.editSkinBody.find('.design-ContextMenu').remove();
                this.contextMenu = $('<ul class="design-ContextMenu " style="display:none;"></ul>');
                
                this.contextMenu.asmatContextMenu({
                    target: contextMenuTarget,
                    dataSource: cConfig,
                    select: function (e) {
                        var i = $(e.item).find(".menu-item");
                        var c = i.dynamicsUI();

                        c.menuAction(i.attr("menu-key"));
                    }
                })
            }

        },
        initCustomEditSkin: function () {


        }, getPropertyConfig: function () {

            if (this.editPropertyConfig != undefined) {
                return this.editPropertyConfig;
            }

            this.editPropertyConfig = [
			    {
			        group: 'base',
			        caption: 'name',
			        type: 'text',
			        id: 'name',
			        cmt: 'name',
			        propType: "prop"
			    }, {
			        group: 'base',
			        caption: 'enable',
			        type: 'DropDownList',
			        id: 'enable',
			        cmt: 'enable',
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
			    }, {
			        group: 'base',
			        caption: 'visible',
			        type: 'DropDownList',
			        id: 'visible',
			        cmt: 'visible',
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
			    }, {
			        group: 'html attribute',
			        caption: 'htmlName',
			        type: 'text',
			        id: 'htmlName',
			        cmt: 'htmlName',
			        propType: "prop"
			    }, {
			        group: 'html attribute',
			        caption: 'cssClass',
			        type: 'text',
			        id: 'cssClass',
			        cmt: 'cssClass',
			        propType: "prop"
			    }, {
			        group: 'html attribute',
			        caption: 'style',
			        type: 'text',
			        id: 'style',
			        cmt: 'style',
			        propType: "prop"
			    }, {
			        group: 'html attribute',
			        caption: 'htmlAttribute',
			        type: 'text',
			        id: 'htmlAttribute',
			        cmt: 'htmlAttribute',
			        propType: "prop"
			    }
            ];

            if (this.getCustomPropertyConfig != undefined) {
                this.getCustomPropertyConfig(this.editPropertyConfig);
            }

            smat.service.fillKeyPath(this.editPropertyConfig, "id", "", "optionConfig");

            return this.editPropertyConfig;
        },
        /**
           * Shortcut
           * @name getShortcutPropertyConfig
           * @methodOf 
           */
        getShortcutPropertyConfig: function () {

            if (this.shortcutConfig != undefined) {
                return this.shortcutConfig;
            }

            var pConfig= this.getPropertyConfig();

            this.shortcutConfig = new Array();

            for (var key in pConfig) {
                if (pConfig[key].shortcutMenu == true) {
                    this.shortcutConfig.push(pConfig[key]);
                }
            }

            return this.shortcutConfig;
        },
        getUiConfig:function(){
            var uiConfig = smat.globalObject.clone(this.config, this.optionIgnoreInfo);
            uiConfig.target = this.body;
            uiConfig.dynamics = true;
            uiConfig.ProjID = this.config.page.config.projID;
            uiConfig.EntityName = this.config.page.config.entityName;
            uiConfig.PageName = this.config.page.config.name;
            uiConfig.PageId = "page_" + this.config.page.uuid;
            uiConfig.pageParams = this.config.page.config.pageParams;

            if (this.config.designing == true && (uiConfig.visible == "false" || uiConfig.visible == false)) {
                uiConfig.visible = undefined;
                //this.body.css("opacity", "0.2");
                this.body.css("background-color", "#F2F2F2");
            }

            return uiConfig;
        },
        /**
           * Shortcut
           * @name getContextMenuConfig
           * @methodOf 
           */
        getContextMenuConfig: function () {

            //if (this.contextMenuConfig != undefined) {
            //    return this.contextMenuConfig;
            //}

            this.contextMenuConfig = [];

            if (this.getCustomContextMenuConfig != undefined) {
                this.getCustomContextMenuConfig(this.contextMenuConfig);
            }

            if (this.config.noDel != true) {
                this.contextMenuConfig.push({
                    group: 'base',
                    text: '<span class="menu-item" dy-uuid="' + this.uuid + '" menu-key="del">削除</span>',
                    encoded: false,
                    id: 'del',
                    cmt: 'del'
                });
            }

            if (this.cBool(this.config.visible) == false) {
                this.contextMenuConfig.push({
                    group: 'base',
                    text: '<span class="menu-item" dy-uuid="' + this.uuid + '" menu-key="visible_true">表示</span>',
                    encoded: false,
                    id: 'visible_true',
                    cmt: 'visible_true'
                });
            }else {
                this.contextMenuConfig.push({
                    group: 'base',
                    text: '<span class="menu-item" dy-uuid="' + this.uuid + '" menu-key="visible_false">非表示</span>',
                    encoded: false,
                    id: 'visible_false',
                    cmt: 'visible_false'
                });
            }

            return this.contextMenuConfig;
        }, trigger: function (options, params) {
            var result = undefined;
            if (options != undefined && options != null) {
                var page = new smat.pagerSender({
                    dynamics: true,
                    EntityName: this.config.entityName,
                    ProjID: this.config.projID,
                    PageName: this.config.name,
                    PageId: "page_" + this.uuid,
                    parentPageId: this.config.parentPageId,
                    pageParams: this.config.pageParams
                });

                var paramStr = "";
                if (arguments.length > 1) {
                    for (var i = 1; i < arguments.length; i++) {
                        if (i == 1) {
                            paramStr = "arguments[1]";
                        } else {
                            paramStr += ",arguments[" + i + "]";
                        }
                    }

                }
                if (paramStr == "") { paramStr = "page"; } else { paramStr += ",page"; }

                var eve = {};
                if (params) {
                    eve.sender = params.sender;
                    eve.dataItem = params.dataItem;
                    eve.item = params.item;
                    eve.page = page;
                    eve.data = params.data;
                    eve.node = params.node;
                    eve.result = params.result;
                }

                if (typeof (options) == "function") {
                    return eval("options(" + paramStr + ")");
                } else if (typeof (options) == "object") {
                    if (smat.dynamics.logicHandler) {
                        var p = eve.sender ?"eve": paramStr;
                        return eval("smat.dynamics.logicHandler.fillLogic(options).action(" + p + ")");
                    }
                } 
            }

            return result;
        },
           getUiId: function () {
               var id = "page_" + this.config.page.uuid + "_" + this.config.name;
               return (this.config.designing == true) ? "Desing_" + id : id;
        },
        active: function () {
            if (this.config.page.activeControl == this) {
                return;
            }
            if (this.config.page.activeControl) {
                this.config.page.activeControl.unActive();
            }

            this.config.page.activeControl = this;
            this.editSkinBox.addClass("edit-skin-box-active");
            this.editSkinBody.children('.edit-skin-zoom-box').show();

            this.config.page.propertysPanel.setCurrentControl(this, this.getPropertyConfig(), this.config);
            if (this.shortcutMenu) {
                this.shortcutMenu.show();
            }
        },
        unActive: function () {
            this.config.page.activeControl = undefined;
            this.editSkinBox.removeClass("edit-skin-box-active");
            this.editSkinBody.children('.edit-skin-zoom-box').hide();
            this.body.find('.edit-box').remove();
            this.config.page.propertysPanel.clear();
            if (this.shortcutMenu) {
                this.shortcutMenu.hide();
            }
            this.config.page.modalClear();
        }, menuAction: function (actionKey) {

            if (actionKey == "del") {
                this.del();
            } else if (actionKey == "visible_true") {
                this.propertyChange({ id: "visible" }, "true", this.config);
            } else if (actionKey == "visible_false") {
                this.propertyChange({ id: "visible" }, "false", this.config);
            }

            this.onMenuAction(actionKey);
        }, onMenuAction: function (actionKey) {

        }, del: function () {
            if (this.editSkinBody) {
                this.editSkinBody.remove();
            }
            if (this.body) {
                this.body.remove();
            }

            if (this.config.parent) {
                this.config.parent.removeChild(this);
            }
        }, optionIgnoreInfo: {
            "parent": 1,
            "page": 1,
            "pageParams":1,
            "designer": 1,
            "designing": 1
        }, getSaveOptions: function () {
            var options = smat.globalObject.clone(this.config, this.optionIgnoreInfo);

            return JSON.stringify(options);


        }, getSaveControls: function (params) {
            if (this.children != undefined) {
                for (var ckey in this.children.data) {
                    var ctl = this.children.data[ckey];
                    var ctlParam = ctl.getSaveParams(params);
                    params.push(ctlParam);
                }
            }
        }, getSaveParams: function (params) {

            var ParentControlName = "0";
            if (this.config.parent != undefined
                && (this.config.parent instanceof smat.dynamics.PageUserControl == false)
                && (this.config.parent instanceof smat.dynamics.Page == false)) {
                ParentControlName = this.config.parent.config.name;
            }

            var control = {
                ProjID: this.config.page.config.projID,
                EntityName: this.config.page.config.entityName,
                FormName: this.config.page.config.name,
                ControlName: this.config.name,
                ParentControlName: ParentControlName,
                ControlDesc: this.config.desc,
                Seq: this.config.seq,
                ControlType: this.config.type,
                ControlOptions: this.getSaveOptions(),
            }

            if (this.config.page instanceof smat.dynamics.PageUserControl) {
                control = {
                    ProjID: this.config.page.config.projID,
                    EntityName: this.config.page.config.entityName,
                    UserControlName: this.config.page.config.name,
                    ControlName: this.config.name,
                    ParentControlName: ParentControlName,
                    ControlDesc: this.config.desc,
                    Seq: this.config.seq,
                    ControlType: this.config.type,
                    ControlOptions: this.getSaveOptions(),
                }
            }
            //child
            this.getSaveControls(params);

            if (this.afterGetSaveParams != undefined) {
                this.afterGetSaveParams(params);
            }

            return control;
        },getControlNames: function (names) {

            names[this.config.name] = 1;

            if (this.children != undefined) {
                for (var ckey in this.children.data) {
                    var ctl = this.children.data[ckey];
                    ctl.getControlNames(names);
                }
            }

        }, getControlByName: function (name) {
            var c = undefined;


            if (this.children != undefined) {
                for (var ckey in this.children.data) {
                    var ctl = this.children.data[ckey];
                    if (ctl.config.name == name) {
                        return ctl;
                    } else {
                        c = ctl.getControlByName(name);
                        if (c) return c;
                    }

                }
            }

            return c;
        },
        getSaveControlsTree: function () {
            var Controls = new Array();
            if (this.children != undefined) {
                for (var ckey in this.children.data) {
                    var ctl = this.children.data[ckey];
                    var ctlParam = ctl.getSaveParamsTree();
                    Controls.push(ctlParam);
                }
            }

            return Controls;
        }, getSaveParamsTree: function () {

            var ParentControlName = "0";
            if (this.config.parent != undefined) {
                ParentControlName = this.config.parent.config.name;
            }

            var control = {
                ProjID: this.config.page.config.projID,
                EntityName: this.config.page.config.entityName,
                FormName: this.config.page.config.name,
                ControlName: this.config.name,
                ParentControlName: ParentControlName,
                ControlDesc: this.config.desc,
                Seq: this.config.seq,
                ControlType: this.config.type,
                ControlOptions: this.getSaveOptions(),
            }
            if (this.config.page instanceof smat.dynamics.PageUserControl) {
                control = {
                    ProjID: this.config.page.config.projID,
                    EntityName: this.config.page.config.entityName,
                    UserControlName: this.config.page.config.name,
                    ControlName: this.config.name,
                    ParentControlName: ParentControlName,
                    ControlDesc: this.config.desc,
                    Seq: this.config.seq,
                    ControlType: this.config.type,
                    ControlOptions: this.getSaveOptions(),
                }
            }
            //child
            control.Controls = this.getSaveControlsTree()

            if (this.afterGetSaveParams != undefined) {
                this.afterGetSaveParams(control);
            }

            return control;
        }
        , setFormData: function (controls,beforeAddChild) {
            if (controls == undefined) {
                return;
            }


            for (var key in controls) {
                var ctl = controls[key];
                var cConfig = smat.service.strToJson(ctl.ControlOptions);
                ctl.cConfig = cConfig;
                if (ctl.rowIndex) cConfig.rowIndex = ctl.rowIndex;
                if (ctl.isUserControl) cConfig.isUserControl = ctl.isUserControl;
                ctl.colIndex = cConfig.colIndex;

                if (ctl.UniqueName) ctl.cConfig.name = ctl.UniqueName;
            }

            controls = $.Enumerable.From(controls)
                 .OrderBy("$.colIndex")
                .ToArray();

            for (var key in controls) {
                var ctl = controls[key];
                var cConfig = ctl.cConfig;
                if (ctl.rowIndex) cConfig.rowIndex = ctl.rowIndex;
                //if (ctl.colIndex) cConfig.colIndex = ctl.colIndex;
                if (beforeAddChild) {
                    beforeAddChild(cConfig);
                }
                var child = this.addChild(cConfig);
                child.setFormData(ctl.Controls)

                //if (child["abjustColsPosition"] != undefined) {
                //    child.abjustColsPosition();
                //}
            }
            if (this.afterSetFormData != undefined) {
                this.afterSetFormData();
            }

        }, checkPropertyChanging: function (property, value) {
            return true;
        }, propertyChange: function (property, value, valueConfig,param) {
            if (valueConfig == undefined) {
                valueConfig = this.config;
            }
            valueConfig[property.id] = value;
            property.value = value;
            if (value === "") {
                valueConfig[property.id] = undefined;
            }

            if (this["propertyChange_" + property.id] != undefined) {
                this["propertyChange_" + property.id](property, value, valueConfig, param)
            }
        },
        propertyChange_enable: function (property, value) {
            this.refresh();
        },
        propertyChange_cssClass: function (property, value) {
            this.refresh();
        },
        propertyChange_style: function (property, value) {
            this.refresh();
        },
        propertyChange_visible: function (property, value) {
            this.refresh();
        },
        propertyChange_htmlAttribute: function (property, value) {
            this.refresh();
        }, refresh: function () {

        }, getClassStr: function () {
            var classStr = "";

            if (this.config.cssClass != undefined) {
                classStr = this.config.cssClass
            }

            return classStr;
        }, getStyleStr: function () {
            var styleStr = "";

            if (this.config.style != undefined) {
                styleStr = this.config.style
            }

            return styleStr;
        }, cBool: function (bVal) {
            if (bVal == "true" || bVal == true) {
                return true;
            } else if (bVal == "false" || bVal == false) {
                return false;
            }
        }, getUiDataType: function (dbDataType) {

            if (dbDataType == "Date") {
                return "Date";
            } else if (
                dbDataType == "Bigint"
                || dbDataType == "Int"
                || dbDataType == "Numeric"
                || dbDataType == "Decimal"
                || dbDataType == "Smallint"
                || dbDataType == "Date"
                || dbDataType == "Date"
                || dbDataType == "Date"
                ) {
                return "Number";
            }

            return "Text";
        }, hide: function () {
            if (this.body) this.body.hide();
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Control, smat.dynamics.Element);
})();