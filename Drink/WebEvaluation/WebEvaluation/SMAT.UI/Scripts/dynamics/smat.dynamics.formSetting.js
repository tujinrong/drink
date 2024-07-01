
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  formSetting
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdFormSetting = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.FormSetting(config);
        });
    };

    smat.dynamics.FormSetting = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.FormSetting.prototype = {
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

            //formFilter
            this.formFilter = this.config.formFilter;

            this.sectionDom = $('<section id="designer_form_list_section" class="panel panel-default" style="height:100%;overflow: auto;margin-bottom: 0;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;"></div>').appendTo(this.sectionDom.find('.panel-body'));

            this.mainRow = $('<div class="col-sm-12" style="height:100%;width:100%;position: relative;"></div>').appendTo(this.box);

            this.leftBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 200px;height: 100%;position: absolute;left: 5px;"></div>').appendTo(this.mainRow);
            this.rightBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;height:100%;position: absolute;padding-left: 205px;left: 5px;"></div>').appendTo(this.mainRow);
            
            this.leftContent = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.leftBox);

            this.toolBarContent = $('<div class="col-sm-12" style="height:33px;padding: 0;background-color: rgb(242, 245, 245);border: 1px solid #ccc;border-bottom: none;z-index: 10;"></div>').appendTo(this.rightBox);

            this.rowtoolBar = $('<div class="row" style="margin: 3px 0 0 0;"><h4 class="form-title" style="line-height: 26px;text-indent: 10px;"></h4><button id="designer_form_list_search_btn" class="btn-dark s-button" style="margin-left:5px; display:none;"><i class="icon-reload"></i>　' + smat.service.optionSet("DyOptionText.Refresh") + '</button></div>').appendTo(this.toolBarContent);
            
            //this.rowGrid = $('<div class="row" style="margin: 0;"><div id="designer_form_list_grid"></div></div>');

            this.mainContentBox = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;position: relative;top: -33px;padding-top: 32px;"></div>').appendTo(this.rightBox);
            this.mainContent = $('<div id="designer_form_main_content" class="col-sm-12 s-form-content" style="height:100%;padding: 0;border-top: 1px solid #ccc;position: relative;overflow: auto;"></div>').appendTo(this.mainContentBox);

            //main content dom build
            this.mainToolBarRow = $('<div class="row" style="margin-right:0;margin-left:0;"></div>').appendTo(this.mainContent);
            this.mainContentToolBar = $('<div class="tool-bar"></div>').appendTo(this.mainToolBarRow);

            $('<div class="line line-dashed b-b line-lg pull-in "></div>').appendTo(this.mainContentToolBar);
            $('<div class="row"><div class="col-sm-12 text-left text-center-xs"><button class="btn-warning s-button btn-save" style="" >保存</button></div></div>').appendTo(this.mainContentToolBar);
            $('<div class="line line-dashed b-b line-lg pull-in "></div>').appendTo(this.mainContentToolBar);

            this.mainGridRow = $('<div class="row"style="margin:20px;margin-bottom:0;"></div>').appendTo(this.mainContent);
            this.mainGridBox = $('<div ></div>').appendTo(this.mainGridRow);

            var uiConfig = {
                target: this.mainGridBox,
                columns: [
                    {
                        title: "ControlName",
                        field: "ControlName",
                        width: "200px"
                    },
                    {
                        title: "Lable",
                        field: "label",
                        template:function(dataItem){
                            var reg = /^codeKind:(\w+)\.(\w+)$/i;
                            if (reg.test(dataItem.label)) {
                                var regArr = reg.exec(dataItem.label);
                                var codeKind = regArr[1];
                                var cd = regArr[2];

                                return smat.service.optionSet(codeKind + "." + cd) + '<button class="btn-primary s-button" style="float: right;">edit</button>';
                            } else {
                                return "国际化Key未设定！！";
                            }
                        },
                        width: "200px"
                    },
                    {
                        title: "Visible",
                        width: "80px",
                        dataType: "dropDownList",
                        editable: "true",
                        valueField: "visible",
                        editorDataSource: visbleDropDown,
                        editorTemplate: visbleEditorTemplate,
                        editorValueTemplate: visbleEditorTemplate,
                        field: "visible"
                    },
                    {
                        title: "Required",
                        width: "80px",
                        dataType: "dropDownList",
                        editable: "true",
                        valueField: "required",
                        editorDataSource: requiredDropDown,
                        editorTemplate: requiredEditorTemplate,
                        editorValueTemplate: requiredEditorTemplate,
                        field: "required"
                    },
                    {
                        title: "　"
                    }
                ],
                dataSource: [],
                dataBound: function (e) {
                    var btns = e.sender.tbody.find('button');
                    
                    btns.bind('click', function (e) {
                        var dataItem = self.mainGrid.uiControl.dataItem($(this).closest('tr'));
                        self.changeOptionSet(dataItem.label);
                    });
                }
            }

            function visbleDropDown(dataItem) {

                var data = [
                    {
                        text: "True",
                        value: "true"
                    },
                    {
                        text: "False",
                        value: "false"
                    }
                ];

                return data;
            }
            function visbleEditorTemplate(dataItem) {
                if (dataItem.value == "true" || dataItem.value == "") {
                    return '<i class="fa fa-check-circle" style="  color: rgb(59, 171, 59);font-size: 20px;margin-left: -8px;margin-top: 3px;"></i>';
                }
                return ' ';
            }
            

            function requiredDropDown(dataItem) {

                var data = [
                    {
                        text: "  ",
                        value: ""
                    },
                    {
                        text: "True",
                        value: "true"
                    }
                ];

                return data;
            }

            function requiredEditorTemplate(dataItem) {
                if (dataItem.value == "true") {
                    return '<i class="fa fa-check-circle" style="  color: rgb(59, 171, 59);font-size: 20px;margin-left: -8px;margin-top: 3px;"></i>';
                }
                return ' ';
            }

            this.mainGrid = new smat.Grid(uiConfig);

            this.rowLeftToolBar = $('<div class="row" style="margin: 3px 0 0 0;"><div class="col-sm-12 text-left text-center-xs"></div></div>').appendTo(this.leftContent);
            
            this.treeviewBox = $('<div class="col-sm-12" style="padding: 10px 0;"></div>').appendTo(this.leftContent);

            this.treeviewBox.asmatTreeView({
                dataSource: [],
                select: function (e) {
                    var dataItem = this.dataItem(e.node);
                    
                    self.openFormPage(dataItem);
                }
            });

            this.treeview = this.treeviewBox.data("asmatTreeView");


            this.btn_search = $("#designer_form_list_search_btn");
            this.btn_save = this.mainContentToolBar.find("button.btn-save");
            this.btn_save.bind('click', function () {
                var datas = self.mainGrid.getDataSource();

                //data abjust
                for (var key in datas) {
                    var item = datas[key];
                    var options = smat.service.strToJson(item.ControlOptions);

                    options.visible = item.visible;
                    options.required = item.required;

                    item.ControlOptions = JSON.stringify(options)
                }

                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.saveControls,
                    params: datas,
                    success: function (result) {
                        smat.service.notice({ msg: smat.service.optionSet("SysMsg.ProcessingCompleted") });
                    }
                });
            });
            
            this.rowtoolBar.smatForm({
                actions: [
                    {
                        action: smat.dynamics.commonURL.getFormList,
                        actionBtn: "designer_form_list_search_btn",
                        getParam: function (param) {
                            param.projID = self.config.projID;
                        },
                        success: function (result) {



                            self.setFormDatas(result);
                        }
                    }
                ]
            });

            if (this.config.type != "") {

            }

            this.initBtnEvent();

            this.btn_search.click();

            this.setBtnStatus();

        }, setFormDatas: function (datas) {
            
            var ds = new Array();
            
            for (var i = 0; i< datas.length;i++) {
                var item = smat.globalObject.clone(datas[i]);
                item.text = item.FormName;
                item.imageUrl = "/SMAT.UI/images/space.png"

                if (this.formFilter) {
                    if (this.formFilter(item) == false) {
                        continue;
                    }
                }

                var entityItem = smat.service.getItemByKey(ds, "EntityName", item.EntityName);
                if (entityItem == undefined) {
                    entityItem = {
                        text: item.EntityDesc,
                        EntityName: item.EntityName,
                        items:new Array()
                    }
                    entityItem.imageUrl = "/SMAT.UI/images/folder.png"
                    ds.push(entityItem);
                }
                entityItem.items.push(item);
            }

            this.treeview.setDataSource(ds);
            this.treeview.expand(this.treeviewBox.find('.s-item'));

            if (this.pageItem != null) {
                var bar = this.treeview.findByText(this.pageItem.FormName);
                this.treeview.select(bar);

            }

        }, openFormPage: function (dataItem) {
            this.dataItem = dataItem;
            this.refresh();
        }, refresh: function () {
            var self = this;
            var projID = this.dataItem.ProjID;
            var formName = this.dataItem.FormName;
            var entityName = this.dataItem.EntityName;

            this.rowtoolBar.find('.form-title').text(formName);

            this.closeContent();
            if (formName == undefined) {

            } else {

                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.getFormControls,
                    params: {
                        ProjID: projID,
                        EntityName: entityName,
                        FormName: formName
                    },
                    success: function (controls) {
                        self.mainGrid.setDataSource(self._getControls(controls));
                    }
                });
            }
            this.setBtnStatus();
        }, closeContent: function () {
            //var pageNode = this.mainContent.find('.s-dy-page');
            //if (pageNode.length > 0) {
            //    smat.service.closeForm({
            //        contentId: pageNode.attr("id")
            //    });
            //}
        }, clearCurrent: function () {
            this.closeContent();
            this.pageItem = null;
            this.page = null;
            this.designer = null;
            this.setBtnStatus();
        }, setBtnStatus: function () {
            
        }, refreshTree: function () {
            this.btn_search.click();
        }, initBtnEvent: function () {
            var self = this;

        }, _getControls: function (controls) {
            var datas = new Array();
            for (var key in controls) {
                var c = controls[key];

                if (c["ControlType"] != "Field") {
                    continue;
                }

                if (this.controlFilter) {
                    if (this.controlFilter(c) == false) {
                        continue;
                    } 
                }
                var options = smat.service.strToJson(c.ControlOptions);
                c["label"] = options["label"];
                c["visible"] = options["visible"];
                c["required"] = options["required"];

                datas.push(controls[key]);
            }
            return datas;
        }, changeOptionSet: function (key) {
            var reg = /^codeKind:(\w+)\.(\w+)$/i;
            if (!reg.test(key)) {
                return;
            }
            var self = this;
            var regArr = reg.exec(key);
            var codeKind = regArr[1];
            var cd = regArr[2];

            //getViewItems
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getOptionSetAllLang,
                params: {
                    ProjID: 1,
                    CodeKind: codeKind,
                    CD: cd
                },
                success: function (result) {

                    var uid = smat.service.uuid();
                    var ebox = $('<section id="' + this.uid + '" class="panel panel-default " style="margin: 0;padding: 10px;height: 400px;"></section>');

                    smat.service.openForm({
                        //m_opacity: 0,
                        contentDom: ebox,
                        width: "410px",
                        top: "10%",
                        title: smat.service.optionSet("DyOptionText.Name")
                    });

                    $('<div class="row" style="margin:8px 0;"><div class="item_grid" ></div></div>').appendTo(ebox);
                    $('<div class="row text-right" style="margin:8px 0;"><button id="okBtn" class="btn-info " style="margin-left:10px;">save</button></div></div>').appendTo(ebox);

                    var itemGridNode = ebox.find(".item_grid");

                    var uiConfig = {
                        target: itemGridNode,
                        columns: [
                            {
                                title: "Lang",
                                field: "Culture",
                                codeKind:"Culture",
                                width: "80px"
                            },
                            {
                                title: "Name",
                                field: "Name",
                                editable: true
                            }
                        ],
                        dataSource:result
                    }

                    var itemGrid = new smat.Grid(uiConfig);


                    var okBtn = ebox.find("#okBtn");
                    okBtn.smatButton({
                        click: function () {
                            smat.service.loadJosnData({
                                url: smat.dynamics.commonURL.saveOptions,
                                params: itemGrid.getDataSource(),
                                success: function (result) {
                                    smat.global.codeMstMap[codeKind] = undefined;
                                    smat.global.codeMst[codeKind] = undefined;
                                    smat.service.closeForm({
                                        contentId: uid
                                    });
                                    self.refresh();
                                }
                            });
                        }
                    })

                }

            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.FormSetting, smat.dynamics.Element);

})();