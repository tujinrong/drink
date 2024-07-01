(function () {
    ///////////////////////////////////////////////////////////////////////
    //  ListView
    ///////////////////////////////////////////////////////////////////////
    /**
	 * 
	 * @constructor
	 * @param {Object} config
	 */
    smat.ListView = function (config) {

        //默认属性
        this.setConfig({
            dataSource: []

        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.ListView.prototype = {

        /**
	     * 初期化
	     * @name init
	     * @methodOf smat.Grid.prototype
	     */
        init: function () {

            //初始化grid
            asmat.destroy($("#" + this.config.id));
            if ($("#" + this.config.id).data("asmatListView")) {
                $("#" + this.config.id).data("asmatListView").destroy();
            }
            $("#" + this.config.id).children().remove();
            this.list = $("#" + this.config.id).asmatListView(this.getConfigForListView());

        },
        getConfigForListView: function (newDataSource) {
            var self = this;

            if (newDataSource != undefined) {
                this.config.dataSource = null;
                this.config.dataSource = newDataSource;
            }

            var c = {};

            c.dataSource = this.config.dataSource;

            c.selectable = true;
            if (this.config.selectable != undefined) {
                c.selectable = this.config.selectable;
            }

            if (this.config.template != undefined) {
                c.template = this.config.template;
            }

            if (this.config.altTemplate != undefined) {
                c.altTemplate = this.config.altTemplate;
            }

            if (this.config.change != undefined) {
                c.change = this.config.change;
            }


            return c;
        },
        reload: function () {

        }, setDataSource: function (newDataSource) {
            asmat.destroy($("#" + this.config.id));
            if ($("#" + this.config.id).data("asmatListView")) {
                $("#" + this.config.id).data("asmatListView").destroy();
            }
            $("#" + this.config.id).children().remove();

            this.list = $("#" + this.config.id).asmatListView(this.getConfigForListView(newDataSource));
        }

    };
    // extend Node
    smat.globalObject.extend(smat.ListView, smat.UI);
})();