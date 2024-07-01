
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SystemMaster = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SystemMaster"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SystemMaster.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.tools = new Array();

            this.tools.push(new smat.dynamics.tool.Filter({
                page: this.page,
                name: "フィルタ"
            }));

            //this.tools.push(new smat.dynamics.tool.UserControl({
            //    page: this.page,
            //    name: "検索条件"
            //}));

            this.tools.push(new smat.dynamics.tool.Control({
                page: this.page,
                name: "コントロール"
            }));

            //this.tools.push(new smat.dynamics.tool.Filed({
            //    page: this.page,
            //    name: "Entity"
            //}));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));
           
        }, templateBuild: function () {
            this.page.createNewView({
                ViewName: this.page.config.designer.mainFormName ,
                ViewDesc: this.page.config.designer.mainFormName ,
            });

            this.mainSection = this.page.addChild({
                type: "Section",
                page: this.page,
                rowsCount: 1,
                name: "main_Section",
                designing: true
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SystemMaster, smat.dynamics.template.BaseTemplate);

})();