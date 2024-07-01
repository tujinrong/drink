
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Pager
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Pager = function (config) {
        //默认属性
        this.setConfig({
            type: "Pager"
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

    smat.dynamics.Pager.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            this.body = $('<div id="' + this.getUiId() + '"   class="' + designClass + ' ' + cssClassStr + '"  style="' + styleStr + '"></div>').appendTo(contextOn);

            var uiConfig = this.getUiConfig();
            this.uiControl = new smat.Pager(uiConfig);

            this.editSkinBody = this.uiControl.box;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }

            if (this.config.designing == true) {
                this.uiControl.dataSource = {
                    totalSize: 50,
                    pageSize: 10,
                    pageNumber:1
                }
                this.uiControl.initPageLink();
            }

        }, getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
            {
                group: 'result',
                caption: 'dataHandler',
                type: 'text',
                id: 'dataHandler',
                cmt: 'dataHandler',
                propType: "prop"
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Pager, smat.dynamics.Field);
})();