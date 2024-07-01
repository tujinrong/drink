
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdFormList = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.FormList(config);
        });
    };

    smat.dynamics.FormList = function (config) {
        //默认属性
        this.setConfig({
            target: undefined,
            belong: ""

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.FormList.prototype = {
        /**
          * 初期化
          * @name init
          * @methodOf smat.DatePicker.prototype
          */
        init: function () {

            this.pageItem = null;
            this.page = null;
            this.designer = null;

            this.config.projID = 1;
            var self = this;

            this.sectionDom = $('<section id="designer_form_list_section" class="panel panel-default" style="height:100%;overflow: auto;margin-bottom: 0;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;"></div>').appendTo(this.sectionDom.find('.panel-body'));

            this.rowCondition = $('<div class="row form-group" style="position: absolute;z-index: 20;"></div>').appendTo(this.box);
            this.txtFormType = $('<input style="margin-left:20px;"/>').appendTo(this.rowCondition);

            this.mainRow = $('<div class="col-sm-12" style="height:100%;width:100%;position: relative;"></div>').appendTo(this.box);

            this.leftBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;padding-top: 40px;width: 200px;height: 100%;position: absolute;left: 5px;"></div>').appendTo(this.mainRow);
            this.rightBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;padding-top: 40px;height:100%;position: absolute;padding-left: 205px;left: 5px;"></div>').appendTo(this.mainRow);

            this.leftContent = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.leftBox);

            this.toolBarContent = $('<div class="col-sm-12" style="height:43px;padding: 0;background-color: rgb(242, 245, 245);border: 1px solid #ccc;border-bottom: none;z-index: 10;"></div>').appendTo(this.rightBox);

            this.rowtoolBar = $('<div class="row" style="margin: 3px 0 0 0;"><h4 class="form-title" style="line-height: 2em;text-indent: 10px;float:left"></h4></div>').appendTo(this.toolBarContent);
            this.toolBar = $('<div style="float:right;margin-right:20px;"><button id="designer_form_list_search_btn" class="btn-dark s-button" style="margin-left:5px;"><i class="icon-reload"></i>　' + smat.service.optionSet("DyOptionText.Refresh") + '</button><div>').appendTo(this.rowtoolBar)
            //this.rowGrid = $('<div class="row" style="margin: 0;"><div id="designer_form_list_grid"></div></div>');

            this.mainContentBox = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;position: relative;top: -43px;padding-top: 42px;"></div>').appendTo(this.rightBox);
            this.mainContent = $('<div id="designer_form_main_content" class="col-sm-12 s-form-content" style="height:100%;padding: 0;border-top: 1px solid #ccc;position: relative;overflow: auto;"></div>').appendTo(this.mainContentBox);

            //this.grid = this.rowGrid.find('#designer_form_list_grid');

            this.saveBtn = $('<button  class="btn-dark s-button" style="margin-left:5px;"><i class="fa fa-save"></i>　' + smat.service.optionSet("DyOptionText.Save") + '</button>').appendTo(this.toolBar);
            this.designBtn = $('<button  class="btn-dark s-button" style="margin-left:5px;"><i class="fa fa-edit"></i>　' + smat.service.optionSet("DyOptionText.Design") + '</button>').appendTo(this.toolBar);
            this.savForeBtn = $('<button  class="btn-dark s-button" style="margin-left:5px;"><i class="fa fa-save"></i>　' + smat.service.optionSet("DyOptionText.SaveAs") + '</button>').appendTo(this.toolBar);
            this.delBtn = $('<button class="btn-dark s-button" style="margin-left:5px;"><i class="fa fa-trash-o"></i>　' + smat.service.optionSet("DyOptionText.Delete") + '</button>').appendTo(this.toolBar);
            this.closeBtn = $('<button class="btn-dark s-button" style="margin-left:5px;"><i class="fa fa-times"></i>　' + smat.service.optionSet("DyOptionText.Close") + '</button>').appendTo(this.toolBar);

            this.rowLeftToolBar = $('<div class="row" style="margin: 3px 0 0 0;"><div class="col-sm-12 text-left text-center-xs"><button id="designer_form_list_new_btn" class="btn-dark " style="margin-left:5px;"><i class="fa fa-play"></i>　' + smat.service.optionSet("DyOptionText.New") + '</button></div></div>').appendTo(this.leftContent);

            this.treeviewBox = $('<div class="col-sm-12" style="padding: 10px 0;"></div>').appendTo(this.leftContent);

            this.treeviewBox.asmatTreeView({
                dataSource: [],
                select: function (e) {
                    var dataItem = this.dataItem(e.node);

                    self.openFormPage(dataItem);
                }
            });

            this.treeview = this.treeviewBox.data("asmatTreeView");

            //this.grid.smatGrid({
            //    dataSource: [],    //数据源
            //    columns: [
            //        {
            //            field: "FormName",
            //            title: "名前"
            //        },
            //        {
            //            field: "EntityDesc",
            //            title: "分類",
            //            width:"200px"
            //        },
            //        //{
            //        //    field: "FormType",
            //        //    title: "画面タイプ",
            //        //    width: "160px"
            //        //},
            //        {
            //            field: "",
            //            title: "作成者",
            //            width: "120px"
            //        },
            //        {
            //            field: "",
            //            title: "作成日",
            //            width: "120px"
            //        }, {
            //            field: "",
            //            title: "",
            //            width: "240px",
            //            attributes: {
            //                "class": "text-center"
            //            },
            //            actions: [
            //                {
            //                    text: '開く',
            //                    click: function (dataItem) {

            //                        var url = smat.dynamics.commonURL.formPage + "/" + dataItem.ProjID + "/" + dataItem.EntityName + "/" + dataItem.FormName;
            //                        smat.service.openForm({
            //                            url: url,
            //                            fillTarget: "main_form_content"
            //                        });
            //                    }
            //                },
            //                {
            //                    text: '設計',
            //                    click: function (dataItem) {
            //                        window.location.href = smat.dynamics.commonURL.formEdit + "?ProjID=" + dataItem.ProjID + "&EntityName=" + dataItem.EntityName + "&FormName=" + dataItem.FormName + "&type=" + dataItem.FormType;
            //                    }
            //                }
            //            ]

            //        }
            //    ],           //列信息
            //    dataBound: function (e) {
            //        var trs = e.sender.tbody.children('tr');
            //        $.each(trs, function (n, value) {
            //            var dataItem = e.sender.dataItem($(this));

            //            if (dataItem["FormType"] == "SimpleYList" || dataItem["FormType"] == "SimpleYEdit") {
            //                $(this).children().eq(4).find('button:eq(1)').remove();
            //            }
            //        });
            //    }
            //});

            $('#designer_form_list_new_btn').smatButton({
                click: function (e) {

                    self.getNewFormName(function (result) {
                        if (result) {
                            self.openFormDesingner({
                                ProjID: self.config.projID,
                                FormName: result.formName,
                                FormDesc: result.formDesc,
                                EntityName: result.entityName,
                                Belong: self.config.belong,
                                CreatedBy: smat.dynamics.role.type,
                                mode: "new"
                            });
                        }

                    });
                    //window.location.href = smat.dynamics.commonURL.formEdit + "?ProjID=" + self.config.projID + "&type=" + self.txtFormType.ui().value();
                }
            });

            var t1 = {
                "SimpleGraph": 1,
                "SimpleSearch": 1,
                "SimpleStatistics": 1,
                "SimpleCrossStatistic": 1
            }

            var t2 = {
                "SimpleYList": 1,
                //"SimpleYEdit": 1,
                "SimpleMasterList": 1,
                "SimpleMasterEdit": 1
            }

            var t3 = {
                "noEdit": 1
            }

            var dataSource = [
                    {
                        text: "<i class='fa fa-search'></i>　" + "BusinessSearch",
                        value: "BusinessSearch"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　" +smat.service.optionSet("DyOptionTemplate.SimpleSearch"),
                        value: "SimpleSearch"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleStatistics"),
                        value: "SimpleStatistics"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleCrossStatistic"),
                        value: "SimpleCrossStatistic"
                    },
                    {
                        text: "<i class='fa fa-bar-chart-o'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleGraph"),
                        value: "SimpleGraph"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleYList"),
                        value: "SimpleYList"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleYEdit"),
                        value: "SimpleYEdit"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleMasterList"),
                        value: "SimpleMasterList"
                    },
                    {
                        text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleMasterEdit"),
                        value: "SimpleMasterEdit"
                    }
            ];

            if (t1[this.config.type] == 1 || this.config.type == "") {
                dataSource =[
                {
                    text: "<i class='fa fa-search'></i>　" + "BusinessSearch",
                    value: "BusinessSearch"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleSearch"),
                    value: "SimpleSearch"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleStatistics"),
                    value: "SimpleStatistics"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleCrossStatistic"),
                    value: "SimpleCrossStatistic"
                },
                {
                    text: "<i class='fa fa-bar-chart-o'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleGraph"),
                    value: "SimpleGraph"
                }
                ]
            } else if (t2[this.config.type] == 1) {
                dataSource = [
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleMasterList"),
                    value: "SimpleMasterList"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleMasterEdit"),
                    value: "SimpleMasterEdit"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleLogicList"),
                    value: "SimpleLogicList"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleSysForm"),
                    value: "SystemMaster"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.ReferForm"),
                    value: "Refer"
                }
                ];
            } else if (t3[this.config.type] == 1) {
                dataSource = [
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleStatistics"),
                    value: "SimpleStatistics"
                },
                {
                    text: "<i class='fa fa-search'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleCrossStatistic"),
                    value: "SimpleCrossStatistic"
                },
                {
                    text: "<i class='fa fa-bar-chart-o'></i>　" + smat.service.optionSet("DyOptionTemplate.SimpleGraph"),
                    value: "SimpleGraph"
                }
                ]
            }

            this.btn_search = $("#designer_form_list_search_btn");
            this.txtFormType.smatButtonGroup({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: dataSource, change: function (e) {
                    self.btn_search.click(); self.clearCurrent();
                    //setTimeout(function () { self.btn_search.click(); self.clearCurrent(); }, 10);
                }
            });

            this.rowtoolBar.smatForm({
                actions: [
                    {
                        action: smat.dynamics.commonURL.getFormList,
                        actionBtn: "designer_form_list_search_btn",
                        getParam: function (param) {
                            param.projID = self.config.projID;
                            param.type = self.txtFormType.ui().value();
                        },
                        success: function (result) {
                            self.setFormDatas(result);
                        }
                    }
                ]
            });

            if (this.config.type != "") {
                //$("#formType_" + this.config.type).prop("checked", "checked");
                self.txtFormType.ui().value(this.config.type)
            }

            this.initBtnEvent();

            this.btn_search.click();

            this.setBtnStatus();

            if (this.config.readOnly == true) {
                $('#designer_form_list_new_btn').hide();
            }

        }, setFormDatas: function (datas) {

            var ds = new Array();

            for (var i = 0; i < datas.length; i++) {
                var item = smat.globalObject.clone(datas[i]);
                item.text = item.FormDesc;
                if (item.FormDesc == null || item.FormDesc == "") item.text = item.FormName;
                item.imageUrl = "/SMAT.UI/images/space.png"

                //Y:
                //if (item.EntityName.indexOf("Y_") == 0) {
                //    continue;
                //}

                //belong:
                //if (item.Belong != null && item.Belong != "" && item.Belong != this.config.belong) {
                //    continue;
                //}

                var entityItem = smat.service.getItemByKey(ds, "EntityName", item.EntityName);
                if (entityItem == undefined) {
                    entityItem = {
                        text: item.EntityDesc,
                        EntityName: item.EntityName,
                        items: new Array()
                    }
                    entityItem.imageUrl = "/SMAT.UI/images/folder.png"
                    ds.push(entityItem);
                }
                entityItem.items.push(item);
            }

            this.treeview.setDataSource(ds);
            this.treeview.expand(this.treeviewBox.find('.s-item'));

            if (this.pageItem != null) {
                var bar = this.treeview.findByText(this.pageItem.FormDesc);
                this.treeview.select(bar);

            }

        }, openFormPage: function (dataItem) {
            var projID = dataItem.ProjID;
            var formName = dataItem.FormName;
            var formDesc = dataItem.FormDesc;
            if (dataItem.FormDesc == null || dataItem.FormDesc == "") formDesc = dataItem.FormName;
            var entityName = dataItem.EntityName;


            //if (this.config.readOnly == true) {
            //    this.rowtoolBar.find('.form-title').text(formDesc);
            //} else {
            //    $('.nav-title').text(formDesc);
            //}

            this.rowtoolBar.find('.form-title').text(formDesc);

            this.closeContent();
            if (formName == undefined) {

            } else {
                var formBox = $("<div style='width:100%;height:100%'></div>");
                smat.service.openForm({
                    contentDom: formBox,
                    fillTarget: "designer_form_main_content"
                });


                this.pageItem = dataItem;
                this.page = smat.dynamics.openPage({
                    projID: projID,
                    formName: formName,
                    entityName: entityName,
                    contextOn: formBox
                });

                this.designer = null;
            }
            this.setBtnStatus();
        },
        getNewFormName: function (handle) {
            var self = this;
            var ebox = $('<section id="' + this.uuid + '_newName" class="panel panel-default " style="margin: 0;padding: 10px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">種別</label><input id="_NEW_Form_Entity" class="input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" id="newNameInput_row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">' + 'CD' + '</label><input id="_FilterName_NEW" class="s-textbox input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Name") + '</label><input id="_FormDesc_NEW" class="s-textbox input-s" style="width:300px;" ></div></div>').appendTo(ebox);

            if (smat.dynamics.isUser()) {
                $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">区分</label><input id="_NEW_Form_Belong" class="input-s" ></div></div>').appendTo(ebox);
            }

            $('<div class="row" style="margin:8px 0;"><div class=" form-group text-center" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(ebox);
            var newNameInput = ebox.find("#_FilterName_NEW");
            var newDescInput = ebox.find("#_FormDesc_NEW");

            var newBelongInput = ebox.find("#_NEW_Form_Belong");


            var newEntityInput = ebox.find("#_NEW_Form_Entity");

            //newNameInput.val(smat.service.optionSet("DyOptionText.New"));
            if (smat.dynamics.isUser()) {
                newNameInput.val(smat.service.uuid());
                ebox.find("#newNameInput_row").hide();

                newBelongInput.smatDropDownList({
                    dataSource: [
                        {
                            text: "共通",
                            value: ""
                        },
                        {
                            text: "店舗",
                            value: "KANRI-9"
                        }
                    ],
                    dataValueField: "value",
                    dataTextField: "text",
                });
            }

            newEntityInput.smatDropDownList({
                dataSource: [],
                dataValueField: "EntityName",
                dataTextField: "EntityDesc",
                change: function (e) {
                    var text = newEntityInput.ui().uiControl.text();
                    //newNameInput.val(text + self.rowCondition.find(".sm-state-active").text().trim() + smat.service.optionSet("DyOptionText.New"));

                    var descStr = text + self.rowCondition.find(".sm-state-active").text().trim();
                    if (smat.dynamics.isUser() == false) {
                        newNameInput.val(descStr);
                    }
                    newDescInput.val(descStr);

                }
            });

            var newNameBtn = ebox.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if (name == "") {
                        alert(smat.service.optionSet("SysMsg.Required", 'CD'));
                        newNameInput.focus();
                        return;
                    }

                    var desc = newDescInput.val();
                    if (desc == "") {
                        alert(smat.service.optionSet("SysMsg.Required", smat.service.optionSet("DyOptionText.Name")));
                        newDescInput.focus();
                        return;
                    }

                    var isExist = false;
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.checkFormExist,
                        async: false,
                        params: {
                            ProjID: self.config.projID,
                            EntityName: newEntityInput.ui().value(),
                            FormName: name,
                            FormDesc: desc,
                            Belong: self.config.belong
                        },
                        success: function (result) {
                            //alert(result);
                            isExist = result;
                        }
                    });
                    if (isExist == true) {
                        if (smat.dynamics.isUser()) {
                            alert(smat.service.optionSet("SysMsg.Exit", "" + smat.service.optionSet("DyOptionText.Name")));
                            newDescInput.focus();
                        } else {
                            alert(smat.service.optionSet("SysMsg.Exit", "CD/" + smat.service.optionSet("DyOptionText.Name")));
                            newNameInput.focus();
                        }


                        return;
                    }

                    smat.service.closeForm({
                        contentId: self.uuid + '_newName',
                        result: {
                            formName: name,
                            formDesc: desc,
                            entityName: newEntityInput.ui().value()
                        }
                    });
                }
            })


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: ebox,
                width: "460px",
                top: "20%",
                title: smat.service.optionSet("DyOptionText.New"),
                afterClose: function (result) {

                    handle(result);
                }
            });

            //===================entity=======================
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntityList,
                async: false,
                params: {
                    ProjID: this.config.projID
                },
                success: function (result) {
                    //alert(result);
                    var d = new Array();
                    for (var k in result) {
                        if (result[k].EntityName != "M_Code") {
                            d.push(result[k]);
                        }
                    }
                    var datas = null;
                    if (smat.dynamics.isUser()) {
                        datas = $.Enumerable.From(result).Where("$.EntityType == '1' ").ToArray();
                    } else {
                        datas = d;
                       // datas = $.Enumerable.From(result).Where("$.EntityName.indexOf('Y_') < 0").ToArray();
                    }

                    newEntityInput.ui().uiControl.setDataSource(datas);
                    var text = newEntityInput.ui().uiControl.text();
                    if (smat.dynamics.isUser() == false) {
                        newNameInput.val(text + self.rowCondition.find(".sm-state-active").text().trim());
                    }
                    newDescInput.val(text + self.rowCondition.find(".sm-state-active").text().trim());

                }
            });
        }, openFormDesingner: function (dataItem) {
            var projID = this.config.projID;
            var formName = dataItem.FormName;
            var formDesc = dataItem.FormDesc;
            var entityName = dataItem.EntityName;

            var mode = dataItem.mode;

            this.closeContent();

            if (mode == "new") {
                this.pageItem = dataItem;
            }

            var designerBox = $("<div id='form_designer'></div>");
            var self = this;
            smat.service.openForm({
                contentDom: designerBox,
                //fillTarget: "designer_form_main_content,"
                width: "96%",
                height: "85%",
                top: "5%",
                afterClose: function (result) {

                    if (self.pageItem.FormName != self.designer.page.config.name || self.designer.mode == "new") {

                        self.pageItem.FormName = self.designer.page.config.name;
                        self.refreshTree();
                        self.setBtnStatus();
                    }

                    self.pageItem.mode = "edit";
                    self.openFormPage(self.pageItem);
                }
            });
            var templateType = this.txtFormType.ui().value();

            this.designer = designerBox.smatDynamicsdDesigner({
                projID: projID,
                formName: formName,
                formDesc: formDesc,
                //hideToolBar:true,
                entityName: entityName,
                //titleTarget: designerBox,
                belong: self.config.belong,
                createdBy: smat.dynamics.role.type,
                mode: mode,
                templateType: templateType
            });
            this.setBtnStatus();
        }, closeContent: function () {
            var pageNode = this.mainContent.find('.s-dy-page');
            if (pageNode.length > 0) {
                smat.service.closeForm({
                    contentId: pageNode.attr("id")
                });

                //this.pageItem = null;
                //this.page = null;
                //this.designer = null;
            }
        }, clearCurrent: function () {
            this.closeContent();
            this.pageItem = null;
            this.page = null;
            this.designer = null;
            this.setBtnStatus();
        }, setBtnStatus: function () {
            this.btn_search.hide();
            this.saveBtn.hide();
            this.designBtn.hide();
            this.savForeBtn.hide();
            this.delBtn.hide();
            this.closeBtn.hide();

            if (this.config.readOnly == true) {
                return;
            }

            this.rowLeftToolBar.show();

            if (this.txtFormType.ui().value() == "SimpleYList") {
                this.rowLeftToolBar.hide();
                return;
            }

            if (this.pageItem != null) {


                //if (this.designer == null) {
                //    this.designBtn.show();
                //    this.delBtn.show();
                //} else {
                //    this.saveBtn.show();
                //    this.savForeBtn.show();
                //    this.delBtn.show();
                //    this.closeBtn.show();

                //    if (this.designer.mode == "new") {
                //        this.delBtn.hide();
                //    }

                //    if (this.designer.page.previewing == true) {
                //        this.designBtn.show();
                //        this.closeBtn.hide();
                //    } else {
                //        this.designBtn.hide();
                //        this.closeBtn.show();
                //    }
                //}

                this.designBtn.show();
                //this.savForeBtn.show();
                this.delBtn.show();
            }
        }, refreshTree: function () {
            this.btn_search.click();
        }, initBtnEvent: function () {
            var self = this;

            this.designBtn.bind("click", function (e) {
                if (self.pageItem != null) {
                    if (self.designer == null) {
                        self.openFormDesingner(self.pageItem);
                    } else {
                        self.designer.page.closePreview();
                    }

                }
                self.setBtnStatus();
            });

            this.closeBtn.bind("click", function (e) {
                if (self.pageItem != null) {
                    if (self.designer == null) {

                    } else {
                        self.designer.page.preview();
                    }

                }
                self.setBtnStatus();
            });

            this.saveBtn.bind("click", function (e) {
                if (self.pageItem != null) {
                    if (self.designer == null) {

                    } else {
                        self.designer.page.save(function (result) {
                            if (self.designer.mode == "new") {
                                self.refreshTree();
                            }
                            self.designer.mode = "edit";
                            self.setBtnStatus();
                        });
                    }
                }

            });

            this.savForeBtn.bind("click", function (e) {
                if (self.pageItem != null) {
                    if (self.designer == null) {

                    } else {
                        self.designer.page.saveFor(function (result) {
                            self.pageItem.FormName = self.designer.page.config.name;
                            self.refreshTree();
                            self.setBtnStatus();
                        });
                    }
                }

            });

            this.delBtn.bind("click", function (e) {
                if (self.pageItem != null) {
                    var confirm_config = {
                        msg: smat.service.optionSet("SysMsg.Delete", self.pageItem.FormDesc),
                        callback: function () {
                            smat.service.loadJosnData({
                                url: smat.dynamics.commonURL.delForm,
                                params: {
                                    ProjID: self.pageItem.ProjID,
                                    EntityName: self.pageItem.EntityName,
                                    FormName: self.pageItem.FormName
                                },
                                success: function (result) {
                                    smat.service.notice({ msg: smat.service.optionSet("SysMsg.DeleteSuccess") });
                                    self.refreshTree();
                                    self.clearCurrent();
                                }
                            });
                        }
                    }
                    smat.service.confirm(confirm_config);
                }

            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.FormList, smat.dynamics.Element);

})();