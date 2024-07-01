
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Field
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Field = function (config) {
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

    smat.dynamics.Field.prototype = {
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

            if (this.config.dataType == "TextArea") {
                this.body = $('<textarea id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + ' ' + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);
            } else {
                this.body = $('<input id="' + this.getUiId() + '" ' + this.getAttrStr() + ' name="' + this.getFieldName() + '" class="' + this.designClass + ' ' + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>').appendTo(this.config.contextOn);
            }

            var uiConfig = this.getFieldUiConfig();

            if (this.config.designing == true) {
                uiConfig.autoLoadViewData = false;
            }

            this.uiControl = new smat[this.config.dataType](uiConfig);

            this.editSkinBody = this.body.closest('div.form-group');
            if (this.config.designing == true) {
                this.editSkinBoxStyle = ""
            }

        }, getFieldUiConfig: function () {
            var uiConfig = this.getUiConfig();
            uiConfig.uiName = uiConfig.name;
            uiConfig.name = undefined;
            if (this.config.label != undefined) {
                var labelAttributes = {};

                if (this.config.labelClass != undefined) {
                    labelAttributes['class'] = this.config.labelClass;
                }

                if (this.config.labelStyle != undefined) {
                    labelAttributes['style'] = this.config.labelStyle;
                }
                var labelText = this.config.label;
                uiConfig.label = {
                    text: labelText,
                    attrs: labelAttributes
                }
            }

            if (this.config.inputBoxClass != undefined || this.config.inputBoxStyle != undefined) {
                var inputBoxAttributes = {};

                if (this.config.inputBoxClass != undefined) {
                    inputBoxAttributes['class'] = this.config.inputBoxClass;
                }

                if (this.config.inputBoxStyle != undefined) {
                    inputBoxAttributes['style'] = this.config.inputBoxStyle;
                }
                uiConfig.inputBox = {
                    attrs: inputBoxAttributes
                }
            }

            if (this.config.page.config.print == true) {
                uiConfig.enable = false;
            }

            if (smat[this.config.dataType] != undefined) {
                uiConfig.target = this.body;
            }

            //auth
            if (!this.isHasAuth()) {
                uiConfig.enable = false;
            }

            var authType = this.getAuthType();
            if (authType == "1") {
                uiConfig.enable = false;
            } else if (authType == "2") {
                uiConfig.visible = false;
            }

            return uiConfig;
        },
        iniEvent: function () {
            if (this.config.designing == true && this.editSkinBody != undefined) {
                var self = this;
                this.editSkinBox.addClass('darg-box')
                this.editSkinBox.asmatDraggable({
                    //filter: ".darg-box",
                    //container: this.config.parent.body,
                    hint: function (e) {
                        return self.fieldHint(this, e);
                    },
                    dragstart: function (e) {
                        $('.edit-skin-box').hide();
                        if (self.editSkinBody.parent().children().length == 1) {
                            $('<div class="row-empty-height"><div>').appendTo(self.editSkinBody.parent());
                        }
                        self.editSkinBody.addClass('draging');
                        if (self.onDragstart) {
                            self.onDragstart(e)
                        }
                    },
                    dragend: function (e) {
                        $('.edit-skin-box').show();
                        self.editSkinBody.removeClass('draging');
                        self.config.page.dragModel = undefined;
                        self.config.page.dragTarget = undefined;
                        //self.config.page.modalClear();
                        if (self.onDragend) {
                            self.onDragend(e)
                        }
                    }, drag: function (e) {
                        var pageX = e.pageX;
                        if (e.pageX == undefined) {
                            pageX = e.x.screen;
                        }

                        if (self.config.page.dropTarget != undefined) {
                            var dx = (pageX - self.config.page.dropTarget.offset().left);
                            self.handleRowDrag(dx, self.config.page.dropTarget, this.options.hintElement);
                        } else {
                            //this.options.hintElement.css('opacity', '1');
                        }
                    }
                });

            }
        },
        fieldHint: function (dragTarget, item) {
            var self = this;
            //self.config.page.modalTarget(self.config.parent);

            this.config.page.dragTarget = dragTarget;
            this.config.page.dragModel = "edit";


            var hintElement = self.editSkinBody.clone();
            hintElement.find('.s-animation-container').remove();
            hintElement.css('border', '1px dashed #19C6F9');
            hintElement.css('background-color', '#fff');

            dragTarget.options.hintElement = hintElement;
            dragTarget.options.ctl = this;
            dragTarget.options.dragType = "edit";

            if (self.onHint) {
                self.onHint(hintElement, dragTarget, item)
            }

            return hintElement;
        }
        , handleRowDrag: function (dx, dropTarget, hintElement) {

            //将hit元素暂时放入drop容器中
            if (dropTarget.find(".drag-temp-element").length == 0) {
                var tempElement = hintElement.clone();
                tempElement.addClass("drag-temp-element");

                var w = tempElement.width();
                var h = tempElement.height();
                tempElement.attr('style', 'border: 1px dashed #19C6F9;background-color: #fff;margin-top: 0px;opacity:0.7;');

                if (this.config.type == "UserControl") {
                    tempElement.css("float", "left");
                }
                if (w > 0) tempElement.width(w);
                if (h > 0) tempElement.height(h);

                if (dropTarget.find(".row-empty-height").length > 0) {
                    tempElement.attr('temp-index', 0);
                } else {
                    tempElement.attr('temp-index', dropTarget.children().length);
                }

                tempElement.appendTo(dropTarget);

                dropTarget.find('.row-empty-height').remove();
                dropTarget.dynamicsUI().abjustRowColsIndex(dropTarget);
            }

            //hintElement.css('opacity', '0');

            if (dropTarget.attr("item-width") != undefined) {

                //计算drag元素在drop容器中所在位置
                var item_width_str = dropTarget.attr("item-width");

                var ws = item_width_str.split(",");
                if (ws.length == 0) {
                    return;
                }
                var tempElement = dropTarget.find('.drag-temp-element');
                var temp_index = Number(tempElement.attr('temp-index'));
                var index = ws.length;
                var tempstart = 0;
                for (var i in ws) {
                    if (dx > tempstart && dx < ws[i]) {
                        index = i;
                        break;
                    }
                    tempstart = ws[i];
                }

                //当所在位置发生变化时，调整位置
                if (index != temp_index) {
                    //abjust index
                    tempElement = tempElement.detach();

                    var beforeElement = dropTarget.children('[col-index="' + index + '"]').not('.draging');
                    if (beforeElement.length > 0) {
                        beforeElement.before(tempElement);
                    } else {
                        tempElement.appendTo(dropTarget);
                    }

                    tempElement.attr('temp-index', index);
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
                    id: 'dataType',
                    cmt: 'dataType',
                    propType: "prop",
                    type: 'DropDownList',
                    dataSource: [
                        {
                            text: "TextBox",
                            value: "TextBox"
                        },
                        {
                            text: "DatePicker",
                            value: "DatePicker"
                        },
                        {
                            text: "DateTimePicker",
                            value: "DateTimePicker"
                        },
                        {
                            text: "DropDownList",
                            value: "DropDownList"
                        },
                        {
                            text: "ComboBox",
                            value: "ComboBox"
                        },
                        {
                            text: "MultiSelect",
                            value: "MultiSelect"
                        },

                        {
                            text: "ButtonGroup",
                            value: "ButtonGroup"
                        },
                        {
                            text: "NumericTextBox",
                            value: "NumericTextBox"
                        },
                        {
                            text: "Refer",
                            value: "Refer"
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
			         caption: 'decimals',
			         type: 'text',
			         id: 'decimals',
			         cmt: 'decimals',
			         propType: "prop"
			     });

            this.editPropertyConfig.push(
			     {
			         group: 'input',
			         caption: 'format',
			         id: 'format',
			         cmt: 'format',
			         propType: "prop",
			         type: 'DropDownList',
			         dataSource: [
                         {
                             text: " ",
                             value: ""
                         },
                         {
                             text: "{0:n0}",
                             value: "{0:n0}"
                         },
                         {
                             text: "{0:n2}",
                             value: "{0:n2}"
                         },
                         {
                             text: "{0:n4}",
                             value: "{0:n4}"
                         }
			         ]
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
			         type: 'Style',
			         id: 'labelStyle',
			         cmt: 'labelStyle',
			         propType: "prop"
			     }
            );
            this.editPropertyConfig.push(
			     {
			         group: 'input box',
			         caption: 'inputBoxClass',
			         type: 'text',
			         id: 'inputBoxClass',
			         cmt: 'inputBoxClass',
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
            this.editPropertyConfig.push({
                group: 'input box',
                caption: 'inputBoxStyle',
                type: 'Style',
                id: 'inputBoxStyle',
                cmt: 'inputBoxStyle',
                propType: "prop"
            }
            );

            if (this.config.dataType == "DatePicker") {
                this.editPropertyConfig.push({
                    group: 'date option',
                    caption: 'depth',
                    type: 'text',
                    id: 'depth',
                    cmt: 'depth',
                    propType: "prop"
                }
                );

                this.editPropertyConfig.push({
                    group: 'date option',
                    caption: 'start',
                    type: 'text',
                    id: 'start',
                    cmt: 'start',
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
			             eventKey: 'datePicker_change',
			             propType: "event"
			         }
                );

                this.editPropertyConfig.push(
			         {
			             group: 'base',
			             caption: 'close',
			             type: 'Logic',
			             id: 'close',
			             cmt: 'close',
			             eventKey: 'datePicker_close',
			             propType: "event"
			         }
                );

                this.editPropertyConfig.push(
			         {
			             group: 'base',
			             caption: 'open',
			             type: 'Logic',
			             id: 'open',
			             cmt: 'open',
			             eventKey: 'datePicker_open',
			             propType: "event"
			         }
                );
            } else if (this.config.dataType == "Refer") {
                this.editPropertyConfig.push({
                    group: 'refer',
                    caption: 'referKey',
                    type: 'DropDownList',
                    id: 'refer-key',
                    cmt: 'referKey',
                    propType: "prop",
                    dataSource: smat.dynamics.getReferList()
                }
               );

                this.editPropertyConfig.push({
                    group: 'data',
                    caption: 'icon',
                    type: 'text',
                    id: 'icon',
                    cmt: 'icon',
                    propType: "prop"
                }
                );
                

                this.editPropertyConfig.push(
			         {
			             group: 'base',
			             caption: 'getParam',
			             type: 'Logic',
			             id: 'getParam',
			             cmt: 'getParam',
			             eventKey: 'refer_getParam',
			             propType: "event"
			         }
                );

                this.editPropertyConfig.push(
			         {
			             group: 'base',
			             caption: 'afterSetValue',
			             type: 'Logic',
			             id: 'afterSetValue',
			             cmt: 'afterSetValue',
			             eventKey: 'refer_afterSetValue',
			             propType: "event"
			         }
                );

                this.editPropertyConfig.push(
			         {
			             group: 'base',
			             caption: 'checkRefer',
			             type: 'Logic',
			             id: 'checkRefer',
			             cmt: 'checkRefer',
			             eventKey: 'refer_checkRefer',
			             propType: "event"
			         }
                );
            } else if (this.config.dataType == "DropDownList"
                || this.config.dataType == "ComboBox"
                || this.config.dataType == "MultiSelect"
                || this.config.dataType == "ButtonGroup") {

                this.editPropertyConfig.push(
			     {
			         group: 'data',
			         caption: 'isBool',
			         type: 'DropDownList',
			         id: 'isBool',
			         cmt: 'isBool',
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
                this.editPropertyConfig.push(
			     {
			         group: 'data',
			         caption: 'template',
			         type: 'Template',
			         id: 'template',
			         cmt: 'template',
			         eventKey: 'grid_column_template',
			         propType: "prop"
			     }
            );

                this.editPropertyConfig.push(
                {
                    group: 'data',
                    caption: 'valueTemplate',
                    type: 'Template',
                    id: 'valueTemplate',
                    cmt: 'valueTemplate',
                    eventKey: 'grid_column_template',
                    propType: "prop"
                }
           );


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

            } else if (this.config.dataType == "NumericTextBox") {
                this.editPropertyConfig.push(
			     {
			         group: 'input box',
			         caption: 'select',
			         type: 'DropDownList',
			         id: 'select',
			         cmt: 'select',
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
                      caption: 'change',
                      type: 'Logic',
                      id: 'change',
                      cmt: 'change',
                      eventKey: 'textbox_change',
                      propType: "event"
                  }
                 );
            } else if (this.config.dataType == "TextBox") {
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
            } else if (this.config.dataType == "TextArea") {
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
            }
        },
        appendTo: function (parentTarget) {
            this.config.parent.children.remove(this.uuid);
            parentTarget.children.set(this.uuid, this);
            this.config.parent = parentTarget;
        }, checkPropertyChanging: function (property, value) {
            var isOk = true;
            if (property.id == "dataType") {
                isOk = false;
                for (var key in property.dataSource) {
                    if (property.dataSource[key].value == value) {
                        isOk = true;
                    }
                }
            }

            return isOk;
        }, getAttrStr: function () {
            var attrStr = "";

            if (this.config.filter != undefined) {
                attrStr = attrStr + "dy-filter='" + this.config.filter + "'"
            }

            if (this.config.maxlength != undefined && this.config.maxlength != "") {
                attrStr = attrStr + "maxlength='" + this.config.maxlength + "'"
            }

            if (this.config.htmlAttribute != undefined && this.config.htmlAttribute != "") {
                attrStr = attrStr + " " + this.config.htmlAttribute
            }

            return attrStr;
        }, refresh: function (isResetProperty) {
            this.uiControl.destroy();

            var box = this.body.closest('div.form-group');

            this.body = null;

            if (this.config.dataType == "TextArea") {
                this.body = $('<textarea id="' + this.getUiId() + '" ' + this.getAttrStr() + '  name="' + this.getFieldName() + '" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>')
            } else {
                this.body = $('<input id="' + this.getUiId() + '" ' + this.getAttrStr() + '  name="' + this.getFieldName() + '" class="' + this.designClass + this.getClassStr() + '" style="' + this.getStyleStr() + '"/>')
            }


            box.replaceWith(this.body);
            box.remove();

            var uiConfig = this.getFieldUiConfig();

            if (this.config.designing == true) {
                uiConfig.autoLoadViewData = false;
            }

            this.uiControl = new smat[this.config.dataType](uiConfig);
            this.editSkinBody = this.body.closest('div.form-group');
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

        },
        getFieldName: function () {
            if (this.config.fieldName != undefined && this.config.fieldName != "") {
                return this.config.page.config.entityName + "." + this.config.fieldName;
            }

            return this.config.name;
        },
        propertyChange_label: function (property, value) {
            value = smat.service.cultureText(value);
            this.uiControl.labelDom.text(value);
        },
        propertyChange_labelClass: function (property, value) {
            var defaultlabelClass = "input-s-sm";
            this.uiControl.labelDom.attr('class', value);
            this.uiControl.labelDom.addClass("control-label");
            this.uiControl.labelDom.addClass(defaultlabelClass);
        },
        propertyChange_labelStyle: function (property, value) {
            this.uiControl.labelDom.attr('style', value);
        },
        propertyChange_inputBoxClass: function (property, value) {
            var defaultlabelClass = "form-group";
            this.uiControl.inputBoxDom.attr('class', value);
            this.uiControl.inputBoxDom.addClass(defaultlabelClass);
        },
        propertyChange_inputBoxStyle: function (property, value) {
            this.uiControl.inputBoxDom.attr('style', value);
        },
        propertyChange_select: function (property, value) {
            this.refresh();
        },
        propertyChange_dataType: function (property, value) {
            this.refresh(true);
        }, propertyChange_style: function (property, value) {
            this.refresh();
        }, propertyChange_cols: function (property, value) {
            this.refresh();
        }, propertyChange_rows: function (property, value) {
            this.refresh();
        }, propertyChange_resize: function (property, value) {
            this.refresh();
        }, getAuthType: function () {
            if (this.config.fieldName && this.config.page.authSet) {
                var entityName = this.config.page.config.entityName;
                if (this.config.page.authSet[entityName + "/" + this.config.fieldName]) {
                    return this.config.page.authSet[entityName + "/" + this.config.fieldName].AuthType;
                }
            }
            return null;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Field, smat.dynamics.Control);
})();