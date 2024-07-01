
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Field
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Editor = function (config) {
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

    smat.dynamics.Editor.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag " : " ";

            this.body = $('<textarea id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);

            var uiConfig = this.getFieldUiConfig();

            this.uiControl = new smat.Editor(uiConfig);

            this.editSkinBody = this.body.closest('.s-editor');
            if (this.config.designing == true) {
                this.editSkinBoxStyle = "";
                this.editSkinBody.css('position', 'relative');
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

        },
        propertyChange_text: function (property, value) {
            value = smat.service.cultureText(value);
            this.body.text(value);
        },
        getFieldName: function () {
            if (this.config.fieldName != undefined && this.config.fieldName != "") {
                return this.config.page.config.entityName + "." + this.config.fieldName;
            }

            return this.config.name;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Editor, smat.dynamics.Field);
})();