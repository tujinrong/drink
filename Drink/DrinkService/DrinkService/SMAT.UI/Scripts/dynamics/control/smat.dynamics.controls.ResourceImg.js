
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  ResourceImg
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.ResourceImg = function (config) {
        //默认属性
        this.setConfig({
            type: "ResourceImg"
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

    smat.dynamics.ResourceImg.prototype = {
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

            this.body = $('<input id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" name="files" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '" />').appendTo(this.config.contextOn);
            
            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.ResourceImg(uiConfig);

            this.editSkinBody = this.body.closest('div.s-resource-box');
            if (this.config.designing == true) {
                this.editSkinBoxStyle = "";
            }

        }, getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
                {
                    group: 'text',
                    caption: 'text',
                    type: 'CultureText',
                    id: 'text',
                    cmt: 'text',
                    propType: "prop"
                });

            this.editPropertyConfig.push(
                {
                    group: 'entity',
                    caption: 'fieldName',
                    type: 'text',
                    id: 'fieldName',
                    cmt: 'fieldName',
                    propType: "prop"
                });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.ResourceImg, smat.dynamics.Field);
})();