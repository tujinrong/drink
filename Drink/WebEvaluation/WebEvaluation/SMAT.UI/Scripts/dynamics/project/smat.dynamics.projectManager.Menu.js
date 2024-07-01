(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdProjectManagerMenu = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.ProjectManagerMenu(config);
        });
    };

    smat.dynamics.ProjectManagerMenu = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.ProjectManagerMenu.prototype = {
        /**
		 * 初期化
		 * @name init
		 * @methodOf smat.dynamics.ProjectManagerMenu.prototype
		 */
        init: function () {
            var self = this;
            this.sectionDom = $('<section id="designer_projectManager_database_section" class="panel panel-default" style="height:800px;overflow: hidden;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;"></div>').appendTo(this.sectionDom.find('.panel-body'));

            this.mainRow = $('<div class="col-sm-12" style="height:100%;width:100%;position: relative;"></div>').appendTo(this.box);

            this.leftBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 25%;height: 100%;float: left;"></div>').appendTo(this.mainRow);
            this.rightBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 60%;height: 100%;left: 15px;"></div>').appendTo(this.mainRow);
            var menuAddBox = $('<div class="row" style="margin: 3px 0 0 0;"><div class="col-sm-12 text-left text-center-xs" style="text-align: right;"><button class="btn-dark s-button" style="margin: 5px 14px;  padding: 5px 10px;">' + smat.service.optionSet("DyOptionText.Add") + '</button></div></div>').appendTo(this.leftBox);
            this.leftContent = $('<div class="col-sm-12" style="height:90%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.leftBox);
            var groupAddBox = $('<div class="row" style="margin: 3px 0 0 0;"><div class="col-sm-12 text-left text-center-xs" style="text-align: right;"><button class="btn-danger s-button" style="margin: 5px 14px;  padding: 5px 10px;"> ' + smat.service.optionSet("DyOptionText.Save") + ' </button><button class="btn-dark s-button" style="margin: 5px 14px;  padding: 5px 10px;">' + smat.service.optionSet("DyOptionText.Add") + '</button></div></div>').appendTo(this.rightBox);
            this.rightContent = $('<div class="col-sm-12" style="height:90%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.rightBox);

            this.rightSection = $('<section class="scrollable wrapper" style="padding: 0 0 0 2px;"><section class="panel panel-default"><div class="panel-body"></div></section></section>').appendTo(this.rightContent);
            this.menuGroupBox = this.rightSection.find(".panel-body");

            this.menuGrid = $("<div>").appendTo(this.leftContent);

            var columns = [
                    {
                        field: "MenuName",
                        title: smat.service.optionSet("DyOptionText.MenuName"),
                    },
                    {
                        field: "MenuDesc",
                        title: smat.service.optionSet("DyOptionText.Desc"),
                    },
                    {
                        field: "MenuName",
                        title: " ",
                        template: function (dataItem) {
                            return '<button class="btn-primary s-button" dataKey=' + dataItem.MenuName + ' style="padding: 3px 6px;min-width: 20px;">' + smat.service.optionSet("DyOptionText.Edit") + '</button><button class="btn-danger s-button" dataKey=' + dataItem.MenuName + ' style="padding: 3px 6px;min-width: 20px;">X</button>';
                        },
                        width: "90px"
                    }
            ]

            this.menuGrid.smatGrid({
                dataSource: [],
                columns: columns,
                dataBound: function (e) {
                    //e.sender.thead.find('tr').remove();
                    //e.sender.table.css('table-layout', 'fixed');
                    var trs = e.sender.tbody.children('tr');
                    if (trs.length > 0) trs.eq(0).children('td').css('border-top', 'none');

                    if (trs.length > 1) {
                    }
                    var grid = e.sender;
                    $.each(trs, function (n, value) {

                        $(this).asmatDraggable({
                            hint: function (item) {
                                self.dragTarget = this;
                                self.dragModel = "move";
                                self.dragDataItem = grid.dataItem($(item));

                                var hintElement = $('<div class="col-xs-12 col-sm-6 col-md-4  "><div class="item"><a href="javascript:void(0)" class="panel-brand text-lt"><i class="home-menu-icon ' + self.dragDataItem.MenuIcon + ' "></i><span class="hidden-nav-xs m-l-sm">' + self.dragDataItem.MenuDesc + '</span></a></div></div>');

                                return hintElement;
                            },
                            dragstart: function (e) {
                            },
                            dragend: function (e) {
                                self.dragDataItem = undefined;
                            }
                        });
                    });
                }
            });

            //menu edit
            this.menuGrid.delegate(".btn-primary", "click", function () {
                var menuName = $(this).attr("dataKey");
                var menu = smat.service.getItemByKey(self.menuGrid.ui().config.dataSource, "MenuName", menuName);
                self.menuDetail(menu);
            });

            //menu del
            this.menuGrid.delegate(".btn-danger", "click", function () {
                var rowSelf = $(this);
                var menuName = $(this).attr("dataKey");
                smat.service.confirm({
                    msg: smat.service.optionSet("DyOptionMsg.MenuDel",menuName),
                    callback: function () {
                        var delFlag = false;
                        smat.service.loadJosnData({
                            url: smat.dynamics.commonURL.delMenu,
                            async: false,
                            params: {
                                ProjID: self.config.projID,
                                MenuName: menuName
                            },
                            success: function (result) {
                                delFlag = result;
                            }
                        });

                        if (delFlag) {
                            rowSelf.closest("tr").remove();
                        }

                    }
                });
            });



            this.btnMenuAdd = menuAddBox.find("button");
            this.btnGroupAdd = groupAddBox.find(".btn-dark");
            this.btnGroupSave = groupAddBox.find(".btn-danger");

            this.btnMenuAdd.bind("click", function () {
                self.menuDetail();
            });
            this.btnGroupAdd.bind("click", function () {
                self.groupDetail();
            });

            this.btnGroupSave.bind("click", function () {
                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.saveMenuGroup,
                    async: false,
                    params: {
                        ProjID: self.config.projID,
                        menuGroupList: self.groupDataSource
                    },
                    success: function (result) {
                        if (result) {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.SaveSuccess"), type: "success" });
                        }
                    }
                });
            });

            this.setMenuData();
            this.setMenuGroupData();


        }, setMenuData: function () {
            var self = this;
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getMenuList,
                async: false,
                params: {
                    ProjID: self.config.projID
                },
                success: function (result) {
                    self.menuGrid.ui().setDataSource(result);
                }
            });

        }, setMenuGroupData: function () {
            var self = this;
            if (self.groupDataSource == undefined) {
                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.getMenuGroupList,
                    async: false,
                    params: {
                        ProjID: self.config.projID
                    },
                    success: function (result) {
                        self.groupDataSource = result;
                    }
                });
            }

            var tempSection = self.menuGroupBox.closest("section");

            self.menuGroupBox.remove();
            this.menuGroupBox = $('<div class="panel-body"></div>').appendTo(tempSection);


            for (var i in self.groupDataSource) {
                var groupSetion = $('<section class="panel panel-home no-borders "><header class="panel-heading font-bold s-group-item"><span class="panel-title">' + self.groupDataSource[i].GroupDesc + '</span><div class="s-group-item-edit" style="float: right;"><button class="btn-primary s-button" dataKey="' + self.groupDataSource[i].GroupName + '" style="padding: 3px 6px;min-width: 20px;">' + smat.service.optionSet("DyOptionText.Desc") + '</button><button class="btn-danger s-button" dataKey="' + self.groupDataSource[i].GroupName + '" style="padding: 3px 6px;min-width: 20px;margin-left: 5px;">X</button></div></header><div class="panel-body group-drop" dataKey="' + self.groupDataSource[i].GroupName + '"></div></section>').appendTo(self.menuGroupBox);
                var menusBox = groupSetion.find(".panel-body");
                for (var j in self.groupDataSource[i].Menus) {
                    $('<div class="col-xs-12 col-sm-6 col-md-4 menuBox" dataKey="' + self.groupDataSource[i].Menus[j].MenuName + '"><div class="item"><a href="javascript:void(0)" class="panel-brand text-lt s-menu-item"><i class="home-menu-icon ' + self.groupDataSource[i].Menus[j].MenuIcon + ' "></i><span class="hidden-nav-xs m-l-sm">' + self.groupDataSource[i].Menus[j].MenuDesc + '</span><button class="btn-danger s-button s-menu-item-del">x</button></a></div></div>').appendTo(menusBox);

                }

                menusBox.asmatSortable({
                    axis: "xy",
                    cursor: "move",
                    //container: ".edit-box",
                    placeholder: function (element) {
                        var placeholder = element.clone();
                        placeholder.css("opacity", "0.3");
                        return placeholder;
                    },
                    hint: function (element) {
                        var h = element.clone();
                        h.css('height', '160px');
                        return h;
                    }, change: function (e) {
                    }
                });
            }

            //group drop
            this.menuGroupBox.asmatDropTargetArea({
                filter: ".group-drop",
                dragenter: function (e) {
                    self.droptargetOnDragEnter(e);
                },
                dragleave: function (e) {
                    self.droptargetOnDragLeave(e);
                },
                drop: function (e) {
                    self.droptargetOnDrop(e);
                }
            });

            //group sortable
            this.menuGroupBox.asmatSortable({
                handler: ".panel-title",
                ignore: "group-drop",
                hint: function (element) {
                    return element.clone().addClass("hint");
                },
                placeholder: function (element) {
                    var placeholder = element.clone();
                    placeholder.css("opacity", "0.3");
                    return placeholder;
                },
                change: function (e) {
                    self.getMenuGroupData();
                }
            });

            // group menu del
            this.menuGroupBox.delegate(".s-menu-item-del", "click", function () {
                $(this).closest(".menuBox").remove();
                self.getMenuGroupData();
            });

            // group edit
            this.menuGroupBox.delegate(".s-group-item-edit .btn-primary", "click", function () {
                var groupName = $(this).attr("dataKey");
                var group = smat.service.getItemByKey(self.groupDataSource, "GroupName", groupName);
                self.groupDetail(group);
            })

            // group del
            this.menuGroupBox.delegate(".s-group-item-edit .btn-danger", "click", function () {
                $(this).closest("section").remove();
                self.getMenuGroupData();
            })
        }, droptargetOnDragEnter: function (e) {
            var self = this;
            if (self.dragDataItem == undefined) return;
            var newMenu = $('<div class="col-xs-12 col-sm-6 col-md-4 drag-temp-element menuBox" dataKey=' + self.dragDataItem.MenuName + ' style="opacity:0.3;"><div class="item"><a href="javascript:void(0)" class="panel-brand text-lt s-menu-item"><i class="home-menu-icon ' + self.dragDataItem.MenuIcon + ' "></i><span class="hidden-nav-xs m-l-sm">' + self.dragDataItem.MenuDesc + '</span><button class="btn-danger s-button s-menu-item-del">x</button></a></div></div>').appendTo($(e.dropTarget));
        }, droptargetOnDragLeave: function (e) {
            var target = $(e.dropTarget);

            target.find(".drag-temp-element").remove();

        }, droptargetOnDrop: function (e) {
            var self = this;
            var target = $(e.dropTarget);
            var groupName = target.attr("dataKey");
            var menuName = target.find(".drag-temp-element").attr("dataKey");

            var group = smat.service.getItemByKey(self.groupDataSource, "GroupName", groupName);

            if (group == undefined) {
                return;
            }

            var menuList = group.Menus;
            
            for (var i in menuList) {
                if (menuName == menuList[i].MenuName) {
                    smat.service.notice({ msg: smat.service.optionSet("DyOptionMsg.MenuExit"), type: "error" });
                    target.find(".drag-temp-element").remove();
                    return;
                }
            }

            target.find(".drag-temp-element").css("opacity", "1");
            target.find(".drag-temp-element").removeClass("drag-temp-element");

            this.getMenuGroupData();
        }, menuDetail: function (data) {
            var self = this;
            var ebox = $('<section id="' + this.uuid + '_newMenu" class="panel panel-default " style="margin: 0;padding: 10px;height: 270px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.MenuName") + '</label><input id="MenuName" class="s-textbox input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Desc") + '</label><input id="MenuDesc" class="s-textbox input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Icon") + '</label><input id="MenuIcon" class="s-textbox input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Url") + '</label><input id="MenuUrl" class="s-textbox input-s" ><button id="_pick_url" class="btn-info s-button" style="margin-left:10px;display:none;">' + smat.service.optionSet("DyOptionText.Select") + '</button><button id="_pick_flow" class="btn-info s-button" style="margin-left:10px;display:none;">' + smat.service.optionSet("DyOptionText.Select") + '</button></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Type") + '</label><input id="ObjetType" class="input-s" ><button id="_pick_newName" class="btn-info s-button" style="margin-left:10px;">' + smat.service.optionSet("DyOptionText.Ok") + '</button></div></div>').appendTo(ebox);
            var menuNameInput = ebox.find("#MenuName");
            var menuDescInput = ebox.find("#MenuDesc");
            var menuIconInput = ebox.find("#MenuIcon");
            var menuUrlInput = ebox.find("#MenuUrl");
            var objetTypeInput = ebox.find("#ObjetType");


            var pickUrlBtn = ebox.find("#_pick_url");
            pickUrlBtn.smatButton({
                click: function () {
                    self.pickUrl(function (result) {
                        if (result == undefined) return;
                        menuUrlInput.val("/" + result.entityName + "/" + result.formName);
                    });
                }
            });

            var pickFlowBtn = ebox.find("#_pick_flow");
            pickFlowBtn.smatButton({
                click: function () {
                    alert(3);
                }
            });

            objetTypeInput.smatDropDownList({
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                { text: smat.service.optionSet("DyOptionText.Menu"), value: "menu" },
                { text: smat.service.optionSet("DyOptionText.DynamicsPage"), value: "dynamics" },
                { text: smat.service.optionSet("DyOptionText.Action"), value: "action" },
                { text: "flow", value: "flow" }
                ],
                change: function ()
                {
                    if (objetTypeInput.val() == "dynamics") {
                        pickUrlBtn.show();
                    } else {
                        pickUrlBtn.hide();
                    }

                    if (objetTypeInput.val() == "flow") {
                        pickFlowBtn.show();
                    } else {
                        pickFlowBtn.hide();
                    }
                },
                index: 0
            });

            var isUpdate = false;
            if (data != undefined) {
                isUpdate = true;
                menuNameInput.val(data["MenuName"]);
                menuDescInput.val(data["MenuDesc"]);
                menuIconInput.val(data["MenuIcon"]);
                menuUrlInput.val(data["MenuUrl"]);
                objetTypeInput.ui().value(data["ObjetType"]);
                if (objetTypeInput.val() == "dynamics") {
                    pickUrlBtn.show();
                }
            }

            var newNameBtn = ebox.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    smat.service.clearErrorBorder(menuNameInput);
                    smat.service.noticeClear();
                    if (menuNameInput.val() == "") {
                        smat.service.notice({ msg: smat.service.optionSet("SysMsg.Required", smat.service.optionSet("DyOptionText.MenuName")), type: "error" });
                        smat.service.addErrorBorder(menuNameInput);
                        menuNameInput.focus();
                        return;
                    }

                    var menu = {
                        ProjID: self.config.projID,
                        MenuName: menuNameInput.val(),
                        MenuDesc: menuDescInput.val(),
                        MenuIcon: menuIconInput.val(),
                        MenuUrl: menuUrlInput.val(),
                        ObjetType: objetTypeInput.val()
                    };

                    var isExist = false;
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.saveMenu,
                        async: false,
                        params: {
                            menu: menu,
                            isUpdate: isUpdate
                        },
                        success: function (result) {
                            isExist = !result;
                        }
                    });
                    if (isExist == true) {
                        smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit",menuNameInput.val()), type: "error" });
                        smat.service.addErrorBorder(menuNameInput);
                        menuNameInput.focus();
                        return;
                    } else {
                        self.setMenuData();
                        smat.service.closeForm({
                            contentId: self.uuid + '_newMenu'
                        });
                    }
                }
            });

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: ebox,
                width: "530px",
                top: "20%",
                title: smat.service.optionSet("DyOptionText.Menu"),
                afterClose: function (result) {
                }
            });
        }, groupDetail: function (data) {
            var self = this;
            var ebox = $('<section id="' + this.uuid + '_newGroup" class="panel panel-default " style="margin: 0;padding: 10px;height: 160px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.GroupName") + '</label><input id="GroupName" class="s-textbox input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Desc") + '</label><input id="GroupDesc" class="s-textbox input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Icon") + '</label><input id="GroupIcon" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info s-button" style="margin-left:10px;">' + smat.service.optionSet("DyOptionText.Ok") + '</button></div></div>').appendTo(ebox);
            var groupNameInput = ebox.find("#GroupName");
            var groupDescInput = ebox.find("#GroupDesc");
            var groupIconInput = ebox.find("#GroupIcon");

            var isUpdate = false;
            var currentGroupName = "";
            if (data != undefined) {
                isUpdate = true;
                groupNameInput.val(data["GroupName"]);
                groupDescInput.val(data["GroupDesc"]);
                groupIconInput.val(data["GroupIcon"]);
                currentGroupName = data["GroupName"];
                isUpdate = true;
            }

            var newNameBtn = ebox.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    smat.service.clearErrorBorder(groupNameInput);
                    smat.service.noticeClear();
                    if (groupNameInput.val() == "") {
                        smat.service.notice({ msg: smat.service.optionSet("SysMsg.Required", smat.service.optionSet("DyOptionText.GroupName")), type: "error" });
                        smat.service.addErrorBorder(groupNameInput);
                        groupNameInput.focus();
                        return;
                    }

                    var currentGroup = smat.service.getItemByKey(self.groupDataSource, "GroupName", currentGroupName);

                    if (isUpdate) {
                        currentGroup.ProjID = self.config.projID;
                        currentGroup.GroupName = groupNameInput.val();
                        currentGroup.GroupDesc = groupDescInput.val();
                        currentGroup.GroupIcon = groupIconInput.val();
                    } else {
                        if (smat.service.getItemByKey(self.groupDataSource, "GroupName", groupNameInput.val()) != undefined) {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit",groupNameInput.val()), type: "error" });
                            smat.service.addErrorBorder(groupNameInput);
                            groupNameInput.focus();
                            return;
                        } else {
                            var groupIndex = self.groupDataSource.length;

                            var group = {
                                ProjID: self.config.projID,
                                GroupName: groupNameInput.val(),
                                GroupDesc: groupDescInput.val(),
                                GroupIcon: groupIconInput.val(),
                                Seq: groupIndex,
                                Menus:[]
                            };

                            self.groupDataSource.push(group);
                        }
                    }
                    self.setMenuGroupData();
                    smat.service.closeForm({
                        contentId: self.uuid + '_newGroup'
                    });
                }
            });

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: ebox,
                width: "410px",
                top: "20%",
                title: smat.service.optionSet("DyOptionText.Group"),
                afterClose: function (result) {
                }
            });
        }, getMenuGroupData: function () {
            var self = this;
            var groups = [];
            this.menuGroupBox.find(".group-drop:visible").each(function (index, element) {
                var groupName = $(this).attr("dataKey");
                var group = smat.service.getItemByKey(self.groupDataSource, "GroupName", groupName);
                group.Menus = [];
                $(this).find(".menuBox:visible").each(function (index, element) {
                    var menuName = $(this).attr("dataKey");
                    var tempMenu = smat.service.getItemByKey(self.menuGrid.ui().config.dataSource, "MenuName", menuName);
                    group.Menus.push(tempMenu);
                });
                groups.push(group);
            });
            this.groupDataSource = groups;
        }, pickUrl: function (handler) {
            var self = this;
            var ebox = $('<section id="' + this.uuid + '_newUrl" class="panel panel-default " style="margin: 0;padding: 10px;height: 100%;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.EntityName") + '</label><input id="EntityName" class="input-s" ><button id="_pick_search" class="btn-info s-button" style="margin-left:10px;">' + smat.service.optionSet("DyOptionText.Search") + '</button></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><div id="form_grid"></div></div></div>').appendTo(ebox);

            var entityNameInput = ebox.find("#EntityName");
            var searchBtn = ebox.find("#_pick_search");
            var grid = ebox.find("#form_grid");
            searchBtn.smatButton({
                click: function () {
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.getFormListByEntityName,
                        async: false,
                        params: {
                            ProjID: self.config.projID,
                            entityName: entityNameInput.val()
                        },
                        success: function (result) {
                            if (result.length == 0)
                            {
                                smat.service.notice({ msg: smat.service.optionSet("SysMsg.NoData"), type: "info" });
                            }
                            grid.ui().config.dataSource = result;
                            grid.ui().refresh();
                        }
                    });
                }
            });

            entityNameInput.smatDropDownList({
                dataSource: [],
                dataValueField: "EntityName",
                dataTextField: "EntityDesc"
            });

            var columns = [
                    {
                        field: "EntityName",
                        title: "EntityName",
                    },
                    {
                        field: "FormName",
                        title: "FormName",
                    },
                    {
                        field: "EntityName",
                        title: " ",
                        template: function (dataItem) {
                            return '<button class="btn-primary s-button" entityKey="' + dataItem.EntityName + '"  formKey="' + dataItem.FormName + '" style="padding: 3px 6px;min-width: 20px;">' + smat.service.optionSet("DyOptionText.Select") + '</button>';
                        },
                        width: "90px"
                    }
            ]

            grid.smatGrid({
                dataSource: [],
                columns: columns
            });
            
            grid.delegate("button", "click", function () {
                var EntityName = $(this).attr("entityKey");
                var FormName = $(this).attr("formKey");
                smat.service.closeForm({
                    contentId: self.uuid + '_newUrl',
                    result: {
                        entityName: EntityName,
                        formName: FormName
                    }
                });
            });

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: ebox,
                width: "500px",
                top: "20%",
                title: smat.service.optionSet("DyOptionText.EntityForm"),
                afterClose: function (result) {
                    handler(result);
                }
            });

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntityList,
                async: false,
                params: {
                    ProjID: this.config.projID
                },
                success: function (result) {
                    entityNameInput.ui().uiControl.setDataSource(result);

                }
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.ProjectManagerMenu, smat.dynamics.Element);

})();