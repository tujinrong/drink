
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  UserControl
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.UserControl = function (config) {
        //默认属性
        this.setConfig({
            type: "UserControl"
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

    smat.dynamics.UserControl.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.children = new smat.hashMap();

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag " : "";
            this.body = $('<div id="' + this.getUiId() + '"  class="form-horizontal s-userControl ' + designClass + this.getClassStr() + '" style="float: left;' + this.getStyleStr() + '"></div>').appendTo(contextOn);
            this.editSkinBody = this.body;

            var uiConfig = this.getUiConfig();
            uiConfig.parentPageId = this.config.page.uuid;
            uiConfig.designing = this.config.designing;

            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
                uiConfig.autoLoad = true;
            }

            this.uiControl = new smat.DyUserControl(uiConfig);

        }, onPageLoad: function () {
            //this.uiControl.iniEvent();
        },
        getCustomPropertyConfig: function () {


            this.editPropertyConfig.push(
			     {
			         group: 'content',
			         caption: 'userControlName',
			         type: 'text',
			         id: 'userControlName',
			         cmt: 'userControlName',
			         propType: "prop"
			     }
            );


            this.editPropertyConfig.push(
			     {
			         group: 'content',
			         caption: 'entity',
			         type: 'text',
			         id: 'entity',
			         cmt: 'entity',
			         propType: "prop"
			     }
            );


        }, refresh: function (isResetProperty) {
            this.uiControl.destroy();


            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag " : "";

            var temp = $('<div id="' + this.getUiId() + '"  class="form-horizontal s-userControl ' + designClass + this.getClassStr() + '" style="float: left;' + this.getStyleStr() + '"></div>')

            this.body.replaceWith(temp);
            this.body.remove();

            this.body = temp;

            var uiConfig = this.getUiConfig();
            uiConfig.parentPageId = this.config.page.uuid;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
                uiConfig.autoLoad = true;
            }

            this.uiControl = new smat.DyUserControl(uiConfig);

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


        }, propertyChange_style: function (property, value) {
            this.body.attr('style', "float: left;" + value);
        }, propertyChange_pageName: function (property, value) {
            this.refresh();
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.UserControl, smat.dynamics.Field);
})();