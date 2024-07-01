
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Layout
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Layout = function (config) {
        //默认属性
        this.setConfig({
            type: "Layout"
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

    smat.dynamics.mobile.Layout.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.children = new smat.hashMap();

            var contextOn = this.config.page.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-panel designing-drag " : "";
            this.body = $('<div class="panel s-mobile-panel ' + designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"></div>').appendTo(contextOn);
            this.editSkinBody = this.body;
            var dropClass = "";
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
                dropClass = "designing-drop";
            }

            if (this.cBool(this.config.headerShow) != false) {
                $('<div class="row sm-header ' + dropClass + '" data-role="header" dy-uuid="' + this.uuid + '" row-index = "0"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }
            $('<div class="row sm-content' + dropClass + '" dy-uuid="' + this.uuid + '" row-index = "1"><div class="row-empty-height"><div></div>').appendTo(this.body);

            if (this.cBool(this.config.footerShow) != false) {
                $('<div class="row sm-footer ' + dropClass + '" data-role="footer" dy-uuid="' + this.uuid + '" row-index = "2"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }
            
            var uiConfig = this.getUiConfig();
            uiConfig.id = this.uuid;
            this.uiControl = new smat.mobile.Layout(uiConfig);
        },
        createNewChild: function (childConfig) {
            childConfig.layoutId = this.uuid;
            if (childConfig.notMobile) {
                return new smat.dynamics[childConfig.type](childConfig);
            } else {
                return new smat.dynamics.mobile[childConfig.type](childConfig);
            }
        }, getChildContextOn: function (rowIndex) {

            var contextOn = this.body.children("[row-index=" + rowIndex + "]");

            contextOn.children(".row-empty-height").remove();
            return contextOn;
        },
        getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
            {
                group: 'layout',
                caption: 'headerShow',
                type: 'DropDownList',
                id: 'headerShow',
                cmt: 'headerShow',
                propType: "prop",
                dataSource: [
                   {
                       text: "true",
                       value: "true"
                   },
                   {
                       text: "false",
                       value: "false"
                   }]
            });

            this.editPropertyConfig.push(
            {
                group: 'layout',
                caption: 'footerShow',
                type: 'DropDownList',
                id: 'footerShow',
                cmt: 'footerShow',
                propType: "prop",
                dataSource: [
                   {
                       text: "true",
                       value: "true"
                   },
                   {
                       text: "false",
                       value: "false"
                   }]
            });
        },
        propertyChange_headerShow: function (property, value) {
            this.config.page.refresh(this.config.name);
        },
        propertyChange_footerShow: function (property, value) {
            this.config.page.refresh(this.config.name);
        }, afterAddChild: function (child) {
            if (!this.config.page.settingData) {
                this.config.page.refresh(this.config.name);
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Layout, smat.dynamics.mobile.Section);
})();