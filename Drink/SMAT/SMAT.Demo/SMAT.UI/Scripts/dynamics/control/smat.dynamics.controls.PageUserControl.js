
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Page
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.PageUserControl = function (config) {
        //默认属性
        this.setConfig({
            type: "PageUserControl"
        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        this.init();

        //设计器初期化
        this.initEditSkin();

        return this;
    };

    smat.dynamics.PageUserControl.prototype = {
       getSaveUrl: function () {
           return smat.dynamics.commonURL.saveUserControl;
       }, checkNameExist: function (EntityName, name) {
           var self = this;
           var isExist = false;
           smat.service.loadJosnData({
               url: smat.dynamics.commonURL.checkUserControlExist,
               async: false,
               params: {
                   ProjID: self.mainProjID,
                   EntityName: EntityName,
                   UserControlName: name
               },
               success: function (result) {
                   //alert(result);
                   isExist = result;
               }
           });
           return isExist;
       }, getSaveParams: function () {
           var params = {};

           params.UserControl = {
               ProjID: this.config.projID,
               EntityName: this.config.entityName,
               UserControlName: this.config.name,
               UserControlDesc: this.config.desc,
               UserControlType: this.config.type,
               UserControlOptions: this.getSaveOptions(),
           }
           params.Controls = new Array();
           //child
           this.getSaveControls(params.Controls)

           params.UserControl.Controls = params.Controls;
           params.Controls = undefined;
           return params;
       }, getSaveParamsTree: function () {
           var params = {};

           params.Form = {
               ProjID: this.config.projID,
               EntityName: this.config.entityName,
               UserControlName: this.config.name,
               UserControlDesc: this.config.desc,
               UserControlType: this.config.type,
               UserControlOptions: this.getSaveOptions(),
           }
           //child
           params.Form.Controls = this.getSaveControlsTree();

           return params;
       }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.PageUserControl, smat.dynamics.Page);

  
})();