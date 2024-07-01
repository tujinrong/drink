
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Field
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.TreeView = function (config) {
        //默认属性
        this.setConfig({
            type: "TreeView"
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

    smat.dynamics.TreeView.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {

            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) { contextOn = $(this.config.contextOn) }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag " : " ";

            this.body = $('<div id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);
            
            if (this.config.checkboxes == "true" && this.config.checkChildren == "true") {
                this.config.checkboxes = { checkChildren: true };
            }

            var uiConfig = this.getFieldUiConfig();

            if (this.config.designing == true) {
                if (uiConfig.dataSource == undefined) {
                    uiConfig.dataSource = [
                        {
                            text: "Node1", items: [
                              { text: "Node11" },
                              { text: "Node12" },
                              { text: "Node13" }
                            ]
                        },
                        {
                            text: "Node2", items: [
                              { text: "Node21" },
                              { text: "Node22" },
                              { text: "Node23" }
                            ]
                        }
                    ]
                }

            }

            this.uiControl = new smat.TreeView(uiConfig);

            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = "height: 99%;width: 99%;left: 0px;top: 0px;";
                this.uiControl.expand(".s-item");
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
                  group: 'data',
                  caption: 'checkboxes',
                  type: 'DropDownList',
                  id: 'checkboxes',
                  cmt: 'checkboxes',
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
                 group: 'data',
                 caption: 'checkChildren',
                 type: 'DropDownList',
                 id: 'checkChildren',
                 cmt: 'checkChildren',
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
			         group: 'base',
			         caption: 'select',
			         type: 'Logic',
			         id: 'select',
			         cmt: 'select',
			         eventKey: 'treeView-select',
			         propType: "event"
			     }
            );

            this.editPropertyConfig.push(
                {
                    group: 'base',
                    caption: 'check',
                    type: 'Logic',
                    id: 'check',
                    cmt: 'check',
                    eventKey: 'treeView-check',
                    propType: "event"
                }
            );
        }, propertyChange_checkboxes: function (property, value) {
            //this.refresh(true);
        },
        propertyChange_text: function (property, value) {
            value = smat.service.cultureText(value);
            this.body.text(value);
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.TreeView, smat.dynamics.Field);
})();