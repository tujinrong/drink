
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  PropertyPanel
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsPropertyPanel = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.property.Panel(config);
        });
    };

    smat.dynamics.property.Panel = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.Panel.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.propertyType = $('<div class="property-type text-right" ><input id="property_type"/></div>').appendTo(this.config.target);
            this.txtProperty_type = this.propertyType.find("#property_type");
            this.propertyPane = $('<div class="property-pane" ></div>').appendTo(this.config.target);

            var uiConfig = smat.globalObject.clone(this.config, this.optionIgnoreInfo);
            uiConfig.target = this.propertyPane;

            if (uiConfig.columns == undefined) {
                uiConfig.columns = [
                    {
                        field: "group",
                        title: "Propertys",
                        width: "100px",
                        template: "#= caption #",
                        groupHeaderTemplate: "#= value # "
                    }, {
                        field: "FIELD2",
                        title: "  ",
                        attributes: {
                            "class": "edit-cell",
                        },
                        template:this.editTemplate
                    },
                ]
            }

            if (uiConfig.dataSource == undefined) {
                uiConfig.dataSource = {
                    data: [],
                    group: { field: "group", dir: "asc" }
                }

                    
            }

            
            uiConfig.resizable = true;
            uiConfig.columnResizeHandleWidth = 6;

            uiConfig.dataBound = function (e) {
                e.sender.thead.find('tr').children("th:eq(1)").text(self.txtProperty_type.ui().value() == "prop" ? "Propertys" : "Events");
                self.dataBound(e);
            }

            

            this.txtProperty_type.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    {
                        text: "<i class='fa fa-wrench'></i>",
                        value: "prop"
                    },
                    {
                        text: "&nbsp;<i class='fa fa-flash'></i>&nbsp;",
                        value: "event"
                    }
                ],
                change: function (e) {
                    self.setCurrentControl(self.currentControl, self.propertyConfig, self.valueConfig);
                }
            });
            this.txtProperty_type = this.propertyType.find("#property_type");

            this.config.target.find(".sm-button").css("padding", ".28em .4em .24em .4em");


            this.propertyGrid = new smat.Grid(uiConfig);

            //Shortcut menu
            //this.shortcutMenu = $('<div class="s-animation-container" style="width: 500px; margin-left: -2px; padding-left: 2px; padding-right: 2px; padding-bottom: 4px; overflow: visible; display: none; position: absolute; top: -40px; z-index: 10002; right: 0px; box-sizing: content-box;"><div class="s-widget s-tooltip s-popup s-group s-reset s-state-border-up"  style="display: block; opacity: 1; position: absolute;"><div class="s-tooltip-content"></div><div class="s-callout s-callout-s" style="left: 31px;"></div></div></div>');
            ////this.shortcutMenu.appendTo(this.config.designer.target);
        }, setCurrentControl: function (control, propertyConfig, valueConfig) {
            this.currentControl = control;
            this.propertyConfig = propertyConfig;
            this.valueConfig = valueConfig;

            var data = propertyConfig;

            for (var key in data) {
                var item = data[key];

                if (valueConfig[item.id] != undefined) {
                    var value = valueConfig[item.id];
                    if (typeof (value) == 'object') {
                        for (var index in value) {
                            if (value[index]["uuid"] == undefined) {
                                value[index]["uuid"] = smat.service.uuid();
                            }
                        }
                    }
                    item.value = value;
                } else {
                    item.value = undefined;
                }
            }

            data = $.Enumerable.From(data).Where("$.propType =='" + this.txtProperty_type.ui().value() + "'").ToArray();

            this.propertyGrid.setDataSource({
                data: data,
                group: { field: "group" },
                sort: [
                    { field: "group", dir: "asc" },
                    { field: "caption", dir: "asc" }
                ]
            });

            //this.showShortcutMenu();
        }
        //, showShortcutMenu: function () {
        //    var sConfig = this.currentControl.getShortcutPropertyConfig();
        //    if (sConfig.length > 0) {
        //        this.shortcutMenu.find('.s-tooltip-content').append('<span class="property-shortcut-item s-select edit-cell-picker" ><span class="s-icon s-i-pencil"></span>項目設定</span>')
        //        this.shortcutMenu.css('width', "100%");
        //        this.shortcutMenu.appendTo(this.currentControl.editSkinBox);
        //        this.shortcutMenu.show();
        //        var sWidth = this.shortcutMenu.find('.s-tooltip-content').width() + 20
        //        this.shortcutMenu.css('width', (sWidth) + "px");
                
        //    }
        //}
        //, clearShortcutMenu: function () {
        //    this.shortcutMenu.hide();
        //    this.shortcutMenu.detach()
        //}
        , clear: function () {
            this.currentControl = undefined;
            //this.clearShortcutMenu();

            this.propertyGrid.setDataSource([]);
        }, editTemplate: function (dataItem) {

            var displayStr = dataItem.value == undefined ? "" : dataItem.value;

            //return '<input title="' + displayStr + '" value="' + displayStr + '"  class="s-textbox" style="width:100%;border: none;height: 1.5em;line-height: 1.5em;text-indent: 0;padding-right: 26px;" /><span  class="s-select edit-cell-picker"><span class="s-icon s-i-arrow-s"></span></span>';
            return '<input title="' + displayStr + '" value="' + displayStr + '"  class="s-textbox" style="width:100%;border: none;height: 1.5em;line-height: 1.5em;text-indent: 0;" />';
        }, dataBound: function (e) {
            var self = this;
            var grid = e.sender;
            $.each(e.sender.tbody.find('td.edit-cell input'), function () {
                var dataItem = grid.dataItem($(this).parent().parent());

                if (dataItem.showInPanel == false) {
                    $(this).closest('tr').hide();
                    return;
                }

                if (typeof (dataItem.value) == 'object' || dataItem.focusOnly == true) {
                    $(this).onlyFocus();
                }
                $(this).bind('focus', function () {
                    var dataItem = grid.dataItem($(this).parent().parent());
                    self.beginEdit($(this).parent(), dataItem);
                });

                $(this).bind('blur', function () {

                    return self.endEdit();
                });

                $(this).keypress(function (e) { 
                    var key = e.which; 
                    if (key == 13) {
                        self.endEdit();
                        return false;
                    }
                });

            });
        }, beginEdit: function (cell, dataItem) {
            if (this.currentCell !=undefined && this.currentCell[0] == cell[0]) {
                return;
            }

            this.currentCell = cell;
            this.currentDataItem = dataItem;

            //if (this["initEditer_" + dataItem.type] != undefined) {
            //    if (this.currentCell.find('.edit-cell-picker').length > 0) {

            //    } else {
            //        this["initEditer_" + dataItem.type]();
            //    }
                
            //}

            if (smat.dynamics.property[dataItem.type] != undefined) {
                if (this.currentCell.find('.edit-cell-picker').length > 0) {

                } else {
                    new smat.dynamics.property[dataItem.type]({
                        currentControl: this.currentControl,
                        currentDataItem: this.currentDataItem,
                        currentCell: this.currentCell,
                        valueConfig: this.valueConfig,
                        page: this.config.page||this.config.designer.page
                    });

                    this.currentCell.children('input').css('padding-right', '26px');
                }

            }

        }, endEdit: function () {

            var value = this.currentCell.children('input').val();

            if ((value != this.currentDataItem.value && (value == "" && this.currentDataItem.value == undefined) == false && (typeof (this.currentDataItem.value) != 'object'))
                || (typeof (this.currentDataItem.value) == 'object') && value == "") {

                
                if (this.currentControl.checkPropertyChanging(this.currentDataItem, value) == false) {
                    alert("option set wrong");
                    this.currentCell.children('input').focus();
                    this.currentCell.children('input').val(this.currentDataItem.value);
                    if (this.currentCell.find('.edit-cell-picker').length > 0) {
                        this.currentCell.find('.edit-cell-picker').dynamicsUI().restoreValue();
                    }
                    return false;
                }

                this.currentDataItem.value = value;
                this.currentControl.propertyChange(this.currentDataItem, value, this.valueConfig);

                return true;
            }

            if (this.config.afterEndEdit) {
                this.config.afterEndEdit(this.currentDataItem);
            }

            this.currentCell = undefined;
            this.currentDataItem = undefined;

            
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.Panel, smat.dynamics.Element);

})();