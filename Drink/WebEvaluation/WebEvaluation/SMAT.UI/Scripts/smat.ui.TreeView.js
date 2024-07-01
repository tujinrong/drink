(function () {
    ///////////////////////////////////////////////////////////////////////
    //  TreeView
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatTreeView = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.TreeView(config);
        });

    };
    /**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
    smat.TreeView = function (config) {

        //默认属性
        this.setConfig({
            dataSource: []

        });

        this.setConfig(config);

        //共通初始化
        this.initCommon();

        //初期化
        this.init();

        return this;
    };

    smat.TreeView.prototype = {

        /**
	     * 初期化
	     * @name init
	     * @methodOf smat.TreeView.prototype
	     */
        init: function () {

            var self = this;

            var uuid = smat.service.uuid();
            $(this.config.target).attr('uuid', uuid);
            smat.global.uiMap.set(uuid, this);
            //this.config.checkboxes = { checkChildren: true };
            var uiConfig = smat.globalObject.clone(this.config);
            uiConfig.select = function (e) {
                var dataItem = this.dataItem(e.node);
                self.trigger(self.config.select, dataItem);
            }
            uiConfig.check = function (e) {
                var dataItem = this.dataItem(e.node);
                self.trigger(self.config.check, dataItem);
            }
            $(this.config.target).asmatTreeView(uiConfig);

            this.uiControl = $(this.config.target).data("asmatTreeView");

            if (this.cBool(uiConfig.visible) == false) {
                this.visible(this.cBool(uiConfig.visible));
            }

        }, destroy: function () {
            smat.global.uiMap.remove($(this.config.target).attr('uuid'));
            if (this.uiControl) {
                this.uiControl.destroy();
            }

        }, enable: function (enable) {
            this.uiControl.enable(enable);

        }, click: function () {
            $(this.config.target).click();
        },
        visible: function (visibleFlag) {
            if (visibleFlag == false) {
                $(this.config.target).hide();
            } else {
                $(this.config.target).show();
            }
        },
        getConfigForTreeView: function (newDataSource) {

            var self = this;
            if (newDataSource != undefined) {
                this.config.dataSource = null;
                this.config.dataSource = newDataSource;
            }

            var c = {};

            if (this.config.checkboxes != undefined) {
                c.checkboxes = this.config.checkboxes;
            }

            if (this.config.template != undefined) {
                c.template = this.config.template;
            }

            c.dataSource = this.config.dataSource;


            if (this.config.change != undefined) {
                c.change = this.config.change;
            }

            return c;
        },
        reload: function () {

        }, setDataSource: function (newDataSource) {
            this.uiControl.setDataSource(newDataSource);

        },expand: function(selector){
            this.uiControl.expand(selector);
        },setTreeDataSource: function(dataSource){
            var treeDataSource = this.adjustData(dataSource, '0', this.config.filterFunc);
            this.setDataSource(treeDataSource);
        },adjustData: function (dataList, pid,filterFunc) {
            var nodes = [];
            for (var i in dataList) {
                var dataItem = dataList[i];

                if (filterFunc) {
                    if (filterFunc(dataItem) == false) continue;
                }

                //var subDataList = dataList.remove(dataItem);

                if (dataItem[this.config.pidField] == pid) {
                    children = this.adjustData(dataList, dataItem[this.config.idField], filterFunc);
                    dataItem.items = children;

                    nodes.push(dataItem);
                }
            }
            return nodes;
        }
    };
    // extend Node
    smat.globalObject.extend(smat.TreeView, smat.UI);
})();