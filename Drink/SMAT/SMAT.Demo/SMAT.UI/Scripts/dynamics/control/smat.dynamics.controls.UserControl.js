
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Field
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.UserControl = function (config) {
        //默认属性
        this.setConfig({
            type: "Button"
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

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = (this.config.cssClass != undefined) ? this.config.cssClass : "";
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            this.body = $('<button id="' + this.getUiId() + '"  class="' + cssClassStr + '"  style="' + styleStr + '">' + this.config.text + '</button>').appendTo(contextOn);

            
            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.Button(uiConfig);

            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block ' + designClass + '" style="margin-left:5px;"></div>')

                this.body.before(box);

                this.body.appendTo(box);
                this.editSkinBody = box;
            }

        }, getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
                {
                    group: 'text',
                    caption: 'text',
                    type: 'text',
                    id: 'text',
                    cmt: 'text',
                    propType: "prop"
                });

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'click',
			         type: 'Logic',
			         id: 'click',
			         cmt: 'click',
			         eventKey: 'button_click',
			         propType: "event"
			     }
            );
        },
        propertyChange_text: function (property, value) {
            this.body.text(value);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.UserControl, smat.dynamics.Field);
})();