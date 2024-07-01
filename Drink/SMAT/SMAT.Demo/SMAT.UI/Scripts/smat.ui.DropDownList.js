(function () {
    ///////////////////////////////////////////////////////////////////////
    //  DropDownList
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDropDownList = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.DropDownList(config);
        });

    };
    /**
     * 
     * @constructor
     * @param {Object} config
     */
    smat.DropDownList = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            uitype: "asmatDropDownList"

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        //共通初始化后
        //this.afterInit();

        return this;
    };

    smat.DropDownList.prototype = {

        /**
         * 初期化
         * @name init
         * @methodOf smat.DropDownList.prototype
         */
        init: function () {


            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);

            //code mst dataSource
           
            if (this.config.dataSource == undefined 
                && this.config.codeKind != undefined) {

                this.config.dataValueField = smat.uiConfig.CodeMst.codeField;
                this.config.dataTextField = smat.uiConfig.CodeMst.nameField;

                var emptyText = "";
                if (this.config.emptyText != undefined) {
                    emptyText = this.config.emptyText;
                }
                var dataSource = smat.service.optionSet(this.config.codeKind).slice();
                
                var emptyItem = {};
                emptyItem[smat.uiConfig.CodeMst.codeField] = "";
                emptyItem[smat.uiConfig.CodeMst.nameField] = emptyText;

                dataSource.unshift(emptyItem);

                this.config.dataSource = dataSource;
            }

            var uiConfig = smat.globalObject.clone(this.config);

            uiConfig.change = function (e) {
                self.trigger(smat.event.CHANGE, e);
            }
            uiConfig.select = function (e) {
                self.trigger(smat.event.SELECT, e);
            }
            uiConfig.close = function (e) {
                self.trigger(smat.event.CLOSE, e);
            }
            uiConfig.open = function (e) {
                self.trigger(smat.event.OPEN, e);
            }
            uiConfig.filter = undefined;
            if (this.config.uitype == "asmatDropDownList") {
                $(this.config.target).asmatDropDownList(uiConfig);

            } else if (this.config.uitype == "asmatComboBox") {
                $(this.config.target).asmatComboBox(uiConfig);

            }
               
            if (this.config.dataSource == undefined) {
                this.config.dataSource = {};
            }

            this.uiControl = $(this.config.target).data(this.config.uitype);
            
            var item = this.config.dataSource[0];
            if (item != undefined) {
                $(self.config.target).attr("value", item[self.config.dataValueField]);
                $(self.config.target).attr("text", item[self.config.dataTextField]);
            }

            this.uiControl.bind("select", function (e) {
                var dataItem = this.dataItem(e.item.index());
                self.config.value = dataItem[self.config.dataValueField];
                $(self.config.target).attr("value", dataItem[self.config.dataValueField]);
                $(self.config.target).attr("text", dataItem[self.config.dataTextField]);
            });

            if (this.config.value != undefined)
            {
                this.value(this.config.value);
                $(self.config.target).attr("value", this.config.value);
            }
        }, destroy: function () {
            //alert(this.valueInput.attr('uuid'));
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            this.uiControl.destroy();

        }, setDataSource: function (data) {
            this.config.dataSource = data;
            
            if (this.config.getIgnoreItem != undefined) {
                var temp = new Array();

                var IgnoreItem = this.config.getIgnoreItem();
                for (var i = 0; i < data.length; i++) {
                    if (IgnoreItem.indexOf(data[i][this.config.dataValueField]) == -1) {
                        temp.push(data[i]);
                    }
                }

                this.uiControl.setDataSource(temp);
            } else {
                this.uiControl.setDataSource(data);
            }

            if (this.config.value != undefined)
            {
                this.value(this.config.value);
                $(this.config.target).attr("value", this.uiControl.value());
                $(this.config.target).attr("text", this.uiControl.text());
            }

        }, value: function (value) {
            if (value == undefined) {
                return this.uiControl.value();
            } else {
                this.uiControl.value(value);
            }
        }, dataItem: function (index) {
            return this.uiControl.dataItem(index);
        }, refresh: function ()
        {
            this.setDataSource(this.config.dataSource);
        },setIgnoreFunc: function(func)
        {
            this.config["getIgnoreItem"] = func;   
        }
    };
    // extend Node
    smat.globalObject.extend(smat.DropDownList, smat.UI);
})();