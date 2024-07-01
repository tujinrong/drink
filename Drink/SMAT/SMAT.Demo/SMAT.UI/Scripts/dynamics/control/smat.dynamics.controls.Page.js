
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Page
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.Page = function (config) {
        //默认属性
        this.setConfig({
            type: "Page"
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

    smat.dynamics.Page.prototype = {
        /**
           * 初期化
           * @name init
           * @methodOf smat.DatePicker.prototype
           */
        init: function () {


            var self = this;

            this.previewing = false;
            this.editViewList = new Array();
            this.children = new smat.hashMap();

            this.skinZindex = 1000;
            var designClass = (this.config.designing == true) ? "designing designing-page designing-drop " : "";
            this.body = $('<section id="page_'+this.uuid+'" class="scrollable wrapper s-dy-page ' + designClass + '"></section>').appendTo(this.config.contextOn);

            //if (this.config.designing == true) {
            //    this.body.parent().bind('click', function (e) {
            //        self.active();
            //    });
            //}

            if (smat.dynamics.logicHandler == undefined) {
                smat.dynamics.logicHandler = new smat.dynamics.logic.Logic();
            }
        }, active: function () {
            if (this.activeControl == this) {
                return;
            }
            if (this.activeControl) {
                this.activeControl.unActive();
            }

            this.activeControl = this;

            this.propertysPanel.setCurrentControl(this, this.getPropertyConfig(), this.config);
        },
        unActive: function () {
            this.activeControl = undefined;
            
            this.propertysPanel.clear();
           
            this.modalClear();
        }, getCustomPropertyConfig: function () {
            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'title',
			         type: 'text',
			         id: 'title',
			         cmt: 'title',
			         propType: "prop"
			     }
            );

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'loaded',
			         type: 'Logic',
			         id: 'loaded',
			         cmt: 'loaded',
			         eventKey: 'page_loaded',
                     propType:"event"
			     }
            );

            this.editPropertyConfig.push(
			     {
			         group: 'base',
			         caption: 'dataRefresh',
			         type: 'Logic',
			         id: 'dataRefresh',
			         cmt: 'dataRefresh',
			         eventKey: 'page_dataRefresh',
			         propType: "event"
			     }
            );

            var pageTemplate = this.config.designer.template;
            if (pageTemplate.config.type == "Refer") {
                this.editPropertyConfig.push(
                    {
                        group: 'refer',
                        caption: 'title',
                        type: 'text',
                        id: 'title',
                        cmt: 'title',
                        eventKey: 'title',
                        propType: "prop"
                    },
                     {
                         group: 'refer',
                         caption: 'loadAllUrl',
                         type: 'text',
                         id: 'loadAllUrl',
                         cmt: 'loadAllUrl',
                         eventKey: 'loadAllUrl',
                         propType: "prop"
                     },
                     {
                         group: 'refer',
                         caption: 'loadOneUrl',
                         type: 'text',
                         id: 'loadOneUrl',
                         cmt: 'loadOneUrl',
                         eventKey: 'loadOneUrl',
                         propType: "prop"
                     },
                     {
                         group: 'refer',
                         caption: 'autoCompleteUrl',
                         type: 'text',
                         id: 'autoCompleteUrl',
                         cmt: 'autoCompleteUrl',
                         eventKey: 'autoCompleteUrl',
                         propType: "prop"
                     },
                     {
                         group: 'refer',
                         caption: 'doCache',
                         type: 'text',
                         id: 'doCache',
                         cmt: 'doCache',
                         eventKey: 'doCache',
                         propType: "prop"
                     },
                     {
                         group: 'refer',
                         caption: 'valueField',
                         type: 'text',
                         id: 'valueField',
                         cmt: 'valueField',
                         eventKey: 'valueField',
                         propType: "prop"
                     },
                     {
                         group: 'refer',
                         caption: 'displayField',
                         type: 'text',
                         id: 'displayField',
                         cmt: 'displayField',
                         eventKey: 'displayField',
                         propType: "prop"
                     },
                     {
                         group: 'refer',
                         caption: 'width',
                         type: 'text',
                         id: 'width',
                         cmt: 'width',
                         eventKey: 'width',
                         propType: "prop"
                     },
                     {
                         group: 'refer',
                         caption: 'height',
                         type: 'text',
                         id: 'height',
                         cmt: 'height',
                         eventKey: 'height',
                         propType: "prop"
                     }
                );
            }

        }, modalTarget: function (control) {
            this.modalControl = control;
            this.modalControlZindex = control.editSkinBody.css('z-index');
            this.modalControlBkColor = control.editSkinBody.css('background-color');
            
            $("<div class='page-modal' style='z-index:19990'></div>").appendTo(this.body);
            control.editSkinBody.css('z-index', "19999");
            control.editSkinBody.css('background-color', "#fff");
        }, modalClear: function () {
            
            if (this.modalControl == undefined) {
                return;
            }

            this.body.find(".page-modal").remove();
            this.modalControl.editSkinBody.css('z-index', this.modalControlZindex);
            this.modalControl.editSkinBody.css('background-color', this.modalControlBkColor);

            this.modalControl = undefined;
            this.modalControlZindex = undefined;
            this.modalControlBkColor = undefined;
        },
        addChild: function (config) {
            var type = config.type;
            if (smat.dynamics[config.type] != undefined) {

                //designing
                config.designing = this.config.designing;
                config.seq = this.children.length + 1;
                

                //page: this,
                config.page = this;
                config.parent = this;

                var child = new smat.dynamics[config.type](config);
                this.children.set(config.name, child);

                return child;
            }
        }, save: function (handle) {

            if (this.config.name == "") {
                this.saveFor();
                return;
            }

            var params = this.getSaveParams();

            smat.service.openLoding();

            smat.service.loadJosnData({
                url: this.getSaveUrl(),
                params: params,
                success: function (result) {
                    if (handle) {
                        handle(result)
                    }
                    smat.service.notice({ msg: "保存完了しました。" });
                }
            })

        }, getSaveUrl: function () {
            return smat.dynamics.commonURL.saveForm;
        }, saveFor: function (handle) {
            var self = this;
            this.getNewFormName(function (result) {
                if (result) {
                    self.config.name = result.formName;
                    self.save(handle);
                }
            })
        }, getNewFormName: function (handle) {
            var self = this;
            var box = $('<section id="' + this.uuid + '_newName" class="panel panel-default " style="margin: 0;padding: 10px;height: 120px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">名称</label><input id="_FilterName_NEW" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(box);
            var newNameInput = box.find("#_FilterName_NEW");
            newNameInput.val(this.config.name);
            
            var newNameBtn = box.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if (name == "") {
                        alert("【名称】を入力してください。");
                        newNameInput.focus();
                        return;
                    }

                    var isExist = self.checkNameExist(self.config.entityName, name);
                    
                    if (isExist == true) {
                        alert("名称:【" + name + "】 が既に使用しています。");
                        newNameInput.focus();
                        return;
                    }

                    smat.service.closeForm({
                        contentId: self.uuid + '_newName',
                        result: {
                            formName: name
                        }
                    });
                }
            })


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: box,
                width: "410px",
                top: "20%",
                title: "名前を付けて保存",
                afterClose: function (result) {

                    handle(result);
                }
            });

        }, checkNameExist: function (EntityName, name) {
            var self = this;
            var isExist = false;
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.checkFormExist,
                async: false,
                params: {
                    ProjID: self.config.projID,
                    EntityName: EntityName,
                    FormName: name
                },
                success: function (result) {
                    //alert(result);
                    isExist = result;
                }
            });
            return isExist;
        }, getSaveParams: function () {
            var params = {};

            params.Form = {
                ProjID: this.config.projID,
                EntityName: this.config.entityName,
                FormName: this.config.name,
                FormDesc: this.config.desc,
                FormType: this.config.type,
                FormOptions: this.getSaveOptions(),
            }
            params.Controls = new Array();
            //child
            this.getSaveControls(params.Controls)

            params.Filters = this.entity.FilterList;
            params.FilterControls = this.entity.FilterControlList;

            params.Views = this.editViewList;

            return params;
        }, getSaveParamsTree: function () {
            var params = {};

            params.Form = {
                ProjID: this.config.projID,
                EntityName: this.config.entityName,
                FormName: this.config.name,
                FormDesc: this.config.desc,
                FormType: this.config.type,
                FormOptions: this.getSaveOptions(),
            }
            //child
            params.Form.Controls = this.getSaveControlsTree();

            params.Filters = this.entity.FilterList;
            params.FilterControls = this.entity.FilterControlList;

            params.Views = this.editViewList;

            return params;
        }, setEntity: function (entity) {
            this.entity = entity;
        }, createNewView: function (config) {

            var tempViewName = config.ViewName;
            var tempIndex = 0;
            while (smat.service.getItemByKey(this.entity.ViewList, "ViewName", tempViewName) != undefined) {
                tempIndex++;
                tempViewName = config.ViewName + tempIndex
            }
            config.ViewName = tempViewName;

            var view = {
                ProjID: this.config.projID,
                EntityName: this.config.entityName,
                ViewName: config.ViewName,
                ViewDesc: config.ViewDesc,
                ItemList: new Array(),
                ViewFilterList: new Array(),
            }

            if (this.editViewList == undefined) {
                this.editViewList = new Array();
            }
            this.editViewList.push(view);

            return view;
        }, setForm: function (form) {
            this.setConfig(smat.service.strToJson(form.FormOptions));
            this.setFormData(form.Controls);
            if (this.config.designing == true) {
                this.active();
            } else {
                smat.service.uiAfterInit(this.body);
                this.trigger(this.config.loaded, { sender: this });
            }
        }, afterSetFormData: function () {

            if (this.config.designer != true && this.pageLoadEventControl != undefined) {
                for(var key in this.pageLoadEventControl){
                    var ctl = smat.dynamics.uiMap.data[this.pageLoadEventControl[key]];

                    if (ctl != undefined && ctl.onPageLoad != undefined) {
                        ctl.onPageLoad();
                    }
                }
            }

        }, dataRefresh: function (result) {
            this.trigger(this.config.dataRefresh, { sender: this, result: result });
        }, handleOnPageLoad: function (control) {

            if(this.pageLoadEventControl == undefined){
                this.pageLoadEventControl = new Array();
            }

            this.pageLoadEventControl.push(control.uuid);

        }, preview: function () {
            var Controls = this.getSaveParamsTree().Form.Controls;
            var self = this;

            if ($("#designer_form_main_content").length > 0) {
                var box = $('<section id="' + this.uuid + '_previewBox" class="panel panel-default " style="margin: 0;padding: 0px; height: 100%;border: none;"></section>');

                smat.service.openForm({
                    fillTarget: "designer_form_main_content",
                    contentDom: box,
                    afterOpen: function () {
                        
                        var prePage = new smat.dynamics.Page({
                            projID: self.config.projID,
                            entityName: self.config.entityName,
                            name: self.config.name,
                            preview: true,
                            contextOn: box,
                            loaded: self.config.loaded
                        });
                        self.previewing = true;
                        self.getSaveParams();
                        prePage.entity = self.entity;
                        prePage.editViewList = self.editViewList;
                        prePage.setFormData(Controls);
                        smat.service.uiAfterInit(prePage.body);
                        prePage.trigger(prePage.config.loaded, { sender: prePage });
                    },
                    afterClose: function (result) {
                        self.previewing = false;
                    }
                });
            } else {
                var box = $('<section id="' + this.uuid + '_previewBox" class="panel panel-default " style="margin: 0;padding: 0px; height: 100%;border: none;"></section>');

                smat.service.openForm({
                    //m_opacity: 0,
                    contentDom: box,
                    width: "80%",
                    height: "80%",
                    title: "preview",
                    afterOpen: function () {
                        var window = box.closest('.s-window-content');
                        window.css('padding', '24px 0 0 34px');
                        window.css('position', 'relative');

                        $("<div style='position: absolute;top: 0;left: 0;height: 24px;width: 100%;background: url(" + smat.global.basePath + "/SMAT.UI/images/ruleTop.jpg) no-repeat 34px 0;'></div>").appendTo(window);
                        $("<div style='position: absolute;top: 0;left: 0;height: 100%;width: 34px;background: url(" + smat.global.basePath + "/SMAT.UI/images/ruleLeft.png) no-repeat 0 24px;'></div>").appendTo(window);
                        $("<div style='position: absolute;top: 0;left: 0;width: 34px;height: 25px; background-color: #000;'></div>").appendTo(window);

                        self.previewing = true;
                        var prePage = new smat.dynamics.Page({
                            projID: self.config.projID,
                            entityName: self.config.entityName,
                            name: self.config.name,
                            preview: true,
                            contextOn: box,
                            loaded: self.config.loaded
                        });
                        self.getSaveParams();
                        prePage.entity = self.entity;
                        prePage.editViewList = self.editViewList;
                        prePage.setFormData(Controls);
                        smat.service.uiAfterInit(prePage.body);
                        prePage.trigger(prePage.config.loaded, { sender: prePage });
                    },
                    afterClose: function (result) {
                        self.previewing = false;
                    }
                });
            }
        }, closePreview: function () {
            if (this.previewing == true) {
                smat.service.closeForm({
                    contentId: this.uuid + "_previewBox"
                });
            } 
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Page, smat.dynamics.Control);

  
})();