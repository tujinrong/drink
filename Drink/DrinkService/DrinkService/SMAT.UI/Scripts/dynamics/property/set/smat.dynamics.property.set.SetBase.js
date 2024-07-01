
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  SetBase
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.property.set.SetBase = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.property.set.SetBase.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.initDom();

            this.setData();
           
        }, initDom: function () {

            if ($(this.config.container).hasClass("form-horizontal") == false) {
                $(this.config.container).addClass("form-horizontal");
            }

        }, setData: function () {

            var propertiedConfig = this.getPropertiesConfig();

            if (propertiedConfig) {
                this._setDataByPropertiesConfig(propertiedConfig)
            }

            if (this._setAbjustUi) {
                this._setAbjustUi();
            }

        }, getPropertiesConfig: function () {

        }, _setDataByPropertiesConfig: function (config) {

            $(this.config.container).children().remove();

            var groups = config.groups;
            for (var gKey in groups) {
                var group = groups[gKey];

                var container = $(this.config.container);
                if (group.legend) {
                    container = $('<fieldset class="s-div" style="margin:4px;padding:5px 30px 5px 5px;width:90%;"><legend style="">' + group.legend + '：</legend></fieldset>').appendTo($(this.config.container));
                    $('<div style="clear: both;" />').appendTo($(this.config.container));
                }

                var items = group.items;
                for (var k in items) {
                    var item = items[k];
                    var row = $('<div class="row" style="margin:4px;"></div>').appendTo(container);
                    var inputBox = $('<div class=" form-group" style=""></div>').appendTo(row);
                    $('<label class="input-s-md control-label" style="float:left;width: 136px;">' + item.title + '</label>').appendTo(inputBox);
                    
                    var style = "";
                    if (item.style) style = item.style;

                    var attr = "";
                    if (item.attr) attr = item.attr;

                    var cssClass = "";
                    if (item.cssClass) cssClass = item.cssClass;


                    var input = $('<input name="' + item.key + '" class="' + cssClass + '" style="float:left; ' + style + '" ' + attr + '/>').appendTo(inputBox);
                    if (smat[item.type] != undefined) {
                        var tempConfig = smat.globalObject.clone(item, { "control": 1, "valueConfig": 1 });

                        if (item.control && item.valueConfig) {
                               
                            tempConfig.value = item.valueConfig[item.key];
                            
                            if (item.type == "CheckBox") {
                                tempConfig.checked = item.valueConfig[item.key];
                            }
                        }

                        if (item.defaultValue && (tempConfig.value != false) && (!tempConfig.value)) {
                            tempConfig.value = item.defaultValue;

                            if (item.control && item.valueConfig) {
                                item.control.propertyChange({ id: item.key }, tempConfig.value, item.valueConfig);
                            }
                            
                        }

                        //change event
                        tempConfig.change = function (e) {
                            //update property
                            var control = e.ui.config.control;
                            var valueConfig = e.ui.config.valueConfig;

                            if (control && valueConfig) {
                                var value = e.ui.value();

                                control.propertyChange({ id: e.ui.config.key }, value, valueConfig);
                            }

                            if (e.ui.config.onChange) {
                                e.ui.config.onChange(e);
                            }

                        }

                        tempConfig.target = input;
                        var ui = new smat[item.type](tempConfig);
                        ui.config.control = item.control;
                        ui.config.valueConfig = item.valueConfig;

                        
                        if (item.visible == false) {
                            row.hide();
                        }
                    }

                    if (item.memo) {
                        $('<label class="control-label" style="margin-left: 5px;">' + item.memo + '</label>').appendTo(inputBox);
                    }
                }
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.property.set.SetBase, smat.dynamics.Element);

})();