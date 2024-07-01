
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdUserControlDesigner = function (config) {

        var uiNode = null;
        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            uiNode = new smat.dynamics.UserControlDesigner(config);
        });
        return uiNode;
    };

    smat.dynamics.UserControlDesigner = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        var result = this.init();

        return result;
    };

    smat.dynamics.UserControlDesigner.prototype = {
        init: function () {
            var self = this;

            this.mainProjID = this.config.projID;
            this.mainEntityName = this.config.entityName;
            this.mainUserControlName = this.config.userControlName;
            this.mainTemplateType = this.config.templateType;

            this.mode = "edit";

            if (this.config.userControlName == undefined || this.config.userControlName == "" || this.config.mode == "new") {
                this.mode = "new";

                if (this.config.userControlName == undefined || this.config.userControlName == "") {
                    this.getNewName(function (result) {
                        if (result != undefined) {
                            self.mainEntityName = result.entityName;
                            self.mainUserControlName = result.name;

                            self.initData();
                        } else {
                            //window.location.href = smat.dynamics.commonURL.formList + "?type=" + self.mainTemplateType;
                            return null;
                        }
                    });
                } else {
                    this.initData();
                }

            } else {
                this.initData();
            }

            return this;

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
        }, initPage: function () {

            this.page = new smat.dynamics.PageUserControl({
                projID: this.mainProjID,
                entityName: this.mainEntityName,
                name: this.mainUserControlName,
                contextOn: this.centerPane,
                type: this.mainTemplateType,
                designer: this,
                designing: true
            });
            this.page.propertysPanel = this.propertysPanel;

            if (this.config.titleTarget != undefined) {
                this.config.titleTarget.text(this.mainUserControlName);
            }

            this.page.setEntity(this.data);

            //this.initEntity();
        }, openForm: function () {
            var self = this;
            smat.service.openLoding();
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getUserControl,
                params: {
                    ProjID: this.mainProjID,
                    EntityName: this.mainEntityName,
                    UserControlName: this.mainUserControlName
                },
                success: function (form) {
                    //alert(result);
                    self.page.propertysPanel = self.propertysPanel;
                    self.page.setForm(form);
                }
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.UserControlDesigner, smat.dynamics.Designer);

})();