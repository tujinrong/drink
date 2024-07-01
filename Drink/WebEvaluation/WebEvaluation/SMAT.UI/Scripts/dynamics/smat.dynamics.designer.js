
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdDesigner = function (config) {

        var uiNode = null;
        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            uiNode = new smat.dynamics.Designer(config);
        });
        return uiNode;
    };

    smat.dynamics.Designer = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            toolPanelWidth:"180px"

        });

        this.setConfig(config);

        //共通初期化
        this.initCommon();

        //初期化
        var result = this.init();

        return result;
    };

    smat.dynamics.Designer.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {
            var self = this;

            this.mainProjID = this.config.projID;
            this.mainEntityName = this.config.entityName;
            this.mainFormName = this.config.formName;
            this.mainTemplateType = this.config.templateType;
            this.mainFormDesc = this.config.formDesc;
            
            
            this.mode = "edit";

            if (this.config.formName == undefined || this.config.formName == "" || this.config.mode == "new") {
                this.mode = "new";

                if (this.config.formName == undefined || this.config.formName == "") {
                    this.getNewName(function (result) {
                        if (result != undefined) {
                            self.mainEntityName = result.entityName;
                            self.mainFormName = result.name;

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

        }, initCommon: function () {
            var self = this;

            this.uuid = smat.service.uuid();
            smat.dynamics.uiMap.set(this.uuid, this);
        }, initData: function () {
            var self = this;
            if (!this.mainEntityName) {
                self.initDesigner();
                if (self.template.onPageLoad) {
                    self.template.onPageLoad();
                }
                return;
            }
            //===================entity=======================
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntity,
                async: false,
                params: {
                    ProjID: this.mainProjID,
                    EntityName: this.mainEntityName
                },
                success: function (result) {
                    //alert(result);

                    self.data = result;
                    self.initDesigner();
                }
            });

        }, initDesigner: function () {
            var self = this;

            location.hash = "";

            this.initDom();
            this.initPage();
            this.templatePage();

            if (this.mode == "new") {
                this.template.templateBuild();
            } else {
                this.openForm();
            }

            if (this.template.setDesignerToolbarState) {
                this.template.setDesignerToolbarState();
            } else {

                if (self.mode == "new") {
                    self.delBtn.hide();
                    self.saveForBtn.hide();
                } else {
                    self.delBtn.show();
                    self.saveForBtn.show();
                }
                this.saveTempBtn.show();
                this.saveBtn.show();
                this.cancelBtn.show();
                //this.newBtn.show();
            }
        }
        , getNewName: function (handle) {
            var self = this;
            var box = $('<section id="' + this.uuid + '_newName" class="panel panel-default " style="margin: 0;padding: 10px;height: 120px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">分類</label><input id="_NEW_Form_Entity" class="input-s" ></div></div>').appendTo(box);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">名称</label><input id="_FilterName_NEW" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(box);
            var newNameInput = box.find("#_FilterName_NEW");

            var newEntityInput = box.find("#_NEW_Form_Entity");

            newEntityInput.smatDropDownList({
                dataSource: [],
                dataValueField: "EntityName",
                dataTextField: "EntityDesc"
            });

            var newNameBtn = box.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if (name == "") {
                        alert(smat.service.optionSet("SysMsg.Required", smat.service.optionSet("DyOptionText.Name")));
                        newNameInput.focus();
                        return;
                    }

                    var isExist = self.checkNameExist(newEntityInput.ui().value(),name);
                    
                    if (isExist == true) {
                        alert(smat.service.optionSet("SysMsg.Exit", name));
                        newNameInput.focus();
                        return;
                    }

                    smat.service.closeForm({
                        contentId: self.uuid + '_newName',
                        result: {
                            name: name,
                            entityName: newEntityInput.ui().value()
                        }
                    });
                }
            })


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: box,
                width: "410px",
                top: "20%",
                title: "新しい画面",
                afterClose: function (result) {

                    handle(result);
                }
            });

            //===================entity=======================
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntityList,
                async: false,
                params: {
                    ProjID: this.mainProjID
                },
                success: function (result) {
                    //alert(result);
                    var datas = $.Enumerable.From(result).Where("$.EntityType == '1'").ToArray();
                    newEntityInput.ui().uiControl.setDataSource(datas);
                }
            });
        }, checkNameExist: function (EntityName, name) {
            var self = this;
             var isExist = false;
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.checkFormExist,
                async: false,
                params: {
                    ProjID: self.mainProjID,
                    EntityName: EntityName,
                    FormName: name
                },
                success: function (result) {
                    //alert(result);
                    isExist = result;
                }
            });
            return isExist;
        }, initPage: function () {
            this.page = new smat.dynamics.Page({
                projID: this.mainProjID,
                entityName: this.mainEntityName,
                name: this.mainFormName,
                title: this.mainFormDesc,
                contextOn: this.centerPane,
                type: this.mainTemplateType,
                belong: this.config.belong,
                createdBy: this.config.createdBy,
                designer: this,
                designing: true,
                isMobile: this.config.isMobile
            });


            this.page.propertysPanel = this.propertysPanel;

            if (this.config.titleTarget != undefined) {
                this.config.titleTarget.text(this.mainFormName);
            }

            this.page.setEntity(this.data);

            //this.initEntity();
        }, openForm: function () {
            var self = this;
            smat.service.openLoding();
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getForm,
                params: {
                    ProjID: this.mainProjID,
                    EntityName: this.mainEntityName,
                    FormName: this.mainFormName
                },
                success: function (form) {
                    //alert(result);
                    self.page.propertysPanel = self.propertysPanel;
                    self.page.setForm(form);

                    if (self.template.onPageLoad) {
                        self.template.onPageLoad();
                    }
                }
            });
        }, templatePage: function () {
            this.template = new smat.dynamics.template[this.mainTemplateType]({
                page: this.page
            });

            this.template.toolsBuild();
        },
        //initEntity: function () {
        //    var self = this;

        //    //===================entity=======================
        //    smat.service.loadJosnData({
        //        url: smat.dynamics.commonURL.getEntity,
        //        async: false,
        //        params: {
        //            ProjID: this.mainProjID,
        //            EntityName: this.mainEntityName
        //        },
        //        success: function (result) {
        //            //alert(result);

        //            self.page.setEntity(result);

        //            var entityData = {
        //                text: result.EntityDesc
        //            }

        //            var items = new Array();
        //            for (var key in result.FieldList) {
        //                var item = smat.globalObject.clone(result.FieldList[key]);
        //                item.text = item.FieldName;
        //                items.push(item);
        //            }
        //            entityData.items = items;

        //            self.leftPane.find('.entity-tree').asmatTreeView({
        //                dataSource: [entityData]
        //            });

        //            var treeview = self.leftPane.find('.entity-tree').data("asmatTreeView");
        //            treeview.expand(self.leftPane.find('.entity-tree .s-item'));

        //            //Drag
        //            $.each(self.leftPane.find('.entity-tree .s-item'), function (n, value) {
        //                if ($(this).find('ul').length == 0) {
        //                    $(this).asmatDraggable({
        //                        treeview: treeview,
        //                        hint: function (e) {
        //                            return self.fieldHint(this, e);
        //                        },
        //                        dragstart: function (e) {
        //                            $('.edit-skin-box').hide();
        //                        },
        //                        dragend: function (e) {
        //                            $('.edit-skin-box').show();
        //                            self.page.dragModel = undefined;
        //                            self.page.dragTarget = undefined;
        //                        }, drag: function (e) {
        //                            var pageX = e.pageX;
        //                            if (e.pageX == undefined) {
        //                                pageX = e.x.screen;
        //                            }

        //                            if (self.page.dropTarget != undefined) {
        //                                var dx = (pageX - self.page.dropTarget.offset().left);
        //                                self.handleRowDrag(dx, self.page.dropTarget, this.options.hintElement);
        //                            }
        //                        }
        //                    });
        //                }

        //            });

        //        }
        //    });
        //    //================================================
        //},
        initDom: function () {
            var self = this;
            this.saveModel = "0";
            this.sectionDom = $('<section class="panel panel-default" style="height:100%;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.sectionDom.attr('dy-uuid', this.uuid);
            this.sectionDom.attr('id', this.config.target.attr('id'));

            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;padding-top: 45px;overflow: hidden;"></div>').appendTo(this.sectionDom.find('.panel-body'));


            //dom
            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.beforeInitDom) {
                smat.dynamics.analysisConfig.beforeInitDom(this);
            }

            this.topPane = $('<div class="top-pane" style="position: absolute;top:0;left:0;background-color:#fff;width:100%;"></div>').appendTo(this.box);
            this.topRow = $('<div class="row"></div>').appendTo(this.topPane);
            $('<div class="line line-dashed b-b line-lg pull-in"></div>').appendTo(this.topPane);

            this.saveTempBtn = $('<button class="btn-dark" style="margin-left:20px;">一時保存</button>').appendTo(this.topRow);
            this.saveBtn = $('<button class="btn-dark" style="margin-left:20px;">' + smat.service.optionSet("DyOptionText.Save") + '</button>').appendTo(this.topRow);
            this.saveForBtn = $('<button class="btn-dark" style="margin-left:20px;">' + smat.service.optionSet("DyOptionText.SaveAs") + '</button>').appendTo(this.topRow);
            this.previewBtn = $('<button class="btn-dark" style="margin-left:20px;display:none;">' + smat.service.optionSet("DyOptionText.Preview") + '</button>').appendTo(this.topRow);
            this.newBtn = $('<button class="btn-dark" style="margin-left:20px;display:none;">' + smat.service.optionSet("SysText.New") + '</button>').appendTo(this.topRow);
            this.cancelBtn = $('<button class="btn-dark" style="margin-left:20px;">' + smat.service.optionSet("DyOptionText.Close") + '</button>').appendTo(this.topRow);
            this.delBtn = $('<button class="btn-danger" style="margin-left:20px;">削除</button>').appendTo(this.topRow);
            this.verticalBox = $('<div class="vertical-box" style="height: 100%; width: 100%;padding: 0;margin: 0;"></div>').appendTo(this.box);

            this.middlePane = $('<div class="middle-pane"></div>').appendTo(this.verticalBox);
            this.bottomPane = $('<div class="bottom-pane"></div>').appendTo(this.verticalBox);


            this.horizontalBox = $('<div class="horizontal-box" style="height: 100%; width: 100%;padding: 0;margin: 0;"></div>').appendTo(this.middlePane);

            //this.leftPane = $('<div class="left-pane"><ul class="panelbar"><li>Entity<div><div class="entity-tree"></div></div></li><li>Control<div><div id="log"></div></div></li></ul></div>').appendTo(this.horizontalBox);
            this.leftPane = $('<div class="left-pane"><ul class="panelbar"></ul></div>').appendTo(this.horizontalBox);
            this.centerPane = $('<div class="center-pane designer-panel"><div class="designer-panel-skin sm-flat text-right" style="width: 100%;height: 60px;margin-bottom: -16px;background-color: transparent;"><a id="tool_page_active" class="button sm-widget sm-button" data-role="button" href="javascript:void(0)" style="background-color: #FFF;padding: .2em .5em;margin: 6px 15px 0 0;"><span class="sm-text"><i class="fa fa-file"></i></span></a></div></div>').appendTo(this.horizontalBox);
            this.rightPane = $('<div class="right-pane"></div>').appendTo(this.horizontalBox);


            if (smat.dynamics.analysisConfig && smat.dynamics.analysisConfig.afterInitDom) {
                smat.dynamics.analysisConfig.afterInitDom(this);
            }

            this.saveTempBtn.hide();
            this.saveBtn.hide();
            this.saveForBtn.hide();
            this.cancelBtn.hide();
            this.newBtn.hide();
            this.delBtn.hide();

            if (this.config.hideToolBar == true) {
                this.topPane.hide();
                this.box.css("padding-top", "0");
                this.sectionDom.children(".panel-body").css("padding", "0").css("margin", "0");
                this.sectionDom.css("padding", "0").css("margin", "0");

            }

            this.verticalBox.asmatSplitter({
                orientation: "vertical",
                panes: [
                    { collapsible: false },
                    { collapsible: false, resizable: false, size: "30px" }
                ]
            });

            this.horizontalBox.asmatSplitter({
                panes: [
                    { collapsible: true, size: self.config.toolPanelWidth},
                    { collapsible: false },
                    { collapsible: true, size: "260px" }
                ]
            });

            if (this.config.hideToolBar == true) {
                
                this.verticalBox.css("border", "none");

                setTimeout(function () { $(window).resize(); }, 10);
                
            }

            //this.leftPane.find('.panelbar').asmatPanelBar({
            //    expandMode: "multiple"
            //});
            //this.toolBar = this.leftPane.find('.panelbar').data("asmatPanelBar");
            //toolBar.expand(this.leftPane.find('.panelbar .s-item'));

            //==================propertys panel===============
            this.propertysPanel = new smat.dynamics.property.Panel({
                target: this.rightPane,
                designer: this
            });
            //==================propertys panel===============

            this.saveBtn.asmatButton({
                click: function () {
                    self.page.save(function (result) {
                        if (self.template.afterPageSave) {
                            self.template.afterPageSave();
                        }

                        self.cancelBtn.click();
                    });
                    self.saveModel = "1";
                }
            });

            this.saveTempBtn.asmatButton({
                click: function () {
                    self.page.save(function (result) {
                        if (self.template.afterPageSave) {
                            self.template.afterPageSave();
                        }

                        self.mode = "edit";
                        self.delBtn.show();
                        self.saveForBtn.show();
                        self.mainFormDesc = self.page.config.title;
                    });
                    self.saveModel = "1";
                }
            });
            

            this.newBtn.asmatButton({
                click: function () {
                    self.initNewPage(self.mainEntityName, self.mainFormDesc);
                }
            });

            this.saveForBtn.asmatButton({
                click: function () {
                    self.page.saveFor(function (result) {
                        if (self.template.afterPageSave) {
                            self.template.afterPageSave();
                        }
                        self.cancelBtn.click();
                    });
                    self.saveModel = "2";
                }
            });
            

            this.cancelBtn.asmatButton({
                click: function () {
                    smat.service.closeForm({
                        contentId: self.config.target.attr('id'),
                        result: self.saveModel
                    });
                    //window.location.href = smat.dynamics.commonURL.formList + "?type=" + self.mainTemplateType;
                }
            });

            this.delBtn.asmatButton({
                click: function () {

                    self._delForm();

                }
            });

            //if (self.mode == "new") {
            //    self.delBtn.hide();
            //    self.saveForBtn.hide();
            //}

            this.previewBtn.asmatButton({
                click: function () {
                    self.page.preview();
                }
            });
            this.previewBtn.asmatTooltip({
                content: "画面機能を使用できます",
                showOn: "mouseenter",
                position: "top"
            });

            this.centerPane.find("#tool_page_active").bind("click", function (e) {
                if (self.page) self.page.active();
            });

            if (this.afterInitDom) {
                this.afterInitDom();
            }
        }, _delForm: function () {
            debugger;
            var self = this;
            var confirm_config = {
                msg: smat.service.optionSet("SysMsg.Delete", self.mainFormDesc),
                callback: function () {

                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.delForm,
                        params: {
                            ProjID: self.mainProjID,
                            EntityName: self.mainEntityName,
                            FormName: self.mainFormName
                        },
                        success: function (result) {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.DeleteSuccess") });
                            smat.service.closeForm({
                                contentId: self.config.target.attr('id')
                            });
                        }
                    });
                }
            }
            smat.service.confirm(confirm_config);

            //window.location.href = smat.dynamics.commonURL.formList + "?type=" + self.mainTemplateType;
        },
        //fieldHint: function (dragTarget, item) {
        //    //alert(treeview.dataItem($(item)).text);
        //    this.page.dragTarget = dragTarget;
        //    this.page.dragModel = "new";
        //    var dataItem = dragTarget.options.treeview.dataItem($(item));

        //    var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff;'></div>");

        //    var inputElement = $("<input />").appendTo(hintElement);

        //    this.iniFieldHintElement(inputElement, dataItem);

        //    dragTarget.options.dataItem = dataItem;
        //    dragTarget.options.hintElement = hintElement;

        //    return hintElement;
        //},
        //iniFieldHintElement: function (inputElement, dataItem) {
        //    //===================================demo
        //    var dataType = "TextBox";
        //    var tempConfig = {
        //        dataType: dataType,
        //        label: {
        //            text: dataItem.text,
        //            attributes: {
        //                style: "text-align:right; padding-right:5px;"
        //            }
        //        },
        //        inputBox: {
        //            attributes: {
        //                'class': "col-fix-1"
        //            }
        //        }
        //    }

        //    if (smat[tempConfig.dataType] != undefined) {
        //        tempConfig.target = inputElement;

        //        new smat[tempConfig.dataType](tempConfig);
        //    }
        //    //===================================demo
        //    this.page.dragTarget.options.childConfig = {
        //        type: "Field",
        //        dataType: dataType,
        //        name: dataItem.FieldName,
        //        label: dataItem.text,
        //        defaultFieldName: dataItem.FieldName,
        //        inputBoxClass: "col-fix-1"
        //    }

        //}
        //, handleRowDrag: function (dx, dropTarget, hintElement) {

        //    //将hit元素暂时放入drop容器中
        //    if (dropTarget.find(".drag-temp-element").length == 0) {
        //        var tempElement = hintElement.children().clone();
        //        tempElement.addClass("drag-temp-element");
        //        tempElement.attr('style', 'border: 1px dashed #19C6F9;background-color: #fff;margin-top: 0px;opacity:0.7;');
        //        if (dropTarget.find(".row-empty-height").length > 0) {
        //            tempElement.attr('temp-index', 0);
        //        } else {
        //            tempElement.attr('temp-index', dropTarget.children().length);
        //        }

        //        tempElement.appendTo(dropTarget);
        //    }

        //    //==============新规时默认追加在行的末尾：注释以下代码==========
        //    //if (dropTarget.attr("item-width") != undefined) {

        //    //    //计算drag元素在drop容器中所在位置
        //    //    var item_width_str = dropTarget.attr("item-width");

        //    //    var ws = item_width_str.split(",");
        //    //    if (ws.length == 0) {
        //    //        return;
        //    //    }
        //    //    var tempElement = dropTarget.find('.drag-temp-element');
        //    //    var temp_index = Number(tempElement.attr('temp-index'));
        //    //    var index = ws.length;
        //    //    var tempstart = 0;
        //    //    for (var i in ws) {
        //    //        if (dx > tempstart && dx < ws[i]) {
        //    //            index = i;
        //    //            break;
        //    //        }
        //    //        tempstart = ws[i];
        //    //    }

        //    //    //当所在位置发生变化时，调整位置
        //    //    if (index != temp_index) {
        //    //        //abjust index
        //    //        tempElement = tempElement.detach();

        //    //        var beforeElement = dropTarget.children('[col-index="' + index + '"]');
        //    //        if (beforeElement.length > 0) {
        //    //            beforeElement.before(tempElement);
        //    //        } else {
        //    //            tempElement.appendTo(dropTarget);
        //    //        }

        //    //        tempElement.attr('temp-index', index);
        //    //    }
        //    //}
        //    //==============================
        //}
        checkSave: function (type) {
            if (this.template.activateAlltool) {
                this.template.activateAlltool();
            }

            if (this.template.beforeSave) {
                this.template.beforeSave();
            }

            if (this.template.checkSave) {
                return this.template.checkSave(type);
            }
            return true;
        }, initNewPage: function (entityName, formDesc) {

            var self = this;
            this.mode = "new";

            this.mainEntityName = entityName;

            if (formDesc) {
                this.mainFormDesc = formDesc;
            }


            //===================entity=======================
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntity,
                async: false,
                params: {
                    ProjID: this.mainProjID,
                    EntityName: this.mainEntityName
                },
                success: function (result) {
                    //alert(result);

                    self.data = result;
                }
            });

            this.centerPane.children().remove();
            
            this.initPage();

            this.template.config.page = this.page;
            this.template.page = this.page;
            this.template._resetPageObj();

            //
            if (this.template.setData) {
                this.template.setData();
            }

            this.template.templateBuild();

            if (this.template.tabBox) {
                this.template.tabBox.ui().uiControl.select(0);
            }
            
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.Designer, smat.dynamics.Element);

})();