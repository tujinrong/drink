(function () {
    ///////////////////////////////////////////////////////////////////////
    //  TreeView
    ///////////////////////////////////////////////////////////////////////
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
	     * @methodOf smat.Grid.prototype
	     */
        init: function () {

            //初始化TreeView
            asmat.destroy($("#" + this.config.id));
            if ($("#" + this.config.id).data("asmatTreeView")) {
                $("#" + this.config.id).data("asmatTreeView").destroy();
            }
            $("#" + this.config.id).children().remove();

            this.tree = $("#" + this.config.id).asmatTreeView(this.getConfigForTreeView());
            $("#" + this.config.id).data("asmatTreeView").expand(".s-item");
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
            if ($("#" + this.config.id).data("asmatTreeView")) {
                //$("#" + this.config.id).data("asmatTreeView").destroy();
                $("#" + this.config.id).data("asmatTreeView").setDataSource(newDataSource);
                $("#" + this.config.id).data("asmatTreeView").expand(".s-item");
            } else {
                asmat.destroy($("#" + this.config.id));
                if ($("#" + this.config.id).data("asmatTreeView")) {
                    $("#" + this.config.id).data("asmatTreeView").destroy();
                }
                $("#" + this.config.id).children().remove();
                this.tree = $("#" + this.config.id).asmatTreeView(this.getConfigForTreeView(newDataSource));
            }

        }
    };
    // extend Node
    smat.globalObject.extend(smat.TreeView, smat.UI);
})();