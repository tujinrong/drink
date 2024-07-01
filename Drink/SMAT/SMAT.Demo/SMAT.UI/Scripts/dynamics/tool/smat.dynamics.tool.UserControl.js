
(function () {

    ///////////////////////////////////////////////////////////////////////
    //  smat.dynamics.tool.UserControl
    ///////////////////////////////////////////////////////////////////////

    smat.dynamics.tool.UserControl = function (config) {
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
    smat.dynamics.tool.UserControl.prototype = {

        init: function () {


        }, toolBuild: function () {
            var self = this;

            this.userControls = new Array();

            this.addBtnBox = $("<div style='text-align:right;background-color: #fff;'><button class='btn-dark s-button' style ='margin: 5px;  padding: 5px 10px;'>+ 作成</button></div>").appendTo(this.config.box);
            this.addBtn = this.addBtnBox.find("button");

            this.addBtn.bind("click", function () {
                
                self.getNewName(function (result) {
                    if (result) {
                        var projID = self.config.page.config.projID;
                        var entityName = self.config.page.config.entityName;

                        var mode = "new";

                        var designerFormBox = $("<div style='height:600px;'></div>");
                        var designerBox = $("<div id='userControl_designer'></div>").appendTo(designerFormBox);

                        smat.service.openForm({
                            contentDom: designerFormBox,
                            width: "1050px",
                            afterClose: function () {
                                self.setControls();
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
                            templateType: templateType
                        });
                    }
                });
            });

            this.treebox = $("<div/>").appendTo(this.config.box);
            
            this.setControls();
        }, setControls: function () {

            var self = this;

            if (this.treebox.data("asmatTreeView")) {
                this.treebox.data("asmatTreeView").destroy();
                this.treebox.remove();
                this.treebox = $("<div/>").appendTo(this.config.box);
            }

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getUserControlList,
                params: {
                    ProjID: this.config.page.entity.ProjID,
                    EntityName: this.config.page.entity.EntityName
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
                            return data.item.UserControlName + "　<span class='s-icon s-i-pencil' style ='height: 18px; margin-left: 5px;'>Edit</span>";
                        }
                    });

                    self.treeview = self.treebox.data("asmatTreeView");
                    self.treeview.expand(self.treebox.find('.s-item'));

                    var treeItems = self.treebox.find('.s-i-pencil');

                    $.each(treeItems, function (n, value) {

                        $(this).bind("click", function () { 
                            var data = self.treeview.dataItem($(this).closest('.s-item'));
                            
                            var projID = self.config.page.config.projID;
                            var entityName = self.config.page.config.entityName;

                            var designerFormBox = $("<div style='height:600px;'></div>");
                            var designerBox = $("<div id='userControl_designer'></div>").appendTo(designerFormBox);

                            smat.service.openForm({
                                contentDom: designerFormBox,
                                width: "1000px",
                                afterClose: function () { 
                                    self.setControls();
                                }
                            });
                            var templateType = "UserControl";

                            self.designer = designerBox.smatDynamicsdUserControlDesigner({
                                projID: projID,
                                userControlName: data.UserControlName,
                                entityName: entityName,
                                titleTarget: designerBox,
                                mainPage: self.config.page,
                                templateType: templateType
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

            uc.setFormData(dataItem.Controls[0].Controls);

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
           
            for (var key in data) {
                if (data[key].ControlType == "Div" || data[key].ControlType == "Form") {
                    data[key].isUserControl = true;
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
                            EntityName: self.config.page.entity.EntityName,
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

        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.tool.UserControl, smat.dynamics.tool.BaseTool);

})();