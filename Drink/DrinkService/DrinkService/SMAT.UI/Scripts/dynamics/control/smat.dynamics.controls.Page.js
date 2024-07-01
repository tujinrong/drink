
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

            //
            this.pagerSender = new smat.pagerSender({
                dynamics: true,
                EntityName: this.config.entityName,
                PageName: this.config.name,
                ProjID: this.config.projID,
                PageId: "page_" + this.uuid,
                parentPageId: this.config.parentPageId,
                pageParams: this.config.pageParams
            });

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
			         caption: 'entityName',
			         type: 'text',
			         id: 'entityName',
			         cmt: 'entityName',
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
                     },
                     {
                         group: 'refer',
                         caption: 'autoTemplate',
                         type: 'Template',
                         id: 'autoTemplate',
                         cmt: 'autoTemplate',
                         eventKey: 'refer_autoTemplate',
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
            var self = this;

            this.config.designer.saving = true;
            if (this.config.designer.checkSave() == false) {
                self.config.designer.saving = false;
                return;
            }

            if (this.config.name == "") {
                this.saveFor();
                return;
            }

            if (self.isSaveFor == true) {
                if (smat.dynamics.onSaveFor) {
                    smat.dynamics.onSaveFor(self);
                }
            }

            var params = this.getSaveParams();

            smat.service.openLoding();

            smat.service.loadJosnData({
                url: this.getSaveUrl(),
                params: params,
                success: function (result) {
                    self.config.designer.saving = false;

                    smat.service.notice({ msg: smat.service.optionSet("SysMsg.SaveSuccess") });

                    if (handle) {
                        handle(result)
                    }
                }
            })

        }, getSaveUrl: function () {
            return smat.dynamics.commonURL.saveForm;
        }, saveFor: function (handle) {
            var self = this;
            this.getNewFormName(function (result) {
                if (result) {
                    self.config.name = result.formName;
                    self.config.title = result.formDesc;
                    self.config.No = result.No;
                    //if (self.config.groupLinkDesc && result.formDesc.indexOf("- コピー") > 0) self.config.groupLinkDesc = self.config.groupLinkDesc + "- コピー";

                    if (self.cloneViewUis) {
                        for (var i in self.cloneViewUis) {
                            var ui = self.cloneViewUis[i];
                            if (ui.config.view && ui.config.view != "") {
                                var newView = self.cloneView(ui.config.view);
                                ui.config.view = newView.ViewName;
                                for (var actionIndex in ui.config.actions) {
                                    ui.config.actions[actionIndex].view = newView.ViewName;
                                }
                            }
                        }
                    }
                    self.isSaveFor = true;
                    self.save(handle);
                    self.isSaveFor = false;
                }
            })
        }, cloneView: function (viewName) {


            var viewSource = this.getEditView(viewName);

            var tempViewName = smat.service.uuid();
            var tempIndex = 0;
            while (this.getEditView(tempViewName) != undefined) {
                tempIndex++;
                tempViewName = config.ViewName + tempIndex
            }
            
            var view = {
                ProjID: this.config.projID,
                EntityName: this.config.entityName,
                ViewName: tempViewName,
                ViewDesc: tempViewName,
                ItemList: new Array(),
                Belong: this.config.belong,
                CreatedBy: smat.dynamics.role.type,
                ViewFilterList: new Array(),
            }

            for (var i in viewSource.ItemList) {
                var item = smat.globalObject.clone(viewSource.ItemList[i])
                item.ViewName = tempViewName;
                view.ItemList.push(item);
            }


            //viewFilters
            for (var i in viewSource.ViewFilterList) {
                var filter = smat.globalObject.clone(viewSource.ViewFilterList[i]);

                var filterSource =  smat.globalObject.clone(smat.service.getItemByKey(this.entity.FilterList, "FilterName", filter.FilterControlName));

                var filterControlSource = smat.globalObject.clone(smat.service.getItemByKey(this.entity.FilterControlList, "FilterControlName", filter.FilterControlName));

                var newFilterName = smat.service.uuid();

                filterSource.FilterName = newFilterName;
                filterControlSource.FilterNames = newFilterName;
                filterControlSource.FilterControlName = newFilterName;
                this.entity.FilterList.push(filterSource);
                this.entity.FilterControlList.push(filterControlSource);

                filter.FilterControlName = newFilterName;
                filter.ViewName = tempViewName;
                view.ViewFilterList.push(filter);
            }

            if (this.editViewList == undefined) {
                this.editViewList = new Array();
            }

            this.editViewList.push(view);

            //
            smat.service.delItemByKey(this.editViewList, "ViewName", viewName);

            return view;
           
        }, getNewFormName: function (handle) {
            var self = this;
            var box = $('<section id="' + this.uuid + '_newName" class="panel panel-default " style="margin: 0;padding: 10px;height: 160px;"></section>');
            $('<div class="row" id="newNameInput_row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">CD</label><input id="_FilterName_NEW" class="s-textbox input-s" style="width: calc(100% - 100px);"/></div></div>').appendTo(box);
            $('<div class="row" id="newNoInput_row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">管理番号</label><input id="_FilterNo_NEW" class="s-textbox input-s" /></div></div>').appendTo(box);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">名称</label><input id="_FilterDesc_NEW" class="s-textbox input-s" style="width: calc(100% - 100px);"/></div></div>').appendTo(box);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group text-center" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(box);

            var newNameInput = box.find("#_FilterName_NEW");                            
            newNameInput.val(this.config.name);

            var newNoInput = box.find("#_FilterNo_NEW");
            var noTemp = Number(this.config.No);
            if (noTemp > 0) {
                noTemp = (noTemp + 1);
            } else {
                noTemp = this.config.No;
            }
            newNoInput.val(noTemp);
            box.find("#newNoInput_row").hide();

            var newDescInput = box.find("#_FilterDesc_NEW");

            var newTitle = this.config.title + "- コピー";

            if (smat.dynamics.getNewFormTitle) {
                newTitle = smat.dynamics.getNewFormTitle(this);
            }

            newDescInput.val(newTitle);


            if (smat.dynamics.isUser()) {
                newNameInput.val(smat.service.uuid());
                box.find("#newNameInput_row").hide();
            }
            
            var newNameBtn = box.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if (name == "") {
                        smat.service.notice({ msg: "【CD】を入力してください。", type: "error" });
                        newNameInput.focus();
                        return;
                    }

                    var noStr = newNoInput.val();
                    if (noStr == "") {
                        //smat.service.notice({ msg: "【管理番号】を入力してください。", type: "error" });
                        //newNoInput.focus();
                        //return;
                    }

                    var desc = newDescInput.val();
                    if (desc == "") {
                        smat.service.notice({ msg: "【名称】を入力してください。", type: "error" });
                        newDescInput.focus();
                        return;
                    }

                    var isExist = false
                    if (smat.dynamics.checkNameExist) {
                        isExist = smat.dynamics.checkNameExist(self)
                    } else {
                        isExist = self.checkNameExist(self.config.entityName, name, desc);
                    }
                    
                    if (isExist == true) {
                        if (smat.dynamics.isUser()) {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit", "" + smat.service.optionSet("DyOptionText.Name")), type: "error" });
                            newDescInput.focus();
                        } else {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit", "CD/" + smat.service.optionSet("DyOptionText.Name")), type: "error" });
                            newNameInput.focus();
                        }

                        return;
                    }

                    smat.service.closeForm({
                        contentId: self.uuid + '_newName',
                        result: {
                            formName: name,
                            formDesc: desc,
                            No: noStr
                        }
                    });
                }
            })


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: box,
                width: "510px",
                top: "20%",
                title: smat.service.optionSet("DyOptionText.SaveAs"),
                afterClose: function (result) {

                    handle(result);
                }
            });

        }, checkNameExist: function (EntityName, name, desc) {
            var self = this;
            var isExist = false;
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.checkFormExist,
                async: false,
                params: {
                    ProjID: self.config.projID,
                    EntityName: EntityName,
                    FormName: name,
                    FormDesc: desc
                },
                success: function (result) {
                    //alert(result);
                    isExist = result;
                }
            });
            return isExist;
        }, getSaveParams: function () {
            var params = {};

            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.createdUserName) {
                this.config.createdUserName = smat.dynamics.analysisConfig.createdUserName(this);
            }

            params.Form = {
                ProjID: this.config.projID,
                EntityName: this.config.entityName,
                FormName: this.config.name,
                FormDesc: this.config.title,
                FormType: this.config.type,
                FormCategory: this.config.category,
                CreatedBy: this.config.createdBy,
                CreatedUserName: this.config.createdUserName,
                Belong: this.config.belong,
                FormState: this.config.state,
                FormNo: this.config.No,
                GroupName: this.config.groupName,
                GroupSeq: this.config.groupSeq,
                GroupLinkDesc: this.config.groupLinkDesc,
                FormOptions: this.getSaveOptions(),
            }


            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.createdUserName) {
                this.config.createdUserName = smat.dynamics.analysisConfig.createdUserName(this);
            }

            params.Controls = new Array();
            //child
            this.getSaveControls(params.Controls)

            //params.Filters = this.entity.FilterList;
            //params.FilterControls = this.entity.FilterControlList;
            
            //Filters
            var filters = [];
            for (var i in this.entity.FilterList) {
                var f = this.entity.FilterList[i];
                var bkItems = $.Enumerable.From(this.entity.backupFilters).Where("$.FilterName == '" + f.FilterName + "'").ToArray();
                if (bkItems.length == 0) {
                    filters.push(f);
                } else {
                    var bkItem = bkItems[0];
                    if (f.FilterDesc != bkItem.FilterDesc
                        || f.Path != bkItem.Path
                        || f.ItemEntityAliasName != bkItem.ItemEntityAliasName
                        || f.FilterSql != bkItem.FilterSql
                        || f.Belong != bkItem.Belong
                        || f.FilterCategory != bkItem.FilterCategory
                        || f.CreatedBy != bkItem.CreatedBy
                        || f.FilterState != bkItem.FilterState
                        || f.IsHaving != bkItem.IsHaving) {
                        filters.push(f);
                    }
                }
            }
            params.Filters = filters;

            //FilterControls
            var filterControls = [];
            for (var i in this.entity.FilterControlList) {
                var f = this.entity.FilterControlList[i];
                var bkItems = $.Enumerable.From(this.entity.backupFilterControls).Where("$.FilterControlName == '" + f.FilterControlName + "'").ToArray();
                if (bkItems.length == 0) {
                    filterControls.push(f);
                } else {
                    var bkItem = bkItems[0];
                    if (f.FilterControlDesc != bkItem.FilterControlDesc
                                          || f.FilterNames != bkItem.FilterNames
                                          || f.UserControlName != bkItem.UserControlName
                                          || f.Belong != bkItem.Belong
                                          || f.FilterCategory != bkItem.FilterCategory
                                          || f.CreatedBy != bkItem.CreatedBy
                                          || f.FilterState != bkItem.FilterState) {
                        filterControls.push(f);
                    }
                }
            }
            params.FilterControls = filterControls;

            params.Views = this.editViewList;

            return params;
        }, getSaveParamsTree: function () {
            var params = {};

            params.Form = {
                ProjID: this.config.projID,
                EntityName: this.config.entityName,
                FormName: this.config.name,
                FormDesc: this.config.title,
                FormType: this.config.type,
                FormCategory: this.config.category,
                CreatedBy: this.config.createdBy,
                CreatedUserName: this.config.createdUserName,
                Belong: this.config.belong,
                FormState: this.config.state,
                FormNo: this.config.No,
                GroupName: this.config.groupName,
                GroupSeq: this.config.groupSeq,
                GroupLinkDesc: this.config.groupLinkDesc,
                FormOptions: this.getSaveOptions(),
            }
            //child
            params.Form.Controls = this.getSaveControlsTree();

            ////params.Filters = this.entity.FilterList;
            ////params.FilterControls = this.entity.FilterControlList;
            //debugger
            ////Filters
            //var filters = [];
            //for (var i in this.entity.FilterList) {
            //    var f = this.entity.FilterList[i];
            //    var bkItem = this.entity.backupFilters.Get(f.FilterName);
            //    if (!bkItem) {
            //        filters.push(f);
            //    } else {
            //        if (f.FilterDesc != bkItem.FilterDesc
            //            || f.Path != bkItem.Path
            //            || f.ItemEntityAliasName != bkItem.ItemEntityAliasName
            //            || f.FilterSql != bkItem.FilterSql
            //            || f.Belong != bkItem.Belong
            //            || f.FilterCategory != bkItem.FilterCategory
            //            || f.CreatedBy != bkItem.CreatedBy
            //            || f.FilterState != bkItem.FilterState
            //            || f.IsHaving != bkItem.IsHaving) {
            //            filters.push(f);
            //        }
            //    }
            //}
            //params.Filters = filters;

            ////FilterControls
            //var filterControls = [];
            //for (var i in this.entity.FilterControls) {
            //    var f = this.entity.FilterControls[i];
            //    var bkItem = this.entity.backupFilterControls.Get(f.FilterControlName);
            //    if (!bkItem) {
            //        filterControls.push(f);
            //    } else {


            //        if (f.FilterControlDesc != bkItem.FilterControlDesc
            //                              || f.FilterNames != bkItem.FilterNames
            //                              || f.UserControlName != bkItem.UserControlName
            //                              || f.Belong != bkItem.Belong
            //                              || f.FilterCategory != bkItem.FilterCategory
            //                              || f.CreatedBy != bkItem.CreatedBy
            //                              || f.FilterState != bkItem.FilterState) {
            //            filterControls.push(f);
            //        }
            //    }
            //}
            //params.FilterControls = filterControls;

            params.Views = this.editViewList;

            return params;
        }, getControlUniqueName: function (name) {

            var result = name;

            var names = this.getAllControlNames();

            var index = 0;

            while (names[result] != undefined) {
                index++;
                result = name + "" + index;
            }

            return result;
            
        }, getAllControlNames: function () {
            var names = {};

            names[this.config.name] = 1;

            if (this.children != undefined) {
                for (var ckey in this.children.data) {
                    var ctl = this.children.data[ckey];
                    ctl.getControlNames(names);
                }
            }

            return names;
        }, setEntity: function (entity) {
            this.entity = entity;
            if (entity) {
                this.entity.backupFilters = smat.globalObject.clone(this.entity.FilterList);
                this.entity.backupFilterControls = smat.globalObject.clone(this.entity.FilterControlList);

            }

        }, createNewView: function (config) {

            var tempViewName = config.ViewName;
            var tempIndex = 0;
            while (this.getEditView(tempViewName) != undefined) {
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
                Belong: this.config.belong,
                CreatedBy: smat.dynamics.role.type,
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
                if (smat.dynamics.afterPageLoad) {
                    smat.dynamics.afterPageLoad(this);
                }

                //linkgroup
                this.initLinkgroup();

                //autoAction
                this.autoAction();
            }
        }, initLinkgroup: function () {

            var self = this;
            if (this.config.groupName) {

                var params = {};
                params.request = {};
                params.request.ProjID = this.config.projID;

                params.request.DsRequests = new Array();

                var fs = "ProjID = '" + this.config.projID + "' and GroupName = N'" + this.config.groupName + "'";
                if (this.config.belong) {
                    fs += " and Belong = '" + this.config.belong + "'";
                }

                if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.getLinkGroupFormFilterStr) {
                    fs = smat.dynamics.analysisConfig.getLinkGroupFormFilterStr(this);
                }

                params.request.DsRequests.push(
                   {
                       TableName: "Y_EntityForm",
                       Filter: fs,
                       OrderBy: "GroupSeq"
                   }
                );
               
                smat.service.loadJosnData({
                    url: smat.global.basePath + smat.dynamics.commonURL.getDyDs,
                    params: params,
                    async: true,
                    success: function (result) {
                        debugger
                        var groupForms = result.ds["Y_EntityForm"];

                        if (groupForms.length > 0) {
                            groupForms = $.Enumerable.From(groupForms).OrderBy("Number($.GroupSeq)").ToArray();

                            var mSection = self.getControlByName("main_Section");
                            if (!mSection) return;
                            var firstRow = mSection.body.children(":first");

                            var linkRow = $('<div class="row text-center" style="padding-bottom: 6px;margin-top: -6px;"></div>').insertBefore(firstRow);

                            for (var index in groupForms) {
                                var lForm = groupForms[index];

                                if (lForm.FormName == self.config.name) {
                                    $('<span class="" style="margin-right:20px;margin-right:10px;border: 1px solid #428BCA;padding: 0 6px;border-radius: 10px;background-color: #428BCA;color: #fff;">' + lForm.GroupLinkDesc + '</span>').appendTo(linkRow);

                                } else {
                                    var ll = $('<a class="s-form-group-link" href="javascript:void(0)" entityName="' + lForm.EntityName + '" formName="' + lForm.FormName + '" style="margin-right:10px;border: 1px solid #DFE2E4;padding: 0 6px;border-radius: 10px;background-color: #fff;color: #428BCA;">' + lForm.GroupLinkDesc + '</a>').appendTo(linkRow);

                                    ll.bind('click', function () {
                                        self.linkToForm($(this).attr('entityName'), $(this).attr('formName'));
                                    })
                                }
                               
                            }
                        }

                        smat.service.closeLoding();
                        
                    }

                });

            }

        }, linkToForm: function (entityName, formName) {

            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.linkToForm) {
                smat.dynamics.analysisConfig.linkToForm(entityName, formName,this);
            } else {
                var projID = this.config.projID;
                var formName = formName;
                var entityName = entityName;

                var fillTarget = $(".s-form-content").attr("id");

                var formParams;
                var form = this.pagerSender.ui("search_form");
                if (form) {
                    formParams = form.getParam(form.config.actions[0]);
                }

                this.pagerSender.close();

                var w = undefined;
                if (!fillTarget) w = "96%";

                smat.service.openForm({
                    page: {
                        projID: projID,
                        entityName: entityName,
                        pageName: formName
                    },
                    fillTarget: fillTarget,
                    width: w,
                    afterClose: function (result) {

                    }
                });
            }

        }, autoAction: function () {
            var p = this.pagerSender.getFormParam();
            
            if (p && p.formParams) {
                var form = this.pagerSender.ui("search_form");
                if (form) {
                    form.setDyParams(p.formParams.request);

                    setTimeout(function () { form.doAction(form.config.actions[0]); }, 100);

                    
                }
            }
        }
        , afterSetFormData: function () {

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

        }, preview: function (fillTarget) {
            var Controls = this.getSaveParamsTree().Form.Controls;
            var self = this;

            this.closePreview();

            if ($("#" + fillTarget).length > 0) {
                var box = $('<section id="' + this.uuid + '_previewBox" class="panel panel-default " style="margin: 0;padding: 0px; height: 100%;border: none;"></section>');

                smat.service.openForm({
                    fillTarget: fillTarget,
                    width: "98%",
                    contentDom: box,
                    afterOpen: function () {
                        
                        var prePage = new smat.dynamics.Page({
                            projID: self.config.projID,
                            entityName: self.config.entityName,
                            title: self.config.title,
                            category: self.config.category,
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
                        if (smat.dynamics.afterPageLoad) {
                            smat.dynamics.afterPageLoad(prePage);
                        }
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
                            title: self.config.title,
                            category: self.config.category,
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
                        if (smat.dynamics.afterPageLoad) {
                            smat.dynamics.afterPageLoad(prePage);
                        }
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
        }, getEditView: function (viewName) {
            var view = smat.service.getItemByKey(this.editViewList, "ViewName", viewName);
            if (view == undefined) {
                view = smat.dynamics.service.getView(this.entity.ProjID, this.entity.EntityName, viewName);
                if (view) {
                    this.editViewList.push(view);
                }
            }
            return view;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Page, smat.dynamics.Control);

  
})();