
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.UserControl = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "UserControl"
        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.UserControl.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.tools = new Array();

            this.tools.push(new smat.dynamics.tool.Filter({
                page: this.page,
                name: smat.service.optionSet("DyOptionText.Filter")
            })); 

            this.tools.push(new smat.dynamics.tool.Control({
                page: this.page,
                name: smat.service.optionSet("DyOptionText.Control")
            }));

            if (smat.dynamics.tool.MobileControl) {
                this.tools.push(new smat.dynamics.tool.MobileControl({
                    page: this.page,
                    name: "MobileControl"
                }));
            }

            //this.tools.push(new smat.dynamics.tool.Filed({
            //    page: this.page,
            //    name: "Entity"
            //}));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));
           
        }, templateBuild: function () {
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
    smat.globalObject.extend(smat.dynamics.template.UserControl, smat.dynamics.template.BaseTemplate);

})();