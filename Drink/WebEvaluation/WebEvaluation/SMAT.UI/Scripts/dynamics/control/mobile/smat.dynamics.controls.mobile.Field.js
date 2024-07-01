
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Field
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.mobile.Field = function (config) {
        //默认属性
        this.setConfig({
            type: "Field"
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

    smat.dynamics.mobile.Field.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var contextOn = this.config.parent.body;
            if (this.config.contextOn != undefined) {
                contextOn = $(this.config.contextOn)
            } else {
                this.config.contextOn = contextOn;
            }

            this.designClass = (this.config.designing == true) ? "designing designing-ui designing-drag " : " ";

            this.body = null;


            var uiType = "Input";

            if (this.config.dataType == "TextArea") {
                uiType = "TextArea"
                this.body = $('<textarea id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + ' ' + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);
            } else {

                if (this.config.dataType == "DropDownList") {
                    uiType = "DropDownList"
                } else if (this.config.dataType == "Slider") {
                    uiType = "Slider"
                }

                this.body = $('<input id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + ' ' + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);
            }

            var uiConfig = this.getFieldUiConfig();

            if (this.config.designing == true) {
                uiConfig.autoLoadViewData = false;
            }

            this.editSkinBody = this.body;

            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block " style="float:left;"></div>')

                this.body.before(box);

                this.body.appendTo(box);
                this.editSkinBody = box;
            }


            this.uiControl = new smat.mobile[uiType](uiConfig);

            

        }, refresh: function (isResetProperty) {
            this.uiControl.destroy();

            var box = this.body.closest('div.box-inline-block');

            this.body = null;

            var uiType = "Input";

            if (this.config.dataType == "TextArea") {
                uiType = "TextArea"
                this.body = $('<textarea id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + ' ' + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>');
            } else {

                if (this.config.dataType == "DropDownList") {
                    uiType = "DropDownList"
                } else if (this.config.dataType == "Slider") {
                    uiType = "Slider"
                }

                this.body = $('<input id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + ' ' + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>');
            }


            box.replaceWith(this.body);
            box.remove();

            var uiConfig = this.getFieldUiConfig();

            if (this.config.designing == true) {
                uiConfig.autoLoadViewData = false;
            }

           
            this.editSkinBody = this.body;
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""

                var box = $('<div class ="box-inline-block" style="float:left;"></div>')

                this.body.before(box);

                this.body.appendTo(box);
                this.editSkinBody = box;
            }

            this.uiControl = new smat.mobile[uiType](uiConfig);

            //this.editSkinBody = this.body.closest('div.form-group');
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

        }, getCustomPropertyConfig: function () {

            this.editPropertyConfig.push(
			     {
			         group: 'entity',
			         caption: 'filter',
			         type: 'Filter',
			         id: 'filter',
			         cmt: 'filter',
			         //focusOnly: true,
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

            this.editPropertyConfig.push(
                {
                    group: 'input box',
                    caption: 'dataType',
                    type: 'DropDownList',
                    id: 'dataType',
                    cmt: 'dataType',
                    propType: "prop",
                    dataSource: [
                        {
                            text: "text",
                            value: "text"
                        },
                        {
                            text: "password",
                            value: "password"
                        },
                        {
                            text: "search",
                            value: "search"
                        },
                        {
                            text: "url",
                            value: "url"
                        },
                        {
                            text: "email",
                            value: "email"
                        },
                        {
                            text: "number",
                            value: "number"
                        },

                        {
                            text: "tel",
                            value: "tel"
                        },
                        {
                            text: "date",
                            value: "date"
                        },
                        {
                            text: "time",
                            value: "time"
                        },
                        {
                            text: "month",
                            value: "month"
                        },
                        {
                            text: "datetime-local",
                            value: "datetime-local"
                        },
                        {
                            text: "DropDownList",
                            value: "DropDownList"
                        },
                        {
                            text: "Slider",
                            value: "Slider"
                        },
                        {
                            text: "TextArea",
                            value: "TextArea"
                        }
                    ]
                });


            this.editPropertyConfig.push(
			     {
			         group: 'input',
			         caption: 'format',
			         type: 'text',
			         id: 'format',
			         cmt: 'format',
			         propType: "prop"
			     });
            this.editPropertyConfig.push(
			     {
			         group: 'input',
			         caption: 'placeholder',
			         type: 'text',
			         id: 'placeholder',
			         cmt: 'placeholder',
			         propType: "prop"
			     });
            
            this.editPropertyConfig.push(
			     {
			         group: 'input',
			         caption: 'maxlength',
			         type: 'text',
			         id: 'maxlength',
			         cmt: 'maxlength',
			         propType: "prop"
			     });
            this.editPropertyConfig.push(
			     {
			         group: 'input',
			         caption: 'cookie',
			         type: 'text',
			         id: 'cookie',
			         cmt: 'cookie',
			         propType: "prop"
			     });
            this.editPropertyConfig.push(
                {
                    group: 'input',
                    caption: 'value',
                    type: 'text',
                    id: 'value',
                    cmt: 'value',
                    propType: "prop"
                });
            this.editPropertyConfig.push(
			     {
			         group: 'input box',
			         caption: 'label',
			         type: 'CultureText',
			         id: 'label',
			         cmt: 'label',
			         propType: "prop"
			     });
            this.editPropertyConfig.push(
			     {
			         group: 'input box',
			         caption: 'labelClass',
			         type: 'text',
			         id: 'labelClass',
			         cmt: 'labelClass',
			         propType: "prop"
			     }
            );

            this.editPropertyConfig.push(
			     {
			         group: 'input box',
			         caption: 'labelStyle',
			         type: 'text',
			         id: 'labelStyle',
			         cmt: 'labelStyle',
			         propType: "prop"
			     }
            );
           

            this.editPropertyConfig.push(
			     {
			         group: 'input box',
			         caption: 'required',
			         type: 'DropDownList',
			         id: 'required',
			         cmt: 'required',
			         propType: "prop",
			         dataSource: [
                        {
                            text: "true",
                            value: "true"
                        },
                        {
                            text: "false",
                            value: ""
                        }]
			     }
            );
           

            if (this.config.dataType == "DropDownList"
                || this.config.dataType == "ComboBox"
                || this.config.dataType == "MultiSelect"
                || this.config.dataType == "ButtonGroup") {
                this.editPropertyConfig.push({
                    group: 'data',
                    caption: 'codeKind',
                    type: 'text',
                    id: 'codeKind',
                    cmt: 'codeKind',
                    propType: "prop"
                }
               );

                this.editPropertyConfig.push({
                    group: 'data',
                    caption: 'valueField',
                    type: 'text',
                    id: 'dataValueField',
                    cmt: 'dataValueField',
                    propType: "prop"
                }
               );

                this.editPropertyConfig.push({
                    group: 'data',
                    caption: 'textField',
                    type: 'text',
                    id: 'dataTextField',
                    cmt: 'dataTextField',
                    propType: "prop"
                }
              );

                this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'change',
			         type: 'Logic',
			         id: 'change',
			         cmt: 'change',
			         eventKey: 'dropDownList_change',
			         propType: "event"
			     }
                );

                if (this.config.dataType == "DropDownList") {
                    this.editPropertyConfig.push(
                {
                    group: 'input box',
                    caption: 'emptyItem',
                    type: 'DropDownList',
                    id: 'emptyItem',
                    cmt: 'emptyItem',
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
                   group: 'input box',
                   caption: 'emptyText',
                   type: 'text',
                   id: 'emptyText',
                   cmt: 'emptyText',
                   propType: "prop"
               });

                    this.editPropertyConfig.push(
                     {
                         group: 'entity',
                         caption: 'view',
                         type: 'View',
                         id: 'view',
                         shortcutMenu: true,
                         cmt: 'view',
                         propType: "prop"
                     });

                    this.editPropertyConfig.push(
                    {
                        group: 'base',
                        caption: 'getParam',
                        type: 'Logic',
                        id: 'getParam',
                        eventKey: 'form_getParam',
                        cmt: 'getParam',
                        propType: "event"
                    });

                    this.editPropertyConfig.push(
                    {
                        group: 'entity',
                        caption: 'autoLoadViewData',
                        type: 'DropDownList',
                        id: 'autoLoadViewData',
                        cmt: 'autoLoadViewData',
                        propType: "prop",
                        dataSource: [
                           {
                               text: "",
                               value: ""
                           },
                           {
                               text: "true",
                               value: "true"
                           }]
                    });

                }

                if (this.config.dataType == "ComboBox") {
                    this.editPropertyConfig.push({
                        group: 'input box',
                        caption: 'optionRequired',
                        type: 'DropDownList',
                        id: 'optionRequired',
                        cmt: 'optionRequired',
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
                         group: 'entity',
                         caption: 'view',
                         type: 'View',
                         id: 'view',
                         shortcutMenu: true,
                         cmt: 'view',
                         propType: "prop"
                     });

                    this.editPropertyConfig.push(
                    {
                        group: 'base',
                        caption: 'getParam',
                        type: 'Logic',
                        id: 'getParam',
                        eventKey: 'form_getParam',
                        cmt: 'getParam',
                        propType: "event"
                    });

                    this.editPropertyConfig.push(
                    {
                        group: 'entity',
                        caption: 'autoLoadViewData',
                        type: 'DropDownList',
                        id: 'autoLoadViewData',
                        cmt: 'autoLoadViewData',
                        propType: "prop",
                        dataSource: [
                           {
                               text: "",
                               value: ""
                           },
                           {
                               text: "true",
                               value: "true"
                           }]
                    });
                }

            }  else if (this.config.dataType == "TextArea") {
                this.editPropertyConfig.push(
			     {
			         group: 'textarea',
			         caption: 'cols',
			         type: 'text',
			         id: 'cols',
			         cmt: 'cols',
			         propType: "prop"
			     }
                );

                this.editPropertyConfig.push(
			     {
			         group: 'textarea',
			         caption: 'rows',
			         type: 'text',
			         id: 'rows',
			         cmt: 'rows',
			         propType: "prop"
			     }
                );

                this.editPropertyConfig.push(
			     {
			         group: 'textarea',
			         caption: 'resize',
			         type: 'DropDownList',
			         id: 'resize',
			         cmt: 'resize',
			         propType: "prop",
			         dataSource: [
                        {
                            text: " ",
                            value: ""
                        },
                        {
                            text: "none",
                            value: "none"
                        }
			         ]
			     }
                );
            } else {
                this.editPropertyConfig.push(
                  {
                      group: 'base',
                      caption: 'change',
                      type: 'Logic',
                      id: 'change',
                      cmt: 'change',
                      eventKey: 'textbox_change',
                      propType: "event"
                  }
                 );
            }
        },
        propertyChange_label: function (property, value) {
            this.refresh();
        },
        propertyChange_labelClass: function (property, value) {
            this.refresh();
        },
        propertyChange_labelStyle: function (property, value) {
            this.refresh();
        },
        propertyChange_placeholder: function (property, value) {
            this.refresh();
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.mobile.Field, smat.dynamics.mobile.Base);
})();