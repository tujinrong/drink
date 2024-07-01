
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  RadioBox
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.RadioBox = function (config) {
        //默认属性
        this.setConfig({
            type: "RadioBox"
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

    smat.dynamics.RadioBox.prototype = {
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

            this.body = $('<input id="' + this.getUiId() + '"  class="' + cssClassStr + '"  style="' + styleStr + '"></input>').appendTo(contextOn);


            var uiConfig = this.getUiConfig();
            uiConfig.text = smat.service.cultureText(uiConfig.text);
            this.initUI(uiConfig);

            this.editSkinBody = this.body.closest('label');
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block ' + designClass + '" style="margin-left:5px;float:left;"></div>')

                this.body.closest('label').before(box);

                this.body.closest('label').appendTo(box);
                this.editSkinBody = box;
            }

        }, initUI: function (uiConfig) {
            this.uiControl = new smat.RadioBox(uiConfig);
        }, getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
                {
                    group: 'input',
                    caption: 'text',
                    type: 'text',
                    id: 'text',
                    cmt: 'text',
                    propType: "prop"
                });

            this.editPropertyConfig.push(
                {
                    group: 'input',
                    caption: 'groupName',
                    type: 'text',
                    id: 'groupName',
                    cmt: 'groupName',
                    propType: "prop"
                });

            this.editPropertyConfig.push(
			     {
			         group: 'input',
			         caption: 'checked',
			         type: 'DropDownList',
			         id: 'checked',
			         cmt: 'checked',
			         propType: "prop",
			         dataSource: [
                        {
                            text: "",
                            value: ""
                        },
                        {
                            text: "checked",
                            value: "checked"
                        }]
			     }
            );

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'change',
			         type: 'Logic',
			         id: 'change',
			         cmt: 'change',
			         eventKey: 'box_change',
			         propType: "event"
			     }
            );
        },
        propertyChange_text: function (property, value) {
            this.body.closest('label').children('span').text(value);
        },
        propertyChange_style: function (property, value) {
            this.body.closest('label').attr('style', value);
        },
        propertyChange_checked: function (property, value) {
            this.body.closest('label').children('input').prop('checked', value);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.RadioBox, smat.dynamics.Field);
})();