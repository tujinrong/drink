
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Label
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Label = function (config) {
        //默认属性
        this.setConfig({
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        //Event初期化
        this.iniEvent();

        return this;
    };

    smat.dynamics.Label.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag " : " ";

            this.body = $('<label id="' + this.getUiId() + '" ' + this.getAttrStr() + ' class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);

            var uiConfig = this.getLabelUiConfig();

            this.uiControl = new smat.Label(uiConfig);

            this.editSkinBody = this.uiControl.labelDom;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block ' + this.designClass + '" style="float:left;"></div>')

                this.uiControl.labelDom.before(box);

                this.uiControl.labelDom.appendTo(box);
                this.body.appendTo(box);

                this.editSkinBody = box;
            }

        }, getLabelUiConfig: function () {
            var uiConfig = this.getUiConfig();
            uiConfig.name = undefined;
            if (this.config.label != undefined) {
                var labelAttributes = {};

                if (this.config.labelClass != undefined) {
                    labelAttributes['class'] = this.config.labelClass;
                }

                if (this.config.labelStyle != undefined) {
                    labelAttributes['style'] = this.config.labelStyle;
                }
                var labelText = this.config.label;
                uiConfig.label = {
                    text: labelText,
                    attrs: labelAttributes
                }
            }

            if (this.config.inputBoxClass != undefined || this.config.inputBoxStyle != undefined) {
                var inputBoxAttributes = {};

                if (this.config.inputBoxClass != undefined) {
                    inputBoxAttributes['class'] = this.config.inputBoxClass;
                }

                if (this.config.inputBoxStyle != undefined) {
                    inputBoxAttributes['style'] = this.config.inputBoxStyle;
                }
                uiConfig.inputBox = {
                    attrs: inputBoxAttributes
                }
            }

            if (smat[this.config.dataType] != undefined) {
                uiConfig.target = this.body;
            }


            return uiConfig;
        }, getCustomPropertyConfig: function () {
            
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
			    }
            ];

            this.editPropertyConfig.push(
			     {
			         group: 'label',
			         caption: 'label',
			         type: 'CultureText',
			         id: 'label',
			         cmt: 'label',
			         propType: "prop"
			     });
            this.editPropertyConfig.push(
			     {
			         group: 'label',
			         caption: 'labelClass',
			         type: 'text',
			         id: 'labelClass',
			         cmt: 'labelClass',
			         propType: "prop"
			     }
            );

            this.editPropertyConfig.push(
			     {
			         group: 'label',
			         caption: 'labelStyle',
			         type: 'text',
			         id: 'labelStyle',
			         cmt: 'labelStyle',
			         propType: "prop"
			     }
            );

        },refresh: function (isResetProperty) {
            this.uiControl.destroy();

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag " : " ";

            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var temp = $('<label id="' + this.getUiId() + '" ' + this.getAttrStr() + ' class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>')

            this.body.closest('.box-inline-block').replaceWith(temp);
            this.body.closest('.box-inline-block').remove();

            this.body = temp;

            var uiConfig = this.getLabelUiConfig();

            this.uiControl = new smat.Label(uiConfig);

            this.editSkinBody = this.uiControl.labelDom;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block ' + this.designClass + '" style="float:left;"></div>')

                this.uiControl.labelDom.before(box);

                this.uiControl.labelDom.appendTo(box);
                this.body.appendTo(box);

                this.editSkinBody = box;
            }

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

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Label, smat.dynamics.Field);
})();