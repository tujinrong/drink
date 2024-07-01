
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Tag
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Tag = function (config) {
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

    smat.dynamics.Tag.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = this.getClassStr();
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var tagType = "span";
            if (this.config.tagType) tagType = this.config.tagType;
            var content = this.getContent();

            this.body = $('<' + tagType + ' id="' + this.getUiId() + '" ' + this.getAttrStr() + ' class="' + designClass + ' ' + cssClassStr + '" style="' + this.getStyleStr() + '">' + content + '</' + tagType + '>').appendTo(contextOn);

            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.Tag(uiConfig);

            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block ' + designClass + '" style="margin-left:5px;float:left;"></div>')
                
                this.body.before(box);

                this.body.appendTo(box);
                this.editSkinBody = box;
            }

        }, refresh: function (isResetProperty) {
            this.uiControl.destroy();

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            var designClass = (this.config.designing == true) ? "designing designing-ui designing-drag" : "";
            var cssClassStr = this.getClassStr();
            var styleStr = (this.config.style != undefined) ? this.config.style : "";

            var tagType = "span";
            if (this.config.tagType) tagType = this.config.tagType;
            var content = this.getContent();

            var temp = $('<' + tagType + ' id="' + this.getUiId() + '" ' + this.getAttrStr() + ' class="' + designClass + ' ' + cssClassStr + '" style="' + this.getStyleStr() + '">' + content + '</' + tagType + '>')

            this.body.closest('.box-inline-block').replaceWith(temp);
            this.body.closest('.box-inline-block').remove();

            this.body = temp;

            var uiConfig = this.getUiConfig();

            this.uiControl = new smat.Tag(uiConfig);

            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block ' + designClass + '" style="float:left;"></div>')

                this.body.before(box);

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

        }, getContent: function () {
            var content = smat.service.cultureText(this.config.content);
            if (!content) content = "";
            if (!this.config.tagType || this.config.tagType == "span" || this.config.tagType == "label") {
                if (!content) content = "content";
            }
            return content;
        }, getCustomPropertyConfig: function () {
           
            this.editPropertyConfig.push(
			     {
			         group: 'tag',
			         caption: 'type',
			         type: 'DropDownList',
			         id: 'tagType',
			         cmt: 'tagType',
			         propType: "prop",
			         dataSource: [
                         {
                             text: " ",
                             value: ""
                         },
                         {
                             text: "span",
                             value: "span"
                         },
                         {
                             text: "label",
                             value: "label"
                         },
                         {
                             text: "img",
                             value: "img"
                         },
                         {
                             text: "i",
                             value: "i"
                         },
                         {
                             text: "h1",
                             value: "h1"
                         },
                         {
                             text: "h2",
                             value: "h2"
                         },
                         {
                             text: "h3",
                             value: "h3"
                         },
                         {
                             text: "h4",
                             value: "h4"
                         },
                         {
                             text: "h5",
                             value: "h5"
                         }
			         ]
			     }
            );

            this.editPropertyConfig.push(
                {
                    group: 'tag',
                    caption: 'content',
                    type: 'CultureText',
                    id: 'content',
                    cmt: 'content',
                    propType: "prop"
                });

            this.editPropertyConfig.push(
                {
                    group: 'tag',
                    caption: 'src',
                    type: 'text',
                    id: 'src',
                    cmt: 'src',
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
        }, getAttrStr: function () {
            var attrStr = "";

            if (this.config.filter != undefined) {
                attrStr = attrStr + "dy-filter='" + this.config.filter + "'"
            }

            if (this.config.maxlength != undefined && this.config.maxlength != "") {
                attrStr = attrStr + "maxlength='" + this.config.maxlength + "'"
            }

            if (this.config.src) {
                attrStr = attrStr + "src='" + this.config.src + "'"
            }

            if (this.config.htmlAttribute != undefined && this.config.htmlAttribute != "") {
                attrStr = attrStr + " " + this.config.htmlAttribute
            }

            return attrStr;
        }, getStyleStr: function () {
            var styleStr = "";

            if (this.config.style != undefined) {
                styleStr = this.config.style
            }

            if (this.config.click) {
                styleStr += "cursor:pointer;";
            }

            return styleStr;
        },
        propertyChange_src: function (property, value) {
            this.refresh();
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Tag, smat.dynamics.Field);
})();