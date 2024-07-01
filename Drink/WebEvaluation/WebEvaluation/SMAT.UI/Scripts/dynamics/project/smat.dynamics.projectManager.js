(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsProjectManager = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.ProjectManager(config);
        });
    };

    smat.dynamics.ProjectManager = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.ProjectManager.prototype = {
        /**
		 * 初期化
		 * @name init
		 * @methodOf smat.dynamics.ProjectManager.prototype
		 */
        init: function () {
            var self = this;
            this.sectionDom = $('<section id="designer_projectManager_section" class="panel panel-default" style="height:100%;overflow: hidden;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;"></div>').appendTo(this.sectionDom.find('.panel-body'));

            this.mainRow = $('<div class="col-sm-12" style="height:100%;width:100%;position: relative;"></div>').appendTo(this.box);

            this.leftBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 250px;height: 100%;position: absolute;left: 5px;"></div>').appendTo(this.mainRow);
            this.rightBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;height:100%;position: absolute;padding-left: 250px;left: 5px;"></div>').appendTo(this.mainRow);

            this.leftContent = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.leftBox);

            this.toolBarContent = $('<div class="col-sm-12" style="height:43px;padding: 0;background-color: rgb(242, 245, 245);border: 1px solid #ccc;border-bottom: none;z-index: 10;"></div>').appendTo(this.rightBox);

            this.mainContentBox = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;position: relative;top: -43px;padding-top: 42px;"></div>').appendTo(this.rightBox);
            this.mainContent = $('<div id="project_manager_content" class="col-sm-12" style="height:100%;padding: 0;border-top: 1px solid #ccc;position: relative;overflow: auto;"></div>').appendTo(this.mainContentBox);

            this.treeviewBox = $('<div class="col-sm-12" style="padding: 10px 0;"></div>').appendTo(this.leftContent);

            this.treeData = [
                {
                    id: 0,
                    text: smat.service.optionSet("DyOptionText.Component"),
                    imageUrl: smat.global.basePath + "/SMAT.UI/images/folder_web.png",
                    items: [
                        {
                            id: 1,
                            text: smat.service.optionSet("DyOptionText.Entity"),
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/box.png",
                            value: "entityList"
                        },
                        {
                            id: 2,
                            text: smat.service.optionSet("DyOptionText.OptionSet"),
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/topic_option.png",
                            value: "optionSet"
                        },
                        //{
                        //    id: 3,
                        //    text: "ダッシュポート"
                        //},
                        //{
                        //    id: 4,
                        //    text: "電子メールテンプレート"
                        //},
                        //{
                        //    id: 5,
                        //    text: "Webリソース"
                        //},
                        {
                            id: 6,
                            text: smat.service.optionSet("DyOptionText.RoleManage"),
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/club_option.png",
                            managerType: "ProjectManagerRole"
                        },
                        {
                            id: 7,
                            text: smat.service.optionSet("DyOptionText.MenuManage"),
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/app.png",
                            managerType: "ProjectManagerMenu"
                        },
                        {
                            id: 8,
                            text: smat.service.optionSet("DyOptionText.SystemOption"),
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/module.png",
                            managerType: "ProjectManagerConfig"

                        },
                        {
                            id: 9,
                            text: smat.service.optionSet("DyOptionText.DbManage"),
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/database_option.png",
                            managerType: "ProjectManagerDatabase"
                        },
                        //{
                        //    id: 10,
                        //    text: "マスター管理",
                        //    managerType: ""
                        //}
                        {
                            id: 11,
                            text: "SearchSetting",
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/chart.png",
                            managerType: "ProjectManagerSearchSetting"

                        }
                    ]
                }
            ];


            this.treeviewBox.asmatTreeView({
                dataSource: self.treeData,
                select: function (e) {
                    var dataItem = this.dataItem(e.node);
                    self.dataItem = dataItem;
                    self.openFormPage(dataItem);
                }
            });

            this.treeview = this.treeviewBox.data("asmatTreeView");

            this.refreshTreeData();


        }, refreshTreeData: function () {
            var self = this;
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntityList,
                //async: false,
                params: {
                    ProjID: self.config.projID
                },
                success: function (result) {
                    //result = $.Enumerable.From(result).Where(function (e) { return e.EntityType != '0'; }).ToArray();

                    self.treeData[0].items[0].items = [];

                    var groups = $.Enumerable.From(result).GroupBy("$.EntityGroup").ToArray();

                    for (var key in groups) {

                        var gname = groups[key].Key();
                        if (!gname) gname = "other";

                        self.treeData[0].items[0].items.push({
                            text: gname,
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/folder.png",
                            value: "group",
                            items: []
                        })
                    }


                    for (var i in result) {
                        var gname = result[i].EntityGroup;
                        if (!gname) gname = "other";

                        var g = $.Enumerable.From(self.treeData[0].items[0].items).Where("$.text == '" + gname + "'").ToArray()[0];

                        g.items.push({
                            text: result[i].EntityDesc,
                            imageUrl: smat.global.basePath + "/SMAT.UI/images/table_edit.png",
                            value: "entity",
                            dataKey: result[i].EntityName,
                            items: [
                                { text: smat.service.optionSet("DyOptionText.Field"), imageUrl: smat.global.basePath + "/SMAT.UI/images/text-field.png", value: "field", dataKey: result[i].EntityName },
                                { text: smat.service.optionSet("DyOptionText.1NRela"), imageUrl: smat.global.basePath + "/SMAT.UI/images/link.png", value: "rela1N", dataKey: result[i].EntityName },
                                { text: smat.service.optionSet("DyOptionText.N1Rela"), imageUrl: smat.global.basePath + "/SMAT.UI/images/link.png", value: "relaN1", dataKey: result[i].EntityName },
                                { text: "Flow", imageUrl: smat.global.basePath + "/SMAT.UI/images/areachart.png", value: "flow", dataKey: result[i].EntityName },
                                //{ text: "業務ルール", value: "busRole", dataKey: result[i].EntityName },
                                //{ text: "フォーム", value: "form", dataKey: result[i].EntityName, typeKey: "'SimpleMasterList','SimpleMasterEdit'" },
                                //{ text: "ビュー", value: "view", dataKey: result[i].EntityName ,typeKey:"'SimpleSearch'"},
                                //{ text: "グラフ", value: "graph", dataKey: result[i].EntityName, typeKey: "'SimpleGraph'" },
                                //{ text: "帳票", value: "report", dataKey: result[i].EntityName, typeKey: "'SimpleStatistics','SimpleCrossStatistic'" },
                            ]
                        })
                    }

                    self.treeview.setDataSource(self.treeData);
                    self.treeview.expandPath([0, 1]);
                }
            });
        }, openFormPage: function (dataItem) {
            var self = this;

            if (self.config.projID == 0 || self.config.projID == undefined) {
                debugger;
            }


            self.closeContent();
            var formBox = $("<div></div>");

            if (dataItem.managerType != undefined) {
                smat.service.openForm({
                    contentDom: formBox,
                    fillTarget: "project_manager_content"
                });
                var tempConfig = {};
                tempConfig.target = formBox;
                tempConfig.projID = self.config.projID;
                new smat.dynamics[dataItem.managerType](tempConfig);
            } else {
                switch (dataItem.value) {
                    case "entityList":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_Entity",
                                pageName: "EntityList"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_Entity",
                                ViewName: "EntityList",
                                FilterDic: { EntityTypeFilter: "1,2" },
                                projectManager: self
                            },
                            fillTarget: "project_manager_content"
                        });
                        break;
                    case "optionSet":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_OptionSet",
                                pageName: "OptionList"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_OptionSet",
                                ViewName: "OptionList",
                                FilterDic: { ProjIDFilter: self.config.projID },
                                projectManager: self
                            },
                            fillTarget: "project_manager_content"
                        });
                        break;
                    case "entity":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_Entity",
                                pageName: "EntityEdit"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_Entity",
                                EntityDataItem: { ProjID: self.config.projID, EntityName: dataItem.dataKey },
                                projectManager: self
                            },
                            fillTarget: "project_manager_content"
                        });
                        break;
                    case "field":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_EntityField",
                                pageName: "EntityField"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_EntityField",
                                ViewName: "EntityField",
                                FilterDic: { EntityNameFilter: dataItem.dataKey }
                            },
                            fillTarget: "project_manager_content"
                        });
                        break;
                    case "rela1N":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_EntityRela1N",
                                pageName: "EntityRela1N"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_EntityRela1N",
                                ViewName: "EntityRela1N",
                                FilterDic: { EntityNameFilter: dataItem.dataKey }
                            },
                            fillTarget: "project_manager_content"
                        });
                        break;
                    case "relaN1":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_EntityRelaN1",
                                pageName: "EntityRelaN1"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_EntityRelaN1",
                                ViewName: "EntityRelaN1",
                                FilterDic: { EntityNameFilter: dataItem.dataKey }
                            },
                            fillTarget: "project_manager_content"
                        });
                        break;
                    case "flow":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_Flow",
                                pageName: "FlowList"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_Flow",
                                ViewName: "FlowList",
                                FilterDic: { EntityNameFilter: dataItem.dataKey }
                            },
                            fillTarget: "project_manager_content"
                        });
                        break;
                    case "form":
                    case "view":
                    case "graph":
                    case "report":
                        smat.service.openPage({
                            page: {
                                projID: self.config.projID,
                                entityName: "Y_EntityForm",
                                pageName: "EntityForm"
                                // templateUrl: smat.dynamics.commonURL.formPage
                            },
                            params: {
                                ProjID: self.config.projID,
                                EntityName: "Y_EntityForm",
                                ViewName: "EntityForm",
                                FilterDic: { EntityNameFilter: dataItem.dataKey, FormTypeFilter: dataItem.typeKey }
                            },
                            fillTarget: "project_manager_content"
                        });

                        break;

                    default:

                }
            }



        }, closeContent: function () {
            var pageNode = this.mainContent.find('.s-dy-page');
            if (pageNode.length > 0) {
                smat.service.closeForm({
                    contentId: pageNode.attr("id")
                });
            }
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.ProjectManager, smat.dynamics.Element);

})();