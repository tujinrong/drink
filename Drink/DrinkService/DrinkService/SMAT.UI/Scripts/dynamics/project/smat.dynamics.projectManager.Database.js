(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdProjectManagerDatabase = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.ProjectManagerDatabase(config);
        });
    };

    smat.dynamics.ProjectManagerDatabase = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);


        //初期化
        this.init();

        return this;
    };

    smat.dynamics.ProjectManagerDatabase.prototype = {
        /**
		 * 初期化
		 * @name init
		 * @methodOf smat.dynamics.ProjectManagerDatabase.prototype
		 */
        init: function () {
            var self = this;
            this.sectionDom = $('<section id="designer_projectManager_database_section" class="panel panel-default" style="height:800px;overflow: hidden;"><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.box = $('<div style="height:100%;width:100%;position: relative;"></div>').appendTo(this.sectionDom.find('.panel-body'));

            this.mainRow = $('<div class="col-sm-12" style="height:100%;width:100%;position: relative;"></div>').appendTo(this.box);

            this.leftBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;z-index: 10;width: 200px;height: 100%;position: absolute;left: 5px;"></div>').appendTo(this.mainRow);
            this.rightBox = $('<div class="col-sm-12" style="margin: 0;padding: 0;height:100%;position: absolute;padding-left: 205px;left: 5px;"></div>').appendTo(this.mainRow);

            this.leftContent = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;overflow-y: auto;"></div>').appendTo(this.leftBox);

            this.toolBarContent = $('<div class="col-sm-12" style="height:43px;padding: 0;background-color: rgb(242, 245, 245);border: 1px solid #ccc;border-bottom: none;z-index: 10;"></div>').appendTo(this.rightBox);

            this.rowtoolBar = $('<div class="row" style="margin: 3px 0 0 0;"><button id="designer_projectManager_database_search_btn" class="btn-dark s-button" style="margin-left:5px;"><i class="icon-reload"></i>　' + smat.service.optionSet("DyOptionText.Refresh") + '</button></div>').appendTo(this.toolBarContent);

            //this.rowGrid = $('<div class="row" style="margin: 0;"><div id="designer_projectManager_database_grid"></div></div>');

            this.mainContentBox = $('<div class="col-sm-12" style="height:100%;padding: 0;border: 1px solid #ccc;position: relative;top: -43px;padding-top: 42px;"></div>').appendTo(this.rightBox);
            this.mainContent = $('<div id="designer_form_main_content" class="col-sm-12" style="height:100%;padding: 0;border-top: 1px solid #ccc;position: relative;overflow: auto;"></div>').appendTo(this.mainContentBox);

            this.settingBox = $('<div class="form-horizontal"></div>').appendTo(this.mainContent);
            var label = $('<div class="row" style="margin:8px 0;"><div class=" form-group"><label class="control-label input-s text-left" style="  text-align: left;margin-left: 15px;">' + smat.service.optionSet("DyOptionText.DetailSet") + '</label></div></div>').appendTo(this.settingBox);
            var createTyple = $('<div class="row" style="margin:8px 0;"><div class=" form-group"><label class="radio m-l-md  i-checks input-s-md"><input type="radio" id="chs-type-cover" checked="checked" class="chs-item" name="buildType" value="cover"><i></i>' + smat.service.optionSet("DyOptionText.Cover") + '</label><label class="radio m-l-md  i-checks input-s-md"><input type="radio" id="chs-type-difference" class="chs-item" name="buildType" value="difference"><i></i>' + smat.service.optionSet("DyOptionText.Difference") + '</label></div></div>').appendTo(this.settingBox);

            this.execBtn = $('<button class="btn-dark s-button" style="margin-left:5px;"><i class="fa fa-play"></i>　' + smat.service.optionSet("DyOptionText.SQLExecute") + '</button>').appendTo(this.rowtoolBar);

            var selectEntityBox = $('<div class="row" style="margin: 3px 0 0 0;"><div class="col-sm-12 text-left text-center-xs"><button id="btn_selectAll" class="btn-dark s-button">' + smat.service.optionSet("DyOptionText.SelectAll") + '</button><button id="btn_unSelectAll" class="btn-dark s-button" style="margin-left:5px;">' + smat.service.optionSet("DyOptionText.UnSelectAll") + '</button></div></div>').appendTo(this.leftContent);
            var btn_selectAll = selectEntityBox.find("#btn_selectAll");
            var btn_unSelectAll = selectEntityBox.find("#btn_unSelectAll");
            this.treeviewBox = $('<div class="col-sm-12" style="padding: 10px 0;"></div>').appendTo(this.leftContent);

            this.treeviewBox.asmatTreeView({
                dataSource: [],
                select: function (e) {
                    var dataItem = this.dataItem(e.node);
                    $(self.sqlBox).text(dataItem.text);
                },
                template: function (dataItem){
                    return '<label class="checkbox m-l-md i-checks input-s-ss"><input id="chk-box-no" type="checkbox" dataKey="' + dataItem.item.EntityName + '" class="chs-item" name="entityChk"><i></i>' + dataItem.item.text + '</label>';
                }
            });

            this.treeview = this.treeviewBox.data("asmatTreeView");

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntityList,
                async: false,
                params: {
                    ProjID: this.config.projID
                },
                success: function (result) {
                    self.setTreeData(result);
                }
            });

            this.execBtn.smatButton({
                click: function (e) {
                    smat.service.openLoding();
                    var entityList = [];

                    var checkedItem = self.treeviewBox.find(":checkbox:checked");

                    for (var i = 0; i < checkedItem.length; i++) {
                        entityList.push($(checkedItem[i]).attr("dataKey"));
                    }
                    
                    smat.service.loadJosnData({
                        url: smat.dynamics.commonURL.execEntitySql,
                        async: false,
                        params: {
                            EntityList: entityList,
                            BuildType: $("input[name='buildType']").val(),
                            ProjID: self.config.projID
                        },
                        success: function (result) {
                            smat.service.closeLoding();
                            smat.service.notice({ msg: smat.service.optionSet("DyOptionText.SQLExecuteDone") });
                        }
                    });
                    //smat.service.notice({ msg: smat.service.optionSet("DyOptionText.SQLExecuteDone") });
                }
            });

            btn_selectAll.smatButton({
                click: function (e) {
                   
                    self.treeviewBox.find(":checkbox").prop("checked", true);
                }
            });

            btn_unSelectAll.smatButton({
                click: function (e) {
                    self.treeviewBox.find(":checkbox:checked").prop("checked", false);

                }
            });
        }, setTreeData: function (datas) {
            var ds = new Array();

            for (var i = 0; i < datas.length; i++) {
                var item = smat.globalObject.clone(datas[i]);
                item.text = item.EntityDesc;
                ds.push(item);
            }

            //ds = $.Enumerable.From(ds).Where("$.EntityName.indexOf('Y_') < 0").ToArray();

            this.treeview.setDataSource(ds);
            this.treeview.expand(this.treeviewBox.find('.s-item'));
            this.treeviewBox.find(".s-in").css("width", "100%");
            this.treeviewBox.find(".checkbox").css("padding-left", "0px").css("margin-top", "0px").css("margin-bottom", "0px");
        },createSql: function(dataItem){
            
        }
	}

	// extend Node
	smat.globalObject.extend(smat.dynamics.ProjectManagerDatabase, smat.dynamics.Element);

})();