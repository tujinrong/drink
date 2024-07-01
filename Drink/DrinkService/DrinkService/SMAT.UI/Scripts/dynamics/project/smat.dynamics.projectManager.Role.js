(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdProjectManagerRole = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.ProjectManagerRole(config);
        });
    };

    smat.dynamics.ProjectManagerRole = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.ProjectManagerRole.prototype = {
        /**
		 * 初期化
		 * @name init
		 * @methodOf smat.dynamics.ProjectManagerRole.prototype
		 */
        init: function () {
            var self = this;
            this.sectionDom = $('<section id="designer_projectManager_role_section" class="panel panel-default" style="height:800px;overflow: hidden;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;"></div>').appendTo(this.sectionDom.find('.panel-body'));

            this.mainRow = $('<div class="col-sm-12" style="height:100%;width:100%;position: relative;"></div>').appendTo(this.box);

            this.leftBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 25%;height: 100%;float: left;"></div>').appendTo(this.mainRow);
            var roleEditBox = $('<div class="row" style="margin: 3px 0 0 0;"><div class="col-sm-12 text-left text-center-xs" style="text-align: right;"><button class="btn-danger s-button" style="margin: 5px 14px;  padding: 5px 10px;"> ' + smat.service.optionSet("DyOptionText.Save") + ' </button><button class="btn-dark s-button" style="margin: 5px 14px;  padding: 5px 10px;">' + smat.service.optionSet("DyOptionText.Add") + '</button></div></div>').appendTo(this.leftBox);
            this.leftContent = $('<div class="col-sm-12" style="height:90%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.leftBox);
            this.rightBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 25%;height: 100%;left: 15px;"></div>').appendTo(this.mainRow);
            var menuAddBox = $('<div class="row" style="margin: 3px 0 0 0;"><div class="col-sm-12 text-left text-center-xs"><button id="btn_selectAll" class="btn-dark s-button" style="  margin: 5px 14px; padding: 5px 10px;">' + smat.service.optionSet("DyOptionText.SelectAll") + '</button><button id="btn_unSelectAll" class="btn-dark s-button" style="  margin: 5px 14px; padding: 5px 10px;">' + smat.service.optionSet("DyOptionText.UnSelectAll") + '</button></div></div>').appendTo(this.rightBox);
            this.rightContent = $('<div class="col-sm-12" style="height:90%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.rightBox);
            
            this.roleGrid = $("<div>").appendTo(this.leftContent);
            this.menuGrid = $("<div>").appendTo(this.rightContent);

            var roleColumns = [
                    {
                        field: "RoleName",
                        title: smat.service.optionSet("DyOptionText.RoleName"),
                    },
                    {
                        field: "RoleDesc",
                        title: smat.service.optionSet("DyOptionText.Desc"),
                    },
                    {
                        field: "RoleName",
                        title: " ",
                        template: function (dataItem) {
                            return '<button class="btn-primary s-button" dataKey=' + dataItem.RoleName + ' style="padding: 3px 6px;min-width: 20px;">' + smat.service.optionSet("DyOptionText.Edit") + '</button><button class="btn-danger s-button" dataKey=' + dataItem.RoleName + ' style="padding: 3px 6px;min-width: 20px;">X</button>';
                        },
                        width: "90px"
                    }
            ]

            this.roleGrid.smatGrid({
                dataSource: [],
                columns: roleColumns
            });

            var menuColumns = [
                    {
                        field: "MenuName",
                        title: " ",
                        template: function (dataItem) {
                            return '<label class="checkbox m-l-md i-checks input-s-ss" style="padding-left: 0px; margin-top: 0px; margin-bottom: 0px;width: 0px;"><input id="chk-box-no" type="checkbox" dataKey="' + dataItem.MenuName + '" class="chs-item" name="entityChk"><i></i></label>';
                        },
                        width: "30px"
                    },
                    {
                        field: "MenuName",
                        title: smat.service.optionSet("DyOptionText.MenuName"),
                    },
                    {
                        field: "MenuDesc",
                        title: smat.service.optionSet("DyOptionText.Desc"),
                    }
            ]

            this.menuGrid.smatGrid({
                dataSource: [],
                columns: menuColumns
            });

            this.btnRoleAdd = roleEditBox.find(".btn-dark");
            this.btnRoleSave = roleEditBox.find(".btn-danger");
            this.btnSelectAll = menuAddBox.find("#btn_selectAll");
            this.btnUnSelectAll = menuAddBox.find("#btn_unSelectAll");

            this.btnRoleAdd.bind("click", function () {
                self.roleDetail();
            });

            this.btnRoleSave.bind("click", function () {
                self.setRoleMenu();
                smat.service.openLoding();
                smat.service.loadJosnData({
                    url: smat.dynamics.commonURL.saveRole,
                    async: false,
                    params: {
                        ProjID: self.config.projID,
                        roleList: self.roleGrid.ui().config.dataSource
                    },
                    success: function (result) {
                        if (result) {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.SaveSuccess"), type: "success" });
                        }

                    }
                });
            });

            this.btnSelectAll.bind("click", function () {
                self.menuGrid.find(":checkbox").prop("checked", true);
            });

            this.btnUnSelectAll.bind("click", function () {
                self.menuGrid.find(":checkbox:checked").prop("checked", false);
            });

            //role select
            this.roleGrid.delegate("tr", "click", function () {
                self.setRoleMenu();
                self.roleGrid.find("tr").css("background-color", "");
                self.roleGrid.find("tr.dataActive").removeClass("dataActive");
                self.menuGrid.find(":checkbox:checked").prop("checked", false);
                $(this).css("background-color", "bisque");
                $(this).addClass("dataActive");
                var roleName = $(this).find(".btn-primary").attr("dataKey");
                var role = smat.service.getItemByKey(self.roleGrid.ui().config.dataSource, "RoleName", roleName);
                for (var i = 0; i < role.Menus.length; i++) {
                    var menuName = role.Menus[i]["MenuName"];
                    self.menuGrid.find("[dataKey='" + menuName + "']").prop("checked", true);
                }
            });

            //role edit
            this.roleGrid.delegate(".btn-primary", "click", function () {
                var roleName = $(this).attr("dataKey");
                var role = smat.service.getItemByKey(self.roleGrid.ui().config.dataSource, "RoleName", roleName);
                self.roleDetail(role);
            });
            //role delete
            this.roleGrid.delegate(".btn-danger", "click", function () {
                var rowSelf = $(this);
                var roleName = $(this).attr("dataKey");
                smat.service.confirm({
                    msg: smat.service.optionSet("DyOptionMsg.MenuDel",roleName),
                    callback: function () {
                        var delFlag = false;
                        var dataSource = self.roleGrid.ui().config.dataSource;
                        var newDataSouce = [];
                        for (var i = 0; i < dataSource.length; i++) {
                            if(dataSource[i]["RoleName"] != roleName)
                            {
                                newDataSouce.push(dataSource[i]);
                            }
                        }
                        self.roleGrid.ui().config.dataSource = newDataSouce;
                        self.roleGrid.ui().refresh();
                    }
                });
            });

            this.setMenuData();
            this.setRoleData();
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
        }, setRoleData: function () {
            var self = this;
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getRoleList,
                async: false,
                params: {
                    ProjID: self.config.projID
                },
                success: function (result) {
                    self.roleGrid.ui().setDataSource(result);
                }
            });
        }, setRoleMenu: function () {
            var self = this;
            if (this.roleGrid.find("tr.dataActive").length > 0)
            {
                var roleName = this.roleGrid.find("tr.dataActive").find(".btn-primary").attr("dataKey");
                var role = smat.service.getItemByKey(self.roleGrid.ui().config.dataSource, "RoleName", roleName);
                role.Menus = [];
                $.each(self.menuGrid.find(":checkbox:checked"), function (n, value) {
                    var menuName = $(this).attr("dataKey");
                    var menu = smat.service.getItemByKey(self.menuGrid.ui().config.dataSource, "MenuName", menuName);
                    role.Menus.push(menu);
                });

            }
        }, roleDetail: function (data) {
            var self = this;
            var ebox = $('<section id="' + this.uuid + '_newRole" class="panel panel-default " style="margin: 0;padding: 10px;height: 120px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.RoleName") + '</label><input id="RoleName" class="s-textbox input-s" ></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group" ><label class="control-label input-s-md text-right" style="margin-right:5px;">' + smat.service.optionSet("DyOptionText.Desc") + '</label><input id="RoleDesc" class="s-textbox input-s" ><button id="_pick_newName" class="btn-info s-button" style="margin-left:10px;">ok</button></div></div>').appendTo(ebox);
            var roleNameInput = ebox.find("#RoleName");
            var roleDescInput = ebox.find("#RoleDesc");

            var isUpdate = false;
            var currentRoleName = "";
            if (data != undefined) {
                isUpdate = true;
                roleNameInput.val(data["RoleName"]);
                roleDescInput.val(data["RoleDesc"]);
                currentRoleName = data["RoleName"];
            }

            var newNameBtn = ebox.find("#_pick_newName");
            newNameBtn.smatButton({
                click: function () {
                    smat.service.clearErrorBorder(roleNameInput);
                    smat.service.noticeClear();
                    if (roleNameInput.val() == "") {
                        smat.service.notice({ msg: "【役割名称】を入力してください。", type: "error" });
                        smat.service.addErrorBorder(roleNameInput);
                        roleNameInput.focus();
                        return;
                    }

                    var currentRole = smat.service.getItemByKey(self.roleGrid.ui().config.dataSource, "RoleName", currentRoleName);

                    if (isUpdate) {
                        currentRole.ProjID = self.config.projID;
                        currentRole.RoleName = roleNameInput.val();
                        currentRole.RoleDesc = roleDescInput.val();
                    } else {
                        if (smat.service.getItemByKey(self.roleGrid.ui().config.dataSource, "RoleName", roleNameInput.val()) != undefined) {
                            smat.service.notice({ msg: smat.service.optionSet("SysMsg.Exit",roleNameInput.val()), type: "error" });
                            smat.service.addErrorBorder(roleNameInput);
                            roleNameInput.focus();
                            return;
                        } else {
                            var role = {
                                ProjID: self.config.projID,
                                RoleName: roleNameInput.val(),
                                RoleDesc: roleDescInput.val(),
                                Menus: []
                            };

                            self.roleGrid.ui().config.dataSource.push(role);
                        }
                    }

                    self.roleGrid.ui().refresh();
                    smat.service.closeForm({
                        contentId: self.uuid + '_newRole'
                    });
                }
            });

            smat.service.openForm({
                //m_opacity: 0,
                contentDom: ebox,
                width: "410px",
                top: "20%",
                title: smat.service.optionSet("DyOptionText.RoleName"),
                afterClose: function (result) {
                }
            });
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.ProjectManagerRole, smat.dynamics.Element);

})();