(function () {

    ///////////////////////////////////////////////////////////////////////
    //  Desinger
    ///////////////////////////////////////////////////////////////////////
    $.fn.smatDynamicsdProjectManagerSearchSetting = function (config) {

        if (config == undefined) config = {};
        $.each($(this), function (n, value) {
            config.target = $(this);

            new smat.dynamics.ProjectManagerSearchSetting(config);
        });
    };

    smat.dynamics.ProjectManagerSearchSetting = function (config) {
        //默认属性
        this.setConfig({
            target: undefined

        });

        this.setConfig(config);

        //初期化
        this.init();

        return this;
    };

    smat.dynamics.ProjectManagerSearchSetting.prototype = {
        /**
		 * 初期化
		 * @name init
		 * @methodOf smat.dynamics.ProjectManagerSearchSetting.prototype
		 */
        init: function () {
            var self = this;
            this.sectionDom = $('<section id="designer_projectManager_search_setting" class="panel panel-default" style=""><div class="panel-body" style="height:100%;padding:15px"></div></section>');
            this.config.target.replaceWith(this.sectionDom);
            this.config.target.remove();
            this.section = this.sectionDom.children(".panel-body")

            var row1 = $('<div class="col-sm-12" style="margin-bottom:5px;"></div>').appendTo(this.section);
            var row2 = $('<div class="col-sm-12" style="margin-bottom:5px;"></div>').appendTo(this.section);
            var row3 = $('<div class="col-sm-12" style="margin-bottom:5px;text-align:center;"></div>').appendTo(this.section);
            var row4 = $('<div class="col-sm-12" style="margin-bottom:5px;"></div>').appendTo(this.section);
            var row5 = $('<div class="col-sm-12" style="margin-bottom:5px;"></div>').appendTo(this.section);
            var row6 = $('<div class="col-sm-12" style="margin-bottom:5px;"></div>').appendTo(this.section);
            var row7 = $('<div class="col-sm-12" style="margin-bottom:5px;"></div>').appendTo(this.section);
            var row8 = $('<div class="col-sm-12" style="margin-bottom:5px;"></div>').appendTo(this.section);

            this.entityInput = $("<input style='width:320px;'/>").appendTo(row1);
            this.searchType = $("<input style='width:320px;'/>").appendTo(row2);


            this.searchBtn = $('<button class="btn-info s-button" style="margin-left:20px;" >検索</button>').appendTo(row3);
            this.autoNewBtn = $('<button class="btn-info s-button" style="margin-left:20px;" >一括作成</button>').appendTo(row3);
            this.newBtn = $('<button class="btn-info s-button" style="margin-left:20px;" >新規</button>').appendTo(row3);
            this.newFoldBtn = $('<button class="btn-info s-button" style="margin-left:20px;" >フォルダ作成</button>').appendTo(row3);

            this.selectBtn = $('<button class="btn-primary s-button" style="margin-left:20px;" >全選択</button>').appendTo(row4);
            this.delBtn = $('<button class="btn-primary s-button" style="margin-left:20px;" >削除</button>').appendTo(row4);
            this.saveBtn = $('<button class="btn-primary s-button" style="margin-left:20px;" >保存</button>').appendTo(row4);
            this.copyBtn = $('<button class="btn-primary s-button" style="margin-left:20px;" >コーピ</button>').appendTo(row4);


            this.grid = $('<div></div>').appendTo(row4);

            this.autoNewBtn.bind("click", function () {
                self.getTempAnalysisField();
            });

            this.saveBtn.bind("click", function () {
                self.saveData();
            });

            this.delBtn.bind("click", function () {
                self.delItem();
            });

            this.searchBtn.bind("click", function () {
                self.getAnalysisField();
            });

            this.copyBtn.bind("click", function () {
                self.copyData();
            });

            this.newBtn.bind("click", function () {
                self.pickFold(function (foldResult) {
                    var foldItem = foldResult.foldItem;
                    var newDataItem = {};

                    newDataItem.ProjID = foldItem.ProjID;
                    newDataItem.AnalysisEntityName = foldItem.AnalysisEntityName;
                    newDataItem.AnalysisKeyType = foldItem.AnalysisKeyType;
                    newDataItem.ParentAnalysisCD = foldItem.AnalysisCD;
                    var ds = self.grid.ui().config.dataSource;
                    newDataItem.AnalysisCD = $.Enumerable.From(ds).Max("Number($.AnalysisCD)")+1;

                    var confirm_config = {
                        title: smat.service.optionSet("SysText.Confirm"),
                        content: "新規タイプを選択してください。",
                        buttons: [
                            {
                                lbl: "&nbsp;&nbsp;&nbsp;一括選択&nbsp;&nbsp;&nbsp;",
                                value: "pick",
                                cls: "btn-primary"
                            },
                            {
                                lbl: "&nbsp;&nbsp;&nbsp;VirtualSql&nbsp;&nbsp;&nbsp;",
                                value: "virtualSql",
                                cls: "btn-primary"
                            },
                            {
                                lbl: "&nbsp;&nbsp;&nbsp;取消&nbsp;&nbsp;&nbsp;",
                                value: "cancel",
                                cls: "btn-primary"
                            }
                        ],
                        callback: function (newType) {
                            debugger;
                            if (newType == "virtualSql") {
                                self.editDataItem(newDataItem,true);
                            }
                        }
                    }
                    smat.service.dialog(confirm_config);
                });
            });

            this.newFoldBtn.bind("click", function () {
                self.pickFold(function (result) {
                    alert(2)
                });
            });

            

            var datas = null;
            //===================entity=======================
            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getEntityList,
                async: false,
                params: {
                    ProjID: this.config.projID
                },
                success: function (result) {
                    var d = new Array();
                    for (var k in result) {
                        if (result[k].EntityName != "M_Code") {
                            d.push(result[k]);
                        }
                    }
                    if (smat.dynamics.isUser()) {
                        datas = $.Enumerable.From(result).Where("$.EntityType == '1' ").ToArray();
                    } else {
                        datas = d;
                        // datas = $.Enumerable.From(result).Where("$.EntityName.indexOf('Y_') < 0").ToArray();
                    }
                }
            });

            this.entityInput.smatDropDownList({
                dataSource: datas,
                dataValueField: "EntityName",
                dataTextField: "EntityDesc",
                label: {
                    text: "entityName",
                    attrs: {
                        style: "width:100px;"
                    }
                }
                , change: function (e) {

                }
            });

            this.searchType.smatDropDownList({
                dataSource: [
                    { text: "抽出項目", value: "1" },
                    { text: "集計項目", value: "2" },
                    { text: "縦集計キー", value: "3" },
                    { text: "集計キー", value: "4" }
                ],
                dataValueField: "value",
                dataTextField: "text",
                label: {
                    text: "searchType",
                    attrs: {
                        style: "width:100px;"
                    }
                }
                , change: function (e) {

                }
            });

            this.grid.smatGrid({
                columns: [
                    {
                        title: "項目",
                        field: "FieldDesc",
                        template: function (dataItem) {

                            var s = '<input type="checkbox" tabindex="-1" id="_' + dataItem.id + '" pid="' + dataItem.parentId + '" class="s-checkbox"/>';

                            var img = '';
                            if (!dataItem.Title) {
                                img = '<img src="/SMAT.UI/images/folder.png" />';
                            }

                            s += '<label for="_' + dataItem.id + '" class="s-checkbox-label" style="margin-left: 5px;">' + img + dataItem.FieldDesc + '</label>'

                            return s;
                        }
                    },
                    {
                        title: "定義名称",
                        field: "Title",
                    },
                    {
                        title: "内部定義",
                        field: "FieldName",
                    },
                    {
                        title: "　　",
                        field: "colTemp",
                        width: "60px",
                        attributes: {},
                        actions: [

                            {
                                template: '<button class="btn-primary btn-up  s-button" style="width: 20px;min-width: 10px;padding: 4px 1px 3px 1px;">↑</button>',
                                click: function (dataItem, index) {
                                    self.moveItemIndex(dataItem, -1);
                                }
                            }, {
                                template: '<button class="btn-primary btn-down  s-button" style="width: 20px;min-width: 10px;padding: 4px 1px 3px 1px;">↓</button>',
                                click: function (dataItem, index) {
                                    self.moveItemIndex(dataItem, 1);
                                }
                            }
                        ]
                    },
                    {
                        title: "　　",
                        field: "colTemp",
                        width: "70px",
                        attributes: {
                            "class": "text-center"
                        },
                        actions: [
                            {
                                text: "修正",
                                click: function (dataItem, index) {
                                    self.editDataItem(dataItem);
                                }
                            }
                        ]
                    }
                ],
                isTreeList: true,
                editable: {
                    move: true
                },
                dataBound: function (e) {
                    var trs = e.sender.tbody.find("tr").not(".s-empty-row");

                    $.each(trs, function () {
                        var dataItem = e.sender.dataItem($(this));

                        var pid = $(this).find("[pid]");

                        var pids = e.sender.tbody.find("[pid='" + dataItem.parentId + "']");

                        if (pids.index(pid) == 0) {
                            $(this).find(".btn-up").hide();
                            $(this).find(".btn-down").css("margin-left", "1.8em");
                        }

                        if (pids.index(pid) == (pids.length - 1)) {
                            $(this).find(".btn-down").hide();
                        }

                    });
                }
            });

            //check link;
            function checkSub(dataItem, checked) {
                if (dataItem.hasChild) {
                    var subPids = self.grid.find("tbody").find("[pid='" + dataItem.id + "']");
                    $.each(subPids, function () {
                        $(this).prop("checked", checked);
                        var subDataItem = self.grid.ui().dataItem($(this).closest("tr"));
                        checkSub(subDataItem, checked);
                    })
                }
            }

            function unCheckParent(dataItem) {
                if (dataItem.parentId) {
                    var pid = self.grid.find("tbody").find("#_" + dataItem.parentId);

                    pid.prop("checked", false);
                    var pidDataItem = self.grid.ui().dataItem(pid.closest("tr"));
                    unCheckParent(pidDataItem);
                }
            }

            $(this.grid).on('change', '[type="checkbox"]', function (e) {
                var dataItem = self.grid.ui().dataItem($(this).closest("tr"));
                checkSub(dataItem, $(this).prop("checked"));
                if (!$(this).prop("checked")) {
                    unCheckParent(dataItem);
                }
            });

            this.getAnalysisField();

        }, initGridEvent: function () {
            var self = this;
            var treeList = this.grid.ui().uiControl;
            treeList.bind("drag", function (e) {
                var t = e.target;
                var hint = e.sender._dragging._draggable.hint;
                if (t.parent().find("img").length == 0) {
                    //hint.find(".s-drag-status").removeClass("s-add").addClass("s-denied");
                    hint.css("opacity", "0.6");
                } else {
                    hint.css("opacity", "1");
                }
            });
            treeList.bind("dragstart", function (e) {
                var t = e.target;
            });
            treeList.bind("drop", function (e) {
                var t = e.target;
                if (e.destination.Title) {
                    e.preventDefault();

                    var fromTr = $(e.sender._dragging.source);
                    var toTr = $(e.dropTarget).closest("tr");

                    var fromSeq = e.source.Seq;
                    var toSeq = e.destination.Seq;

                    if (fromSeq > toSeq) {

                        e.destination.Seq += 1;
                        var nextTr = toTr.next();
                        var nextDataItem = self.grid.ui().dataItem(nextTr);
                        while (nextDataItem.Seq < fromSeq) {
                            nextDataItem.Seq += 1;
                            nextTr = nextTr.next();
                            nextDataItem = self.grid.ui().dataItem(nextTr);
                        }

                        e.source.Seq = toSeq;

                        fromTr.detach();
                        toTr.before(fromTr);
                    } else if (toSeq > fromSeq) {
                        e.destination.Seq -= 1;
                        var prevTr = toTr.prev();
                        var prevDataItem = self.grid.ui().dataItem(prevTr);
                        while (prevDataItem.Seq > fromSeq) {
                            prevDataItem.Seq -= 1;
                            prevTr = prevTr.prev();
                            prevDataItem = self.grid.ui().dataItem(prevTr);
                        }

                        e.source.Seq = toSeq;
                        fromTr.detach();
                        toTr.after(fromTr);

                    }

                }
                //e.preventDefault();
            });
            treeList.bind("dragend", function (e) {
                var t = e.target;

            });


        }, pickFold: function (handle) {

            var self = this;
            var ebox = $('<section id="' + this.uuid + '_pickFold" class="panel panel-default " style="margin: 0;padding: 10px;"></section>');
            $('<div class="row" style="margin:8px 0;"><div id="_FoldTree"></div></div>').appendTo(ebox);
            $('<div class="row" style="margin:8px 0;"><div class=" form-group text-center" ><button id="_pick_fold" class="btn-info " style="margin-left:10px;">選択</button></div></div>').appendTo(ebox);

            var ds = self.grid.ui().config.dataSource;
            ds = $.Enumerable.From(ds).Where("!$.Title").ToArray();

            var _FoldTree = ebox.find("#_FoldTree");
           
            _FoldTree.smatGrid({
                dataSource:ds,
                columns: [
                    {
                        title: "フォルダ",
                        field: "FieldDesc",
                        template: function (dataItem) {

                            var s = '<input type="radio" tabindex="-1" id="_fold_' + dataItem.id + '" name="pickFold" pid="' + dataItem.parentId + '" class="s-radio"/>';

                            var img = '<img src="/SMAT.UI/images/folder.png" />';

                            s += '<label for="_fold_' + dataItem.id + '" class="s-radio-label" style="margin-left: 5px;">' + img + dataItem.FieldDesc + '</label>'

                            return s;
                        }
                    }
                ],
                isTreeList: true,
                dataBound: function (e) {

                }
            });

            if (ds.length > 0) {
                _FoldTree.ui().uiControl.expand(ebox.find("#_FoldTree tbody>tr:eq(0)"));
            }

            var _pick_fold = ebox.find("#_pick_fold");
            _pick_fold.smatButton({
                click: function () {
                    var checkeds = ebox.find("tbody").find("input:checked");
                    if (checkeds.length == 0) {
                        smat.service.notice({ msg: "目標フォルダを選択してください。" ,type:"info"})
                        return;
                    }

                    var dataItem = _FoldTree.ui().dataItem(checkeds.closest("tr"));

                    smat.service.closeForm({
                        contentId: self.uuid + '_pickFold',
                        result: {
                            foldItem: dataItem,
                        }
                    });
                }
            })


            smat.service.openForm({
                //m_opacity: 0,
                contentDom: ebox,
                width: "460px",
                top: "20%",
                title: "目標フォルダ選択",
                afterClose: function (result) {
                    if (result) {
                        handle(result);
                    }
                }
            });


        }, editDataItem: function (dataItem, isNew) {
            var self = this;

            //groupItem
            var configItems = [];

            configItems.push({
                key: "FieldDesc",
                title: "FieldDesc",
                style: " width: 340px;",
                type: "TextBox",
                value: dataItem.FieldDesc

            });

            configItems.push({
                key: "Title",
                title: "Title",
                style: " width: 340px;",
                type: "TextBox",
                value: dataItem.Title

            });

            configItems.push({
                key: "FieldName",
                title: "FieldName",
                style: " width: 340px;",
                type: "TextBox",
                value: dataItem.FieldName

            });

            configItems.push({
                key: "IsGroupBy",
                title: "IsGroupBy",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Off", value: "false" },
                    { text: "On", value: "true" }
                ],
                value: dataItem.IsGroupBy
            });

            configItems.push({
                key: "IsSum",
                title: "IsSum",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Off", value: "false" },
                    { text: "On", value: "true" }
                ],
                value: dataItem.IsSum
            });

            configItems.push({
                key: "IsCount",
                title: "IsCount",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Off", value: "false" },
                    { text: "On", value: "true" }
                ],
                value: dataItem.IsCount
            });

            configItems.push({
                key: "IsAvg",
                title: "IsAvg",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Off", value: "false" },
                    { text: "On", value: "true" }
                ],
                value: dataItem.IsAvg
            });

            configItems.push({
                key: "IsMax",
                title: "IsMax",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Off", value: "false" },
                    { text: "On", value: "true" }
                ],
                value: dataItem.IsMax
            });

            configItems.push({
                key: "IsMin",
                title: "IsMin",
                type: "ButtonGroup",
                dataTextField: "text",
                dataValueField: "value",
                dataSource: [
                    { text: "Off", value: "false" },
                    { text: "On", value: "true" }
                ],
                value: dataItem.IsMin
            });

            configItems.push({
                key: "EntityName",
                title: "EntityName",
                style: " width: 340px;",
                type: "TextBox",
                //enable: isNew == true,
                value: dataItem.EntityName

            });

            configItems.push({
                key: "Alias",
                title: "Alias",
                style: " width: 340px;",
                type: "TextBox",
                //enable: isNew == true,
                value: dataItem.Alias

            });
            

            configItems.push({
                key: "Path",
                title: "Path",
                style: " width: 340px;",
                type: "TextBox",
                //enable: isNew == true,
                value: dataItem.Path

            });

            configItems.push({
                key: "DataType",
                title: "DataType",
                style: " width: 340px;",
                type: "TextBox",
                //enable: isNew == true,
                value: dataItem.DataType

            });

            configItems.push({
                key: "Format",
                title: "Format",
                style: " width: 340px;",
                type: "TextBox",
                value: dataItem.Format

            });

            configItems.push({
                key: "OptionSet",
                title: "OptionSet",
                style: " width: 340px;",
                type: "TextBox",
                //type: "DropDownList",
                //codeKind: "CodeType",
                value: dataItem.OptionSet
            });
            
            
            configItems.push({
                key: "VirtualSql",
                title: "VirtualSql",
                style: " width: 340px;",
                type: "TextBox",
                value: dataItem.VirtualSql

            });
            configItems.push({
                key: "imageUrl",
                title: "imageUrl",
                style: " width: 340px;",
                type: "TextBox",
                value: dataItem.imageUrl

            });
            configItems.push({
                key: "text",
                title: "text",
                style: " width: 340px;",
                type: "TextBox",
                value: dataItem.text

            });

            

            smat.service.getUserConfig({
                title: "詳細",
                width: "640px",
                height: "80%",
                items: configItems,
                checkResult: function (result,uid) {
                    //smat.service.notice({ msg: "集計後条件「" + result.HavingFilterText + "」は正しくありません。", type: "error" });
                    return true;
                },
                callback: function (result) {
                    if (result) {
                        dataItem.FieldDesc = result.FieldDesc;
                        dataItem.Title = result.Title;
                        dataItem.IsGroupBy = result.IsGroupBy;
                        dataItem.IsSum = result.IsSum;
                        dataItem.IsCount = result.IsCount;
                        dataItem.IsAvg = result.IsAvg;
                        dataItem.IsMax = result.IsMax;
                        dataItem.IsMin = result.IsMin;
                        dataItem.FieldName = result.FieldName;
                        dataItem.EntityName = result.EntityName;
                        dataItem.Path = result.Path;
                        dataItem.DataType = result.DataType;
                        dataItem.VirtualSql = result.VirtualSql;
                        dataItem.imageUrl = result.imageUrl;
                        dataItem.text = result.text;
                        dataItem.Format = result.Format;
                        dataItem.Alias = result.Alias;
                        dataItem.OptionSet = result.OptionSet;
                        
                        

                        if (!dataItem.text) {
                            dataItem.text = dataItem.FieldDesc;
                        }

                        if (isNew) {
                            var ds = self.grid.ui().config.dataSource;
                            ds.push(dataItem);
                            self.setGridDataSource(ds);
                        }
                    }
                }
            });



        }, copyData: function () {
            var self = this;
            //groupItem
            var configItems = [];

            configItems.push({
                key: "type",
                title: "type",
                style: " width: 340px;",
                type: "DropDownList",
                dataSource: [
                   { text: "抽出項目", value: "1" },
                   { text: "集計項目", value: "2" },
                   { text: "縦集計キー", value: "3" },
                   { text: "集計キー", value: "4" }
                ],
                dataValueField: "value",
                dataTextField: "text",

            });

            smat.service.getUserConfig({
                title: "詳細",
                width: "640px",
                items: configItems,
                callback: function (result) {
                    if (result) {
                        self.saveData(result.type);
                    }
                }
            });



        }, setGridDataSource: function (ds) {
            var self = this;
            var tempAfs = $.Enumerable.From(ds).OrderBy("Number($.ParentAnalysisCD)").ThenBy("$.Seq").ToArray();

            var ProjID;
            var AnalysisEntityName;
            var AnalysisKeyType;

            var pid = null;
            var seq = 1;
            var afs = [];
            for (var key in tempAfs) {
                var item = tempAfs[key];

                if (!ProjID) {
                    ProjID = item.ProjID;
                    AnalysisEntityName = item.AnalysisEntityName;
                    AnalysisKeyType = item.AnalysisKeyType;
                }

                item.id = item.AnalysisCD;
                item.parentId = Number(item.ParentAnalysisCD);
                if (item.parentId == 0) {
                    item.parentId = null;
                }
                if (!item.FieldDesc) {
                    item.FieldDesc = item.text;
                };

                if (!item.Title) item.hasChild = true;

                if (item.parentId != pid) {
                    pid = item.parentId;
                    seq = 1;
                }
                item.Seq = seq;
                seq++;

                afs.push(item);
            }


            self.grid.ui().setDataSource(afs);
            self.initGridEvent();

            self.grid.ui().delList = [];
            self.grid.ui().ProjID = ProjID;
            self.grid.ui().AnalysisEntityName = AnalysisEntityName;
            self.grid.ui().AnalysisKeyType = AnalysisKeyType;
            if (afs.length > 0) {
                self.grid.ui().uiControl.expand($(self.grid.ui().config.target).find("tbody>tr:eq(0)"));
            }

        }, delItem: function () {
            var self = this;
            var checkeds = this.grid.find("tbody").find("input:checked");

            $.each(checkeds, function () {
                self.grid.ui().delList.push($(this).attr("id").replace("_", ""));
                $(this).closest("tr").remove();
            })


        }, saveData: function (copyAnalysisKeyType) {
            var self = this;
            var projID = self.grid.ui().ProjID;
            var entityName = self.grid.ui().AnalysisEntityName;
            var AnalysisKeyType = self.grid.ui().AnalysisKeyType;

            /////
            var params = {};
            params.request = {};
            params.request.ProjID = projID;
            params.request.EntityName = "Y_AnalysisField";

            params.request.SaveData = new Array();

            smat.service.openLoding();

            if (copyAnalysisKeyType) {

                //del info
                var delList = self.grid.ui().delList;
                for (var key in delList) {
                    params.request.SaveData.push(
                        {
                            DyDelTableName: "Y_AnalysisField",
                            ProjID: projID,
                            AnalysisEntityName: entityName,
                            AnalysisKeyType: copyAnalysisKeyType
                        }
                    );
                }
            } else {

                //del info
                var delList = self.grid.ui().delList;
                for (var key in delList) {
                    params.request.SaveData.push(
                        {
                            DyDelTableName: "Y_AnalysisField",
                            ProjID: projID,
                            AnalysisEntityName: entityName,
                            AnalysisKeyType: AnalysisKeyType,
                            AnalysisCD: delList[key]
                        }
                    );
                }
            }

            var trs = this.grid.find("tbody").find("tr").not(".s-empty-row");

            $.each(trs, function () {
                var dataItem = self.grid.ui().dataItem($(this));
                params.request.SaveData.push({
                    DyTableName: "Y_AnalysisField"
                    , ProjID: dataItem.ProjID
                    , AnalysisEntityName: dataItem.AnalysisEntityName
                    , AnalysisKeyType: copyAnalysisKeyType ? copyAnalysisKeyType : dataItem.AnalysisKeyType
                    , AnalysisCD: dataItem.AnalysisCD
                    , ParentAnalysisCD: dataItem.parentId
                    , Seq: dataItem.Seq
                    , Alias: dataItem.Alias
                    , CharSet: dataItem.CharSet
                    , ControlType: dataItem.ControlType
                    , DataType: dataItem.DataType
                    , DefaultValue: dataItem.DefaultValue
                    , EntityName: dataItem.EntityName
                    , FieldDesc: dataItem.FieldDesc
                    , FieldName: dataItem.FieldName
                    , HideInView: dataItem.HideInView
                    , IdentitySql: dataItem.IdentitySql
                    , IsAvg: dataItem.IsAvg
                    , IsCount: dataItem.IsCount
                    , IsFilter: dataItem.IsFilter
                    , IsGroupBy: dataItem.IsGroupBy
                    , IsIdentity: dataItem.IsIdentity
                    , IsKey: dataItem.IsKey
                    , IsLogicItem: dataItem.IsLogicItem
                    , IsMax: dataItem.IsMax
                    , IsMin: dataItem.IsMin
                    , IsNullable: dataItem.IsNullable
                    , IsSum: dataItem.IsSum
                    , IsVirtual: dataItem.IsVirtual
                    , Length: dataItem.Length
                    , Memo: dataItem.Memo
                    , OptionSet: dataItem.OptionSet
                    , Path: dataItem.Path
                    , Precise: dataItem.Precise
                    , ProjID: dataItem.ProjID
                    , Title: dataItem.Title
                    , VirtualSql: dataItem.VirtualSql
                    , imageUrl: dataItem.imageUrl
                    , text: dataItem.text
                    , Format: dataItem.Format
                });
            });

            smat.service.loadJosnData({
                url: smat.global.basePath + smat.dynamics.commonURL.save,
                params: params,
                async: true,
                success: function (result) {
                    smat.service.notice({ msg: "saved!" })
                }
            });

        }, moveItemIndex: function (dataItem, step) {
            var tr = this.grid.find("#_" + dataItem.id).closest("tr");
            var pid = $(tr).find("[pid]");
            var pids = this.grid.find("tbody").find("[pid='" + dataItem.parentId + "']");
            var index = pids.index(pid);

            if (step > 0 && index != (pids.length - 1)) {
                var toTr = $(pids[index + 1]).closest("tr");

                var toDataItem = this.grid.ui().dataItem(toTr);

                toDataItem.Seq--;
                dataItem.Seq++;

                tr.detach();
                if (toDataItem.hasChild) {
                    var subPids = this.grid.find("tbody").find("[pid='" + toDataItem.id + "']")
                    var realTotr = $(subPids[subPids.length - 1]).closest("tr");
                    realTotr.after(tr);
                } else {
                    toTr.after(tr);
                }

                if (dataItem.hasChild) {
                    var subPids = this.grid.find("tbody").find("[pid='" + dataItem.id + "']");
                    var lastTr = tr;
                    $.each(subPids, function () {
                        lastTr.after($(this).closest("tr"));
                        lastTr = $(this).closest("tr");
                    })
                }

                tr.removeClass("slideInDown").removeClass("slideInUp").removeClass("animated");
                toTr.removeClass("slideInDown").removeClass("slideInUp").removeClass("animated");

                if (index == 0) {
                    toTr.find(".btn-up").hide();
                    toTr.find(".btn-down").css("margin-left", "1.8em");

                    tr.find(".btn-up").show();
                    tr.find(".btn-down").css("margin-left", ".16em");
                }

                if (index == (pids.length - 2)) {
                    tr.find(".btn-down").hide();
                    if (index > 0) {
                        toTr.find(".btn-down").show();
                    }
                }

                tr.addClass('animated slideInDown');
                toTr.addClass('animated slideInUp');
            }

            if (step < 0 && index != 0) {

                var toTr = $(pids[index - 1]).closest("tr");

                var toDataItem = this.grid.ui().dataItem(toTr);

                toDataItem.Seq++;
                dataItem.Seq--;

                tr.detach();
                toTr.before(tr);


                if (dataItem.hasChild) {
                    var subPids = this.grid.find("tbody").find("[pid='" + dataItem.id + "']");
                    var lastTr = tr;
                    $.each(subPids, function () {
                        lastTr.after($(this).closest("tr"));
                        lastTr = $(this).closest("tr");
                    })
                }

                tr.removeClass("slideInDown").removeClass("slideInUp").removeClass("animated");
                toTr.removeClass("slideInDown").removeClass("slideInUp").removeClass("animated");

                if (index == 1) {
                    tr.find(".btn-up").hide();
                    tr.find(".btn-down").css("margin-left", "1.8em");

                    toTr.find(".btn-up").show();
                    toTr.find(".btn-down").css("margin-left", ".16em");
                }

                if (index == (pids.length - 1)) {
                    tr.find(".btn-down").show();
                    if (index > 0) {
                        toTr.find(".btn-down").hide();
                    }
                }

                toTr.addClass('animated slideInDown');
                tr.addClass('animated slideInUp');
            }


        }, getAnalysisField: function () {
            var self = this;
            var projID = 1;
            var entityName = this.entityInput.ui().value();
            var AnalysisKeyType = this.searchType.ui().value();

            var params = {};
            params.request = {};
            params.request.ProjID = projID;

            params.request.DsRequests = new Array();

            params.request.DsRequests.push(
               {
                   TableName: "Y_AnalysisField",
                   Filter: "ProjID = " + projID + " and AnalysisEntityName = '" + entityName + "' and AnalysisKeyType = '" + AnalysisKeyType + "'"
               }
            );

            smat.service.loadJosnData({
                url: smat.dynamics.commonURL.getDyDs,
                params: params,
                async: true,
                success: function (result) {
                    self.setGridDataSource(result.ds["Y_AnalysisField"]);
                }

            });

        }
        , getTempAnalysisField: function () {

            var self = this;
            var projID = 1;
            var entityName = this.entityInput.ui().value();
            var AnalysisKeyType = this.searchType.ui().value();

            var entityData = smat.dynamics.service.getEntityFieldTreeData(projID, entityName);

            var fieldData = smat.dynamics.entityFieldTree(entityData, this.config.reType, function (item) { return self.dataFilter(item) });

            if (fieldData == null && entityData) {
                fieldData = {
                    text: entityData.EntityDesc,
                    imageUrl: "/SMAT.UI/images/folder.png",
                    items: []
                }
            }

            if (fieldData && AnalysisKeyType == "2") {

                fieldData.items.unshift(
                    {
                        ProjID: projID,
                        EntityName: entityName,
                        FieldName: "count",
                        Seq: 0,
                        text: "件数",
                        Title: "件数",
                        FieldDesc: "件数",
                        VirtualSql: "*"
                    }
                );

            }

            if (fieldData) {
                var keys = {};
                function markRemoveRepeatEntity(items, keys) {
                    for (var i in items) {
                        if (items[i].items) {
                            if (keys[items[i].EntityName + ":" + items[i].text]) {
                                items[i] = null;
                            } else {
                                keys[items[i].EntityName + ":" + items[i].text] = 1;
                                markRemoveRepeatEntity(items[i].items, keys);

                                var allNull = true;
                                for (var key in items[i].items) {
                                    if (items[i].items[key] != null) {
                                        allNull = false;
                                    }
                                }
                                if (allNull == true) {
                                    items[i] = null;
                                }
                            }
                        } else {
                            if (keys["Entity" + ":" + items[i].text]) {
                                items[i] = null;
                            } else {
                                keys["Entity" + ":" + items[i].text] = 1;
                            }
                        }
                    }
                }

                function removeRepeatEntity(item) {

                    for (var i = item.items.length - 1; i >= 0; i--) {
                        if (item.items[i] == null) {
                            item.items.splice(i, 1);
                        } else if (item.items[i].items) {
                            removeRepeatEntity(item.items[i]);
                        }
                    }

                }

                //markRemoveRepeatEntity(fieldData.items, keys);

                removeRepeatEntity(fieldData);

                var afs = [];

                var tempAnalysisCD = 1;
                afs.push({
                    ProjID: projID
                  , AnalysisEntityName: entityName
                  , AnalysisKeyType: AnalysisKeyType
                  , AnalysisCD: tempAnalysisCD++
                  , ParentAnalysisCD: "0"
                  , Seq: 1
                   , imageUrl: fieldData.imageUrl
                    , text: fieldData.text
                })

                function fillAfsdatas(afs, items, ParentAnalysisCD) {
                    for (var i in items) {
                        var AnalysisCD = tempAnalysisCD++;

                        if (items[i].items) {

                            afs.push({
                                ProjID: projID
                                 , AnalysisEntityName: entityName
                                 , AnalysisKeyType: AnalysisKeyType
                                 , AnalysisCD: AnalysisCD
                                 , ParentAnalysisCD: ParentAnalysisCD
                                 , Seq: (i + 1)
                                  , imageUrl: items[i].imageUrl
                                   , text: items[i].text
                            });
                            fillAfsdatas(afs, items[i].items, AnalysisCD);
                        } else {
                            if (items[i].text == "　") {
                                continue;
                            }

                            var entityList = self.entityInput.ui().config.dataSource;
                            var entitys = $.Enumerable.From(entityList).Where("$.EntityName == '" + items[i].EntityName + "' ").ToArray();

                            var Title = items[i].Title;
                            if (entitys.length > 0) {
                                Title = entitys[0].EntityDesc + "・" + items[i].Title;
                            }

                            afs.push({
                                ProjID: projID
                                , AnalysisEntityName: entityName
                                , AnalysisKeyType: AnalysisKeyType
                                , AnalysisCD: AnalysisCD
                                , ParentAnalysisCD: ParentAnalysisCD
                                , Seq: (i + 1)
                                , Alias: items[i].Alias
                                , CharSet: items[i].CharSet
                                , ControlType: items[i].ControlType
                                , DataType: items[i].DataType
                                , DefaultValue: items[i].DefaultValue
                                , EntityName: items[i].EntityName
                                , FieldDesc: items[i].FieldDesc
                                , FieldName: items[i].FieldName
                                , HideInView: items[i].HideInView
                                , IdentitySql: items[i].IdentitySql
                                , IsAvg: items[i].IsAvg
                                , IsCount: items[i].IsCount
                                , IsFilter: items[i].IsFilter
                                , IsGroupBy: items[i].IsGroupBy
                                , IsIdentity: items[i].IsIdentity
                                , IsKey: items[i].IsKey
                                , IsLogicItem: items[i].IsLogicItem
                                , IsMax: items[i].IsMax
                                , IsMin: items[i].IsMin
                                , IsNullable: items[i].IsNullable
                                , IsSum: items[i].IsSum
                                , IsVirtual: items[i].IsVirtual
                                , Length: items[i].Length
                                , Memo: items[i].Memo
                                , OptionSet: items[i].OptionSet
                                , Path: items[i].Path
                                , Precise: items[i].Precise
                                , ProjID: items[i].ProjID
                                , Title: Title
                                , VirtualSql: items[i].VirtualSql
                                , imageUrl: items[i].imageUrl
                                , text: items[i].text
                            })
                        }
                    }
                }
                fillAfsdatas(afs, fieldData.items, "1");

                self.setGridDataSource(afs);
            }
        }, dataFilter: function (item) {
            var self = this;
            if (self.config.dragType == "LogicItem") {
                //return item.IsLogicItem == true;
            }

            if (self.config.dragType == "GroupItem") {
                return item.IsGroupBy == true;
            }

            if (self.config.dragType == "SumItem") {

                if (item.IsSum || item.IsCount) {
                    if (!item.IsSum) {
                        item.text = item.text.replace("コード", "") + "数";
                        item.FieldDesc = item.FieldDesc.replace("コード", "") + "数";
                    }

                    return true;

                } else {
                    return false
                }


            }

            return true;
        }
    }

    // extend Node
    smat.globalObject.extend(smat.dynamics.ProjectManagerSearchSetting, smat.dynamics.Element);

})();