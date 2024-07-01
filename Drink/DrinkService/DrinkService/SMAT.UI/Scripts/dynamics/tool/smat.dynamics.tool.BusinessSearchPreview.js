
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.BusinessSearchPreview
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.BusinessSearchPreview = function (config) {
        //默认属性
        this.setConfig({
            page: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    /**
    * 初期化
    * @name init
    * @methodOf smat.Control.prototype
    */
    smat.dynamics.tool.BusinessSearchPreview.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

           
            this.boxUid = smat.service.uuid();
            this.config.propertyContainer.prop("id", this.boxUid);

        }, onActivate: function () {
            this.setPropertyData();
        }, setPropertyData: function () {

            if (this.config.page.config.designer.saving != true) {
                if (this.config.page.config.designer.checkSave("preview")) {
                    this.config.page.preview(this.boxUid);
                } else {
                    this.config.page.closePreview();
                }
            }
            

        }, onPageLoad: function () {
            
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.BusinessSearchPreview, smat.dynamics.tool.BaseTool);

})();