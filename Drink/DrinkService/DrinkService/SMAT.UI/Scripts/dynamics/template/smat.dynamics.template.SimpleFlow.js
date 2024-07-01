
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleFlow = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleFlow"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleFlow.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.tools = new Array();

            this.tools.push(new smat.dynamics.tool.FlowControl({
                page: this.page,
                name: "Control"
            }));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));
           
        }, templateBuild: function () {
            
           

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SimpleFlow, smat.dynamics.template.BaseTemplate);

})();