
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.UserControl
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.UserControl = function (config) {
        //默认属性
        this.setConfig({
            page: undefined,
            conditionOnly: false,
            userControlEntity:undefined,
            category: 'SearchCondition',
            isTemplate:true
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
    smat.dynamics.tool.UserControl.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

            this.userControls = new Array();

            if (!this.config.userControlEntity) {
                this.config.userControlEntity = self.config.page.config.entityName
            }

            this.addBtnBox = $("<div style='text-align:right;background-color: #fff;'><button class='btn-dark s-button' style ='margin: 5px;  padding: 5px 10px;'>+ " + smat.service.optionSet("SysText.New") + "</button></div>").appendTo(this.config.box);
            this.addBtn = this.addBtnBox.find("button");

            this.addBtn.bind("click", function () {
                
                self.getNewName(function (result) {
                    if (result) {
                        var projID = self.config.page.config.projID;
                        var entityName = self.config.userControlEntity;

                        var mode = "new";

                        var designerFormBox = $("<div style='height:600px;'></div>");
                        var designerBox = $("<div id='userControl_designer'></div>").appendTo(designerFormBox);

                        smat.service.openForm({
                            contentDom: designerFormBox,
                            width: "1050px",
                            afterClose: function () {
                                self.setData();
                            }
                        });
                        var templateType = "UserControl";

                        self.designer = designerBox.smatDynamicsdUserControlDesigner({
                            projID: projID,
                            userControlName: result.name,
                            entityName: entityName,
                            titleTarget: designerBox,
                            mode: mode,
                            mainPage:self.config.page,
                            templateType: templateType,
                            category: self.config.category
                        });
                    }
                });
            });

            this.treebox = $("<div/>").appendTo(this.config.box);

            if (this.config.conditionOnly) {
                this.addBtn.hide();
            }
            
            this.setData();
        }, setData: function () {

            var self = this;

            if (this.treebox.data("asmatTreeView")) {
                this.treebox.data("asmatTreeView").destroy();
                this.treebox.remove();
                this.treebox = $("<div/>").appendTo(this.config.box);
            }

            if (!this.config.page.entity) return;

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getUserControlList,
                params: {
                    ProjID: this.config.page.entity.ProjID,
                    EntityName: this.config.userControlEntity||this.config.page.entity.EntityName,
                    Category: this.config.category
                },
                success: function (result) {
                    self.userControls = result;
                    var items = new Array();
                    for (var key in result) {
                        var item = smat.globalObject.clone(result[key]);
                        item.text = item.UserControlName;
                        items.push(item);
                    }

                    self.treebox.asmatTreeView({
                        dataSource: items,
                        template: function (data) {
                            if (self.config.conditionOnly) {
                                return data.item.UserControlName;
                            } else {
                                return data.item.UserControlName + "　<span class='s-icon s-i-pencil' style ='height: 18px; margin-left: 5px;'>Edit</span>";
                            }
                        }
                    });

                    self.treeview = self.treebox.data("asmatTreeView");
                    self.treeview.expand(self.treebox.find('.s-item'));

                    var treeItems = self.treebox.find('.s-i-pencil');

                    $.each(treeItems, function (n, value) {

                        $(this).bind("click", function () { 
                            var data = self.treeview.dataItem($(this).closest('.s-item'));
                            
                            var projID = self.config.page.config.projID;
                            var entityName = self.config.userControlEntity;

                            var designerFormBox = $("<div style='height:600px;'></div>");
                            var designerBox = $("<div id='userControl_designer'></div>").appendTo(designerFormBox);

                            smat.service.openForm({
                                contentDom: designerFormBox,
                                width: "1000px",
                                afterClose: function () { 
                                    self.setData();
                                }
                            });
                            var templateType = "UserControl";

                            self.designer = designerBox.smatDynamicsdUserControlDesigner({
                                projID: projID,
                                userControlName: data.UserControlName,
                                entityName: entityName,
                                titleTarget: designerBox,
                                mainPage: self.config.page,
                                templateType: templateType,
                                category: data.UserControlCategory
                            });
                        });
                    });

                    self.initDragItem(self.treebox.find(".s-item:not([aria-expanded])"));

                }
            });
        }, dragHint: function (dragTarget, item) {

            var hintElement = $("<div id='hint' style='border: 1px dashed #19C6F9;background-color: #fff; '></div>");

            var inputElement = $("<div />").appendTo(hintElement);

            var dataItem = dragTarget.options.dataItem;

            var controlType = dataItem.controlType;
            var dataType = "TextBox";
            var tempConfig = {};

            
            var uc = new smat.dynamics.PageUserControl({
                projID: dataItem.ProjID,
                entityName: dataItem.EntityName,
                name: dataItem.UserControlName,
                contextOn: inputElement
            });

            //uc.setForm(dataItem);
            uc.settingData = true;
            uc.setFormData(dataItem.Controls[0].Controls, function (cConfig) {
                cConfig.visible = true;
            });

            return hintElement;
        },
        dragDataItem: function (dragTarget, item) {

            //var dataItem = dragTarget.options.treeview.dataItem($(item));
            var dataItem = this.treeview.dataItem($(item));
            dataItem = smat.service.getItemByKey(this.userControls, "UserControlName", dataItem.UserControlName);

            return dataItem;
        },
        dragChildConfig: function (dragTarget, item) {

            var data = dragTarget.options.dataItem.Controls[0].Controls;

            if (this.config.isTemplate == true) {
                
                if (smat.dynamics.getDragChildConfig) {
                    data = smat.dynamics.getDragChildConfig(this, dragTarget, data);
                } else {
                    for (var key in data) {
                        if (this.config.unique == true) {
                            data[key].unique = true;
                        }

                        if (data[key].ControlType == "Div" || data[key].ControlType == "Form") {
                            data[key].isUserControl = true;
                        }
                    }

                }

            } else {
                data = {
                    entity: this.config.userControlEntity,
                    userControlName: dragTarget.options.dataItem.UserControlName,
                    type: "UserControl",
                    name: dragTarget.options.dataItem.UserControlName
                }
                
            }
            
            return data;


        },
        dragType: function (dragTarget, item) {
            return "Control";
        },
        getNewName: function (handle) {
            var self = this;
            var ebox = $('<section id="' + this.uuid + '_newName" class="panel panel-default " style="margin: 0;padding: 10px;height: 120px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-sm text-right" style="margin-right:5px;">名称</label><input id="_FilterName_NEW" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info " style="margin-left:10px;">ok</button></div></div>').appendTo(ebox);
            var newNameInput = ebox.find("#_FilterName_NEW");

            var newNameBtn = ebox.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    var name = newNameInput.val();
                    if (name == "") {
                        alert("【名称】を入力してください。");
                        newNameInput.focus();
                        return;
                    }

                    var isExist = false;
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.checkUserControlExist,
                        async: false,
                        params: {
                            ProjID: self.config.page.entity.ProjID,
                            EntityName: self.config.userControlEntity,
                            UserControlName: name
                        },
                        success: function (result) {
                            //alert(result);
                            isExist = result;
                        }
                    });
                    if (isExist == true) {
                        alert("名称:【" + name + "】 が既に使用しています。");
                        newNameInput.focus();
                        return;
                    }

                    smat.service.closeForm({
                        contentId: self.uuid + '_newName',
                        result: {
                            name: name
                        }
                    });
                }
            })


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: ebox,
                width: "410px",
                top: "20%",
                title: "名称",
                afterClose: function (result) {

                    handle(result);
                }
            });

        }, onActivate: function () {
            if (this.config.conditionOnly) {
                var splitter = this.config.page.config.designer.horizontalBox.data("asmatSplitter");
                splitter.collapse(".right-pane");

                var form = this.config.page.getControlByName("search_form");
                var toolBar = this.config.page.getControlByName("toolBar");
                var result_div = this.config.page.getControlByName("result_div");

                if (Number(form.config.rowsCount) < 10) {
                    form.config.rowsCount = 10;

                    form.propertyChange_rowsCount({ id: "rowsCount" }, form.config.rowsCount);
                }

                toolBar.body.closest("div.designing-drop").hide();
                result_div.body.closest("div.designing-drop").hide();
                //result_div.hide();

                //condition
                var mainSection = this.config.page.getControlByName("main_Section").body;

                //
                //$('<div class="row">検索条件:</div>').insertBefore(mainSection.children(".row:first"));

                var view = this.config.page.getEditView(form.config.view);

                var filterList = [], havingFilterList=[];

                if (view) {
                    for (var key in view.ViewFilterList) {
                        var filterItemTpem = smat.service.getItemByKey(this.config.page.entity.FilterList, "FilterName", view.ViewFilterList[key].FilterControlName);

                        if (filterItemTpem && filterItemTpem.IsHaving == false) {
                            filterList.push(filterItemTpem);
                        } else if (filterItemTpem && filterItemTpem.IsHaving == true) {
                            havingFilterList.push(filterItemTpem);
                        }
                    }
                }
                
                $(".filterListBox").remove();
                $(".havingFilterListBox").remove();

                if (filterList.length > 0) {
                    var filterStr = "";
                    for (var i in filterList) {
                        var filterItem = filterList[i];
                        if (filterItem.FilterDesc.indexOf("ViewItemFilter:") != 0) {
                            continue;
                        }
                        var viewItem = smat.service.getItemByKey(view.ItemList, "ItemName", filterItem.FilterDesc.replace("ViewItemFilter:", ""));
                        if (!viewItem) {
                            continue;
                        }

                        var sqlStr = "";
                        sqlStr = filterItem.FilterSql.replace(viewItem.ItemSql + " ", "");
                        sqlStr = sqlStr.replace("is not null", "⇒値あり");
                        sqlStr = sqlStr.replace("is null", "⇒値なし");

                        if (sqlStr.indexOf("between") >= 0 && sqlStr.indexOf("and") >= 0) {
                            sqlStr = sqlStr.replace("between ", "").replace("and", "～")
                        } else if (sqlStr.indexOf("in(") >= 0 && sqlStr.indexOf(")") >= 0) {
                            sqlStr = sqlStr.replace("in(", "").replace(")", "");
                        }

                        filterStr += viewItem.ItemDesc + " " + sqlStr + "　　";
                    }

                    $('<section class="filterListBox panel panel-default" style="margin-bottom: 5px;"><section class="panel-body">抽出条件：　' + filterStr + '</section></section>').insertBefore(mainSection.closest(".s-dy-page"));
                }

                if (havingFilterList.length > 0) {
                    var filterStr = "";
                    for (var i in havingFilterList) {
                        var filterItem = havingFilterList[i];
                        if (filterItem.FilterDesc.indexOf("ViewItemFilter:") != 0) {
                            continue;
                        }
                        var viewItem = smat.service.getItemByKey(view.ItemList, "ItemName", filterItem.FilterDesc.replace("ViewItemFilter:", ""));
                        if (!viewItem) {
                            continue;
                        }

                        var sqlStr = "";
                        sqlStr = filterItem.FilterSql.replace("DISTINCT ", "");
                        sqlStr = sqlStr.replace(viewItem.Group + "(" + viewItem.ItemSql + ")" + " ", "");
                        sqlStr = sqlStr.replace(" COUNT(*) ", "");
                        sqlStr = sqlStr.replace("is not null", "値あり");
                        sqlStr = sqlStr.replace("is null", "値なし");

                        if (sqlStr.indexOf("between") >= 0 && sqlStr.indexOf("and") >= 0) {
                            sqlStr = sqlStr.replace("between ", "").replace("and", "～")
                        } else if (sqlStr.indexOf("in(") >= 0 && sqlStr.indexOf(")") >= 0) {
                            sqlStr = sqlStr.replace("in(", "").replace(")", "");
                        }

                        filterStr += viewItem.ItemDesc + " " + sqlStr + "　　";
                    }

                    $('<section class="havingFilterListBox panel panel-default"  style="margin-bottom: 0px;"><section class="panel-body">集計後条件：　' + filterStr + '</section></section>').insertBefore(mainSection.closest(".s-dy-page"));
                }
            }
        }, beforeSave: function () {
            if (this.config.conditionOnly) {
                var splitter = this.config.page.config.designer.horizontalBox.data("asmatSplitter");
                splitter.collapse(".right-pane");

                var form = this.config.page.getControlByName("search_form");

                var num = Number(form.config.rowsCount);
                var newNum = 0;
                for (var i = num - 1 ; i >= 0 ; i--) {
                    var row = form.body.children(".row:eq(" + i + ")");
                    if (row.find("div.edit-skin-box-fill").length > 0) {
                        newNum = i + 1;
                        break;
                    } 
                }

                form.config.rowsCount = newNum;

                form.propertyChange_rowsCount({ id: "rowsCount" }, newNum);

            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.UserControl, smat.dynamics.tool.BaseTool);

})();