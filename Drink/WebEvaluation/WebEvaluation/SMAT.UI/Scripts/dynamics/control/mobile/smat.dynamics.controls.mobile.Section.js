
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Section
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Section = function (config) {
        //默认属性
        this.setConfig({
            type: "Section"
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

    smat.dynamics.mobile.Section.prototype = {
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
            this.body = $('<div class=" panel s-mobile-panel ' + designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"></div>').appendTo(contextOn);
            this.editSkinBody = this.body;
            var dropClass = "";
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
                dropClass = "designing-drop";
            }

            for (var i = 0; i < this.config.rowsCount; i++) {
                $('<div class="row ' + dropClass + '" dy-uuid="' + this.uuid + '" row-index = "' + i + '"><div class="row-empty-height"><div></div>').appendTo(this.body);
            }
        },
        createNewChild: function (childConfig) {

            if (childConfig.notMobile) {
                return new smat.dynamics[childConfig.type](childConfig);
            } else {
                return new smat.dynamics.mobile[childConfig.type](childConfig);
            }

            
        }, isCanCreateNewChild: function (childConfig) {
            if (childConfig.notMobile) {
                return (smat.dynamics[childConfig.type] != undefined);
            } else {
                return (smat.dynamics.mobile[childConfig.type] != undefined);
            }
            
        },
        getCustomPropertyConfig: function () {
            
        }, afterAddChild: function (child) {
            if ( this.config.designing == true && !child.config.notMobile) {
                this.config.page.refreshApplication();
            }
            
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Section, smat.dynamics.Section);
})();