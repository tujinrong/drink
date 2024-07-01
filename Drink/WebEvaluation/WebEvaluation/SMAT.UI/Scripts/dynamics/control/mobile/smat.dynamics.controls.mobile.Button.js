
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Button
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Button = function (config) {
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

    smat.dynamics.mobile.Button.prototype = {
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

            var text = smat.service.cultureText(this.config.text);
            if (!text) text = "";
            this.body = $('<a id="' + this.getUiId() + '"  class="' + cssClassStr + '"  style="' + styleStr + '">' + text + '</a>').appendTo(contextOn);


            var uiConfig = this.getUiConfig();

            uiConfig.text = smat.service.cultureText(this.config.text);

            this.uiControl = new smat.mobile.Button(uiConfig);

            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block ' + designClass + '" style="margin-left:5px;float:left;"></div>')

                this.body.before(box);

                this.body.appendTo(box);
                this.editSkinBody = box;
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
			         group: 'base',
			         caption: 'click',
			         type: 'Logic',
			         id: 'click',
			         cmt: 'click',
			         eventKey: 'button_click',
			         propType: "event"
			     }
            );

            this.editPropertyConfig.push(
			     {
			         group: 'text',
			         caption: 'align',
			         type: 'text',
			         id: 'align',
			         cmt: 'align',
			         propType: "prop"
			     });

            this.editPropertyConfig.push(
			     {
			         group: 'text',
			         caption: 'icon',
			         type: 'text',
			         id: 'icon',
			         cmt: 'icon',
			         propType: "prop"
			     });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Button, smat.dynamics.mobile.Base);
})();