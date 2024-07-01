
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.template
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.template.SimpleAppEmpty = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            type: "SimpleAppEmpty"

        });

        this.setConfig(config);

        this.page = config.page;
        //初期化
        this.init();

        return this;
    };

    smat.dynamics.template.SimpleAppEmpty.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.tools = new Array();

            //this.tools.push(new smat.dynamics.tool.Filed({
            //    page: this.page,
            //    name: smat.service.optionSet("DyOptionText.Field")
            //}));
            
            this.tools.push(new smat.dynamics.tool.MobileControl({
                page: this.page,
                name: smat.service.optionSet("DyOptionText.Control")
            }));

            this.tools.push(new smat.dynamics.tool.UserControl({
                page: this.page,
                userControlEntity: "Y_Entity",
                category: "AppUserControl",
                isTemplate: true,
                name: "userControl"
            }));

            //this.tools.push(new smat.dynamics.tool.View({
            //    page: this.page,
            //    name: "View"
            //}));
           
        }, templateBuild: function () {
            
            this.mainSection = this.page.addChild({
                type: "Layout",
                page: this.page,
                name: "main_Section",
                designing: true
            });

            this.mainSection.addChild({
                type: "Tabstrip",
                rowIndex: 2,
                text: "codeKind:SysText.Search",
                name: "tabstrip"
            });

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.template.SimpleAppEmpty, smat.dynamics.template.BaseTemplate);

})();